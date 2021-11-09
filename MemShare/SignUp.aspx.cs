using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SAARA_Competition_and_Ranking
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCaptchaError.Text = "Incorrect CAPTCHA code";
                lblCaptchaError.Visible = false;    
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email;
            string sql;

            string conStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

            msgCaptcha.UserInputID = CaptchaCode.ClientID;

            if (IsPostBack)
            {
                string userInput = CaptchaCode.Text;
                bool isHuman = msgCaptcha.Validate(userInput);
                CaptchaCode.Text = null;

                if (isHuman)
                {
                    lblCaptchaError.Visible = false;

                    SqlConnection con = new SqlConnection(conStr);

                    sql = @"SELECT Email FROM tblUsers WHERE Email = '" + txtEmail.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    email = txtEmail.Text;

                    con.Open();

                    string quaryresult = Convert.ToString(cmd.ExecuteScalar());

                    if (!quaryresult.Equals(email))
                    {
                        sql = @"INSERT INTO dbo.tblUsers VALUES ('" + txtEmail.Text + "', '" + txtPassword.Text + "', 'U','" + txtName.Text + "','" + txtSurname.Text + "','" + txtContactNo.Text + "')";
                        cmd = new SqlCommand(sql, con);

                        cmd.ExecuteNonQuery();

                        con.Close();

                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        lblError.Text = "Email is already registered.<br /> Please try again";
                        lblError.Visible = true;
                    }

                }
                else
                {
                    lblCaptchaError.Visible = true;
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Welcome.aspx");
        }
    }
}