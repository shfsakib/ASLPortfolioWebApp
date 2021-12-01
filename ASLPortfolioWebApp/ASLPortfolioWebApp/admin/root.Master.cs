using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        private function func;

        public Site1()
        {
            func = function.GetInstance();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                func.CheckCookies();
            }
        }

        protected void lnkLogOut_OnClick(object sender, EventArgs e)
        {
            func.Logout();
        }
    }
}