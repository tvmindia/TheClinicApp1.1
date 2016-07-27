<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/popup.Master" AutoEventWireup="true" CodeBehind="Units.aspx.cs" Inherits="TheClinicApp1._1.MasterAdd.Units"  %>
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
    <script src="../js/jquery.tablePagination.0.1.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
    <script src="../js/Messages.js"></script>
  
    <script>

         function Validation() {
             debugger;
             if (($('#<%=txtDescription.ClientID%>').val().trim() == "")) {
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

      $(document).ready(function () {       
  

          $('.alert_close').click(function () {
              $(this).parent(".alert").hide();
          });

          $('.nav_menu').click(function () {
              $(".main_body").toggleClass("active_close");
          });          

          $('[data-toggle="tooltip"]').tooltip();

      });

         //---------------* Function to check  Unit duplication *--------------//

         function CheckUnitDuplication(txtCategoryName) {
             
             var name = document.getElementById('<%=txtDescription.ClientID %>').value;
             name = name.trim();

             if (name != "") {

                 //name = name.replace(/\s/g, '');
                 PageMethods.ValidateUnit(name, OnSuccess, onError);

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

    <script src="../js/jquery-1.8.3.min.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>
    <link href="../css/TheClinicApp.css" rel="stylesheet" />

    <script>

        var UnitID = '';

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
            $("[id*=dtgViewAllUnits] td:eq(0)").click(function () {

                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

                document.getElementById('<%=imgWebLnames.ClientID %>').style.display = "none";
                document.getElementById('<%=errorLnames.ClientID %>').style.display = "none";

                if ($(this).text() == "") {

                    var jsonResult = {};
                    UnitID = $(this).closest('tr').find('td:eq(3)').text();

                    var Master = new Object();

                    Master.UnitID = UnitID;

                    jsonResult = GetUnitDetailsByUnitID(Master);
                    if (jsonResult != undefined) {
                        BindUnitControls(jsonResult);
                    }
                }
            });
        });

        function GetUnitDetailsByUnitID(Master) {
            var ds = {};
            var table = {};
            var data = "{'mstrObj':" + JSON.stringify(Master) + "}";
            ds = getJsonData(data, "../MasterAdd/Units.aspx/BindUnitDetailsOnEditClick");
            table = JSON.parse(ds.d);
            return table;
        }


        function BindUnitControls(Records) {
            $.each(Records, function (index, Records) {

                $("#<%=txtDescription.ClientID %>").val(Records.Description);

                $("#<%=hdnUnitID.ClientID %>").val(Records.UnitID);

                $("#UnitClose").click();
            });

        }
        
        //-------------------------------- *END : EDIT Button Click * ------------------------- //

        //-------------------------------- * Delete Button Click * ------------------------- //

        $(function () {
            $("[id*=dtgViewAllUnits] td:eq(1)").click(function () {
                document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

                if ($(this).text() == "") {
                    var DeletionConfirmation = ConfirmDelete();
                    if (DeletionConfirmation == true) {
                        UnitID = $(this).closest('tr').find('td:eq(3)').text();
                        DeleteUnitByID(UnitID);
                        //window.location = "StockIn.aspx?HdrID=" + receiptID;
                    }
                }
            });
        });

        function DeleteUnitByID(UnitID) { //------* Delete Receipt Header by receiptID (using webmethod)

            if (UnitID != "") {

                PageMethods.DeleteUnitByID(UnitID, OnSuccess, onError);

                function OnSuccess(response, userContext, methodName) {
                    debugger;
                    if (response == false) {
                        $("#UnitClose").click();
                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;
                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
                    }

                    else {
                        debugger;
                        $("#<%=hdnUnitID.ClientID %>").val("");
                        var PageIndx = parseInt(1);
                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {
                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }
                        GetUnits(PageIndx);                
                    }                
                 
                }
                function onError(response, userContext, methodName) {
                }
            }
        }

        //-------------------------------- * END : Delete Button Click * ------------------------- //

        $(function () {
           
            GetUnits(1);
        });
        $("[id*=txtSearch]").live("keyup", function () {
            
            GetUnits(parseInt(1));
        });
        $(".Pager .page").live("click", function () {
            GetUnits(parseInt($(this).attr('page')));
        });
        function SearchTerm() {
            return jQuery.trim($("[id*=txtSearch]").val());
        };
        function GetUnits(pageIndex) {

            $.ajax({

                type: "POST",
                url: "../MasterAdd/Units.aspx/ViewAndFilterUnits",
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
            debugger;
            $(".Pager").show();
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var Units = xml.find("Units");
            if (row == null) {
                row = $("[id*=dtgViewAllUnits] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllUnits] tr").not($("[id*=dtgViewAllUnits] tr:first-child")).remove();
            if (Units.length > 0) {


                $.each(Units, function () {
                   
                    var medicine = $(this);
                    
                    $("td", row).eq(0).html($('<img />')
                       .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(1).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(2).html($(this).find("Description").text());
                    $("td", row).eq(3).html($(this).find("UnitID").text());
                   

                    $("[id*=dtgViewAllUnits]").append(row);
                    row = $("[id*=dtgViewAllUnits] tr:last-child").clone(true);
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
                $("[id*=dtgViewAllUnits]").append(empty_row);
                $(".Pager").hide();
            }

            var th = $("[id*=dtgViewAllUnits] th:contains('UnitID')");
            th.css("display", "none");
            $("[id*=dtgViewAllUnits] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };


        function OpenModal() {
            $('#txtSearch').val('');
            GetUnits(parseInt(1));

        }

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
          <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
              <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
         <li id="master" runat="server" class="active"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
             <li id="log" runat="server"><a name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
         </ul>
         
         <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
             <div class="tagline">
                 <a class="nav_menu">Menu</a>
                 Masters
                 <ul class="top_right_links">
                     <li>
                         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label>
                     </li>
                     <li>
                         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click"  formnovalidate />
                     </li>
                 </ul>
             </div>
          
              <div class="icon_box">
                  <a class="all_assignrole_link" data-toggle="modal" data-target="#AllUnits" onclick="OpenModal();">
                      <span class="tooltip1">
                          <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label></span>
                          <img src="../images/units.png" />
                          <span class="tooltiptext1">View All Units</span>
                      </span>
                  </a>     
            </div>
             <div class="right_form tab_right_form">
                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Categories.aspx">Categories</a></li>
                        <li role="presentation" class="active"><a href="Units.aspx">Units</a></li>
                        <li role="presentation" ><a href="Medicnes.aspx">Medicines</a></li>
                        <li role="presentation"   ><a href="AddDoctor.aspx">Doctor</a></li>
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
                                    <li>
                                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btSave_ServerClick"  OnClientClick="return Validation(); " />
                                    </li>
                                    <li><a class="new"  href="Units.aspx"><span></span>New</a></li>
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
                                        <label for="name">Unit</label><input id="txtDescription" runat="server" type="text" name="name"  onchange="CheckUnitDuplication();"  />
                                        <span class="tooltip2">
                                            <asp:Image ID="imgWebLnames" runat="server"  ImageUrl="~/Images/newfff.png" style="display:none" />
                                            <span class="tooltiptext2">Unit is Available</span>
                                        </span>
                                        <span class="tooltip2">
                                            <asp:Image ID="errorLnames" runat="server"  ImageUrl="~/Images/newClose.png" style="display:none"/>
                                            <span class="tooltiptext2">Unit is Unavailable</span>
                                        </span>
                                    </div>
                                    <%--<div class="col-lg-8">
                                        <label for="name">Code</label><input id="txtCOde" runat="server" type="text" name="name" required  />
                                        </div>--%> 

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
</div>

         </div>
 </div>
    
    <div id="AllUnits" class="modal fade" role="dialog">
          <div class="modal-dialog" style="min-width:550px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="border-color:#3661C7;">  
          <button type="button" class="close" data-dismiss="modal" id="UnitClose">&times;</button>     
        <h3 class="modal-title">View All Units</h3>
      </div>
      
         <div class="modal-body"  style="overflow-y: scroll; overflow-x: hidden;max-height:500px;">
       
         <div class="col-lg-12" style="height:480px">

                 <div class="col-lg-12" style="height:40px">
              <div class="search_div">
              <input class="field1" type="text" placeholder="Search with Name.." id="txtSearch" />
                  <input class="button3" type="button" value="Search" disabled/>
                  </div>
          </div>
             

             <div class="col-lg-12" style="height:400px">
            

                  <asp:GridView ID="dtgViewAllUnits" runat="server" AutoGenerateColumns="False"   DataKeyNames="UnitID"  class="table" >
                        
                        <Columns>
                          
                            <asp:TemplateField ItemStyle-Width="20%"  >
                                    <ItemTemplate>
                                        
                                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" style="border:none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment"  formnovalidate OnClick="ImgBtnUpdate_Click"   />
                                    </ItemTemplate>
                                </asp:TemplateField>



                       <asp:TemplateField HeaderText="" ItemStyle-Width="20%" >
             <ItemTemplate>
              <asp:ImageButton ID="ImgBtnDelete" style="border:none!important" runat="server" ImageUrl="~/images/Deleteicon1.png"  OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click"  formnovalidate/>
               </ItemTemplate>
                </asp:TemplateField>



                            <asp:BoundField DataField="Description" HeaderText="Unit" ItemStyle-CssClass="Match">
                               
                            </asp:BoundField>
                           
                                 <asp:BoundField DataField="UnitID" HeaderText="UnitID">
                               
                            </asp:BoundField>
                           

                               <%--<asp:BoundField DataField="Code" HeaderText="Code">
                               
                            </asp:BoundField>--%>

                        

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
    
    <asp:HiddenField ID="hdnUnitID" runat="server" />

</asp:Content>
