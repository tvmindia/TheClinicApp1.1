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
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        common cmn = new common();
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
      
        #endregion Property
        
        #region Methods

        #region InsertPrescriptionHeader 
        public void InsertPrescriptionHeader()
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
            cmd.CommandText = "[InsertPrescriptionHeader]";

            cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
            cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
            cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(VisitID);
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

            cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value =UpdatedBy;
            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;

           cmd.ExecuteNonQuery();
           
             }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertPrescriptionHeader";

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
                
        #endregion InsertPrescriptionHeader 

        #region UpdatePrescriptionHeader 
        public void UpdatePrescriptionHeader(string PrescID)
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
                cmd.CommandText = "[UpdatePrescriptionHeader]";
                cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdatePrescriptionHeader";
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

        #endregion UpdatePrescriptionHeader 

        #region ViewPrescriptionHeader

        public DataSet  ViewPrescriptionHeader()
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
                cmd.CommandText = "[ViewPrescriptionHeader]";
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
                eObj.Method = "ViewPrescriptionHeader";
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

        #endregion ViewPrescriptionHeader

        #region DeletePrescriptionHeader
        public void DeletePrescriptionHeader(string PrescID)
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeletePrescriptionHeader]";
                cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeletePrescriptionHeader";
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

        #endregion DeletePrescriptionHeader

        #endregion Methods

    }
    public class PrescriptionDetails
    {
        #region Global Variables

        string Module = "PrescriptionDetails";      
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        common cmn = new common();
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
                 cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);
                 //cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                 cmd.ExecuteNonQuery();
             }

             catch (Exception ex)
             {
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
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
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
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
                cmd.Parameters.Add("@UpdatedDate",SqlDbType.DateTime).Value= cmn.ConvertDatenow(DateTime.Now);

                cmd.ExecuteNonQuery();

             }

             catch (Exception ex)
             {
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
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
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
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
                 cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                 SqlDataAdapter adapter = new SqlDataAdapter();
                 adapter.SelectCommand = cmd;
                 dt = new DataTable();
                 adapter.Fill(dt);
                 con.Close();
             }
             catch (Exception ex)
             {
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
                 eObj.Method = "SearchMedicinewithCategory"; 

                 eObj.InsertError();
             }

            
             return dt;

         }
         #endregion SearchMedicinewithCategory

        #endregion Methods

    }
}