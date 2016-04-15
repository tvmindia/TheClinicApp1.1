
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

    <script>
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
      <script>
          var test = jQuery.noConflict();
          test(document).on('ready', function () {
              test("#FileUpload").fileinput({
                  browseLabel: 'Upload'
              });
              
         });
        </script>    
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
    <script>
        function SetIframeSrc(HyperlinkID){
            if (HyperlinkID=="AllRegistrationIframe")
            {
                var AllRegistrationIframe=document.getElementById('ViewAllRegistration');
                AllRegistrationIframe.src="../Registration/ViewAllRegistration.aspx";
            }
        }
    </script>
  
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
         <a class="all_registration_link" data-toggle="modal" data-target="#add_medicine" ><span title="All Registerd" data-toggle="tooltip" data-placement="left" onclick="SetIframeSrc('AllRegistrationIframe')"><img src="../images/registerd.png" /></span></a>
         <a class="Todays_registration_link" data-toggle="modal" data-target="#TodaysRegistration" ><span title="Todays Register" data-toggle="tooltip" data-placement="left"><img src="../images/registerd.png" /></span></a>
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" id="txtSearch" name="txtSearch" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div>
         <ul class="top_right_links"><li><a class="save" id="btSave" runat="server" onserverclick="btSave_ServerClick" ><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>
         </div>        
         <div class="right_form">         
         <div id="Errorbox"  style="height:25%;  display:none;"  runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>

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
      <div class="col-lg-8 margin_bottom"><label for="name">Name</label><input id="txtName" runat="server" type="text" name="name" required /></div>
      <div class="col-lg-4 upload_photo_col">
      <div class="margin_bottom upload_photo">
      <img id="ProfilePic" runat="server" src="../images/UploadPic.png" />
      </div>
      <div class="upload">
      <label class="control-label">Upload Picture</label>
      <asp:FileUpload ID="FileUpload1" ForeColor="Green" Font-Size="12px" runat="server" />
      </div>
      </div>
      <div class="col-lg-8 margin_bottom"><label for="sex">Sex</label>
          <asp:RadioButton ID="rdoMale" runat="server" GroupName="Active" Text="Male" CssClass="checkbox-inline" Width="9%" />
          <asp:RadioButton ID="rdoFemale" runat="server" GroupName="Active" Text="Female" CssClass="checkbox-inline" Width="9%" />
      </div>
      <div class="col-lg-8"><label for="age">Age</label><input id="txtAge" runat="server" type="text" name="age" required /></div>
      </div>
      </div>            
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-12">
      <label for="address">Address</label><textarea id="txtAddress" runat="server" style="width:43%" required></textarea>
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="mobile">Mobile</label><input id="txtMobile" runat="server" type="text" name="mobile" />
      </div>
      <div class="col-lg-4">
      <label for="email">Email</label><input id="txtEmail" runat="server" type="text" name="email" />
      </div>
      </div>
      
      <div class="row field_row">  
      <div class="col-lg-4">
      <label for="marital">Marital</label>
          <asp:DropDownList ID="ddlMarital" runat="server" Width="100%" Height="40px">
              <asp:ListItem Value="3">Single</asp:ListItem>
              <asp:ListItem Value="1">Married</asp:ListItem>
              <asp:ListItem Value="2">Divorced</asp:ListItem>
          </asp:DropDownList>
      </div>
      <div class="col-lg-4">
      <label for="occupation">Occupation</label><input id="txtOccupation" runat="server" type="text" name="occupation" />
      </div>
      </div>
      
        </div> 
        
          <div class="col-md-12" id="divDisplayNumber" visible="false" style="font-size:20px" runat="server">
                    <table>
                        <tr>
                            <td>
                                 <asp:Label ID="lblDisplayFileNumber" runat="server" Text="File Number"></asp:Label>:
                            </td>
                            <td>
                                <asp:Label ID="lblFileCount" runat="server" Text=""></asp:Label>
                            </td>
                            </tr>
                        <tr>
                            <td>
                                 <asp:Label ID="lblTokenNumber" runat="server" Text="Token Number"></asp:Label>:
                            </td>
                            <td>
                                <asp:Label ID="lblTokencount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
         
         
         </div>
         
         </div>  
    </div>  
        <!---------------------------------- Modal Section --------------------------------------->

    <div id="add_medicine" class="modal fade" role="dialog">
  <div class="modal-dialog" style="height:600px;">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">       
        <h4 class="modal-title">Add New Medicine</h4>
      </div>
      <div class="modal-body" style="height:400px;">

             <iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>
    
      </div>      
    </div>

  </div>
</div> 

        <div class="modal fade" id="TokenRegistration" role="dialog">
            <div class="modal-dialog modal-lg1" style="width:400px;">

                <!-- Modal content-->
                
                <div class="modal-content" style="height:100%;overflow-y:no-display;">
                    <div class="modal-header" style="background-color:royalblue">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="font-size:20px;color:white;font-family:Castellar;">Token Registration</h4>

                    </div>
                    <div class="modal-body" style="background-color:white;overflow-x:hidden;overflow-y:hidden;">
                        <div class="col-lg-12" style="color:brown;">
                            Would You Like to Book A Token ?
                        </div>
                         <div class="col-lg-10">
                           <button type="button" class="close" data-dismiss="modal" style="color:royalblue;font-size:25px;">>>>Skip</button>
                        </div>
                        <div class="col-lg-12">
                            
                     <asp:Label Text="Select Your Doctor" Font-Size="Large" Font-Bold="true"  runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlDoctorName" Height="40%" Width="100%" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-12 Span-One">
                        
                    <div class="col-md-7" >
                        <div class="col-md-12 Span-One">
                            
                             
                        </div>
                         

                        <div class="col-md-12 Span-One">
                            <div class="col-md-7">
                                <div class="form-group">
                   
                                    
                                </div>
                           
                            </div>
                             <div class="col-md-5">           
                                
                            </div>
                        </div>

                          <div class="col-md-12 Span-One">
                            <div class="col-md-8">
                                <div class="form-group">
                   
                                    
                                </div>
                           
                            </div>
                             
                        </div>
                        <%-- Patient Details Diplay region --%>
                        
                      
               <%--    <table class="tokenPatientDetailsTable TileContent" id="divContainer" style="display:none" >
                       <tr>
                           <td class="tokenPatientDetailsTableColumn">
                                       <label  class="subheadingLabel" style="text-align:center" >Patient Details</label>

                           </td>
                       </tr>

                        <tr>
                           <td  class="tokenPatientDetailsTableColumn">

                                       <asp:Label ID="lblFile" CssClass="largefont " runat="server" Text=""></asp:Label>

                           </td>
                       </tr>
                        <tr>
                           <td  class="tokenPatientDetailsTableColumn">
                                    
                                          <asp:Label ID="lblName"  CssClass="tokenPatientDetailsName "  runat="server" Text=""></asp:Label>

                           </td>
                       </tr>
                        <tr>
                           <td  class="tokenPatientDetailsTableColumn">
                                        <asp:Label ID="lblAge" runat="server"  CssClass="tokenPatientDetailslabel" Text=""></asp:Label>

                           </td>
                       </tr>
                         <tr>
                           <td  class="tokenPatientDetailsTableColumn">
                                 <asp:Label ID="lblGender" runat="server" CssClass="tokenPatientDetailslabel" Text=""></asp:Label>
                           </td>
                       </tr>
                        <tr>
                           <td  class="tokenPatientDetailsTableColumn">
                                           <asp:Label ID="lblPhone" runat="server" CssClass="tokenPatientDetailslabel" Text=""></asp:Label>

                           </td>
                       </tr>

                        <tr>
                           <td  class="tokenPatientDetailsTableColumn">
                                    <asp:Label ID="lblToken" Visible="false" CssClass="largefont tokenPatientDetailslabel"  runat="server" Text="Token NO"></asp:Label>

                           </td>
                       </tr>
                        
                   </table>--%>
                                     

                            <div class="col-md-12 Span-One">
                            <div class="col-md-8">
                                <div class="form-group">
                   
                                    <div class="col-md-12">
                                          &nbsp
                                                       
                                    </div>
                                </div>
                           
                            </div>
                             <div class="col-md-4">           
                                <div class="col-md-12">                 
                                     &nbsp
                                </div>
                            </div>
                        </div>
                        
                    </div>

                        </div>
                    </div>
                    <div class="modal-footer" style="background-color:white;">
                      <asp:Button ID="btntokenbooking" runat="server" Text="BOOK TOKEN"  type="submit" CssClass="button" OnClick="btntokenbooking_Click" BorderColor="DarkSeaGreen" ForeColor="White" BackColor="#3366ff" ValidationGroup="Submit" formnovalidate />
                        ....
                    </div>
                </div>
                
            </div>
        </div>


        <div class="modal fade" id="myModal" role="dialog">
            <div class="modal-dialog  modal-lg">

                <!-- Modal content-->
                <div class="modal-content" >
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">View All Registrations</h4>

                    </div>
                    <div class="modal-body" >
                        
                        <asp:GridView ID="dtgViewAllRegistration" runat="server" AutoGenerateColumns="False" style="text-align:center;width:100%;" CellPadding="4" ForeColor="#333333" GridLines="None" Height="30px" AllowPaging="true" OnPageIndexChanging="dtgViewAllRegistration_PageIndexChanging" PageSize="5">
                            
                            <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("image")+"|"+Eval("ImageType")%>' OnCommand="ImgBtnUpdate_Command" formnovalidate />
                                       

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

                            <PagerStyle HorizontalAlign="Center" ForeColor="White" BackColor="#2461BF"></PagerStyle>

                            <RowStyle BackColor="#EFF3FB"></RowStyle>

                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                            <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                            <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                            <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                            <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                        </asp:GridView>
                            </div>
                   
                    <div class="modal-footer">
                   

                    </div>
                </div>

            </div>
        </div>
      
        
        <div class="modal fade" id="TodaysRegistration" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Todays Registrations</h4>

                    </div>
                    <div class="modal-body">
                     
                        <asp:GridView ID="dtgViewTodaysRegistration" runat="server" AutoGenerateColumns="False" style="text-align:center;width:100%;" CellPadding="4" ForeColor="#333333" GridLines="None">
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
                    <div class="modal-footer">
                       

                    </div>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </div>
        </div>
        <!------------------------------------------------------------------------------------------>   
                    
<!-- All Registration Modal -->
<%--<div id="ViewAllregistration" class="modal fade" role="dialog">
  <div class="modal-dialog">
  
  <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Registered List </h4>
      </div>
      <div class="modal-body">

      </div>

  </div>

  </div>

</div>   --%>      
         
<!-- Todays Registration Modal -->
<%--<div id="ViewTodaysRegistration" class="modal fade" role="dialog">
  <div class="modal-dialog">
  
  <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Registered List </h4>
      </div>
      <div class="modal-body">

      </div>

  </div>

  </div>

</div>--%>
     
     

   
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <asp:ScriptManager runat="server"></asp:ScriptManager>   
    <script type="text/javascript">
       
        function openModal() {
            debugger;
             $('#TokenRegistration').modal('show');
           
        }
        </script>
   
            
</asp:Content>
