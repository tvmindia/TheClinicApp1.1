#region CopyRight

//Author      : Thomson K Varkey
//Created Date: Feb-04-2016

#endregion CopyRight

#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
#endregion Included Namespaces

namespace TheClinicApp.ClinicDAL
{
    public class Patient
    {
        #region Constructor
        public Patient()
        {
            FileID = Guid.NewGuid();
        }
        #endregion Constructor

        #region GlobalVariables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        #endregion GlobalVariables

        #region Connectionstring
        dbConnection dcon = new dbConnection();
        #endregion Connectionstring

        #region Properties
        #region Patientproperty
        public Guid PatientID
        {
            get;
            set;
        }
        public Guid ClinicID 
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public DateTime DOB
        {
            get;
            set;
        }
        public string Gender
        {
            get;
            set;
        }
        public string MaritalStatus
        {
            get;
            set;
        }
        public string Occupation
        {
            get;
            set;
        }
        public object Picupload
        {
            get;
            set;
        }
        public string ImageType
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        #endregion Patientproperty

        #region FileProperty
        
        public string FileNumber
        {
            get;
            set;
        }
        public Guid FileID
        {
            get;
            set;
        }
        #endregion FileProperty
        #endregion Properties

        #region Methods
        //****Patient Functionalities****//
        #region Patients Methods
        #region AddPatientDetails
        /// <summary>
        /// Insert Patients On save Click
        /// </summary>
        public void AddPatientDetails()
        {

            SqlConnection con = null;
            try
            {
                
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertPatientDetails]";
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                pud.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                pud.Parameters.Add("@DOB", SqlDbType.Date).Value = DOB;
                pud.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = Gender;
                pud.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar, 50).Value = MaritalStatus;
                pud.Parameters.Add("@Occupation", SqlDbType.NVarChar, 255).Value = Occupation;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = UA.userName;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UA.userName;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@image", SqlDbType.Image,0).Value =Picupload;
                pud.Parameters.Add("@ImageType", SqlDbType.NVarChar, 6).Value = ImageType;               
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
        #endregion AddPatientDetails

        #region UpdatePatientDetails
        /// <summary>
        /// Updates the PatientDetails
        /// </summary>
        public void UpdatePatientDetails()
        {
            SqlConnection con = null;
            try
            {
                
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "UpdatePatientDetails";
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                pud.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                pud.Parameters.Add("@DOB", SqlDbType.Date).Value = DOB;
                pud.Parameters.Add("@Gender", SqlDbType.NVarChar, 50).Value = Gender;
                pud.Parameters.Add("@MaritalStatus", SqlDbType.NVarChar, 50).Value = MaritalStatus;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UA.userName;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);  
                pud.ExecuteNonQuery();
                if (int.Parse(Output.Value.ToString()) == -1)
                {
                    ////not successfull
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
        #endregion UpdatePatientDetails

        #region GetPatientDetails
        /// <summary>
        /// Select PatientId from database using parameters
        /// </summary>
        /// <param Name="Name"></param>
        /// <param Address="Address"></param>
        /// <param Phone="Phone"></param>
        /// <param Email="Email"></param>
        /// <returns></returns>
        public Guid GetPatientID(string Name,string Address,string Phone,string Email)
         
        {
            SqlConnection con = null;
            Guid PatID = Guid.Empty;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "GetPatientID";
                pud.Parameters.Add("@Name", SqlDbType.NVarChar,255).Value = Name;
                pud.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                pud.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = Phone;
                pud.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = Email;
                SqlParameter OutparamId = pud.Parameters.Add("@OutPatientId", SqlDbType.UniqueIdentifier);
                OutparamId.Direction = ParameterDirection.Output;
                pud.ExecuteNonQuery();
                if (OutparamId.Value != null)
                
                {
                    PatID=Guid.Parse(OutparamId.Value.ToString());
                    return PatID;
                }
                return PatID;
                
            }
            catch (Exception ex)
            {
                //var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
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
        
        #endregion GetPatientDetails

        #region SearchPatientDetails
        /// <summary>
        /// Search and fill for updation 
        /// </summary>
        /// <param name="searchtxt"></param>
        public void SearchPatientDetails(string searchtxt)
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "UpdatePatientDetails";
                pud.Parameters.Add("@string", SqlDbType.NVarChar, 255).Value = searchtxt;
               
                SqlParameter OutparamId = pud.Parameters.Add("@OutparamId", SqlDbType.SmallInt);
                OutparamId.Direction = ParameterDirection.Output;
                string strreturn;
                strreturn=pud.ExecuteScalar().ToString();
                //if (int.Parse(OutparamId.Value.ToString()) != 0)
                //{
                //    //not successfull
                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.UpdationSuccessData(page, "Not Updated");
                //}
                //else
                //{
                //    //successfull
                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.UpdationSuccessData(page);
                //}


            }
            catch (Exception ex)
            {
                //var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
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

        #endregion SearchPatientDetails

        #region DeletePatientDetails

        public void DeletePatientDetails()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "DeletePatientDetails";
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;

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
        #endregion DeletePatientDetails
        #endregion Patients Methods

        //***Add New File***//
        #region AddFile
        public void AddFile()
        {

            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertNewFile]";
                pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = DateTime.Now;               
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = UA.userName;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UA.userName;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@FileNumber", SqlDbType.NVarChar, 50).Value = FileNumber;
                SqlParameter OutputFileNumber = pud.Parameters.Add("@OutFileNumber", SqlDbType.NVarChar, 50);
                OutputFileNumber.Direction = ParameterDirection.Output;
                pud.ExecuteNonQuery();

                if (OutputFileNumber.Value.ToString() != "")
                {
                    //not successfull   

                    FileNumber = OutputFileNumber.Value.ToString();

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
        #endregion AddFile

        //***Grid Bind For All regisration && Todays Registration
        #region GridBind
        #region ViewAllRegistration
        /// <summary>
        /// ***********
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRegistration()
        {
            SqlConnection con = null;
           
            try
            {
                DataTable dt = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                //var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
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
        #endregion ViewAllRegistration

        #region ViewDateRegistration
        public DataTable GetDateRegistration()
        {
            SqlConnection con = null;
            try
            {
                DateTime now = DateTime.Now;
                DataTable dt1 = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewDateRegistration", con);
                cmd.Parameters.Add("@CreatedDate", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt1 = new DataTable();
                adapter.Fill(dt1);
                return dt1;
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
        #endregion ViewDateRegistration
        #endregion GridBind      

        #region GetSearchBoxData
        public DataTable GetSearchBoxData()
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("GetSearchBoxData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion GetSearchBoxData

        #region SearchWithAny
        public DataTable GetSearchBoxDataByAnyValue(string value)
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();


            SqlCommand cmd = new SqlCommand("SearchPatientDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@string", SqlDbType.NVarChar, 255).Value = value;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion SearchWithAny

        #region SearchWithName
        public DataTable GetSearchWithName(string SearchName)
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SearchPatientWithName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = SearchName;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion SearchWithName

        #region SelectPatient
        public DataTable SelectPatient()
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SelectPatient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion SelectPatient

        #endregion Methods
    }

}