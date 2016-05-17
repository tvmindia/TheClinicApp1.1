using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace TheClinicApp1._1.ClinicDAL
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

        public Guid UsrID
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


        public bool CheckDoctorIdUsed()
        {
            bool isUsed = false;

            //dbConnection dcon = null;



            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckDoctorIdUsed", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlParameter outflag = cmd.Parameters.Add("@Cnt", SqlDbType.Bit);
                outflag.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                object ID = outflag.Value;


                isUsed = Convert.ToBoolean(ID);
                //if (cnt > 0)
                //{
                //    isUsed = true;
                //}
            }
            catch (Exception ex)
            {
                //throw ex;
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


            return isUsed;

        }


        #region View Doctors

        public DataTable ViewDoctors()
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

                cmd = new SqlCommand("[ViewDoctors]", conn);
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
       
        #endregion View Doctors

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
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = DoctorPhone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = DoctorEmail;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;


                if (UsrID != Guid.Empty)
                {
                    pud.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UsrID;
                }

                //pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = updatedBy;

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
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    

                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 1)
                    {
                        //successfull

                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavedSuccessMessage(page);
                    }

                    if (rslt == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);
                            
                    }
                   


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

        #region Get Doctor Details
        public DataTable GetDoctorDetails()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetDoctorDetails", con);
               
                cmd.CommandType = CommandType.StoredProcedure;

             
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);
                
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
            return dtUsers;
        }

        #endregion  Get Doctor Details


        #region Get Doctor Details By ID
        public DataTable GetDoctorDetailsByID()
        {
            SqlConnection con = null;
            DataTable dtDoctorById = null;
            try
            {
                dtDoctorById = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetDoctorDetailsByID", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtDoctorById);

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
            return dtDoctorById;
        }

        #endregion  Get Doctor Details By ID


        #region GetDoctorDetailsByUserID
        public DataTable GetDoctorDetailsByUserID()
        {
            SqlConnection con = null;
            DataTable dtDoctorByUsrId = null;
            try
            {
                dtDoctorByUsrId = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetDoctorDetailsByUserID", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UsrID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtDoctorByUsrId);

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
            return dtDoctorByUsrId;
        }

        #endregion GetDoctorDetailsByUserID

        #region Delete Doctor By DoctorID

        public void DeleteDoctorByID()
        {


            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDoctors";

                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;

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
                    eObj.DeletionNotSuccessMessage(page);

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

        #endregion Delete Doctor By DoctorID


        


        #region Validate Doctor Name
        public bool CheckDoctorNameDuplication()
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckDoctorNameDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DoctorName", SqlDbType.VarChar, 255).Value = DoctorName;
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
                //throw ex;
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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

        #endregion Validate Doctor Name



        #region Update Doctors
        public void UpdateDoctors()
        {
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateDoctors]";

                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = DoctorName;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = DoctorPhone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = DoctorEmail;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = updatedBy;

                //cmd.ExecuteNonQuery();


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

                //throw ex;
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


        #endregion Update Doctors



            //SqlConnection con = null;
            //DataTable dtUsers = null;
            //try
            //{
            //    dtUsers = new DataTable();
            //    dbConnection dcon = new dbConnection();
            //    con = dcon.GetDBConnection();
            //    SqlCommand cmd = new SqlCommand("GetDoctorIdByDoctorName", con);

            //    cmd.CommandType = CommandType.StoredProcedure;


            //    cmd.Parameters.Add("@DoctorName", SqlDbType.NVarChar,255).Value = DoctorName;

            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    adapter.SelectCommand = cmd;
            //    adapter.Fill(dtUsers);

            //}
            //catch (Exception ex)
            //{
            //    var page = HttpContext.Current.CurrentHandler as Page;
            //    eObj.ErrorData(ex, page);

            //}
            //finally
            //{
            //    if (con != null)
            //    {
            //        con.Dispose();
            //    }

            //}
            //return dtUsers;
       


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


        //--------------Unit


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
                //pud.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;
                pud.Parameters.Add("@Code", SqlDbType.NVarChar, 255).Value = Code;
                pud.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = Description;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar,255).Value = createdBy;
                //pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                //pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                //pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
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
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 1)
                    {
                        //successfull

                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavedSuccessMessage(page);
                    }

                    if (rslt == 2)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);

                    }
                   



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



        #region View All Units
        public DataTable ViewAllUnits()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewUnits", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);

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
            return dtUsers;
        }

        #endregion  View All Units


        #region Get Unit By UnitID
        public DataTable GetUnitByID()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetUnitByID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);

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
            return dtUsers;
        }

        #endregion  Get Unit By UnitID


        #region Delete unit By UnitId

        public void  DeleteUnitByUnitId()
        {


            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteunitByID";
            
                cmd.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;

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
                    eObj.DeletionNotSuccessMessage(page);

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

        #endregion  Delete unit By UnitId


        #region View PrescDT By Unit
        public DataTable GetPrescDTByUnit()
        {
            SqlConnection con = null;
            DataTable dtUnit = null;
            try
            {
                dtUnit = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetPrescDTByUnit", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar,255).Value = Description;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUnit);

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
            return dtUnit;
        }

        #endregion View PrescDT By Unit

        #region View ReceiptDT By Unit
        public DataTable GetReceiptDTByUnit()
        {
            SqlConnection con = null;
            DataTable dtUnit = null;
            try
            {
                dtUnit = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetReceiptDTByUnit", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 255).Value = Description;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUnit);

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
            return dtUnit;
        }

        #endregion  View ReceiptDT By Unit

        #region Validate Unit
        public bool CheckUnitDuplication(string UnitDescription)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckUnitDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@unit", SqlDbType.VarChar, 255).Value = UnitDescription;
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

        #endregion Validate Unit


        #region Update Unit

        public void UpdateUnits()
        {
            dbConnection dcon = new dbConnection();

            try
            {

                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateUnits]";

                cmd.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;

                cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 255).Value = Code;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = Description;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = updatedBy;



                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                cmd.ExecuteNonQuery();

                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                    //Success
                }
                else
                {
                    if (Outputval == 2)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);

                        //Already exists!
                    }
                }



                //SqlParameter Output = new SqlParameter();
                //Output.DbType = DbType.Int32;
                //Output.ParameterName = "@Status";
                //Output.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(Output);
                //cmd.ExecuteNonQuery();

                //if (Output.Value.ToString() == "")
                //{
                //    //not successfull   

                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.SavingFailureMessage(page);
                //    //eObj.UpdationNotSuccessMessage(page);

                //}
                //else
                //{
                //    //successfull

                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.SavedSuccessMessage(page);
                //    //eObj.UpdationSuccessMessage(page);

                //}


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


        #endregion Update Unit


        public bool CheckUnitIsUsed()
        {
            bool isUsed = false;


            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckUnitIsUsed", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar,255).Value = Description;

                object ID = cmd.ExecuteScalar();

                if (ID != null )
                {
                    //isUsed = Convert.ToBoolean(ID); 

                    int c = Convert.ToInt32(ID);

                    if (c > 0)
                    {
                        isUsed = true;
                    }



                }
             

               
               
            }
            catch (Exception ex)
            {
                //throw ex;
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }


            return isUsed;


        }

        //------------------------------------*Unit *--------------------------------------------//

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



        #region View All Medicines
        public DataTable ViewAllMedicines()
        {
            SqlConnection con = null;
            DataTable dtMedicines = null;
            try
            {
                dtMedicines = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewMedicines", con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtMedicines);

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
            return dtMedicines;
        }

        #endregion  View All Medicines








        #endregion Methods
    }
}