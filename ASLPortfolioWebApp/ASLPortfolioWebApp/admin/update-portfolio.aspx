<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="update-portfolio.aspx.cs" Inherits="ASLPortfolioWebApp.admin.update_portfolio" %>
<%@ Import Namespace="ASLPortfolioWebApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtPath" onfocus="blur();" Style="visibility: hidden;" runat="server"></asp:TextBox>
    <asp:updatepanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border">
                    <h3>Update Portfolio</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-8 table-responsive text-center">
                            <asp:GridView ID="gridPhotos" class="table table-bordered table-striped " runat="server" OnPageIndexChanging="gridPhotos_OnPageIndexChanging" OnRowCommand="gridPhotos_OnRowCommand" OnRowEditing="gridPhotos_OnRowEditing" OnRowUpdating="gridPhotos_OnRowUpdating" OnRowDeleting="gridPhotos_OnRowDeleting" OnRowCancelingEdit="gridPhotos_OnRowCancelingEdit" AutoGenerateColumns="False" ShowHeader="True" EmptyDataText="No Photos Found" ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="10">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <table class="" width="100%">
                                                <tr>
                                                    <th style="text-align: center">Choose_Picture</th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                      <% if (gridPhotos.Rows.Count < 9)
                                                            { %>
                                                        <asp:FileUpload onchange="ImagePreview(this)" ID="FileUpload" accept=".png,.jpg,.jpeg" runat="server" />
                                                        <% }
                                                            else
                                                            { %>
                                                        <h4>You can't upload more than 9 photos</h4>
                                                        <% } %>  

                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%ASLPortfolioWebApp.function func = function.GetInstance(); %>
                                            <% if (func.ViewPermission())
                                                { %>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("PicId") %>' />
                                            <img id="imgPortfolio" src='<%# Eval("Picture")%>' alt="photo" style="border: 2px solid black; cursor: pointer; width: 50px; height: 50px; border: 2px solid white;" />

                                            <% } %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("PicId") %>' />
                                            <asp:FileUpload ID="editFileUpload" onchange="ImagePreview(this)" accept=".png,.jpg,.jpeg" runat="server" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemStyle Width="20%"></ItemStyle>
                                        <HeaderTemplate>
                                            <table class="" width="100%">
                                                <tr>
                                                    <th>
                                                        <label>Action</label>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                            <%ASLPortfolioWebApp.function funct = function.GetInstance(); %>
                                                       
                                                        <% if (gridPhotos.Rows.Count < 9)
                                                            { %>
                                                        <% if (funct.InsertPermission())
                                                            { %>
                                                        <asp:LinkButton ID="addNewButton1" runat="server" class="btn btn-primary" CommandName="AddNew" ToolTip="Add"><i class="fas fa-plus"></i></asp:LinkButton>
                                                        <% } %>
                                                       
                                                        <% } %>

                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%ASLPortfolioWebApp.function function = function.GetInstance(); %>
                                            <% if (function.ViewPermission())
                                                { %>
                                            <% if (function.UpdatePermission())
                                                { %>
                                            <asp:LinkButton ID="editButton" runat="server" class="" CommandName="Edit" ToolTip="Edit"><i class="fas fa-edit fa-lg"></i></asp:LinkButton>
                                            <% } %>
                                            <% if (function.DeletePermission())
                                                { %>
                                            <asp:LinkButton ID="deleteButton" runat="server" class="" CommandName="Delete" ToolTip="Delete"><i class="fas fa-trash-alt fa-lg"></i></asp:LinkButton>
                                            <% } %>
                                            <% } %>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="updateButton" runat="server" class="" CommandName="Update" ToolTip="Update"><i class="fas fa-save fa-lg"></i></asp:LinkButton>
                                            <asp:LinkButton ID="cancelButton1" runat="server" class="" CommandName="Cancel" ToolTip="Cancel"><i class="fas fa-times fa-lg"></i></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-6">
                            <% function func = function.GetInstance();
                                if (func.UpdatePermission())
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
                                <asp:Button ID="btnUpdate" class="btn btn-primary" OnClick="btnUpdate_OnClick" runat="server" Text="Update Portfolio" />
                            </div>
                            <% } %>
                        </div>
                        <div class="col-md-3"></div>
                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnUpdate"/>
        </Triggers>
    </asp:updatepanel>
    <script> 
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
