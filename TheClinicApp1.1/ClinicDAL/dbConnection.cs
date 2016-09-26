using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class dbConnection
    {
        #region Global variables

        public SqlConnection SQLCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ClinicAppConnectionString"].ConnectionString);
        public SqlTransaction DBTrans;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global variables

        public int ConnectDB()
        {
            try
            {
                if (SQLCon.State == ConnectionState.Closed)
                {

                    SQLCon.Open();
                }
              
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
            }
            return 1;
        }

        public SqlConnection GetDBConnection()
        {
            try
            {
                if (SQLCon.State == ConnectionState.Closed)
                {

                    SQLCon.Open();
                }
                return SQLCon;
            }
            catch (Exception ex)
            {
                try
                {
                    HttpContext.Current.Response.Redirect("../underConstruction.aspx?cause=dbDown", true);
                }
                catch (Exception)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.ErrorData(ex, page);
                }

                return null;

            }
        }

        public int DisconectDB()
        {
            try
            {
                if (SQLCon.State == ConnectionState.Open)
                {
                    SQLCon.Close();
                }
               

            }
            catch (Exception)
            {
                throw;
            }

            return 1;
        }
    }
}