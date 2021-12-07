<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="update-role.aspx.cs" Inherits="ASLPortfolioWebApp.admin.update_role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:updatepanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Update Roles</h3>
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
                                <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_OnClick" runat="server" Text="Update Role" />
                                <a class="btn btn-warning" href="/admin/manage-role.aspx">Back Manage Role</a>
                            </div>
                        </div>
                        <div class="col-md-3"></div> 
                    </div> 
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:updatepanel>
</asp:Content>
