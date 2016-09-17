<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="ReportsList.aspx.cs" Inherits="TheClinicApp1._1.ReportsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    

    <script src="Scripts/jquery-1.12.0.min.js"></script>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>

    <style>
     tr.even td {
  background-color:#e1e6ef;
 
    border: none;
    
  
    
}
tr.odd td {
  background-color:  #ffffff;
 
    border: none;
  
 
}


table td{
    width:19%!important;
    height:30%!important;
    padding-left:5px;
    padding-bottom:5px;
    margin:5px 5px 5px 5px 5px;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px!important;

}


/*.tab th{

     
     font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
    font-weight:100;
}*/


.tab, tr, td,th {
 
    border: none;
   
     /*border-collapse:separate;
    border-spacing:0 20px;*/
    
}

.tab th{

     font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:18px;
    font-weight:100;
     border: none;
    border-collapse:collapse;
 
    text-align:left;
   background-color:white;
   color:black;
    text-decoration: underline;

    /*font-weight:100;
      font-size:20px!important;*/

}

    </style>

      <%--Style ANd Script Files OF CAlenderControl--%>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.9.1.min.js"></script>     
    <script src="../js/jquery-ui.js"></script>


    




    <script>


        $(document).ready(function () {         
            
         
           

            $('.nav_menu').click(function () {
                
                $(".main_body").toggleClass("active_close");
            });  
        });

        function SetIframeSrc(ID, ReportName)
        {
            debugger;
            //This function loads report within an iframe 
            //Browser back button causes navigations to previous tabs instead of report list . 
            //Inordser to solve the browser back issue history is manipulated using PUSHSTATE
            //Changing the src of iframe causes additional entry in pustate
            //To avoid additional entry issue, rather changing src of iframe , Iframe Is Recreated
            //While displaying Report , list will be hidden and vice versa
          
           // document.getElementById('ReprtList').style.display = "none";
           
            //if (window.history && window.history.pushState) {

             //   window.history.pushState('forward', null, 'IndividualReport.aspx'); //Manipulation of url as well as browser history
              //  $(window).on('popstate', function () {

            //    document.getElementById('ReprtList').style.display = "";
                 
            //    $("#IframeReport").attr("src", "");
                  
            //    });

            //}

            //--------------------------- * replace the old iframe with a new one * ---------------------------//
            //var urls = " IndividualReport .aspx?ID=" + ID + "&ReportName=" + ReportName;
            //var original = document.getElementsByTagName("iframe")[0];
            //var newFrame = document.createElement("iframe");
            //newFrame.src = urls;
            //newFrame.frameBorder = 0;
            //newFrame.id = "IframeReport";
            //newFrame.style = "width: 100%; height: 600px";
            //var parent = original.parentNode;
            //parent.replaceChild(newFrame, original);


          document.getElementById('ReprtList').style.display = "none";
        $("#IframeReport").attr("src", " IndividualReport .aspx?ID=" + ID + "&ReportName=" + ReportName);
          
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div class="main_body">
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a></div>
         <ul class="menu">

               <li id="patients"><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                 <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
             <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                 <li id="admin"  runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                 <li id="Repots" class="active"><a name="hello"  href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
              

         </ul><p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p></div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Reports <ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li><li>         
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click" OnClientClick="redirect();"  ToolTip="Logout" formnovalidate /></li></ul>
         </div>
             
            
                  
         <%--<div class="right_form" >  
                    
         <div id="Errorbox"  style="height:30%;display:none;" runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
         </div>
         </div>
                   
      

      </div>--%>

          
               <div  id="ReprtList">

                                     

                                    <div class="col-lg-12">

 <div class="col-lg-1"></div>
    <div class="col-lg-10">  <asp:PlaceHolder ID = "PlaceHolder1" runat="server"  /></div>
                                        
  <div class="col-lg-1"></div>                                   
                                    
                                    </div>
                  
                 

     </div>
              
              
                <div class="col-lg-12" >
              <iframe id="IframeReport" style ="width: 100%; height: 1000px;overflow-x:hidden;overflow-y:scroll"  frameBorder="0"></iframe>-

                </div>
    </div>
            
             
                 </div>   
</asp:Content>
