<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TheClinicApp1._1.Admin.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Script Files -->
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/jquery-1.12.0.min.js"></script>

    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>

    <script>
        function SetIframeSrc(HyperlinkID) {
            debugger;
            if (HyperlinkID == "AllUsersIframe") {
                var AllUsersIframe = document.getElementById('AllUsersIframe');
                AllUsersIframe.src = "AddNewMedicine.aspx";
                //$('#OutOfStock').modal('show');
            }

        }


        function LoginNameCheck(txtLoginName) {
            debugger;
            var name = document.getElementById('<%=txtLoginName.ClientID %>').value;
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

    </script>




    <div class="main_body">

        <!-- Left Navigation Bar -->
        <div class="left_part">
            <div class="logo"><a href="#">
                <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" class="active"><a name="hello" onclick="selectTile('admin','')"><span class="icon registration"></span><span class="text">Admin</span></a></li>
            </ul>

            <p class="copy">
                <asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
        </div>

        <!-- Right Main Section -->
        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Administrator
            </div>

            <div class="icon_box">
         <a class="all_registration_link" data-toggle="modal" data-target="#View_AllUsers" ><span title="View All Users" data-toggle="tooltip" data-placement="left" onclick="SetIframeSrc('AllUsersIframe')"><img src="../images/add_medicine.png"/></span></a>
        
     
         </div>

            <div class="right_form tab_right_form">

                <div class="page_tab">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="Admin.aspx">User</a></li>
                       
                        <li role="presentation"><a href="StockOut.aspx">Assign Role</a></li>
                    </ul>
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane active" id="stock">
                            <div class="grey_sec">
                                <%--<div class="search_div">
                                    <input class="field" type="search" placeholder="Search here..." id="txtSearch" />
                                    <input class="button" type="submit" value="Search" />
                                </div>--%>
                                <ul class="top_right_links" >
                                    <li><a class="save" id="Save" runat="server" onserverclick="Save_ServerClick"><span></span>Save</a></li>
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
                                        <asp:Label ID="lblDisplayFileNumber" runat="server" Text="File Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblFileCount" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;<asp:Label ID="lblTokenNumber" runat="server" Text="Token Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblTokencount" runat="server" Text=""></asp:Label></strong></div>

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
                                        
                                        <label for="Login Name">Login Name</label>
                                        <asp:TextBox ID="txtLoginName" runat="server" onchange="LoginNameCheck(this)"></asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>
                                       
                                        </div>

                                         <div class="col-lg-4 ">
                                         <asp:Image ID="imgWebLnames" runat="server" ToolTip="Login name is Available" ImageUrl="~/Images/Check.png" Width="30%" Height="10%" />


                                            <asp:Image ID="errorLnames" runat="server" ToolTip="Login name is Unavailable" ImageUrl="~/Images/newClose.png" />
                                             </div>
                                   
                                </div>


                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="First Name">First Name</label>
                                        <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-4 ">
                                        <label for="First Name">Last Name</label>
                                        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                       <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>


                              

                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="Password">Password</label>
                                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-lg-4 ">
                                        <label for="Phone">Phone</label>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPhoneNumber" runat="server" ValidationExpression="^[0-9+-]*$" ErrorMessage="Please enter a valid phone number" ForeColor="Red"></asp:RegularExpressionValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhoneNumber" ErrorMessage="Please fill out this field" ForeColor="Red">

                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>

                                <div class="row field_row">
                                    
                                </div>

                                <div class="row field_row">
                                    <div class="col-lg-4 ">
                                        <label for="Email">Email</label>
                                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtEmail" runat="server" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ErrorMessage="Please enter a valid Email" ForeColor="Red"></asp:RegularExpressionValidator>
                                        
                                    </div>
                                </div>

                                <div class="row field_row">

                                    <div class="col-lg-8">
                                        <div class="row">
                                            <div class="col-lg-8">

                                                <label for="sex">
                                                    Active
                                                    <asp:RadioButton ID="rdoActiveYes" runat="server" GroupName="Active" Text="Yes" CssClass="checkbox-inline" Width="9%" Checked="true" />
                                                    <asp:RadioButton ID="rdoActiveNo" runat="server" GroupName="Active" Text="No" CssClass="checkbox-inline" Width="9%" />

                                                </label>

                                            </div>

                                        </div>
                                    </div>
                                </div>


                                 <div class="row field_row">

                                    <div class="col-lg-8">
                                        <div class="row">
                                            <div class="col-lg-8">

                                                <label for="sex">
                                                    IsDoctor
                                                    <asp:RadioButton ID="rdoDoctor" runat="server" GroupName="Doctor" Text="Yes" CssClass="checkbox-inline" Width="9%"  />
                                                    <asp:RadioButton ID="rdoNotDoctor" runat="server" GroupName="Doctor" Text="No" CssClass="checkbox-inline" Width="9%" Checked="true"/>

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

<div id="View_AllUsers" class="modal fade" role="dialog">
  <div class="modal-dialog" style="height:600px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h3 class="modal-title">View All Users</h3>
      </div>
      <div class="modal-body" style="height:500px;">
           <iframe id="AllUsersIframe" style ="width: 100%; height: 100%" frameBorder="0" ></iframe>

             
      </div>      
    </div>

  </div>
</div>


    </div>




    <%--   <div class="right_form">         
       
            
  

         </div>--%>

        

</asp:Content>
