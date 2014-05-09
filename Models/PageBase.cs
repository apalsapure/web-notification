using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Notification.Models
{
    public class PageBase : Page
    {
        protected override void OnPreRender(EventArgs e)
        {
            if (App.UserContext.IsLoggedIn == false) FormsAuthentication.RedirectToLoginPage();
            base.OnPreRender(e);
        }
    }
}