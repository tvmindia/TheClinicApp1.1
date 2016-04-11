/*! jQuery Dynamic Grid using TextBoxes| (c) Author: Thomson Kattingal | /www.thrithvam.com */
/*!No Modifications are allowed without permission*/
  
var iCnt = 0;
// CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
var container = $(document.createElement('div')).css({
    width: '100%',
    borderTopColor: '#FFFFFF', borderBottomColor: '#FFFFFF',
    borderLeftColor: '#FFFFFF', borderRightColor: '#FFFFFF'
});

function clickStockAdd(id) {
    iCnt = iCnt + 1;

    // ADD new row with fields needed.
    $(container).append('<div><table class="table" style="width:100%;border:0;">'
               + ' <td ><input id="txtMedName' + iCnt + '" type="text" placeholder="Medicine' + iCnt + '" class="input"/></td>'
                + '<td ><input id="txtMedUnit' + iCnt + '" class="input" type="text" placeholder="Unit' + iCnt + '" /></td>'
                + '<td ><input id="txtMedCode' + iCnt + '" type="text" placeholder="Code' + iCnt + '" class="input"/></td>'
                + '<td><input id="txtMedCat' + iCnt + '" type="text" placeholder="Category' + iCnt + '" class="input"/></td>'
                 + '<td><input id="txtMedQty' + iCnt + '" type="text" placeholder="Quantity' + iCnt + '" class="input"/></td>'
                 + '<td style="background:#E6E5E5">'
                 + '<input type="button" id="btRemove' + iCnt + '" class="bt1" value="-" onclick="clickdelete(' + iCnt + ')" style="width:20px" /></td>'
                 + '<td style="background:#E6E5E5">'
                 + '<input type="button" id="btAdd' + iCnt + '" value="+" onclick="clickStockAdd(' + iCnt + ')" class="bt" style="width:20px" /></td>'
                 + '</tr></table><div>');
    $('#maindiv').after(container);
    $('#btAdd' + id).css("visibility", "hidden");
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
    if (iCnt > 1)
    {

        if ((id == iCnt) && (Removecount == 0))
        {
            $('#btAdd' + (iCnt - 1) + '').css('visibility', 'visible')
            $('#btRemove' + id).closest("div").remove();
            iCnt = iCnt - 1;
        }
        else if ((id <= iCnt) && (Removecount != 0))
        {
            $('#btAdd' + (iCnt - 2) + '').css('visibility', 'visible')
            $('#btRemove' + id).closest("div").remove();
            iCnt = iCnt - 1;
        }
        else if((id == iCnt) && (Removecount != 0))
        {
            $('#btAdd' + (iCnt - 2) + '').css('visibility', 'visible')
            $('#btRemove' + id).closest("div").remove();
            iCnt = iCnt - 1;
        }
        else if ((id == (iCnt - 1))&&(Removecount==0))
        {        
            $('#btRemove' + id).closest("div").remove();        
            Removecount = Removecount + 1;
        }
        else
        {
            $('#btRemove' + id).closest("div").remove();
            $('#btAdd' + (iCnt - 2) + '').css('visibility', 'visible')
            iCnt = iCnt - 1;
        }
    }

    else
    {
        $('#btAdd').css('visibility', 'visible')
        $('#btRemove' + id).closest("div").remove();
        iCnt = 0;
    }

}
// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';

function GetTextValue() {
    alert(values);
    $(divValue)
        .empty()
        .remove();
    values = '';
    $('.input').each(function () {
        divValue = $(document.createElement('div')).css({
            padding: '5px', width: '200px'
        });
        var datas = document.getElementById('<%=HiddenField1.ClientID%>');
        values += this.value + '|';
        alert(values);
        datas.value = values;
    });

}