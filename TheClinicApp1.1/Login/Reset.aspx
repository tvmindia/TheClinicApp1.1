﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="TheClinicApp1._1.Login.Reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>resetpassword</title>
    <link href="../css/mui.css" rel="stylesheet" />
    <script type="text/javascript">
        function Validate() {
            debugger;
        var password = document.getElementById("txtNewPassword").value;
        var confirmPassword = document.getElementById("txtConfirmPassword").value;
        if (password != confirmPassword) {
            var labelValid = document.getElementById("lblError");            
            labelValid.innerText = "Password Missmatch";
            labelValid.style.color = "red";
            return false;
        }
        else {
            return true;
        }
        
    }
</script>
</head>
    <body>
    <form id="form1" runat="server">
    <div class="mui-container">
        <h1></h1>
        <%--<div style="height:100px">
         <h1 style="text-align:center">Would You Like to Change Password ?</h1>
        </div>--%>
       <%-- <h1></h1>
        <h1></h1>
        <h1></h1>--%>
  <legend>Change Password</legend>
  <div class="mui-textfield">
    <input type="password" placeholder="New Password" id="txtNewPassword" runat="server" />
  </div>
  <div class="mui-textfield">
    <input type="password" placeholder="Confirm Password" id="txtConfirmPassword" runat="server" />
  </div>
  
        <asp:Button ID="btnReset" CssClass="mui-btn mui-btn--raised" OnClientClick="return Validate()" OnClick="btnReset_Click" runat="server" Text="Submit" />
  <%--<button type="submit" class="mui-btn mui-btn--raised" id="btnReset" runat="server" onclick="return Validate();" onserverclick="btnReset_ServerClick">Submit</button>--%>

        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
          <div id="Errorbox"  style="height:25%;  display:none;"  runat="server" >
    <div>
    <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
    <asp:Label ID="lblMsgges" runat="server" Visible="false" Text=""></asp:Label>
    </div>
    </div> 
    </div>

        

    </form>
</body>

</html>
