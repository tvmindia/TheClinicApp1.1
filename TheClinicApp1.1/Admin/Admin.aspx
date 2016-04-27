<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TheClinicApp1._1.Admin.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Script Files -->  
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
   
    <script src="../js/jquery-1.12.0.min.js"></script>
     
    <script src="../js/bootstrap.min.js"></script>  
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>

     <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" ><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
             <li id="admin" class="active"><a name="hello" onclick="selectTile('admin','')"><span class="icon registration"></span><span class="text">Admin</span></a></li>
         </ul>
         
         <p class="copy"><asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
        Administrator</div>
          
         <div class="grey_sec">
         <div class="search_div">
            
         <input class="field" type="search" id="txtSearch" name="txtSearch" placeholder="Search here..." />
         <input class="button" type="button" id="btnSearch" value="Search" runat="server"   />
         </div>
         <ul class="top_right_links"></ul>
         </div>        
         <div class="right_form">         
         <div id="Errorbox"  style="height:30%;display:none;" runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
         </div>
         </div>
             <div class="alert alert-info" id="divDisplayNumber" visible="false" runat="server" ><a class="alert_close">X</a>
             <div>
             <div><asp:Label ID="lblDisplayFileNumber" runat="server" Text="File Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblFileCount" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;<asp:Label ID="lblTokenNumber" runat="server" Text="Token Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblTokencount" runat="server" Text=""></asp:Label></strong></div>
                           
             </div>

             </div>
                
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
            
  

         </div>

         </div>
 </div> 
</asp:Content>
