<%@ Page Language="C#" Async="true" Title="SMTP Settings" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Notification.Settings" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="container-fluid composer ">
        <div class="row">
            <div class="col-md-12">
                <h2 style="margin-top: 5px">SMTP Settings</h2>
            </div>
        </div>
        <p class="validation-summary-errors">
            <asp:Label runat="server" ID="ErrorMessage" />
        </p>
        <table>
            <tr>
                <td>
                    <div class="ptm">
                        <label class="text-muted">User Name</label>
                        <br />
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <label class="text-muted">Password</label>
                        <br />
                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <label class="text-muted">SMTP Host</label>
                        <br />
                        <asp:TextBox ID="txtHost" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHost" CssClass="field-validation-error" ErrorMessage="The SMTP Host field is required." />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <label class="text-muted">SMTP Port</label>
                        <br />
                        <asp:TextBox ID="txtPort" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPort" CssClass="field-validation-error" ErrorMessage="The SMTP Port field is required." />
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPort" ValidationExpression="\d+" CssClass="field-validation-error" ErrorMessage="The SMTP Port field should be a number." />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <asp:CheckBox ID="chkSSL" runat="server" Text="&nbsp;Use SSL"></asp:CheckBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <asp:Label ID="lblEmailMessage" runat="server" CssClass="alert-danger" Text="" />&nbsp;
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="">
                        <asp:Button ID="btnSaveSettings" OnClick="btnSaveSettings_Click" runat="server" CssClass="btn-primary" Text="Save" Width="100" />
                    </div>
                </td>
            </tr>
        </table>
    </div>


</asp:Content>

