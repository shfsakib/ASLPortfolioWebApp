<%@ Page Title="" Language="C#" MasterPageFile="~/web/web.Master" AutoEventWireup="true" CodeBehind="portfolio.aspx.cs" Inherits="ASLPortfolioWebApp.web.portfolio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row p-4">
        <div class="col-12 text-center">
            <h1>Portfolio</h1>
        </div>
    </div>
    <div class="row p-4">
        <asp:Repeater ID="portfolioRepeater" runat="server">
            <ItemTemplate>
                <div class="col-12 col-sm-6 col-md-4 col-lg-3">
                    <a href='<%# "/web/portfolio-details.aspx?id="+Eval("PortfolioId") %>' class="text-decoration-none text-dark">
                        <div class="card">
                            <asp:Image ID="Image1" class="card-img-top p-4 bg-white" runat="server" ImageUrl='<%#Eval("Picture") %>' />
                            <div class="card-body text-center">
                                <h3><%#Eval("Title") %></h3>
                            </div>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
