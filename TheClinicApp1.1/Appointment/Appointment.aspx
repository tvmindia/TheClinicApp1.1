<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="TheClinicApp1._1.Appointment.Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/1.12.0jquery-ui.js"></script>
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
        td.fc-day.ui-widget-content.fc-today.ui-state-highlight {
            background-color: #a8d9f3;
        }
         .lblColor {
             font-size: smaller;
             line-height: 1;
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
  background:#256db7;
}

.Today {
  background: #a8d9f3;
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
    overflow:hidden!important;
    min-height:300px!important;
}


/*.ui-state-highlight {
    border: 1px solid #06adfc !important;
    background: #ffef8f url("images/ui-bg_highlight-soft_25_ffef8f_1x100.png") 50% top repeat-x ;
    color: #363636;
}*/
/*.fc-ltr .fc-basic-view .fc-day-number {
    border: medium none !important;
    text-align: right;
}*/
/* Modal Content */
.modal-content {
    position: relative;
    background-color: #fefefe;
    margin: auto;
    padding: 0;
    /*border: 1px solid #888;*/
    width: 100%;
    height:100%;
    /*box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);*/
    -webkit-animation-name: animatetop;
    -webkit-animation-duration: 0.4s;
    animation-name: animatetop;
    animation-duration: 0.4s
}

/* Add Animation */
@-webkit-keyframes animatetop {
    from {top:-300px; opacity:0} 
    to {top:0; opacity:1}
}

@keyframes animatetop {
    from {top:-300px; opacity:0}
    to {top:0; opacity:1}
}

.modal-header {
    padding: 2px 16px;
    /*background-color: #5cb85c;*/
    color: white;
}

.modal-body {padding: 2px 16px;
             text-align:center;
}

.modal-footer {
    padding: 2px 16px;
    /*background-color: #5cb85c;*/
    color: white;
    border:none!important;
}
.btnOkay
{
background-color: #336699;
width:100px;
height:30px;
cursor:pointer;
margin-left:50px;
}
.btnCncl
{
background-color: #808080;
width:100px;
height:30px;
cursor:pointer;
margin-left:50px;
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

/*input{ float:left;}*/

.timepicker_wrap {
  padding: 10px;
  border-radius: 5px;
  z-index: 2;
  display: none;
  width: 240px;
  box-shadow: 2px 2px 5px 0px rgba(50, 50, 50, 0.35);
  background: #f6f6f6;
  border: 1px solid #ccc;
  float: left;
  position: absolute;
  top: 27px;
  left: 0px;
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
        .fc-event[href], .fc-event.fc-draggable {
    cursor: pointer;
    width: 15px!important;
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
        $(document).ready(function () {
           
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
               <%-- document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";--%>


                return false;
            }
        })
            .autocomplete("instance")._renderItem = function (ul, item) {
                return $("<li>")
                  .append("<a>" + item.label + "<br>" + item.desc + "</a>")
                  .appendTo(ul);

            };
            $(".save").click(function () {
                debugger;
                var appointmentDate=$("#txtAppointmentDate").val();
                var name=$("#txtPatientName").val();
                var mobile=$("#txtPatientMobile").val();
                var place=$("#txtPatientPlace").val();
                var scheduleID=$("#hdfScheduleID").val();       
                var Time=$( "input:checked" ).val();
               
                Time=Time.split('-')[0];
                Time=Time.replace('AM','');
                Time=Time.replace('PM','');
                var Appointments=new Object();
                
                Appointments.AppointmentDate=appointmentDate;
                Appointments.Name=name;
                Appointments.Mobile=mobile;
                Appointments.Location=place;
                Appointments.ScheduleID=scheduleID;
                Appointments.AllottingTime=Time;
                var ds={};
                ds=InsertPatientAppointment(Appointments);

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
           
            $("#txtPatientName").val(Records.Name);
            $("#txtPatientMobile").val(Records.Phone);
            $("#txtPatientPlace").val(Records.Address);
     
               
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
        function AppendList(date)
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
        }
    </script>
    <div class="main_body">
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
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
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate />

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
                    <div id="Errorbox" style="display: none;" runat="server">
                            <a class="alert_close">X</a>
                            <div>
                                <strong>
                                    <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label>
                                </strong>
                                <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
                            </div>
                        </div>                        
                  
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">
                                <div class="search_div">
                                    <asp:DropDownList ID="ddlDoctor" runat="server" Width="180px" BackColor="White" ForeColor="#7d6754" Font-Names="Andalus" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <ul class="top_right_links" >
                                    <li><a class="save" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
                                </ul>
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
                                                          <td></td>
                                                          <td>
                                                               <div id="TimeAvailability" style="max-height:115px;overflow:auto;">
                                                                   
                                                               </div>
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
                <%--  <asp:HiddenField ID="hdfScheduleID" runat="server" />--%>
                <input type="hidden" id="hdfScheduleID" />
                 <input type="hidden" id="hdfDoctorID" />

            </div>
        </div>

</asp:Content>
