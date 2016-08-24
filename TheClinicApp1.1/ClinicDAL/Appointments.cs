using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class Appointments
    {
        #region global
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string Module = "Appointments";
        #endregion global

        #region Appointment Properties
        public string AppointmentID
        {
            get;
            set;
        }
        public string DoctorID
        {
            get;
            set;

        }
        public string ScheduleID
        {
            get;
            set;
        }
        public string PatientID
        {
            get;
            set;
        }
        public string AppointmentDate
        {
            get;
            set;
        }
        public string Mobile
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Boolean IsRegistered
        {
            get;
            set;
        }
        public int appointmentno
        {
            get;
            set;
        }

        public string AllottingTime
        {
            get;
            set;
        }
        public Boolean IsPresent
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string Location
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
        public int AppointmentStatus
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        #endregion Appointment Properties

        #region Appointment Methods

        #region InsertPatientAppointment
        public Int16 InsertPatientAppointment()  
        {
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null, OutparameterAppointmentID = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if(ScheduleID == "")
            {
                throw new Exception("ScheduleID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertPatientAppointment";
                if(PatientID!=null)
                {
                    cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PatientID);
                }
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ScheduleID);
                cmd.Parameters.Add("@AppointmentDate", SqlDbType.DateTime).Value = AppointmentDate;
                cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar,20).Value =Mobile;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar,25).Value = Name;
                cmd.Parameters.Add("@IsRegistered", SqlDbType.Bit).Value = IsRegistered;
                cmd.Parameters.Add("@appointmentno", SqlDbType.Int).Value = appointmentno;
                cmd.Parameters.Add("@AllottingTime", SqlDbType.NVarChar,10).Value = AllottingTime;
                cmd.Parameters.Add("@AppointmentStatus", SqlDbType.Int).Value = AppointmentStatus;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar,255).Value = CreatedBy;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                outParameter = cmd.Parameters.Add("@InsertStatus", SqlDbType.SmallInt);
                outParameter.Direction = ParameterDirection.Output;
                OutparameterAppointmentID = cmd.Parameters.Add("@OutPatientAppointmentID", SqlDbType.UniqueIdentifier);
                OutparameterAppointmentID.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                AppointmentID = OutparameterAppointmentID.Value.ToString();
                status = outParameter.Value.ToString();
                if(status=="1")
                {
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
                eObj.Method = "InsertPatientAppointment";
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
        #endregion InsertPatientAppointment

        #region UpdatePatientAppointment
        public Int16 UpdatePatientAppointment()
        {

            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (AppointmentID == "")
            {
                throw new Exception("AppointmentID is Empty!!");
            }
            if (PatientID == "")
            {
                throw new Exception("PatientID is Empty!!");
            }
            if(ScheduleID =="")
            {
                throw new Exception("ScheduleID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdatePatientAppointment";
                cmd.Parameters.Add("@AppointmentID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(AppointmentID);
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PatientID);
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ScheduleID);
                cmd.Parameters.Add("@AppointmentDate", SqlDbType.DateTime).Value = AppointmentDate;
                cmd.Parameters.Add("@Mobile", SqlDbType.Int).Value = Mobile;
                cmd.Parameters.Add("@Name", SqlDbType.Bit).Value = Name;
                cmd.Parameters.Add("@IsRegistered", SqlDbType.Bit).Value = IsRegistered;
                cmd.Parameters.Add("@appointmentno", SqlDbType.Int).Value = appointmentno;
                cmd.Parameters.Add("@AllottingTime", SqlDbType.Decimal).Value = AllottingTime;
                cmd.Parameters.Add("@AppointmentStatus", SqlDbType.Int).Value = AppointmentStatus;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
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
                eObj.Method = "UpdatePatientAppointment";
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
        #endregion UpdatePatientAppointment

        #region CancelAppointment
        public Int16 CancelAppointment()
        {

            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (AppointmentID == "")
            {
                throw new Exception("AppointmentID is Empty!!");
            }
          
           
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[CancelPatientAppointment]";
                cmd.Parameters.Add("@AppointmentID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(AppointmentID);
              
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
               
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value =System.DateTime.Now;
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
                eObj.Method = "CancelAppointment";
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
        #endregion CancelAppointment

        #region GetAllPatientAppointmentDetailsByClinicID
        public DataSet GetAllPatientAppointmentDetailsByClinicID()
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
                cmd.CommandText = "[GetAllPatientAppointmentDetailsByClinicID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
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
                eObj.Method = "GetAllPatientAppointmentDetailsByClinicID";
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

        #endregion GetAllPatientAppointmentDetailsByClinicID

        #region GetAllPatientAppointmentDetailsBetweenDates
        public DataSet GetAllPatientAppointmentDetailsBetweenDates()
        {
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if(DoctorID =="")
            {
                throw new Exception("DoctorID is Empty!!");
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
                cmd.CommandText = "[GetAllPatientAppointmentDetailsBetweenDates]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(this.DoctorID);
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
                eObj.Method = "GetAllPatientAppointmentDetailsBetweenDates";
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
        #endregion GetAllPatientAppointmentDetailsBetweenDates

        #region Today's Patient: Appointment View Search Paging

        public string ViewAndFilterTodayAppointments(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterTodayPatientAppointments";


                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                //cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = "2016-08-11";//now.ToString("yyyy-MM-dd");
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "TodayAppointments");

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
                eObj.Method = "ViewAndFilterTodayAppointments";
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

        #endregion Today's Patient: Appointment View Search Paging

       


        #region AllotedPatientAbsentUpdate
        public Int16 AllotedPatientAbsentUpdate()
        {
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlParameter outParameter = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (AppointmentID == "")
            {
                throw new Exception("AppointmentID is Empty!!");
            }
            if (PatientID == "")
            {
                throw new Exception("PatientID is Empty!!");
            }
            if (ScheduleID == "")
            {
                throw new Exception("ScheduleID is Empty!!");
            }
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdatePatientAppointmentStatus]";
                cmd.Parameters.Add("@AppointmentID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(AppointmentID);
                cmd.Parameters.Add("@AppointmentStatus", SqlDbType.Int).Value = AppointmentStatus;
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
                eObj.Method = "AllotedPatientAbsentUpdate";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
           
            return Int16.Parse(outParameter.Value.ToString());
        }

        #endregion AllotedPatientAbsentUpdate

        #region MobileMethods


        #region GetAppointmentDatesandPatientCount
        public DataTable GetAppointmentDatesandPatientCount()
        {
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataTable dt = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetAppointmentDatesandCount]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                sda.SelectCommand = cmd;
                dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAppointmentDatesandCount";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return dt;
        }

        #endregion GetAllPatientAppointmentDetailsByClinicID

        #region GetAppointmentPatientDetails
        public DataTable GetAppointmentPatientDetails()
        {
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataAdapter sda = null;
            DataTable dt = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                sda = new SqlDataAdapter();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetAppointmentPatientsDetails]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@AppointmentDate", SqlDbType.Date).Value = AppointmentDate;
                sda.SelectCommand = cmd;
                dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetAppointmentPatientDetails";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return dt;
        }

        #endregion GetAppointmentPatientDetails

        #endregion MobileMethods

          #region GetAppointedPatientDetails
         public DataSet GetAppointedPatientDetails()
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            if (ClinicID == "")
            {
                throw new Exception("ClinicID is Empty!!");
            }
            if (AppointmentID == "")
            {
                throw new Exception("AppointmentID is Empty!!");
            }
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetAppointedPatientDetailsByAppointmentID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(AppointmentID);
              
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
                eObj.Method = "GetAppointedPatientDetails";
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
        #endregion GetAppointedPatientDetails

         #region GetAppointedPatientDetailsByScheudleID
         public DataSet GetAppointedPatientDetailsByScheudleID()
         {
             SqlConnection con = null;
             DataSet ds = null;
             SqlDataAdapter sda = null;
             if (ClinicID == "")
             {
                 throw new Exception("ClinicID is Empty!!");
             }
             if (ScheduleID == "")
             {
                 throw new Exception("AppointmentID is Empty!!");
             }
             try
             {
                 dbConnection dcon = new dbConnection();
                 con = dcon.GetDBConnection();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = con;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[GetAppointedPatientDetailsByScheudleID]";
                 cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                 cmd.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ScheduleID);

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
                 eObj.Method = "GetAppointedPatientDetailsByScheudleID";
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
         #endregion GetAppointedPatientDetailsByScheudleID

        #endregion Appointment Methods



    }
}