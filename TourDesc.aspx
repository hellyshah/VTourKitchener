<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Header.Master" CodeBehind="TourDesc.aspx.cs" Inherits="VTourKitchener.TourDesc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="js/jTourDesc.js"></script>
    
    <style>
        ul {
            padding: 0 0 0 0;
            margin: 0 0 0 0;
        }

            ul li {
                list-style: none;
                margin-bottom: 25px;
            }

                ul li img {
                    cursor: pointer;
                }

        .modal-body {
            padding: 5px !important;
        }

        .modal-content {
            border-radius: 0;
        }

        .modal-dialog img {
            text-align: center;
            margin: 0 auto;
        }

        .controls {
            width: 50px;
            display: block;
            font-size: 11px;
            padding-top: 8px;
            font-weight: bold;
        }

        .next {
            float: right;
            text-align: right;
        }
        /*override modal for demo only*/
        .modal-dialog {
            max-width: 500px;
            padding-top: 90px;
        }

        @media screen and (min-width: 768px) {
            .modal-dialog {
                width: 500px;
                padding-top: 90px;
            }
        }

        @media screen and (max-width:1500px) {
            #ads {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<form id="form1" runat="server">--%>
    <input type="hidden" class="clsLatitudeGmap" />
    <input type="hidden" class="clsLogitudeGmap" />
    <input type="hidden" class="clsTourTitleGmap" />
    <input type="hidden" class="clsTourIdGmap" />
     <input id="sessionInput" type="hidden" value='<%= Session["Id"] %>' />
    <div>
        <div id="map" style="width: 100%; height: 500px; margin-top: 100px"></div>
        <script>
            function myMap() {
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
                var vLogitude = vars["vLogitude"];
                var vLAtitude = vars["vLatitude"];
                var vTourID = vars["TourID"];
                var vTourTitle = vars["TourTitle"];


                var myCenter = new google.maps.LatLng(vLAtitude, vLogitude);
                var mapCanvas = document.getElementById("map");
                var mapOptions = { center: myCenter, zoom: 5 };
                var map = new google.maps.Map(mapCanvas, mapOptions);
                var marker = new google.maps.Marker({ position: myCenter });
                marker.setMap(map);

                var infowindow = new google.maps.InfoWindow({
                    content: vTourTitle
                });
                infowindow.open(map, marker);
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(map, marker);
                });
            }


        </script>

        <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC6ulSYYn29WHPVkwN0wP9t50I7nyt4sAg&callback=myMap"></script>

    </div>
    <button type="button" id="btnAddTourImage"  class="btn btn-info btn-lg" data-toggle="modal" data-target="#modelAddImage">Add Image</button>
   
    <div class="container">
        <div class="row" style="text-align: center; border-bottom: 1px dashed #ccc; padding: 0 0 20px 0; margin-bottom: 40px;">
            <h3 style="font-family: arial; font-weight: bold; font-size: 30px;">Photo Gallery
            </h3>
            <div id="clsImageGalary"></div>
            
        </div>
    </div>
    <!-- /container -->

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!-- Modal -->
  <div class="modal fade" id="modelAddImage" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Add Image</h4>
        </div>
        <div class="modal-body">
           <table style="background-color: white">
            
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
                    <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtImageDesc" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUploadTourImage" runat="server" Text="Upload" OnClick="btnInsertTourImage_Click" />
                </td>
               
            </tr>
        </table>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>
  



</asp:Content>
