


function ConfirmDelete(IsDelete) {
  
    //---* we can call this function without any parameter , if required confirm msg is for deletion
    // function is called by parameter -false , for specific confirm msg (cancellation of schedule)


    if (IsDelete == false) //-- Cancelation
    {
        return confirm(" Are you sure you want to Cancel this schedule? ");
    }
    if(IsDelete == true)//Patient appointement cancel
    {
        return confirm(" Are you sure you want to Cancel this Appointment? ");
    }
    else //Deletion
    {
        return confirm(" Are you sure you want to delete? ");
    }

}


function ConfirmLogout()
{
    return confirm(" Are you sure you want to Logout? ");
}