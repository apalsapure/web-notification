using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notification.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            var message = await Models.User.Authenticate(UserName.Text, Password.Text);
            if (string.IsNullOrEmpty(message))
            {
                FormsAuthentication.RedirectFromLoginPage(UserName.Text, true);
            }
            else ErrorMessage.Text = message;
        }
    }
}