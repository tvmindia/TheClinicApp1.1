<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TheClinicApp1._1.Admin.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <link href="../css/TheClinicApp.css" rel="stylesheet" />--%>
    <link href="../css/main.css" rel="stylesheet" />
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/jquery-1.3.2.min.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <%--<script src="../js/bootstrap.min.js"></script>--%>
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableCdn="true"></asp:ScriptManager>
    <!-- Script Files -->

    <%-- <style>
    .modal table thead {
    background: #5681e6;
    color:red!important;
    border: 0;
    
    border-left: 1px solid #dbdbdb;
    
    font-size: 13px!important;
   
    font-family: 'raleway-semibold';
    text-align: center;
    }   
    </style>--%>
    <style>
           .selected_row {
            background-color: #d3d3d3!important;
        }

            select
        {
            color:black!important;
        }

    </style>
    <script>
       
        function Validation() {
            debugger;
           

            if (($('#<%=txtLoginName.ClientID%>').val().trim() == "") || ($('#<%=txtFirstName.ClientID%>').val().trim() == "") || ($('#<%=txtPassword.ClientID%>').val().trim() == "") || ($('#<%=txtConfirmPassword.ClientID%>').val().trim() == "") || ($('#<%=txtPhoneNumber.ClientID%>').val().trim() == "") || ($('#<%=txtEmail.ClientID%>').val().trim() == "")) {

                var lblclass = Alertclasses.danger;
                var lblmsg = msg.Requiredfields;
                var lblcaptn = Caption.Confirm;
                ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
                return false;
            }
            else if ($("#<%=ddlGroup.ClientID%> option:selected").text() == "--Select Clinic--")
            {

                var lblclass = Alertclasses.danger;
                var lblmsg = AlertMsgs.ClinicRequired;
                var lblcaptn = Caption.FailureMsgCaption;
                ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                return false;
            }


            else {
                return true;
            }
        }

        $(document).ready(function () {
            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });

            $('.nav_menu').click(function () {
                $(".main_body").toggleClass("active_close");
            });
        });

        function SetIframeSrc(HyperlinkID) {
            if (HyperlinkID == "AllUsersIframe") {
                var AllUsersIframe = document.getElementById('AllUsersIframe');
                AllUsersIframe.src = "AddNewMedicine.aspx";
                //$('#OutOfStock').modal('show');
            }
        }

        //EmailID duplication check

        function EmailIDDuplicationCheck(txtEmail) {
            var name = document.getElementById('<%=txtEmail.ClientID %>').value;
            name = name.trim();
            if (name != "") {
                //name = name.replace(/\s/g, '');

                var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

                PageMethods.ValidateEmailID(name,clinicID, OnSuccess, onError);

                function OnSuccess(response, userContext, methodName) {
                    var LnameImage = document.getElementById('<%=imgEmailAvailable.ClientID %>');
                    var errLname = document.getElementById('<%=imgEmailUnAvailable.ClientID %>');
                    if (response == false) {
                        LnameImage.style.display = "block";
                        errLname.style.display = "none";
                    }
                    if (response == true) {
                        errLname.style.display = "block";
                        errLname.style.color = "Red";
                        errLname.innerHTML = "EmailID Alreay Exists"
                        LnameImage.style.display = "none";
                    }
                }
                function onError(response, userContext, methodName) {

                }
            }

            else {
                if (name == "") {
                    var LnameImage = document.getElementById('<%=imgEmailAvailable.ClientID %>');
                    LnameImage.style.display = "none";
                    var errLname = document.getElementById('<%=imgEmailUnAvailable.ClientID %>');
                    errLname.style.display = "none";
                }
            }
        }


        //Login name duplication check

        function LoginNameCheck(txtLoginName) {

            var name = document.getElementById('<%=txtLoginName.ClientID %>').value;
            name = name.trim();
            if (name != "") {
                //name = name.replace(/\s/g, '');

                PageMethods.ValidateLoginName(name, OnSuccess, onError);

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

        function PassowrdEqualityCheck() {
            if (document.getElementById('<%=txtConfirmPassword.ClientID %>').value != document.getElementById('<%=txtPassword.ClientID %>').value) {
                document.getElementById('<%=txtConfirmPassword.ClientID %>').style.borderColor = "#ff0000";
            }
            else {
                document.getElementById('<%=txtConfirmPassword.ClientID %>').style.borderColor = "#00b300";
            }
        }
    </script>

    <script src="../js/jquery-1.3.2.min.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>

    <script type="text/javascript">

        //$(function () {
        //    $('[data-toggle="tooltip"]').tooltip();
        //});
           <%-- var gridViewRowCount = document.getElementById("<%= dtgViewAllUsers.ClientID %>").rows.length;
            if (gridViewRowCount >0) {
                $('table').tablePagination({});
            }--%>
       

        function SetRequired() {
            document.getElementById('<%=txtLoginName.ClientID %>').required = true;
            document.getElementById('<%=txtFirstName.ClientID %>').required = true;
            document.getElementById('<%=txtPassword.ClientID %>').required = true;
            document.getElementById('<%=txtConfirmPassword.ClientID %>').required = true;
            document.getElementById('<%=txtLoginName.ClientID %>').required = true;
            document.getElementById('<%=txtEmail.ClientID %>').required = true;

        }

    </script>


    <%--  //------------- AUTOFILL SCRIPT ---------%>
    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>

    <script type="text/javascript">

        var UserID = '';

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
            $("[id*=dtgViewAllUsers] td:eq(0)").click(function () {
                if ($(this).closest('tr').find('td:eq(6)').text() != $('#<%=hdnLoginedUserID.ClientID%>').val()) { //Making sure ,not editing the logined user data

                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                document.getElementById('<%=imgWebLnames.ClientID %>').style.display = "none";
                document.getElementById('<%=errorLnames.ClientID %>').style.display = "none";
                document.getElementById('<%=imgEmailAvailable.ClientID %>').style.display = "none";
                document.getElementById('<%=imgEmailUnAvailable.ClientID %>').style.display = "none";

                    $("#<%=txtLoginName.ClientID %>").attr("readonly", "readonly");

                if ($(this).text() == "") {
                    var jsonResult = {};
                    UserID = $(this).closest('tr').find('td:eq(6)').text();              
                    var User = new Object();
                    User.UserID = UserID;
                    jsonResult = GetUserDetailsByUserID(User);
                    if (jsonResult != undefined) {
                      
                        BindUserControls(jsonResult);
                    }
                } }
            });
        });


        function GetUserDetailsByUserID(User) {
            var ds = {};
            var table = {};
            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

            var data = "{'userObj':" + JSON.stringify(User) + ",'clinicID':" + JSON.stringify(clinicID) + "}";
            ds = getJsonData(data, "../Admin/Admin.aspx/BindUserDetailsOnEditClick");
            table = JSON.parse(ds.d);
            return table;
        }

        function BindUserControls(Records) {
            $.each(Records, function (index, Records) {

                $("#<%=txtLoginName.ClientID %>").val(Records.LoginName);
                $("#<%=txtFirstName.ClientID %>").val(Records.FirstName);
                $("#<%=txtLastName.ClientID %>").val(Records.LastName);
                $("#<%=txtPhoneNumber.ClientID %>").val(Records.PhoneNo);
                $("#<%=txtEmail.ClientID %>").val(Records.Email);
                $("#<%=hdnUserID.ClientID %>").val(Records.UserID);

                $("#<%=ddlGroup.ClientID %>").val(Records.ClinicID);
               
                if (Records.Active == true) {
                    $("#<%=rdoActiveYes.ClientID %>").prop('checked', true);
                }
                else {
                    $("#<%=rdoActiveNo.ClientID %>").prop('checked', true);
                }
                var clinicID = $("#<%=hdnClinicID.ClientID %>").val();
                PageMethods.CheckUserIsDoctor(Records.UserID,clinicID, OnSuccess, onError);

                function OnSuccess(response, userContext, methodName) {

                    if (response == false) {
                        $("#<%=rdoNotDoctor.ClientID %>").prop('checked', true);
                    }
                    if (response == true) {
                        $("#<%=rdoDoctor.ClientID %>").prop('checked', true);
                    }
                }
                function onError(response, userContext, methodName) {
                }

                $("#UserClose").click();
            });

        }
        //-------------------------------- * Delete Button Click * ------------------------- //

        $(function () {
            $("[id*=dtgViewAllUsers] td:eq(1)").click(function () {
                
                if ($(this).closest('tr').find('td:eq(6)').text() != $('#<%=hdnLoginedUserID.ClientID%>').val()) { //Making sure ,not deleting the logined user data


                    if ($(this).text() == "") {
                        var DeletionConfirmation = ConfirmDelete();
                        if (DeletionConfirmation == true) {
                            UserID = $(this).closest('tr').find('td:eq(6)').text();

                            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

                            DeleteUserByID(UserID, clinicID);
                            //window.location = "StockIn.aspx?HdrID=" + receiptID;
                        }
                    }
                }
            });
        });

        function DeleteUserByID(UserID, clinicID) { //------* Delete Receipt Header by receiptID (using webmethod)

            if (UserID != "") {
                PageMethods.DeleteUserByID(UserID,clinicID, OnSuccess, onError);

                function OnSuccess(response, userContext, methodName) {
                  
                    if (response == false) {
                        $("#UserClose").click();
                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;
                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
                    }

                    else {
                        $("#<%=hdnUserID.ClientID %>").val("");
                        var PageIndx = parseInt(1);
                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {
                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }
                        GetUsers(PageIndx);
<%--
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


        $(function () {
         
            GetUsers(1);
        });
        $("[id*=txtSearch]").live("keyup", function () {
        
            GetUsers(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetUsers(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        function GetUsers(pageIndex) {

            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

            $.ajax({

                type: "POST",
                url: "../Admin/Admin.aspx/ViewAndFilterUser",
                data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + ',clinicID: "' + clinicID + '"}',
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
            var Users = xml.find("Users");
            if (row == null) {
                row = $("[id*=dtgViewAllUsers] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllUsers] tr").not($("[id*=dtgViewAllUsers] tr:first-child")).remove();
            if (Users.length > 0) {

                $.each(Users, function () {

                    $("td", row).eq(0).html($('<img />')
                       .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(1).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                  

                    if ($(this).find("UserID").text() == $('#<%=hdnLoginedUserID.ClientID%>').val()) {
                        $("td", row).addClass("selected_row");
                        $("td", row).eq(2).html($(this).find("LoginName").text());
                        $("td", row).eq(0).html($('<img />')
                     .attr('src', "" + '../images/Editicon1.png' + "")).removeClass('CursorShow');

                        $("td", row).eq(1).html($('<img />')
                   .attr('src', "" + '../images/Deleteicon1.png' + "")).removeClass('CursorShow');

                    }
                    else
                    {
                        $("td", row).removeClass("selected_row");
                        $("td", row).eq(2).html($(this).find("LoginName").text());

                        $("td", row).eq(0).html($('<img />')
                    .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                        $("td", row).eq(1).html($('<img />')
                  .attr('src', "" + '../images/Deleteicon1.png' + "")).removeClass('CursorShow');


                    }

                    $("td", row).eq(3).html($(this).find("FirstName").text());
                    $("td", row).eq(4).html($(this).find("LastName").text());
                    $("td", row).eq(5).html($(this).find("Active").text());

                   
                   

                    $("td", row).eq(6).html($(this).find("UserID").text());

                    $("[id*=dtgViewAllUsers]").append(row);
                    row = $("[id*=dtgViewAllUsers] tr:last-child").clone(true);
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
            } else {

                var empty_row = row.clone(true);
                $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                $("td:first-child", empty_row).attr("align", "center");
                $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                $("td", empty_row).not($("td:first-child", empty_row)).remove();
                $("[id*=dtgViewAllUsers]").append(empty_row);

                $(".Pager").hide();

                if ($('#txtSearch').val() == '') {
                   
                    $("#<%=lblCaseCount.ClientID %>").text(0);
                }

            }



            var th = $("[id*=dtgViewAllUsers] th:contains('UserID')");
            th.css("display", "none");
            $("[id*=dtgViewAllUsers] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };

        function OpenModal() {
            $('#txtSearch').val('');
            GetUsers(parseInt(1));
        }

        function ChangeClinic(e)
        {
            if ($("#<%=ddlGroup.ClientID%> option:selected").text() != "--Select Clinic--")
            {
                $("#<%=hdnClinicID.ClientID %>").val(e.value);
                
            }

            else
            {
                $("#<%=hdnClinicID.ClientID %>").val("");

            }

            GetUsers(parseInt(1));
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
                <li id="patients"><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                 <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" class="active" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                <li id="log" runat="server"><a name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">
                &copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label>
            </p>
        </div>

        <!-- Right Main Section -->
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Administrator
                <ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>
            </div>

            <div class="icon_box">
                <a class="all_admin_link" data-toggle="modal" data-target="#AllUsers" onclick="OpenModal();">
                    <span class="tooltip1">
                        <span class="count">
                            <asp:Label ID="lblCaseCount" runat="server" Text="0">
                            </asp:Label></span>
                        <img src="../images/multiuser.png" />
                        <span class="tooltiptext1">View All Users</span>
                    </span>
                </a>
            </div>

            <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="Admin.aspx">Create User</a></li>

                        <li role="presentation"><a href="AssignRoles.aspx">Assign Role</a></li>
                        <li role="presentation" runat="server" id="liSAdmin" visible="false"><a href="SAdmin.aspx">SAdmin</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="stocks">
                            <div class="grey_sec">

                                     <asp:DropDownList ID="ddlGroup" onchange="ChangeClinic(this)" runat="server" CssClass="drop" Width="225px" Style="font-family: Arial, Verdana, Tahoma;">             
                                     </asp:DropDownList>
                                   
                                <%--<div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links">
                                    <li>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" OnClientClick="return Validation();" />
                                    </li>
                                    <li><a class="new" href="Admin.aspx"><span></span>New</a></li>
                                </ul>
                            </div>

                            <div id="Errorbox" style="height: 30%; display: none;" runat="server">
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
                                        <label for="name">Login Name</label><input id="txtLoginName" runat="server" type="text" name="name" onchange="LoginNameCheck(this)" />
                                        <span class="tooltip2">
                                            <asp:Image ID="imgWebLnames" runat="server" ImageUrl="~/Images/newfff.png" Style="display: none" />
                                            <span class="tooltiptext2">Login name is Available</span>
                                        </span>
                                        <span class="tooltip2">
                                            <asp:Image ID="errorLnames" runat="server" ImageUrl="~/Images/newClose.png" Style="display: none" />
                                            <span class="tooltiptext2">Login name is Unavailable</span>
                                        </span>
                                    </div>
                                </div>

                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="name">First Name</label><input id="txtFirstName" runat="server" type="text" name="name" />
                                    </div>
                                    <div class="col-lg-4 ">
                                        <label for="name">Last Name</label><input id="txtLastName" runat="server" type="text" name="name" />
                                    </div>
                                </div>

                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="name">Password</label><input id="txtPassword" runat="server" type="password" name="name" autocomplete="off" />
                                    </div>
                                    <div class="col-lg-4 ">
                                        <label for="name">Re-Type Password</label><input id="txtConfirmPassword" runat="server" type="password" name="name" autocomplete="off" onkeyup="PassowrdEqualityCheck()" />
                                    </div>
                                </div>

                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="name">Phone</label><input id="txtPhoneNumber" runat="server" type="text" onkeypress="return isNumber(event)" name="name" pattern="^[0-9+-]*$" />
                                    </div>
                                    <div class="col-lg-4 ">
                                        <label for="name">Email</label><input id="txtEmail" runat="server" type="email" name="name"  onchange="EmailIDDuplicationCheck(this)" />
                                        <span class="tooltip2">
                                        <asp:Image ID="imgEmailAvailable" runat="server" ImageUrl="~/Images/newfff.png" Style="display: none" />
                                            <span class="tooltiptext2">EmailID is Available</span>
                                        </span>
                                        <span class="tooltip2">
                                        <asp:Image ID="imgEmailUnAvailable" runat="server" ImageUrl="~/Images/newClose.png" Style="display: none" />
                                             <span class="tooltiptext2">EmailID is Unavailable</span>
                                        </span>
                                    </div>
                                </div>

                                <div class="row field_row">
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div>
                                                <label for="sex" style="margin-left: 15px">
                                                    IsActive
                                                <asp:RadioButton ID="rdoActiveYes" runat="server" GroupName="Active" Text="Yes" CssClass="checkbox-inline" Checked="true" />
                                                    <asp:RadioButton ID="rdoActiveNo" runat="server" GroupName="Active" Text="No" CssClass="checkbox-inline" />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div>
                                                <label for="sex" style="margin-left: 15px">
                                                    IsDoctor
                                                    <asp:RadioButton ID="rdoDoctor" runat="server" GroupName="Doctor" Text="Yes" CssClass="checkbox-inline" />
                                                    <asp:RadioButton ID="rdoNotDoctor" runat="server" GroupName="Doctor" Text="No" CssClass="checkbox-inline" Checked="true" />
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="AllUsers" class="modal fade" role="dialog">
        <div class="modal-dialog" style="min-width: 550px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-color: #3661C7;">
                    <button type="button" class="close" data-dismiss="modal" id="UserClose">&times;</button>
                    <h3 class="modal-title">View All Users</h3>
                </div>

                <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                    <div class="col-lg-12" style="height: 480px;">
                        <div class="col-lg-12" style="height: 40px">
                            <div class="search_div">
                                <input class="field1" type="text" placeholder="Search with Name.." id="txtSearch" />
                                <input class="button3" type="button" value="Search" disabled />
                            </div>
                        </div>
                        <div class="col-sm-12" style="height: 400px;">
                            <asp:GridView ID="dtgViewAllUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" Style="width: 100%; font-size: 13px!important;">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnUpdate" runat="server" Style="border: none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment" formnovalidate OnClick="ImgBtnUpdate_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" Style="border: none!important" runat="server" ImageUrl="~/images/Deleteicon1.png" OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" formnovalidate />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LoginName" HeaderText="Login Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="Active" HeaderText="Active" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="UserID" HeaderText="UserID"></asp:BoundField>
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
    <%--<asp:HiddenField ID="hdnUserCountChanged" runat="server" value="False"/>--%>
      <%--<asp:HiddenField ID="hdnLoginUserID" runat="server" />--%>
    <asp:HiddenField ID="hdnUserID" runat="server" />
    <asp:HiddenField ID="hdnDeleteButtonClick" runat="server" />
      <asp:HiddenField ID="hdnLoginedUserID" runat="server" /> 
    
      <asp:HiddenField ID="hdnClinicID" runat="server" Value="" /> <%----- *ClinicID generally accessed from UA, If clinic changed ClinicID will be saved to this hiddenfield  *--%>
    
    <%--<asp:HiddenField ID="hdnClinicName" runat="server" Value="" />--%>

</asp:Content>
