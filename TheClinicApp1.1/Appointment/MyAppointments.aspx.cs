using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.Appointment
{
    public partial class MyAppointments : System.Web.UI.Page
    {
        #region Global Variables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        #endregion Global Variables
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            if (UA != null)
            {
                hdfUserRole.Value = UA.userInRole;
            }
            if (!IsPostBack)
            {
                dropdowndoctor();
            }
        }

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
                    ddlDoctor.Items.Insert(0, "--Select Doctor--");
                }
            }


        }

        #endregion Bind Dropdown

        #region GetAppointmentDateAndCount
        [System.Web.Services.WebMethod]
        public static string GetAppointmentDateAndCount(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);
           
            if (UA != null)
            {
                DataTable dt = null;
                dt = AppointObj.GetAppointmentDatesandPatientCountForMyAppointment();
                if(dt!=null)
                {
                    int count = dt.Rows.Count;
                    //Converting to Json
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            childRow = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                childRow.Add(col.ColumnName, row[col]);
                            }
                            parentRow.Add(childRow);
                        }
                    }
                }
            

                return jsSerializer.Serialize(parentRow);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAppointmentDateAndCount

        #region GetAppointmentedPatientDetails
        [System.Web.Services.WebMethod]
        public static string GetAppointmentedPatientDetails(Appointments AppointObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            AppointObj.ClinicID = Convert.ToString(UA.ClinicID);

            if (UA != null)
            {
                DataTable dt = null;
                dt = AppointObj.GetAppointedPatientDetailsForMyAppointments();
                int count = dt.Rows.Count;
                //Converting to Json
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }

                return jsSerializer.Serialize(parentRow);
            }
            return jsSerializer.Serialize("");
        }
        #endregion GetAppointmentedPatientDetails
    }
}