﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="DoctorSchedule.aspx.cs" Inherits="TheClinicApp1._1.Appointment.DoctorSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <%--<link href="../css/main.css" rel="stylesheet" />--%>
    <%--<link href="../css/TheClinicApp.css" rel="stylesheet" />--%>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>

    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/1.12.0jquery-ui.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>
    <script src="../Scripts/Common/Common.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/fullcalendar.min.js"></script>
    <script src="../js/lang-all.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../css/fullcalendar.css" rel="stylesheet" />
    <link href="../css/fullcalendar.print.css" rel="stylesheet" media='print' rel='stylesheet' />
    <%--<script src="../js/MainCalendarEvents.js"></script>--%>
    <script src="../Scripts/UserJS/DoctorSchedule.js"></script>

    <script src="../js/timepicki.js"></script>

    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../css/timepicki.css" rel="stylesheet" />
    <%--<script src="../js/jquery-1.9.1.min.js"></script>--%>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/DrSchedule.css" rel="stylesheet" />
        <style>
        .blink {
  animation: blink .5s step-end infinite alternate;
}
@keyframes blink { 
    0%{box-shadow:0 0 10px #07d8d8;}
   50% { box-shadow:0 0 20px #41f5ef; }
       
}
label.noAppointments {
    border: 1px solid #62bbe9;
    width: 520px;
    margin-left: 15px;
    text-align: center;
    padding: 173px;
    min-height:445px;
}
    </style>

    <%--var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;--%>

    <script>

        timStart = '';
        minsStart = '';
        merStart = '';
        var DoctorID = '';

        $(document).ready(function () {
            debugger;
            var selectedDoc = $("#<%=ddlDoctor.ClientID%> option:selected").text();
            debugger;
            if (selectedDoc == "-- Select Doctor --") {
                $("#ContentPlaceHolder1_ddlDoctor").toggleClass('blink');
                $(".token_id_card").css("display", "none");
                $(".noAppointments").css("display", "");
            }
            var usrRole=$('#<%=hdfUserRole.ClientID%>').val()
            if(usrRole=="Doctor")
            {
                $("#PatientApointment").css("display","none");
                $("#DoctorSchedule").css("display","none");
            }
            if(usrRole=="Receptionist")
            {
                $("#MyAppointment").css("display","none");
            }
            if ($("#<%=ddlDoctor.ClientID%> option:selected").text() != "-- Select Doctor --") {
                $('#hdnDoctorName').val($("#<%=ddlDoctor.ClientID%> option:selected").text());
                DoctorID = $("#<%=ddlDoctor.ClientID%> option:selected").val();

                BindCalender(false);

                //var jsonDrSchedule = {};

                //var Doctor = new Object();
                //Doctor.DoctorID = DoctorID;

                //$('#hdnIsDrChanged').val("Yes");

                //jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
                //if (jsonDrSchedule != undefined) {

                //    json = jsonDrSchedule;

                //    BindScheduledDates();

                //    //---------------* Refreshing calender(By removing current json and adding the new one) *------------------//

                //    $('#calendar').fullCalendar('removeEvents');
                //    $('#calendar').fullCalendar('addEventSource', json);
                //    $('#calendar').fullCalendar('rerenderEvents');
                //}
            }


            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });
            $('.nav_menu').click(function () {
           
                $(".main_body").toggleClass("active_close");
            });

            // var DoctorID = document.getElementById('<%=hdnDoctorID.ClientID%>').value

            if (DoctorID != "" && DoctorID != null) {
                GetScheduleByDrID(DoctorID);

                //ele.attr('data-timepicki-tim', tim);
                //ele.attr('data-timepicki-mini', mini);
            }

            else {
                $("#tblDates tr").remove();

                var html = '<tr><td><i>' + "No scheduled date!" + '</i></td></tr>';
                $("#tblDates").append(html);

            }

        });


        //-- To set time picker with regular time 
        //-- Schedule order is passed to the function
        //-- At time of edit click ,we know the time to be binded , so it will also passed , and this time willo get binded to timepicker
        function SetDefaultTime(inputID, time) {

            if (time == null) {

                debugger;

                time = GetRegularScheduleByDrID();
            }

            var timeParts = time.split(',');

            if (inputID == 'txtStartTime') {
                debugger;


                time = timeParts[0];
            }

            if (inputID == 'txtEndTime') {
                time = timeParts[1];
            }

            TimeIn24hrFormat = time;
            var hourEnd = TimeIn24hrFormat.indexOf(":");
            var H = +TimeIn24hrFormat.substr(0, hourEnd);
            var h = H % 12 || 12;
            var ampm = H < 12 ? "AM" : "PM";

            if (parseInt(H) == 0) {

                ampm = "AM";
            }


            timStart = h;
            minsStart = TimeIn24hrFormat.substr(hourEnd, 4).replace(':', '').trim();
            merStart = ampm;

            if (timStart != '' && minsStart != '' && merStart != '') {
                defultTime = timStart + ',' + minsStart + ',' + merStart;
            }

            else {
                defultTime = "";
            }

            return defultTime;
        }

        function OpenModal() {

            $('#myModal').modal('show');


        }

        //-- This method is invoked while changing doctor(using doctor dropdown) or in document.ready
        //-- If method is invoked while changing doctor(using doctor dropdown) , value of variable 'isDropdownItemChanged' will be true
        //-- If method is invoked in document.ready  value of variable 'isDropdownItemChanged' will set to false
        function BindCalender(e, isDropdownItemChanged) {
            var selectedDoc = e.value;
            debugger;
            if (selectedDoc == "-- Select Doctor --") {
                $("#ContentPlaceHolder1_ddlDoctor").toggleClass('blink');
                $(".token_id_card").css("display", "none");
                $(".noAppointments").css("display", "");
            }
            else {
                $("#ContentPlaceHolder1_ddlDoctor").removeClass('blink');
                $(".token_id_card").css("display", "");
                $(".noAppointments").css("display", "none");
            }
            if (isDropdownItemChanged == true) {
                $('#hdnDoctorName').val(e.options[e.selectedIndex].text);
                DoctorID = e.value;
            }

            ClearControls();

            var jsonDrSchedule = {};
            var Doctor = new Object();
            Doctor.DoctorID = DoctorID;
            $('#hdnIsDrChanged').val("Yes");

            jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
            if (jsonDrSchedule != undefined) {

                json = jsonDrSchedule;
                BindScheduledDates();

                //---------------* Refreshing calender(By removing current json and adding the new one) *------------------//

                $('#calendar').fullCalendar('removeEvents');

                // --- 1.previous events are retreived from  hiddenfield hdnAllEvents
                //---- 2.then event bg color is removed

                var Events = $('#hdnAllEvents').val(); //retrieve array

                if (Events != null && Events != "") {
                    EventsToBeRemoved = JSON.parse(Events);
                    for (var i = 0; i < EventsToBeRemoved.length; i++) {
                        $('#calendar').find('.fc-day[data-date="' + EventsToBeRemoved[i] + '"]').removeClass('ui-state-highlight');
                        $('#calendar').find('.fc-day[data-date="' + EventsToBeRemoved[i] + '"]').removeAttr('background-color');
                    }
                }
                $('#calendar').fullCalendar('addEventSource', json);
                $('#calendar').fullCalendar('rerenderEvents');
            }
        }

        function ClearControls() {

            $("#txtAppointmentDate").val("");
            $("#txtStartTime").val("");
            $("#txtEndTime").val("");
            $("#txtMaxAppoinments").val("");

            $("#hdnScheduleID").val("");
            $("#hdnIsErrorTime").val("");
            $("#hdnIsDrChanged").val("No");

            $("#tblDates tr").remove();

            var dates = '<tr><td><i>' + "No scheduled date!" + '</i></td></tr>';
            $("#tblDates").append(dates);


            $("#tblTimes tr").remove();

            var times = '<tr><td><i>' + "No scheduled time!" + '</i></td></tr>';
            $("#tblTimes").append(times);

        }

    </script>

    <div class="main_body">

        <div class="left_part">
            <div class="logo">
                <a href="#">
                    <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
            </div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                <li id="Appoinments" class="active"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon registration"></span><span class="text">Appoinments</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>

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
                        <li role="presentation" id="PatientApointment" ><a href="Appointment.aspx">Book Appoinments</a></li>
                        <li role="presentation" id="DoctorSchedule" class="active"><a href="DoctorSchedule.aspx">Doctor Schedule</a></li>
                         <li role="presentation" id="MyAppointment"><a href="MyAppointments.aspx">My Appointments</a></li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active">
                            <div class="grey_sec">

                                <%--<asp:DropDownList ID="ddlDoctor" runat="server" Width="180px" BackColor="White" ForeColor="#7d6754" Font-Names="Andalus" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged"></asp:DropDownList>--%>

                                <asp:DropDownList ID="ddlDoctor" runat="server" onchange="BindCalender(this,true)" CssClass="drop" Width="210px" Style="font-family: Arial, Verdana, Tahoma;font-size:large;"></asp:DropDownList>
                                <%--CssClass="drop"--%>



                                <%-- <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" disabled/>
                                </div>--%>
                                <ul class="top_right_links">
                                    <li><a class="save" onclick=" AddSchedule();"><span></span>Save</a></li>
                                    <li><a class="new" href="DoctorSchedule.aspx"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div id="Errorbox" style="height: 30%; display: none;">
                                <a class="alert_close">X</a>
                                <div>
                                    <strong>
                                        <span id="lblErrorCaption"></span>

                                    </strong>
                                    <span id="lblMsgges"></span>
                                </div>
                            </div>
                            <div class="tab_table">

                                <div class="row field_row">
                                    <div class="col-lg-12">

                                        <div class="col-lg-6">

                                            <div id='calendar'></div>
                                            <div class="loader" style="float: left"></div>


                                            <div id="myModal" class="modal fade" role="dialog">
                                                <div class="modal-dialog" style="min-width: 550px;">

                                                    <!-- Modal content-->
                                                    <div class="modal-content">
                                                        <div class="modal-header" style="border-color: #3661C7;">
                                                            <button type="button" class="close" data-dismiss="modal" id="UserClose">&times;</button>
                                                            <h3 class="modal-title">Existing Appoinments</h3>
                                                        </div>

                                                        <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                                                            <div class="col-lg-12" style="height: 480px;">
                                                                <div id="divCancellationMsg" class="col-lg-12" style="display: none">


                                                                    <table id="tblReason" style="width: 100%; font-weight: 600; font-size: 16px">
                                                                        <tr>
                                                                            <td style="width: 35%">Reason for cancellation</td>
                                                                            <td style="width: 65%">
                                                                                <input id="txtReason" type="text" name="name" style="border-color: #aab0d4;" /></td>
                                                                        </tr>
                                                                    </table>




                                                                    <fieldset style="border-radius: 15px!important; border: 2px solid #3661C7!important;">

                                                                        <legend style="font-family: caviardreams-regular; color: #3661C7; width: 25%; margin-left: 2%">Regret SMS</legend>

                                                                        <div class="col-lg-12">

                                                                            <div class="row field_row">
                                                                                <div class="col-lg-12" id="divMsg"></div>
                                                                                <div class="col-lg-12">
                                                                                    <table id="tblSms" style="width: 100%!important">
                                                                                        <thead>
                                                                                            <tr>
                                                                                                <th style="width: 50%">Name</th>
                                                                                                <th style="width: 50%">Mobile No</th>
                                                                                            </tr>
                                                                                        </thead>

                                                                                        <tbody id="tbodySms">
                                                                                        </tbody>


                                                                                    </table>
                                                                                </div>
                                                                            </div>

                                                                        </div>




                                                                    </fieldset>



                                                                </div>

                                                                <div class="col-sm-12" style="height: 400px;">
                                                                    <table id="tblPatients" style="width: 100%!important;">

                                                                        <thead>
                                                                            <tr>
                                                                                <th>Patient</th>
                                                                                <th>Appoinment Time</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody id="tbodyPatients">
                                                                        </tbody>


                                                                    </table>
                                                                </div>

                                                            </div>

                                                        </div>

                                                        <div class="modal-footer">
                                                            <div class="grey_sec" id="TableFooter">
                                                                Would You still like to cancel this schedule..?
                              
                                <ul class="top_right_links">
                                    <li><a class="save" id="Okay"><span></span>Yes</a></li>
                                    <li><a class="new" id="Cancel"><span></span>No</a></li>
                                </ul>
                                                            </div>

                                                            <div class="grey_sec" id="MsgFooter" style="display: none">
                                                                <ul class="top_right_links">

                                                                    <li><a class="save" id="SendSms"><span></span>Send SMS</a></li>
                                                                    <li><a class="new" id="Skip"><span></span>Skip</a></li>

                                                                </ul>
                                                            </div>


                                                        </div>


                                                    </div>
                                                </div>
                                            </div>











                                            <br />


                                            <div id="colorBox" style="display: none">

                                                <table id="tblColorCodes">
                                                    <tr>
                                                        <td>

                                                            <div class="foo Count"></div>
                                                            <label class="lblColor">Scheduled Dates</label>
                                                        </td>
                                                        <td>
                                                            <div class="foo Dates"></div>
                                                            <label class="lblColor">Schedules</label>
                                                        </td>
                                                        <td>
                                                            <div class="foo Today"></div>
                                                            <label class="lblColor">Today</label>
                                                        </td>
                                                    </tr>
                                                </table>



                                                <%--  <ul>
                                                    <li >
                                                        <div class="foo Count"></div>
                                                    
                                                        <label class="Legend">Scheduled Dates</label>
                                                    </li>
                                                    <li>
                                                        <div class="foo Dates"></div>
                                                        <label>Scheduled Times</label>
                                                    </li>
                                                    <li>
                                                        <div class="foo Today"></div>
                                                        <label>Today</label>
                                                    </li>
                                                </ul>--%>
                                            </div>



                                        </div>
                                        <br />

                                        <div class="col-lg-6" style="height: 100%;">

                                            <%--   <div class="col-lg-12">--%>

                                            <div class="token_id_card" style="width: 100%;">
                                                <div class="name_field" style="background-color: #99c4e0!important; text-transform: none">
                                                    <%--<asp:Label runat="server"  Text="Add Schedule"></asp:Label>--%>
                                                    <label id="lblAddSchedule" style="text-align: left; font-size: 18px; line-height: 38px; font-weight: bold; color: #fff; font-family: 'roboto-bold';">Add Schedule</label>


                                                </div>

                                                <div class="card_white">

                                                    <div class="row field_row">

                                                        <div class="col-lg-6">

                                                            <label>Scheduled Date</label>
                                                            <input class="" name="Date" id="txtAppointmentDate" type="text" readonly="true" style="font-weight: Bold;" />

                                                        </div>

                                                        <div class="col-lg-6 ">
                                                            <label style="width: 100%;">Max Appoinments</label>

                                                            <input class="" name="MaxAppoinmnt" type="text" id="txtMaxAppoinments" onkeypress="return isNumber(event)" />
                                                        </div>


                                                    </div>

                                                    <div class="row field_row">
                                                        <div class="col-lg-6 ">
                                                            <label>Start Time</label>

                                                            <input type="text" class="txtAddNew" id="txtStartTime" name="time" />
                                                        </div>
                                                        <div class="col-lg-6 ">
                                                            <label>End Time</label>

                                                            <input type="text" class="txtAddNew" id="txtEndTime" name="time" />
                                                        </div>


                                                    </div>



                                                    <div class="row field_row">
                                                        <div class="col-lg-12">
                                                            <label>Existing Schedules</label>

                                                            <hr style="background-color: #99c4e0" />

                                                            <table id="tblTimes">
                                                                <tr>
                                                                    <td><i>No scheduled time!</i></td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>

                                                    <div style="height: 30px"></div>

                                                </div>
                                            </div>
                                            <%--</div>--%>


                                            <div class="token_id_card" style="width: 100%;">

                                                <div class="name_field" style="background-color: #99c4e0!important; text-transform: none">

                                                    <label id="lblExistingSchedules" style="text-align: left; font-size: 18px; line-height: 38px; font-weight: bold; color: #fff; font-family: 'roboto-bold';">Schedule List</label>
                                                    <%--<asp:Label runat="server"  id="lblExistingSchedules" Text="Schedule List"></asp:Label>--%>
                                                </div>
                                                <div class="card_white">

                                                    <div class="field_label">

                                                        <table id="tblDates">
                                                        </table>
                                                    </div>
                                                </div>

                                            </div>
                                            <label class="noAppointments">Please Select Doctor..!!!</label>
                                        </div>
                                    </div>
                                </div>

                            </div>


                            <asp:HiddenField ID="hdnDoctorID" runat="server" Value="" />

                        </div>
                    </div>
                </div>
            </div>




        </div>
        <input type="hidden" id="hdnScheduleID" value="" />
        <input type="hidden" id="hdnIsErrorTime" value="false" />
        <input type="hidden" id="hdnIsDrChanged" value="No" />
        <input type="hidden" id="hdnAllEvents" value="" />
        <input type="hidden" id="hdnIsDeletionByDate" value="" />
        <input type="hidden" id="hdnDoctorName" value="" />
         <input type="hidden" id="hdfUserRole" runat="server" />
    </div>
</asp:Content>
