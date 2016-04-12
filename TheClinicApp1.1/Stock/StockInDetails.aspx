<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="StockInDetails.aspx.cs" Inherits="TheClinicApp1._1.Stock.StockDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <div class="main_body">


        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
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
                Stock In Details...
            </div>
            <div class="icon_box">
                <a class="add_medicine" data-toggle="modal" data-target="#add_medicine"><span title="All Medicine" data-toggle="tooltip" data-placement="left">
                    <img src="../images/add_medicine.png" /></span></a>
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
                                    <input class="field" type="search" placeholder="Search here..." />
                                    <input class="button" type="submit" value="Search" />
                                </div>
                                <ul class="top_right_links">
                                    <li><a class="back" href="StockIn.aspx"><span></span>Back</a></li>
                                    <li><a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick" href="#"><span></span>Save</a></li>
                                    <li><a class="new" href="#"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div class="tab_table">

                                <table class="details_table" width="100%" border="0">
                                    <tr>
                                        <td>Bill No</td>
                                        <td>
                                            <asp:TextBox ID="txtBillNo" TabIndex="1" runat="server"></asp:TextBox>
                                        </td>
                                        <td>Date</td>
                                        <td>
                                            <asp:TextBox ID="txtDate" TabIndex="2" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Bill No2</td>
                                        <td>
                                            <asp:TextBox ID="txtRefNo2" TabIndex="3" runat="server"></asp:TextBox>
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
                                                    <input id="txtMedicine" type="text" placeholder="Medicine" class="input" /></td>
                                                <td>
                                                    <input id="txtUnit" class="input" type="text" placeholder="Unit" /></td>
                                                <td>
                                                    <input id="txtCode" type="text" placeholder="Code" class="input" /></td>
                                                <td>
                                                    <input id="txtCategory" type="text" placeholder="Category" class="input" /></td>
                                                <td>
                                                    <input id="txtQuantity" type="text" placeholder="Quantity" class="input" /></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" value="-" class="bt1" style="width: 20px;" /></td>
                                                <td style="background-color: transparent">
                                                    <input type="button" id="btAdd" onclick="clickStockAdd(); this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" />
                                                </td>
                                                <td style="background-color: transparent">
                                                    <input id="hdnDetailID' + iCnt + '" type="hidden" />
                                                    <input id="hdnQty' + iCnt + '" type="hidden" /></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                    <div id="maindiv">
                                    </div>
                                </div>
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



    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/Dynamicgrid.js"></script>

    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });

        });
    </script>
</asp:Content>
