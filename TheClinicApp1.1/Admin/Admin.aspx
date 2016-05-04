<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TheClinicApp1._1.Admin.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="../css/TheClinicApp.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />

    




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" EnableCdn="true"></asp:ScriptManager>
    <!-- Script Files -->
   <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/jquery-1.12.0.min.js"></script>

    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>


    <script>

      $(document).ready(function () 
{
         

  if ($('#<%=hdnUserCountChanged.ClientID %>').val() == "True") {
             GetMedicines(1);
                $('#<%=hdnUserCountChanged.ClientID %>').val('False');
            }


                //images that represents medicine name duplication hide and show
                var LnameImage = document.getElementById('<%=imgWebLnames.ClientID %>');
                LnameImage.style.display = "none";
                var errLname = document.getElementById('<%=errorLnames.ClientID %>');
                errLname.style.display = "none";

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


        function LoginNameCheck(txtLoginName) {
           
            var name = document.getElementById('<%=txtLoginName.ClientID %>').value;

if(name != " ")
{

            name = name.replace(/\s/g, '');

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
        }

    </script>


      <%--  //------------- AUTOFILL SCRIPT ---------%>
    <script src="../js/jquery-1.8.3.min.js"></script>
    
    <script src="../js/ASPSnippets_Pager.min.js"></script>


    <script type="text/javascript">

var   UserID = '';



   $(function () {
       $("[id*=dtgViewAllUsers] td:eq(1)").click(function () {



                var DeletionConfirmation = ConfirmDelete();

                if (DeletionConfirmation == true) {
                    UserID = $(this).closest('tr').find('td:eq(6)').text();


 window.location = "Admin.aspx?UsrID=" + UserID;


 /* PageMethods.DeleteUserByID(UserID, OnSuccess, onError);

            function OnSuccess(response, userContext, methodName)
             {

               if(response == true)
{
 GetMedicines(1);
}
            }
            function onError(response, userContext, methodName)
             {


            }
*/

                 
                }
            });


        });


   
   $(function () {
       $("[id*=dtgViewAllUsers] td:eq(0)").click(function () {

           debugger;
           UserID = $(this).closest('tr').find('td:eq(6)').text();
           window.location = "Admin.aspx?UsrIDtoEdit=" + UserID;
       });


   });



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
                url: "../Admin/Admin.aspx/GetMedicines",
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
                row = $("[id*=dtgViewAllUsers] tr:last-child").clone(true);
            }
            $("[id*=dtgViewAllUsers] tr").not($("[id*=dtgViewAllUsers] tr:first-child")).remove();
            if (Medicines.length > 0) {
                $.each(Medicines, function () {



                    var medicine = $(this);
 $("th", row).eq(0).text(" ");

                    //$("td", row).eq(0).html('<a href="#">' + $(this).find("MedicineCode").text() + '</a>');
 $("td", row).eq(0).html($('<img />')
                       .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');


 $("td", row).eq(1).html($('<img />')
                       .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                    $("td", row).eq(2).html($(this).find("LoginName").text());
                    $("td", row).eq(3).html($(this).find("FirstName").text());
                    $("td", row).eq(4).html($(this).find("LastName").text());
                    $("td", row).eq(5).html($(this).find("Active").text());
                   
                    $("td", row).eq(6).html($(this).find("UserID").text());

                    $("[id*=dtgViewAllUsers]").append(row);
                    row = $("[id*=dtgViewAllUsers] tr:last-child").clone(true);
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
                $("[id*=dtgViewAllUsers]").append(empty_row);
            }



 var th = $("[id*=dtgViewAllUsers] th:contains('UserID')");
            th.css("display", "none");
            $("[id*=dtgViewAllUsers] tr").each(function () {
                $(this).find("td").eq(th.index()).css("display", "none");
            });

        };


 
        </script>



    <div class="main_body">

        <!-- Left Navigation Bar -->
        <div class="left_part">
            <div class="logo">
                <a href="#">
                    <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
            </div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                 <li id="admin" class="active" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label>
            </p>
        </div>

        <!-- Right Main Section -->
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Administrator
            </div>

            <div class="icon_box">
                <a class="all_registration_link" data-toggle="modal" data-target="#AllUsers" ><span title="View All Users" data-toggle="tooltip" data-placement="left" ><img src="../images/multiuser.png" /></span></a>
                  </div>

            <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="Admin.aspx">Create User</a></li>

                        <li role="presentation"><a href="AssignRoles.aspx">Assign Role</a></li>
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
                                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click"/>
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

                                     

                                    <div class="col-lg-8">


                                        <label for="name">Login Name</label><input id="txtLoginName" runat="server" type="text" name="name" required onchange="LoginNameCheck(this)" />

                                          <asp:Image ID="imgWebLnames" runat="server" ToolTip="Login name is Available" ImageUrl="~/Images/newfff.png" />


                                    <asp:Image ID="errorLnames" runat="server" ToolTip="Login name is Unavailable" ImageUrl="~/Images/newClose.png" />
                                         </div>

                                        <%--<label for="Login Name">Login Name</label>--%>


                                        <%--<asp:TextBox ID="txtLoginName" runat="server" onchange="LoginNameCheck(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>--%>

                                   
                                     <%-- <div class="col-lg-4">
                               
                                          <br />
                                             <br />

                                  
                                           </div>--%>

                                       
                                     
                                </div>

                       


                            <div class="row field_row">
                                <div class="col-lg-4 ">

                                        <label for="name">First Name</label><input id="txtFirstName" runat="server" type="text" name="name" required  />


                                  <%--  <label for="First Name">First Name</label>
                                    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                    </asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="col-lg-4 ">

                                     <label for="name">Last Name</label><input id="txtLastName" runat="server" type="text" name="name"   />


                                    <%--<label for="First Name">Last Name</label>
                                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>--%>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>--%>
                                </div>
                            </div>




                            <div class="row field_row">
                                <div class="col-lg-4 ">

                                     <label for="name">Password</label><input id="txtPassword" runat="server" type="text" name="name"  required />

                                   <%-- <label for="Password">Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please fill out this field" ForeColor="Red">

                                    </asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="col-lg-4 ">

                                      <label for="name">Phone</label><input id="txtPhoneNumber" runat="server" type="text" name="name"  pattern="^[0-9+-]*$"  required />

                                   <%-- <label for="Phone">Phone</label>
                                    <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPhoneNumber" runat="server" ValidationExpression="^[0-9+-]*$" ErrorMessage="Please enter a valid phone number" ForeColor="Red"></asp:RegularExpressionValidator>--%>

                                </div>
                            </div>

                          

                            <div class="row field_row">
                                <div class="col-lg-8 ">
                                    <label for="name">Email</label><input id="txtEmail" runat="server" type="text" name="name"  pattern="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"  required />

                                  <%--  <label for="Email">Email</label>
                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail" runat="server" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ErrorMessage="Please enter a valid Email" ForeColor="Red"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please fill out this field" ForeColor="Red">

                                    </asp:RequiredFieldValidator>--%>

                                </div>
                            </div>

                            <div class="row field_row">

                                <div class="col-lg-4">
                                    <div class="row">
                                        <div>
                                            <label for="sex">
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
                                            <label for="sex">
                                                IsDoctor
                                                    <asp:RadioButton ID="rdoDoctor" runat="server" GroupName="Doctor" Text="Yes" CssClass="checkbox-inline"  />
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
          <div class="modal-dialog" style="height:600px">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="border-color:#3661C7;">  
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
        <h3 class="modal-title">View All Users</h3>
      </div>
      <div class="modal-body" style="height:500px" >
       <%--<iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>--%>
         

        <asp:GridView ID="dtgViewAllUsers" runat="server" AutoGenerateColumns="False" class="table" >
                        
                        <Columns>
                           <%-- <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("UserID")+"|"+Eval("LoginName")+"|"+Eval("FirstName")+"|"+Eval("LastName")+"|"+Eval("Active")+"|"+Eval("Password")+"|"+Eval("Email")+"|"+Eval("PhoneNo")%>'  formnovalidate />


                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/Cancel.png" CommandName="CommentDelete" CommandArgument='<%# Eval("UserID")%>'  OnClientClick=" return ConfirmDelete()" formnovalidate />

                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:Image ID="img2" runat="server" 
                                                            OnClientClick="ConfirmDelete()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                            

                             <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Image ID="img1" runat="server" 
                                                            OnClientClick="ConfirmDelete()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                            <asp:BoundField DataField="LoginName" HeaderText="Login Name">
                               
                            </asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="First Name">
                              
                            </asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="Last Name">
                              
                            </asp:BoundField>
                            <asp:BoundField DataField="Active" HeaderText="Active">
                              
                            </asp:BoundField>

                             <asp:BoundField DataField="UserID" HeaderText="UserID">
                              
                            </asp:BoundField>
                            


                           <%--  <asp:BoundField DataField="Password" HeaderText="Password">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                            </asp:BoundField>--%>

                        </Columns>
                        
                    </asp:GridView>
            <div class="Pager"></div>
    
    </div>
         
         
    </div>

  </div>
        </div>


    <asp:HiddenField ID="hdnUserCountChanged" runat="server" />
      <asp:HiddenField ID="hdnUserID" runat="server" />
</asp:Content>
