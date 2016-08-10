﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="TheClinicApp1._1.Appointment.Appointment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../js/JavaScript_selectnav.js"></script>
  
       <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>
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
 .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
            font-size:inherit;
        }
 table td
 {
     border:none!important;
 }
 table
 {
border:none!important;
 }
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
                alert();
            });
        });
        //end of document.ready
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
           
            $("#<%=txtPatientName.ClientID %>").val(Records.Name);
            $("#<%=txtPatientMobile.ClientID %>").val(Records.Phone);
            $("#<%=txtPatientPlace.ClientID %>").val(Records.Address);
     
               
        }

        function GetPatientDetails(Patient) {
         
            var ds = {};
            var table = {};
            var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
            ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetails");
            table = JSON.parse(ds.d);
            return table;
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
                         <li role="presentation" class="active"><a href="Appointment.aspx">Appoinments</a></li>
                        <li role="presentation" ><a href="DoctorSchedule.aspx">Schedule</a></li>
                       
                        
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" >
                            <div class="grey_sec">
                                <div class="search_div">
                                    <asp:DropDownList ID="ddlDoctor" runat="server" Width="180px" BackColor="White" ForeColor="#7d6754" Font-Names="Andalus" CssClass="ddl"></asp:DropDownList>
                                </div>
                                <ul class="top_right_links" >
                                    <li><a class="save" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">
                          <div class="row field_row" >
                                    <div class="col-lg-12">
                                <div class="col-lg-5">
    <div id='calendar'></div>
<div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-content">
   
    <div class="modal-body">
    <table class="tblModal"><tr>
        <td> <label for="title">Title:</label></td>
        <td><input type="text" class="txtAddNew" id="txtTitle" /></td>
           </tr>
        <tr>
            <td> <label for="endDate">Date:</label></td>
            <td> <input type="text" class="txtAddNew" id="txtEndDate" /></td>
            <%--<td><label for="mandatoryField" id="endDateMandatory" style="color:red;">*yyyy-mm-dd</label></td>--%>
        </tr>
        <tr>
            <td><label for="startTime">Start Time:</label></td>
            <td><input type="text" class="txtAddNew" id="txtstartTime" name="time"/></td>
        </tr>
        <tr>
            <td><label for="endTime">End Time:</label></td>
            <td><input type="text" class="txtAddNew" id="txtEndTime" name="time" /></td>
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
                                   <div class="col-lg-1"  style="float:left" >

                                            <div class="loader" style="float:left"></div>

                                        </div>
                                          <div class="col-lg-6" >
                                              <div id="AppointmentLit"></div>
                                              <div id="PatientReg">
                                                  <table>
                                                       <tr>
                                                          <td>
                                                              <asp:Label ID="lblAppointmentDate" runat="server" Text="Date:"></asp:Label>
                                                          </td>
                                                          <td>
                                                               <input class="" name="Date" id="txtAppointmentDate" type="text" disabled="disabled"/>
                                                             <%-- <asp:TextBox ID="txtAppointmentDate" runat="server"></asp:TextBox>--%>
                                                              <br />
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td> <asp:Label ID="lblPatient" runat="server" Text="Patient:"></asp:Label></td>
                                                          <td>
                                                               <div class="search_div">

                        <input class="field" type="search" id="txtSearch" onblur="bindPatient()" name="txtSearch" placeholder="Search patient..." />
                        <input class="button" type="button" id="btnSearch" value="Search" runat="server" onserverclick="btnSearch_ServerClick" disabled />
                    </div>
                                                              <br />
                                                          </td>
                                                          
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                              <asp:Label ID="lblPatientName" runat="server" Text="Name:"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPatientName" runat="server"></asp:TextBox>
                                                              <br />
                                                          </td>

                                                      </tr>
                                                     
                                                      <tr>
                                                          <td>
                                                              <asp:Label ID="lblPatientMobile" runat="server" Text="Mobile:"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPatientMobile" runat="server"></asp:TextBox>
                                                              <br />
                                                          </td>
                                                      </tr>
                                                      
                                                      <tr>
                                                           <td>
                                                              <asp:Label ID="lblPatientPlace" runat="server" Text="Place:"></asp:Label>
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtPatientPlace" runat="server"></asp:TextBox>
                                                          </td>
                                                      </tr>
                                                     
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
        </div>

</asp:Content>
