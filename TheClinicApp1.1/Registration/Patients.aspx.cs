#region CopyRight

//Author      : Thomson K Varkey
//Created Date: Feb-04-2016

#endregion CopyRight

#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
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
        private static int PageSize = 8;
        #endregion GlobalVariables

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            DataTable dt = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            //*Binding Clinic Name 
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            lblUserName.Text = "👤 " + Login+" ";
            dt=UA.GetRoleName1(Login);
            foreach (DataRow dr in dt.Rows)
            {
                RoleName.Add(dr["RoleName"].ToString());
            }

            //*Check Roles Assigned and Giving Visibility For Admin Tab
            if(RoleName.Contains(Const.RoleAdministrator))
            {
                admin.Visible = true;
                master.Visible = true;
                //this.admin.Style.Add("Visibility", "Visible");
                //this.master.Style.Add("Visibility", "Visible");
            }
            tok.ClinicID = UA.ClinicID.ToString();
            PatientObj.ClinicID = Guid.Parse(UA.ClinicID.ToString());
            gridDataBind();

            //*Binding The Search Box 
            listFilter = null;
            listFilter = BindName();           
        }
        #endregion PageLoad

        #region Methods
       
        #region GridBind
        public void gridDataBind()
        {
            
            #region GridAllRegistration   
            //*Grid Bind Showing All Registered Patients In the Modal POPUP

            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataSource = PatientObj.GetAllRegistration();
            GridView1.DataBind();
            #endregion GridAllRegistration

            #region GridTodaysRegistration
            //*Grid Bind Showing Todays Registered Patients In the Modal POPUP

            dtgViewTodaysRegistration.EmptyDataText = "....Till Now No Registration....";
            dtgViewTodaysRegistration.DataSource = PatientObj.GetDateRegistration();
            dtgViewTodaysRegistration.DataBind();
            #endregion GridTodaysRegistration

            listFilter = null;
            listFilter = BindName();
            if (!IsPostBack)
            {
                DropdownDoctors();
            }
         
        }
        #endregion GridBind

        #region BindDropdownDoc
        /// <summary>
        /// Bind Dropdown For Selecting Doctor While Booking
        /// </summary>
        public void DropdownDoctors()
        {
            DataSet ds = tok.DropBindDoctorsName();
            ddlDoctorName.SelectedIndex = -1;
            ddlDoctorName.DataSource = ds.Tables[0];
            ddlDoctorName.DataValueField = "DoctorID";
            ddlDoctorName.DataTextField = "Name";
            ddlDoctorName.DataBind();
        }
        #endregion BindDropdownDOc

        #region BindDataAutocomplete
        /// <summary>
        /// Binding Data From DataBase For the Search Field
        /// </summary>
        /// <returns></returns>
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
       
        #region clearfield
        /// <summary>
        /// Clear The Fields For New Registration
        /// </summary>
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

        #region convertImage
        /// <summary>
        /// Conveting Image into Bytes Array
        /// </summary>
        /// <param name="fuImage"></param>
        /// <returns></returns>
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

        #region Events

        #region GridDelete
        /// <summary>
        /// Delete Patient Details and File If Exist Token and more Cant delete Msg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnDelete_Command(object sender, CommandEventArgs e)
        {
            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
            lblFileCount.Text = string.Empty;
            lblTokencount.Text = string.Empty;
            divDisplayNumber.Style["display"] = "none";
            Guid PatientID = Guid.Parse(e.CommandArgument.ToString());
            PatientObj.PatientID = PatientID;
            PatientObj.DeleteFile();
            PatientObj.DeletePatientDetails();
            gridDataBind();
        }


        #endregion GridDelete

        #region EditPatients
        /// <summary>
        /// Fill the Patient Details For Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        {
            lblErrorCaption.Text = string.Empty;
            lblMsgges.Text = string.Empty;
            Errorbox.Style["display"] = "none";
            lblFileCount.Text = string.Empty;
            lblTokencount.Text = string.Empty;
            divDisplayNumber.Style["display"] = "none";
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
            else
            {
                rdoMale.Checked = false;
                rdoFemale.Checked = false;
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

        #region ClearScreen
        /// <summary>
        /// New Button click for New screen where field get cleared
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        #region BookingToken
        /// <summary>
        /// Button Event For Token Registration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btntokenbooking_Click(object sender, EventArgs e)
        {
            //btnnew.Visible = true;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tok.DoctorID = ddlDoctorName.SelectedValue;
            if (HiddenField1.Value == "")
            {
                tok.PatientID = HdnFirstInsertID.Value;
            }
            else
            {
                tok.PatientID = HiddenField1.Value;
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

        #region MainButton
        /// <summary>
        /// Save and Update Checking The Hiddenfield Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            try
            {

                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
                string msg = string.Empty;
                var page = HttpContext.Current.CurrentHandler as Page;
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
                    else
                    {
                        PatientObj.Gender = "";
                    }
                }
                PatientObj.MaritalStatus = ddlMarital.SelectedItem.ToString();
                PatientObj.Occupation = (txtOccupation.Value != "") ? txtOccupation.Value.ToString() : null;
                PatientObj.CreatedBy = UA.userName;
                PatientObj.UpdatedBy = UA.userName;
                if (PatientObj.Name != null&PatientObj.DOB!=null)
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
                        HdnFirstInsertID.Value = PatientObj.PatientID.ToString();
                        PatientObj.AddPatientDetails();
                        PatientObj.AddFile();
                        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + g.ToString();
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
                        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + HiddenField1.Value.ToString();
                        if(ProfilePic.Src=="")
                        {
                            ProfilePic.Src = "../images/UploadPic1.png";
                        }
                    }
                }
                else
                {
                    msg = "Please fill out the mandatory fields And Try Again";
                    eObj.InsertionNotSuccessMessage(page, msg);
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
        /// <summary>
        /// Search Button click which check the String from txtsearch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                lblErrorCaption.Text = string.Empty;
                lblMsgges.Text = string.Empty;
                Errorbox.Style["display"] = "none";
                lblFileCount.Text = string.Empty;
                lblTokencount.Text = string.Empty;
                divDisplayNumber.Style["display"] = "none";
                DataRow dr = null;
                string path = Server.MapPath("~/Content/ProfilePics/").ToString();
                string Name = Request.Form["txtSearch"];
                if (Name !=string.Empty)
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
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FUNNNN", "Alert.render('Invalid Suggesion');", true);

                }
            }
            catch
            {
                Response.Redirect("../Registration/Patients.aspx");
                
            }
        }
        #endregion SearchButtonClick

        #region Paging
        /// <summary>
        /// Paging for Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView1.UseAccessibleHeader = false;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        #endregion Paging

       
        #endregion Events

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