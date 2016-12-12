using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class pharmacy
    {

       #region Precription

        #region Global Variables

        common cmn = new common();
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA; 

        #endregion Global Variables

        /* PATIENT DETAILS SP NAME: VIEWPATIENTDETAILS,VIEWPATIENTBOOKING */
        #region DoctorPrescriptionproperty

        public string Module = "Pharmacy";       
    
        public Guid PrescriptionID
        {
            get;
            set;
        }        
        public Guid ClinicID
        {
            get;
            set;
        }
        public Guid PatientID
        {
            get;
            set;
        }
        public Guid DoctorID
        {
            get;
            set;
        }
    
        #endregion DoctorPrescriptionproperty

        #region Methods

        #region PHARMACY View Search Paging

        public string ViewAndFilterPatientsInPharmacy(string searchTerm, int pageIndex, int PageSize)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;

            var xml = string.Empty;
            try
            {
                DateTime now = DateTime.Now;

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ViewAndFilterPatientsInPharmacy";

                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value =cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode; 

                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Pharmacy");

                //-----------Paging Section 

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = pageIndex;
                dt.Rows[0]["PageSize"] = PageSize;
                dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                ds.Tables.Add(dt);

                xml = ds.GetXml();


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterPatientsInPharmacy";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

            return xml;
        }

        #endregion PHARMACY View Search Paging




        #region GetPatientPharmacyDetails

        public DataSet GetPatientPharmacyDetails()
        {

            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                DateTime now = DateTime.Now;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetPatientPharmacyDetails]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetPatientPharmacyDetails";
                eObj.InsertError();
            }
            finally
            {              
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return ds;

        }

        #endregion GetPatientPharmacyDetails



        #region GetPatientPharmacyDetailsByID

        public DataSet GetPatientPharmacyDetailsByID()
        {

            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                DateTime now = DateTime.Now;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetPatientPharmacyDetailsByID]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetPatientPharmacyDetailsByID";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return ds;

        }

        #endregion GetPatientPharmacyDetails

        #region PrescriptionDetails

        public DataSet PrescriptionDetails()
        {

            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[PrescriptionDetails]";

                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                // PrescriptionDT.PrescID,MedicineID,Qty,Unit,Dosage,Timing,Days will be diplayed

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Medicines");
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "PrescriptionDetails";
                eObj.InsertError();
               
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;
        }

        #endregion PrescriptionDetails
        
        #endregion Methods

        #endregion Precription

    }
}