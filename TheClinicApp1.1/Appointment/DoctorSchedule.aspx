<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="DoctorSchedule.aspx.cs" Inherits="TheClinicApp1._1.Appointment.DoctorSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <script src="../js/JavaScript_selectnav.js"></script>
  
  <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/1.12.0jquery-ui.js"></script> 
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>

    <script src="../js/moment.min.js"></script>
    <script src="../js/fullcalendar.min.js"></script>
    <script src="../js/lang-all.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../css/fullcalendar.css" rel="stylesheet" />
    <link href="../css/fullcalendar.print.css" rel="stylesheet" media='print' rel='stylesheet'  />
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
   margin-top:20%;
   margin-left:45%;
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
}

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

    <script>

      $(document).ready(function ()
      {
          $('.alert_close').click(function () {
              $(this).parent(".alert").hide();
          });
          debugger;

         var DoctorID = document.getElementById('<%=hdnDoctorID.ClientID%>').value

          if (DoctorID != "" && DoctorID != null) {
              GetScheduleByDrID(DoctorID);
          }

          else
          {
              $("#tblDates tr").remove();

                  var html = '<tr><td>' + "No Scheduled Date yet !" + '</td></tr>';
                  $("#tblDates").append(html);
             
          }
          
        });

     
    </script>
    
    <div class="main_body" >
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
                         <li role="presentation" ><a href="Appointment.aspx">Appoinments</a></li>
                        <li role="presentation" class="active"><a href="DoctorSchedule.aspx">Schedule</a></li>
                       
                        
                    </ul>

                    <div id="Errorbox" style="height: 30%; display: none;" >
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
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">

                                 <asp:DropDownList ID="ddlDoctor" runat="server" Width="180px" BackColor="White" ForeColor="#7d6754" AutoPostBack="true" OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged" Font-Names="Andalus" CssClass="ddl"></asp:DropDownList>

                               <%-- <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" disabled/>
                                </div>--%>
                                <ul class="top_right_links" >
                                    <li><a class="save"  onclick=" AddSchedule();"><span></span>Save</a></li>
                                    <li><a class="new" href="DoctorSchedule.aspx"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">
                              
                                <div class="row field_row"  >
                                    <div class="col-lg-12">

                                       
                                        <div class="col-lg-5" style="height:500px" >

                                             <div id='calendar' ></div>
                                             <div class="loader" style="float:left"></div>
                                        </div>

                                         <%--<div class="col-lg-1"  style="float:left" >

                                           

                                        </div>--%>
                                          <div class="col-lg-6" >
                                              
                                              <div style="height:50%;" class="col-lg-12" >

                                                  <table  >

                                                      <thead>
                                                          <tr>  <th colspan="2" style="background-color: #dae4f1!important;color:black">Add Schedule</th></tr>
                                                        
                                                      </thead>
                                                      <tbody >
                                                          <tr>
                                                              <td style="width:30%">Date</td>
                                                              <td>
                                                                 

                                                                  <input class="" name="Date" id="txtAppointmentDate" type="text" readonly="true"/>

                                                              </td>

                                                          </tr>
                                                          <tr>
                                                              <td>Time</td>
                                                              <td style="min-height:31px;">
                                                                  <table id="tblTimes">
                                                                      
                                                                  </table>


                                                              </td>

                                                          </tr>
                                                          <tr>
                                                              <td >
                                                                   <div class="col-lg-12" style="padding-left:0px!important;padding-right:0px!important;">
                                                                 
                                                                        <div class="col-lg-6"  style="padding-left:0px!important;padding-right:0px!important;" >
                                                                   <input   type="text" placeholder="Start Time" id="txtStartTime" />
                                                                      </div>
                                                                        
                                                                       <div class="col-lg-6" style="padding-left:0px!important;padding-right:0px!important;">
                                                                  <input   type="text" placeholder="End Time" id="txtEndTime"/>
                                                                      </div>
                                                                     </div> 
                                                              </td>
                                                              <td><input class="" name="MaxAppoinmnt" type="text" placeholder="maximum appoinments " id="txtMaxAppoinments"/></td>
                                                              
                                                          </tr>

                                                          
                                                      </tbody>

                                                  </table>


                                                   

                                              </div>

                                             <div style="height:130px"></div>
                                          
                                              <div class="col-lg-12">

                                                 <table  >

                                                      <thead>
                                                          <%--<tr>  <th colspan="2" style="background-color: #dae4f1!important;color:black">Schedule List</th></tr>--%>
                                                         <tr>  <th  style="background-color: #dae4f1!important;color:black">Schedule List</th></tr>
                                                      </thead>
                                                      <tbody>
                                                         
                                                          <tr>
                                                             
                                                              <td>
                                                                  <table id="tblDates" >
                                                                      
                                                                  </table>


                                                              </td>

                                                          </tr>
                                                          
                                                      </tbody>

                                                  </table>

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
                 

                

            </div>
       

</asp:Content>
