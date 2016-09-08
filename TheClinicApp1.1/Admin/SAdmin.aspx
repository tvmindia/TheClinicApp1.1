<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="SAdmin.aspx.cs" Inherits="TheClinicApp1._1.Admin.SAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../js/bootstrap-multiselect.js"></script>
    <script src="../js/ASPSnippets_Pager.min.js"></script>
    <script src="../js/Messages.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
         <div class="right_part" style="background-color:white;">
         <div class="tagline">
         <a class="nav_menu">Menu</a>Create Group and Clinics<ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22" ></asp:Label></li><li>
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li></ul> </div>
          
              <div class="icon_box">
                   <a class="all_assignrole_link" data-toggle="modal" data-target="#AllClinics" onclick="OpenModal();">
                         <span class="tooltip1">
                             <span class="count" style="background:#bb33d4;"><asp:Label ID="lblClinicCount" runat="server" Text="0"></asp:Label>
                             </span>
                             <img src="../images/ClinicEdit.png" />
                             <span class="tooltiptext1">View Clinics</span>
                         </span>           
                   </a>
            </div>

               <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation"><a href="Admin.aspx">Create User</a></li>
                        <li role="presentation" ><a href="AssignRoles.aspx">Assign Roles</a></li>
                        <li role="presentation" runat="server" id="liSAdmin" class="active"><a href="SAdmin.aspx">SAdmin</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content" style="border:0px;">

                        <div role="tabpanel" class="tab-pane active" id="stock_in">
                             <div class="grey_sec">
                                <%--<div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links">
                                    <li>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" OnClientClick="return Validation();" />
                                    </li>
                                    <li><a class="new" href="SAdmin.aspx"><span></span>New</a></li>
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
                            <div class="col-lg-12" style="margin-top:2%">
                                 <div class="col-lg-8">
                                     <div style="margin-bottom:3%" id="rdbdiv">
                                         <label for="name">
                                             <asp:RadioButton ID="rdoDoctor" runat="server" GroupName="Doctor" Text="New Group" CssClass="checkbox-inline" onclick="hello(1)" />
                                                    <asp:RadioButton ID="rdoNotDoctor" runat="server" GroupName="Doctor" Text="Existing Group" CssClass="checkbox-inline" onclick="hello(2)" Checked="true" />
                                         </label>
                                         <asp:HiddenField ID="hdnGroupselect" ClientIDMode="Static" runat="server" Value="Exist" />
                                         </div>
                                     <div style="margin-top:1%;margin-bottom:1%;display:none;" id="NewGroup">
                                     <label for="location">Group Name</label><input id="txtGroupName" runat="server" type="text" name="name"/>
                                     </div>
                                     <div style="margin-bottom:1%" id="ExistingGroup">
                                     <label for="name">Select Group</label>	          
			                         <asp:DropDownList ID="ddlGroup" runat="server" Width="100%" Height="31px" CssClass="drop">             
                                     </asp:DropDownList>
                                         </div>
                                     <div style="margin-bottom:1%">
		                         <label for="name">Clinic Name</label><input id="txtClinicName" runat="server" type="text" name="name" onchange="LoginNameCheck(this)" />
                                 </div>
                                         <div style="margin-bottom:1%">
                                 <label for="location">Clinic Location</label><input id="txtLocation" runat="server" type="text" name="name"/>
                                 </div>
                                     <div style="margin-bottom:1%">
                                     <label for="address">Clinic Address</label><input id="txtAddress" runat="server" type="text" name="name"/>
                                 </div>
                                     <div style="margin-bottom:1%">
                                     <label for="phone">Clinic Phone</label><input id="txtPhone" runat="server" type="text" name="name"/>
                                 </div>
                                     <div style="margin-bottom:1%" id="DivMultiRoles">
                                         <label for="phone">Select Required Roles</label>
                                         <asp:ListBox ID="lstFruits" runat="server" SelectionMode="Multiple">
                                            <asp:ListItem Text="Administrator" Value="1" />
                                             <asp:ListItem Text="Pharmacist" Value="2" />
                                               <asp:ListItem Text="Doctor" Value="3" />
                                                  <asp:ListItem Text="Receptionist" Value="4" />
                                        </asp:ListBox>
                                         </div>
                                 </div>    
                                
                                <div class="col-lg-3" >
                                    <div style="margin:4% 4% 4% 4%">
                                     <img class="" id="LogPic" src="../images/Uploadlogo.PNG" runat="server" />
                                    <div class="upload">
                                    <label class="control-label">Upload Big Logo</label>

                                            <asp:FileUpload ID="fluplogo" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload(this);showpreview(this);" />
                                        </div>
                                    </div>
                                    <div style="margin:4% 4% 4% 4%">
                                     <img class="" id="LogsmallPic" src="../images/Uploadlogo.PNG" runat="server" />
                                    <div class="upload">
                                    <label class="control-label">Upload Small Logo</label>

                                            <asp:FileUpload ID="fluplogosmall" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload(this);showpreviewsmall(this);" />
                                        </div>
                                    </div>
                                    
                                 </div>
                                <asp:HiddenField ID="hdnClinicID" runat="server" /> 
                                 </div>
                               </div>
                        </div>
                    </div>
                </div> 
             </div>
         </div>
     <!-- Alert Container -->
        <div id="dialogoverlay"></div>
        <div id="dialogbox">
            <div>
                <div id="dialogboxhead"></div>
                <div id="dialogboxbody"></div>
                <div id="dialogboxfoot"></div>
            </div>
        </div>
       <div id="AllClinics" class="modal fade" role="dialog">
        <div class="modal-dialog" style="min-width: 550px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-color: #3661C7;">
                    <button type="button" class="close" data-dismiss="modal" id="UserClose">&times;</button>
                    <h3 class="modal-title">View All Clinics</h3>
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
                            <asp:GridView ID="dtgViewAllClinics" runat="server" AutoGenerateColumns="False" DataKeyNames="ClinicID" Style="width: 100%; font-size: 13px!important;">

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnUpdate" runat="server" Style="border: none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment" formnovalidate OnClick="ImgBtnUpdate_Click"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnDelete" Style="border: none!important" runat="server" ImageUrl="~/images/Deleteicon1.png" OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" formnovalidate />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="GroupName" HeaderText="Group Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="ClinicName" HeaderText="Clinic Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="Location" HeaderText="Clinic Location" ItemStyle-CssClass="Match"></asp:BoundField>
                                    <asp:BoundField DataField="ClinicID" HeaderText="ClinicID"></asp:BoundField>
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
    

         <script>
             function showpreview(input) {
                 debugger;
                 if (input.files && input.files[0]) {
                     var reader = new FileReader();
                     reader.onload = function (e) {
                         $('#<%=LogPic.ClientID %>').attr('src', e.target.result);
                     }
                     reader.readAsDataURL(input.files[0]);
                 }
             }
             function showpreviewsmall(input) {
                 debugger;
                 if (input.files && input.files[0]) {
                     var reader = new FileReader();
                     reader.onload = function (e) {
                         $('#<%=LogsmallPic.ClientID %>').attr('src', e.target.result);
                     }
                     reader.readAsDataURL(input.files[0]);
                 }
             }


             var validFiles = ["bmp", "gif", "png", "jpg", "jpeg"];
             function OnUpload(f) {
                 debugger;
                 document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

                 var source = f.value;
                 var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
                 for (var i = 0; i < validFiles.length; i++) {
                     if (validFiles[i] == ext)

                         break;
                 }
                 if (i >= validFiles.length) {
                     Alert.render("Format Not Supporting\n\n Try:" + validFiles.join(", "));
                     f.value = '';
                 }
                 return true;
             }
             $(function () {
                 $('[id*=lstFruits]').multiselect({
                     includeSelectAllOption: true
                 });
             });
    function hello(num) {
        debugger;
        if(num==1)
        {
            $('#ExistingGroup').hide();
            $('#NewGroup').show();
            $('#hdnGroupselect').val('New');
            $('#<%=lstFruits.ClientID%>').multiselect('select', ['1', '2', '3']);
        }
        if (num == 2) {
            debugger;
            $('#NewGroup').hide();
            $('#ExistingGroup').show();
            $('#hdnGroupselect').val('Exist');
            $('#<%=lstFruits.ClientID%>').multiselect('deselect', ['1', '2', '3']);
            
        }
    }
    var ClinicID = '';

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
        debugger;
        $("[id*=dtgViewAllClinics] td:eq(0)").click(function () {
            
            debugger;
            document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
            
            $("#<%=txtGroupName.ClientID %>").attr("readonly", true);
            $("#<%=txtGroupName.ClientID %>").attr('style', 'background-color:lightgray');

           

            if ($(this).text() == "") {
                var jsonResult = {};
                ClinicID = $(this).closest('tr').find('td:eq(4)').text();
                var Clinic = new Object();
                Clinic.ClinicID = ClinicID;
                jsonResult = GetUserDetailsByClinicID(Clinic);
                if (jsonResult != undefined) {

                    BindUserControls(jsonResult);
                }
            }
        });
    });


             function GetUserDetailsByClinicID(Clinic) {
                 debugger;
        var ds = {};
        var table = {};
        var data = "{'ClinicObj':" + JSON.stringify(Clinic) + "}";
        ds = getJsonData(data,"../Admin/SAdmin.aspx/BindClinicDetailsOnEditClick");
        table = JSON.parse(ds.d);
        return table;
    }

    function BindUserControls(Records) {
        debugger;
        $.each(Records, function (index, Records) {
            $('#<%=txtGroupName.ClientID%>').val(Records.Name1)
            $("#<%=txtClinicName.ClientID %>").val(Records.Name);
            $('#<%=hdnClinicID.ClientID%>').val(Records.ClinicID)
            $("#<%=txtAddress.ClientID %>").val(Records.Address);
            $("#<%=txtLocation.ClientID%>").val(Records.Location);
            $("#<%=txtPhone.ClientID %>").val(Records.Phone);
            $("#<%=ddlGroup.ClientID%>").val(Records.Name1);
            $("#<%=LogPic.ClientID%>").attr('src', '../Handler/ImageHandler.ashx?ClinicLogoID=' + Records.ClinicID + '');
            $("#<%=LogsmallPic.ClientID%>").attr('src', '../Handler/ImageHandler.ashx?ClinicLogosmallID=' + Records.ClinicID + '');
            $('#rdbdiv').hide();
            $('#DivMultiRoles').hide();
            $('#ExistingGroup').hide();
            $('#NewGroup').show();
            $('#hdnGroupselect').val('Update');

            

            function OnSuccess(response, userContext, methodName) {

                
            }
            function onError(response, userContext, methodName) {
            }

            $("#UserClose").click();
        });

    }
    $(function () {

        GetClinic(1);
    });
    $("[id*=txtSearch]").click("keyup", function () {
        debugger;
        GetClinic(parseInt(1));
    });
    $(".Pager .page").live("click", function () {
        GetClinic(parseInt($(this).attr('page')));
    });
    function SearchTerm() {
        return jQuery.trim($("[id*=txtSearch]").val());
    };
    function GetClinic(pageIndex) {
        debugger;
        $.ajax({

            type: "POST",
            url: "../Admin/SAdmin.aspx/ViewAndFilterClinic",
            data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {

                //alert(response.d);
            },
            error: function (response) {

                //alert(response.d);
            }
        });
    }
    var row;
    function OnSuccess(response) {
        debugger;
        $(".Pager").show();

        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var Users = xml.find("Users");
        if (row == null) {
            row = $("[id*=dtgViewAllClinics] tr:last-child").clone(true);
        }
        $("[id*=dtgViewAllClinics] tr").not($("[id*=dtgViewAllClinics] tr:first-child")).remove();
        if (Users.length > 0) {

            $.each(Users, function () {
                debugger;
                $("td", row).eq(0).html($('<img />')
                   .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                //$("td", row).eq(1).html($('<img />')
                //   .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                $("td", row).eq(1).html($(this).find("GroupName").text());
                $("td", row).eq(2).html($(this).find("Name").text());
                $("td", row).eq(3).html($(this).find("Location").text());

                $("td", row).eq(4).html($(this).find("ClinicID").text());

                $("[id*=dtgViewAllClinics]").append(row);
                row = $("[id*=dtgViewAllClinics] tr:last-child").clone(true);
            });
            var pager = xml.find("Pager");

            if ($('#txtSearch').val() == '') {
                var GridRowCount = pager.find("RecordCount").text();

                $("#<%=lblClinicCount.ClientID %>").text(GridRowCount);
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
        }



        var th = $("[id*=dtgViewAllClinics] th:contains('ClinicID')");
        th.css("display", "none");
        $("[id*=dtgViewAllClinics] tr").each(function () {
            $(this).find("td").eq(th.index()).css("display", "none");
        });

    };
    function OpenModal()
    {
        debugger;
        $('#txtSearch').val('');
        GetClinic(parseInt(1));
    }
   
</script>
</asp:Content>
