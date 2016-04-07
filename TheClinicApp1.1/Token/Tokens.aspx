<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Tokens.aspx.cs" Inherits="TheClinicApp1._1.Token.Tokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

        <meta name="description" content="" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />

    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />

    <link href="../css/normalize.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

     <!-- #main-container -->
     <div class="main_body">                  
          <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" class="active"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Tritham Ayurveda</p>
         </div>
         
         
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">nav</a>
         Tagline will be here...</div>     
         <div class="icon_box">
         <a class="all_token_link" data-toggle="modal" data-target="#all_token" ><span title="All Tokens" data-toggle="tooltip" data-placement="left"><img src="../images/tokens.png"/></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here...">
         <input class="button" type="submit" value="Search">
         </div>
         <ul class="top_right_links"><li><a class="book_token" href="#"><span></span>Book Token</a></li></ul>
         </div>
         
         <div class="right_form">
         
         <div class="token_id_card">
             <div class="name_field">Lorem Ipsum <span class="generate_token">5</span></div>
                 <div class="light_grey">
                     <div class="col3_div">25yrs<span>Age</span></div>
                     <div class="col3_div">Female<span>Gender</span></div>
                     <div class="col3_div">1562<span>File No</span></div>
                 </div>
                 <div class="card_white">
                    <div class="field_label"><label>Address</label> Test Test</div>  
                    <div class="field_label"><label>Mobile</label> 456789123</div>  
                    <div class="field_label"><label>Email</label> <a href="mailto: demo@test.com">demo@test.com</a></div>  
                    <div class="field_label"><label>Last visit</label> 22.01.2014</div>    
                </div>
             </div>
         
         </div>
         
         </div>         
         </div>

    <!-- Modal -->
<div id="all_token" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">All Tokens</h4>
      </div>
      <div class="modal-body">
      <h4>Doctor 1</h4>
        <table class="table" width="100%" border="0">
          <tr>
            <th>Token No</th>
            <th>Name</th>
            <th>Time</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>
        <br>

      <h4>Doctor 2</h4>
        <table class="table" width="100%" border="0">
          <tr>
            <th>Token No</th>
            <th>Name</th>
            <th>Time</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>
      </div>      
    </div>

  </div>
</div>
    
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

<script>
			var test=jQuery.noConflict();
			test(document).ready(function(){	
				
			test('[data-toggle="tooltip"]').tooltip();  
			
				
				test('.nav_menu').click(function(){
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
                  var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '..Login/AccessDenied.aspx', '../Pharmacy/Pharmacy.aspx', '../Stock/Stock.aspx'];
              }
              else {
                  var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx'];
              }
              for (i = 0; i < tileList.length; i++) {
                  if (id == tileList[i]) {
                      document.getElementById(id).className = 'active'
                      window.location.href = Url[i];


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
