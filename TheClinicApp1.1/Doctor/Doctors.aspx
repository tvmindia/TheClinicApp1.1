<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Doctors.aspx.cs" Inherits="TheClinicApp1._1.Doctor.Doctors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
       
        <meta name="description" content="" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
<!-- FAVICON -->
<link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../css/bootstrap-spinner.css" rel="stylesheet" />
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/normalize.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />

    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <!-- #main-container -->
         
         
         <div class="main_body">


         
          <div class="left_part">
         <div class="logo"><a href="#"><img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a></div>
         <ul class="menu">
         <li  id="patients"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
         <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
         <li id="doctor" class="active"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
         <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
         <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
         </ul>
         
         <p class="copy">&copy;Tritham Ayurveda</p>
         </div>
         
         
         <div class="right_part">         
         <div class="tagline">
         <a class="nav_menu">nav</a>
         <span>Doctors...</span>
         </div>
         <div class="icon_box">
         <a class="records" data-toggle="modal" ><span class="count">5</span><span title="Notification" data-toggle="tooltip" data-placement="left"><img src="../images/records.png"/></span></a>
         <a class="casehistory_link" data-toggle="modal" data-target="#casehistory" ><span title="Case HIstory" data-toggle="tooltip" data-placement="left"><img src="../images/case-history.png"/></span></a>
         <a class="tokens_link" data-toggle="modal" data-target="#tokens" ><span title="Tokens" data-toggle="tooltip" data-placement="left"><img src="../images/tokens.png"/></span></a>         
         </div>
         <div class="grey_sec">
         <div class="search_div">
         <input class="field" type="search" placeholder="Search here..." />
         <input class="button" type="submit" value="Search" />
         </div>
         <ul class="top_right_links"><li><a class="save" href="#"><span></span>Save</a></li><li><a class="new" href="#"><span></span>New</a></li></ul>
         </div>
         <div class="right_form">
         
         <div class="token_id_card">
             <div class="name_field"><img src="../images/UploadPic.png" width="80" height="80" /> Lorem Ipsum </div>
                 <div class="light_grey">
                     <div class="col3_div">25yrs<span>Age</span></div>
                     <div class="col3_div">Female<span>Gender</span></div>
                     <div class="col3_div">1562<span>File No</span></div>
                 </div>
                 <div class="card_white">
                    <div class="field_label"><label>Doctor</label> Test Test</div>      
                </div>
             </div>
         

          
         
         <div id="accordion">
  <h3>Personal</h3>
  <div>
  <div class="row field_row">
  <div class="col-lg-4">
  <div class="row">
  <div class="col-lg-8"><label for="height">Height</label>  
  <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" value="1" data-max="200" data-min="1" data-step="1">
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
        <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" value="1" data-max="200" data-min="1" data-step="1">
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
  </div>
  <div class="col-lg-4"><label for="weight">Weight</label>
  <div class="input-group spinner weight" data-trigger="spinner" id="customize-spinner">
          <input type="text" class="form-control text-center" value="1" data-max="200" data-min="1" data-step="1">
          <div class="input-group-addon">
            <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
            <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
          </div>
        </div>
  </div>
  </div>
  </div>
  <div class="col-lg-4"><label for="bowel">Bowel</label><input id="bowel" type="text" name="bowel"></div>
  <div class="col-lg-4"><label for="appettie">Appettie</label><input id="appettie" type="text" name="appettie"></div>
  </div>
  
  <div class="row field_row">  
  <div class="col-lg-4"><label for="micturation">Micturation</label><input id="micturation" type="text" name="micturation"></div>
  <div class="col-lg-4"><label for="sleep">Sleep</label><input id="sleep" type="text" name="sleep"></div>  
  </div>
  
  <div class="row">  
  <div class="col-lg-12"><label for="symptoms">Symptoms</label><textarea id="symptoms"></textarea></div>
  </div>
  
  </div>
  
  <h3>Sys ex</h3>
  <div>
  <div class="row">  
  <div class="col-lg-4"><label for="cardiovascular">Cardiovascular</label><input id="cardiovascular" type="text" name="cardiovascular"></div>
  <div class="col-lg-4"><label for="nervoussystem">Nervoussystem</label><input id="nervoussystem" type="text" name="nervoussystem"></div>  
  <div class="col-lg-4"><label for="musculoskeletal">Musculoskeletal</label><input id="musculoskeletal" type="text" name="musculoskeletal"></div>  
  </div>
  </div>
  
  <h3>Gen</h3>
  <div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="palloe">Palloe</label><input id="palloe" type="text" name="palloe"></div>
  <div class="col-lg-4"><label for="icterus">Icterus</label><input id="icterus" type="text" name="icterus"></div>  
  <div class="col-lg-4"><label for="clubbing">Clubbing</label><input id="clubbing" type="text" name="clubbing"></div>  
  </div>
  <div class="row">  
  <div class="col-lg-4"><label for="cyanasis">Cyanasis</label><input id="cyanasis" type="text" name="cyanasis"></div>
  <div class="col-lg-4"><label for="lymphnodes">Lymphnodes</label><input id="lymphnodes" type="text" name="lymphnodes"></div>  
  <div class="col-lg-4"><label for="edima">Edima</label><input id="edima" type="text" name="edima"></div>  
  </div>
  </div>
  
  <h3>Diagnosys</h3>
  <div>
  <div class="row">  
  <div class="col-lg-12"><label for="diagnosys">Diagnosys</label><textarea id="diagnosys"></textarea></div>
  </div>
  </div>
  
  <h3>Remarks</h3>
  <div>
  <div class="row">  
  <div class="col-lg-6"><label for="remarks">Remarks</label><input id="remarks" type="text" name="remarks"></div>
  </div>
  </div>
  
  <h3>Clinical</h3>
  <div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="pulse">Pulse</label><input id="pulse" type="text" name="pulse"></div>
  <div class="col-lg-4"><label for="bp">Bp</label><input id="bp" type="text" name="bp"></div>  
  <div class="col-lg-4"><label for="tounge">Tounge</label><input id="tounge" type="text" name="tounge"></div>  
  </div>
  <div class="row field_row">  
  <div class="col-lg-4"><label for="heart">Heart</label><input id="heart" type="text" name="heart"></div>
  <div class="col-lg-4"><label for="lymphnodes">Lymphnodes</label><input id="lymphnodes" type="text" name="lymphnodes"></div>  
  <div class="col-lg-4"><label for="resp_rate">Resp rate</label><input id="resp_rate" type="text" name="resp_rate"></div>  
  </div>
  <div class="row">  
  <div class="col-lg-12"><label for="others">Others</label><textarea id="others"></textarea></div>
  </div>
  </div>
  
  <h3>Prescription</h3>
  <div>  
  <table class="table" width="100%" border="0">
  <tr>
    <th>Medicine</th>
    <th>Dozage</th>
    <th>Qty</th>
    <th>Unit</th>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>

  
  </div>
  
</div>
         
         
         </div>
         
         </div>         
         </div>
         
         
         
         
         
         
<!-- Modal -->
<div id="casehistory" class="modal fade" role="dialog">
  <div class="modal-dialog">
	
    <!-- Modal content-->
    <div class="modal-content">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Case History</h4>
      </div>
      <div class="modal-body">
        <table class="table" width="100%" border="0">
          <tr>
            <th>Sl No.</th>
            <th>Date</th>
            <th>Remarks</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>
      </div>      
    </div>

  </div>
</div>         
         
<div id="tokens" class="modal fade" role="dialog">
  <div class="modal-dialog">	
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        
        <h4 class="modal-title">Tokens</h4>
      </div>
      <div class="modal-body">
        <table class="table" width="100%" border="0">
          <tr>
            <th>Token No.</th>
            <th>Name</th>
            <th>Time</th>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
          </tr>
        </table>
      </div>      
    </div>

  </div>
</div>         

         
        
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/jquery.spinner.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
                  
       
        
        
        	
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
			test(document).ready(function(){				
				
							
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
</asp:Content>
