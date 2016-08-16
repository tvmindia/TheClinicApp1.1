


        var json;
var title, eventStartDate, eventEndDate;
var tooltip;
var DoctorID;

$(document).ready(function () {
   
  //  GetScheduleByDrID();


    // ---- Get date in yyyy mm dd format , to set default date----- //

    Date.prototype.yyyymmdd = function () {
        var mm = this.getMonth() + 1; // getMonth() is zero-based
        var dd = this.getDate();

        return [this.getFullYear(), !mm[1] && '0', mm, !dd[1] && '0', dd].join(''); // padding
    };

    var today = new Date();
    today.yyyymmdd();

//----------------------------------------------------------------------//

    //$("#txtstartTime").timepicki();
    //          $("#txtEndTime").timepicki();
            
    //GetAllCalendarData();

    setTimeout(function () {
        var initialLangCode = 'en';

        $('#calendar').fullCalendar({
            theme: true,
            header: {
                left: 'prev,next today myCustomButton',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },

            defaultDate: today,
            businessHours: true, // display business hours
            lang: initialLangCode,
            selectable: true,
            selectHelper: true,
           
            select: function (start, end) {
             
                //CustomClick();
                // $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true
              
                $('#calendar').fullCalendar('unselect');

                GetScheduledTimesByDate();
            },



            editable: true,
           
            eventRender: function (event, element, view) { 
                // like that
                //var dateString = moment(event.start).format('YYYY-MM-DD');
                //$('#calendar').find('.fc-day-number[data-date="' + dateString + '"]').css('background-color', '#FAA732');
                //$('#calendar').find('.fc-day-number[data-date="' + dateString + '"]').addClass('fc-today ui-state-highlight');
               // fc-day-number fc-thu fc-today ui-state-highlight
              //  fc-day-number fc-tue fc-future
                 
            
            },

            dayClick: function (date, jsEvent, view) {

                eventStartDate = date.format();
                eventEndDate = date.format();

            },
            eventAfterRender: function (event, element) {
              
             //  $('.fc-content').remove();


                $(element).tooltip({
                   
                    container: "body"
                });
            },

            eventMouseover: function (calEvent, jsEvent) {

                if ((calEvent.end != null) && (calEvent.title) != null && (calEvent.id) != null && (calEvent.start) != null) {
                    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;border-style: solid; border-width: 5px;height:150px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center"></h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '<p><b>End:</b>' + calEvent.end._i + '</p></div>';
                }
                if (calEvent.end == null) {
                    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;height:110px;border-style: solid; border-width: 5px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center"></h3><p><b>Start:</b>' + calEvent.start._i + '</p></div>';
                }
                $("body").append(tooltip);
                $(this).mouseover(function (e) {
                    $(this).css('z-index', 10000);
                    $('.tooltipevent').fadeIn('500');
                    $('.tooltipevent').fadeTo('10', 1.9);
                }).mousemove(function (e) {
                    $('.tooltipevent').css('top', e.pageY + 10);
                    $('.tooltipevent').css('left', e.pageX + 20);
                });
            },

            eventMouseout: function (calEvent, jsEvent) {
                $(this).css('z-index', 8);
                $('.tooltipevent').remove();
            },

            eventLimit: true, // allow "more" link when too many events

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





function GetScheduledTimesByDate()
{
    var jsonDeatilsByDate = {};

    var Doctor = new Object();


    if (DoctorID != null && DoctorID != "") {

        $("#txtAppointmentDate").val(eventStartDate);

        Doctor.DoctorID = DoctorID;
        Doctor.SearchDate = eventStartDate;

        jsonDeatilsByDate = GetAllDoctorScheduleDetailsByDate(Doctor);

        if (jsonDeatilsByDate != undefined)
        {
         BindTimes(jsonDeatilsByDate);
        }
    }

    else {
        alert("Please Select a doctor");
    }

}




function GetScheduleByDrID(drID) {
  

    DoctorID = drID;

    var jsonDrSchedule = {};

    var Doctor = new Object();
    Doctor.DoctorID = drID;

    jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
    if (jsonDrSchedule != undefined) {
        json = jsonDrSchedule;
        BindScheduledDates(jsonDrSchedule);
    }
   
}

function BindScheduledDates(Records)
{
  

    //var Doctor = new Object();
    //Doctor.DoctorID = drID;

    //var ds = {};
    //var table = {};
    //var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
    //ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetDoctorScheduleDetailsByDoctorID");
    //Records = JSON.parse(ds.d);


    $("#tblDates tr").remove();

    $.each(Records, function (index, Records) {

        var html = '<tr><td>' + Records.start + '</td></tr>';
        $("#tblDates").append(html);

    });

    
   
    if (Records.length == 0) 

    {
        var html = '<tr><td>' + "No Scheduled Date yet !" + '</td></tr>';
        $("#tblDates").append(html);
    }

}



function GetAllDoctorScheduleDetailsByDate(Doctor)
{
    var ds = {};
    var table = {};
    var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetAllDoctorScheduleDetailsByDate");
    table = JSON.parse(ds.d);
    return table;
}

function BindTimes(Records) {

    $("#tblTimes tr").remove();
  
    $.each(Records, function (index, Records) {

        var ScheduleID = Records.ID;


        if (Records.Starttime != null && Records.Endtime != null) {

       
        var html = '<tr ScheduleID="' + Records.ID + '" ><td>' + Records.Starttime + "-" + Records.Endtime + '</td><td class="center"><img id="imgDelete" src="../Images/delete-cross.png" onclick="RemoveTime(\'' + ScheduleID + '\')"/></td></tr>';
        $("#tblTimes").append(html);
        }

    });

    if (Records.length == 0)
    {
        var html = '<tr><td>' + "No Scheduled time yet !" + '</td></tr>';
        $("#tblTimes").append(html);
    }
  


}


function RemoveTime(ScheduleID) {
    

        var Doctor = new Object();
        Doctor.DocScheduleID = ScheduleID;

        var ds = {};
        var table = {};

    var data = "{'DocObj':" +JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/CancelDoctorSchedule");
    table = JSON.parse(ds.d);

    if (table.status == 0)
    {
        alert(" Sorry, Already scheduled an appointment!")
    }
    else {
        GetScheduledTimesByDate();
    }
}


    function GetDoctorScheduleDetailsByDoctorID(Doctor) {
        var ds = { };
        var table = { };
        var data = "{'DocObj':" +JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetDoctorScheduleDetailsByDoctorID");
        table = JSON.parse(ds.d);
        return table;
}


    /*Add New Calendar Event */
    function AddEvent(CalendarSchedule) {
      
        var data = "{'calendarObj':" +JSON.stringify(CalendarSchedule) + "}";

        jsonResult = getJsonData(data, "../JqueryEvents.aspx/AddCalendarEvent");
        var table = { };
        table = JSON.parse(jsonResult.d);
        return table;
}

    /*Call webmethod to insert new Event  */
    function getJsonData(data, page) {
        var jsonResult = {
    };
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
    function CustomClick() {
      
        $("#myModal").dialog('open');
        function D(J) { return (J < 10 ? '0': '') +J;
        };
        var dt = new Date();
        var hours = dt.getHours();
        var ampm = hours >= 12 ? 'PM': 'AM';
        hours = hours % 12;
        hours = hours ? hours: 12; // the hour '0' should be '12'
        var starttime = D(dt.getHours()) + " : " +dt.getMinutes() + " : " +ampm;
        $("#txtstartTime").val(starttime);

        var mins = hours * 60 +dt.getMinutes() +30;


        var minutes = (mins % (24 * 60) / 60 | 0) + ' : ' + D(mins % 60);


        var endtime = minutes + " : " +ampm;
        $("#txtEndTime").val(endtime);
        $("#txtEndDate").val(eventEndDate);
}

    /*Web method to get all calendar data from database*/
    function GetAllCalendarData(data, page) {

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

    function AddSchedule() {

        debugger;

        var JsonNewSchedule = {};

        var Doctor = new Object();
        Doctor.DoctorID = DoctorID;
        Doctor.DoctorAvailDate = document.getElementById('txtAppointmentDate').value;
        Doctor.PatientLimit = parseInt(document.getElementById('txtMaxAppoinments').value);
        Doctor.IsAvailable = true;
        Doctor.Starttime = document.getElementById('txtStartTime').value;
        Doctor.Endtime = document.getElementById('txtEndTime').value;

        JsonNewSchedule = AddDrSchedule(Doctor);

        if (JsonNewSchedule != undefined)
        {
          //  alert(JsonNewSchedule.status);


            if (JsonNewSchedule.status == "1") {
                //SUCCESS

            var lblErrorCaption = document.getElementById('lblErrorCaption');
            var lblMsgges = document.getElementById('lblMsgges');
            var Errorbox = document.getElementById('Errorbox');

            var lblclass = Alertclasses.sucess;
            var lblmsg = msg.InsertionSuccessFull;
            var lblcaptn = Caption.SuccessMsgCaption;

            Errorbox.style.display = "";
            Errorbox.className = lblclass;
            lblErrorCaption.innerHTML = lblcaptn;
            lblMsgges.innerHTML = lblmsg;

            }

        }

    }

    function AddDrSchedule(Doctor) {
        var ds = {};
        var table = {};
        var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/InsertDoctorSchedule");
        table = JSON.parse(ds.d);
        return table;
    }




