﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="OutOfStock.aspx.cs" Inherits="TheClinicApp1._1.Stock.OutOfStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    .search_div input.field {
    padding: 2px 2px 2px 20px;
    height: 34px;
    line-height: 34px;
    background: #D7E2EA;
    border: burlywood;
    display: block;
    color: #000;
    font-size: 14px;
    font-family: 'caviardreams-regular';
    width: 100%;
    -webkit-border-radius: 20px;
    -moz-border-radius: 20px;
    border-radius: 20px;
}
    </style>
    <script src="../js/jquery-1.12.0.min.js"></script>

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
                url: "../Stock/OutOfStock.aspx/ViewAndFilterOutOfStock",
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
            var Medicines = xml.find("OutOfStock");
            if (row == null) {
                row = $("[id*=gvOutOfStock1] tr:last-child").clone(true);
            }
            $("[id*=gvOutOfStock1] tr").not($("[id*=gvOutOfStock1] tr:first-child")).remove();
            if (Medicines.length > 0) {
                $.each(Medicines, function () {
                    var medicine = $(this);


                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');

                    $("td", row).eq(0).html($(this).find("MedicineName").text());
                    $("td", row).eq(1).html($(this).find("CategoryName").text());
                    $("td", row).eq(2).html($(this).find("Unit").text());
                    $("td", row).eq(3).html($(this).find("Qty").text());
                    $("td", row).eq(4).html($(this).find("ReOrderQty").text());


                    $("[id*=gvOutOfStock1]").append(row);
                    row = $("[id*=gvOutOfStock1] tr:last-child").clone(true);
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
                $("[id*=gvOutOfStock1]").append(empty_row);
            }
        };
        
    </script>

    <div class="col-lg-12" style="height: 480px;">

         <div class="col-lg-12" style="height: 40px">

   
                                <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" disabled />
                                </div>
                               
                          
          </div>

          <div class="col-sm-12" style="height: 400px;">

              <asp:GridView ID="gvOutOfStock1" runat="server" AutoGenerateColumns="False" Style="width: 100%; border-color: #dbdbdb" class="table">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <Columns>
            <%--  <asp:BoundField DataField="MedicineID" HeaderText="MedicineID" />--%>
            <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" ItemStyle-CssClass="Match"/>

            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="Match" />
            <asp:BoundField DataField="Unit" HeaderText="Unit"  ItemStyle-CssClass="Match"/>
            <asp:BoundField DataField="Qty" HeaderText="Existing Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Match" />
            <asp:BoundField DataField="ReOrderQty" HeaderText="ReOrder Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Match" />

        </Columns>

    </asp:GridView>

           </div>

         <div class="Pager"></div>
         </div>


</asp:Content>
