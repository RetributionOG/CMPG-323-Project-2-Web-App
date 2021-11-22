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
    public partial class SharePhotos : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int userid = getUserId();
            SqlConnection con = new SqlConnection(sqlStr);
            SqlDataAdapter adp = new SqlDataAdapter("photos", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@userid", Convert.ToInt32(userid.ToString()));
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dlimage1.DataSource = dt;
            dlimage1.DataBind();
        }

        private int getUserId()
        {
            string email = (Session["shareemail"].ToString());
            SqlConnection con = new SqlConnection(sqlStr);
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT Id FROM tblUsers WHERE Email = '" + email + "'";
            cmd = new SqlCommand(sql, con);
            int userId = (int)cmd.ExecuteScalar();
            return userId;
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            try
            {
                //string shareMemEmail = txtShare.Text;
                int OwnerId = (int)Session["ownerid"];
                int ShareMemId = getShareMemId();
                string photoId = txtPhotoId.Text;
                int photoid = int.Parse(photoId.ToString());
                SqlConnection conn = new SqlConnection(sqlStr);
                conn.Open();
                SqlCommand comm;
                string insert = "INSERT INTO tblShared VALUES(@OwnerId, @ShareMemId, @PhotoId)";
                comm = new SqlCommand(insert, conn);
                comm.Parameters.AddWithValue("@OwnerId", OwnerId);
                comm.Parameters.AddWithValue("@ShareMemId", ShareMemId);
                comm.Parameters.AddWithValue("@PhotoId", photoid);
                comm.ExecuteNonQuery();
                conn.Close();

                Response.Write("<script>alert('Photo shared successfully')</script>");
                Response.Redirect("Home.aspx");


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error sharing the photo')</script>");
            }
        }


        private int getShareMemId()
        {
            //string shareMemEmail = txtShare.Text;
            int ShareMemId = 0;
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string shareMemId = "SELECT Id FROM tblUsers WHERE Email = '" + txtShare.Text + "'";
                cmd = new SqlCommand(shareMemId, con);
                ShareMemId = (int)cmd.ExecuteScalar();

            }
            catch
            {
                Response.Write("<script>alert('Connection error while retrieving Share Member ID')</script>");
                //return tags;
            }
            return ShareMemId;
        }
    }
}