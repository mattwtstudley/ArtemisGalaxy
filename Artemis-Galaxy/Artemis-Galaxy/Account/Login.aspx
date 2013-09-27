<%@ Page Title="Log in" Language="C#" MasterPageFile="~/AGHeader.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Artemis_Galaxy.Account.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="cphMainContent">
    <form id="frmLogin" runat="server">
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-body">
                    <section id="loginForm">
                        <h2>Log-in to Artemis Galaxy:</h2>
                        <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false">
                            <LayoutTemplate>
                                <p class="validation-summary-errors">
                                    <asp:Literal runat="server" ID="FailureText" />
                                </p>
                                <fieldset>
                                    <ol class="list-group">
                                        <li class="list-group-item">
                                            <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                            <asp:TextBox runat="server" ID="UserName" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                                        </li>
                                        <li class="list-group-item">
                                            <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                                            <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                        </li>
                                        <li class="list-group-item">
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                                </div>
                                                <div class="form-control">
                                                    Remember me?
                                                </div>
                                            </div>
                                        </li>
                                    </ol>
                                    <asp:Button runat="server" CommandName="Login" Text="Log in" />
                                </fieldset>
                            </LayoutTemplate>
                        </asp:Login>
                        <p>
                            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
                            if you don't have an account.
                        </p>
                    </section>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
