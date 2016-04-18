<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="StockOutDetails.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockOutDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        
.warning{
            
    background: url(../Images/Button-Warning-icon.png) no-repeat;
            
    background-size:6% 80%;
    padding-left:1%;
    text-indent: 11%;
    border:1px solid #ccc;
           
}

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/Dynamicgrid.js"></script>


     <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
   
          <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>

    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });

            $('.alert_close').click(function () {
                
                $(this).parent(".alert").hide();
            });

            //$('[data-toggle="tooltip"]').tooltip();



            $('.nav_menu').click(function () {
                
                $(".main_body").toggleClass("active_close");
            });

            GetClientIDOfRemovedID('<%=hdnRemovedIDs.ClientID%>');
            RefillTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
        });

        function ClearControls()
        {
            var i = 1;
            $('.input').each(function () {
                i++;
            });
            var NumberOfColumns = i - 1;
            var NumberOfRows = NumberOfColumns / 5;


            debugger;
            document.getElementById('<%=txtIssueNO.ClientID%>').value = '';
            document.getElementById('<%=txtDate.ClientID%>').value = '';
            document.getElementById('<%=txtIssuedTo.ClientID%>').value = '';

            for (var k = 0; k < NumberOfRows; k++)
            {
                document.getElementById('txtMedicine' + k).value = '';
                document.getElementById('txtUnit' + k).value = '';
                document.getElementById('txtCode' + k).value = '';
                document.getElementById('txtCategory' + k).value = '';
                document.getElementById('txtQuantity' + k).value = '';
            }
        }



    </script>
    
    <script src="../js/jquery-1.12.0.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    
                        
        <script>
           

            function autocompleteonfocus(controlID)
            {
                //---------* Medicine auto fill, it also filters the medicine that has been already saved  *----------//

                //debugger;
                var topcount =document.getElementById('<%=hdnRowCount.ClientID%>').value;
 
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
                    var i=1;
                    while(i<=topcount)
                    {
                        if (i==1)
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

      
       
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock" class="active"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Trithvam Ayurveda</p>
         </div>
         
         
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">nav</a>
         Stock Out Details...</div>   
         <div class="icon_box">
         <a class="add_medicine" data-toggle="modal" data-target="#add_medicine" ><span title="All Medicine" data-toggle="tooltip" data-placement="left"><img src="../images/add_medicine.png"/></span></a>
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
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div> 
         <ul class="top_right_links"><li><a class="back" href="StockOut.aspx"><span></span>Back</a></li><li><a class="save" id="btnSave" runat="server" onserverclick="btnSave_ServerClick"  ><span></span>Save</a></li><li><a class="new" href="#"  onclick="ClearControls();"><span></span>New</a></li></ul>
         </div>
         
         <div class="tab_table">         
         
         <table class="details_table" width="100%" border="0">
          <tr>
            <td>Issue No</td>
              <td>
                <asp:TextBox ID="txtIssueNO" runat="server" required ></asp:TextBox></td>
            <td>Date</td>
               <td>
                <asp:TextBox ID="txtDate" runat="server" required></asp:TextBox></td>
          </tr>
          <tr>
            <td >Issued To</td>
              <td>
                <asp:TextBox ID="txtIssuedTo" runat="server" EnableViewState="false" required></asp:TextBox>

            </td>
          </tr>  
        </table>
      <div class="prescription_grid">
                                    <table class="table" style="width: 100%; border: 0;">
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
                                                    <input id="txtMedicine0" type="text" placeholder="Medicine0" class="input" onblur="BindControlsByMedicneName('0')" onfocus="autocompleteonfocus('0')" /></td>
                                                <td>
                                                    <input id="txtUnit0" class="input" type="text" placeholder="Unit0" /></td>
                                                <td>
                                                    <input id="txtCode0" type="text" placeholder="Code0" class="input" /></td>
                                                <td>
                                                    <input id="txtCategory0" type="text" placeholder="Category0" class="input" /></td>
                                                <td>
                                                    <input id="txtQuantity0" type="text" placeholder="Quantity0" class="input" onblur="CheckMedicineIsOutOfStock('0')" onfocus="RemoveWarning('0')"/></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" value="-" class="bt1" style="width: 20px;" /></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" id="btAdd" onclick="clickStockAdd(); this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" />
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
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">All Medicine</h4>
      </div>
      <div class="modal-body">
        <table class="table" width="100%" border="0">
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
        </table>
      </div>      
    </div>

  </div>
</div>         
         
     
</asp:Content>
