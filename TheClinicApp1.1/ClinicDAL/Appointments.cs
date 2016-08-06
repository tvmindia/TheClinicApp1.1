using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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
        public int Mobile
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

        public decimal AllottingTime
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
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PatientID);
                cmd.Parameters.Add("@ScheduleID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ScheduleID);
                cmd.Parameters.Add("@AppointmentDate", SqlDbType.DateTime).Value = AppointmentDate;
                cmd.Parameters.Add("@Mobile", SqlDbType.Int).Value = Mobile;
                cmd.Parameters.Add("@Name", SqlDbType.Bit).Value = Name;
                cmd.Parameters.Add("@IsRegistered", SqlDbType.Bit).Value = IsRegistered;
                cmd.Parameters.Add("@appointmentno", SqlDbType.Int).Value = appointmentno;
                cmd.Parameters.Add("@AllottingTime", SqlDbType.Decimal).Value = AllottingTime;
                cmd.Parameters.Add("@IsPresent", SqlDbType.Bit).Value = IsPresent;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = Location;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar,255).Value = CreatedBy;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                outParameter = cmd.Parameters.Add("@InsertStatus", SqlDbType.SmallInt);
                outParameter.Direction = ParameterDirection.Output;
                OutparameterAppointmentID = cmd.Parameters.Add("@OutPatientAppointmentID", SqlDbType.UniqueIdentifier);
                OutparameterAppointmentID.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                AppointmentID = OutparameterAppointmentID.Value.ToString();
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
                cmd.Parameters.Add("@IsPresent", SqlDbType.Bit).Value = IsPresent;
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
                cmd.CommandText = "[]";
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



        #endregion Appointment Methods



    }
}