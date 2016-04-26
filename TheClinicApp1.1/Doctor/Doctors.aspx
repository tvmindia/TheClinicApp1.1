<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeBehind="Doctors.aspx.cs" Inherits="TheClinicApp1._1.Doctor.Doctors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
     <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
     <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
        <script>
           

            function autocompleteonfocus(controlID)
            {
                //---------* Medicine auto fill, it also filters the medicine that has been already saved  *----------//

                
                var topcount =document.getElementById('<%=hdnRowCount.ClientID%>').value;
 
                if (topcount==0)
                {
                    var ac=null; 
                    ac = <%=listFilter %>;
                    $( "#txtMedName"+controlID).autocomplete({
                        source: ac
                    });
                }
                else
                {
                    var ac=null;
                    ac = <%=listFilter %>;
                    var i=1;
                    while(i<=topcount)
                    {
                        if (i==1)
                        {
                            var item=  document.getElementById('txtMedName'+i).value 
                                 
                            var result = ac.filter(function(elem){
                                return elem != item; 
                            });

                        }
                        else
                        {
                            if (document.getElementById('txtMedName'+i) != null)

                            {
                                var item=  document.getElementById('txtMedName'+i).value 
                                                           

                                result = result.filter(function(elem){
                                    return elem != item; 
                                }); 
                            }
                        }
                        i++;
                    }
            
                            
                    $( "#txtMedName"+controlID).autocomplete({
                        source: result
                    });

                }

            } 



            function GetTextBoxValuesPresLocal(){
            
                GetTextBoxValuesPres('<%=hdnTextboxValues.ClientID%>');
            }

            
            function FillTextboxUsingXml(){
                debugger;
                alert("success!");
                RefillMedicineTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
            }
            function reset(){
                $('input[type=text]').val('');  
                $('#textarea').val(''); 
                $('input[type=select]').val('');
                $('input[type=radio]').val('');
                $('input[type=checkbox]').val('');  
            }

		</script>



    <!-- #main-container -->
         
         <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
         <div class="main_body">         
         <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName%>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor" class="active"><a name="hello" onclick="selectTile('doctor','<%=RoleName%>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName%>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName%>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" ><a name="hello" onclick="selectTile('admin','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Admin</span></a></li>
         </ul>
         
         <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
         </div>      
         <div class="right_part">         
         <div class="tagline">
         <a class="nav_menu">Menu</a>
         <span>Doctors...</span>
         </div>
         <div class="icon_box">
         <a class="records" data-toggle="modal" data-target="#casehistory"><span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label></span><span title="Case HIstory" data-toggle="tooltip" data-placement="left"><img src="../images/case-history.png"/></span></a>
         <a class="casehistory_link" data-toggle="modal" data-target="#tokens" ><span title="Tokens" data-toggle="tooltip" data-placement="left"><img src="../images/tokens.png"/></span></a>
        <%-- <a class="tokens_link" data-toggle="modal" data-target="#tokens" ><span title="Tokens" data-toggle="tooltip" data-placement="left"><img src="../images/tokens.png"/></span></a>--%>         
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" id="txtSearch" name="txtSearch" type="search" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div>

         <ul class="top_right_links"><li><asp:Button ID="btnSave" runat="server" Text="save" CssClass="button1" OnClientClick="GetTextBoxValuesPresLocal();" OnClick="btnSave_Click" /></li><li><a class="new" href="#" id="btnNew" runat="server" onclick="reset();" onserverclick="btnNew_ServerClick"><span></span>New</a></li></ul>
         </div>
         <div class="right_form"> 
         <div id="Errorbox"  style="height:30%;display:none;" runat="server" ><a class="alert_close">X</a>
         <div>
         <strong> <asp:Label ID="lblErrorCaption" runat="server" Text=""></asp:Label> </strong>
         <asp:Label ID="lblMsgges" runat="server" Text=""></asp:Label>
         </div>
         </div>
         <div class="token_id_card">
             <div class="name_field"><img id="ProfilePic" src="../images/UploadPic1.png" width="80" height="80" runat="server" /><asp:Label ID="lblPatientName" runat="server" Text="Test_Name"></asp:Label></div>
                 <div class="light_grey">
                     <div class="col3_div"><asp:Label ID="lblAgeCount" runat="server" Text=""></asp:Label><span>Age</span></div>
                     <div class="col3_div"><asp:Label ID="lblGenderDis" runat="server" Text=""></asp:Label><span>Gender</span></div>
                     <div class="col3_div"><asp:Label ID="lblFileNum" runat="server" Text=""></asp:Label><span>File No</span></div>
                 </div>
                 <div class="card_white">
                    <div class="field_label"><label>Doctor</label><asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label></div>      
                </div>
             </div>
             <asp:HiddenField ID="HiddenField2" runat="server" />
         
         <div id="accordion">
  <h3>Personal</h3>
  <div>
  <div class="row field_row">
  <div class="col-lg-4">
  <div class="row">
  <div class="col-lg-8"><label for="height">Height</label>  
  <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" id="txtHeightFeet" runat="server" value="1" data-max="200" data-min="1" data-step="1" />
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
        <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" id="txtHeightInch" runat="server" value="1" data-max="200" data-min="1" data-step="1" />
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
  </div>
  <div class="col-lg-4"><label for="weight">Weight</label>
  <div class="input-group spinner weight" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" id="txtWeight" runat="server" value="1" data-max="200" data-min="1" data-step="1" />
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
  </div>
  </div>
  </div>
   <div class="col-lg-4"><label for="bowel">Bowel</label><input id="bowel" type="text" name="bowel" runat="server" /></div>
  <div class="col-lg-4"><label for="appettie">Appettie</label><input id="appettie" type="text" name="appettie"  runat="server" /></div>
  </div>
  
  <div class="row field_row">  
  <div class="col-lg-4"><label for="micturation">Micturation</label><input id="micturation" type="text" name="micturation" runat="server" /></div>
  <div class="col-lg-4"><label for="sleep">Sleep</label><input id="sleep" type="text" name="sleep" runat="server" /></div>  
  </div>
  
  <div class="row">  
  <div class="col-lg-12"><label for="symptoms">Symptoms</label><textarea id="symptoms" runat="server" ></textarea></div>
  </div>
  
  </div>
  
  <h3>Systematic Examination</h3>
  <div>
  <div class="row">  
  <div class="col-lg-4"><label for="cardiovascular">Cardiovascular</label><input id="cardiovascular" type="text" name="cardiovascular" runat="server" /></div>
  <div class="col-lg-4"><label for="nervoussystem">Nervoussystem</label><input id="nervoussystem" type="text" name="nervoussystem" runat="server" /></div>  
  <div class="col-lg-4"><label for="musculoskeletal">Musculoskeletal</label><input id="musculoskeletal" type="text" name="musculoskeletal" runat="server" /></div>  
  </div>
  </div>
  
  <h3>General Examination</h3>
  <div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="palloe">Palloe</label><input id="palloe" type="text" name="palloe" runat="server" /></div>
  <div class="col-lg-4"><label for="icterus">Icterus</label><input id="icterus" type="text" name="icterus" runat="server" /></div>  
  <div class="col-lg-4"><label for="clubbing">Clubbing</label><input id="clubbing" type="text" name="clubbing" runat="server" /></div>  
  </div>
  <div class="row">  
  <div class="col-lg-4"><label for="cyanasis">Cyanasis</label><input id="cyanasis" type="text" name="cyanasis" runat="server" /></div>
  <div class="col-lg-4"><label for="lymphnodes">Lymphnodes</label><input id="lymphGen" type="text" name="lymphGen" runat="server" /></div>  
  <div class="col-lg-4"><label for="edima">Edima</label><input id="edima" type="text" name="edima" runat="server" /></div>  
  </div>
  </div>
  
  <h3>Diagnosys</h3>
  <div>
  <div class="row">  
  <div class="col-lg-12"><label for="diagnosys">Diagnosys</label><textarea id="diagnosys" runat="server"></textarea></div>
  </div>
  </div>
  
  <h3>Remarks</h3>
  <div>
  <div class="row">  
  <div class="col-lg-6"><label for="remarks">Remarks</label><input id="remarks" type="text" name="remarks" runat="server" /></div>
  </div>
  </div>
  
  <h3>Clinical Details</h3>
  <div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="pulse">Pulse</label><input id="pulse" type="text" name="pulse" runat="server" /></div>
  <div class="col-lg-4"><label for="bp">Bp</label><input id="bp" type="text" name="bp" runat="server" /></div>  
  <div class="col-lg-4"><label for="tounge">Tounge</label><input id="tounge" type="text" name="tounge" runat="server" /></div>  
  </div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="heart">Heart</label><input id="heart" type="text" name="heart" runat="server" /></div>
  <div class="col-lg-4"><label for="lymphnodes">Lymphnodes</label><input id="lymphnodes" type="text" name="lymphnodes" runat="server" /></div>  
  <div class="col-lg-4"><label for="resp_rate">Resp rate</label><input id="resp_rate" type="text" name="resp_rate" runat="server" /></div>  
  </div>
  <div class="row">  
  <div class="col-lg-12"><label for="others">Others</label><textarea id="others" runat="server"></textarea></div>
  </div>
  </div>
  
  <h3>Prescription Section</h3>
  <div>  
  <table class="table" style="width:100%;border:0;" >
  <tbody><tr>
    <th>Medicine</th>
    <th>Quantity</th>
    <th>Unit</th>
    <th>Dosage</th>
    <th>Timing</th>
    <th>Days</th>
  </tr>
  <tr> 
     <td ><input id="txtMedName0" type="text" placeholder="Medicine" class="input" onfocus="autocompleteonfocus(0)"/></td>
      <td ><input id="txtMedQty0" type="text" placeholder="Qty" class="input"/></td>
      <td ><input id="txtMedUnit0" class="input" type="text" placeholder="Unit" /></td>
      <td ><input id="txtMedDos0" type="text" placeholder="Dosage" class="input"/></td>
      <td><input id="txtMedTime0" type="text" placeholder="Timing" class="input"/></td>
      <td><input id="txtMedDay0" type="text" placeholder="Days" class="input"/></td><td style="background:#E6E5E5">
      <input type="button" value="-" class="bt1" style="width:20px;"/></td><td style="background:#E6E5E5">
         <input type="button" id="btAdd" onclick="clickAdd();this.style.visibility = 'hidden';" value="+" class="bt1" style="width:20px" />         
         </td><td style="background-color: transparent"><input id="hdnDetailID0" type="hidden" /><input id="hdnQty0" type="hidden" /></td>
  </tr>
  </tbody>      
  </table>
 <div id="maindiv"> 
  </div>
  
  </div>
  
</div>
         
         <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
             <asp:HiddenField ID="hdnXmlData" runat="server" />
                    <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnTextboxValues" runat="server" />
                    <asp:HiddenField ID="hdnManageGridBind" runat="server" Value="False" />
             <asp:HiddenField ID="HdnForVisitID" runat="server" />
                    <asp:HiddenField ID="hdnHdrInserted" runat="server" />
                    <asp:HiddenField ID="hdnRemovedIDs" runat="server" />
         </div>
         
         </div>         
         </div>
                 
<!-- Modal -->
<div id="casehistory" class="modal fade" role="dialog">
  <div class="modal-dialog" style="width:60%;height:70%;">
	
    <!-- Modal content-->
    <div class="modal-content" style="width:100%;height:100%">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>        
        <h3 class="modal-title">Case History</h3>
      </div>
      <div class="modal-body" style="width:100%;height:100%">
          <div class="col-lg-12">
         <asp:GridView ID="GridViewVisitsHistory" runat="server" AutoGenerateColumns="False" Style="max-width: 100%;min-width:100%;" DataKeyNames="FileID" CellPadding="4" GridLines="None" ForeColor="#333333">
              <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
              <Columns>
                  <asp:TemplateField>
                      <ItemTemplate>
                          <asp:ImageButton ID="ImgBtnUpdateVisits" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("VisitID")+"|" + Eval("PrescriptionID") %>' OnCommand="ImgBtnUpdateVisits_Command" Width="50%" formnovalidate />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField HeaderText="Date" DataField="CreatedDate" />
                  <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
              </Columns>

              <EditRowStyle BackColor="#2461BF"></EditRowStyle>

              <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True"></FooterStyle>

              <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

              <PagerStyle HorizontalAlign="Center" ForeColor="White" BackColor="#2461BF"></PagerStyle>

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
         
<div id="tokens" class="modal fade" role="dialog">
  <div class="modal-dialog" style="width:60%;height:70%;">	
    <!-- Modal content-->
    <div class="modal-content" style="width:100%;height:100%">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h3 class="modal-title">Tokens</h3>
      </div>
      <div class="modal-body" style="width:100%;height:100%" >
          <div class="col-lg-12">
                   <asp:GridView ID="GridViewTokenlist" runat="server" AutoGenerateColumns="False" style="text-align:center;width:100%;" DataKeyNames="UniqueId" CellPadding="4" GridLines="None" ForeColor="#333333">
              <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
              <Columns>
                   <asp:TemplateField>
                      <ItemTemplate>
                      <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Pencil-01.png" CommandName="Comment" CommandArgument='<%# Eval("PatientID")%>' OnCommand="ImgBtnUpdate_Command1" Width="50%" BorderColor="White" formnovalidate />
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                  <asp:BoundField HeaderText="Patient Name" DataField="Name" />                 
              </Columns>
            <EditRowStyle BackColor="#0080AA"></EditRowStyle>
            <FooterStyle BackColor="#0080AA" ForeColor="White" Font-Bold="True"></FooterStyle>
            <HeaderStyle BackColor="#0080AA" Font-Bold="True" ForeColor="White"></HeaderStyle>
            <PagerStyle HorizontalAlign="Center" ForeColor="White" BackColor="#284775"></PagerStyle>
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

  </div>
</div>         

         
        
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/jquery.spinner.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Dynamicgrid.js"></script>
       
        
        
        	
			<script>
			var test=jQuery.noConflict();
            test(function(){
              test('#customize-spinner').spinner('changed',function(e, newVal, oldVal){
                test('#old-val').text(oldVal);
                test('#new-val').text(newVal);
              });
            })
            </script>
            
            <script>
			var test=jQuery.noConflict();
			test(document).ready(function () {
			   
			    test('.alert_close').click(function () {
			        test(this).parent(".alert").hide();
			    });	
							
			test('[data-toggle="tooltip"]').tooltip(); 	
			
			test('#accordion').accordion({
				collapsible:true,
				heightStyle: "content",
				beforeActivate: function(event, ui) {
					 // The accordion believes a panel is being opened
					if (ui.newHeader[0]) {
						var currHeader  = ui.newHeader;
						var currContent = currHeader.next('.ui-accordion-content');
					 // The accordion believes a panel is being closed
					} else {
						var currHeader  = ui.oldHeader;
						var currContent = currHeader.next('.ui-accordion-content');
					}
					 // Since we've changed the default behavior, this detects the actual status
					var isPanelSelected = currHeader.attr('aria-selected') == 'true';
					
					 // Toggle the panel's header
					currHeader.toggleClass('ui-corner-all',isPanelSelected).toggleClass('accordion-header-active ui-state-active ui-corner-top',!isPanelSelected).attr('aria-selected',((!isPanelSelected).toString()));
					
					// Toggle the panel's icon
					currHeader.children('.ui-icon').toggleClass('ui-icon-triangle-1-e',isPanelSelected).toggleClass('ui-icon-triangle-1-s',!isPanelSelected);
					
					 // Toggle the panel's content
					currContent.toggleClass('accordion-content-active',!isPanelSelected)    
					if (isPanelSelected) { currContent.slideUp(); }  else { currContent.slideDown(); }
		
					return false; // Cancels the default action
				}
			});
			
							
				test('.nav_menu').click(function(){
					test(".main_body").toggleClass("active_close");
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
 
  
</asp:Content>
