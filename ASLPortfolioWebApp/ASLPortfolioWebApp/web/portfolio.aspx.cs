using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.web
{
    public partial class portfolio : System.Web.UI.Page
    {
        private function func;

        public portfolio()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load();
            }
        }

        private void Load()
        {
            func.LoadRepeater(portfolioRepeater, $@"SELECT   TOP 1     Portfolios.PortfolioId, Portfolios.Title, Portfolios.Description, PortfolioPicture.Picture
FROM            Portfolios INNER JOIN
                         PortfolioPicture ON Portfolios.PortfolioId = PortfolioPicture.PortfolioId");
        }

    }
}