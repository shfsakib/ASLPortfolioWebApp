<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="manage-role-action.aspx.cs" Inherits="ASLPortfolioWebApp.admin.manage_role_action" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Manage Role Action</h3>
                </div>
                <div class="box-body col-12">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Role Name</label>
                                <asp:DropDownList ID="ddlRole" class="form-control select2" runat="server"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Actions</label>
                                <asp:CheckBox ID="chkInsert" CssClass="form-control" Text="&nbsp;&nbsp;Insert" runat="server" />
                                <asp:CheckBox ID="chkUpdate" CssClass="form-control" Text="&nbsp;&nbsp;Update" runat="server" />
                                <asp:CheckBox ID="chkDelete" CssClass="form-control" Text="&nbsp;&nbsp;Delete" runat="server" />
                                <asp:CheckBox ID="chkView" CssClass="form-control" Text="&nbsp;&nbsp;View" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnAdd" class="btn btn-primary" OnClick="btnAdd_OnClick" runat="server" Text="Save Role Action" />
                            </div>
                        </div>
                        <div class="col-md-3"></div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <asp:GridView ID="gridAction" Width="100%" class="table table-hover table-bordered table-striped" OnPageIndexChanging="gridAction_OnPageIndexChanging" AutoGenerateColumns="False" ShowHeader="True" ShowHeaderWhenEmpty="True" EmptyDataText="No Action Found" AllowPaging="True" PageSize="30" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Role_Name">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hiddenActionId" runat="server" Value='<%#Eval("ActionId")%>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Insert_Permission">
                                        <ItemTemplate>
                                              <asp:Label ID="lblInsert" runat="server" Text='<%#Eval("Insert")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update_Permission">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpdate" runat="server" Text='<%#Eval("Update")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete_Permission">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDelete" runat="server" Text='<%#Eval("Delete")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View_Permission">
                                        <ItemTemplate>
                                            <asp:Label ID="lblView" runat="server" Text='<%#Eval("View")%>'></asp:Label>
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
