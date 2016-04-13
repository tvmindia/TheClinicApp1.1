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
        ClinicDAL.Patient PatientObj = new ClinicDAL.Patient();
        public string listFilter=null;
        public string RoleName = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            listFilter = null;
            listFilter = BindName();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
        }

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
          
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            ClinicDAL.CaseFile.Visit VisitsObj = new ClinicDAL.CaseFile.Visit();
            VisitsObj.DoctorID = Guid.Parse("469489AE-6237-47B3-B8CC-74B62EC81D77");
            VisitsObj.ClinicID = Guid.Parse("2C7A7172-6EA9-4640-B7D2-0C329336F289");
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
            VisitsObj.CreatedBy = "thomson";
            VisitsObj.UpdatedBy = "thomson";
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
            string values = HiddenField1.Value;
            if (values != null)
            {
                string[] Invalue = values.Split('|');
                int count = Invalue.Length - 1;
                for (int i = 0; i < count; i = i + 4)
                {
                    string w = Invalue[i], x = Invalue[i + 1], y = Invalue[i + 2], z = Invalue[i + 3];
                }
            } 
        }
    }
}