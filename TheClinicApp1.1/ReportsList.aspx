<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="ReportsList.aspx.cs" Inherits="TheClinicApp1._1.ReportsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style>
     tr.even td {
  background-color: #99b3f0;
 
    border: none;
    
}
tr.odd td {
  background-color:  #ffffff;
 
    border: none;
 
}


.tab, tr, td,th {
    border: none;
    border-collapse:collapse
}

.tab th{
     border: none;
    border-collapse:collapse;
    height:50px;
}

    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
               <li id="Repots"  class="active"><a name="hello" href="ReportsList.aspx" ><span class="icon admin"></span><span class="text">Reports</span></a></li>
         <li id="master" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
         <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" ><span class="icon logout"></span><span class="text">Logout</span></a></li>
         
         </ul><p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p></div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Reports <ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li><li>         
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClientClick="redirect();"  ToolTip="Logout" formnovalidate /></li></ul>
         </div>
             
            
                  
         <%--<div class="right_form" >  
                    
         <div id="Errorbox"  style="height:30%;display:none;" runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
         </div>
         </div>
                   
      

      </div>--%>

             <div style="height:20px"></div>
               <div class="row field_row">

                                     

                                    <div class="col-lg-8">

 <div class="col-lg-1"></div>
    <div class="col-lg-6">  <asp:PlaceHolder ID = "PlaceHolder1" runat="server" /></div>
                                        
  <div class="col-lg-1"></div>                                   
                                    
                                    </div>
                   </div>

     </div>
    </div> 

</asp:Content>
