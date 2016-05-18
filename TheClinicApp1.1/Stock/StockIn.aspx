<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="StockIn.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/TheClinicApp.css" rel="stylesheet" />





    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>

    <script src="../js/DeletionConfirmation.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>

        var receiptID = '';

        var test = jQuery.noConflict();
        test(document).ready(function () {

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });



            $('.alert_close').click(function () {


                $(this).parent(".alert").hide();

            });


        });


        $(function () {
            $("[id*=GridViewStockin] td:first").click(function () {

                //var rowCount = $("[id*=GridViewStockin] td").closest("tr").length;
                if ($(this).text() == "") {



                    var DeletionConfirmation = ConfirmDelete();

                    if (DeletionConfirmation == true) {
                        receiptID = $(this).closest('tr').find('td:eq(5)').text();


                        window.location = "StockIn.aspx?HdrID=" + receiptID;
                    }
                }
            });
        });




        function SetIframeSrc(HyperlinkID) {

            if (HyperlinkID == "NewMedicineIframe") {
                var NewMedicineIframe = document.getElementById('NewMedicineIframe');
                NewMedicineIframe.src = "AddNewMedicine.aspx";
                //$('#OutOfStock').modal('show');
            }

        }









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

                    //if ($(this).find("ReceiptID").text().trim() != "") {

                        $("td", row).eq(0).html($('<img />')
                           .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');






                        $("td", row).eq(1).html($(this).find("RefNo1").text());
                        $("td", row).eq(2).html($(this).find("RefNo2").text());
                        $("td", row).eq(3).html($(this).find("Date").text());


                        $("td", row).eq(4).html('Details').click(function () {

                            receiptID = $(this).closest('tr').find('td:eq(5)').text();
                            window.location = "StockInDetails.aspx?receiptID=" + receiptID;
                        }).addClass('CursorShow');

                        $("td", row).eq(5).html($(this).find("ReceiptID").text());



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

                        var searchPattern = new RegExp('(' + SearchTerm() + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTerm() + "</span>"));
                    });

                
            } else {
                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found for the search criteria.").removeClass('CursorShow');
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=GridViewStockin]").append(empty_row);
            }

            var th = $("[id*=GridViewStockin] th:contains('ReceiptID')");
            th.css("display", "none");
            $("[id*=GridViewStockin] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };



    </script>



    <!-- #main-container -->




    <div class="main_body">

        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock" class="active"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="master" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Stock In<ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"  ></asp:Label></li><li>
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li></ul>        
            
            </div>
            <div class="icon_box">
                <a class="add_medicine" data-toggle="modal" data-target="#add_medicine"><span title="Add New Medicine" data-toggle="tooltip" data-placement="left" onclick="SetIframeSrc('NewMedicineIframe')">
                    <img src="../images/add_medicinedemo.png" /></span></a>
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
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>
                                <ul class="top_right_links">
                                    <li><a class="save" id="btSave" runat="server" style="visibility: hidden" onserverclick="btSave_ServerClick" href="#"><span></span>Save</a></li>
                                    <li><a class="new" onserverclick="btNew_ServerClick" href="StockInDetails.aspx"><span></span>New</a></li>
                                </ul>
                            </div>



                            <div id="Errorbox" style="display: none;" runat="server">
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

                                <table class="table" style="width: 100%; border: 0;">

                                    <tr>
                                        <asp:GridView ID="GridViewStockin" runat="server" Width="100%" AutoGenerateColumns="False" class="table">
                                            <Columns>
                                                <%--<asp:BoundField HeaderText="Receipt ID" DataField="ReceiptID" />--%>

                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Image ID="img1" runat="server" 
                                                            />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="RefNo1" HeaderText="Ref No" ItemStyle-CssClass="Match" />
                                                <asp:BoundField DataField="RefNo2" HeaderText="Additional Ref No" ItemStyle-CssClass="Match" />
                                                <asp:BoundField HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" DataField="Date" ItemStyle-CssClass="Match" />

                                                <asp:BoundField HeaderText="Details" ItemStyle-CssClass="Match" />
                                                <asp:BoundField DataField="ReceiptID" HeaderText="ReceiptID" ItemStyle-CssClass="Match" />
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
            <div class="modal-dialog" style="height: 600px;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: royalblue">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                        <h4 class="modal-title">Add New Medicine</h4>
                    </div>
                    <div class="modal-body" style="height: 500px;">

                        <iframe id="NewMedicineIframe" style="width: 100%; height: 100%" frameborder="0"></iframe>

                        
                    </div>
                </div>

            </div>
        </div>



    </div>




</asp:Content>
