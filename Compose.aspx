<%@ Page Language="C#" Title="Compose" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compose.aspx.cs" Inherits="Notification.Compose" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="composer">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel">
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
                        <div class="col-md-12 text-right" style="z-index: 1">
                            <strong>Compose :</strong>
                            <asp:DropDownList ID="drpCompose" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpCompose_SelectedIndexChanged" />
                        </div>
                    </div>
                </div>
                <asp:MultiView ID="multiView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="emailView" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <h2 class="pbm">Email Composer</h2>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="pbm">
                                                    <label class="text-muted">From</label>
                                                    <br />
                                                    <asp:TextBox ID="txtEmailFrom" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts pbm">
                                                    <label class="text-muted">To</label>
                                                    <br />
                                                    <asp:TextBox ID="txtEmailTo" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts pbm">
                                                    <label class="text-muted">Cc</label>
                                                    <br />
                                                    <asp:TextBox ID="txtEmailCC" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts">
                                                    <asp:Label ID="lblEmailMessage" runat="server" CssClass="alert-danger" Text="" />&nbsp;
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts pbm">
                                                    <asp:Button ID="btnSendEmail" OnClick="btnSendEmail_Click" runat="server" CssClass="btn-primary" Text="Send" Width="100" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-8 bg-white">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="border-bottom ptm pbm">
                                                <strong>Choose type of Email : </strong>
                                                <asp:DropDownList ID="drpEmailType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpEmailType_SelectedIndexChanged" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 pts pbm">
                                            <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="email-subject" placeholder="Add a Subject" />
                                        </div>
                                    </div>
                                    <asp:MultiView ID="multiEmailType" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="viewEmailRaw" runat="server">
                                            <%--Raw Email--%>
                                            <div class="row">
                                                <div class="col-md-12 pbm">
                                                    <asp:TextBox TextMode="MultiLine" ID="txtEmailMessage" CssClass="email-body" runat="server" Rows="30" placeholder="Add a message here" />
                                                </div>
                                            </div>
                                        </asp:View>
                                        <asp:View ID="viewEmailTemplate" runat="server">
                                            <%--Template Email--%>
                                            <div class="row">
                                                <div class="col-md-12 pts">
                                                    <div class="pts pbm">
                                                        <label class="text-muted">Template Name</label>
                                                        <br />
                                                        <asp:TextBox ID="txtTemplateName" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="pts">
                                                        <label class="text-muted">Place Holders</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row pbm">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK1" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV1" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                            <div class="row pbm">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK2" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV2" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                            <div class="row pbm">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK3" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV3" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                            <div class="row pbm">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK4" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV4" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                            <div class="row pbm">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK5" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV5" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                            <div class="row pbl">
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHK6" runat="server" placeholder="Key" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtHV6" runat="server" placeholder="Value" />
                                                </div>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="pushView" runat="server">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
                                    <h2 class="pbm">Push Notification</h2>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <table>
                                        <tr>
                                            <td>
                                                <div class="pbm">
                                                    <label class="text-muted">From</label>
                                                    <br />
                                                    <asp:TextBox ID="txtPushFrom" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts pbm">
                                                    <label class="text-muted">To : </label>
                                                    <br />
                                                    <asp:DropDownList ID="drpPushToType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPushToType_SelectedIndexChanged" Style="margin-left: 0px!important"></asp:DropDownList>
                                                    <br />
                                                    <asp:TextBox ID="txtPushTo" runat="server" CssClass="mts" placeholder="Comma seperated values"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts">
                                                    <asp:Label ID="lblPushMessage" runat="server" CssClass="alert-danger" />&nbsp;
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="pts pbm">
                                                    <asp:Button ID="btnSendPush" OnClick="btnSendPush_Click" runat="server" CssClass="btn-primary" Text="Send" Width="100" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-md-8 bg-white">
                                    <div class="row">
                                        <div class="col-md-12 pts pbm">
                                            <asp:TextBox ID="txtPushMessage" runat="server" CssClass="email-subject" placeholder="Add a Message" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 pts">
                                            <div class="pts pbm">
                                                <label class="text-muted">Badge</label>
                                                <br />
                                                <asp:TextBox ID="txtPushBadge" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 pts">
                                            <div class="pts pbl mbl">
                                                <label class="text-muted">Expiry Duration (seconds)</label>
                                                <br />
                                                <asp:TextBox ID="txtPushExpiry" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

