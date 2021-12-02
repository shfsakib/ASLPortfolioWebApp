using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class add_photos : System.Web.UI.Page
    {
        private function func;

        public add_photos()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count <= 0)
                {

                    func.AlertWithRedirect(this, "Please add a portfolio first!", "/admin/manage-portfolio.aspx");
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
    }
}