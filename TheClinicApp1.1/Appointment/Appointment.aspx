<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="TheClinicApp1._1.Appointment.Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <%--   <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/jquery-1.3.2.min.js"></script>
        <script src="../js/jquery-1.12.0.min.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/bootstrap.min.js"></script>
        <script src="../js/fileinput.js"></script>
        <script src="../js/JavaScript_selectnav.js"></script>
        <script src="../js/DeletionConfirmation.js"></script>
        <script src="../js/Messages.js"></script>
        <script src="../js/Dynamicgrid.js"></script>--%>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
     <script src="../js/jquery-1.12.0.min.js"></script>
     <script src='../js/moment.min.js'></script>
  <script src='../js/fullcalendar.min.js'></script>
  <script src='../js/lang-all.js'></script>
    <link href='../css/jquery-ui.min.css' rel='stylesheet' />
  <link href='../css/fullcalendar.css' rel='stylesheet' />
  <link href='../css/fullcalendar.print.css' media='print' rel='stylesheet'  />
      <%--  <script src='../js/MainCalendarEvents.js'></script>--%>
    <script src="../js/timepicki.js"></script>
    <link href="../css/timepicki.css" rel="stylesheet" />
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />
 
    <div class="main_body">
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                     <li id="patients" ><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                     <li id="Appoinments"  class="active"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon registration"></span><span class="text">Appoinments</span></a></li>
                    <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                  
                   
                </ul>

                <p class="copy">
                    &copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label>
                </p>
            </div>

            <div class="right_part">
                <div class="tagline">
                    <a class="nav_menu">nav</a>
                    Appoinments<ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label>

                        </li>

                         <li>
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate />

                         </li>
                     
                    </ul>
                </div>
                
                <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                         <li role="presentation" class="active"><a href="Appointment.aspx">Appoinments</a></li>
                        <li role="presentation" ><a href="DoctorSchedule.aspx">Schedule</a></li>
                       
                        
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">
                                <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" disabled/>
                                </div>
                                <ul class="top_right_links" >
                                    <li><a class="save" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">
                                 <div class="loader"></div>
    <div id='calendar'></div>
<div id="myModal" class="modal">
                               <div class="modal-content">
   
    <div class="modal-body">
    <table><tr>
        <td> <label for="title">Title:</label></td>
        <td><input type="text" id="txtTitle" /></td>
           </tr>
        <tr>
            <td> <label for="endDate">Date:</label></td>
            <td> <input type="text" id="txtEndDate" /></td>
            <%--<td><label for="mandatoryField" id="endDateMandatory" style="color:red;">*yyyy-mm-dd</label></td>--%>
        </tr>
        <tr>
            <td><label for="startTime">Start Time:</label></td>
            <td><input type="text" id="txtstartTime" name="time"/></td>
        </tr>
        <tr>
            <td><label for="endTime">End Time:</label></td>
            <td><input type="text" id="txtEndTime" name="time" /></td>
        </tr>
    </table>
         
    </div>
      <br />
    <div class="modal-footer">
       <table><tr>
           <td>
               <button class="btnOkay" id="btnOk">OK</button>
           </td><td> <button class="btnCncl" id="btnCancel">Cancel</button></td>

              </tr></table>
  
       
    </div>
  </div>
    </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                 

                

            </div>
        </div>
    <script>

        var json;
        var title, eventStartDate, eventEndDate;
        var tooltip;

        $(document).ready(function () {
            //$("#txtstartTime").timepicki();
            //$("#txtEndTime").timepicki();
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

                    defaultDate: '2016-07-12',
                    businessHours: true, // display business hours
                    lang: initialLangCode,
                    selectable: true,
                    selectHelper: true,
                    select: function (start, end) {

                        CustomClick();
                        // $('#calendar').fullCalendar('renderEvent', eventData, true); // stick? = true

                        $('#calendar').fullCalendar('unselect');
                    },
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

                        eventStartDate = date.format();
                        eventEndDate = date.format();

                    },
                    eventAfterRender: function (event, element) {
                        $(element).tooltip({
                            title: event.title,
                            container: "body"
                        });
                    },

                    eventMouseover: function (calEvent, jsEvent) {

                        if ((calEvent.end != null) && (calEvent.title) != null && (calEvent.id) != null && (calEvent.start) != null) {
                            var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;border-style: solid; border-width: 5px;height:150px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center">' + calEvent.title + '</h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '<p><b>End:</b>' + calEvent.end._i + '</p></div>';
                        }
                        if (calEvent.end == null) {
                            var tooltip = '<div class="tooltipevent" style="text-align:center;width:200px;height:110px;border-style: solid; border-width: 5px;border-color:#999966;color:#000000;background:#e6e6e6 ;position:absolute;z-index:10001;"><h3 style="background:#3661c7 ;color:#ffffff; text-align:center">' + calEvent.title + '</h3><p><b>id:</b>' + calEvent.id + '<br/><p><b>Start:</b>' + calEvent.start._i + '</p></div>';
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
                    debugger;
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
        function AddEvent(CalendarSchedule) {
            debugger;
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
        function CustomClick() {
            debugger;
            $("#myModal").dialog('open');
            function D(J) { return (J < 10 ? '0' : '') + J; };
            var dt = new Date();
            var hours = dt.getHours();
            var ampm = hours >= 12 ? 'PM' : 'AM';
            hours = hours % 12;
            hours = hours ? hours : 12; // the hour '0' should be '12'
            var starttime = D(dt.getHours()) + " : " + dt.getMinutes() + " : " + ampm;
            $("#txtstartTime").val(starttime);

            var mins = hours * 60 + dt.getMinutes() + 30;


            var minutes = (mins % (24 * 60) / 60 | 0) + ' : ' + D(mins % 60);


            var endtime = minutes + " : " + ampm;
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

    </script>
</asp:Content>
