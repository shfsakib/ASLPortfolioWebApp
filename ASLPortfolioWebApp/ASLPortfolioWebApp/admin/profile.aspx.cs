using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASLPortfolioWebApp.admin
{
    public partial class profile : System.Web.UI.Page
    {
        private function func;

        public profile()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            ViewState["pic"] = func.IsExist($@"SELECT Picture FROM Users WHERE UserId='{func.UserIdCookie()}'");
            profileImage.ImageUrl = ViewState["pic"].ToString();
            nameLabel.Text =
                txtName.Text = func.IsExist($@"SELECT Name FROM Users WHERE UserId='{func.UserIdCookie()}'");
            lblEmail.Text = txtEmail.Text = func.IsExist($@"SELECT Email FROM Users WHERE UserId='{func.UserIdCookie()}'");
            txtPass.Text = lblPass.Text = func.IsExist($@"SELECT Password FROM Users WHERE UserId='{func.UserIdCookie()}'");
        }
        protected void btnEdit_ServerClick(object sender, EventArgs e)
        {
            btnEdit.Visible = false;
            lnkUpdate.Visible = true;
            nameLabel.Visible = lblEmail.Visible = lblPass.Visible = false;
            txtName.Visible = txtEmail.Visible = txtPass.Visible = fileProfile.Visible = true;
        }

        protected void lnkUpdate_OnClick(object sender, EventArgs e)
        {
            string pic = "";
            if (txtPath.Text != "")
            {
                pic = func.SaveBase64ToImage(txtPath.Text);
                txtPath.Text = "";
            }
            else
            {
                pic = ViewState["pic"].ToString();
            }
            bool ans = func.Execute($@"UPDATE Users SET Name='{txtName.Text}',Email='{txtEmail.Text}',Password='{txtPass.Text}',Picture='{pic}' WHERE UserId='{func.UserIdCookie()}'");
            if (ans)
            {
                func.Alert(this, "Profile updated successfully", "s", false);
                LoadProfile();
                HttpCookie cookie = function.CreateCookie();
                cookie.Expires = DateTime.Now.AddDays(-1);
                cookie["Name"] = txtName.Text;
                cookie["Email"] = txtEmail.Text;
                cookie["Picture"] = pic;
                cookie["UserId"] = func.UserIdCookie();
                cookie["Type"] = func.TypeCookie();
                cookie.Expires = DateTime.Now.AddDays(60);
                Response.Cookies.Add(cookie);
                Response.Redirect("/admin/profile.aspx");
            }
            else
            {
                func.Alert(this, "Failed to update", "e", false);
                LoadProfile();
                btnEdit.Visible = true;
                lnkUpdate.Visible = false;
                nameLabel.Visible = lblEmail.Visible = lblPass.Visible = true;
                txtName.Visible = txtEmail.Visible = txtPass.Visible = fileProfile.Visible = false;
            }

        }
    }
}