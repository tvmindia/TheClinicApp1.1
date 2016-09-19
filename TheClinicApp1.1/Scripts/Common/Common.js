

function getJsonData(data, page) {
    var jsonResult = {};
    // $("#loadingimage").show();
    var req = $.ajax({
        type: "post",
        url: page,
        data: data,
        delay: 3,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"

    }).done(function (data) {

        //     $("#loadingimage").hide();
        jsonResult = data;
    });
    return jsonResult;
}

function DisplayAlertMessages(lblclass, lblcaptn, lblmsg)
{
    var lblErrorCaption = document.getElementById('lblErrorCaption');
    var lblMsgges = document.getElementById('lblMsgges');
    var Errorbox = document.getElementById('Errorbox');

    Errorbox.style.display = "";
    Errorbox.className = lblclass;
    lblErrorCaption.innerHTML = lblcaptn;
    lblMsgges.innerHTML = lblmsg;

}

//--------------------- * Converting Start time from 24 hr Format to 12hr format * --------------------//  
function ConvertTimeFormatFrom24hrTo12hr(Time) {


    var TimeIn24hrFormat = Time;
    var hourEnd = TimeIn24hrFormat.indexOf(":");
    var H = +TimeIn24hrFormat.substr(0, hourEnd);
    var h = H % 12 || 12;
    var ampm = parseInt(H) < 12 ? "AM" : "PM";
    //TimeIn12hrFormat = h + TimeIn24hrFormat.substr(hourEnd, 4) + ampm;
    if (parseInt(H) == 0) {

        ampm = "AM";
    }


    TimeIn12hrFormat = moment(Time, ["h:mm A"]).format("hh:mm") + ampm;

    return TimeIn12hrFormat;
}



var AppoinmentCancellationMessage = '<p>' + "Dear" + '<strong>' + " %PATIENT NAME%," + '</strong>' + '<br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp' + "Your Appoinment with Dr." + '<strong>' + "%DOCTOR NAME%" + '</strong>' + " On " + '<strong>' + "%DATE%" + '</strong>' + "  At " + '<strong>' + " %TIME%" + '</strong>' + "  has been cancelled due to" + '<strong>' + " %REASON% " + '</strong>' +'<br />' + '</p>';

var AppoinmentCancellationMessageWithoutHtml =  "Dear %PATIENT NAME%, Your Appoinment with Dr.%DOCTOR NAME% On %DATE% At %TIME% has been cancelled due to %REASON%.";

var CancelReason = "Some inconvenience";