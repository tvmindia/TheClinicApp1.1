
<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="TheClinicApp1._1.Registration.Patients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Script Files -->  
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
   
    <script src="../js/jquery-1.12.0.min.js"></script>
     
    <script src="../js/bootstrap.min.js"></script>  
    <script src="../js/fileinput.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>






    <!---   Script for fileupload preview, Created By:Thomson Kattingal --->   
    <script type="text/javascript">
         function showpreview(input) {
             debugger;

             if (input.files && input.files[0]) {
                 debugger;

                 var reader = new FileReader();
                 reader.onload = function (e) {
                     debugger;
                        
                     $('#<%=ProfilePic.ClientID %>').attr('src', e.target.result);
                 }
                 reader.readAsDataURL(input.files[0]);
             }

         }
         
    </script>
    <!--------------------------------------------------------------------->
    <!---   Script Nav button to srink and reform in document Ready --->
    <script> 
        function getPatientId(Patient)
        {
            var PatientDetails=Patient;
            alert(PatientDetails);
        }


        var test = jQuery.noConflict();
        test(document).ready(function () {


            test('.alert_close').click(function () {
                test(this).parent(".alert").hide();
            });

            test('[data-toggle="tooltip"]').tooltip();



            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });

            

        });

		</script>
     <!---------------------------------------------------------------->
    <script>
          var test = jQuery.noConflict();
          test(document).on('ready', function () {
              test("#FileUpload").fileinput({
                  browseLabel: 'Upload'
              });
              
         });
        </script> 
    <!--- Script for AutocompletionTextBox preview, Created By:Thomson Kattingal --->   
    <script>
        var test=jQuery.noConflict();
        test(document).on('ready',function(){
            debugger;
            var ac=null;
            ac = <%=listFilter %>;
            $( "#txtSearch" ).autocomplete({
                source: ac
            });
        });
             
    </script>
    <!------------------------------------------------------------------------------>
  
    <!---------------------------------------------------------------------->
   <!------------------------------------->
  
        <!-- Main Container -->
        <div class="main_body">   
      
      <!-- Left Navigation Bar -->  
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients" class="active"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy"><asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>
         
         <!-- Right Main Section -->
         <div class="right_part">
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         Patients Registration</div>
         <div class="icon_box">
         <a class="all_registration_link" data-toggle="modal" data-target="#myModal" ><span title="All Registerd" data-toggle="tooltip" data-placement="left" onclick="SetIframeSrc('AllRegistrationIframe')"><img src="../images/registerd9724185.png" /></span></a>
         <a class="Todays_registration_link" data-toggle="modal" data-target="#TodaysRegistration" ><span title="Todays Register" data-toggle="tooltip" data-placement="left"><img src="../images/registerd.png" /></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
            
         <input class="field" type="search" id="txtSearch" name="txtSearch" placeholder="Search here..." />
         <input class="button" type="button" id="btnSearch" value="Search" runat="server" onserverclick="btnSearch_ServerClick" />
         </div>
         <ul class="top_right_links"><li><asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="button1" OnClick="btnSave_Click" /></li><li><a class="new" href="#" runat="server" id="btnNew" onserverclick="btnNew_ServerClick"><span></span>New</a></li></ul>
         </div>        
         <div class="right_form">         
         <div id="Errorbox"  style="height:30%;display:none;" runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
         </div>
         </div>
             <div class="alert alert-info" id="divDisplayNumber" visible="false" runat="server" ><a class="alert_close">X</a>
             <div>
             <div><asp:Label ID="lblDisplayFileNumber" runat="server" Text="File Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblFileCount" runat="server" Text=""></asp:Label></strong>&nbsp;&nbsp;<asp:Label ID="lblTokenNumber" runat="server" Text="Token Number"></asp:Label>:&nbsp;<strong><asp:Label ID="lblTokencount" runat="server" Text=""></asp:Label></strong></div>
                           
             </div>

             </div>
                
         <div class="alert alert-success" style="display:none">
          <strong>Success!</strong> Indicates a successful or positive action.<a class="alert_close">X</a>
        </div>        
        <div class="alert alert-info" style="display:none">
          <strong>Info!</strong> Indicates a neutral informative change or action.<a class="alert_close">X</a>
        </div>
        
        <div class="alert alert-warning" style="display:none">
          <strong>Warning!</strong> Indicates a warning that might need attention.<a class="alert_close">X</a>
        </div>
        
        <div class="alert alert-danger" style="display:none">
          <strong>Danger!</strong> Indicates a dangerous or potentially negative action.<a class="alert_close">X</a>
        </div>
            
      <div class="registration_form">        
      <div class="row field_row">  
      <div class="col-lg-8">
      <div class="row"> 
      <div class="col-lg-8 margin_bottom"><label for="name">Name</label><input id="txtName" runat="server" type="text" name="name" required pattern="^[A-z][A-z\.\s]+$" title="The Name is required and cannot be empty" /></div>
      <div class="col-lg-4 upload_photo_col">
      <div class="margin_bottom upload_photo">
      <img id="ProfilePic" src="~/images/UploadPic1.png" runat="server"  />
      </div>
      <div class="upload">
      <label class="control-label">Upload Picture</label>
          
      <asp:FileUpload ID="FileUpload1" ForeColor="Green" Font-Size="12px" runat="server" onchange="showpreview(this);" />
      </div>
      </div>
      <div class="col-lg-8 margin_bottom"><label for="sex">Sex</label>
          <asp:RadioButton ID="rdoMale" runat="server" GroupName="Active" Text="Male" CssClass="checkbox-inline" Width="9%" />
          <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Active" Text="Female" CssClass="checkbox-inline" Width="9%" />
      </div>
      <div class="col-lg-8"><label for="age">Age</label><input id="txtAge" runat="server" type="number" name="age" required title="The Age is required and cannot be empty !should be Number" /></div>
      </div>
      </div>            
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-12">
      <label for="address">Address</label><textarea id="txtAddress" runat="server" style="width:43%" ></textarea>
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="mobile">Mobile</label><input id="txtMobile" runat="server" type="text" name="mobile" minlength="5" pattern="{10}[0-9]" title="Not a Required Field if enter Only Numbers" />
      </div>
      <div class="col-lg-4">
      <label for="email">Email</label><input id="txtEmail" runat="server" type="email" name="email" title="The Email should keep a correct format like testname@test.te" />
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="marital">Marital</label>
          <asp:DropDownList ID="ddlMarital" runat="server" Width="100%" Height="40px">
              <asp:ListItem Value="Single" Text="Single"></asp:ListItem>
              <asp:ListItem Value="Married" Text="Married"></asp:ListItem>
              <asp:ListItem Value="Divorced" Text="Divorced"></asp:ListItem>
          </asp:DropDownList>
      </div>
      <div class="col-lg-4">
      <label for="occupation">Occupation</label><input id="txtOccupation" runat="server" type="text" name="occupation" />
      </div>

      </div>

      </div>

         </div>

         </div>
 </div> 
  
        <!---------------------------------- Modal Section --------------------------------------->
        <!-- All Registration Iframe Modal -->
       <%-- <div id="add_medicine" class="modal fade" role="dialog">
  <div class="modal-dialog" style="height:600px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">  
          <button type="button" class="close" data-dismiss="modal">&times;</button>     
        <h4 class="modal-title">Add New Medicine</h4>
      </div>
      <div class="modal-body" style="height:400px;">

             <iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>
    
      </div>      
    </div>

  </div>
</div> --%>
              
        <!-- Token Registration Modal -->
        <div class="modal fade" id="TokenRegistration" role="dialog">
            <div class="modal-dialog modal-lg1" style="width:40%;height:70%">

                <!-- Modal content-->
                
                <div class="modal-content" style="height:100%;overflow-y:no-display;">
                    <div class="modal-header" style="background-color:royalblue">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="font-size:20px;color:white;">Token Registration</h4>

                    </div>
                    <div class="modal-body" style="background-color:white;overflow-x:hidden;overflow-y:hidden;height:60%;width:100%;">
                        <div class="col-sm-12">
                        <div class="col-lg-12" style="color:brown;">
                            Would You Like to Book A Token ?
                        </div>
                         
                        <div class="col-lg-12">
                            
                     <asp:Label Text="Select Your Doctor " Font-Size="Large" Font-Bold="true"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlDoctorName" Height="70%" Width="100%" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-lg-12">
                            <h1 style="color:white;">Good Luck</h1>
                            <h1 style="color:white;">Good Luck</h1>
                        </div>
                      <div class="col-lg-10">
                           <button type="button" class="close" data-dismiss="modal" style="color:blueviolet;font-size:25px;">>>>Skip</button>
                        </div>
                    </div>
                    <div class="modal-footer">
                      <asp:Button ID="btntokenbooking" runat="server" Text="BOOK TOKEN"  type="submit" CssClass="button" OnClick="btntokenbooking_Click" BorderColor="DarkSeaGreen" ForeColor="White" BackColor="#3366ff" ValidationGroup="Submit" formnovalidate />                    
                    </div>
                    </div>
                </div>
                
            </div>
        </div>

        <!-- All Registration Modal -->
        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog  modal-lg" style="width:60%;height:70%">
                <!-- Modal content-->
                <div class="modal-content" style="width:100%;height:100%" >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">View All Registrations</h3>
                    </div>
                    <div class="modal-body" style="width:100%;height:100%;overflow-x:auto;" >
                        <div class="col-sm-12">                        
                        <asp:GridView ID="dtgViewAllRegistration" CssClass="table" runat="server" AutoGenerateColumns="False" style="text-align:center;" ForeColor="#333333" GridLines="None" AllowPaging="true" OnPageIndexChanging="dtgViewAllRegistration_PageIndexChanging" PageSize="5" Width="100%">                            
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("Occupation")%>' OnCommand="ImgBtnUpdate_Command" formnovalidate />
                                       
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/Cancel.png" CommandName="CommentDelete" CommandArgument='<%# Eval("PatientID")%>' OnClientClick="return confirm('Deletion Confirmation \n\n\n\n\ Are you sure you want to delete this item ?');" OnCommand="ImgBtnDelete_Command" formnovalidate />
                                       
                                    </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Patient Name">                                    
                                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                </asp:BoundField>

                            </Columns>
                            <EditRowStyle HorizontalAlign="Center" BackColor="#0080AA"></EditRowStyle>
                            <FooterStyle BackColor="#0080AA" ForeColor="White" Font-Bold="True"></FooterStyle>
                            <HeaderStyle BackColor="#0080AA" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerStyle HorizontalAlign="Center" ForeColor="black" BackColor="#2461BF"></PagerStyle>
                            <RowStyle BackColor="#EFF3FB"></RowStyle>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                            </div>
                    </div>                 
                    <div class="modal-footer">
                    </div>
                </div>
                </div>
            </div>
     
      
        <!-- Todays Registration Modal -->
        <div class="modal fade" id="TodaysRegistration" role="dialog">
            <div class="modal-dialog modal-lg" style="width:60%;height:70%">
                <!-- Modal content-->               
                <div class="modal-content" style="width:100%;height:100%">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">Todays Registrations</h3>

                    </div>
                    <div class="modal-body" style="width:100%;height:100%;overflow-x:auto;">
                     <div class="col-sm-12">
                        <asp:GridView ID="dtgViewTodaysRegistration" CssClass="table" runat="server" AutoGenerateColumns="False" style="text-align:center;width:100%;" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnUpdate1" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("image")+"|"+Eval("ImageType")%>' OnCommand="ImgBtnUpdate_Command" formnovalidate />

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnDelete1" runat="server" ImageUrl="~/Images/Cancel.png" CommandName="CommentDelete" CommandArgument='<%# Eval("PatientID")%>' OnClientClick="return confirm('Deletion Confirmation \n\n\n\n\ Are you sure you want to delete this item ?');" OnCommand="ImgBtnDelete_Command" formnovalidate />
                                       

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone"></asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                                
                            </Columns>
                            <EditRowStyle BackColor="#0080AA"></EditRowStyle>
                            <FooterStyle BackColor="#0080AA" ForeColor="White" Font-Bold="True"></FooterStyle>
                            <HeaderStyle BackColor="#0080AA" Font-Bold="True" ForeColor="White"></HeaderStyle>
                            <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                            <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                            <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                            <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                       

                    </div>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
        </div>
        <!------------------------------------------------------------------------------------------>   
                    
    <!-- Script Files -->
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <!---   Script includes function for open Modals preview, Created By:Thomson Kattingal --->
    <asp:ScriptManager runat="server"></asp:ScriptManager>   
    <script type="text/javascript">  
        <!---Function for Open Token Registration Modal and All Registarion Modal----->
        function openModal() {
            debugger;
             $('#TokenRegistration').modal('show');
           
        }
        function openmyModal() {
            $('#myModal').modal('show');
        }
        </script>
   <!----------------------------------------------------------------------------------------> 
   <!------------------------------------->
            
</asp:Content>
