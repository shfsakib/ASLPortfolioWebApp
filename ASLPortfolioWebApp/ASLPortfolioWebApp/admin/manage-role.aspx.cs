using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;

namespace ASLPortfolioWebApp.admin
{
    public partial class manage_role : System.Web.UI.Page
    {
        private function func;

        public manage_role()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                func.CheckTypeCookie(this, "Super Admin");
                Load();
            }
        }

        private void Load()
        {
            Session["roleId"] = null;
            func.LoadGrid(gridRole, $@"SELECT * FROM Roles ORDER By RoleName ASC");
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (txtRoleName.Text == "")
            {
                func.Alert(this, "Role name is required", "w", false);
            }
            else
            {
                bool ans = func.Execute($@"INSERT INTO Roles(RoleName) VALUES('{txtRoleName.Text}')");
                if (ans)
                {
                    func.Alert(this, "Role name added successfully", "s", false);
                    txtRoleName.Text = "";
                    Load();
                }
                else
                {
                    func.Alert(this, "Failed to add role name", "e", false);
                }
            }
        }

        protected void gridRole_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRole.PageIndex = e.NewPageIndex;
            Load();
        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField roleId = (HiddenField)linkButton.Parent.FindControl("hiddenRoleId");
            Session["roleId"] = roleId.Value;
            Response.Redirect("/admin/update-role.aspx");

        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField roleId = (HiddenField)linkButton.Parent.FindControl("hiddenRoleId");
            bool ans = func.Execute($@"DELETE FROM Roles WHERE RoleId='{roleId.Value}'");
            if (ans)
            {
                func.Alert(this, "Role name removed successfully", "s", false);
                Load();
            }
            else
            {
                func.Alert(this, "Failed to remove role name", "e", false);
            }
        }

        protected void gridRole_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hiddenRoleId = (HiddenField)e.Row.FindControl("hiddenRoleId");
                Label lblRoleName = (Label)e.Row.FindControl("lblRoleName");
                LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");
                LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");
                if (lblRoleName.Text == "Super Admin")
                {
                    lnkUpdate.Visible = lnkRemove.Visible = false;
                }
            }
        }
    }
}