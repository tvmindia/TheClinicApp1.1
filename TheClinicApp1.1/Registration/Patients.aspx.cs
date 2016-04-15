﻿#region CopyRight

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

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
            
            gridDataBind();
            listFilter = null;
            listFilter = BindName();
            
        }

        

       
        #region Methods

        #region GridBind
        public void gridDataBind()
        {
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
                        
            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
            ProfilePic.Visible = true;
            //btnnew.Visible = true;
            HiddenField1.Value = PatientID.ToString();
            
        }
        #endregion EditPatients

        #region SearchButtonClick
        //protected void btnSearch_ServerClick1(object sender, EventArgs e)
        //{
           
        //    DataRow dr = null;
        //    string path = Server.MapPath("~/Content/ProfilePics/").ToString();
        //    string Name = Request.Form["txtSearch"];
        //    if (Name != " ")
        //    {
        //        DataTable dt = PatientObj.GetSearchWithName(Name);
        //        dr = dt.NewRow();
        //        dr = dt.Rows[0];
        //        DateTime date = DateTime.Now;
        //        int year = date.Year;
        //        Guid PatientID = Guid.Parse(dr["PatientID"].ToString());
        //        txtName.Value = dr["Name"].ToString();
        //        string Gender= dr["Gender"].ToString();
        //        if (Gender.Trim() == "Male")
        //        {

        //            rdoMale.Checked = true;
        //        }
        //        else if (Gender.Trim() == "Female")
        //        {
        //            rdoFemale.Checked = true;
        //        }
        //        DateTime DT = Convert.ToDateTime(dr["DOB"].ToString());
        //        int Age = year - DT.Year;
        //        txtAge.Value = Age.ToString();
        //        txtAddress.Value = dr["Address"].ToString();
        //        txtMobile.Value = dr["Phone"].ToString();
        //        txtEmail.Value = dr["Email"].ToString();
        //        string Status= dr["MaritalStatus"].ToString();
        //        //
        //        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
        //        HiddenField1.Value = PatientID.ToString();
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Invalid Suggesion');", true);

        //    }
        //}
        #endregion SearchButtonClick

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
        protected void btnnew_Click(object sender, EventArgs e)
        {
            ClearFields();
            ProfilePic.Src = "../Images/UploadPic.png";
            //btnnew.Visible = false;
            divDisplayNumber.Visible = false;
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

       
        #endregion Methods

        
       

        

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

                PatientObj.Occupation = (txtOccupation.Value != "") ? txtOccupation.Value.ToString() : null;
                PatientObj.CreatedBy = UA.userName;
                PatientObj.UpdatedBy = UA.userName;
                if ((PatientObj.Name != null) && (PatientObj.Address != null))
                {
                    string filenum = "F" + clinID.Substring(0, 4) + txtName.Value.Substring(0, 3) + txtMobile.Value.Substring(7, 3);
                    PatientObj.FileNumber = filenum.Trim();

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

                        var page = HttpContext.Current.CurrentHandler as Page;

                    }
                    else
                    {
                        PatientObj.PatientID = Guid.Parse(HiddenField1.Value);
                        PatientObj.UpdatePatientDetails();
                        var page = HttpContext.Current.CurrentHandler as Page;
                    }
                }
                gridDataBind();
                lblFileCount.Text = PatientObj.FileNumber;
                //lblFile.Text = PatientObj.FileNumber;
                //lblName.Text = txtName.Value;
                //lblAge.Text = txtAge.Value;
                //lblPhone.Text = txtMobile.Value;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "openModal();", true);
            }
            catch
            {
                Response.Redirect("../Registration/Patients.aspx");
            }
        }

       

       
    }
}