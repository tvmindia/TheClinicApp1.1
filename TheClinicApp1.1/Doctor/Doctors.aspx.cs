
#region CopyRight

//Author        : Thomson K Varkey
//Created Date  : March-04-2016

//Modified BY   : SHAMILA T P
//Modified Date : July -21 - 2016

#endregion CopyRight

#region Namespaces
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Web.Services;
using System.Web.UI;
using System.Drawing;  
using System.Web.UI.WebControls;

using TheClinicApp1._1.ClinicDAL;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.Xml;

#endregion Namespaces

namespace TheClinicApp1._1.Doctor
{
    public partial class Doctors : System.Web.UI.Page
    {
        #region GlobalVariables
        private static int PageSize = 8;
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ClinicDAL.ErrorHandling eObj = new ClinicDAL.ErrorHandling();
        ClinicDAL.TokensBooking tok = new ClinicDAL.TokensBooking();
        ClinicDAL.CaseFile.Visit CaseFileObj = new ClinicDAL.CaseFile.Visit();
        ClinicDAL.Patient PatientObj = new ClinicDAL.Patient();
        ClinicDAL.Doctor DoctorObj = new ClinicDAL.Doctor();
        ClinicDAL.Stocks stockObj = new ClinicDAL.Stocks();
        ClinicDAL.PrescriptionDetails PrescriptionObj= new ClinicDAL.PrescriptionDetails();
        ClinicDAL.PrescriptionHeaderDetails PrescriptionHeadObj = new ClinicDAL.PrescriptionHeaderDetails();
        ClinicDAL.CaseFile.Visit VisitsObj = new ClinicDAL.CaseFile.Visit();
        ClinicDAL.CaseFile.Visit.VisitAttachment AttachObj=new ClinicDAL.CaseFile.Visit.VisitAttachment();
        ClinicDAL.common cmnObj = new common();
        Appointments AppointObj = null;
        public string listFilter=null;
        public string RoleName = null;
        public string NameBind = null;
        #endregion GlobalVariables

        #region Events

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                BindDummyRow();
                BindHistoryDummyRow();

            //}
          
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string Login = UA.userName;
            PrescriptionObj.ClinicID = UA.ClinicID.ToString();
            tok.ClinicID = UA.ClinicID.ToString();

            listFilter = null;
            listFilter = GetMedicineNames();
            NameBind = null;
            NameBind = BindName();

            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            RoleName = UA.GetRoleName1(Login);

            if (RoleName.Contains(Const.RoleDoctor))
            {
                DataTable dt = new DataTable();
                dt = UA.GetDoctorAndDoctorID(Login);
                string DoctorName = dt.Rows[0]["Name"].ToString();
                Guid DoctorID = Guid.Parse(dt.Rows[0]["DoctorID"].ToString());
                VisitsObj.DoctorID = DoctorID;
                PrescriptionHeadObj.DoctorID = DoctorID;
                tok.DoctorID = DoctorID.ToString();
                tok.ClinicID = UA.ClinicID.ToString();
                lblDoctor.Text = "Dr." + DoctorName;

            }
            //gridviewbind();
        }
        #endregion PageLoad

        #region Save Button Click
        /// <summary>
        /// Save Button with Insert and Update of Visits, PrescriptionHeader,PrescriptionDetails 
        /// </summary>

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
              if (HiddenField2.Value != string.Empty)
                {
                    hdnFileIDAfterPostBack.Value = HiddenField2.Value;
                }
              else
              {
                  HiddenField2.Value = hdnFileIDAfterPostBack.Value;
              }


                if (HiddenField2.Value != string.Empty)
                {
                    UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
                    VisitsObj.ClinicID = UA.ClinicID;
                    VisitsObj.FileID = Guid.Parse(HiddenField2.Value);
                    int parsedValuefeet;
                    int parsedValueinch;
                    if ((int.TryParse(txtHeightFeet.Value.ToString().Trim(), out parsedValuefeet)) && (int.TryParse(txtHeightInch.Value.ToString().Trim(), out parsedValueinch)))
                    {
                        if (parsedValuefeet >= 0)
                        {
                            int feet = Convert.ToInt32(parsedValuefeet);
                            int inch = Convert.ToInt32(parsedValueinch);
                            VisitsObj.Height = float.Parse(feet + "." + inch);
                            VisitsObj.Weight = float.Parse(txtWeight.Value);

                        }
                    }
                    VisitsObj.Symptoms = (symptoms.Value != "") ? symptoms.Value.ToString() : null;
                    VisitsObj.Cardiovascular = (cardiovascular.Value != "") ? cardiovascular.Value.ToString() : null;
                    VisitsObj.Nervoussystem = (nervoussystem.Value != "") ? nervoussystem.Value.ToString() : null;
                    VisitsObj.Musculoskeletal = (musculoskeletal.Value != "") ? musculoskeletal.Value.ToString() : null;
                    VisitsObj.Palloe = (palloe.Value != "") ? palloe.Value.ToString() : null;
                    VisitsObj.Icterus = (icterus.Value != "") ? icterus.Value.ToString() : null;
                    VisitsObj.Clubbing = (clubbing.Value != "") ? clubbing.Value.ToString() : null;
                    VisitsObj.Cyanasis = (cyanasis.Value != "") ? cyanasis.Value.ToString() : null;
                    VisitsObj.Edima = (edima.Value != "") ? edima.Value.ToString() : null;
                    VisitsObj.Bowel = (bowel.Value != "") ? bowel.Value.ToString() : null;
                    VisitsObj.Appettie = (appettie.Value != "") ? appettie.Value.ToString() : null;
                    VisitsObj.Micturation = (micturation.Value != "") ? micturation.Value.ToString() : null;
                    VisitsObj.Sleep = (sleep.Value != "") ? sleep.Value.ToString() : null;
                    VisitsObj.Diagnosys = (diagnosys.Value != "") ? diagnosys.Value.ToString() : null;
                    VisitsObj.Remarks = (remarks.Value != "") ? remarks.Value.ToString() : null;
                    VisitsObj.CreatedBy = UA.userName;
                    VisitsObj.UpdatedBy = UA.userName;
                    VisitsObj.Bp = (bp.Value != "") ? bp.Value.ToString() : null;
                    VisitsObj.Pulse = (pulse.Value != "") ? pulse.Value.ToString() : null;
                    VisitsObj.Tounge = (tounge.Value != "") ? tounge.Value.ToString() : null;
                    VisitsObj.Heart = (heart.Value != "") ? heart.Value.ToString() : null;
                    VisitsObj.LymphGen = (lymphGen.Value != "") ? lymphGen.Value.ToString() : null;
                    VisitsObj.LymphClinic = (lymphnodes.Value != "") ? lymphnodes.Value.ToString() : null;
                    VisitsObj.RespRate = (resp_rate.Value != "") ? resp_rate.Value.ToString() : null;
                    VisitsObj.Others = (others.Value != "") ? others.Value.ToString() : null;

                    //appointed patient appointment status to 4 ie consulted
                    AppointObj = new Appointments();
                    AppointObj.ClinicID = VisitsObj.ClinicID.ToString();
                    AppointObj.AppointmentID = HdfUniqueID.Value;//unique id may be token id or appointment id
                    AppointObj.UpdatedBy = UA.userName;
                    AppointObj.AppointedPatientConsultDoctorStatusByAppointmentID();

                    if (HdnForVisitID.Value == "")
                    {
                        if (hdnRemovedIDs.Value.Trim() == string.Empty)
                        {


                            VisitsObj.AddVisits();
                            PrescriptionHeadObj.PrescID = VisitsObj.PrescriptionID.ToString();
                            PrescriptionHeadObj.VisitID = VisitsObj.VisitID.ToString();
                            HdnForVisitID.Value = VisitsObj.VisitID.ToString();
                            PrescriptionHeadObj.ClinicID = UA.ClinicID.ToString();
                            PrescriptionHeadObj.CreatedBy = UA.userName;
                            PrescriptionHeadObj.UpdatedBy = UA.userName;
                            PrescriptionHeadObj.CreatedDate = cmnObj.ConvertDatenow(DateTime.Now);
                            PrescriptionHeadObj.UpdatedDate = cmnObj.ConvertDatenow(DateTime.Now);
                            PrescriptionHeadObj.InsertPrescriptionHeader();


                        }
                    }
                    else
                    {
                        VisitsObj.VisitID = Guid.Parse(HdnForVisitID.Value.ToString());
                        VisitsObj.UpdateVisits();
                    }


                    if (FileUpload1.HasFile)
                    {
                        byte[] ImageByteArray = null;
                        ImageByteArray = ConvertImageToByteArray(FileUpload1);
                        AttachObj.Attachment = ImageByteArray;
                        AttachObj.Type = Path.GetExtension(FileUpload1.PostedFile.FileName);


                        int l = (FileUpload1.PostedFile.FileName).IndexOf(".");
                        if (l > 0)
                        {
                            AttachObj.Name = (FileUpload1.PostedFile.FileName).Substring(0, l);
                        }
                        AttachObj.Description = (Imgdesc.Value != "") ? Imgdesc.Value.ToString() : null;

                        AttachObj.VisitID = VisitsObj.VisitID;


                        float Size = FileUpload1.PostedFile.ContentLength / 1024;
                        float sizeinMB = Size / 1024;
                        string fileSize;
                        if ((int)sizeinMB == 0)
                        {
                            fileSize = Size + "KB";
                        }
                        else
                        {
                            fileSize = sizeinMB.ToString("0.00") + "MB";
                        }
                        AttachObj.Size = fileSize;

                        AttachObj.InsertFileAttachment();





                    }



                    if (HdnPrescID.Value != string.Empty)
                    {
                        PrescriptionObj.PrescID = HdnPrescID.Value.ToString();
                    }
                    else
                    {
                        PrescriptionObj.PrescID = VisitsObj.PrescriptionID.ToString();
                        HdnPrescID.Value = VisitsObj.PrescriptionID.ToString();
                    }

                    string last = string.Empty;
                    string values = hdnTextboxValues.Value;
                    string[] Rows = values.Split('$');
                    for (int i = 0; i < Rows.Length - 1; i++)
                    {
                        string[] tempRow = Rows;

                        last = tempRow[i].Split('|').Last();

                        string[] columns = tempRow[i].Split('|');

                        //if (last == string.Empty || last == "")
                        // {

                        //----------------- * CASE : INSERT *-----------------------------------//
                        if ((columns[0] != null) && (columns[1] != null))
                        {

                            PrescriptionObj.MedicineName = columns[0];
                            int parsedValue;
                            if (int.TryParse(columns[1], out parsedValue))
                            {
                                if (parsedValue >= 1)
                                {
                                    PrescriptionObj.Qty = parsedValue;
                                }
                            }
                            else
                            {
                                string msg = string.Empty;
                                var page = HttpContext.Current.CurrentHandler as Page;
                                msg = "Your Entered Quantity Fails to Match Please Update With a Valid Quantity";
                                eObj.InsertionNotSuccessMessage(page, msg);
                            }

                            PrescriptionObj.Unit = columns[2];
                            PrescriptionObj.Dosage = columns[3];
                            PrescriptionObj.Timing = columns[4];
                            PrescriptionObj.Days = columns[5];
                            if (columns[6] != "")
                            {

                                PrescriptionObj.ClinicID = UA.ClinicID.ToString();
                                PrescriptionObj.UpdatedBy = UA.userName;
                                PrescriptionObj.UpdatePrescriptionDetails(columns[6].ToString());

                            }
                            else
                            {
                                PrescriptionObj.CreatedBy = UA.userName;
                                PrescriptionObj.ClinicID = UA.ClinicID.ToString();
                                PrescriptionObj.UpdatedBy = UA.userName;
                                PrescriptionObj.InsertPrescriptionDetails();
                            }

                        }
                        // }
                    }
                    if (hdnRemovedIDs.Value != string.Empty)
                    {

                        //----------------- * CASE : DELETE *-----------------------------------//

                        string hdRemovedIDValue = hdnRemovedIDs.Value;

                        string[] RemovedIDs = hdRemovedIDValue.Split(',');

                        for (int i = 0; i < RemovedIDs.Length - 1; i++)
                        {

                            if ((RemovedIDs[i] != "") || (RemovedIDs[i] != string.Empty))
                            {
                                string UniqueId = RemovedIDs[i].ToString();
                                //string medId =   DetailObj.GetMedicineIDByUniqueID(Guid.Parse(UniqueId));
                                PrescriptionObj.ClinicID = UA.ClinicID.ToString();
                                PrescriptionObj.DeletePrescriptionDetails(UniqueId);
                                //DetailObj.DeleteReceiptDetails(UniqueId);
                                hdnRemovedIDs.Value = "";

                            }
                        }

                    }
                    //}
                    //gridviewbind();
                    if (HdnPrescID.Value != string.Empty)
                    {
                        DataSet MedicinList = PrescriptionObj.ViewPrescriptionDetails(HdnPrescID.Value.ToString());
                        var xml = MedicinList.GetXml();
                        hdnXmlData.Value = xml;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);
                    }
                    else
                    {
                        DataSet MedicinList = PrescriptionObj.ViewPrescriptionDetails(HdnPrescID.Value.ToString());
                        var xml = MedicinList.GetXml();
                        hdnXmlData.Value = xml;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);
                        //PrescriptionObj.PrescID = VisitsObj.PrescriptionID.ToString();
                    }


                }
                else
                {
                    string msg = string.Empty;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    msg = "Select A Token Or Search And Find Patient";
                    eObj.InsertionNotSuccessMessage(page, msg);
                }
            }


            catch
            {
                Response.Redirect("../Doctor/Doctors.aspx");
            }

            if (HiddenPatientID.Value != string.Empty)
            {
                PatntIdAftrPostback.Value = HiddenPatientID.Value;
            }
           
        }

        #endregion Save Button Click

        #region NewButtonClickEvent
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            ClearButton();
            lblNew_history.Text = "New Case";

        }
        #endregion NewButtonClickEvent

        #region Logout
        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        #endregion Logout

        #endregion Events

        #region Methods

        //Token Table

        #region Bookings for doctor View Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterBookingsForDoctor(string searchTerm, int pageIndex)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            string Login = UA.userName;
            DataTable dt = new DataTable();
            dt = UA.GetDoctorAndDoctorID(Login);

            TokensBooking tok = new ClinicDAL.TokensBooking();
            tok.ClinicID = UA.ClinicID.ToString();

            Guid DoctorID = Guid.Parse(dt.Rows[0]["DoctorID"].ToString());
            tok.DoctorID = DoctorID.ToString();

            tok.DateTime = DateTime.Now;

            var xml = tok.ViewAndFilterTokenOfDoctors(searchTerm, pageIndex, PageSize);

            return xml;
        }

        #region Bind Dummy Row

        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            //dummy.Columns.Add("Edit");
            dummy.Columns.Add(" ");
            dummy.Columns.Add("TokenNo");
            dummy.Columns.Add("appointmentno");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("DateTime");
            dummy.Columns.Add("IsProcessed");

           
            //dummy.Columns.Add("DoctorID");
            dummy.Columns.Add("PatientID");
            dummy.Columns.Add("UniqueID");
            dummy.Rows.Add();

            GridViewTokenlist.DataSource = dummy;
            GridViewTokenlist.DataBind();
        }

        #endregion Bookings for doctor View Search Paging

       
        #endregion ToKen booking View Search Paging

        //History's Edit Click

        #region Bind Patient Details On Edit Click
       
        [System.Web.Services.WebMethod]
        public static string BindPatientDetailsOnEditClick(Patient PatientObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            PatientObj.ClinicID = UA.ClinicID;
            //DataSet dsPatient = 
            PatientObj.GetPatientDetailsByID();


            string jsonResult = null;
            //DataSet ds = null;
            //ds = dsPatient;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
           // List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            //Dictionary<string, object> childRow;
            //if (ds.Tables[0].Rows.Count > 0)
           // {
            //    foreach (DataRow row in ds.Tables[0].Rows)
            //    {
              //      childRow = new Dictionary<string, object>();
              //      foreach (DataColumn col in ds.Tables[0].Columns)
              //      {
               //         childRow.Add(col.ColumnName, row[col]);
               //     }
               //     parentRow.Add(childRow);
               // }
           // }
            jsonResult = jsSerializer.Serialize(PatientObj);

            return jsonResult; //Converting to Json
        }
        #endregion Bind Patient Details On Edit Click

        #region Bind Visit Details On Edit Click

        [System.Web.Services.WebMethod]
        public static string BindVisitDetailsOnEditClick(CaseFile.Visit CaseFileObj)
        {
            CaseFileObj.GetVisits();

            string jsonResult = null;
           
            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            jsonResult = jsSerializer.Serialize(CaseFileObj);

            return jsonResult; //Converting to Json
        }
        #endregion Bind Visit Details On Edit Click

        #region Get Visit Attachment

        [WebMethod]
        public static string GetVisitAttatchment(CaseFile.Visit.VisitAttachment AttachObj)
        {
            DataSet dsAttachment = AttachObj.GetVisitAttachment();


            string jsonResult = null;
            DataSet ds = null;
            ds = dsAttachment;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        if (col.ColumnName != "Attachment")
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }


                    }
                    parentRow.Add(childRow);
                }
            }
            jsonResult = jsSerializer.Serialize(parentRow);

            return jsonResult; //Converting to Json
        }

        #endregion Get Visit Attachment

        #region Get Prescription Details Xml
        [WebMethod]
        public static string GetPrescriptionDetailsXml(string PrescriptionID)
        {
            ClinicDAL.PrescriptionDetails PrescriptionObj = new ClinicDAL.PrescriptionDetails();

            DataSet MedicinList = PrescriptionObj.ViewPrescriptionDetails(PrescriptionID);
            var xml = MedicinList.GetXml();

            return xml;
        }

        #endregion Get Prescription Details Xml

        [WebMethod]
        public static void DeleteAttachment(string AttachID)
        {
            CaseFile.Visit.VisitAttachment AttachObj = new CaseFile.Visit.VisitAttachment();

            AttachObj.AttachID = Guid.Parse(AttachID);

            AttachObj.DeletevisitAttachmentByAttachID();

        }


       //History

        #region Get History

        [System.Web.Services.WebMethod]
        public static string GetHistory(string searchTerm, int pageIndex,string PatientID)
        {
            var xml = string.Empty;

           ClinicDAL.Doctor DoctorObj = new ClinicDAL.Doctor();

            DataSet GridBindVisits = null;
            Guid FileIDForGrid = Guid.Empty;

            CaseFile.Visit CaseFileObj = new ClinicDAL.CaseFile.Visit();

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            if (PatientID != null && PatientID != string.Empty)
            {
                DoctorObj.PatientIdForFile = Guid.Parse(PatientID);
                DataTable DtFileID = DoctorObj.GetFileIDUSingPatientID();
                GridBindVisits = new DataSet();
                if (DtFileID.Rows.Count > 0)
                {
                    FileIDForGrid = Guid.Parse(DtFileID.Rows[0]["FileID"].ToString());
                    CaseFileObj.FileID = FileIDForGrid;

                    xml = CaseFileObj.ViewAndFilterVisits(searchTerm, pageIndex, PageSize);
                }
            }

            else
            {
                //It will return pager part , so that no records found text will be dispalyed
                xml = CaseFileObj.ViewAndFilterVisits(searchTerm, pageIndex, PageSize);
            }

            return xml;

        }
        #endregion Get History

        #region Bind History Dummy Table
        private void BindHistoryDummyRow()
        {
           

            DataTable dummy = new DataTable();

            //dummy.Columns.Add("Edit");
            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Remarks");
            dummy.Columns.Add("CrDate");
            dummy.Columns.Add("FileID");
            dummy.Columns.Add("VisitID");
            dummy.Columns.Add("PrescriptionID");

            dummy.Rows.Add();

            GridViewVisitsHistory.DataSource = dummy;
            GridViewVisitsHistory.DataBind();
            
        }
        #endregion Bind History Dummy Table

        #region Get Medicine Names

        /// <summary>
        /// Get all medicine names to be binded into list filter
        /// </summary>
        /// <returns></returns>
        private string GetMedicineNames()
        {
            StringBuilder output = new StringBuilder();
          
            DataTable dt = PrescriptionObj.SearchMedicinewithCategory();
            if (dt.Rows.Count > 0)
            {
               
                output.Append("[");
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    output.Append("\"" + dt.Rows[i]["Name"].ToString() + "\"");

                    if (i != (dt.Rows.Count - 1))
                    {
                        output.Append(",");
                    }
                }
                output.Append("]");
               
            }
            return output.ToString();
        }

        #endregion Get Medicine Names

        #region BindSearch
        private string BindName()
        {
           
            DataTable dt = tok.GetSearchBoxData();
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
        #endregion BindSearch
      
        #region FillPatientDetails
        protected void ImgBtnUpdate_Command1(object sender, CommandEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func1", "reset();", true);
            ClearButton();
            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
            DataRow dr = null;
            PatientObj.PatientID = Guid.Parse(e.CommandArgument.ToString());
            Guid PatientIDForFile = Guid.Parse(e.CommandArgument.ToString());

            DoctorObj.PatientIdForFile = PatientIDForFile;
            DataTable DtFileID = DoctorObj.GetFileIDUSingPatientID();
            dr = DtFileID.NewRow();
            dr = DtFileID.Rows[0];
            Guid FileIDForGrid = Guid.Parse(dr["FileID"].ToString());

            DataTable GridBindVisits = new DataTable();
            GridBindVisits = CaseFileObj.GetGridVisits(FileIDForGrid); //binding history Cases
            //GridViewVisitsHistory.EmptyDataText = "No Records Found";
            //GridViewVisitsHistory.DataSource = GridBindVisits;
            //GridViewVisitsHistory.DataBind();

            lblCaseCount.Text = GridViewVisitsHistory.Rows.Count.ToString();
         

            DataTable dt = PatientObj.SelectPatient(); //binding patient Details in header
            dr = dt.NewRow();
            dr = dt.Rows[0];
            DateTime date = DateTime.Now;
            int year = date.Year;
            Guid PatientID = Guid.Parse(dr["PatientID"].ToString());
            lblPatientName.Text = dr["Name"].ToString();
            lblGenderDis.Text = dr["Gender"].ToString();
            HiddenField2.Value = FileIDForGrid.ToString();
            lblFileNum.Text = dr["FileNumber"].ToString();
            DateTime DT = Convert.ToDateTime(dr["DOB"].ToString());
            int Age = year - DT.Year;
            lblAgeCount.Text = Age.ToString();
            //lblAddress.Text = dr["Address"].ToString();
            //lblLastVisitDate.Text = dr["CreatedDate"].ToString();
            string imagetype=dr["ImageType"].ToString();
            if(imagetype.Trim()!=string.Empty)
            {
                ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
            }
            else
            {
                ProfilePic.Src = "../images/UploadPic1.png";
            }
            //ProfilePic.Visible = true;
            HiddenField1.Value = PatientID.ToString();

        }
        #endregion FillPatientDetails

        #region Get MedicineDetails By Medicine Name

        /// <summary>
        /// To fill textboxes with medicine details when when medicne name is inserted
        /// </summary>
        /// <param name="MedName"></param>
        /// <returns>String of medicine details</returns>

        [WebMethod]

        public static string MedDetails(string MedName)
        {

            IssueHeaderDetails IssuedtlsObj = new IssueHeaderDetails();

            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            if(UA!=null)
            {
                IssuedtlsObj.ClinicID = UA.ClinicID.ToString();
            }
            
            DataSet ds = IssuedtlsObj.GetMedicineDetailsByMedicineName(MedName);
            string Unit = "";
          

            if (ds.Tables[0].Rows.Count > 0)
            {
                Unit = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
              
            }

            return String.Format("{0}", Unit);


        }


        #endregion Get MedicineDetails By Medicine Name

        #region ClearButton Method
        public void ClearButton()
        {
            HdnForVisitID.Value = string.Empty;
            HdnPrescID.Value = string.Empty;
            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
        }
        #endregion ClearButton Method

        #region convertImage
        /// <summary>
        /// Conveting Image into Bytes Array
        /// </summary>
        /// <param name="fuImage"></param>
        /// <returns></returns>
        private byte[] ConvertImageToByteArray(FileUpload fuImage)
        {
            byte[] ImageByteArray;
            try
            {
                MemoryStream ms = new MemoryStream(fuImage.FileBytes);
                ImageByteArray = ms.ToArray();
                return ImageByteArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion convertImage

        #endregion Methods

        //--NOTE: Below events and functions are not using now

        #region Search And Find the Patient Visits
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            ClearButton();

           // Page.ClientScript.RegisterStartupScript(this.GetType(), "func1", "reset();", true);

            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
            DataRow dr = null;
            if (HiddenPatientID.Value != string.Empty)
            {
                PatientObj.PatientID = Guid.Parse(HiddenPatientID.Value.ToString());
                Guid PatientIDForFile = Guid.Parse(HiddenPatientID.Value.ToString());

                DoctorObj.PatientIdForFile = PatientIDForFile;
                DataTable DtFileID = DoctorObj.GetFileIDUSingPatientID();
                dr = DtFileID.NewRow();
                dr = DtFileID.Rows[0];
                Guid FileIDForGrid = Guid.Parse(dr["FileID"].ToString());

                DataTable GridBindVisits = new DataTable();
                GridBindVisits = CaseFileObj.GetGridVisits(FileIDForGrid);
                //GridViewVisitsHistory.EmptyDataText = "No Records Found";
                //GridViewVisitsHistory.DataSource = GridBindVisits;
                //GridViewVisitsHistory.DataBind(); 
                lblCaseCount.Text = GridViewVisitsHistory.Rows.Count.ToString();

                DataTable dt = PatientObj.SelectPatient();
                dr = dt.NewRow();
                dr = dt.Rows[0];
                DateTime date = DateTime.Now;
                int year = date.Year;
                Guid PatientID = Guid.Parse(dr["PatientID"].ToString());
                lblPatientName.Text = dr["Name"].ToString();
                lblGenderDis.Text = dr["Gender"].ToString();
                HiddenField2.Value = FileIDForGrid.ToString();
                lblFileNum.Text = dr["FileNumber"].ToString();
                DateTime DT = Convert.ToDateTime(dr["DOB"].ToString());
                int Age = year - DT.Year;
                lblAgeCount.Text = Age.ToString();
                //lblAddress.Text = dr["Address"].ToString();
                //lblLastVisitDate.Text = dr["CreatedDate"].ToString();
                string imagetype = dr["ImageType"].ToString();
                if (imagetype.Trim() != string.Empty)
                {
                    ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
                }
                else
                {
                    ProfilePic.Src = "../images/UploadPic1.png";
                }
                //ProfilePic.Visible = true;

                HiddenField1.Value = PatientID.ToString();
                HiddenPatientID.Value = PatientID.ToString();
            }
        }
        #endregion Search And Find the Patient Visits

        #region Update Visits
        protected void ImgBtnUpdateVisits_Command(object sender, CommandEventArgs e)
        {

            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
            string[] Visits = e.CommandArgument.ToString().Split(new char[] { '|' });
            CaseFileObj.VisitID = Guid.Parse(Visits[0]);
            BindAttachment(Guid.Parse(Visits[0]));


            CaseFileObj.GetVisits();
            txtHeightFeet.Value = CaseFileObj.Height.ToString();
            txtWeight.Value = CaseFileObj.Weight.ToString();
            bowel.Value = CaseFileObj.Bowel;
            appettie.Value = CaseFileObj.Appettie;
            micturation.Value = CaseFileObj.Micturation;
            sleep.Value = CaseFileObj.Sleep;
            symptoms.Value = CaseFileObj.Symptoms;
            cardiovascular.Value = CaseFileObj.Cardiovascular;
            nervoussystem.Value = CaseFileObj.Nervoussystem;
            musculoskeletal.Value = CaseFileObj.Musculoskeletal;
            palloe.Value = CaseFileObj.Palloe;
            icterus.Value = CaseFileObj.Icterus;
            clubbing.Value = CaseFileObj.Clubbing;
            cyanasis.Value = CaseFileObj.Cyanasis;
            lymphGen.Value = CaseFileObj.LymphClinic;
            edima.Value = CaseFileObj.Edima;
            diagnosys.Value = CaseFileObj.Diagnosys;
            remarks.Value = CaseFileObj.Remarks;
            pulse.Value = CaseFileObj.Pulse;
            bp.Value = CaseFileObj.Bp;
            tounge.Value = CaseFileObj.Tounge;
            heart.Value = CaseFileObj.Heart;
            lymphnodes.Value = CaseFileObj.LymphGen;
            resp_rate.Value = CaseFileObj.RespRate;
            others.Value = CaseFileObj.Others;

            string PrescriptionID = Visits[1];
            HdnPrescID.Value = Visits[1];
            HdnForVisitID.Value = Visits[0];
            DataSet MedicinList = PrescriptionObj.ViewPrescriptionDetails(PrescriptionID);
            var xml = MedicinList.GetXml();
            hdnXmlData.Value = xml;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);

            lblNew_history.Text = "History: " + CaseFileObj.Date.ToShortDateString();

        }
        #endregion Update Visits

        #region BindVisitAttachment
        public void BindAttachment(Guid VisitID)
        {
            DataTable dt = new DataTable();
            AttachObj.VisitID = VisitID;
            dt = AttachObj.GetAttachment();
            foreach (DataRow dr in dt.Rows)
            {
                HtmlGenericControl img1 = new HtmlGenericControl("img");
                img1.Attributes.Add("id", "" + dr["AttachID"].ToString());
                img1.Attributes.Add("src", "../Handler/ImageHandler.ashx?AttachID=" + dr["AttachID"].ToString());
                //img1.Attributes.Add("width", "120");
                img1.Attributes.Add("height", "120");
                img1.Attributes.Add("class", "imagpreview");
                VistImagePreview.Controls.Add(img1);

            }
        }
        #endregion BindVisitAttachment

        #region GridBindTokens
        public void gridviewbind()
        {
            //Gridview Binding to Diplay DoctorName,Token No,Patient Name,TIME
            tok.DateTime = DateTime.Now;
            DataSet gds = tok.DoctorViewToken();

            DataRow[] dr = gds.Tables[0].Select("IsProcessed=False");
            lblTokenCount.Text = dr.Length.ToString();

            GridViewTokenlist.EmptyDataText = "No Records Found";
            GridViewTokenlist.DataSource = gds;
            GridViewTokenlist.DataBind();


        }
        #endregion GridBindTokens

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static string PatientDetails(string file)
        {
            try
            {
                ClinicDAL.TokensBooking obj = new ClinicDAL.TokensBooking();
                UIClasses.Const Const = new UIClasses.Const();
                ClinicDAL.UserAuthendication UA;
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                obj.ClinicID = UA.ClinicID.ToString();

                DataSet ds = obj.GetpatientDetails(file);

                string FileNumber = Convert.ToString(ds.Tables[0].Rows[0]["FileNumber"]);
                string Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                string Gender = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]);
                string Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                string Phone = Convert.ToString(ds.Tables[0].Rows[0]["Phone"]);
                string Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);

                string PatientID = Convert.ToString(ds.Tables[0].Rows[0]["PatientID"]);
                string ClinicID = Convert.ToString(ds.Tables[0].Rows[0]["ClinicID"]);
                string lastvisit = Convert.ToString(ds.Tables[0].Rows[0]["LastVisitDate"]);

                DateTime date = DateTime.Now;
                int year = date.Year;
                DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
                int Age = year - DT.Year;
                string DOB = Age.ToString();

                return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + "|" + "{3}" + " | " + "{4}" + "|" + "{5}" + " | " + "{6}" + "|" + "{7}" + " | " + "{8}", FileNumber, Name, DOB, Gender, Address, Phone, Email, PatientID, ClinicID);
            }
            catch
            {

                return null;
            }

        }
        #endregion WebMethod

        #region Row Coloring for Tokens View
        /// <summary>
        /// color the Isprocessed rows in the Token View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }

        protected void GridViewTokenlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = GetColumnIndexByName(e.Row, "IsProcessed");
                string columnValue = e.Row.Cells[index].Text;

                //foreach (TableCell cell in e.Row.Cells)
                //{
                if (columnValue == "True")
                {
                    e.Row.Cells[index].Text = "Yes";
                    e.Row.BackColor = Color.LightGray;
                }
                else
                {
                    e.Row.Cells[index].Text = "No";
                }
                //}               
            }

        }
        #endregion Row Coloring for Tokens View

    }
        
}