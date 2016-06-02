
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

        #endregion Global Variables

        #region Public Properties

        public Guid PatientID
        {
            get;
            set;
        }
        public string ReportName
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

        public string AddColumn
        {
            get;
            set;
        }

        public string RemoveColumn
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Methods

        #region Get Data To Be Reported

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

                if (ReportName == "AllRegisteredPatients")
                {
                    cmd = new SqlCommand("ViewAllRegistration", con);
                   
                }
                else if (ReportName == "PatientByID")
                {
                    cmd = new SqlCommand("GetPatientDetailsByPatientID", con);
                    cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                }

                else if (ReportName == "OutOfStock")
                {
                     cmd = new SqlCommand("ViewOutofStockMedicines", con); 
                }

                else if (ReportName == "Stock")
                {
                    cmd = new SqlCommand("ViewMedicines", con); 
                }

                else if (ReportName == "Transaction")
                {
                    //cmd = new SqlCommand("ViewMedicines", con); 
                }

                //cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = FromDate;
                //cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate;

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();

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

        #region Set SP Based On Report Name

        /// <summary>
        /// Set stored procedure based on report name
        /// </summary>
        public void SetSPBasedOnReportName()
        {

        }

        #endregion Set SP Based On Report Name


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


        #endregion Methods

    }
}