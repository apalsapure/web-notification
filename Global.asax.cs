using Appacitive.Sdk.Internal;
using Notification.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Notification
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Initialize Appacitive SDK
            Appacitive.Sdk.AppContext.InitializeForAspnet(ConfigurationManager.AppSettings["app-id"], ConfigurationManager.AppSettings["api-key"],
                Appacitive.Sdk.Environment.Sandbox);

            //Map your model object to appacitive type
            Appacitive.Sdk.AppContext.Types.MapObjectType<User>("user");
            Appacitive.Sdk.AppContext.Types.MapObjectType<EmailItem>("email");
            Appacitive.Sdk.AppContext.Types.MapObjectType<PushItem>("push");
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
