﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="StockOutDetails.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockOutDetails"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>


    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/Messages.js"></script>
  

    <script>
        var test = jQuery.noConflict();
            
        test(document).ready(function () {

            
            var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
            LnameImage.style.display = "none";
            var errLname = document.getElementById('<%=errorLnames.ClientID %>');
        errLname.style.display = "none";


        test('.nav_menu').click(function () {
            
            test(".main_body").toggleClass("active_close");
            
        });

            
        $('.alert_close').click(function () {
                
            
            $(this).parent(".alert").hide();
            
        });           
         
        $('input[type=text],input[type=number]').on('focus', function () {
            debugger;
            $(this).css({ borderColor: '#dbdbdb' });
            $("#Errorbox").hide(1000);
        });
            
            //date picker
            
        $("[id$=txtDate1]").datepicker({
            
            dateFormat: 'dd-M-yy',
            
            buttonImageOnly: true,
            
          
            
        });           


            
        GetClientIDOfRemovedID('<%=hdnRemovedIDs.ClientID%>','<%=hdnRowCount.ClientID%>');
                RefillTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
        });


        
        function SetIframeSrc(HyperlinkID) {

            if (HyperlinkID == "NewMedicineIframe") {
                var NewMedicineIframe = document.getElementById('NewMedicineIframe');
                NewMedicineIframe.src = "AddNewMedicine.aspx";
                //$('#OutOfStock').modal('show');
            }

        }





        function ClearControls()
        {
            var i = 1;
            $('.input').each(function () {
                i++;
            });
            var NumberOfColumns = i - 1;
            var NumberOfRows = NumberOfColumns / 5;


       
            document.getElementById('<%=txtIssueNO.ClientID%>').value = '';
        document.getElementById('<%=txtDate1.ClientID%>').value = '';
        document.getElementById('<%=txtIssuedTo.ClientID%>').value = '';

        for (var k = 0; k < NumberOfRows; k++)
        {
            document.getElementById('txtMedicine' + k).value = '';
            document.getElementById('txtUnit' + k).value = '';
            document.getElementById('txtCode' + k).value = '';
            document.getElementById('txtCategory' + k).value = '';
            document.getElementById('txtQuantity' + k).value = '';
        }

        PageMethods.GenerateIssueNo(OnSuccess, onError);

        function OnSuccess(response, userContext, methodName)
        {

            document.getElementById('<%=txtIssueNO.ClientID%>').value = response;
              
            }
            function onError(response, userContext, methodName) {

            }



            }

            function CheckIssueNoDuplication(IssueNo) {

                //---------------* Function to check Issue Number duplication *-----------------// 
            
                var IssueNo = document.getElementById('<%=txtIssueNO.ClientID %>').value;

                IssueNo = IssueNo.trim();

                if (IssueNo != "") {

                    IssueNo = IssueNo.replace(/\s/g, '');

                    PageMethods.CheckIssueNoDuplication(IssueNo, OnSuccess, onError);

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
                else{
                    if (IssueNo == "") {
                        var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                        LnameImage.style.display = "none";
                        var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                        errLname.style.display = "none";
                    }
                }



}
       
      

    </script>

    <script src="../js/jquery-1.12.0.min.js"></script>
    <%--<link href="../css/bootstrap.min.css" rel="stylesheet" />--%>
    <script src="../js/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />


    <script>
        function TextFieldsValidation()
        {
            var i=0;
            var j=0;
            debugger;
            $(".tblMedicine tbody tr td input:text,input[type=number]").each(function() {
                debugger;
                var txtBox= this.id;
             
                if(txtBox.includes("txtQuantity"))
                {
                    if(this.value=="")
                    {
                        this.style.borderColor = "red";
                        this.style.backgroundPosition = "95% center";
                        this.style.backgroundRepeat = "no-repeat";
                        j=1;
                    }
                    if($("#txtMedicine"+i).val()=="")
                    {
                        $("#txtMedicine"+i).css("borderColor","red");
                        $("#txtMedicine"+i).css("backgroundPosition","95% center");
                        $("#txtMedicine"+i).css("backgroundRepeat","no-repeat");
                        j=1;
                    }
                    i=i+1;
                }
           
            });

            if(j==1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
           function FieldsValidation()
            {
                    try
                    {
                        var issueNo = $('#<%=txtIssueNO.ClientID%>');
                        var issuedTo = $('#<%=txtIssuedTo.ClientID%>');
                         var date = $('#<%=txtDate1.ClientID%>');
                        
                        var container = [
                              { id: issueNo[0].id, name: issueNo[0].name, Value: issueNo[0].value },
                              { id: issuedTo[0].id, name: issuedTo[0].name, Value: issuedTo[0].value },
                               { id: date[0].id, name: date[0].name, Value: date[0].value },
                               
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
                            else if (container[i].Value == "-1") {
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
                            TextFieldsValidation();
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
        
        
        function Validation() {

            debugger;
            var fieldBool = FieldsValidation();
            if(fieldBool==false)
            {
                return false;
            }
            else {

                var medicineFieldsBool=TextFieldsValidation();
                if(medicineFieldsBool==false)
                {
                    return false;
                }
                else
                {
                     GetTextBoxValues('<%=hdnTextboxValues.ClientID %>','<%=hdnRemovedIDs.ClientID %>');
                     return true;
                }
               
            }

        }



            
        function autocompleteonfocus(controlID)
            
        {
            
            //---------* Medicine auto fill, it also filters the medicine that has been already saved  *----------//

            
            //
            
            var topcount =Number(document.getElementById('<%=hdnRowCount.ClientID%>').value)+Number(1);
 
            if (topcount==0)
            {
                var ac=null; 
                ac = <%=listFilter %>;
            $( "#txtMedicine"+controlID).autocomplete({
                source: ac
            });
        }
        else
        {
            var ac=null;
            ac = <%=listFilter %>;
                    var i=0;
                    while(i<topcount)
                    {
                        if (i==0)
                        {
                            var item=  document.getElementById('txtMedicine'+i).value 
                                 
                            var result = ac.filter(function(elem){
                                return elem != item; 
                            });

                        }
                        else
                        {
                            if (document.getElementById('txtMedicine'+i) != null)

                            {
                                var item=  document.getElementById('txtMedicine'+i).value 
                                                           

                                result = result.filter(function(elem){
                                    return elem != item; 
                                }); 
                            }
                        }
                        i++;
                    }
            
                            
                    $( "#txtMedicine"+controlID).autocomplete({
                        source: result
                    });

                }

            } 

    </script>

       <style>
        
.button1{
        background: url("../images/save.png") no-repeat 0 center;
        height: 33px;
        width: 60px;
        display: inline-block;
        vertical-align: top;
        padding: 8px 10px 7px;
        text-transform: uppercase;
        font-size: 14px;
        line-height: 18px;
        text-align: center;
        font-family:'raleway-semibold';
        min-width: 83px;
        background-color:#abd357 ;
        -webkit-border-radius: 2px;
        -moz-border-radius: 2px;
        border-radius: 2px;
        text-indent: 20px;
        background-position-x:5px;

        color: inherit;

    }

    </style>



    <div class="main_body">



        <div class="left_part">
            <div class="logo">
                <a href="#">
                   <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
            </div>
          <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
              <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock" class="active"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
              <li id="admin" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
              <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
              <li id="master" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
              <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">nav</a>
                Stock Out Details...<ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"  ></asp:Label></li><li>
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li></ul>        
            
            </div>
            <div class="icon_box">
            <a class="add_medicine" data-toggle="modal" data-target="#add_medicine"  onclick="SetIframeSrc('NewMedicineIframe')">
                       <span class="tooltip1">                    
                    <img src="../images/add_medicinedemo.png" />   
                         <span class="tooltiptext1">Add New Medicine</span>                   
                       </span>
                </a>
            </div>
            <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Stock.aspx">Stock</a></li>
                        <li role="presentation"><a href="StockIn.aspx">Stock In</a></li>
                        <li role="presentation" class="active"><a href="StockOut.aspx">Stock Out</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="stock_in">
                            <div class="grey_sec">

                                <%--<div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>

                                <ul class="top_right_links">
                                    <li><a class="back" href="StockOut.aspx"><span></span>Back</a></li>
                                    <li>
                                        <%--<a class="save" id="btnSave" runat="server" href="#" onserverclick="btnSave_ServerClick" onclick="return Validation();"><span></span>Save</a>--%>
                                          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_ServerClick" OnClientClick="return Validation();"/>
                                    </li>
                                    <li><a class="new" href="StockOutDetails.aspx"><span></span>New</a></li>
                                </ul>
                            </div>



                            <div id="Errorbox" style="display: none;" runat="server" ClientIDMode="Static">
                                <a class="alert_close">X</a>
                                <div>
                                    <strong>
                                        <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label>
                                    </strong>
                                    <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>

                                </div>

                            </div>

                            <div class="alert alert-success" style="display: none">
                                <strong>Success!</strong> Indicates a successful or positive action.<a class="alert_close">X</a>
                            </div>
                            <div class="alert alert-info" style="display: none">
                                <strong>Info!</strong> Indicates a neutral informative change or action.<a class="alert_close">X</a>
                            </div>

                            <div class="alert alert-warning" style="display: none">
                                <strong>Warning!</strong> Indicates a warning that might need attention.<a class="alert_close">X</a>
                            </div>

                            <div class="alert alert-danger" style="display: none">
                                <strong>Danger!</strong> Indicates a dangerous or potentially negative action.<a class="alert_close">X</a>
                            </div>




                            <div class="tab_table">

                                <table class="details_table" width="100%" border="0">
                                    <tr>
                                        <td>Issue No</td>
                                        <td>
                                            <asp:TextBox ID="txtIssueNO" Width="80%" runat="server"  onchange="CheckIssueNoDuplication(this)"></asp:TextBox></td>

                                        <td >
                                            <span class="tooltip2">
                                            <asp:Image ID="imgWebLnames" runat="server" ImageUrl="~/Images/newfff.png"  />
                                                 <span class="tooltiptext2">Issue No  is Available</span>
                                            </span>
                                            <span class="tooltip2">
                                            <asp:Image ID="errorLnames" runat="server"  ImageUrl="~/Images/newClose.png" />
                                                <span class="tooltiptext2">Issue No  is Unavailable</span>
                                            </span>
                                        </td>        
                                        
                                        <td>Date</td>
                                        <td>
                                            <asp:TextBox ID="txtDate1" CssClass="txtDate1Class" Width="80%" runat="server" ></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Issued To</td>
                                        <td>
                                            <asp:TextBox ID="txtIssuedTo" Width="80%" runat="server" EnableViewState="false" ></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <div class="">
                                    <table  style="width: 100%; border: 0;" class="tblMedicine">
                                        <tbody>
                                            <tr>
                                                <th>Medicine</th>
                                                <th>Unit</th>
                                                <th>Code</th>
                                                <th>Category</th>
                                                <th>Quantity</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input id="txtMedicine0" type="text"  class="input" onblur="BindControlsByMedicneName('0')" onfocus="autocompleteonfocus('0')" /></td>
                                                <td>
                                                    <input id="txtUnit0" class="input" readonly="true" type="text"  /></td>
                                                <td>
                                                    <input id="txtCode0" type="text" readonly="true" class="input" /></td>
                                                <td>
                                                    <input id="txtCategory0" type="text" readonly="true"  class="input" /></td>
                                                <td>
                                                    <input id="txtQuantity0" class="input" onkeypress="return isNumber(event)" min="1" type="number" onblur="CheckMedicineIsOutOfStock('0')" onfocus="RemoveWarning('0')" /></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" value="-" class="bt1" style="width: 20px;" onclick="ClearAndRemove()" accesskey="-" /></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" id="btAdd" onclick="clickStockAdd(); this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" accesskey="+" />
                                                </td>
                                                <td style="background-color: transparent">
                                                    <%--<input id="hdnDetailID' + iCnt + '" type="hidden" />
                                                    <input id="hdnQty' + iCnt + '" type="hidden" />--%>
                                                    <input id="hdnDetailID0" type="hidden" />
                                                    <input id="hdnQty0" type="hidden" />

                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>

                                    <div id="maindiv">
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <asp:HiddenField ID="hdnXmlData" runat="server" />
                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnTextboxValues" runat="server" />
                    <asp:HiddenField ID="hdnManageGridBind" runat="server" Value="False" />

                    <asp:HiddenField ID="hdnHdrInserted" runat="server" />
                    <asp:HiddenField ID="hdnRemovedIDs" runat="server" />

                </div>

            </div>







        </div>
    </div>






    <!-- Modal -->
    <div id="add_medicine" class="modal fade" role="dialog">
        <div class="modal-dialog" style="height:600px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header"  style="border-color:royalblue">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                    <h4 class="modal-title">Add New Medicine</h4>
                </div>
                <div class="modal-body" style="height:500px;">

                     <iframe id="NewMedicineIframe" style ="width: 100%; height: 100%" frameBorder="0" ></iframe>


                    <%--<table class="table" width="100%" border="0">
                        <tr>
                            <th>Sl No.</th>
                            <th>Date</th>
                            <th>Remarks</th>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>--%>

                </div>
            </div>

        </div>
    </div>


</asp:Content>
