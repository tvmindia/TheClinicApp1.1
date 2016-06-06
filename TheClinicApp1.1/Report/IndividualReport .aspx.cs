﻿
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion Included Namespaces

namespace TheClinicApp1._1.Report
{
    public partial class IndividualReport : System.Web.UI.Page
    {
        #region Global Variables

        Reports clinicReprtObj = new Reports();
        HTMLReports htmlRprtObj = new HTMLReports();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;


        #endregion Global Variables

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = string.Empty;

            if (Request.QueryString["ID"] != null)
            {
                ID = Request.QueryString["ID"].ToString();
            }

           

             UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (!IsPostBack)
            {
                clinicReprtObj.ReportID = Guid.Parse(ID);

                string html = clinicReprtObj.GetReport();

                PlaceHolder1.Controls.Add(new Literal { Text = html });

                htmlRprtObj.LogoURL = "../images/logo-small.png";
                htmlRprtObj.Name = UA.Clinic;
                htmlRprtObj.ReportName = clinicReprtObj.ReportName;
                 string header =   htmlRprtObj.GenerateHeader();
                 PlaceHolder3.Controls.Add(new Literal { Text = header });


                htmlRprtObj.CreatedBy = UA.userName;
                string footer =   htmlRprtObj.GenerateFooter();
                PlaceHolder2.Controls.Add(new Literal { Text = footer });


            }

        }

        #endregion Page Load

        #endregion Events

        #region Methods

        #endregion Methods
    }
}