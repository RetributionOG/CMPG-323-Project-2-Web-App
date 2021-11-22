using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace MemShare
{
    public partial class Home : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
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

            Response.Clear();
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
                //int userId = getUserId();
                //string path;
                if (FileUpload1.HasFile == false)
                {
                    Response.Write("<script>alert('Please enter photo.')</script>");
                }

                else
                {
                    SqlConnection con = new SqlConnection(sqlStr);
                    int UserId = getUserId();
                    var fileAsByteArray = FileUpload1.FileBytes;
                    var storageacc = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=memsharestorageaccount;AccountKey=+05e+/h6NbdmaAR7ppt5qATeJkmKALH0EyIhTKiE+mgs1TUEjQ+ku+kM0YOHYbTY1ZBHDspLE4tnQ0EQnuvENQ==;EndpointSuffix=core.windows.net");
                    var blobClient = storageacc.CreateCloudBlobClient();
                    var container = blobClient.GetContainerReference("memshareimages");
                    var blockBlob = container.GetBlockBlobReference(FileUpload1.FileName);
                    blockBlob.UploadFromByteArray(fileAsByteArray, 0, fileAsByteArray.Length);
                    string picPath = blockBlob.Uri.ToString();

                    Session["photo"] = picPath;

                    string addOrUpdate = "addphoto";
                    Session["addOrUpdate"] = addOrUpdate;

                    Response.Redirect("MetaData.aspx");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('A connection error occured. Please try again')</script>");
            }

        }

        protected void btnViewPhotos_Click(object sender, EventArgs e)
        {
            BindDatalist();
            testPhoto();
        }

        private void testPhoto()
        {

            int uid = getUserId();
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDate = "SELECT photoId FROM tblPhotos WHERE UserId = '" + uid + "'";
                cmd = new SqlCommand(capDate, con);

                //isPhoto = cmd.ExecuteScalar().ToString();
                if (cmd.ExecuteScalar() == null)
                    Response.Write("<script>alert('No Photos for this user')</script>");
                else
                {
                    cmd.ExecuteScalar().ToString();
                }
                //return date;
            }
            catch
            {
                Response.Write("<script>alert('No Photos for this user')</script>");
                //return "";
            }


        }

        public void BindDatalist()
        {
            int userid = getUserId();
            SqlConnection con = new SqlConnection(sqlStr);
            SqlDataAdapter adp = new SqlDataAdapter("photos", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@userid", Convert.ToInt32(userid.ToString()));
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dlimage.DataSource = dt;
            dlimage.DataBind();
        }

        protected void btnDeletePhoto_Click(object sender, EventArgs e)
        {
            if (txtphotoId.Text == "")
            {
                Response.Write("<script>alert('Please enter a photo ID')</script>");

            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection(sqlStr);

                    con.Open();
                    SqlCommand com;
                    string delete = "DELETE FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
                    com = new SqlCommand(delete, con);
                    com.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand com1;
                    delete = "DELETE FROM tblShared WHERE PhotoId = '" + txtphotoId.Text + "'";
                    com1 = new SqlCommand(delete, con);
                    com1.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand comm;
                    delete = "DELETE FROM tblPhotos WHERE photoId = '" + txtphotoId.Text + "'";
                    comm = new SqlCommand(delete, con);
                    comm.ExecuteNonQuery();
                    con.Close();
                    BindDatalist();
                }
                catch
                {
                    Response.Write("<script>alert('A delete error error occured')</script>");
                }
            }
        }

        private string getGeolocation()
        {

            string geolocation = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string geoloc = "SELECT Geolocation FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
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

        private string getTags()
        {
            string tags = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string tag = "SELECT Tags FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
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

        private string getCaptureDate()
        {
            string date = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDate = "SELECT CaptureDate FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
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

        private string getCaptureBy()
        {
            string captureBy = "";
            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDy = "SELECT CaptureBy FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
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

        private string getNewPhotoId()
        {

            try
            {
                bool flag = false;
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [tblMetaData]";
                cmd.Connection = con;
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd[1].ToString() == txtphotoId.Text)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                    return txtphotoId.Text;
                else
                {
                    return "";
                }
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                return "";
            }
            //return photoid;

        }

        private string TestShareMemId()
        {
            string ui = getUserId().ToString();
            string pid = getNewPhotoId().ToString();
            try
            {
                bool flag = false;
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM [tblShared]";
                cmd.Connection = con;
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd[2].ToString() == ui && rd[3].ToString() == pid)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                    return ui;
                else
                {
                    return "";
                }
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                return "";
            }
            //return photoid;

        }

        private string testUserPhoto()
        {
            string TestUserPhoto = "";

            try
            {
                SqlConnection con = new SqlConnection(sqlStr);
                con.Open();
                SqlCommand cmd;
                string capDy = "SELECT UserId FROM tblPhotos WHERE photoId = '" + txtphotoId.Text + "'";
                cmd = new SqlCommand(capDy, con);
                TestUserPhoto = cmd.ExecuteScalar().ToString();
                //return captureBy;
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
                //return "";
            }

            //}
            return TestUserPhoto;

        }

        protected void btnViewMetaData_Click(object sender, EventArgs e)
        {
            string userId = getUserId().ToString();
            string testsharememid = TestShareMemId();
            string newPhotoid = getNewPhotoId();//id of the photo
            //string newSharedPhotoId = getSharedNewPhotoId();


            //string validUser = "";
            if (txtphotoId.Text == "")
            {
                Response.Write("<script>alert('Please enter photo ID')</script>");
            }

            else
            {
                //string test = "";
                //string ifState = "";
                if (testsharememid == "")
                {
                    string validUser = testUserPhoto();//user id of the received photo  kyk wie het die foto ingesit


                    if (validUser == userId)
                    {
                        if (txtphotoId.Text == "")
                        {
                            Response.Write("<script>alert('Please enter photo ID')</script>");
                        }
                        else if (newPhotoid == "")
                        {
                            //Response.Write("<script>alert('No photo for entered ID')</script>");
                            lblGeolocation.Text = "Geolocation: None";
                            lblTags.Text = "Tags: None";
                            lblCaptureDate.Text = "Capture date: None";
                            lblCaptureBy.Text = "Capture by: None";
                        }

                        else
                        {

                            string location = getGeolocation();
                            if (location == "")
                            {
                                lblGeolocation.Text = "Geolocation: None";
                            }
                            else
                            {
                                lblGeolocation.Text = "Geolocation: " + location;
                            }

                            string tags = getTags();
                            if (tags == "")
                            {
                                lblTags.Text = "Tags: None";
                            }
                            else
                            {
                                lblTags.Text = "Tags: " + tags;
                            }

                            string date = getCaptureDate();
                            if (date == "")
                            {
                                lblCaptureDate.Text = "Capture date: None";
                            }
                            else
                            {
                                lblCaptureDate.Text = "Capture date: " + date;
                            }

                            string capBy = getCaptureBy();
                            if (capBy == "")
                            {
                                lblCaptureBy.Text = "Capture by: None";
                            }
                            else
                            {
                                lblCaptureBy.Text = "Capture by: " + capBy;
                            }
                        }
                    }
                    else
                    {

                        Response.Write("<script>alert('Invalid photo ID')</script>");
                        //    txtphotoId.Text = "";

                    }
                }
                else
                {
                    string shareMem = getShareMemId().ToString();//user id of photo shared with 5 kyk met wie was die photo geshare

                    if (shareMem == userId)
                    {
                        if (txtphotoId.Text == "")
                        {
                            Response.Write("<script>alert('Please enter photo ID')</script>");
                        }
                        else if (newPhotoid == "")
                        {
                            //Response.Write("<script>alert('No photo for entered ID')</script>");
                            lblGeolocation.Text = "Geolocation: None";
                            lblTags.Text = "Tags: None";
                            lblCaptureDate.Text = "Capture date: None";
                            lblCaptureBy.Text = "Capture by: None";
                        }

                        else
                        {

                            string location = getGeolocation();
                            if (location == "")
                            {
                                lblGeolocation.Text = "Geolocation: None";
                            }
                            else
                            {
                                lblGeolocation.Text = "Geolocation: " + location;
                            }

                            string tags = getTags();
                            if (tags == "")
                            {
                                lblTags.Text = "Tags: None";
                            }
                            else
                            {
                                lblTags.Text = "Tags: " + tags;
                            }

                            string date = getCaptureDate();
                            if (date == "")
                            {
                                lblCaptureDate.Text = "Capture date: None";
                            }
                            else
                            {
                                lblCaptureDate.Text = "Capture date: " + date;
                            }

                            string capBy = getCaptureBy();
                            if (capBy == "")
                            {
                                lblCaptureBy.Text = "Capture by: None";
                            }
                            else
                            {
                                lblCaptureBy.Text = "Capture by: " + capBy;
                            }
                        }
                    }
                    else
                    {

                        Response.Write("<script>alert('Invalid photo ID')</script>");
                        //    txtphotoId.Text = "";

                    }
                }


            }
        }

        protected void btnDeleteMetaData_Click(object sender, EventArgs e)
        {
            string userId2 = getUserId().ToString();
            string testsharememid = TestShareMemId();
            string newPhotoid2 = getNewPhotoId();//id of the photo


            //string validUser = "";
            if (txtphotoId.Text == "")
            {
                Response.Write("<script>alert('Please enter photo ID to delete')</script>");
            }

            else
            {

                if (testsharememid == "")
                {
                    string validUser2 = testUserPhoto();//user id of the received photo  kyk wie het die foto ingesit


                    if (validUser2 == userId2)
                    {

                        if (newPhotoid2 == "")
                        {
                            Response.Write("<script>alert('No photo for entered ID')</script>");

                        }
                        else
                        {
                            try
                            {
                                SqlConnection con = new SqlConnection(sqlStr);

                                con.Open();
                                SqlCommand comm;
                                string delete = "DELETE FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
                                comm = new SqlCommand(delete, con);
                                comm.ExecuteNonQuery();
                                con.Close();
                            }
                            catch
                            {
                                Response.Write("<script>alert('Connection error')</script>");
                            }
                        }

                    }
                    else
                    {

                        Response.Write("<script>alert('Invalid photo ID')</script>");
                        //    txtphotoId.Text = "";

                    }
                }
                else
                {
                    string shareMem = getShareMemId().ToString();//user id of photo shared with 5 kyk met wie was die photo geshare

                    if (shareMem == userId2)
                    {

                        if (newPhotoid2 == "")
                        {
                            Response.Write("<script>alert('No photo for entered ID')</script>");
                        }

                        else
                        {
                            try
                            {
                                SqlConnection con = new SqlConnection(sqlStr);

                                con.Open();
                                SqlCommand comm;
                                string delete = "DELETE FROM tblMetaData WHERE PhotoId = '" + txtphotoId.Text + "'";
                                comm = new SqlCommand(delete, con);
                                comm.ExecuteNonQuery();
                                con.Close();
                            }
                            catch
                            {
                                Response.Write("<script>alert('Connection error')</script>");
                            }

                        }
                    }
                }


            }
        }

        protected void btnAlbums_Click(object sender, EventArgs e)
        {
            Response.Redirect("Albums.aspx");
        }

        protected void btnUpdateMetaData_Click(object sender, EventArgs e)
        {
            string Photoid = getNewPhotoId();
            if (txtphotoId.Text == "")
            {
                Response.Write("<script>alert('Please enter photo ID')</script>");
            }
            else if (Photoid == "")
            {
                //Response.Write("<script>alert('No photo for entered ID')</script>");
                lblGeolocation.Text = "Geolocation: None";
                lblTags.Text = "Tags: None";
                lblCaptureDate.Text = "Capture date: None";
                lblCaptureBy.Text = "Capture by: None";

                string addOrUpdate = "updateAdd";
                string photoID = txtphotoId.Text;
                Session["addOrUpdate"] = addOrUpdate;
                Session["photoId"] = photoID;
                Session["photo"] = getphotoPath();

                Response.Redirect("MetaData.aspx");


            }
            else
            {
                string userId = getUserId().ToString();
                string validUser = testUserPhoto();
                string newPhotoid = getNewPhotoId();
                //string pid = getNewPhotoId();
                if (txtphotoId.Text == "")
                {
                    Response.Write("<script>alert('Please enter photo ID')</script>");
                }


                else if (validUser != userId)
                {
                    Response.Write("<script>alert('Invalid photo ID')</script>");
                    txtphotoId.Text = "";
                }
                else
                {
                    string addOrUpdate = "update";
                    string photoID = txtphotoId.Text;
                    Session["addOrUpdate"] = addOrUpdate;
                    Session["photoId"] = photoID;
                    Session["photo"] = getphotoPath();

                    Response.Redirect("MetaData.aspx");
                }

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

        protected void btnShare_Click(object sender, EventArgs e)
        {
            string shareEmail = (Session["email"].ToString());
            int UserId = getUserId();
            Session["shareemail"] = shareEmail;
            Session["ownerid"] = UserId;


            Response.Redirect("SharePhotos.aspx");
        }

        protected void btnViewShared_Click(object sender, EventArgs e)
        {
            try
            {
                BindDatalistShare();
                //int uid = getUserId();
                //SqlConnection con = new SqlConnection(sqlStr);
                //string query = "SELECT tblPhotos.PhotoPath, tblPhotos.photoId from [dbo].[tblPhotos] " +
                //    "join [dbo].[tblShared] on tblPhotos.photoid = tblShared.PhotoId join [dbo].[tblUsers] on tblShared.ShareMemId = '" + uid + "'";
                //SqlCommand cmd = new SqlCommand(query, con);
                //con.Open();
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);

                //dlShared.DataSource = dt;
                //dlShared.DataBind();
                //con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Connection error while collecting shared photos')</script>");
            }
        }

        public void BindDatalistShare()
        {

            int userid = getUserId();
            SqlConnection con = new SqlConnection(sqlStr);
            DataTable dt = new DataTable();

            con.Open();
            SqlCommand cmd;
            string photoId = "SELECT PhotoId FROM tblShared WHERE ShareMemId = '" + userid + "'";
            cmd = new SqlCommand(photoId, con);
            //int ShareMemId = (int)cmd.ExecuteScalar();

            var reader = cmd.ExecuteReader();

            var tempList = new List<int>();

            while (reader.Read())
            {
                tempList.Add(((int)reader[0]));
            }
            reader.Close();

            foreach (int row in tempList)
            {
                SqlDataAdapter adp = new SqlDataAdapter("GetPhotoData", con);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@id", row);
                adp.Fill(dt);
            }



            //adp = new SqlDataAdapter("GetPhotoData", con);
            //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //adp.SelectCommand.Parameters.AddWithValue("@id", Convert.ToInt32(ShareMemId.ToString()));
            //dt = new DataTable();
            //adp.Fill(dt);
            dlShared.DataSource = dt;
            dlShared.DataBind();
            con.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void dlimage_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("") + e.CommandArgument);
                Response.End();
            }
        }

        protected void dlShared_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("") + e.CommandArgument);
                Response.End();
            }
        }
        private int getShareMemId()
        {
            string uid = getUserId().ToString();
            SqlConnection con = new SqlConnection(sqlStr);
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT ShareMemId FROM tblShared WHERE PhotoId = '" + txtphotoId.Text + "'";
            cmd = new SqlCommand(sql, con);
            int sharememid = (int)cmd.ExecuteScalar();
            return sharememid;
        }
    }

}

        
