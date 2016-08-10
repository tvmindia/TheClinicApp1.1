
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
                    txtPatientName.Text = PatientObj.Name;
                    string Gender = PatientObj.Gender;
                    txtPatientMobile.Text = PatientObj.Phone;
                    txtPatientPlace.Text = PatientObj.Occupation;
                    
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

    }
}