<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="TheClinicApp1._1.Login.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/jquery-1.12.0.min.js"></script>
     <script src="../js/JavaScript_selectnav.js"></script>

    <link href="../css/main.css" rel="stylesheet" />
     <div class="main_body">
  
    <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                   <li id="admin" ><a name="hello" onclick="selectTile('admin','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Admin</span></a></li>

         </ul>
         
         <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         <div class="right_part">
              <div class="tagline">
         <a class="nav_menu">nav</a>
               Sorry, You are not authorized to access <span id="module" runat="server" style="width:auto;"></span>  info ! </div>   
    <div class="content padding-t-100">
        <img src="../images/403.png" alt="403- Access denied" style="opacity:0.4; margin:auto;height:100%; width:100%;padding-top:10%;background-color:#FFFFFF"  /> 
    </div>
        

         </div>
         </div>
    <script src="../js/JavaScript_selectnav.js"></script>
      <script>
          var test = jQuery.noConflict();
          test(document).ready(function () {
              activateTabSelection('<%=From%>');
              test('.nav_menu').click(function () {
                  test(".main_body").toggleClass("active_close");
              });

          });
		</script>

         

</asp:Content>
