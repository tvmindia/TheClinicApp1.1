using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Messages = TheClinicApp.UIClasses.Messages;

namespace TheClinicApp1._1.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

                if (username.Value.ToString().Trim() != "")
                {
                    TheClinicApp.UIClasses.Const constants = new TheClinicApp.UIClasses.Const();

                    //if (username.Value != password.Value)
                    //{
                    //    password.Value = Encrypt(password.Value);
                    //}

                    //ClinicDAL.UserAuthendication UA = new ClinicDAL.UserAuthendication(username.Value, password.Value);

                    TheClinicApp.ClinicDAL.UserAuthendication UA = new TheClinicApp.ClinicDAL.UserAuthendication(username.Value, password.Value);

                    if (UA.ValidUser)
                    {
                        if (Session[constants.LoginSession] != null)
                        {
                            Session.Remove(constants.LoginSession);
                        }

                        Session.Add(constants.LoginSession, UA);
                        Response.Redirect(constants.HomePage);

                    }



                }
                else
                {

                    lblmsg.Text = Messages.LoginFailed;
                }


            }
        }
    }
}