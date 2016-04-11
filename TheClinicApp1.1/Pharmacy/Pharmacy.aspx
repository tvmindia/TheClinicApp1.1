<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Pharmacy.aspx.cs" Inherits="TheClinicApp1._1.Pharmacy.Pharmacy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

     <!-- #main-container -->
         

         
         <div class="main_body">
        
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy" class="active"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Trithvam Ayurveda</p>
         </div>
         
         
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Pharmacy...</div>
         <div class="icon_box">
         <a class="patient_list" data-toggle="modal" data-target="#patient_list" ><span title="Patient List" data-toggle="tooltip" data-placement="left"><img src="../images/patient_list.png"/></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here...">
         <input class="button" type="submit" value="Search">
         </div>
         <ul class="top_right_links"><li><a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick" href="#"><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>
         </div>
         <div class="right_form">
         
         <div class="token_id_card">
             <div class="name_field"><img src="../images/UploadPic.png" width="80" height="80" />Patient_Name</div>
                 <div class="light_grey">
                     <div class="col3_div">25yrs<span>Age</span></div>
                     <div class="col3_div">Male<span>Gender</span></div>
                     <div class="col3_div">1562<span>File No</span></div>
                 </div>
                 <div class="card_white">
                    <div class="field_label"><label>Doctor</label> Test Test</div>      
                </div>
             </div>
         
         
         <div class="prescription_grid" >
        <table class="table" style="width:100%;border:0;" >
  <tbody><tr>
    <th>Medicine</th>
    <th>Quantity</th>
    <th>Unit</th>
    <th>Dosage</th>
    <th>Timing</th>
    <th>Days</th>
  </tr>
  <tr>
     <td ><input id="txtMedName" type="text" placeholder="Medicine" class="input"/></td>
      <td ><input id="txtMedQty" type="text" placeholder="Qty" class="input"/></td>
      <td ><input id="txtMedUnit" class="input" type="text" placeholder="Unit" /></td>
      <td ><input id="txtMedDos" type="text" placeholder="Dosage" class="input"/></td>
      <td><input id="txtMedTime" type="text" placeholder="Timing" class="input"/></td>
      <td><input id="txtMedDay" type="text" placeholder="Days" class="input"/></td><td style="background:#E6E5E5">
    <input type="button" value="-" class="bt1" style="width:20px;"/></td><td style="background:#E6E5E5">
         <input type="button" id="btAdd" onclick="clickAdd(); this.style.visibility = 'hidden';" value="+" class="bt1" style="width:20px" />         
         </td>
  </tr>
  </tbody>      
  </table>
 <div id="maindiv"> 
  </div>
         </div>
         
         </div>
         
         </div>         
         </div>
         
         
         
         
         
         
<!-- Modal -->
<div id="patient_list" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>        
        <h4 class="modal-title">Patient List </h4>
      </div>
      <div class="modal-body">
        <table class="table" width="100%" border="0">
          <tr>
            <th>Sl No.</th>
            <th>Date</th>
            <th>Remarks</th>
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
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
                
        <script>
			var test=jQuery.noConflict();
				test(document).ready(function(){
					
				test('.nav_menu').click(function(){
					test(".main_body").toggleClass("active_close");
				});
			
			});			
		</script>
</asp:Content>
