using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MemShare
{
    public partial class Login: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Remove("isValid");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string password;
                string sql;
                string decryptPass;

                string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                SqlConnection con = new SqlConnection(sqlStr);

                sql = @"SELECT Password FROM tblUsers WHERE Email = '" + txtEmail.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);

                password = txtPassword.Text;

                con.Open();

                string quaryresult = Convert.ToString(cmd.ExecuteScalar());

                if (quaryresult != null)
                {
                    decryptPass = DecryptString(quaryresult);

                    if (decryptPass.Equals(password))
                    {
                        HttpCookie isValid = new HttpCookie("isValid");
                        isValid["valid"] = txtEmail.Text;
                        Response.Cookies.Add(isValid);
                        isValid.Expires = DateTime.Now.AddMinutes(30);

                        Session["email"] = txtEmail.Text;
                        Session["password"] = txtPassword.Text;

                        cmd.Dispose();
                        con.Close();

                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        lblError.Text = "Log in credentials do not match.<br /> Please try again";
                        lblError.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured.<br /> Please try again";
                lblError.Visible = true;
            }
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestCode.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Welcome.aspx");
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }
    }
}