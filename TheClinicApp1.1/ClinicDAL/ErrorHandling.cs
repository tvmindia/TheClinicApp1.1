
#region CopyRight

//Author      : 
//Created Date: 

//Modified By   : SHAMILA T P
//Modified Date : Mar-4-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Messages = TheClinicApp1._1.UIClasses.Messages;

#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class ErrorHandling
    {

        //public ErrorHandling()
        //{

        //    ErrorID = Guid.NewGuid();
        //}


        #region Global Variables

        public Guid ErrorID
        {
            get;
            set;
        }
        public int ErrorNumber
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Module
        {
            get;
            set;
        }

        public string Method
        {
            get;
            set;
        }

        public Guid UserID
        {
            get;
            set;
        }

        #endregion Global Variables

        #region Insert Error

        /// <summary>
        /// To add the the exception details to errorlog
        /// </summary>

        public void InsertError()
        {
            dbConnection dcon = null;

            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertErrorLog]";

                cmd.Parameters.Add("@ErrorID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = Description;
                cmd.Parameters.Add("@Module", SqlDbType.NVarChar, 15).Value = Module;
                cmd.Parameters.Add("@Method", SqlDbType.NVarChar, 15).Value = Method;

                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;



                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                ErrorNumber = Outputval;

                var page = HttpContext.Current.CurrentHandler as Page;
                DisplayErrorNo(page);
              
            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion Insert Error


        public void DisplayErrorNo(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.ErrorNumber + ErrorNumber;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.FailureMsgCaption;
            divMask1.Attributes["class"] = "alert alert-danger";
        }





        public void InsertionSuccessMessage(Page pg)
        {

            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = Messages.InsertionSuccessFull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
            // Success.Text = "Successfully Inserted"; 

        }
        public void InsertionSuccessMessage1(Page pg,string msg)
        {

            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
            // Success.Text = "Successfully Inserted"; 

        }
        public void InsertionSuccessMessage(Page pg, string msg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.InsertionSuccessFull;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            divMask1.Attributes["class"] = "alert alert-success";

        }

        public void InsertionSuccessMessage(Page pg, string msgCaption,string msgText)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = msgCaption;
            lblMsgges.Text = msgText;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            divMask1.Attributes["class"] = "alert alert-success";

        }
        public void UpdationSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = Messages.UpdationSuccessFull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
        }
        public void PatientUpdationSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = Messages.PatientUpdationSuccessFull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
        }
        public void UpdationNotSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.FailureMsgCaption;
            lblMsgges.Text = Messages.UpdationFailure;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            divMask1.Attributes["class"] = "alert alert-info";

        }

        public void UpdationSuccessMessage(Page pg, string msg)//if update fails
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            divMask1.Attributes["class"] = "alert alert-info";
        }

        public void ErrorData(Exception ex, Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = ex.Message;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            //lblErrorCaption.Text = "Danger!";
            lblErrorCaption.Text = Messages.ExceptionMsgCaption;
            //divMask1.Attributes["class"] = "alert lblErrorCaptionDanger fade in";
            divMask1.Attributes["class"] = "alert alert-danger";


        }
        public void DeletePatientErrorData(string ex,Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = ex;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            //lblErrorCaption.Text = "Danger!";
            lblErrorCaption.Text = Messages.ExceptionMsgCaption;
            //divMask1.Attributes["class"] = "alert lblErrorCaptionDanger fade in";
            divMask1.Attributes["class"] = "alert alert-danger";


        }
        public void DeleteSuccessMessage(Page pg)
        {
           
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = Messages.DeletionSuccessFull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
            // Success.Text = "Successfully Inserted"; 


        }
        public void DeleteSuccessMessage(Page pg,string msg)
        {

            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
            // Success.Text = "Successfully Inserted"; 


        }

        public void WarningMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.Warning;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.WarningMsgCaption;
            divMask1.Attributes["class"] = "alert alert-warning";
        }
        public void WarningMessage1(Page pg,string msg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.WarningMsgCaption;
            divMask1.Attributes["class"] = "alert alert-warning";
        }

        public void InsertionNotSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.InsertionFailure;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.InsertionFailureMsgCaption;
            divMask1.Attributes["class"] = "alert alert-info";
        }

        public void AlreadyExistsMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.InsertionFailure;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.AlreadyExistsMsgCaption;
            divMask1.Attributes["class"] = "alert alert-danger";
        }

        public void InsertionNotSuccessMessage(Page pg,string msg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.Confirm;
            divMask1.Attributes["class"] = "alert alert-danger";

        }

        public void DeletionNotSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.DeletionFailure;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.FailureMsgCaption;
            divMask1.Attributes["class"] = "alert alert-danger";

        }
        public void DeletionNotSuccessMessage(Page pg, string msg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = msg;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.FailureMsgCaption;
            divMask1.Attributes["class"] = "alert alert-danger";

        }
       
        public void SavedSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            lblMsgges.Text = Messages.SavedSuccessfull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   

            //divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            divMask1.Attributes["class"] = "alert alert-success";
            // Success.Text = "Successfully Inserted"; 
        }

        public void  SavingFailureMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.SavingFailure;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.FailureMsgCaption;
            divMask1.Attributes["class"] = "alert alert-danger";
        }


       

    }
}