#region CopyRight

//Author      : Thomson K Varkey
//Created Date: Feb-22-2016

#endregion CopyRight


#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class Doctor
    {
        #region Connectionstring
        dbConnection dcon = new dbConnection();
        #endregion Connectionstring

        #region global
 
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;            
        public string Module = "Doctor";


        #endregion global

        #region GrabFileID
        
        #region Property



        public Guid PatientIdForFile
        {
            get;
            set;
        }

        //public string fileID
        //{
        //    get;
        //    set;
        //}

        #endregion Property

        #region Methods

        #region GetFileIDUSingPatientID
        public DataTable GetFileIDUSingPatientID()
        {
            SqlConnection con = null;
            DataTable DtFileID = new DataTable();

            try
            {
                DateTime now = DateTime.Now;
               
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetFileID", con);
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientIdForFile;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DtFileID = new DataTable();
                adapter.Fill(DtFileID);
              
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];   
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetFileIDUSingPatientID";
                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return DtFileID;

        }
     
        #endregion GetFileIDUSingPatientID      

        #region ViewAllRegistration
        /// <summary>
        /// ***********
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRegistration()
        {
            SqlConnection con = null;
            DataTable dt = new DataTable();
            try
            {              
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();             
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];   
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAllRegistration";
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
                cmd.Parameters.Add("@CreatedDate", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt1 = new DataTable();
                adapter.Fill(dt1);               
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];   
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
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

        #endregion Methods

        #endregion GrabFileID

        #region Doctor Properties
        #endregion Doctor Properties
        public string ClinicID
        {
            get;
            set;
        }
        #region Doctor Schedule properties
        public string DocScheduleID
        {
            get;
            set;
        }
        public string DoctorID
        {
            get;
            set;
        }
        public string DoctorAvailDate
        {
            get;
            set;
        }
        public Int16 PatientLimit
        {
            get;
            set;
        }
        public Boolean IsAvailable
        {
            get;
            set;
        }
       
        public decimal StartTime
        {
            get;
            set;
        }
        public decimal EndTime
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;

        }
        public string CreatedDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public string UpdatedDate
        {
            get;
            set;
        }
        public string StartDate
        {
            get;
            set;
        }
        public string EndDate
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }

        public string SearchDate
        {
            get;
            set;
        }
        #endregion Doctor Schedule properties

        #region Doctor Schedule methods
        #region GetDoctorScheduleDetailsByDocScheduleID
        public DataSet GetDoctorScheduleDetailsByDocScheduleID()
        {
            if (DocScheduleID == "")
            {
                throw new Exception("DocScheduleID is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = null;


            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDoctorScheduleDetailsByDocScheduleID";
                cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.DocScheduleID);
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


            }
            catch (Exception ex)
            {

                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetDoctorScheduleDetailsByDocScheduleID";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return ds;
        }
        #endregion GetDoctorScheduleDetailsByDocScheduleID

        #region GetDoctorScheduleDetailsByDoctorID
        public DataSet GetDoctorScheduleDetailsByDoctorID()
        {
            if (DoctorID == "")
            {
                throw new Exception("DoctorID is Empty!!");
            }
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetDoctorScheduleDetailsByDoctorID";
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.DoctorID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.ClinicID);
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);
           }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetDoctorScheduleDetailsByDoctorID";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return ds;
        }

        #endregion GetDoctorScheduleDetailsByDoctorID

        #region GetAllDoctorsScheduledBetweenDates
        public DataSet GetAllDoctorsScheduledBetweenDates()
        {
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllDoctorsScheduledBetweenDates";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.ClinicID);
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAllDoctorsScheduledBetweenDates";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return ds;
        }
        #endregion GetAllDoctorsScheduledBetweenDates

        #region GetAllDoctorsScheduleDetailsBetweenDates
        public DataSet GetAllDoctorsScheduleDetailsBetweenDates()
        {
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (DoctorID == "")
            {
                throw new Exception("DoctorID is Empty!!");
            }
            if (StartDate == "")
            {
                throw new Exception("StartDate is Empty!!");
            }
            if (EndDate == "")
            {
                throw new Exception("EndDate is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataSet ds = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllDoctorsScheduleDetailsBetweenDatesForCalender";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.ClinicID);
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAllDoctorsScheduleDetailsBetweenDates";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return ds;
        }
        #endregion GetAllDoctorsScheduleDetailsBetweenDates

        #region InsertDoctorSchedule
        public Int16 InsertDoctorSchedule()
        {
            
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter=null, outSchedulID = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if(DoctorID == "")
            {
                throw new Exception("DoctorID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertDoctorSchedule";
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@AvailableDate", SqlDbType.DateTime).Value = DoctorAvailDate;
                cmd.Parameters.Add("@PatientLimit", SqlDbType.Int).Value = PatientLimit;
                cmd.Parameters.Add("@IsAvailable", SqlDbType.Bit).Value = IsAvailable;
                cmd.Parameters.Add("@Starttime", SqlDbType.Decimal).Value = StartTime;
                cmd.Parameters.Add("@Endtime", SqlDbType.Decimal).Value = EndTime;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar,255).Value = CreatedBy;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                outParameter = cmd.Parameters.Add("@InsertStatus", SqlDbType.SmallInt);
                outParameter.Direction = ParameterDirection.Output;
                outSchedulID = cmd.Parameters.Add("@outID", SqlDbType.UniqueIdentifier);
                outSchedulID.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                DocScheduleID = outSchedulID.Value.ToString();
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertDoctorSchedule";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            //insert success or failure
            return Int16.Parse(outParameter.Value.ToString());
        }
        #endregion InsertDoctorSchedule

        #region UpdateDoctorSchedule
        public Int16 UpdateDoctorSchedule()
        {

            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (DoctorID == "")
            {
                throw new Exception("DoctorID is Empty!!");
            }
            if (DocScheduleID == "")
            {
                throw new Exception("DocScheduleID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDoctorSchedule";
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DocScheduleID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@AvailableDate", SqlDbType.DateTime).Value = DoctorAvailDate;
                cmd.Parameters.Add("@PatientLimit", SqlDbType.Int).Value = PatientLimit;
                cmd.Parameters.Add("@IsAvailable", SqlDbType.Bit).Value = IsAvailable;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@Starttime", SqlDbType.Decimal).Value = StartTime;
                cmd.Parameters.Add("@Endtime", SqlDbType.Decimal).Value = EndTime;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                outParameter = cmd.Parameters.Add("@UpdateStatus", SqlDbType.SmallInt);
                outParameter.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateDoctorSchedule";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            //insert success or failure
            return Int16.Parse(outParameter.Value.ToString());
        }
        #endregion UpdateDoctorSchedule

        #region CancelDoctorSchedule
        public Int16 CancelDoctorSchedule()
        {
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null;
           
            if (DocScheduleID == "")
            {
                throw new Exception("DocScheduleID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CancelDoctorSchedule";
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DocScheduleID);
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                outParameter = cmd.Parameters.Add("@UpdateStatus", SqlDbType.SmallInt);
                outParameter.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateDoctorSchedule";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            //insert success or failure
            return Int16.Parse(outParameter.Value.ToString());
        }
        #endregion CancelDoctorSchedule

        #endregion Doctor Schedule methods

        #region Doctor Methods
        #region GetAllDoctorsByClinicID
        public DataSet GetAllDoctorsByClinicID()
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetDoctorDetails]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                sda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                sda.Fill(ds);
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAllDoctorsByClinicID";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;

        }
        #endregion GetAllDoctorsByClinicID
        #region GetAllScheduleDetailsByDoctorID
        public DataSet GetAllScheduleDetailsByDoctorID()
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (DoctorID == "")
            {
                throw new Exception("DoctorID is Empty!!");
            }
            if (StartDate == "")
            {
                throw new Exception("StartDate is Empty!!");
            }
            if (EndDate == "")
            {
                throw new Exception("EndDate is Empty!!");
            }
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetAllScheduleDetailsByDoctorID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = StartDate;
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = EndDate;
                sda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                sda.Fill(ds);
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAllScheduleDetailsByDoctorID";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;

        }
        #endregion GetAllScheduleDetailsByDoctorID


        #region GetAllDoctorScheduleDetailsByDate
        public DataSet GetAllDoctorScheduleDetailsByDate()
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (SearchDate == "")
            {
                throw new Exception("SearchDate is Empty!!");
            }
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllDoctorScheduleDetailsByDate";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@SearchDate", SqlDbType.Date).Value = DateTime.Parse(SearchDate);
                sda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                sda.Fill(ds);
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAllDoctorScheduleDetailsByDate";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;

        }
        #endregion GetAllDoctorScheduleDetailsByDate

        #region IsDoctorScheduleAllotedForPatientAppointment
        public Boolean  CheckDoctorScheduleAllotedForPatientAppointment()
        {
            SqlConnection con = null;
            SqlParameter outstatusparm = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (DocScheduleID == "")
            {
                throw new Exception("DocScheduleID is Empty!!");
            }
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "IsDoctorScheduleAllotedForPatientAppointment";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DocScheduleID);
                outstatusparm = cmd.Parameters.Add("@Status", SqlDbType.Bit);

                outstatusparm.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                
               
              
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "CheckDoctorScheduleAllotedForPatientAppointment";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return bool.Parse(outstatusparm.Value.ToString());
        }
        #endregion IsDoctorScheduleAllotedForPatientAppointment


        #endregion Doctor Methods




    }
    public class CaseFile
    {
        #region global

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string Module = "Doctor/CaseFile";

        private DateTime CreatedDateLocal;
        private DateTime UpdatedDateLocal;

        #endregion global
         
        #region Constructors
            public CaseFile()
            {
                FileID = Guid.NewGuid();
            }
            public CaseFile(Guid FileID)
            {
                this.FileID = FileID;
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetFileDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                DataRow row = dt.NewRow();
                row = dt.Rows[0];
                PatientID = new Guid(row["PatientID"].ToString());
                ClinicID = new Guid(row["ClinicID"].ToString());
                FileDate = DateTime.Parse(row["FileDate"].ToString());
                FileNumber = row["FileNumber"].ToString();
                CreatedBy=row["CreatedBy"].ToString();
                CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                UpdatedBy = row["UpdatedBy"].ToString();
                UpdatedDateLocal = DateTime.Parse(row["UpdatedDate"].ToString());
                con.Close();
            }
            #endregion Constructors

        #region Fileproperty
 

        public Guid FileID
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
        public DateTime FileDate
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get { return CreatedDateLocal; }
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get { return UpdatedDateLocal; }
        }
        public string FileNumber
        {
            get;
            set;
        }
        #endregion Fileproperty

        #region File Methods

        #region AddFile
        public void AddFile()
        {
            dbConnection dcon = new dbConnection();

            try
            {
                dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = dcon.SQLCon;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertNewFile]";
                pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@FileNumber", SqlDbType.NVarChar, 50).Value = FileNumber;
                //pud.Parameters.Add("@image", SqlDbType.VarBinary).Value = image; 
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "AddFile";
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

        #endregion AddFile

        #region UpdateFile
        public void UpdateFile()
        {
            SqlConnection con = null;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "UpdateFile";
                pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@FileNumber", SqlDbType.NVarChar, 50).Value = FileNumber;
                //SqlParameter OutparamId = pud.Parameters.Add("@OutparamId", SqlDbType.SmallInt);
                //OutparamId.Direction = ParameterDirection.Output;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();
                if (int.Parse(Output.Value.ToString()) == -1)
                {
                    // ////not successfull
                    // var page = HttpContext.Current.CurrentHandler as Page;
                    //eObj.U
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateFile";
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
        #endregion UpdateFile

        #endregion File Methods

        #region Visit Class
        public class Visit
        {
            #region global

            ErrorHandling eObj = new ErrorHandling();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            public string Module = "Doctor/CaseFile/Visit";
            common cmn = new common();

            private DateTime CreatedDateLocal;
            private DateTime UpdatedDateLocal;

            #endregion global          

            #region Constructors
            public Visit(CaseFile caseFile) 
            {
                FileID = caseFile.FileID;
                ClinicID = caseFile.ClinicID;
            }
            public Visit()
            {
               VisitID = Guid.NewGuid();
               PrescriptionID = Guid.NewGuid();
            }
            public Visit(Guid VisitID)
            {
                this.VisitID = VisitID;
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetVisitDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                DataRow row = dt.NewRow();
                row = dt.Rows[0];
                FileID = new Guid(row["FileID"].ToString());
                DoctorID = new Guid(row["DoctorID"].ToString());
                ClinicID = new Guid(row["ClinicID"].ToString());
                Date = DateTime.Parse(row["Date"].ToString());
                Height = float.Parse(row["Height"].ToString());
                Weight = float.Parse(row["Weight"].ToString());
                Symptoms = row["Symptoms"].ToString();
                Bowel = row["Bowel"].ToString();
                Appettie = row["Appettie"].ToString();
                Micturation = row["Micturation"].ToString();
                Sleep = row["Sleep"].ToString();
                Cardiovascular = row["Cardiovascular"].ToString();
                Nervoussystem = row["Nervoussystem"].ToString();
                Musculoskeletal = row["Musculoskeletal"].ToString();
                Palloe = row["Palloe"].ToString();
                Icterus = row["Icterus"].ToString();
                Clubbing = row["Clubbing"].ToString();
                Cyanasis = row["Cyanasis"].ToString();           
                LymphGen = row["LymphGen"].ToString();
                Edima = row["Edima"].ToString();
                Diagnosys = row["Diagnosys"].ToString();
                Remarks = row["Remarks"].ToString();
                Pulse = row["Pulse"].ToString();
                Bp = row["Bp"].ToString();
                Tounge = row["Tounge"].ToString();
                Heart = row["Heart"].ToString();
                LymphClinic = row["LymphClinic"].ToString();
                RespRate = row["RespRate"].ToString();
                Others = row["Others"].ToString();
                if (row["PrescriptionID"] != DBNull.Value) { PrescriptionID = new Guid(row["PrescriptionID"].ToString()); }                
                CreatedBy=row["CreatedBy"].ToString();
                CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                UpdatedBy = row["UpdatedBy"].ToString();
                UpdatedDateLocal = DateTime.Parse(row["UpdatedDate"].ToString());
                con.Close();
            }
            #endregion Constructors

            #region VisitsProperty
            public Guid VisitID
            {
                get;
                set;
            }
            public Guid FileID
            {
                get;
                set;
            }
            public Guid DoctorID
            {
                get;
                set;
            }
            public Guid ClinicID
            {
                get;
                set;
            }
            public DateTime Date
            {
                get;
                set;
            }
            /// <summary>
            /// Personal details
            /// </summary>
            public float Height
            {
                get;
                set;
            }
            public float Weight
            {
                get;
                set;
            }
            public string Symptoms
            {
                get;
                set;
            }

            public string Bowel
            {
                get;
                set;
            }
            public string Appettie
            {
                get;
                set;
            }
            public string Micturation
            {
                get;
                set;
            }
            public string Sleep
            {
                get;
                set;
            }
            /// <summary>
            /// Systematic Examination
            /// </summary>
            public string Cardiovascular
            {
                get;
                set;
            }
            
            public string Nervoussystem
            {
                get;
                set;
            }
            
            public string Musculoskeletal
            {
                get;
                set;
            }
            /// <summary>
            /// General examination
            /// </summary>
            public string Palloe
            {
                get;
                set;
            }
            public string Icterus
            {
                get;
                set;
            }
            public string Clubbing
            {
                get;
                set;
            }
            public string Cyanasis
            {
                get;
                set;
            }
            public string LymphGen
            {
                get;
                set;
            }
            public string Edima
            {
                get;
                set;
            }
            /// <summary>
            /// Daignosys
            /// </summary>
           
            public string Diagnosys
            {
                get;
                set;
            }
            /// <summary>
            /// Remarks
            /// </summary>
            /// 
            public string Remarks
            {
                get;
                set;
            }
            /// <summary>
            /// Clinical Needs
            /// </summary>
            /// 
            public string Pulse
            {
                get;
                set;
            }
            public string Bp
            {
                get;
                set;
            }
            public string Tounge
            {
                get;
                set;
            }
            public string Heart
            {
                get;
                set;
            }
            public string LymphClinic
            {
                get;
                set;
            }
            public string RespRate
            {
                get;
                set;
            }
            public string Others
            {
                get;
                set;
            }
            public Guid PrescriptionID
            {
                get;
                set;
            }
            public string CreatedBy
            {
                get;
                set;
            }
            public DateTime CreatedDate
            {
                get { return CreatedDateLocal; }
            }
            public string UpdatedBy
            {
                get;
                set;
            }
            public DateTime UpdatedDate
            {
                get { return UpdatedDateLocal; }
            }
            #endregion Visitsproperty

            #region Visit Methods

            #region AddVisits
            public void AddVisits()
            {

                SqlConnection con = null;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand pud = new SqlCommand();
                    pud.Connection = con;
                    pud.CommandType = System.Data.CommandType.StoredProcedure;
                    pud.CommandText = "[InsertVisitList]";
                    pud.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                    pud.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;                   
                    pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                    pud.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@Height", SqlDbType.Real).Value =Height;
                    pud.Parameters.Add("@Weight", SqlDbType.Real).Value = Weight;
                    pud.Parameters.Add("@Symptoms", SqlDbType.NVarChar, 255).Value = Symptoms;
                    pud.Parameters.Add("@Bowel", SqlDbType.NVarChar, 255).Value = Bowel;
                    pud.Parameters.Add("@Appettie", SqlDbType.NVarChar, 255).Value = Appettie;
                    pud.Parameters.Add("@Micturation", SqlDbType.NVarChar, 255).Value = Micturation;
                    pud.Parameters.Add("@Sleep", SqlDbType.NVarChar, 255).Value = Sleep;
                    pud.Parameters.Add("@Cardiovascular", SqlDbType.NVarChar, 255).Value = Cardiovascular;
                    pud.Parameters.Add("@Nervoussystem", SqlDbType.NVarChar, 255).Value = Nervoussystem;
                    pud.Parameters.Add("@Musculoskeletal", SqlDbType.NVarChar, 255).Value = Musculoskeletal;
                    pud.Parameters.Add("@Palloe", SqlDbType.NVarChar, 255).Value = Palloe;
                    pud.Parameters.Add("@Icterus", SqlDbType.NVarChar, 255).Value = Icterus;
                    pud.Parameters.Add("@Clubbing", SqlDbType.NVarChar, 255).Value = Clubbing;
                    pud.Parameters.Add("@Cyanasis", SqlDbType.NVarChar, 255).Value = Cyanasis;
                    pud.Parameters.Add("@LymphGen",SqlDbType.NVarChar,255).Value=LymphGen;
                    pud.Parameters.Add("@Edima",SqlDbType.NVarChar,255).Value=Edima;                   
                    pud.Parameters.Add("@Diagnosys", SqlDbType.NVarChar, 255).Value = Diagnosys;
                    pud.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255).Value = Remarks;
                    pud.Parameters.Add("@Pulse", SqlDbType.NVarChar, 255).Value = Pulse;
                    pud.Parameters.Add("@Bp", SqlDbType.NVarChar, 255).Value = Bp;
                    pud.Parameters.Add("@Tounge", SqlDbType.NVarChar, 255).Value = Tounge;
                    pud.Parameters.Add("@Heart", SqlDbType.NVarChar, 255).Value = Heart;
                    pud.Parameters.Add("@LymphClinic", SqlDbType.NVarChar, 255).Value = LymphClinic;
                    pud.Parameters.Add("@RespRate", SqlDbType.NVarChar, 255).Value = RespRate;
                    pud.Parameters.Add("@Others", SqlDbType.NVarChar, 255).Value = Others;
                    pud.Parameters.Add("@PrescriptionID", SqlDbType.UniqueIdentifier).Value = PrescriptionID;
                    pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value =CreatedBy;
                    pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                    pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                   
                    SqlParameter Output = new SqlParameter();
                    Output.DbType = DbType.Int32;
                    Output.ParameterName = "@Status";
                    Output.Direction = ParameterDirection.Output;
                    pud.Parameters.Add(Output);
                    pud.ExecuteNonQuery();

                    if (Output.Value.ToString() == "")
                    {
                       // not successfull   

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
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "AddVisits";
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
            #endregion AddVisits

            #region GetVisitsGrid
            public void GetVisits()
            {

                if (VisitID == Guid.Empty)
                {
                    throw new Exception("VisitID Is Empty!!");
                }
                dbConnection dcon = null;
                try
                {
                   
                    DateTime now = DateTime.Now;
                    DataTable GridVisits = null;
                    dcon = new dbConnection();
                    dcon.GetDBConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = dcon.SQLCon;
                    cmd.CommandText = "GetVisitDetails";
                    cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    GridVisits = new DataTable();
                    adapter.Fill(GridVisits);
                    if(GridVisits.Rows.Count>0)
                    {
                        DataRow dr = GridVisits.Rows[0];
                        Height =float.Parse(dr["Height"].ToString());
                        Weight = float.Parse(dr["Weight"].ToString());
                        Symptoms = dr["Symptoms"].ToString();
                        Cardiovascular = dr["Cardiovascular"].ToString();
                        Nervoussystem = dr["Nervoussystem"].ToString();
                        Musculoskeletal = dr["Musculoskeletal"].ToString();
                        Palloe = dr["Palloe"].ToString();
                        Icterus = dr["Icterus"].ToString();
                        Clubbing = dr["Clubbing"].ToString();
                        Cyanasis = dr["Cyanasis"].ToString();
                        Bowel = dr["Bowel"].ToString();
                        Appettie = dr["Appettie"].ToString();
                        Micturation = dr["Micturation"].ToString();
                        Sleep = dr["Sleep"].ToString();
                        Diagnosys = dr["Diagnosys"].ToString();
                        Remarks = dr["Remarks"].ToString();
                        Pulse = dr["Pulse"].ToString();
                        Bp = dr["Bp"].ToString();
                        Tounge = dr["Tounge"].ToString();
                        Heart = dr["Heart"].ToString();
                        LymphGen = dr["LymphGen"].ToString();
                        LymphClinic = dr["LymphClinic"].ToString();
                        RespRate = dr["RespRate"].ToString();
                        Others = dr["Others"].ToString();
                        Date = Convert.ToDateTime((dr["Date"]));


                    }
                    
                   }
                catch (Exception ex)
                {
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "GetVisits";
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

            public DataTable GetGridVisits(Guid FileID)
            {
               
                if (FileID == Guid.Empty)
                {
                    throw new Exception("FileID Is Empty!!");
                }
                SqlConnection con = null;
                DataTable GridBindVisits = new DataTable();

                try
                {
                    DateTime now = DateTime.Now;
                 
                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("ViewVisitListUsingFileID", con);
                    cmd.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    GridBindVisits = new DataTable();
                    adapter.Fill(GridBindVisits);
                    con.Close();
                  
                }
                catch (Exception ex)
                {
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "GetVisitsGrid";
                    eObj.InsertError();
                }
                finally
                {
                    if (con != null)
                    {
                        con.Dispose();
                    }

                }
                return GridBindVisits;
            }


            /*--------- View Search Paging Of Visits ------------*/

            #region ViewAndFilterVisits

            public string ViewAndFilterVisits(string searchTerm, int pageIndex, int PageSize)
            {
                var xml = string.Empty;
                SqlConnection con = null;
                DataSet ds = null;
                SqlDataAdapter sda = null;
                try
                {
                    if (FileID != null && FileID != Guid.Empty)
                    {

                    DateTime now = DateTime.Now;
                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[ViewAndFilterVisits]";

                    cmd.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                    cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;

                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;



                    sda = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();
                    sda.SelectCommand = cmd;
                    ds = new DataSet();
                    sda.Fill(ds, "Visits");

                    //-----------Paging Section 

                    DataTable dt = new DataTable("Pager");
                    dt.Columns.Add("FILEID");
                    dt.Columns.Add("PageIndex");
                    dt.Columns.Add("PageSize");
                    dt.Columns.Add("RecordCount");
                    dt.Rows.Add();
                    dt.Rows[0]["FILEID"] = FileID;
                    dt.Rows[0]["PageIndex"] = pageIndex;
                    dt.Rows[0]["PageSize"] = PageSize;
                    dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                    ds.Tables.Add(dt);

                    xml = ds.GetXml();
                    }

                    else
                    {
                        ds = new DataSet();
                        DataTable dt = new DataTable("Pager");

                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();

                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = 0;
                        ds.Tables.Add(dt);

                        xml = ds.GetXml();
                    }


                }

                catch (Exception ex)
                {
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "ViewAndFilterVisits";
                    eObj.InsertError();
                }

                finally
                {
                    if (con != null)
                    {
                        con.Dispose();
                    }

                }
                return xml;

            }

            #endregion ViewAndFilterVisits



            #endregion GetVisitsGrid

            #region UpdateVisits
            /// <summary>
            /// Updates the Visits
            /// </summary>
            public void UpdateVisits()
            {
                SqlConnection con = null;
                try
                {
                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand pud = new SqlCommand();
                    pud.Connection = con;
                    pud.CommandType = System.Data.CommandType.StoredProcedure;
                    pud.CommandText = "UpdateVisits";
                    pud.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    pud.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@Height", SqlDbType.Real).Value = Height;
                    pud.Parameters.Add("@Weight", SqlDbType.Real).Value = Weight;
                    pud.Parameters.Add("@Symptoms", SqlDbType.NVarChar, 255).Value = Symptoms;
                    pud.Parameters.Add("@Bowel", SqlDbType.NVarChar, 255).Value = Bowel;
                    pud.Parameters.Add("@Appettie", SqlDbType.NVarChar, 255).Value = Appettie;
                    pud.Parameters.Add("@Micturation", SqlDbType.NVarChar, 255).Value = Micturation;
                    pud.Parameters.Add("@Sleep", SqlDbType.NVarChar, 255).Value = Sleep;
                    pud.Parameters.Add("@Cardiovascular", SqlDbType.NVarChar, 255).Value = Cardiovascular;
                    pud.Parameters.Add("@Nervoussystem", SqlDbType.NVarChar, 255).Value = Nervoussystem;
                    pud.Parameters.Add("@Musculoskeletal", SqlDbType.NVarChar, 255).Value = Musculoskeletal;
                    pud.Parameters.Add("@Palloe", SqlDbType.NVarChar, 255).Value = Palloe;
                    pud.Parameters.Add("@Icterus", SqlDbType.NVarChar, 255).Value = Icterus;
                    pud.Parameters.Add("@Clubbing", SqlDbType.NVarChar, 255).Value = Clubbing;
                    pud.Parameters.Add("@Cyanasis", SqlDbType.NVarChar, 255).Value = Cyanasis;
                    pud.Parameters.Add("@LymphGen", SqlDbType.NVarChar, 255).Value = LymphGen;
                    pud.Parameters.Add("@Edima", SqlDbType.NVarChar, 255).Value = Edima;
                    pud.Parameters.Add("@Diagnosys", SqlDbType.NVarChar, 255).Value = Diagnosys;
                    pud.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255).Value = Remarks;
                    pud.Parameters.Add("@Pulse", SqlDbType.NVarChar, 255).Value = Pulse;
                    pud.Parameters.Add("@Bp", SqlDbType.NVarChar, 255).Value = Bp;
                    pud.Parameters.Add("@Tounge", SqlDbType.NVarChar, 255).Value = Tounge;
                    pud.Parameters.Add("@Heart", SqlDbType.NVarChar, 255).Value = Heart;
                    pud.Parameters.Add("@LymphClinic", SqlDbType.NVarChar, 255).Value = LymphClinic;
                    pud.Parameters.Add("@RespRate", SqlDbType.NVarChar, 255).Value = RespRate;
                    pud.Parameters.Add("@Others", SqlDbType.NVarChar, 255).Value = Others;
                    pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                    pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    //SqlParameter OutparamId = pud.Parameters.Add("@OutparamId", SqlDbType.SmallInt);
                    //OutparamId.Direction = ParameterDirection.Output;
                    SqlParameter Output = new SqlParameter();
                    Output.DbType = DbType.Int32;
                    Output.ParameterName = "@Status";
                    Output.Direction = ParameterDirection.Output;
                    pud.Parameters.Add(Output);
                    pud.ExecuteNonQuery();
                    if (int.Parse(Output.Value.ToString()) != 1)
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
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "UpdateVisits";
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
            #endregion UpdateVisits

            #region SearchPatientWithName
            public DataTable SearchPatientWithName()
            {
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SearchPatientWithName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                return dt;

            }
            #endregion SearchPatientWithName

            #region Get Visit List with Name for Mobile
            /// <summary>
            /// Get Visit List with Name for Mobile app to upload image
            /// </summary>
            /// <returns>Datatable with details from table</returns>
            public DataTable GetVisitListforMobile()
            {
                DataTable dt = new DataTable();
                SqlConnection con = null;
                SqlDataAdapter daObj;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();

                    SqlCommand cmdSelect = new SqlCommand("GetVisitListForMobile", con);
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                    //cmdSelect.Parameters.AddWithValue("@DoctorID", DoctorID);

                    daObj = new SqlDataAdapter(cmdSelect);
                    
                    daObj.Fill(dt);

                }
                catch (SqlException ex)
                {
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "GetVisitListforMobile";
                    eObj.InsertError();
                }
                finally
                {
                    con.Close();
                }

                return dt;
            }
            #endregion Get Visit List with Name for Mobile

            #region Search_VisitList_from Mobile
            /// <summary>
            /// Get Visit List with Name for Mobile app to upload image
            /// </summary>
            /// <returns>Datatable with details from table</returns>
            public DataTable GetVisitSearchforMobile(string stringsearch)
            {
                DataTable dt = new DataTable();
                SqlConnection con = null;
                SqlDataAdapter daObj;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();

                    SqlCommand cmdSelect = new SqlCommand("SearchVisitsfromMobile", con);
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                    cmdSelect.Parameters.AddWithValue("@clinicid", ClinicID);
                    cmdSelect.Parameters.AddWithValue("@stringsearch", stringsearch);

                    daObj = new SqlDataAdapter(cmdSelect);

                    daObj.Fill(dt);

                }
                catch (SqlException ex)
                {
                    UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                    eObj.Description = ex.Message;
                    eObj.Module = Module;
                    eObj.UserID = UA.UserID;
                    eObj.Method = "GetVisitSearchforMobile";
                    eObj.InsertError();
                }
                finally
                {
                    con.Close();
                }

                return dt;
            }
            #endregion  Search_VisitList_from Mobile

            #endregion Visit Methods

            #region Visit Attachment Class
            public class VisitAttachment
                {
                    #region global

                    ErrorHandling eObj = new ErrorHandling();
                    UIClasses.Const Const = new UIClasses.Const();
                    ClinicDAL.UserAuthendication UA;
                    public string Module = "Doctor/CaseFile/Visit/VisitAttachment";

                    private DateTime CreatedDateLocal;

                    #endregion global                  

                    #region Constructors
                    public VisitAttachment(Visit visit){
                        VisitID = visit.VisitID;
                        ClinicID = visit.ClinicID;
                    }
                    public VisitAttachment()
                    {
                        AttachID = Guid.NewGuid();
                    }
                    public VisitAttachment(Guid AttachID)
                    {
                        this.AttachID = AttachID;
                        DataTable dt = null;
                        SqlConnection con = null;
                        dbConnection dcon = new dbConnection();
                        con = dcon.GetDBConnection();
                        SqlCommand cmd = new SqlCommand("GetVisitAttachDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttachID", SqlDbType.UniqueIdentifier).Value = AttachID;
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                        DataRow row = dt.NewRow();
                        row = dt.Rows[0];
                        VisitID = new Guid(row["VisitID"].ToString());
                        ClinicID = new Guid(row["ClinicID"].ToString());
                        Description = row["Description"].ToString();
                        Attachment = row["Attachment"];
                        Name = row["Name"].ToString();
                        this.Type = row["Type"].ToString();
                        Size=row["Size"].ToString();
                        CreatedBy=row["CreatedBy"].ToString();
                        CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                        con.Close();
                    }
                    #endregion Constructors

                    #region VisitAttachmentsProperty
                    public Guid AttachID
                    {
                        get;
                        set;
                    }
                    public Guid VisitID
                    {
                        get;
                        set;
                    }
                    public Guid ClinicID
                    {
                        get;
                        set;
                    }
                    public string Description
                    {
                        get;
                        set;
                    }
                    public object Attachment
                    {
                        get;
                        set;
                    }
                    public string Name
                    {
                        get;
                        set;
                    }
                    public string Type
                    {
                        get;
                        set;
                    }
                    public string Size
                    {
                        get;
                        set;
                    }
                    public string CreatedBy
                    {
                        get;
                        set;
                    }
                    public DateTime CreatedDate
                    {
                        get { return CreatedDateLocal;}
                    }
                    #endregion VisitAttachmentsProperty
            
                    #region VisitAttachment methods

                    #region FileAttachment
                    public int InsertFileAttachment(Boolean isFromMobile = false, string userName = "")
                    {
                        if (AttachID == null)
                        {
                            throw new Exception("AttachID propery is not set!!");
                        }
                        if(VisitID==null){
                            throw new Exception("VisitID propery is not set!!");
                        }
                        if (ClinicID == null)
                        {
                            throw new Exception("ClinicID propery is not set!!");
                        }
                        int result = 0;
                        SqlConnection con = null;
                        try
                        {
                            dbConnection dcon = new dbConnection();
                            con = dcon.GetDBConnection();
                            UIClasses.Const Const = new UIClasses.Const();

                            SqlCommand cmdInsert = new SqlCommand("FileAttachmentInsert", con);
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            if (isFromMobile)
                            {
                                cmdInsert.Parameters.AddWithValue("@ClinicID", ClinicID);
                                cmdInsert.Parameters.AddWithValue("@CreatedBy", userName);
                            }
                            else
                            {
                                ClinicDAL.UserAuthendication UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                                cmdInsert.Parameters.AddWithValue("@ClinicID", UA.ClinicID);
                                cmdInsert.Parameters.AddWithValue("@CreatedBy", UA.userName);
                            }
                            cmdInsert.Parameters.AddWithValue("@AttachID", AttachID);
                            cmdInsert.Parameters.AddWithValue("@VisitID", VisitID);
                            cmdInsert.Parameters.AddWithValue("@Description", Description);
                            cmdInsert.Parameters.AddWithValue("@Attachment", Attachment);
                            cmdInsert.Parameters.AddWithValue("@Name", Name);
                            cmdInsert.Parameters.AddWithValue("@Type", Type);
                            cmdInsert.Parameters.AddWithValue("@Size", Size);
                            cmdInsert.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            result = cmdInsert.ExecuteNonQuery();
                            if (!isFromMobile)
                            {
                                var page = HttpContext.Current.CurrentHandler as Page;
                                eObj.InsertionSuccessMessage(page, "Data Inserted Successfully..!!!!");
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (!isFromMobile)
                            {
                                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                                eObj.Description = ex.Message;
                                eObj.Module = Module;
                                eObj.UserID = UA.UserID;
                                eObj.Method = "InsertFileAttachment";
                                eObj.InsertError();
                            }
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                        }
                        return result;

                    }
                    #endregion FileAttachment

                    #region GetAttachID using VisitID
                    public DataTable GetAttachment()
                    {

                        if (VisitID == Guid.Empty)
                        {
                            throw new Exception("VisitID Is Empty!!");
                        }
                        SqlConnection con = null;
                        DataTable dt = new DataTable();

                        try
                        {
                            DateTime now = DateTime.Now;

                            dbConnection dcon = new dbConnection();
                            con = dcon.GetDBConnection();
                            SqlCommand cmd = new SqlCommand("GetAttachmentUsingVisitID", con);
                            cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = cmd;
                            dt = new DataTable();
                            adapter.Fill(dt);
                            con.Close();

                        }
                        catch (Exception ex)
                        {
                            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                            eObj.Description = ex.Message;
                            eObj.Module = Module;
                            eObj.UserID = UA.UserID;
                            eObj.Method = "GetAttachment";
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

                    public DataSet GetVisitAttachment()
                    {

                        if (VisitID == Guid.Empty)
                        {
                            throw new Exception("VisitID Is Empty!!");
                        }
                        SqlConnection con = null;
                        DataSet ds = new DataSet();

                        try
                        {
                            DateTime now = DateTime.Now;

                            dbConnection dcon = new dbConnection();
                            con = dcon.GetDBConnection();
                            SqlCommand cmd = new SqlCommand("GetAttachmentUsingVisitID", con);
                            cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = cmd;
                            ds = new DataSet();
                            adapter.Fill(ds);
                            con.Close();

                        }
                        catch (Exception ex)
                        {
                            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                            eObj.Description = ex.Message;
                            eObj.Module = Module;
                            eObj.UserID = UA.UserID;
                            eObj.Method = "GetVisitAttachment";
                            eObj.InsertError();
                        }
                        finally
                        {
                            if (con != null)
                            {
                                con.Dispose();
                            }

                        }
                        return ds;
                    }


                    #endregion GetAttachID using VisitID
                    
                    #region GetAttachmentImages

                    public byte[] GetAttachmentImages()
                    {


                        dbConnection dcon = null;
                        SqlCommand cmd = null;
                        SqlDataReader rd = null;
                        byte[] imageproduct = null;
                        try
                        {
                            dcon = new dbConnection();
                            dcon.GetDBConnection();
                            cmd = new SqlCommand();
                            cmd.Connection = dcon.SQLCon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[GetAttachmentImages]";
                            cmd.Parameters.Add("@AttachID", SqlDbType.UniqueIdentifier).Value = AttachID;
                            rd = cmd.ExecuteReader();
                            if ((rd.Read()) && (rd.HasRows) && (rd["Attachment"] != DBNull.Value))
                            {
                                imageproduct = (byte[])rd["Attachment"];
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        finally
                        {
                            if (dcon.SQLCon != null)
                            {
                                rd.Close();
                                dcon.DisconectDB();
                            }
                        }
                        return imageproduct;

                    }
                    #endregion GetBoutiqueLogo

                    #region Delete Visit Attachment By AttachID

                    public void DeletevisitAttachmentByAttachID()
                    {
                        dbConnection dcon = null;

                        try
                        {
                            dcon = new dbConnection();
                            dcon.GetDBConnection();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = dcon.SQLCon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "[DeleteVisitAttachmentByID]";

                            cmd.Parameters.Add("@AttachID", SqlDbType.UniqueIdentifier).Value = AttachID;

                            cmd.ExecuteNonQuery();

                        }

                        catch (Exception ex)
                        {
                            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                            eObj.Description = ex.Message;
                            eObj.Module = Module;
                            eObj.UserID = UA.UserID;
                            eObj.Method = "DeletevisitAttachmentByAttachID";

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

                    #endregion Delete Visit Attachment By AttachID

                    #endregion VisitAttachment methods
                }
            #endregion Visit Attachment Class
        }
        #endregion Visit Class
 
    }
       
}