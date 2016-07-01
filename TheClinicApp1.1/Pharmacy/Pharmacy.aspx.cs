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

#endregion Namespaces

namespace TheClinicApp1._1.Pharmacy
{
    public partial class Pharmacy : System.Web.UI.Page
    {

        #region Gobalvariables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;
        public string listFilter = null;
        public string NameBind = null;

        ErrorHandling eObj = new ErrorHandling();
        PrescriptionDetails PrescriptionObj = new PrescriptionDetails();
        pharmacy pharmacypobj = new pharmacy();
        TokensBooking tokobj = new TokensBooking();
        Patient patobj = new Patient();
       
        IssueHeaderDetails issuehdobj = new IssueHeaderDetails();

        #endregion Gobalvariables

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string Login = UA.userName;
            tokobj.ClinicID = UA.ClinicID.ToString();
            RoleName= UA.GetRoleName1(Login);  
            pharmacypobj.ClinicID = UA.ClinicID;
            PrescriptionObj.ClinicID = UA.ClinicID.ToString();
            listFilter = null;
            listFilter = GetMedicineNames();
            NameBind = null;
            NameBind = BindName();
            gridviewbind();
            btnSave.Attributes.Add("onclick", "return  GetTextBoxValuesPres('" + hdnTextboxValues.ClientID + "','" + lblErrorCaption.ClientID + "','" + Errorbox.ClientID + "','" + lblMsgges.ClientID + "');");
        }
        #endregion Pageload

        #region Gridviewbind
        public void gridviewbind()
        {
            DataSet gds = pharmacypobj.GetPatientPharmacyDetails(); //Function Call to Get Patient Pharamacy Details
            GridViewPharmacylist.EmptyDataText = "No Records Found";
            GridViewPharmacylist.DataSource = gds;
            GridViewPharmacylist.DataBind();

            lblPharmacyCount.Text = gds.Tables[0].Rows.Count.ToString();


        }
        #endregion Gridviewbind

        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static string PatientDetails(string file)
        {
            ClinicDAL.TokensBooking obj = new ClinicDAL.TokensBooking();
            UIClasses.Const Const = new UIClasses.Const();
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

            DateTime date = DateTime.Now;
            int year = date.Year;
            DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
            int Age = year - DT.Year;
            string DOB = Age.ToString();

            return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + "|" + "{3}" + " | " + "{4}" + "|" + "{5}" + " | " + "{6}" + "|" + "{7}" + " | " + "{8}", FileNumber, Name, DOB, Gender, Address, Phone, Email, PatientID, ClinicID);

        }

        #endregion WebMethod

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
            string Quantity="";

            if (ds.Tables[0].Rows.Count > 0)
            {
                Unit = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
                Quantity = Convert.ToString(ds.Tables[0].Rows[0]["Qty"]);
            }
            return String.Format("{0}"+"|"+"{1}", Unit,Quantity);
        }

        #endregion Get MedicineDetails By Medicine Name

        #region ClickEvents

        protected void ImgBtn_Command(object sender, CommandEventArgs e)
        {
            string[] Visits = e.CommandArgument.ToString().Split(new char[] { '|' });
            string PatientId = Visits[0];
            string DoctorID = Visits[1];
            pharmacypobj.DoctorID = Guid.Parse(DoctorID);
            pharmacypobj.PatientID = Guid.Parse(PatientId);
            tokobj.ClinicID = UA.ClinicID.ToString();
            Patientidtorefill.Value = PatientId;//saving in a hidden field to reill
            DataSet ds = tokobj.GetPatientTokenDetailsbyID(PatientId); //Get Patient Token Details by ID Function Call
            lblPatientName.Text = ds.Tables[0].Rows[0][2].ToString();
            lblDoctor.Text = ds.Tables[0].Rows[0][1].ToString();
            lblFileNum.Text = ds.Tables[0].Rows[0][7].ToString();
            lblGenderDis.Text = ds.Tables[0].Rows[0][6].ToString();
            DateTime date = DateTime.Now;
            int year = date.Year;
            DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0][8].ToString());
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
            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientId.ToString();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            DataRow dr = null;
          
            if (HiddenPatientID.Value != "" || Patientidtorefill.Value!="")
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
                    issuehdobj.Date = DateTime.Now;
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
                    lblPatientName.Text = "PATIENT_NAME";
                    lblAgeCount.Text = "AGE";
                    lblGenderDis.Text = "GENDER";
                    lblFileNum.Text = "FILE NO";
                    lblDoctor.Text = "";
                    HiddenPatientID.Value = "";
                    Patientidtorefill.Value = "";
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

        #endregion ClickEvents

    }
}