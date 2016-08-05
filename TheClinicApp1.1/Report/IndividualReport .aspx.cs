
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

        #region Bind Report

        /// <summary>
        /// Report ID and Name are passed via query string
        /// Specified columns are binded to coulmns dropdown
        /// Get The report content by reportID
        /// Returned html are set to corresponding placeholders
        /// </summary>
        public void BindReport()
        {
                if (Request.QueryString["ReportName"] != null)
                {
                    Page.Title = Request.QueryString["ReportName"].ToString();

                }

                string ID = string.Empty;
                string Date = string.Empty;
                if (Request.QueryString["ID"] != null)
                {
                    ID = Request.QueryString["ID"].ToString();
                }

                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

                if (ID != string.Empty)
                {
                    clinicReprtObj.ReportID = Guid.Parse(ID);

                    DataTable dt = clinicReprtObj.GetDataToBeReported();
                    string SpecifiedColumns = clinicReprtObj.SpecifiedColumns;

                    if (!IsPostBack)
                    {
                        BindCoulmnNameDropdown();
                    }

                    string html = clinicReprtObj.GetReport(dt);                               //body
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
        }

        #endregion Bind Report

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }

        }

        #endregion Page Load

        #region Search Button Click
        /// <summary>
        /// Where Condition is passed to class from here
        /// Hiddenfield contains array of conditions
        /// Array items retrieved by comma, and replace comma by OR , to build where condition ,which joins the conditions by OR 
        /// Some times search value may contain comma (eg:address ), Then value is passed from client side by replacing comma by # symbol
        /// If # symbol is there , it will be replaced by commas 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (hdnWhereConditions.Value != string.Empty)
            {
                if (hdnWhereConditions.Value.Contains(","))
                {
                    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace(",", " OR ");
                }

                if (hdnWhereConditions.Value.Contains("#"))
                {
                    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace("#", ",");
                }

            }

            clinicReprtObj.WhereCondition = hdnWhereConditions.Value.ToString();
            BindReport();
            ClientScript.RegisterStartupScript(GetType(), "id", "MakeListUsingArray()", true);

            //------ * Clearing Controls * ------//
            txtvalue.Text = string.Empty;
            BindCoulmnNameDropdown();

            //   hdnArray.Value = "";
        }

        #endregion Search Button Click

        #region Refresh Button Click
        protected void imgbtnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            BindReport();
           
            //------ * Clearing Controls * ------//
            txtvalue.Text = string.Empty;
            BindCoulmnNameDropdown();

        }

        #endregion Refresh Button Click

        #endregion Events

        #region Methods

        #region Bind Dropdown With Column Names 

        public void BindCoulmnNameDropdown()
        {
          DataTable dt =  clinicReprtObj.GetReportDetailsByReportID();

          if (dt.Rows.Count > 0)
          {
          string    DefaultCoulmns = dt.Rows[0]["ColumnNames"].ToString();
           string   ColumnCaptions = dt.Rows[0]["ColumnCaptions"].ToString();

       string[] dfultColmns = DefaultCoulmns.Split(',');
       var dfltClmArray = dfultColmns.ToArray();

       string[] Captions = ColumnCaptions.Split(',');
       var CaptionArray = Captions.ToArray();

       Dictionary<string, string> Coulmns = new Dictionary<string, string>();
       
       for (int i = 0; i < dfltClmArray.Length; i++)
       {

           if (clinicReprtObj.SpecifiedColumns != null)
           {

         string[] SpecifiedClmns =  clinicReprtObj.SpecifiedColumns.Split(',');

         foreach (var item in SpecifiedClmns)
         {

             if (CaptionArray[i].ToString() == item.ToString())
             {
                 Coulmns.Add(dfltClmArray[i].ToString(), CaptionArray[i].ToString());  
             }

         }
           }

           else
           {
               Coulmns.Add(dfltClmArray[i].ToString(), CaptionArray[i].ToString());  
           }



      }
       if (Coulmns != null)
       {
           ddlColumns.DataSource = Coulmns;
           ddlColumns.DataTextField = "Value";
           ddlColumns.DataValueField = "Key";
           ddlColumns.DataBind();

           ddlColumns.Items.Insert(0, "--Select--");
       }

       
          }

        }

        #endregion Bind Dropdown With Column Names

        #endregion Methods

    }
}