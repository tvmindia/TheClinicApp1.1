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
            RoleID=Guid.NewGuid();
        }
        #endregion Constructors

        #region Global variables

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        string ModuleUnit = "Unit";
        string ModuleDoctor = "Add Doctor";

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

        /// <summary>
        /// User id of logined user
        /// </summary>
     

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
        public object Logosmall
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
        public string Createdby
        {
            get;
            set;
        }
        public string Updatedby
        {
            get;
            set;
        }

        #endregion ClinicProperty
        #region RolesProperty

        public Guid RoleID
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
        #endregion RolesProperty

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
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = Createdby;
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
                pud.Parameters.Add("@Logo", SqlDbType.Image, 0).Value = Logo;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = Createdby;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = Createdby;
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

        #region Update Clinic

        public void UpdateClinic()
        {
            dbConnection dcon = new dbConnection();

            try
            {

                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = ClinicName;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = ClinicLocation;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = ClinicAddress;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = ClinicPhone;
                cmd.Parameters.Add("@Logo", SqlDbType.VarBinary).Value = Logo;
                cmd.Parameters.Add("@Logosmall", SqlDbType.VarBinary).Value = Logosmall;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = updatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value=DateTime.Now;
                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //Success
                    eObj.SavedSuccessMessage(page);

                }
                else
                {
                   
                        var page = HttpContext.Current.CurrentHandler as Page;   //TROUBLE UPDATE!
                        eObj.UpdationNotSuccessMessage(page);
                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateUnits";

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


        #endregion Update Clinic

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
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtMedicines);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "InsertUnits";

                eObj.InsertError();

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

        //------------------------------------* Doctor :Start *--------------------------------------------//

        #region Doctor View Search Paging

        public string ViewAndFilterDoctors(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterDoctors";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Doctors");

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
                eObj.Module = ModuleDoctor;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterDoctors";
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

        #endregion Doctor View Search Paging


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

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataTable();
                da.Fill(ds);

                conn.Close();

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewDoctors";

                eObj.InsertError();
            }


            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

            }
            return ds;
        }

        #endregion View Doctors

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "CheckDoctorNameDuplication";

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

        #endregion Validate Doctor Name

        #region AddDoctors
        public void InsertDoctors()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                DoctorID = Guid.NewGuid();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertDoctors]";

                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = DoctorName;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = DoctorPhone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = DoctorEmail;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = createdBy;


                if (UsrID != Guid.Empty)
                {
                    pud.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UsrID;
                }

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page; //not successfull   
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 1)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //successfull
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "InsertDoctors";

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
        #endregion AddDoctors

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
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateDoctors";

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


        #endregion Update Doctors

        #region Check DoctorId Used

        public bool CheckDoctorIdUsed()
        {
            bool isUsed = false;

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

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "CheckDoctorIdUsed";

                eObj.InsertError();

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

        #endregion Check DoctorId Used

        #region Get RoleID Of Doctor

        public string GetRoleIDOfDoctor()
        {
            string DoctorRoleID = string.Empty;

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetRoleIDOFDoctor]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    DoctorRoleID = ID.ToString();
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetRoleIDOfDoctor";

                eObj.InsertError();

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return DoctorRoleID;
        }

        #endregion Get RoleID Of Doctor

        #region Delete Doctor By DoctorID

        public string DeleteDoctorByIDForWM(bool rdoNotDoctor = false)
        {
            string result = string.Empty;
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


                if (rdoNotDoctor == true)
                {

                 result =     Output.Value.ToString();
                }




            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteDoctorByID";

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

        public void DeleteDoctorByID(bool rdoNotDoctor = false)
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
                    if (rdoNotDoctor == true)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavingFailureMessage(page);
                    }
                    else
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.DeletionNotSuccessMessage(page);
                    }

                }
                else
                {
                    //successfull

                    if (rdoNotDoctor == true)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavedSuccessMessage(page);
                    }

                    else
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.DeleteSuccessMessage(page);
                    }

                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteDoctorByID";

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

        #endregion Delete Doctor By DoctorID

        #region Get Doctor Details By Doctor ID
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDoctorDetailsByID";

                eObj.InsertError();
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

        public DataSet GetDoctorDetailsByIDForWM()
        {
            SqlConnection con = null;
            DataSet dsDoctorById = null;
            try
            {
                dsDoctorById = new DataSet();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetDoctorDetailsByID", con);

                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dsDoctorById);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDoctorDetailsByID";

                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dsDoctorById;
        }

        #endregion  Get Doctor Details By Doctor ID

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleDoctor;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetDoctorDetailsByUserID";

                eObj.InsertError();

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

        #endregion GetDoctorDetailsByUserID-

        //----------------------------------------* Doctor : End *--------------------------------------------//


        
        //------------------------------------* Unit :Start *--------------------------------------------//

        #region UNIT View Search Paging

        public string ViewAndFilterUnits(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterUnits";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Units");

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
                eObj.Module = ModuleUnit;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterUnits";
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

        #endregion UNIT View Search Paging


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

                UnitID = Guid.NewGuid();

                pud.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;
                pud.Parameters.Add("@Code", SqlDbType.NVarChar, 255).Value = Code;
                pud.Parameters.Add("@Description", SqlDbType.NVarChar, 255).Value = Description;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar,255).Value = createdBy;
               
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page;   //not successfull   
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 1)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //successfull
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "InsertUnits";

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

        #endregion AddUnits

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

                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //Success
                    eObj.SavedSuccessMessage(page);

                }
                else
                {
                    if (Outputval == 2)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;   //Already exists!
                        eObj.AlreadyExistsMessage(page);

                    }
                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateUnits";

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


        #endregion Update Unit

        #region Check Unit Is Used

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

                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 255).Value = Description;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                object ID = cmd.ExecuteScalar();

                if (ID != null)
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "CheckUnitIsUsed";

                eObj.InsertError();
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

        #endregion  Check Unit Is Used

        #region Get Unit By UnitID

        public DataSet GetUnitByIDForWM()
        {
            SqlConnection con = null;
            DataSet dsUnits = null;
            try
            {
                dsUnits = new DataSet();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetUnitByID", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UnitID", SqlDbType.UniqueIdentifier).Value = UnitID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dsUnits);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetUnitByIDForWM";

                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dsUnits;
        }

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "GetUnitByID";

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

        #endregion  Get Unit By UnitID

        #region Delete unit By UnitId

        public string DeleteUnitByUnitIdForWM()
        {
            string result = string.Empty;
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

                result = Output.Value.ToString();


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteUnitByUnitId";

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

        public void DeleteUnitByUnitId()
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
                    var page = HttpContext.Current.CurrentHandler as Page;  //not successfull  
                    eObj.DeletionNotSuccessMessage(page);

                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page; //successfull
                    eObj.DeleteSuccessMessage(page);

                }

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteUnitByUnitId";

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

        #endregion  Delete unit By UnitId

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
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "CheckUnitDuplication";

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

        #endregion Validate Unit

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAllUnits";

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

        #endregion  View All Units

        //----------------------------------------* Unit : End *--------------------------------------------//
        #region AddRoles
        public void InsertRole()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertRoles]";

                //UnitID = Guid.NewGuid();

                //pud.Parameters.Add("@RoleID", SqlDbType.UniqueIdentifier).Value = RoleID;
                pud.Parameters.Add("@RoleName", SqlDbType.NVarChar, 255).Value = RoleName;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@Createdby", SqlDbType.NVarChar, 255).Value = createdBy;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@Updatedby", SqlDbType.NVarChar, 255).Value = createdBy;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page;   //not successfull   
                    eObj.SavingFailureMessage(page);

                }
                else
                {
                    int rslt = Convert.ToInt32(Output.Value.ToString());

                    if (rslt == 1)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //successfull
                        eObj.SavedSuccessMessage(page);
                    }

                    else
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);

                    }

                }


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = ModuleUnit;

                eObj.UserID = UA.UserID;
                eObj.Method = "InsertUnits";

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
        #endregion AddRoles
        #endregion Methods
    }
}