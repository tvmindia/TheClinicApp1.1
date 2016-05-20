using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class PrescriptionHeaderDetails
    {

        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();

        string Module = "Prescription";

        #endregion Global Variables

        #region Property

        public string PrescID
        {
            get;
            set;

        }
        public string VisitID
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public Guid DoctorID
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
       
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get;
            set;
        }

        /// <summary>
        /// User id of logined user
        /// </summary>
        public Guid usrid
        {
            get;
            set;
        }

                
        #endregion Property
        
        #region Methods

        #region InsertPrescriptionHeaderDetails
        public void InsertPrescriptionHeaderDetails()
        {
          dbConnection dcon = null;
            
            try
            {

            DateTime now = DateTime.Now;
            dcon = new dbConnection();
            dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dcon.SQLCon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertPrescriptionHeaderDetails]";

            cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
            cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
            cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(VisitID);
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
          
            cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value =UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;

           cmd.ExecuteNonQuery();
           
             }

            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
                eObj.Method = "InsertPrescriptionHeaderDetails";

                eObj.InsertError();

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }      
                
        #endregion InsertPrescriptionHeaderDetails

        #region UpdatePrescriptionHeaderDetails
        public void UpdatePrescriptionHeaderDetails(string PrescID)
        {

            dbConnection dcon = null;

            try
            {

                DateTime now = DateTime.Now;
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdatePrescriptionHeaderDetails]";

                cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;


                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
                eObj.Method = "UpdatePrescriptionHeaderDetails";

                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion UpdatePrescriptionHeaderDetails

        #region ViewPrescriptionHeaderDetails

        public DataSet  ViewPrescriptionHeaderDetails()
        {

            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
 
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ViewPrescriptionHeaderDetails]";

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


            }

            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
                eObj.Method = "UpdatePrescriptionHeaderDetails";

                eObj.InsertError();

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

            return ds;


        }


        #endregion ViewPrescriptionHeaderDetails

        #region DeletePrescriptionDetails

        public void DeletePrescriptionDetails( string PrescID)
        {

            dbConnection dcon = null;

            try
            {


                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeletePrescriptionHeaderDetails]";

                cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
                eObj.Method = "DeletePrescriptionDetails";

                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion DeletePrescriptionDetails

        #endregion Methods

    }
    public class PrescriptionDetails
    {
        #region Global Variables

        string Module = "PrescriptionDetails";
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        public PrescriptionDetails()
            {
                UniqueID = Guid.NewGuid();
            }

        #region Property

        public Guid UniqueID
        {
            get;
            set;
        }
         public string PrescID
        {
            get;
            set;
        }
         public string MedicineName
        {
            get;
            set;
        }
         public string ClinicID
        {
            get;
            set;
        }
         public int Qty
        {
            get;
            set;
        }
         public string Unit
         {
             get;
             set;
         }
         public string Dosage
         {
             get;
             set;
         }
         public string Timing
         {
             get;
             set;
         }
         public string Days
         {
             get;
             set;
         }
         public string CreatedBy
         {
             get;
             set;
         }
         public DateTime CreatedDate
         {
             get;
             set;
         }
         public string UpdatedBy
         {
             get;
             set;
         }
         public DateTime UpdatedDate
         {
             get;
             set;
         }

         /// <summary>
         /// User id of logined user
         /// </summary>
         public Guid usrid
         {
             get;
             set;
         }

        #endregion Property     
        
        #region Methods
        
        #region InsertPrescriptionDetails

         public void InsertPrescriptionDetails()
         {

             dbConnection  dcon = null;

             try
             {

                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[InsertMedicinestoPrescriptionHD]";               
                 cmd.Parameters.Add("@PrescriptionID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                 cmd.Parameters.Add("@MedicineName",SqlDbType.NVarChar,255).Value = MedicineName;               
                 cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);                 
                 cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                 cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 20).Value = Unit;
                 cmd.Parameters.Add("@Dosage", SqlDbType.NVarChar, 20).Value = Dosage;
                 cmd.Parameters.Add("@Timing", SqlDbType.NVarChar, 20).Value = Timing;
                 cmd.Parameters.Add("@Days", SqlDbType.NVarChar, 20).Value = Days;           
                 cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                 cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                 cmd.ExecuteNonQuery();
             }

             catch (Exception ex)
             {
                 eObj.Description = ex.Message;
                 eObj.Module = Module;

                 eObj.UserID = usrid;
                 eObj.Method = "InsertPrescriptionDetails";

                 eObj.InsertError();
             }

             finally
             {
                 if (dcon.SQLCon != null)
                 {
                     dcon.DisconectDB();
                 }

             }

         }      


        #endregion InsertPrescriptionDetails

        #region ViewPrescriptionDetails

         public DataSet ViewPrescriptionDetails(string PresicID)
         {

             dbConnection  dcon = null;
             DataSet ds = null;
             SqlDataAdapter sda = null;
             try
             {

                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[ViewPrescriptionDetails]";
                 cmd.Parameters.Add("@PrescriptionID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PresicID);
                 sda = new SqlDataAdapter();
                 cmd.ExecuteNonQuery();
                 sda.SelectCommand = cmd;
                 ds = new DataSet();
                 sda.Fill(ds,"Medicines");
              

             }

             catch (Exception ex)
             {
                 eObj.Description = ex.Message;
                 eObj.Module = Module;

                 eObj.UserID = usrid;
                 eObj.Method = "ViewPrescriptionDetails";

                 eObj.InsertError();
             }

             finally
             {
                 if (dcon.SQLCon != null)
                 {
                     dcon.DisconectDB();
                 }

             }
             return ds;
         }
                
        #endregion ViewPrescriptionDetails

        #region UpdatePrescriptionDetails

         public void UpdatePrescriptionDetails(string UniqueID)
         {
             dbConnection dcon = null;

             try
             {
                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[UpdatePrescriptionDetails]";

                 cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                 cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar,255).Value = MedicineName;

                 cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                 cmd.Parameters.Add("@Unit", SqlDbType.NVarChar,20).Value = Unit;
                 cmd.Parameters.Add("@Dosage", SqlDbType.NVarChar, 20).Value = Dosage;
                 cmd.Parameters.Add("@Timing", SqlDbType.NVarChar, 20).Value = Timing;
                 cmd.Parameters.Add("@Days", SqlDbType.NVarChar, 20).Value = Days;
                 cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;

                 cmd.ExecuteNonQuery();

             }

             catch (Exception ex)
             {
                 eObj.Description = ex.Message;
                 eObj.Module = Module;

                 eObj.UserID = usrid;
                 eObj.Method = "UpdatePrescriptionDetails";

                 eObj.InsertError();
             }

             finally
             {
                 if (dcon.SQLCon != null)
                 {
                     dcon.DisconectDB();
                 }

             }

         }


        #endregion UpdatePrescriptionDetails

        #region DeletePrescriptionDetails

         public void DeletePrescriptionDetails( string UniqueID)
         {
             dbConnection dcon = null;

             try
             {
                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[DeletePrescriptionDetails]";

                 cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                               
                 cmd.ExecuteNonQuery();

             }

             catch (Exception ex)
             {
                 eObj.Description = ex.Message;
                 eObj.Module = Module;

                 eObj.UserID = usrid;
                 eObj.Method = "DeletePrescriptionDetails";

                 eObj.InsertError();
             }

             finally
             {
                 if (dcon.SQLCon != null)
                 {
                     dcon.DisconectDB();
                 }

             }

         }

        #endregion DeletePrescriptionDetails

        #region SearchMedicinewithCategory
         public DataTable SearchMedicinewithCategory()
         {

             DataTable dt = null;
             SqlConnection con = null;
             dbConnection dcon = new dbConnection();

             try
             {
                 con = dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand("SearchMedicinewithCategory", con);
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

                 eObj.UserID = usrid;
                 eObj.Method = "SearchMedicinewithCategory"; 

                 eObj.InsertError();
             }

            
             return dt;

         }
         #endregion SearchMedicinewithCategory

        #endregion Methods

    }
}