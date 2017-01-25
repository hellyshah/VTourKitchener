using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VTourKitchener
{
    public partial class UserDashBoard : System.Web.UI.Page
    {
        string vConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\TourDB.mdf"";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_NearbyPlaces();

            }
        }

        public void Bind_NearbyPlaces()
        {
            using (SqlConnection con = new SqlConnection(vConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblNearbyMaster", con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                drpNearbyPlaces.DataSource = dt;
                drpNearbyPlaces.DataBind();
                drpNearbyPlaces.Items.Clear();
                drpNearbyPlaces.DataTextField = "Placename";
                drpNearbyPlaces.DataValueField = "Id";
                drpNearbyPlaces.DataBind();
                drpNearbyPlaces.Items.Insert(0, "Please Select NearBy Places");
            }
        }

        protected void drpNearbyPlaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            string i = drpNearbyPlaces.SelectedValue.ToString();
            int drpindex = drpNearbyPlaces.SelectedIndex;
            if (!drpNearbyPlaces.SelectedIndex.Equals(0))
            {
                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tblNearbyMaster where ID = '" + drpNearbyPlaces.SelectedValue.ToString() + "'", con);
                    SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    Adpt.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            lblLatitude.Text = row["Latitude"].ToString();
                            lblLogitude.Text = row["Logitude"].ToString();
                        }
                    }

                }
            }
            mp1.Show();
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(filename);
            int fileSize = postedFile.ContentLength;

            if (filename.Trim().Equals(string.Empty) || drpNearbyPlaces.SelectedValue.Equals(0))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter All the Values')", true);
            }
            else
            {
                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".jpeg"
                    || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp")
                {
                    Stream stream = postedFile.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);



                    using (SqlConnection con = new SqlConnection(vConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("spInsertTourMaster", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter paramTourTitle = new SqlParameter()
                        {
                            ParameterName = "@TourTitle",
                            Value = txtTourTitle.Text
                        };
                        cmd.Parameters.Add(paramTourTitle);

                        SqlParameter paramTourThumbnail = new SqlParameter()
                        {
                            ParameterName = "@TourThumbnail",
                            Value = bytes
                        };
                        cmd.Parameters.Add(paramTourThumbnail);

                        SqlParameter paramAddedBy = new SqlParameter()
                        {
                            ParameterName = "@AddedBy",
                            Value = Convert.ToInt32(Session["Id"])
                        };
                        cmd.Parameters.Add(paramAddedBy);

                        SqlParameter paramAddedDate = new SqlParameter()
                        {
                            ParameterName = "@AddedDate",
                            Value = DateTime.Now.ToString("yyyy-MM-dd")
                        };
                        cmd.Parameters.Add(paramAddedDate);

                        SqlParameter paramNearByPlaceID = new SqlParameter()
                        {
                            ParameterName = "@NearByPlaceId",
                            Value = drpNearbyPlaces.SelectedValue.ToString()
                        };
                        cmd.Parameters.Add(paramNearByPlaceID);


                        SqlParameter paramModifiedDate = new SqlParameter()
                        {
                            ParameterName = "@ModifiedDate",
                            Value = DateTime.Now.ToString("yyyy-MM-dd")
                        };
                        cmd.Parameters.Add(paramModifiedDate);

                        SqlParameter paramModiFiedBy = new SqlParameter()
                        {
                            ParameterName = "@ModiFiedBy",
                            Value = 1
                        };
                        cmd.Parameters.Add(paramModiFiedBy);


                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Successful')", true);
                        txtTourTitle.Text = string.Empty;
                        FileUpload1.PostedFile.InputStream.Dispose();
                        lblLogitude.Text = "";
                        lblLatitude.Text = "";

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only images (.jpg, .png, .gif and .bmp) can be uploaded')", true);
                    txtTourTitle.Text = string.Empty;
                    FileUpload1.PostedFile.InputStream.Dispose();
                    lblLogitude.Text = "";
                    lblLatitude.Text = "";
                }
            }
        }

        protected void btnClose(object sender, EventArgs e)
        {
            txtTourTitle.Text = string.Empty;
            FileUpload1.PostedFile.InputStream.Dispose();
            
        }
    }
}