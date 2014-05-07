<%@ Page Language="C#" Title="SMTP Settings" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Notification.Settings" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container-fluid composer ">
        <div class="row">
            <div class="col-md-12">
                <h2 style="margin-top: 5px">SMTP Settings</h2>
            </div>
        </div>
        <table>
            <tr>
                <td>
                    <div class="pbm">
                        <label class="text-muted">User Name</label>
                        <br />
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pts pbm">
                        <label class="text-muted">Password</label>
                        <br />
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pts pbm">
                        <label class="text-muted">SMTP Host</label>
                        <br />
                        <asp:TextBox ID="txtHost" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pts pbm">
                        <label class="text-muted">SMTP Port</label>
                        <br />
                        <asp:TextBox ID="txtPort" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="pts pbm">
                        <asp:CheckBox ID="chkSSL" runat="server" Text="Use SSL"></asp:CheckBox>
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
                        <asp:Button ID="btnSaveSettings" OnClick="btnSaveSettings_Click" runat="server" CssClass="btn-primary" Text="Save" Width="100" />
                    </div>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>

