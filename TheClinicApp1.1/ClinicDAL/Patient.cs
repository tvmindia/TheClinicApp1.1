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
using Messages = TheClinicApp1._1.UIClasses.Messages;
#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
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
        common cmn = new common();
        #endregion GlobalVariables

        #region Connectionstring
        dbConnection dcon = new dbConnection();
        #endregion Connectionstring

        #region Properties
        #region Patientproperty

        public string Module = "Patient";
        /// <summary>
        /// user id of login user
        /// </summary>
        public Guid usrid
        {
            get;
            set;
        }

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
        public string Age
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
        public bool isPatientDeleted
        {
            get;
            set;
        }
        public int PatientAge
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

        public string AppointmentID
        {
            get;
            set;
        }
        public Boolean IsAppointed
        {
            get;
            set;
        }
        
        #endregion FileProperty
        #endregion Properties

        #region Methods
        //****Patient Functionalities****//
        #region Patients Methods


        #region Patient All Registration View Search Paging

        public string ViewAndFilterAllPatients(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterAllRegistration";

              
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                //cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;

                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "AllRegistration");

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
                eObj.Method = "ViewAndFilterAllRegistration";
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

        #endregion Patient All Registration View Search Paging


        #region Today's Patient: Registration View Search Paging

        public string ViewAndFilterTodayPatients(string searchTerm, int pageIndex, int PageSize)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;

            DateTime now = DateTime.Now;

            var xml = string.Empty;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ViewAndFilterTodayRegistration";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                //cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.NVarChar, 50).Value =cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "TodayRegistration");

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
                eObj.Method = "ViewAndFilterTodayPatients";
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

        #endregion Today's Patient: Registration View Search Paging


        #region UpdatePatientPicture
        public void UpdatePatientPicture()
        {
            SqlConnection con = null;
            try
            {
                
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[UpdatePatientPicture]";
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@image", SqlDbType.Image,0).Value =Picupload;
                pud.Parameters.Add("@ImageType", SqlDbType.NVarChar, 6).Value = ImageType;               
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "UpdatePatientPicture";
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

        #endregion UpdatePatientPicture

        #region AddPatientDetails
        /// <summary>
        /// Insert Patients On save Click
        /// </summary>
        public int AddPatientDetails()
        {

            SqlConnection con = null;
            SqlParameter Output = null;
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
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = CreatedBy;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value =cmn.ConvertDatenow(DateTime.Now);
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value =cmn.ConvertDatenow(DateTime.Now);
                pud.Parameters.Add("@image", SqlDbType.Image, 0).Value = Picupload;
                pud.Parameters.Add("@ImageType", SqlDbType.NVarChar, 6).Value = ImageType;
                //@AppointmentID enables the appointed patient to become  registered patient
                pud.Parameters.Add("@AppointmentID", SqlDbType.UniqueIdentifier).Value = (AppointmentID!=null)?Guid.Parse(AppointmentID):Guid.Empty;
                Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);              
                pud.ExecuteNonQuery();

                if (Output.Value.ToString() == "0")
                {
                    //not successfull   

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionNotSuccessMessage(page);
                    return 0;

                }
                else
                {
                    //successfull
                    string msg = Messages.PatInsertionSuccessFull;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionSuccessMessage1(page,msg);
                   


                }
                
                
                }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "AddPatientDetails";
                eObj.InsertError();

            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return int.Parse(Output.Value.ToString());
            
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
                pud.Parameters.Add("@Occupation", SqlDbType.NVarChar, 255).Value = Occupation;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value =cmn.ConvertDatenow(DateTime.Now);

              
                
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
                    eObj.PatientUpdationSuccessMessage(page);
                }


            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "UpdatePatientDetails";
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
                pud.Parameters.Add("@Name", SqlDbType.NVarChar,255).Value = (Name=="null")?DBNull.Value.ToString():Name;
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
                
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "GetPatientID";
                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return PatID;
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
              
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "SearchPatientDetails";
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
                if (Output.Value.ToString() != "1")
                {
                    //not successfull   
                    string msg = Messages.PatientDeletionFailure;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.DeletionNotSuccessMessage(page, msg);

                }
                else
                {
                    //successfull
                    string msg = Messages.PatientDeletionSuccessFull;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.DeleteSuccessMessage(page,msg);
                }
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "DeletePatientDetails";
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

        public string DeletePatientByPatientID()
        {
            string result = string.Empty;
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

                isPatientDeleted = true;
                result = Output.Value.ToString();

               
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "DeletePatientDetails";
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
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value =cmn.ConvertDatenow(DateTime.Now);               
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = CreatedBy;
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);
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
                    string msg = Messages.PatInsertionSuccessFull;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionSuccessMessage1(page, msg);
                }
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "AddFile";
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
        #endregion AddFile

        #region Validate Patient Exist
        public bool CheckPatientTokenExist(Guid PatientID)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckPatientTokenExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
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
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "CheckPatientTokenExist";
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

        #endregion Validate Patient Exist

        //***Generate File Number***//
        #region Generate_File_Number
        public string Generate_File_Number()
        {
            string fileNumberPrefix = System.Web.Configuration.WebConfigurationManager.AppSettings["FileNumberPrefix"];
            string NUM;
            string FileNO=string.Empty;
            dbConnection dcon = null;
          
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetTopFileNO]";
              
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                SqlParameter OutparmItemId = cmd.Parameters.Add("@String", SqlDbType.NVarChar,50);
                OutparmItemId.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                NUM = OutparmItemId.Value.ToString();

                if (NUM != "")
                {
                    //trim here
                    int x = Convert.ToInt32(NUM.Substring(NUM.IndexOf('-') + 1));
                    x = x + 1;
                    x.ToString();
                    FileNO = fileNumberPrefix + "-" + x;
                }
                else
                {
                    int x = 1000;
                     FileNO = fileNumberPrefix + "-" + x;
                }
             }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "Generate_File_Number";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return FileNO;
           

            
        }
#endregion Generate_File_Number


        //***Grid Bind For All regisration && Todays Registration
        #region GridBind

         #region ViewAllRegistration
     
        public DataTable GetAllRegistration()
        {
            SqlConnection con = null;
            DataTable dt = new DataTable();
            try
            {
                
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllRegistration", con);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
               
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "GetAllRegistration";
                eObj.InsertError();              
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dt;
        }
        #endregion ViewAllRegistration

        #region ViewDateRegistration
        public DataTable GetDateRegistration()
        {
            SqlConnection con = null;
            DataTable dt1 = new DataTable();
            try
            {
                DateTime now = DateTime.Now;
               
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewDateRegistration", con);
                cmd.Parameters.Add("@CreatedDate", SqlDbType.NVarChar, 50).Value =cmn.ConvertDatenow(now).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt1 = new DataTable();
                adapter.Fill(dt1);
               
            }
            catch (Exception ex)
            {
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = usrid;
                eObj.Method = "GetDateRegistration";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dt1;
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
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
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
        public void GetSearchWithName(string SearchName)
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SearchPatientWithName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = SearchName;
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
            cmd.Parameters.Add("@MobileNo", SqlDbType.NVarChar, 50).Value = Phone;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                PatientID = Guid.Parse(dr["PatientID"].ToString());
                Name = dr["Name"].ToString();
                Gender = dr["Gender"].ToString();
                DOB = Convert.ToDateTime(dr["DOB"].ToString());
                Address = dr["Address"].ToString();
                Phone = dr["Phone"].ToString();
                Email = dr["Email"].ToString();
                MaritalStatus = dr["MaritalStatus"].ToString();
                Occupation = dr["Occupation"].ToString();
                ImageType = dr["ImageType"].ToString();
                FileNumber = dr["FileNumber"].ToString();
                DateTime date =cmn.ConvertDatenow(DateTime.Now);
                int year = date.Year;
                DateTime DT = DOB;
                PatientAge = year - DT.Year;
                 
                //DOB = Convert.ToDateTime(Age.ToString());
            }
            con.Close();        

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

        public void GetPatientDetailsByID()
        {
            DateTime date =cmn.ConvertDatenow(DateTime.Now);
            int year = date.Year;
            DataTable ds = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SelectPatient", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            ds = new DataTable();
            adapter.Fill(ds);
            if(ds.Rows.Count>0)
            {
                 foreach (DataRow row in ds.Rows)
                 {
                    DateTime dt = Convert.ToDateTime(row["DOB"].ToString());
                    int Agenow = year - dt.Year;
                    Name=row["Name"].ToString();
                    Age=Agenow.ToString();
                    Gender=row["Gender"].ToString();
                    Address = row["Address"].ToString();
                    Phone = row["Phone"].ToString();
                    Email = row["Email"].ToString();
                    MaritalStatus = row["MaritalStatus"].ToString();
                    Occupation = row["Occupation"].ToString();
                    PatientID = Guid.Parse(row["PatientID"].ToString());
                    ImageType = row["ImageType"].ToString();
                    FileNumber = row["FileNumber"].ToString();
                    FileID = Guid.Parse(row["FileID"].ToString());

                 }
                
            }
            con.Close();

        }


        #endregion SelectPatient

        #endregion Methods
    }

}