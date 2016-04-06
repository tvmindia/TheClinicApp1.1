
#region CopyRight

//Author      : 
//Created Date: 

//Modified By   : SHAMILA T P
//Modified Date : Mar-4-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Messages = TheClinicApp.UIClasses.Messages;

#endregion Included Namespaces

namespace TheClinicApp.ClinicDAL
{
    public class ErrorHandling
    {
        public int ErrorNumber
        {
            get;
            set;
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

            divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
            // Success.Text = "Successfully Inserted"; 

        }
        public void InsertionSuccessMessage(Page pg, string msg)//if insert does not happend becasue of already existing
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

            divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";

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

            divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";

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

            divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";

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

            divMask1.Attributes["class"] = "alert lblErrorCaptionSuccess fade in";
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
            divMask1.Attributes["class"] = "alert lblErrorCaptionDanger fade in";


        }
        public void DeleteSuccessMessage(Page pg)
        {
            var master1 = pg.Master;
            ContentPlaceHolder mpContentPlaceHolder1;
            mpContentPlaceHolder1 = (ContentPlaceHolder)master1.FindControl("ContentPlaceHolder1");
            HtmlControl divMask1 = (HtmlControl)mpContentPlaceHolder1.FindControl("Errorbox") as HtmlControl;
            Label lblMsgges = mpContentPlaceHolder1.FindControl("lblMsgges") as Label;
            lblMsgges.Text = Messages.DeletionSuccessFull;
            divMask1.Style["display"] = "";// divMask1.Style["display"] = "";   
            Label lblErrorCaption = mpContentPlaceHolder1.FindControl("lblErrorCaption") as Label;
            lblErrorCaption.Text = Messages.SuccessMsgCaption;
            divMask1.Attributes["class"] = "alert lblErrorCaptionDanger fade in";
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
            divMask1.Attributes["class"] = "alert lblErrorCaptionWarning fade in";
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
            divMask1.Attributes["class"] = "alert lblErrorCaptionDanger fade in";
        }
    }
}