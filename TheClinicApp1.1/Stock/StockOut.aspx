<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="StockOut.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/TheClinicApp.css" rel="stylesheet" />

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

            $('.alert_close').click(function () {


                $(this).parent(".alert").hide();

            });

        });


        $(function () {
            $("[id*=gvIssueHD] td:first").click(function () {

                var DeletionConfirmation = ConfirmDelete();

                if (DeletionConfirmation == true) {
                    issueID = $(this).closest('tr').find('td:eq(5)').text();


                    window.location = "StockOut.aspx?HdrID=" + issueID;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  //------------- AUTOFILL SCRIPT ---------%>
    <script src="../js/jquery-1.8.3.min.js"></script>

    <script src="../js/ASPSnippets_Pager.min.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <script type="text/javascript">


        $(function () {
            GetIssueHD(1);
        });

        $("[id*=txtSearch]").live("keyup", function () {

            GetIssueHD(parseInt(1));
        });

        $(".Pager .page").live("click", function () {
            GetIssueHD(parseInt($(this).attr('page')));
        });

        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        function GetIssueHD(pageIndex) {

            $.ajax({
                type: "POST",
                url: "../Stock/StockOut.aspx/GetIssueHD",
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
        var issueID = '';

        function OnSuccess(response) {



            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var IssueHD = xml.find("IssueHD");
            if (row == null) {
                row = $("[id*=gvIssueHD] tr:last-child").clone(true);
            }
            $("[id*=gvIssueHD] tr").not($("[id*=gvIssueHD] tr:first-child")).remove();
            if (IssueHD.length > 0) {
                $.each(IssueHD, function () {

                    //$("td", row).eq(0).html('<a href="NewIssue.aspx">' + $(this).find("RefNo1").text() + '</a>');

                    //issueID = $(this).find("IssueID").text();



                    //$("td", row).eq(0).html($(this).find("IssueNO").text()).click(function () {

                    //    issueID = $(this).closest('tr').find('td:eq(2)').text();
                    //    window.location = "NewIssue.aspx?issueID=" + issueID;
                    //});

                    //$("td", row).eq(0).html($(this).find('[id*=img1]').attr('src', '../images/Cancel.png'));
                    //$("td", row).eq(0).html("<img src='../images/Cancel.png' />");


                    $("td", row).eq(0).html($('<img />')
                        .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');






                    $("td", row).eq(1).html($(this).find("IssueNO").text());

                    $("td", row).eq(2).html($(this).find("IssuedTo").text());
                    $("td", row).eq(3).html($(this).find("Date").text());
                    $("td", row).eq(4).html('Details').click(function () {

                        issueID = $(this).closest('tr').find('td:eq(5)').text();
                        window.location = "StockOutDetails.aspx?issueID=" + issueID;
                    }).addClass('CursorShow');

                    $("td", row).eq(5).html($(this).find("IssueID").text());



                    //$("td", row).eq(2).html($(this).find("Date").text());
                    $("[id*=gvIssueHD]").append(row);
                    row = $("[id*=gvIssueHD] tr:last-child").clone(true);




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
                $("[id*=gvIssueHD]").append(empty_row);
            }


            var th = $("[id*=gvIssueHD] th:contains('IssueID')");
            th.css("display", "none");
            $("[id*=gvIssueHD] tr").each(function () {
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
                <li id="admin" visible="false" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="master" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">nav</a>
                Stock Out<ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22" Font-Underline="true"></asp:Label></li><li>
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
                        <li role="presentation"><a href="StockIn.aspx">Stock In</a></li>
                        <li role="presentation" class="active"><a href="StockOut.aspx">Stock Out</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="stock_out">
                            <div class="grey_sec">
                                <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch">
                                    <input class="button" type="submit" value="Search">
                                </div>
                                <ul class="top_right_links">
                                    <li><a visible="false" class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="StockOutDetails.aspx"><span></span>New</a></li>
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

                                <asp:GridView ID="gvIssueHD" runat="server" Style="width: 100%" AutoGenerateColumns="False" class="table">
                                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                    <Columns>


                                        <%--<asp:BoundField DataField="IssueNO" HeaderText="IssueNO"  ItemStyle-Font-Underline="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Blue" ItemStyle-CssClass="cursorshow" />--%>


                                        <%--<asp:BoundField  HeaderText="Delete"  ItemStyle-CssClass="Match"  />--%>
                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:Image ID="img1" runat="server" 
                                                    OnClientClick="ConfirmDelete()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="IssueNO" HeaderText="IssueNO" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="IssuedTo" HeaderText="IssuedTo" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-CssClass="Match" />
                                        <asp:BoundField HeaderText="Details" ItemStyle-CssClass="Match" />
                                        <asp:BoundField DataField="IssueID" HeaderText="IssueID" ItemStyle-CssClass="Match" />

                                    </Columns>

                                </asp:GridView>
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

    </div>

</asp:Content>
