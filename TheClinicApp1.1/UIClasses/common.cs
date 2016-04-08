using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheClinicApp1._1.UIClasses
{
    public class common
    {
    }
    public class Const
    {
        //   public string LoginSession = "LoginDetails";
        public string LoginSession
        {
            get
            {
                return "LoginDetails";

            }
        }


        public string HomePage
        {
            get
            {
                return "../Registration/Patients.aspx";
            }
        }

        public string LoginPage
        {
            get
            {
                return "Default.aspx";
            }
        }

        public string HomePageURL
        {
            get
            {
                return "~/Home.aspx";
            }
        }
    }
}