using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using TheClinicApp1._1.ClinicDAL;
using System.Text;
using System.Web.Services;


namespace TheClinicApp1._1.Token
{
    public partial class Tokens : System.Web.UI.Page
    {


        #region Global Variables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string listFilter = null;
        public string RoleName = null;

        TokensBooking tokenObj = new TokensBooking();
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
           
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];           
            string Login = UA.userName;
            RoleName = UA.GetRoleName1(Login);
                        
            tokenObj.ClinicID = UA.ClinicID.ToString();
            DataSet gds = tokenObj.ViewToken();
            lblCaseCount.Text = gds.Tables[0].Rows.Count.ToString();
            listerfilterbind();
            if (!IsPostBack)
            {       
                //binding the values of doctor dropdownlist
                DataSet ds = tokenObj.DropBindDoctorsName();
                ddlDoctor.DataSource = ds.Tables[0];
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataBind();
            }

           
        }   
        #region listerfilterbind
        public void listerfilterbind()
        { 
            listFilter = null;
            listFilter = BindName();
        }
        #endregion listerfilterbind

        #region BindDataAutocomplete
        private string BindName()
        {
            DataTable dt = tokenObj.GetSearchBoxData();
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
        #endregion BindDataAutocomplete

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
            
            return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + "|" + "{3}" + " | " + "{4}" + "|" + "{5}" + " | " + "{6}" + "|" + "{7}" + " | " + "{8}", FileNumber, Name, DOB, Gender, Address, Phone, Email, PatientID, ClinicID );
                       
        }        
        #endregion WebMethod
 
        public void ClearFields()
        {
            
                lblFileNo.Text = "";
                lblPatientName.Text = "name";
                lblAge.Text = "";
                lblGender.Text = "";
                lblAddress.Text = "";
                lblMobile.Text = "";
                lblEmail.Text = "";
                lblLastVisit.Text = "";
                lblToken.Text = "_";
                BookedDoctorName.Visible = false;
                lblDoctor.Visible = false;

        }
     
        protected void btnBookToken_ServerClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (hdnfileID.Value!="")
            {
                tokenObj.DoctorID = ddlDoctor.SelectedValue;
                tokenObj.PatientID = HiddenPatientID.Value;
                tokenObj.ClinicID = HiddenClinicID.Value;
                tokenObj.CreatedBy = UA.userName;
                tokenObj.DateTime = DateTime.Now;

                int tokenNo = tokenObj.InsertToken();
                lblToken.Text = tokenNo.ToString();
                lblToken.Visible = true;
                hdnfileID.Value = "";
                //diplaying number of bookings
                DataSet gds = tokenObj.ViewToken();
                lblCaseCount.Text = gds.Tables[0].Rows.Count.ToString();

                // reloading values in to repective fields
                DataSet dst = tokenObj.GetPatientTokenDetailsbyID(HiddenPatientID.Value);              

                lblFileNo.Text = Convert.ToString(dst.Tables[0].Rows[0]["FileNumber"]);
                lblPatientName.Text = Convert.ToString(dst.Tables[0].Rows[0]["Name"]);
                lblGender.Text = Convert.ToString(dst.Tables[0].Rows[0]["Gender"]);
                lblAddress.Text = Convert.ToString(dst.Tables[0].Rows[0]["Address"]);
                lblMobile.Text = Convert.ToString(dst.Tables[0].Rows[0]["Phone"]);
                lblEmail.Text = Convert.ToString(dst.Tables[0].Rows[0]["Email"]);
                lblLastVisit.Text = Convert.ToString(dst.Tables[0].Rows[0]["LastVisitDate"]);

                DateTime date = DateTime.Now;
                int year = date.Year;
                DateTime DT = Convert.ToDateTime(dst.Tables[0].Rows[0]["DOB"].ToString());
                int Age = year - DT.Year;
                lblAge.Text = Age.ToString();

                BookedDoctorName.Visible = true;
                lblDoctor.Visible = true;
                lblDoctor.Text = Convert.ToString(dst.Tables[0].Rows[0]["DoctorName"]);




            }
            else 
            {
                var page = HttpContext.Current.CurrentHandler as Page;

                msg = "Please select Patient Details in Search";

                eObj.InsertionNotSuccessMessage(page, msg);

                ClearFields();
            
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