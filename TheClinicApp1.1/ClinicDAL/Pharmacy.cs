using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace TheClinicApp.ClinicDAL
{
    public class Pharmacy
    {
       #region Precription

        /* PATIENT DETAILS SP NAME: VIEWPATIENTDETAILS,VIEWPATIENTBOOKING
         * 
         */
        #region DoctorPrescription
        #region PrescriptionpropertyDoctorInsert
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



        #endregion PrescriptionpropertyDoctorInsert
        #endregion DoctorPrescription
        #region PhramacyPatientDetails

        public DataSet PhramacyPatientDetails(Guid PatientID)
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
                cmd.CommandText = "[PharmacyPatientDetails]";

                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion PhramacyPatientDetails

        #region PrescriptionDetails

        public DataSet PrescriptionDetails(Guid PatientID)
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
                
                // PrescriptionDT.PrescID,MedicineID,Qty,Unit,Dosage,Timing,Days will be diplayed

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion PrescriptionDetails

        #endregion Precription

    }
}