<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="DoctorSchedule.aspx.cs" Inherits="TheClinicApp1._1.Appointment.DoctorSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>

    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/1.12.0jquery-ui.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>

    <script src="../js/moment.min.js"></script>
    <script src="../js/fullcalendar.min.js"></script>
    <script src="../js/lang-all.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../css/fullcalendar.css" rel="stylesheet" />
    <link href="../css/fullcalendar.print.css" rel="stylesheet" media='print' rel='stylesheet' />
    <%--<script src="../js/MainCalendarEvents.js"></script>--%>
    <script src="../Scripts/UserJS/DoctorSchedule.js"></script>

    <script src="../js/timepicki.js"></script>
    <link href="../css/timepicki.css" rel="stylesheet" />
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />


    <style>
        body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 14px;
        }

        #calendar {
            max-width: 900px;
            margin: 0 auto;
        }

        .loader {
            border: 10px solid #f3f3f3; /* Light grey */
            border-top: 10px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 25px;
            height: 25px;
            animation: spin 2s linear infinite;
            margin-top: 20%;
            margin-left: 45%;
        }

        .icon-plus {
            background-color: #5677a4;
            border: none;
            color: white;
            padding: 5px 12px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 10px;
            margin: 4px 2px;
            cursor: pointer;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            /*border: 1px solid #888;*/
            width: 100%;
            /*height: 100%;*/
            /*box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);*/
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s;
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {
                top: -300px;
                opacity: 0;
            }

            to {
                top: 0;
                opacity: 1;
            }
        }

        @keyframes animatetop {
            from {
                top: -300px;
                opacity: 0;
            }

            to {
                top: 0;
                opacity: 1;
            }
        }

        .modal-header {
            padding: 2px 16px;
            /*background-color: #5cb85c;*/
            color: white;
        }

        .modal-body {
            padding: 2px 16px;
            text-align: center;
        }

        .modal-footer {
            padding: 2px 16px;
            /*background-color: #5cb85c;*/
            color: white;
        }

        .btnOkay {
            background-color: #336699;
            width: 100px;
            height: 30px;
            cursor: pointer;
            margin-left: 50px;
        }

        .btnCncl {
            background-color: #808080;
            width: 100px;
            height: 30px;
            cursor: pointer;
            margin-left: 50px;
        }

        .ti_tx,
        .mi_tx,
        .mer_tx {
            width: 100%;
            text-align: center;
            margin: 10px 10px;
        }

        .time,
        .mins,
        .meridian {
            width: 60px;
            float: left;
            margin: 0px 10px;
            font-size: 20px;
            color: #2d2e2e;
            font-family: 'arial';
            font-weight: 700;
        }

        .prev,
        .next {
            cursor: pointer;
            padding: 18px;
            width: 28%;
            border: 1px solid #ccc;
            background: url(../images/arrow.png) no-repeat;
            border-radius: 5px;
        }

        .next {
            background-position: 50% 150%;
        }

        .prev {
            background-position: 50% -50%;
        }

        .time_pick {
            position: relative;
        }

        /*input{ float:left;}*/

        .timepicker_wrap {
            padding: 10px;
            border-radius: 5px;
            z-index: 2;
            display: none;
            width: 100%;
            box-shadow: 2px 2px 5px 0px rgba(50, 50, 50, 0.35);
            background: #f6f6f6;
            border: 1px solid #ccc;
            float: left;
            position: absolute;
            top: 27px;
            left: 0px;
            height: 145px;
        }

        .arrow_top {
            position: absolute;
            top: -10px;
            left: 20px;
            background: url(../images/top_arr.png) no-repeat;
            width: 18px;
            height: 10px;
            z-index: 3;
        }

        #imgCancelAll {
            cursor: pointer;
        }


        #imgDelete {
            cursor: pointer;
        }

        #imgUpdate {
            cursor: pointer;
        }

        #imgUpdate {
            cursor: pointer;
        }

        .card_white .field_label label:after {
            content: none !important;
        }


        #tblTimes td {
            font-size: 17px !important;
            border: none;
        }

        #tblDates td {
            font-size: 17px !important;
        }

        borderclass {
            border: 1px solid red;
        }

        td.fc-day.ui-widget-content.fc-today.ui-state-highlight {
            background-color: #ffd19a;
          border-color:orange;
        }



        #tblTimes, #tblTimes tr, #tblTimes th, #tblTimes td {
            border: none;
        }

        #tblDates, #tblDates tr, #tblDates th, #tblDates td {
            border: none;
        }

        #tblTimes tr:nth-child(even) {
            background: #ebf0f3;
        }

        #tblDates tr:nth-child(even) {
            background: #ebf0f3;
        }


        #tblColorCodes, #tblColorCodes tr, #tblColorCodes th, #tblColorCodes td {
            border: none;
        }



        /*.ui-state-highlight {
    border:2px  solid #06adfc !important;
    background: #ffef8f url("images/ui-bg_highlight-soft_25_ffef8f_1x100.png") 50% top repeat-x ;
    color: #363636;
  
} 
 

.fc-ltr .fc-basic-view .fc-day-number {
    border: medium none !important;
    text-align: right;
}*/
        .lblColor {
            font-size: smaller;
            line-height: 1;
        }

        .foo {
            float: left;
            width: 10px;
            height: 10px;
            /*border: 1px solid rgba(0, 0, 0, .2);*/
            border: none;
        }

        .Count {
            background: rgb(222, 237, 247);
        }

        .Dates {
            background: #3baae3;
        }

        .Today {
            background: #ffd19a;
        }

        .Legend {
            font-size: smaller;
        }

        .ui-dialog-title
        {
            color:white!important;
        }

        .modal table
        {
            width:100%!important;
            height:100%!important;
        }

        .modal table th{
            background-color:#99c4e0!important;
        }
      
        .success-dialog
        {
            border:2px solid #3baae3;
        }

    label {
    color: #666666;
    display: block;
    font-family: "raleway-semibold";
    font-size: 14px;
    font-weight: 500;
    line-height: 14px;
    margin: 1px 15px 10px;
}

        /*.fc-content
{
    display:none;
}
.fc-time {
    display:none;
}

.fc-day-grid-event .fc-time{
    display:none;
}*/
    </style>

     <%--var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;--%>

    <script>

        timStart = '';
        minsStart = '';
        merStart = '';
        var DoctorID = '';

        $(document).ready(function () {


          

            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });
            $('.nav_menu').click(function () {
                $(".main_body").toggleClass("active_close");
            });

           // var DoctorID = document.getElementById('<%=hdnDoctorID.ClientID%>').value

            if (DoctorID != "" && DoctorID != null) {
                GetScheduleByDrID(DoctorID);



                debugger;

                //ele.attr('data-timepicki-tim', tim);
                //ele.attr('data-timepicki-mini', mini);
            }

            else {
                $("#tblDates tr").remove();

                var html = '<tr><td><i>' + "No scheduled date!" + '</i></td></tr>';
                $("#tblDates").append(html);

            }

        });

        function SetDefaultTime(inputID, time) {

            debugger;
            if (time == null)
            {
                time = GetRegularScheduleByDrID();
            }

            var timeParts = time.split(',');

            if (inputID == 'txtStartTime') {
                time = timeParts[0];
            }

            if (inputID == 'txtEndTime') {
                time = timeParts[1];
            }


            //var starttime = timeParts[0];
            //var endtime = timeParts[1];

            //TimeIn24hrFormat = time;

            TimeIn24hrFormat = time;
            var hourEnd = TimeIn24hrFormat.indexOf(":");
            var H = +TimeIn24hrFormat.substr(0, hourEnd);
            var h = H % 12 || 12;
            var ampm = H < 12 ? "AM" : "PM";

            timStart = h;
            minsStart = TimeIn24hrFormat.substr(hourEnd, 4).replace(':', '').trim();
            merStart = ampm;



            if (timStart != '' && minsStart != '' && merStart != '') {
                defultTime = timStart + ',' + minsStart + ',' + merStart;
            }

            return defultTime;
        }

        function OpenModal() {
            debugger;
            $("#myModal").dialog('open');
        }
        function SetDropdown(e)
        {
            debugger;

            DoctorID = e.value;
           
            var jsonDrSchedule = {};

            var Doctor = new Object();
            Doctor.DoctorID = DoctorID;
          
            jsonDrSchedule = GetDoctorScheduleDetailsByDoctorID(Doctor);
            if (jsonDrSchedule != undefined) {

                json = jsonDrSchedule;

                BindScheduledDates();

                $('#calendar').fullCalendar('removeEvents');
                $('#calendar').fullCalendar('addEventSource', json);
                $('#calendar').fullCalendar('rerenderEvents');


            }


        }
      
    </script>

    <div class="main_body">

        <div class="left_part">
            <div class="logo">
                <a href="#">
                    <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
            </div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="Appoinments" class="active"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon registration"></span><span class="text">Appoinments</span></a></li>
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
                        <li role="presentation"><a href="Appointment.aspx">Patient Appoinments</a></li>
                        <li role="presentation" class="active"><a href="DoctorSchedule.aspx">Doctor Schedule</a></li>

                    </ul>

                    <div id="Errorbox" style="height: 30%; display: none;">
                        <a class="alert_close">X</a>
                        <div>
                            <strong>
                                <label id="lblErrorCaption"></label>
                            </strong>
                            <label id="lblMsgges"></label>
                        </div>
                    </div>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active">
                            <div class="grey_sec">

                                <%--<asp:DropDownList ID="ddlDoctor" runat="server" Width="180px" BackColor="White" ForeColor="#7d6754" Font-Names="Andalus" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged"></asp:DropDownList>--%>

                                <asp:DropDownList ID="ddlDoctor" runat="server"  onchange="SetDropdown(this)"  Width="180px" BackColor="White" ForeColor="#7d6754" Font-Names="Andalus" ></asp:DropDownList>

                              


                                <%-- <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" disabled/>
                                </div>--%>
                                <ul class="top_right_links">
                                    <li><a class="save" onclick=" AddSchedule();"><span></span>Save</a></li>
                                    <li><a class="new" href="DoctorSchedule.aspx"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">

                                <div class="row field_row">
                                    <div class="col-lg-12">

                                        <div class="col-lg-6">

                                            <div id='calendar'></div>
                                            <div class="loader" style="float: left"></div>
                                            <div id="myModal" class="modal" >

                                                <!-- Modal content -->
                                                <div class="modal-content" >

                                                    <div class="modal-body" >

                                                      <%--    <div class="token_id_card">
                            <div class="name_field1">Would You still like to cancel the schedule..<span></span> ?</div>
                            <div class="light_grey">
                            </div>
                            <div class="card_white">
                                <asp:Label Text="Select Your Doctor " Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlDoctorName" Height="70%" AppendDataBoundItems="true" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                        </div>--%>



                                                        <table id="tblPatients" style="width:100%!important;height:100%!important" >

                                                            <thead  >
                                                                <tr>
                                                                    <th >Patient</th>
                                                                    <th >Appoinment Time</th>
                                                                </tr>
                                                            </thead>

                                                          

                                                        </table>

                                                    </div>
                                                    <br />
                                                    <div class="modal-footer">

                                                              <%-- <div class="token_id_card">
                            <div class="name_field1">Would You still like to cancel the schedule..<span></span> ?</div>
                            <div class="light_grey">
                            </div>
                            <div class="card_white">
                                <asp:Label Text="Select Your Doctor " Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlDoctorName" Height="70%" AppendDataBoundItems="true" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                        </div>--%>

                                                      <%--  <div class="token_id_card">
                                                          <div class="name_field1">Would You still like to cancel the schedule..<span></span> ?</div>
                                                            </div>--%>
                                                     <%--   <table>
                                                           
                                                            <tr>
                                                                <td>
                                                                    <button class="btnOkay" id="btnOk">OK</button>
                                                                </td>
                                                                <td>
                                                                    <button class="btnCncl" id="btnCancel">Cancel</button></td>

                                                            </tr>
                                                        </table>--%>


                                                        <div class="grey_sec">
                                                          Would You still like to cancel this schedule..?
                              
                                <ul class="top_right_links">
                                    <li><a class="save" ><span></span>Yes</a></li>
                                    <li><a class="new" id="Cancel" ><span></span>No</a></li>
                                </ul>
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

                                                            <input class="" name="MaxAppoinmnt" type="text" id="txtMaxAppoinments" onkeypress="return CheckisNumber(event)" />
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
          <input type="hidden" id="hdnIsErrorTime" value="" />
       
    </div>
</asp:Content>
