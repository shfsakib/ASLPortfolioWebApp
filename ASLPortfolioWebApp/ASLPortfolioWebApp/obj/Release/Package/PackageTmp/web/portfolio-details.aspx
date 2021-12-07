<%@ Page Title="" Language="C#" MasterPageFile="~/web/web.Master" AutoEventWireup="true" CodeBehind="portfolio-details.aspx.cs" Inherits="ASLPortfolioWebApp.web.portfolio_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row p-4">
                <div class="col-12 text-center">
                    <h1>
                        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                    </h1>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-5 p-2">
                    <asp:Image ID="imgLarge" Style="border: 2px solid gainsboro; padding: 10px;" runat="server" CssClass="large-img" />
                </div>
                <div class="col-md-3 text-center p-2">
                    <div class="col-md-12 table-div">
                        <asp:DataList ID="dataList" class="m-auto" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                            <ItemTemplate>
                                <asp:Image ID="imgPortfolios" class='<%#Eval("PicId")%>' ImageUrl='<%#Eval("Picture")%>' Style="width: 75px; height: 75px; padding: 5px; border: 1px solid gainsboro;" runat="server"></asp:Image>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-md-12 p-4">
                        <asp:LinkButton ID="lnkPrev" class="btn btn-primary" OnClick="lnkPrev_OnClick" runat="server"><i class="fas fa-chevron-left"></i></asp:LinkButton>
                        <asp:LinkButton ID="lnkNext" class="btn btn-primary" OnClick="lnkNext_OnClick" runat="server"><i class="fas fa-chevron-right"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row pt-4">
                <div class="col-md-1"></div>
                <div class="col-md-10 text-center">
                    <p class="text-justify font-18">
                        <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
                    </p>
                </div>
                <div class="col-md-1"></div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
