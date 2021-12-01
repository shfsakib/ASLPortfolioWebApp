<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="update-role-action.aspx.cs" Inherits="ASLPortfolioWebApp.admin.update_role_action" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:updatepanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Update Role Action</h3>
                </div>
                <div class="box-body col-12">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Role Name</label>
                                <asp:DropDownList ID="ddlRole" disabled class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Actions</label>
                                <asp:CheckBox ID="chkInsert" CssClass="form-control" Text="&nbsp;&nbsp;Insert" runat="server" />
                                <asp:CheckBox ID="chkUpdate" CssClass="form-control" Text="&nbsp;&nbsp;Update" runat="server" />
                                <asp:CheckBox ID="chkDelete" CssClass="form-control" Text="&nbsp;&nbsp;Delete" runat="server" />
                                <asp:CheckBox ID="chkView" CssClass="form-control" Text="&nbsp;&nbsp;View" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_OnClick" runat="server" Text="Update Role Action" />
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
