<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AssignRoles.aspx.cs" Inherits="TheClinicApp1._1.Admin.AssignRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<style type="text/css">

 #chkDiv table
{
    width:150px!important;
    border-color:rgb(169, 169, 169);
    border:none!important;   
}
#chkDiv table td
{
     border-top:none!important;
     height:31px!important;
}
 label {
    color: #666666;
    display: block;
    font-family: "raleway-semibold";
    font-size: 16px;
    font-weight: 500;
    line-height: 18px;
    margin: 0 0 5px;

    padding-left: 15px;
    text-indent: -15px;
}
 .checkboxes label {
    display: inline!important;
    float: right!important;
}
.checkboxes input {
    vertical-align: central!important;

}
.checkboxes label span {
    vertical-align: central!important;
}
.checkboxlist_nowrap tr td label
{
    white-space:nowrap;
    overflow:hidden;
    width:100%;
}

/*.modal table td {
    text-align: left;
    height:auto;
    border-top:1px solid lightgrey!important;
    border:none !important;
    }

.modal table td{
    width:30px;
    height:auto;
     font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    padding-left:4px;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
     border:none !important;
   
}

.modal table td+td{
    width:auto;
    height:auto;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
     border:none !important;
}

.modal table th {
   
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
    
}


.modal table
{
    width:525px!important;
   
}*/
@media (max-width:1168px)
{
    .search_div input.field1 {
    width: 448px;
    }
    .search_div input.button3 {
        left: 348px;
    }
    .modal table{   
    width: 448px!important;
   
    }
}

.button1{
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
        font-family:'raleway-semibold';
        min-width: 83px;
        background-color:#abd357 ;
        -webkit-border-radius: 2px;
        -moz-border-radius: 2px;
        border-radius: 2px;
        text-indent: 20px;
        background-position-x:5px;

        color: inherit;

    }
     select
        {
            color:black!important;
        }
</style>


    <%-- <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />--%>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <%--<script src="../js/bootstrap.min.js"></script>--%>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>
    <script src="../Scripts/Common/Common.js"></script>
    <script>
        function Validation() {
           
            if (($('#<%=ddlUsers.ClientID%>').val().trim() == "--Select--")) {


                var lblclass = Alertclasses.danger;
                var lblmsg = msg.CompulsorySelect;
                var lblcaptn = Caption.Confirm;

                ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                return false;
            }
            else {
                return true;
            }

        }
    </script>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
                .modal table thead {
                        background-color: #5681e6;
                        text-align: center;
                        color: white;
                }
        </style>

    <script>

       var test = jQuery.noConflict();
        test(document).ready(function () {            

           
            $("[id*=chklstRoles] input[type=checkbox]").click(function () {
                           
                debugger;
$("#<%=hdnSelectedRoles.ClientID %>").val("");
$("#<%=hdnUnSelectedRoles.ClientID %>").val("");

                var CHK = document.getElementById("<%=chklstRoles.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");

                
                for (var i=0;i<checkbox.length;i++)
                {
                    debugger;
                    var SelectedRoles =   $("#<%=hdnSelectedRoles.ClientID %>").val();
                    var UnSelectedRoles = $("#<%=hdnUnSelectedRoles.ClientID %>").val();

                    var roleID = checkbox[i].value;
                             
                    if (checkbox[i].checked) {
                        $("#<%=hdnSelectedRoles.ClientID %>").val(SelectedRoles+"|"+roleID);    
                    }  
                    <%--           
                    else
                    {
                        $("#<%=hdnUnSelectedRoles.ClientID %>").val(UnSelectedRoles+"|"+roleID);
                    }--%>
                                
                }

                for (var i=0;i<checkbox.length;i++)
                {
                    debugger;
                    var SelectedRoles =   $("#<%=hdnSelectedRoles.ClientID %>").val();
                    var UnSelectedRoles = $("#<%=hdnUnSelectedRoles.ClientID %>").val();

                    var roleID = checkbox[i].value;
                             
                    if (checkbox[i].checked == false) {
                        $("#<%=hdnUnSelectedRoles.ClientID %>").val(UnSelectedRoles+"|"+roleID); 
                    }  
                               
                   
                                
                }
            });








<%--            if ($('#<%=hdnUserCountChanged.ClientID %>').val() == "True") {
                GetMedicines(1);
                $('#<%=hdnUserCountChanged.ClientID %>').val('False');
            }--%>

            //$('[data-toggle="tooltip"]').tooltip();


            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });
            
            $('.alert_close').click(function () {
                $(this).parent(".alert").hide();
            });

            //$('.table').tablePagination({});

            //var rows = $('#<%=dtgViewAllUserInRoles.ClientID%> tr').not('thead tr');

            //$('#txtSearchAssignedRole').keyup(function () {             
            //    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase().split(' ');
            //    rows.hide().filter(function () {
            //        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
            //        var matchesSearch = true;
            //        $(val).each(function (index, value) {
            //            matchesSearch = (!matchesSearch) ? false : ~text.indexOf(value);
            //        });
            //        return matchesSearch;
            //    }).show();

            //    //-------------------------------No records found.-----------------------------------------// 
            //    debugger;
            //    //finding the row of html table displaying while searching 
            //    var numOfVisibleRows = $('tbody tr').filter(function () {
            //        return $(this).css('display') !== 'none';
            //    }).length;

            //    //here number of rows while no records found is 3
            //    if (numOfVisibleRows == 3) {
            //        debugger;
            //        $('#norows').remove();
            //        var bodyId = "tbdy";
            //        $('table thead').attr('id', bodyId);
            //        var textdis = "No records found.";
            //        var html = '<div id="norows" style="width:100%; padding-left: 200px;">' + textdis + '</div>';
            //        $('#tbdy').after(html);
            //    }
            //    else {
            //        $('#norows').remove();
            //    }
            //    //----------------------------------No records found.--------------------------------------//
            //    $('#tablePagination').hide();
            //    if (val == "") {                   
            //        $('.table').tablePagination({});
            //        $('#tablePagination').show();
            //    }
            //});
        });
    </script>

      <%--  //------------- AUTOFILL SCRIPT ---------%>
   <%-- <script src="../js/jquery-1.8.3.min.js"></script>    
    <script src="../js/ASPSnippets_Pager.min.js"></script>--%>

     <script src="../js/jquery-1.3.2.min.js"></script>
   <script src="../js/jquery-1.12.0.min.js"></script>
   <script src="../js/jquery-ui.js"></script> 
        <script src="../js/bootstrap.min.js"></script>
    
     <link href="../css/TheClinicApp.css" rel="stylesheet" />
      <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>

    <script type="text/javascript">
       
      

        var UniqueID = '';
        var cellinitial = '';
        var isPostBack = <%=Convert.ToString(Page.IsPostBack).ToLower()%>;

  
      //  -------------*SEARCH AND PAGING SCRIPT------------------------//

        $(function () {

           

            GetAssignedRoles(1);

            cellinitial = $("[id*=chklstRoles] td").eq(0).clone(true);





            //if (isPostBack == false) {
            //    BindUsers();
            //}
                
                //BindRoles();
            
        });

        $("[id*=txtSearch]").live("keyup", function () {
            GetAssignedRoles(parseInt(1));
        });

        $(".Pager .page").live("click", function () {
            GetAssignedRoles(parseInt($(this).attr('page')));
        });

        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };

        function GetAssignedRoles(pageIndex) {
            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

            $.ajax({
                type: "POST",
                url: "../Admin/AssignRoles.aspx/ViewAndFilterAssignedRoles",
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
        function OnSuccess(response)
        {

            $(".Pager").show();
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var AssignedRoles = xml.find("AssignedRoles");
            if (row == null) {
                row = $("[id*=dtgViewAllUserInRoles] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllUserInRoles] tr").not($("[id*=dtgViewAllUserInRoles] tr:first-child")).remove();
            if (AssignedRoles.length > 0) {
                $.each(AssignedRoles, function () {
                    
                   
                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');
                   


                    //$("td", row).eq(0).html($('<img />')
                    //                      .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                   

                    $("td", row).eq(0).html($(this).find("FirstName").text());

                    if ($(this).find("RoleNames").text() != "") {
                        $("td", row).eq(1).html($(this).find("RoleNames").text());
                    }

                    else
                    {
                        $("td", row).eq(1).html('No roles');
                    }
                   
                    $("td", row).eq(2).html($(this).find("UserID").text());
                  

                    $("[id*=dtgViewAllUserInRoles]").append(row);
                    row = $("[id*=dtgViewAllUserInRoles] tr:last-child").clone(true);
                });

                var pager = xml.find("Pager");
               
                    var GridRowCount = pager.find("RecordCount").text();

                    $("#<%=lblCaseCount.ClientID %>").text(GridRowCount);
              
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
                $("[id*=dtgViewAllUserInRoles]").append(empty_row);
                $(".Pager").hide();
            }



            var th = $("[id*=dtgViewAllUserInRoles] th:contains('UserID')");
            th.css("display", "none");
            $("[id*=dtgViewAllUserInRoles] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };


        function OpenModal() {

            GetAssignedRoles(parseInt(1));

        }

        function ChangeClinic(e) {
            if ($("#<%=ddlClinic.ClientID%> option:selected").text() != "--Select Clinic--") {
                $("#<%=hdnClinicID.ClientID %>").val(e.value);
               
            }

            else {
                $("#<%=hdnClinicID.ClientID %>").val("");

            }

            GetAssignedRoles(parseInt(1));
            BindUsers();
            BindRoles();

            $("#<%=Errorbox.ClientID %>").css("display", "none"); 
        }

        function  ChangeUser(e)
        {
            debugger;

            if ($("#<%=ddlUsers.ClientID%> option:selected").text() != "--Select--") {
               
                //selected="selected" 
              var selecteduserID =  $("#<%=ddlUsers.ClientID%> option:selected").val();


                $("#<%=hdnSelectedUservalue.ClientID%>").val(selecteduserID);
               
                debugger;
                
                var RoleAssign = new Object();
                RoleAssign.UserID =$("#<%=ddlUsers.ClientID%> option:selected").val();
                var AssignedRoles ={};
               
                AssignedRoles=  GetAssignedRolesByUserID(RoleAssign);
                var CHK = document.getElementById("<%=chklstRoles.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");

                
                for (var i=0;i<checkbox.length;i++)
                {
                    checkbox[i].checked = false;
                }

                if (AssignedRoles != undefined) {

                    Records = AssignedRoles;

                    $.each(Records, function (index, Records) {

                        for (var i=0;i<checkbox.length;i++)
                        {
                            if (Records.RoleID == checkbox[i].value) 
                            {
                                checkbox[i].checked = true;

                            }

                        }

                    });
                }


            }



            //--Select--

            $("#<%=Errorbox.ClientID %>").css("display", "none"); 
        }


        function GetAssignedRolesByUserID(RoleAssign) {
            var ds = {};
            var table = {};
            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();

            var data = "{'roleObj':" + JSON.stringify(RoleAssign) + ",'clinicID':" + JSON.stringify(clinicID) + "}";
            ds = getJsonData(data, "../Admin/AssignRoles.aspx/GetAssignedRoles");
            table = JSON.parse(ds.d);
            return table;
        }



        //$(function () {

        function BindUsers()
        {
            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();
            $.ajax({
                type: "POST",
                url: "../Admin/AssignRoles.aspx/GetUsers",
                data: '{clinicID: "' + clinicID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlUsers = $("[id*=ddlUsers]");
                    ddlUsers.empty().append('<option value="0">--Select--</option>');
                    $.each(r.d, function () {
                        ddlUsers.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });

        }

       
        function BindRoles() {

         

            var clinicID = $("#<%=hdnClinicID.ClientID %>").val();
            $.ajax({
                type: "POST",
                url: "../Admin/AssignRoles.aspx/GetRoles",
                data: '{clinicID: "' + clinicID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    debugger;
                    var Roles = r.d;
                    var repeatColumns = parseInt("<%=chklstRoles.RepeatColumns%>");
                    if (repeatColumns == 0) {
                        repeatColumns = 1;
                    }

                        var    cell = $("[id*=chklstRoles] td").eq(0).clone(true);
                  
                        if (cell.length == 0) {
                            cell = cellinitial;
                        }

                        $("[id*=chklstRoles] tr").remove();
                    
                        $.each(Roles, function (i) {

                        var row;
                        if (i % repeatColumns == 0) {
                            row = $("<tr />");
                            $("[id*=chklstRoles] tbody").append(row);
                        } else {
                            row = $("[id*=chklstRoles] tr:last-child");
                        }

                        var checkbox = $("input[type=checkbox]", cell);

                        //Set Unique Id to each CheckBox.
                        checkbox[0].id = checkbox[0].id.replace("0", i);

                        //Give common name to each CheckBox.
                        //checkbox[0].name = "RoleID";

                        //Set the CheckBox value.
                        checkbox.val(this.Value);
                        checkbox.prop('checked', false);

                        var label = cell.find("label");
                        if (label.length == 0) {
                            label = $("<label />");
                        }

                        //Set the 'for' attribute of Label.
                        label.attr("for", checkbox[0].id);

                        //Set the text in Label.
                        label.html(this.Text);

                        //Append the Label to the cell.
                        cell.append(label);

                        //Append the cell to the Table Row.
                        row.append(cell);
                        cell = $("[id*=chklstRoles] td").eq(0).clone(true);
                    });


                      


<%--
                            if ($(this).is(":checked")) 
                            {   
                                var SelectedRoles =   $("#<%=hdnSelectedRoles.ClientID %>").val();
                                
                                var cell = $(this).parent();
                               
                                var label = cell.find("label");

                                $("#<%=hdnSelectedRoles.ClientID %>").val(SelectedRoles+"|"+label.text());    

                            }
                            --%>
                       
                         
                          

                            
                            //var cell = $(this).parent();
                            //var hidden = cell.find("input[type=hidden]");
                            //var label = cell.find("label");
                            //if ($(this).is(":checked")) {
                            //    //Add Hidden field if not present.
                            //    if (hidden.length == 0) {
                            //        hidden = $("<input type = 'hidden' />");
                            //        cell.append(hidden);
                            //    }
                            //    hidden[0].name = "RoleName";
 
                            //    //Set the Hidden Field value.
                            //    hidden.val(label.text());
                            //    cell.append(hidden);
                            //} 
                            //else {
                            //    cell.remove(hidden);
                            //}
                       


                }
            });

        }

        </script>


         <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" ><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
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
         
         <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
        Administrator <ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22" ></asp:Label></li><li>
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li></ul> </div>
          
              <div class="icon_box">
                   <a class="all_assignrole_link" data-toggle="modal" data-target="#AssignedRoles" onclick="OpenModal();">
                         <span class="tooltip1">
                             <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label>
                             </span>
                             <img src="../images/AssignUser.png" />
                             <span class="tooltiptext1">View Assigned Roles</span>
                         </span>           
                   </a>
            </div>

               <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Admin.aspx">Create User</a></li>
                        <li role="presentation" class="active"><a href="AssignRoles.aspx">Assign Roles</a></li>
                        <li role="presentation" runat="server" id="liSAdmin" visible="false"><a href="SAdmin.aspx">SAdmin</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="stock_in">
                            <div class="grey_sec">

                                <asp:DropDownList ID="ddlClinic" onchange="ChangeClinic(this)" runat="server" CssClass="drop" Width="250px" Style="font-family: Arial, Verdana, Tahoma;">
                                     </asp:DropDownList>
                                

                              <%--  <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links">
                                    <li>
                                        <%--<a  id="btSave" runat="server" CssClass="button1" onserverclick="btSave_ServerClick"><span></span>Save</a>--%>

                                          <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btSave_ServerClick" OnClientClick="return Validation();" />

                                    </li>
                                    <li><a class="new"  href="AssignRoles.aspx"><span></span>New</a></li>
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

                            

                            

                               
      <%--<div class="col-lg-4">
      <label for="marital">User</label>
      <asp:DropDownList ID="ddlUsers" runat="server" Width="100%" Height="40px" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
             
          </asp:DropDownList>   

      </div>--%>
      <%--<div class="col-lg-4">
       <label for="marital">Role</label> <asp:CheckBoxList ID="chklstRoles" runat="server" CssClass="mui-checkbox--inline checkboxlist_nowrap"  ></asp:CheckBoxList>

            </div>--%>

        <%--  <asp:DropDownList ID="ddlRoles" runat="server" Width="100%"  multiple="multiple">
             
          </asp:DropDownList>--%>

           <%--<asp:RequiredFieldValidator
             ID="RequiredFieldValidator1"
             runat="server"
             ControlToValidate="chklstRoles"
             InitialValue="--Select--"
             ErrorMessage="* Please select an item."
             ForeColor="Red"
            
             >
        </asp:RequiredFieldValidator>--%>

     


<div class="">     


<div class="row field_row">  
    <div style="height:40px;"></div>
      <div class="col-lg-5">
         <label for="name">User</label>	          
			 <asp:DropDownList ID="ddlUsers" runat="server" Width="250px" Height="31px" CssClass="drop" onchange="ChangeUser(this)" >  
                 
                 <%--AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged"--%>
                            
          </asp:DropDownList>
		</div>

  
    <div class="col-lg-4" >  
        <label for="name">Role</label>	
        <div class="checkbox checkboxlist col-sm-4" id="chkDiv" >
			 <asp:CheckBoxList ID="chklstRoles" runat="server"  >
               
			 </asp:CheckBoxList>
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

    <div id="AssignedRoles" class="modal fade" role="dialog">
          <div class="modal-dialog" style="min-width:550px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="border-color:#3661C7;">  
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
        <h3 class="modal-title">View All AssignedRoles</h3>
      </div>
      
        <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">

                    <div class="col-lg-12" style="height: 480px">

                        <div class="col-lg-12" style="height: 40px">
                            <div class="search_div">
                                <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchAssignedRole" />
                                <input class="button3" type="button" value="Search" />
                            </div>
                        </div>


                        <div class="col-lg-12" style="height: 400px">
                            
  <asp:GridView ID="dtgViewAllUserInRoles" runat="server" AutoGenerateColumns="False" >
             
                            <Columns>    
                              <%--   <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Image ID="img1" runat="server" 
                                                            OnClientClick="ConfirmDelete()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                   <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-CssClass="Match">
                                </asp:BoundField>
                                <asp:BoundField DataField="Role" HeaderText="Assigned Role" ItemStyle-CssClass="Match">                                   
                                </asp:BoundField>                           
                                   
                                   <asp:BoundField DataField="UserID" HeaderText="UserID">   
                                </asp:BoundField>
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
      <asp:HiddenField ID="hdnUserCountChanged" runat="server" />
    <asp:HiddenField ID="hdnClinicID" runat="server" Value="" /> <%----- *ClinicID generally accessed from UA, If clinic changed ClinicID will be saved to this hiddenfield  *--%>
     <asp:HiddenField ID="hdnSelectedUservalue" runat="server" Value="" />
     <asp:HiddenField ID="hdnSelectedRoles" runat="server" Value="" /> 
    <asp:HiddenField ID="hdnUnSelectedRoles" runat="server" Value="" />

</asp:Content>
