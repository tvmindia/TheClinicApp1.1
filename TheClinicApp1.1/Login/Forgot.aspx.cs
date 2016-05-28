
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

#endregion Included Namespaces

namespace TheClinicApp1._1.Login
{
    public partial class Forgot : System.Web.UI.Page
    {
        #region GlobalVariables
        ClinicDAL.User userObj = new ClinicDAL.User();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Master mstrobj = new Master();

        #endregion GlobalVariables

        #region Events

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            
        }
        #endregion PageLoad

        #endregion Events

        #region Methods

        #region Verify Code
        protected void btnVerify_ServerClick(object sender, EventArgs e)
        {
            try
            {
                userObj.Email = txtEmail.Value;
                DataTable dtCode = userObj.GetUserVerificationCodeByEmailID();
                int verificationCode = Convert.ToInt32(dtCode.Rows[0]["VerificationCode"]);
                DateTime vcCreatedTime = Convert.ToDateTime(dtCode.Rows[0]["VerificatinCreatedTime"]);
                string UserID = dtCode.Rows[0]["UserID"].ToString();
                ;
                DateTime CurrentTime = DateTime.Now;
                if ((CurrentTime - vcCreatedTime) < TimeSpan.FromDays(1))
                {
                    if (verificationCode.ToString() == txtVerificationCode.Value)
                    {
                        Response.Redirect("Reset.aspx?UserID=" + UserID);
                    }
                    else
                    {
                        lblError.Text = "Verification Code is not correct";
                    }

                }
                else
                {
                    lblError.Text = "Time expired";
                }
            }
            catch
            {
                lblError.Text = "Failure! Enter A valid Code";
            }
            
        }
        #endregion Verify Code

        #region Send Verification Code
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
                 DataTable      dtUsr =   userObj.GetUserDetailsByEmailID();

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

                msg = "<body><p>Your Verification Code of " + ClinicName + " with Login name " + username + " is <font color='red'>" + verificationCode + "</font></p><p>" + msg + "</p></body>";
                    //" Your Verification Code of " + ClinicName + " with Login name " + username + " is " + verificationCode+msg;
            }

            DateTime vcCreatedTime = Convert.ToDateTime(dtCode.Rows[0]["VerificatinCreatedTime"]);
            DateTime CurrentTime = DateTime.Now;
            MailMessage Msg = new MailMessage();

            // Sender e-mail address.
            Msg.From = new MailAddress("info.thrithvam@gmail.com");

            // Recipient e-mail address.
            Msg.To.Add(txtEmail.Value);

            string body = verificationCode.ToString();
            string message = "<body><p><p>&nbsp;&nbsp;<h3>Hello ,</h3>"+msg+"<p>Enter Your Code in given field and change your Password<p><p><p><p>&nbsp;&nbsp;&nbsp;&nbsp; ClinicApp&nbsp; Admin<p><p><p><p><p>Please do not reply to this email with your password. We will never ask for your password, and we strongly discourage you from sharing it with anyone.<p><p></body>";
            Msg.Subject = "Verification Code";
            Msg.Body = message;
            Msg.IsBodyHtml = true;

            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("info.thrithvam", "thrithvam@2015");
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            Msg = null;
            }
            else
            {
                lblError.Text = "Enter A valid Email";
            }
            }
            catch
            {
                
            }
        }
        #endregion Send Verification Code

        #endregion Methods
    }
}