<%@ Page Title="Log in" EnableSessionState="True" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Notification.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="mbl" style="border-bottom: solid 1px #ddd;">
        <hgroup class="title">
            <h1><%: Title %>.</h1>
        </hgroup>
    </div>
    <div class="ptm">
        <p class="validation-summary-errors">
            <asp:Label ID="ErrorMessage" runat="server"></asp:Label>
        </p>
        <form class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="UserName">Email Address</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="Password">Password</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:CheckBox runat="server" ID="RememberMe" />
                    <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10 ptl pbm">
                    <asp:Button ID="btnLogin" runat="server" CssClass="form-control btn-primary" CommandName="Login" Text="Log in" OnClick="btnLogin_Click" />
                </div>
            </div>
        </form>

        <div class="col-sm-offset-2 col-sm-10 ptm pbm">
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" Style="text-decoration: underline">Register</asp:HyperLink>
            if you don't have an account.
        </div>
    </div>
</asp:Content>
