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

            Response.Clear();
        }

        private void fill()
        {
            ////string userId;
            //int userId = getUserId();
            //////string email = (Session["email"].ToString());
            //SqlConnection con = new SqlConnection(sqlStr);
            ////con.Open();
            ////SqlCommand cmd;
            ////string sql = "SELECT Photo FROM tblPhotos WHERE UserId = '" + userId + "'";
            ////cmd = new SqlCommand(sql, con);
            ////string photo = (string)cmd.ExecuteScalar();//"File uploaded to " + "~/Images/" + FileUpload1.FileName;



            ////Label2.Text = photo;
            //SqlDataAdapter adap = new SqlDataAdapter("SELECT PhotoPath FROM tblPhotos where UserId = '" + userId + "'", con);
            //DataSet ds = new DataSet();
            //adap.Fill(ds);
            //DataList1.DataSource = ds;
            //DataList1.DataBind();
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

                    Session["photo"] = path;

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
            //fill();
            BindDatalist();
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
                    SqlCommand comm;
                    delete = "DELETE FROM tblPhotos WHERE photoId = '" + txtphotoId.Text + "'";
                    comm = new SqlCommand(delete, con);
                    comm.ExecuteNonQuery();
                    con.Close();
                    BindDatalist();
                    //DisplayAll();
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

        private string getPhotoId()
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
                if(flag == true)
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



        protected void btnViewMetaData_Click(object sender, EventArgs e)
        {
            string photoid = getPhotoId();
            if (txtphotoId.Text == "")
            {
                Response.Write("<script>alert('Please enter photo ID')</script>");
            }
           else if(photoid == "")
            {
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

        protected void btnDeleteMetaData_Click(object sender, EventArgs e)
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

        protected void btnAlbums_Click(object sender, EventArgs e)
        {
            Response.Redirect("Albums.aspx");
        }

        protected void btnUpdateMetaData_Click(object sender, EventArgs e)
        {
            string addOrUpdate = "update";
            string photoID = txtphotoId.Text;
            Session["addOrUpdate"] = addOrUpdate;
            Session["photoId"] = photoID;

            Response.Redirect("MetaData.aspx");
        }

        protected void dlimage_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("/Images/") + e.CommandArgument);
                Response.End();
            }
        }
    }
}