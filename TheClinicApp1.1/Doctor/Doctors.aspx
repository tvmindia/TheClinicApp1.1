<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Main.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeBehind="Doctors.aspx.cs" Inherits="TheClinicApp1._1.Doctor.Doctors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
              .selected_row
    {
        background-color: #d3d3d3!important;
    }



        </style>

    <asp:Panel DefaultButton="btnSave" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server" EnableCdn="true"></asp:ScriptManager>
        

        <script src="../js/jquery-1.12.0.min.js"></script>
        <script src="../js/vendor/modernizr-2.6.2-respond-1.1.0.min.js"></script>
        <script src="../js/jquery-ui.js"></script>
        <script src="../js/fileinput.js"></script>
        <script src="../js/Messages.js"></script>
        <script>
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
                        document.getElementById('txtMedUnit' + ControlNo).value = response;                       
                    }   
                }  
                function onError(response, userContext, methodName) {       
                }
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


            function GetTextBoxValuesPresLocal()
            {   
                GetTextBoxValuesPresDoc('<%=hdnTextboxValues.ClientID%>');
            }
                    
            function FillTextboxUsingXml(){                
                GetClientIDOfRemovedID('<%=hdnRemovedIDs.ClientID%>','<%=hdnRowCount.ClientID%>');
            RefillMedicineTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
        }


        function reset(){
            $('input[type=text]').val('');  
            $('textarea').val(''); 
            $('input[type=select]').val('');
            $('input[type=radio]').val('');
            $('input[type=checkbox]').val('');  
        }

        function CheckEmpty()
        {                
            if(($('#<%=txtHeightFeet.ClientID%>').val() != '')&&($('#<%=txtHeightInch.ClientID%>').val()!='')&&($('#<%=txtWeight.ClientID%>').val()!=''))
            {
                $('#<%=txtHeightFeet.ClientID%>').val('');
                $('#<%=txtHeightInch.ClientID%>').val('');
                $('#<%=txtWeight.ClientID%>').val('');
            }    

            if(($('input[type=text]').val()=='')&&($('textarea').val()==''))
            {                 
                // Alert.render("Sorry...");
                return false;                    
            }                
        }

     

        </script>
        <script> 

            ///Called for onblur event of SEARCH textbox
            //retrieves patient details by web method and then bind controls
            //Calls the function to bind history using PatientID

            function bindPatientDetails()
            {              
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
                    document.getElementById('<%=lblFileNum.ClientID%>').innerHTML=string1[0];
                    document.getElementById('<%=lblPatientName.ClientID%>').innerHTML=string1[1];
                    document.getElementById('<%=lblAgeCount.ClientID%>').innerHTML=string1[2];
                    document.getElementById('<%=lblGenderDis.ClientID%>').innerHTML=string1[3];            
                    document.getElementById('<%=HiddenPatientID.ClientID%>').value=string1[7];
           
                 <%--   var BtID=document.getElementById('<%=btnSearch.ClientID%>')
                
                    $('#<%=btnSearch.ClientID%>').click();--%>
                    document.getElementById('txtSearch').value="";//clear search box  
                    

                    PatientID = string1[7];

                    if ( PatientID!= '') 
                    {

                        GetHistory(1,PatientID);
                    }

                }          
                function onError(response, userContext, methodName)
                {                   
                }   
                

            }

           
        </script>

        <%--Script And Css For Paging,Search--%>

         <link href="../css/TheClinicApp.css" rel="stylesheet" />

         <script src="../js/jquery-1.8.3.min.js"></script>
        <script src="../js/ASPSnippets_Pager.min.js"></script>
         <script src="../js/jquery-ui.js"></script>
       
        <script> 
            
            var PatientID = '';

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


            //-------------------------------- * VIEW Button Click OF TOKEN List * ------------------------- //

            //On Clicking view button Patient details will be binded
            //Calls the function to bind history ,by patientID

            $(function () {
                $("[id*=GridViewTokenlist] td:eq(0)").click(function () { 
                    
                    PatientID = $(this).closest('tr').find('td:eq(5)').text();

                    reset();

                    document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                    
                    if ($(this).text() == "") {
                        var jsonResult = {};
                        var jsonResult1 = {};

                        var Patient = new Object();
                        Patient.PatientID = PatientID;
                       
                        jsonResult = GetPatientDetailsByID(Patient);
                        if (jsonResult != undefined) {
                          
                            BindControlsWithPatientDetails(jsonResult); 
                        }
                       
                        GetHistory(1,PatientID);

                    }

                });
            });

            //-------------------------------- *END : VIEW Button Click * ------------------------- //

            function GetPatientDetailsByID(Patient) {
                var ds = {};
                var table = {};
                var data = "{'PatientObj':" + JSON.stringify(Patient) + "}";
                ds = getJsonData(data, "../Doctor/Doctors.aspx/BindPatientDetailsOnEditClick");
                table = JSON.parse(ds.d);
                return table;
            }

            function BindControlsWithPatientDetails(Records) {
                $.each(Records, function (index, Records) {
                    <%-- $("#<%=txtCategoryName.ClientID %>").val(Records.Name);
                    $("#<%=hdnCategoryId.ClientID %>").val(Records.CategoryID);--%>

                    //Fill Patient Details

                    $("#<%=lblPatientName.ClientID %>").text(Records.Name) ;
                    $("#<%=lblDoctor.ClientID %>").text(Records.DOCNAME);
                    $("#<%=lblFileNum.ClientID %>").text(Records.FileNumber);
                    $("#<%=lblGenderDis.ClientID %>").text(Records.Gender);
                    $("#<%=HiddenField1.ClientID %>").val(Records.PatientID); 

                   
                    //---- Age Calculation By substracting DOB year from Current year

                    var DOB = new Date(Date.parse(ConvertJsonToDate(Records.DOB),"MM/dd/yyyy"));
                    var Age = (new Date().getFullYear() )-   (DOB.getFullYear());
                    $("#<%=lblAgeCount.ClientID %>").text(Age) ;

                    var   imagetype =Records.ImageType;

                    var ProfilePic = $("#<%=ProfilePic.ClientID %>");

                    if (imagetype != '')
                    {
                        ProfilePic.src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID;
                    }
                    else
                    {
                        ProfilePic.src = "../images/UploadPic1.png";
                    }

                    $("#DoctrClose").click();
                });
            }


//------------------------------------------------- * History Edit Click * ------------------------------------//

            var FileID ='';
            var VisitID = '';
            var PrescriptionID ='';

            ///On clicking history's EDIT button 
            //visit details are binded by visitID
            //Visit Attachment details are binded by visistID
            //Prescription details are binded by prescriptionID
            //(patient Detils are already binded)

            $(function () {
                $("[id*=GridViewVisitsHistory] td:eq(0)").click(function () {

                    $("#HistoryClose").click();

                    reset();

                    document.getElementById('<%=Errorbox.ClientID %>').style.display = "none";
                    
                    if ($(this).text() == "") {
                        var jsonVisit = {};
                        var jsonVisitAttchmnt = {};

                        FileID = $(this).closest('tr').find('td:eq(3)').text();
                        VisitID = $(this).closest('tr').find('td:eq(4)').text();
                        PrescriptionID = $(this).closest('tr').find('td:eq(5)').text();


                        $("#<%=HdnPrescID.ClientID %>").val(PrescriptionID);
                        $("#<%=HdnForVisitID.ClientID %>").val(VisitID);

                        //------------------------ Binding Visit Deatils By VisitID

                         var Visit = new Object();
                         Visit.VisitID = VisitID;

                         jsonVisit = GetVisitDetailsByvisitID(Visit);
                         if (jsonVisit != undefined) {
                          
                             BindVisitDetails(jsonVisit);
                         }

                        //----------------------- Binding Visit Attachment Deatils By VisitID

                         var   VisitAttachment = new Object();
                         VisitAttachment.VisitID = VisitID;

                         jsonVisitAttchmnt = GetAttachmentDetailsByvisitID(VisitAttachment)

                         if (jsonVisitAttchmnt != undefined) {
                          
                             BindAttachment(jsonVisitAttchmnt);
                        }

                        //----------------------- Binding Prescription Details
                        GetPrescriptionDetails(PrescriptionID);

                    }
                });
            });

            function GetVisitDetailsByvisitID(Visit) {
                var ds = {};
                var table = {};
                var data = "{'CaseFileObj':" + JSON.stringify(Visit) + "}";
                ds = getJsonData(data, "../Doctor/Doctors.aspx/BindVisitDetailsOnEditClick");
                table = JSON.parse(ds.d);
                return table;
            }

            function GetAttachmentDetailsByvisitID(VisitAttachment) {

                var ds = {};
                var table = {};
                var data = "{'AttachObj':" + JSON.stringify(VisitAttachment) + "}";
                ds = getJsonData(data, "../Doctor/Doctors.aspx/GetVisitAttatchment");
                table = JSON.parse(ds.d);
                return table;
            }

            function BindAttachment(Records)
            {
                $.each(Records, function (index, Records) {

                    var AttchmntID = Records.AttachID;

                    var img= $('<img id="'+AttchmntID+'">'); //Equivalent: $(document.createElement('img'))
                    img.attr('src', "../Handler/ImageHandler.ashx?AttachID="+AttchmntID);
                    img.attr("height", "120");
                    img.attr("class", "imagpreview");

                    img.appendTo(  $("#<%=VistImagePreview.ClientID %>"));

                });
            }

            function BindVisitDetails(Records) {
                
                    $("#<%=txtHeightFeet.ClientID %>").val(Records.Height);
                    $("#<%=txtWeight.ClientID %>").val(Records.Weight);
                    $("#<%=bowel.ClientID %>").val(Records.Bowel);
                    $("#<%=appettie.ClientID %>").val(Records.Appettie);
                    $("#<%=micturation.ClientID %>").val(Records.Micturation);
                    $("#<%=sleep.ClientID %>").val(Records.Sleep);
                    $("#<%=symptoms.ClientID %>").val(Records.Symptoms);
                    $("#<%=cardiovascular.ClientID %>").val(Records.Cardiovascular);
                    $("#<%=nervoussystem.ClientID %>").val(Records.Nervoussystem);
                    $("#<%=musculoskeletal.ClientID %>").val(Records.Musculoskeletal);
                    $("#<%=palloe.ClientID %>").val(Records.Palloe);
                    $("#<%=icterus.ClientID %>").val(Records.Icterus);
                    $("#<%=clubbing.ClientID %>").val(Records.Clubbing);
                    $("#<%=cyanasis.ClientID %>").val(Records.Cyanasis);
                    $("#<%=lymphGen.ClientID %>").val(Records.LymphGen);
                    $("#<%=edima.ClientID %>").val(Records.Edima);
                    $("#<%=diagnosys.ClientID %>").val(Records.Diagnosys);
                    $("#<%=remarks.ClientID %>").val(Records.Remarks);
                    $("#<%=pulse.ClientID %>").val(Records.Pulse);
                    $("#<%=bp.ClientID %>").val(Records.Bp);
                    $("#<%=tounge.ClientID %>").val(Records.Tounge);
                    $("#<%=heart.ClientID %>").val(Records.Heart);
                    $("#<%=lymphnodes.ClientID %>").val(Records.LymphClinic);
                    $("#<%=resp_rate.ClientID %>").val(Records.RespRate);
                    $("#<%=others.ClientID %>").val(Records.Others);
                   
            }

            function GetPrescriptionDetails(PrescriptionID) {

                $.ajax({

                    type: "POST",
                    url: "../Doctor/Doctors.aspx/GetPrescriptionDetailsXml",
                    data: '{PrescriptionID: "' + PrescriptionID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: PrescriptionSuccess,
                    failure: function (response) {

                        alert(response.d);
                    },
                    error: function (response) {

                        alert(response.d);
                    }
                });
            }
           
            function PrescriptionSuccess(response) {
              
                $("#<%=hdnXmlData.ClientID %>").val(response.d) ;

                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var Pharmacy = xml.find("Medicines");
               
                FillTextboxUsingXml();

            };

            //---------------------------------------------------------- * HISTORY Grid BinD,Paging,Search *--------------------------------------------------//

            $("[id*=txtSearchVisit]").live("keyup", function () 
            {
                //Search in Visit table

                GetHistory(parseInt(1),PatientID);
            });
            $(".Pager .page").live("click", function () 
            {
                //Next Click(paging) of Visit table

                GetHistory(parseInt($(this).attr('page')),PatientID);
            });
            function SearchTerm() {
                return jQuery.trim($("[id*=txtSearchVisit]").val());
            };

            function GetHistory(pageIndex,PatientID) {
                $.ajax({

                    type: "POST",
                    url: "../Doctor/Doctors.aspx/GetHistory",
                    data: '{searchTerm: "' + SearchTerm() + '", pageIndex: ' + pageIndex + ', PatientID: "' + PatientID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: HistorySuccess,
                    failure: function (response) {

                        alert(response.d);
                    },
                    error: function (response) {

                        alert(response.d);
                    }
                });
            }

            var row= null;
            function HistorySuccess(response) {
                
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var Visits = xml.find("Visits");

                if (row == null) {
                    row = $("[id*=GridViewVisitsHistory] tr:last-child").clone(true);
                }
                $("[id*=GridViewVisitsHistory] tr").not($("[id*=GridViewVisitsHistory] tr:first-child")).remove();
                if (Visits.length > 0) {

                    $.each(Visits, function () {
                      
                        $("td", row).eq(0).html($('<img />')
                           .attr('src', "" + '../images/Editicon1.png' + "")).addClass('CursorShow');
                         
                        //$("td", row).eq(1).html($(this).find("TokenNo").text());
                        $("td", row).eq(1).html($(this).find("Remarks").text());

                        $("td", row).eq(2).html($(this).find("CrDate").text());
                       
                        $("td", row).eq(3).html($(this).find("FileID").text());

                        $("td", row).eq(4).html($(this).find("VisitID").text());
                        $("td", row).eq(5).html($(this).find("PrescriptionID").text());

                        $("[id*=GridViewVisitsHistory]").append(row);
                        row = $("[id*=GridViewVisitsHistory] tr:last-child").clone(true);
                    });
                    var pager = xml.find("Pager");

                    $.each(pager, function ()
                    {
                        $("#<%=HiddenField2.ClientID %>").val($(this).find("FILEID").text()); 

                    });

                    if ($('#txtSearchVisit').val() == '') {
                        var GridRowCount = pager.find("RecordCount").text();

                        $("#<%=lblCaseCount.ClientID %>").text(GridRowCount);
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

                    var pager = xml.find("Pager");

                    $.each(pager, function ()
                    {
                        $("#<%=HiddenField2.ClientID %>").val($(this).find("FILEID").text()); 

                    });

                    var empty_row = row.clone(true);
                    $("td:first-child", empty_row).attr("colspan", $("td", row).length);
                    $("td:first-child", empty_row).attr("align", "center");
                    $("td:first-child", empty_row).html("No records found.").removeClass('CursorShow');
                    $("td", empty_row).not($("td:first-child", empty_row)).remove();
                    $("[id*=GridViewVisitsHistory]").append(empty_row);
                }


//-------- Hiding Columns fileid ,visitid,prescriptionid

                var FileIDColumn = $("[id*=GridViewVisitsHistory] th:contains('FileID')");
                FileIDColumn.css("display", "none");
                $("[id*=GridViewVisitsHistory] tr").each(function () {
                    $(this).find("td").eq(FileIDColumn.index()).css("display", "none");
                });


                var VisitIDColumn = $("[id*=GridViewVisitsHistory] th:contains('VisitID')");
                VisitIDColumn.css("display", "none");
                $("[id*=GridViewVisitsHistory] tr").each(function () {
                    $(this).find("td").eq(VisitIDColumn.index()).css("display", "none");
                });


                var PrescriptionIDColumn = $("[id*=GridViewVisitsHistory] th:contains('PrescriptionID')");
                PrescriptionIDColumn.css("display", "none");
                $("[id*=GridViewVisitsHistory] tr").each(function () {
                    $(this).find("td").eq(PrescriptionIDColumn.index()).css("display", "none");
                });

                row= null;

            };
            
            //---------------------------------------------------------- * Token Grid BinD,Paging,Search *--------------------------------------------------//

            $(function () {
           
                GetBookingsForDoctor(1);
            });
            $("[id*=txtSearchINGridview]").live("keyup", function () {
              
                //Search in Token table

                GetBookingsForDoctor(parseInt(1));
            });
            $(".Pager .page").live("click", function () {

               //Next Click(paging) of Token table

                GetBookingsForDoctor(parseInt($(this).attr('page')));
            });
            function SearchTerm() {
                return jQuery.trim($("[id*=txtSearchINGridview]").val());
            };

            //----------- * Bind Token table * -------- //

            function GetBookingsForDoctor(pageIndex) {

                $.ajax({

                    type: "POST",
                    url: "../Doctor/Doctors.aspx/ViewAndFilterBookingsForDoctor",
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
               
                var xmlDoc = $.parseXML(response.d);
                var xml = $(xmlDoc);
                var DoctorTokens = xml.find("DoctorTokens");
                if (row == null) {
                    row = $("[id*=GridViewTokenlist] tr:last-child").clone(true);
                }
                $("[id*=GridViewTokenlist] tr").not($("[id*=GridViewTokenlist] tr:first-child")).remove();
                if (DoctorTokens.length > 0) {

                    $.each(DoctorTokens, function () {
                       
                        $("td", row).eq(0).html($('<img />')
                           .attr('src', "" + '../images/paper.png' + "")).addClass('CursorShow');

                        $("td", row).eq(1).html($(this).find("TokenNo").text());
                        $("td", row).eq(2).html($(this).find("Name").text());


                        $("td", row).eq(3).html($(this).find("DateTime").text());
                        $("td", row).eq(4).html($(this).find("IsProcessed").text());
                         $("td", row).eq(5).html($(this).find("PatientID").text());

                      

                        if ($(this).find("IsProcessed").text()=="true") {
                            $("td", row).addClass("selected_row");
                        }
                        if ($(this).find("IsProcessed").text() == "false") {
                            $("td", row).removeClass("selected_row");
                        }


                        $("[id*=GridViewTokenlist]").append(row);
                        row = $("[id*=GridViewTokenlist] tr:last-child").clone(true);
                    });
                    var pager = xml.find("Pager");

                    if ($('#txtSearchINGridview').val() == '') {
                        var GridRowCount = pager.find("RecordCount").text();

                        $("#<%=lblTokenCount.ClientID %>").text(GridRowCount);
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
                    $("[id*=GridViewTokenlist]").append(empty_row);
                }

                var PatientIDColumn = $("[id*=GridViewTokenlist] th:contains('PatientID')");
                PatientIDColumn.css("display", "none");
                $("[id*=GridViewTokenlist] tr").each(function () {
                    $(this).find("td").eq(PatientIDColumn.index()).css("display", "none");
                });

                row= null;

            };

            //------ * Function To open modal popup * -----//

            function OpenModal() {

                $('#txtSearchINGridview').val('');
                GetBookingsForDoctor(parseInt(1));

            }


       </script>

        <!-- #main-container -->
        <asp:HiddenField ID="hdnfileID" runat="server" />
        <asp:HiddenField ID="HiddenPatientID" runat="server" />
        <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
        <div class="main_body">
            <div class="left_part">
                <div class="logo">
                    <a href="#">
                        <img class="big" src="../images/logo.png" /><img class="small" src="../images/logo-small.png" /></a>
                </div>
                <ul class="menu">
                    <li id="patients"><a name="hello" onclick="selectTile('patients','<%=RoleName%>')"><span class="icon registration"></span><span class="text">Patient</span></a></li>
                    <li id="token"><a name="hello" onclick="selectTile('token','<%=RoleName %>')"><span class="icon token"></span><span class="text">Token</span></a></li>
                    <li id="doctor" class="active"><a name="hello" onclick="selectTile('doctor','<%=RoleName%>')"><span class="icon doctor"></span><span class="text">Doctor</span></a></li>
                    <li id="pharmacy"><a name="hello" onclick="selectTile('pharmacy','<%=RoleName%>')"><span class="icon pharmacy"></span><span class="text">Pharmacy</span></a></li>
                    <li id="stock"><a name="hello" onclick="selectTile('stock','<%=RoleName%>')"><span class="icon stock"></span><span class="text">Stock</span></a></li>
                    <li id="admin" runat="server"><a name="hello" onclick="selectTile('<%=admin.ClientID %>','<%=RoleName %>')"><span class="icon admin"></span><span class="text">Admin</span></a></li>
                    <li id="Repots"><a name="hello" href="../Report/ReportsList.aspx"><span class="icon report"></span><span class="text">Reports</span></a></li>
                    <li id="master" runat="server"><a name="hello" onclick="selectTile('<%=master.ClientID %>','')"><span class="icon master"></span><span class="text">Master</span></a></li>
                    <li id="log" runat="server"><a class="logout" name="hello" id="Logout" runat="server" onserverclick="Logout_ServerClick"><span class="icon logout"></span><span class="text">Logout</span></a></li>
                </ul>

                <p class="copy">&copy;<asp:Label ID="lblClinicName" runat="server" Text="Trithvam Ayurvedha"></asp:Label></p>
            </div>
            <div class="right_part">

                <div class="tagline">
                    <a class="nav_menu">Menu</a>
                    Doctors...<ul class="top_right_links">
                        <li>
                            <asp:Label ID="lblUserName" CssClass="label" runat="server" Text="UserName" ForeColor="#d8bb22"></asp:Label></li>
                        <li>
                            <asp:ImageButton ID="LogoutButton" ImageUrl="~/images/LogoutWhite.png" BorderColor="White" runat="server" OnClick="LogoutButton_Click" ToolTip="Logout" formnovalidate /></li>
                    </ul>

                </div>

                <div class="icon_box">
                    <a class="records" data-toggle="modal" data-target="#casehistory" >
                        <span class="tooltip1">
                        <span class="count"><asp:Label ID="lblCaseCount" runat="server" Text="0"></asp:Label>
                        </span>                      
                            <img src="../images/case-history.png" />
                            <span class="tooltiptext1">Case History</span>
                        </span>
                    </a>
                    <a class="casehistory_link" data-toggle="modal" data-target="#tokens" onclick="OpenModal();">
                        <span class="tooltip1">
                        <span class="count"><asp:Label ID="lblTokenCount" runat="server" Text="0"></asp:Label>  
                             </span>                       
                            <img src="../images/tokens.png" />
                            <span class="tooltiptext1">Tokens</span>
                        </span>                              
                    </a>                   
                </div>

                <div class="grey_sec">

                    <div class="search_div">
                        <input class="field" id="txtSearch" onblur="  bindPatientDetails();" name="txtSearch" type="search" placeholder="Search patient..." />
                        <input type="hidden" id="project-id" />
                        <p id="project-description" style="display: none"></p>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search"  />
                        <%--<input class="button" type="submit" id="btnSearch" value="Search" />--%>
                    </div>

                    <ul class="top_right_links">
                        <li>
                            <asp:Button ID="btnSave" runat="server" Text="save" CssClass="button1" OnClientClick="GetTextBoxValuesPresLocal();" OnClick="btnSave_Click" /></li>
                        <li><a class="new" href="#" id="btnNew" runat="server" onclick="reset();" onserverclick="btnNew_ServerClick"><span></span>New</a></li>
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

                    <div class="token_id_card">
                        <div class="name_field">
                            <img id="ProfilePic" src="../images/UploadPic1.png" width="80" height="80" runat="server" /><asp:Label ID="lblPatientName" runat="server" Text="Patient Name"></asp:Label>
                        </div>
                        <div class="light_grey">
                            <div class="col3_div">
                                <asp:Label ID="lblAgeCount" runat="server" Text=""></asp:Label><span>Age</span>
                            </div>
                            <div class="col3_div">
                                <asp:Label ID="lblGenderDis" runat="server" Text=""></asp:Label><span>Gender</span>
                            </div>
                            <div class="col3_div">
                                <asp:Label ID="lblFileNum" runat="server" Text=""></asp:Label><span>File No</span>
                            </div>
                        </div>
                        <div class="card_white">
                            <div class="field_label">
                                <label>Doctor</label><asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="prescription_grid" style="padding-left:0px !important; max-width: 1000px !important;">
                        <asp:Label ID="lblNew_history" CssClass="blink" runat="server" Text="New Case" ForeColor="#FF9933" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    </div>


                    <asp:HiddenField ID="HiddenField2" runat="server" />

                    <div id="accordion">

                        <h3>Personal</h3>
                        <div>
                            <div class="row field_row">
                                <div class="col-lg-4">
                                    <div class="row">
                                        <div class="col-lg-7">
                                            <label for="height">Height</label>
                                            <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
                                                <input type="text" class="form-control text-center" id="txtHeightFeet" runat="server" data-min="0" data-max="100" data-step="1" onkeypress="return isNumber(event)"/>
                                                <div class="input-group-addon">
                                                    <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
                                                    <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
                                                </div>
                                            </div>
                                            <div class="input-group spinner height" data-trigger="spinner" id="customize-spinner">
                                                <input type="text" class="form-control text-center" id="txtHeightInch" runat="server" data-min="0" data-max="100" data-step="1" onkeypress="return isNumber(event)" />
                                                <div class="input-group-addon">
                                                    <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
                                                    <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-5">
                                            <label for="weight">Weight</label>
                                            <div class="input-group spinner weight" data-trigger="spinner" id="customize-spinner">
                                                <input type="text" class="form-control text-center" id="txtWeight" runat="server" data-min="0" data-max="200" data-step="1" onkeypress="return isNumber(event)"/>
                                                <div class="input-group-addon">
                                                    <a href="" class="spin-up" data-spin="up"><i class="fa fa-caret-up"></i></a>
                                                    <a href="" class="spin-down" data-spin="down"><i class="fa fa-caret-down"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <label for="bowel">Bowel</label><input id="bowel" type="text" name="bowel" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="appettie">Appetite</label><input id="appettie" type="text" name="appettie" runat="server" />
                                </div>
                            </div>

                            <div class="row field_row">
                                <div class="col-lg-4">
                                    <label for="micturation">Micturition</label><input id="micturation" type="text" name="micturation" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="sleep">Sleep</label><input id="sleep" type="text" name="sleep" runat="server" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <label for="symptoms">Symptoms</label><textarea id="symptoms" runat="server"></textarea>
                                </div>
                            </div>

                        </div>

                        <h3>Systematic Examination</h3>
                        <div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label for="cardiovascular">Cardiovascular</label><input id="cardiovascular" type="text" name="cardiovascular" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="nervoussystem">Nervous System</label><input id="nervoussystem" type="text" name="nervoussystem" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="musculoskeletal">Musculoskeletal</label><input id="musculoskeletal" type="text" name="musculoskeletal" runat="server" />
                                </div>
                            </div>
                        </div>

                        <h3>General Examination</h3>
                        <div>
                            <div class="row field_row">
                                <div class="col-lg-4">
                                    <label for="palloe">Pallor</label><input id="palloe" type="text" name="palloe" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="icterus">Icterus</label><input id="icterus" type="text" name="icterus" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="clubbing">Clubbing</label><input id="clubbing" type="text" name="clubbing" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <label for="cyanasis">Cyanosis</label><input id="cyanasis" type="text" name="cyanasis" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="lymphnodes">Lymph Nodes</label><input id="lymphGen" type="text" name="lymphGen" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="edima">Edema</label><input id="edima" type="text" name="edima" runat="server" />
                                </div>
                            </div>
                        </div>

                        <h3>Diagnosis</h3>
                        <div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <label for="diagnosys">Diagnosis</label><textarea id="diagnosys" runat="server"></textarea>
                                </div>
                            </div>
                        </div>

                        <h3>Remarks</h3>
                        <div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <label for="remarks">Remarks</label><textarea id="remarks" runat="server"></textarea>
                                </div>
                            </div>
                        </div>

                        <h3>Clinical Details</h3>
                        <div>
                            <div class="row field_row">
                                <div class="col-lg-4">
                                    <label for="pulse">Pulse</label><input id="pulse" type="text" name="pulse" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="bp">Blood Pressure</label><input id="bp" type="text" name="bp" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="tounge">Tounge</label><input id="tounge" type="text" name="tounge" runat="server" />
                                </div>
                            </div>
                            <div class="row field_row">
                                <div class="col-lg-4">
                                    <label for="heart">Heart</label><input id="heart" type="text" name="heart" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="lymphnodes">Lymph Nodes</label><input id="lymphnodes" type="text" name="lymphnodes" runat="server" />
                                </div>
                                <div class="col-lg-4">
                                    <label for="resp_rate">Respiratory Rate</label><input id="resp_rate" type="text" name="resp_rate" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <label for="others">Others</label><textarea id="others" runat="server"></textarea>
                                </div>
                            </div>
                        </div>
                        <h3>Case Images</h3>
                        <div>
                            <div class="col-lg-12">

                                <div class="col-lg-4">
                                    <label for="Imgdesc">Image Description</label><textarea id="Imgdesc" runat="server"></textarea>
                                    <%-- Multiple Upload Functions --%>

                                    <asp:FileUpload ID="FileUpload1" ForeColor="Red" Font-Size="12px" runat="server" onchange="OnUpload();" />
                                </div>
                                <div class="col-lg-8">
                                    <div id="VistImagePreview" runat="server">
                                    </div>
                                </div>

                            </div>

                        </div>
                        <h3>Prescription Section</h3>
                        <div>
                            <table class="table" style="width: 100%; border: 0!important;">
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
                                            <input id="txtMedQty0" onkeypress="return isNumber(event)" type="text" class="input" /></td>
                                        <td>
                                            <input id="txtMedUnit0" class="input" readonly="true" type="text" onfocus="focusplz(0)" /></td>
                                        <td>
                                            <input id="txtMedDos0" type="text" class="input" /></td>
                                        <td>
                                            <input id="txtMedTime0" type="text" class="input" /></td>
                                        <td>
                                            <input id="txtMedDay0" type="text" class="input" /></td>
                                        <td style="background: #E6E5E5">
                                            <input type="button" value="-" class="bt1" onclick="ClearAndRemove1()" style="width: 20px;" accesskey="-" /></td>
                                        <td style="background: #E6E5E5">
                                            <input type="button" id="btAdd" onclick="clickAdd(0);this.style.visibility = 'hidden';" value="+" class="bt1" style="width: 20px" accesskey="+" />
                                        </td>
                                        <td style="background-color: transparent">
                                            <input id="hdnDetailID0" type="hidden" />
                                            <input id="hdnQty0" type="hidden" /></td>
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
        <!-- Alert Container -->
        <div id="dialogoverlay"></div>
        <div id="dialogbox">
            <div>
                <div id="dialogboxhead"></div>
                <div id="dialogboxbody"></div>
                <div id="dialogboxfoot"></div>
            </div>
        </div>
        <div id="casehistory" class="modal fade" role="dialog">
            <div class="modal-dialog" style="min-width: 550px">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: #3661C7;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="HistoryClose"><span aria-hidden="true">&times;</span></button>
                        <h3 class="modal-title">Case History</h3>
                    </div>
                    
                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">

                    <div class="col-lg-12" style="height: 480px">

                        <div class="col-lg-12" style="height: 40px">
                            <div class="search_div">
                                <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchVisit" />
                                <input class="button3" type="button" value="Search" />
                            </div>
                        </div>


                        <div class="col-lg-12" style="height: 400px">
                            
  <asp:GridView ID="GridViewVisitsHistory" runat="server" AutoGenerateColumns="False" class="table" >
                                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:ImageButton ID="ImgBtnUpdateVisits" runat="server" Style="border: none!important" ImageUrl="~/images/Editicon1.png"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" ItemStyle-CssClass="Match" />
                                    <asp:BoundField DataField="CrDate" HeaderText="Date" ItemStyle-CssClass="Match"></asp:BoundField>

                                   <asp:BoundField HeaderText="FileID" DataField="FileID"  /> 
                                     <asp:BoundField HeaderText="VisitID" DataField="VisitID"  /> 
                                     <asp:BoundField HeaderText="PrescriptionID" DataField="PrescriptionID"  /> 

                                  
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

        <div id="tokens" class="modal fade" role="dialog">
            <div class="modal-dialog" style="min-width: 550px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="border-color: royalblue;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="DoctrClose"><span aria-hidden="true">&times;</span></button>

                        <h3 class="modal-title">Tokens</h3>
                    </div>

                    <div class="modal-body" style="overflow-y: scroll; overflow-x: hidden; max-height: 500px;">

                    <div class="col-lg-12" style="height: 480px">

                        <div class="col-lg-12" style="height: 40px">
                            <div class="search_div">
                                <input class="field1" type="text" placeholder="Search with Name.." id="txtSearchINGridview" />
                                <input class="button3" type="button" value="Search" />
                            </div>
                        </div>


                        <div class="col-lg-12" style="height: 400px">
                            <asp:GridView ID="GridViewTokenlist" OnRowDataBound="GridViewTokenlist_RowDataBound" runat="server" AutoGenerateColumns="False" >

                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="35px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnUpdate" runat="server" ImageUrl="../images/paper.png"   />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Token No" DataField="TokenNo" ItemStyle-CssClass="Match"  />
                                    <asp:BoundField HeaderText="Patient Name" DataField="Name" ItemStyle-CssClass="Match" />
                                    <asp:BoundField HeaderText="Time" DataField="DateTime" ItemStyle-CssClass="Match" />
                                    <asp:BoundField HeaderText="Consulted" DataField="IsProcessed" ItemStyle-CssClass="Match" />
                                   <asp:BoundField HeaderText="PatientID" DataField="PatientID" />  
                                </Columns>
                            </asp:GridView>

                        </div>
                        <div class="Pager">
                        </div>


                    </div>
                </div>




                   

                </div>
                <asp:HiddenField ID="HdnPrescID" runat="server" />
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


        <!---   Script for case images fileupload preview & FileType Checking   --->
        <script type="text/javascript">
           
         
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
            }
            // --------------------------------------------------------//
       
            //------------------------Animate Div---------------------------//
            function blink(selector){ 
                    $(selector).animate({fontSize: "2.2em"},2000,function(){
                  //  $(selector).animate({fontSize: "2em"},2000 )
                       // blink(this);                    
                });
            }          
            //---------------------------------------------------------//



            var test=jQuery.noConflict();
            test(document).ready(function () {
       
               
                blink('.blink');//div blinking function
			   
                test('.alert_close').click(function () {
                    test(this).parent(".alert").hide();
                });	
							
                test(function () {
                   
                    test('[data-toggle="tooltip"]').tooltip()
                })	
			
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
            var test=jQuery.noConflict();
            test(document).on('ready',function(){
           
                $( "#ui-id-3,#ui-id-5,#ui-id-7,#ui-id-9,#ui-id-11,#ui-id-13,#ui-id-15" ).click();//accordian keep open

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
                    maxResults: 10,
                    source: function(request, response) {
                        //var results = $.ui.autocomplete.filter(projects, request.term);

                        
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

                        //bindPatientDetails();

                        return false;
                    }
                })
                 .autocomplete( "instance" )._renderItem = function( ul, item ) {
                     return $( "<li>" )
                     .append( "<a>" + item.label + "<br>" + item.desc + "</a>" )
                     .appendTo( ul );
                 }; 
                //$( "#txtSearch" ).autocomplete({
                //source: ac
                //});
                GetClientIDOfRemovedID('<%=hdnRemovedIDs.ClientID%>','<%=hdnRowCount.ClientID%>');
                RefillTextboxesWithXmlData('<%=hdnXmlData.ClientID%>');
            });
              
        </script>

    </asp:Panel>

</asp:Content>
