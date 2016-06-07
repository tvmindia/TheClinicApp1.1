<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sucess.aspx.cs" Inherits="TheClinicApp1._1.Login.Sucess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/Masterw3.css" rel="stylesheet" />
    <title></title>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script>
        $(document).ready(function () {
            var timer = setTimeout(function () {
             
                var close = $("#id01", parent.document.body);
                close[0].style.display = "none";
            }, 10000);
        });
       
    </script>
   <style>
       .sucess{
           font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
       }
       
   </style>
</head>
<body>
    <div class="w3-container w3-half w3-margin-top" >
    <form id="form1" class="w3-container w3-card-4" runat="server">
    <div class="sucess">
    <h2 style="color:royalblue;font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;">Your Password Changed !</h2>
        <div style="height:50px;padding-left:150px;opacity:0.8;">
            <img src="../images/Successs.jpg" style="width:50px;height:50px;visibility:hidden;" />        
           </div>
           
        </div>
        
    </div>
    </form>
        </div>
</body>
</html>
