using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASLPortfolioWebApp;

namespace ASLPortfolioWebApp.admin
{
    public partial class log_in : System.Web.UI.Page
    {
        private function func;
        private HttpCookie cookieData = function.GetCookie();

        public log_in()
        {
            func = function.GetInstance();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (cookieData != null)
                {
                    if (func.TypeCookie() == "Super Admin")
                    {
                        Response.Redirect("/admin/manage-role.aspx");
                    }
                    else
                    {
                        Response.Redirect("/admin/add-portfolio.aspx");
                    }
                }
                else
                {
                    txtEmail.Focus();
                }   
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            if (txtEmail.Value == "")
            {
                func.Alert(this, "Email is required","w",false);
            }
            else if (!func.EmailValidation(txtEmail.Value))
            {
                func.Alert(this, "Enter valid email", "w", false);
            }
            else if (txtPass.Value == "")
            {
                func.Alert(this, "Password is required", "w", false);
            }
            else
            {
                
                string password = func.IsExist($"SELECT Password FROM Users WHERE Email='{txtEmail.Value}' AND Password='{txtPass.Value}' COLLATE Latin1_General_CS_AI");

                if (password == txtPass.Value.Trim())
                {
                    HttpCookie cookie = function.CreateCookie();
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    cookie["Name"] = func.IsExist($"SELECT Name FROM Users WHERE Email='{txtEmail.Value}'");
                    cookie["UserId"] =
                       func.IsExist($"SELECT UserId FROM Users WHERE Email='{txtEmail.Value}'");
                    cookie["Email"] = func.IsExist($"SELECT Email FROM Users WHERE Email='{txtEmail.Value}'");
                    cookie["Picture"] = func.IsExist($"SELECT Picture FROM Users WHERE Email='{txtEmail.Value}'");
                    cookie["Type"] = func.IsExist($"SELECT Roles.RoleName FROM Roles INNER JOIN Users ON Roles.RoleId=Users.RoleId WHERE Users.Email='{txtEmail.Value}'");
                    cookie.Expires = DateTime.Now.AddDays(60);
                    Response.Cookies.Add(cookie);

                    if (func.TypeCookie() == "Super Admin")
                    {
                        Response.Redirect("/admin/manage-role.aspx");
                    }
                    else
                    {
                        Response.Redirect("/admin/add-portfolio.aspx");
                    }
                }
                else
                {
                    func.Alert(this, "Invalid email or password", "w", false);
                }
            }
        }
    }
}