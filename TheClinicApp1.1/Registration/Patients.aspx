<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="TheClinicApp1._1.Registration.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Script for Custom Alert Box For Checking Uploded image is Supported or not--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>

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
                 .reged{background-color: #ECFCEA!important;} 
                 .even{background-color: white;} 

                
            
        </style>
        <!-- Script Files -->
        <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/jquery-1.3.2.min.js"></script>
        <script src="../js/jquery-1.12.0.min.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/bootstrap.min.js"></script>
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
            //function Checkfiles(f){
            //    debugger;
            //    f = f.elements;
            //    if(/.*\.(gif)|(jpeg)|(jpg)|(doc)$/.test(f['filename'].value.toLowerCase()))
            //        return true;
            //    alert('Please Upload Gif or Jpg Images, or Doc Files Only.');
            //    f['filename'].focus();
            //    return false;
            //};
                var validFiles = ["bmp", "gif", "png", "jpg", "jpeg"];
                function OnUpload(f) 
                {
                    document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";

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
        <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>
        <script type="text/javascript">
            $(document).ready( function (){
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
                        debugger;
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

                        BindPatientDetails();
                        document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";


                        return false;
                    }
                })
            .autocomplete( "instance" )._renderItem = function( ul, item ) {
                return $( "<li>" )
                  .append( "<a>" + item.label + "<br>" + item.desc + "</a>" )
                  .appendTo( ul );

            };             

                $('.alert_close').click(function () {

                    $(this).parent(".alert").hide();
                });


                $('.nav_menu').click(function () {
                    $(".main_body").toggleClass("active_close");
                });
            
            }); 
 //-----------------------------------------------------------------------------------script validation Author: Thomson Kattingal-----------------------------------//       
            function validate()
            {
             
                var ictrl;
                var check=0;
                var regex = /^[a-zA-Z0-9,.;:"'@#$%*+! ]{0,255}$/;
                var ctrl =[];
                var domelement = document.querySelectorAll("input[type=text],textarea");
                var domcount=0;
                for(domcount;domcount<domelement.length;domcount++)
                {
                    ctrl.push(domelement[domcount].value);
                }
                              
                for(ictrl=0;ictrl<ctrl.length;ictrl++)
                {
                    if (regex.test(ctrl[ictrl])) 
                    {
                        check=1;
                    }
                    else {
                        document.getElementById('<%=Errorbox.ClientID%>').style.display="block";  
                        document.getElementById('<%=Errorbox.ClientID%>').className="alert alert-danger";
                        document.getElementById('<%=lblErrorCaption.ClientID%>').innerHTML="Warning!";         
                        document.getElementById('<%=lblMsgges.ClientID%>').innerHTML="We can't accept brackets or parentheses";
                        check=0;
                        return false;
                    }
                }
                if(check==1){
                    return true;
                }
            }
//--------------------------------------------------------------------------------------------------------------------------------------------------//
        </script>
        <script>
       
            function validation()
            {    
                return validate();
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
                    $('#txtSearchPatient').val('');

                    $('#myModal').modal('show');

                    GetAllPatients(1);
                }
                else if(id==2)
                {
                    $('#txtSearchTodayPatient').val('');
                    $('#TodaysRegistration').modal('show');
                    GetTodayPatients(1);
                }
                else if(id==3)
                {
                    $('#txtSearchTodayAppointment').val('');
                    $('#TodaysAppointment').modal('show');
                    GetTodayPatientAppointments(1);
                }

            }
        </script>
        <script>
            //---getting data as json-----//
            function getJsonData(data, page) {
                var jsonResult = {};
                var req = $.ajax({
                    type: "post",
                    url: page,
                    data: data,
                    delay: 3,
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"

                }).done(function (data) {
                    jsonResult = data;
                });
                return jsonResult;
            }

            function ConvertJsonToDate(jsonDate) {
                if (jsonDate != null) {
                    var dateString = jsonDate.substr(6);
                    var currentTime = new Date(parseInt(dateString));
                    var month = currentTime.getMonth();
                    var day = currentTime.getDate();
                    var year = currentTime.getFullYear();
                    var monthNames = [
                                  "Jan", "Feb", "Mar",
                                  "Apr", "May", "Jun", "Jul",
                                  "Aug", "Sep", "Oct",
                                  "Nov", "Dec"
                    ];
                    var result = day + '-' + monthNames[month] + '-' + year;
                    return result;
                }
            }

            function GetPatientDetailsByID(Patient) {
                debugger;
                var ds = {};
                var table = {};
                var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
                ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetailsOnEditClick");
                table = JSON.parse(ds.d);
                return table;
            }
            function GetPatientAppointment(Appointments)
            {
                var ds = {};
                var table = {};
                var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
                ds = getJsonData(data, "../Appointment/Appointment.aspx/GetPatientAppointmentDetailsByAppointmentID");
                table = JSON.parse(ds.d);
                return table;
            }
            //Click event function for search patient and bind
            function BindPatientDetails()
            {
                var jsonPatient = {};
                var SearchItem = $('#txtSearch').val();
                var Patient = new Object();
                if(SearchItem != '')
                { 
                    Patient.Name = SearchItem;

                    jsonPatient = GetPatientDetails(Patient);
                    if (jsonPatient != undefined)
                    {
                          
                        BindPatient(jsonPatient);
                    }
                }
            }
            //Get Whole data using json and server side code  search
            function GetPatientDetails(Patient) {

                var ds = {};
                var table = {};
                var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
                ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetails");
                table = JSON.parse(ds.d);
                return table;
            }
            //Bind Registration form from search
            function BindPatient(Records)
            {
                $("#<%=txtName.ClientID %>").val(Records.Name);
                $("#<%=txtAge.ClientID %>").val(Records.Age);
                $("#<%=txtAddress.ClientID %>").val(Records.Address);
                $("#<%=txtMobile.ClientID %>").val(Records.Phone);
                $("#<%=txtEmail.ClientID %>").val(Records.Email);
                $("#<%=txtOccupation.ClientID %>").val(Records.Occupation);
                $("#<%=Hdnimagetype.ClientID %>").val(Records.ImageType);
                $("#<%=HiddenField1.ClientID %>").val(Records.PatientID);
                if (Records.Gender ==  "Male") {
                    $("#<%=rdoMale.ClientID %>").prop('checked', true);
                }
                else {
                    $("#<%=rdoFemale.ClientID %>").prop('checked', true);
                }

                $("#<%=ddlMarital.ClientID %> option:contains(" + Records.MaritalStatus + ")").attr('selected', 'selected');
                var   imagetype =Records.ImageType;
                var patientid = Records.PatientID;
                var ProfilePic = document.getElementById("<%=ProfilePic.ClientID%>");
                if (imagetype != '')
                {
                    ProfilePic.src = "../Handler/ImageHandler.ashx?PatientID=" + Records.PatientID;
                }
                else
                {
                   ProfilePic.src = "../images/UploadPic1.png";
                }
            }

            //Bind Registration form from Edit click in modal popup
            function BindControlsWithPatientDetails(Records) 
            {
                    $("#<%=txtName.ClientID %>").val(Records.Name);
                    $("#<%=txtAge.ClientID %>").val(Records.Age);
                    $("#<%=txtAddress.ClientID %>").val(Records.Address);
                    $("#<%=txtMobile.ClientID %>").val(Records.Phone);
                    $("#<%=txtEmail.ClientID %>").val(Records.Email);
                    $("#<%=txtOccupation.ClientID %>").val(Records.Occupation);
                    $("#<%=Hdnimagetype.ClientID %>").val(Records.ImageType);
                    $("#<%=HiddenField1.ClientID %>").val(Records.PatientID);
           
                    if (Records.Gender ==  "Male") {
                        $("#<%=rdoMale.ClientID %>").prop('checked', true);
                    }
                    else {
                        $("#<%=rdoFemale.ClientID %>").prop('checked', true);
                    }

                    $("#<%=ddlMarital.ClientID %> option:contains(" + Records.MaritalStatus + ")").attr('selected', 'selected');

                    var imagetype =Records.ImageType;
                    var patientid =  Records.PatientID;
                    var ProfilePic = document.getElementById("<%=ProfilePic.ClientID%>")  ;

                    if (imagetype != '')
                    {
                        ProfilePic.src = "../Handler/ImageHandler.ashx?PatientID=" + Records.PatientID;
                    }
                    else
                    {
                        ProfilePic.src = "../images/UploadPic1.png";
                    }
                    $("#AllRegistrationClose").click();
                }



            function BindControlsWithPatientAppointmentDetails(Records) //KK
            {
                $.each(Records, function (index, Records) {
                   
                    $("#<%=txtName.ClientID %>").val(Records.Name);
                   
                    $("#<%=txtAddress.ClientID %>").val(Records.Location);
                    $("#<%=txtMobile.ClientID %>").val(Records.Mobile);
                   
                 
                    $("#<%=hdfAppointmentID.ClientID %>").val(Records.AppointmentID);
           
                });

                $("#TodayAppointmentClose").click();

            }

             
        </script>

        <link href="../css/TheClinicApp.css" rel="stylesheet" />
        <script src="../js/jquery-1.8.3.min.js"></script>
        <script src="../js/ASPSnippets_Pager.min.js"></script>
        <script src="../js/jquery-ui.js"></script>

        <script>

            function DeletePatient(Patient) {
                var ds = {};
                var table = {};
                var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
                ds = getJsonData(data, "../Registration/Patients.aspx/DeletePatient");
                table = JSON.parse(ds.d);
                return table;
            }

           

            function DeleteTodayPatientByID(PatientID) {  //Deletion In Today's Registration

                if (PatientID != "") {

                    var Patient = new Object();
                    Patient.PatientID = PatientID;
                    var  response =    DeletePatient(Patient);

                    if (response.isPatientDeleted == false) {

                        $("#TodayRegistrationClose").click();

                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;

                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                    }

                    else {
                        var PageIndx = parseInt(1);

                        if ($(".pgrHistory span")[0] != null && $(".Pager span")[0].innerText != '') {

                            PageIndx = parseInt($(".pgrHistory span")[0].innerText);
                        }

                        GetTodayPatients(PageIndx); 
    
                    }
                }
            }
            //Appointment Cancel from grid
            function CancelTodayAppointementByID(AppointmentID) {  //Cancel Today's Registration

                if (AppointmentID != "") {

                    var Appointments = new Object();
                    Appointments.AppointmentID = AppointmentID;
                    var  response =    CancelAppointment(Appointments);

                    if (response.status == 0) {

                        $("#TodayAppointmentClose").click();

                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;
                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);
                       }
                        else {
                        var PageIndx = parseInt(1);

                        if ($(".pgrHistoryAppointment span")[0] != null && $(".Pager span")[0].innerText != '') {

                            PageIndx = parseInt($(".pgrHistoryAppointment span")[0].innerText);
                        }

                        GetTodayPatientAppointments(PageIndx);
    
                    }
                }
            }



            function DeletePatientByID(PatientID) { //Deletion In All Registration

                if (PatientID != "") {

                    var Patient = new Object();
                    Patient.PatientID = PatientID;
                    var  response =    DeletePatient(Patient);

                    if (response.isPatientDeleted == false) {

                        $("#AllRegistrationClose").click();

                        var lblclass = Alertclasses.danger;
                        var lblmsg = msg.AlreadyUsed;
                        var lblcaptn = Caption.FailureMsgCaption;

                        ErrorMessagesDisplay('<%=lblErrorCaption.ClientID %>', '<%=lblMsgges.ClientID %>', '<%=Errorbox.ClientID %>', lblclass, lblcaptn, lblmsg);

                    }

                    else {
                        var PageIndx = parseInt(1);

                        if ($(".Pager span")[0] != null && $(".Pager span")[0].innerText != '') {

                            PageIndx = parseInt($(".Pager span")[0].innerText);
                        }

                        GetAllPatients(PageIndx);
     
                    }

                }

            }

            //------------------------------- * All Registration Edit Click * -------------------------------//

            $(function () {
                debugger;
                $("[id*=GridView1] td:eq(0)").click(function () { 

                    PatientID = $(this).closest('tr').find('td:eq(5)').text();
                       
                    document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                    
                    var jsonResult = {};
                         
                    var Patient = new Object();
                    Patient.PatientID = PatientID;
                       
                    jsonResult = GetPatientDetailsByID(Patient);
                    if (jsonResult != undefined) {
                          
                        BindControlsWithPatientDetails(jsonResult);
                    }
             
                });

            });


            //------------------------------- * Today's Registration Edit Click * -------------------------------//

            $(function () {
                $("[id*=dtgViewTodaysRegistration] td:eq(0)").click(function () { 
                   
                    PatientID = $(this).closest('tr').find('td:eq(5)').text();
                       
                    document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                    
                    var jsonResult = {};
                      
                    var Patient = new Object();
                    Patient.PatientID = PatientID;
                       
                    jsonResult = GetPatientDetailsByID(Patient);
                    if (jsonResult != undefined) {
                          
                        BindControlsWithPatientDetails(jsonResult);
                    }
                    $("#TodayRegistrationClose").click();
                });

            });


            //------------------------------- * Today's Appointment Edit Click * -------------------------------//

            $(function () {
                debugger;
                $("[id*=dtgTodaysAppointment] td:eq(0)").click(function () { 
                   
                    if($(this).closest('tr').find('td:eq(7)').text()=='00000000-0000-0000-0000-000000000000')//patientid
                    {
                        var AppointmentID = $(this).closest('tr').find('td:eq(6)').text();
                        document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                        var jsonResult = {};
                        var Appointments = new Object();
                        Appointments.AppointmentID = AppointmentID;
                        jsonResult = GetPatientAppointment(Appointments);
                        if (jsonResult != undefined) {
                          
                            BindControlsWithPatientAppointmentDetails(jsonResult);
                        }
                        $("#TodayAppointmentClose").click();
                    }
                });

            });

            //------------------------------- * All Registration Delete Click * -------------------------------//

            $(function () {
                $("[id*=GridView1] td:eq(1)").click(function () {
                  
                    if ($(this).text() == "") {
                        var DeletionConfirmation = ConfirmDelete();
                        if (DeletionConfirmation == true) {
                            PatientID = $(this).closest('tr').find('td:eq(5)').text();
                            DeletePatientByID(PatientID);
                            
                        }
                    }
                });
            });


            //------------------------------- * Today Registration Delete Click * -------------------------------//
            
            $(function () {
                $("[id*=dtgViewTodaysRegistration] td:eq(1)").click(function () {
                   
                    if ($(this).text() == "") {
                        var DeletionConfirmation = ConfirmDelete();
                        if (DeletionConfirmation == true) {
                            PatientID = $(this).closest('tr').find('td:eq(5)').text();
                           
                            DeleteTodayPatientByID(PatientID);
                        }
                    }
                });
            });


            //------------------------------- * Today Appointment Cancel Click * -------------------------------//
            
            $(function () {
                $("[id*=dtgTodaysAppointment] td:eq(2)").click(function () {
                  
                       if ($(this).text() == "") {
                        var CancelConfirmation = ConfirmDelete(true);
                        if (CancelConfirmation == true) {
                         var AppointmentID = $(this).closest('tr').find('td:eq(6)').text();
                         CancelTodayAppointementByID(AppointmentID);
                        }
                    }
                });
            });


            //------------------------------- * Search In All Registration Gridview * -------------------------------//

            $("[id*=txtSearchTodayPatient]").live("keyup", function () {
              
                GetTodayPatients(parseInt(1));
            });

            //------------------------------- * Next Click(Paging) of All Registration Gridview * -------------------------------//
            $(".pgrHistory .page").live("click", function () {
                GetTodayPatients(parseInt($(this).attr('page')));
            });

            function SearchTermInTodayList() {
                return jQuery.trim($("[id*=txtSearchTodayPatient]").val());
            };
            function SearchTermInTodayAppointmentList() {
                return jQuery.trim($("[id*=txtSearchTodayAppointment]").val());
            };

            function GetTodayPatients(pageIndex) {
                $.ajax({
                    type: "POST",
                    url: "../Registration/Patients.aspx/ViewAndFilterTodayPatients",
                    data: '{searchTerm: "' + SearchTermInTodayList() + '", pageIndex: ' + pageIndex + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: TodayRegistrationSuccess,
                    failure: function (response) {

                        alert(response.d);
                    },
                    error: function (response) {

                        alert(response.d);
                    }
                });
            }

            function GetTodayPatientAppointments(pageIndex)
            {
                $.ajax({
                    type: "POST",
                    url: "../Registration/Patients.aspx/ViewAndFilterTodayPatientAppointments",
                    data: '{searchTerm: "' + SearchTermInTodayList() + '", pageIndex: ' + pageIndex + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: TodayAppointmentSuccess,
                    failure: function (response) {

                        alert(response.d);
                    },
                    error: function (response) {

                        alert(response.d);
                    }
                });
            }

            $(function () {
               
                GetAllPatients(1);
                GetTodayPatients(1);
                GetTodayPatientAppointments(1);
            });


            //------------------------------- * Search In Today Registration Gridview * -------------------------------//
            $("[id*=txtSearchPatient]").live("keyup", function () {
               
                GetAllPatients(parseInt(1));
            });

            //------------------------------- * Next Click(Paging) of Today's Registration Gridview * -------------------------------//
            $(".Pager .page").live("click", function () {
                GetAllPatients(parseInt($(this).attr('page')));
            });
           
            function SearchTerm() {
                return jQuery.trim($("[id*=txtSearchPatient]").val());
            };


            //---------------------------------- * Bind All Registration Gridview * -----------------------------//

            function GetAllPatients(pageIndex) {

                $.ajax({

                    type: "POST",
                    url: "../Registration/Patients.aspx/ViewAndFilterAllPatients",
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
                debugger;
                $(".Pager").show();

                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var AllRegistration = xml.find("AllRegistration");

                if (row == null) {
                    row = $("[id*=GridView1] tr:last-child").clone(true);
                }
                $("[id*=GridView1] tr").not($("[id*=GridView1] tr:first-child")).remove();
                if (AllRegistration.length > 0) {
                    
                    $.each(AllRegistration, function () {
                      
                        $("td", row).eq(0).html($('<img id="ImgBtnUpdate" />')
                           .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                        $("td", row).eq(1).html($('<img id="ImgBtnDelete" />')
                         .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                        $("td", row).eq(2).html($(this).find("Name").text());
                        $("td", row).eq(3).html($(this).find("Address").text());


                        $("td", row).eq(4).html($(this).find("Phone").text());
                      
                        $("td", row).eq(5).html($(this).find("PatientID").text());

                        $("[id*=GridView1]").append(row);
                        row = $("[id*=GridView1] tr:last-child").clone(true);
                    });
                    var pager = xml.find("Pager");

                    if ($('#txtSearchPatient').val() == '') {
                        var GridRowCount = pager.find("RecordCount").text();

                        debugger;

                        if(parseInt(GridRowCount,10)>="99")
                        {
                            $("#<%=lblRegCount.ClientID %>").text("99+");
                            $("#<%=lblRegCount.ClientID %>").css('font-size','11px');
                        }
                        else
                        {
                            $("#<%=lblRegCount.ClientID %>").text(GridRowCount);
                        }
                        
                    }

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
                    $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=GridView1]").append(empty_row);

                    $(".Pager").hide();
                }

                var PatienIDColumn = $("[id*=GridView1] th:contains('PatientID')");
                PatienIDColumn.css("display", "none");
                $("[id*=GridView1] tr").each(function () {
                    $(this).find("td").eq(PatienIDColumn.index()).css("display", "none");
                });
                
            };

            row= null;


         //---------------------- * Bind Today's Registration Gridview *--------------------------------------------------//

            var TodayRegRow;
            function TodayRegistrationSuccess(response) {

                $(".pgrHistory").show();
               
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var AllRegistration = xml.find("TodayRegistration");
                if (TodayRegRow == null) {
                    TodayRegRow = $("[id*=dtgViewTodaysRegistration] tr:last-child").clone(true);
                }
                $("[id*=dtgViewTodaysRegistration] tr").not($("[id*=dtgViewTodaysRegistration] tr:first-child")).remove();
                if (AllRegistration.length > 0) {
                    
                    $.each(AllRegistration, function () {
                      
                        $("td", TodayRegRow).eq(0).html($('<img />')
                           .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');

                        $("td", TodayRegRow).eq(1).html($('<img />')
                         .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                        $("td", TodayRegRow).eq(2).html($(this).find("Name").text());
                        $("td", TodayRegRow).eq(3).html($(this).find("Address").text());


                        $("td", TodayRegRow).eq(4).html($(this).find("Phone").text());
                      
                        $("td", TodayRegRow).eq(5).html($(this).find("PatientID").text());

                        $("[id*=dtgViewTodaysRegistration]").append(TodayRegRow);
                        TodayRegRow = $("[id*=dtgViewTodaysRegistration] tr:last-child").clone(true);
                    });
                    var pager = xml.find("Pager");

                    if ($('#txtSearchTodayPatient').val() == '') {
                        var GridRowCount = pager.find("RecordCount").text();

                        $("#<%=lblTodayRegCount.ClientID %>").text(GridRowCount);
                    }

                    $(".pgrHistory").ASPSnippets_Pager({
                        ActiveCssClass: "current",
                        PagerCssClass: "pager",
                        PageIndex: parseInt(pager.find("PageIndex").text()),
                        PageSize: parseInt(pager.find("PageSize").text()),
                        RecordCount: parseInt(pager.find("RecordCount").text())
                    });

                    $(".Match").each(function () {
                        var searchPattern = new RegExp('(' + SearchTermInTodayList() + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTermInTodayList() + "</span>"));
                    });
                } else {
                    var empty_row = TodayRegRow.clone(true);
                    $("td:first-child", empty_row).attr("colspan", $("td", TodayRegRow).length);
                    $("td:first-child", empty_row).attr("align", "center");
                    $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=dtgViewTodaysRegistration]").append(empty_row);

                    $(".pgrHistory").hide();

                }

                var PatienIDColumn = $("[id*=dtgViewTodaysRegistration] th:contains('PatientID')");
                PatienIDColumn.css("display", "none");
                $("[id*=dtgViewTodaysRegistration] tr").each(function () {
                    $(this).find("td").eq(PatienIDColumn.index()).css("display", "none");
                });
              
            };

            TodayRegRow = null;


           //------------------------------------------------* Bind Today's Appointments Gridview *--------------------------------------------------//

            var TodayAppoRow;
            function TodayAppointmentSuccess(response) 
            {
              
                $(".pgrHistoryAppointment").show();
               
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var AllAppointments = xml.find("TodayAppointments");
                if (TodayAppoRow == null) {
                    TodayAppoRow = $("[id*=dtgTodaysAppointment] tr:last-child").clone(true);
                }
                $("[id*=dtgTodaysAppointment] tr").not($("[id*=dtgTodaysAppointment] tr:first-child")).remove();
                if (AllAppointments.length > 0) {
                   // var tempdd='<select name="hall" id="hall" value="3"><option>Present</option> <option>Cancel</option></select>';
                    $.each(AllAppointments, function () {
                         //$("td", TodayAppoRow).eq(1).html($('<img />')
                         // .attr('src', "" + '../images/Deleteicon1.png' + "")).addClass('CursorShow');

                        $("td", TodayAppoRow).eq(2).html($(this).find("Name").text());
                        $("td", TodayAppoRow).eq(3).html($(this).find("Location").text());
                        $("td", TodayAppoRow).eq(4).html($(this).find("Mobile").text());
                        $("td", TodayAppoRow).eq(5).html($(this).find("AllottingTime").text());
                        $("td", TodayAppoRow).eq(6).html($(this).find("AppointmentID").text());
                       
                        var appointid=$(this).find("AppointmentID").text();
                        var currntrowobj=$(this);
                        var registerflag=currntrowobj.find("IsRegistered").text();
                        var appointstatus=currntrowobj.find("AppointmentStatus").text();
                        //if(currntrowobj.find("PatientID").text()=='00000000-0000-0000-0000-000000000000')
                        //if(currntrowobj.find("AppointmentStatus").text()=='0')//appointed:0
                        if(registerflag =='false')
                        {
                            $("td", TodayAppoRow).removeClass("reged");
                            $("td", TodayAppoRow).eq(0).html($('<img />')
                            .attr('src', "" + '../images/NonregisteredUSer.png' + ""));
                            $("td", TodayAppoRow).eq(1).html('<select id=' + appointid +' name="Action" onchange="DDAction(this);"><option value="-1">--Select--</option><option value="0">Absent</option></select>');
                        }
                        //  if(currntrowobj.find("PatientID").text()!='00000000-0000-0000-0000-000000000000')
                        // if(currntrowobj.find("AppointmentStatus").text()=='1')//present:1
                        debugger;
                        if(registerflag =='true')
                        {
                            switch (appointstatus)
                            {
                                case "1":
                                    $("td", TodayAppoRow).addClass("reged");
                                    $("td", TodayAppoRow).eq(0).html($('<img />')); 
                                    $("td", TodayAppoRow).eq(1).html('Present');
                                    break;

                                case "2":
                                    $("td", TodayAppoRow).addClass("reged");
                                    $("td", TodayAppoRow).eq(0).html($('<img />')); 
                                    $("td", TodayAppoRow).eq(1).html('Absent');
                                    break;

                                case "4": 
                                    $("td", TodayAppoRow).addClass("reged");
                                    $("td", TodayAppoRow).eq(0).html($('<img />')); 
                                    $("td", TodayAppoRow).eq(1).html('Consulted');
                                    break;

                                default: 
                                    $("td", TodayAppoRow).addClass("reged");
                                    $("td", TodayAppoRow).eq(0).html($('<img />'));
                                    $("td", TodayAppoRow).eq(1).html('<select id=' + appointid +' name="Action" onchange="DDAction(this);"><option value="-1">--Select--</option><option value="1">Present</option><option value="0">Absent</option></select>');
                            }

                            //if(currntrowobj.find("AppointmentStatus").text()=='1')
                            //{
                            //    $("td", TodayAppoRow).addClass("reged");
                            //    $("td", TodayAppoRow).eq(0).html($('<img />')); 
                            //    $("td", TodayAppoRow).eq(1).html('Present');
                            //}
                            //    if(currntrowobj.find("AppointmentStatus").text()=='4')//consulted:4
                            //    {
                            //        $("td", TodayAppoRow).addClass("reged");
                            //        $("td", TodayAppoRow).eq(0).html($('<img />')); 
                            //        $("td", TodayAppoRow).eq(1).html('Consulted');
                            //    }
                            //else
                            //{
                            //    $("td", TodayAppoRow).addClass("reged");
                            //    $("td", TodayAppoRow).eq(0).html($('<img />'));
                            //    $("td", TodayAppoRow).eq(1).html('<select id=' + appointid +' name="Action" onchange="DDAction(this);"><option value="-1">--Select--</option><option value="1">Present</option><option value="0">Absent</option></select>');
                            //}
                        }
                        //if(currntrowobj.find("AppointmentStatus").text()=='4')//consulted:4
                        //{
                        //    $("td", TodayAppoRow).addClass("reged");
                        //    $("td", TodayAppoRow).eq(0).html($('<img />')); 
                        //    $("td", TodayAppoRow).eq(1).html('Consulted');
                        //}
                        $("td", TodayAppoRow).eq(7).html(currntrowobj.find("PatientID").text());
                        $("td", TodayAppoRow).eq(8).html($(this).find("AppointmentStatus").text());
                        $("td", TodayAppoRow).eq(9).html($(this).find("IsRegistered").text());
                        $("[id*=dtgTodaysAppointment]").append(TodayAppoRow);
                        TodayAppoRow = $("[id*=dtgTodaysAppointment] tr:last-child").clone(true);
                    });
                    var pager = xml.find("Pager");
                    if ($('#txtSearchTodayAppointment').val() == '') {
                        var GridRowCount = pager.find("RecordCount").text();
                        $("#<%=lblAppointmentCount.ClientID %>").text(GridRowCount);
                    }

                    $(".pgrHistoryAppointment").ASPSnippets_Pager({
                        ActiveCssClass: "current",
                        PagerCssClass: "pager",
                        PageIndex: parseInt(pager.find("PageIndex").text()),
                        PageSize: parseInt(pager.find("PageSize").text()),
                        RecordCount: parseInt(pager.find("RecordCount").text())
                    });

                    $(".Match").each(function () {
                        var searchPattern = new RegExp('(' + SearchTermInTodayList() + ')', 'ig');
                        $(this).html($(this).text().replace(searchPattern, "<span class = 'highlight'>" + SearchTermInTodayList() + "</span>"));
                    });
                    } else {
                    var empty_row = TodayAppoRow.clone(true);
                    $("td:first-child", empty_row).attr("colspan", $("td", TodayAppoRow).length);
                    $("td:first-child", empty_row).attr("align", "center");
                    $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=dtgTodaysAppointment]").append(empty_row);

                    $(".pgrHistoryAppointment").hide();

                }

                var AppointmentIDColumn = $("[id*=dtgTodaysAppointment] th:contains('AppointmentID')");
                AppointmentIDColumn.css("display", "none");
                $("[id*=dtgTodaysAppointment] tr").each(function () {
                    $(this).find("td").eq(AppointmentIDColumn.index()).css("display", "none");
                });
                var PatientIDColumn = $("[id*=dtgTodaysAppointment] th:contains('PatientID')");
                PatientIDColumn.css("display", "none");
                $("[id*=dtgTodaysAppointment] tr").each(function () {
                    $(this).find("td").eq(PatientIDColumn.index()).css("display", "none");
                });
                var AppointStatusColumn = $("[id*=dtgTodaysAppointment] th:contains('AppointmentStatus')");
                AppointStatusColumn.css("display", "none");
                $("[id*=dtgTodaysAppointment] tr").each(function () {
                    $(this).find("td").eq(AppointStatusColumn.index()).css("display", "none");
                });

                var IsRegisteredColumn = $("[id*=dtgTodaysAppointment] th:contains('IsRegistered')");
                IsRegisteredColumn.css("display", "none");
                $("[id*=dtgTodaysAppointment] tr").each(function () {
                    $(this).find("td").eq(IsRegisteredColumn.index()).css("display", "none");
                });
            };

            TodayAppoRow = null;






    function DDAction(ddobj)
            {
             if(ddobj.value==0)//cancel apppointment
             {
                 if(confirm("Are You Sure?"))
                 {
                     var Appointments=new Object();
                     Appointments.AppointmentID=ddobj.id;
                     AppointmentIsAbsent(Appointments);
                     GetTodayPatientAppointments(1);
                 }
             }
            if(ddobj.value==1)//patient present
            {
                if(confirm("Are You Sure?"))
                {
                    var Appointments=new Object();
                    Appointments.AppointmentID=ddobj.id;
                    AppointmentIsPresent(Appointments);
                    GetTodayPatientAppointments(1);
                }
               
            }
            }
 function CancelAppointment(Appointments)
            {
                var ds = {};
                var table = {};
                var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
                ds = getJsonData(data, "../Appointment/Appointment.aspx/CancelAppointment");
                table = JSON.parse(ds.d);
                return table;
            }

function AppointmentIsPresent(Appointments)
{
   debugger;
    try
    {
        var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
        jsonResult = getJsonData(data, "../Appointment/Appointment.aspx/PresentPatientAppointment");
         var table = {};
        table = JSON.parse(jsonResult.d);
    }
    catch(e)
    {
      
    }
    return table;
}

function AppointmentIsAbsent(Appointments)
{
    debugger;
    try
    {
        var data = "{'AppointObj':" + JSON.stringify(Appointments) + "}";
        jsonResult = getJsonData(data, "../Appointment/Appointment.aspx/AbsentPatientAppointment");
        var table = {};
        table = JSON.parse(jsonResult.d);
    }
    catch(e)
    {
      
    }
    return table;
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
                        <img class="big" id="biglogo" runat="server" src="../images/logo.png" /><img id="smalllogo" class="small" runat="server" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                    <li id="patients" class="active"><a name="hello" onclick="selectTile('patients')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                     <li id="Appoinments" ><a name="hello" onclick="selectTile('Appoinments')"><span class="icon Appoinmnts"></span><span class="text">Appoinments</span></a></li>
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

                     <a class="todays_appointment_link" onclick="OpenModal('3');">
                        <span class="tooltip1">
                            <span class="count" style="background-color:#e05d46!important;">
                                <asp:Label ID="lblAppointmentCount" runat="server" Text="0"></asp:Label></span>
                                <img src="../images/Appoinments.jpg" />
                            <span class="tooltiptext1">Today's Appointments</span>
                        </span>
                    </a>
                    <a class="all_registration_link" onclick="OpenModal('1');">
                        <span class="tooltip1">
                            <span class="count">
                                <asp:Label ID="lblRegCount" runat="server" Text="0"></asp:Label></span>
                            <img src="../images/registerd9724185.png" />
                            <span class="tooltiptext1">All Registration</span>
                        </span>
                    </a>
                   
                    <a class="Todays_registration_link" onclick="OpenModal('2');">
                        <span class="tooltip1">
                            <span class="count">
                                <asp:Label ID="lblTodayRegCount" runat="server" Text="0"></asp:Label></span>
                            <img src="../images/registerd.png" />
                            <span class="tooltiptext1">Today's Register</span>
                        </span>
                    </a>
                </div>
                <div class="grey_sec">
                    <div class="search_div">

                        <input class="field" type="search" id="txtSearch" name="txtSearch" placeholder="Search patient..." />
                        <input class="button" type="button" id="btnSearch" value="Search" runat="server" disabled="disabled"/>
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
                                        <label for="name">Name</label><input id="txtName" runat="server" onkeypress="return isnotNumber(event)" type="text" name="name" pattern="^\S+[A-z][A-z\.\s]+$" title="⚠ The Name is required and it allows alphabets only." />
                                    </div>
                                    <div class="col-lg-4 upload_photo_col">
                                        <div class="margin_bottom upload_photo">
                                            <img id="ProfilePic" src="~/images/UploadPic1.png" style="height: 142px;" runat="server" />
                                        </div>
                                        <div class="upload">
                                            <label class="control-label">Upload Picture</label>

                                            <asp:FileUpload ID="FileUpload1" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload(this);showpreview(this);" />
                                        </div>
                                    </div>
                                    <div class="col-lg-8">
                                        <label for="sex">Sex<asp:RadioButton ID="rdoMale" runat="server" GroupName="Active" Text="Male" CssClass="checkbox-inline" Width="9%" /><asp:RadioButton ID="rdoFemale" runat="server" GroupName="Active" Text="Female" CssClass="checkbox-inline" Width="9%" /></label>
                                    </div>
                                    <div class="col-lg-8">
                                        <label for="age">Age</label><input id="txtAge" runat="server" type="number" onkeypress="return isNumber(event)" name="age" min="1" pattern="\d*" title="⚠ The Age is required and entry should be Numbers no Negative Values Expected." />
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
                                <label for="mobile">Mobile</label><input id="txtMobile" runat="server" type="text" onkeypress="return isNumber(event)" name="mobile" minlength="5" pattern="\d*" title="⚠ This entry can only contain Numbers." />
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
                        <button type="button" class="close" data-dismiss="modal" id="AllRegistrationClose">&times;</button>
                        <h3 class="modal-title">All Registration</h3>
                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <%--<iframe id="ViewAllRegistration" style ="width: 100%; height: 100%" ></iframe>--%>
                        <div class="col-lg-12" style="height: 480px;">
                            <div class="col-lg-12" style="height: 40px">
                                <div class="search_div">
                                    <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchPatient" />
                                    <input class="button3" type="button" value="Search" disabled />
                                </div>
                            </div>
                            <div class="col-lg-12" style="height: 400px;">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Editicon1.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/Deleteicon1.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="Match"></asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" ItemStyle-CssClass="Match"></asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" ItemStyle-CssClass="Match"></asp:BoundField>
                                        <asp:BoundField DataField="PatientID" HeaderText="PatientID"></asp:BoundField>


                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="Pager">
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
                        <button type="button" class="close" data-dismiss="modal" id="TodayRegistrationClose">&times;</button>
                        <h3 class="modal-title">Today's Registrations</h3>

                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <div class="col-lg-12" style="height: 480px;">
                            <div class="col-lg-12" style="height: 40px">
                                <div class="search_div">
                                    <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchTodayPatient" />
                                    <input class="button3" type="button" value="Search" disabled />
                                </div>
                            </div>
                            <div class="col-sm-12" style="height: 400px;">
                                <asp:GridView ID="dtgViewTodaysRegistration" runat="server" AutoGenerateColumns="False">

                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnUpdate1" runat="server" ImageUrl="~/Images/Editicon1.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnDelete1" runat="server" ImageUrl="~/Images/Deleteicon1.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                        <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="Phone"></asp:BoundField>
                                        <asp:BoundField DataField="PatientID" HeaderText="PatientID"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="pgrHistory">
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                  

                </div>
            </div>
        </div>


       <%-- Todays Appointments Modal--%>
          <div class="modal fade" id="TodaysAppointment" role="dialog">
            <div class="modal-dialog" style="min-width: 550px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: #3661C7;">
                        <button type="button" class="close" data-dismiss="modal" id="TodayAppointmentClose">&times;</button>
                        <h3 class="modal-title">Today's Appointments</h3>

                    </div>
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">
                        <div class="col-lg-12" style="height: 480px;">
                            <div class="col-lg-12" style="height: 40px">
                                <div class="search_div">
                                    <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchTodayAppointment" />
                                    <input class="button3" type="button" value="Search" disabled />
                                </div>
                            </div>
                           
                            <div class="col-sm-12" style="height: 400px;">
                                <asp:GridView ID="dtgTodaysAppointment" runat="server" AutoGenerateColumns="False">

                                    <Columns>
                                       
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%--<asp:ImageButton  ID="ImgBtnUpdate1" Style="border: none!important" runat="server" ImageUrl="../images/NonregisteredUSer.png" HeaderText="Details" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                               <%-- <asp:ImageButton  ID="ImgBtnDelete1" Style="border: none!important" runat="server" ImageUrl="~/Images/Deleteicon1.png" HeaderText="Action" />--%>
                                               <%-- <asp:DropDownList ID="iddropdownAction" runat="server"></asp:DropDownList>--%>
                                               <%-- <select name="Action" onchange="jsFunction(this.value);"><option value="-1">--Select--</option><option value="1">Present</option><option value="0">Cancel</option></select>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                        <asp:BoundField DataField="Location" HeaderText="Location"></asp:BoundField>
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile No"></asp:BoundField>
                                        <asp:BoundField DataField="AllottingTime" HeaderText="Time"></asp:BoundField>
                                        <asp:BoundField DataField="AppointmentID" HeaderText="AppointmentID"></asp:BoundField>
                                        <asp:BoundField DataField="PatientID" HeaderText="PatientID"></asp:BoundField>
                                        <asp:BoundField DataField="AppointmentStatus" HeaderText="AppointmentStatus"></asp:BoundField>
                                        <asp:BoundField DataField="IsRegistered" HeaderText="IsRegistered"></asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            
                            <div class="pgrHistoryAppointment">
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdfAppointmentID" runat="server" />
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


        <%--<script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>--%>
        <%--<script src="../js/jquery-1.3.2.min.js"></script>--%>

        <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>

        <script src="../js/jquery-1.12.0.min.js"></script>
        <script src="../js/ASPSnippets_Pager.min.js"></script>
        <script src="../js/bootstrap.min.js"></script>

        <script src="../js/jquery-ui.js"></script>

        <!---   Script includes function for open Modals preview, Created By:Thomson Kattingal --->


        <script type="text/javascript">  
    <!---Function for Open Token Registration Modal and All Registarion Modal----->
    function openModal() {
        
        $('#TokenRegistration').modal('show');
           
    }
    function openmyModal() {
      
        $('#myModal').modal('show');
        
    }

    function openModalAppointment()
    {
        $('#TodaysAppointment').modal('show');
    }


  
    
        </script>











        <style>



</style>
        <!---------------------------------------------------------------------------------------->
        <!------------------------------------->
    </asp:Panel>
</asp:Content>
