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
            if (!IsPostBack)
            {
                string addOrUpdate = Session["addOrUpdate"].ToString();

                if (addOrUpdate == "addphoto")
                {
                    string photo = (Session["photo"].ToString());
                    imgUpload.ImageUrl = photo;

                }
                else if (addOrUpdate == "update")
                {
                    string photoId = (Session["photoId"].ToString());

                    string photo = Session["photo"].ToString();

                    imgUpload.ImageUrl = photo;

                    string photoID = Session["photoId"].ToString();
                    string loc = getGeolocation(photoID);
                    string capBy = getCaptureBy(photoID);
                    string tags = getTags(photoID);
                    DateTime date = Convert.ToDateTime(getCaptureDate(photoID));
                    Calendar1.SelectedDate = date;
                    txtGeo.Text = loc;
                    txtCaptureBy.Text = capBy;
                    txtTags.Text = tags;
                    //lblTest.Text = wtf.ToString();
                }
                else if (addOrUpdate == "updateAdd")
                {
                    string photoId = (Session["photoId"].ToString());
                    string photo = getPhotoPath();
                    imgUpload.ImageUrl = photo;

                    Calendar1.SelectedDate = DateTime.Today;
                    txtGeo.Text = "None";
                    txtCaptureBy.Text = "None";
                    txtTags.Text = "None";
                }

                //int photoid = getPhotoId();
                //lbltest.Text = photoid.ToString();
            }
        }

        private string getphotoPath()
        {
            string photoID = Session["photoId"].ToString();
            SqlConnection con = new SqlConnection(sqlStr);
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT PhotoPath FROM tblPhotos WHERE photoId = '" + photoID + "'";
            cmd = new SqlCommand(sql, con);
            string photoPath = cmd.ExecuteScalar().ToString();
            return photoPath;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string addOrUpdate = Session["addOrUpdate"].ToString();
            
            if (addOrUpdate == "addphoto")// || addOrUpdate == "updateAdd")
            {
                try
                {
                    string path = Session["photo"].ToString();
                    //int photoid = getPhotoId();
                    SqlConnection con = new SqlConnection(sqlStr);
                    //con.Open();

                    //insert data into photo table
                    int userId = getUserId();
                    //string path = (Session["photo"].ToString());
                    int albumId = 0;


                    //insert values in procedure
                    con.Open();
                    SqlCommand sqlcmd = new SqlCommand("photolastid", con);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@PhotoPath", path);
                    sqlcmd.Parameters.AddWithValue("@UserId", userId);
                    sqlcmd.Parameters.AddWithValue("@AlbumId", albumId);
                    sqlcmd.Parameters.AddWithValue("@Photo", "None");
                    sqlcmd.Parameters.Add("@photoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    sqlcmd.ExecuteNonQuery();

                    //get photoid of newly inserted photo
                    string photoid = sqlcmd.Parameters["@photoId"].Value.ToString();
                    con.Close();

                    Calendar1.SelectedDate = DateTime.Today;

                    if (txtGeo.Text == "")
                    {
                        txtGeo.Text = "None";
                    }
                    if (txtCaptureBy.Text == "")
                    {
                        txtCaptureBy.Text = "None";
                    }
                    if (txtTags.Text == "")
                    {
                        txtTags.Text = "None";
                    }
                    //if (Calendar1.SelectedDate)
                    //{
                    //    date =  DateTime.Today;
                    //}

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

                    Response.Write("<script>alert('Photo was added successfully')</script>");
                    Response.Redirect("Home.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('A connection error occured. Please try again')</script>");
                }
            }
            else if (addOrUpdate == "update")
            {
                string photoID = Session["photoId"].ToString();
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand comm;
                string update = "UPDATE tblMetaData SET Geolocation = '" + txtGeo.Text + "'" + " WHERE PhotoID = '" + photoID + "'";
                comm = new SqlCommand(update, con);
                comm.ExecuteNonQuery();
                update = "UPDATE tblMetaData SET Tags = '" + txtTags.Text + "'" + " WHERE PhotoID = '" + photoID + "'";
                comm = new SqlCommand(update, con);
                comm.ExecuteNonQuery();
                update = "UPDATE tblMetaData SET Captureby = '" + txtCaptureBy.Text + "'" + " WHERE PhotoID = '" + photoID + "'";
                comm = new SqlCommand(update, con);
                comm.ExecuteNonQuery();
                update = "UPDATE tblMetaData SET CaptureDate = '" + Calendar1.SelectedDate + "'" + " WHERE PhotoID = '" + photoID + "'";
                comm = new SqlCommand(update, con);
                comm.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Your data was successfully updated!')</script>");

                Response.Redirect("Home.aspx");
            }
            else if (addOrUpdate == "updateAdd")
            {
                try
                {
                    //int photoid = getPhotoId();
                    SqlConnection con = new SqlConnection(sqlStr);
                    //con.Open();

                    //insert data into photo table
                    int userId = getUserId();
                    //string path = (Session["photo"].ToString());
                    int albumId = 0;


                    ////insert values in procedure
                    //con.Open();
                    //SqlCommand sqlcmd = new SqlCommand("photolastid", con);
                    //sqlcmd.CommandType = CommandType.StoredProcedure;
                    //sqlcmd.Parameters.AddWithValue("@PhotoPath", path);
                    //sqlcmd.Parameters.AddWithValue("@UserId", userId);
                    //sqlcmd.Parameters.AddWithValue("@AlbumId", albumId);
                    //sqlcmd.Parameters.AddWithValue("@Photo", "None");
                    //sqlcmd.Parameters.Add("@photoId", SqlDbType.Int).Direction = ParameterDirection.Output;
                    //sqlcmd.ExecuteNonQuery();

                    //get photoid of newly inserted photo
                    //string photoid = sqlcmd.Parameters["@photoId"].Value.ToString();
                    //con.Close();

                    Calendar1.SelectedDate = DateTime.Today;

                    if (txtGeo.Text == "")
                    {
                        txtGeo.Text = "None";
                    }
                    if (txtCaptureBy.Text == "")
                    {
                        txtCaptureBy.Text = "None";
                    }
                    if (txtTags.Text == "")
                    {
                        txtTags.Text = "None";
                    }

                    string photoid = Session["photoId"].ToString();

                    con.Open();
                    SqlCommand comm = new SqlCommand("INSERT INTO tblMetaData VALUES(@PhotoId, @Geolocation, @Tags, @CaptureDate, @CaptureBy)", con);
                    comm.Parameters.AddWithValue("@PhotoId", photoid);
                    comm.Parameters.AddWithValue("@Geolocation", txtGeo.Text);
                    comm.Parameters.AddWithValue("@Tags", txtTags.Text);
                    comm.Parameters.AddWithValue("@CaptureDate", Calendar1.SelectedDate);
                    comm.Parameters.AddWithValue("@CaptureBy", txtCaptureBy.Text);

                    comm.ExecuteNonQuery();
                    con.Close();

                    Response.Write("<script>alert('Photo was added successfully')</script>");
                    Response.Redirect("Home.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('A connection error occured. Please try again')</script>");
                }
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

        private string getGeolocation(string photoID)
        {

            string geolocation = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string geoloc = "SELECT Geolocation FROM tblMetaData WHERE PhotoId = '" + photoID + "'";
                cmd = new SqlCommand(geoloc, con);
                geolocation = cmd.ExecuteScalar().ToString();

            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                //return "";
            }
            return geolocation;

        }

        private string getTags(string photoID)
        {
            string tags = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string tag = "SELECT Tags FROM tblMetaData WHERE PhotoId = '" + photoID + "'";
                cmd = new SqlCommand(tag, con);
                tags = cmd.ExecuteScalar().ToString();

            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                //return tags;
            }
            return tags;
        }

        private string getCaptureDate(string photoID)
        {
            string date = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDate = "SELECT CaptureDate FROM tblMetaData WHERE PhotoId = '" + photoID + "'";
                cmd = new SqlCommand(capDate, con);
                date = cmd.ExecuteScalar().ToString();
                //return date;
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                //return "";
            }
            return date;

        }

        private string getCaptureBy(string photoID)
        {
            string captureBy = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDy = "SELECT CaptureBy FROM tblMetaData WHERE PhotoId = '" + photoID + "'";
                cmd = new SqlCommand(capDy, con);
                captureBy = cmd.ExecuteScalar().ToString();
                //return captureBy;
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                //return "";
            }
            return captureBy;

        }

        private string getPhotoPath()
        {
            string photoId = (Session["photoId"].ToString());

            SqlConnection con = new SqlConnection(sqlStr);

            con.Open();
            SqlCommand comm;
            string photoPath = "SELECT PhotoPath FROM tblPhotos WHERE PhotoId = '" + photoId + "'";
            comm = new SqlCommand(photoPath, con);
            string Id = comm.ExecuteScalar().ToString();
            con.Close();

            return Id;
        }
    }
}