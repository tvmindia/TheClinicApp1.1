using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.Registration
{
    public partial class ViewAllRegistration : System.Web.UI.Page
    {
        ClinicDAL.Patient PatientObj=new ClinicDAL.Patient();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region GridAllRegistration
            dtgViewAllRegistration.EmptyDataText = "No Records Found";
            dtgViewAllRegistration.DataSource = PatientObj.GetAllRegistration();
            dtgViewAllRegistration.DataBind();
            #endregion GridAllRegistration
        }
    }
}