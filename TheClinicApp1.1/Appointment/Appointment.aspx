<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="TheClinicApp1._1.Appointment.Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/1.12.0jquery-ui.js"></script>
     <script src="../js/Messages.js"></script>
    <script src="../Scripts/Common/Common.js"></script>
  <script src='../js/moment.min.js'></script>
  <script src='../js/fullcalendar.min.js'></script>
  <script src='../js/lang-all.js'></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
  <link href='../css/fullcalendar.css' rel='stylesheet' />
  <link href='../css/fullcalendar.print.css' media='print' rel='stylesheet'  />
        <script src='../js/MainCalendarEvents.js'></script>
    <script src="../js/timepicki.js"></script>
    <link href="../css/timepicki.css" rel="stylesheet" />
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
     <style>
        
        body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 14px;
        }
        .clscolor
        {
display: block; float: left; text-align: center;
        }
        #calendar {
            max-width: 900px;
            margin: 0 auto;
        }
        .drop
   {
        border-radius:5px;
        border-color:lightgray;
        
    }
        /*td.fc-day.ui-widget-content.fc-today.ui-state-highlight {
            background-color: #a8d9f3;
        }*/
         .lblColor {
             font-size: smaller;
             line-height: 1;
         }
         .drop
   {
        border-radius:5px !important;
        border-color:lightgray !important;
        
    }
         select {
    color: black;
}
        .foo {
  float: left;
  width: 10px;
  height: 10px;
  border: 1px solid rgba(0, 0, 0, .2);
}

.Count {
  background: rgb(222, 237, 247);
}

.Dates {
  background:#3baae3;
}

.Today {
  background: #ffd19a;
}
        .name_field {background: #3661c7; padding: 10px 20px; text-align: left; font-size: 18px; line-height: 38px; font-weight: bold; color: #fff; text-transform: uppercase; font-family:'roboto-bold'; overflow: hidden; position: relative;}
       .card_white {padding: 15px; display: block; font-family:'roboto-light';}
       .card_white .field_label {display: block; clear: both; color: #000; font-size: 15px;}
.card_white .field_label label {width: 21%; display: inline-block; position: relative; margin: 0 20px 0 0; color: #000; font-size: 15px;}
.card_white .field_label label:after {position: absolute; top: 0; bottom: 0; right: 0; content: ":";}
.card_white .field_label a {color: #000;}
label {
    color: #666666;
    display: block;
    font-family: "raleway-semibold";
    font-size: 14px;
    font-weight: 500;
    line-height: 14px;
    margin: 1px 15px 10px;
}
         .loader {
    border: 10px solid #f3f3f3; /* Light grey */
    border-top: 10px solid #3498db; /* Blue */
    border-radius: 50%;
    width: 25px;
    height: 25px;
    animation: spin 2s linear infinite;
   margin-top:20%;
   margin-left:45%;
}
        .modal table td
        {
            border:none!important;
        }
        .modal table
        {
            border:none!important;
        }
        .icon-plus
        {
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
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
/* The Modal (background) */


/*.ui-state-highlight {
    border: 1px solid #06adfc !important;
    background: #ffef8f url("images/ui-bg_highlight-soft_25_ffef8f_1x100.png") 50% top repeat-x ;
    color: #363636;
}*/
.fc-ltr .fc-basic-view .fc-day-number {
    border: medium none !important;  
    text-align: right;
}
/* Modal Content */


/* Add Animation */
@-webkit-keyframes animatetop {
    from {top:-300px; opacity:0} 
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

.ti_tx,
.mi_tx,
.mer_tx {
  width: 100%;
  text-align: center;
  margin: 10px 0;
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
  margin: auto;
  background: url(../images/arrow.png) no-repeat;
  border-radius: 5px;
}

.next { background-position: 50% 150%; }

.prev { background-position: 50% -50%; }

.time_pick { position: relative; }
.ui-state-highlight {
    border: 1px solid #faf523 !important;
    background: #ffef8f url("images/ui-bg_highlight-soft_25_ffef8f_1x100.png") 50% top repeat-x ;
    color: #363636;
}
/*.fc-ltr .fc-basic-view .fc-day-number {
    border: medium none !important;
    text-align: right;
}*/
/*input{ float:left;}*/
 td.fc-day.ui-widget-content.fc-today.ui-state-highlight {
            background-color: #a8d9f3;
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


.txtAddNew{
width:230px !important;
}
.tblModal
{
border:none !important;
}
.search_div
{
    width:200px;
}
.wrapper {
    border:none;
    display:inline-block;
    position:relative;
}
  td.fc-day.ui-widget-content.fc-today.ui-state-highlight {
            background-color: #ffd19a;
          
        }
 table td
 {
     border:none!important;
 }
 table
 {
border:none!important;
 }
 .searchBtn{
     position: absolute; top: 0; right: 2px; bottom: 0; margin: auto; background: #1d49b2; color: #fff; font-family:'caviardreams-regular'; height: 26px; padding: 0; text-align: center; line-height: 26px; font-size: 14px; border: 0; width: 78px; 
-webkit-border-top-right-radius: 0px;
-webkit-border-bottom-right-radius: 0px;
-moz-border-radius-topright: 0px;
-moz-border-radius-bottomright: 0px;
border-top-right-radius: 0px;
border-bottom-right-radius: 0px;
 }
         .searchPatient {
             position: absolute;
             top: 0;
             right: 1px;
             bottom: 0;
             margin: auto;
           
             color: #fff;
             font-family: 'caviardreams-regular';
             height: 26px;
             padding: 0;
             text-align: center;
             line-height: 26px;
             font-size: 14px;
             border: 0;
             width: 50px;
            border-radius:100%;
             background-image: url("../images/searchPanel.png");
             background-repeat:no-repeat;
           background-position: 20px center;
           background-color:white;
         }
         .fc-day-grid-event > .fc-content {
    white-space: normal;
}
        .fc-event[href], .fc-event.fc-draggable {
    cursor: pointer;
    /*width: 50px!important;
    height:25px;*/
}
         /*.fc-day-grid-event .fc-content {
             background-color:#256db7;
             text-align:center;
         }*/
 .header {
    height: 40px;
 text-align:center;
    background-color: #c6d9eb;
    border: 2px solid #c6d9eb;
    height: 40px;
    -webkit-border-top-left-radius: 5px;
    -webkit-border-top-right-radius: 5px;
    -moz-border-radius-topleft: 5px;
    -moz-border-radius-topright: 5px;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
}
 .Patientheader
 {
 height: 40px;
 text-align:center;
    background-color: #dae4f1;
    border: 2px solid #dae4f1;
    height: 40px;
    -webkit-border-top-left-radius: 5px;
    -webkit-border-top-right-radius: 5px;
    -moz-border-radius-topleft: 5px;
    -moz-border-radius-topright: 5px;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
 }

 .txtBox
 {
     width:350px;
     background-color:#ffffff;
 }
 .Formatdate
 {
   font-size:20px!important;
   font-weight:lighter;
 }
 .tblTimes   tr:nth-child(even) {background: #ebf0f3}
      .tblDates   tr:nth-child(even) {background: #ebf0f3} 
    </style>
    <script>
        var DoctorID="";
        $(document).ready(function () {
            var bindTitle="";
            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            }); 
            $('#ddltimeSlot').change(function() {
                alert( "Handler for .change() called." );
            });
            var ac=null;
            ac = <%=listFilter %>;

            var length= ac.length;
            var projects = new Array();
            for (i=0;i<length;i++)
            {  
                var name= ac[i].split('🏠');
                projects.push({  value : name[0], label: name[0], desc: name[1]})   
            }
            $("#txtSearch").autocomplete({
           
                maxResults: 10,
                source: function (request, response) {
               
                    //--- Search by name or description(file no , mobile no, address) , by accessing matched results with search term and setting this result to the source for autocomplete
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    var matching = $.grep(projects, function (value) {

                        var name = value.value;
                        var label = value.label;
                        var desc = value.desc;

                        return matcher.test(name) || matcher.test(desc);
                    });
                    var results = matching; // Matched set of result is set to variable 'result'

                    response(results.slice(0, this.options.maxResults));
                },
                focus: function (event, ui) {
                    $("#txtSearch").val(ui.item.label);

                    return false;
                },
                select: function (event, ui) {
                BindPatientDetails();
                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                  .append("<a>" + item.label + "<br>" + item.desc + "</a>")
                  .appendTo(ul);
            };

            $(".new").click(function () {
                clearDropDown();
                hideDropDown();
                clearTextBoxes();
                document.getElementById("TimeAvailability").innerHTML = '';
                document.getElementById("TimeAvailability").style.display='none';
                document.getElementById("listBody").innerHTML = '';
                document.getElementById("availableSlot").style.display='none';
                $('#<%=lblAppointment.ClientID%>').text("Add Appointment");
                $('#<%=lblList.ClientID%>').text("Patient List");
                document.getElementById("NoSlots").style.display = 'none';
            });

            $(".save").click(function () {
                var start="";
                var end="";
                var appointmentDate=$("#txtAppointmentDate").val();
                var name=$("#txtPatientName").val();
                var mobile=$("#txtPatientMobile").val();
                var place=$("#txtPatientPlace").val();
                var scheduleID=$("#hdfScheduleID").val();       
                var Time=$( "input:checked" ).val();
                var patientID=$("#hdfPatientID").val();
                var regEx = /^[+-]?\d+$/;
                debugger;
                var timeLength= $("#hdfTimeListLength").val();
                if(mobile.match(regEx)&&mobile.length>=5)
                {
                    if($( "input:checked" ).val()!=undefined)
                    {
                        Time=Time.split('-')[0];
                        if(Time.split(':')[0].length==1)
                        {
                            Time="0"+Time;
                        }
                        var Appointments=new Object();
                        Appointments.AppointmentDate=appointmentDate;
                        Appointments.Name=name;
                        Appointments.Mobile=mobile;
                        Appointments.Location=place;
                        Appointments.ScheduleID=scheduleID;
                        Appointments.AllottingTime=Time;
                        Appointments.PatientID=patientID;
                    
                        var ds={};
                        ds=InsertPatientAppointment(Appointments);
                        if(ds.status=="1")
                        {
                            refreshList();
                            fillPatientDetails();
                            var lblclass = Alertclasses.sucess;
                            var lblmsg = msg.AppointmentSaveSuccessFull;
                            var lblcaptn = Caption.SuccessMsgCaption;

                            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
                        }
                        else
                        {
                            var lblclass = Alertclasses.sucess;
                            var lblmsg = msg.AppointmentSaveFailure;
                            var lblcaptn = Caption.FailureMsgCaption;

                            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
                        }
                    }
                    else if(timeLength=="2")
                    {
                        start=$("#hdfStartTime").val();
                        start=parseFloat(start);
                        end=$("#hdfEndTime").val();
                        end=parseFloat(end);
                        start = end-start;
                        var duration=start.toString();
                        var d1=parseInt(duration.split('.')[0]);
                        var d2=parseInt(duration.split('.')[1]);
                        var a = $("#hdfLastAppointedTime").val();
                        var hours=a.split('.')[0];
                        var minute=a.split('.')[1];
                        hours=parseInt(hours);
                        if (a.includes('PM') && hours < 12) {
                            hours = parseInt(hours) + 12;
                            a = hours + '.' + minute;
                        }
                        var alot1=parseInt(a.split('.')[0]);
                        var alot2=parseInt(a.split('.')[1]);
                        var hourTotal=d1+alot1;
                        var minTotal=d2+alot2;
                        var remainingminuteTotal=null;
                        if(minTotal>60)
                        {
                            remainingminuteTotal=60-minTotal;
                            hourTotal=hourTotal+1;
                            minTotal=remainingminuteTotal;
                            minTotal=minTotal.toString();
                            minTotal=minTotal.replace('-','');
                        }
                        a=hourTotal+":"+minTotal;
                        a=a.toString();
                        var j = parseInt(a.split(':')[0]);                                 
                        if(a.split(':')[1].length==1)
                        {
                            var d= a.split('.')[1];
                            d="0"+d;
                            Time=a.split('.')[0]+":"+d;
                        }
                        else
                        {
                            Time=a;
                        }
                        var Appointments=new Object();
                        Appointments.AppointmentDate=appointmentDate;
                        Appointments.Name=name;
                        Appointments.Mobile=mobile;
                        Appointments.Location=place;
                        Appointments.ScheduleID=scheduleID;
                        Appointments.AllottingTime=Time;
                        Appointments.PatientID=patientID;
                    
                        var ds={};
                        ds=InsertPatientAppointment(Appointments);
                        if(ds.status=="1")
                        {
                            refreshList();
                            fillPatientDetails();
                            var lblclass = Alertclasses.sucess;
                            var lblmsg = msg.AppointmentSaveSuccessFull;
                            var lblcaptn = Caption.SuccessMsgCaption;

                            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
                        }
                        else
                        {
                            var lblclass = Alertclasses.sucess;
                            var lblmsg = msg.AppointmentSaveFailure;
                            var lblcaptn = Caption.FailureMsgCaption;

                            DisplayAlertMessages(lblclass, lblcaptn, lblmsg);
                        }
                    }
                    else
                    {
                        alert("Please select a time");             
                    }
                   
                }
                else
                {
                    alert("Please enter a valid mobile number");
                }
            });
          
        });
    //end of document.ready

    function InsertPatientAppointment(Appointments)
    {
     
        var ds = {};
        var table = {};
           
        var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
        ds=getJsonData(data, "../Appointment/Appointment.aspx/InsertPatientAppointment");
        table = JSON.parse(ds.d);
        return table;
    }
    function GetDoctorID()
    {
      
        var docID=  $('#<%=hdfddlDoctorID.ClientID%>').val();
            var calID= $("#hdfDoctorID").val(docID);
            BindCalendar(calID);
        }
        function bindPatient() {
           
            if (document.getElementById("txtSearch").innerText != "")
                $('#<%=btnSearch.ClientID%>').click();
        }
        function BindPatientDetails() {   
            var jsonPatient = {};
            var SearchItem = $('#txtSearch').val();
            var Patient = new Object();

            if (SearchItem != '') {
                Patient.Name = SearchItem;

                jsonPatient = GetPatientDetails(Patient);
                if (jsonPatient != undefined) {

                    BindPatient(jsonPatient);
                }

            }

        }
       function BindPatient(Records)
        {
           debugger;
        $("#txtPatientName").val(Records.Name);
        $("#txtPatientMobile").val(Records.Phone);
        $("#txtPatientPlace").val(Records.Address);
        $("#hdfPatientID").val(Records.PatientID);
        document.getElementById("txtPatientName").disabled='true';
        document.getElementById("txtPatientPlace").disabled='true';
       }

        function GetPatientDetails(Patient) {
            var ds = {};
            var table = {};
            var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
            ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetails");
            table = JSON.parse(ds.d);
            return table;
        }
        function getFormattedDate(input){
            var pattern=/(.*?)\/(.*?)\/(.*?)$/;
            var result = input.replace(pattern,function(match,p1,p2,p3){
                var months=['Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec'];
                return (p2<10?"0"+p2:p2)+" "+months[(p1-1)]+" "+p3;
            });
            return result;
            
        }
        function clearDropDown()
        {
            var ddl= document.getElementById("<%=ddltimeSlot.ClientID %>");
            ddl.innerHTML="";
        }

        function hideDropDown()
        {
            var ddl= document.getElementById("<%=ddltimeSlot.ClientID %>");
            ddl.style.display='none';
            document.getElementById("br1").style.display='none';
            document.getElementById("br2").style.display='none';
            document.getElementById("timeSlot").style.display='none';
            document.getElementById("NoSlots").style.display = 'none';
        }
        function showDropDown()
        {
            var ddl= document.getElementById("<%=ddltimeSlot.ClientID %>");
            ddl.style.display='block';
            document.getElementById("br1").style.display='block';
            document.getElementById("br2").style.display='block';
            document.getElementById("timeSlot").style.display='block';
        }
        function BindSlotDropDown(title,eventDate)
        {
            debugger;
            bindTitle=title;
            var ddl= document.getElementById("<%=ddltimeSlot.ClientID %>");
            
                $.each(title, function (index, Records) {
                    var option = document.createElement("option");
                    option.text = title[index].split("=")[1];
                    option.value=title[index].split("=")[0];
                    if(title[0])
                    {
                        $("#ddltimeSlot option:selected").text=title[0].split("=")[1];
                        $("#hdfScheduleID").val(title[0].split("=")[0]);
                        $("#hdfTime").val(title[0].split("=")[1]);
                    }
                    ddl.add(option);
                });
            
      
        }
        function SlotDropDownOnchange(title,eventDate)
        {
            debugger;
            bindTitle=title;
            var ddl= document.getElementById("<%=ddltimeSlot.ClientID %>");
            if(eventDate!=$("#hdEventDate").val())
            {
                $.each(title, function (index, Records) {
                    var option = document.createElement("option");
                    option.text = title[index].split("=")[1];
                    option.value=title[index].split("=")[0];
                    if(title[0])
                    {
                        $("#ddltimeSlot option:selected").text=title[index].split("=")[1];
                        $("#hdfScheduleID").val(title[index].split("=")[0]);
                    }
                   //ddl.options[ddl.selectedIndex].text;
                  
                    ddl.add(option);
                });
            }
            var docTime=ddl.options[ddl.selectedIndex].text;
            $("#hdfTime").val(docTime);
            AppendList(eventDate.split(' ')[0]);
        }
        function AppendList(date)
        {
            var FormattedDate=getFormattedDate(date);
            var date=('<span class="Formatdate">'+FormattedDate+'</span>');
            var appList = "Add Appointment For ";
            var list="Patient List For ";
            var time=" at "+$("#hdfTime").val();
            $("#txtAppointmentDate").val(FormattedDate);
            $('#<%=lblAppointment.ClientID%>').text('');
            $('#<%=lblList.ClientID%>').text('');
            $('#<%=lblAppointment.ClientID%>').text(appList).append(date).append(time);
            $('#<%=lblList.ClientID%>').append(list).append(date).append(time);
        }
        function AppendListForDayClick(date)
        {
            var FormattedDate=getFormattedDate(date);
            var date=('<span class="Formatdate">'+FormattedDate+'</span>');
            var appList = "Add Appointment For ";
            var list="Patient List For ";
            $("#txtAppointmentDate").val(FormattedDate);
            $('#<%=lblAppointment.ClientID%>').text('');
            $('#<%=lblList.ClientID%>').text('');
            $('#<%=lblAppointment.ClientID%>').text(appList).append(date);
            $('#<%=lblList.ClientID%>').append(list).append(date);
            document.getElementById("NoSlots").style.display = 'none';
        }
        function TimelistOnchange(i)
        {
            // var i=  $("#ddltimeSlot option:selected").text(); 
            var scheduleId=i.value;
            $("#hdfScheduleID").val(scheduleId);
            fillPatientDetails();
            SlotDropDownOnchange(bindTitle,$("#hdEventDate").val())
        }
        //-- This method is invoked while changing doctor
        function SetDropdown(e) {

            debugger;
            //  $('#hdnDoctorName').val(e.options[e.selectedIndex].text);

            clearDropDown();
            hideDropDown();
            clearTextBoxes();
            document.getElementById("TimeAvailability").innerHTML = '';
            document.getElementById("TimeAvailability").style.display='none';
            document.getElementById("listBody").innerHTML = '';
            document.getElementById("availableSlot").style.display='none';
            document.getElementById("NoSlots").style.display = 'none';
            $('#<%=lblAppointment.ClientID%>').text("Add Appointment");
            $('#<%=lblList.ClientID%>').text("Patient List");
            var imgLen=$('#calendar').find(".fc-day #imgSelect").length;
            for(var i=0;i<=imgLen;i++)
            {
                $('#calendar').find("#imgSelect").remove();
            }
            
            DoctorID = e.value;
            if(DoctorID!="--Select--")
            {
                $("#hdfDoctorID").val(DoctorID);
                BindCalendar(DoctorID);
                var jsonDrSchedule = {};

                ////var Doctor = new Object();
                ////Doctor.DoctorID = DoctorID;

                $('#hdnIsDrChanged').val("Yes");

                jsonDrSchedule = BindFullCalendarEvents(DoctorID);
                if (jsonDrSchedule != undefined) {

                    json = jsonDrSchedule;

                    ////BindScheduledDates();

                    //---------------* Refreshing calender(By removing current json and adding the new one) *------------------//
                    //if ($('#calendar').find("#imgSelect").length != 0)
                    //{
              
                    // }
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
            else
            {
                $('#calendar').fullCalendar('removeEvents');
                var Events = $('#hdnAllEvents').val(); //retrieve array

                if (Events != null && Events != "") {
                    EventsToBeRemoved = JSON.parse(Events);

                    for (var i = 0; i < EventsToBeRemoved.length; i++) {

                        $('#calendar').find('.fc-day[data-date="' + EventsToBeRemoved[i] + '"]').removeClass('ui-state-highlight');
                        $('#calendar').find('.fc-day[data-date="' + EventsToBeRemoved[i] + '"]').removeAttr('background-color');

                    }

                }
            }

        }
        

    </script>
    <div class="main_body">
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img class="small" id="smalllogo" runat="server" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                     <li id="patients" ><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                     <li id="Appoinments"  class="active"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
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
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" CssClass="drop" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate />

                         </li>
                     
                    </ul>
                </div>
                
                <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="Appointment.aspx">Patient Appoinments</a></li>
                        <li role="presentation" ><a href="DoctorSchedule.aspx">Doctor Schedule</a></li> 
                        
                       
                        
                    </ul>
                    <!-- Tab panes -->
                                  
                  
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">
                               <%-- <div class="search_div">--%>
                                     <asp:DropDownList ID="ddlDoctor" runat="server" onchange="SetDropdown(this)" CssClass="drop" Width="210px" style="font-family: Arial, Verdana, Tahoma;"></asp:DropDownList>
                               <%-- </div>--%>
                                <ul class="top_right_links" >
                                    <li><a class="save" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
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
                          <div class="row field_row" >
                                    <div class="col-lg-12">
                                <div class="col-lg-6">
    <div id='calendar'></div>
 <div class="loader" style="float:left"></div>
                                    <br />
                                    <div id="colorBox" style="display:none;">
                                        <%--<ul class="clscolor">
                                            <li>--%>
                                       <%-- <div class="clscolor">--%>
                                        <table>
                                            <tr>
                                                <td>
                                                    
                                                <div class="foo Count"></div>
                                                <label class="lblColor">Scheduled Dates</label>
                                                </td>
                                                <td>
 <div class="foo Dates"></div>
                                                <label class="lblColor">Appointments Count</label>
                                                </td>
                                                <td>
                                                     <div class="foo Today"></div>
                                                <label class="lblColor">Today</label>
                                                </td>
                                            </tr>
                                        </table>
                                                
                                                
                                          <%-- </div>--%>
                                           <%-- </li>
                                        </ul>--%>
                                    </div>
                                    </div>
                             
                                          <div class="col-lg-6" style="height: 100%;">
                                               <div id="PatientReg">
                                                   <div class="name_field" style="background-color: #99c4e0!important; text-transform: none">
                                                       <asp:Label runat="server" ID="lblAppointment" Text="Add Appointment"></asp:Label>
                                                     <%--  <label id="lblAppointment">Add Appointment</label>--%>

                                                   </div>
                                                   <div class="card_white">
                                                  <table>
                                                       <tr>
                                                          <td>
                                                              <label>Date</label>
                                                             <%-- <asp:Label ID="lblAppointmentDate" runat="server" Text="Date:"></asp:Label>--%>
                                                          </td>
                                                          <td>
                                                               <input class="txtBox" name="Date" id="txtAppointmentDate" type="text" disabled="disabled"/>
                                                             <%-- <asp:TextBox ID="txtAppointmentDate" runat="server"></asp:TextBox>--%>
                                                             
                                                             <br />
                                                          
                                                          </td>
                                                          
                                                      </tr>
                                                      <tr>
                                                          <td> <label id="timeSlot" style="display:none">Time Slots</label></td>
                                                          <td>  
                                                            
                                                              <asp:DropDownList ID="ddltimeSlot" onchange="TimelistOnchange(this); return true;" CssClass="drop" runat="server" AutoPostBack="true" Width="350px" Height="31px" style="display:none;">
                                                             
                                                </asp:DropDownList>
                                               
                                                             <label id="br2" style="display:none;"><br /></label>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                             <label id="availableSlot" style="display:none">Available Slots</label>
                                                          </td>
                                                          <td>
                                                               <label id="NoSlots" style="display:none;">No TimeSlots Available..!!!</label>
                                                               <div id="TimeAvailability" style="max-height:115px;width:350px;overflow:auto;border:1px solid #a8d9f3;display:none; padding-left: 10px;">
                                                                 
                                                               </div>
                                                             <label id="br1" style="display:none;"><br /></label>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td><%-- <asp:Label ID="lblPatient" runat="server" Text="Patient:"></asp:Label>--%>
                                                              <label>Patient</label>
                                                          </td>
                                                          <td>
                                                               <div class="wrapper">

                        <input class="txtBox" type="search" id="txtSearch" onblur="bindPatient()" name="txtSearch" placeholder="Search patient..." />
                        <input class="searchPatient" type="button" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" disabled />
                    </div>
                                                              <br />
                                                              <br />
                                                            
                                                          </td>
                                                          
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                             <%-- <asp:Label ID="lblPatientName" runat="server" Text="Name:"></asp:Label>--%>
                                                              <label>Name</label>
                                                          </td>
                                                          <td>
                                                              <input type="text" id="txtPatientName" class="txtBox" />
                                                             <%-- <asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox>--%>
                                                              <br />
                                                          </td>

                                                      </tr>
                                                     
                                                      <tr>
                                                          <td>
                                                              <%--<asp:Label ID="lblPatientMobile" runat="server" Text="Mobile:"></asp:Label>--%>
                                                              <label>Mobile</label>
                                                          </td>
                                                          <td>
                                                              <input type="text" id="txtPatientMobile" class="txtBox"/>
                                                              <%--<asp:TextBox ID="txtPatientMobile" runat="server"></asp:TextBox>--%>
                                                              <br />
                                                          </td>
                                                      </tr>
                                                      
                                                      <tr>
                                                           <td>
                                                             <%-- <asp:Label ID="lblPatientPlace" runat="server" Text="Place:"></asp:Label>--%>
                                                               <label>Place</label>
                                                          </td>
                                                          <td>
                                                              <input type="text" id="txtPatientPlace" class="txtBox"/>
                                                              <%--<asp:TextBox ID="txtPatientPlace" runat="server"></asp:TextBox>--%>
                                                          </td>
                                                      </tr>
                                                     
                                                  </table>
                                                 </div>
                                                 
                                              </div>
                                              <br />
                                              <div id="AppointmentList" >
                                                  <div class="name_field" style="background-color: #99c4e0!important; text-transform: none">
                                                        <asp:Label ID="lblList" runat="server" Text="Patient List"></asp:Label>
                                                    </div>
                                                  <div class="card_white" >
                                                      <table id="listBody" class="tblTimes">

                                                      </table>
       
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
                <asp:HiddenField ID="hdfddlDoctorID" runat="server" />
                <asp:HiddenField ID="hdfDuration" runat="server" />
                <%--  <asp:HiddenField ID="hdfScheduleID" runat="server" />--%>
                <input type="hidden" id="hdfScheduleID" />
                 <input type="hidden" id="hdfDoctorID" />
                <input type="hidden" id="hdEventDate" />
                <input type="hidden" id="hdfTime" />
                 <input type="hidden" id="hdnIsDrChanged" value="No" />
        <input type="hidden" id="hdnAllEvents" value="" />
                <input type="hidden" id="hdfLastAppointedTime" />
                <input type="hidden" id="hdfStartTime" />
                <input type="hidden" id="hdfEndTime" />
                <input type="hidden" id="hdfPatientID" />
                <input type="hidden" id="hdfTimeListLength" />
            </div>
        </div>

</asp:Content>
