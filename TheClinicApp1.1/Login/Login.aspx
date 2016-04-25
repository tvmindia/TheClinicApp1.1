<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TheClinicApp1._1.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
          left: 30%;
         
           }
    </style>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
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
     <form id="form1" runat="server">
    <div class="logo" >
        <img src="../favicon.ico"  style="width:50px"/>        
    </div>    
    <div class="login">         
        <div class="input-field ">
          <input id="username" runat="server" type="text"  class="validate" style="color:white;font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif" autocomplete="off" />
          <label for="username" class="captions">User Name</label>
        </div>                
        <div class="input-field">
          <input id="password" type="password" class="validate" runat="server" style="color:white;" readonly onfocus="this.removeAttribute('readonly');" />
          <label for="password" class="captions">Password</label>
        </div>
       
     <div style="text-align:right">

          <button class="btn waves-effect waves-light" type="submit" name="action"  >Login</button>       
     </div>  
 

        <div  class="loginerrormessage">
            <asp:Label   ID="lblmsg" runat="server" Text=""></asp:Label>

        </div>     
</div>

    </form>
    <button id="bt_forgot" onclick="SetIframeSrc('AllRegistrationIframe'),document.getElementById('id01').style.display='block'" class="w3-btn" style="background-color:transparent;color:white;">Forget Password?</button> 
        <div id="id01" class="w3-modal" runat="server">
  <span onclick="document.getElementById('id01').style.display='none'" 
  class="w3-closebtn w3-hover-red w3-container w3-padding-16 w3-display-topright w3-xxlarge" style="color:white">×</span>
  <div class="w3-modal-content w3-card-8 w3-animate-zoom" style="max-width:600px;height:372px;">  
              <iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>
  </div>
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
     
</div>
   
</body>
</html>
