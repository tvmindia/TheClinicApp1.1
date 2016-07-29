
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

        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }


        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.QueryString["ReportName"] != null)
            {
                Page.Title = Request.QueryString["ReportName"].ToString();

              //  hdfReportName.Value = Request.QueryString["ReportName"].ToString();
            }

           
            string ID = string.Empty;
            string Date = string.Empty;
            if (Request.QueryString["ID"] != null)
            {
                ID = Request.QueryString["ID"].ToString();
            }

            //if (Request.QueryString["Date"] != null && Request.QueryString["Date"].ToString() !=string.Empty)
            //{
            //    Date = Request.QueryString["Date"].ToString();
            //}

             UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            //if (!IsPostBack)
            //{
                if (ID != string.Empty)
	            {
                clinicReprtObj.ReportID = Guid.Parse(ID);



                //if (txtFromDate.Value != string.Empty)
                //{
                //    clinicReprtObj.FromDate = Convert.ToDateTime(txtFromDate.Value);
                //}

                //if (txtToDate.Value != string.Empty)
                //{
                //    clinicReprtObj.ToDate = Convert.ToDateTime(txtToDate.Value);
                //}

               
                string html = clinicReprtObj.GetReport();                               //body


                if (html != string.Empty)
                {
                    PlaceHolder1.Controls.Add(new Literal { Text = html });

                    htmlRprtObj.LogoURL = "../images/logo-small.png";                   //header
                    htmlRprtObj.Name = UA.Clinic;

                    htmlRprtObj.ReportName = clinicReprtObj.ReportName;

                    if (htmlRprtObj.LogoURL != null && htmlRprtObj.Name != null && htmlRprtObj.ReportName != null)
                    {

                        string header = htmlRprtObj.GenerateHeader();
                        PlaceHolder3.Controls.Add(new Literal { Text = header });
                    }

                    htmlRprtObj.CreatedBy = UA.userName;                                //footer

                    if (htmlRprtObj.CreatedBy != null)
                    {
                        string footer = htmlRprtObj.GenerateFooter();
                        PlaceHolder2.Controls.Add(new Literal { Text = footer });
                    }

                   
                    
                }

               
            }

           // }

           



        }

        #endregion Page Load

        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }

        #endregion Events

        #region Methods

        #endregion Methods
    }
}