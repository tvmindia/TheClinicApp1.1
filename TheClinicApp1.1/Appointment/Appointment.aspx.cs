
#region CopyRight

//Author      : SHAMILA T P
//Created Date: Aug-8-2016

#endregion CopyRight

#region Included Namespaces

using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion  Included Namespaces

namespace TheClinicApp1._1.Appointment
{
    public partial class Appointment : System.Web.UI.Page
    {
        #region Global Variables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string listFilter = null;
        #endregion Global Variables

        #region Event Properties
        public class Event
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string isAvailable { get; set; }
            public string appointmentID { get; set; }
            public string allottedTime { get; set; }

            public string patientName
            {
                get;
                set;
            }

        }
        struct twolists
        {
            public List<Event> events;
            public List<string> str;
        }
        #endregion Event Properties

        #region Methods

        #region InsertPatientAppointment
        [System.Web.Services.WebMethod]
        public static string InsertPatientAppointment(Appointments AppointObj)
        {

            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            try
            {
                if (UA != null)
                {
                    if (UA.ClinicID.ToString() != "")
                    {

                        AppointObj.CreatedBy = UA.userName;
                        AppointObj.status = AppointObj.InsertPatientAppointment().ToString();
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
                eObj.Module = "Appointment.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertPatientAppointment";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(AppointObj);
        }

        #endregion InsertPatientAppointment

        #region UpdatePatientAppointment
        [System.Web.Services.WebMethod]
        public static string UpdatePatientAppointment(Appointments AppointObj)
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

                        AppointObj.UpdatedBy = UA.userName;
                        AppointObj.status = AppointObj.UpdatePatientAppointment().ToString();
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
                eObj.Module = "Appointment.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdatePatientAppointment";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(AppointObj);
        }
        #endregion UpdatePatientAppointment

        #region GetAllPatientAppointmentDetailsByClinicID
        [System.Web.Services.WebMethod]
        public static string GetAllPatientAppointmentDetailsByClinicID(TheClinicApp1._1.ClinicDAL.Doctor docObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Event> events = new List<Event>();
            DataSet ds = new DataSet();
            if (UA != null)
            {
                docObj.ClinicID = Convert.ToString(UA.ClinicID);

                ds = docObj.GetAllScheduleDetailsByDoctorID();
                int count = ds.Tables[0].Rows.Count;
                for (int i = 0; i <= count - 1; i++)
                {
                    events.Add(new Event()
                    {
                        id = ds.Tables[0].Rows[i]["event_id"].ToString(),
                        title ="Appointments "+ ds.Tables[0].Rows[i]["title"].ToString() + "/" + ds.Tables[0].Rows[i]["PatientLimit"].ToString(),
                        start = ds.Tables[0].Rows[i]["event_start"].ToString(),
                        end = ds.Tables[0].Rows[i]["event_end"].ToString()
                    });
                }

            }
            return JsonConvert.SerializeObject(events);

        }
        #endregion GetAllPatientAppointmentDetailsByClinicID

        #region GetAllPatientAppointmentDetailsBetweenDates
        [System.Web.Services.WebMethod]
        public static string GetAllPatientAppointmentDetailsBetweenDates(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (UA != null)
            {
                DataSet ds = null;
                ds = AppointObj.GetAllPatientAppointmentDetailsBetweenDates();
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
        #endregion GetAllPatientAppointmentDetailsBetweenDates

        #region AllotedPatientAbsentUpdate
        [System.Web.Services.WebMethod]
        public static string AllotedPatientAbsentUpdate(Appointments AppointObj)
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

                        AppointObj.UpdatedBy = UA.userName;
                        AppointObj.status = AppointObj.AllotedPatientAbsentUpdate().ToString();
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
                eObj.Module = "Appointment.aspx.cs";
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdatePatientAppointment";
                eObj.InsertError();
            }
            return jsSerializer.Serialize(AppointObj);
        }
        #endregion AllotedPatientAbsentUpdate

        #region Bind Dropdown
        public void dropdowndoctor()
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
                    ddlDoctor.Items.Insert(0, "--Select--");
                }
            }


        }

        #endregion Bind Dropdown

        #region BindDataAutocomplete
        /// <summary>
        /// Binding Data From DataBase For the Search Field provided for Patient Search
        /// </summary>
        /// <returns></returns>
        private string BindName()
        {
            Patient PatientObj = new Patient();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            PatientObj.ClinicID = UA.ClinicID;
            DataTable dt = PatientObj.GetSearchBoxData(); //Function call to get  Search BoxData
            StringBuilder output = new StringBuilder();
            output.Append("[");
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                output.Append("\"" + dt.Rows[i]["Name"].ToString() + "🏠📰 " + dt.Rows[i]["FileNumber"].ToString() + "|" + dt.Rows[i]["Address"].ToString() + "|" + dt.Rows[i]["Phone"].ToString() + "\"");
                if (i != (dt.Rows.Count - 1))
                {
                    output.Append(",");
                }
            }
            output.Append("]");
            return output.ToString();

        }
        #endregion BindDataAutocomplete

        #region GetAppointedPatientDetails
        [System.Web.Services.WebMethod]
        public static string GetAppointedPatientDetails(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                if (AppointObj.AppointmentID != "")
                { 
                ds = AppointObj.GetAppointedPatientDetails();
                int count = ds.Tables[0].Rows.Count;
                //Converting to Json


                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= count - 1; i++)
                    {
                        events.Add(new Event()
                        {
                            id = ds.Tables[0].Rows[i]["ScheduleID"].ToString(),
                            end = ds.Tables[0].Rows[i]["name"].ToString()
                        });
                    }
                }
                return jsSerializer.Serialize(events);
            }
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAppointedPatientDetails

        #region GetPatientAppointmentDetailsByAppointmentID
         [System.Web.Services.WebMethod]
        public static string GetPatientAppointmentDetailsByAppointmentID(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                if (AppointObj.AppointmentID != "")
                {
                    ds = AppointObj.GetPatientAppointmentDetailsByAppointmentID();
                    int count = ds.Tables[0].Rows.Count;
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
                                childRow.Add(col.ColumnName, row[col]);
                            }
                            parentRow.Add(childRow);
                        }
                    }
                    return jsSerializer.Serialize(parentRow);
                }
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetPatientAppointmentDetailsByAppointmentID

        #region GetAppointedPatientDetailsByScheduleID
        [System.Web.Services.WebMethod]
        public static string GetAppointedPatientDetailsByScheduleID(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            List<Event> events = new List<Event>();

            if (UA != null)
            {
                DataSet ds = null;
                ds = AppointObj.GetAppointedPatientDetailsByScheudleID();
                int count = ds.Tables[0].Rows.Count;
                //Converting to Json
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= count - 1; i++)
                    {
                        events.Add(new Event()
                        {
                            id = ds.Tables[0].Rows[i]["ScheduleID"].ToString(),
                            title = ds.Tables[0].Rows[i]["name"].ToString(),
                            isAvailable = ds.Tables[0].Rows[i]["AppointmentStatus"].ToString(),
                            appointmentID = ds.Tables[0].Rows[i]["ID"].ToString(),
                            allottedTime=ds.Tables[0].Rows[i]["AllottingTime"].ToString()
                        });
                    }
                }

                return jsSerializer.Serialize(events);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAppointedPatientDetailsByScheduleID

        #region GetDoctorAvailability
        [System.Web.Services.WebMethod]
        public static string GetDoctorAvailability(ClinicDAL.Doctor docObj)
        {

            int starth = 0;
            int startm = 0;
            int endH = 0;
            int endM = 0;
            int appointmentMinutes = 0;
            int patientLimit = 0;
            string startAppointment ="";
            string endAppointment = "";
            string startDuration = "";
            string endDuration = "";
            TimeSpan duration = new TimeSpan();
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            JavaScriptSerializer jsSerializerstr = new JavaScriptSerializer();
            docObj.ClinicID = Convert.ToString(UA.ClinicID);
            List<Event> events = new List<Event>();
            List<string> str = new List<string>();
            char[] charsToTrim = { ' ', '\t' };
            string[] arr = { };
            if (UA != null)
            {
                DataSet ds = null;
                ds = docObj.GetDoctorAvailability();
                int count = ds.Tables[0].Rows.Count;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    startAppointment = ds.Tables[0].Rows[0]["Starttime"].ToString();
                    endAppointment = ds.Tables[0].Rows[0]["Endtime"].ToString();
                    patientLimit =Convert.ToInt32(ds.Tables[0].Rows[0]["PatientLimit"].ToString());
                    string endHour = endAppointment.Split(':')[0];
                    string startHour = startAppointment.Split(':')[0];
                    if (endHour=="24")
                    {
                     
                        endDuration = "23:59";
                    }
                    if (startHour=="24")
                    {
                        startDuration = "23:59";
                    }
                    if(endDuration!="")
                    {
                        duration = DateTime.Parse(endDuration).Subtract(DateTime.Parse(startAppointment));
                        int endMinute = Convert.ToInt32(endAppointment.Split(':')[1])+1;
                        int startMinute =Convert.ToInt32(startAppointment.Split(':')[1]);
                        int totalminute = endMinute - startMinute;
                        totalminute = Math.Abs(totalminute);
                        string time = "00:" + totalminute;
                        TimeSpan ts = TimeSpan.Parse(time);
                        duration=duration.Add(ts);
                        
                    }
                    else if(startDuration!="")
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
                    else if(startDuration!="" && endDuration!="")
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
                    
                    appointmentMinutes =Convert.ToInt32(duration.TotalMinutes);
                    appointmentMinutes = appointmentMinutes / patientLimit;
                    
                    for (int i = 0; i <= count - 1; i++)
                    {
                        events.Add(new Event()
                        {
                            start = ds.Tables[0].Rows[i]["Starttime"].ToString(),
                            end = ds.Tables[0].Rows[i]["Endtime"].ToString()
                        });
                        string sTime = ds.Tables[0].Rows[i]["Starttime"].ToString();
                        sTime = sTime.Replace(" ", "");
                        TimeSpan ts = TimeSpan.Parse(sTime);
                        int startHours = ts.Hours;
                        int minutes = ts.Minutes;
                        starth = startHours;
                        startm = minutes;
                        TimeSpan endts = new TimeSpan();
                        DateTime dts = new DateTime();
                        string eTime = endAppointment;
                        if(endAppointment=="24:00")
                        {
                            int endMinute = Convert.ToInt32(endAppointment.Split(':')[1])+ 1;
                            string time = "00:" + endMinute;
                            TimeSpan tp = TimeSpan.Parse(time);
                            dts =DateTime.Parse(endDuration);
                            dts =dts.Add(tp);
                            string rs = Convert.ToString(dts);
                            char[] whitespace = new char[] { ' ', '\t' };
                            rs = rs.Split(whitespace)[1];
                            endts = TimeSpan.Parse(rs);
                        }
                        else
                        {
                            eTime = eTime.Replace(" ", "");
                            endts = TimeSpan.Parse(eTime);
                        }                       
                        int endHours = endts.Hours;
                        int endMinutes = endts.Minutes;
                        endH = endHours;
                        endM = endMinutes;
                    }
                }
                var clockIn=new DateTime();
                var clockOut=new DateTime();
                clockIn = new DateTime(2011, 5, 25, starth, startm, 00);
                clockOut = new DateTime(2011, 5, 25, endH, endM, 00);
                var hours = GetWorkingHourIntervals(clockIn, clockOut, appointmentMinutes);
                
                foreach (var h in hours)
                {
                    string a = Convert.ToString(h);
                    str.Add(a);

                }
                return jsSerializer.Serialize(str);

            }
            return jsSerializer.Serialize("");
        }
        #endregion GetDoctorAvailability

        #region AbsentPatientAppointment
        [System.Web.Services.WebMethod]
        public static string AbsentPatientAppointment(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            AppointObj.UpdatedBy = Convert.ToString(UA.userName);

            if (UA != null)
            {
                AppointObj.AppointmentStatus = 2;//absent
                AppointObj.UpdatedBy = UA.userName;
                AppointObj.ClinicID = UA.ClinicID.ToString();
                AppointObj.status = AppointObj.PatientAppointmentStatusUpdate().ToString();
                //Converting to Json
                return jsSerializer.Serialize(AppointObj);
            }
            return jsSerializer.Serialize("");
        }


        #endregion AbsentPatientAppointment

        #region CancelAppointment
        [System.Web.Services.WebMethod]
        public static string CancelAppointment(Appointments AppointObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            AppointObj.UpdatedBy = Convert.ToString(UA.userName);

            if (UA != null)
            {
                AppointObj.AppointmentStatus = 3;
                AppointObj.UpdatedBy = UA.userName;
                AppointObj.ClinicID = UA.ClinicID.ToString();
                AppointObj.status = AppointObj.PatientAppointmentStatusUpdate().ToString();

                //Converting to Json
                return jsSerializer.Serialize(AppointObj);
            }
            return jsSerializer.Serialize("");
        }
        #endregion CancelAppointment

        #region PresentPatientAppointment
        [System.Web.Services.WebMethod]
        public static string PresentPatientAppointment(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
            AppointObj.UpdatedBy = Convert.ToString(UA.userName);

            if (UA != null)
            {
                AppointObj.AppointmentStatus = 1;
                AppointObj.UpdatedBy = UA.userName;
                AppointObj.ClinicID = UA.ClinicID.ToString();
                AppointObj.status = AppointObj.PatientAppointmentStatusUpdate().ToString();
              

                //Converting to Json
                return jsSerializer.Serialize(AppointObj);
            }
            return jsSerializer.Serialize("");
        }

        #endregion PresentPatientAppointment

        #region GetAllScheduleDetails
        [System.Web.Services.WebMethod]
        public static string GetAllScheduleDetails(ClinicDAL.Doctor docObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            docObj.ClinicID = Convert.ToString(UA.ClinicID);
           
            if (UA != null)
            {

                docObj.GetScheduleDetailsByScheduleID();

                //Converting to Json
                return jsSerializer.Serialize(docObj);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAllScheduleDetails

        #region GetWorkingHourIntervals
        public static IEnumerable<DateTime> GetWorkingHourIntervals(DateTime clockIn, DateTime clockOut, int appointmentMinutes)
        {
            
            yield return clockIn;

            DateTime d = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, clockIn.Hour, 0, 0, clockIn.Kind).AddMinutes(appointmentMinutes);
           
            while (d < clockOut)
            {
                int minute = Convert.ToInt32(Math.Round(d.Minute / 5.0) * 5);
                TimeSpan ts = new TimeSpan(d.Hour, minute, 0);
                d = d.Date + ts;
                yield return d;
                d = d.AddMinutes(appointmentMinutes);
            }

            yield return clockOut;
        }
        #endregion GetWorkingHourIntervals

        #endregion Methods

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            listFilter = null;
            listFilter = BindName();
            if (!IsPostBack)
            {
                dropdowndoctor();
            }
        }

        #endregion Page Load

        #region Logout

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Session.Clear();
            Response.Redirect("../Default.aspx");
        }

        #endregion Logout

        #region btnSearch_ServerClick
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Patient PatientObj = new Patient();
            try
            {
               
                string path = Server.MapPath("~/Content/ProfilePics/").ToString();
                string Name = Request.Form["txtSearch"];
                if (Name != string.Empty)
                {
                    PatientObj.GetSearchWithName(Name);
                    DateTime date = DateTime.Now;
                    int year = date.Year;
                    Guid PatientID = PatientObj.PatientID;
                   
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "btn_Search", "Alert.render('Invalid Suggesion');", true);

                }
                
            }
            catch
            {
                Response.Redirect("../Appointment/Appointment.aspx");

            }
        }
        #endregion btnSearch_ServerClick

        #endregion Events

    }
}