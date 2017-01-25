$(document).ready(function () {

    GetTourGridAll();

    $(document).on('click', '.clsAllTours', function () {
        //$('.clsAllTours').addClass('active');
        //$('.clsMyTours').removeClass('active');
        //$('.clsAddTour').removeClass('active');
        GetTourGridAll();

    });

    $(document).on('click', '.clsMyTours', function () {
        //$('.clsMyTours').addClass('active');
        //$('.clsAllTours').removeClass('active');
        //$('.clsAddTour').removeClass('active');
        GetMyTourGrid();
    });
    $(document).on('click', '.divclsLikes', function () {  // click for update
        //alert("Handler for .click() called.");      
        var vTourID = $(this).find('.clstourID1').val();
        var vUserid = $('#sessionInput').val();
        InsertLikes(vTourID, vUserid);

    });
    $(document).on('click', '.clsTourCard', function () {  // click for update
        //alert("Handler for .click() called.");
        var vLogitude = $(this).find('.Logitude').val();
        var vLAtitude = $(this).find('.Latitude').val();
        var vTourID = $(this).find('.clstourID').val();
        var vTourTitle = $(this).find('.clsTourTitle').val();
        window.location = "TourDesc.aspx?vLatitude=" + vLAtitude + "&vLogitude=" + vLogitude + "&TourID=" + vTourID + "&TourTitle=" + vTourTitle;
    });

});/// end of document.ready


function GetTourGridAll() {
    $.post("VTHandlar.ashx", {
        func: 'GetTourGridAll',
    }, function (result) {
        //alert(result);
        $('#mainCards').html(result);
    });
}
function GetMyTourGrid() {
    $.post("VTHandlar.ashx", {
        func: 'GetMyTourGrid',
    }, function (result) {
        //alert(result);
        $('#mainCards').html(result);
    });
}

function InsertLikes(vTourID, vUserid) {
    $.post("VTHandlar.ashx", {
        func: 'InsertLikes', TourID: vTourID, Userid: vUserid
    }, function (result) {
        //alert(result);
        //  location.reload();
        GetTourGridAll();
        //$('#mainCards').html(result);
    });


}



