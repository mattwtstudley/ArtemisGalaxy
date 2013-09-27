<%@ Page Title="" Language="C#" MasterPageFile="~/AGHeader.Master" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="Artemis_Galaxy.Account.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHtmlHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="server">
    <form id="Form1" runat="server">
        <div class="container">
            <div class="panel panel-default">
                <div class="panel-body">
                    <section id="profileEditForm">
                        <h2>Edit User Profile</h2>
                        <ol class="list-group">
                            <li class="list-group-item">
                                <asp:Label runat="server">Character Name:</asp:Label>
                                <asp:TextBox runat="server" ID="tbCharacterName"></asp:TextBox>
                            </li>
                            <li class="list-group-item">
                                <asp:Label runat="server">Time Zone:</asp:Label>
                                <asp:DropDownList ID="ddlTimezone" runat="server">
                                </asp:DropDownList>
                            </li>
                            <li class="list-group-item">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Customize Avatar</div>
                                    <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-4">
                                                        <asp:Label ID="Label1" runat="server">Avatar:</asp:Label>
                                                        <br />
                                                        <asp:Image ID="imgProfileAvatar" ImageUrl="~/img/Avatars/NoAvatar.png" runat="server" Width="128" Height="128" />
                                                    </div>
                                                    <div class="col-lg-4 ">
                                                        <div class="panel panel-info">
                                                            <div class="panel-heading">
                                                                Avatar rules:
                                                            </div>
                                                            <div class="panel-body">
                                                                Avatars should be 128x128 and can be ing png or jpg formats. 
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>          
                                                <div class="row">
                                                    <div class="btn-group">
                                                        <div class="col-lg-8">
                                                            <asp:FileUpload ID="fuAvatarUpload" runat="server" />
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <asp:Button ID="btnUploadNewAvatar" runat="server" Text="Upload new Avatar" />
                                                        </div>
                                                    </div>
                                                </div>
                                    </div>
                                </div>
                            </li>
                            <li class="list-group-item">
                                <asp:Button runat="server" ID="btnSaveChanges" Text="Save Changes" />
                            </li>
                        </ol>
                    </section>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
