using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MemShare
{
    public partial class Albums : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDatalist();
            }
        }
        public void BindDatalist()
        {
            SqlConnection con = new SqlConnection(sqlStr);
            SqlDataAdapter adp = new SqlDataAdapter("GetAlbum", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@useremail", Session["email"]);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dlImages.DataSource = dt;
            dlImages.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlStr);

            try
            {
                string email = (Session["email"].ToString());
                string filepath = Server.MapPath("/Images/") + Guid.NewGuid() + albumcover.PostedFile.FileName;
                albumcover.SaveAs(filepath);
                string fl = filepath.Substring(filepath.LastIndexOf("\\"));
                string[] split = fl.Split('\\');
                string newpath = split[1];
                string imagepath = "/Images/" + newpath;
                SqlCommand cmd = new SqlCommand("InsertAlbum", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@name", txtalbumname.Text);
                cmd.Parameters.AddWithValue("@albumcover", imagepath);
                cmd.Parameters.AddWithValue("@owneremail", email);
                cmd.Parameters.Add("@AlbumId", SqlDbType.Int);
                cmd.Parameters["@AlbumId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Session["albumid"] = cmd.Parameters["@AlbumId"].Value;
                Response.Redirect("Gallery.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('A connection error occured. Please try again')</script>");
            }
        }

        protected void dlImages_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int id = Convert.ToInt32(dlImages.DataKeys[e.Item.ItemIndex].ToString());
            Session["albumid"] = id;
            Response.Redirect("AlbumViewer.aspx");
        }

        protected void btnDeleteAlbum_Click(object sender, EventArgs e)
        {
            if (txtDelete.Text == "")
            {
                Response.Write("<script>alert('Please enter an album name')</script>");

            }
            else
            {
                try
                {
                    string albumId = getAlbumId(txtDelete.Text);

                    SqlConnection con = new SqlConnection(sqlStr);

                    con.Open();
                    SqlCommand com;
                    string delete = "DELETE FROM tblAlbum WHERE AlbumId = '" + albumId + "'";
                    com = new SqlCommand(delete, con);
                    com.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand comm;
                    delete = "DELETE FROM tblPhotos WHERE albumId = '" + albumId + "'";
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

        private string getAlbumId(string albumName)
        {
            string albumId;
            SqlConnection con = new SqlConnection(sqlStr);
            con.Open();
            SqlCommand cmd;
            string sql = "SELECT AlbumId FROM tblAlbum WHERE AlbumName = '" + albumName + "'";
            cmd = new SqlCommand(sql, con);
            albumId = cmd.ExecuteScalar().ToString();
            return albumId;
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}