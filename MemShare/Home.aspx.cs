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
    public partial class Home : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //fill();
            if (!IsPostBack)
            {
                try
                {
                    string email = (Session["email"].ToString());
                    SqlConnection con = new SqlConnection(sqlStr);
                    con.Open();
                    SqlCommand cmd;
                    string sql = "SELECT Name FROM tblUsers WHERE Email = '" + email + "'";
                    cmd = new SqlCommand(sql, con);
                    string name = (string)cmd.ExecuteScalar();
                    lblWelcome.Text = "Welcome " + name;
                }
                catch (Exception ex)
                {

                    Response.Write("<script>alert('A connection error occured. Please try again')</script>");
                }
            }


        }

        private void fill()
        {
            //string userId;
            int userId = getUserId();
            ////string email = (Session["email"].ToString());
            SqlConnection con = new SqlConnection(sqlStr);
            //con.Open();
            //SqlCommand cmd;
            //string sql = "SELECT Photo FROM tblPhotos WHERE UserId = '" + userId + "'";
            //cmd = new SqlCommand(sql, con);
            //string photo = (string)cmd.ExecuteScalar();//"File uploaded to " + "~/Images/" + FileUpload1.FileName;



            //Label2.Text = photo;
            SqlDataAdapter adap = new SqlDataAdapter("SELECT Photo FROM tblPhotos where UserId = '" + userId + "'", con);
            DataSet ds = new DataSet();
            adap.Fill(ds);
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }

        private int getUserId()
        {
            string email = (Session["email"].ToString());
            SqlConnection con = new SqlConnection(sqlStr);
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT Id FROM tblUsers WHERE Email = '" + email + "'";
            cmd = new SqlCommand(sql, con);
            int userId = (int)cmd.ExecuteScalar();
            return userId;
        }

        
        protected void btnAddPhoto_Click1(object sender, EventArgs e)
        {
            try
            {
                int userId = getUserId();
                string path;
                if (FileUpload1.HasFile == false)
                {
                    Response.Write("<script>alert('Please enter photo.')</script>");
                }
             
                else
                {



                    if (FileUpload1.HasFile)
                        FileUpload1.SaveAs(HttpContext.Current.Request.PhysicalApplicationPath + "/Images/" + FileUpload1.FileName);
                    path = FileUpload1.FileName;


                    SqlConnection con = new SqlConnection(sqlStr);
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblPhotos VALUES(@Photo, @UserId)", con);
                    cmd.Parameters.AddWithValue("@Photo", path);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('A connection error occured. Please try again')</script>");
            }

        }

        protected void btnViewPhotos_Click(object sender, EventArgs e)
        {
            fill();
        }
    }
}