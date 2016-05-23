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
        #region GlobalVariables
        UIClasses.Const Const = new UIClasses.Const();    
        ClinicDAL.UserAuthendication UA;
        ClinicDAL.Patient PatientObj = new ClinicDAL.Patient();
       
        ErrorHandling eObj = new ErrorHandling();
        
        #endregion GlobalVariables
      
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            PatientObj.ClinicID = Guid.Parse(UA.ClinicID.ToString());           

            #region GridAllRegistration
            dtgViewAllRegistration.EmptyDataText = "No Records Found";
            dtgViewAllRegistration.DataSource = PatientObj.GetAllRegistration();
            dtgViewAllRegistration.DataBind();
            #endregion GridAllRegistration
        }
        //#region EditPatients
        //protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        //{ 
        //    DateTime date = DateTime.Now;
        //    int year = date.Year;
        //    string[] Patient = e.CommandArgument.ToString().Split(new char[] { '|' });

        //    Guid PatientID = Guid.Parse(Patient[0]);           
        //    HiddenField1.Value = PatientID.ToString();
        //    HiddenField2.Value=Patient[1];
        //    string Address=Patient[2];
        //    string Phone = Patient[3];
        //    string Gmail = Patient[4];
        //    string DOB=Patient[5];
        //    string Gender = Patient[6];
        //    string Marital = Patient[7];
        //    string Pic = Patient[8];
        //    string Type = Patient[9];
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "SetFields();", true);
        //}
        //#endregion EditPatients

        #region GridDelete
        protected void ImgBtnDelete_Command(object sender, CommandEventArgs e)
        {

            Guid PatientID = Guid.Parse(e.CommandArgument.ToString());
            PatientObj.PatientID = PatientID;
            PatientObj.DeletePatientDetails();
           
        }


        #endregion GridDelete

        #region Paging
        protected void dtgViewAllRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgViewAllRegistration.PageIndex = e.NewPageIndex;
            dtgViewAllRegistration.DataBind();

        }

        #endregion Paging

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            dtgViewAllRegistration.UseAccessibleHeader = false;
            dtgViewAllRegistration.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        {

        }
    }
}