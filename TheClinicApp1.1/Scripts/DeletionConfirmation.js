


function ConfirmDelete(IsDelete) {
    debugger;


    if (IsDelete == false) //-- Cancelation
    {
        return confirm(" Are you sure you want to Cancel the schedule? ");
    }
    else //Deletion
    {
        return confirm(" Are you sure you want to delete? ");
    }
  
   
}