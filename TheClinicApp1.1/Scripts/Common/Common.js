

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



var AppoinmentCancellationMessage = '<p>' + "Dear" + '<strong>' + " %PATIENT NAME%," + '</strong>' + '<br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp' + "Your Appoinment with Dr." + '<strong>' + "%DOCTOR NAME%" + '</strong>' + "On" + '<strong>' + " %DATE%" + '</strong>' + "  At " + '<strong>' + " %TIME%" + '</strong>' + "  has been cancelled due to" + '<strong>' + " %REASON% " + '</strong>' + ". Please contact our office to schedule a new appoinment." + '<br /><br />' + "<b><i>" + "This sms will be send to the contacts given below " + "</i></b>" + '</p>';

var CancelReason = "Some inconvenience";