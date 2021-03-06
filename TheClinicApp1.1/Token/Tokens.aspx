﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" CodeBehind="Tokens.aspx.cs" Inherits="TheClinicApp1._1.Token.Tokens" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel DefaultButton="btnBookToken" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
        <style>
            .hello {
                font-size: 30px;
                font-family: 'Footlight MT';
                font-weight: bold;
            }
       
        </style>

        <script src="../js/jquery-1.9.1.min.js"></script>
        <script src="../js/vendor/jquery-1.11.1.min.js"></script>
        <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/JavaScript_selectnav.js"></script>
        <script src="../js/Dynamicgrid.js"></script>
        <script src="../Scripts/DeletionConfirmation.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/bootstrap.min.js"></script>
        <script src="../js/Messages.js"></script>
        <script>
            $(document).ready(function () {
                debugger;
                //$("#AllTokenIframe").prop('contentWindow').GetTokenBooking(parseInt(1));


              

                $('.alert_close').click(function () {                
            
                    $(this).parent(".alert").hide();            
                });      

                $('[data-toggle="tooltip"]').tooltip();
        
                $('.nav_menu').click(function () {
                
                    $(".main_body").toggleClass("active_close");
                });  
        
                var ac=null;
           

                ac = <%=listFilter %>;
                var length= ac.length;
                var projects = new Array();
                for (i=0;i<length;i++)
                {  
                    var name= ac[i].split('🏠');
                    projects.push({  value : name[0], label: name[0], desc: name[1]})   
                }

                $( "#txtSearch" ).autocomplete({
                    maxResults: 10,
                    source: function(request, response) {

//--- Search by name or description(file no , mobile no, address) , by accessing matched results with search term and setting this result to the source for autocomplete
            var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
            var matching = $.grep(projects, function (value) {

                var name = value.value;
                var  label= value.label;
                var desc= value.desc;

                return matcher.test(name) || matcher.test(desc);
            });
                        var results = matching; // Matched set of result is set to variable 'result'

                        response(results.slice(0, this.options.maxResults));
                    },
                    focus: function( event, ui ) {
                        $( "#txtSearch" ).val( ui.item.label );
                 
                        return false;
                    },
                    select: function( event, ui ) {


                        $( "#project" ).val( ui.item.label );
      
                        $( "#project-description" ).html( ui.item.desc );                  
                    
                        bindPatientDetails();
                        document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                        return false;
                    }
                })
            .autocomplete( "instance" )._renderItem = function( ul, item ) {
                return $( "<li>" )
                  .append( "<a>" + item.label + "<br>" + item.desc + "</a>" )
                  .appendTo( ul );
            };             
            });   
        </script>

        <script>
            function SetIframeSrc(HyperlinkID) {          
                if (HyperlinkID == "AllTokenIframe") {
                    var AllTokenIframe = document.getElementById('AllTokenIframe');
                    AllTokenIframe.src = "ViewTokens.aspx";
                    //$('#OutOfStock').modal('show');
                }
            }
            function CheckddlDoctor()
            { 
                debugger;
                if ( $('#<%=hdnfileID.ClientID%>').val()!="" && $('#<%=ddlDoctor.ClientID%>').val()=="--Select--")
                {
                    debugger;
                    var lblclass = Alertclasses.danger;
                    var lblmsg = msg.SelectDoctor;
                    var lblcaptn = Caption.Confirm;

                    ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                    return false;
                }
                else
                {
                    debugger;
                    return true;
                } 

            }
        
            function bindPatientDetails()
            {
           debugger;
                // $(".alert").hide(); 
 
                var PatientName = document.getElementById("project-description").innerText;
                document.getElementById('<%=lblToken.ClientID%>').innerHTML="_";       
                     
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
                    document.getElementById('<%=lblFileNo.ClientID%>').innerHTML=string1[0];
                    document.getElementById('<%=lblPatientName.ClientID%>').innerHTML=string1[1];
                    document.getElementById('<%=lblAge.ClientID%>').innerHTML=string1[2];
                    document.getElementById('<%=lblGender.ClientID%>').innerHTML=string1[3];
                    document.getElementById('<%=lblAddress.ClientID%>').innerHTML=string1[4];
                    document.getElementById('<%=lblMobile.ClientID%>').innerHTML=string1[5];
                    document.getElementById('<%=lblEmail.ClientID%>').innerHTML=string1[6];
                    document.getElementById('<%=HiddenPatientID.ClientID%>').value=string1[7];
                    document.getElementById('<%=HiddenClinicID.ClientID%>').value=string1[8];
                    document.getElementById('<%=lblLastVisit.ClientID%>').innerHTML=string1[9];
                    if(document.getElementById('<%=BookedDoctorName.ClientID%>') != null)
                    {
                        document.getElementById('<%=BookedDoctorName.ClientID%>').style.visibility= 'hidden';
                    }              
                    document.getElementById('txtSearch').value="";//clearin the earch box  
                    document.getElementById('DropDownDoctor').style.visibility= 'visible';
                    debugger;

                    document.getElementById('#<%=ddlDoctor.ClientID%>').selectedIndex = 0;

    }          
    function onError(response, userContext, methodName)
    {     
    }  
    document.getElementById('txtSearch').value="";
            }


           function SetGridviewRowCount(GridRowCount)
            {
                $("#<%=lblCaseCount.ClientID %>").text(GridRowCount);
            }



        </script>

        <!-- #main-container -->
        <div class="main_body">
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                    <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName %>')"><span class="icon registration"></span><span class="text">Registration</span></a></li>
                     <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
                    <li id="token" class="active"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor"><a name="hello" onclick="selectTile('doctor','<%=RoleName %>')"><span class="icon doctor"></span><span class="text">Doctor's OP</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName %>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName %>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server" ClientIDMode="Static"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Masters</span></a></li>
                    <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
                </ul>

                <p class="copy">
                    &copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label>
                </p>
            </div>

            <div class="right_part">
                <div class="tagline">
                    <a class="nav_menu">nav</a>
                    Tokens<ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                        <li>
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClientClick="redirect();" OnClick="LogoutButton_Click" formnovalidate /></li>
                    </ul>
                </div>
                <div class="icon_box">
                    
                    <a class="all_token_link" data-toggle="modal" data-target="#all_token" onclick="SetIframeSrc('AllTokenIframe')">
                         <span class="tooltip1">
                        <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label>
                        </span>                     
                            <img src="../images/tokens.png" />
                      <span class="tooltiptext1">Today's Tokens</span>
                          </span>
                    </a>
                </div>
                <div class="grey_sec">
                    <div class="search_div">
                        <input class="field" id="txtSearch" onblur="bindPatientDetails()" name="txtSearch" type="search" placeholder="Search patient..." />
                        <input type="hidden" id="project-id" />
                        <p id="project-description" style="display: none"></p>
                        <%--  <input class="button" onserverclick="btnSearch_ServerClick" runat="server"  value="Search" />--%>
                        <input id="btnSearch" class="button" type="button" value="Search" disabled />
                    </div>
                    <ul class="top_right_links">
                        <li>
                            <asp:Button ID="btnBookToken" CssClass="button1" Style="width: 125px !important;" runat="server" Text="Book Token" OnClientClick="return CheckddlDoctor();" OnClick="btnBookToken_ServerClick" />
                        </li>
                        <li style="display: none"><a class="new" href="Tokens.aspx"><span></span>New</a></li>
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
                  <div class="" style="padding-top:20px;padding-left:50px;" id="Div1" runat="server">
                            <label>Search & Select a Patient, then Book Token </label>
                        </div>

                <div class="right_form">                    
                 
                      
                                          
                  

                    <div style="height: 40px"></div>
                    <div>

                        <div class="token_id_card">
                            <div class="name_field">
                                <asp:Label ID="lblPatientName" runat="server" Text="Name"></asp:Label><span class="generate_token"><asp:Label ID="lblToken" runat="server" Text="_"></asp:Label></span>
                            </div>
                            <div class="light_grey">
                                <div class="col3_div">Age<span><asp:Label ID="lblAge" runat="server" Font-Size="Large"></asp:Label></span></div>
                                <div class="col3_div">Gender<span><asp:Label ID="lblGender" runat="server" Font-Size="Large"></asp:Label></span></div>
                                <div class="col3_div">File No<span><asp:Label ID="lblFileNo" runat="server" Font-Size="Large"></asp:Label></span></div>
                            </div>
                            <div class="card_white">
                                <div class="field_label">
                                    <label>Address</label><asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </div>
                                <div class="field_label">
                                    <label>Mobile</label><asp:Label ID="lblMobile" runat="server"></asp:Label>
                                </div>
                                <div class="field_label">
                                    <label>Email</label>
                                    <a href="mailto: demo@test.com">
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label></a>
                                </div>
                                <div class="field_label">
                                    <label>Last visit</label><asp:Label ID="lblLastVisit" runat="server"></asp:Label>
                                </div>

                                <div class="field_label" id="BookedDoctorName" visible="false" runat="server">
                                    <label id="labelbookeddoctor">Doctor</label>
                                    <asp:Label ID="lblDoctor" runat="server"></asp:Label>
                                </div>
                                <br />
                                <br />
                                <div class="field_label" id="DropDownDoctor" style="visibility: hidden">
                                    <label>Doctor</label><asp:DropDownList ID="ddlDoctor" Width="60%" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </div>


        <!-- Hidden Fields -->
        <asp:HiddenField ID="HiddenPatientID" runat="server" />
        <asp:HiddenField ID="HiddenClinicID" runat="server" />
        <asp:HiddenField ID="hdnfileID" runat="server" />

        <!-- Modal -->
        <div id="all_token" class="modal fade" role="dialog">
            <div class="modal-dialog" style="min-width: 550px;">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: royalblue;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                        <h3 class="modal-title">Today's Patient Bookings</h3>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <div class="col-lg-12" style="height: 480px;">
                            <iframe id="AllTokenIframe" scrolling="no" style="width: 100%; height: 100%;" frameborder="0"></iframe>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
</asp:Content>
