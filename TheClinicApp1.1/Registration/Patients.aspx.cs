#region CopyRight

//Author      : Thomson K Varkey
//Created Date: Feb-04-2016

#endregion CopyRight

#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
#endregion Included Namespaces


namespace TheClinicApp1._1.Registration
{
    public partial class Patients : System.Web.UI.Page
    {
        #region GlobalVariables
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Patient PatientObj = new Patient();
        TokensBooking tok = new TokensBooking();
        ErrorHandling eObj = new ErrorHandling();
        public string listFilter = null;
        public string RoleName = null;
        #endregion GlobalVariables

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
            tok.ClinicID = UA.ClinicID.ToString();           
            gridDataBind();
            listFilter = null;
            listFilter = BindName();
            
        }
        #endregion PageLoad

        #region Methods

        #region GridBind
        public void gridDataBind()
        {
            #region GridAllRegistration
            dtgViewAllRegistration.EmptyDataText = "No Records Found";
            dtgViewAllRegistration.DataSource = PatientObj.GetAllRegistration();
            dtgViewAllRegistration.DataBind();
            #endregion GridAllRegistration
            #region GridDateRegistration
            dtgViewTodaysRegistration.EmptyDataText = "....Till Now No Registration....";
            dtgViewTodaysRegistration.DataSource = PatientObj.GetDateRegistration();
            dtgViewTodaysRegistration.DataBind();
            #endregion GridDateRegistration
            listFilter = null;
            listFilter = BindName();
            DropdownDoctors();
        }
        #endregion GridBind

        #region BindDropdownDoc
        public void DropdownDoctors()
        {
            DataSet ds = tok.DropBindDoctorsName();          
            ddlDoctorName.DataSource = ds.Tables[0];
            ddlDoctorName.DataValueField = "DoctorID";
            ddlDoctorName.DataTextField = "Name";
            ddlDoctorName.DataBind();
        }
        #endregion BindDropdownDOc

        #region BindDataAutocomplete
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
        #endregion BindDataAutocomplete

        #region EditPatients
        protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        {
            
            DateTime date = DateTime.Now;
            int year = date.Year;
            string[] Patient = e.CommandArgument.ToString().Split(new char[] { '|' });
            Guid PatientID = Guid.Parse(Patient[0]);
            txtName.Value = Patient[1];
            if (Patient[6].Trim() == "Male")
            {
                rdoFemale.Checked = false;
                rdoMale.Checked = true;
            }
            else if (Patient[6].Trim() == "Female")
            {
                rdoMale.Checked = false;
                rdoFemale.Checked = true;
            }
            
            DateTime dt = Convert.ToDateTime(Patient[5]);
            int Age = year - dt.Year;
            txtAge.Value = Age.ToString();
            txtAddress.Value = Patient[2];
            txtMobile.Value = Patient[3];
            txtEmail.Value = Patient[4];
            ddlMarital.SelectedValue = Patient[7];
            txtOccupation.Value = Patient[8];            
            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
            ProfilePic.Visible = true;
            //btnnew.Visible = true;
            HiddenField1.Value = PatientID.ToString();
            
        }
        #endregion EditPatients

        
        
        
       
        #region GridDelete
        protected void ImgBtnDelete_Command(object sender, CommandEventArgs e)
        {   
            Guid PatientID = Guid.Parse(e.CommandArgument.ToString());
            PatientObj.PatientID = PatientID;
            PatientObj.DeletePatientDetails();
            gridDataBind();
        }

       
        #endregion GridDelete

        #region clearfield
        public void ClearFields()
        {
            txtName.Value = "";

            txtAge.Value = "";
            txtAddress.Value = "";
            txtEmail.Value = "";
            txtMobile.Value = "";
            txtOccupation.Value = "";
            rdoFemale.Checked=false;
            rdoMale.Checked=false;            
            ProfilePic.Src = "../Images/UploadPic.png";
        }
        #endregion clearfield

        #region BookingToken
        protected void btntokenbooking_Click(object sender, EventArgs e)
        {
            //btnnew.Visible = true;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tok.DoctorID = ddlDoctorName.SelectedValue;
            tok.PatientID = HiddenField1.Value;
            if(tok.PatientID=="")
            {
                tok.PatientID = PatientObj.GetPatientID(txtName.Value, txtAddress.Value, txtMobile.Value, txtEmail.Value).ToString();
                
            }
            tok.ClinicID = UA.ClinicID.ToString();
            tok.CreateDate = DateTime.Now;
            tok.DateTime = DateTime.Now;
            tok.CreatedBy = UA.userName;
            int tokenNo = tok.InsertToken();
            lblTokencount.Text = ":" + tokenNo.ToString();
            //lblToken.Visible = true;
            divDisplayNumber.Visible = true;
        }
        #endregion BookingToken

        #region ClearScreen
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            ClearFields();
            ProfilePic.Src = "../Images/UploadPic1.png";
            divDisplayNumber.Visible = false;
            lblMsgges.Text = string.Empty;
            lblErrorCaption.Text = string.Empty;
            Errorbox.Visible = false;
            HiddenField1.Value = string.Empty;
        }
        #endregion ClearScreen

        #region convertImage
        private byte[] ConvertImageToByteArray(FileUpload fuImage)
        {
            byte[] ImageByteArray;
            try
            {
                MemoryStream ms = new MemoryStream(fuImage.FileBytes);
                ImageByteArray = ms.ToArray();
                return ImageByteArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion convertImage

        #region Paging
        protected void dtgViewAllRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {          
            dtgViewAllRegistration.PageIndex = e.NewPageIndex;
            dtgViewAllRegistration.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "openmyModal();", true);
        }

        #endregion Paging
        #endregion Methods

        #region MainButton
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            try
            {

                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
                PatientObj.ClinicID = UA.ClinicID;
                DateTime _date = DateTime.Now;
                if (txtAge.Value != "")
                {

                    int age = Convert.ToInt32(txtAge.Value);
                    int year = _date.Year;
                    int DOB = year - age;
                    PatientObj.DOB = DateTime.Parse(DOB + "-01-01");
                }
                string clinID = UA.ClinicID.ToString();
                PatientObj.Name = (txtName.Value != "") ? txtName.Value.ToString() : null;
                PatientObj.Address = (txtAddress.Value != "") ? txtAddress.Value.ToString() : null;
                PatientObj.Phone = (txtMobile.Value != "") ? txtMobile.Value.ToString() : null;
                PatientObj.Email = (txtEmail.Value != "") ? txtEmail.Value.ToString() : null;
                if (rdoMale.Checked == true)
                {
                    PatientObj.Gender = "Male";
                }
                else
                {
                    if (rdoFemale.Checked == true)
                    {
                        PatientObj.Gender = "Female";
                    }
                }
                PatientObj.MaritalStatus = ddlMarital.SelectedItem.ToString();
                PatientObj.Occupation = (txtOccupation.Value != "") ? txtOccupation.Value.ToString() : null;
                PatientObj.CreatedBy = UA.userName;
                PatientObj.UpdatedBy = UA.userName;
                if (PatientObj.Name != null)
                {
                    
                    PatientObj.FileNumber = PatientObj.Generate_File_Number().ToString();


                    if (HiddenField1.Value == "")
                    {
                        if (FileUpload1.HasFile)
                        {
                            byte[] ImageByteArray = null;
                            ImageByteArray = ConvertImageToByteArray(FileUpload1);
                            PatientObj.Picupload = ImageByteArray;
                            PatientObj.ImageType = Path.GetExtension(FileUpload1.PostedFile.FileName);

                        }

                        Guid g = Guid.NewGuid();
                        PatientObj.PatientID = g;
                        PatientObj.AddPatientDetails();
                        PatientObj.AddFile();
                        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + g.ToString();
                        var page = HttpContext.Current.CurrentHandler as Page;

                    }
                    else
                    {
                        if (FileUpload1.HasFile)
                        {
                            byte[] ImageByteArray = null;
                            ImageByteArray = ConvertImageToByteArray(FileUpload1);
                            PatientObj.PatientID = Guid.Parse(HiddenField1.Value);
                            PatientObj.Picupload = ImageByteArray;
                            PatientObj.ImageType = Path.GetExtension(FileUpload1.PostedFile.FileName);
                            PatientObj.UpdatePatientPicture();

                        }
                        PatientObj.PatientID = Guid.Parse(HiddenField1.Value);
                        PatientObj.UpdatePatientDetails();                 
                        var page = HttpContext.Current.CurrentHandler as Page;
                        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + HiddenField1.Value.ToString();
                    }
                }
                gridDataBind();
                lblFileCount.Text = PatientObj.FileNumber;
                if (HiddenField1.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "openModal();", true);
                }
            }
            catch
            {
                Response.Redirect("../Registration/Patients.aspx");
            }
        }
        #endregion MainButton

        #region SearchButtonClick
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                
                DataRow dr = null;
                string path = Server.MapPath("~/Content/ProfilePics/").ToString();
                string Name = Request.Form["txtSearch"];
                if (Name != "")
                {
                    DataTable dt = PatientObj.GetSearchWithName(Name);
                    dr = dt.NewRow();
                    dr = dt.Rows[0];
                    DateTime date = DateTime.Now;
                    int year = date.Year;
                    Guid PatientID = Guid.Parse(dr["PatientID"].ToString());
                    txtName.Value = dr["Name"].ToString();
                    string Gender = dr["Gender"].ToString();
                    if (Gender.Trim() == "Male")
                    {

                        rdoMale.Checked = true;
                    }
                    else if (Gender.Trim() == "Female")
                    {
                        rdoFemale.Checked = true;
                    }
                    DateTime DT = Convert.ToDateTime(dr["DOB"].ToString());
                    int Age = year - DT.Year;
                    txtAge.Value = Age.ToString();
                    txtAddress.Value = dr["Address"].ToString();
                    txtMobile.Value = dr["Phone"].ToString();
                    txtEmail.Value = dr["Email"].ToString();
                    string Status = dr["MaritalStatus"].ToString();
                    ddlMarital.SelectedValue = Status;
                    txtOccupation.Value = dr["Occupation"].ToString();

                    ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
                    HiddenField1.Value = PatientID.ToString();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Invalid Suggesion');", true);

                }
            }
            catch
            {
                Response.Redirect("../Registration/Patient.aspx");
            }
        }
        #endregion SearchButtonClick
    }
}