<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="ASLPortfolioWebApp.admin.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <div class="box">
                <div class="box-header with-border">
                    <h3>Profile</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4" style="text-align: center">
                            <asp:Image ID="profileImage" runat="server" ImageUrl="/Link/user.png" class="rounded-circle" Width="175px" Height="175px" alt="profile_image" Style="border: 1px solid black; border-radius: 50%;" />
                            <br />
                            <br />
                            <asp:FileUpload ID="fileProfile" class="form-control" onchange="ImagePreview(this)" Visible="False" accept=".png,.jpg,.jpeg" runat="server" />
                        </div>
                        <div class="col-md-4"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4" style="text-align: center">
                            <br />
                            <asp:Label ID="nameLabel" runat="server" Style="font-family: comic sans ms; font-size: 20px;" Text="Mr. xyz"></asp:Label>
                            <asp:TextBox ID="txtName" CssClass="form-control wd" Visible="False" placeholder="Enter Full Name" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4" style="text-align: center">
                            <br />
                            <a id="btnEdit" runat="server" class="btn btn-primary" style="color: white;" title="Edit" onserverclick="btnEdit_ServerClick"><i class="fas fa-edit"></i>&nbsp;&nbsp;Edit Profile</a>
                            <asp:LinkButton ID="lnkUpdate" Visible="False" CssClass="btn btn-success" OnClick="lnkUpdate_OnClick" runat="server"><i class="fas fa-check"></i>&nbsp;&nbsp;Update</asp:LinkButton>
                            <br />
                        </div>
                        <div class="col-md-4"></div>
                    </div>
                    <div class="box-header with-border"></div>
                    <div class="row box-body">
                        <div class="col-md-2">Email :</div>
                        <div class="col-md-4">
                            <asp:Label ID="lblEmail" runat="server" Text="name@example.com"></asp:Label>
                            <asp:TextBox ID="txtEmail" CssClass="form-control wd" Visible="False" placeholder="name@exmaple.com" autocomplete="off" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">Password :</div>
                        <div class="col-md-4">
                            <asp:Label ID="lblPass" runat="server" Text="********"></asp:Label>
                            <asp:TextBox ID="txtPass" CssClass="form-control wd" Visible="False" placeholder="********" autocomplete="off" runat="server"></asp:TextBox>
                        </div>
                        <asp:TextBox ID="txtPath" onfocus="blur();" Style="visibility: hidden;" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkUpdate" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#<%=profileImage.ClientID%>").attr('src', e.target.result);
                    $("#ContentPlaceHolder1_txtPath").val(e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
                }
            }
    </script>
</asp:Content>
