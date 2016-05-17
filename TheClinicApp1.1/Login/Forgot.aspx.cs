using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Login
{
    public partial class Forgot : System.Web.UI.Page
    {
        #region GlobalVariables
        ClinicDAL.User userObj = new ClinicDAL.User();
        #endregion GlobalVariables

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion PageLoad

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
            try
            {
                 //----------*Add verification code*------------//
            Random random = new Random();
            int verificationCode = random.Next(1000, 10000);

            userObj.verificationCode = verificationCode.ToString();
            userObj.Email = txtEmail.Value;
            if(txtEmail.Value!="")
            { 
            userObj.AddVerificationCode();

            //----------*Get verification code*------------//
            userObj.Email = txtEmail.Value;
            DataTable dtCode = userObj.GetUserVerificationCodeByEmailID();
            verificationCode = Convert.ToInt32(dtCode.Rows[0]["VerificationCode"]);
            DateTime vcCreatedTime = Convert.ToDateTime(dtCode.Rows[0]["VerificatinCreatedTime"]);
            string username = (dtCode.Rows[0]["LoginName"]).ToString();
            DateTime CurrentTime = DateTime.Now;
            MailMessage Msg = new MailMessage();

            // Sender e-mail address.
            Msg.From = new MailAddress("info.thrithvam@gmail.com");

            // Recipient e-mail address.
            Msg.To.Add(txtEmail.Value);

            string body = verificationCode.ToString();
            string message = "<body><p><p>&nbsp;&nbsp;<h3>Hello&nbsp;<font color='blue'>" + username + "</font>,</h3><p>&nbsp; Here is your 4 digit verification Code for Security purposes<p>&nbsp;Enter Your Code in given field and change your Password<p><h1> Verification Code is&nbsp;&nbsp;<font color='red'>" + body + "</font></h1><p><p><p>&nbsp;&nbsp;&nbsp;&nbsp; ClinicApp&nbsp; Admin<p><p><p><p><p>Please do not reply to this email with your password. We will never ask for your password, and we strongly discourage you from sharing it with anyone.<p><p></body>";
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