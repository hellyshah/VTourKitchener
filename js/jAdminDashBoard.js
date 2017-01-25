$(document).ready(function () {

    GetTourGridAll();

    $(document).on('click', '.clsAllTours', function () {
        //$('.clsAllTours').addClass('active');
        //$('.clsMyTours').removeClass('active');
        GetTourGridAll();

    });

    $(document).on('click', '.clsMyTours', function () {
        //$('.clsMyTours').addClass('active');
        //$('.clsAllTours').removeClass('active');
        GetMyTourGrid();
    });
    $(document).on('click', '.clsAllUsers', function () {
        GetAllUsersGrid();
    });

    
    $(document).on('click', '.divclsLikes', function () {  // click for update
        //alert("Handler for .click() called.");      
        var vTourID = $(this).find('.clstourID1').val();
        var vUserid = $('#sessionInput').val();
        InsertLikes(vTourID, vUserid);

    });
    

    $(document).on('click', '.btnMakeAdmin', function () {  // click for update
        //alert("Handler for .click() called.");
        var vUserID = $(this).find('.vUserIDTable').val();
        MakeUserAdmin(vUserID);
       
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

function MakeUserAdmin(vUserID) {
    $.post("VTHandlar.ashx", {
        func: 'MakeUserAdmin', UserID : vUserID 
    }, function (result) {
        //alert(result);
        if (result == "Success") {
            alert("This user have admin rights");
        }
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
function GetAllUsersGrid() {
    $.post("VTHandlar.ashx", {
        func: 'GetAllUsersGrid',
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


