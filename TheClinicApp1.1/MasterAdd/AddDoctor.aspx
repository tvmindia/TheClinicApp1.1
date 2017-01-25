<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AddDoctor.aspx.cs" Inherits="TheClinicApp1._1.MasterAdd.AddDoctor" %>

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

         
           .selected_row {
            background-color: #d3d3d3!important;
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
        $(document).ready(function () {

  <%--var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
          LnameImage.style.display = "none";
          var errLname = document.getElementById('<%=errorLnames.ClientID %>');
          errLname.style.display = "none";--%>


            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });

            $('.nav_menu').click(function () {
                $(".main_body").toggleClass("active_close");
            });

            $('[data-toggle="tooltip"]').tooltip();

        });

        function Validation() {
          
            if (($('#<%=txtName.ClientID%>').val().trim() == "") || ($('#<%=txtLoginName.ClientID%>').val().trim() == "") || ($('#<%=txtPhoneNumber.ClientID%>').val().trim() == "") || ($('#<%=txtEmail.ClientID%>').val().trim() == "")) {
                var lblclass = Alertclasses.danger;
                var lblmsg = msg.Requiredfields;
                var lblcaptn = Caption.Confirm;

                ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
              return false;
          }
          else {
              return true;
          }
      }

        //---------------* Function to check  Login name duplication *--------------//      

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
        //---------------* Function to check  Email ID duplication *--------------//      
       
        function EmailIDDuplicationCheck(txtEmail) {

           
            var name = document.getElementById('<%=txtEmail.ClientID %>').value;
            name = name.trim();
            if (name != "") {

                //name = name.replace(/\s/g, '');

                PageMethods.ValidateEmailID(name, OnSuccess, onError);

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

    </script>

     <link href="../css/TheClinicApp.css" rel="stylesheet" />
        <script src="../js/jquery-1.8.3.min.js"></script>
    
    <script src="../js/ASPSnippets_Pager.min.js"></script>

    <script type="text/javascript">

        var DoctorID = '';
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
            $("[id*=dtgDoctors] td:eq(0)").click(function () {

                $("#<%=txtLoginName.ClientID %>").attr("readonly", "readonly");

                if ($(this).closest('tr').find('td:eq(6)').text() != $('#<%=hdnLoginedUserID.ClientID%>').val()) {

                //if ($(this).text() == "") {

                    var jsonResult = {};
                    DoctorID = $(this).closest('tr').find('td:eq(5)').text();
                   
                    var Master = new Object();

                    Master.DoctorID = DoctorID;

                    jsonResult = GetDoctorDetailsByUserID(Master);
                    if (jsonResult != undefined) {
                     
                        BindDoctorControls(jsonResult);
                    }
                }
            });
        });


        function GetDoctorDetailsByUserID(Master) {
            var ds = {};
            var table = {};
            var data = "{'mstrObj':" + JSON.stringify(Master) + "}";
            ds = getJsonData(data, "../MasterAdd/AddDoctor.aspx/BindDoctorDetailsOnEditClick");
            table = JSON.parse(ds.d);
            return table;
        }

        function BindDoctorControls(Records) {
            $.each(Records, function (index, Records) {

                $("#<%=txtLoginName.ClientID %>").val(Records.LoginName);
                $("#<%=txtName.ClientID %>").val(Records.Name);
                $("#<%=txtPhoneNumber.ClientID %>").val(Records.Phone);
                $("#<%=txtEmail.ClientID %>").val(Records.Email);
               
                $("#<%=hdnUserID.ClientID %>").val(Records.UserID);
                $("#<%=hdnDrID.ClientID %>").val(Records.DoctorID);

                $("#DoctorClose").click();
            });

        }


        //-------------------------------- * END : EDIT Button Click * ------------------------- //


        //-------------------------------- * Delete Button Click * ------------------------- //

        $(function () {
            $("[id*=dtgDoctors] td:eq(1)").click(function () {
               
                if ($(this).closest('tr').find('td:eq(6)').text() != $('#<%=hdnLoginedUserID.ClientID%>').val()) { 
                //if ($(this).text() == "") {
                    var DeletionConfirmation = ConfirmDelete();
                    if (DeletionConfirmation == true) {
                        DoctorID = $(this).closest('tr').find('td:eq(5)').text();
                        UserID = $(this).closest('tr').find('td:eq(6)').text();
                        DeleteDoctorByID(DoctorID, UserID);
                        //window.location = "StockIn.aspx?HdrID=" + receiptID;
                    }
                }
            });
        });

        function DeleteDoctorByID(DoctorID, UserID) { //------* Delete Receipt Header by receiptID (using webmethod)

            if (DoctorID != "") {
               
                PageMethods.DeleteDoctorByID(DoctorID,UserID, OnSuccess, onError);
               
                function OnSuccess(response, userContext, methodName) {
                   
                    if (response == false) {

                        $("#DoctorClose").click();

                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;

                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                    }

                    else {

                        $("#<%=hdnUserID.ClientID %>").val("");
                        $("#<%=hdnDrID.ClientID %>").val("");

                        var PageIndx = parseInt(1);

                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {

                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }

                        GetDoctors(PageIndx);

                    }

                }
                function onError(response, userContext, methodName) {

                }

            }
        }

        //-------------------------------- * END : Delete Button Click * ------------------------- //


        //---------------------------------------------------------- * Doctor Grid BinD,Paging,Search *--------------------------------------------------//

        $(function () {
           GetDoctors(1);
        });
        $("[id*=txtSearch]").live("keyup", function () {
            GetDoctors(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetDoctors(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        function GetDoctors(pageIndex) {

            $.ajax({

                type: "POST",
                url: "../MasterAdd/AddDoctor.aspx/ViewAndFilterDoctor",
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
            var Doctors = xml.find("Doctors");
            if (row == null) {
                row = $("[id*=dtgDoctors] tr:last-child").clone(true);
            }
            $("[id*=dtgDoctors] tr").not($("[id*=dtgDoctors] tr:first-child")).remove();
            if (Doctors.length > 0) {

                $.each(Doctors, function () {
                    debugger;
                    var medicine = $(this);
                    
                    $("td", row).eq(0).html($('<img />')
                       .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');


                    $("td", row).eq(1).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(2).html($(this).find("Name").text());
                    $("td", row).eq(3).html($(this).find("Phone").text());
                    $("td", row).eq(4).html($(this).find("Email").text());
                    $("td", row).eq(5).html($(this).find("DoctorID").text());

                  if ($(this).find("UserID").text() == $('#<%=hdnLoginedUserID.ClientID%>').val()) {
                        $("td", row).addClass("selected_row");
                        $("td", row).eq(6).html($(this).find("UserID").text());
                        $("td", row).eq(0).html($('<img />')
                     .attr('src', "" + '../images/Editicon1.png' + "")).removeClass('CursorShow');

                        $("td", row).eq(1).html($('<img />')
                   .attr('src', "" + '../images/Deleteicon1.png' + "")).removeClass('CursorShow');

                    }
                    else {
                        $("td", row).removeClass("selected_row");
                        $("td", row).eq(6).html($(this).find("UserID").text());

                        $("td", row).eq(0).html($('<img />')
                    .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                        $("td", row).eq(1).html($('<img />')
                  .attr('src', "" + '../images/Deleteicon1.png' + "")).removeClass('CursorShow');


                    }


                    $("[id*=dtgDoctors]").append(row);
                    row = $("[id*=dtgDoctors] tr:last-child").clone(true);
                });
                var pager = xml.find("Pager");

                if ($('#txtSearch').val() == '')
                {
                    var GridRowCount = pager.find("RecordCount").text();

                  <%--  $("#<%=lblCaseCount.ClientID %>").text(GridRowCount);--%>

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
                $("[id*=dtgDoctors]").append(empty_row);

                $(".Pager").hide();
            }

            var th = $("[id*=dtgDoctors] th:contains('DoctorID')");
            th.css("display", "none");
            $("[id*=dtgDoctors] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

            var thUserID = $("[id*=dtgDoctors] th:contains('UserID')");
            thUserID.css("display", "none");
            $("[id*=dtgDoctors] tr").each(function () {
                $(this).find("td").eq(thUserID.index()).css("display", "none");
            });


        };


        //Open MOdal Popup
        function OpenModal()
        {
            $('#txtSearch').val('');

            GetDoctors(parseInt(1));
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
                <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                <li id="master" runat="server" class="active"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                <li id="log" runat="server"><a name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>
        <!-- Right Main Section -->
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>Masters<ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>
            </div>
            <div class="icon_box">
                <a class="all_admin_link" data-toggle="modal" data-target="#AllDoctors" onclick="OpenModal();">
                        <span class="tooltip1">
                    <span class="count">
                        <asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label></span>                 
                        <img src="../images/viewdoctor.png" />
                          <span class="tooltiptext1">View All Doctors</span>
                    </span>
                </a>
            </div>
            <div class="right_form tab_right_form">
                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Categories.aspx">Categories</a></li>
                        <li role="presentation"><a href="Units.aspx">Units</a></li>
                        <li role="presentation"><a href="Medicnes.aspx">Medicines</a></li>
                     <%--   <li role="presentation" class="active"><a href="AddDoctor.aspx">Doctor</a></li>--%>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="stock_in">
                            <div class="grey_sec">
                                <ul class="top_right_links">
                                    <li>
                                        <%--<a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick"><span></span>Save</a>--%>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" OnClientClick="return Validation();" />
                                    </li>
                                    <li><a class="new" href="AddDoctor.aspx"><span></span>New</a></li>
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
                            <div class="tab_table">
                                <div class="row field_row">
                                    <div class="col-lg-8">
                                        <label for="name">Name</label><input id="txtName" runat="server" type="text" name="name" />                                     
                                    </div>
                                </div>
                                 <div class="row field_row">
                                    <div class="col-lg-8">
                                        <label for="name">Login Name</label><input id="txtLoginName" onchange="LoginNameCheck(this)" runat="server" type="text" name="lname" />                                      
                                        <span class="tooltip2">
                                              <asp:Image ID="imgWebLnames" runat="server" ImageUrl="~/Images/newfff.png" style="display:none"/>
                                              <span class="tooltiptext2">Login name is Available</span>
                                        </span>
                                          <span class="tooltip2">
                                        <asp:Image ID="errorLnames" runat="server"  ImageUrl="~/Images/newClose.png" style="display:none"/>
                                                  <span class="tooltiptext2">Login name is Unavailable</span>
                                        </span>
                                    </div>
                                </div>
                                <div class="row field_row">
                                    <div class="col-lg-8">
                                        <label for="name">Phone</label><input id="txtPhoneNumber" onkeypress="return isNumber(event)" runat="server" type="text" name="name" pattern="^[0-9+-]*$" />
                                    </div>
                                </div>
                                <div class="row field_row">
                                    <div class="col-lg-8">
                                        <label for="name">Email</label>
                                        
                                        <input id="txtEmail" runat="server" type="email" name="email" onchange="EmailIDDuplicationCheck(this)" />
                                        <%--<input id="txtEmail" runat="server" type="text" name="name" onchange="EmailIDDuplicationCheck(this)" />--%>
                                          <span class="tooltip2">
                                        <asp:Image ID="imgEmailAvailable" runat="server"  ImageUrl="~/Images/newfff.png" style="display:none"/>
                                                <span class="tooltiptext2">Email ID  is Available</span>
                                        </span> 
                                               <span class="tooltip2">
                                        <asp:Image ID="imgEmailUnAvailable" runat="server"  ImageUrl="~/Images/newClose.png" style="display:none"/>
                                                     <span class="tooltiptext2">Email ID  is Unavailable</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal section-->

    <div id="AllDoctors" class="modal fade" role="dialog">
        <div class="modal-dialog" style="min-width: 550px;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-color: #3661C7;">
                    <button type="button" class="close" data-dismiss="modal" id="DoctorClose">&times;</button>
                    <h3 class="modal-title">View All Doctors</h3>
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
                                
                                  <asp:GridView ID="dtgDoctors" runat="server" AutoGenerateColumns="False" CssClass="table" >
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnUpdate" runat="server" Style="border: none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment" OnClick="ImgBtnUpdate_Click" formnovalidate />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" Style="border: none!important" runat="server" ImageUrl="~/images/Deleteicon1.png" OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" formnovalidate />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="Phone" HeaderText="Phone" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="DoctorID" HeaderText="DoctorID"></asp:BoundField>
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

    <asp:HiddenField ID="hdnDrID" runat="server" />
    <asp:HiddenField ID="hdnUserID" runat="server" />
    <asp:HiddenField ID="hdnLoginedUserID" runat="server" /> 

</asp:Content>
