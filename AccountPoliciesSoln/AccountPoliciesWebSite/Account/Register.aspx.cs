using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Account_Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CreateUserButton_Click(object sender, EventArgs e)
    {
        // Create two Guids
        Guid emailGuid = Guid.NewGuid();
        Guid cookieGuid = Guid.NewGuid();

        // Use the emailGuid to encrypt the cookie guid
        String encryptedCookieGuid = EncyptionHelper.EncryptStringWithKey(cookieGuid.ToString(), emailGuid.ToString());

        // Set the encrypted emailGuid in a cookie
        HttpCookie cookie = new HttpCookie("ChallengeCookie")
        {
            Value = encryptedCookieGuid,
            Expires = DateTime.Now.AddMinutes(10)
        };
        Response.Cookies.Add(cookie);

        // Encrypt the emailGuid (concatenate the email address to that)
        String encryptedEmailGuid = EncyptionHelper.EncryptString(emailGuid.ToString() + Email.Text);

        // Create the email body
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("A request for creating a user in our system has been received.");
        sb.AppendLine("");
        sb.AppendLine("To complete the request and create your user account, please click on the following link:");
        sb.AppendLine("");
        sb.AppendLine(String.Format("<a href=\"{0}://{1}/Account/VerifyAccount.aspx?val={2}\">Verify Account</a>", 
            HttpContext.Current.Request.Url.Scheme, 
            "localhost:51032/AccountPoliciesWebSite", 
            encryptedEmailGuid));
        sb.AppendLine("");
        sb.AppendLine(String.Format("Please note that the link will expire on {0}", DateTime.Now.AddMinutes(10).ToString()));
        sb.AppendLine("");
        sb.AppendLine("If you have received this email without trying to create an account on our site, please ignore it.");

        // Send the email
        EmailHelper.send(Email.Text, "Registration Request", sb.ToString());

        Response.Redirect("EmailSent.aspx");
    }
}
