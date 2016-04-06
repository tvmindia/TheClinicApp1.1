using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TheClinicApp.ClinicDAL
{
    public class common
    {
        #region Property

        public string ErrorID
        {
            get;
            set;

        }
        public string Description
        {
            get;
            set;

        }
        public string Module
        {
            get;
            set;

        }
        public string Method
        {
            get;
            set;

        }
        public string UserID
        {
            get;
            set;

        }
        public string EventID
        {
            get;
            set;

        }
        public string ClinicID
        {
            get;
            set;

        }
        public string EventDec
        {
            get;
            set;

        }
        public string TableName
        {
            get;
            set;

        }
        public string ParentKey
        {
            get;
            set;

        }
        public string ActionType
        {
            get;
            set;

        }
         

         
        #endregion Property


        #region Methods

        #region InsertErrorLog

        public void InsertErrorLog()
        {
            
          SqlConnection con = null;
            
            try
            {

            Guid ErrorID = new Guid();
            DateTime now = DateTime.Now;

            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertErrorLog]";

            cmd.Parameters.Add("@ErrorID", SqlDbType.UniqueIdentifier).Value = ErrorID;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = Description;
            cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd"); 
            cmd.Parameters.Add("@Module", SqlDbType.NVarChar, 255).Value = Module;
            cmd.Parameters.Add("@Method", SqlDbType.NVarChar, 255).Value = Method;
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UserID);
   
            cmd.ExecuteNonQuery();
           
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
         
        #endregion InsertErrorLog

        #region InsertEvents

        public void InsertEvents()
        {
             SqlConnection con = null;
            
            try
            {

            Guid EventID = new Guid();
            DateTime now = DateTime.Now;

            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertEvents]";

            cmd.Parameters.Add("@ErrorID", SqlDbType.UniqueIdentifier).Value = EventID;
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = EventID;
            cmd.Parameters.Add("@EventDesc", SqlDbType.NVarChar).Value = Module;
            cmd.Parameters.Add("@TableName", SqlDbType.NVarChar, 255).Value = Method;
            cmd.Parameters.Add("@ParentKey", SqlDbType.NVarChar, 255).Value = Module;
            cmd.Parameters.Add("@ActionType", SqlDbType.NVarChar, 255).Value = Method;
            cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd"); 
            cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UserID);
   
            cmd.ExecuteNonQuery();
           
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
         


       

        #endregion InsertEvents

       
        #endregion Methods
    }
}