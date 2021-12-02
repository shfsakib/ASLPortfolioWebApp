using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class portfolio_details : System.Web.UI.Page
    {
        private function func;
        private string topId = "", lastId = "";
        public portfolio_details()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count <= 0)
                {
                    func.AlertWithRedirect(this, "Please select a portfolio first!", "/admin/manage-portfolio.aspx");
                }
                else
                {
                    if (!func.ViewPermission())
                    {
                        func.AlertWithRedirect(this, "You don\\'t have view permission", "/admin/manage-portfolio.aspx");
                        return;
                    }
                    Load();
                }
            }
        }

        private void Load()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            ViewState["picId"] = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "'");
            imgLarge.ImageUrl = func.IsExist($@"SELECT TOP 1 Picture FROM PortfolioPicture WHERE PortfolioId = '" + id + "'");
            func.LoadDataList(dataList, "SELECT * FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId ASC");
            lbltitle.Text = func.IsExist($@"SELECT Title FROM Portfolios WHERE PortfolioId = '" + id + "'");
            lblDescription.Text = func.IsExist($@"SELECT Description FROM Portfolios WHERE PortfolioId = '" + id + "'");

            topId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId ASC");
            lastId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId DESC");
        }

        protected void lnkPrev_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            topId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId ASC");
            lastId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId DESC");

            if (topId != ViewState["picId"].ToString())
            {
                string prevId = ViewState["picId"].ToString();
                ViewState["picId"] = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PicId < '" + prevId + "' ORDER By PicId DESC");
                imgLarge.ImageUrl = func.IsExist($@"SELECT TOP 1 Picture FROM PortfolioPicture WHERE PicId = '" + ViewState["picId"] + "'");
                func.JqueryCommand(this, "$('." + ViewState["picId"].ToString() + "').css('border', '5px solid gainsboro')");
            }
            else
            {
                imgLarge.ImageUrl = func.IsExist($@"SELECT TOP 1 Picture FROM PortfolioPicture WHERE PicId = '" + topId + "'");
                func.JqueryCommand(this, "$('." + topId + "').css('border', '5px solid gainsboro')");

            }

        }

        protected void lnkNext_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            topId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId ASC");
            lastId = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId DESC");

            if (lastId != ViewState["picId"].ToString())
            {
                string nextId = ViewState["picId"].ToString();
                ViewState["picId"] = func.IsExist($@"SELECT TOP 1 PicId FROM PortfolioPicture WHERE PicId > '" + nextId + "' ORDER By PicId ASC");
                imgLarge.ImageUrl = func.IsExist($@"SELECT TOP 1 Picture FROM PortfolioPicture WHERE PicId = '" + ViewState["picId"] + "'");

                func.JqueryCommand(this, "$('." + ViewState["picId"].ToString() + "').css('border', '5px solid gainsboro')");
            }
            else
            {
                imgLarge.ImageUrl = func.IsExist($@"SELECT TOP 1 Picture FROM PortfolioPicture WHERE PicId = '" + lastId + "'");

                func.JqueryCommand(this, "$('." + lastId + "').css('border', '5px solid gainsboro')");
            }
        }
    }
}