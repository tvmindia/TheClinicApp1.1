var msg= {
   
    Requiredfields: "Fields may be empty or invaild",
    CompulsorySelect: "Select an item",
    SelectDoctor: "Please Select Doctor",
    AlreadyIssued: "Already issued.Can't be deleted",
    DeletionSuccessFull: "Deleted Successfully",
    AlreadyUsed: "Already used . Can't be deleted",
    InsertionSuccessFull:  "Successfully Inserted"
}

var Caption = {
    Confirm: "Please Confirm!",
    SuccessMsgCaption: "Success!",
    FailureMsgCaption: "Failure!"
}


var Alertclasses = {

    warning: "alert alert-warning",
    info: "alert alert-info",
    sucess: "alert alert-success",
    danger: "alert alert-danger"
    

}

var AppoinmentCancellationMessage = '<p>' + "Dear" + '<strong>' + " %PATIENT NAME%," + '</strong>' + '<br /><br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp' + "Your Appoinment with Dr." + '<strong>' + "%DOCTOR NAME%" + '</strong>' + "On" + '<strong>' + " %DATE%" + '</strong>' + "  At " + '<strong>' + " %TIME%" + '</strong>' + "  has been cancelled due to" + '<strong>' + " %REASON% " + '</strong>' + ". Please contact our office to schedule a new appoinment." + '<br /><br />' + "<b><i>" + "This sms will be send to the contacts given below " + "</i></b>" + '</p>';
 
var CancelReason = "Some inconvenience";