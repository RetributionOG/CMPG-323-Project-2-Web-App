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
    public partial class Search : System.Web.UI.Page
    {
        string sqlStr = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        int id;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try 
            {
                string searchField = rblSearchField.Text;
                string searchTerm = txtSearchTerm.Text;

                if (searchField == "Geographic Location")
                {
                    searchField = "Geolocation";
                }
                else if (searchField == "Captured Date (yyyy/MM/dd)")
                {
                    searchField = "CaptureDate";
                }
                else if (searchField == "Captured By")
                {
                    searchField = "CaptureBy";
                }

                SqlConnection con = new SqlConnection(sqlStr);
                string search = "SELECT PhotoId FROM tblMetaData WHERE " + searchField + " = '" + searchTerm + "'";
                SqlCommand cmd = new SqlCommand(search, con);
                con.Open();
                var reader = cmd.ExecuteReader();

                var tempList = new List<int>();

                while (reader.Read())
                {
                    tempList.Add(((int)reader[0]));  
                }

                foreach (int row in tempList)
                {
                    SqlDataAdapter adp = new SqlDataAdapter("GetPhotoData", con);
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@id", row);
                    adp.Fill(dt);
                }

                //SqlDataAdapter adp = new SqlDataAdapter("GetPhotoData", con);
                //adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                //adp.SelectCommand.Parameters.AddWithValue("@id", tempList.ToArray());
                //DataTable dt = new DataTable();
                //adp.Fill(dt);

                dlimage.DataSource = dt;
                dlimage.DataBind();
                con.Close();
            }
            catch
            {
                Response.Write("<script>alert('Connection error')</script>");
            }
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