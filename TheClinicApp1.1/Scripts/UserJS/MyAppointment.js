$(document).ready(function () {
    debugger;
    var selectedDoc = $(".drop").val();
    debugger;
    if (selectedDoc == "--Select Doctor--") {
        $(".drop").toggleClass('blink');
        //  $("#MyAppointmentHead").css("display", "none");
        $(".AppointmentDateAndCount").html('');
        html = "<label class=noAppointments>Please Select Doctor..!!!</label>";
        $(".AppointmentDateAndCount").append(html);
        $(".name_field").css("display", "none");
        $("#rdoToday").prop("checked", "checked");
        $(".rdo").attr("disabled", true);
    }
    if (selectedDoc == null)
    {
        html = "<label class=noAppointments>No Appointments Scheduled For Today..!!!</label>";
        $(".AppointmentDateAndCount").append(html);
    }
    $(".datepicker").datepicker({
        dateFormat: 'dd-M-yy'
    });
    var $selectAll = $("input:radio[name=dateSelection]");
    $selectAll.on("change", function () {
        debugger;
        if(this.value==4)
        {
            $("#txtStartDate").css("display", "");
        }
        else
        {
            $("#txtStartDate").css("display", "none");
        }
        $("#hdfRadioValue").val(this.value);
        DoctorChange(this.value);
    });
    debugger;

    var usrRole = $("#hdfUserRole").val();
    if (usrRole == "Doctor") {
        $("#PatientApointment").css("display", "none");
        $("#DoctorSchedule").css("display", "none");
        $(".grey_sec").css("display", "none");
    }
    if (usrRole == "Receptionist") {
        $("#MyAppointment").css("display", "none");
    }
    $('.nav_menu').click(function () {

        $(".main_body").toggleClass("active_close");
    });

    
});
//end of document.ready

function BindMyAppointments(Records) {
    var html = "";
    debugger;
    $(".AppointmentDateAndCount").html('');
    $.each(Records, function (index, Records) {
        html = '<div class="accordion" style="border-bottom: 1px solid #e6e2e2;"><div class="accordion-inner" style="border-top:none;"><div class="lead" style="margin-bottom:0px;"><a id="' + formattedDate(Records.AvailableDate) + '" onclick="BindPatients(this);" title="AppointmentDate and No.of Appointments" class="unitLink"</a>' + DateConversion(formattedDate(Records.AvailableDate)) + ' &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Apointments-' + Records.P_Count + '</a></h4></div></div></div>'
        $(".AppointmentDateAndCount").append(html);
    })
    if(Records.length==0)
    {
      var rdoValue = $("#hdfRadioValue").val();
        AppointmentDay = "";
        if (rdoValue == "1") {
            AppointmentDay = "Today";
        }
        if (rdoValue == "2") {
            AppointmentDay = "This Week";
        }
        if (rdoValue == "3") {
            AppointmentDay = "This Month";
        }
        if (rdoValue == "4") {
            AppointmentDay = "The Selected Date";
        }
        html = "<label class=noAppointments>No Appointments Scheduled For <span id=AppointmentDay>" + AppointmentDay + "</span>..!!!</label>";
        $(".AppointmentDateAndCount").append(html);
    }
}
function BindPatients(e)
{
    var jsonResult = {};
    $("#PatientDiv").css("display", "");
    debugger;
    var Appointments = new Object();
    Appointments.AppointmentDate = e.id;
    Appointments.DoctorID = $("#hdfDocID").val();
    jsonResult = GetPatientDetailsForMyAppointment(Appointments);
    if(jsonResult!=undefined)
    {
        BindPatientDetails(jsonResult)
    }
}

function BindPatientDetails(Records)
{
    var jsonResult = {};
    var html = "";
    debugger;
    //var parentDiv = document.getElementById("listBody");
    // document.getElementById("listItems").innerHTML = '';
    $("#listBody").html('');
    $.each(Records, function (index, Records) {
       // title = '<tr><td><label>' + Records.Name + '</label></td><td><label>' + Records.appointmentno + '</label></td><td><label>' + Records.AllottingTime + '</label></td></tr>';
        //parentDiv.innerHTML = title;
        //html = "  <div class=cards><table id=patientTable><tr class=trAppNo><td><label>App.No : </label></td><td><label><span title=Name><i class='fa fa-user' aria-hidden=true></i></span></label></td><td><label><span title=Time><i class='fa fa-clock-o' aria-hidden=true></i></span></label></td><td><label><span title=Mobile><i class='fa fa-mobile' aria-hidden=true></i></span></label></td></tr><tr><td><label>" + Records.appointmentno + "</label></td><td><label>" + Records.Name + "</label></td><td><label>" + Records.AllottingTime + "</label></td><td><label>" + Records.Mobile + "</label></td></tr></table></div>"
        html = "  <div class=cards><table id=patientTable><tr><td ><label class='tdLabel1'>App.No : " + Records.appointmentno + "</label></td><td><label class='tdLabel2'><span title=Name><i class='fa fa-user' aria-hidden=true></i></span>&nbsp;&nbsp;" + Records.Name + "</label></td><td><label class='tdLabel3'><span title=Time><i class='fa fa-clock-o' aria-hidden=true></i></span>&nbsp;&nbsp;" + TimeConversion(Records.AllottingTime) + "</label></td><td><label class='tdLabel4'><span title=Mobile><i class='fa fa-phone' aria-hidden=true></i></span>&nbsp;&nbsp;" + Records.Mobile + "</label></td></tr></table></div>"
        $("#listBody").append(html);
    })
    if(Records.length==0)
    { 
       var rdoValue=$("#hdfRadioValue").val();
        AppointmentDay = "";
        if (rdoValue == "1")
        {
            AppointmentDay = "Today";
        }
        if (rdoValue == "2") {
            AppointmentDay = "This Week";
        }
        if (rdoValue == "3") {
            AppointmentDay = "This Month";
        }
        if (rdoValue == "4") {
            AppointmentDay = "The Selected Date";
        }
        html = "<label class=noAppointments>No Appointments Scheduled For <span id=AppointmentDay>" + AppointmentDay + "</span>..!!!</label>";
        $("#listBody").append(html);
    }
}
function DoctorChange(e) {
    debugger;
    $("#txtStartDate").val("");
    var selectedDoc = $(".drop").val();
    debugger;
    if (selectedDoc == "--Select Doctor--") {
        $(".AppointmentDateAndCount").html('');
        html = "<label class=noAppointments>Please Select Doctor..!!!</label>";
        $(".AppointmentDateAndCount").append(html);
        $(".drop").toggleClass('blink');
        $("#txtStartDate").val("");
        $("#rdoToday").prop("checked", "checked");
        $(".rdo").attr("disabled", true);
    }
    else
    {
        $(".rdo").attr("disabled", false);
        $(".drop").removeClass('blink');
        $("#CalloutDiv").css("display", "none");
    }
    debugger;
    var jsonResult = {};
    $("#PatientDiv").css("display", "none");
    var dateSelectionValue = $("input:radio[name=dateSelection]").val();
    if ($("#hdfRadioValue").val() == "")
    {
        $("#hdfRadioValue").val(dateSelectionValue);
    }
    if ($("#ContentPlaceHolder1_ddlDoctor option:selected").text() != "--Select Doctor--") {
        $(".rdo").attr("disabled", false);
        $(".AppointmentDateAndCount").css("display", "block");
        $("#MyAppointmentHead").css("display", "block");
        $("#listBody").html('');

        var DocID = $("#ContentPlaceHolder1_ddlDoctor option:selected").val();
        $("#hdfDocID").val(DocID);
        var Appointments = new Object();
        Appointments.DoctorID = DocID;
        if (e != null)
        {
            Appointments.AppointmentDayValue = e;
        }
        else
        {
            Appointments.AppointmentDayValue = dateSelectionValue;
        }
      
        jsonResult = GetAppointmentDateAndPatientCount(Appointments);
        if (jsonResult != undefined)
        {
            BindMyAppointments(jsonResult);
        }
        
    }
    else {
        //$(".AppointmentDateAndCount").css("display", "none");
        $("#MyAppointmentHead").css("display", "none");
        $("#hdfDocID").val('');
    }
}


function GetAppointmentDateAndPatientCount(Appointments) {
    var ds = {};
    var table = {};
    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "../Appointment/MyAppointments.aspx/GetAppointmentDateAndCount");
    table = JSON.parse(ds.d);
    return table;
}
function GetPatientDetailsForMyAppointment(Appointments)
{
    var ds = {};
    var table = {};
    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "../Appointment/MyAppointments.aspx/GetAppointmentedPatientDetails");
    table = JSON.parse(ds.d);
    return table;
}
function formattedDate(date) {
    debugger;
    var value = new Date
            (
                 parseInt(date.replace(/(^.*\()|([+-].*$)/g, ''))
            );
    var dat = value.getDate() +
                           "-" +
        value.getMonth() +
                             1 +
                           "-" +
           value.getFullYear();
    return dat;
}
function AppointmentOtherDateChange(e)
{
    debugger;
    var jsonResult = {};
    if ($("#ContentPlaceHolder1_ddlDoctor option:selected").text() != "--Select Doctor--") {
        $(".rdo").attr("disabled", false);
        $(".AppointmentDateAndCount").css("display", "block");
        $("#MyAppointmentHead").css("display", "block");
        $("#listBody").html('');
        var FromDate = e.value;
        var DocID = $("#hdfDocID").val();
        var Appointments = new Object();
        Appointments.DoctorID = DocID;
        Appointments.AppointmentDayValue = e = "4";
        Appointments.appointmentStartDate = FromDate;
        jsonResult = GetAppointmentDateAndPatientCount(Appointments);
        if (jsonResult != undefined) {
            BindMyAppointments(jsonResult);
        }
    }
    else {
        //$(".AppointmentDateAndCount").css("display", "none");
        $("#MyAppointmentHead").css("display", "none");
        $("#hdfDocID").val('');
    }
}

function DateConversion(date)
{
    debugger;
    var m_names = new Array("Jan", "Feb", "Mar",
"Apr", "May", "Jun", "Jul", "Aug", "Sep",
"Oct", "Nov", "Dec");

    var curr_date = date.split("-")[0];
    var curr_month = date.split("-")[1];
    var curr_year = date.split("-")[2];
    return (curr_date + "-" + m_names[parseInt(curr_month)]
    + "-" + curr_year);
}
function TimeConversion(Time)
{
    debugger;
    var hours = Time.split(":")[0];
    var minute = Time.split(":")[1];
    hours = parseInt(hours);
    var ampm = hours >= 12 ? 'PM' : 'AM';
    var hh = hours;
    var ampm = hours >= 12 ? 'PM' : 'AM';
    if (hours >= 13) {
        hours = hh - 12;
        ampm = "PM";
    }
    if (hours == 00) {
        hours = 12;
        ampm = "AM";
    }
    Time = hours + '.' + minute + ampm;

    if (Time.split('.')[0].length == 1) {
        var d = Time.split('.')[0];
        d = "0" + d;
        Time = d + "." + minute + ampm;
    }
    if (minute.length == 1) {
        var d = minute;
        d = "0" + d;
        Time = hours + "." + d + ampm;
    }
    if ((Time.split('.')[0].length == 1) && (minute.length == 1)) {
        var h = Time.split('.')[0];
        h = "0" + h;
        var m = minute;
        m = "0" + m;
        Time = h + "." + m + ampm;
    }

    return Time;
}