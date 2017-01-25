$(document).ready(function () {

    //var vUserId = $('sessionInput').val();
    //if (vUserId == null) {
    //    $('#btnAddTourImage').hide();
    //}

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

    GetTourImagesAllByTourID(vTourID);
    $(document).on('click', '.btnAddTourImage', function () {  // click for update
        //alert("Handler for .click() called.");      
       
        //InsertLikes(vTourID);

    });

    $(document).on('click', '.clsLiImg', function () {
        var src = $(this).attr('src');
        var img = '<img src="' + src + '" class="img-responsive"/>';

        //start of new code new code
        var index = $(this).parent('li').index();

        var html = '';
        html += img;
        html += '<div style="height:25px;clear:both;display:block;">';
        html += '<a class="controls next" href="' + (index + 2) + '">next &raquo;</a>';
        html += '<a class="controls previous" href="' + (index) + '">&laquo; prev</a>';
        html += '</div>';

        $('#myModal').modal();
        $('#myModal').on('shown.bs.modal', function () {
            $('#myModal .modal-body').html(html);
            //new code
            $('a.controls').trigger('click');
        })
        $('#myModal').on('hidden.bs.modal', function () {
            $('#myModal .modal-body').html('');
        });




    });

   
});/// end of document.ready

$(document).on('click', 'a.controls', function () {
    var index = $(this).attr('href');
    var src = $('ul.row li:nth-child(' + index + ') img').attr('src');

    $('.modal-body img').attr('src', src);

    var newPrevIndex = parseInt(index) - 1;
    var newNextIndex = parseInt(newPrevIndex) + 2;

    if ($(this).hasClass('previous')) {
        $(this).attr('href', newPrevIndex);
        $('a.next').attr('href', newNextIndex);
    } else {
        $(this).attr('href', newNextIndex);
        $('a.previous').attr('href', newPrevIndex);
    }

    var total = $('ul.row li').length + 1;
    //hide next button
    if (total === newNextIndex) {
        $('a.next').hide();
    } else {
        $('a.next').show()
    }
    //hide previous button
    if (newPrevIndex === 0) {
        $('a.previous').hide();
    } else {
        $('a.previous').show()
    }


    return false;
});

function GetTourImagesAllByTourID(vTourID) {
    $.post("VTHandlar.ashx", {
        func: 'GetTourImagesAllByTourID', TourID: vTourID
    }, function (result) {
        //alert(result);
        $('#clsImageGalary').html(result);
    });
}