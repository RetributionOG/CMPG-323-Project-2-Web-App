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
    public partial class Gallery : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Visible = false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlStr);
            int UserId = getUserId();
            string filepath = Server.MapPath("/Images/") + Guid.NewGuid() + FileUpload.PostedFile.FileName;
            FileUpload.SaveAs(filepath);
            string fl = filepath.Substring(filepath.LastIndexOf("\\"));
            string[] split = fl.Split('\\');
            string newpath = split[1];
            string imagepath = "/Images/" + newpath;
            con.Open();
            SqlCommand cmd = new SqlCommand("InsertPhoto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@albumid", Convert.ToInt32(Session["albumid"].ToString()));
            cmd.Parameters.AddWithValue("@photopath", newpath);
            cmd.Parameters.AddWithValue("@userid", UserId);
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            id = (int)cmd.Parameters["@id"].Value;
            BindGrid();
        }
        public void BindGrid()
        {
            SqlConnection con = new SqlConnection(sqlStr);
            SqlDataAdapter adp = new SqlDataAdapter("GetPhotoData", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            grdPhoto.DataSource = dt;
            grdPhoto.DataBind();
            btnSave.Visible = true;
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

        protected void btnGallery_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlbumViewer.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Albums.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlStr);
            foreach (GridViewRow gvrow in grdPhoto.Rows)
            {
                int id = Convert.ToInt32(grdPhoto.DataKeys[gvrow.RowIndex].Value.ToString());
                string name = ((TextBox)gvrow.FindControl("TextBox1")).Text;
                SqlCommand cmd = new SqlCommand("UpdatePhoto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Photo", name);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();
                grdPhoto.DataSource = null;
                grdPhoto.DataBind();
            }
        }
    }
}