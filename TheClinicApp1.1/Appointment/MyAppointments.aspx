<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="MyAppointments.aspx.cs" Inherits="TheClinicApp1._1.Appointment.MyAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link href="../css/font-awesome.css" rel="stylesheet" />
    <script src="../Scripts/Common/Common.js"></script>
    <script src="../js/fullcalendar.min.js"></script>
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
    <script src="../Scripts/UserJS/MyAppointment.js"></script>
     <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />
  
     <script src="../js/bootstrap.min.js"></script>
    <link href="../css/MyAppointment.css" rel="stylesheet" />
    <style>
        a.unitLink
        {
            color:#9a8594;
        }
    </style>

         <!-- Main Container -->
        <div class="main_body">

            <!-- Left Navigation Bar -->
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
                </div>
                       <ul class="menu">
                    <li id="patients" ><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                     <li id="Appoinments" class="active" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                    <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                    <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server"><span class="icon logout"></span><span class="text">Logout</span></a></li>

                </ul>
                    <p class="copy">   &copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
                </div>
              <div class="right_part">
                <div class="tagline">
                    <a class="nav_menu">nav</a>
                    My Appointments
                    <ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                        <li>
                           <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate /></li>
                    </ul>
                </div>
                   <div class="right_form tab_right_form">
                       <div class="page_tab">
                            <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" id="PatientApointment"><a href="Appointment.aspx">Book Appoinments</a></li>
                        <li role="presentation" id="DoctorSchedule"><a href="DoctorSchedule.aspx">Doctor Schedule</a></li> 
                        <li role="presentation" id="MyAppointment" class="active"><a href="MyAppointments.aspx">My Appointments</a></li>
                        
                       
                        
                    </ul>
                <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                               <div class="grey_sec">
                               <%-- <div class="search_div">--%>
                                     <asp:DropDownList ID="ddlDoctor" runat="server" onchange="DoctorChange();" CssClass="drop" Width="210px" style="font-family: Arial, Verdana, Tahoma;font-size:large;"></asp:DropDownList>
                               <%-- </div>--%>
                  
                            </div>
                              <div class="tab_table">
                          <div class="row field_row" >
                     <div class="col-lg-12">
                             <table style="border:none;">
                                       <tr>
                                           <td>
                                               <input type="radio" name="dateSelection" class="rdo" id="rdoToday" value="1" checked="checked" />
                                               <label class="rdoLabel">Today</label>
                                           </td>
                                           <td>
                                               <input type="radio" name="dateSelection" class="rdo" id="rdoWeek" value="2"  />
                                               <label class="rdoLabel">This Week</label>
                                           </td>
                                           <td>
                                               <input type="radio" name="dateSelection" class="rdo" id="rdoMonth" value="3" />
                                               <label class="rdoLabel">This Month</label>
                                           </td>
                                           <td>
                                               <input type="radio" name="dateSelection" class="rdo" id="rdoOther" value="4" />
                                               <label class="rdoLabel">Other Date</label>
                             <%--                    <input  class="input-large datepicker" style="display:none;" onchange="AppointmentOtherDateChange(this);" name="StartDate" id="txtStartDate" type="text" readonly="true" placeholder="Select From Date"/>--%>
                                           </td>
                                       </tr>
                                 <tr>
                                      <td></td>
                                     <td></td>
                                     <td></td>
                                     <td> <input  class="input-large datepicker" style="display:none;position:fixed;"  onchange="AppointmentOtherDateChange(this);" name="StartDate" id="txtStartDate" type="text" readonly="true" placeholder="Start Date"/></td>
                                 </tr>
                                   </table>
                          <br />
                                <div class="col-lg-5 ">
                               <div class="name_field" id="MyAppointmentHead" style="background-color: #99c4e0!important; text-transform: none">
                                                        <asp:Label ID="Label1" runat="server" Text="Appointment Date and Patient Count"></asp:Label>
                                                    </div>
                                   
                                   <%--   <h4 id="MyAppointmentHead"> </h4>
                                   --%> <div class="AppointmentDateAndCount"></div>
                                  
                                    </div>

                         <div class="col-lg-7" id="PatientDiv" style="display:none">
         <div id="AppointmentList" >
                                                  <div class="name_field" style="background-color: #99c4e0!important; text-transform: none">
                                                        <asp:Label ID="lblList" runat="server" Text="Patient List"></asp:Label>
                                                    </div>
                                                  <div class="card_white" id="listBody">
                                                     <%-- <table id="listBody" class="tblTimes">
                                                          <thead>
                                                              <tr>
                                                                  <th>Name</th>
                                                                  <th>Appointment No.</th>
                                                                  <th>Time</th>
                                                              </tr>

                                                          </thead>
                                                          <tbody id="listItems">

                                                          </tbody>
                                                      </table>--%>

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
            </div>
              <input type="hidden" id="hdfUserRole" ClientIDMode="Static" runat="server" />
            <input type="hidden" id="hdfDocID" />
            <input type="hidden" id="hdfRadioValue" />
            </div>
  
</asp:Content>
