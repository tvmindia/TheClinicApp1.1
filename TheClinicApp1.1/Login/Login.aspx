﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TheClinicApp1._1.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ClinicLite</title>
   <!-- FAVICON -->
    <link rel="shortcut icon" href="../Logo.png" type="image/x-icon" />
    <!-- Css Files -->
    <style type="text/css">
        .auto-style1 {
            width: 203px;
            height: 117px;
            left:40%;
            top:40%;
            position:fixed;
            z-index:-1;
        }
 
         .auto-style2 {
            width: 203px;
            height: 117px;
            left:40%;
            top:70%;
            position:fixed;
            z-index:-1;
          }
          #bt_forgot{
          position:absolute;
          transition: .5s ease;
          top: 60%;
          left: 33%;      
          }         
          
        @media (max-width: 768px) {
           #bt_forgot {
           font-size:60%;
           top:85%;
           }
           #user,#pass
           {
           font-size:40%;
           }
           #logins{
              font-size:0.875em;
              display:block;
              left:-20px;
              margin-top:35px;
           }
           
           
}
input:-webkit-autofill {
    /* background-color: transparent !important; */
    -webkit-box-shadow: 0 0 0 1000px rgba(111, 109, 109, 0.18) inset !important;
    /* -webkit-box-shadow: none; */
    /* opacity: 0.75; */
    /* transition: background-color 5000s ease-in-out 0s !important; */
}
    </style>
    <meta name="viewport" content="width=device-width,height=device-height,initial-scale=1.0"/>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/materialize.min.js"></script>
       <script>
        function SetIframeSrc(HyperlinkID){
            if (HyperlinkID=="AllRegistrationIframe")
            {
                var AllRegistrationIframe=document.getElementById('ViewAllRegistration');
                AllRegistrationIframe.src="../Login/Forgot.aspx";
            }
        }

         

    </script>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <link href="../css/materialize.min.css" rel="stylesheet" />
    <link href="../css/Masterw3.css" rel="stylesheet" />
</head>
<body class="Semitransparent_1">
    <%-- <form id="form1" runat="server" autocomplete="off">--%>
    <div class="logo" >
        <img src="../Logo.png"  style="width:50px"/>        
    </div>    
    <div class="login" id="loginRowFluid">       
        <form id="form1" runat="server" autocomplete="off">  
            <h3>Sign In</h3>
        <div class="input-field">
          <input id="username" runat="server" placeholder="User Name" type="text"  class="validate" style="height:2rem!important;color:white;font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif" autocomplete="off" />
        <%--  <label for="username" class="captions" id="user">User Name</label>--%>
        </div>                
        <div class="input-field">
          <input id="password" type="password" class="validate" placeholder="Password" runat="server" style="height:2rem!important;color:white;" autocomplete="off" />
        <%--  <label for="password" class="captions" id="pass">Password</label>--%>
        </div>
       
     <div class="input-field col s12" style="text-align:right">

          <button class="btn waves-effect waves-light" type="submit" name="action" id="logins" autofocus>LOGIN</button>       
     </div>  
 

        <div  class="loginerrormessage">
            <asp:Label   ID="lblmsg" runat="server" Text=""></asp:Label>

        </div>  
            </form>   
</div>

   <%-- </form>--%>
    <button id="bt_forgot" onclick="SetIframeSrc('AllRegistrationIframe'),document.getElementById('id01').style.display='block'" class="w3-btn" style="background-color:transparent;color:white;">Forget Password?</button> 
        <div id="id01" class="w3-modal" runat="server">
  <span onclick="document.getElementById('id01').style.display='none'" 
  class="w3-closebtn w3-hover-red w3-container w3-padding-16 w3-display-topright w3-xxlarge" style="color:white">×</span>
  <div class="w3-modal-content w3-card-8 w3-animate-zoom" style="max-width:450px;height:245px;">  
              <iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>
  </div>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
     
</div>
   
</body>
</html>
