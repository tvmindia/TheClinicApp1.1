
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

#endregion Included Namespaces

namespace TheClinicApp.UIClasses
{
    public class Messages
    {


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
            get { return "Already exists!"; }
        }

        public static string FailureMsgCaption
        {
            get { return "Failure!"; }
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

        public static string UpdationSuccessFull
        {
            get { return "Successfully Updated"; }
        }

        public static string DeletionSuccessFull
        {
            get { return "Deleted Successfully"; }
        }
        public static string SuccessfulUpload
        {
            get { return "Successfully Uploaded"; }
        }

//----------------* Failure Messages *--------------//

        public static string LoginFailed
        {
            get { return "User Name / Password is wrong!"; }
        }

        public static string InsertionFailure
        {
            get { return "Insertion Not Successful"; }
        }

        public static string UpdationFailure
        {
            get { return "Updatation Failure"; }
        }

        public static string Warning
        {
            get { return "Warning Msg "; }
        }

    }
}