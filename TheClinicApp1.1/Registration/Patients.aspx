﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" EnableEventValidation="false"    AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="TheClinicApp1._1.Registration.Patients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Script for Custom Alert Box For Checking Uploded image is Supported or not--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <asp:ScriptManager runat="server" EnablePageMethods="true"  ></asp:ScriptManager>

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
        <%--<script src="../js/jquery.tablePagination.0.1.js"></script>--%>
        <script type="text/javascript">
            $(document).ready( function (){
                debugger;
                var ac=null;
                ac = <%=listFilter %>;


                var length= ac.length;
                var projects = new Array();
                for (i=0;i<length;i++)
                {  
                    var name= ac[i].split('🏠');
                    projects.push({  value : name[0], label: name[0], desc: name[1]})   
                }
                debugger;
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

                    BindPatientDetails();

                    //  $('#<%=btnSearch.ClientID%>').click();

                        //$( "#project" ).val( ui.item.label );
      
                        //$( "#project-description" ).html( ui.item.desc );                  
                        
 
                        return false;
                    }
                })
            .autocomplete( "instance" )._renderItem = function( ul, item ) {
                return $( "<li>" )
                  .append( "<a>" + item.label + "<br>" + item.desc + "</a>" )
                  .appendTo( ul );

            };             




                <%--$( "#txtSearch" ).autocomplete({
                    source: ac,
                    select: function(event, ui){
                        $( "#txtSearch" ).val( ui.item.label );
                        $('#<%=btnSearch.ClientID%>').click();
                    }

                });--%>



                $('.alert_close').click(function () {

                    debugger;

                    $(this).parent(".alert").hide();
                });


                $('.nav_menu').click(function () {
                    $(".main_body").toggleClass("active_close");
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
          var ds = {};
          var table = {};
          var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
          ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetailsOnEditClick");
          table = JSON.parse(ds.d);
          return table;
      }


             function BindPatientDetails()
              {

debugger;
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



          function GetPatientDetails(Patient) {

                var ds = {};
                var table = {};
                var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
                ds = getJsonData(data, "../Registration/Patients.aspx/BindPatientDetails");
                table = JSON.parse(ds.d);
                return table;
            }

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
                    var ProfilePic = document.getElementById("<%=ProfilePic.ClientID%>")  ;

                    if (imagetype != '')
                    {
                        debugger;

                        ProfilePic.src = "../Handler/ImageHandler.ashx?PatientID=" + patientid;
                    }
                    else
                    {
                        ProfilePic.src = "../images/UploadPic1.png";
                    }

                    var DOB = new Date(Date.parse(ConvertJsonToDate(Records.DOB),"MM/dd/yyyy"));
                    var Age = (new Date().getFullYear() )-   (DOB.getFullYear());

                    $("#<%=txtAge.ClientID %>").val(parseInt(Age)) ;
               
            }


            function BindControlsWithPatientDetails(Records) {
                $.each(Records, function (index, Records) {
                    <%-- $("#<%=txtCategoryName.ClientID %>").val(Records.Name);
                    $("#<%=hdnCategoryId.ClientID %>").val(Records.CategoryID);--%>

                    //Fill Patient Details

                    debugger;

                     
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
                    var patientid =  Records.PatientID;
                    var ProfilePic = document.getElementById("<%=ProfilePic.ClientID%>")  ;

                    if (imagetype != '')
                    {
                        debugger;

                        ProfilePic.src = "../Handler/ImageHandler.ashx?PatientID=" + patientid;
                    }
                    else
                    {
                        ProfilePic.src = "../images/UploadPic1.png";
                    }

                    var DOB = new Date(Date.parse(ConvertJsonToDate(Records.DOB),"MM/dd/yyyy"));
                    var Age = (new Date().getFullYear() )-   (DOB.getFullYear());

                    $("#<%=txtAge.ClientID %>").val(parseInt(Age)) ;
                });

                $("#AllRegistrationClose").click();

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



            
            function DeleteTodayPatientByID(PatientID) { 

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






            function DeletePatientByID(PatientID) { 

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
                    debugger;
                    //PageMethods.DeletePatientByID(PatientID, OnSuccess, onError);
                    debugger;
 <%--                   function OnSuccess(response, userContext, methodName) {
                        debugger;
                        if (response.isPatientDeleted == true) {

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
                        function onError(response, userContext, methodName) {

                        }

                    }--%>
                }

            }



            $(function () {
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


            $(function () {
                $("[id*=dtgViewTodaysRegistration] td:eq(0)").click(function () { 
                    debugger;
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

            $(function () {
                $("[id*=GridView1] td:eq(1)").click(function () {
                    debugger;

                    if ($(this).text() == "") {
                        var DeletionConfirmation = ConfirmDelete();
                        if (DeletionConfirmation == true) {
                            PatientID = $(this).closest('tr').find('td:eq(5)').text();
                            DeletePatientByID(PatientID);
                            //window.location = "StockIn.aspx?HdrID=" + receiptID;
                        }
                    }
                });
            });

           
            $(function () {
                $("[id*=dtgViewTodaysRegistration] td:eq(1)").click(function () {
                    debugger;

                    if ($(this).text() == "") {
                        var DeletionConfirmation = ConfirmDelete();
                        if (DeletionConfirmation == true) {
                            PatientID = $(this).closest('tr').find('td:eq(5)').text();
                           
                            DeleteTodayPatientByID(PatientID);
                        }
                    }
                });
            });



            $("[id*=txtSearchTodayPatient]").live("keyup", function () {
                debugger;
                GetTodayPatients(parseInt(1));
            });

            $(".pgrHistory .page").live("click", function () {
                GetTodayPatients(parseInt($(this).attr('page')));
            });
            function SearchTermInTodayList() {
                return jQuery.trim($("[id*=txtSearchTodayPatient]").val());
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

          

            $(function () {
                debugger;
                GetAllPatients(1);
                GetTodayPatients(1);
            });
            $("[id*=txtSearchPatient]").live("keyup", function () {
                debugger;
                GetAllPatients(parseInt(1));
            });
            $(".Pager .page").live("click", function () {
                GetAllPatients(parseInt($(this).attr('page')));
            });
           
            

            function SearchTerm() {
                return jQuery.trim($("[id*=txtSearchPatient]").val());
            };
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

                $(".Pager").show();

                debugger;
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var AllRegistration = xml.find("AllRegistration");


                if (row == null) {
                    row = $("[id*=GridView1] tr:last-child").clone(true);
                }
                $("[id*=GridView1] tr").not($("[id*=GridView1] tr:first-child")).remove();
                if (AllRegistration.length > 0) {
                    
                    $.each(AllRegistration, function () {
                        debugger;
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

                        $("#<%=lblRegCount.ClientID %>").text(GridRowCount);
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

                    debugger;

                    var empty_row = row.clone(true);
                    $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                    $("td:first-child", empty_row).attr("align", "center");
                    $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=GridView1]").append(empty_row);

                    $(".Pager").hide();
                }



                //var th = $("[id*=GridView1] th:contains('DoctorID')");
                //th.css("display", "none");
                //$("[id*=GridViewPharmacylist] tr").each(function () {
                //    $(this).find("td").eq(th.index()).css("display", "none");
                //});

                var PatienIDColumn = $("[id*=GridView1] th:contains('PatientID')");
                PatienIDColumn.css("display", "none");
                $("[id*=GridView1] tr").each(function () {
                    $(this).find("td").eq(PatienIDColumn.index()).css("display", "none");
                });
                
            };

            row= null;


            var TodayRegRow;
            function TodayRegistrationSuccess(response) {

                $(".pgrHistory").show();
               
                debugger;
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var AllRegistration = xml.find("TodayRegistration");
                if (TodayRegRow == null) {
                    TodayRegRow = $("[id*=dtgViewTodaysRegistration] tr:last-child").clone(true);
                }
                $("[id*=dtgViewTodaysRegistration] tr").not($("[id*=dtgViewTodaysRegistration] tr:first-child")).remove();
                if (AllRegistration.length > 0) {
                    
                    $.each(AllRegistration, function () {
                        debugger;
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



                //var th = $("[id*=GridView1] th:contains('DoctorID')");
                //th.css("display", "none");
                //$("[id*=GridViewPharmacylist] tr").each(function () {
                //    $(this).find("td").eq(th.index()).css("display", "none");
                //});

                var PatienIDColumn = $("[id*=dtgViewTodaysRegistration] th:contains('PatientID')");
                PatienIDColumn.css("display", "none");
                $("[id*=dtgViewTodaysRegistration] tr").each(function () {
                    $(this).find("td").eq(PatienIDColumn.index()).css("display", "none");
                });
              
            };

            TodayRegRow = null;

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
         

                    <a class="all_registration_link" onclick="OpenModal('1');">
                          <span class="tooltip1">
                        <span class="count"><asp:Label ID="lblRegCount" runat="server" Text="0"></asp:Label></span>                     
                        <img src="../images/registerd9724185.png" /> 
                             <span class="tooltiptext1"> All Registration</span>
                    </span>
                    </a>
                    <a class="Todays_registration_link" onclick="OpenModal('2');">
                            <span class="tooltip1">
                        <span class="count"><asp:Label ID="lblTodayRegCount" runat="server" Text="0"></asp:Label></span>                      
                        <img src="../images/registerd.png" /> 
                             <span class="tooltiptext1"> Today's Register</span>
                    </span>
                    </a>
                </div>
                <div class="grey_sec">
                    <div class="search_div">

                        <input class="field" type="search" id="txtSearch" onblur="bindPatient()" name="txtSearch" placeholder="Search patient..." />
                        <input class="button" type="button" id="btnSearch" value="Search" runat="server" onserverclick="btnSearch_ServerClick" disabled/>
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

                                            <asp:FileUpload ID="FileUpload1" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload();showpreview(this);" />
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
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" >

                                    <Columns>
                              <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="border: none!important" ID="ImgBtnUpdate" runat="server" ImageUrl="~/Images/Editicon1.png"   />
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
                        <button type="button" class="close" data-dismiss="modal" >&times;</button>
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
                                <asp:GridView ID="dtgViewTodaysRegistration" runat="server" AutoGenerateColumns="False" >

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

    
        </script>











        <style>



</style>
        <!---------------------------------------------------------------------------------------->
        <!------------------------------------->
    </asp:Panel>
</asp:Content>
