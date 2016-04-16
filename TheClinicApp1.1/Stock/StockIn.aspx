﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="StockIn.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockIn" %>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
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

   <script type="text/javascript">


       $(function () {

           GetMedicines(1);
       });
       $("[id*=txtSearch]").live("keyup", function () {

           GetMedicines(parseInt(1));
       });
       $(".Pager .page").live("click", function () {
           GetMedicines(parseInt($(this).attr('page')));
       });
       function SearchTerm() {
           return jQuery.trim($("[id*=txtSearch]").val());
       };
       function GetMedicines(pageIndex) {

           $.ajax({

               type: "POST",
               url: "../Stock/StockIn.aspx/GetMedicines",
               data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + '}',
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: OnSuccess,
               failure: function (response) {

                   alert(response.d);
               },
               error: function (response) {

                   alert(response.d);
               }
           });
       }
       var row;
       function OnSuccess(response) {

           var xmlDoc = $.parseXML(response.d);
           var xml = $(xmlDoc);
           var Medicines = xml.find("Medicines");
           if (row == null) {
               row = $("[id*=GridViewStockin] tr:last-child").clone(true);
           }
           $("[id*=GridViewStockin] tr").not($("[id*=GridViewStockin] tr:first-child")).remove();
           if (Medicines.length > 0) {
               $.each(Medicines, function () {
                   var medicine = $(this);


                   //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');

                   $("td", row).eq(0).html($(this).find("RefNo1").text());
                   $("td", row).eq(1).html($(this).find("RefNo2").text());
                   $("td", row).eq(2).html($(this).find("Date").text());
                 

                   $("[id*=GridViewStockin]").append(row);
                   row = $("[id*=GridViewStockin] tr:last-child").clone(true);
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
                   debugger;
                   var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                   $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
               });
           } else {
               var empty_row = row.clone(true);
               $("td:first-child", empty_row).attr("colspan", $("td", row).length);
               $("td:first-child", empty_row).attr("align", "center");
               $("td:first-child", empty_row).html("No records found for the search criteria.");
               $("td", empty_row).not($("td:first-child", empty_row)).remove();
               $("[id*=GridViewStockin]").append(empty_row);
           }
       };



        </script>

    

    <!-- #main-container -->
         
            


         <div class="main_body">
        
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
         <a class="nav_menu">Menu</a>
         Stock In</div>         
         <div class="icon_box">
         <a class="add_medicine" data-toggle="modal" data-target="#add_medicine" ><span title="All Medicine" data-toggle="tooltip" data-placement="left"><img src="../images/add_medicine.png"/></span></a>
         </div>
         <div class="right_form tab_right_form">
         
         <div class="page_tab">
      <!-- Nav tabs -->
      <ul class="nav nav-tabs" role="tablist">
        <li role="presentation"><a href="Stock.aspx">Stock</a></li>
        <li role="presentation" class="active"><a href="StockIn.aspx">Stock In</a></li>
        <li role="presentation"><a href="StockOut.aspx">Stock Out</a></li>
      </ul>    
      <!-- Tab panes -->
      <div class="tab-content">
        
        <div role="tabpanel" class="tab-pane active" id="stock_in">
        <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." id="txtSearch">
         <input class="button" type="submit" value="Search">
         </div> 
         <ul class="top_right_links"><li><a class="save" id="btSave" runat="server" style="visibility:hidden" onserverclick="btSave_ServerClick" href="#"><span></span>Save</a></li><li><a class="new"  onserverclick="btNew_ServerClick" href="StockInDetails.aspx"><span></span>New</a></li></ul>
         </div>
         
         <div class="tab_table">         
         
         <table class="table" style="width:100%;border:0;">
        
                <tr>
   <asp:GridView ID="GridViewStockin" runat="server" Width="100%" AutoGenerateColumns="False" class="table">
                    <Columns>
                        <%--<asp:BoundField HeaderText="Receipt ID" DataField="ReceiptID" />--%>
                        <asp:BoundField DataField="RefNo1" HeaderText="Bill Number" />
                        <asp:BoundField DataField="RefNo2" HeaderText="Reference Number" />
                        <asp:BoundField HeaderText="Date"  DataFormatString="{0:dd/MM/yyyy}"   DataField="Date" />
                        <%--<asp:HyperLinkField  Text="Details" HeaderText="Click Here" DataNavigateUrlFields="ReceiptID" DataNavigateUrlFormatString="~/Stock/StockInDetails.aspx?ReceiptID={0}" />--%>
                                               
                    </Columns>
                </asp:GridView>
                </tr>   
        </table>
           <div class="Pager">


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
