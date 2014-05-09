<%@ Page Title="New Register Registration" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Notification.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="mbl" style="border-bottom: solid 1px #ddd;">
        <hgroup class="title">
            <h1><%: Title %></h1>
        </hgroup>
    </div>

    <div class="ptm">
        <p class="validation-summary-errors">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>
        <form class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 control-label" AssociatedControlID="Name">Name</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="Name" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Name"
                        CssClass="field-validation-error" ErrorMessage="The name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-sm-2 control-label">Email address</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="Email" TextMode="Email" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                        CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-sm-2 control-label">Password</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                        CssClass="field-validation-error" ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-sm-2 control-label">Confirm password</asp:Label>
                <div class="col-sm-10">
                    <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                        CssClass="field-validation-error" Display="Static" ErrorMessage="The confirm password field is required." />
                    <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                        CssClass="field-validation-error" Display="Static" ErrorMessage="The password and confirmation password do not match." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10 pts">
                    <asp:Button runat="server" CssClass="form-control btn-primary" CommandName="MoveNext" Text="Register" ID="btnRegister" OnClick="btnRegister_Click" />
                </div>
            </div>
        </form>
    </div>
</asp:Content>
