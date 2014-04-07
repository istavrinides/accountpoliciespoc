using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Configuration;
using System.Net;

public partial class Account_Login : System.Web.UI.Page
{
    #region "Properties"

    private LogDbDataLayer _dl
    {
        get
        {
            return new LogDbDataLayer();
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the IP is blacklisted
        // If so, do not allow any more attempts
        if (isIPBlackListed())
            Response.Redirect(@"~\NotAllowed.aspx");

        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }

    protected void LoginUser_OnLoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
    {
        // Get the user object
        var user = Membership.GetUser(LoginUser.UserName);

        // If it is a valid user
        // Delay the login based on the number of failed attempts
        if (user != null)
        {
            ProfileCommon _profile = (ProfileCommon)System.Web.Profile.ProfileBase.Create(LoginUser.UserName, true);

            System.Threading.Thread.Sleep(_profile.FailedLoginAttempts * 1000);
        }

    }

    protected void LoginUser_OnLogginError(object sender, EventArgs e)
    {
        // Get the user object
        var user = Membership.GetUser(LoginUser.UserName);

        // If it is valid user, increase the number of attempts and log the IP
        if (user != null)
        {
            // Only increase when not locked out (no sense in doing so when user is locked out)
            if (!user.IsLockedOut)
            {
                ProfileCommon _profile = (ProfileCommon)System.Web.Profile.ProfileBase.Create(LoginUser.UserName, true);

                _profile.FailedLoginAttempts++;
                _profile.Save();

                // Get the request IP Address
                _dl.increaseIPAddressInvalidAttempts(Request.UserHostAddress);
            }
        }
    }

    protected void LoginUser_OnAuthenticate(object sender, AuthenticateEventArgs e)
    {
        // Get the user object
        var user = Membership.GetUser(LoginUser.UserName);

        // If it is a valid user
        if (user != null)
        {
            // If the user correctly authenticated
            if (Membership.ValidateUser(LoginUser.UserName, LoginUser.Password))
            {
                ProfileCommon _profile = (ProfileCommon)System.Web.Profile.ProfileBase.Create(LoginUser.UserName, true);

                // Remove the IP from the (possible) blacklist
                _dl.removeIPAddressInvalidAttempts(Request.UserHostAddress);

                // If the user is currently locked out
                if (user.IsLockedOut)
                {
                    DateTime lockoutDateTime = user.LastLockoutDate;

                    // If the lock duration has been exceeded, unlock the user and reset the failed login attempts
                    if (lockoutDateTime.AddMinutes(int.Parse(WebConfigurationManager.AppSettings["LockoutDuration_Minutes"])) < DateTime.Now)
                    {
                        user.UnlockUser();

                        _profile.FailedLoginAttempts = 0;
                        _profile.Save();

                        // Set the auth cookie if remember me is selected
                        if(LoginUser.RememberMeSet)
                            FormsAuthentication.SetAuthCookie(LoginUser.UserName, false /* createPersistentCookie */);

                        e.Authenticated = true;
                    }
                    // If lockout duration has not been exceeded, add delay again
                    // don't lock-out and don't authenticate
                    else
                    {
                        System.Threading.Thread.Sleep(_profile.FailedLoginAttempts * 1000);
                    }
                }
                // If the user is not locked out, reset the failed login attempts
                // and return authenticated
                else
                {
                    _profile.FailedLoginAttempts = 0;
                    _profile.Save();

                    if (LoginUser.RememberMeSet)
                        FormsAuthentication.SetAuthCookie(LoginUser.UserName, false /* createPersistentCookie */);

                    e.Authenticated = true;
                }
            }
        }
        else
        {
            // Get the request IP Address and increase invalid attempts (log the IP)
            // Possible brute-force attempts
            logIP();

            Random r = new Random();
            System.Threading.Thread.Sleep(r.Next(10) * 1000);
        }
    }

    #region "Helpers"

    private void logIP()
    {
        _dl.increaseIPAddressInvalidAttempts(getIp());

    }

    private Boolean isIPBlackListed()
    {
        return _dl.checkIPAddressExceededAttempts(getIp());
    }

    private String getIp()
    {
        return Request.UserHostAddress;
    }

    #endregion
}
