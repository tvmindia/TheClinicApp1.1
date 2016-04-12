<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="TheClinicApp1._1.Stock.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <style type="text/css">
           .highlight {
            background-color: #FFFFAF;
        }


          </style>


     <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

   <script src="../js/vendor/jquery-1.11.1.min.js"></script>

    <script src="../js/bootstrap.min.js"></script>
        
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
        
                
        <script>
            var test = jQuery.noConflict();
            test(document).ready(function () {

                test('.nav_menu').click(function () {
                    test(".main_body").toggleClass("active_close");
                });

            });
		</script>

      <%--  //------------- AUTOFILL SCRIPT ---------%>
    <script src="../js/jquery-1.8.3.min.js"></script>
    
    <script src="../js/ASPSnippets_Pager.min.js"></script>
  

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">


        $(function () {
            debugger;
            GetMedicines(1);
        });
        $("[id*=txtSearch]").live("keyup", function () {
            debugger;
            GetMedicines(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetMedicines(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        function GetMedicines(pageIndex) {
            debugger;
            $.ajax({
              
                type: "POST",
                url: "../Stock/Stock.aspx/GetMedicines",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    debugger;
                    alert(response.d);
                },
                error: function (response) {
                    debugger;
                    alert(response.d);
                }
            });
        }
        var row;
        function OnSuccess(response) {
            debugger;
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Medicines = xml.find("Medicines");
            if (row == null) {
                row = $("[id*=gvMedicines] tr:last-child").clone(true);
            }
            $("[id*=gvMedicines] tr").not($("[id*=gvMedicines] tr:first-child")).remove();
            if (Medicines.length > 0) {
                $.each(Medicines, function () {
                    var medicine = $(this);


                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');

                    $("td", row).eq(0).html($(this).find("MedicineCode").text());
                    $("td", row).eq(1).html($(this).find("MedicineName").text());
                    $("td", row).eq(2).html($(this).find("CategoryName").text());
                    $("td", row).eq(3).html($(this).find("Unit").text());
                    $("td", row).eq(4).html($(this).find("Qty").text());

                    $("[id*=gvMedicines]").append(row);
                    row = $("[id*=gvMedicines] tr:last-child").clone(true);
                });
                var pager = xml.find("Pager");
                $(".Pager").ASPSnippets_Pager({
                    ActiveCssClass: "current",
                    PagerCssClass: "pager",
                    PageIndex: parseInt(pager.find("PageIndex").text()),
                    PageSize: parseInt(pager.find("PageSize").text()),
                    RecordCount: parseInt(pager.find("RecordCount").text())
                });

                $(".Match").each(function () {
                    var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                    $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                });
            } else {
                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found for the search criteria.");
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=gvMedicines]").append(empty_row);
            }
        };
    </script>


     <div class="main_body">
          
       

          
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stocks"  class="active"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Trithvam Ayurveda</p>
         </div>
         
         
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Stock</div>
         <div class="icon_box">
         <a class="add_medicine" data-toggle="modal" data-target="#add_medicine" ><span title="All Medicine" data-toggle="tooltip" data-placement="left"><img src="../images/add_medicine.png"/></span></a>
         </div>
         <div class="right_form tab_right_form">
         
         <div class="page_tab">
      <!-- Nav tabs -->
      <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="Stock.aspx">Stock</a></li>
        <li role="presentation"><a href="StockIn.aspx">Stock In</a></li>
        <li role="presentation"><a href="StockOut.aspx">Stock Out</a></li>
      </ul>    
      <!-- Tab panes -->
      <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="stock">
        <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
         <input class="button" type="submit" value="Search" />
         </div> 
         <ul class="top_right_links" style="visibility:hidden" ><li><a class="save" href="#"><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>
         </div>
         
         <div class="tab_table">         
         

 <asp:GridView ID="gvMedicines" runat="server" Style="width: 100%" AutoGenerateColumns="False" class="table" >
            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            <Columns>
            
                <asp:BoundField DataField="MedicineCode" HeaderText="Medicine Code"  ItemStyle-CssClass="Match"  />
              <%--<asp:BoundField DataField="MedicineCode" HeaderText="Medicine Code"   ItemStyle-Font-Underline="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Blue" ItemStyle-CssClass="cursorshow Match" />--%>

                <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name"  ItemStyle-CssClass="Match"  />
                <asp:BoundField DataField="CategoryName" HeaderText="Category Name"  ItemStyle-CssClass="Match"  />
                 <asp:BoundField DataField="Unit" HeaderText="Unit"  ItemStyle-CssClass="Match"  />
                 <asp:BoundField DataField="Qty" HeaderText="Qty"  ItemStyle-CssClass="Match"  />


            </Columns>
            <EditRowStyle BackColor="#0080AA"></EditRowStyle>

            <FooterStyle BackColor="#0080AA" ForeColor="White" Font-Bold="True"></FooterStyle>

            <HeaderStyle BackColor="#3FBF7F" Font-Bold="True" ForeColor="White"></HeaderStyle>

            <PagerStyle HorizontalAlign="Center" ForeColor="White" BackColor="#2461BF"></PagerStyle>

            <RowStyle BackColor="#EFF3FB"></RowStyle>

            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
        </asp:GridView>

        <div class="Pager">


         <%--<table class="table" width="100%" border="0">
          <tr>
            <th>Code</th>
            <th>Med Name</th>
            <th>Category</th>
            <th>Qty</th>
            <th>Unit</th>
            <th>Reorder Qty</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>--%>
         
         </div>
        </div>
        
        
      </div>    
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
        
        <h4 class="modal-title">Add New Medicine</h4>
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
         
   </div>
</asp:Content>
