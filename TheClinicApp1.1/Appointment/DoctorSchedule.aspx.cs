﻿
#region CopyRight

//Author      : SHAMILA T P
//Created Date: Aug-8-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        #region Methods
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

        #region GetDoctorScheduleDetailsByDoctorID
        
        [System.Web.Services.WebMethod]
        public static string GetDoctorScheduleDetailsByDoctorID(TheClinicApp1._1.ClinicDAL.Doctor DocObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            if (UA != null)
            {
                DataSet ds = null;
                ds = DocObj.GetDoctorScheduleDetailsByDoctorID();
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
        #endregion GetDoctorScheduleDetailsByDoctorID

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
                        DocObj.status = DocObj.UpdateDoctorSchedule().ToString();
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
                        DocObj.status = DocObj.CancelDoctorSchedule().ToString();
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
            return jsSerializer.Serialize(DocObj);
        }
        #endregion CancelDoctorSchedule

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