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
    public partial class RequestCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNotExist.Text = "";
            lblNotExist.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void btnSendCode_Click(object sender, EventArgs e)
        {
            try 
            {
                string sql;
                string conStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

                SqlConnection con = new SqlConnection(conStr);

                sql = @"SELECT Email FROM tblUsers WHERE Email = '" + txtEmail.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);

                con.Open();

                string quaryresult = Convert.ToString(cmd.ExecuteScalar());

                if (quaryresult.Equals(txtEmail.Text))
                {
                    Random randNum = new Random();
                    int uniqueNum = randNum.Next(1000, 9999);

                    MailMessage mailMessage = new MailMessage("camerondebastos3@gmail.com", txtEmail.Text);
                    mailMessage.Subject = "Reset Password";

                    mailMessage.Body = "Hello," + Environment.NewLine + Environment.NewLine + "You have requested to reset your password on the SAARA website. Use the code below and enter it on the page where you are now." + Environment.NewLine + Environment.NewLine +"Your unique code: "+uniqueNum + Environment.NewLine +
                                           Environment.NewLine + "This code is valid for 15 minutes."+ Environment.NewLine  + Environment.NewLine +"Best regards,"+ Environment.NewLine  +"SAARA";

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = "camerondebastos3@gmail.com",
                        Password = "KCdb172426"
                    };

                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);

                    Session["Code"] = uniqueNum;
                    Session["Email"] = txtEmail.Text;
                    Session.Timeout = 15;

                    cmd.Dispose();
                    con.Close();

                    Response.Redirect("ResetPassword.aspx");
                }
                else
                {
                    lblNotExist.Text = "Email does not exist";
                    lblNotExist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblNotExist.Text = "An error occured. Try again";
                lblNotExist.Visible = true;
            }
        }
    }
}