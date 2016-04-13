﻿/*! jQuery Dynamic Grid using TextBoxes| (c) Author: Thomson Kattingal | /www.thrithvam.com */
/*!No Modifications are allowed without permission*/
var last = 0;
var iCnt = 0;
var ExistingRowCount = 0;
// CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
var container = $(document.createElement('div')).css({
    width: '100%',
    borderTopColor: '#FFFFFF', borderBottomColor: '#FFFFFF',
    borderLeftColor: '#FFFFFF', borderRightColor: '#FFFFFF'
});

function clickStockAdd(id) {
    iCnt = iCnt + 1;

     //ADD new row with fields needed.
   
    $(container).append('<div id="div' + iCnt + '"><table class="table" style="width:100%;border:0;"><tr>'
       + '<td><input id="txtMedicine' + iCnt + '" class="input" type="text" placeholder="Medicine' + iCnt + '" onblur="BindControlsByMedicneName(' + iCnt + ')" onfocus="autocompleteonfocus(' + iCnt + ')"  /></td>'
       + '<td><input id="txtUnit' + iCnt + '" readonly="true" class="input "  type="text" placeholder="Unit' + iCnt + '" /></td>'
       + '<td><input id="txtCode' + iCnt + '" readonly="true" class="input "  type="text" placeholder="Med Code' + iCnt + '"/></td>'
       + '<td><input id="txtCategory' + iCnt + '" readonly="true" class="input "  type="text" placeholder="Category' + iCnt + '"/></td>'
       + '<td><input id="txtQuantity' + iCnt + '" class="input" min="1" type="number" placeholder="Quantity' + iCnt + '" onblur="CheckMedicineIsOutOfStock(' + iCnt + ')" onfocus="RemoveWarning(' + iCnt + ')" /></td>'
       + '<td style="background:#E6E5E5">'
       + '<input type="button" id="btRemove' + iCnt + '" class="bt1" value="-" onclick="clickdelete(' + iCnt + ')" style="width:20px" /></td>'
       + '<td style="background:#E6E5E5">'
       + '<input type="button" id="btAdd' + iCnt + '" value="+" onclick="clickStockAdd(' + iCnt + ')" class="bt" style="width:20px" /></td>'
       + '<td style="background:#E6E5E5"><input id="hdnDetailID' + iCnt + '" type="hidden" /> <input id="hdnQty' + iCnt + '" type="hidden" /></td>'
                              + '</tr> </table> </div>');

    $('#maindiv').after(container);
    $('#btAdd' + id).css("visibility", "hidden");

    ExistingRowCount = ExistingRowCount + 1;
    last = last + 1;
}

function clickAdd(id) {
    debugger;
    iCnt = iCnt + 1;

    // ADD new row with fields needed.
    $(container).append('<div><table class="table" style="width:100%;border:0;">'
               + ' <td ><input id="txtMedName' + iCnt + '" type="text" placeholder="Medicine' + iCnt + '" class="input"/></td>'
                + '<td ><input id="txtMedQty' + iCnt + '" type="text" placeholder="Qty' + iCnt + '" class="input"/></td>'
                + '<td ><input id="txtMedUnit' + iCnt + '" class="input" type="text" placeholder="Unit' + iCnt + '" /></td>'
                + '<td ><input id="txtMedDos' + iCnt + '" type="text" placeholder="Dosage' + iCnt + '" class="input"/></td>'
                + '<td><input id="txtMedTime' + iCnt + '" type="text" placeholder="Timing' + iCnt + '" class="input"/></td>'
                 + '<td><input id="txtMedDay' + iCnt + '" type="text" placeholder="Days' + iCnt + '" class="input"/></td>'
                 + '<td style="background:#E6E5E5">'
                 + '<input type="button" id="btRemove' + iCnt + '" class="bt1" value="-" onclick="clickdelete(' + iCnt + ')" style="width:20px" /></td>'
                 + '<td style="background:#E6E5E5">'
                 + '<input type="button" id="btAdd' + iCnt + '" value="+" onclick="clickAdd(' + iCnt + ')" class="bt" style="width:20px" /></td>'
                 + '</tr></table><div>');
    $('#maindiv').after(container);
    $('#btAdd' + id).css("visibility", "hidden");

}

//Delete Div where the remove button contain.
var Removecount = 0;

function clickdelete(id) {
    debugger;
    if (ExistingRowCount > 1)
    {
         
        if (id == iCnt && id==ExistingRowCount)
        {
            $('#btAdd' + (iCnt - 1) + '').css('visibility', 'visible')
            last = id - 1;
            $('#btRemove' + id).closest("div").remove();
            //iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;
        }
//***********************************************************************************

        if (id == iCnt) {
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

        }


        else {
            $('#btRemove' + id).closest("div").remove();
            // iCnt = iCnt - 1;
            ExistingRowCount = ExistingRowCount - 1;
        }
    }

    else
    {
        $('#btAdd').css('visibility', 'visible')
        $('#btRemove' + id).closest("div").remove();
        ExistingRowCount = ExistingRowCount - 1;
        last = iCnt;
        
    }

}

// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';
//------------ *   Function to get textbox values -- stores textbox values into hidden field when data is submitted *-----------//
function GetTextBoxValues() {
 
    values = '';
    var i = 1;
    $('.input').each(function () {
        i++;
    });
    var NumberOfColumns = i - 1;
    var NumberOfRows = NumberOfColumns / 5;
    for (var k = 1; k <= NumberOfRows; k++)
    {
        var qty = document.getElementById('txtQuantity' + k).value;
        if (qty.indexOf('Must be') > -1)
        {
            document.getElementById('txtQuantity' + k).value = document.getElementById('hdnQty' + k).value;
        }

        if (((document.getElementById('txtQuantity' + k) != null) && (document.getElementById('txtQuantity' + k).value != '') && (isNaN(document.getElementById('txtQuantity' + k).value)) == false) && ((document.getElementById('txtMedicine' + k) != null) && (document.getElementById('txtMedicine' + k).value != '')))
        {
            var CurrentMedName = document.getElementById('txtMedicine' + k).value;
            if (values.indexOf(CurrentMedName) > -1)
            { }
            else
            {
                values += document.getElementById('txtMedicine' + k).value + '|' + document.getElementById('txtUnit' + k).value + '|' + document.getElementById('txtCode' + k).value + '|' + document.getElementById('txtCategory' + k).value + '|' + document.getElementById('txtQuantity' + k).value + '|' + document.getElementById('hdnDetailID' + k).value + '$';
            }
        }

        if ((document.getElementById('txtQuantity' + k) != null) && ((document.getElementById('txtQuantity' + k).value == '') || (isNaN(document.getElementById('txtQuantity' + k).value) == true)))
        {
            if ((document.getElementById('hdnDetailID' + k) != null) && (document.getElementById('hdnDetailID' + k).value != ''))
            {
                RemovedIDs += document.getElementById('hdnDetailID' + k).value + ',';
                
                if (RemovedIDs == ',')
                {
                    RemovedIDs = '';
                }
                document.getElementById('<%=hdnRemovedIDs.ClientID%>').value = RemovedIDs;
            }
        }
    }
    document.getElementById('<%=hdnTextboxValues.ClientID%>').value = values;

}

//-----* Function to bind textboxes by medicine name -- fills textboxes when focus is lost from medicine textbox  *----//
function BindControlsByMedicneName(ControlNo) {
   

    if (ControlNo >= 1) {
        var MedicineName = document.getElementById('txtMedicine' + ControlNo).value;

    }

    if (MedicineName != "") {

        PageMethods.MedDetails(MedicineName, OnSuccess, onError);
    }

    function OnSuccess(response, userContext, methodName) {
        if (ControlNo >= 1) {

            var MedicineDetails = new Array();
            MedicineDetails = response.split('|');

            document.getElementById('txtUnit' + ControlNo).value = MedicineDetails[0];
            document.getElementById('txtCode' + ControlNo).value = MedicineDetails[1];
            document.getElementById('txtCategory' + ControlNo).value = MedicineDetails[2];
            document.getElementById('txtQuantity' + ControlNo).placeholder = " Out Of: " + MedicineDetails[3];
            document.getElementById('hdnQty' + ControlNo).value = parseInt(MedicineDetails[3]);

        }

    }
    function onError(response, userContext, methodName) {

    }


}

//-----------* Checks whether medicine is out of stock , when user input quantity , and is called onblur event of quantity textbox *-----------// 
function CheckMedicineIsOutOfStock(ControlNo) {
                 
    if (document.getElementById('txtMedicine' + ControlNo) != null && document.getElementById('txtQuantity' + ControlNo) != null)
    {
        var Qty = parseInt(document.getElementById('hdnQty' + ControlNo).value);
        var MedicineName = document.getElementById('txtMedicine' + ControlNo).value;
        var InputQty = Number(document.getElementById('txtQuantity' + ControlNo).value);
        document.getElementById('hdnQty' + ControlNo).value = InputQty;

        if ((MedicineName != "") && (InputQty != ""))
        {
            //----------- * Case Of Insert *----------//
            if (document.getElementById('hdnDetailID' + ControlNo).value == "")
            {
               
                if (InputQty > Qty)
                {
                    $("#txtQuantity" + ControlNo).addClass("warning");
                    $("#txtQuantity" + ControlNo).attr('type', 'text');
                    $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });
                    $("#txtQuantity" + ControlNo).val('Must be < ' + Qty);                   
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
                if (InputQty > QtyInStock)
                {
                    $("#txtQuantity" + ControlNo).addClass("warning");
                    $("#txtQuantity" + ControlNo).attr('type', 'text');
                    $("#txtQuantity" + ControlNo).css({ 'color': ' #ffad99' });
                    $("#txtQuantity" + ControlNo).val('Must be < ' + QtyInStock);       
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



//----------------------------------- * Function to rebind medicine textboxes -- refills controls by retrieving data from xml *--------------------//
function RefillTextboxesWithXmlData(Medicines) {

   
    var XmlDataFromHF = document.getElementById('<%=hdnXmlData.ClientID%>').value;
    var xmlDoc = $.parseXML(XmlDataFromHF);
    var xml = $(xmlDoc);
    var Medicines = xml.find("Medicines");
    var i = 1;

    if (Medicines.length > 0)
    {
        document.getElementById('<%=txtIssueNO.ClientID %>').readOnly = true;

        $.each(Medicines, function () {

            debugger;
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
            document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + QtyInStock;
            document.getElementById('txtMedicine' + i).readOnly = true; // --------* medicine name set to non-editable after saving *--------//

            PageMethods.GetQtyInStock(MedicineName, OnSuccess, onError);
            function OnSuccess(response, userContext, methodName)
            {
                QtyInStock = parseInt(response);
            }
            function onError(response, userContext, methodName)
            {  }
            var oldQty = parseInt(MedicineQuantity);
            var total = parseInt(oldQty + parseInt(QtyInStock));
            document.getElementById('txtQuantity' + i).placeholder = " Out Of: " + total;
            i = i + 1;
        });

    }


}



