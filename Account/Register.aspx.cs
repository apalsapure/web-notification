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
        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            var split = Name.Text.Split(' ').ToList();
            string firstName = split[0], lastName = "";
            if (split.Count > 0)
            {
                split.RemoveAt(0);
                lastName = string.Join(" ", split);
            }

            var message = await Models.User.CreateUser(Email.Text, Password.Text, Email.Text, firstName, lastName);
            if (string.IsNullOrEmpty(message))
            {
                FormsAuthentication.SetAuthCookie(Email.Text, createPersistentCookie: false);
                Response.Redirect("~/");
            }
            else ErrorMessage.Text = message;
        }
    }
}