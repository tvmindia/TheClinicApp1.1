﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddNewMedicine.aspx.cs" Inherits="TheClinicApp1._1.Stock.AddNewMedicine" %>
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
            height: 31px;
            font-family: 'roboto-light';
            font-weight: bold;
            font-size: 14px;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
        }
  

    </style>


    <script src="../js/jquery-1.12.0.min.js"></script>
     <script src="../js/Messages.js"></script>
        <script src="../js/Dynamicgrid.js"></script>
    <script>  
        $(document).ready(function () {
         
          

                //images that represents medicine name duplication hide and show
                var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                LnameImage.style.display = "none";
                var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                errLname.style.display = "none";

                var CodeAvailableImage = document.getElementById('<%=imgCodeAvailable.ClientID %>');
                 CodeAvailableImage.style.display = "none";
                 var CodeUnavailableImage = document.getElementById('<%=imgCodeUnavailable.ClientID %>');
                 CodeUnavailableImage.style.display = "none";

                 $('input[type=text],input[type=number]').on('focus', function () {
                     debugger;
                     $(this).css({ borderColor: '#dbdbdb' });
                     $("#Errorbox").hide(1000);
                 });
                 $('select').on('focus', function () {
                     debugger;
                     $(this).css({ borderColor: '#dbdbdb' });

                 });

  $('.alert_close').click(function () {
                
                $(this).parent(".alert").hide();
            });

            //$('[data-toggle="tooltip"]').tooltip();



            $('.nav_menu').click(function () {
                
                $(".main_body").toggleClass("active_close");
            });

            if ($('#<%=hdnManageGridBind.ClientID %>').val() == "True") {
                parent.GetMedicines(1);
                $('#<%=hdnManageGridBind.ClientID %>').val('False');
            }


          


                });



        //---------------* Function to check medicine name duplication *-----------------//

        function CheckMedicineNameDuplication(txtmedicineName)
        {
            
            var name = document.getElementById('<%=txtmedicineName.ClientID %>').value;
            name = name.replace(/\s/g, '');

            if (name != "")
            {


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
        }


        function CheckMedicineCodeDuplication(txtCode) {

            var name = document.getElementById('<%=txtCode.ClientID %>').value;
            name = name.replace(/\s/g, '');

            if (name != "") {

            PageMethods.ValidateMedicineCode(name, OnSuccess, onError);

            function OnSuccess(response, userContext, methodName) {

                var CodeAvailableImage = document.getElementById('<%=imgCodeAvailable.ClientID %>');
                var CodeUnavailableImage = document.getElementById('<%=imgCodeUnavailable.ClientID %>');
                if (response == false) {

                    CodeAvailableImage.style.display = "block";
                    CodeUnavailableImage.style.display = "none";

                }
                if (response == true) {
                    CodeUnavailableImage.style.display = "block";
                    CodeUnavailableImage.style.color = "Red";
                    CodeUnavailableImage.innerHTML = "Name Alreay Exists"
                    CodeAvailableImage.style.display = "none";

                }
            }
            function onError(response, userContext, methodName) {

            }
        }
        }









        function HideErrorMsg()
        {
           


            
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


        function ClearControls()
        {
            

            var Errorbox = document.getElementById('<%=Errorbox.ClientID %>');

            if (Errorbox != null) {
                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
            }



            document.getElementById('<%=txtmedicineName.ClientID %>').value= " ";
            document.getElementById('<%=txtCode.ClientID %>').value = " ";
            document.getElementById('<%=txtOrderQuantity.ClientID %>').value = "1";
          
            document.getElementById('<%=ddlCategory.ClientID %>').value = "--Select--";
            document.getElementById('<%=ddlUnits.ClientID %>').value = "--Select--";

            document.getElementById('<%=RegularExpressionValidator1.ClientID %>').style.color = "white";
 

            var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
            LnameImage.style.display = "none";
            var errLname = document.getElementById('<%=errorLnames.ClientID %>');
            errLname.style.display = "none";

            var CodeAvailableImage = document.getElementById('<%=imgCodeAvailable.ClientID %>');
            CodeAvailableImage.style.display = "none";
            var CodeUnavailableImage = document.getElementById('<%=imgCodeUnavailable.ClientID %>');
            CodeUnavailableImage.style.display = "none";
        }

        function SetValidatorColor()
        {
          
            document.getElementById('<%=RegularExpressionValidator1.ClientID %>').style.color = "red";
        }
      
        function Validation()
        {
            debugger;
            var BoolValue = FieldsValidation();
            if(BoolValue==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
         function FieldsValidation()
         {
             debugger;
               try
                    {
                //$('#Displaydiv').remove();

                   var medicineName = $('#<%=txtmedicineName.ClientID%>');
                   var code = $('#<%=txtCode.ClientID%>');
                   var category = $('#<%=ddlCategory.ClientID%>');
                   var units = $('#<%=ddlUnits.ClientID%>');
                   var orderQuantity = $('#<%=txtOrderQuantity.ClientID%>');
                
                    var container = [
                            { id: medicineName[0].id, name: medicineName[0].name, Value: medicineName[0].value },
                            { id: code[0].id, name: code[0].name, Value: code[0].value },
                            { id: category[0].id, name: category[0].name, Value: category[0].value },
                            { id: units[0].id, name: units[0].name, Value: units[0].value },
                             { id: orderQuantity[0].id, name: orderQuantity[0].name, Value: orderQuantity[0].value },
                    ];
                
                        
                       
                        var j = 0;
       
                        for (var i = 0; i < container.length; i++) {
                            if (container[i].Value == "") {
                                j = 1;
               
                                var txtB = document.getElementById(container[i].id);
                                txtB.style.borderColor = "red";
                                txtB.style.backgroundPosition = "95% center";
                                txtB.style.backgroundRepeat = "no-repeat";
               
                            }
                            else if (container[i].Value == "--Select--") {
                                j = 1;
               
                                var txtB = document.getElementById(container[i].id);
                                txtB.style.borderColor = "red";
                                txtB.style.backgroundPosition = "93% center";
                                txtB.style.backgroundRepeat = "no-repeat";
             
                            }
                        }
                        if (j == '1') {
                           var lblclass = Alertclasses.danger;
                    var lblmsg = msg.Requiredfields;
                    var lblcaptn = Caption.Confirm;
                    ErrorMessagesDisplay('<%=lblErrorCaption.ClientID%>','<%=lblMsgges.ClientID%>','<%=Errorbox.ClientID%>' ,lblclass,lblcaptn,lblmsg);
                            return false;
                        }
                        if (j == '0') {
          
                            //saveMember();
                            return true;
                        }
                    }
                    catch(e)
                    {
                        //noty({ type: 'error', text: e.message });
                    }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   

 
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
 <div class="main_body">

          <div id="Errorbox"  style="display:none;"  ClientIDMode="Static" runat="server" ><a class="alert_close">X</a>
                    <div>
                            <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
                                 <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>

                            </div>

                            </div>                     

        

       <%-- <div class="prescription_grid" >--%>
        
     

        <table  style="width: 100%;border-collapse:separate;border-spacing:0.5em;"> 
        

                <tr>
                    <td>Medicine Name

                        

                    </td>
                    <td >
                        <asp:TextBox ID="txtmedicineName" runat="server" onchange="CheckMedicineNameDuplication(this)" required></asp:TextBox>
                    </td>

                    <td style="width:10%;border:none;">
                        <span class="tooltip1">
                        <asp:Image ID="imgWebLnames" runat="server" ImageUrl="~/Images/newfff.png" />
                             <span class="tooltiptext1">Medicine Name is Available</span>
                    </span>
                    <span class="tooltip1">                        
                        <asp:Image ID="errorLnames" runat="server"  ImageUrl="~/Images/newClose.png" />
                         <span class="tooltiptext1">Medicine Name  is Unavailable</span>
                    </span>
                        <br />
                        <br />
                      
                    </td>  
                                                      
                </tr>
            
                <tr>
                    <td >Medicine Code</td>
                    <td >
                        <asp:TextBox ID="txtCode" runat="server"   onchange="CheckMedicineCodeDuplication(this)" required></asp:TextBox>
                    </td>
                    <td >
                       <span class="tooltip1">
                        <asp:Image ID="imgCodeAvailable" runat="server"  ImageUrl="~/Images/newfff.png" />
                        <span class="tooltiptext1">Medicine Code is Available</span>
                    </span>
                    <span class="tooltip1">
                        <asp:Image ID="imgCodeUnavailable" runat="server"  ImageUrl="~/Images/newClose.png" />
                        <span class="tooltiptext1">Medicine Code  is Unavailable</span>
                    </span>
                          <br />
                        <br />
                        <br />
                    </td>
                </tr>

                <tr>
                    <td >Category</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlCategory" CssClass="Dropdown" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                     
                    </td>
                    <td >  <br />
                        </td>

                </tr>

                <tr>
                    <td >Unit</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlUnits" CssClass="Dropdown" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                                  </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:TextBox ID="txtUnit" runat="server" required></asp:TextBox>--%>

                     <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                            ControlToValidate="txtUnit"
                            ValidationExpression="^[a-zA-Z ]+$"
                            Display="Static"
                            EnableClientScript="true"
                            ForeColor="Red"
                            MinimumValue="1"
                            Type="text"
                            Text="Please enter a valid unit!"
                            runat="server" />--%>
                    </td>
                   <td>  <br />
                        <br />
                        <br /></td>
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
                            Text="* Please enter a quantity greater than 0!"
                            runat="server" />


                    </td>
                    <td></td>
                </tr>


            

                <%--<tr>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnSave" onclick="this.style.visibility='hidden';" onserverclick="btnSave_ServerClick"> Save </button></td>
                <td>  <button class="w3-btn w3-section w3-teal w3-ripple" runat="server" id="btnNew" onclick="this.style.visibility='hidden';" onserverclick="btnNew_ServerClick" > New </button></td>
            </tr>--%>
           
        </table>

    <div class="grey_sec" style="bottom:0!important;left:0!important;right:0!important;position:absolute!important;">
      <ul class="top_right_links">
                                   
                                    <li><a class="save" id="btnSave" runat="server" onserverclick="btnSave_ServerClick" onclick="return Validation();"  ><span></span>Save</a></li>
                                    <li><a class="new" id="btnNew"   runat="server" onserverclick="btnNew_ServerClick"  onclick="ClearControls();"><span></span>New</a></li>
       <%--<li><a class="new" id="btnNew" href="AddNewMedicine.aspx"><span></span>New</a></li>--%>
                                </ul>
         </div>
    <%-- <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="save" Width="45%" OnClick="btnSave_Click" ValidationGroup="Required" />
        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="new" Width="45%" OnClick="btnNew_Click" ValidationGroup="Required" OnClientClick="RemoveRequiredAttribute();"  />--%>
        </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>

   
    <asp:HiddenField ID="hdnManageGridBind" runat="server"  Value="False"/>

</asp:Content>
