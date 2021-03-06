﻿/*! jQuery Dynamic Grid using TextBoxes| (c) Author: Thomson Kattingal | /www.thrithvam.com */
/*!No Modifications are allowed without permission*/

var last = 0;
var iCnt = 0;
var ExistingRowCount = 0;
var IdToRemove;
var RemovedIDs = '';
var PageCalledFrom = '';
var CurrentRowCount;


function ClearAndRemove()
{
    debugger;
    document.getElementById('txtMedicine0').value = "";
    document.getElementById('txtUnit0').value = "";

    document.getElementById('txtCode0').value = '';
    document.getElementById('txtCategory0').value = "";
    document.getElementById('txtQuantity0').value = "";
    document.getElementById('txtQuantity0').placeholder = "";

    //document.getElementById('txtMedicine0').readOnly = false;
    RemovedIDs += document.getElementById('hdnDetailID0').value + ',';

    if (RemovedIDs == ',') {
        RemovedIDs = '';
    }
    //document.getElementById('<%=hdnRemovedIDs.ClientID%>').value = RemovedIDs;


    if (RemovedIDs != '') {
        document.getElementById(IdToRemove).value = RemovedIDs;
    }

    
}


function ClearAndRemove1() {
    debugger;
    document.getElementById('txtMedName0').value = "";
    document.getElementById('txtMedUnit0').value = "";

    document.getElementById('txtMedQty0').value = '';
    document.getElementById('txtMedDos0').value = "";
    document.getElementById('txtMedTime0').value = "";
    document.getElementById('txtMedDay0').value = "";

   


    RemovedIDs += document.getElementById('hdnDetailID0').value + ',';

    if (RemovedIDs == ',') {
        RemovedIDs = '';
    }
    //document.getElementById('<%=hdnRemovedIDs.ClientID%>').value = RemovedIDs;


    if (RemovedIDs != '') {
        document.getElementById(IdToRemove).value = RemovedIDs;
    }


}



function SetPageIDCalled(page)
{
    PageCalledFrom = page;
}


function GetClientIDOfRemovedID(DetailID,rowCnt )
{
    
     IdToRemove = DetailID;
     CurrentRowCount = rowCnt;
      
}


// CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
var container = $(document.createElement('div')).css({
    width: '100%',
    borderTopColor: '#FFFFFF', borderBottomColor: '#FFFFFF',
    borderLeftColor: '#FFFFFF', borderRightColor: '#FFFFFF'
});

function RemoveWarning(ControlNo) {
    debugger;
    
    //--------------* To remove warning msg from textbox if the medicine is not out of stock , and is called onfocus event of quantity textbox *-------------------//
    var isnotnumber = document.getElementById('txtQuantity' + ControlNo).value;
    if (isNaN(isnotnumber))
    {


        $("#txtQuantity" + ControlNo).removeClass("warning");
        $("#txtQuantity" + ControlNo).css({ 'color': 'black' });
        $("#txtQuantity" + ControlNo).attr('type', 'number');
    }

}




function clickStockAdd(id) {
    iCnt = iCnt + 1;
    
     //ADD new row with fields needed.
   
    $(container).append('<div id="div' + iCnt + '"><table  style="width:100%;border:0;"><tr>'
       + '<td><input id="txtMedicine' + iCnt + '" class="input" type="text"  onblur="BindControlsByMedicneName(' + iCnt + ')" onfocus="autocompleteonfocus(' + iCnt + ')" onclick="RemoveStyle(this);" /></td>'
       + '<td><input id="txtUnit' + iCnt + '" readonly="true" class="input "  type="text"  /></td>'
       + '<td><input id="txtCode' + iCnt + '" readonly="true" class="input "  type="text" /></td>'
       + '<td><input id="txtCategory' + iCnt + '" readonly="true" class="input "  type="text" /></td>'
       + '<td><input id="txtQuantity' + iCnt + '" onclick="RemoveStyle(this);" onkeypress="return isNumber(event)" class="input" min="1" type="number"  onblur="CheckMedicineIsOutOfStock(' + iCnt + ')" onfocus="RemoveWarning(' + iCnt + ')" autocomplete="off" /></td>'
       + '<td><input type="button" id="btRemove' + iCnt + '" class="bt1" value="-" onclick="clickdelete(' + iCnt + ')" style="width:20px" accesskey="-" /></td>'
       + '<td><input type="button" id="btAdd' + iCnt + '" value="+" onclick="clickStockAdd(' + iCnt + ')" class="bt" style="width:20px" accesskey="+" /></td>'
       + '<td><input id="hdnDetailID' + iCnt + '" type="hidden" /> <input id="hdnQty' + iCnt + '" type="hidden" /></td>'
                              + '</tr> </table> </div>');

    $('#maindiv').after(container);
    $('#btAdd' + id).css("visibility", "hidden");

    ExistingRowCount = ExistingRowCount + 1;

    if (CurrentRowCount != null && CurrentRowCount != '')
    {
        document.getElementById(CurrentRowCount).value = ExistingRowCount;
    }

   


    last = last + 1;
}
function RemoveStyle(e)
{
    debugger;
    document.getElementById(e.id).style.borderColor = "#dbdbdb";
    $("#Errorbox").hide(1000);
}
function DocPrescription() {
    //Check div exist and if then remove
    if (iCnt > 0)
    {
        for (var k = 1; k <= iCnt; k++) {
            $('#div' + k).remove();
        }
    }
    
    $('#initPresc').remove();
    iCnt = 0;
    //creating prescription section for doctor 
    $('#PrecsDiv').append('<div id="initPresc"><table class="table" id="prestab" style="width: 100%; border: 0!important;">'
                                +'<tbody><tr><th>Medicine</th><th>Quantity</th><th>Unit</th><th>Dosage</th><th>Timing</th><th>Days</th></tr>'
                                    +'<tr><td><input id="txtMedName0" type="text" class="input" onblur="BindMedunitbyMedicneName('+0+')" onfocus="autocompleteonfocus(0)" /></td>'
                                        +'<td><input id="txtMedQty0" onkeypress="return isNumber(event)" type="text" class="input" /></td>'
                                        +'<td><input id="txtMedUnit0" class="input" readonly="true" type="text" onfocus="focusplz(0)" /></td>'
                                        +'<td><input id="txtMedDos0" type="text" class="input" /></td>'
                                        +'<td><input id="txtMedTime0" type="text" class="input" /></td>'
                                        +'<td><input id="txtMedDay0" type="text" class="input" /></td>'
                                        +'<td style="background: #E6E5E5"><input type="button" value="-" class="bt1" onclick="ClearAndRemove1()" style="width: 20px;" accesskey="-" /></td>'
                                        +'<td style="background: #E6E5E5"><input type="button" id="btAdd" onclick="clickAdd(0);visib(this);" value="+" class="bt1" style="width: 20px" accesskey="+" />'
                                        +'</td><td style="background-color: transparent"><input id="hdnDetailID0" type="hidden" />'
                                            +'<input id="hdnQty0" type="hidden" /></td></tr></tbody></table><div id="maindiv"></div></div>');
}
//function for Add new row for prescription 
function visib(f) {
    f.style.visibility = 'hidden';
}
function clickAdd(id) {
    iCnt = iCnt + 1;
    // ADD new row with fields needed.
    $(container).append('<div id="div' + iCnt + '"><table id="tblMedicine" class="table" style="width:100%;">'
             + ' <td ><input id="txtMedName' + iCnt + '" type="text" class="input"  onblur="BindMedunitbyMedicneName(' + iCnt + ')" onfocus="autocompleteonfocus(' + iCnt + ')"  /></td>'
                + '<td ><input id="txtMedQty' + iCnt + '" type="text" onclick="RemoveStyle(this);" onkeypress="return isNumber(event)" class="input" onfocus="focuscontrol(' + iCnt + ')" title="Red Color Indicates No Stock" onkeyup="CheckPharmacyMedicineIsOutOfStock(' + iCnt + ')" onchange="RemoveWarningPharm(' + iCnt + ')" autocomplete="off"/></td>'
                + '<td ><input id="txtMedUnit' + iCnt + '"  readonly="true" onclick="RemoveStyle(this);"  class="input" type="text" onfocus="focusplz(' + iCnt + ')" /></td>'
                + '<td ><input id="txtMedDos' + iCnt + '" type="text" onclick="RemoveStyle(this);" class="input"/></td>'
                + '<td><input id="txtMedTime' + iCnt + '" type="text" onclick="RemoveStyle(this);" class="input"/></td>'
                 + '<td><input id="txtMedDay' + iCnt + '" type="text" onclick="RemoveStyle(this);" class="input"/></td>'
                 + '<td style="background:#E6E5E5" class="add">'
                 + '<input type="button" id="btRemove' + iCnt + '" class="bt1" value="-" onclick="clickdelete(' + iCnt + ')" style="width:20px" accesskey="-" /></td>'
                 + '<td style="background:#E6E5E5" class="add">'
                 + '<input type="button" id="btAdd' + iCnt + '" value="+" onclick="clickAdd(' + iCnt + ')" class="bt" style="width:20px" accesskey="+" /></td>'
                 + '<td style="background:#E6E5E5" class="add"><input id="hdnDetailID' + iCnt + '" type="hidden" /> <input id="hdnQty' + iCnt + '" type="hidden" /></td>'
                 + '</tr></table><div>');
    $('#maindiv').after(container);
    $('#btAdd' + id).css("visibility", "hidden");

    ExistingRowCount = ExistingRowCount + 1;
   
    if (CurrentRowCount != null && CurrentRowCount != '')
    {
        document.getElementById(CurrentRowCount).value = ExistingRowCount;
    }


    last = last + 1;
}

//Delete Div where the remove button contain.
var Removecount = 0;


function clickdelete(id) {
  
    
    RemovedIDs += document.getElementById('hdnDetailID' + id).value + ',';

    if (RemovedIDs == ',') {
        RemovedIDs = '';
    }
    //document.getElementById('<%=hdnRemovedIDs.ClientID%>').value = RemovedIDs;


    if (RemovedIDs!='') {
        document.getElementById(IdToRemove).value = RemovedIDs;
    }


    if (ExistingRowCount > 1)
    {
        var status=0;
        
        if (id == iCnt && id==ExistingRowCount)
        {
          
            $('#btAdd' + (iCnt - 1) + '').css('visibility', 'visible')
            last = id - 1;
            $('#btRemove' + id).closest("div").remove();
            //iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;

            if (CurrentRowCount != null && CurrentRowCount != '')
            {
                document.getElementById(CurrentRowCount).value = ExistingRowCount;
            }


             status = 1;
        }
//***********************************************************************************

        if ( id == iCnt && status!=1 ) {
            
            var loc = id;
            //find the id before this control
            while (id > 0) {
                var myElem = document.getElementById('div' + (id - 1) + '') 
                if (myElem != null) {
                    $('#btAdd' + (id - 1) + '').css('visibility', 'visible')
                    last = id - 1;
                    id = 0;

                }
                else {
                    id--
                }
            }

            $('#btRemove' + loc).closest("div").remove();
            //iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;

            if (CurrentRowCount != null && CurrentRowCount != '') {
                document.getElementById(CurrentRowCount).value = ExistingRowCount;
            }



        }

//***********************************************************************************
     

        else if (last == id) {
          
            var loc = id;
            //find the id before this control
            while (id > 0) {
                var myElem = document.getElementById('div' + (id - 1) + '')
                if (myElem != null) {
                    $('#btAdd' + (id - 1) + '').css('visibility', 'visible')
                    last = id - 1;
                    id = 0;

                }
                else {

                    id--
                }
            }

            $('#btRemove' + loc).closest("div").remove();
            //iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;

            if (CurrentRowCount != null && CurrentRowCount != '') {
                document.getElementById(CurrentRowCount).value = ExistingRowCount;
            }
        }


        else if ( status!=1) {
          
            $('#btRemove' + id).closest("div").remove();
            // iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;
            if (CurrentRowCount != null && CurrentRowCount != '') {
                document.getElementById(CurrentRowCount).value = ExistingRowCount;
            }
            else
            {
                //do nothing
            }
        }
    }

    else
    {
     
        $('#btAdd').css('visibility', 'visible')
        $('#btRemove' + id).closest("div").remove();
        ExistingRowCount = ExistingRowCount - 1;
        if (CurrentRowCount != null && CurrentRowCount != '') {
            document.getElementById(CurrentRowCount).value = ExistingRowCount;
        }
        last = iCnt;
        
    }
 
  

}


// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';
//------------ *   Function to get textbox values -- stores textbox values into hidden field when data is submitted *-----------//
function GetTextBoxValues(hdnTextboxValues, hdnRemovedIDs) {
    
    values = '';
    var i = 1;
    $('.input').each(function () {
        i++;
    });
    var NumberOfColumns = i - 1;
    var NumberOfRows = NumberOfColumns / 5;


    var topId = iCnt;

    for (var k = 0; k <= topId; k++)

    {

        if (document.getElementById('txtQuantity' + k) == null) {
            continue;
        }
        if (((document.getElementById('txtQuantity' + k) != null) && (document.getElementById('txtQuantity' + k).value == '')) || ((document.getElementById('txtMedicine' + k) != null) && (document.getElementById('txtMedicine' + k).value == '')))
        {
            continue;
        }
        if (((document.getElementById('txtQuantity' + k) != null) && (document.getElementById('txtQuantity' + k).value != '') && (isNaN(document.getElementById('txtQuantity' + k).value)) == false) && ((document.getElementById('txtMedicine' + k) != null) && (document.getElementById('txtMedicine' + k).value != '')))
        {
            var CurrentMedName = document.getElementById('txtMedicine' + k).value;
            if (values.indexOf(CurrentMedName) > -1)
            { }
            else
            {
                
                
                //if (((document.getElementById('txtUnit' + k) != null) && (document.getElementById('txtUnit' + k).value == '')) || ((document.getElementById('txtCode' + k) != null) && (document.getElementById('txtCode' + k).value == '')) || ((document.getElementById('txtCategory' + k) != null) && (document.getElementById('txtCategory' + k).value == '')))
                //{
                //    continue;
                //}
                //else
                //{

                    values += document.getElementById('txtMedicine' + k).value + '|' + document.getElementById('txtUnit' + k).value + '|' + document.getElementById('txtCode' + k).value + '|' + document.getElementById('txtCategory' + k).value + '|' + document.getElementById('txtQuantity' + k).value + '|' + document.getElementById('hdnDetailID' + k).value + '$';
                //}
              
            }
        }

    }


    document.getElementById(hdnTextboxValues).value = values;

}


//-----* Function to bind textboxes by medicine name -- fills textboxes when focus is lost from medicine textbox  *----//
function BindControlsByMedicneName(ControlNo) {
   
  
    if (ControlNo >= 0) {
        var MedicineName = document.getElementById('txtMedicine' + ControlNo).value;

    }

    if (MedicineName != "") {

        PageMethods.MedDetails(MedicineName, OnSuccess, onError);
       
    }
    
    function OnSuccess(response, userContext, methodName) {
        
        if (ControlNo >= 0) {
           
            var MedicineDetails = new Array();
            MedicineDetails = response.split('|');

            document.getElementById('txtUnit' + ControlNo).value = MedicineDetails[0];
            document.getElementById('txtUnit' + ControlNo).readOnly=true;


            document.getElementById('txtCode' + ControlNo).value = MedicineDetails[1];
            document.getElementById('txtCode' + ControlNo).readOnly = true;

            document.getElementById('txtCategory' + ControlNo).value = MedicineDetails[2];
            document.getElementById('txtCategory' + ControlNo).readOnly = true;

           // document.getElementById('txtQuantity' + ControlNo).focus();

            if (PageCalledFrom != 'StockIn') {
               


                if (Number(MedicineDetails[3]) <= 0) {

                    $("#txtQuantity" + ControlNo).addClass("warning");
                    $("#txtQuantity" + ControlNo).attr('type', 'text');
                    $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                    $("#txtQuantity" + ControlNo).val('No Stock');
                   
                    
                }

                else {
                    document.getElementById('txtQuantity' + ControlNo).placeholder = " Out Of: " + MedicineDetails[3];
                }


            }


            document.getElementById('hdnQty' + ControlNo).value = parseInt(MedicineDetails[3]);

        }
       
    }
    function onError(response, userContext, methodName) {
       
    }


}


//-----------* Checks whether medicine is out of stock , when user input quantity , and is called onblur event of quantity textbox *-----------// 
function CheckMedicineIsOutOfStock(ControlNo) {
    
    

    if(   PageCalledFrom != 'StockIn')

    {
        if (document.getElementById('txtMedicine' + ControlNo) != null && document.getElementById('txtQuantity' + ControlNo) != null)
        {

           


            if (isNaN(document.getElementById('txtQuantity' + ControlNo).value) == false && (document.getElementById('txtQuantity' + ControlNo).value != ""))
{
            var Qty = parseInt(document.getElementById('hdnQty' + ControlNo).value);
            var MedicineName = document.getElementById('txtMedicine' + ControlNo).value;
            var InputQty = Number(document.getElementById('txtQuantity' + ControlNo).value);

        
            if ((MedicineName != "") && (Qty != 0))
            {
                //----------- * Case Of Insert *----------//
                if (document.getElementById('hdnDetailID' + ControlNo).value == "")
                {
               
                    if (Qty <= 0) {
                        $("#txtQuantity" + ControlNo).addClass("warning");
                        $("#txtQuantity" + ControlNo).attr('type', 'text');
                        $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                        $("#txtQuantity" + ControlNo).val('No Stock');

                    }



                    else  if (InputQty > Qty || InputQty <= 0)
                    {
                        $("#txtQuantity" + ControlNo).addClass("warning");
                        $("#txtQuantity" + ControlNo).attr('type', 'text');
                        $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                        if (InputQty > Qty)
                        {
                            $("#txtQuantity" + ControlNo).val('Must be <=' + Qty);
                        }

                        if (InputQty <= 0)
                        {
                            $("#txtQuantity" + ControlNo).val('Must be > 0');
                        }

                    }
                    else
                    {
                        $("#txtQuantity" + ControlNo).removeClass("warning");
                        $("#txtQuantity" + ControlNo).css({ 'color': 'black' });
                        $("#txtQuantity" + ControlNo).attr('type', 'number');
                    }
                }
                    //----------- * Case Of Update *----------//     
                else
                {             
                    var QtyInStock = parseInt(document.getElementById('txtQuantity' + ControlNo).getAttribute("placeholder").replace(" Out Of: ", ""));


                    if (QtyInStock <= 0)
                    {
                        $("#txtQuantity" + ControlNo).addClass("warning");
                        $("#txtQuantity" + ControlNo).attr('type', 'text');
                        $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                        $("#txtQuantity" + ControlNo).val('No Stock');

                    }


                    else  if (InputQty > QtyInStock || InputQty <= 0 )
                    {
                        $("#txtQuantity" + ControlNo).addClass("warning");
                        $("#txtQuantity" + ControlNo).attr('type', 'text');
                        $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                        if (InputQty > Qty)
                        {

                            $("#txtQuantity" + ControlNo).val('Must be <= ' + QtyInStock);
                        }

                        if (InputQty <= 0)
                        {
                            $("#txtQuantity" + ControlNo).val('Must be > 0');
                        }
                    }

                    else
                    {
                        $("#txtQuantity" + ControlNo).removeClass("warning");
                        $("#txtQuantity" + ControlNo).css({ 'color': 'black' });
                        $("#txtQuantity" + ControlNo).attr('type', 'number');
                    }
                }
            }
        }
        }
    }

    else
    {
        if (PageCalledFrom == 'StockIn')
        {
            

            var InputQty = document.getElementById('txtQuantity' + ControlNo).value;

            if (document.getElementById('txtQuantity' + ControlNo).value != "") {
    
                InputQty = Number(document.getElementById('txtQuantity' + ControlNo).value);

            if ( InputQty <= 0) {
                $("#txtQuantity" + ControlNo).addClass("warning");
                $("#txtQuantity" + ControlNo).attr('type', 'text');
                $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });

                if (InputQty <= 0) 
                    $("#txtQuantity" + ControlNo).val('Must be > 0');
                

            }
        }

        }
    }


}




// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';
//------------ *   Function to get textbox values -- stores textbox values into hidden field when data is submitted *-----------//
function GetTextBoxValuesPresDoc(hdnTextboxValues) {
  
    
    values = '';
    var i = 1;
    $('.input').each(function () {
        i++;
    });
    var NumberOfColumns = i - 1;
    var NumberOfRows = NumberOfColumns / 6;
    var topId = iCnt;

    for (var k = 0; k <= topId; k++) {

        if (document.getElementById('txtMedName' + k) == null) {
            continue;
        }
        if (((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value == '')) || ((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value == ''))) {
            continue;
        }
        //if (((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value != '') && (isNaN(document.getElementById('txtMedName' + k).value)) == false) && ((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value != ''))) {
        var CurrentMedName = document.getElementById('txtMedName' + k).value;
        if (values.indexOf(CurrentMedName) > -1) {


        }
        else {


            //if (((document.getElementById('txtUnit' + k) != null) && (document.getElementById('txtUnit' + k).value == '')) || ((document.getElementById('txtCode' + k) != null) && (document.getElementById('txtCode' + k).value == '')) || ((document.getElementById('txtCategory' + k) != null) && (document.getElementById('txtCategory' + k).value == '')))
            //{
            //    continue;
            //}
            //else
            //{

            values += document.getElementById('txtMedName' + k).value + '|' + document.getElementById('txtMedQty' + k).value + '|' + document.getElementById('txtMedUnit' + k).value + '|' + document.getElementById('txtMedDos' + k).value + '|' + document.getElementById('txtMedTime' + k).value + '|' + document.getElementById('txtMedDay' + k).value + '|' + document.getElementById('hdnDetailID' + k).value + '$';
            //}

        }
        //}

    }
   
    document.getElementById(hdnTextboxValues).value = values;
   

}




//----------------------------------- * Function to rebind medicine textboxes -- refills controls by retrieving data from xml *--------------------//
function RefillTextboxesWithXmlData(hdnXmlData) {
  
    
    //var XmlDataFromHF = document.getElementById('<%=hdnXmlData.ClientID%>').value;

    var XmlDataFromHF = document.getElementById(hdnXmlData).value;
    var xmlDoc = $.parseXML(XmlDataFromHF);
    var xml = $(xmlDoc);
    var Medicines = xml.find("Medicines");
    var i = 0;

    if (Medicines.length > 0)
    {
        //document.getElementById('<%=txtIssueNO.ClientID %>').readOnly = true;

        $.each(Medicines, function () {
       
            if (i > 0) {
                clickStockAdd(i);
            }

            if (Medicines.length >1)
            {
             

                $('#btAdd').css('visibility', 'hidden');
                $('#btAdd' + (Medicines.length - 1)).css('visibility', 'visible');
            }

           
            var MedicineName = $(this).find("MedicineName").text();
            var MedicineCode = $(this).find("MedCode").text();
            var MedicineUnit = $(this).find("Unit").text();
            var MedicineCategory = $(this).find("CategoryName").text();
            var MedicineQuantity = $(this).find("QTY").text();
            var UniqueID = $(this).find("UniqueID").text();
            var QtyInStock = $(this).find("QtyInStock").text();

            document.getElementById('txtMedicine' + i).value = MedicineName;
            document.getElementById('txtCode' + i).value = MedicineCode;
            document.getElementById('txtUnit' + i).value = MedicineUnit;
            document.getElementById('txtCategory' + i).value = MedicineCategory;
            document.getElementById('txtQuantity' + i).value = MedicineQuantity;
            document.getElementById('hdnQty' + i).value = MedicineQuantity;
            document.getElementById('hdnDetailID' + i).value = UniqueID;

            document.getElementById('txtMedicine' + i).readOnly = true; // --------* medicine name set to non-editable after saving *--------//

            if (PageCalledFrom != 'StockIn') {

                document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + QtyInStock;


                PageMethods.GetQtyInStock(MedicineName, OnSuccess, onError);
                function OnSuccess(response, userContext, methodName) {
                    QtyInStock = parseInt(response);
                }
                function onError(response, userContext, methodName)
                { }
                var oldQty = parseInt(MedicineQuantity);
                var total = parseInt(oldQty + parseInt(QtyInStock));
                document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + total;
            }


            i = i + 1;
        });

    }


}


function RefillMedicineTextboxesWithXmlData(hdnXmlData) {

 
    //var XmlDataFromHF = document.getElementById('<%=hdnXmlData.ClientID%>').value;

    var XmlDataFromHF = document.getElementById(hdnXmlData).value;
    var xmlDoc = $.parseXML(XmlDataFromHF);
    var xml = $(xmlDoc);
    var Medicines = xml.find("Medicines");
    var i = 0;

    if (Medicines.length > 0) {
        //document.getElementById('<%=txtIssueNO.ClientID %>').readOnly = true;

        $.each(Medicines, function () {

            if (i > 0) {
                clickAdd(i);
            }




            var MedicineName = $(this).find("MedicineName").text();
            var MedicineDosage = $(this).find("Dosage").text();
            var MedicineTiming = $(this).find("Timing").text();
            var MedicineDays = $(this).find("Days").text();
            var PrescriptionID = $(this).find("PrescriptionID").text();
            //var MedicineCode = $(this).find("MedCode").text();
            var MedicineUnit = $(this).find("Unit").text();
            //var MedicineCategory = $(this).find("CategoryName").text();
            var MedicineQuantity = $(this).find("Qty").text();
            var UniqueID = $(this).find("UniqueID").text();
            var QtyInStock = $(this).find("MedQTY").text();

            var PresQty = parseInt(MedicineQuantity);
            var stockQty = parseInt(QtyInStock);

            if (stockQty < PresQty) {
                document.getElementById('txtMedQty' + i).style.color = "red";
            }

            document.getElementById('txtMedName' + i).value = MedicineName;
            document.getElementById('txtMedQty' + i).value = MedicineQuantity;
            document.getElementById('txtMedUnit' + i).value = MedicineUnit;
            document.getElementById('txtMedDos' + i).value = MedicineDosage;
            document.getElementById('txtMedTime' + i).value = MedicineTiming;
            document.getElementById('txtMedDay' + i).value = MedicineDays;
            document.getElementById('hdnQty' + i).value = PrescriptionID;
            document.getElementById('hdnDetailID' + i).value = UniqueID;

            document.getElementById('txtMedName' + i).readOnly = true; // --------* medicine name set to non-editable after saving *--------//

            //if (PageCalledFrom != 'StockIn') {

            //    document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + QtyInStock;


            //    PageMethods.GetQtyInStock(MedicineName, OnSuccess, onError);
            //    function OnSuccess(response, userContext, methodName) {
            //        QtyInStock = parseInt(response);
            //    }
            //    function onError(response, userContext, methodName)
            //    { }
            //    var oldQty = parseInt(MedicineQuantity);
            //    var total = parseInt(oldQty + parseInt(QtyInStock));
            //    document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + total;
            //}


            i = i + 1;
        });

    }

    //else {
    //    DocPrescription();
    //}
}




//----------------------Pharmacy-------------//

function RefillPresMedicineTextboxesWithXmlData(hdnXmlData) {

    debugger;

    var XmlDataFromHF = document.getElementById(hdnXmlData).value;
    var xmlDoc = $.parseXML(XmlDataFromHF);
    var xml = $(xmlDoc);
    var Medicines = xml.find("Medicines");
    var i = 0;

    if (Medicines.length > 0) {
        debugger;

        $.each(Medicines, function () {
       
            debugger;
            if (i > 0) {

                clickAdd(i);
            }
            if (Medicines.length > 1) {
                debugger;

                $('#btAdd').css('visibility', 'hidden');
                $('#btAdd' + (Medicines.length - 1)).css('visibility', 'visible');
            }

            var MedicineName = $(this).find("MedicineName").text();
            var MedicineDosage = $(this).find("Dosage").text();
            var MedicineTiming = $(this).find("Timing").text();
            var MedicineDays = $(this).find("Days").text();
            var PrescriptionID = $(this).find("PrescriptionID").text();            
            var MedicineUnit = $(this).find("Unit").text();
            var MedicineQuantity = $(this).find("Qty").text();
            var UniqueID = $(this).find("UniqueID").text();
            var QtyInStock = $(this).find("MedQTY").text();

            var PresQty = parseInt(MedicineQuantity);
            var stockQty = parseInt(QtyInStock);

            if (stockQty < PresQty) {
                document.getElementById('txtMedQty' + i).style.color = "red";
                document.getElementById('OutOfStockMessage').style.display = "";
            }

            document.getElementById('txtMedName' + i).value = MedicineName;
            document.getElementById('txtMedQty' + i).value = MedicineQuantity;
            document.getElementById('txtMedUnit' + i).value = MedicineUnit;
            document.getElementById('txtMedDos' + i).value = MedicineDosage;
            document.getElementById('txtMedTime' + i).value = MedicineTiming;
            document.getElementById('txtMedDay' + i).value = MedicineDays;
            document.getElementById('hdnQty' + i).value = QtyInStock;
            document.getElementById('hdnDetailID' + i).value = UniqueID; //PrescDT uniqueid

            document.getElementById('txtMedName' + i).readOnly = true; // --------* medicine name set to non-editable after saving *--------//
            document.getElementById('txtMedUnit' + i).readOnly = true;
            document.getElementById('txtMedDos' + i).readOnly = true;
            document.getElementById('txtMedTime' + i).readOnly = true;
            i = i + 1;
        });

    }

}

// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';
//------------ *   Function to get textbox values -- stores textbox values into hidden field when data is submitted *-----------//
function GetTextBoxValuesPres(hdnTextboxValues, lblErrorCaption, Errorbox,lblMsgges) {
    debugger;
    values = '';
    var i = 1;
    $('.input').each(function () {
        i++;
    });
    var NumberOfColumns = i - 1;
    var NumberOfRows = NumberOfColumns / 6;
    var topId = iCnt;

    for (var k = 0; k <= topId; k++)
    {      
        if (document.getElementById('txtMedName' + k) == null)
        {
            continue;
        }
        if (((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value == '')) || ((document.getElementById('txtMedName' + k) != null) && (document.getElementById('txtMedName' + k).value == '')))
        {
            continue;
        }

        var MEDQTY = document.getElementById('txtMedQty' + k).value;
        var STOCKQTY = document.getElementById('hdnQty' + k).value;
        var x = parseInt(MEDQTY);
        var y = parseInt(STOCKQTY);
        if (x > y || isNaN(x) || isNaN(y)) {           

            //refering from messages.js
            var lblclass = Alertclasses.danger;
            var lblmsg = msg.Requiredfields;
            var lblcaptn = Caption.Confirm;

            ErrorMessagesDisplay(lblErrorCaption, lblMsgges, Errorbox, lblclass, lblcaptn, lblmsg);

            return false;
            continue;
        }

        var CurrentMedName = document.getElementById('txtMedName' + k).value;
        if (values.indexOf(CurrentMedName) > -1)
        {
        }
        else
        {
            values += document.getElementById('txtMedName' + k).value + '|' + document.getElementById('txtMedQty' + k).value + '|' + document.getElementById('txtMedUnit' + k).value + '|' + document.getElementById('txtMedDos' + k).value + '|' + document.getElementById('txtMedTime' + k).value + '|' + document.getElementById('txtMedDay' + k).value + '|' + document.getElementById('hdnDetailID' + k).value + '$';
        }    

    }
    document.getElementById(hdnTextboxValues).value = values;
   
}

function RemoveWarningPharm(ControlNo) {
 
    debugger;
    //--------------* To remove warning msg from textbox if the medicine is not out of stock , and is called onfocus event of quantity textbox *-------------------//
    if ((document.getElementById('txtMedQty' + ControlNo).value) != 'No Stock') {


        $("#txtMedQty" + ControlNo).removeClass("warning");
       // $("#txtMedQty" + ControlNo).css({ 'color': 'black' });
        $("#txtMedQty" + ControlNo).attr('type', 'number');
    }

}

function CheckPharmacyMedicineIsOutOfStock(ControlNo)
{
    debugger;
 
    var Qty1
    if (PageCalledFrom != 'doctor page')
    {
        if (document.getElementById('txtMedName' + ControlNo) != null && document.getElementById('txtMedQty' + ControlNo) != null)
        {

            if (isNaN(document.getElementById('txtMedQty' + ControlNo).value) == false && (document.getElementById('txtMedQty' + ControlNo).value != ""))
            {
                var MedicineName = document.getElementById('txtMedName' + ControlNo).value;

              
                PageMethods.MedDetails(MedicineName, OnSuccess, onError);
              
                function OnSuccess(response, userContext, methodName)
                {
                    debugger;
                   
                    if (ControlNo >= 0)
                    {
                        debugger;
                        var MedicineDetails = new Array();
                        MedicineDetails = response.split('|');
                      
                        Qty1 = MedicineDetails[1]; //  setting existing stock quantity using page method

                        if (isNaN(Qty1) == false) {
                            debugger;
                            var Qty = Number(Qty1);
                            var InputQty = Number(document.getElementById('txtMedQty' + ControlNo).value);


                            if ((MedicineName != ""))
                            {
                                debugger;
                                if (Qty <= 0) {
                                    debugger;
                                        $("#txtMedQty" + ControlNo).addClass("warning");
                                        $("#txtMedQty" + ControlNo).attr('type', 'text');
                                        $("#txtMedQty" + ControlNo).css({ 'color': ' #ffad99' });
                                        
                                        $("#txtMedQty" + ControlNo).prop('title','No Stock');
                                       // $("#txtMedQty" + ControlNo).val('0');
                                    // $("#txtMedQty" + ControlNo).val('No Stock');
                                      //  document.getElementById('OutOfStockMessage').style.display = "";

                                    }

                                    else if (InputQty > Qty || InputQty <= 0) {
                                        debugger;
                                        $("#txtMedQty" + ControlNo).addClass("warning");
                                      //  $("#txtMedQty" + ControlNo).attr('type', 'text');
                                       // $("#txtMedQty" + ControlNo).css({ 'color': ' #ffad99' });

                                        if (InputQty > Qty) {
                                            debugger;
                                            $("#txtMedQty" + ControlNo).css({ 'color': ' #ffad99' });
                                          //  $("#txtMedQty" + ControlNo).val('Qty<=' + Qty);
                                            $("#txtMedQty" + ControlNo).prop('title', 'Quantity must be ≤' + Qty);
                                           // document.getElementById('OutOfStockMessage').style.display = "";
                                        }

                                        if (InputQty <= 0) {
                                            debugger;
                                            //$("#txtMedQty" + ControlNo).val('Qty > 0');
                                            $("#txtMedQty" + ControlNo).css({ 'color': ' #ffad99' });
                                        }

                                    }
                                    else {
                                        debugger;
                                        $("#txtMedQty" + ControlNo).removeClass("warning");
                                        $("#txtMedQty" + ControlNo).css({ 'color': 'black' });
                                      //  $("#txtMedQty" + ControlNo).attr('type', 'number');
                                       // document.getElementById('OutOfStockMessage').style.display = "none";
                                    }  
                            }
                        }
                    }
                }
                function onError(response, userContext, methodName) { }
            }  
        }
    }


}


//-----------Error Messages--------------//

function ErrorMessagesDisplay(ErrorCaption, lblMsgges, Errorbox,lblclass,lblcaptn,lblmsg)
{
    debugger;
  

   document.getElementById(Errorbox).style.display = "";
   document.getElementById(Errorbox).className = lblclass;   
    
    document.getElementById(ErrorCaption).innerHTML = lblcaptn;
    document.getElementById(lblMsgges).innerHTML = lblmsg;



}
 


// function Allowing only numbers in Textbox

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || (charCode > 57)|| (charCode==08))) {
        return false;
    }
    return true;
}

// function Allowing only alphabets in Textbox 
function isnotNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode ==32)|| (charCode==08)) {
        return true;
    }
    return false;
}
