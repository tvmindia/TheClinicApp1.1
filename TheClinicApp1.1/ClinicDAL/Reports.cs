
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
        common cmn = new common();

        #endregion Global Variables

        #region Public Properties

        public List<string> ClinicColumns = new List<string>();

        //public string DefaultColumns { get; set; }
        //public string CoulmnAlias { get; set; }

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

        public DateTime Date // For equality check
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

        public String DefaultCoulmns
        {
            get;
            set;
        }


        public string ColumnCaptions
        {
            get;
            set;
        }

        public string WhereCondition
        {
            get;
            set;
        }
        public string SpecifiedColumns
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
        public string GetReport( DataTable dt)
        {
            string html = string.Empty;

          //  DataTable dt = GetDataToBeReported();

            if (WhereCondition != null & WhereCondition != string.Empty)
            {
                //if (WhereCondition.Contains("OR"))
                //{
                //    var Seperator = new System.Text.RegularExpressions.Regex("OR");
                //    string[] individualConditions = Seperator.Split(WhereCondition);

                //    for (int i = 0; i < individualConditions.Length; i++)
                //    {
                //        var LikeSeperator = new System.Text.RegularExpressions.Regex("LIKE");
                //        string[] clmnName = LikeSeperator.Split(individualConditions[i]);

                //        for (int j = 0; j < dt.Rows.Count; j++)
                //        {
                //            if (dt.Columns[j].ColumnName == clmnName[0].ToString().Trim())
                //            {
                //                if (dt.Columns[j].DataType == typeof(string))
                //                {
                //                    var getType = dt.Columns[i].DataType;

                //                    object propvalue = Convert.ChangeType(clmnName[1], getType);
                //                }
                //            }
                //        }

                //    }

                //}

                //


                 DataRow[] FilteredRow = dt.Select(WhereCondition);
                 DataTable filteredTable = dt.Clone();

                 //foreach (string str in filter)
                 //{
                 //    DataRow[] filteredRows = dt.Select("categoryid=" + str); //search for categoryID
                 //    foreach (DataRow dtr in filteredRows)
                 //    {
                 //        filteredTable.ImportRow(dtr);
                 //    }

                 //}

                

                 foreach (DataRow dr in FilteredRow)
                 {
                     filteredTable.ImportRow(dr);
                    
                 }
                 dt = filteredTable;

            }


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

                    HtmlReprtObj.Columns.Add("Name", 0);
                    HtmlReprtObj.Columns.Add("Address", 0);
                    HtmlReprtObj.Columns.Add("Phone", 0);
                    HtmlReprtObj.Columns.Add("Age", 2);
                    HtmlReprtObj.Columns.Add("Email", 0);
                    HtmlReprtObj.Columns.Add("Gender", 0);
                    HtmlReprtObj.Columns.Add("Marital Status", 0);
                    HtmlReprtObj.Columns.Add("Occupation", 0);

                    SpecifiedColumns = "Name" + "," + "Address" + "," + "Phone" + "," + "Email" + "," + "Occupation";


                }
                else if (ReportCode == 1003)                                    //Individual Patient //NOT using now
                {
                    cmd = new SqlCommand("ReportPatientByID", con);
                    cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;

                    HtmlReprtObj.Columns.Add("Name",0);
                    HtmlReprtObj.Columns.Add("Address",0);
                    HtmlReprtObj.Columns.Add("Phone",0);
                    //HtmlReprtObj.Columns.Add("DOB");
                    HtmlReprtObj.Columns.Add("Email",0);
                    HtmlReprtObj.Columns.Add("Gender",0);
                    HtmlReprtObj.Columns.Add("MaritalStatus",0);
                    HtmlReprtObj.Columns.Add("Occupation",0);

                    SpecifiedColumns = "Name" + "," + "Address" + "," + "Phone" + "," + "Email" +","+  "Occupation";

                }

                else if (ReportCode == 1002)                           //Out of stock
                {
                    cmd = new SqlCommand("ReportOutOfStockMedicine", con);

                    HtmlReprtObj.Columns.Add("Name", 0);
                    HtmlReprtObj.Columns.Add("Code", 10);
                    HtmlReprtObj.Columns.Add("Unit", 3);
                    HtmlReprtObj.Columns.Add("Qty", 1);
                    HtmlReprtObj.Columns.Add("Category", 0);
                    HtmlReprtObj.Columns.Add("ReOrder Qty", 1);

                    SpecifiedColumns = "Name" + "," + "Code" + "," + "Unit" + "," + "Category" ;


                }

                else if (ReportCode == 1000)                //stock
                {
                    cmd = new SqlCommand("ReportMedicines", con);

                    HtmlReprtObj.Columns.Add("Name", 0);
                    HtmlReprtObj.Columns.Add("Code", 10);
                    HtmlReprtObj.Columns.Add("Unit", 3);
                    HtmlReprtObj.Columns.Add("Qty", 1);
                    HtmlReprtObj.Columns.Add("Category", 0);
                    HtmlReprtObj.Columns.Add("ReOrder Qty", 1);

                    SpecifiedColumns = "Name" + "," + "Code" + "," + "Unit" + "," + "Category";
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

                    if (FromDate != null && FromDate != DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value =  FromDate.ToString("yyyy-MM-dd"); 
                    }

                    if (ToDate != null && ToDate != DateTime.MinValue)
                    {
                        cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = ToDate.ToString("yyyy-MM-dd"); 
                    }

                    //if (Date != null && Date != DateTime.MinValue)
                    //{
                    //     cmd.Parameters.Add("@Date", SqlDbType.Date).Value = Date.ToString("yyyy-MM-dd");;
                    //}

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    dt = new DataTable();

                    adapter.Fill(dt);


                    //foreach (DataColumn column in dt.Columns)
                    //{
                    //    column.ColumnName = column.ColumnName.Replace("_", " ");
                    //}

                    //dt.AcceptChanges();
                       

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
                DefaultCoulmns = dt.Rows[0]["ColumnNames"].ToString();
                ColumnCaptions = dt.Rows[0]["ColumnCaptions"].ToString();
            }

        }

        #endregion Set Report Name Based On Report ID

        #endregion Methods

    }
}