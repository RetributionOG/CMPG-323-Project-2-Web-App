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

namespace SAARA_Competition_and_Ranking
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            SqlCommand cmd;

            if (txtCode.Text.Equals(Session["Code"].ToString()))
            {
                if (txtPassword.Text.Equals(txtPasswordConfirm.Text))
                {
                    SqlConnection con = new SqlConnection(conStr);
                    string sql;
                    string email;

                    email = Session["Email"].ToString();

                    sql = @"UPDATE tblUsers SET Passwords = '" + txtPasswordConfirm.Text + "' WHERE Email = '" + email + "'";

                    con.Open();

                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();

                    con.Close();

                    MailMessage mailMessage = new MailMessage("camerondebastos3@gmail.com", email);
                    mailMessage.Subject = "Password Reset Successful";

                    mailMessage.Body = "Hello," + Environment.NewLine + Environment.NewLine + "Your request to reset your password on the SAARA website has been successfully completed."+
                                           Environment.NewLine + Environment.NewLine + "Best regards," + Environment.NewLine + "SAARA";

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "camerondebastos3@gmail.com",
                        Password = "KCdb172426"
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);

                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblError.Text = "Passwords do not match.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Incorrect code.";
                lblError.Visible = true;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}