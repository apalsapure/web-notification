using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Notification.Models
{
    public class ExceptionPolicy
    {
        public static bool HandleException(Exception ex)
        {
            //TODO
            return false;
        }

        private static bool LogoutUser()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            return false;
        }
    }
}