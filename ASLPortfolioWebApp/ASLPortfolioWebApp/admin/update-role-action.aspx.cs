
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;

namespace ASLPortfolioWebApp.admin
{
    public partial class update_role_action : System.Web.UI.Page
    {
        private function func;

        public update_role_action()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                func.CheckTypeCookie(this, "Super Admin");

                if (Session["actionId"] == null)
                {
                    func.AlertWithRedirect(this, "Please select a role action for update", "/admin/manage-role-action.aspx");
                }
                else
                {
                    if (!func.UpdatePermission())
                    {
                        func.AlertWithRedirect(this, "You don\\'t have update permission", "/admin/manage-role-action.aspx");
                        return;
                    }
                    Load();
                }
            }
        }
        private void Load()
        {
            func.BindDropDown(ddlRole, "select role", $@"SELECT RoleId Id,RoleName Name FROM Roles ORDER BY Name ASC");
            ddlRole.SelectedValue = func.IsExist($@"SELECT RoleId FROM RoleActions WHERE ActionId='{Session["actionId"].ToString()}'");
            chkInsert.Checked = Convert.ToBoolean(func.IsExist($@"SELECT CASE RoleActions.[Insert] WHEN 1 THEN 'TRUE' WHEN 0 THEN 'FALSE' END [Insert] FROM RoleActions WHERE ActionId='{Session["actionId"].ToString()}'"));
            chkUpdate.Checked = Convert.ToBoolean(func.IsExist($@"SELECT CASE RoleActions.[Update] WHEN 1 THEN 'TRUE' WHEN 0 THEN 'FALSE' END [Update] FROM RoleActions WHERE ActionId='{Session["actionId"].ToString()}'"));
            chkDelete.Checked = Convert.ToBoolean(func.IsExist($@"SELECT CASE RoleActions.[Delete] WHEN 1 THEN 'TRUE' WHEN 0 THEN 'FALSE' END [Delete] FROM RoleActions WHERE ActionId='{Session["actionId"].ToString()}'"));
            chkView.Checked = Convert.ToBoolean(func.IsExist($@"SELECT CASE RoleActions.[View] WHEN 1 THEN 'TRUE' WHEN 0 THEN 'FALSE' END [View] FROM RoleActions WHERE ActionId='{Session["actionId"].ToString()}'"));
        }
        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            if (!chkInsert.Checked && !chkDelete.Checked && !chkUpdate.Checked && !chkView.Checked)
            {
                func.Alert(this, "Minimum one action permission is required", "w", false);
            }
            else
            {
                string insert = "0", update = "0", delete = "0", view = "0";
                if (chkInsert.Checked)
                {
                    insert = "1";
                }
                if (chkUpdate.Checked)
                {
                    update = "1";
                }
                if (chkDelete.Checked)
                {
                    delete = "1";
                }
                if (chkView.Checked)
                {
                    view = "1";
                }
                bool ans = func.Execute($@"UPDATE RoleActions SET [Insert]='{insert}',[Update]='{update}',[Delete]='{delete}',[View]='{view}' WHERE ActionId='{Session["actionId"].ToString()}'");
                if (ans)
                {
                    func.AlertRedirect(this, "Role action updated successfully", "s", "/admin/manage-role-action.aspx", false);

                    Load();
                }
                else
                {
                    func.Alert(this, "Failed to update role action", "e", false);
                }
            }
        }
    }
}