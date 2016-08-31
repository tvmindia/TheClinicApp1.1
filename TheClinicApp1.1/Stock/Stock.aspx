<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="TheClinicApp1._1.Stock.Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />

    <script>
       
        function SetIframeSrc(HyperlinkID) {
            if (HyperlinkID == "NewMedicineIframe") {
                var NewMedicineIframe = document.getElementById('NewMedicineIframe');
                NewMedicineIframe.src = "AddNewMedicine.aspx";
                //$('#OutOfStock').modal('show');
            }
            else if (HyperlinkID == "OutOfStockIframe") {
                var OutOfStockIframe = document.getElementById('OutOfStockIframe');
                OutOfStockIframe.src = "OutOfStock.aspx";
                //$('#OutOfStock').modal('show');
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });
            $('[data-toggle="tooltip"]').tooltip();
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
                url: "../Stock/Stock.aspx/ViewAndFilterMedicine",
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
                    $("td", row).eq(5).html($(this).find("ReOrderQty").text());

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
            }
            else {

                debugger;
                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=gvMedicines]").append(empty_row);
                $(".Pager").hide();

            }
         
        };



    </script>






    <div class="main_body">


        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stocks" class="active"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Stock<ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>
            </div>

            <div class="icon_box">          
                 
                <a class="all_registration_link" data-toggle="modal" data-target="#add_medicine" onclick="SetIframeSrc('NewMedicineIframe')">
                       <span class="tooltip1">                    
                    <img src="../images/add_medicinedemo.png" />
                           <span class="tooltiptext1">Add New Medicine</span>
                       </span>
                </a>
                <a class="OutOfStock_link" data-toggle="modal" data-target="#View_OutOfStock">
                        <span class="tooltip1">
                    <span class="count"><asp:Label ID="lblReOrderCount" runat="server" Text="0"></asp:Label>
                    </span>
                    <span  onclick="SetIframeSrc('OutOfStockIframe')">
                    <img src="../images/edited.png" />
                           <span class="tooltiptext1"> Reorder Alert</span>
                    </span></span></a>
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
                                    <input class="button" type="submit" value="Search" disabled/>
                                </div>
                                <ul class="top_right_links" style="visibility: hidden">
                                    <li><a class="save" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">
                                <div id="StockTableDiv" style="height:335px">
                                <asp:GridView ID="gvMedicines" runat="server" Style="width: 100%" AutoGenerateColumns="False" class="table">
                                    <Columns>
                                        <asp:BoundField DataField="MedicineCode" HeaderText="Medicine Code" ItemStyle-CssClass="Match" />
                                        <%--<asp:BoundField DataField="MedicineCode" HeaderText="Medicine Code"   ItemStyle-Font-Underline="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Blue" ItemStyle-CssClass="cursorshow Match" />--%>
                                        <asp:BoundField DataField="MedicineName" HeaderText="Medicine Name" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="Qty" HeaderText="Existing Qty" ItemStyle-CssClass="Match" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ReOrderQty" HeaderText="ReOrder Quantity" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="Match" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                                <div class="Pager">
                                  <%-- Paging implemented here --%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <!-- Modal -->
        <div id="add_medicine" class="modal fade" role="dialog">
            <div class="modal-dialog" style="height: 600px;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: royalblue">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add New Medicine</h4>
                    </div>

                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <div class="col-lg-12" style="height: 480px;">
                            <iframe id="NewMedicineIframe" style="width: 100%; height: 100%" frameborder="0"></iframe>
                        </div>
                    </div>

                    
                </div>

            </div>
        </div>

        <div id="View_OutOfStock" class="modal fade" role="dialog">
            <div class="modal-dialog" style="height: 600px;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                        <h3 class="modal-title">Reorder Alert</h3>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                          <div class="col-lg-12" style="height: 480px;">

                        <iframe id="OutOfStockIframe" style="width: 100%; height: 100%" frameborder="0"></iframe>

                              </div>
                    </div>
                </div>

            </div>
        </div>





    </div>
</asp:Content>
