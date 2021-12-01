using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class manage_portfolio : System.Web.UI.Page
    {
        private function func;

        public manage_portfolio()
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
            func.LoadGrid(gridPortfolio, $@"SELECT * FROM Portfolios ORDER BY Title ASC");
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (txtTile.Text == "")
            {
                func.Alert(this, "Title is required", "w", false);
            }
            else if (txtDescription.Text == "")
            {
                func.Alert(this, "Description is required", "w", false);
            }
            else if (txtPath.Text == "")
            {
                func.Alert(this, "Portfolio cover picture is required", "w", false);
            }
            else
            {
                string pic = "";
                if (txtPath.Text != "")
                {
                    pic = func.SaveBase64ToImage(txtPath.Text);
                    txtPath.Text = "";
                }
                else
                {
                    func.Alert(this, "Portfolio cover picture is required", "w", false);
                    return;

                }

                ViewState["id"] = func.GenerateId($@"SELECT MAX(PortfolioId) FROM Portfolios");
                bool ans = func.Execute(
                    $@"INSERT INTO Portfolios(PortfolioId,Title,Description) VALUES('{ViewState["id"]}','{txtTile.Text}','{txtDescription.Text}')");
                if (ans)
                {
                    bool picAns = func.Execute($@"INSERT INTO PortfolioPicture(PortfolioId,Picture) VALUES('{ViewState["id"]}','{pic}')");
                    if (picAns)
                    {
                        //func.Alert(this, "Portfolio added successfully", "s", false);
                        //func.Redirect(this, "/admin/manage-portfolio.aspx");
                        func.ConfirmationAlert(this, "Portfolio added successfully. Do you want to add more portfolio photos?", "location.replace('/admin/add-photos.aspx?id="+ ViewState["id"] + "')", "Swal.fire('You can add more photos during update portfolio', '', 'warning')");
                        Load();
                    }
                    else
                    {
                        func.Alert(this, "Failed to add portfolio", "e", false);
                    }
                }
            }
        }

        protected void gridPortfolio_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPortfolio.PageIndex = e.NewPageIndex;
            Load();
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField id = (HiddenField)linkButton.Parent.FindControl("id");
            bool ans = func.Execute($@"DELETE FROM Portfolios WHERE PortfolioId='{id.Value}'");
            if (ans)
            {
                bool picAns = func.Execute($@"DELETE FROM PortfolioPicture WHERE PortfolioId='{id.Value}'");
                if (picAns)
                {
                    func.Alert(this, "Portfolio removed successfully", "s", false);
                    Load();
                }
                else
                {
                    func.Alert(this, "Failed to remove portfolio", "e", false);
                }

            }
        }

        protected void gridPortfolio_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField id = (HiddenField)e.Row.FindControl("id");
                DataList dataImage = (DataList)e.Row.FindControl("dataImage");
                func.LoadDataList(dataImage, $@"SELECT * FROM PortfolioPicture WHERE PortfolioId='{id.Value}'");
            }
        }

        protected void lnkView_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField id = (HiddenField)linkButton.Parent.FindControl("id");

        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField id = (HiddenField)linkButton.Parent.FindControl("id");

        }
    }
}