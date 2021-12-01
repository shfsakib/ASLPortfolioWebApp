using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;

namespace ASLPortfolioWebApp.admin
{
    public partial class manage_role_action : System.Web.UI.Page
    {
        private function func;

        public manage_role_action()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                func.CheckTypeCookie(this,"Super Admin");
                Load();
            }
        }

        private void Load()
        {
            func.BindDropDown(ddlRole, "select role", $@"SELECT RoleId Id,RoleName Name FROM Roles ORDER BY Name ASC");
            func.LoadGrid(gridAction, $@"	SELECT        RoleActions.ActionId, RoleActions.RoleId, CASE RoleActions.[Insert] WHEN 1 THEN 'YES' WHEN 0 THEN 'NO' END [Insert],CASE RoleActions.[Update] WHEN 1 THEN 'YES' WHEN 0 THEN 'NO' END [Update],CASE RoleActions.[Delete] WHEN 1 THEN 'YES' WHEN 0 THEN 'NO' END [Delete],CASE RoleActions.[View] WHEN 1 THEN 'YES' WHEN 0 THEN 'NO' END [View], Roles.RoleName
	FROM            RoleActions INNER JOIN
							 Roles ON RoleActions.RoleId = Roles.RoleId ORDER BY RoleActions.ACTIONId DESC");
        }
        protected void gridAction_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAction.PageIndex = e.NewPageIndex;
            Load();
        }

        private bool IsRoleExist(string id)
        {
            bool result = false;
            string exist = func.IsExist($@"SELECT ActionId FROM RoleActions WHERE RoleId='{id}'");
            if (exist != "")
            {
                result = true;
            }

            return result;
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (ddlRole.SelectedIndex <= 0)
            {
                func.Alert(this, "Role name is required", "w", false);
            }
            else if (!chkInsert.Checked && !chkDelete.Checked && !chkUpdate.Checked && !chkView.Checked)
            {
                func.Alert(this, "Minimum one action permission is required", "w", false);
            }
            else if (IsRoleExist(ddlRole.SelectedValue))
            {
                func.Alert(this, "Action already added for this role", "w", false);
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
                bool ans = func.Execute($@"INSERT INTO RoleActions(RoleId,[Insert],[Update],[Delete],[View]) VALUES('{ddlRole.SelectedValue}','{insert}','{update}','{delete}','{view}')");
                if (ans)
                {
                    func.Alert(this, "Role action added successfully", "s", false);
                    Refresh();
                    Load();
                }
                else
                {
                    func.Alert(this, "Failed to add role name", "e", false);
                }
            }
        }

        private void Refresh()
        {
            ddlRole.SelectedIndex = -1;
            chkInsert.Checked = chkUpdate.Checked = chkDelete.Checked = chkView.Checked = false;
        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField actionid = (HiddenField)linkButton.Parent.FindControl("hiddenActionId");
            Session["actionId"] = actionid.Value;
            Response.Redirect("/admin/update-role-action.aspx");
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField actionid = (HiddenField)linkButton.Parent.FindControl("hiddenActionId");
            bool ans = func.Execute($@"DELETE FROM RoleActions WHERE ActionId='{actionid.Value}'");
            if (ans)
            {
                func.Alert(this, "Role action removed successfully", "s", false);
                Load();
            }
            else
            {
                func.Alert(this, "Failed to remove role action", "e", false);
            }
        }
    }
}