﻿
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class Reports
    {

        #region Global Variables

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string Module = "Reports";
        HTMLReports HtmlReprtObj = new HTMLReports();

        #endregion Global Variables

        #region Public Properties

        public List<string> ClinicColumns = new List<string>();

        public string ReportName
        {
            get;
            set;
        }


        public Guid ReportID
        {
            get;
            set;
        }

        public int ReportCode
        {
            get;
            set;
        }

        public DateTime FromDate
        {
            get;
            set;
        }

        public DateTime ToDate
        {
            get;
            set;
        }


        public Guid PatientID
        {
            get;
            set;
        }

        public bool DisplaySerailNo
        {
            get;
            set;
        }
      
        #endregion Public Properties

        #region Methods


        #region Get Report

        /// <summary>
        /// To get report ,by giving a datasource and calls the function to convert it to html table
        /// </summary>
        /// <returns></returns>
        public string GetReport()
        {
            string html = string.Empty;

            DataTable dt = GetDataToBeReported();

            if (dt != null)
            {
                HtmlReprtObj.Datasource = dt;
                html = HtmlReprtObj.GenerateReport();

            }

            
             return html;
        }

        #endregion Get Report

        #region Get Data To Be Reported

        /// <summary>
        /// Set datatable to be reported
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataToBeReported()
        {
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            SqlConnection con = null;
            SqlCommand cmd    = null;
            DataTable dt      = new DataTable();
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();

                SetReportNameBasedOnReportID();

                if (ReportCode == 1001)
                {
                    cmd = new SqlCommand("ReportRegisteredPatients", con);       //All patients

                    //-------* Specify columns to be added -------*/

                    HtmlReprtObj.Columns.Add("Name");                  
                    HtmlReprtObj.Columns.Add("Address");
                    HtmlReprtObj.Columns.Add("Phone");
                    HtmlReprtObj.Columns.Add("DOB");
                    HtmlReprtObj.Columns.Add("Email");
                    HtmlReprtObj.Columns.Add("Gender");
                    HtmlReprtObj.Columns.Add("Marital Status");
                    HtmlReprtObj.Columns.Add("Occupation");


                }
                else if (ReportCode == 1003)                                    //Individual Patient //NOT using now
                {
                    cmd = new SqlCommand("ReportPatientByID", con);
                    cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;

                    HtmlReprtObj.Columns.Add("Name");
                    HtmlReprtObj.Columns.Add("Address");
                    HtmlReprtObj.Columns.Add("Phone");
                    //HtmlReprtObj.Columns.Add("DOB");
                    HtmlReprtObj.Columns.Add("Email");
                    HtmlReprtObj.Columns.Add("Gender");
                    HtmlReprtObj.Columns.Add("MaritalStatus");
                    HtmlReprtObj.Columns.Add("Occupation");
                     
                }

                else if (ReportCode == 1002)                           //Out of stock
                {
                    cmd = new SqlCommand("ReportOutOfStockMedicine", con);

                    HtmlReprtObj.Columns.Add("Name");
                    HtmlReprtObj.Columns.Add("Medicine Code");
                    HtmlReprtObj.Columns.Add("Unit");
                    HtmlReprtObj.Columns.Add("Qty");
                    HtmlReprtObj.Columns.Add("Category Name");
                    HtmlReprtObj.Columns.Add("ReOrderQty");
                    
                }

                else if (ReportCode == 1000)                //stock
                {
                    cmd = new SqlCommand("ReportMedicines", con);

                    HtmlReprtObj.Columns.Add("Name");
                    HtmlReprtObj.Columns.Add("Medicine Code");
                    HtmlReprtObj.Columns.Add("Unit");
                    HtmlReprtObj.Columns.Add("Qty");
                    HtmlReprtObj.Columns.Add("Category Name");
                    HtmlReprtObj.Columns.Add("ReOrderQty");
                }

                else if (ReportCode == 1004)       //Transaction  //NOT using now
                {
                    cmd = new SqlCommand("ViewMedicines", con); 
                }

                //cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                //cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;


                if (cmd != null)
                {
                    cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    dt = new DataTable();
                    adapter.Fill(dt);
                    con.Close();
                }

                else
                {
                    dt = null;
                }

            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetDataToBeReported";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dt;
        }
        #endregion Get Data To Be Reported

        #region Get Report List

        public DataTable GetReportList()
        {
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            SqlConnection con = null;
            SqlCommand cmd    = null;
            DataTable dtReportList      = new DataTable();
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();

                cmd = new SqlCommand("GetReportList", con);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dtReportList = new DataTable();
                adapter.Fill(dtReportList);
                con.Close();
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetReportList";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dtReportList;
        }

        #endregion Get Report List

        #region Get Report Details By ReportID

        public DataTable GetReportDetailsByReportID()
        {
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            SqlConnection con = null;
            SqlCommand cmd = null;
            DataTable dtReportList = new DataTable();
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();

                cmd = new SqlCommand("GetReportDetailsbyID", con);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;
                cmd.Parameters.Add("@ReportID", SqlDbType.UniqueIdentifier).Value = ReportID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dtReportList = new DataTable();
                adapter.Fill(dtReportList);
                con.Close();
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetReportDetailsbyID";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dtReportList;
        }

        #endregion Get Report Details By ReportID

        #region Set Report Name Based On Report ID

        public void SetReportNameBasedOnReportID()
        {
            DataTable dt = GetReportDetailsByReportID();

            if (dt.Rows.Count > 0)
            {
                ReportCode = Convert.ToInt32(dt.Rows[0]["ReportCode"].ToString());
                ReportName = dt.Rows[0]["Name"].ToString();
            }

        }

        #endregion Set Report Name Based On Report ID

        #endregion Methods

    }
}