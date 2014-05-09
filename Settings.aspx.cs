using Appacitive.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notification
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnSaveSettings_Click(object sender, EventArgs e)
        {
            var port = 0;
            int.TryParse(txtPort.Text, out port);

            var message = await Models.User.SaveSMTPSettings(txtUserName.Text, txtPassword.Text, txtHost.Text, port, chkSSL.Checked);
            if (string.IsNullOrEmpty(message))
            {
                ErrorMessage.CssClass = "alert-success";
                ErrorMessage.Text = "Settings saved successfully.";
            }
            else
            {
                ErrorMessage.CssClass = "alert-danger";
                ErrorMessage.Text = message;
            }
        }
    }
}