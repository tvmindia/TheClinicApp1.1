<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="SAdmin.aspx.cs" Inherits="TheClinicApp1._1.Admin.SAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
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
          
            <%--  <div class="icon_box">
                   <a class="all_assignrole_link" data-toggle="modal" data-target="#AssignedRoles" onclick="OpenModal();">
                         <span class="tooltip1">
                             <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label>
                             </span>
                             <img src="../images/AssignUser.png" />
                             <span class="tooltiptext1">View Assigned Roles</span>
                         </span>           
                   </a>
            </div>--%>

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
                                    <li><a class="new" href="Admin.aspx"><span></span>New</a></li>
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
                                     <div style="margin-bottom:3%">
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
                                 </div>    
                                
                                <div class="col-lg-3" style="margin:4% 4% 4% 4%">
                                    <img class="" id="LogPic" src="../images/Uploadlogo.PNG" runat="server" />
                                    <div class="upload">
                                    <label class="control-label">Upload Logo</label>

                                            <asp:FileUpload ID="fluplogo" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload(this);showpreview(this);" />
                                        </div>
                                 </div>
                                    
                                 </div>
                               </div>
                        </div>
                    </div>
                </div> 
             </div>
         </div>
       
         <script>
   
    function hello(num) {
        debugger;
        if(num==1)
        {
            $('#ExistingGroup').hide();
            $('#NewGroup').show();
            $('#hdnGroupselect').val('New');
        }
        if (num == 2) {
            $('#NewGroup').hide();
            $('#ExistingGroup').show();
            $('#hdnGroupselect').val('Exist');
            
        }
    }

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
    
    var validFiles = ["bmp", "gif", "png", "jpg", "jpeg"];
    function OnUpload(f) {
        debugger;
        document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

        //var obj = document.getElementById('#<%=fluplogo.ClientID %>');
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
</script>
</asp:Content>
