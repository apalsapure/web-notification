using Notification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notification
{
    public partial class Compose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                drpCompose.DataSource = new List<string> { "Email", "Push Notification" };
                drpCompose.DataBind();

                drpEmailType.DataSource = new List<string> { "Raw", "Template" };
                drpEmailType.DataBind();

                drpPushToType.DataSource = new List<string> { "Channels", "Device Ids", "Query", "Broadcast" };
                drpPushToType.DataBind();
            }
        }

        protected void drpEmailType_SelectedIndexChanged(object sender, EventArgs e)
        {
            multiEmailType.ActiveViewIndex = drpEmailType.SelectedIndex;
        }

        protected void drpPushToType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPushTo.Visible = drpPushToType.SelectedIndex != 3;
        }

        protected void drpCompose_SelectedIndexChanged(object sender, EventArgs e)
        {
            multiView.ActiveViewIndex = drpCompose.SelectedIndex;
        }

        protected void btnSendPush_Click(object sender, EventArgs e)
        {
            var pushItem = new PushItem();
            pushItem.Badge = txtPushBadge.Text;
            pushItem.From = txtPushFrom.Text;
            pushItem.To = txtPushTo.Text;
            pushItem.ToType = drpPushToType.SelectedIndex;
            int expiry = 0;
            int.TryParse(txtPushExpiry.Text.Trim(), out expiry);
            pushItem.Expiry = expiry;
            pushItem.Message = txtPushMessage.Text;
            var message = pushItem.Save();
            if (string.IsNullOrEmpty(message))
            {
                lblPushMessage.CssClass = "alert-success";
                lblPushMessage.Text = "Push sent successfully.";
            }
            else
            {
                lblPushMessage.CssClass = "alert-danger";
                lblPushMessage.Text = message;
            }
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            var emailItem = new EmailItem();
            emailItem.From = txtEmailFrom.Text;
            emailItem.To = txtEmailTo.Text;
            emailItem.CC = txtEmailCC.Text;
            emailItem.Subject = txtEmailSubject.Text;
            if (multiEmailType.ActiveViewIndex == 0)
            {
                emailItem.Body = txtEmailMessage.Text;
            }
            else
            {
                emailItem.TemplateName = txtTemplateName.Text;
                emailItem.PlaceHolders = new Dictionary<string, string>();
                if (string.IsNullOrEmpty(txtHK1.Text.Trim()) == false) emailItem.PlaceHolders[txtHK1.Text.Trim()] = txtHV1.Text.Trim();
                if (string.IsNullOrEmpty(txtHK2.Text.Trim()) == false) emailItem.PlaceHolders[txtHK2.Text.Trim()] = txtHV2.Text.Trim();
                if (string.IsNullOrEmpty(txtHK3.Text.Trim()) == false) emailItem.PlaceHolders[txtHK3.Text.Trim()] = txtHV3.Text.Trim();
                if (string.IsNullOrEmpty(txtHK4.Text.Trim()) == false) emailItem.PlaceHolders[txtHK4.Text.Trim()] = txtHV4.Text.Trim();
                if (string.IsNullOrEmpty(txtHK5.Text.Trim()) == false) emailItem.PlaceHolders[txtHK5.Text.Trim()] = txtHV5.Text.Trim();
                if (string.IsNullOrEmpty(txtHK6.Text.Trim()) == false) emailItem.PlaceHolders[txtHK6.Text.Trim()] = txtHV6.Text.Trim();
            }
            var message = emailItem.Save();
            if (string.IsNullOrEmpty(message))
            {
                lblEmailMessage.CssClass = "alert-success";
                lblEmailMessage.Text = "Email sent successfully.";
            }
            else
            {
                lblEmailMessage.CssClass = "alert-danger";
                lblEmailMessage.Text = message;
            }
        }
    }
}