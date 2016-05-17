<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="TheClinicApp1._1.Login.Forgot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../css/Masterw3.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
</head>
    <body>

<%-------- <header class="w3-container w3-teal" >
  <h1 style="text-align:center;">Forgot Password</h1>
</header>--------%>

<div class="w3-container w3-half w3-margin-top">

<form id="form1" class="w3-container w3-card-4" runat="server">

<p>    
<label class="w3-label w3-validate">Email</label>
<input class="w3-input" type="text" runat="server" id="txtEmail" style="width:90%" required="required" readonly onfocus="this.removeAttribute('readonly');"/></p>

<p>
<button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnVerificationCode" onserverclick="btnVerificationCode_ServerClick1"> Send me verification code </button></p>


<p>
<label class="w3-label w3-validate">Verification Code</label>
<input class="w3-input" type="password" runat="server" id="txtVerificationCode" style="width:90%" /></p>

<p>
<button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnVerify" onserverclick="btnVerify_ServerClick" formnovalidate>Verify</button></p>
 <p>
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
</p>
</form>

</div>

</body>
</html>
