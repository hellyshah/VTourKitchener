using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace VTourKitchener
{
    /// <summary>
    /// Summary description for VTHandlar
    /// </summary>
    public class VTHandlar : IHttpHandler, IRequiresSessionState
    {
        string vConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\TourDB.mdf"";Integrated Security=True";
        public void ProcessRequest(HttpContext context)
        {

            string vFunc = context.Request.Form["Func"];

            switch (vFunc)
            {
                case "GetTourGridAll":
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(GetTourGridAll());
                    break;
                case "GetAllUsersGrid":
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(GetAllUsersGrid());
                    break;
                case "GetMyTourGrid":
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(GetMyTourGrid(context));
                    break;
                case "InsertLikes":
                    try
                    {
                        int vTourID = Convert.ToInt32(context.Request.Form["TourID"]);
                        int vUserid = Convert.ToInt32(context.Request.Form["Userid"]);
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(InsertLikes(vTourID, vUserid));
                    }
                    catch (Exception e)
                    {
                        int vTourID = Convert.ToInt32(context.Request.Form["TourID"]);
                        int vUserid = 9999;
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(InsertLikes(vTourID, vUserid));
                    }
                    break;
                case "GetTourImagesAllByTourID":
                    int vTourID1 = Convert.ToInt32(context.Request.Form["TourID"]);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(GetTourImagesAllByTourID(vTourID1));
                    break;
                case "MakeUserAdmin":
                    int vUser = Convert.ToInt32(context.Request.Form["UserID"]);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(MakeUserAdmin(vUser));
                    break;
            }
        }

        private string InsertLikes(int vTourID, int vUserid)
        {
            try
            {
                SqlConnection con = new SqlConnection(vConnectionString);

                SqlCommand com = new SqlCommand("spInsertLikes", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter("@TourId", vTourID);
                SqlParameter p2 = new SqlParameter("@LikedBy", vUserid);

                com.Parameters.Add(p1);
                com.Parameters.Add(p2);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration done Successfully,Login to continue')", true);
                //Response.Redirect("Login.aspx");
                return "Success";
            }
            catch (Exception ex)
            {
                //Response.Redirect("WebForm1.aspx");
                return "Success1";
            }
        }

        private string MakeUserAdmin(int vUserid)
        {
            try
            {
                SqlConnection con = new SqlConnection(vConnectionString);

                SqlCommand com = new SqlCommand("spMakeUserAdmin", con);
                com.CommandType = CommandType.StoredProcedure;
                
                SqlParameter p2 = new SqlParameter("@ID", vUserid);

                
                com.Parameters.Add(p2);

                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Registration done Successfully,Login to continue')", true);
                //Response.Redirect("Login.aspx");
                return "Success";
            }
            catch (Exception ex)
            {
                //Response.Redirect("WebForm1.aspx");
                return "Success1";
            }
        }

        private string GetTourImagesAllByTourID(int vTourID)
        {
            string strHTML = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetTourImagesAllByTourID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("@TourId", vTourID);
                    cmd.Parameters.Add(p1);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        strHTML += "</div><ul class='row'>";
                        foreach (DataRow row in dt.Rows)
                        {
                            int vtourID = Convert.ToInt32(row["ID"]);
                            String vtourTitle = row["TourID"].ToString();
                            Byte[] vtourThumn = (byte[])row["Image"];
                            int vAddedBy = Convert.ToInt32(row["AddedBy"]);
                            DateTime vAddedDate = Convert.ToDateTime(row["AddedDate"]);
                            String vDescription = row["Description"].ToString();
                            strHTML += "<div class = 'clsLiImg'>";
                            strHTML += "<li class='col-lg-2 col-md-2 col-sm-3 col-xs-4'>";
                            string src = "data:image/jpeg;base64," + Convert.ToBase64String(vtourThumn);
                            strHTML += "<Image class='vImageThumb' style='width:254px;height:185px;' alt='image' class='img-responsive' value='" + vtourID + "'' src='" + src + "'/>";
                            strHTML += "</li>";
                            strHTML += "</div>";
                        }
                        strHTML += "</ul></div>";
                    }
                    else
                    {
                        strHTML += "No Images are available...";
                    }

                }
            }
            catch (Exception ex)
            {

            }
            return strHTML;
        }

        private string GetTourGridAll()
        {
            string strTourGrid = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllTours", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            int vtourID = Convert.ToInt32(row["ID"]);
                            String vtourTitle = row["TourTitle"].ToString();
                            Byte[] vtourThumn = (byte[])row["TourThumbnail"];
                            String vAddedBy = row["Username"].ToString();
                            DateTime vAddedDate = Convert.ToDateTime(row["AddedDate"]);
                            int vNearByPlaceID = Convert.ToInt32(row["NearByPlaceID"]);
                            DateTime vModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                            //int vModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                            //String vModifiedBy = row["ModifiedBy"].ToString();
                            decimal vLatitude = Convert.ToDecimal(row["Latitude"]);
                            decimal vLogitude = Convert.ToDecimal(row["Logitude"]);
                            int vLikes = 0;

                            using (SqlConnection con1 = new SqlConnection(vConnectionString))
                            {
                                SqlCommand cmd2 = new SqlCommand("spGetLikes", con);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                SqlParameter p3 = new SqlParameter("@ID", vtourID);

                                cmd2.Parameters.Add(p3);

                                SqlDataAdapter Adpt2 = new SqlDataAdapter(cmd2);
                                DataTable dt2 = new DataTable();
                                Adpt2.Fill(dt2);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dt2.Rows)
                                    {
                                        vLikes = Convert.ToInt32(row2["Count"]);
                                    }
                                }

                            }

                            strTourGrid += "<div class='col-lg-3 col-md-3 col-sm-6 col-xs-6 col-xxs-12' style='margin-top: 15px; >";
                            string src = "data:image/jpeg;base64," + Convert.ToBase64String(vtourThumn);
                            strTourGrid += "<div class='tm-home-box-2'><Image class='vtourID' style='width:254px;height:185px;' alt='image' class='img-responsive' value='" + vtourID + "'' src='" + src + "'/>";
                            strTourGrid += "<h3>" + vtourTitle + "</h3>";
                            strTourGrid += "<p class='tm-date'>" + vAddedDate + "</p>";
                            strTourGrid += "<p>" + vLikes + "</p>";
                            strTourGrid += "<div class='tm-home-box-2-container'>    <div  class='tm-home-box-2-link divclsLikes'>";
                            strTourGrid += "<input type='hidden' class='clstourID1' value='" + vtourID + "'' />";
                            strTourGrid += "<i class='fa fa-heart tm-home-box-2-icon border-right'></i></div>    <div class='tm-home-box-2-link clsTourCard'>";
                            strTourGrid += "<span class='tm-home-box-2-description'>Travel</span>";
                            strTourGrid += "<input type='hidden' class='Logitude' value='" + vLatitude + "'' />";
                            strTourGrid += "<input type='hidden' class='Latitude' value='" + vLogitude + "'>";
                            strTourGrid += "<input type='hidden' class='clstourID' value='" + vtourID + "'' />";
                            strTourGrid += "<input type='hidden' class='clsTourTitle' value='" + vtourTitle + "'' />";

                            strTourGrid += "<input type='hidden' class='AddedBy' value='" + vAddedBy + "'' />";
                            strTourGrid += "<input type='hidden' class='AddedDate' value='" + vAddedDate + "'>";
                            strTourGrid += "</div> ";


                            strTourGrid += "   <a href='#' class='tm-home-box-2-link'>";
                            strTourGrid += "<i class='fa fa-edit tm-home-box-2-icon border-left '></i></a></div></div></div>";





                        }

                        //Response.Redirect("~/Welcome.aspx", false);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                //Response.Redirect("WebForm1.aspx");
            }
            return strTourGrid;
            //return @"<div class=""col-xs-12 col-sm-4"">                        <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"">        <img src=""https://1.bp.blogspot.com/-Bii3S69BdjQ/VtdOpIi4aoI/AAAAAAAABlk/F0z23Yr59f0/s640/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html""> Bootstrap 3 Carousel FadeIn Out Effect              </a>            </h4>            <p class="""">                Tutorial to make a carousel bootstrap by adding more wonderful effect fadein ...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">        <img src=""https://3.bp.blogspot.com/-bAsTyYC8U80/VtLZRKN6OlI/AAAAAAAABjY/kAoljiMALkQ/s400/material%2Bnavbar.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/02/awesome-material-design-responsive-menu.html""> Material Design Responsive Menu              </a>            </h4>            <p class="""">                Material Design is a visual programming language made by Google. Language programming...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://codepen.io/wisnust10/full/ZWERZK/"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">        <img src=""https://4.bp.blogspot.com/-TDIJ17DfCco/Vtneyc-0t4I/AAAAAAAABmk/aa4AjmCvRck/s1600/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">5  Button Hover Animation Effects              </a>            </h4>            <p class="""">                tutorials button hover animation, although very much a hover button is very beauti...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"">        <img src=""https://1.bp.blogspot.com/-Bii3S69BdjQ/VtdOpIi4aoI/AAAAAAAABlk/F0z23Yr59f0/s640/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html""> Bootstrap 3 Carousel FadeIn Out Effect              </a>            </h4>            <p class="""">                Tutorial to make a carousel bootstrap by adding more wonderful effect fadein ...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div>";
        }

        private string GetAllUsersGrid()
        {
            string strTourGrid = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;


                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        strTourGrid += "<table style='width:100%;' class='table table-striped'><thead><tr><td>Username</td><td>Email</td><td>Mobile</td><td>IsAdmin</td><td>Make Admin</td><td>Edit Info</td></tr></thead>";
                        foreach (DataRow row in dt.Rows)
                        {
                            int vUserID = Convert.ToInt32(row["ID"]);
                            String vUseraName = row["Username"].ToString();
                            String vPassword = row["Password"].ToString();
                            String vEmail = row["Email"].ToString();
                            String vMobile = row["Mobile"].ToString();
                            Boolean vISAdmin = Convert.ToBoolean(row["ISAdmin"]);

                            string vAdmin = string.Empty;
                            if (vISAdmin)
                            {
                                vAdmin = "Yes";
                            }
                            else
                            {
                                vAdmin = "No";
                            }

                            strTourGrid += "<tr style='width:100%;'><td>" + vUseraName + "</td>";
                            strTourGrid += "<td>" + vEmail + "</td>";
                            strTourGrid += "<td>" + vMobile + "</td>";
                            strTourGrid += "<td>" + vISAdmin + "</td>";
                            strTourGrid += "<td><button type='button' class='btn btn-default btn-md btnMakeAdmin'> <input type='hidden' class='vUserIDTable' value='" + vUserID + "'' />  <span class='glyphicon glyphicon-user clsMakeAdmin'></span></button></td>";
                            strTourGrid += "<td><button type='button' class='btn btn-default btn-md btnEditUser'>   <span class='glyphicon glyphicon-user clsMakeAdmin'></span></button></td></tr>";
                        }
                        strTourGrid += "</table>";

                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {

            }
            return strTourGrid;

        }
        private string GetMyTourGrid(HttpContext context)
        {
            string strTourGrid = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(vConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetMyTours", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("@ID", Convert.ToString(context.Session["ID"]));
                    cmd.Parameters.Add(p1);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            int vtourID = Convert.ToInt32(row["ID"]);
                            String vtourTitle = row["TourTitle"].ToString();
                            //String vtourDesc = row["TourDescription1"].ToString();
                            //String vtourDesc1 = row["TourDescription2"].ToString();
                            Byte[] vtourThumn = (byte[])row["TourThumbnail"];
                            String vAddedBy = row["Username"].ToString();
                            DateTime vAddedDate = Convert.ToDateTime(row["AddedDate"]);
                            int vNearByPlaceID = Convert.ToInt32(row["NearByPlaceID"]);
                            DateTime vModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                            //int vModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                            //String vModifiedBy = row["ModifiedBy"].ToString();
                            decimal vLatitude = Convert.ToDecimal(row["Latitude"]);
                            decimal vLogitude = Convert.ToDecimal(row["Logitude"]);
                            int vLikes = 0;

                            using (SqlConnection con1 = new SqlConnection(vConnectionString))
                            {
                                SqlCommand cmd2 = new SqlCommand("spGetLikes", con);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                SqlParameter p3 = new SqlParameter("@ID", vtourID);

                                cmd2.Parameters.Add(p3);

                                SqlDataAdapter Adpt2 = new SqlDataAdapter(cmd2);
                                DataTable dt2 = new DataTable();
                                Adpt2.Fill(dt2);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dt2.Rows)
                                    {
                                        vLikes = Convert.ToInt32(row2["Count"]);
                                    }
                                }

                            }

                            strTourGrid += "<div class='col-lg-3 col-md-3 col-sm-6 col-xs-6 col-xxs-12' style='margin-top: 15px; margin-left: 10px'>";
                            string src = "data:image/jpeg;base64," + Convert.ToBase64String(vtourThumn);
                            strTourGrid += "<div class='tm-home-box-2'><Image class='vtourID' style='width:254px;height:185px;' alt='image' class='img-responsive' value='" + vtourID + "'' src='" + src + "'/>";
                            strTourGrid += "<h3>" + vtourTitle + "</h3>";
                            strTourGrid += "<p class='tm-date'>" + vAddedDate + "</p>";
                            strTourGrid += "<p>" + vLikes + "</p>";
                            strTourGrid += "<div class='tm-home-box-2-container'>    <div  class='tm-home-box-2-link divclsLikes'>";
                            strTourGrid += "<input type='hidden' class='clstourID1' value='" + vtourID + "'' />";
                            strTourGrid += "<i class='fa fa-heart tm-home-box-2-icon border-right'></i></div>    <div class='tm-home-box-2-link clsTourCard'>";
                            strTourGrid += "<span class='tm-home-box-2-description'>Travel</span>";
                            strTourGrid += "<input type='hidden' class='Logitude' value='" + vLatitude + "'' />";
                            strTourGrid += "<input type='hidden' class='Latitude' value='" + vLogitude + "'>";
                            strTourGrid += "</div> ";
                            strTourGrid += "<input type='hidden' class='clstourID' value='" + vtourID + "'' />";
                            strTourGrid += "<input type='hidden' class='clsTourTitle' value='" + vtourTitle + "'' />";

                            strTourGrid += "<input type='hidden' class='AddedBy' value='" + vAddedBy + "'' />";
                            strTourGrid += "<input type='hidden' class='AddedDate' value='" + vAddedDate + "'>";

                            strTourGrid += "   <a href='#' class='tm-home-box-2-link'>";
                            strTourGrid += "<i class='fa fa-edit tm-home-box-2-icon border-left '></i></a></div></div></div>";





                        }

                        //Response.Redirect("~/Welcome.aspx", false);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                //Response.Redirect("WebForm1.aspx");
            }
            return strTourGrid;
            //return @"<div class=""col-xs-12 col-sm-4"">                        <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"">        <img src=""https://1.bp.blogspot.com/-Bii3S69BdjQ/VtdOpIi4aoI/AAAAAAAABlk/F0z23Yr59f0/s640/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html""> Bootstrap 3 Carousel FadeIn Out Effect              </a>            </h4>            <p class="""">                Tutorial to make a carousel bootstrap by adding more wonderful effect fadein ...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">        <img src=""https://3.bp.blogspot.com/-bAsTyYC8U80/VtLZRKN6OlI/AAAAAAAABjY/kAoljiMALkQ/s400/material%2Bnavbar.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/02/awesome-material-design-responsive-menu.html""> Material Design Responsive Menu              </a>            </h4>            <p class="""">                Material Design is a visual programming language made by Google. Language programming...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://codepen.io/wisnust10/full/ZWERZK/"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">        <img src=""https://4.bp.blogspot.com/-TDIJ17DfCco/Vtneyc-0t4I/AAAAAAAABmk/aa4AjmCvRck/s1600/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"">5  Button Hover Animation Effects              </a>            </h4>            <p class="""">                tutorials button hover animation, although very much a hover button is very beauti...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/5-button-hover-animation-effects-css3.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div><div class=""col-xs-12 col-sm-4"">    <div class=""card"">        <a class=""img-card"" href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"">        <img src=""https://1.bp.blogspot.com/-Bii3S69BdjQ/VtdOpIi4aoI/AAAAAAAABlk/F0z23Yr59f0/s640/cover.jpg"" />      </a>        <div class=""card-content"">            <h4 class=""card-title"">                <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html""> Bootstrap 3 Carousel FadeIn Out Effect              </a>            </h4>            <p class="""">                Tutorial to make a carousel bootstrap by adding more wonderful effect fadein ...            </p>        </div>        <div class=""card-read-more"">            <a href=""http://www.fostrap.com/2016/03/bootstrap-3-carousel-fade-effect.html"" class=""btn btn-link btn-block"">                Read More            </a>        </div>    </div></div>";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}