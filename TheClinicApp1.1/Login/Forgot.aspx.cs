﻿
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

using Messages = TheClinicApp1._1.UIClasses.Messages;

#endregion Included Namespaces

namespace TheClinicApp1._1.Login
{
    public partial class Forgot : System.Web.UI.Page
    {
        #region GlobalVariables

        ClinicDAL.User userObj = new ClinicDAL.User();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        common cmn = new common();
        Master mstrobj = new Master();
        MailSending mailObj = new MailSending();

        #endregion GlobalVariables

        #region Events

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
        }
        #endregion PageLoad

        #region Verify Code
        protected void btnVerify_ServerClick(object sender, EventArgs e)
        {
            int verificationCode = 0;
            string UserID = string.Empty;
            DateTime vcCreatedTime;

            bool Verified = false;
            bool TimeExpired = false;

            try
            {
                userObj.Email = txtEmail.Value;


                DataTable dtCode = userObj.GetUserVerificationCodeByEmailID();

                foreach (DataRow dr in dtCode.Rows)
                {

                    verificationCode = Convert.ToInt32(dr["VerificationCode"]);
                    vcCreatedTime = Convert.ToDateTime(dr["VerificatinCreatedTime"]);
                    UserID = dr["UserID"].ToString();

                    DateTime CurrentTime =cmn.ConvertDatenow(DateTime.Now);
                    if ((CurrentTime - vcCreatedTime) < TimeSpan.FromDays(1))
                    {

                        if (verificationCode.ToString() == txtVerificationCode.Value)
                        {
                            Verified = Verified | true;
                            break;
                        }
                    }

                    else
                    {
                        TimeExpired = TimeExpired | true;
                    }

                }


                if (Verified)
                {
                    if (TimeExpired == false)
                    {
                        Response.Redirect("../Login/Reset.aspx?UserID=" + UserID, false);
                    }
                    else
                    {
                        lblError.Text = Messages.TimeExpired;
                    }
                }

                else
                {
                    lblError.Text = Messages.IncorrectVerificationCode;
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }
        #endregion Verify Code

        #region Send Verification Code

        /// <summary>
        /// User has to give the mail id (which was given at time of registration) in corresponding textbox,verification code will be sent to mail , then textbox to enter verification code will be displayed, user has 
        /// to enter verification code within a day ,then will be redirected to change password section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        protected void btnVerificationCode_ServerClick1(object sender, EventArgs e)
        {
            string username = string.Empty;
            string ClinicName = string.Empty;
            string msg = string.Empty;

            try
            {
                //----------*Add verification code*------------//
                if (txtEmail.Value != "")
                {
                    Random random = new Random();
                    int verificationCode = 0;


                    userObj.Email = txtEmail.Value;
                    DataTable dtUsr = userObj.GetUserDetailsByEmailID();

                    foreach (DataRow dr in dtUsr.Rows)
                    {
                        Guid clinicid = Guid.Parse(dr["ClinicID"].ToString());
                        userObj.ClinicID = clinicid;

                        verificationCode = random.Next(1000, 10000);
                        userObj.verificationCode = verificationCode.ToString();
                        userObj.AddVerificationCode();
                    }

                    //----------*Get verification code*------------//
                    userObj.Email = txtEmail.Value;
                    DataTable dtCode = userObj.GetUserVerificationCodeByEmailID();

                    foreach (DataRow dr in dtCode.Rows)
                    {
                        verificationCode = Convert.ToInt32(dr["VerificationCode"]);
                        username = (dr["LoginName"]).ToString();
                        ClinicName = (dr["ClinicName"]).ToString();

                        msg = "<body><p>Your verification code of " + ClinicName + " with login name " + username + " is <font color='red'>" + verificationCode + "</font></p><p>" + msg + "</p></body>";
                        //" Your Verification Code of " + ClinicName + " with Login name " + username + " is " + verificationCode+msg;

                    }


                    if (msg != string.Empty)
                    {
                        //-- Area of verification code will be displayed and email area will be hidden

                        Code.Style.Add("display", "block");
                        email.Style.Add("display", "none");
                        instruction.Visible = true;
                        instruction.InnerText = Messages.EmailInstruction + txtEmail.Value;

                        mailObj.Email = txtEmail.Value;
                        mailObj.msg = msg;
                        mailObj.SendForgotPaaswordEmail();

                    }

                    DateTime CurrentTime =cmn.ConvertDatenow(DateTime.Now);

                }
                else
                {
                    lblError.Text = Messages.InvalidEmailID;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        #endregion Send Verification Code

        #endregion Events

        #region Methods

        #endregion Methods
    }
}