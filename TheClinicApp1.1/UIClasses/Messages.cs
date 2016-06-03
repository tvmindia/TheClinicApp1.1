
#region CopyRight

//Author      :
//Created Date: 

//Modified BY   : SHAMILA T P
//Modified Date : Mar-4-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Messages = TheClinicApp1._1.UIClasses.Messages;

#endregion Included Namespaces

namespace TheClinicApp1._1.UIClasses
{
    public class Messages
    {

        public static string EmailInstruction
        {
            get { return "Please check your email for a message with verification code.Your code is 4 digit long . We sent code to "; }
        }

        public static string VerificationCodeMismatch
        {
            get { return "Password does not match the confirm password"; }
        }

        public static string InvalidEmailID
        {
            get { return "Enter A valid Email-ID"; }
        }

        public static string TimeExpired
        {
            get { return "Time expired"; }
        }

        public static string IncorrectVerificationCode
        {
            get { return "Verification Code is invalid"; }
        }


        public static string EditImageButtonDisabled
        {
            get { return " Logined user can't be edited"; }
        }

        public static string DeleteImageButtonDisabled
        {
            get { return " Logined user can't be deleted"; }
        }

        public static string ErrorNumber
        {
            get { return "Error Number = "; }
        }

       public static string DisableAssignRole
        {
            get { return "Can't assign role for logined user"; }
        }

 //----------------* Messages Captions *--------------//
        public static string ExceptionMsgCaption
        {
            get { return "Exception!"; }
        }

        public static string SuccessMsgCaption
        {
            get { return "Success!"; }
        }


        public static string WarningMsgCaption
        {
            get { return "Warning!"; }
        }

        public static string InsertionFailureMsgCaption
        {
            get { return "Somthing Wroung try Again!"; }
        }

        public static string FailureMsgCaption
        {
            get { return "Failure!"; }
        }

        public static string AlreadyExistsMsgCaption
        {
            get { return "Already exists!"; }
        }
        public static string Mandatory
        {
            get { return "Please fill out the mandatory fields Name and Age"; }
        }
        public static string AgeIssue
        {
            get { return "Age seems to Empty or Not a number"; }
        }
        public static string Imagesupport
        {
            get { return "The Image Is not Supporting Save a new one"; }
        }
       
        public static string Confirm
        {
            get { return "Please Confirm!"; }
        }

//----------------* Success Messages *--------------//
        public static string LoginSuccess
        {
            get { return "Successfully logged in"; }
        }

        public static string InsertionSuccessFull
        {
            get { return "Successfully Inserted"; }
        }
        public static string PatInsertionSuccessFull
        {
            get { return "Patient Successfully Saved with a Case File"; }
        }
        public static string TokenExist
        {
            get { return "Token Exist Cant able to Delete Patient"; }
        }

        public static string UpdationSuccessFull
        {
            get { return "Successfully Updated"; }
        }
        public static string PatientUpdationSuccessFull
        {
            get { return "Patient Successfully Edited And Saved"; }
        }

        public static string DeletionSuccessFull
        {
            get { return "Deleted Successfully"; }
        }
        public static string PatientDeletionSuccessFull
        {
            get { return "The Patient Deleted Successfully"; }
        }
        public static string SuccessfulUpload
        {
            get { return "Successfully Uploaded"; }
        }
        public static string SavedSuccessfull
        {
            get { return "Successfully Saved!"; }
        }


//----------------* Failure Messages *--------------//

        public static string LoginFailed
        {
            get { return "User Name / Password is wrong!"; }
        }

        public static string InsertionFailure
        {
            get { return "Not Successfuly Saved Try Again"; }
        }

        public static string UpdationFailure
        {
            get { return "Edit Failed Try Again Later"; }
        }

        public static string Warning
        {
            get { return "Warning Msg "; }
        }

        public static string DeletionFailure
        {
            get { return "Deletion Not Successful "; }
        }
        public static string PatientDeletionFailure
        {
            get { return "Deletion Not Successful For this Patient "; }
        }

        public static string SavingFailure
        {
            get { return "Saving Not Successful "; }
        }

        public static string MandatoryFields
        {
            get { return "Please fill out all the fields"; }
        }

        public static string AlreadyUsedForDeletion
        {
            get { return "Already used . Can't be deleted"; }
        }

        public static string AlreadyUsedForUpdation
        {
            get { return "Already used . Can't be changed"; }
        }

        public static string SelectCatergory
        {
            get { return "Please select a category ! "; }
        }

        public static string Selectunit
        {
            get { return "Please select a unit ! "; }
        }
        public static string ValidMedicineName
        {
            get { return "Please enter a valid medicine name"; }
        }

        public static string ReorderQtyMandatory
        {
            get { return "Please enter reorder quantity"; }
        }
      
         public static string validReorderQty
        {
            get { return "Please enter a quantity greater than 0"; }
        }
         public static string PassowrdMismatch
         {
             get { return "Passwords do not match ! "; }
         }
        public static string ConfirmInput
         {
             get { return "Fields may be empty or invaild"; }
         }

       
    }
}