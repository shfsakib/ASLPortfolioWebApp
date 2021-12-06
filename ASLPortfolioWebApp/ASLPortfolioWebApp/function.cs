using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace ASLPortfolioWebApp
{
    public class function
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private static function _instance;
        private SqlConnection con;
        public static function GetInstance()
        {
            if (_instance == null)
            {
                _instance = new function();
            }
            return _instance;
        }

        public function()
        {
            if (con == null)
            {
                con = new SqlConnection(Connection);
            }
        }

        //public String Connection = new SqlConnectionStringBuilder
        //{
        //    DataSource = ".\\local",
        //    InitialCatalog = "PortfolioDb",
        //    UserID = "sa",
        //    Password = "ShfS@kib16",
        //    MultipleActiveResultSets = true,
        //    Pooling = true,
        //    MinPoolSize = 0,
        //    MaxPoolSize = 4000,
        //    ConnectTimeout = 0
        //}.ToString();
        public string Connection = @"Data Source=.\local;Initial Catalog=PortfolioDb;User ID=sa;Password=ShfS@kib16";
        public void BindDropDown(DropDownList ddl, string root, string query)
        {
            con = new SqlConnection(Connection);
            DataSet dataSet = new DataSet();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);
                ddl.DataSource = dataSet;
                ddl.DataTextField = "Name";
                ddl.DataValueField = "ID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--" + root.ToUpper() + "--"));
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        public void BindCheckBoxList(CheckBoxList ob, string root, string query)
        {
            con = new SqlConnection(Connection);
            DataSet dataSet = new DataSet();
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);
                ob.DataSource = dataSet;
                ob.DataTextField = "Name";
                ob.DataValueField = "ID";
                ob.DataBind();
                ob.Items.Insert(0, new ListItem(root.ToUpper()));
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
        public bool Execute(string str)
        {
            bool result = false;
            SqlConnection Conn = new SqlConnection(Connection);
            try
            {

                if (Conn.State != ConnectionState.Open) Conn.Open();
                SqlCommand cmd = new SqlCommand(str, Conn);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                    result = true;
                else
                    result = false;
                if (Conn.State != ConnectionState.Closed) Conn.Close();
            }
            catch { if (Conn.State != ConnectionState.Closed) Conn.Close(); }
            return result;
        }
        public string IsExist(string str)
        {
            string result = "";
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                {
                    result = DR[0].ToString();
                }
                DR.Close();
                if (con.State != ConnectionState.Closed) con.Close();

            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
            return result;
        }
        public bool ValidDate(TextBox date)
        {
            try
            {
                if (date.Text == "" || date.Text.Length != 10)
                    return true;
                else
                    DateTime.Parse(date.Text, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            }
            catch (Exception EX)
            { return true; }

            return false;
        }
        public bool ValidDateI(HtmlInputText date)
        {
            try
            {
                if (date.Value == "" || date.Value.Length != 10)
                    return true;
                else
                    DateTime.Parse(date.Value, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
            }
            catch (Exception EX)
            { return true; }

            return false;
        }
        public string Date()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy_hh:mm:ss");
            return date;
        }
        public DateTime Timezone(DateTime datetime)
        {
            var timezoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime printDate = TimeZoneInfo.ConvertTime(datetime, timezoneInfo);
            return printDate;
        }
        public void FocusTools(Page page, string toolName)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "$('#ContentPlaceHolder1_" + toolName + "').focus()", true);
        }
        public void FocusTool(Page page, string toolName)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "$('#'" + toolName + "'').focus()", true);
        }
        public void Alert(Page page, string msg, string type, bool confirm)
        {
            int timer = 0;
            if (type == "s")
            {
                type = "success";
            }
            else if (type == "e")
            {
                type = "error";
            }
            else if (type == "w")
            {
                type = "warning";
            }
            if (confirm)
            {
                timer = 6000;
            }
            else
            {
                timer = 1500;
            }
            string alert = @"Swal.fire({  position: 'center',  icon: '" + type + "',title: '" + msg + "',showConfirmButton:'" + confirm + "',timer:'" + timer + "'})";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Popup", alert, true);
        }
        public void ConfirmationAlert(Page page, string msg, string yesAction, string noAction)
        {

            string alert = @"Swal.fire({position: 'center', title: '" + msg + "', " +
                           "showDenyButton: true," +
                           "showCancelButton: true," +
                           "confirmButtonText: 'Yes'," +
                           "denyButtonText: 'No'," + @"}).then((result) => {
  if (result.isConfirmed) {
    " + yesAction + @"
  } else if (result.isDenied) {
   " + noAction + @"}
    })";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Popup", alert, true);
        }
        public void PopAlert(Page page, string msg)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "alert('" + msg + "')", true);

        }
        public void AlertRedirect(Page page, string msg, string type, string link, bool confirm)
        {
            int timer = 0;
            if (type == "s")
            {
                type = "success";
            }
            else if (type == "e")
            {
                type = "error";
            }
            else if (type == "w")
            {
                type = "warning";
            }
            if (confirm)
            {
                timer = 6000;
            }
            else
            {
                timer = 1500;
            }
            string alert = @"$(document).ready(function(){Swal.fire({  position: 'center',  icon: '" + type + "',title: '" + msg + "',showConfirmButton:'" + confirm + "',timer:'" + timer + "'});setTimeout(function(){location.replace('" + link + "')},1500);});";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "Popup", alert, true);
        }
        public void AlertWithRedirect(Page page, string msg, string link)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "alert('" + msg + "');setTimeout(function(){location.replace('" + link + "')},100);", true);
        }
        public void Redirect(Page page, string link)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", "setTimeout(function(){location.replace('" + link + "')},100);", true);
        }
        public void JqueryCommand(Page page, string command)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "script", command, true);
        }
        public bool EmailValidation(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void LoadGrid(GridView ob, string query)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            try
            {
                ob.Visible = true;
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }
        public void LoadDataList(DataList ob, string query)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            try
            {
                ob.Visible = true;
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }
        public void LoadRepeater(Repeater ob, string query)
        {
            DataTable table = new DataTable();
            SqlConnection con = new SqlConnection(Connection);
            try
            {
                ob.Visible = true;
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                ob.DataSource = table;
                ob.DataBind();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch (Exception ex)
            {
                if (con.State != ConnectionState.Closed) con.Close();
            }
        }
        public bool MobileNoValidation(string mobileNo)
        {
            try
            {
                if (mobileNo == "" || mobileNo.Length < 11 || !mobileNo.StartsWith("0"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
        public bool SendEmail(string fromMail, string toMail, string subject, string body, string fromPass)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromMail);
                message.To.Add(new MailAddress(toMail));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromMail, fromPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string CheckPassword(string pass)
        {
            if (pass.Length < 6 || pass.Length > 15)
            {
                return "Password must be between 6-15 character";
            }
            else if (pass.Contains(" "))
            {
                return "Remove space from your password";
            }
            else if (!pass.Any(char.IsUpper))
            {
                return "Password must have at least one capital latter";
            }
            else if (!pass.Any(char.IsLower))
            {
                return "Password must have at least one small latter";
            }
            else if (!pass.Any(char.IsDigit))
            {
                return "Password must be a combination of alphanumeric characters";
            }
            else
            {
                return "Strong password";
            }
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public string ImageToBase64(Image image,
            System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public string SaveBase64ToImage(string base64String)
        {
            Random random = new Random();
            string ran = random.Next(1111, 999999).ToString();
            string imageData = base64String;
            imageData = imageData.Substring(imageData.LastIndexOf(',') + 1);
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(imageData);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            string filePath = HttpContext.Current.Server.MapPath("/photos/") + ran + ".png"; ;
            image.Save(filePath, ImageFormat.Png);
            return "/photos/" + ran + ".png";
        }
        public string SaveBase64ToImageWithPath(string path, string base64String)
        {
            Random random = new Random();
            string ran = random.Next(1111, 999999).ToString();
            string imageData = base64String;
            imageData = imageData.Substring(imageData.LastIndexOf(',') + 1);
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(imageData);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
                imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            string filePath = HttpContext.Current.Server.MapPath(path) + ran + ".png"; ;
            image.Save(filePath, ImageFormat.Png);
            return path + ran + ".png";
        }

        public bool InsertPermission()
        {
            bool ans = false;
            string x = IsExist($@"SELECT        RoleActions.[Insert]
FROM            RoleActions INNER JOIN
                         Roles ON RoleActions.RoleId = Roles.RoleId WHERE Roles.RoleName='{TypeCookie()}'");
            if (x == "1")
            {
                ans = true;
            }

            return ans;
        }
        public bool UpdatePermission()
        {
            bool ans = false;
            string x = IsExist($@"SELECT        RoleActions.[Update]
FROM            RoleActions INNER JOIN
                         Roles ON RoleActions.RoleId = Roles.RoleId WHERE Roles.RoleName='{TypeCookie()}'");
            if (x == "1")
            {
                ans = true;
            }

            return ans;
        }
        public bool DeletePermission()
        {
            bool ans = false;
            string x = IsExist($@"SELECT        RoleActions.[Delete]
FROM            RoleActions INNER JOIN
                         Roles ON RoleActions.RoleId = Roles.RoleId WHERE Roles.RoleName='{TypeCookie()}'");
            if (x == "1")
            {
                ans = true;
            }

            return ans;
        }
        public bool ViewPermission()
        {
            bool ans = false;
            string x = IsExist($@"SELECT        RoleActions.[View]
FROM            RoleActions INNER JOIN
                         Roles ON RoleActions.RoleId = Roles.RoleId WHERE Roles.RoleName='{TypeCookie()}'");
            if (x == "1")
            {
                ans = true;
            }

            return ans;
        }
        public void CheckCookies()
        {
            HttpCookie cookies = HttpContext.Current.Request.Cookies["GarageInfo"];
            if (cookies == null)
            {
                HttpContext.Current.Response.Redirect("/admin/log-in.aspx", true);
            }
            else if (cookies != null)
            {
                string x = IsExist($"SELECT UserId FROM Users where UserId='{UserIdCookie()}'");
                if (x == "")
                {
                    Logout();
                }
            }
        }

        public void Logout()
        {
            HttpCookie cookie = function.CreateCookie();
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            HttpContext.Current.Response.Redirect("/admin/log-in.aspx");
        }
        public string UserIdCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["UserId"];
        }
        public string PictureCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["Picture"];
        }
        public string NameCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["Name"];
        }
        public string MobileCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["Mobile"];
        }
        public string EmailCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["Email"];
        }
        public string TypeCookie()
        {
            HttpCookie cookies = GetCookie();
            return cookies["Type"];
        }

        public void CheckTypeCookie(Page page, string type)
        {
            HttpCookie cookies = GetCookie();
            if (cookies == null)
            {
                HttpContext.Current.Response.Redirect("/admin/log-in.aspx");
            }
            else
            {
                if (cookies["Type"] != type)
                {
                    HttpContext.Current.Response.Redirect("/admin/log-in.aspx");
                }
            }
        }

        public static HttpCookie CreateCookie()
        {
            HttpCookie cookie = new HttpCookie("GarageInfo");
            if (cookie == null || cookie?.Value == "")
            {
                cookie = null;
            }
            return cookie;
        }
        public static HttpCookie GetCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["GarageInfo"];
            if (cookie == null || cookie?.Value == "")
            {
                cookie = null;
            }
            return cookie;
        }
        public void AdminType(Page page, string type1, string type2)
        {
            HttpCookie cookies = GetCookie();
            if (cookies["Type"] == type1)
            {
            }
            else if (cookies["Type"] == type2)
            {
            }
            else
            {
                HttpContext.Current.Response.Redirect("/admin/log-in.aspx");
            }
        }

        public string GenerateId(string query)
        {
            string id = "";
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    id = reader[0].ToString();
                    if (id == "")
                        id = "1001";
                    else
                    {
                        id = (int.Parse(id) + 1).ToString();
                    }

                }
                reader.Close();
                if (con.State != ConnectionState.Closed) con.Close();
            }
            catch
            {
            }
            return id;
        }

        public string TimeConvert(string time)
        {
            DateTime dateTime = Convert.ToDateTime(time);
            return dateTime.ToString("hh:mm tt");
        }

    }
}