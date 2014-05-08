using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using Notification.Models;

namespace Notification.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        protected void RegisterUser_CreatingUser(object sender, LoginCancelEventArgs e)
        {
            TextBox TextboxfirstName = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("FirstName");
            TextBox TextboxLastName = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("LastName");
            TextBox TextboxName = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Name");
            TextBox TextboxEmail = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Email");
            TextBox TextboxPassword = (TextBox)RegisterUser.CreateUserStep.ContentTemplateContainer.FindControl("Password");

            MembershipCreateStatus status;
            AppacitiveMembershipProvider mp = (AppacitiveMembershipProvider)Membership.Provider;
            var split = TextboxName.Text.Split(' ').ToList();
            string firstName = split[0], lastName = "";
            if (split.Count > 0)
            {
                split.RemoveAt(0);
                lastName = string.Join(" ", split);
            }

            mp.CreateUser(TextboxEmail.Text, TextboxPassword.Text, TextboxEmail.Text, firstName, lastName, out status);
        }
    }
}