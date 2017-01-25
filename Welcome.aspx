<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Header.Master" CodeBehind="Welcome.aspx.cs" Inherits="VTourKitchener.Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/Main.css" rel="stylesheet" />
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jWelcome.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <input id="sessionInput" type="hidden" value='<%= Session["Id"] %>' />
    <!-- Carousel -->
    <div id="carousel-example-generic" class="carousel slide carousel-fade" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target='#carousel-example-generic' data-slide-to='0' class='active'>
                <asp:Image ID="Image1" runat="server" ImageUrl="img/12.jpg"
                    Width="50px" Height="50px" CssClass="img-circle" /></li>
            <li data-target='#carousel-example-generic' data-slide-to='1'>
                <asp:Image ID="Image2" runat="server" ImageUrl="img/13.jpg"
                    Width="50px" Height="50px" CssClass="img-circle" /></li>

            <li data-target='#carousel-example-generic' data-slide-to='2'>
                <asp:Image ID="Image3" runat="server" ImageUrl="img/14.jpg"
                    Width="50px" Height="50px" CssClass="img-circle" /></li>
        </ol>
        <!-- Wrapper for slides -->
        <div class="carousel-inner">
            <div class="item active one">
                <!-- <img src="#" alt="" /> -->
                <div class="main-text hidden-xs">
                    <div class="col-md-12 text-center">
                        <h1>Arise! Awake! and stop not until the goal is reached.<br />
                            <span class="yellow">- Swami Vivekananda </span></h1>
                        <div class="">
                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-clear btn-sm btn-min-block">Login</asp:HyperLink>
                            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-clear btn-sm btn-min-block                           ">Registration</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item two">
                <div class="main-text hidden-xs">
                    <div class="col-md-12 text-center">
                        <h1>A <b class="yellow">Goal</b> without <b class="yellow">a plan</b><br />
                            <span class="span">is just a wish</span></h1>
                        <div class="">
                            <asp:HyperLink ID="HyperLink3" runat="server" CssClass="btn btn-clear btn-sm btn-min-block">learn More</asp:HyperLink>
                            <asp:HyperLink ID="HyperLink4" runat="server" CssClass="btn btn-clear btn-sm btn-min-block                           ">Download</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
            <div class="item three">
                <div class="main-text hidden-xs">
                    <div class="col-md-12 text-center">
                        <h1>You cannot<b class="yellow"> believe</b> in <b class="yellow">God</b><br />
                            <span class="span">until you believe in yourself.</span></h1>
                        <div class="">
                            <asp:HyperLink ID="HyperLink5" runat="server" CssClass="btn btn-clear btn-sm btn-min-block">Android</asp:HyperLink>
                            <asp:HyperLink ID="HyperLink6" runat="server" CssClass="btn btn-clear btn-sm btn-min-block                           ">Apple</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Controls -->
        <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left"></span></a><a class="right carousel-control"
                href="#carousel-example-generic" role="button" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
    </div>

    

    
    <!-- Modal -->
    <%--<div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Tour</h4>
                </div>
                <div class="modal-body">
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
                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="Upload Image"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTourDesription" runat="server" Text="Enter Tour Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTourDescription" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTourDesription2" runat="server" Text="Enter Tour Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTourDescription1" runat="server"></asp:TextBox>
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
                    <asp:Label ID="lblLatitude" runat="server" Text="Enter Tour Latitude"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLatitude" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblLogitude" runat="server" Text="Enter Tour Logitude"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLogitude" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
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
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>--%>

    <div class="tm-gray-bg">
        <div >
            <div class="row">
                <div class="tm-section-header" style="margin-top: 10px">
                    <div class="col-lg-3 col-md-3 col-sm-3">
                        <hr />
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6">
                        <h2 class="tm-section-title">Welcome to Kitchener </h2>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3">
                        <hr />
                    </div>
                </div>
            </div>
            <div class="row" id="mainCards">
                
            </div>

        </div>
    </div>
</asp:Content>

