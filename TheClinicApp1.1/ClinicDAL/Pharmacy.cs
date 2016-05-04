using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace TheClinicApp1._1.ClinicDAL
{
    public class pharmacy
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


        #endregion PrescriptionpropertyDoctorInsert
        #endregion DoctorPrescription


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
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");

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
                sda.Fill(ds,"Medicines");


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