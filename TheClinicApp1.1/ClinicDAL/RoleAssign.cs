﻿#region CopyRight

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

        public string Module = "Assign Role";

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        common cmn = new common();
        #endregion Global Variables

        #region Public Properties

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

        #endregion Public Properties

        #region Methods


        #region AssignedRole View Search Paging

        public string ViewAndFilterAssignedRoles(string searchTerm, int pageIndex, int PageSize)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;

            var xml = string.Empty;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ViewAllUserInRoles";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "AssignedRoles");

                //-----------Paging Section 

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = pageIndex;
                dt.Rows[0]["PageSize"] = PageSize;
                dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                ds.Tables.Add(dt);

                xml = ds.GetXml();


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterAssignedRoles";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

            return xml;
        }

        #endregion AssignedRole View Search Paging



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

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);
               
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDetailsOfAllUsers";

                eObj.InsertError();
               
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtUsers;
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
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.CommandType = CommandType.StoredProcedure;



                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);
              
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDetailsOfAllRoles";

                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtRoles;
        }

        #endregion View All Roles


        #region GetAssignedRoleByUserID

        public DataTable GetAssignedRoleByUserID()
        {
            SqlConnection con = null;
            DataTable dtRoles = null;
            try
            {
                dtRoles = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetAssignedRoleByUserID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);
               
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetAssignedRoleByUserID";

                eObj.InsertError();
               
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtRoles;
        }


        #endregion GetAssignedRoleByUserID


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

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);
               
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDetailsOfAllAssignedRoles";

                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtRoles;
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
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //not successfull   
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page;   //successfull
                    eObj.SavedSuccessMessage(page);

                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "AssignRole";

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
        #endregion Assign Role

        #region Delete Assigned Role By UserID


        public int DeleteAssignedRoleByUserIDForWM() 
        {
            int result = 0;

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

                cmd.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier).Value = RoleID;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                result = Convert.ToInt32(Output.Value.ToString());

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteAssignedRoleByUserID";

                eObj.InsertError();

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return result;
        }

        public int DeleteAssignedRoleByUserID()
        {
            int rslt = 0;
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

                cmd.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier).Value = RoleID;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();
                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //not successfull   
                    eObj.SavingFailureMessage(page);
                }
                else
                {
                    rslt = Convert.ToInt32(Output.Value.ToString());
                    var page = HttpContext.Current.CurrentHandler as Page;  //successfull
                    eObj.SavedSuccessMessage(page);

                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteAssignedRoleByUserID";

                eObj.InsertError();

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return rslt;
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