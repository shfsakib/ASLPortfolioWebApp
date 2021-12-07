<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="manage-role.aspx.cs" Inherits="ASLPortfolioWebApp.admin.manage_role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Manage Roles</h3>
                </div>
                <div class="box-body col-12">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Role Name</label>
                                <asp:TextBox ID="txtRoleName" autocomplete="off" class="form-control" placeholder="Super Admin,Admin...." runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnAdd" class="btn btn-primary" OnClick="btnAdd_OnClick" runat="server" Text="Save Role" />
                            </div>
                        </div>
                        <div class="col-md-3"></div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gridRole" Width="100%" class="table table-hover table-bordered table-striped" OnPageIndexChanging="gridRole_OnPageIndexChanging" OnRowDataBound="gridRole_OnRowDataBound" AutoGenerateColumns="False" ShowHeader="True" ShowHeaderWhenEmpty="True" EmptyDataText="No Role Found" AllowPaging="True" PageSize="30" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Role_Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenRoleId" runat="server" Value='<%#Eval("RoleId")%>' />
                                            <asp:Label ID="lblRoleName" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate> 
                                            <asp:LinkButton ID="lnkUpdate" class="btn btn-primary" OnClick="lnkUpdate_OnClick" runat="server" ToolTip="Update"><i class="fas fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRemove" class="btn btn-danger" OnClick="lnkRemove_OnClick" runat="server" ToolTip="Remove"><i class="fas fa-trash-alt"></i></asp:LinkButton>
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
</asp:Content>
