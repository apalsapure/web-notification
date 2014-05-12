using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appacitive.Sdk;
using System.Web.Security;

namespace Notification.Models
{
    public class ExceptionPolicy
    {
        public static bool HandleException(Exception ex)
        {
            //user in the context is null
            if (AppContext.UserContext.LoggedInUser == null) return LogoutUser();

            //check if exception occurred is an api error
            if (ex is AppacitiveApiException)
            {
                var appEx = ex as AppacitiveApiException;

                //user session expired
                if (appEx.Code == "19036")
                    return LogoutUser();

                return false;
            }
            else if (ex is AppacitiveRuntimeException)
            {
                return true;
            }
            else return false;
        }

        private static bool LogoutUser()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            return false;
        }
    }
}