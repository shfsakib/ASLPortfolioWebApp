<%@ Page Title="" Language="C#" MasterPageFile="~/admin/root.Master" AutoEventWireup="true" CodeBehind="add-photos.aspx.cs" Inherits="ASLPortfolioWebApp.admin.add_photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:textbox id="txtPath" onfocus="blur();" style="visibility: hidden;" runat="server"></asp:textbox>
    <asp:updatepanel runat="server">
        <ContentTemplate>
            <div class="box">
                <div class="box-header with-border">
                    <h3>Manage Portfolio Photos</h3>
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
                                                                    <asp:FileUpload onchange="ImagePreview(this)" ID="FileUpload" accept=".png,.jpg,.jpeg" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("PicId") %>' />   
                                                        <img id="imgPortfolio" src='<%# Eval("Picture")%>' alt="photo" style="border: 2px solid black; cursor: pointer; width: 120px; height: 120px; border: 2px solid white;" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("PicId") %>'/>                                                                                                 
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
                                                                    <% if (gridPhotos.Rows.Count <= 9)
                                                                        { %>
                                                                    <asp:LinkButton ID="addNewButton1" runat="server" class="btn btn-primary" CommandName="AddNew" ToolTip="Add"><i class="fas fa-plus"></i></asp:LinkButton>
                                                               <% } %>
                                                                        </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="editButton" runat="server" class="" CommandName="Edit" ToolTip="Edit"><i class="fas fa-edit fa-lg"></i></asp:LinkButton>
                                                        <asp:LinkButton ID="deleteButton" runat="server" class="" CommandName="Delete" ToolTip="Delete"><i class="fas fa-trash-alt fa-lg"></i></asp:LinkButton>

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
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
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
