
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class MailSending
    {
        #region Global Variables
        public string Email
        {
            get;
            set;
        }

        public string msg
        {
            get;
            set;
        }

        #endregion Global Variables

        #region Public Variables

        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        string VerificationCode = System.Web.Configuration.WebConfigurationManager.AppSettings["VerificationCode"];
        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];

        #endregion   Public Variables

        #region Methods

        #region SendEmail

        public void SendEmail()
        {
            MailMessage Msg = new MailMessage();

            Msg.From = new MailAddress(EmailFromAddress);

            Msg.To.Add(Email);

            string message = "<body><h3>Hello ,</h3>" + msg + "<p>Enter Your Code in given field and change your Password<p><p><p><p>&nbsp;&nbsp;&nbsp;&nbsp; ClinicApp&nbsp; Admin<p><p><p><p><p>Please do not reply to this email with your password. We will never ask for your password, and we strongly discourage you from sharing it with anyone.</body>";
            Msg.Subject = VerificationCode;
            Msg.Body = message;
            Msg.IsBodyHtml = true;

            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            Msg = null;
        }


        #endregion SendEmail

        #endregion Methods


    }
}