using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;
using Image = System.Drawing.Image;

namespace ASLPortfolioWebApp.admin
{
    public partial class manage_user : System.Web.UI.Page
    {
        private function func;
        Random random = new Random();
        public manage_user()
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
            func.BindDropDown(ddlRole, "select role", $@"SELECT RoleId Id,RoleName Name FROM Roles ORDER BY Name ASC");
            func.LoadGrid(gridUser, $@"SELECT        Users.UserId, Users.Name, Users.Email, Users.RoleId, Users.Picture, Users.Password, Roles.RoleName
FROM            Users INNER JOIN
                         Roles ON Users.RoleId = Roles.RoleId
ORDER BY Users.UserId");
        }

        private bool IsEmailExist(string email)
        {
            bool result = false;
            string x = func.IsExist($"SELECT Email FROM Users WHERE EMAIL='{txtEmail.Text}'");
            if (x != "")
            {
                result = true;
            }

            return result;
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                func.Alert(this, "Name is required", "w", false);
            }
            else if (txtEmail.Text == "")
            {
                func.Alert(this, "Email is required", "w", false);
            }
            else if (IsEmailExist(txtEmail.Text))
            {
                func.Alert(this, "Email already exist", "w", false);
            }
            else if (ddlRole.SelectedIndex <= 0)
            {
                func.Alert(this, "User role is required", "w", false);
            }
            else if (txtPass.Text == "")
            {
                func.Alert(this, "Password is required", "w", false);
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
                    pic = "/Link/user.png";
                }

                bool ans = func.Execute($@"INSERT INTO Users(Name,Email,RoleId,Picture,Password) VALUES('{txtName.Text}','{txtEmail.Text}','{ddlRole.SelectedValue}','{pic}','{txtPass.Text}')");
                if (ans)
                {
                    func.Alert(this, "User added successfully", "s", false);
                    txtName.Text = txtEmail.Text = txtPass.Text = "";
                    ddlRole.SelectedIndex = -1;
                    Load();
                }
                else
                {
                    func.Alert(this, "Failed to add user", "e", false);
                }
            }
        }

        protected void lnkRemove_OnClick(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            HiddenField userId = (HiddenField)linkButton.Parent.FindControl("HiddenField1");
            bool ans = func.Execute($@"DELETE FROM Users WHERE UserId='{userId.Value}'");
            if (ans)
            {
                func.Alert(this, "User removed successfully", "s", false);
                Load();
            }
            else
            {
                func.Alert(this, "Failed to remove user", "e", false);
            }
        }

        protected void gridUser_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUser.PageIndex = e.NewPageIndex;
            Load();
        }

        protected void gridUser_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField userId = (HiddenField)e.Row.FindControl("HiddenField1");
                LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");
                if (func.UserIdCookie() == userId.Value)
                {
                    lnkRemove.Visible = false;
                }
            }
        }
    }
}