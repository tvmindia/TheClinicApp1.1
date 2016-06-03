
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

namespace TheClinicApp1._1.ClinicDAL
{
    public class User
    {

        #region Global Variables

        public string Module = "User";
        ErrorHandling eObj = new ErrorHandling();
        dbConnection dcon = new dbConnection();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        #endregion Global Variables

        #region Public Properties

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

        public string PhoneNo
        {
            get;
            set;
        }

       

        #endregion Public Properties

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
        #endregion View All Users

        #region GetUserDetailsByLoginName
        public DataTable GetUserDetailsByLoginName()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetUserDetailsByLoginName", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@LoginName", SqlDbType.UniqueIdentifier).Value = UA.userName;

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
                eObj.Method = "GetUserDetailsByLoginName";

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
        #endregion GetUserDetailsByLoginName

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

                UserID = Guid.NewGuid();

                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;

                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar,255).Value = loginName;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = firstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 255).Value = lastName;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = isActive;


                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;
                cmd.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar,40).Value = passWord;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar, 30).Value = PhoneNo;
               
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page; //not successfull   
                    eObj.SavingFailureMessage(page);
                   
                }
                else
                {
                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 2 || rslt == 3)
                    {
                        //rslt=2 -Login name already exists , rslt=3 - Email id already exists

                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);

                    }



                    else
                    {
                        if (rslt == 1)
                        {
                            var page = HttpContext.Current.CurrentHandler as Page;  //successfull
                            eObj.SavedSuccessMessage(page);
                           
                        }
                    }
                    
                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "AddUser";

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
        #endregion AddUsers

        #region Get User Details By UserID


        public DataTable GetUserDetailsByUserID()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetUserDetailsByUserID", con);
               
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;
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

                eObj.UserID = UA.UserID; ;
                eObj.Method = "GetUserDetailsByUserID";

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



        #endregion Get User Details By UserID

        #region Get User Details By EmailID


        public DataTable GetUserDetailsByEmailID()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
           
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetUserDetailsByEmailID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
               
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);

                if (con != null)
                {
                    con.Dispose();
                }

            return dtUsers;
        }



        #endregion Get User Details By EmailID

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
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                cmd.Parameters.Add("@PhoneNo", SqlDbType.NVarChar, 30).Value = PhoneNo;

                cmd.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;


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
                    //var page = HttpContext.Current.CurrentHandler as Page; //successfull
                    //eObj.SavedSuccessMessage(page);

                    int rslt = Convert.ToInt32(Output.Value.ToString());


                    if (rslt == 1)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //successfull
                        eObj.SavedSuccessMessage(page);

                    }


                    if (rslt == 2 )
                    {
                        // rslt=2- Email id already exists

                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);

                    }

                  
                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID ;
                eObj.Method = "UpdateuserByUserID";

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
                    var page = HttpContext.Current.CurrentHandler as Page;   //not successfull   
                    eObj.DeletionNotSuccessMessage(page);

                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //successfull
                    eObj.DeleteSuccessMessage(page);

                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID ;
                eObj.Method = "DeleteUserByUserID";

                eObj.InsertError();
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
                SqlCommand cmd = new SqlCommand("CheckLoginNameDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@loginName", SqlDbType.VarChar, 50).Value = CheckUser;
                //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ValidateUsername";

                eObj.InsertError();
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

        #region Validate EmailID
        public bool ValidateEmailID()
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckEmailIDDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = Email;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ValidateEmailID";

                eObj.InsertError();
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

        #endregion Validate EmailID

        //------------*Methods Used For Forgot Password Implementation *------------//

        #region Add verificationcode (Generated random number)

        public void AddVerificationCode()
        {
            dbConnection dcon = new dbConnection();

                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[AddVerificationCode]";

                cmd.Parameters.Add("@VerificationCode", SqlDbType.NVarChar, 20).Value = verificationCode;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

        }

        #endregion Add verificationcode (Generated random number)
        
        #region Get User Verification Code By EmailID

        public DataTable GetUserVerificationCodeByEmailID()
        {
            SqlConnection con = null;
            DataTable dtVerificationCode = null;
           
                dtVerificationCode = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetVerificationCodeByEmailID", con);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtVerificationCode);
          
                if (con != null)
                {
                    con.Dispose();
                }

            

            return dtVerificationCode;
        }

        #endregion  Get User Verification Code By EmailID

        #region Reset Password

        public void ResetPassword(Guid UserID)
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "ResetPassword";
                cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserID;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = passWord;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                //if (Output.Value.ToString() == "")
                //{
                //    //not successfull   

                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.UpdationNotSuccessMessage(page);

                //}
                //else
                //{
                //    //successfull

                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.UpdationSuccessMessage(page);

                //}


            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

          
        }

        #endregion  Reset Password

        #endregion Methods

    }
}