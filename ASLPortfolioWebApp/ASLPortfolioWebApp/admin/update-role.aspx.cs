using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;

namespace ASLPortfolioWebApp.admin
{
    public partial class update_role : System.Web.UI.Page
    {
        private function func;

        public update_role()
        {
            func = function.GetInstance();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                func.CheckTypeCookie(this, "Super Admin");

                if (Session["roleId"] == null)
                {
                    func.AlertWithRedirect(this, "Please select a role for update", "/admin/manage-role.aspx");
                }
                else
                {
                    txtRoleName.Text = func.IsExist($@"SELECT RoleName FROM Roles WHERE RoleId='{Session["roleId"].ToString()}'");
                }
            }
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (txtRoleName.Text == "")
            {
                func.Alert(this, "Role name is required", "w", false);
            }
            else
            {
                bool ans = func.Execute($@"UPDATE Roles SET RoleName='{txtRoleName.Text}' WHERE RoleId='{Session["roleId"].ToString()}'");
                if (ans)
                {
                    func.AlertRedirect(this, "Role updated successfully", "s", "/admin/manage-role.aspx", false);
                }
                else
                {
                    func.Alert(this, "Failed to update role name", "e", false);
                }
            }
        }
    }
}