<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="AssignRoles.aspx.cs" Inherits="TheClinicApp1._1.Admin.AssignRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <style type="text/css">

table
{
    width:150px!important;
    border-color:rgb(169, 169, 169);
}


 table td
{
     border-top:none!important;
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

        .modal table td {
    text-align: left;
    height:auto;
    
    }
.modal table td{
    width:30px;
    height:auto;
    padding-left:4px;
}
.modal table td+td{
    width:auto;
    height:auto;
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:14px;
    font-weight:200;
    padding-left:4px;
}
.modal table th {
   
    font-family:Cambria, Cochin, Georgia, Times, Times New Roman, serif;
    font-size:16px;
    
}


.modal table
{
    width:550px!important;
}


</style>


     <%--<link href="../css/TheClinicApp.css" rel="stylesheet" />--%>

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/jquery-1.12.0.min.js"></script>

    <script src="../js/DeletionConfirmation.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>

       var test = jQuery.noConflict();
        test(document).ready(function () {



            if ($('#<%=hdnUserCountChanged.ClientID %>').val() == "True") {
                GetMedicines(1);
                $('#<%=hdnUserCountChanged.ClientID %>').val('False');
            }

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });



            $('.alert_close').click(function () {


                $(this).parent(".alert").hide();

            });


        });

    </script>

      <%--  //------------- AUTOFILL SCRIPT ---------%>
    <script src="../js/jquery-1.8.3.min.js"></script>
    
    <script src="../js/ASPSnippets_Pager.min.js"></script>

    <script type="text/javascript">

        var UniqueID = '';


        //$(function () {
        //    $("[id*=dtgViewAllUserInRoles] td:eq(0)").click(function () {



        //        var DeletionConfirmation = ConfirmDelete();

        //        if (DeletionConfirmation == true) {

        //            debugger;

        //            UniqueID = $(this).closest('tr').find('td:eq(3)').text();


        //            window.location = "AssignRoles.aspx?UniqueID=" + UniqueID;


                    


        //        }
        //    });


        //});


  


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
                url: "../Admin/AssignRoles.aspx/GetMedicines",
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
                row = $("[id*=dtgViewAllUserInRoles] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllUserInRoles] tr").not($("[id*=dtgViewAllUserInRoles] tr:first-child")).remove();
            if (Medicines.length > 0) {
                $.each(Medicines, function () {

                    debugger;

                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');
                   


                    //$("td", row).eq(0).html($('<img />')
                    //                      .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                   

                    $("td", row).eq(0).html($(this).find("Name").text());

                    $("td", row).eq(1).html($(this).find("Role").text());
                   
                    $("td", row).eq(2).html($(this).find("UniqueID").text());
                  

                    $("[id*=dtgViewAllUserInRoles]").append(row);
                    row = $("[id*=dtgViewAllUserInRoles] tr:last-child").clone(true);
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
                $("[id*=dtgViewAllUserInRoles]").append(empty_row);
            }



            var th = $("[id*=dtgViewAllUserInRoles] th:contains('UniqueID')");
            th.css("display", "none");
            $("[id*=dtgViewAllUserInRoles] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };

        $(function() {
            $('#chkveg').multiselect({
                includeSelectAllOption: true
            });
        });

        </script>


         <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" ><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
          <li id="admin" class="active" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
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
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li></ul> </div>
          
              <div class="icon_box">

 <a class="all_registration_link" data-toggle="modal" data-target="#AssignedRoles" ><span title="View Assigned Roles" data-toggle="tooltip" data-placement="left" ><img src="../images/AssignUser.png" /></span></a>


                

            </div>

<div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Admin.aspx">Create User</a></li>
                        <li role="presentation" class="active"><a href="AssignRoles.aspx">Assign Roles</a></li>
                        
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane active" id="stock_in">
                            <div class="grey_sec">
                              <%--  <div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links">
                                    <li><a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick"><span></span>Save</a></li>
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

                                <div class="row field_row">  
      <div class="col-lg-4">
      <label for="marital">User</label>
          <asp:DropDownList ID="ddlUsers" runat="server" Width="100%" Height="40px" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
             
          </asp:DropDownList>
           <asp:RequiredFieldValidator
             ID="RequiredFieldValidator2"
             runat="server"
             ControlToValidate="ddlUsers"
             InitialValue="--Select--"
             ErrorMessage="* Please select an item."
             ForeColor="Red"
            
             >
        </asp:RequiredFieldValidator>



      </div>
      <div class="col-lg-4">
       <label for="marital">Role</label> <asp:CheckBoxList ID="chklstRoles" runat="server" CssClass="mui-checkbox--inline checkboxlist_nowrap"  ></asp:CheckBoxList>

           
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
        <h3 class="modal-title">View All Assigned Roles</h3>
      </div>
      <div class="modal-body"  style="overflow-y: scroll; overflow-x: hidden;max-height:500px;">
       <%--<iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>--%>
         <div class="col-lg-12" style="height:500px">

            <asp:GridView ID="dtgViewAllUserInRoles" runat="server" AutoGenerateColumns="False" class="table" >
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                            <Columns>

                              <%--   <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Image ID="img1" runat="server" 
                                                            OnClientClick="ConfirmDelete()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                   <asp:BoundField DataField="Name" HeaderText="Name">

                                </asp:BoundField>


                                <asp:BoundField DataField="Role" HeaderText="Assigned Role">
                                   
                                </asp:BoundField>

                              
                                   
                                  
                                     <asp:BoundField DataField="UniqueID" HeaderText="UniqueID">
                                   
                                   

                                </asp:BoundField>
                            </Columns>
                         
                           
                        </asp:GridView>


            <div class="Pager"></div>
    </div>
    </div>
         
         
    </div>

  </div>
        </div>

      <asp:HiddenField ID="hdnUserCountChanged" runat="server" />
</asp:Content>
