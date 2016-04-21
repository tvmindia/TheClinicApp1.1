using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        ClinicDAL.CaseFile.Visit VisitsObj = new ClinicDAL.CaseFile.Visit();
        public string listFilter=null;
        public string RoleName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
             //OnClientClick="GetTextBoxValuesPres('<%=hdnTextboxValues.ClientID%>');"
            //btnSave.Attributes.Add("OnClientClick", "GetTextBoxValuesPres('" + hdnTextboxValues.ClientID + "')");
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
                tok.DoctorID = DoctorID.ToString();
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
            VisitsObj.FileID = Guid.Parse("232CFE06-E9E9-42C1-B2F6-B992ABE0140A");
            int feet = Convert.ToInt32(txtHeightFeet.Value);
            int inch = Convert.ToInt32(txtHeightInch.Value);
            VisitsObj.Height = float.Parse(feet.ToString() + "." + inch.ToString());
            VisitsObj.Weight = float.Parse(txtWeight.Value);
            VisitsObj.Symptoms = (symptoms.Value != null) ? symptoms.Value.ToString() : null;
            VisitsObj.Cardiovascular = (cardiovascular.Value != null) ? cardiovascular.Value.ToString() : null;
            VisitsObj.Nervoussystem = (nervoussystem.Value != null) ? nervoussystem.Value.ToString() : null;
            VisitsObj.Musculoskeletal = (musculoskeletal.Value != null) ? musculoskeletal.Value.ToString() : null;
            VisitsObj.Palloe = (palloe.Value != null) ? palloe.Value.ToString() : null;
            VisitsObj.Icterus = (icterus.Value != null) ? icterus.Value.ToString() : null;
            VisitsObj.Clubbing = (clubbing.Value != null) ? clubbing.Value.ToString() : null;
            VisitsObj.Cyanasis = (cyanasis.Value != null) ? cyanasis.Value.ToString() : null;
            VisitsObj.Edima = (edima.Value != null) ? edima.Value.ToString() : null;
            VisitsObj.Bowel = (bowel.Value != null) ? bowel.Value.ToString() : null;
            VisitsObj.Appettie = (appettie.Value != null) ? appettie.Value.ToString() : null;
            VisitsObj.Micturation = (micturation.Value != null) ? micturation.Value.ToString() : null;
            VisitsObj.Sleep = (sleep.Value != null) ? sleep.Value.ToString() : null;
            VisitsObj.Diagnosys = (diagnosys.Value != null) ? diagnosys.Value.ToString() : null;
            VisitsObj.Remarks = (remarks.Value != null) ? remarks.Value.ToString() : null;
            VisitsObj.CreatedBy = UA.userName;
            VisitsObj.UpdatedBy = UA.userName;
            VisitsObj.Bp = (bp.Value != null) ? bp.Value.ToString() : null;
            VisitsObj.Pulse = (pulse.Value != null) ? pulse.Value.ToString() : null;
            VisitsObj.Tounge = (tounge.Value != null) ? tounge.Value.ToString() : null;
            VisitsObj.Heart = (heart.Value != null) ? heart.Value.ToString() : null;
            VisitsObj.LymphGen = (lymphGen.Value != null) ? lymphGen.Value.ToString() : null;
            VisitsObj.LymphClinic = (lymphnodes.Value != null) ? lymphnodes.Value.ToString() : null;
            VisitsObj.RespRate = (resp_rate.Value != null) ? resp_rate.Value.ToString() : null;
            VisitsObj.Others = (others.Value != null) ? others.Value.ToString() : null;
            if (VisitsObj.Diagnosys != "")
            {
                VisitsObj.AddVisits();
            }

            //Fetching values of prescription from Design throurh Hiddenfield
            //string values = HiddenField1.Value;
            //if (values != null)
            //{
            //    string[] Invalue = values.Split('|');
            //    int count = Invalue.Length - 1;
            //    for (int i = 0; i < count; i = i + 4)
            //    {
            //        string w = Invalue[i], x = Invalue[i + 1], y = Invalue[i + 2], z = Invalue[i + 3];
            //    }
            //}

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
                        PrescriptionObj.CreatedDate = DateTime.Now;

                        //if (ViewState["IssueHdrID"] != null && ViewState["IssueHdrID"].ToString() != string.Empty)
                        //{
                        //    //PrescriptionObj.IssueID = Guid.Parse(ViewState["IssueHdrID"].ToString());
                        //}
                        PrescriptionObj.InsertPrescriptionDetails();

                    }
                }

               // if (last != string.Empty)
                //{
                    //----------------- * CASE : UPDATE *---------------------------------//

                    //if ((columns[0] != null) && (columns[4] != null))
                    //{
                    //    string uniqueID = last;
                    //    IssueDetails UpIssueDtlObj = new IssueDetails(new Guid(uniqueID));

                    //    UpIssueDtlObj.ClinicID = UA.ClinicID.ToString();
                    //    UpIssueDtlObj.Qty = Convert.ToInt32(columns[4]);
                    //    UpIssueDtlObj.UpdatedBy = UA.userName;

                    //    //string medicineID = IssuedtlObj.GetMedcineIDByMedicineName(columns[0]);

                    //    UpIssueDtlObj.UpdateIssueDetails(uniqueID);
                    //}
                //
            //}

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

            //HiddenField1.Value = PatientID.ToString();
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
            //btnnew.Visible = true;
            string PrescriptionID = Visits[1];
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
    }
}