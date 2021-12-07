<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="manage-user.aspx.cs" Inherits="ASLPortfolioWebApp.admin.manage_user" %>

<%@ Import Namespace="ASLPortfolioWebApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtPath" onfocus="blur();" Style="visibility: hidden;" runat="server"></asp:TextBox>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Manage User</h3>
                </div>
                <div class="box-body col-12">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Name</label>
                                <asp:TextBox ID="txtName" autocomplete="off" class="form-control" placeholder="Enter User Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" TextMode="Email" autocomplete="off" class="form-control" placeholder="name@example.com" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>User Role</label>
                                <asp:DropDownList ID="ddlRole" class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Password</label>
                                <asp:TextBox ID="txtPass" autocomplete="off" class="form-control" placeholder="Password" TextMode="Password" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Profile Picture</label>
                                <asp:FileUpload ID="fileSignup" onchange="ImagePreview(this)" CssClass="form-control" accept=".png,.jpg,.jpeg,.giff" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnAdd" class="btn btn-primary" OnClick="btnAdd_OnClick" runat="server" Text="Create User" />
                            </div>
                        </div>
                        <div class="col-md-3"></div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gridUser" Width="100%" class="table table-hover table-bordered table-striped" OnPageIndexChanging="gridUser_OnPageIndexChanging" OnRowDataBound="gridUser_OnRowDataBound" AutoGenerateColumns="False" ShowHeader="True" ShowHeaderWhenEmpty="True" EmptyDataText="No Users Found" AllowPaging="True" PageSize="30" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="User_Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("UserId")%>' />
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RoleName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRole" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Picture">
                                        <ItemTemplate>
                                            <asp:Image ID="Image11" Width="100px" Height="100px" ImageUrl='<%#Eval("Picture")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate> 
                                            <asp:LinkButton ID="lnkRemove" class="btn btn-danger" OnClick="lnkRemove_OnClick" runat="server" ToolTip="Remove"><i class="fas fa-trash-alt fa-lg"></i></asp:LinkButton>
         
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
    <script>
        //function ImagePreview() {
        //    $("#ContentPlaceHolder1_lblPath").text($("#ContentPlaceHolder1_fileSignup").val());
        //}
        function ImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $("#ContentPlaceHolder1_txtPath").val(e.target.result);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
</asp:Content>
