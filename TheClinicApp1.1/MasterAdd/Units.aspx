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


    </style>


         <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/jquery-1.12.0.min.js"></script>

    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/DeletionConfirmation.js"></script>
    <script src="../js/jquery.tablePagination.0.1.js"></script>
  
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

          $('table').tablePagination({});

      });

         //---------------* Function to check  Unit duplication *--------------//

         function CheckUnitDuplication(txtCategoryName) {
             debugger;
             var name = document.getElementById('<%=txtDescription.ClientID %>').value;

             name = name.trim();


             if (name != "") {


                 name = name.replace(/\s/g, '');

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
         <li id="master" runat="server" class="active"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
             <li id="log" runat="server"><a name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
         </ul>
         
         <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
        Masters <ul class="top_right_links"><li>
         <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li><li>
         <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png"  BorderColor="White" runat="server" OnClick="LogoutButton_Click"  formnovalidate /></li></ul></div>
          
              <div class="icon_box">

 <a class="all_assignrole_link" data-toggle="modal" data-target="#AllUnits" >
     <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label></span>
     <span title="View All Units" data-toggle="tooltip" data-placement="left" >
         <img src="../images/units.png" /></span></a>


                

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
                                        <%--<a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick"><span></span>Save</a>--%>
                                         <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btSave_ServerClick" />

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
     
              <label for="name">Unit</label><input id="txtDescription" runat="server" type="text" name="name" required onchange="CheckUnitDuplication();"  />
           <asp:Image ID="imgWebLnames" runat="server" ToolTip="Desciption is Available" ImageUrl="~/Images/newfff.png" style="display:none" />


                                    <asp:Image ID="errorLnames" runat="server" ToolTip="Desciption is Unavailable" ImageUrl="~/Images/newClose.png" style="display:none"/>

      </div>
     <%-- <div class="col-lg-8">
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
          <div class="modal-dialog" style="height:600px">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header" style="border-color:#3661C7;">  
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
        <h3 class="modal-title">View All Units</h3>
      </div>
      <div class="modal-body" style="height:500px" >
       <%--<iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>--%>
         

        <asp:GridView ID="dtgViewAllUnits" runat="server" AutoGenerateColumns="False"   DataKeyNames="UnitID" OnPreRender="dtgViewAllUnits_PreRender">
                        
                        <Columns>
                          
                            <asp:TemplateField>
                                    <ItemTemplate>
                                        
                                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" style="border:none!important" ImageUrl="~/images/Editicon1.png" CommandName="Comment"  formnovalidate OnClick="ImgBtnUpdate_Click"  />
                                    </ItemTemplate>
                                </asp:TemplateField>



                       <asp:TemplateField HeaderText="">
             <ItemTemplate>
              <asp:ImageButton ID="ImgBtnDelete" style="border:none!important" runat="server" ImageUrl="~/images/Deleteicon1.png"  OnClientClick="return ConfirmDelete();" OnClick="ImgBtnDelete_Click" formnovalidate/>
               </ItemTemplate>
                </asp:TemplateField>



                            <asp:BoundField DataField="Description" HeaderText="Unit">
                               
                            </asp:BoundField>
                           
                               <%--<asp:BoundField DataField="Code" HeaderText="Code">
                               
                            </asp:BoundField>--%>

                        

                        </Columns>
                        
                    </asp:GridView>
           
    
    </div>
         
         
    </div>

  </div>
        </div>
     <asp:HiddenField ID="hdnUnitID" runat="server" />

</asp:Content>
