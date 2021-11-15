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
    public partial class MetaData : System.Web.UI.Page
    {

        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string photo = (Session["photo"].ToString());
            imgUpload.ImageUrl = photo;
            //int photoid = getPhotoId();
            //lbltest.Text = photoid.ToString();
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                //int photoid = getPhotoId();
                SqlConnection con = new SqlConnection(sqlStr);
                //con.Open();

                //insert data into photo table
                int userId = getUserId();
                string path = (Session["photo"].ToString());
                int albumId = 0;

                //SqlCommand cmd = new SqlCommand("INSERT INTO tblPhotos VALUES(@Photo, @UserId, @AlbumId)", con);

                //con.Open();
                //cmd.Parameters.AddWithValue("@Photo", path);
                //cmd.Parameters.AddWithValue("@UserId", userId);
                //cmd.Parameters.AddWithValue("@AlbumId", albumId);
                //cmd.ExecuteNonQuery();
                //con.Close();

                //insert values in procedure
                con.Open();
                SqlCommand sqlcmd = new SqlCommand("photolastid", con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@Photo", path);
                sqlcmd.Parameters.AddWithValue("@UserId", userId);
                sqlcmd.Parameters.AddWithValue("@AlbumId", albumId);
                sqlcmd.Parameters.Add("@photoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlcmd.ExecuteNonQuery();

                //get photoid of newly inserted photo
                string photoid = sqlcmd.Parameters["@photoId"].Value.ToString();
                lbltest.Text = photoid;
                con.Close();

                //insert data into the metadata table
                con.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO tblMetaData VALUES(@PhotoId, @Geolocation, @Tags, @CaptureDate, @CaptureBy)", con);
                comm.Parameters.AddWithValue("@PhotoId", photoid);
                comm.Parameters.AddWithValue("@Geolocation", txtGeo.Text);
                comm.Parameters.AddWithValue("@Tags", txtTags.Text);
                comm.Parameters.AddWithValue("@CaptureDate", Calendar1.SelectedDate);
                comm.Parameters.AddWithValue("@CaptureBy", txtCaptureBy.Text);
                comm.ExecuteNonQuery();
                con.Close();

                Response.Redirect("Home.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('A connection error occured. Please try again')</script>");
            }
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

        //private int getPhotoId()
        //{
        //    //string photo = (Session["photo"].ToString());
        //    //SqlConnection con = new SqlConnection(sqlStr);
        //    //con.Open();
        //    //SqlCommand cmd;
        //    //string sql = "SELECT photoId FROM tblPhotos WHERE Photo = '" + photo + "'";
        //    //cmd = new SqlCommand(sql, con);
        //    //int photoId = (int)cmd.ExecuteScalar();
        //    //return photoId;

            

        //}
    }
}