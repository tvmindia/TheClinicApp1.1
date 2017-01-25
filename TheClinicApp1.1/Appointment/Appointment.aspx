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
    <link href="../css/patientAppointment.css" rel="stylesheet" />
    <script>
        var DoctorID="";
        $(document).ready(function () {
            debugger;
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
           var role=$('#<%=lblAppointment.ClientID%>').val()
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
                    var MobileNo = ui.item.desc.split('|')[2];
                    BindPatientDetails(MobileNo);
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
debugger;
                var start="";
                var end="";
var appointmentNo="";
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
                        appointmentNo=Time.split('+')[1];
                        Time=Time.split('+')[0];
                        
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
                        Appointments.appointmentno=appointmentNo;
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
                        alert("Please select appointment time");             
                    }
                   
                }
                else
                {
                    alert("Please enter a valid mobile number");
                }
            });
            $('.nav_menu').click(function () {
           
                $(".main_body").toggleClass("active_close");
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
        function BindPatientDetails(MobileNo) {   
            var jsonPatient = {};
            var SearchItem = $('#txtSearch').val();
            var Patient = new Object();
            Patient.Phone = MobileNo;
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
            
            if(DoctorID!="--Select Doctor--")
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
                            var dateString = moment(EventsToBeRemoved[i]).format('DD');
             
                            $('.fc-day-number').each(function () {
                  
                                var dayDate = moment(EventsToBeRemoved[i]).format('YYYY-MM-DD');
                   
                                var day = $(this).text();
                                var dayTemp = day;
                  
                                if (day < 10)
                                {
                                    day = "0" + day;
                                }

                                if (dateString == day) {
                        
                                    if ($(this).attr('data-date') == dayDate ) {
    
                                        $(this).html(dayTemp );
                                    }
                       
                                }

                            }); 
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
                     <li id="patients" ><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                     <li id="Appoinments"  class="active"><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                    <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                  
                   
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
                        <li role="presentation" id="PatientApointment" class="active"><a href="Appointment.aspx">Patient Appoinments</a></li>
                        <li role="presentation" id="DoctorSchedule"><a href="DoctorSchedule.aspx">Doctor Schedule</a></li> 
                        <li role="presentation" id="MyAppointment"><a href="MyAppointments.aspx">My Appointments</a></li>
                        
                       
                        
                    </ul>
                    <!-- Tab panes -->
                                  
                  
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">
                               <%-- <div class="search_div">--%>
                                     <asp:DropDownList ID="ddlDoctor" runat="server" onchange="SetDropdown(this)" CssClass="drop" Width="210px" style="font-family: Arial, Verdana, Tahoma;font-size:large;"></asp:DropDownList>
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
                    <label style="display:none" id="lblUserRole" runat="server"></label>
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
                <input type="hidden" id="hdfAppointmentNoCollection" />
                <input type="hidden" id="hdfUserRole" runat="server" />
            </div>
        </div>

</asp:Content>
