
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

        #endregion Global Variables

        #region Event Properties
        public class Event
        {
            public string id { get; set; }
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }

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
        public static string GetAllPatientAppointmentDetailsByClinicID()
        {
            Appointments AppointObj = new Appointments();
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Event> events = new List<Event>();
            DataSet ds = new DataSet();
            if (UA != null)
            {
                AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
                ds = AppointObj.GetAllPatientAppointmentDetailsByClinicID();
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


        #endregion Methods

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {

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

        #endregion Events

    }
}