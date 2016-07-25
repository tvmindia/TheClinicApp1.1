<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewTokens.aspx.cs" Inherits="TheClinicApp1._1.Token.ViewTokens" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableCdn="true"></asp:ScriptManager>

 <style>
table{
    border-collapse: collapse;    
    width: 100%;
    
    }

table td {
    text-align: left;
    height:auto;
    
    }
table td{
    width:auto;
    height:auto;
    
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
}
table td+td+td{
    width:150px;
    height:auto;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
   
}
/*.modal table tr:nth-child(even){background-color: #f2f2f2}*/

table th {
    background-color: #5681e6;
    text-align: center;
    color: white;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
}


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
   
    td:first-child
{
    width: 50px!important;
}
     .selected_row
    {
        background-color: #d3d3d3;
    }
</style>
    
    <script src="../js/DeletionConfirmation.js"></script>
   
    <asp:HiddenField ID="hdnCount" runat="server" />


     <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>

    <script>

        var UniqueID = '';

        
        //-------------------------------- * Delete Button Click * ------------------------- //

        $(function () {
            $("[id*=GridViewTokenlist] td:eq(0)").click(function () {
               
                if ($(this).text() == "") {
                    var DeletionConfirmation = ConfirmDelete();
                    if (DeletionConfirmation == true) {
                        UniqueID = $(this).closest('tr').find('td:eq(6)').text();
                        DeleteTokenByUniqueID(UniqueID);
                        //window.location = "StockIn.aspx?HdrID=" + receiptID;
                    }
                }
            });
        });

        function DeleteTokenByUniqueID(UniqueID) {

            if (UniqueID != "") {
                PageMethods.DeleteTokenByUniqueID(UniqueID, OnSuccess, onError);
                function OnSuccess(response, userContext, methodName) {
                    debugger;

                    if (response == true)
                    {
                        var PageIndx = parseInt(1);
                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {
                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }
                        GetTokenBooking(PageIndx);
                    }

                }
                function onError(response, userContext, methodName) {
                }
            }
        }

        //-------------------------------- * END : Delete Button Click * ------------------------- //




        $(function () {
            GetTokenBooking(1);
        });

        $("[id*=txtSearch]").live("keyup", function () {
            GetTokenBooking(parseInt(1));
        });

        $(".Pager .page").live("click", function () {

            GetTokenBooking(parseInt($(this).attr('page')));
        });

        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };

        function GetTokenBooking(pageIndex) {
            $.ajax({
                type: "POST",
                url: "../Token/ViewTokens.aspx/ViewAndFilterPatientBooking",
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
            $(".Pager").show();
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var PatientBooking = xml.find("PatientBooking");
          

            if (row == null) {
                row = $("[id*=GridViewTokenlist] tr:last-child").clone(true);
            }
            $("[id*=GridViewTokenlist] tr").not($("[id*=GridViewTokenlist] tr:first-child")).remove();
            if (PatientBooking.length > 0) {
                $.each(PatientBooking, function () {
                 
                    $("td", row).eq(0).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(1).html($(this).find("DOCNAME").text());
                    $("td", row).eq(2).html($(this).find("TokenNo").text());
                    $("td", row).eq(3).html($(this).find("Name").text());
                    $("td", row).eq(4).html($(this).find("Date").text());
                    //$("td", row).eq(5).html($(this).find("IsProcessed").text());
                    $("td", row).eq(6).html($(this).find("UniqueID").text());

                    if ($(this).find("IsProcessed").text()=="true") {
                        $("td", row).addClass("selected_row");
                        $("td", row).eq(5).html("Yes");

                    }
                    if ($(this).find("IsProcessed").text() == "false") {
                        $("td", row).removeClass("selected_row");
                        $("td", row).eq(5).html("No");
                    }


                    $("[id*=GridViewTokenlist]").append(row);
                    row = $("[id*=GridViewTokenlist] tr:last-child").clone(true);

                });
                var pager = xml.find("Pager");

                if ($('#txtSearch').val() == '') {
                    debugger;
                    var GridRowCount = pager.find("RecordCount").text();

                    parent.SetGridviewRowCount(GridRowCount);

                }
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
            }
            else {

                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=GridViewTokenlist]").append(empty_row);
                $(".Pager").hide();

            }


            var th = $("[id*=GridViewTokenlist] th:contains('UniqueID')");
            th.css("display", "none");
            $("[id*=GridViewTokenlist] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });


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

              <asp:GridView ID="GridViewTokenlist" runat="server" AutoGenerateColumns="False" >
    
       <Columns>
          <asp:TemplateField HeaderText="">
             <ItemTemplate>
              <asp:ImageButton ID="ImgBtnDelete" style="border:none!important" runat="server" ImageUrl="~/images/Deleteicon1.png"  OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" />
               </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField HeaderText="Doctor" DataField="DOCNAME" ItemStyle-CssClass="Match" />
                  <asp:BoundField HeaderText="Token No" DataField="TokenNo" ItemStyle-CssClass="Match" />
                  <asp:BoundField HeaderText="Patient" DataField="Name" ItemStyle-CssClass="Match" />
                   <asp:BoundField HeaderText="Time" DataField="Date" ItemStyle-CssClass="Match" />
           <asp:BoundField HeaderText="Consulted"  DataField="IsProcessed" ItemStyle-CssClass="Match" />
           <asp:BoundField HeaderText="UniqueID"  DataField="UniqueID" ItemStyle-CssClass="Match" />
                 </Columns>

       </asp:GridView>

           </div>

         <div class="Pager"></div>
         </div>



    
             
</asp:Content>
