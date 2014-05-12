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
            if (ex is AppacitiveApiException)
            {
                var appEx = ex as AppacitiveApiException;

                //user session expired
                if (appEx.Code == "19036")
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }

                return false;
            }
            else if (ex is AppacitiveRuntimeException)
            {
                return true;
            }
            else return false;
        }
    }
}