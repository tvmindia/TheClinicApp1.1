using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace TheClinicApp.ClinicDAL
{
    public class Master
    {
        #region Constructors
        public Master()
        {
            GroupID = Guid.NewGuid();
            ClinicID = Guid.NewGuid();
            DoctorID = Guid.NewGuid();
        }
        #endregion Constructors

        #region Global variables

        ErrorHandling eObj = new ErrorHandling();

        public string loginName
        {
            get;
            set;
        }

        public string createdBy
        {
            get;
            set;
        }

        public string updatedBy
        {
            get;
            set;
        }

        #region Connectionstring
        dbConnection dcon = new dbConnection();
        #endregion Connectionstring

        #region Groupproperty
        public Guid GroupID
        {
            get;
            set;
        }
        public string GroupName
        {
            get;
            set;
        }
        public object Logo
        {
            get;
            set;
        }
        #endregion Groupproperty

        #region ClinicProperty
        public Guid ClinicID
        {
            get;
            set;
        }
        public string ClinicName
        {
            get;
            set;
        }
        public string ClinicAddress
        {
            get;
            set;
        }
        public string ClinicLocation
        {
            get;
            set;
        }
        public string ClinicPhone
        {
            get;
            set;
        }

        #endregion ClinicProperty

        #region DoctorProperty
        public Guid DoctorID
        {
            get;
            set;
        }
        public String DoctorName
        {
            get;
            set;
        }
        public String DoctorPhone
        {
            get;
            set;
        }
        public String DoctorEmail
        {
            get;
            set;
        }
        #endregion DoctorsProperty

        #region CategoryProperty
        public Guid CategoryID
        {
            get;
            set;
        }
        public String CategoryName
        {
            get;
            set;
        }
        #endregion CategoryProperty

        #region unitProperty
        public Guid UnitID
        {
            get;
            set;
        }
        public String Code
        {
            get;
            set;
        }
        public String Description
        {
            get;
            set;
        }
        #endregion unitProperty

        #endregion Global variables

        #region Methods
        #region AddGroups
        public void InsertGroups()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertGroup]";
                pud.Parameters.Add("@GroupID", SqlDbType.UniqueIdentifier).Value = GroupID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = GroupName;
                pud.Parameters.Add("@Logo", SqlDbType.Image, 0).Value = Logo;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion AddGroups

        #region AddClinics
        public void InsertClinics()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertClinics]";
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@GroupID", SqlDbType.UniqueIdentifier).Value = GroupID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = ClinicName;
                pud.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = ClinicLocation;
                pud.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = ClinicAddress;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = ClinicPhone;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }


        }
        #endregion AddClinics

        #region AddDoctors
        public void InsertDoctors()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertDoctors]";
                //pud.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                //pud.Parameters.Add("@GroupID", SqlDbType.UniqueIdentifier).Value = GroupID;
                //pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                //pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;

                //pud.Parameters.Add("@LoginName", SqlDbType.NVarChar, 255).Value = loginName;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = DoctorName;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = DoctorPhone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = DoctorEmail;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }


        }
        #endregion AddDoctors

        #region AddCategoryID
        public void InsertCategories()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertCategories]";
                pud.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = CategoryName;
                pud.Parameters.Add("@ClinicID", SqlDbType.VarBinary, 255).Value = ClinicID;
                pud.Parameters.Add("@CreatedBY", SqlDbType.DateTime).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion AddCategories

        #region AddUnit
        public void InsertUnits()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertUnits]";
                pud.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;
                pud.Parameters.Add("@Code", SqlDbType.NVarChar, 255).Value = Code;
                pud.Parameters.Add("@Description", SqlDbType.VarBinary, 255).Value = Description;
                pud.Parameters.Add("@ClinicID", SqlDbType.VarBinary, 255).Value = ClinicID;
                pud.Parameters.Add("@CreatedBY", SqlDbType.DateTime).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion AddUnits

        #region BindGroupName
        public DataTable BindGroupName()
        {
            DataTable datatableGroup = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            try
            {
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("[SelectGroups]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                datatableGroup = new DataTable();
                adapter.Fill(datatableGroup);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return datatableGroup;
        }
        #endregion BindGroupName

        #region BindClinicName
        public DataTable GetAllClinics()
        {
            SqlConnection conn = null;
            DataTable ds = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            dbConnection dcon = new dbConnection();
            try
            {
                conn = dcon.GetDBConnection();
                //conn.Open();

                cmd = new SqlCommand("[SelectClinics]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataTable();
                da.Fill(ds);

                conn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }

        }
        #endregion BindClinicName
        #endregion Methods
    }
}