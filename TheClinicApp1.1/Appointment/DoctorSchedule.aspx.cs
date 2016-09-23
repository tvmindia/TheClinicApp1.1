
#region CopyRight

//Author      : SHAMILA T P
//Created Date: Aug-8-2016

#endregion CopyRight

#region Included Namespaces

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;


#endregion Included Namespaces

namespace TheClinicApp1._1.Appointment
{
    public partial class DoctorSchedule : System.Web.UI.Page
    {
        #region Global Variables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        #endregion Global Variables

        #region Event Properties
        public class Event
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
        }
        #endregion Event Properties

        #region Methods

        #region GetAllDoctorScheduleDetailsByDate
        [System.Web.Services.WebMethod]
        public static string GetAllDoctorScheduleDetailsByDate(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (UA != null)
            {
                DataSet ds = null;

                DocObj.ClinicID = UA.ClinicID.ToString();
                ds = DocObj.GetAllDoctorScheduleDetailsByDate();

                //Converting to Json
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            childRow.Add(col.ColumnName, row[col].ToString());
                        }
                        parentRow.Add(childRow);
                    }
                }
                return jsSerializer.Serialize(parentRow);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAllDoctorScheduleDetailsByDate

        #region GetDoctorScheduleDetailsByDocScheduleID
        [System.Web.Services.WebMethod]
        public static string GetDoctorScheduleDetailsByDocScheduleID(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (UA != null)
            {
                DataSet ds = null;
                ds = DocObj.GetDoctorScheduleDetailsByDocScheduleID();
                //Converting to Json
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            childRow.Add(col.ColumnName, row[col].ToString());
                        }
                        parentRow.Add(childRow);
                    }
                }
                return jsSerializer.Serialize(parentRow);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetDoctorScheduleDetailsByDocScheduleID

        #region Bind Dropdown
        public void BindDoctorDropdown()
        {
            TokensBooking tokenObj = new TokensBooking();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            tokenObj.ClinicID = Convert.ToString(UA.ClinicID);
            //binding the values of doctor dropdownlist
            DataSet ds = tokenObj.DropBindDoctorsName();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDoctor.Items.Clear();
                ddlDoctor.DataSource = ds.Tables[0];
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataBind();
                if (ddlDoctor.Items.Count != 1)//checking number of doctors.if there is only one doctor, no need of select 
                {
                    ddlDoctor.Items.Insert(0, "-- Select Doctor --");
                }
                else{
                    if (ds.Tables[0].Rows.Count ==0)
                {
                  hdnDoctorID.Value =  ds.Tables[0].Rows[0]["DoctorID"].ToString();
                }
                }
               

            }

        }

        #endregion Bind Dropdown

        #region GetDoctorScheduleDetailsByDoctorID (Avoiding Cancelled Events)
        
        [System.Web.Services.WebMethod]
        public static string GetDoctorScheduleDetailsByDoctorID(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                DocObj.ClinicID = UA.ClinicID.ToString();
                ds = DocObj.GetDoctorScheduleDetailsByDoctorID();

                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i <= count - 1; i++)
                {
                    events.Add(new Event()
                    {
                        id = ds.Tables[0].Rows[i]["event_id"].ToString(),
                       // title = ds.Tables[0].Rows[i]["title"].ToString(),
                   //    title = "",
                        start = ds.Tables[0].Rows[i]["event_start"].ToString(),
                        end = ds.Tables[0].Rows[i]["event_end"].ToString(),
                        StartTime = ds.Tables[0].Rows[i]["Starttime"].ToString(),
                        EndTime = ds.Tables[0].Rows[i]["Endtime"].ToString()
                    });
                }

            }

            return JsonConvert.SerializeObject(events);
           
        }
        #endregion GetDoctorScheduleDetailsByDoctorID

        #region GetDoctorScheduleDetailsByDoctorID (All :Including cancelled events)

        [System.Web.Services.WebMethod]
        public static string GetAllScheduleDetailsOfDoctor(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                DocObj.ClinicID = UA.ClinicID.ToString();
                ds = DocObj.GetAllSchedulesByDoctorID();

                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                //    {
                //        if (i == 0)
                //        {
                //            break;
                //        }
                //        for (int j = i - 1; j >= 0; j--)
                //        {
                //            if (dt.Rows[i]["AvailableDate"].ToString() == dt.Rows[j]["AvailableDate"].ToString())
                //            {
                //                dt.Rows[i].Delete();
                //                break;
                //            }
                //        }
                //    }
                //    dt.AcceptChanges();
                //}


                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            childRow.Add(col.ColumnName, row[col].ToString());
                        }
                        parentRow.Add(childRow);
                    }
                }
                return jsSerializer.Serialize(parentRow);
            }

            return jsSerializer.Serialize("");

        }
        #endregion GetDoctorScheduleDetailsByDoctorID

        #region GetRegularScheduleOFDoctor

        [System.Web.Services.WebMethod]
        public static string GetRegularScheduleOFDoctor(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                DocObj.ClinicID = UA.ClinicID.ToString();
                ds = DocObj.GetRegularScheduleOFDoctor();

                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            childRow.Add(col.ColumnName, row[col].ToString());
                        }
                        parentRow.Add(childRow);
                    }
                }
                return jsSerializer.Serialize(parentRow);
            }

            return jsSerializer.Serialize("");

        }
        #endregion GetRegularScheduleOFDoctor

        #region GetAllDoctorsScheduledBetweenDates
        [System.Web.Services.WebMethod]
        public static string GetAllDoctorsScheduledBetweenDates(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (UA != null)
            {
                DataSet ds = null;
                ds = DocObj.GetAllDoctorsScheduledBetweenDates();
                //Converting to Json
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in ds.Tables[0].Columns)
                        {
                            childRow.Add(col.ColumnName, row[col].ToString());
                        }
                        parentRow.Add(childRow);
                    }
                }
                return jsSerializer.Serialize(parentRow);
            }
            return jsSerializer.Serialize("");
        }

        #endregion GetAllDoctorsScheduledBetweenDates

        #region InsertDoctorSchedule
        [System.Web.Services.WebMethod]

        public static string InsertDoctorSchedule(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {

            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {
                        DocObj.ClinicID = UA.ClinicID.ToString();
                        DocObj.CreatedBy = UA.userName;
                        DocObj.status = DocObj.InsertDoctorSchedule().ToString();
                    }
                }
                else
                {
                    //redirect to login page

                    // return "NullUA";

                }
            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertDoctorSchedule";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(DocObj);
        }
        #endregion InsertDoctorSchedule

        #region UpdateDoctorSchedule
        [System.Web.Services.WebMethod]
        public static string UpdateDoctorSchedule(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            bool isSccheduleIDUsed = false;
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {
                        DocObj.ClinicID = UA.ClinicID.ToString();
                        DocObj.UpdatedBy = UA.userName;

                        if (DocObj.StartTimeOnEdit != DocObj.StartTime || DocObj.EndTimeOnEdit != DocObj.EndTime) // Validation is only required if time has changed
                        {

                            int appointmentMinutes = 0;
                            int patientLimit = 0;
                            string startAppointment = "";
                            string endAppointment = "";
                            string startDuration = "";
                            string endDuration = "";
                            TimeSpan duration = new TimeSpan();

                            DataSet ds = null;
                            ds = DocObj.GetDoctorAvailability();
                            int count = ds.Tables[0].Rows.Count;
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                startAppointment = ds.Tables[0].Rows[0]["Starttime"].ToString();
                                endAppointment = ds.Tables[0].Rows[0]["Endtime"].ToString();
                                patientLimit = Convert.ToInt32(ds.Tables[0].Rows[0]["PatientLimit"].ToString());
                                string endHour = endAppointment.Split(':')[0];
                                string startHour = startAppointment.Split(':')[0];
                                if (endHour == "24")
                                {

                                    endDuration = "23:59";
                                }
                                if (startHour == "24")
                                {
                                    startDuration = "23:59";
                                }
                                if (endDuration != "")
                                {
                                    duration = DateTime.Parse(endDuration).Subtract(DateTime.Parse(startAppointment));
                                    int endMinute = Convert.ToInt32(endAppointment.Split(':')[1]) + 1;
                                    int startMinute = Convert.ToInt32(startAppointment.Split(':')[1]);
                                    int totalminute = endMinute - startMinute;
                                    totalminute = Math.Abs(totalminute);
                                    string time = "00:" + totalminute;
                                    TimeSpan ts = TimeSpan.Parse(time);
                                    duration = duration.Add(ts);

                                }
                                else if (startDuration != "")
                                {
                                    duration = DateTime.Parse(endAppointment).Subtract(DateTime.Parse(startDuration));
                                    int endMinute = Convert.ToInt32(endAppointment.Split(':')[1]) + 1;
                                    int startMinute = Convert.ToInt32(startAppointment.Split(':')[1]);
                                    int totalminute = endMinute - startMinute;
                                    totalminute = Math.Abs(totalminute);
                                    string time = "00:" + totalminute;
                                    TimeSpan ts = TimeSpan.Parse(time);
                                    duration = duration.Add(ts);
                                }
                                else if (startDuration != "" && endDuration != "")
                                {
                                    duration = DateTime.Parse(endDuration).Subtract(DateTime.Parse(startDuration));
                                    int endMinute = Convert.ToInt32(endAppointment.Split(':')[1]) + 1;
                                    int startMinute = Convert.ToInt32(startAppointment.Split(':')[1]);
                                    int totalminute = endMinute - startMinute;
                                    totalminute = Math.Abs(totalminute);
                                    string time = "00:" + totalminute;
                                    TimeSpan ts = TimeSpan.Parse(time);
                                    duration = duration.Add(ts);
                                }
                                else
                                {
                                    duration = DateTime.Parse(endAppointment).Subtract(DateTime.Parse(startAppointment));
                                }


                                appointmentMinutes = Convert.ToInt32(duration.TotalMinutes);
                                appointmentMinutes = appointmentMinutes / patientLimit;

                                Appointments AppointObj = new Appointments();

                                DataSet dsAllotedStartTimes = null;
                                AppointObj.ClinicID = UA.ClinicID.ToString();
                                AppointObj.ScheduleID = DocObj.DocScheduleID;
                                dsAllotedStartTimes = AppointObj.GetAppointedPatientDetailsByScheudleID();

                                //   duration = duration.ToString("HH:mm");

                                if (dsAllotedStartTimes.Tables[0].Rows.Count > 0) // -- Schedule is having Appoinments
                                {

                                    // --- Check whether Appoinment times are between the new scheduled time

                                    int minIndex = 0;
                                    int maxIndex = dsAllotedStartTimes.Tables[0].Rows.Count-1;

                                    string SchedduleStartTime = DocObj.correctStartTime(DocObj.StartTime);
                                    string ScheduleEndTime = DocObj.correctStartTime(DocObj.EndTime);
                                    int SchedduleStartTimeHr = Convert.ToInt32(SchedduleStartTime.Split(':')[0]);
                                    int SchedduleStartTimeMin = Convert.ToInt32(SchedduleStartTime.Split(':')[1]);
                                    int SchedduleEndTimeHr = Convert.ToInt32(ScheduleEndTime.Split(':')[0]);
                                    int SchedduleEndTimeMin = Convert.ToInt32(ScheduleEndTime.Split(':')[1]);

                                    string AllotedMinStarttime = dsAllotedStartTimes.Tables[0].Rows[minIndex]["AllottingTime"].ToString();
                                    AllotedMinStarttime = AllotedMinStarttime.Substring(0, AllotedMinStarttime.LastIndexOf(":"));
                                    int AllotedMinStarttimeHr = Convert.ToInt32(AllotedMinStarttime.Split(':')[0]);
                                    int AllotedEndStarttimeMin = Convert.ToInt32(AllotedMinStarttime.Split(':')[1]);
                                  
                                    string AllotedMaxEndTime = DateTime.Today.Add(TimeSpan.Parse(dsAllotedStartTimes.Tables[0].Rows[maxIndex]["AllottingTime"].ToString()).Add(new TimeSpan(0, appointmentMinutes, 0))).ToString("hh:mm");
                                    int AllotedMaxEndtimeHr = Convert.ToInt32(AllotedMaxEndTime.Split(':')[0]);
                                    int AllotedMaxEndtimeMin = Convert.ToInt32(AllotedMaxEndTime.Split(':')[1]);

                                    if (AllotedMinStarttimeHr < SchedduleStartTimeHr || AllotedMaxEndtimeHr > SchedduleEndTimeHr)
                                    {
                                        isSccheduleIDUsed = true;
                                    }
                                    if (AllotedMinStarttimeHr == SchedduleStartTimeHr && isSccheduleIDUsed == false)
                                    {
                                        if (AllotedEndStarttimeMin < SchedduleStartTimeMin)
                                        {
                                            isSccheduleIDUsed = true;
                                        }
                                    }

                                    if (AllotedMaxEndtimeHr == SchedduleEndTimeHr && isSccheduleIDUsed == false)
                                    {
                                        if (AllotedMaxEndtimeMin > SchedduleEndTimeMin)
                                        {
                                            isSccheduleIDUsed = true;
                                            
                                        }
                                    }

                                }
                            }

                            //DataSet dsAppointedpatients = DocObj.GetAllPatientDetails();
                            //int NoOfPatients = dsAppointedpatients.Tables[0].Rows.Count;

                            //if (NoOfPatients > 0)
                            //{
                            //    isSccheduleIDUsed = true;
                            //}
                        }
                        //  isSccheduleIDUsed = DocObj.CheckDoctorScheduleAllotedForPatientAppointment();

                        if (isSccheduleIDUsed == false)
                        {
                            DocObj.status = DocObj.UpdateDoctorSchedule().ToString();
                        }
                        else
                        {
                            DocObj.status = "0";
                        }

                     
                    }
                }
                else
                {
                    //redirect to login page

                    // return "NullUA";

                }
            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateDoctorSchedule";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(DocObj);
        }
        #endregion UpdateDoctorSchedule

        #region CancelDoctorSchedule
        [System.Web.Services.WebMethod]
        public static string CancelDoctorSchedule(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            bool isSccheduleIDUsed = false;
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {

                        DocObj.ClinicID = UA.ClinicID.ToString();

                        DataSet dsAppointedpatients = DocObj.GetAllPatientDetails();
                         int NoOfPatients = dsAppointedpatients.Tables[0].Rows.Count;

                         if (NoOfPatients > 0)
                         {
                             isSccheduleIDUsed = true;
                         }

                       //  isSccheduleIDUsed = DocObj.CheckDoctorScheduleAllotedForPatientAppointment();

                        if (isSccheduleIDUsed == false)
                        {
                            DocObj.UpdatedBy = UA.userName;
                            DocObj.status = DocObj.CancelDoctorSchedule().ToString();
                            return jsSerializer.Serialize(DocObj);
                        }

                        else
                        {
                            //DocObj.status = "0"; //Already Used
                            //return jsSerializer.Serialize(DocObj);

                            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                            Dictionary<string, object> childRow;
                            if (dsAppointedpatients.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in dsAppointedpatients.Tables[0].Rows)
                                {
                                    childRow = new Dictionary<string, object>();
                                    foreach (DataColumn col in dsAppointedpatients.Tables[0].Columns)
                                    {
                                        childRow.Add(col.ColumnName, row[col].ToString());
                                    }
                                    parentRow.Add(childRow);
                                }
                            }

                            return jsSerializer.Serialize(parentRow);
                        }
                    }
                }
                else
                {
                    //redirect to login page

                    // return "NullUA";

                }
            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "CancelDoctorSchedule";
                eObj.InsertError();
            }
            return jsSerializer.Serialize("");
        }
        #endregion CancelDoctorSchedule

        #region Cancel ALL Schedules By Available Date
        [System.Web.Services.WebMethod]
        public static string CancelAllSchedulesByDate(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            bool isSccheduleIDUsed = false;
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {
                        DocObj.UpdatedBy = UA.userName;
                        DocObj.ClinicID = UA.ClinicID.ToString();

                        DataSet dsAppointedpatients = DocObj.GetAllPatientDetailsByAppointedDate();
                        int NoOfPatients = dsAppointedpatients.Tables[0].Rows.Count;

                        if (NoOfPatients > 0)
                        {
                            isSccheduleIDUsed = true;
                        }

                        if (isSccheduleIDUsed == false)
                        {
                            DocObj.status = DocObj.CancelAllScheduleByDoctor().ToString();
                        }
                        else
                        {
                            //DocObj.status = "0"; //Already Used

                            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                            Dictionary<string, object> childRow;
                            if (dsAppointedpatients.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow row in dsAppointedpatients.Tables[0].Rows)
                                {
                                    childRow = new Dictionary<string, object>();
                                    foreach (DataColumn col in dsAppointedpatients.Tables[0].Columns)
                                    {
                                        childRow.Add(col.ColumnName, row[col].ToString());
                                    }
                                    parentRow.Add(childRow);
                                }
                            }

                            return jsSerializer.Serialize(parentRow);


                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "CancelAllSchedulesByDate";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(DocObj);
        }
        #endregion Cancel ALL Schedules By Available Date

        #region Cancel Appoinments By ScheduleID
        [System.Web.Services.WebMethod]
        public static string CancelAppoinmentsByScheduleID(Appointments AppointObj)
        {
            TheClinicApp1._1.ClinicDAL.Doctor DocObj = new ClinicDAL.Doctor();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {
                       
                        DocObj.DocScheduleID = AppointObj.ScheduleID;


                        AppointObj.UpdatedBy = UA.userName;
                        AppointObj.ClinicID = UA.ClinicID.ToString();
                        AppointObj.status =  AppointObj.CancelAppoinmentsByScheuleID().ToString();


                       if ( AppointObj.status == "1")
                       {
                           DocObj.UpdatedBy = UA.userName;
                           DocObj.ClinicID = UA.ClinicID.ToString();
                           DocObj.status = DocObj.CancelDoctorSchedule().ToString();
                          
                       }
                       else
                       {
                           DocObj.status = "0";
                       }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "CancelAllAppoinments";
                eObj.InsertError();
            }

            return jsSerializer.Serialize(DocObj);
            //return jsSerializer.Serialize(AppointObj);
        }
        #endregion CancelAllAppoinments

        #region Cancel ALL Appoinments By Available Date
        [System.Web.Services.WebMethod]
        public static string CancelAllAppoinmentsByDate(Appointments AppointObj)
        {
            TheClinicApp1._1.ClinicDAL.Doctor DocObj = new ClinicDAL.Doctor();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {
                        AppointObj.UpdatedBy = UA.userName;
                        AppointObj.ClinicID = UA.ClinicID.ToString();
                        AppointObj.status = AppointObj.CancelAllAppoinmentsByDate().ToString();

                        if ( AppointObj.status == "1")
                        {
                             DocObj.UpdatedBy = UA.userName;
                             DocObj.ClinicID = UA.ClinicID.ToString();
                             DocObj.DoctorID = AppointObj.DoctorID;
                             DocObj.DoctorAvailDate = AppointObj.AppointmentDate;
                             DocObj.status = DocObj.CancelAllScheduleByDoctor().ToString();
                        
                        }

                        
                     }
                       
                    }
                

            }
            catch (Exception ex)
            {
                ErrorHandling eObj = new ErrorHandling();
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = "DoctorSchedule.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "CancelAllAppoinmentsByDate";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(DocObj);
        }
        #endregion Cancel ALL Appoinments By Available Date

        #region Send Message

        /// <summary>
        /// Message content and mobile Numbers are passed as method parameters
        /// message is seperated by | symbol , so individual msgs are retrieved by splitting it by this symbol
        /// MobileNos is seperated by | symbol , so individual numbers are retrieved by splitting it by this symbol
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="MobileNos"></param>

         [System.Web.Services.WebMethod]
        public static void SendMessage(string Msg, string MobileNos)
         {
            string[] IndividualMsgs = Msg.Split('|');
            string[] IndividualMobileNos = MobileNos.Split('|');
            foreach (var msg in IndividualMsgs) //msg is individual message
            {
                foreach (var Num in IndividualMobileNos) //Num is individual Number
                {
                    if (Num != string.Empty)
                    {
                        if (msg != string.Empty)
                        {
                            string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=shamilatps5@gmail.com:123456&senderID=TEST SMS&receipientno=" + Num + "&msgtxt=" + msg + "&state=4";
                            WebRequest request = HttpWebRequest.Create(strUrl);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream s = (Stream)response.GetResponseStream();
                            StreamReader readStream = new StreamReader(s);
                            string dataString = readStream.ReadToEnd();
                            response.Close();
                            s.Close();
                            readStream.Close();

                        } 
                    }
                }

            }
        }


        #endregion  Send Message

        #endregion Methods

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDoctorDropdown();
            }

        }

        #endregion Page Load

        #region Logout
        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            string LogoutConfirmation = Request.Form["confirm_value"];

            if (LogoutConfirmation == "true")
            {
            Session.Remove(Const.LoginSession);
            Session.Clear();
            Response.Redirect("../Default.aspx");
            }
        }

         #endregion Logout

        #region Doctor Dropdown Selected Index Changed
        protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDoctor.SelectedItem.Text !=  "--Select Doctor--")
            {
                hdnDoctorID.Value = ddlDoctor.SelectedValue;     
            }

        }

        #endregion Doctor Dropdown Selected Index Changed

        #endregion Events

    }
}