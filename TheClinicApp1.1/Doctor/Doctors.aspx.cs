using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.Doctor
{
    public partial class Doctors : System.Web.UI.Page
    {
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
        public string listFilter=null;
        public string RoleName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
             
            listFilter = null;
            listFilter = GetMedicineNames();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];          
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
            if(RoleName=="Doctor")
            {
                DataTable dt = new DataTable();
                dt = UA.GetDoctorAndDoctorID(Login);
                string DoctorName = dt.Rows[0]["Name"].ToString();
                Guid DoctorID = Guid.Parse(dt.Rows[0]["DoctorID"].ToString());
                VisitsObj.DoctorID = DoctorID;
                PrescriptionHeadObj.DoctorID = DoctorID;
                tok.DoctorID = DoctorID.ToString();
                tok.ClinicID = UA.ClinicID.ToString();
                lblDoctor.Text = "Dr."+DoctorName;

            }
            gridviewbind();
        }


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
            DataTable dt = PatientObj.GetSearchBoxData();

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
        #endregion BindSearch



        protected void btnSave_Click(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];                        
            VisitsObj.ClinicID = UA.ClinicID;
            VisitsObj.FileID = Guid.Parse(HiddenField2.Value);
            int feet = Convert.ToInt32(txtHeightFeet.Value);
            int inch = Convert.ToInt32(txtHeightInch.Value);
            VisitsObj.Height = float.Parse(feet.ToString() + "." + inch.ToString());
            VisitsObj.Weight = float.Parse(txtWeight.Value);
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
            if (HdnForVisitID.Value == "")
            {
                VisitsObj.AddVisits();
            }
            else
            {
                VisitsObj.UpdateVisits();
            }
            if (VisitsObj.PrescriptionID!=Guid.Empty)
            { 
            
            PrescriptionHeadObj.PrescID = VisitsObj.PrescriptionID.ToString();
            PrescriptionHeadObj.VisitID = VisitsObj.VisitID.ToString();
            PrescriptionHeadObj.ClinicID = UA.ClinicID.ToString();
            PrescriptionHeadObj.CreatedBy = UA.userName;
            PrescriptionHeadObj.UpdatedBy = UA.userName;
            PrescriptionHeadObj.CreatedDate = DateTime.Now;
            PrescriptionHeadObj.UpdatedDate = DateTime.Now;
            PrescriptionHeadObj.InsertPrescriptionHeaderDetails();
            PrescriptionObj.PrescID = PrescriptionHeadObj.PrescID.ToString();

            string last = string.Empty;
            string values = hdnTextboxValues.Value;
            string[] Rows = values.Split('$');
            for (int i = 0; i < Rows.Length - 1; i++)
            {
                string[] tempRow = Rows;

                last = tempRow[i].Split('|').Last();

                string[] columns = tempRow[i].Split('|');

                if (last == string.Empty || last == "")
                {

                    //----------------- * CASE : INSERT *-----------------------------------//
                    if ((columns[0] != null) && (columns[1] != null))
                    {

                        PrescriptionObj.MedicineName = columns[0];
                        PrescriptionObj.Qty = Convert.ToInt32(columns[1]);
                        PrescriptionObj.Unit = columns[2];
                        PrescriptionObj.Dosage = Convert.ToInt32(columns[3]);
                        PrescriptionObj.Timing = columns[4];
                        PrescriptionObj.Days = Convert.ToInt32(columns[5]);
                        PrescriptionObj.CreatedBy = UA.userName;
                        PrescriptionObj.ClinicID = UA.ClinicID.ToString();
                        PrescriptionObj.UpdatedBy = UA.userName;                       
                        PrescriptionObj.InsertPrescriptionDetails();

                    }
                }
            }
            }
        }
    
        #region FillPatientDetails
        protected void ImgBtnUpdate_Command1(object sender, CommandEventArgs e)
        {
            DataRow dr = null;
            PatientObj.PatientID = Guid.Parse(e.CommandArgument.ToString());
            Guid PatientIDForFile = Guid.Parse(e.CommandArgument.ToString());

            DoctorObj.PatientIdForFile = PatientIDForFile;
            DataTable DtFileID = DoctorObj.GetFileIDUSingPatientID();
            dr = DtFileID.NewRow();
            dr = DtFileID.Rows[0];
            Guid FileIDForGrid = Guid.Parse(dr["FileID"].ToString());

            DataTable GridBindVisits = new DataTable();
            GridBindVisits = CaseFileObj.GetGridVisits(FileIDForGrid);
            GridViewVisitsHistory.EmptyDataText = "No Records Found";
            GridViewVisitsHistory.DataSource = GridBindVisits;
            GridViewVisitsHistory.DataBind();
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
            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
            //ProfilePic.Visible = true;

            HiddenField1.Value = PatientID.ToString();

        }
        #endregion FillPatientDetails

        


        #region Update Visits
        protected void ImgBtnUpdateVisits_Command(object sender, CommandEventArgs e)
        {

            string[] Visits = e.CommandArgument.ToString().Split(new char[] { '|' });
            CaseFileObj.VisitID = Guid.Parse(Visits[0]);
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
            HdnForVisitID.Value=Visits[0];
            DataSet MedicinList = PrescriptionObj.ViewPrescriptionDetails(PrescriptionID);
            var xml = MedicinList.GetXml();
            hdnXmlData.Value = xml;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "func", "FillTextboxUsingXml();", true);
        }
        #endregion Update Visits

        #region GridBindTokens
        public void gridviewbind()
        {
            //Gridview Binding to Diplay DoctorName,Token No,Patient Name,TIME
            tok.DateTime = DateTime.Now;
            DataSet gds = tok.DoctorViewToken();
            GridViewTokenlist.EmptyDataText = "No Records Found";
            GridViewTokenlist.DataSource = gds;
            GridViewTokenlist.DataBind();


        }
        #endregion GridBindTokens


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
          

            if (ds.Tables[0].Rows.Count > 0)
            {
                Unit = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
              
            }

            return String.Format("{0}", Unit);


        }


        #endregion Get MedicineDetails By Medicine Name

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            HdnForVisitID.Value = string.Empty;
        }
    }
}