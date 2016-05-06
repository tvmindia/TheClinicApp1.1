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
        #endregion AddDoctors

        #region Update Doctor By Doctor Name

        public void UpdateDoctorByName(string OldName)
        {
            dbConnection dcon = new dbConnection();

            try
            {

                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateDoctorByName]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
               
                cmd.Parameters.Add("@OldName", SqlDbType.NVarChar, 255).Value = OldName;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = DoctorName;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = DoctorPhone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = DoctorEmail;
                
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
                    eObj.SavingFailureMessage(page);
                    //eObj.UpdationNotSuccessMessage(page);

                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                    //eObj.UpdationSuccessMessage(page);

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


        #endregion Update Doctor  Doctor Name

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

        #region Delete Doctor By Name

        public void DeleteDoctorByName()
        {
            

            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDoctorByName";
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 255).Value = DoctorName;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

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

        #endregion Delete Doctor By Name


        #region Get Doctor By Name

        public string GetDoctorIdByName()
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
                cmd.CommandText = "[GetDoctorIdByDoctorName]";

                cmd.Parameters.Add("@DoctorName", SqlDbType.NVarChar,255).Value = DoctorName;
             

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    DoctorRoleID = ID.ToString();
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

            return DoctorRoleID;


        }


        #endregion Get Doctor By Name

        

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
       

        public bool GetDoctorIDInVisits()
        {
            bool IDusedOrNot = false;
            string DoctorRoleID = string.Empty;

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetDoctorIDInVisits]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    IDusedOrNot = true;
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

            return IDusedOrNot;
        }


        public bool GetDoctorIDInTokens()
        {
            bool IDusedOrNot = false;
            string DoctorRoleID = string.Empty;

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetDoctorIDInTokens]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    IDusedOrNot = true;
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

            return IDusedOrNot;
        }


        public bool GetDoctorIDInPrescHD()
        {
            bool IDusedOrNot = false;
            string DoctorRoleID = string.Empty;

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetDoctorIDInPrescHD]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    IDusedOrNot = true;
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

            return IDusedOrNot;
        }



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

        #region Get Medicine By Unit
        public DataTable GetMedicineByUnit()
        {
            SqlConnection con = null;
            DataTable dtUnit = null;
            try
            {
                dtUnit = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetMedicineByUnit", con);

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

        #endregion  Get Medicine By Unit

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