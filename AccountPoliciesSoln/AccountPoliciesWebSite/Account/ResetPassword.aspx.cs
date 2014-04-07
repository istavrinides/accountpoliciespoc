using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_ResetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            verifyAccount();
        }
    }

    private void verifyAccount()
    {
        lblVerified.Visible = false;
        lblCannotVerify.Visible = false;
        
        // Check if the cookie is set (not expired) and if the querystring is present
        if (Request.Cookies["ChallengeCookie"] == null || Request.QueryString["val"] == null)
        {
            lblCannotVerify.Visible = true;
        }
        else
        {
            try
            {
                // Get the cookie value
                String challenge = Request.Cookies["ChallengeCookie"].Value;

                // Get the query string value
                String encryptedEmailGuid = Request.QueryString["val"].ToString();

                // Decrypt the query string to get the email Guid and email address
                String emailGuidWithEmailAddress = EncyptionHelper.DecryptString(encryptedEmailGuid);

                // Email Guid and address
                String emailGuid = emailGuidWithEmailAddress.Substring(0, 36);
                String email = emailGuidWithEmailAddress.Substring(36, emailGuidWithEmailAddress.Length - 36);

                // Decrypt the cookie Guid
                String cookieGuid = EncyptionHelper.DecryptStringWithKey(challenge, emailGuid);

                Guid toParse = Guid.Empty;

                // Check if it is a valid guid
                if (!Guid.TryParse(cookieGuid, out toParse))
                {
                    lblCannotVerify.Visible = true;
                }
                else
                {
                    // If the user does not exist
                    if (Membership.GetUser(email) == null)
                        lblCannotVerify.Visible = true;
                    else
                    {
                        // Allow password reset
                        lblVerified.Visible = true;
                        pnlAdditonalInfo.Visible = true;
                    }
                }
            }
            catch
            {
                lblCannotVerify.Visible = true;
            }
        }
    }

    protected void ResetPasswordButton_Click(object sender, EventArgs e)
    {
        ErrorMessage.Visible = false;

        // Get the user's email address
        String encryptedEmailGuid = Request.QueryString["val"].ToString();

        String emailGuidWithEmailAddress = EncyptionHelper.DecryptString(encryptedEmailGuid);

        String emailGuid = emailGuidWithEmailAddress.Substring(0, 36);
        String email = emailGuidWithEmailAddress.Substring(36, emailGuidWithEmailAddress.Length - 36);

        try
        {
            // Get the user object
            MembershipUser user = Membership.GetUser(email);

            // Reset to a random password
            String oldPassword = user.ResetPassword();

            // Update the user object
            Membership.UpdateUser(user);

            // Change to the given password
            user.ChangePassword(oldPassword, Password.Text);

            // Expire the cookie value to invalidate the link
            HttpCookie cookie = new HttpCookie("ChallengeCookie")
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            Response.Cookies.Add(cookie);

            // Log in the user
            FormsAuthentication.SetAuthCookie(email, false);

            Response.Redirect("ChangePasswordSuccess.aspx");
        }
        catch (MembershipCreateUserException ex)
        {
            // We should only get invalid password error
            // But, it could also be a provider error
            switch (ex.StatusCode)
            {
                case MembershipCreateStatus.InvalidPassword:
                    ErrorMessage.Text = String.Format("Passwod must be at least {0} characters long and contain at least {1} special characters.", Membership.MinRequiredPasswordLength, Membership.MinRequiredNonAlphanumericCharacters);
                    break;
                case MembershipCreateStatus.ProviderError:
                    ErrorMessage.Text = String.Format("Could not create account. Please try registering again later.");
                    break;
            }

            ErrorMessage.Visible = true;
        }
    }
}