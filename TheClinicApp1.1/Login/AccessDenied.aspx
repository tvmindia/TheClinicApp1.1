<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="TheClinicApp1._1.Login.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/jquery-1.12.0.min.js"></script>
    <link href="../css/main.css" rel="stylesheet" />
     <div class="main_body">
  
    <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock" class="active"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Tritham Ayurveda</p>
         </div>
         <div class="right_part">
              <div class="tagline">
         <a class="nav_menu">nav</a>
         Sorry Unauthorized Access.....</div>   
    <div class="content padding-t-100">
        <img src="../images/403.png" alt="403- Access denied" style="opacity:0.4; margin:auto;height:100%; width:100%;padding-top:10%;background-color:#FFFFFF"  /> 
    </div>
        

         </div>
         </div>
      <script>
          var test = jQuery.noConflict();
          test(document).ready(function () {

              test('.nav_menu').click(function () {
                  test(".main_body").toggleClass("active_close");
              });

          });
		</script>

            <script type="text/javascript">
         var settingOpen = 0;
         function selectTile(id) {
             debugger;
             var Role = 'Doctor';
            debugger;
            var tileList = ['patients', 'token', 'doctor', 'pharmacy', 'stock'];
            if (Role == 'Doctor') {
                var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Doctor/Doctors.aspx', '../Pharmacy/Pharmacy.aspx', '../Login/AccessDenied.aspx'];

            }
            else if (Role == 'pharmacist') {
                var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Pharmacy/Pharmacy.aspx', '../Stock/Stock.aspx'];
            }
            else {
                var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx'];
            }
            for (i = 0; i < tileList.length; i++) {
                if (id == tileList[i]) {
                   
                    window.location.href=Url[i];
                    document.getElementById(id).className = 'active'
                    
                }
                else {
                    document.getElementById(tileList[i]).className = tileList[i]

                }

            }

        }


        function openSettings() {
            if (settingOpen == 0) {
                $("#settings").fadeIn("slow", function () {

                });
                settingOpen = 1;
            }
            else {
                $("#settings").fadeOut("slow", function () {

                });
                settingOpen = 0;
            }

        }

        function OpenPageOnHyperLinkClick(HyperLinkid) {
            var url = "";
            debugger;
            if (HyperLinkid == "hlkAssignRoles") {
                NavigateUrl = "../Admin/AssignRoles.aspx";
            }

            else if (HyperLinkid == "hlkCreateUser") {
                NavigateUrl = "../Admin/User.aspx";
            }
            else if (HyperLinkid == "hlkInputMasters") {
                NavigateUrl = "../Admin/Masters.aspx";
            }

            document.getElementById('main').src = NavigateUrl;
            document.getElementById('settings').style.display = "none";

        }


    </script>

</asp:Content>
