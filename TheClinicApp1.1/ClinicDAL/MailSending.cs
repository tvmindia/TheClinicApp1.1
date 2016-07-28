
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
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

        public string MailSubject
        {
            get;
            set;
        }

        #endregion Global Variables

        #region Public Variables

        //---* Keys assosiated with mail sending.its values are set in web.config ,app settings section -- *//

        string EmailFromAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["EmailFromAddress"];
        string host = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-host"];
        string smtpUserName = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-UserName"];
        string smtpPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SMTP-Password"];
        string VerificationCode = System.Web.Configuration.WebConfigurationManager.AppSettings["VerificationCode"];
        string port = System.Web.Configuration.WebConfigurationManager.AppSettings["Port"];

        #endregion   Public Variables

        #region Methods

        public void SendForgotPaaswordEmail()
        {
            string message = "<body><h3>Hello ,</h3>" + msg + "<p>Enter Your Code in given field and change your Password<p><p><p><p>&nbsp;&nbsp;&nbsp;&nbsp; ClinicApp&nbsp; Admin<p><p><p><p><p>Please do not reply to this email with your password. We will never ask for your password, and we strongly discourage you from sharing it with anyone.</body>";
            msg= message;
            MailSubject = VerificationCode;

            SendEmail();
        }


        #region SendEmail

        public void SendEmail()
        {
            MailMessage Msg = new MailMessage();

            Msg.From = new MailAddress(EmailFromAddress);

            Msg.To.Add(Email);

           
            Msg.Subject = MailSubject; 
            Msg.Body = msg;
            Msg.IsBodyHtml = true;

            // your remote SMTP server IP.
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = Convert.ToInt32(port);
            smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            smtp.EnableSsl = true;
            smtp.Send(Msg);
            Msg = null;
        }


        #endregion SendEmail


        public void FormatAndSendEmail(string FirstName, string Clinic,string LoginName, string Password)
        {
            string Url = "";

            Url = "Templates/unsubscribe.html";

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/" + Url)))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("User", FirstName);
            body = body.Replace("Tritvam General Clinic", Clinic);
            body = body.Replace("Name", LoginName);
            body = body.Replace("6502", Password);
           
            msg = body;
            SendEmail();
        }


        #endregion Methods


    }
}