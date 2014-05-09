using Notification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notification
{
    public partial class _Default : PageBase
    {
        private const int _pageSize = 15;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                gridEmail.PageSize = _pageSize;
                gridEmail.DataSource = await EmailItem.LoadData(gridEmail.PageIndex, gridEmail.PageSize);
                gridEmail.DataBind();

                gridPush.PageSize = _pageSize;
            }
        }

        protected async void gridEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmail.PageIndex = e.NewPageIndex;
            gridEmail.DataSource = await EmailItem.LoadData(e.NewPageIndex, gridEmail.PageSize);
            gridEmail.DataBind();
        }

        protected async void gridEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridEmail.SelectedIndex == -1) return;
            emailMultiView.ActiveViewIndex = 1;

            var selectedItemId = gridEmail.SelectedRow.Cells[1].Text;
            var selectedItem = await EmailItem.Fetch(selectedItemId);
            lblEmailSubject.InnerText = selectedItem.Subject;
            lblEmailTo.InnerText = selectedItem.To;
            lblEmailFrom.InnerText = selectedItem.From;
            lblEmailCreated.InnerText = selectedItem.CreatedStr;

            if (string.IsNullOrEmpty(selectedItem.TemplateName))
            {
                multiViewEmailType.ActiveViewIndex = 0;
                lblEmailBody.InnerHtml = selectedItem.Body;
                ScriptManager.RegisterClientScriptBlock(updatePanel, updatePanel.GetType(), Guid.NewGuid().ToString(), "$('#iframeEmail').contents().find('body').html($('#MainContent_lblEmailBody').html());$('#iframeEmail').height($('#iframeEmail').contents().find('body').height() + 30);", true);
            }
            else
            {
                multiViewEmailType.ActiveViewIndex = 1;
                lblTemplateName.Text = selectedItem.TemplateName;
                placeHolderGridView.DataSource = selectedItem.PlaceHolders.ToList();
                placeHolderGridView.DataBind();
            }

            gridEmail.SelectedIndex = -1;
        }

        protected void gridEmail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Hiding the Select Button Cell in Header Row.
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Hiding the Select Button Cells showing for each Data Row. 
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

                // Attaching one onclick event for the entire row, so that it will
                // fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(this.gridEmail, "Select$" + e.Row.RowIndex);
            }
        }

        protected async void gridPush_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPush.PageIndex = e.NewPageIndex;
            gridPush.DataSource = await PushItem.LoadData(e.NewPageIndex, gridPush.PageSize);
            gridPush.DataBind();
        }

        protected async void gridPush_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridPush.SelectedIndex == -1) return;
            pushMultiView.ActiveViewIndex = 1;

            var selectedItemId = gridPush.SelectedRow.Cells[1].Text;
            var selectedItem = await PushItem.Fetch(selectedItemId);
            lblPushMessage.InnerText = selectedItem.Message;
            lblPushTo.InnerText = selectedItem.To;
            lblPushFrom.InnerText = selectedItem.From;
            lblPushCreated.InnerText = selectedItem.CreatedStr;
            lblExpiry.InnerHtml = selectedItem.Expiry > 0 ? selectedItem.Expiry.ToString() : "-";
            lblPushBadge.InnerText = selectedItem.Badge == null ? "-" : selectedItem.Badge;

            gridPush.SelectedIndex = -1;
        }

        protected void gridPush_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // Hiding the Select Button Cell in Header Row.
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Hiding the Select Button Cells showing for each Data Row. 
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Display, "none");

                // Attaching one onclick event for the entire row, so that it will
                // fire SelectedIndexChanged, while we click anywhere on the row.
                e.Row.Attributes["onclick"] =
                  ClientScript.GetPostBackClientHyperlink(this.gridPush, "Select$" + e.Row.RowIndex);
            }
        }

        protected async void lnkEmails_Click(object sender, EventArgs e)
        {
            lnkEmails.CssClass += " active";
            lnkPush.CssClass = lnkPush.CssClass.Replace("active", string.Empty).Trim();
            emailMultiView.Visible = true;
            emailMultiView.ActiveViewIndex = 0;
            pushMultiView.Visible = false;

            gridEmail.DataSource = await EmailItem.LoadData(gridEmail.PageIndex, gridEmail.PageSize);
            gridEmail.DataBind();
        }

        protected async void lnkPush_Click(object sender, EventArgs e)
        {
            lnkPush.CssClass += " active";
            lnkEmails.CssClass = lnkPush.CssClass.Replace("active", string.Empty).Trim();
            emailMultiView.Visible = false;
            pushMultiView.ActiveViewIndex = 0;
            pushMultiView.Visible = true;

            gridPush.DataSource = await PushItem.LoadData(gridPush.PageIndex, gridPush.PageSize);
            gridPush.DataBind();
        }
    }
}