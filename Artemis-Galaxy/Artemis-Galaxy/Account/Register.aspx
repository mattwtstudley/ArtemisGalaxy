<%@ Page Title="Register" Language="C#" MasterPageFile="~/AGHeader.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Artemis_Galaxy.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="cphMainContent">
    <form runat="server">
        <div class="container">
            <div class="page-header">
                <h1>Enlist!</h1>
            </div>
            <div class="alert alert-info">Passwords are required to be a minimum of <%: Membership.MinRequiredPasswordLength %> characters in length.</div>
            <div class="panel panel-default">
                <div class="panel-body">
                    <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser">
                        <LayoutTemplate>
                            <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
                            <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
                        </LayoutTemplate>
                        <WizardSteps>
                            <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                                <ContentTemplate>
                                    <p class="validation-summary-errors">
                                        <asp:Literal runat="server" ID="ErrorMessage" />
                                    </p>

                                    <fieldset>
                                        <legend>Create an account for Artemis Galaxy</legend>
                                        <ol class="list-group">
                                            <li class="list-group-item">
                                                <div class="input-group">
                                                    <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">User Name</asp:Label>
                                                    <asp:TextBox runat="server" ID="UserName" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                                        CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                                                </div>
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Email">Email address</asp:Label>
                                                <asp:TextBox runat="server" ID="Email" TextMode="Email" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email"
                                                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="Label3" runat="server" AssociatedControlID="Password">Password</asp:Label>
                                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Password"
                                                    CssClass="field-validation-error" ErrorMessage="The password field is required." />
                                            </li>
                                            <li class="list-group-item">
                                                <asp:Label ID="Label4" runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
                                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ConfirmPassword"
                                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                                            </li>
                                        </ol>
                                        <asp:Button ID="Button1" runat="server" CommandName="MoveNext" Text="Register" />
                                    </fieldset>
                                </ContentTemplate>
                                <CustomNavigationTemplate />
                            </asp:CreateUserWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </div>
            </div>
        </div>


    </form>
</asp:Content>
