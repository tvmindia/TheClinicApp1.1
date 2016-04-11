<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="TheClinicApp1._1.Registration.Patients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Script Files -->
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>        
    <script src="../js/bootstrap.min.js"></script>    
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
		     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <!-- Main Container -->
  <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" class="active"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Trithvam Ayurveda</p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Patients Registration</div>
         <div class="icon_box">
         <a class="all_registration_link" data-toggle="modal" data-target="#ViewAllregistration" ><span title="All Registerd" data-toggle="tooltip" data-placement="left"><img src="../images/registerd.png" /></span></a>
         <a class="all_registration_link" data-toggle="modal" data-target="#ViewTodaysRegistration" ><span title="Todays Register" data-toggle="tooltip" data-placement="left"><img src="../images/registerd.png" /></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div>
         <ul class="top_right_links"><li><a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick" ><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>
         </div>        
         <div class="right_form">         
         <div class="alert alert-success" style="display:none">
          <strong>Success!</strong> Indicates a successful or positive action.<a class="alert_close">X</a>
        </div>        
        <div class="alert alert-info" style="display:none">
          <strong>Info!</strong> Indicates a neutral informative change or action.<a class="alert_close">X</a>
        </div>
        
        <div class="alert alert-warning" style="display:none">
          <strong>Warning!</strong> Indicates a warning that might need attention.<a class="alert_close">X</a>
        </div>
        
        <div class="alert alert-danger" style="display:none">
          <strong>Danger!</strong> Indicates a dangerous or potentially negative action.<a class="alert_close">X</a>
        </div>
         
      <div class="registration_form">        
      <div class="row field_row">  
      <div class="col-lg-8">
      <div class="row"> 
      <div class="col-lg-8 margin_bottom"><label for="name">Name</label><input id="txtName" runat="server" type="text" name="name" /></div>
      <div class="col-lg-4 upload_photo_col">
      <div class="margin_bottom upload_photo">
      <img src="../images/UploadPic.png" />
      </div>
      <div class="upload">
      <label class="control-label">Upload Picture</label>
      <asp:FileUpload ID="FileUpload1" ForeColor="Green" Font-Size="12px" runat="server" />
      </div>
      </div>
      <div class="col-lg-8 margin_bottom"><label for="sex">Sex</label>
          <asp:RadioButton ID="rdoMale" runat="server" GroupName="Active" Text="Male" CssClass="checkbox-inline" />
          <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Active" Text="Female" CssClass="checkbox-inline" />
      </div>
      <div class="col-lg-8"><label for="age">Age</label><input id="txtAge" runat="server" type="text" name="age" /></div>
      </div>
      </div>            
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-12">
      <label for="address">Address</label><textarea id="txtAddress" runat="server" style="width:43%"></textarea>
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="mobile">Mobile</label><input id="txtMobile" runat="server" type="text" name="mobile" />
      </div>
      <div class="col-lg-4">
      <label for="email">Email</label><input id="txtEmail" runat="server" type="text" name="email" />
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="marital">Marital</label>
          <asp:RadioButton ID="rdoSingle" runat="server" GroupName="Status" Text="Single" CssClass="checkbox-inline" />
          <asp:RadioButton ID="rdoMarried" runat="server" GroupName="Status" Text="Married" CssClass="checkbox-inline" />
          <asp:RadioButton ID="rdoDivorced" runat="server" GroupName="Status" Text="Divorced" CssClass="checkbox-inline" />
      </div>
      <div class="col-lg-4">
      <label for="occupation">Occupation</label><input id="txtOccupation" runat="server" type="text" name="occupation" />
      </div>
      </div>
      
        </div> 
        
         <div class="generate_token">
         <span><label for="token">Token</label>5</span>
         <span><label for="file_id">File ID</label>H12345</span>
         </div>
         
         
         </div>
         
         </div>  
      </div>
                 
<!-- All Registration Modal -->
<div id="ViewAllregistration" class="modal fade" role="dialog">
  <div class="modal-dialog">
  
  <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Registered List </h4>
      </div>
      <div class="modal-body">

      </div>

  </div>

  </div>

</div>         
         
<!-- Todays Registration Modal -->
<div id="ViewTodaysRegistration" class="modal fade" role="dialog">
  <div class="modal-dialog">
  
  <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Registered List </h4>
      </div>
      <div class="modal-body">

      </div>

  </div>

  </div>

</div>
     <script>
         var test = jQuery.noConflict();
         test(document).on('ready', function () {
             test("#FileUpload").fileinput({
                 browseLabel: 'Upload'
             });
         });
        </script>      
     <script>
         var test = jQuery.noConflict();
         test(document).ready(function () {


             test('.alert_close').click(function () {
                 test(this).parent(".alert").hide();
             });

             test('[data-toggle="tooltip"]').tooltip();



             test('.nav_menu').click(function () {
                 test(".main_body").toggleClass("active_close");
             });



         });

		</script>
           
</asp:Content>
