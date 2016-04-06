using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace TheClinicApp.ClinicDAL
{
    public class PrescriptionHeaderDetails
    {
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
        public string DoctorID
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

        #region InsertPrescriptionHeaderDetails
        public void InsertPrescriptionHeaderDetails()
        {

          dbConnection dcon = null;
            
            try
            {

            Guid PrescID = new Guid();
            
            DateTime now = DateTime.Now;
            dcon = new dbConnection();
            dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dcon.SQLCon;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertPrescriptionHeaderDetails]";

            cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value =  PrescID;
            cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
            cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(VisitID);
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
          
            cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
           
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value =UpdatedBy;
            cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = UpdatedDate;

           

           cmd.ExecuteNonQuery();
           
             }

            catch (Exception ex)
            {
                 
                throw ex;
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

        //#region UpdatePrescriptionHeaderDetails
        //public void UpdatePrescriptionHeaderDetails(string PrescID)
        //{

        //    dbConnection dcon = null;

        //    try
        //    {

        //        DateTime now = DateTime.Now;
        //        dcon = new dbConnection();
        //        dcon.GetDBConnection();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = dcon.SQLCon;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "[UpdatePrescriptionHeaderDetails]";

        //        cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
        //        cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
        //        cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
        //        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
        //        cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
        //        cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
        //        cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = UpdatedDate;


        //        cmd.ExecuteNonQuery();

        //    }

        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (dcon.SQLCon != null)
        //        {
        //            dcon.DisconectDB();
        //        }

        //    }

        //}


        //#endregion UpdatePrescriptionHeaderDetails

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

                //cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                //cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = DateTime;
                //cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
               
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
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

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

                throw ex;
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
        #region Property
        
         public string UniqueID
        {
            get;
            set;
        }
         public string PrescID
        {
            get;
            set;
        }
         public string MedicineID
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
         public int  Unit
         {
             get;
             set;
         }
         public int Dosage
         {
             get;
             set;
         }
         public string Timing
         {
             get;
             set;
         }
         public int Days
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

                 Guid UniqueID = new Guid();

                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[InsertPrescriptionDetails]";

                 cmd.Parameters.Add("@UniqueID",SqlDbType.UniqueIdentifier).Value = UniqueID;

                 cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                 cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(MedicineID);               
                 cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);                 
                 cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                 cmd.Parameters.Add("@Unit", SqlDbType.Real).Value = Unit;
                 cmd.Parameters.Add("@Dosage", SqlDbType.Real).Value = Dosage;
                 cmd.Parameters.Add("@Timing", SqlDbType.NVarChar, 255).Value = Timing;
                
                 cmd.Parameters.Add("@Days", SqlDbType.Int).Value = Days;           
                 cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                 cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
                 cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                 cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = UpdatedDate;

                 cmd.ExecuteNonQuery();

             }

             catch (Exception ex)
             {

                 throw ex;
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

         public DataSet ViewPrescriptionDetails()
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

                 //
             

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
                 if (dcon.SQLCon != null)
                 {
                     dcon.DisconectDB();
                 }

             }

         }
                
        #endregion ViewPrescriptionDetails

        #region UpdatePrescriptionDetails

         public void UpdatePrescriptionDetails( string UniqueID)
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
                 cmd.Parameters.Add("@MedcineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(MedicineID);

                 cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                 cmd.Parameters.Add("@Unit", SqlDbType.Real).Value = Unit;
                 cmd.Parameters.Add("@Dosage", SqlDbType.Real).Value = Dosage;
                 cmd.Parameters.Add("@Timing", SqlDbType.NVarChar, 255).Value = Timing;
                 cmd.Parameters.Add("@Days", SqlDbType.Int).Value = Days;           
                 cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                 cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = CreatedDate;
                 cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                 cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime).Value = UpdatedDate;


                 cmd.ExecuteNonQuery();

             }

             catch (Exception ex)
             {

                 throw ex;
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

                 throw ex;
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
}