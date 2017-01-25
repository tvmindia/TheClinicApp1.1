<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="MyAppointments.aspx.cs" Inherits="TheClinicApp1._1.Appointment.MyAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <%-- <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />--%>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <%--<script src="../js/bootstrap.min.js"></script>--%>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>
    <script src="../Scripts/Common/Common.js"></script>

    <script>
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
        });
    </script>

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
                    <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                    <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server"><span class="icon logout"></span><span class="text">Logout</span></a></li>

                </ul>
                    <p class="copy"><img class="imagelogo" src="../Logo.png" /><asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
                </div>
              <div class="right_part">
                <div class="tagline">
                    <a class="nav_menu">Menu</a>
                    My Appointments
                    <ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                        <li>
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" ToolTip="Logout" formnovalidate /></li>
                    </ul>
                </div>
                   <div class="right_form tab_right_form">
                       <div class="page_tab">
                            <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" id="PatientApointment" class="active"><a href="Appointment.aspx">Patient Appoinments</a></li>
                        <li role="presentation" id="DoctorSchedule"><a href="DoctorSchedule.aspx">Doctor Schedule</a></li> 
                        <li role="presentation" id="MyAppointment"><a href="MyAppointments.aspx">My Appointments</a></li>
                        
                       
                        
                    </ul>
               <label>My Appointment page is under construction</label>
            </div>
</div>
            </div>
              <input type="hidden" id="hdfUserRole" runat="server" />
            </div>
  
</asp:Content>
