$(document).ready(function () {
                                
    var iCnt = 0;

    //Removing Div on btRemove button click
    $("body").on("click", "#btRemove", function () 
    {
        debugger;
        if (iCnt > 1)
        {
            $(this).closest("div").remove();

            if ($('#btAdd' + (iCnt) + '').css('visibility') == 'visible')
            {
                debugger;
                //alert(0010);
                //Nothing doing
            }
            else
            {

                $('#btAdd' + (iCnt - 1) + '').css('visibility', 'visible')
                iCnt = iCnt - 1;

            }
                                                
        }

    });


    // CREATE A "DIV" ELEMENT AND DESIGN IT USING JQUERY ".css()" CLASS.
    var container = $(document.createElement('div')).css({
        width: '100%',
        borderTopColor: '#FFFFFF', borderBottomColor: '#FFFFFF',
        borderLeftColor: '#FFFFFF', borderRightColor: '#FFFFFF'
    });

    //Onclick function for Adding Controls and Textboxes
    $('body').on('click', ('#btAdd,.bt'), function () {

        if (iCnt <= 100) {
            iCnt = iCnt + 1;

            // ADD TEXTBOX And Controls
            $(container).append('<div><table class="table" style="width:100%;border:0;"><tr><td><input id="txtMedname5" type="text" class="input" placeholder="Medicine"/></td><td><input id="txtMedname5" type="text" class="input" placeholder="Timmings"/></td><td><input id="txtMeddoz5" type="text" class="input" placeholder="Dozage"/></td><td><input id="txtMedDays5" type="text" class="input" placeholder="Days"/></td><td style="background:#E6E5E5"><input type="button" id="btRemove" class="bt1" value="-" style="width:20px" /></td><td style="background:#E6E5E5"><input type="button" id="btAdd' + (iCnt) + '" value="+" onclick=this.style="visibility:hidden;" class="bt" style="width:20px" /></td></tr></table><div>');

            // ADD THE DIV ELEMENTS TO THE "Prescription" CONTAINER.

            $('#maindiv').after(container);

        }

            // AFTER REACHING THE SPECIFIED LIMIT, DISABLE THE "ADD" BUTTON.
            // (20 IS THE LIMIT WE HAVE SET)
        else {
            $(container).append('<label>Reached the limit</label>');
            $('#btAdd').attr('class', 'bt-disable');
            $('#btAdd').attr('disabled', 'disabled');

        }

    });
    var ac=null;
    ac = <%=listFilter %>;
    $( "#txtSearch" ).autocomplete({
        source: ac
    })
});

// PICK THE VALUES FROM EACH TEXTBOX WHEN "SUBMIT" BUTTON IS CLICKED.
var divValue, values = '';

function GetTextValue() {

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