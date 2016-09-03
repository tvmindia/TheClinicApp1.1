
var AvailableCount = '';
var ScheduleNo = 0;
var json;
var title, eventStartDate, eventEndDate;
var tooltip;
var DoctorID;
var MonthName='';
var Year = '';
var AllotedEndTimes = [];
var AllotedStartTimes = [];

var allEvents = [];

var StartTimeOnEdit = '';
var EndTimeOnEdit = '';



    $(document).mouseup(function (e) {
    var container = $("#calendar");

    if (!container.is(e.target) // if the target of the click isn't the container...
        && container.has(e.target).length === 0) // ... nor a descendant of the container
    {

        $("#txtAppointmentDate").css({ border: '1px solid #dbdbdb' }).animate({
            borderWidth: 1
        }, 500);

    }
});

    $(document).ready(function () {
        debugger;
      
        //$('body').on('click', 'button.fc-prev-button', function () {
        //    $('#hdnIsDrChanged').val("");
        //});

        //$('body').on('click', 'button.fc-next-button', function () {
        //    $('#hdnIsDrChanged').val("");
        //});
      


      //  document.getElementsByClassName('timepicker_wrap').append('<p>ddbb</p>');

    $("#txtStartTime").timepicki();
    $("#txtEndTime").timepicki();
    
 
    $("#myModal").dialog({
        autoOpen: false,
        closeOnEscape: false,
        draggable: false,
        height: 300,
        width: 500,
        // hide: { effect: "explode", duration: 1000 },
        //modal: true,
        resizable: false,
        show: { effect: "blind", duration: 800 },
        title: "Appoinments",
        dialogClass: 'no-close success-dialog',
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        }
    }).prev(".ui-dialog-titlebar").css("background", "#336699");;

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
           // timeFormat: 'hh:mm a',

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
            //dayRender: function (date, cell) {
            //    cell.css("background-color", "red");
            //},
            select: function (start, end) {
                debugger;
               // CustomClick();

                $("#txtStartTime").val('');
                $("#txtEndTime").val('');
                $("#txtMaxAppoinments").val('');
                $("#hdnScheduleID").val("");
                $("#hdnIsErrorTime").val("");
                $("#hdnIsDrChanged").val("No");


                if (DoctorID != null && DoctorID != "") {

                    //background-color: #a8d9f3;

                    $("#txtAppointmentDate").val(moment(eventStartDate).format('DD MMM YYYY'));
                    $("#lblAddSchedule").html("Add Schedule On : "+moment(eventStartDate).format('DD MMM YYYY'));

                    $("#txtAppointmentDate").css({ border: '0 solid #3baae3' }).animate({
                        borderWidth: 3
                    }, 500);

                    $('#calendar').fullCalendar('unselect');

                    GetScheduledTimesByDate();

                    //$('#tblTimes tr').each(function (i, el) {

                    //    debugger;

                    //    var $tds = $(this).find('td'),
                    //        content = $tds.eq(i).text()

                    //    alert($(this).eq(i).text());

                    //    //    product = $tds.eq(1).text(),
                    //    //    Quantity = $tds.eq(2).text();
                    //    // do something with productId, product, Quantity
                    //});


                }


                else {
                    alert("Please Select a doctor");
                }



            },
          
            editable: false,
           
             eventRender: function (event, element, view) {
                 debugger;
                 

                 //--1. All events are stored to allEvents array 
                 //--2. When changing doctor, allevents array will be cleared by monitoring value of hiddenfield which is, hdnIsDrChanged
                 //-- 3. By retrieving values from array , bg color is appied
                 //-- 4.Color fr TODAY date will reapplied , as it loses its background color


                 if ($('#hdnIsDrChanged').val() == "Yes") { //Checking doctor dropdown value changed , then allevents array is cleared, 
                     allEvents = [];
                     $('#hdnIsDrChanged').val(""); // -- this assignment is to ensure that allevents array is cleared onlt once after changing doctor dropdown

                 }

                 var dateString = moment(event.start).format('YYYY-MM-DD');

                //--------------------- * Converting Start time from 24 hr Format to 12hr format * --------------------//  
               
                StrtTimeIn12hrFormat = ConvertTimeFormatFrom24hrTo12hr(event.StartTime);
               
                //--------------------- * Converting Start time from 24 hr Format to 12hr format * --------------------// 

                endTimeIn12hrFormat = ConvertTimeFormatFrom24hrTo12hr(event.EndTime);
               
               element.context.textContent = StrtTimeIn12hrFormat+'..'; //Event data 
             
              
               allEvents.push(dateString); //-- Event dates are pushed to array
          
               $('#hdnAllEvents').val(JSON.stringify(allEvents));


            },
         
            dayClick: function (date, jsEvent, view) {
              
              eventStartDate = date.format();
                eventEndDate = date.format();

            },
            eventAfterRender: function (event, element) {
              
                //----* here BG color for evnts are applied by retrieving events from allevents array *-----//

                var dateString = moment(event.start).format('YYYY-MM-DD');
                
                ////--------------------- * Applying bg-Color for event dates * --------------------// 
                if ($.inArray(dateString, allEvents) != -1) { 
                    debugger;
                    $('#calendar').find('.fc-day[data-date="' + dateString + '"]').addClass('ui-state-highlight')
                    $('#calendar').find('.fc-day[data-date="' + dateString + '"]').css({ 'background-color': '#deedf7!important' });

                }

                ////--------------------- * Applying bg-Color for Today * --------------------// 
                      var TodayDate= moment(today).format('YYYY-MM-DD')

                     $('#calendar').find('.fc-day[data-date="' + TodayDate + '"]').addClass('ui-state-highlight')
                     $('#calendar').find('.fc-day[data-date="' + TodayDate + '"]').css({ 'background-color': '#ffd19a!important' });

                
             //  $('.fc-content').remove();
              //  element.append("<img  src='../images/hand.png' />");

                $(element).tooltip({
                   
                    container: "body"
                });
            },
           
            eventMouseover: function (calEvent, jsEvent) {

               

                if ((calEvent.StartTime != null) && (calEvent.EndTime) != null )
                {
                    StrtTimeIn12hrFormat = ConvertTimeFormatFrom24hrTo12hr(calEvent.StartTime);
                    endTimeIn12hrFormat = ConvertTimeFormatFrom24hrTo12hr(calEvent.EndTime);
                    var tooltip = '<div class="tooltipevent" style="text-align:center;width:150px;border-style: solid; border-width: 1px;height:110px;border-color:#3a87ad;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3baae3 ;margin-top:0;color:#ffffff; text-align:center">' + 'Appoinment' + '</h3><p><b>Start Time: </b>' + StrtTimeIn12hrFormat + '<p><b>End Time: </b>' + endTimeIn12hrFormat + '</p></div>';
                    //var tooltip = '<div class="tooltipevent" style="text-align:center;border-style: solid; border-width: 5px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center"></h3><p><b>Start Time:</b>' + StrtTimeIn12hrFormat + '<p><b>End Time:</b>' + endTimeIn12hrFormat + '</p></div>';
                }



                //if ((calEvent.end != null) && (calEvent.title) != null && (calEvent.id) != null && (calEvent.start) != null) {
                //    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;border-style: solid; border-width: 5px;height:150px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center"></h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '<p><b>End:</b>' + calEvent.end._i + '</p></div>';
                //}
                //if (calEvent.end == null) {
                //    var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;height:110px;border-style: solid; border-width: 5px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center"></h3><p><b>Start:</b>' + calEvent.start._i + '</p></div>';
                //}
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

         
           , dayRender: function (date, element) {
              
               //var dateString = moment(date).format('YYYY-MM-DD');
               //$('#calendar').find('.fc-day[data-date="' + dateString + '"]').removeClass('ui-state-highlight');
               //$('#calendar').find('.fc-day[data-date="' + dateString + '"]').removeAttr('background-color');



            document.getElementById("colorBox").style.display = "block";
            var date = new Date($("#calendar").fullCalendar('getDate').format());
            
             MonthName = getMonthName(parseInt(date.getMonth()));
             Year  = parseInt(date.getFullYear());

             if ($("#lblExistingSchedules").text() != "Schedule List of : " + MonthName) {
                 $("#lblExistingSchedules").html("Schedule List of : " + MonthName);
                 BindScheduledDates();
             }

        
            //    //var month_int = date.getMonth();

            //    //alert(month_int);


            if ($("#imgSelect").length == 0) {
                $(".fc-day-number").append("<img id='imgSelect' src='../Images/add.png' title='Add Schedule' style='float: left;cursor:pointer;height:10px!important' />")
                //  $(".fc-day-number").append("<p id='imgSelect'  title='Add Schedule' style='float: left;cursor:pointer;background-color:black;color:white' >+</p>")
            }

        },
          
           eventClick: function (date, jsEvent, view) {
               debugger;

                $("#txtStartTime").val('');
                $("#txtEndTime").val('');
                $("#txtMaxAppoinments").val('');
                $("#hdnScheduleID").val("");
                $("#hdnIsErrorTime").val("");
                $("#hdnIsDrChanged").val("No");


               //var clickedDate = date;


               //alert(clickedDate.start);

               if (DoctorID != null && DoctorID != "") {

                   //background-color: #a8d9f3;

                   $("#txtAppointmentDate").val(moment(date.start).format('DD MMM YYYY'));
                   $("#lblAddSchedule").html("Add Schedule On : " + moment(date).format('DD MMM YYYY'));

                   $("#txtAppointmentDate").css({ border: '0 solid #3baae3' }).animate({
                       borderWidth: 3
                   }, 500);

                   $('#calendar').fullCalendar('unselect');

                   GetScheduledTimesByDate(date.start);
               }


               else {
                   alert("Please Select a doctor");
               }



           }
        });
    }, 3600);
   
    //function IsDateHasEvent(date) {
    //    var allEvents = [];
    //    allEvents = $('#calendar').fullCalendar('clientEvents');
    //    var event = $.grep(allEvents, function (v) {
    //        return +v.start === +date;
    //    });
    //    return event.length > 0;
    //}

    $('.loader').delay(3150).fadeOut('slow');

    /*Modal dialog Cancel button click*/
    $('#Cancel').click(function () {
        $("#txtTitle").val("");
        $("#txtEndDate").val("");
        $("#txtstartTime").val("");
        $("#txtEndTime").val("");
        $("#myModal").dialog("close");
    });


    $('#Okay').click(function () {
        debugger;

        var Appointments = new Object();
        ScheduleID = document.getElementById('hdnScheduleID').value;
        
        Appointments.ScheduleID = ScheduleID;

        var ds = {};
        var table = {};

        var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/CancelAllAppoinments");
        table = JSON.parse(ds.d);

       
        if (table.status == 1) {





            GetScheduledTimesByDate();
            BindScheduledDates();

            var jsonDrSchedule = {};

            var Doctor = new Object();
            Doctor.DoctorID = DoctorID;

            jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
            if (jsonDrSchedule != undefined) {

                $('#calendar').fullCalendar('removeEventSource', json);

                json = jsonDrSchedule;

                $('#calendar').fullCalendar('addEventSource', json);
                $('#calendar').fullCalendar('refetchEvents');
            }


            // $('#calendar').fullCalendar('refetchEvents');
        }




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


    getMonthName = function (MonthNo) {
        var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        return monthNames[MonthNo];
    }

//------------------------Animate Div---------------------------//
    function blink(selector) {
  
    $(selector).animate({ fontSize: "2.2em" }, 2000, function () {
        //  $(selector).animate({fontSize: "2em"},2000 )
        // blink(this);                    
    });
}
//---------------------------------------------------------//

    function ConvertTimeFormatFrom24hrTo12hr(Time)
    {
    var TimeIn24hrFormat = Time;
    var hourEnd = TimeIn24hrFormat.indexOf(":");
    var H = +TimeIn24hrFormat.substr(0, hourEnd);
    var h = H % 12 || 12;
    var ampm = H < 12 ? "AM" : "PM";
    //TimeIn12hrFormat = h + TimeIn24hrFormat.substr(hourEnd, 4) + ampm;
    TimeIn12hrFormat = moment(Time, ["h:mm A"]).format("hh:mm") + ampm;

    

    return TimeIn12hrFormat;
}

/*end of document.ready*/

    function GetRegularScheduleByDrID() {
        debugger;
        var strttime = '';
        var endtime = '';
        var jsonRegularSchedule = {};

        var Doctor = new Object();

        if (DoctorID != null && DoctorID != "" ) {

            Doctor.DoctorID = DoctorID;
          
            if (AvailableCount == 0 || ScheduleNo>0) {
                Doctor.ScheduleOrder = parseInt(ScheduleNo + 1);
            }
            
           
            

            //if ($("#tblTimes tr").length == 1)
            //{
            //    var firstTd = $("#tblTimes tr td").text();

            //    if (firstTd == "No scheduled time!")
            //    {
            //        Doctor.ScheduleOrder = parseInt(1);
            //    }
            //    else
            //    {
            //        Doctor.ScheduleOrder = parseInt($("#tblTimes tr").length + 1);
            //    }
            //} 
            //else
            //{
            //    Doctor.ScheduleOrder = parseInt($("#tblTimes tr").length + 1);
            //}
           

            var ds = {};
            var table = {};
            var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
            ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetRegularScheduleOFDoctor");
            Records = JSON.parse(ds.d);

            $.each(Records, function (index, Records) {

                if (Records.Starttime != null && Records.Endtime != null) {
                    debugger;
                    strttime = Records.Starttime;
                    endtime = Records.Endtime;
                }

            })

        }


        Time = strttime + ',' + endtime;
        return Time;
        //return strttime;
    }

    function GetScheduledTimesByDate(Date)
    {
    var jsonDeatilsByDate = {};

    var Doctor = new Object();


    if (DoctorID != null && DoctorID != "") {

       // $("#divDate").fadeTo('fast', 0).fadeTo('fast', 1).fadeTo('fast', 0).fadeTo('fast', 1)
       
      //  $("#txtAppointmentDate").css("border-color", "#3661c7");

       // $("#txtAppointmentDate").toggleClass('borderClass');

       // $("#txtAppointmentDate").val(eventStartDate);

        Doctor.DoctorID = DoctorID;

        if (Date != null) {
            Doctor.SearchDate = Date; 
        }
        else
        {
            Doctor.SearchDate = eventStartDate;
        }
     

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
      
      
        debugger;
    DoctorID = drID;

    var jsonDrSchedule = {};

    var Doctor = new Object();
    Doctor.DoctorID = drID;
   // var MonthName = document.getElementById('hdnMonthName').value;
    
    //if (MonthName == '') {
    //    var todaysDate = new Date();
    //    MonthName =getMonthName( parseInt( todaysDate.getMonth()));
    //}

    //Doctor.MonthName = MonthName;
    jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
    if (jsonDrSchedule != undefined) {

       

        json = jsonDrSchedule;
        BindScheduledDates();

    }
   
}

    function BindScheduledDates()
    {
     

        var Doctor = new Object();
        Doctor.DoctorID = DoctorID;

        if (MonthName == '' && Year == '') {
            var todaysDate = new Date();
            MonthName = getMonthName(parseInt(todaysDate.getMonth()));
            Year = parseInt(todaysDate.getFullYear());
        }

        Doctor.MonthName = MonthName;
        Doctor.Year = Year;

        if (DoctorID != null && DoctorID != "") {

        var ds = {};
        var table = {};
        var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetAllScheduleDetailsOfDoctor");
        Records = JSON.parse(ds.d);

        $("#tblDates tr").remove();
    
        NotAvailableDates = [];
        AvailableDates = [];
  
        $.each(Records, function (index, Records) {
       
            if (Records.IsAvailable == "True") {

                if ($.inArray(Records.AvailableDate, AvailableDates) == -1) { //check if Date not exits than add it
                    AvailableDates.push(Records.AvailableDate); //put object in collection to access it's all values
                }

            }

        });
   
        $.each(Records, function (index, Records) {

            if (Records.IsAvailable == "False") {

                if ($.inArray(Records.AvailableDate, NotAvailableDates) == -1) { //check if Date not exits then add it
              
                    if ($.inArray(Records.AvailableDate, AvailableDates) == -1) {

                        NotAvailableDates.push(Records.AvailableDate); //put object in collection to access it's all values
                    }

                }

            }
        });

        if (AvailableDates.length > 0) {

            for (var i = 0; i < AvailableDates.length; i++) {
                var html = '<tr><td>' + AvailableDates[i] + '</td><td class="center"><img id="imgCancelAll" align="right" height="20" style="margin-right:10px" src="../images/Deleteicon1.png" onclick="CancelAllSchedules(this)"  title="Cancel" /></td></tr>';
                $("#tblDates").append(html);
            }

        }

        if (NotAvailableDates.length > 0) {

            for (var i = 0; i < NotAvailableDates.length; i++)
            {
                var html = '<tr><td><strike>' + NotAvailableDates[i] + '</strike></td><td></td></tr>';
                $("#tblDates").append(html);
            }

        }

   
        if (Records.length == 0) 

        {
            var html = '<tr><td><i>' + "No scheduled date!" + '</i></td></tr>';
            $("#tblDates").append(html);
        }
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
    AllotedEndTimes = [];
    AllotedStartTimes = [];
    ScheduleNo = 0;
    
    if (Records.length == 0) {
        AvailableCount = 0;
    }


    $.each(Records, function (index, Records) {
        debugger;
        var ScheduleID = Records.ID;

        if (Records.Starttime != null && Records.Endtime != null) {

            strttime = ConvertTimeFormatFrom24hrTo12hr(Records.Starttime);
            endtime = ConvertTimeFormatFrom24hrTo12hr(Records.Endtime);

            if (Records.IsAvailable == "True")
            {
                var html = '<tr ScheduleID="' + Records.ID + '" ><td>' + strttime + "-" + endtime + '</td><td class="center"><img id="imgDelete" align="right" height="20" style="margin-right:10px" src="../images/Deleteicon1.png" title="Cancel" onclick="RemoveTime(\'' + ScheduleID + '\')"/><img id="imgUpdate"  height="18" align="right" src="../images/Editicon1.png" title="Change" onclick="BindScheduleOnEditClick(\'' + ScheduleID + '\')" /></td></tr>';
                AllotedEndTimes.push(Records.Endtime);
                AllotedStartTimes.push(Records.Starttime);
                ScheduleNo = parseInt( ScheduleNo + 1);
            }
            else
            {
                var html = '<tr ScheduleID="' + Records.ID + '" ><td><strike>' + strttime + "-" + endtime + '</td></strike><td></td></tr>';
            }

           

            $("#tblTimes").append(html);
        }

    });

    if (Records.length == 0)
    {
        var html = '<tr><td><i>' + "No scheduled time!" + '</i></td></tr>';
        $("#tblTimes").append(html);
    }
  
}

    function BindScheduleOnEditClick(ScheduleID)
{
    var Doctor = new Object();
    Doctor.DocScheduleID = ScheduleID;

    var ds = {};
    var table = {};

    var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/GetDoctorScheduleDetailsByDocScheduleID");
    table = JSON.parse(ds.d);
    Records = table;

    $.each(Records, function (index, Records) {

        var ScheduleID = Records.ID;
        document.getElementById('hdnScheduleID').value = ScheduleID;

        if (Records.Starttime != null && Records.Endtime != null) {

            debugger;

            var TimeIn24hrFormat = Records.Starttime;
            var hourEnd = TimeIn24hrFormat.indexOf(":");
            var H = +TimeIn24hrFormat.substr(0, hourEnd);
            var h = H % 12 || 12;
            var ampm = H < 12 ? "AM" : "PM";
            //TimeIn12hrFormat = h + TimeIn24hrFormat.substr(hourEnd, 4) + ampm;
            TimeIn12hrFormat = moment(Records.Starttime, ["h:mm A"]).format("hh : mm : ") + ampm;
            strttime = TimeIn12hrFormat;
           
            TimeIn24hrFormat = Records.Endtime;
             hourEnd = TimeIn24hrFormat.indexOf(":");
             H = +TimeIn24hrFormat.substr(0, hourEnd);
             h = H % 12 || 12;
             ampm = H < 12 ? "AM" : "PM";
            //TimeIn12hrFormat = h + TimeIn24hrFormat.substr(hourEnd, 4) + ampm;
             TimeIn12hrFormat = moment(Records.Endtime, ["h:mm A"]).format("hh : mm : ") + ampm;
            endtime = TimeIn12hrFormat;

            StartTimeOnEdit = Records.Starttime;
            EndTimeOnEdit = Records.Endtime;
           
            $("#txtStartTime").val(strttime);
            $("#txtEndTime").val(endtime);
            $("#txtMaxAppoinments").val(Records.PatientLimit);

            strttime =  moment(strttime, ["h:mm A"]).format("hh:mm");
            endtime = moment(endtime, ["h:mm A"]).format("hh:mm");

            //SetDefaultTime('txtStartTime', strttime+","+endtime);
            //SetDefaultTime('txtEndTime', strttime + "," + endtime);
         }

    });

}

    function UpadteDrSchedule(Doctor) {
        var ds = {};
        var table = {};
        var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/UpdateDoctorSchedule");
        table = JSON.parse(ds.d);
        return table;
    }

    function RemoveTime(ScheduleID) {
        debugger;
        document.getElementById('hdnScheduleID').value = ScheduleID;

    var DeletionConfirmation = ConfirmDelete(false);
    if (DeletionConfirmation == true) {
    var Doctor = new Object();
    Doctor.DocScheduleID = ScheduleID;

    var ds = {};
    var table = {};

    var data = "{'DocObj':" +JSON.stringify(Doctor) + "}";
    ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/CancelDoctorSchedule");
    table = JSON.parse(ds.d);
   
    debugger;
    if (table.status == 1) {
        GetScheduledTimesByDate();
        BindScheduledDates();

        var jsonDrSchedule = {};

        var Doctor = new Object();
        Doctor.DoctorID = DoctorID;

        jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
        if (jsonDrSchedule != undefined) {
           
            $('#calendar').fullCalendar('removeEventSource', json);
          
            json = jsonDrSchedule;

            $('#calendar').fullCalendar('addEventSource', json);
            $('#calendar').fullCalendar('refetchEvents');
        }


        // $('#calendar').fullCalendar('refetchEvents');
    }

  else {
        OpenModal();
       //  $("#tblPatients tr").remove();
        $('tblPatients tr:not(:first)').remove();
        debugger;
        Records = table;

        $.each(Records, function (index, Records) {

        
            var html = '<tr><td>' + Records.Name + '</td><td>' + Records.AllottingTime + '</td></tr>';

                $("#tblPatients").append(html);
            

        })



  


        //alert(" Sorry, Already scheduled an appointment!")
    }

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

    Array.prototype.remove = function () {
        var what, a = arguments, L = a.length, ax;
        while (L && this.length) {
            what = a[--L];
            while ((ax = this.indexOf(what)) !== -1) {
                this.splice(ax, 1);
            }
        }
        return this;
    };


    function AddSchedule() {
        debugger;

        if (  $("#hdnIsErrorTime").val()== "false") {
    
        var JsonNewSchedule = {};
        var Isalloted = false;


        if (document.getElementById('hdnScheduleID').value != null && document.getElementById('hdnScheduleID').value != "")
        {
    
            //------------ * UPDATE CASE * ----------------//

            debugger;
            var JsonUpdatedSchedule = {};

            var Doctor = new Object();
            Doctor.DocScheduleID = document.getElementById('hdnScheduleID').value;
            Doctor.PatientLimit = parseInt(document.getElementById('txtMaxAppoinments').value);
            Doctor.Starttime = document.getElementById('txtStartTime').value.replace(/ /g, '');
            Doctor.Endtime = document.getElementById('txtEndTime').value.replace(/ /g, '');


            AllotedStartTimes.remove(StartTimeOnEdit);
            AllotedEndTimes.remove(EndTimeOnEdit);

            
             
        }

        //else {
  
        if (DoctorID == "" || DoctorID == null) {
            alert("Please select a doctor");
        }

        else {
    
            if (isNaN(document.getElementById('txtMaxAppoinments').value) == false) {
    
               
                if (document.getElementById('txtAppointmentDate').value.trim() != "" )
                {
                    if (document.getElementById('txtMaxAppoinments').value.trim() != "") {
    
                        if (document.getElementById('txtStartTime').value.trim() != "") {
                            if (document.getElementById('txtEndTime').value.trim() != "") {

                                //&&  &&  && document.getElementById('txtEndTime').value.trim() != "")
                    
                                var StartimeInput = document.getElementById('txtStartTime').value;
                                var endtimeInput = document.getElementById('txtEndTime').value;

                                var InputStartTimeIn24hrFormat = moment(StartimeInput, ["h:mm A"]).format("HH:mm"); //INPUT start time in 24hr format
                                var InputEndTimeIn24hrFormat = moment(endtimeInput, ["h:mm A"]).format("HH:mm");

                                //  InputStartTimeIn24hrFormat = InputStartTimeIn24hrFormat.isValid() ? InputStartTimeIn24hrFormat.format("L") : "";

                                if (InputStartTimeIn24hrFormat < InputEndTimeIn24hrFormat)
                                {

                                    var ItemCount = AllotedEndTimes.length;

                                    if (ItemCount > 0)  //---* if no alloted schedule, no checking is needed *---//
                                    {
                                        for (var i in AllotedEndTimes)
                                        {
                                            var AlreadyAllotedEndTime = moment(AllotedEndTimes[i], ["h:mm A"]).format("HH:mm");
                                            var AlreadyAllotedStartTime = moment(AllotedStartTimes[i], ["h:mm A"]).format("HH:mm");
                     
                                            var FirstItem = moment(AllotedStartTimes[0], ["h:mm A"]).format("HH:mm");

                                            //-----* Item Has to be added to the FIRST position *-----//
                                            if (InputEndTimeIn24hrFormat <= FirstItem )
                                            {
                                                Isalloted == false;
                                                break;
                                            }

                                            else {
                                      

                                                //-----* Item Has to be added to the LAST position *-----//
                                                if (InputStartTimeIn24hrFormat >= AlreadyAllotedEndTime)
                                                {
                                                    Isalloted == false;
                                                }
                                                else
                                                {
                                                    //-----* Item Has to be added IN BETWEEN *-----//

                                                    var ItemJustAbove = moment(AllotedEndTimes[i-1], ["h:mm A"]).format("HH:mm");

                                                    if ((InputEndTimeIn24hrFormat <= AlreadyAllotedStartTime) && (InputStartTimeIn24hrFormat >= ItemJustAbove)) {
                                                        Isalloted = false;
                                                        break;
                                                    }

                                                    else {
                                                        Isalloted = true;
                                                        alert("Sorry,This time has been already alloted.");
                                                        break;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    Isalloted = true;
                                    alert("Please enter a valid time");
                       
                                }
                                //}

                                if (Isalloted == false &&  document.getElementById('hdnScheduleID').value == "" )
                                {
                                    //------------ * INSERT CASE * ----------------//
                                    var Doctor = new Object();
                                    Doctor.DoctorID = DoctorID;
                                    Doctor.DoctorAvailDate = document.getElementById('txtAppointmentDate').value;
                                    Doctor.PatientLimit = parseInt(document.getElementById('txtMaxAppoinments').value);
                                    Doctor.IsAvailable = true;
                                    Doctor.Starttime = document.getElementById('txtStartTime').value.replace(/ /g, '');
                                    Doctor.Endtime = document.getElementById('txtEndTime').value.replace(/ /g, '');

                                    JsonNewSchedule = AddDrSchedule(Doctor);


                                }
                                if (Isalloted == false && document.getElementById('hdnScheduleID').value != "")
                                {
                                    debugger;

                                    //------------ * UPDATE CASE * ----------------//

                        
                      
                                    //if ((StartTimeOnEdit.replace(/ /g, ''))==(moment(document.getElementById('txtStartTime').value, ["h:mm A"]).format("HH:mm")) ) {
                            
                                    //    Doctor.Starttime = StartTimeOnEdit;
                         
                                    //}

                                    //if ((EndTimeOnEdit.replace(/ /g, '')) == (moment(document.getElementById('txtEndTime').value, ["h:mm A"]).format("HH:mm"))) {

                                    //    Doctor.Endtime = EndTimeOnEdit;

                                    //}
                                    JsonUpdatedSchedule = UpadteDrSchedule(Doctor);

                                    JsonNewSchedule = JsonUpdatedSchedule;

                                    document.getElementById('hdnScheduleID').value = "";
                                }
                            }

                            else {
                                alert("Please enter end time");
                            }


                        }
                        else {
                            alert("Please enter start time");
                        }
                
                    }
                    else
                    {
                        alert("Please enter maximum appoinments");
                    }

                }

                else
                {
                    alert("Please select a date");
                }
            }

            else {
                alert("Please enter a valid number");
            }

        }
       

        if (JsonNewSchedule != undefined) {
            //  alert(JsonNewSchedule.status);

            if (JsonNewSchedule.status == "1") {
                //SUCCESS

                var jsonDeatilsByDate = {};

                var Doctor = new Object();


                if (DoctorID != null && DoctorID != "") {

                    Doctor.DoctorID = DoctorID;
                    Doctor.SearchDate = document.getElementById('txtAppointmentDate').value;

                    jsonDeatilsByDate = GetAllDoctorScheduleDetailsByDate(Doctor);

                    if (jsonDeatilsByDate != undefined) {

                        BindTimes(jsonDeatilsByDate);

                        $("#txtStartTime").val("");
                        $("#txtEndTime").val("");
                        $("#txtMaxAppoinments").val("");


                        BindScheduledDates();

                        var jsonDrSchedule = {};

                        var Doctor = new Object();
                        Doctor.DoctorID = DoctorID;

                        jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
                        if (jsonDrSchedule != undefined) {

                            $('#calendar').fullCalendar('removeEventSource', json);

                            json = jsonDrSchedule;

                            $('#calendar').fullCalendar('addEventSource', json);
                            $('#calendar').fullCalendar('refetchEvents');
                        }

                    }
                }

                //var lblErrorCaption = document.getElementById('lblErrorCaption');
                //var lblMsgges = document.getElementById('lblMsgges');
                //var Errorbox = document.getElementById('Errorbox');

                //var lblclass = Alertclasses.sucess;
                //var lblmsg = msg.InsertionSuccessFull;
                //var lblcaptn = Caption.SuccessMsgCaption;

                //Errorbox.style.display = "";
                //Errorbox.className = lblclass;
                //lblErrorCaption.innerHTML = lblcaptn;
                //lblMsgges.innerHTML = lblmsg;

            }

        }
        }

        else
        {
            alert("Please enter a valid time");
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

    function ConvertJsonToDate(jsonDate) {
        if (jsonDate != null) {
           
            var dateString = jsonDate.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth();
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            var monthNames = [
                          "Jan", "Feb", "Mar",
                          "Apr", "May", "Jun", "Jul",
                          "Aug", "Sep", "Oct",
                          "Nov", "Dec"
            ];
            var result = day + '-' + monthNames[month] + '-' + year;
            return result;
        }
    }

    function  CheckisNumber(evt)
    {
        var IsNumber = isNumber();

        if (IsNumber == false) {
            alert("Please enter a number");
        }
    }

    function CancelAllSchedulesByDate(Doctor)
    {
        var ds = {};
        var table = {};
        var data = "{'DocObj':" + JSON.stringify(Doctor) + "}";
        ds = getJsonData(data, "../Appointment/DoctorSchedule.aspx/CancelAllSchedulesByDate");
        table = JSON.parse(ds.d);
        return table;
    }

    function CancelAllSchedules($this)
    {
        debugger;
        var DeletionConfirmation = ConfirmDelete(false);
        if (DeletionConfirmation == true) {
        date =    $($this).closest('td').prev('td').text();
        var DrAvaildate = moment(date).format('YYYY-MM-DD');
        var JsonCancellAll = {};
        var Doctor = new Object();
        Doctor.DoctorID = DoctorID; 
        Doctor.DoctorAvailDate = DrAvaildate;
        JsonCancellAll = CancelAllSchedulesByDate(Doctor);
        if (JsonCancellAll != undefined)
        {
            if (JsonCancellAll.status == "0")
            {
                alert(" Sorry, Already scheduled an appointment!")
            }

            if (JsonCancellAll.status == "1") {

                var jsonDeatilsByDate = {};

                var Doctor = new Object();

                if (DoctorID != null && DoctorID != "") {

                    Doctor.DoctorID = DoctorID;
                    //  Doctor.SearchDate = document.getElementById('txtAppointmentDate').value;
                    Doctor.SearchDate = DrAvaildate;

                    jsonDeatilsByDate = GetAllDoctorScheduleDetailsByDate(Doctor);

                    if (jsonDeatilsByDate != undefined) {

                        //   BindTimes(jsonDeatilsByDate);

                        $("#txtStartTime").val("");
                        $("#txtEndTime").val("");
                        $("#txtMaxAppoinments").val("");

                        BindScheduledDates();

                        var jsonDrSchedule = {};

                        var Doctor = new Object();
                        Doctor.DoctorID = DoctorID;

                        jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
                        if (jsonDrSchedule != undefined) {

                            $('#calendar').fullCalendar('removeEventSource', json);

                            json = jsonDrSchedule;

                            $('#calendar').fullCalendar('addEventSource', json);
                            $('#calendar').fullCalendar('refetchEvents');
                        }

                    }
                }
            }
        }
    }
 }
