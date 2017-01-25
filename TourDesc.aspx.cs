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
    public partial class TourDesc : System.Web.UI.Page
    {
        string vConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\TourDB.mdf"";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnInsertTourImage_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string filename = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(filename);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".jpeg"
                || fileExtension.ToLower() == ".png" || fileExtension.ToLower() == ".bmp")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                Byte[] bytes = binaryReader.ReadBytes((int)stream.Length);



                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spInsertTourImage", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramTourTitle = new SqlParameter()
                    {
                        ParameterName = "@TourID",
                        Value = Request.QueryString["TourID"]
                    };
                    cmd.Parameters.Add(paramTourTitle);

                    SqlParameter paramTourThumbnail = new SqlParameter()
                    {
                        ParameterName = "@Image",
                        Value = bytes
                    };
                    cmd.Parameters.Add(paramTourThumbnail);

                    SqlParameter paramAddedBy = new SqlParameter()
                    {
                        ParameterName = "@AddedBy",
                        Value =1
                        //Value = Convert.ToInt32( Session["Id"])
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
                        ParameterName = "@Description",
                        Value = txtImageDesc.Text
                    };
                    cmd.Parameters.Add(paramNearByPlaceID);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Successful')", true);                    
                    Response.Redirect(Request.RawUrl);
                    FileUpload1.PostedFile.InputStream.Dispose();           
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only images (.jpg, .png, .gif and .bmp) can be uploaded')", true);
                
            }
        }
    }
}