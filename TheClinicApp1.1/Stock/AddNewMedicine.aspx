<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddNewMedicine.aspx.cs" Inherits="TheClinicApp1._1.Stock.AddNewMedicine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Masterw3.css" rel="stylesheet" />

    <script src="../js/jquery-1.12.0.min.js"></script>
    
    <script>  
        $(document).ready(function () {
         
                //images that represents medicine name duplication hide and show
                var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                LnameImage.style.display = "none";
                var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                errLname.style.display = "none";
                });



        //---------------* Function to check medicine name duplication *-----------------//

        function CheckMedicineNameDuplication(txtmedicineName) {
            debugger;
            var name = document.getElementById('<%=txtmedicineName.ClientID %>').value;
            name = name.replace(/\s/g, '');

            PageMethods.ValidateMedicineName(name, OnSuccess, onError);

            function OnSuccess(response, userContext, methodName) {

                var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                if (response == false) {

                    LnameImage.style.display = "block";
                    errLname.style.display = "none";

                }
                if (response == true) {
                    errLname.style.display = "block";
                    errLname.style.color = "Red";
                    errLname.innerHTML = "Name Alreay Exists"
                    LnameImage.style.display = "none";

                }
            }
            function onError(response, userContext, methodName) {

            }
        }




    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
    <div class="main_body">

        


        
        <table class="table" border="0" style="width:90%">
        <tr>
            <td>Medicine Name </td>
            <td><asp:TextBox ID="txtmedicineName" runat="server" onchange="CheckMedicineNameDuplication(this)"></asp:TextBox>

            </td>


            
       <asp:Image ID="imgWebLnames" runat="server" ToolTip="Login Name is Available" ImageUrl="~/Images/Check.png" Width="10%" Height="10%"  />
                                        
      <asp:Image ID="errorLnames" runat="server" ToolTip="Login Name is Unavailable" ImageUrl="~/Images/newClose.png"  />


        </tr>
        <tr>
            <td>Medicine Code </td>
            <td><asp:TextBox ID="txtCode" runat="server"></asp:TextBox></td>
            
        </tr>
        <tr>
            <td>Category</td>
            <td><asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true"></asp:DropDownList></td>
            
        </tr>

         <tr>
            <td>Unit</td>
            <td><asp:TextBox ID="txtUnit" runat="server"></asp:TextBox></td>
             
        </tr>

        <tr>
            <td>Reorder Quantity</td>
            <td><asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox></td>
           
        </tr>
            <tr><td> <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnSave" onclick="this.style.visibility='hidden';" onserverclick="btnSave_ServerClick"> Save </button>
            </td>
</tr>
    </table>
       

        </div>


</asp:Content>
