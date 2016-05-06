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
#endregion Namespaces

using TheClinicApp1._1.ClinicDAL; 

namespace TheClinicApp1._1.Pharmacy
{
    public partial class Pharmacy : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;
        public string listFilter = null;
        public string NameBind = null;

        ErrorHandling eObj = new ErrorHandling();
        PrescriptionDetails PrescriptionObj = new PrescriptionDetails();
        pharmacy pharmacypobj = new pharmacy();
        TokensBooking tokobj = new TokensBooking();
       
        IssueHeaderDetails issuehdobj = new IssueHeaderDetails();

        


        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            lblUserName.Text = "👤 " + Login + " ";
            dtRols = UA.GetRoleName1(Login);
            foreach (DataRow dr in dtRols.Rows)
            {

                RoleName.Add(dr["RoleName"].ToString());

            }
            if (RoleName.Contains(Const.RoleAdministrator))
            {
                //this.hide.style.Add("display", "none");
                this.admin.Style.Add("Visibility", "Visible");
                this.master.Style.Add("Visibility", "Visible");
            }

            pharmacypobj.ClinicID = UA.ClinicID;

            listFilter = null;
            listFilter = GetMedicineNames();
            NameBind = null;
            NameBind = BindName();

            gridviewbind();



            btnSave.Attributes.Add("onclick", "GetTextBoxValuesPres('" + hdnTextboxValues.ClientID + "')");


        }


        public void gridviewbind()
        {
            DataSet gds = pharmacypobj.GetPatientPharmacyDetails();
            GridViewPharmacylist.EmptyDataText = "No Records Found";
            GridViewPharmacylist.DataSource = gds;
            GridViewPharmacylist.DataBind();

        }



        #region WebMethod

        [WebMethod(EnableSession = true)]
        public static string PatientDetails(string file)
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
        #endregion WebMethod

        #region Get Medicine Names

        /// <summary>
        /// Get all medicine names to be binded into list filter
        /// </summary>
        /// <returns></returns>
        private string GetMedicineNames()
        {
            // Patient PatientObj = new Patient();


            DataTable dt = PrescriptionObj.SearchMedicinewithCategory();

            StringBuilder output = new StringBuilder();
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
            return output.ToString();
        }

        #endregion Get Medicine Names

        #region BindSearch
        private string BindName()
        {

            DataTable dt = tokobj.GetSearchBoxData();
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

            IssuedtlsObj.ClinicID = UA.ClinicID.ToString();

            DataSet ds = IssuedtlsObj.GetMedicineDetailsByMedicineName(MedName);
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


        protected void ImgBtn_Command(object sender, CommandEventArgs e)
        {

            string[] Visits = e.CommandArgument.ToString().Split(new char[] { '|' });
            string PatientId = Visits[0];
            string DoctorID = Visits[1];
            pharmacypobj.DoctorID = Guid.Parse(DoctorID);
            pharmacypobj.PatientID = Guid.Parse(PatientId);

            DataSet ds = tokobj.GetPatientTokenDetailsbyID(PatientId);

            lblPatientName.Text = ds.Tables[0].Rows[0][2].ToString();
            lblDoctor.Text = ds.Tables[0].Rows[0][1].ToString();
            lblFileNum.Text = ds.Tables[0].Rows[0][7].ToString();
            lblGenderDis.Text = ds.Tables[0].Rows[0][6].ToString();
            DateTime date = DateTime.Now;
            int year = date.Year;
            DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0][8].ToString());
            int Age = year - DT.Year;
            lblAgeCount.Text = Age.ToString();

            DataSet MedicinList = pharmacypobj.PrescriptionDetails();
            var xml = MedicinList.GetXml();
            hdnXmlData.Value = xml;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);


            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientId.ToString();


        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
             

            if (hdnsave.Value == "" && lblPatientName.Text != "Patient_Name")
            {
            issuehdobj.ClinicID = UA.ClinicID.ToString();
            issuehdobj.IssueNO = issuehdobj.Generate_Issue_Number();
            issuehdobj.IssuedTo = lblPatientName.Text;
            issuehdobj.Date = DateTime.Now;
            issuehdobj.CreatedBy = UA.userName;
            issuehdobj.ClinicID = UA.ClinicID.ToString();
            ViewState["IssueHdrID"] = issuehdobj.IssueID;

            issuehdobj.InsertIssueHeader();



               string last = string.Empty;
               
               string values = hdnTextboxValues.Value;

               string[] Rows = values.Split('$');



               for (int i = 0; i < Rows.Length - 1; i++)
               {
                   IssueDetails IssuedtlObj = new IssueDetails(); //Object is created in loop as each entry should have different uniqueID
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
                         IssuedtlObj.InsertIssueDetails();                  
               }
               hdnsave.Value = "saved";
            }
              else 
            {
                var page = HttpContext.Current.CurrentHandler as Page;

                msg = "NOT SAVED";

                eObj.InsertionNotSuccessMessage(page, msg);

                 
            
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
    }
}