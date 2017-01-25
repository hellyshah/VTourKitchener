using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VTourKitchener
{
    public partial class Login : System.Web.UI.Page
    {
        string vConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=""|DataDirectory|\TourDB.mdf"";Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUserName.Text;
                string password = txtPassword.Text;
                string pattern = null;
                pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (username.Trim().Equals(string.Empty))
                {
                    lblLoginValidaton.Text = "Please Enter Email as Username";
                }
                else
                {

                    if (password.Trim().Equals(string.Empty))
                    {
                        lblLoginValidaton.Text = "Please Enter Password";
                    }
                    else
                    {
                        if (!Regex.IsMatch(username, pattern))
                        {
                            lblLoginValidaton.Text = "Please Enter Your Email";
                        }
                        else
                        {
                            using (SqlConnection con = new SqlConnection(vConnectionString))
                            {

                                SqlCommand com = new SqlCommand("spCheckUser", con);
                                com.CommandType = CommandType.StoredProcedure;
                                SqlParameter p1 = new SqlParameter("username", txtUserName.Text);
                                SqlParameter p2 = new SqlParameter("password", txtPassword.Text);
                                com.Parameters.Add(p1);
                                com.Parameters.Add(p2);
                                con.Open();
                                DataTable dtUserData = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter(com);
                                da.Fill(dtUserData);

                                if (dtUserData.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dtUserData.Rows)
                                    {
                                        Session["Id"] = row["Id"].ToString();
                                        Session["Username"] = row["Username"].ToString();
                                        Session["IsAdmin"] = row["IsAdmin"].ToString();
                                    }
                                    if (Convert.ToBoolean(Session["IsAdmin"]))
                                    {
                                        Response.Redirect("~/AdminDashboard.aspx", false);
                                    }
                                    else
                                    {
                                        Response.Redirect("~/UserDashBoard.aspx", false);
                                    }

                                }
                                else
                                {
                                    lblLoginValidaton.Text = "Invalid User name and password";

                                }
                            }
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            //string strcon = "Data Source=.;uid=sa;pwd=Password$2;database=master";
            try
            {
                if (!txtUserNameSignUp.Text.Trim().Equals("") && !txtPassWordtoReg.Text.Trim().Equals("")
                    && !txtEmail.Text.Trim().Equals("") && !txtMobile.Text.Trim().Equals(""))
                {
                    string pattern = null;
                    pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";


                    if (!Regex.IsMatch(txtUserNameSignUp.Text.Trim().ToString(), pattern))
                    {
                        //lblValidationReg.Text = "Please Enter Your Email";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Your Email')", true);
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(vConnectionString))
                        {

                            SqlCommand com = new SqlCommand("spCheckEmail", con);
                            com.CommandType = CommandType.StoredProcedure;
                            SqlParameter p1 = new SqlParameter("username", txtEmail.Text);
                            com.Parameters.Add(p1);
                            con.Open();
                            DataTable dtUserData = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            da.Fill(dtUserData);

                            if (dtUserData.Rows.Count > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "Please Sign Up with another Email ID as this id is in Use", true);
                            }
                            else
                            {
                                SqlCommand com1 = new SqlCommand("spInsertUser", con);
                                com1.CommandType = CommandType.StoredProcedure;
                                SqlParameter p0 = new SqlParameter("@Username", txtUserNameSignUp.Text);
                                SqlParameter p2 = new SqlParameter("@Password", txtPassWordtoReg.Text);
                                SqlParameter p3 = new SqlParameter("@Email", txtEmail.Text);
                                SqlParameter p4 = new SqlParameter("@Mobile", txtMobile.Text);
                                SqlParameter p5 = new SqlParameter("@ISAdmin", false);
                                com1.Parameters.Add(p0);
                                com1.Parameters.Add(p2);
                                com1.Parameters.Add(p3);
                                com1.Parameters.Add(p4);
                                com1.Parameters.Add(p5);
                                //con.Open();
                                com1.ExecuteNonQuery();
                                
                                Response.Redirect("~/UserDashBoard.aspx", false);
                            }
                        }

                    }


                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Mandatory Fields')", true);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("Error.aspx", false);
            }


        }
    }
}