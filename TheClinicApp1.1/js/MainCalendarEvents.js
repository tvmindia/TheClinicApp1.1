
var json;
var eventStartDate, eventEndDate;
var title = "";
var tooltip;
var start, end = "";
var finaltime = "";
var schID = "";
var checkItems = "";
$(document).ready(function () {


    $("#txtstartTime").timepicki();
    $("#txtEndTime").timepicki();
    //$("#myModal").dialog({
    //    autoOpen: false,
    //    closeOnEscape: false,
    //    draggable: false,
    //    height: 300,
    //    width: 500,
    //    hide: { effect: "explode", duration: 1000 },
    //    //modal: true,
    //    resizable: false,
    //    show: { effect: "blind", duration: 800 },
    //    title: "New Event",
    //    dialogClass: 'no-close success-dialog',
    //    open: function (event, ui) {
    //        $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
    //    }
    //}).prev(".ui-dialog-titlebar").css("background", "#336699");

    //  GetJSonDataForCalender();

    setTimeout(function () {
        var initialLangCode = 'en';

        $('#calendar').fullCalendar({
            theme: true,
            header: {
                left: 'prev,next today myCustomButton',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },

            defaultDate: '2016-08-12',
            businessHours: true, // display business hours
            lang: initialLangCode,
            selectable: true,
            selectHelper: true,
            select: function (start, end) {

                //CustomClick();
                // $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
              //  $("#txtAppointmentDate").val(eventStartDate);
                $('#calendar').fullCalendar('unselect');
            },
            displayEventTime: false,
            editable: true,
            viewRender: function (view, element) {

                var add_url = '<a class="tip add-task" title="" href="#"\n\
        data-original-title="Dodaj zadanie" onClick="CustomClick();" style="height:25px;margin-left:100px;margin-top:50px;"><img src="../img/add.png" width="25px;"/></a>';
                $(".fc-more-cell").prepend(add_url);
            },
            eventDrop: function (event, delta, revertFunc) {

                alert(event.title + " was dropped on " + event.start.format());

                if (!confirm("Are you sure about this change?")) {
                    revertFunc();
                }

            },
            eventOverlap: function (stillEvent, movingEvent) {

                return stillEvent.allDay && movingEvent.allDay;
            },
            dayClick: function (date, jsEvent, view) {
                debugger;
                eventStartDate = date.format();
                eventEndDate = date.format();
               // var dayClickFormat = eventStartDate.replace(/[^a-zA-Z 0-9]+/g, '/');
                var date = new Date(eventStartDate);
          
                    // Months use 0 index.
                var dayClickFormat = date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();
                
                AppendList(dayClickFormat);
              
               // $("#txtAppointmentDate").val(dateFormat);
            },
            eventClick: function (calEvent, jsEvent, view) {
                debugger;
                document.getElementById("TimeAvailability").innerHTML = '';
                document.getElementById("listBody").innerHTML = '';
                AppendList(calEvent._start._i.split(' ')[0]);
                // var date = $("#txtAppointmentDate").val();

                var ScheduleID = GetAllNames(calEvent.id);
                schID = ScheduleID;
                $("#hdfScheduleID").val(ScheduleID[0].id);
                var names = GetAllPatientList(ScheduleID[0].id)
                for (index = 0; index < names.length; ++index) {
                    if (names[index].isAvailable == "3") {
                        title = title + '<tr><td><label><strike>' + names[index].title + '</strike></label></td></tr>';
                    }
                    else {
                        title = title + '<tr><td><label>' + names[index].title + '</label></td><td class="center"><img id="imgDelete" src="../Images/Deleteicon1.png" onclick="RemoveFromList(\'' + names[index].appointmentID + '\')"/></td></tr>';
                    }
                    var parentDiv = document.getElementById("listBody");//  $("#AppointmentList");
                    //var newlabel = document.createElement("Label");
                    //newlabel.innerHTML = title;
                    // parentDiv.appendChild(newlabel);
                    parentDiv.innerHTML = title;
                    // title = title + names[index].title + "<br />";
                }

                // title=names[0].end;
                eventStartDate = calEvent.start._i;
              //  $("#txtAppointmentDate").val(eventStartDate.split(' ')[0]);
                //var parentDiv = document.getElementById("listBody");//  $("#AppointmentList");
                //var newlabel = document.createElement("Label");
                //newlabel.innerHTML = title;
                //parentDiv.appendChild(newlabel);
                debugger;
                title = "";
                var docId = $("#hdfDoctorID").val();
                var timeList = GetAllottedTime(docId, eventStartDate, ScheduleID[0].id);
                var html = "";
                for (index = 0; index < timeList.length - 1; index++) {
                    checkItems = timeList.length - 1;
                    var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
                    startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
                    
                        
                            var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
                            endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
                            var StartAndEnd = startTime + "-" + endTime;
                            // var timeList = GetTimeList();
                            html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime' onClick='" + selectOnlyThis(this.id) + "' id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");

                        
                }
                $("#TimeAvailability").append(html);
                timeList = "";
            },
            eventAfterRender: function (event, element, view) {


                $(element).removeClass('MaxHght');
                if (view.name == 'month') {

                    $(element).addClass('MaxHght');
                    var year = event._start.year(), month = event._start.month() + 1, date = event._start.date();
                    var result = year + '-' + (month < 10 ? '0' + month : month) + '-' + (date < 10 ? '0' + date : date);
                    $(element).addClass(result);
                    var ele = $('td[data-date="' + result + '"]'), count = $('.' + result).length;
                    $(ele).find('.viewMore').remove();
                    if (count == 1) {
                        $('.' + result + ':gt(2)').remove();
                        $(ele).find('.fc-day-number').after('<a class="viewMore"> More</a>');

                    }
                }

            },

            eventMouseover: function (calEvent, jsEvent) {

                //if ((calEvent.end != null) && (calEvent.title) != null && (calEvent.id) != null && (calEvent.start) != null) {
                //    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;border-style: solid; border-width: 5px;height:150px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center">' + calEvent.title + '</h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '<p><b>End:</b>' + calEvent.end._i + '</p></div>';
                //}
                //if (calEvent.end == null)
                //{
                //    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;height:110px;border-style: solid; border-width: 5px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center">' + calEvent.title + '</h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '</p></div>';
                //}
                //$("body").append(tooltip);
                //$(this).mouseover(function (e) {
                //    $(this).css('z-index', 10000);
                //    $('.tooltipevent').fadeIn('500');
                //    $('.tooltipevent').fadeTo('10', 1.9);
                //}).mousemove(function (e) {
                //    $('.tooltipevent').css('top', e.pageY + 10);
                //    $('.tooltipevent').css('left', e.pageX + 20);
                //});
            },

            eventMouseout: function (calEvent, jsEvent) {

                $(this).css('z-index', 8);
                $('.tooltipevent').remove();
            },

            eventLimit: true, // allow "more" link when too many events
            eventRender: function (event, element, view) {
                debugger;
                var dateString = moment(event.start).format('YYYY-MM-DD');
                //$('#calendar').find('.fc-day[data-date="' + dateString + '"]').css({ 'background-color': '#b3d4fc!important' });
                $('#calendar').find('.fc-day[data-date="' + dateString + '"]').addClass('ui-state-highlight')
                $('#calendar').find('.fc-day[data-date="' + dateString + '"]').css({ 'background-color': '#deedf7!important', 'border': '2px solid red' });
                document.getElementById("colorBox").style.display = "block";
            },
            events: json,
            viewDisplay: function getDate(date) {

                var lammCurrentDate = new Date();
                var lammMinDate = new Date(lammCurrentDate.getFullYear(), lammCurrentDate.getMonth(), 1, 0, 0, 0, 0);

                if (date.start <= lammMinDate) {
                    $(".fc-button-prev").css("display", "none");
                }
                else {
                    $(".fc-button-prev").css("display", "inline-block");
                }
            }
        });
    }, 3600);
    $('body').on('change', 'input[type="checkbox"]', function () {
        selectOnlyThis(this.id);
    });
    $('.loader').delay(3150).fadeOut('slow');
   
    /*Modal dialog Cancel button click*/
    $('.btnCncl').click(function () {
        $("#txtTitle").val("");
        $("#txtEndDate").val("");
        $("#txtstartTime").val("");
        $("#txtEndTime").val("");
        $("#myModal").dialog("close");
    });

    /*Modal dialog OK button click*/
    $('.btnOkay').click(function () {
        var CalendarSchedule = new Object();

        title = $("#txtTitle").val();
        //   eventEndDate = $("#txtEndDate").val();
        CalendarSchedule.title = title;
        CalendarSchedule.event_start = eventStartDate;
        CalendarSchedule.event_end = eventEndDate;
        CalendarSchedule.startTime = $("#txtstartTime").val();
        CalendarSchedule.endTime = $("#txtEndTime").val();

        if (title != null) {

            var result = AddEvent(CalendarSchedule);
            if (result.status == "1") {
                alert("Sucessfull..!!!")
                $("#txtTitle").val("");
                $("#txtEndDate").val("");
                $("#txtstartTime").val("");
                $("#txtEndTime").val("");
                $("#myModal").dialog("close");
                $('#calendar').fullCalendar('removeEvents');
                $('#calendar').fullCalendar('addEventSource', json);
                $('#calendar').fullCalendar('rerenderEvents');
            }
            else {
                alert("Error..!!!");
            }


        }
    });
});
/*end of document.ready*/

/*Add New Calendar Event */

function GetAllottedTime(docId, eventStartDate, id) {
    debugger;
    var names = GetAllPatientList(id);
    for (var j = 0; j < names.length; ++j)
    {
        if(names[j].isAvailable=="3")
        {
            delete names[j];
            names.splice(j, 1);
        }
    }
    var timeList = GetAllTimeAvailability(docId, eventStartDate);
    for (index = 0; index < timeList.length - 1; index++) {
        checkItems = timeList.length - 1;
        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
        for (var i = 0; i < names.length; ++i) {
            var hours = names[i].allottedTime.split(':')[0];
            var ampm = hours >= 12 ? 'PM' : 'AM';
            var time = names[i].allottedTime + ampm;
            time = time.replace(/\./g, ':');
            if (time == startTime) {
                delete timeList[index];
                timeList.splice(index, 1);
                //names[0].allottedTime.includes("9.00");
            }
        }
    }
    return timeList;
}

function selectOnlyThis(id) {

    if (id != "") {
        // for (var i = 0; i <= checkItems; i++) {
        $('input:checkbox').prop('checked', false);

        // }
        document.getElementById(id).checked = true;
    }
}
function AddEvent(CalendarSchedule) {
    var data = "{'calendarObj':" + JSON.stringify(CalendarSchedule) + "}";
    jsonResult = getJsonData(data, "../JqueryEvents.aspx/AddCalendarEvent");
    var table = {};
    table = JSON.parse(jsonResult.d);
    return table;
}

/*Call webmethod to insert new Event  */
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
/*open modal dialog*/
//function CustomClick()
//    {
//    debugger;
//    $("#myModal").dialog('open');
//    function D(J) { return (J < 10 ? '0' : '') + J; };
//    var dt = new Date();
//    var hours = dt.getHours();
//    var ampm = hours >= 12 ? 'PM' : 'AM';
//    hours = hours % 12;
//    hours = hours ? hours : 12; // the hour '0' should be '12'
//    var starttime = D(dt.getHours()) + " : " + dt.getMinutes()+" : " + ampm;
//    $("#txtstartTime").val(starttime);

//    var mins = hours * 60 + dt.getMinutes() + 30;


//    var minutes = (mins % (24 * 60) / 60 | 0) + ' : ' + D(mins % 60);


//    var endtime =  minutes +" : "+ ampm;
//    $("#txtEndTime").val(endtime);
//    $("#txtEndDate").val(eventEndDate);
//}
//function GetAllPatientAppointmentData()
//{
//    debugger;
//    var Appointments = new Object();
//    var data = "{'AppObj':" + JSON.stringify(Appointments) + "}";
//    var page = "../Appointment/Appointment.aspx/GetAllPatientAppointmentDetailsByClinicID";
//    GetJSonDataForCalender();

//}


//
/*Web method to get all calendar data from database*/
function GetAllNames(id) {

    var ds = {};
    var table = {};
    var Appointments = new Object();
    Appointments.AppointmentID = id;

    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetAppointedPatientDetails");
    table = JSON.parse(ds.d);
    return table;
}
function RemoveFromList(AppointmentID) {

  
    var Appointments = new Object();
    Appointments.AppointmentID = AppointmentID;

    var ds = {};
    var table = {};

    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "../Appointment/Appointment.aspx/CancelAppointment");
    table = JSON.parse(ds.d);

    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetAppointedPatientDetails");
    table = JSON.parse(ds.d);
    return table;

    //GetScheduledTimesByDate();
    //BindScheduledDates();

    //var jsonDrSchedule = {};

    //var Doctor = new Object();
    //Doctor.DoctorID = DoctorID;

    //jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
    //if (jsonDrSchedule != undefined) {

    //$('#calendar').fullCalendar('removeEventSource', json);


    //$('#calendar').fullCalendar('addEventSource', json);
    //$('#calendar').fullCalendar('refetchEvents');
    //}


    // $('#calendar').fullCalendar('refetchEvents');

}
function GetAllTimeAvailability(docID, date) {

    var ds = {};
    var table = {};
    var Doctor = new Object();
    Doctor.DoctorID = docID;
    Doctor.DoctorAvailDate = date;
    var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetDoctorAvailability");
    table = JSON.parse(ds.d);
    return table;
}
function GetTimeList() {
  
    var ds = {};
    var table = {};
    var Appointments = new Object();
    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetTimeIntervals");
    // table = JSON.parse(ds.d);
    return table;
}
function GetAllPatientList(id) {

    var ds = {};
    var table = {};
    var Appointments = new Object();
    Appointments.ScheduleID = id;

    var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetAppointedPatientDetailsByScheduleID");
    table = JSON.parse(ds.d);
    return table;
}
function BindCalendar(docID) {

    var Doctor = new Object();
    Doctor.DoctorID = docID.val();
    //var ds = {};
    //var table = {};
    var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
    GetJSonDataForCalender(data, "Appointment.aspx/GetAllPatientAppointmentDetailsByClinicID");
    //table = JSON.parse(ds.d);
    //return table;
}
function GetJSonDataForCalender(data, page) {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        data: data,
        url: page,
        dataType: "json",
        success: function (data) {
            //json = $.parseJSON(data.d);
            json = JSON.parse(data.d);
            $('div[id*=fullcal]').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                editable: true,
                events: json//data.d
            });
            //$("div[id=loading]").hide();
            //$("div[id=fullcal]").show();

            //  var table = {};
            // table = 
            //  alert(table);
            //  $("div[id=fullcal]").show();
            //  json = $.parseJSON(data.d);
            //json = JSON.parse(json);
            // return json;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error");
        }
    });
}
