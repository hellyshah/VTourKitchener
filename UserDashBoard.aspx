<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDashBoard.aspx.cs" Inherits="VTourKitchener.UserDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap Core CSS -->
    <title>Virtual Tour to Kitchener</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link href="css/Main.css" rel="stylesheet" />
    <!-- SmartMenus jQuery Bootstrap Addon CSS -->
    <link href="css/jquery.smartmenus.bootstrap.css" rel="stylesheet" />
    <link href="css/owl/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="css/owl/owl.theme.css" rel="stylesheet" type="text/css" />
    <link href="css/owl/owl.transitions.css" rel="stylesheet" type="text/css" />
    <!-- Custom CSS -->
    <link href="css/1-col-portfolio.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Ubuntu:300,400,700' rel='stylesheet' type='text/css' />
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:300,400italic,700italic,400,700" rel="stylesheet" type="text/css" />
    <!-- Required plugin - Animate.css -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/3.4.0/animate.min.css" />
    <link href="fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <!-- Header Navbar fixed top -->
        <div class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                            class="icon-bar"></span><span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#"><i class="fa fa-diamond"></i>Virtual Tour to Kitchener</a>
                </div>
                <div class="navbar-collapse collapse">
                    <!-- Left nav -->
                    <ul class="nav navbar-nav navbar-right">
                        <li runat="server" id="home">
                            <a>HI  <%= Session["Username"] %></a> </li>
                        <li runat="server" id="LogOut"><a href="Welcome.aspx" runat="server">Logout</a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <%--main Content--%>
        <input id="sessionInput" type="hidden" value='<%= Session["Id"] %>' />

        <div>
            
            <%--<button type="button" id="btnAddTour" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>--%>
        </div>
        <ul class="nav nav-pills" style="margin-top: 51px">
            <li class="clsAllTours active"><a>All Tours</a></li>
            <li class="clsMyTours active"><a>My Tours</a></li>
            <li class="clsAddTour active"><asp:LinkButton ID="btnAddTourlnk" runat="server" >Add Tour</asp:LinkButton></li>
            
        </ul>

        <div class="row" id="mainCards"  style="margin-left:15px"></div>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <!-- ModalPopupExtender -->
        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnAddTourlnk"
            CancelControlID="Button2" BackgroundCssClass="Background">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
            <table style="background-color: white">
                <tr>
                    <td>
                        <asp:Label ID="bblTourTitle" runat="server" Text="Enter Tour Title"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTourTitle" runat="server"></asp:TextBox>
                    </td>
                </tr>
                

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Select Nearby Places"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpNearbyPlaces" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpNearbyPlaces_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Latitude"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:Label ID="lblLatitude" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Logitude"></asp:Label>
                        
                    </td>
                    <td>
                        <asp:Label ID="lblLogitude" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Upload Image"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
               
                <tr>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Close" OnClick="btnClose" />

                    </td>
                </tr>
            </table>
        </asp:Panel>

        <footer id="fh5co-footer" class="padding100">
            <div class="fh5co-footer-content">
                <div class="container">
                    <div class="row">
                        <div class="col-md-3 col-sm-4 col-md-push-3 animated wow fadeInLeft" data-wow-delay="0.2s">
                            <h3 class="fh5co-lead">About</h3>
                            <ul>
                                <li><a href="#">Tour</a></li>
                                <li><a href="#">Company</a></li>
                                <li><a href="#">Jobs</a></li>
                                <li><a href="#">Blog</a></li>
                                <li><a href="#">New Features</a></li>
                                <li><a href="#">Contact Us</a></li>
                            </ul>
                        </div>
                        <div class="col-md-3 col-sm-4 col-md-push-3 animated wow fadeInLeft" data-wow-delay="0.4s">
                            <h3 class="fh5co-lead">Support</h3>
                            <ul>
                                <li><a href="#">Help Center</a></li>
                                <li><a href="#">Terms of Service</a></li>
                                <li><a href="#">Security</a></li>
                                <li><a href="#">Privacy Policy</a></li>
                                <li><a href="#">Careers</a></li>
                                <li><a href="#">More Apps</a></li>
                            </ul>
                        </div>
                        <div class="col-md-3 col-sm-4 col-md-push-3 animated wow fadeInLeft" data-wow-delay="0.6s">
                            <h3 class="fh5co-lead">More Links</h3>
                            <ul>
                                <li><a href="#">Feedback</a></li>
                                <li><a href="#">Frequently Ask Questions</a></li>
                                <li><a href="#">Terms of Service</a></li>
                                <li><a href="#">Privacy Policy</a></li>
                                <li><a href="#">Careers</a></li>
                                <li><a href="#">More Apps</a></li>
                            </ul>
                        </div>

                        <div class="col-md-3 col-sm-12 col-md-pull-9 animated wow fadeInLeft" data-wow-delay="0.8s">
                            <div class="fh5co-footer-logo"><a href="index.html">Virtual Tour to Kitchener</a></div>
                            <p class="fh5co-copyright">
                                <small>&copy; 2016. All Rights Reserved.
                                <br>
                                    by <a href="http://aboostrap.com/" target="_blank">aspxtemplates.com</a> Images: <a href="http://aspxtemplates.com/" target="_blank">Pexels</a></small>
                            </p>
                            <p class="fh5co-social-icons">
                                <a href="#"><i class="fa fa-twitter"></i></a>
                                <a href="#"><i class="fa fa-facebook"></i></a>
                                <a href="#"><i class="fa fa-instagram"></i></a>
                                <a href="#"><i class="fa fa-dribbble"></i></a>
                                <a href="#"><i class="fa fa-youtube"></i></a>
                            </p>
                        </div>

                    </div>
                </div>
            </div>
        </footer>
        <!-- jQuery -->
        <script src="js/jquery-1.11.1.min.js"></script>
        <script src="js/jquery.js"></script>
        <script src="js/jUserDashBoard.js"></script>
        <!-- Bootstrap Core JavaScript -->
        <script src="js/bootstrap.min.js"></script>
        <script src="js/wow.min.js" type="text/javascript"></script>
        <script src="js/tutorial.js"></script>
        <script src="css/owl/owl.carousel.js" type="text/javascript"></script>
        <!-- SmartMenus jQuery plugin -->
        <script type="text/javascript" src="js/jquery.smartmenus.js"></script>
        <!-- SmartMenus jQuery Bootstrap Addon -->
        <script type="text/javascript" src="js/jquery.smartmenus.bootstrap.js"></script>
        <script src="js/contact.js" type="text/javascript"></script>
    </form>
</body>
</html>
