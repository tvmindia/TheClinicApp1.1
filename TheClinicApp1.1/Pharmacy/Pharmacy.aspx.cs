#region Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
using System.Web.Script.Serialization; 

#endregion Namespaces

namespace TheClinicApp1._1.Pharmacy
{
    public partial class Pharmacy : System.Web.UI.Page
    {

        #region Gobalvariables

        private static int PageSize = 8;

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;
        public string listFilter = null;
        public string NameBind = null;
        common cmn = new common();
        ErrorHandling eObj = new ErrorHandling();
        PrescriptionDetails PrescriptionObj = new PrescriptionDetails();
        pharmacy pharmacypobj = new pharmacy();
        TokensBooking tokobj = new TokensBooking();
        Patient patobj = new Patient();
       
        IssueHeaderDetails issuehdobj = new IssueHeaderDetails();

        #endregion Gobalvariables

        #region Methods

        #region Pharmacy View Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterPatientBooking(string searchTerm, int pageIndex)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            pharmacy pharmacypobj = new pharmacy();
             pharmacypobj.ClinicID = UA.ClinicID;

             var xml = pharmacypobj.ViewAndFilterPatientsInPharmacy(searchTerm, pageIndex, PageSize);

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
            dummy.Columns.Add("DOCNAME");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("CreatedDate");
            dummy.Columns.Add("IsProcessed");
            dummy.Columns.Add("DoctorID");
            dummy.Columns.Add("PatientID");
            
            dummy.Rows.Add();

            GridViewPharmacylist.DataSource = dummy;
            GridViewPharmacylist.DataBind();
        }

        #endregion Pharmacy View Search Paging


        #endregion ToKen booking View Search Paging

        #region Bind Pharmacy Details On Edit Click
        /// <summary>
        /// To get specific order details by orderid for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindPharmacyDetailsOnEditClick(pharmacy pharmacypobj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            pharmacypobj.ClinicID = UA.ClinicID;
            DataSet dtPharmacy = pharmacypobj.GetPatientPharmacyDetailsByID();


            string jsonResult = null;
            DataSet ds = null;
            ds = dtPharmacy;

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
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            jsonResult = jsSerializer.Serialize(parentRow);

            return jsonResult; //Converting to Json
        }
        #endregion Bind Pharmacy Details On Edit Click

        #region Get Prescription Details Xml
        [WebMethod]
        public static string GetPrescriptionDetailsXml(string PatientID)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            pharmacy pharmacypobj = new pharmacy();
            pharmacypobj.ClinicID = UA.ClinicID;
            pharmacypobj.PatientID = Guid.Parse(PatientID);

            DataSet MedicinList = pharmacypobj.PrescriptionDetails();  //Prescription Details Function Call
            var xml = MedicinList.GetXml();

            return xml;
        }

        #endregion Get Prescription Details Xml

        #region PatientDetails

        [WebMethod(EnableSession = true)]
        public static string PatientDetails(string file)
        {
            ClinicDAL.TokensBooking obj = new ClinicDAL.TokensBooking();
            UIClasses.Const Const = new UIClasses.Const();
            common cmn = new common();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            obj.ClinicID = UA.ClinicID.ToString();

            DataSet ds = obj.GetpatientDetails(file); //Function Call to Get Patient Details

            string FileNumber = Convert.ToString(ds.Tables[0].Rows[0]["FileNumber"]);
            string Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
            string Gender = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]);
            string Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
            string Phone = Convert.ToString(ds.Tables[0].Rows[0]["Phone"]);
            string Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
            string PatientID = Convert.ToString(ds.Tables[0].Rows[0]["PatientID"]);
            string ClinicID = Convert.ToString(ds.Tables[0].Rows[0]["ClinicID"]);
            string lastvisit = Convert.ToString(ds.Tables[0].Rows[0]["LastVisitDate"]);

            DateTime date =cmn.ConvertDatenow(DateTime.Now);
            int year = date.Year;
            DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
            int Age = year - DT.Year;
            string DOB = Age.ToString();

            return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + "|" + "{3}" + " | " + "{4}" + "|" + "{5}" + " | " + "{6}" + "|" + "{7}" + " | " + "{8}", FileNumber, Name, DOB, Gender, Address, Phone, Email, PatientID, ClinicID);

        }

        #endregion PatientDetails

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
            DataTable dt = tokobj.GetSearchBoxData(); //Function call for Get Search Box Data
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

        #region Get MedicineDetails By Medicine Name

        [WebMethod]
        public static string MedDetails(string MedName)
        {
            IssueHeaderDetails IssuedtlsObj = new IssueHeaderDetails();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            IssuedtlsObj.ClinicID = UA.ClinicID.ToString();

            DataSet ds = IssuedtlsObj.GetMedicineDetailsByMedicineName(MedName); //Get Medicine Details By Medicine Name
            string Unit = "";
            string Quantity = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                Unit = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
                Quantity = Convert.ToString(ds.Tables[0].Rows[0]["Qty"]);
            }
            return String.Format("{0}" + "|" + "{1}", Unit, Quantity);
        }

        #endregion Get MedicineDetails By Medicine Name

        #endregion Methods

        #region Events

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDummyRow();

            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string Login = UA.userName;
            tokobj.ClinicID = UA.ClinicID.ToString();
            RoleName = UA.GetRoleName1(Login);
            pharmacypobj.ClinicID = UA.ClinicID;
            PrescriptionObj.ClinicID = UA.ClinicID.ToString();
            listFilter = null;
            listFilter = GetMedicineNames();
            NameBind = null;
            NameBind = BindName();
            //gridviewbind();
            btnSave.Attributes.Add("onclick", "return  GetTextBoxValuesPres('" + hdnTextboxValues.ClientID + "','" + lblErrorCaption.ClientID + "','" + Errorbox.ClientID + "','" + lblMsgges.ClientID + "');");
        }
        #endregion Pageload

        #region ClickEvents

        #region Save Button Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            DataRow dr = null;

            if (HiddenPatientID.Value != "" || Patientidtorefill.Value != "")
            {
                if (hdnTextboxValues.Value != "")
                {
                    if (HiddenPatientID.Value != "")
                    {
                        patobj.PatientID = Guid.Parse(HiddenPatientID.Value);
                        DataTable dt = patobj.SelectPatient();              // Select Patient function call                    
                        dr = dt.NewRow();
                        dr = dt.Rows[0];
                        lblPatientName.Text = dr["Name"].ToString();
                    }
                    issuehdobj.ClinicID = UA.ClinicID.ToString();
                    issuehdobj.IssueNO = issuehdobj.Generate_Issue_Number(); // Generate Issue Number function call                    
                    issuehdobj.PrescID = hdnPrescID.Value;
                    issuehdobj.IssuedTo = lblPatientName.Text;
                    issuehdobj.Date =cmn.ConvertDatenow(DateTime.Now);
                    issuehdobj.CreatedBy = UA.userName;
                    issuehdobj.ClinicID = UA.ClinicID.ToString();
                    ViewState["IssueHdrID"] = issuehdobj.IssueID;

                    issuehdobj.InsertIssueHeader();                           // insert issue header function call  

                    string last = string.Empty;
                    string values = hdnTextboxValues.Value;
                    string[] Rows = values.Split('$');

                    for (int i = 0; i < Rows.Length - 1; i++)
                    {
                        IssueDetails IssuedtlObj = new IssueDetails();       //Object is created in loop as each entry should have different uniqueID
                        string[] tempRow = Rows;
                        string[] columns = tempRow[i].Split('|');
                        IssuedtlObj.MedicineName = columns[0];
                        IssuedtlObj.Qty = Convert.ToInt32(columns[1]);
                        IssuedtlObj.CreatedBy = UA.userName;
                        IssuedtlObj.ClinicID = UA.ClinicID.ToString();
                        if (ViewState["IssueHdrID"] != null && ViewState["IssueHdrID"].ToString() != string.Empty)
                        {
                            IssuedtlObj.IssueID = Guid.Parse(ViewState["IssueHdrID"].ToString());
                        }
                        IssuedtlObj.InsertIssueDetails();                       // insert issue Details function call  
                    }
                    // hdnsave.Value = "saved";
                    //lblPatientName.Text = "PATIENT_NAME";
                    //lblAgeCount.Text = "AGE";
                    //lblGenderDis.Text = "GENDER";
                    //lblFileNum.Text = "FILE NO";
                    //lblDoctor.Text = "";
                    //HiddenPatientID.Value = "";
                    //Patientidtorefill.Value = "";
                    //ProfilePic.Src = "../images/UploadPic1.png";
                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    msg = "Please fill all the fields ";
                    eObj.InsertionNotSuccessMessage(page, msg);
                }
            }
            else
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                msg = "Patient Details not found ";
                eObj.InsertionNotSuccessMessage(page, msg);

                if (Patientidtorefill.Value != "")
                {
                    pharmacypobj.PatientID = Guid.Parse(Patientidtorefill.Value);
                    DataSet MedicinList = pharmacypobj.PrescriptionDetails();
                    var xml = MedicinList.GetXml();
                    hdnXmlData.Value = xml;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);
                }
            }
        }

        #endregion Save Button Click

        #region LogOuT

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {  string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        }

        #endregion LogOuT

        #endregion ClickEvents

        #endregion Events

        //--NOTE: Below events and functions are not using now

        #region Gridviewbind
        public void gridviewbind()
        {
            DataSet gds = pharmacypobj.GetPatientPharmacyDetails(); //Function Call to Get Patient Pharamacy Details

            DataRow[] dr = gds.Tables[0].Select("IsProcessed=False");
            lblPharmacyCount.Text = dr.Length.ToString();

            GridViewPharmacylist.EmptyDataText = "No Records Found";
            GridViewPharmacylist.DataSource = gds;
            GridViewPharmacylist.DataBind();

           // lblPharmacyCount.Text = gds.Tables[0].Rows.Count.ToString();


        }
        #endregion Gridviewbind

        #region Row Coloring for Pharmacy View
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
        protected void GridViewPharmacylist_RowDataBound(object sender, GridViewRowEventArgs e)
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
        #endregion Row Coloring for Pharmacy View

        #region ImageBUtton Click

        protected void ImgBtn_Command(object sender, CommandEventArgs e)
        {
            string[] Visits = e.CommandArgument.ToString().Split(new char[] { '|' });
            string PatientId = Visits[0];
            string DoctorID = Visits[1];
            pharmacypobj.DoctorID = Guid.Parse(DoctorID);
            pharmacypobj.PatientID = Guid.Parse(PatientId);
            tokobj.ClinicID = UA.ClinicID.ToString();
            Patientidtorefill.Value = PatientId;//saving in a hidden field to reill
            DataSet gds = pharmacypobj.GetPatientPharmacyDetailsByID();
            //DataSet ds = tokobj.GetPatientTokenDetailsbyID(PatientId); //Get Patient Token Details by ID Function Call
            lblPatientName.Text = gds.Tables[0].Rows[0][1].ToString();
            lblDoctor.Text = gds.Tables[0].Rows[0][12].ToString();
            lblFileNum.Text = gds.Tables[0].Rows[0][7].ToString();
            lblGenderDis.Text = gds.Tables[0].Rows[0][6].ToString();
            DateTime date =cmn.ConvertDatenow(DateTime.Now);
            int year = date.Year;
            DateTime DT = Convert.ToDateTime(gds.Tables[0].Rows[0][8].ToString());
            int Age = year - DT.Year;
            lblAgeCount.Text = Age.ToString();
            DataSet MedicinList = pharmacypobj.PrescriptionDetails();  //Prescription Details Function Call
            if (MedicinList.Tables[0].Rows.Count > 0)
            {
                hdnPrescID.Value = MedicinList.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                hdnPrescID.Value = "";
            }
            var xml = MedicinList.GetXml();
            hdnXmlData.Value = xml;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);

            string imagetype = gds.Tables[0].Rows[0][13].ToString();
            if (imagetype.Trim() != string.Empty)
            {
                ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientId.ToString();
            }
            else
            {
                ProfilePic.Src = "../images/UploadPic1.png";
            }
        }

        #endregion ImageBUtton Click

    }
}