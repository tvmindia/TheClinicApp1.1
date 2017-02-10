<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="TheClinicApp1._1.MasterAdd.Categories" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableCdn="true"></asp:ScriptManager>

    <style>
        .modal table thead {
            background-color: #5681e6;
            text-align: center;
            color: white;
        }
        .button1 {
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
            font-family: 'raleway-semibold';
            min-width: 83px;
            background-color: #abd357;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            text-indent: 20px;
            background-position-x: 5px;
            color: inherit;
        }
    </style>

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>

    <script>
        
        function Validation() {
           
            var fieldValidationBool = FieldsValidation();
            if (fieldValidationBool == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }
        function FieldsValidation()
            {
                    try
                    {
                        //$('#Displaydiv').remove();
                        var name = $('#<%=txtCategoryName.ClientID%>');
                       

                        var container = [
                            { id: name[0].id, name: name[0].name, Value: name[0].value },
                           
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
        $(document).ready(function () {

            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });

            $('.nav_menu').click(function () {
                $(".main_body").toggleClass("active_close");
            });

            //$('table').tablePagination({});
            $('input[type=text]').on('focus', function () {
                debugger;
                $(this).css({ borderColor: '#dbdbdb' });
                $("#Errorbox").hide(1000);
            });
            $('[data-toggle="tooltip"]').tooltip();

          

        });
        //---------------* Function to check category name duplication *--------------//

        function CheckCategoryNameDuplication(txtCategoryName) {

            var name = document.getElementById('<%=txtCategoryName.ClientID %>').value;
            name = name.trim();
            if (name != "") {
                //name = name.replace(/\s/g, '');

                PageMethods.ValidateCategoryName(name, OnSuccess, onError);

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
          else {
              if (name == "") {
                  var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                  LnameImage.style.display = "none";
                  var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                  errLname.style.display = "none";
              }
          }
      }

    </script>

    <%--  //------------- AUTOFILL SCRIPT ---------%>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>
    <script>

        var CategoryID = '';

        //---getting data as json-----//
        function getJsonData(data, page) {
            var jsonResult = {};
            var req = $.ajax({
                type: "post",
                url: page,
                data: data,
                delay: 3,
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json"

            }).done(function (data) {
                jsonResult = data;
            });
            return jsonResult;
        }

        //-------------------------------- * EDIT Button Click * ------------------------- //


        $(function () {
            $("[id*=dtgViewAllCategories] td:eq(0)").click(function () {
                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                document.getElementById('<%=imgWebLnames.ClientID %>').style.display = "none";
                document.getElementById('<%=errorLnames.ClientID %>').style.display = "none";

                if ($(this).text() == "") {
                    var jsonResult = {};
                    CategoryID = $(this).closest('tr').find('td:eq(3)').text();
                    var Category = new Object();
                    Category.CategoryID = CategoryID;
                    jsonResult = GetCategoryDetailsBycategoryID(Category);
                    if (jsonResult != undefined) {
                       
                        BindCategoryControls(jsonResult);
                    }
                }
            });
        });

        function GetCategoryDetailsBycategoryID(Category) {
            var ds = {};
            var table = {};
            var data = "{'CategoryObj':" + JSON.stringify(Category) + "}";
            ds = getJsonData(data, "../MasterAdd/Categories.aspx/BindCategoryDetailsOnEditClick");
            table = JSON.parse(ds.d);
            return table;
        }


        function BindCategoryControls(Records) {
            $.each(Records, function (index, Records) {
                $("#<%=txtCategoryName.ClientID %>").val(Records.Name);
                $("#<%=hdnCategoryId.ClientID %>").val(Records.CategoryID);
                $("#CategoryClose").click();
            });
        }
        //-------------------------------- *END : EDIT Button Click * ------------------------- //
        
        //-------------------------------- * Delete Button Click * ------------------------- //

        $(function () {
            $("[id*=dtgViewAllCategories] td:eq(1)").click(function () {
                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

                if ($(this).text() == "") {
                    var DeletionConfirmation = ConfirmDelete();
                    if (DeletionConfirmation == true) {
                        CategoryID = $(this).closest('tr').find('td:eq(3)').text();
                        DeleteCategoryByID(CategoryID);
                        //window.location = "StockIn.aspx?HdrID=" + receiptID;
                    }
                }
            });
        });

        function DeleteCategoryByID(CategoryID) { //------* Delete Receipt Header by receiptID (using webmethod)

            if (CategoryID != "") {
                PageMethods.DeleteCategoryByID(CategoryID, OnSuccess, onError);
                function OnSuccess(response, userContext, methodName) {
                  
                    if (response == false) {
                        $("#CategoryClose").click();
                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;
                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
                    }

                    else {
                      
                        var PageIndx = parseInt(1);
                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {
                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }
                        GetCategories(PageIndx);

                        <%--$("#<%=hdnCategoryId.ClientID %>").val("");
                        var lblclass = Alertclasses.sucess;
                        var lblmsg = msg.DeletionSuccessFull;
                        var lblcaptn = Caption.SuccessMsgCaption;
                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);--%>
                    }
                }
                function onError(response, userContext, methodName) {
                }
            }
        }
                
        //-------------------------------- * END : Delete Button Click * ------------------------- //

        
        //---------------------------------------------------------- * Category Grid BinD,Paging,Search *--------------------------------------------------//

        $(function () {
            GetCategories(1);
        });

        $("[id*=txtSearch]").live("keyup", function () {
            GetCategories(parseInt(1));
        });

        $(".Pager .page").live("click", function () {

            GetCategories(parseInt($(this).attr('page')));
        });

        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };

        function GetCategories(pageIndex) {
            $.ajax({
                type: "POST",
                url: "../MasterAdd/Categories.aspx/ViewAndFilterCategories",
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
            var Categories = xml.find("Categories");
            
            if (row == null) {
                row = $("[id*=dtgViewAllCategories] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllCategories] tr").not($("[id*=dtgViewAllCategories] tr:first-child")).remove();
            if (Categories.length > 0) {
                $.each(Categories, function () {
                    var medicine = $(this);
                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');

                    $("td", row).eq(0).html($('<img />')
                     .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');


                    $("td", row).eq(1).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(2).html($(this).find("Name").text());
                    $("td", row).eq(3).html($(this).find("CategoryID").text());

                    $("[id*=dtgViewAllCategories]").append(row);
                    row = $("[id*=dtgViewAllCategories] tr:last-child").clone(true);

                });
                var pager = xml.find("Pager");

                if ($('#txtSearch').val() == '') {

                    var GridRowCount = pager.find("RecordCount").text();

                    $("#<%=lblCaseCount.ClientID %>").text(GridRowCount);

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
                $("[id*=dtgViewAllCategories]").append(empty_row);
                $(".Pager").hide();

            }


            var th = $("[id*=dtgViewAllCategories] th:contains('CategoryID')");
            th.css("display", "none");
            $("[id*=dtgViewAllCategories] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };



        //Open Modal Popup
        function OpenModal() {

            $('#txtSearch').val('');
            GetCategories(parseInt(1));

        }

    </script>





    <div class="main_body">

        <!-- Left Navigation Bar -->
        <div class="left_part">
            <div class="logo">
                <a href="#">
                    <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
            </div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                 <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server"  ClientIDMode="Static" class="active"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                <li id="log" runat="server" ><a name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">
                &copy;
                <asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label>
            </p>
        </div>

        <!-- Right Main Section -->
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Masters
                <ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>
            </div>

            <div class="icon_box">

                <a class="all_admin_link" data-toggle="modal" data-target="#AllCategories" onclick="OpenModal();">
                    <span class="tooltip1">
                        <span class="count">
                            <asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label></span>
                        <img src="../images/categories-512 copy.png" />
                        <span class="tooltiptext1">View All Categories</span>
                    </span>
                </a>
            </div>

            <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="Categories.aspx">Categories</a></li>

                        <li role="presentation"><a href="Units.aspx">Units</a></li>

                        <li role="presentation"><a href="Medicnes.aspx">Medicines</a></li>
                       <%-- <li role="presentation"><a href="AddDoctor.aspx">Doctor</a></li>--%>

                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="stock">
                            <div class="grey_sec">
                                <%--<div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links">
                                    <li>
                                        <%--<a class="save" id="Save" runat="server" onserverclick="Save_ServerClick"><span></span>Save</a>--%>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" OnClientClick="return Validation(); " />
                                    </li>
                                    <li><a class="new" href="Categories.aspx"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div id="Errorbox" style="height: 30%; display: none;" ClientIDMode="Static" runat="server">
                                <a class="alert_close">X</a>
                                <div>
                                    <strong>
                                        <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label>
                                    </strong>
                                    <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="alert alert-info" id="divDisplayNumber" visible="false" runat="server">
                                <a class="alert_close">X</a>
                                <div>
                                    <div>
                                        <asp:Label ID="lblDisplayFileNumber" runat="server" Text="File Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblFileCount" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;<asp:Label ID="lblTokenNumber" runat="server" Text="Token Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblTokencount" runat="server" Text=""></asp:Label></strong>
                                    </div>
                                </div>
                            </div>
                            <div class="tab_table">
                                <div class="row field_row">
                                    <div class="col-lg-8">
                                        <label for="address">Category Name</label><input name="address" id="txtCategoryName" type="text" title="Please enter a value" runat="server" onchange="CheckCategoryNameDuplication(this)" />
                                        <span class="tooltip2">
                                            <asp:Image ID="imgWebLnames" runat="server" ImageUrl="~/Images/newfff.png" Style="display: none" />
                                            <span class="tooltiptext2">Category name is Available</span>
                                        </span>
                                        <span class="tooltip2">
                                        <asp:Image ID="errorLnames" runat="server" ImageUrl="~/Images/newClose.png" Style="display: none" />
                                            <span class="tooltiptext2">Category name is Unavailable</span>
                                        </span>
                                    </div>




                                    <%--<div class="col-lg-4">--%>



                                    <%--<label for="name">CAtegory Name</label><input id="txtCategoryName" runat="server" type="text" name="name" required="required" pattern="^\S+[A-z][A-z\.\s]+$" title="⚠ The Name is required and it allows alphabets only." autofocus="autofocus" />--%>



                                    <%-- <asp:TextBox ID="txtCategoryName" runat="server" onchange="CheckCategoryNameDuplication(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>--%>

                                    <%-- </div>--%>
                                    <%--<div class="col-lg-4">--%>

                                    <%--<br />
                                             <br />--%>


                                    <%--</div>--%>
                                </div>

                            </div>








                        </div>
                    </div>

                </div>


            </div>
        </div>
    </div>



    <div id="AllCategories" class="modal fade" role="dialog">
        <div class="modal-dialog" style="min-width: 550px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-color: #3661C7;">
                    <button type="button" class="close" data-dismiss="modal" id="CategoryClose">&times;</button>
                    <h3 class="modal-title">View All Categories</h3>
                </div>
                <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">

                    <div class="col-lg-12" style="height: 480px">

                        <div class="col-lg-12" style="height: 40px">
                            <div class="search_div">
                                <input class="field1" type="text" placeholder="Search with Name.." id="txtSearch" />
                                <input class="button3" type="button" value="Search" disabled/>
                            </div>
                        </div>


                        <div class="col-lg-12" style="height: 400px">
                            <asp:GridView ID="dtgViewAllCategories" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID" class="table">

                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:ImageButton ID="ImgBtnUpdate" runat="server" Style="border: none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment" formnovalidate OnClick="ImgBtnUpdate_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" Style="border: none!important" runat="server" ImageUrl="~/images/Deleteicon1.png" OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" formnovalidate />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:BoundField DataField="Name" HeaderText="Category Name" ItemStyle-CssClass="Match"></asp:BoundField>

                                    <asp:BoundField DataField="CategoryID" HeaderText="CategoryID"></asp:BoundField>



                                </Columns>

                            </asp:GridView>

                        </div>
                        <div class="Pager">
                        </div>


                    </div>
                </div>


            </div>

        </div>
    </div>





    <asp:HiddenField ID="hdnCategoryId" runat="server" />
</asp:Content>
