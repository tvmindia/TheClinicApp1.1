<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Tokens.aspx.cs" Inherits="TheClinicApp1._1.Token.Tokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .hello{
            font-size:30px;
            font-family:'Footlight MT';
            font-weight:bold;
        }
    </style>

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

     <!-- #main-container -->
     <div class="main_body">                  
          <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token" class="active"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Tritham Ayurveda</p>
         </div>
         
         
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">nav</a>
         Need Tokens..</div>     
         <div class="icon_box">
         <a class="all_token_link" data-toggle="modal" data-target="#all_token" ><span title="All Tokens" data-toggle="tooltip" data-placement="left"><img src="../images/tokens.png"/></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div>
         <ul class="top_right_links"><li><a class="book_token" runat="server" id="btnBookToken" onserverclick="btnBookToken_ServerClick"><span></span>Book Token</a></li></ul>
         </div>
         
         <div class="right_form">
         
         <div class="token_id_card">
             <div class="name_field"><asp:Label ID="lblPatientName" runat="server" Text="Patient_test"></asp:Label><span class="generate_token">5</span></div>
                 <div class="light_grey">
                     <div class="col3_div">Age<span><asp:Label ID="lblAge" runat="server" Text="22" Font-Size="Large"></asp:Label></span></div>
                     <div class="col3_div">Gender<span><asp:Label ID="lblGender" runat="server" Text="Male" Font-Size="Large"></asp:Label></span></div>
                     <div class="col3_div">File No<span><asp:Label ID="lblFileNo" runat="server" Text="1120" Font-Size="Large"></asp:Label></span></div>
                 </div>
                 <div class="card_white">
                    <div class="field_label"><label>Address</label><asp:Label ID="lblAddress" runat="server" Text="Patient_address"></asp:Label></div>  
                    <div class="field_label"><label>Mobile</label><asp:Label ID="lblMobile" runat="server" Text="9656605436"></asp:Label></div>  
                    <div class="field_label"><label>Email</label> <a href="mailto: demo@test.com"><asp:Label ID="lblEmail" runat="server" Text="tom.a4s.son@gmail.com"></asp:Label></a></div>  
                    <div class="field_label"><label>Last visit</label><asp:Label ID="lblLastVisit" runat="server" Text="08-04-2016"></asp:Label></div>    
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
    <script src="../js/JavaScript_selectnav.js"></script> 
</asp:Content>
