
var json;
var eventStartDate, eventEndDate;
var title = "";
var tooltip;
var start, end = "";
var finaltime = "";
var schID = "";
var checkItems = "";
var scheduleId = "";
$(document).ready(function () {
    $('.alert_close').click(function () {
        $(this).parent(".alert").hide();
    });
    setTimeout(function () {
        var initialLangCode = 'en';

        $('#calendar').fullCalendar({
            theme: true,
            header: {
                left: 'prev,next today myCustomButton',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            timezone: 'local',//--- otherwise date shows wrong by one day
            businessHours: true, // display business hours
            lang: initialLangCode,
            selectable: true,
            selectHelper: true,
            select: function (start, end) {
                var dateString = moment(start).format('YYYY-MM-DD');
                if ($('.fc-day[data-date="' + dateString + '"]').find("#imgSelect").length != 0)
                {
                    clearTextBoxes();
                    clearDropDown();
                    showDropDown();
                    var titles = "";
                    document.getElementById("TimeAvailability").innerHTML = '';
                    document.getElementById("listBody").innerHTML = '';
                    title = '';
                    $("#hdEventDate").val(dateString);
                    var ds = {};
                    var Doctor = new Object();
                    Doctor.DoctorID = $("#hdfDoctorID").val();
                    Doctor.SearchDate = dateString;
                    ds = GetAllDoctorScheduleDetailsByDate(Doctor);
                    if (ds != undefined) {

                        BindTimes(ds, dateString);
                    }
                    scheduleId = $("#hdfScheduleID").val();
                    AppendList(dateString);
                    var names = GetAllPatientList(scheduleId)
                    for (index = 0; index < names.length; ++index) {
                        var hours = names[index].allottedTime.split('.')[0];
                        var minute = names[index].allottedTime.split('.')[1];
                        if (minute == undefined || minute == "") {
                            minute = names[index].allottedTime.split(':')[1];
                            minute = minute.replace(':', '');
                        }
                        minute = minute.replace('PM', '');
                        minute = minute.replace('AM', '');
                        hours = names[index].allottedTime[0] + names[index].allottedTime[1];
                        hours = hours.replace(':', '');
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
                        var time = hours + '.' + minute + ampm;

                        if (time.split('.')[0].length == 1) {
                            var d = time.split('.')[0];
                            d = "0" + d;
                            time = d + "." + minute + ampm;
                        }
                        if (minute.length == 1) {
                            var d = minute;
                            d = "0" + d;
                            time = hours + "." + d + ampm;
                        }
                        if ((time.split('.')[0].length == 1) && (minute.length == 1)) {
                            var h = time.split('.')[0];
                            h = "0" + h;
                            var m = minute;
                            m = "0" + m;
                            time = h + "." + m + ampm;
                        }
                        var s = parseFloat(names[0].allottedTime.replace(':', '.'));
                        $("#hdfStartTime").val(s);
                        if (names[1] != undefined) {
                            var e = parseFloat(names[1].allottedTime.replace(':', '.'));
                            $("#hdfEndTime").val(e);
                        }
                        if (names[index].isAvailable == "3") {
                            title = title + '<tr><td><label><strike>' + names[index].title + '</strike></label></td></tr>';
                        }
                        else {
                            title = title + '<tr><td><label>' + names[index].title + '</td><td>' + time + '</label></td><td class="center"><img id="imgDelete" src="../Images/Deleteicon1.png" onclick="RemoveFromList(\'' + names[index].appointmentID + '\')"/></td></tr>';
                            $("#hdfLastAppointedTime").val(time);
                        }
                        var parentDiv = document.getElementById("listBody");
                        parentDiv.innerHTML = title;
                    }
                    eventStartDate = dateString;
                    document.getElementById("availableSlot").style.display = "block";
                    document.getElementById("TimeAvailability").style.display = "block";
                    title = "";
                    var docId = $("#hdfDoctorID").val();
                    var timeList = GetAllottedTime(docId, eventStartDate, scheduleId);
                    var timeCount = timeList.length - 1;
                    if (timeCount == 0) {
                        document.getElementById("TimeAvailability").innerHTML = '';
                        document.getElementById("TimeAvailability").style.display = 'none';
                        document.getElementById("NoSlots").style.display = 'block';
                    }
                    else {
                        document.getElementById("NoSlots").style.display = 'none';
                    }
                    var html = "";
                    for (index = 0; index < timeList.length - 1; index++) {
                        checkItems = timeList.length - 1;
                        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
                        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
                        var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
                        endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
                        var StartAndEnd = startTime + "-" + endTime;                   
                        html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime' id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");
                    }
                   
                    $("#TimeAvailability").append(html);
                    timeList = "";
                }
              
                $('#calendar').fullCalendar('unselect');
            },
            displayEventTime: false,
            editable: false,
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
                clearTextBoxes();
                document.getElementById("TimeAvailability").innerHTML = '';
                document.getElementById("listBody").innerHTML = '';
                document.getElementById("availableSlot").style.display = "none";
                document.getElementById("TimeAvailability").style.display = "none";
                document.getElementById("timeSlot").style.display = "none";
                hideDropDown();
                eventStartDate = date.format();
                eventEndDate = date.format();
                var date = new Date(eventStartDate);
                    // Months use 0 index.
                var dayClickFormat = date.getMonth() + 1 + '/' + date.getDate() + '/' + date.getFullYear();   
                AppendListForDayClick(dayClickFormat);
            },
            eventClick: function (calEvent, jsEvent, view) {
                clearTextBoxes();
                clearDropDown();
                showDropDown();
                var titles = "";
                document.getElementById("TimeAvailability").innerHTML = '';
                document.getElementById("listBody").innerHTML = '';
                title = '';
                $("#hdEventDate").val(calEvent.start._i);
                if (calEvent.title != "")
                {
                    var ds = {};
                    var Doctor = new Object();
                    Doctor.DoctorID = $("#hdfDoctorID").val();
                    Doctor.SearchDate = calEvent.start._i;
                    ds = GetAllDoctorScheduleDetailsByDate(Doctor);
                    if (ds != undefined) {

                        BindTimes(ds, calEvent.start._i);
                    }
                }
                scheduleId = $("#hdfScheduleID").val();
                AppendList(calEvent._start._i.split(' ')[0]);
               var names = GetAllPatientList(scheduleId)
               for (index = 0; index < names.length; ++index) {
                    var hours = names[index].allottedTime.split('.')[0];
                    var minute = names[index].allottedTime.split('.')[1];
                    if (minute == undefined||minute=="")
                    {
                        minute = names[index].allottedTime.split(':')[1];
                        minute = minute.replace(':', '');
                    }
                    minute = minute.replace('PM', '');
                    minute = minute.replace('AM', '');
                    hours = names[index].allottedTime[0] + names[index].allottedTime[1];
                    hours = hours.replace(':', '');
                    var ampm = hours >= 12 ? 'PM' : 'AM';
                    var hh = hours;
                    var ampm = hours >= 12 ? 'PM' : 'AM';
                    if (hours >= 13) {
                        hours = hh - 12;
                        ampm = "PM";
                    }
                    if (hours == 00)
                    {
                        hours = 12;
                        ampm = "AM";
                    }
                    var time = hours + '.' + minute + ampm;

                    if (time.split('.')[0].length == 1) {
                        var d = time.split('.')[0];
                        d = "0" + d;
                        time = d + "." + minute + ampm;
                    }
                    if (minute.length == 1)
                    {
                        var d = minute;
                        d = "0" + d;
                        time = hours + "." + d + ampm;
                    }
                    if ((time.split('.')[0].length == 1) && (minute.length == 1)) {
                        var h = time.split('.')[0];
                        h = "0" + h;
                        var m = minute;
                        m = "0" + m;
                        time = h + "." + m + ampm;
                    }
                    var s = parseFloat(names[0].allottedTime.replace(':', '.'));
                    $("#hdfStartTime").val(s);
                    if (names[1] != undefined) {
                        var e = parseFloat(names[1].allottedTime.replace(':', '.'));
                        $("#hdfEndTime").val(e);
                    }
                    if (names[index].isAvailable == "3") {
                        title = title + '<tr><td><label><strike>' + names[index].title + '</strike></label></td></tr>';
                    }
                    else {
                        title = title + '<tr><td><label>' + names[index].title + '</td><td>' + time + '</label></td><td class="center"><img id="imgDelete" src="../Images/Deleteicon1.png" onclick="RemoveFromList(\'' + names[index].appointmentID + '\')"/></td></tr>';
                        $("#hdfLastAppointedTime").val(time);
                    }
                    var parentDiv = document.getElementById("listBody");
                    parentDiv.innerHTML = title;
                }
                eventStartDate = calEvent.start._i;
                document.getElementById("availableSlot").style.display = "block";
                document.getElementById("TimeAvailability").style.display = "block";
                title = "";
                var docId = $("#hdfDoctorID").val();
                debugger;
                var timeList = GetAllottedTime(docId, eventStartDate, scheduleId);
                $("#hdfTimeListLength").val(timeList.length);
                var timeCount = timeList.length - 1;
                if (timeCount == 0)
                {
                    document.getElementById("TimeAvailability").innerHTML = '';
                    document.getElementById("TimeAvailability").style.display = 'none';
                    document.getElementById("NoSlots").style.display = 'block';
                }
                else
                {
                    document.getElementById("NoSlots").style.display = 'none';
                }
                var html = "";
                for (index = 0; index < timeList.length - 1; index++) {
                    checkItems = timeList.length - 1;
                    var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
                    startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
                    var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
                    endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
                    var StartAndEnd = startTime + "-" + endTime;
                    html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime' onClick='" + selectOnlyThis(this.id) + "' id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");
                }
                
                $("#TimeAvailability").append(html);
                timeList = "";
            },
            dayRender: function (date, element) {
                debugger;
                document.getElementById("colorBox").style.display = "block";
                //var docId = $("#hdfDoctorID").val();
                //if((docId!="")&&(docId!=undefined))
                //{
                //    alert();
                //    if ($('.fc-day[data-date="' + dateString + '"]').find("#imgSelect").length == 0) {
                //        $('#calendar').find('.fc-day[data-date="' + dateString + '"]').append("<img id='imgSelect' src='../Images/add.png' title='Add Appointment' onclick='CustomClick();' style='float: left;	background-repeat: no-repeat;cursor:pointer;height:10px!important' />")
                //    }
                //}
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
                debugger;
                var dateString = moment(calEvent.start).format('YYYY-MM-DD');
                //tooltip = '<div class="tooltiptopicevent" style="width:auto;height:auto;background:#feb811;position:absolute;z-index:10001;padding:10px 10px 10px 10px ;  line-height: 200%;">' + 'title: ' + ': ' + calEvent.title + '</br>' + 'start: ' + ': ' + calEvent.start + '</div>';


                //$('.fc-day[data-date="' + dateString + '"]').find("#imgSelect").append(tooltip);
                //var dateString = moment(calEvent.start._i).format('YYYY-MM-DD');
                //if ($('.fc-day[data-date="' + dateString + '"]').find("#imgSelect").length == 1) {
                //    $(".fc-day-number").append("<img id='imgSelect' src='../Images/add.png' title='Add Schedule' background-repeat: no-repeat !important; style='float: left;cursor:pointer;height:10px!important' />")
                //}
            },

            eventMouseout: function (calEvent, jsEvent) {
                $(this).css('z-index', 8);
                $('.tooltiptopicevent').remove();
            },

            eventLimit: true, // allow "more" link when too many events
            eventRender: function (event, element, view) {              
                if ($('#hdnIsDrChanged').val() == "Yes") { //Checking doctor dropdown value changed , then allevents array is cleared, 
                    allEvents = [];
                    $('#hdnIsDrChanged').val(""); // -- this assignment is to ensure that allevents array is cleared onlt once after changing doctor dropdown

                }

                var dateString = moment(event.start).format('YYYY-MM-DD');
                allEvents.push(dateString); //-- Event dates are pushed to array

                $('#hdnAllEvents').val(JSON.stringify(allEvents));
                //$('#calendar').find('.fc-day[data-date="' + dateString + '"]').css({ 'background-color': '#b3d4fc!important' });
                $('#calendar').find('.fc-day[data-date="' + dateString + '"]').addClass('ui-state-highlight')
                $('#calendar').find('.fc-day[data-date="' + dateString + '"]').css({ 'background-color': '#deedf7!important' });
               

                if ($('.fc-day[data-date="' + dateString + '"]').find("#imgSelect").length == 0) {
                    $('#calendar').find('.fc-day[data-date="' + dateString + '"]').append("<img id='imgSelect' src='../Images/add.png' title='Add Appointment' onclick='CustomClick();' style='float: left;	background-repeat: no-repeat !important;cursor:pointer !important;height:10px!important' />")
                }
              
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
  
});
/*end of document.ready*/

function ConvertTimeFormatFrom24hrTo12hr(Time) {
    var TimeIn24hrFormat = Time;
    var hourEnd = TimeIn24hrFormat.indexOf(":");
    var H = +TimeIn24hrFormat.substr(0, hourEnd);
    var h = H % 12 || 12;
    var ampm = H < 12 ? "AM" : "PM";
    TimeIn12hrFormat = moment(Time, ["h:mm A"]).format("hh:mm") + ampm;
    return TimeIn12hrFormat;
}

function fillPatientDetails() {
    clearTextBoxes();
    showDropDown();
    var titles = "";
    document.getElementById("TimeAvailability").innerHTML = '';
    document.getElementById("listBody").innerHTML = '';
    title = '';
    scheduleId = $("#hdfScheduleID").val();
    AppendList(eventStartDate.split(' ')[0]);
    var names = GetAllPatientList(scheduleId)
    for (index = 0; index < names.length; ++index) {
        var hours = names[index].allottedTime.split('.')[0];
        var minute = names[index].allottedTime.split('.')[1];
        if (minute == undefined || minute == "") {
            minute = names[index].allottedTime.split(':')[1];
          
        }
        debugger;
        minute = minute.replace('PM', '');
        minute = minute.replace('AM', '');
        hours = names[index].allottedTime[0] + names[index].allottedTime[1];
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
        var time = hours + '.' + minute + ampm;
        if (time.split('.')[0].length == 1) {
            var d = time.split('.')[0];
            d = "0" + d;
            time = d + "." + minute + ampm;
        }
        if (minute.length == 1) {
            var d = minute;
            d = "0" + d;
            time = hours + "." + d + ampm;
        }
        if ((time.split('.')[0].length == 1) && (minute.length == 1))
        {
            var h = time.split('.')[0];
            h = "0" + h;
            var m = minute;
            m = "0" + m;
            time = h + "." + m + ampm;
        }
        var s = parseFloat(names[0].allottedTime.replace(':', '.'));
        $("#hdfStartTime").val(s);
        if (names[1] != undefined) {
            var e = parseFloat(names[1].allottedTime.replace(':', '.'));
            $("#hdfEndTime").val(e);
        }
       
        
        if (names[index].isAvailable == "3") {
            title = title + '<tr><td><label><strike>' + names[index].title + '</strike></label></td></tr>';
        }
        else {
            title = title + '<tr><td><label>' + names[index].title + '</td><td>' + time + '</label></td><td class="center"><img id="imgDelete" src="../Images/Deleteicon1.png" onclick="RemoveFromList(\'' + names[index].appointmentID + '\')"/></td></tr>';
            $("#hdfLastAppointedTime").val(time);
        }
        var parentDiv = document.getElementById("listBody");
        parentDiv.innerHTML = title;
    }
   
    document.getElementById("availableSlot").style.display = "block";
    document.getElementById("TimeAvailability").style.display = "block";
    title = "";
    var docId = $("#hdfDoctorID").val();
    var timeList = GetAllottedTime(docId, eventStartDate, scheduleId);
    var timeCount = timeList.length - 1;
    if (timeCount == 0) {
        document.getElementById("TimeAvailability").innerHTML = '';
        document.getElementById("TimeAvailability").style.display = 'none';
        document.getElementById("NoSlots").style.display = 'block';
    }
    else {
        document.getElementById("NoSlots").style.display = 'none';
    }
    var html = "";
    for (index = 0; index < timeList.length - 1; index++) {
        checkItems = timeList.length - 1;
        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];


        var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
        endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
        var StartAndEnd = startTime + "-" + endTime;
        html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime'  id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");
    }
    
    $("#TimeAvailability").append(html);
    timeList = "";
}
/*Add New Calendar Event */
function RereshTimeCheckBox()
{
    scheduleId = $("#hdfScheduleID").val();
    title = "";
    var docId = $("#hdfDoctorID").val();
    var timeList = GetAllottedTime(docId, eventStartDate, scheduleId);
    var html = "";
    for (index = 0; index < timeList.length - 1; index++) {
        checkItems = timeList.length - 1;
        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];


        var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
        endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
        var StartAndEnd = startTime + "-" + endTime;
        html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime'  id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");
    }
    document.getElementById("availableSlot").style.display = "block";
    document.getElementById("TimeAvailability").style.display = "block";
    $("#TimeAvailability").append(html);
    timeList = "";
}
function BindTimes(Records,eventDate) {
    AllotedEndTimes = [];
    AllotedStartTimes = [];
    ScheduleNo = 0;

    if (Records.length == 0) {
        AvailableCount = 0;
    }

    $.each(Records, function (index, Records) {
        var ScheduleID = Records.ID;

        if (Records.Starttime != null && Records.Endtime != null) {

            strttime = ConvertTimeFormatFrom24hrTo12hr(Records.Starttime);
            endtime = ConvertTimeFormatFrom24hrTo12hr(Records.Endtime);

            if (Records.IsAvailable == "True") {
                var html = '<tr ScheduleID="' + Records.ID + '" ><td>' + strttime + "-" + endtime + '</td><td class="center"><img id="imgDelete" align="right" height="20" style="margin-right:10px" src="../images/Deleteicon1.png" title="Cancel" onclick="RemoveTime(\'' + ScheduleID + '\')"/><img id="imgUpdate"  height="18" align="right" src="../images/Editicon1.png" title="Change" onclick="BindScheduleOnEditClick(\'' + ScheduleID + '\')" /></td></tr>';
                AllotedEndTimes.push(Records.Endtime);
                AllotedStartTimes.push(Records.ID + "=" + strttime + "-" + endtime);
                ScheduleNo = parseInt(ScheduleNo + 1);
            }
            else {
                var html = '<tr ScheduleID="' + Records.ID + '" ><td><strike>' + strttime + "-" + endtime + '</td></strike><td></td></tr>';
            }
        }

    });
    BindSlotDropDown(AllotedStartTimes, eventDate);
    if (Records.length == 0) {
    }

}
function refreshTime(){
    document.getElementById("TimeAvailability").innerHTML = '';
    var docId = $("#hdfDoctorID").val();
    var scheduleID = $("#hdfScheduleID").val();
    var timeList = GetAllottedTime(docId, eventStartDate, scheduleID);
    var html = "";
    for (index = 0; index < timeList.length - 1; index++) {
        checkItems = timeList.length - 1;
        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
        var endTime = timeList[index + 1].split(' ')[1] + " " + timeList[index + 1].split(' ')[2];
        endTime = endTime.split(':')[0] + ":" + endTime.split(':')[1] + endTime.split(' ')[1];
        var StartAndEnd = startTime + "-" + endTime;
        html = html + ("<table class='tblDates'><tr><td><input type='checkbox' class='chkTime' onClick='" + selectOnlyThis(this.id) + "' id='chk_" + index + "' value='" + StartAndEnd + "'  /></td><td><label >" + StartAndEnd + "</label></td></tr><table><br/>");
    }
    $("#TimeAvailability").append(html);
    timeList = "";
}

function refreshList(){
    document.getElementById("listBody").innerHTML = '';
    title = '';
    var scheduleID = $("#hdfScheduleID").val();   
    var names = GetAllPatientList(scheduleID);
    for (index = 0; index < names.length; ++index) {
        var hours = names[index].allottedTime.split('.')[0];
        var minute = names[index].allottedTime.split('.')[1];
        if (minute == undefined || minute == "") {
            minute = names[index].allottedTime.split(':')[1];
        }
        hours = names[index].allottedTime[0] + names[index].allottedTime[1];
        var hh = hours;
        var ampm = hours >= 12 ? 'PM' : 'AM';
        if (hours >= 13) {
            hours = hh - 12;
            ampm = "PM";
        }
        var time = hours + '.' + minute + ampm;
        if (names[index].isAvailable == "3") {
            title = title + '<tr><td><label><strike>' + names[index].title + '</strike></label></td></tr>';
        }
        else {
            title = title + '<tr><td><label>' + names[index].title + '</td><td>' + time + '</label></td><td class="center"><img id="imgDelete" src="../Images/Deleteicon1.png" onclick="RemoveFromList(\'' + names[index].appointmentID + '\')"/></td></tr>';
        }
        var parentDiv = document.getElementById("listBody");
        parentDiv.innerHTML = title;
    }
    
  
    $('#calendar').fullCalendar('removeEventSource', json);
    var calID = $("#hdfDoctorID").val();
    
    json = RebindCalendar(calID);

    $('#calendar').fullCalendar('addEventSource', json);

    $('#calendar').fullCalendar('refetchEvents');
}
function GetAllottedTime(docId, eventStartDate, id) {
    debugger;
    var names = GetAllPatientList(id);
    for (var j = 0; j < names.length; j++)
    {
        if(names[j].isAvailable=="3")
        {
            names[j] = '';
        }
    }
    names = names.filter(Boolean);
    var timeList = GetAllTimeAvailability(docId, eventStartDate, id);
    for (index = 0; index < timeList.length - 1; index++) {
        checkItems = timeList.length - 1;
        var startTime = timeList[index].split(' ')[1] + " " + timeList[index].split(' ')[2];
        startTime = startTime.split(':')[0] + ":" + startTime.split(':')[1] + startTime.split(' ')[1];
        for (var i = 0; i < names.length; ++i) {
            var hours = names[i].allottedTime.split(':')[0];
            var ampm = hours >= 12 ? 'PM' : 'AM';
            var time = names[i].allottedTime + ampm;
            time = time.replace(/\./g, ':');
            var hh = hours;
            if (time.split(':')[0] == 00) {
                var d = time.split(':')[0];
                d = "12";
                time = d + ":" + time.split(':')[1];
            }
            if (hours >= 13) {
                hours = hh - 12;
                ampm = "PM";
                time = hours + ":" + time.split(':')[1];
            }
            
            debugger;
            if ((time.split(':')[0].length == 2) && (time.split(':')[0][0])=="0") {
                time = time.replace(time.split(':')[0][0], '');
            }

            if (time == startTime) {
                timeList[index]='';
            }
        }
    }
    timeList = timeList.filter(Boolean);
    return timeList;
}

function selectOnlyThis(id) {
    if (id != "") {
        $('input:checkbox').prop('checked', false);
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
    var req = $.ajax({
        type: "post",
        url: page,
        data: data,
        delay: 3,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (data) {
        jsonResult = data;
    });
    return jsonResult;
}

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
function clearTextBoxes()
{
    $("#txtPatientName").val("");
    $("#txtPatientMobile").val("");
    $("#txtPatientPlace").val("");
    $("#txtSearch").val("");
    $("#txtAppointmentDate").val("");
    $("#hdfPatientID").val("");
    document.getElementById("txtPatientName").disabled = '';
    document.getElementById("txtPatientPlace").disabled = '';
}

function RemoveFromList(AppointmentID) {
    var Appointments = new Object();
    Appointments.AppointmentID = AppointmentID;
    var result = confirm("Are you sure?");
    if (result) {
        var ds = {};
        var table = {};

        var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
        ds = getJsonData(data, "../Appointment/Appointment.aspx/CancelAppointment");
        table = JSON.parse(ds.d);
        if (table.status == "1") {
            var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
            ds = getJsonData(data, "Appointment.aspx/GetAppointedPatientDetails");
            table = JSON.parse(ds.d);
            refreshList();
            fillPatientDetails();
            var lblclass = Alertclasses.sucess;
            var lblmsg = msg.AppointmentCancelSuccessFull;
            var lblcaptn = Caption.SuccessMsgCaption;

            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
            return table;
        }
        else {
            var lblclass = Alertclasses.sucess;
            var lblmsg = msg.AppointmentCancelSuccessFull;
            var lblcaptn = Caption.FailureMsgCaption;

            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
        }
    }
}

function GetAllScheduleDetails(scheduleID){
    var ds = {};
    var table = {};
    var Doctor = new Object();
    Doctor.DocScheduleID = scheduleID;
    var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetAllScheduleDetails");
    table = JSON.parse(ds.d);
    return table;
}

function GetAllDoctorScheduleDetailsByDate(Doctor) {
    var ds = {};
    var table = {};
    var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetAllDoctorScheduleDetailsByDate");
    table = JSON.parse(ds.d);
    return table;
}

function GetAllTimeAvailability(docID, date,schId) {
    var ds = {};
    var table = {};
    var Doctor = new Object();
    Doctor.DoctorID = docID;
    Doctor.DoctorAvailDate = date;
    Doctor.DocScheduleID = schId;
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
    Doctor.DoctorID = docID;
    var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
    GetJSonDataForCalender(data, "Appointment.aspx/GetAllPatientAppointmentDetailsByClinicID");
}

function BindFullCalendarEvents(docID){
    var ds = {};
    var table = {};
    var Doctor = new Object();
    Doctor.DoctorID = docID;
    
    var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "Appointment.aspx/GetAllPatientAppointmentDetailsByClinicID");
    table = JSON.parse(ds.d);
    return table;
}

function RebindCalendar(docID){
    var ds = {};
    var table = {};
        var Doctor = new Object();
        Doctor.DoctorID = docID;
       
        var data = "{'docObj':" + JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "Appointment.aspx/GetAllPatientAppointmentDetailsByClinicID");
        table = JSON.parse(ds.d);
        return table;    
}

function GetJSonDataForCalender(data, page) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        data: data,
        url: page,
        dataType: "json",
        success: function (data) {
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
           
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("error");
        }

    });
}
