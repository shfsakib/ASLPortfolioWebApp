using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class update_portfolio : System.Web.UI.Page
    {
        private function func;

        public update_portfolio()
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
                    Load();
                }
            }
        }
        private void Load()
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            func.LoadGrid(gridPhotos, "SELECT * FROM PortfolioPicture WHERE PortfolioId = '" + id + "' ORDER BY PicId DESC");
            txtTile.Text = func.IsExist($@"SELECT Title FROM Portfolios WHERE PortfolioId='{id}'");
            txtDescription.Text = func.IsExist($@"SELECT Description FROM Portfolios WHERE PortfolioId='{id}'");
        }
        protected void gridPhotos_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPhotos.PageIndex = e.NewPageIndex;
            Load();
        }

        protected void gridPhotos_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddNew"))
            {
                if (txtPath.Text != "")
                {
                    Random random = new Random();
                    string ran = random.Next(11111, 999999).ToString();
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    string pic = "";
                    if (txtPath.Text != "")
                    {
                        pic = func.SaveBase64ToImageWithPath("/photos/portfolio/", txtPath.Text);
                        txtPath.Text = "";
                    }
                    else
                    {
                        func.Alert(this, "Please choose an image first", "w", false);
                        txtPath.Text = "";
                        return;
                    }

                    bool result = func.Execute($@"INSERT INTO PortfolioPicture(PortfolioId,Picture) VALUES('{id}','{pic}')");
                    if (result)
                    {
                        func.Alert(this, "Picture added successfully", "s", true);
                        txtPath.Text = "";
                        Load();
                    }
                    else
                    {
                        func.Alert(this, "Failed to remove picture", "e", true);
                    }
                }
                else
                {
                    func.Alert(this, "Please choose an image first", "e", true);
                    txtPath.Text = "";
                }
            }
        }

        protected void gridPhotos_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gridPhotos.EditIndex = e.NewEditIndex;
            Load();
        }

        protected void gridPhotos_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = (gridPhotos.Rows[e.RowIndex].FindControl("HiddenField2") as HiddenField).Value;
            string pic = "";
            if (txtPath.Text != "")
            {
                pic = func.SaveBase64ToImageWithPath("/photos/portfolio/", txtPath.Text);
                txtPath.Text = "";
            }
            else
            {
                func.Alert(this, "Please choose an image first", "w", false);
                txtPath.Text = "";
                return;
            }

            bool result = func.Execute($@"UPDATE PortfolioPicture SET Picture='{pic}' WHERE PicId='{id}'");
            if (result)
            {
                func.Alert(this, "Picture updated successfully", "s", false);
                gridPhotos.EditIndex = -1;
                Load();
            }
            else
            {
                func.Alert(this, "Failed to update successfully", "e", false);
            }
        }

        protected void gridPhotos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HiddenField id = (HiddenField)gridPhotos.Rows[e.RowIndex].FindControl("HiddenField1");
            bool result = func.Execute("DELETE FROM PortfolioPicture WHERE PicId='" + id.Value + "'");
            if (result)
            {
                func.Alert(Page, "Picture removed successfully", "s", true);
                Load();
            }
            else
            {
                func.Alert(Page, "Failed to remove picture", "e", true);
            }
        }

        protected void gridPhotos_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridPhotos.EditIndex = -1;
            Load();
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (txtTile.Text == "")
            {
                func.Alert(this, "Title is required", "w", false);
            }
            else if (txtDescription.Text == "")
            {
                func.Alert(this, "Description is required", "w", false);
            }
            else
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                bool ans = func.Execute($@"UPDATE Portfolios SET Title='{txtTile.Text}',Description='{txtDescription.Text}' WHERE PortfolioId='{id}'");
                if (ans)
                {
                    func.AlertRedirect(this,"Portfolio updated successfully","s","/admin/manage-portfolio.aspx",false);
                }
                else
                {
                    func.Alert(this, "Failed to update portfolio", "e", false);
                }
            }
        }
    }
}