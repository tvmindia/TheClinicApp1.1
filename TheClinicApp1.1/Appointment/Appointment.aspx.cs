
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
using System.Linq;
using System.Text;
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
            AppointObj.ClinicID =Convert.ToString(UA.ClinicID);
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
                        title = ds.Tables[0].Rows[i]["title"].ToString(),
                        start = ds.Tables[0].Rows[i]["event_start"].ToString(),
                        end = ds.Tables[0].Rows[i]["event_end"].ToString()
                    });
                }

            }
            return JsonConvert.SerializeObject(events);

           
            //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //if (UA != null)
            //{
            //    AppointObj.ClinicID =Convert.ToString( UA.ClinicID);
            //    DataSet ds = null;
            //    ds = AppointObj.GetAllPatientAppointmentDetailsByClinicID();
            //    //Converting to Json
            //    List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            //    Dictionary<string, object> childRow;
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow row in ds.Tables[0].Rows)
            //        {
            //            childRow = new Dictionary<string, object>();
            //            foreach (DataColumn col in ds.Tables[0].Columns)
            //            {
            //                childRow.Add(col.ColumnName, row[col].ToString());
            //            }
            //            parentRow.Add(childRow);
            //        }
            //    }
            //    return jsSerializer.Serialize(parentRow);
            //}
            //return jsSerializer.Serialize("");
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
            AppointObj.ClinicID =Convert.ToString(UA.ClinicID);
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                ds = AppointObj.GetAppointedPatientDetails();
               // ds = AppointObj.GetAppointedPatientDetailsByScheudleID();
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
            return jsSerializer.Serialize("");
        }
        #endregion GetAppointedPatientDetails

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
              //  ds = AppointObj.GetAppointedPatientDetails();
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
                            isAvailable = ds.Tables[0].Rows[i]["AppointmentStatus"].ToString()
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

            int starth=0;
            int startm=0;
            int endH=0;
            int endM=0;
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            JavaScriptSerializer jsSerializerstr = new JavaScriptSerializer();
           docObj.ClinicID = Convert.ToString(UA.ClinicID);
           twolists lists;
            List<Event> events = new List<Event>();
            List<string> str = new List<string>();
            char[] charsToTrim = { ' ', '\t' };
            string[] arr = { };
            if (UA != null)
            {
                DataSet ds = null;
                ds = docObj.GetDoctorAvailability();
                int count = ds.Tables[0].Rows.Count;
                // ds = AppointObj.GetAppointedPatientDetailsByScheudleID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= count - 1; i++)
                    {
                        events.Add(new Event()
                        {
                            start = ds.Tables[0].Rows[i]["Starttime"].ToString(),
                            end = ds.Tables[0].Rows[i]["Endtime"].ToString()
                        });
                        string sTime = ds.Tables[0].Rows[i]["Starttime"].ToString();
                        sTime = sTime.Replace(" ","");
                        TimeSpan ts = TimeSpan.Parse(sTime);
                       
                       int startHours = ts.Hours;
                       int minutes = ts.Minutes;
                       starth = startHours;
                       startm = minutes;
                       string eTime = ds.Tables[0].Rows[i]["Endtime"].ToString();
                       eTime=eTime.Replace(" ", "");
                       TimeSpan endts = TimeSpan.Parse(eTime);
                       int endHours = endts.Hours;
                       int endMinutes = endts.Minutes;
                       endH = endHours;
                       endM = endMinutes;
                    }
                }
               
                
                //Converting to Json
                var clockIn = new DateTime(2011, 5, 25, starth, startm, 00);
                var clockOut = new DateTime(2011, 5, 25, endH, endM, 00);
              //  JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                var hours = GetWorkingHourIntervals(clockIn, clockOut);
              //  List<Event> events = new List<Event>();

                foreach (var h in hours)
                {
                    string a =Convert.ToString(h);
                    str.Add(a);
                    
                }

               
               //lists.events = events;
               //lists.str = str;
                return jsSerializer.Serialize(str);
               
                          }
            return jsSerializer.Serialize("");
        }
        #endregion GetDoctorAvailability

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
            List<Event> events = new List<Event>();
            if (UA != null)
            {
                DataSet ds = null;
                //  ds = AppointObj.GetAppointedPatientDetails();
                AppointObj.status = AppointObj.CancelAppointment().ToString();
             
                //Converting to Json
                return jsSerializer.Serialize(AppointObj);
            }
            return jsSerializer.Serialize("");
        }
        #endregion CancelAppointment

      public  static IEnumerable<DateTime> GetWorkingHourIntervals(DateTime clockIn, DateTime clockOut)
        {
            yield return clockIn;

            DateTime d = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, clockIn.Hour, 0, 0, clockIn.Kind).AddHours(1);

            while (d < clockOut)
            {
                yield return d;
                d = d.AddHours(1);
            }

            yield return clockOut;
        }
      public static void GetTimeIntervals(Appointments AppointObj)
      {
          var clockIn = new DateTime(2011, 5, 25, 13, 40, 56);
          var clockOut = new DateTime(2011, 5, 25, 18, 22, 12);
          JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
          string hours = GetWorkingHourIntervals(clockIn, clockOut).ToString();
          List<Event> events = new List<Event>();
         
          foreach (var h in hours)
          {

             
          }
          //return jsSerializer.Serialize(events);  
      }
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

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            Patient PatientObj = new Patient();
            try
            {
               // lblErrorCaption.Text = string.Empty;
              //  lblMsgges.Text = string.Empty;
               // Errorbox.Style["display"] = "none";
               // lblFileCount.Text = string.Empty;
               // lblTokencount.Text = string.Empty;
               // divDisplayNumber.Style["display"] = "none";

                string path = Server.MapPath("~/Content/ProfilePics/").ToString();
                string Name = Request.Form["txtSearch"];
                if (Name != string.Empty)
                {
                    PatientObj.GetSearchWithName(Name);
                    DateTime date = DateTime.Now;
                    int year = date.Year;
                    Guid PatientID = PatientObj.PatientID;
                    //txtPatientName.Text = PatientObj.Name;
                    //string Gender = PatientObj.Gender;
                    //txtPatientMobile.Text = PatientObj.Phone;
                    //txtPatientPlace.Text = PatientObj.Occupation;
                    
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FUNNNN", "Alert.render('Invalid Suggesion');", true);

                }
                // gridDataBind();
            }
            catch
            {
                Response.Redirect("../Appointment/Appointment.aspx");

            }
        }

        #endregion Events

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

       
        protected void ddlDoctor_SelectedIndexChanged1(object sender, EventArgs e)
        {
            hdfddlDoctorID.Value = ddlDoctor.SelectedValue;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GetDoctorID()", true);
        }

    }
}