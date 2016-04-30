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

        PrescriptionDetails PrescriptionObj = new PrescriptionDetails();
        pharmacy pharmacypobj = new pharmacy();
        


        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);


            pharmacypobj.ClinicID = UA.ClinicID;

            listFilter = null;
            listFilter = GetMedicineNames();

            gridviewbind();


           


        }


        public void gridviewbind()
        {
            DataSet gds = pharmacypobj.GetPatientPharmacyDetails();
            GridViewPharmacylist.EmptyDataText = "No Records Found";
            GridViewPharmacylist.DataSource = gds;
            GridViewPharmacylist.DataBind();

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

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void ImgBtn_Command(object sender, CommandEventArgs e)
        {

        }







    }
}