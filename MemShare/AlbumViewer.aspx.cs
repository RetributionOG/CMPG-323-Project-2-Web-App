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
    public partial class AlbumViewer : System.Web.UI.Page
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
            SqlDataAdapter adp = new SqlDataAdapter("gallery", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@albumid", Convert.ToInt32(Session["albumid"].ToString()));
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dlimage.DataSource = dt;
            dlimage.DataBind();
        }

        protected void btnAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("Albums.aspx");
        }

        protected void btnMore_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gallery.aspx");
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