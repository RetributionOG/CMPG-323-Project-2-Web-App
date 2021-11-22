using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MemShare
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlCommand cmd;

            if (txtPassword.Text.Equals(txtPasswordConfirm.Text))
            {
                SqlConnection con = new SqlConnection(sqlStr);
                string sql;
                string newEncryptPass;
                string email;

                email = txtEmail.Text;

                newEncryptPass = encryptpass(txtPasswordConfirm.Text);

                sql = @"UPDATE tblUsers SET Password = '" + newEncryptPass + "' WHERE Email = '" + email + "'";

                con.Open();

                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                con.Close();

                Response.Redirect("Login.aspx");
            }
            else
            {
                lblError.Text = "Passwords do not match.";
                lblError.Visible = true;
            }
            }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        public string encryptpass(string password)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}