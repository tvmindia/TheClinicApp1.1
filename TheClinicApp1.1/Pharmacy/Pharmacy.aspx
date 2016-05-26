<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeBehind="Pharmacy.aspx.cs" Inherits="TheClinicApp1._1.Pharmacy.Pharmacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel DefaultButton="btnSave" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>

   <%-- <link href="../css/TheClinicApp.css" rel="stylesheet" />--%>
    <script src="../js/jquery-1.12.0.min.js"></script>
    <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <script src="../js/jquery-ui.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/fileinput.js"></script>
    <script src="../js/vendor/jquery-1.11.1.min.js"></script>
    <script src="../js/JavaScript_selectnav.js"></script>
    <script src="../js/Messages.js"></script>
    <script src="../js/Dynamicgrid.js"></script>

    <script>
        var test = jQuery.noConflict();
        test(document).ready(function () {
   
            test('.alert_close').click(function () {
                test(this).parent(".alert").hide();
            });	

            test('.nav_menu').click(function () {
                test(".main_body").toggleClass("active_close");
            });

            SetPageIDCalled('Pharmacy');

            var ac=null;
            ac = <%=NameBind %>;
            var length= ac.length;
            var projects = new Array();
            for (i=0;i<length;i++)
            {        

                var name= ac[i].split('🏠');
                projects.push({  value : name[0], label: name[0], desc: name[1]})   
   
            }

            $( "#txtSearch" ).autocomplete({
                //maxResults: 10,
                source: function(request, response) {
                    var results = $.ui.autocomplete.filter(projects, request.term);
                    response(results.slice(0, this.options.maxResults));
                },
                focus: function( event, ui ) {
                    $( "#txtSearch" ).val( ui.item.label );
                    return false;
                },
                select: function( event, ui ) {
                    $( "#project" ).val( ui.item.label );
      
                    $( "#project-description" ).html( ui.item.desc );        
 
                    return false;
                }
            })
             .autocomplete( "instance" )._renderItem = function( ul, item ) {
                 return $( "<li>" )
                 .append( "<a>" + item.label + "<br>" + item.desc + "</a>" )
                 .appendTo( ul );
             }; 

            test('body').on('keydown', 'input[type=text], select, textarea', function(e) {
                var self = $(this)
                  , form = self.parents('form:eq(0)')
                  , focusable
                  , next
                ;
                if (e.keyCode == 13) {
                    focusable = form.find('input,a,select,button,textarea').filter(':visible');
                    next = focusable.eq(focusable.index(this)+1);
                    if (next.length) {
                        next.focus();
                    } else {
                        form.submit();
                    }
                    return false;
                }
            });	
        });
    </script>

    <script> 
        function bindPatientDetails()
        {
                $(".alert").hide();
          
            var PatientName = document.getElementById("project-description").innerText;
                    
            var file=PatientName.split('|')      
            var file1=file[0].split('📰 ')
            var fileNO=file1[1]
            if (PatientName!="")
            { 
                PageMethods.PatientDetails(fileNO, OnSuccess, onError);  
            }
            function OnSuccess(response, userContext, methodName) 
            {   
                var string1 = new Array();
                string1 = response.split('|');
                document.getElementById('<%=hdnfileID.ClientID%>').value=string1[0];
                document.getElementById('<%=lblFileNum.ClientID%>').innerText=string1[0];
                document.getElementById('<%=lblPatientName.ClientID%>').innerText=string1[1];
                document.getElementById('<%=lblAgeCount.ClientID%>').innerText=string1[2];
                document.getElementById('<%=lblGenderDis.ClientID%>').innerText=string1[3];            
                document.getElementById('<%=HiddenPatientID.ClientID%>').value=string1[7];               
            
                document.getElementById('txtSearch').value="";//clearin the Search box
                
            }          
            function onError(response, userContext, methodName)
            {                   
            } 
            document.getElementById('txtSearch').value="";//clearin the Search box
        }

    </script>

    <!-- #main-container -->
    <script>

        function FillTextboxUsingXml(){
            
            GetClientIDOfRemovedID('<%=hdnRemovedIDs.ClientID%>','<%=hdnRowCount.ClientID%>');
                RefillPresMedicineTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
    }


    function BindMedunitbyMedicneName(ControlNo) 
    {
        
        if (ControlNo >= 0) {
            var MedicineName = document.getElementById('txtMedName' + ControlNo).value;
        }
        if (MedicineName != "") 
        {
            PageMethods.MedDetails(MedicineName, OnSuccess, onError);       
        }    
        function OnSuccess(response, userContext, methodName) 
        {        
            if (ControlNo >= 0) 
            {                  
                var MedicineDetails = new Array();
                MedicineDetails = response.split('|');
                document.getElementById('txtMedUnit' + ControlNo).value = MedicineDetails[0];  
                document.getElementById('hdnQty' + ControlNo).value=MedicineDetails[1];  
            }   
        }  
        function onError(response, userContext, methodName) {       
        }
    }

    function focuscontrol(ControlNo)
    {
        document.getElementById('txtMedQty' + ControlNo).value="";
        $("#txtMedQty" + ControlNo).css({ 'color': 'black' });

    }                 
           

    function autocompleteonfocus(controlID)
    {
        //---------* Medicine auto fill, it also filters the medicine that has been already saved  *----------//
                
        var topcount =Number(document.getElementById('<%=hdnRowCount.ClientID%>').value)+Number(1);
 
                if (topcount==0)
                {
                    var ac=null; 
                    ac = <%=listFilter %>;
                $( "#txtMedName0").autocomplete({
                    source: ac
                });
            }
            else
            {
                var ac=null;
                ac = <%=listFilter %>;
                var i=0;
                while(i<=topcount)
                {
                    if (i==0)
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
    </script>



    <div class="main_body">

        <div class="left_part">
            <div class="logo">
                <a href="#">
                    <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
            </div>
            <ul class="menu">
                <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                <li id="pharmacy" class="active"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                <li id="admin" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                <li id="master" runat="server" visible="false"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
            </ul>

            <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text=""></asp:Label></p>
        </div>


        <div class="right_part">
            <div class="tagline">
                <a class="nav_menu">Menu</a>
                Pharmacy
                <ul class="top_right_links">
                    <li>
                        <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                    <li>
                        <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClick="LogoutButton_Click" formnovalidate /></li>
                </ul>

            </div>
            <div class="icon_box">
                <a class="patient_list" data-toggle="modal" data-target="#patient_list"><span title="Patient List" data-toggle="tooltip" data-placement="left">
                    <img src="../images/patient_list.png" /></span></a>
            </div>
            <div class="grey_sec">
                <div class="search_div">
                    <input class="field" id="txtSearch" onblur="bindPatientDetails()" name="txtSearch" type="search" placeholder="Search here..." />
                    <input type="hidden" id="project-id" />
                    <p id="project-description" style="display: none"></p>
                    <input class="button" type="button" value="Search" />
                </div>
                <ul class="top_right_links">
                    <li>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button1" OnClick="btnSave_Click" /></li>
                    <li><a class="new" href="Pharmacy.aspx"><span></span>New</a></li>
                </ul>
            </div>
            <div class="right_form">


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


                <div class="token_id_card">
                    <div class="name_field">
                        <img src="../images/UploadPic1.png" id="ProfilePic" runat="server" width="80" height="80" /><asp:Label ID="lblPatientName" runat="server" Text="Patient_Name"></asp:Label>
                    </div>
                    <div class="light_grey">
                        <div class="col3_div">
                            <asp:Label ID="lblAgeCount" runat="server" Text="Age"></asp:Label>
                        </div>
                        <div class="col3_div">
                            <asp:Label ID="lblGenderDis" runat="server" Text="Gender"></asp:Label>
                        </div>
                        <div class="col3_div">
                            <asp:Label ID="lblFileNum" runat="server" Text="File No"></asp:Label>
                        </div>
                    </div>
                    <div class="card_white">
                        <div class="field_label">
                            <label>Doctor</label><asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>


                <div class="prescription_grid">
                    <table class="table" style="width: 100%;">
                        <tbody>
                            <tr>
                                <th>Medicine</th>
                                <th>Quantity</th>
                                <th>Unit</th>
                                <th>Dosage</th>
                                <th>Timing</th>
                                <th>Days</th>
                            </tr>
                            <tr>
                                <td>
                                    <input id="txtMedName0" type="text" class="input" onblur="BindMedunitbyMedicneName('0')" onfocus="autocompleteonfocus(0)" /></td>
                                <td>
                                    <input id="txtMedQty0" type="text" class="input" onfocus="focuscontrol(0)" onblur="CheckPharmacyMedicineIsOutOfStock('0')" onchange="RemoveWarningPharm('0')" autocomplete="off" /></td>
                                <td>
                                    <input id="txtMedUnit0" class="input" readonly="true" type="text" onfocus="focusplz(0)"/></td>
                                <td>
                                    <input id="txtMedDos0" type="text" class="input" /></td>
                                <td>
                                    <input id="txtMedTime0" type="text" class="input" /></td>
                                <td>
                                    <input id="txtMedDay0" type="text" class="input" /></td>
                                <td style="background: #F2F2F2">
                                    <input type="button" value="-" class="bt1" onclick="ClearAndRemove1()" style="width: 20px;" /></td>
                                <td style="background: #F2F2F2">
                                    <input type="button" id="btAdd" onclick="clickAdd(0); this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" />
                                </td>
                                <td style="background-color: #F2F2F2">
                                    <input id="hdnDetailID0" type="hidden" />
                                    <input id="hdnQty0" type="hidden" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <div id="maindiv">
                    </div>
                </div>

            </div>

        </div>


        <asp:HiddenField ID="hdnfileID" runat="server" />
        <asp:HiddenField ID="HiddenPatientID" runat="server" />

        <asp:HiddenField ID="hdnXmlData" runat="server" />
        <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
        <asp:HiddenField ID="hdnTextboxValues" runat="server" />
        <asp:HiddenField ID="hdnRemovedIDs" runat="server" />
        <asp:HiddenField ID="hdnPrescID" runat="server" />

        <asp:HiddenField ID="Patientidtorefill" runat="server" />
    </div>


    <!-- Modal -->
    <div id="patient_list" class="modal fade" role="dialog">
        <div class="modal-dialog" style="height: 600px;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Patient List</h4>
                </div>
                <div class="modal-body" style="height: 500px; overflow-y: scroll; overflow-x: hidden;">

                    <asp:GridView ID="GridViewPharmacylist" runat="server" AutoGenerateColumns="False" DataKeyNames="PatientID">

                        <Columns>
                            <asp:TemplateField ItemStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="../images/paper.png" CommandArgument='<%# Eval("PatientID")+"|" + Eval("DoctorID") %>' OnCommand="ImgBtn_Command" ImageAlign="Middle" BorderColor="White" formnovalidate />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Doctor" DataField="DOCNAME" />
                            <asp:BoundField HeaderText="Token No" DataField="TokenNo" />
                            <asp:BoundField HeaderText="Patient Name" DataField="Name" />
                            <asp:BoundField HeaderText="DoctorID" Visible="false" DataField="DoctorID" />
                            <asp:BoundField HeaderText="PatientID" Visible="false" DataField="PatientID" />

                        </Columns>
                    </asp:GridView>


                </div>
            </div>

        </div>
    </div>

    <style>
        .table{
        margin-bottom: 3px!important;
        background-color:#F2F2F2!important;
        border:0!important;
        
    }
        table td {
            border: 0;
            border-top: 1px solid #F2F2F2 !important;
            border-left: 1px solid #F2F2F2 !important;
            background-color:white;
        }
        .table td.add{
            background-color:#F2F2F2!important;
        }
    </style>
</asp:Panel>
</asp:Content>
