<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddNewMedicine.aspx.cs" Inherits="TheClinicApp1._1.Stock.AddNewMedicine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Masterw3.css" rel="stylesheet" />

    <style>

        .Dropdown
        {
        display: block; padding: 5px; width: 100%; border: 1px solid #dbdbdb; height: 41px; font-family:'roboto-light'; font-weight: bold; font-size: 14px; 
        -webkit-border-radius: 3px;
        -moz-border-radius: 3px;
        border-radius: 3px;
        }




    </style>


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

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
 <div class="main_body">

          <div id="Errorbox"  style="display:none;"  runat="server" ><a class="alert_close">X</a>
                    <div>
                            <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
                                 <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>

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

       <%-- <div class="prescription_grid" >--%>
        
        <table class="table" border="0" style="width:100%">
            <tbody>

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
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:DropDownList ID="ddlCategory" CssClass="Dropdown"  runat="server" AutoPostBack="true">

                </asp:DropDownList>
            </ContentTemplate>
                    </asp:UpdatePanel>

            </td>
            
        </tr>

         <tr>
            <td>Unit</td>
            <td><asp:TextBox ID="txtUnit" runat="server"></asp:TextBox></td>
             
        </tr>

        <tr>
            <td>Reorder Quantity</td>
            <td><asp:TextBox ID="txtOrderQuantity" runat="server"></asp:TextBox></td>
           
        </tr>

            <%--<tr>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnSave" onclick="this.style.visibility='hidden';" onserverclick="btnSave_ServerClick"> Save </button></td>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnNew" onclick="this.style.visibility='hidden';" onserverclick="btnNew_ServerClick" > New </button></td>
            </tr>--%>
</tbody>
    </table>

         
        <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnSave" onclick="this.style.visibility='hidden';" onserverclick="btnSave_ServerClick"> Save </button>
        <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnNew" onclick="this.style.visibility='hidden';" onserverclick="btnNew_ServerClick" > New </button>
          
        </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

   


</asp:Content>
