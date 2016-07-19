
#region Included Namespaces

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

#endregion Included Namespaces

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

        #region Methods

        #region Dropdown
        public void dropdowndoctor()
        {
            //binding the values of doctor dropdownlist
            DataSet ds = tokenObj.DropBindDoctorsName();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDoctor.Items.Clear();
                ddlDoctor.DataSource = ds.Tables[0];
                ddlDoctor.DataValueField = "DoctorID";
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataBind();
                if (ddlDoctor.Items.Count != 1)//checking number of doctors.if there is only one doctor, no need of select 
                {
                    ddlDoctor.Items.Insert(0, "--Select--");
                }
            }


        }

        #endregion Dropdown

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
            DataTable dt = tokenObj.GetSearchBoxData(); //Function call to get  Search BoxData
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
        /// <summary>
        /// WEB METHOD called from javascript from aspx page
        /// To Get Patient Details  
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]

        public static string PatientDetails(string file)
        {
            string FileNumber = string.Empty;
            string Name = string.Empty;
            string Gender = string.Empty;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = string.Empty;
            string PatientID = string.Empty;
            string ClinicID = string.Empty;
            string lastvisit = string.Empty;
            string DOB = string.Empty;

            ClinicDAL.TokensBooking obj = new ClinicDAL.TokensBooking();
            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            obj.ClinicID = UA.ClinicID.ToString();
            DataSet ds = obj.GetpatientDetails(file);  //Function Call to Get Patient Details
            if (ds.Tables[0].Rows.Count > 0)
            {
                FileNumber = Convert.ToString(ds.Tables[0].Rows[0]["FileNumber"]);
                Name = Convert.ToString(ds.Tables[0].Rows[0]["Name"]);
                Gender = Convert.ToString(ds.Tables[0].Rows[0]["Gender"]);
                Address = Convert.ToString(ds.Tables[0].Rows[0]["Address"]);
                Phone = Convert.ToString(ds.Tables[0].Rows[0]["Phone"]);
                Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);

                PatientID = Convert.ToString(ds.Tables[0].Rows[0]["PatientID"]);
                ClinicID = Convert.ToString(ds.Tables[0].Rows[0]["ClinicID"]);
                lastvisit = Convert.ToString(ds.Tables[0].Rows[0]["LastVisitDate"]);


                DateTime date = DateTime.Now;
                int year = date.Year;
                DateTime DT = Convert.ToDateTime(ds.Tables[0].Rows[0]["DOB"].ToString());
                int Age = year - DT.Year;
                DOB = Age.ToString();
            }
            return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + "|" + "{3}" + " | " + "{4}" + "|" + "{5}" + " | " + "{6}" + "|" + "{7}" + " | " + "{8}" + " | " + "{9}", FileNumber, Name, DOB, Gender, Address, Phone, Email, PatientID, ClinicID, lastvisit);

        }
        #endregion WebMethod

        #endregion Methods

        #region Events

        #region PageLoad

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
                dropdowndoctor();
            }
            if (Request.QueryString["id"] != null)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.DeleteSuccessMessage(page);
                //  info.Visible = false;
            }
        }

        #endregion PageLoad

        #region ButtonClicks

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
            ddlDoctor.SelectedIndex = 0;
            dropdowndoctor();

        }

        protected void btnBookToken_ServerClick(object sender, EventArgs e)
        {
            string msg = string.Empty;
            if (hdnfileID.Value != "")
            {
                tokenObj.DoctorID = ddlDoctor.SelectedValue;
                tokenObj.PatientID = HiddenPatientID.Value;
                tokenObj.ClinicID = HiddenClinicID.Value;
                tokenObj.CreatedBy = UA.userName;
                tokenObj.DateTime = DateTime.Now;
                int tokenNo = tokenObj.InsertToken(); //Function Call Inserting Token
                lblToken.Text = tokenNo.ToString();
                lblToken.Visible = true;
                hdnfileID.Value = "";
                //diplaying number of bookings
                DataSet gds = tokenObj.ViewToken();  //Fuction Call to Get number of Tokens 
                lblCaseCount.Text = gds.Tables[0].Rows.Count.ToString();

                // reloading values in to repective fields
                DataSet dst = tokenObj.GetPatientTokenDetailsbyID(HiddenPatientID.Value); //Function Call to Get Patient Token Details
                if (dst != null && dst.Tables[0].Rows.Count > 0)
                {
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

                    dropdowndoctor();
                    // info.Visible = false;
                }
            }
            else
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                msg = "Please select Patient Details in Search";
                eObj.InsertionNotSuccessMessage(page, msg);
                //info.Visible = true;
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

        #endregion ButtonClicks

        #endregion Events

       
    }
}