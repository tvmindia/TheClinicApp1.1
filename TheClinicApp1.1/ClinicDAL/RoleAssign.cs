#region CopyRight

//Author      : SHAMILA T P
//Created Date: Feb-29-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

#endregion  Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class RoleAssign
    {
        #region Global Variables

        ErrorHandling eObj = new ErrorHandling();
       
        public Guid UniqueID
        {
            get;
            set;
        }


        public Guid UserID
        {
            get;
            set;

        }


        public Guid RoleID
        {
            get;
            set;

        }

        public Guid ClinicID
        {
            get;
            set;

        }

        public string CreatedBy
        {
            get;
            set;
        }


        #endregion Global Variables

        #region Methods

        #region View All users
        public DataTable GetDetailsOfAllUsers()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllUsers", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);
                return dtUsers;
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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
        #endregion  View All users 

        #region View All Roles

        public DataTable GetDetailsOfAllRoles()
        {
            SqlConnection con = null;
            DataTable dtRoles = null;
            try
            {
                dtRoles = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllRoles", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);
                return dtRoles;
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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

        #endregion View All Roles

        #region View Assigned Roles

        public DataTable GetDetailsOfAllAssignedRoles()
        {
            SqlConnection con = null;
            DataTable dtRoles = null;
            try
            {
                dtRoles = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllUserInRoles", con);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);
                return dtRoles;
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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

        #endregion View Assigned Roles

        #region Assign Role
        public void AssignRole()
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[InsertUserInRoles]";

                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;
                cmd.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier).Value = RoleID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@Createdby", SqlDbType.NVarChar, 255).Value = CreatedBy;


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
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);


                }


            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
        }
        #endregion Assign Role

        #region Delete Assigned Role By UniqueID

        public void DeleteAssignedRoleByUniqueID()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUserInRolesByUniqueID";
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = UniqueID;

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
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);


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

        #endregion Delete Assigned Role By UniqueID


        #region Delete Assigned Role By UserID

        public void DeleteAssignedRoleByUserID()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUserInRolesByUserID";
                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;

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
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);


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

        #endregion Delete Assigned Role By UserID


        //#region Update Assigned Role By UserID


        //public void UpdateAssignedRoleByUserID()
        //{
        //    dbConnection dcon = new dbConnection();

        //    try
        //    {

        //        dcon.GetDBConnection();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = dcon.SQLCon;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "[UpdateDoctorByName]";

        //        cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

        //        cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;
        //        cmd.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier).Value = RoleID;
        //        cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
               


        //        //cmd.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = ;

               


        //        SqlParameter Output = new SqlParameter();
        //        Output.DbType = DbType.Int32;
        //        Output.ParameterName = "@Status";
        //        Output.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(Output);
        //        cmd.ExecuteNonQuery();

        //        if (Output.Value.ToString() == "")
        //        {
        //            //not successfull   

        //            var page = HttpContext.Current.CurrentHandler as Page;
        //            eObj.SavingFailureMessage(page);
        //            //eObj.UpdationNotSuccessMessage(page);

        //        }
        //        else
        //        {
        //            //successfull

        //            var page = HttpContext.Current.CurrentHandler as Page;
        //            eObj.SavedSuccessMessage(page);
        //            //eObj.UpdationSuccessMessage(page);

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        var page = HttpContext.Current.CurrentHandler as Page;
        //        eObj.ErrorData(ex, page);

        //    }

        //    finally
        //    {
        //        if (dcon.SQLCon != null)
        //        {
        //            dcon.DisconectDB();
        //        }

        //    }
        //}


        //#endregion Update Assigned Role By UserID

        #endregion Methods
    }
}