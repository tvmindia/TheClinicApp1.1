
#region CopyRight

//Author      : SHAMILA T P
//Created Date: Feb-26-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

#endregion Included Namespaces

namespace TheClinicApp.ClinicDAL
{
    public class User
    {

        #region Global Variables
       
        ErrorHandling eObj = new ErrorHandling();
        dbConnection dcon = new dbConnection();


        private Guid Clinic_ID;

        public Guid ClinicID
        {
            get;
            set;
        }

        public Guid UserID
        {
            get;
            set;

        }
        public string loginName
        {
            get;
            set;
         }

        public string firstName
        {
            get;
            set;
        }

        public string lastName
        {
            get;
            set;
        }

        public bool isActive
        {
            get;
            set;
        }

        public string updatedBy
        {
            get;
            set;
        }

        public DateTime updatedDate
        {
            get;
            set;
        }

        public string createdBy
        {
            get;
            set;
        }

        public DateTime createdDate
        {
            get;
            set;
        }

        public string passWord
        {
            get;
            set;
        }

        public string verificationCode
        {
            get;
            set;
        }

        public string Email
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
        #endregion View All Users

        #region AddUser
        public void AddUser()
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[InsertUsers]";
               
                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar,255).Value = loginName;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 255).Value = lastName;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = isActive;


                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;
                cmd.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar,40).Value = passWord;
               
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
        #endregion AddUsers

        #region Update User By UserID

        public void UpdateuserByUserID()
        {
            dbConnection dcon = new dbConnection();

            try
            {


                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateUserByUserID]";

                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;
                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 255).Value = loginName;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 255).Value = lastName;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = passWord;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = isActive;


                //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = "2C7A7172-6EA9-4640-B7D2-0C329336F289";
                //cmd.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;
                cmd.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;


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
                    eObj.UpdationNotSuccessMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.UpdationSuccessMessage(page);

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

        #endregion Update User By UserID

        #region Delete User By UserID

        public void DeleteUserByUserID()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUserByUserID";
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
                    eObj.WarningMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.DeleteSuccessMessage(page);


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

        #endregion Delete User By UserID

        #region ValidateUsername
        public bool ValidateUsername(string CheckUser)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckLoginName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@loginName", SqlDbType.VarChar, 50).Value = CheckUser;
                SqlParameter outflag = cmd.Parameters.Add("@flag", SqlDbType.Bit);
                outflag.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                flag = (bool)outflag.Value;
                if (flag == true)
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            return false;
        }

        #endregion ValidateUsername

//------------*Methods Used For Forgot Password Implementation *------------//

        #region Add verificationcode (Generated random number)

        public void AddVerificationCode()
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[AddVerificationCode]";

                cmd.Parameters.Add("@VerificationCode", SqlDbType.NVarChar, 20).Value = verificationCode;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                
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

        #endregion Add verificationcode (Generated random number)
        
        #region Get User Verification Code By EmailID

        public DataTable GetUserVerificationCodeByEmailID()
        {
            SqlConnection con = null;
            DataTable dtVerificationCode = null;
            try
            {
                dtVerificationCode = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetVerificationCodeByEmailID", con);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtVerificationCode);
                return dtVerificationCode;
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

        #endregion  Get User Verification Code By EmailID

        #region Reset Password

        public void ResetPassword()
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "ResetPassword";

                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 255).Value = loginName;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = passWord;
               

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
                    eObj.UpdationNotSuccessMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.UpdationSuccessMessage(page);

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

        #endregion  Reset Password

        #endregion Methods

    }
}