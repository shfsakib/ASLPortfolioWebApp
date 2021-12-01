<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="manage-portfolio.aspx.cs" Inherits="ASLPortfolioWebApp.admin.manage_portfolio" %>

<%@ Import Namespace="ASLPortfolioWebApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtPath" onfocus="blur();" Style="visibility: hidden;" runat="server"></asp:TextBox>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border col-12">
                    <h3>Manage Portfolio</h3>
                </div>
                <div class="box-body col-12">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <% function func = function.GetInstance();
                                if (func.InsertPermission())
                                { %>
                            <div class="form-group">
                                <label>Title</label>
                                <asp:TextBox ID="txtTile" autocomplete="off" class="form-control" placeholder="Enter Title Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="100px" autocomplete="off" class="form-control" placeholder="Portfolio Description" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Portfolio Cover Picture</label>
                                <asp:FileUpload ID="fileSignup" onchange="ImagePreview(this)" CssClass="form-control" accept=".png,.jpg,.jpeg,.giff" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnAdd" class="btn btn-primary" OnClick="btnAdd_OnClick" runat="server" Text="Create Portfolio" />
                            </div>
                            <% } %>
                        </div>
                        <div class="col-md-3"></div>

                    </div>
                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <% if (func.ViewPermission())
                                { %>
                            <asp:GridView ID="gridPortfolio" Width="100%" class="table table-hover table-bordered table-striped" OnRowDataBound="gridPortfolio_OnRowDataBound" OnPageIndexChanging="gridPortfolio_OnPageIndexChanging" AutoGenerateColumns="False" ShowHeader="True" ShowHeaderWhenEmpty="True" EmptyDataText="No Portfolio Found" AllowPaging="True" PageSize="30" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Portfolio_Title">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="id" runat="server" Value='<%#Eval("PortfolioId")%>' />
                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Title")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" Style="display: block; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Picture">
                                        <ItemTemplate>
                                            <asp:DataList ID="dataImage" runat="server" RepeatColumns="3">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image11" Width="50px" Height="50px" ImageUrl='<%#Eval("Picture")%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <%function func = function.GetInstance();
                                                if (func.ViewPermission())
                                                { %>
                                            <asp:LinkButton ID="lnkView" class="btn btn-primary mt-1" OnClick="lnkView_OnClick" runat="server" ToolTip="View"><i class="fas fa-eye fa-lg"></i></asp:LinkButton>
                                            <% } %>
                                            <% if (func.UpdatePermission())
                                                { %>
                                            <asp:LinkButton ID="lnkUpdate" class="btn btn-success mt-1" OnClick="lnkUpdate_OnClick" runat="server" ToolTip="Update"><i class="fas fa-edit fa-lg"></i></asp:LinkButton>
                                            <% } %>
                                            <% if (func.DeletePermission())
                                                { %>
                                            <asp:LinkButton ID="lnkRemove" class="btn btn-danger mt-1" OnClick="lnkRemove_OnClick" runat="server" ToolTip="Remove"><i class="fas fa-trash-alt fa-lg"></i></asp:LinkButton>
                                            <% } %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <% } %>
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
