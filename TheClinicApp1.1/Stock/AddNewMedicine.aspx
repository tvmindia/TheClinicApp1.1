<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddNewMedicine.aspx.cs" Inherits="TheClinicApp1._1.Stock.AddNewMedicine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/Masterw3.css" rel="stylesheet" />

   
    <style>


    table     td, th,tr {
        
   border:0 none;
}

        .Dropdown {
            display: block;
            padding: 5px;
            width: 100%;
            border: 1px solid #dbdbdb;
            height: 41px;
            font-family: 'roboto-light';
            font-weight: bold;
            font-size: 14px;
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

  $('.alert_close').click(function () {
                
                $(this).parent(".alert").hide();
            });

            //$('[data-toggle="tooltip"]').tooltip();



            $('.nav_menu').click(function () {
                
                $(".main_body").toggleClass("active_close");
            });




                });



        //---------------* Function to check medicine name duplication *-----------------//

        function CheckMedicineNameDuplication(txtmedicineName) {
            
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


        function HideErrorMsg()
        {
            debugger;


            
            var Errorbox = document.getElementById('<%=Errorbox.ClientID %>');
            var errorCaption = document.getElementById('<%=lblErrorCaption.ClientID %>');
            var errorMsg = document.getElementById('<%=lblMsgges.ClientID %>');

            if (Errorbox != null) {
                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
            }


            if(errorCaption != null)
            {
                errorCaption.innerText = '';
                
                document.getElementById('<%=lblErrorCaption.ClientID %>').style.display = "none";
            }

            if (errorMsg != null)
            {
                errorMsg.innerText = '';
               

                document.getElementById('<%=lblMsgges.ClientID %>').style.display = "none"; 
            }
        }


        function RemoveRequiredAttribute()
        {
            document.getElementById('<%=txtmedicineName.ClientID %>').required = false;
            document.getElementById('<%=txtCode.ClientID %>').required = false;
            document.getElementById('<%=txtUnit.ClientID %>').required = false;
            document.getElementById('<%=txtOrderQuantity.ClientID %>').required = false;
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
        
     

        <table  style="width: 100%;border-collapse:separate; 
border-spacing:0.5em;"> 
        

                <tr>
                    <td>Medicine Name

                        

                    </td>
                    <td >
                        <asp:TextBox ID="txtmedicineName" runat="server" onchange="CheckMedicineNameDuplication(this)" required></asp:TextBox>

                    </td>

                    <td style="width:10%;border:none;">

 <asp:Image ID="imgWebLnames" runat="server" ToolTip="Login Name is Available" ImageUrl="~/Images/Check.png" Width="90%" Height="18%" />

                    <asp:Image ID="errorLnames" runat="server" ToolTip="Login Name is Unavailable" ImageUrl="~/Images/newClose.png" />

                    </td>
                   


                </tr>

                <tr>
                    <td >Medicine Code 

                    </td>
                    <td >
                        <asp:TextBox ID="txtCode" runat="server" required></asp:TextBox>

                    </td>
                    <td ></td>
                </tr>

                <tr>
                    <td >Category</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCategory" CssClass="Dropdown" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                                <%--  <asp:RequiredFieldValidator
             ID="RequiredFieldValidator1"
             runat="server"
             ControlToValidate="ddlCategory"
             InitialValue="--Select--"
             ErrorMessage="* Please select an item."
             ForeColor="Red"
             Font-Names="Impact"
             >
        </asp:RequiredFieldValidator>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                    <td ></td>

                </tr>

                <tr>
                    <td >Unit</td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" required></asp:TextBox>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                            ControlToValidate="txtUnit"
                            ValidationExpression="^[a-zA-Z ]+$"
                            Display="Static"
                            EnableClientScript="true"
                            ForeColor="Red"
                            MinimumValue="1"
                            Type="text"
                            Text="Please enter a valid unit!"
                            runat="server" />
                    </td>
                   <td></td>
                </tr>

                <tr>
                    <td>Reorder Quantity</td>
                    <td>
                        <asp:TextBox ID="txtOrderQuantity" runat="server" required TextMode="Number" Text="1"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                            ControlToValidate="txtOrderQuantity"
                            ValidationExpression="[1-9]\d{0,5}" 
                            Display="Static"
                            EnableClientScript="true"
                            ForeColor="Red"
                            MinimumValue="1"
                            Type="Integer"
                            Text="Please enter a quantity greater than 0!"
                            runat="server" />

                    </td>
                    <td></td>
                </tr>

                <%--<tr>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnSave" onclick="this.style.visibility='hidden';" onserverclick="btnSave_ServerClick"> Save </button></td>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnNew" onclick="this.style.visibility='hidden';" onserverclick="btnNew_ServerClick" > New </button></td>
            </tr>--%>
           
        </table>

    

         
          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="w3-btn w3-section w3-teal w3-ripple" Width="45%" OnClick="btnSave_Click" ValidationGroup="Required" />
        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="w3-btn w3-section w3-teal w3-ripple" Width="45%" OnClick="btnNew_Click" ValidationGroup="Required" OnClientClick="RemoveRequiredAttribute();"  />
        </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

   


</asp:Content>
