<%@ Page Title="Notifications" Async="true" EnableSessionState="True" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Notification._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="updatePanel">
        <ProgressTemplate>
            <div class="loader">
                <div class="loader-content">
                    Loading...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-2 left-nav">
                        <div class="row">
                            <asp:LinkButton CssClass="col-md-12 active" ID="lnkEmails" runat="server" Text="Emails" OnClick="lnkEmails_Click" />
                        </div>
                        <div class="row">
                            <asp:LinkButton CssClass="col-md-12" ID="lnkPush" runat="server" Text="Push Notifications" OnClick="lnkPush_Click" />
                        </div>
                    </div>
                    <div class="col-md-10 right-container">
                        <asp:MultiView ID="emailMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="emailListView" runat="server">

                                <asp:GridView runat="server" ID="gridEmail" AutoGenerateColumns="false"
                                    CssClass="message-table" ShowHeader="false" CellSpacing="0" CellPadding="0" AllowPaging="true"
                                    PagerSettings-Mode="NextPreviousFirstLast" PagerStyle-HorizontalAlign="Right" PageSize="10" PagerSettings-Visible="true"
                                    OnRowDataBound="gridEmail_RowDataBound" AutoGenerateSelectButton="true" PagerStyle-CssClass="grid-pager" EmptyDataRowStyle-CssClass="grid-pager"
                                    OnSelectedIndexChanged="gridEmail_SelectedIndexChanged" OnPageIndexChanging="gridEmail_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Id" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="To" ItemStyle-CssClass="message-to ellipsis" />
                                        <asp:BoundField DataField="Subject" ItemStyle-CssClass="message-subject ellipsis" />
                                        <asp:BoundField DataField="Created" DataFormatString="{0:d}" ItemStyle-CssClass="message-timestamp ellipsis" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span class="no-record text-muted">No email record found.</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="emailDetailView" runat="server">
                                <table class="item-details">
                                    <tr>
                                        <td>
                                            <h3 id="lblEmailSubject" runat="server"></h3>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong id="lblEmailFrom" runat="server"></strong>
                                            <label id="lblEmailCreated" class="pull-right text-muted prl font-small" runat="server"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="text-muted">To: </label>
                                            <label id="lblEmailTo" class="text-muted" runat="server"></label>
                                        </td>
                                    </tr>
                                    <asp:MultiView ID="multiViewEmailType" runat="server">
                                        <asp:View ID="viewEmailBody" runat="server">
                                            <tr>
                                                <td>
                                                    <label id="lblEmailBody" runat="server" class="hidden"></label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <iframe id="iframeEmail"></iframe>
                                                </td>
                                            </tr>
                                        </asp:View>
                                        <asp:View ID="viewEmailTemplated" runat="server">
                                            <tr>
                                                <td>
                                                    <div class="ptm pbm">
                                                        <strong>Template Name</strong>
                                                        <br />
                                                        <asp:Label ID="lblTemplateName" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="pts pbm">
                                                        <strong>Place Holders</strong>
                                                        <br />
                                                        <asp:GridView ID="placeHolderGridView" runat="server" ShowHeader="false" AutoGenerateColumns="false"
                                                            CssClass="placeHolderGrid" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="90%">
                                                            <AlternatingRowStyle BackColor="#DCDCDC"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Key" ItemStyle-CssClass="ellipsis" />
                                                                <asp:BoundField DataField="Value" ItemStyle-CssClass="ellipsis" />
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <p style="margin-left: -12px;" class="text-muted">No place holders defined.</p>
                                                            </EmptyDataTemplate>
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>

                                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"></HeaderStyle>

                                                            <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>

                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black"></RowStyle>

                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                                                            <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                                                            <SortedAscendingHeaderStyle BackColor="#0000A9"></SortedAscendingHeaderStyle>

                                                            <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                                                            <SortedDescendingHeaderStyle BackColor="#000065"></SortedDescendingHeaderStyle>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </asp:View>
                                    </asp:MultiView>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                        <asp:MultiView ID="pushMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="pushListView" runat="server">
                                <asp:GridView runat="server" ID="gridPush" AutoGenerateColumns="false"
                                    CssClass="message-table" ShowHeader="false" CellSpacing="0" CellPadding="0" AllowPaging="true"
                                    PagerSettings-Mode="NextPreviousFirstLast" PagerStyle-HorizontalAlign="Right" PageSize="10" PagerSettings-Visible="true"
                                    OnRowDataBound="gridPush_RowDataBound" AutoGenerateSelectButton="true" PagerStyle-CssClass="grid-pager" EmptyDataRowStyle-CssClass="grid-pager"
                                    OnSelectedIndexChanged="gridPush_SelectedIndexChanged" OnPageIndexChanging="gridPush_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Id" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="To" ItemStyle-CssClass="message-to ellipsis" />
                                        <asp:BoundField DataField="Message" ItemStyle-CssClass="message-subject ellipsis" />
                                        <asp:BoundField DataField="Created" DataFormatString="{0:d}" ItemStyle-CssClass="message-timestamp ellipsis" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span class="no-record text-muted">No push record found.</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="pushDetailsView" runat="server">
                                <table class="item-details">
                                    <tr>
                                        <td>
                                            <div class="ptm">
                                                <strong id="lblPushFrom" runat="server"></strong>
                                                <label id="lblPushCreated" class="pull-right text-muted prl font-small" runat="server"></label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="text-muted">To: </label>
                                            <label id="lblPushTo" class="text-muted" runat="server"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="ptm pbm">
                                                <strong>Message</strong>
                                                <br />
                                                <label id="lblPushMessage" runat="server"></label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="pts pbm">
                                                <strong>Badge</strong>
                                                <br />
                                                <label id="lblPushBadge" runat="server"></label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="pts pbm">
                                                <strong>Expiry</strong>
                                                <br />
                                                <label id="lblExpiry" runat="server"></label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
