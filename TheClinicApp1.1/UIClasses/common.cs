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

        public string LoginPageURL
        {
            get
            {
                return "../Login/Login.aspx";
            }
        }

      


        public string PatientPage
        {
            get
            {
                return "Patients.aspx";
            }
        }


        public string TokenPage
        {
            get
            {
                return "Tokens.aspx";
            }
        }


        public string DoctorPage
        {
            get
            {
                return "Doctors.aspx";
            }
        }


        public string PharmacyPage
        {
            get
            {
                return "Pharmacy.aspx";
            }
        }


        public string StockPage
        {
            get
            {
                return "Stock.aspx";
            }
        }


        public string AdminPage
        {
            get
            {
                return "Admin.aspx";
            }
        }


        public string Patient
        {
            get
            {
                return "patients";
            }
        }


        public string Token
        {
            get
            {
                return "token";
            }
        }


        public string Doctor
        {
            get
            {
                return "doctor";
            }
        }


        public string Pharmacy
        {
            get
            {
                return "pharmacy";
            }
        }


        public string Stock
        {
            get
            {
                return "stock";
            }
        }


        public string Admin
        {
            get
            {
                return "admin";
            }
        }





        public string RoleDoctor
        {
            get
            {
                return "Doctor";
            }
        }

        public string RoleAdministrator
        {
            get
            {
                return "Administrator";
            }
        }

        public string RolePharmacist
        {
            get
            {
                return "pharmacist";
            }
        }

        public string AccessDenied
        {
            get
            {
                return "AccessDenied.aspx";
            }
        }

        public string AccessDeniedURL
        {
            get
            {
                return "../Login/AccessDenied.aspx";
            }
        }




        public string GetCurrentPageName(HttpRequest Request )
        {
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }


    }
}