using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BilgiYarimasiWEB.Classes;
using System.Data;

namespace BilgiYarimasiWEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie userCookie = Request.Cookies["UserData"];
            if (userCookie != null)
            {
               string kullaniciAdi = userCookie["UserName"];
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            SqlCommand commandLogin = new SqlCommand("SELECT * FROM Deneme1 WHERE Name=@pname AND Surname=@pSname", SqlConnecitonClass.connection);
            SqlConnecitonClass.CheckConnection();
            commandLogin.Parameters.AddWithValue("@pname", tboxName.Text);
            commandLogin.Parameters.AddWithValue("@pSname", tboxSurname.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(commandLogin);
            HttpCookie userCookie = new HttpCookie("UserData");
            userCookie["UserName"] = tboxName.Text; 
            userCookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(userCookie); 
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Response.Write("Giriş yapıldı");
                Response.Redirect("Sorular.aspx");
            }
            else
            {
                Response.Write("Kullanıcı adı veya Soyadı Hatalı");
            }
        }
    }
}