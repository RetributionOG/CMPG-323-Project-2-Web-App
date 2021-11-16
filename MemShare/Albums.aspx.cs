﻿using System;
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
                Binddatalist();
            }
        }
        public void Binddatalist()
        {
            SqlConnection con = new SqlConnection(sqlStr);
            SqlDataAdapter adp = new SqlDataAdapter("GetAlbum", con);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

        protected void btncreate_Click(object sender, EventArgs e)
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
                Response.Redirect("PhotoGallery.aspx");
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('A connection error occured. Please try again')</script>");
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            int id = Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString());
            Session["albumid"] = id;
            Response.Redirect("gallery.aspx");
        }
    }
}