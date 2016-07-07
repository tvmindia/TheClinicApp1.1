<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="TheClinicApp1._1.Registration.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Script for Custom Alert Box For Checking Uploded image is Supported or not--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel DefaultButton="btnSave" runat="server">
        <style type="text/css">
            /* Simple message styles customization */
            #errors {
                border-left: 5px solid #a94442;
                padding-left: 15px;
            }

                #errors li {
                    list-style-type: none;
                }

                    #errors li:before {
                        content: '\b7\a0';
                    }
        </style>

        <style>
            /*Custom Style for the table inside the modal popup*/
            .modal thead {
                background-color: #5681e6;
                text-align: center;
                color: white;
                font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                font-size: 16px;
            }

            .modal table {
                border-collapse: collapse;
                width: 525px;
            }

                .modal table td {
                    text-align: left;
                    height: auto;
                    padding: 8px;
                }

                .modal table td {
                    width: 45px;
                    height: 10px;
                }

                    .modal table td + td + td {
                        width: auto;
                        height: auto;
                        font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                        font-size: 14px;
                        font-weight: 200;
                        padding-left: 4px;
                    }
                /*.modal table tr:nth-child(even){background-color: #f2f2f2}*/
                .modal table th {
                    background-color: #5681e6;
                    text-align: center;
                    color: white;
                    font-family: Cambria, Cochin, Georgia, Times, Times New Roman, serif;
                    font-size: 16px;
                }
        </style>
        <!-- Script Files -->
        <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/jquery-1.3.2.min.js"></script>
        <script src="../js/jquery-1.12.0.min.js"></script>

        <script src="../js/fileinput.js"></script>
        <script src="../js/JavaScript_selectnav.js"></script>
        <script src="../js/DeletionConfirmation.js"></script>
        <script src="../js/Messages.js"></script>
        <script src="../js/Dynamicgrid.js"></script>
        <!---   Script for fileupload preview & FileType Checking  Created By:Thomson Kattingal --->
        <script type="text/javascript">
            function showpreview(input) 
            {
                if (input.files && input.files[0]) 
                {
                    var reader = new FileReader();
                    reader.onload = function (e) 
                    {
                        $('#<%=ProfilePic.ClientID %>').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    }           
                }
         
                var validFiles = ["bmp", "gif", "png", "jpg", "jpeg"];
                function OnUpload() 
                {
                    var obj = document.getElementById("<%=FileUpload1.ClientID%>");
              var source = obj.value;
              var ext = source.substring(source.lastIndexOf(".") + 1, source.length).toLowerCase();
              for (var i = 0; i < validFiles.length; i++) 
              {
                  if (validFiles[i] == ext)
                      break;
              }
              if (i >= validFiles.length) 
              {
                  Alert.render("Format Not Supporting\n\n Try:" + validFiles.join(", "));
                  document.getElementById("<%=FileUpload1.ClientID%>").value = '';
            }
            return true;
        }
            function bindPatient(){                
                if (document.getElementById("txtSearch").innerText !="")
                $('#<%=btnSearch.ClientID%>').click();
        }
         
        </script>
        <!--------------------------------------------------------------------->
        <!---   Script Nav button to srink and reform, AutoComplete Textbox, Table Pagination in document Ready --->
        <script src="../js/jquery.tablePagination.0.1.js"></script>
        <script type="text/javascript">
            $(document).ready( function (){
                debugger;
                var ac=null;
                ac = <%=listFilter %>;
                $( "#txtSearch" ).autocomplete({
                    source: ac,
                    select: function(event, ui){
                        $( "#txtSearch" ).val( ui.item.label );
                        $('#<%=btnSearch.ClientID%>').click();
                    }



                  
            
                });


            
                $(function () {
               
                    $('[data-toggle="tooltip"]').tooltip()
                })

                $('.alert_close').click(function () {
                    $(this).parent(".alert").hide();
                });


                $('.nav_menu').click(function () {
                    $(".main_body").toggleClass("active_close");
                });
            
         

                var rows = $('#<%=GridView1.ClientID%> tr').not('thead tr');

            $('#txtSearchPatient').keyup(function() {                  
                var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase().split(' ');

                rows.hide().filter(function() {                  
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    var matchesSearch = true;
                    $(val).each(function(index, value) {                       
                        matchesSearch = (!matchesSearch) ? false : ~text.indexOf(value);
                    });                                   
                    return matchesSearch;                    
                }).show();

                debugger;
                //finding the row of html table displaying while searching 
                var numOfVisibleRows = $('tbody tr').filter(function() {
                    return $(this).css('display') !== 'none';
                }).length;

 

                //number of rows while no records found is 1
                if (numOfVisibleRows==1)
                {
                    debugger;
                    $('#norows').remove();
                    var bodyId="tbdy";              
                    $('table').attr('id', bodyId);
                    var textdis="No records found.";
                    var html = '<div id="norows" style="width:100%; padding-left: 200px;">'+textdis+'</div>';                  
                    $('#tbdy').after(html);                
                }
                else
                {
                    $('#norows').remove();
                } 

                $('#tablePagination').remove();
                if (val == "") {                 
                    $('table').tablePagination({
                        rowCountstart:1,
                        rowCountend :7
                    });
                }               
            });
          

         
            var rowss = $('#<%=dtgViewTodaysRegistration.ClientID%> tr').not('thead tr');           


            $('#txtSearchPatient1').keyup(function() {
                debugger;
                var val1 = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase().split(' ');

                rowss.hide().filter(function() {
                    var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
                    var matchesSearch = true;
                    $(val1).each(function(index, value) {
                        
                        matchesSearch = (!matchesSearch) ? false : ~text.indexOf(value);
                    });
                    return matchesSearch;
                }).show();
                
              debugger;
                //finding the row of html table displaying while searching 
                var numOfVisibleRows1 = $('tbody tr').filter(function() {
                    return $(this).css('display') !== 'none';
                }).length;

 

                //number of rows while no records found is 2
                if (numOfVisibleRows1==7)
                {
                    debugger;
                    $('#norows1').remove();
                    //var bodyId1="tbdy1";              
                   // $('thead').attr('id', bodyId1);
                    var text1="No records found.";
                    var html1 = '<div id="norows1" style="width:100%; padding-left: 200px;">'+text1+'</div>';     
                   // $('#ContentPlaceHolder1_dtgViewTodaysRegistration').append(html1);
                    $('#ContentPlaceHolder1_dtgViewTodaysRegistration').after(html1);                
                }
                else
                {
                    $('#norows1').remove();
                } 


                $('#tablePagination').remove();
                if (val1 == "") {
                    debugger;
                    $('table').tablePagination({
                        rowCountstart:1,
                        rowCountend :7
                    });
                    $('#tablePagination').show();
                }
               
            });
            

            }); 
        
           

        </script>
        <script>
       
            function validation()
            {                     
                if( ($('#<%=txtName.ClientID%>').val()=="")||  ($('#<%=txtAge.ClientID%>').val()=="") )
                {
                    var lblclass = Alertclasses.danger;
                    var lblmsg = msg.Requiredfields;
                    var lblcaptn = Caption.Confirm;
                    ErrorMessagesDisplay('<%=lblErrorCaption.ClientID%>','<%=lblMsgges.ClientID%>','<%=Errorbox.ClientID%>' ,lblclass,lblcaptn,lblmsg);

                return false;
            }
            else
            {
                return true;
            }


        }
        
        function OpenModal(id){
            if(id=='1')            
            {
                $('#tablePagination').remove();
                $('table').tablePagination({
                    rowCountstart:1,
                    rowCountend :7,
                    currPage:1
                });
                $('#tablePagination').show();
                $('#txtSearchPatient').val('');
                $('#myModal').modal('show');
            }
            else if(id==2)
            {
                $('#tablePagination').remove();
                $('table').tablePagination({
            
                    rowCountstart:1,
                    rowCountend :7
                });              
                $('#txtSearchPatient1').val('');
                $('#TodaysRegistration').modal('show');
            }
        }

       
        </script>
        <!------------------------------------------------------------------------------>
        <!---------------------------------------------------------------------->
        <!------------------------------------->

        <!-- Main Container -->
        <div class="main_body">

            <!-- Left Navigation Bar -->
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                    <li id="patients" class="active"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                    <li id="token"><a name="hello" onclick="selectTile('token')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor"><a name="hello" onclick="selectTile('doctor')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                    <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>

                </ul>
                <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
            </div>

            <!-- Right Main Section -->
            <div class="right_part">
                <div class="tagline">
                    <a class="nav_menu">Menu</a>
                    Patients Registration
                    <ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                        <li>
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate /></li>
                    </ul>
                </div>
                <div class="icon_box">
                    <%--<a class="all_registration_link" data-toggle="modal" data-target="#myModal" ><span title="All Registerd" data-toggle="tooltip" data-placement="left" onclick="SetIframeSrc('AllRegistrationIframe')"><img src="../images/registerd9724185.png" /></span></a>--%>

                    <a class="all_registration_link" onclick="OpenModal('1');"><span class="count"><asp:Label ID="lblRegCount" runat="server" Text="0"></asp:Label></span><span title="All Registered" data-toggle="tooltip" data-placement="left">
                        <img src="../images/registerd9724185.png" /></span></a>
                    <a class="Todays_registration_link" onclick="OpenModal('2');"><span class="count"><asp:Label ID="lblTodayRegCount" runat="server" Text="0"></asp:Label></span><span title="Todays Register" data-toggle="tooltip" data-placement="left">
                        <img src="../images/registerd.png" /></span></a>
                </div>
                <div class="grey_sec">
                    <div class="search_div">

                        <input class="field" type="search" id="txtSearch" onblur="bindPatient();" name="txtSearch" placeholder="Search patient..." />
                        <input class="button" type="button" id="btnSearch" value="Search" runat="server" onserverclick="btnSearch_ServerClick" />
                    </div>
                    <ul class="top_right_links">
                        <li>
                            <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="button1" OnClientClick="return validation();" OnClick="btnSave_Click" /></li>
                        <li><a class="new" href="Patients.aspx" runat="server" id="btnNew"><span></span>New</a></li>
                    </ul>
                </div>
                <div class="right_form">

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
                    <div class="registration_form">
                        <div class="row field_row">
                            <div class="col-lg-8">
                                <div class="row">
                                    <div class="col-lg-8 margin_bottom">
                                        <label for="name">Name</label><input id="txtName" runat="server" type="text" name="name" pattern="^\S+[A-z][A-z\.\s]+$" title="⚠ The Name is required and it allows alphabets only." />
                                    </div>
                                    <div class="col-lg-4 upload_photo_col">
                                        <div class="margin_bottom upload_photo">
                                            <img id="ProfilePic" src="~/images/UploadPic1.png" style="height: 142px;" runat="server" />
                                        </div>
                                        <div class="upload">
                                            <label class="control-label">Upload Picture</label>

                                            <asp:FileUpload ID="FileUpload1" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload();showpreview(this);" />
                                        </div>
                                    </div>
                                    <div class="col-lg-8">
                                        <label for="sex">Sex<asp:RadioButton ID="rdoMale" runat="server" GroupName="Active" Text="Male" CssClass="checkbox-inline" Width="9%" /><asp:RadioButton ID="rdoFemale" runat="server" GroupName="Active" Text="Female" CssClass="checkbox-inline" Width="9%" /></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label for="age">Age</label><input id="txtAge" runat="server" type="number" name="age" min="1" pattern="\d*" title="⚠ The Age is required and entry should be Numbers no Negative Values Expected." />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row field_row">
                            <div class="col-lg-8">
                                <label for="address">Address</label><input name="address" id="txtAddress" type="text" runat="server" />
                            </div>
                        </div>

                        <div class="row field_row">
                            <div class="col-lg-4">
                                <label for="mobile">Mobile</label><input id="txtMobile" runat="server" type="text" name="mobile" minlength="5" pattern="\d*" title="⚠ This entry can only contain Numbers." />
                            </div>
                            <div class="col-lg-4">
                                <label for="email">Email</label><input id="txtEmail" runat="server" type="email" name="email" title="⚠ Invalid Email Check format expects testname@test.te" />
                            </div>
                        </div>
                        <asp:HiddenField ID="HdnFirstInsertID" runat="server" />
                        <asp:HiddenField ID="Hdnimagetype" runat="server" />
                        <div class="row field_row">
                            <div class="col-lg-4">
                                <label for="marital">Marital</label>
                                <asp:DropDownList ID="ddlMarital" CssClass="drop" runat="server" Width="100%" Height="31px">
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
        <!-- All Registration -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog" style="min-width: 550px;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: #3661C7;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">All Registration</h3>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <%--<iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>--%>
                        <div class="col-lg-12" style="height: 480px;">
                            <div class="col-lg-12" style="height: 40px">
                                <div class="search_div">
                                    <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchPatient" />
                                    <input class="button3" type="button" value="Search" />
                                </div>
                            </div>
                            <div class="col-lg-12" style="height: 400px;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EnableModelValidation="true" OnPreRender="GridView1_PreRender" GridLines="Horizontal">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Editicon1.png" CommandName="Comment" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("Occupation")+"|"+Eval("ImageType")%>' OnCommand="ImgBtnUpdate_Command" formnovalidate />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/Deleteicon1.png" CommandName="CommentDelete" CommandArgument='<%# Eval("PatientID")%>' OnClientClick="return ConfirmDelete();" OnCommand="ImgBtnDelete_Command" formnovalidate />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address"></asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Token Registration Modal -->
        <div class="modal fade" id="TokenRegistration" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: #3661C7;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">Token Registration</h3>
                    </div>
                    <div class="modal-body" style="">
                        <div class="token_id_card">
                            <div class="name_field1">Would You like To Book a Token...<span></span> ?</div>
                            <div class="light_grey">
                            </div>
                            <div class="card_white">
                                <asp:Label Text="Select Your Doctor " Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlDoctorName" Height="70%" AppendDataBoundItems="true" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="background-color: grey;">
                        <ul class="top_right_links">
                            <li>
                                <asp:Button ID="btntokenbooking" runat="server" Text="Book" CssClass="button1" OnClick="btntokenbooking_Click" /><span></span><span></span><span></span></li>
                            <li><a class="new" href="#" id="NewA1" data-dismiss="modal"><span></span>Skip</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <!-- Todays Registration Modal -->
        <div class="modal fade" id="TodaysRegistration" role="dialog">
            <div class="modal-dialog" style="min-width: 550px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: #3661C7;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3 class="modal-title">Today's Registrations</h3>

                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <div class="col-lg-12" style="height: 480px;">
                            <div class="col-lg-12" style="height: 40px">
                                <div class="search_div">
                                    <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchPatient1" />
                                    <input class="button3" type="button" value="Search" />
                                </div>
                            </div>
                            <div class="col-sm-12" style="height: 400px;">
                                <asp:GridView ID="dtgViewTodaysRegistration" runat="server" AutoGenerateColumns="False" EnableModelValidation="true" OnPreRender="dtgViewTodaysRegistration_PreRender" GridLines="Horizontal">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnUpdate1" runat="server" ImageUrl="~/Images/Editicon1.png" CommandArgument='<%# Eval("PatientID")+"|" + Eval("Name") + "|" + Eval("Address")+"|"+ Eval("Phone")+"|"+ Eval("Email")+"|"+Eval("DOB")+"|"+Eval("Gender")+"|"+Eval("MaritalStatus")+"|"+Eval("Occupation")+"|"+Eval("ImageType")%>' OnCommand="ImgBtnUpdate_Command" formnovalidate />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnDelete1" runat="server" ImageUrl="~/Images/Deleteicon1.png" CommandName="CommentDelete" CommandArgument='<%# Eval("PatientID")%>' OnClientClick="return ConfirmDelete();" OnCommand="ImgBtnDelete_Command" formnovalidate />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="Phone"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
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
        <!--------------------->
        <!------------------------------------------------------------------------------------------>
        <!-- Script Files -->
        <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/jquery-1.3.2.min.js"></script>
        <script src="../js/jquery-1.12.0.min.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/bootstrap.min.js"></script>

        <script src="../js/jquery.tablePagination.0.1.js"></script>
        <!---   Script includes function for open Modals preview, Created By:Thomson Kattingal --->
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <script type="text/javascript">  
    <!---Function for Open Token Registration Modal and All Registarion Modal----->
    function openModal() {
        
        $('#TokenRegistration').modal('show');
           
    }
    function openmyModal() {
      
        $('#myModal').modal('show');
        
    }

    
        </script>

        <style>



</style>
        <!---------------------------------------------------------------------------------------->
        <!------------------------------------->
    </asp:Panel>
</asp:Content>
