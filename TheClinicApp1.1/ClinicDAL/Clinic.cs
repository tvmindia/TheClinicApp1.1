using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace TheClinicApp.ClinicDAL
{
    public class Clinic
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        #endregion Global Variables

        #region constructor

        public Clinic()
        {

            ClinicID = Guid.NewGuid();

        }
        public Clinic(Guid ClinicID1)
        {
            ClinicID = ClinicID1;
            
        }

        #endregion constructor

        #region Property
        public Guid ClinicID
        {
            set;
            get;
        }
        public Guid GroupID
        {
            set;
            get;
        }
        public string Name
        {
            set;
            get;
        }
        public string Location
        {
            set;
            get;
        }
        public string Address
        {
            set;
            get;
        }
        public string Phone
        {
            set;
            get;
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


        #region Method

        #region InsertClinics
        public void InsertClinics()
        {


            dbConnection dcon = null;

            try
            {




                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@GroupID", SqlDbType.UniqueIdentifier).Value = GroupID;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = Location;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = Phone;

                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    //not successfull   

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionNotSuccessMessage(page);


                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionSuccessMessage(page);


                }


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



        #endregion InsertClinics

        #region UpdateClinics
        public void UpdateClinics()
        {


            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =ClinicID;
              
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = Location;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = Phone;

              
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;



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



        #endregion UpdateClinics

        #region DeleteClinics
        public void DeleteClinics()
        {


            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =  ClinicID;


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



        #endregion DeleteClinics

        #region ViewClinic
        public DataSet ViewClinic(string ClinicID)
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
                cmd.CommandText = "[ViewClinic]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =ClinicID;

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

        #endregion ViewClinic


        #endregion Method



    }
}