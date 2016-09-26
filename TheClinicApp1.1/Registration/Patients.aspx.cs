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
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
using Messages = TheClinicApp1._1.UIClasses.Messages;
#endregion Included Namespaces


namespace TheClinicApp1._1.Registration
{
    public partial class Patients : System.Web.UI.Page
    {
        #region GlobalVariables

        private static int PageSize = 8;
        UIClasses.Const Const = new UIClasses.Const();    
        ClinicDAL.UserAuthendication UA;
        Patient PatientObj = new Patient();
        Appointments AppointObj =new Appointments();

        TokensBooking tok = new TokensBooking();
        ErrorHandling eObj = new ErrorHandling();
        public string listFilter = null;

        #endregion GlobalVariables

        #region Methods
       
        #region All patient View Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterAllPatients(string searchTerm, int pageIndex)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Patient PatientObj = new Patient();
            PatientObj.ClinicID = UA.ClinicID;
            var xml = PatientObj.ViewAndFilterAllPatients(searchTerm, pageIndex, PageSize);
            return xml;
        }

        #region Bind Dummy Row

        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add("Edit");
            dummy.Columns.Add(" ");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("Address");
            dummy.Columns.Add("Phone");
            dummy.Columns.Add("PatientID"); 
            dummy.Rows.Add();

            GridView1.DataSource = dummy;
            GridView1.DataBind();
        }

        #endregion All patient View Search Paging

        #region BindDropdownDoc
        /// <summary>
        /// Bind Dropdown For Selecting Doctor While Booking
        /// </summary>
        public void DropdownDoctors()
        {
            DataSet ds = tok.DropBindDoctorsName();
           // ddlDoctorName.SelectedIndex = -1;
            ddlDoctorName.DataSource = ds.Tables[0];
            ddlDoctorName.DataValueField = "DoctorID";
            ddlDoctorName.DataTextField = "Name";
            ddlDoctorName.DataBind();

            ddlDoctorName.Items.Insert(0, "--Select--");
        }
        #endregion BindDropdownDOc

        #region BindDataAutocomplete
        /// <summary>
        /// Binding Data From DataBase For the Search Field provided for Patient Search
        /// </summary>
        /// <returns></returns>
        private string BindName()
        {
            DataTable dt = PatientObj.GetSearchBoxData(); //Function call to get  Search BoxData
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

        #region Today's patient View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterTodayPatients(string searchTerm, int pageIndex)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Patient PatientObj = new Patient();
            PatientObj.ClinicID = UA.ClinicID;

            var xml = PatientObj.ViewAndFilterTodayPatients(searchTerm, pageIndex, PageSize);

            return xml;
        }

        #region Bind Today Registration Dummy Row

        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindTodayregistrationDummyRow()
        {
            DataTable dummy = new DataTable();

            //dummy.Columns.Add("Edit");
            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("Address");
            dummy.Columns.Add("Phone");
            dummy.Columns.Add("PatientID");
            dummy.Rows.Add();

            dtgViewTodaysRegistration.DataSource = dummy;
            dtgViewTodaysRegistration.DataBind();
        }

        #endregion Bind Today Registration Dummy Row

        #endregion  Today's patient View Search Paging

        #region Today's patientAppointment view Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterTodayPatientAppointments(string searchTerm, int pageIndex)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Appointments AppointmentObj = new Appointments();
            AppointmentObj.ClinicID = UA.ClinicID.ToString();
           
            var xml = AppointmentObj.ViewAndFilterTodayAppointments(searchTerm, pageIndex, PageSize);
            return xml;
        }

        #region Bind Today Appointment Dummy Row

        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindTodayAppointmentDummyRow()
        {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("Name");
            dummy.Columns.Add("Location");
            dummy.Columns.Add("Mobile");
            dummy.Columns.Add("AllottingTime");
            dummy.Columns.Add("AppointmentID");
            dummy.Columns.Add("PatientID");
            dummy.Columns.Add("AppointmentStatus");
            dummy.Columns.Add("IsRegistered");
            dummy.Rows.Add();
            dtgTodaysAppointment.DataSource = dummy;
            dtgTodaysAppointment.DataBind();
        }

        #endregion Bind Today Appointment Dummy Row
        #endregion Today's patientAppointment view Search Paging

        #region Delete Patient

        [System.Web.Services.WebMethod]
        public static string DeletePatient(Patient PatientObj)
        {
            string result = string.Empty;
            if (!(PatientObj.CheckPatientTokenExist(PatientObj.PatientID)))
            {
                result = PatientObj.DeletePatientByPatientID();
            }

            string jsonResult = null;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            jsonResult = jsSerializer.Serialize(PatientObj);

            return jsonResult; //Converting to Json
        }

        #endregion Delete Patient

   

        #endregion Get Patient Details

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
            Errorbox.Attributes.Add("display", "none");
            HiddenField1.Value = string.Empty;
        }
        #endregion ClearScreen

        #region Get Patient Details

        [System.Web.Services.WebMethod]
        public static string BindPatientDetails(Patient PatientObj)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            PatientObj.ClinicID = UA.ClinicID;
            PatientObj.GetSearchWithName(PatientObj.Name);
           
            string jsonResult = null;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            jsonResult = jsSerializer.Serialize(PatientObj);

            return jsonResult; //Converting to Json
        }
        #endregion Get Patient Details

        #region Bind Patient Details On Edit Click

        [System.Web.Services.WebMethod]
        public static string BindPatientDetailsOnEditClick(Patient PatientObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            PatientObj.ClinicID = UA.ClinicID;
            //DataSet dsPatient =
            PatientObj.GetPatientDetailsByID();


            string jsonResult = null;
           // DataSet ds = null;
            //ds = dsPatient;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
           // List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            //Dictionary<string, object> childRow;
            //if (ds.Tables[0].Rows.Count > 0)
           // {
                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                   // childRow = new Dictionary<string, object>();
                  //  foreach (DataColumn col in ds.Tables[0].Columns)
                 //   {
                  //      childRow.Add(col.ColumnName, row[col]);
                 //   }
                //    parentRow.Add(childRow);
               // }
           // }
            jsonResult = jsSerializer.Serialize(PatientObj);

            return jsonResult; //Converting to Json
        }
        #endregion Bind Patient Details On Edit Click

        #endregion Methods

        #region Events

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tok.ClinicID = UA.ClinicID.ToString();
            PatientObj.ClinicID = Guid.Parse(UA.ClinicID.ToString());

            BindDummyRow();
            BindTodayregistrationDummyRow();
            BindTodayAppointmentDummyRow();
            //gridDataBind();
            listFilter = null;
            listFilter = BindName();

            if (!IsPostBack)
            {
                //*Bind Patients in to the dropdown for token registration
                DropdownDoctors();
            }
        }
        #endregion PageLoad

        #region BookingToken
        /// <summary>
        /// Button Event For Token Registration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btntokenbooking_Click(object sender, EventArgs e)
        {
            if (ddlDoctorName.SelectedValue != "--Select--")
            {
            //btnnew.Visible = true;
            
            string patid="";
            if (HiddenField1.Value == "")
            {
               patid = HdnFirstInsertID.Value;
            }
            else
            {
               patid= HiddenField1.Value;
            }
            int tokenNo = NewToken(ddlDoctorName.SelectedValue, patid);
            lblTokencount.Text = ":" + tokenNo.ToString();
            //lblToken.Visible = true;
            divDisplayNumber.Visible = true;
            //gridDataBind();
                 }
        }
        #endregion BookingToken

        #region NewToken
        public int NewToken(string doctorid,string patientid)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tok.PatientID = patientid;
            tok.DoctorID = doctorid;
            tok.ClinicID = UA.ClinicID.ToString();
            tok.CreateDate = DateTime.Now;
            tok.DateTime = DateTime.Now;
            tok.CreatedBy = UA.userName;
            return tok.InsertToken();
        }
        #endregion NewToken

        #region MainButton
        /// <summary>
        /// Save and Update by Checking The Hiddenfield1 Value
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
                int parsedValue;
                if (int.TryParse(txtAge.Value, out parsedValue))
                {
                    if (parsedValue >= 0)
                    {
                        int age = Convert.ToInt32(parsedValue);
                        int year = _date.Year;
                        int DOB = year - age;
                        PatientObj.DOB = DateTime.Parse(DOB + "-01-01");
                    }
                }

                string clinID = UA.ClinicID.ToString();
                PatientObj.Name = (txtName.Value != "") ? txtName.Value.ToString() : null;
                PatientObj.Address = (txtAddress.Value != "") ? txtAddress.Value.ToString() : null;
                PatientObj.Phone = (txtMobile.Value != "") ? txtMobile.Value.ToString() : null;
                PatientObj.Email = (txtEmail.Value != "") ? txtEmail.Value.ToString() : null;
                PatientObj.AppointmentID = (hdfAppointmentID.Value != "") ? hdfAppointmentID.Value.ToString() : null;

                AppointObj.AppointmentID = PatientObj.AppointmentID;
               
             
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
                if ((PatientObj.Name != null) && (parsedValue != 0))
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
                            Hdnimagetype.Value = PatientObj.ImageType;
                        }

                        Guid g = Guid.NewGuid();
                        PatientObj.PatientID = g;
                        HdnFirstInsertID.Value = PatientObj.PatientID.ToString();

                        //*Insert New Patient and Case File 

                        if(PatientObj.AddPatientDetails()==1)
                        {
                         if (AppointObj.AppointmentID!=null)
                         {
                             AppointObj.ClinicID = PatientObj.ClinicID.ToString();
                             AppointObj.AppointmentStatus = 1;//patientpresent status
                             AppointObj.UpdatedBy = UA.userName;
                             AppointObj.PatientAppointmentStatusUpdate();//change status of appointment
                             AppointObj.PatientAppointmentNumberAllotByAppointmentID();//appointment no
                         }
                        

                         PatientObj.AddFile();

                         
                        }

                      
                        if (FileUpload1.HasFile)
                        {
                            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + g.ToString();
                        }
                        else
                        {
                            ProfilePic.Src = "../images/UploadPic1.png";
                        }
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
                            Hdnimagetype.Value = PatientObj.ImageType;
                            //*Upadate Patient Picture
                            PatientObj.UpdatePatientPicture();

                        }
                        PatientObj.PatientID = Guid.Parse(HiddenField1.Value);
                        //*update Patient Details
                        PatientObj.UpdatePatientDetails();
                        if (Hdnimagetype.Value != "")
                        {
                            ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + HiddenField1.Value.ToString();
                        }
                        else
                        {
                            ProfilePic.Src = "../images/UploadPic1.png";
                        }
                    }
                }
                else if ((PatientObj.Name == null))
                {
                    msg = Messages.Mandatory;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }
                else
                {
                    msg = Messages.AgeIssue;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }
                //gridDataBind();
                lblFileCount.Text = PatientObj.FileNumber;
                if((HiddenField1.Value == "")&&(hdfAppointmentID.Value == ""))
                {
                    HiddenField1.Value = PatientObj.PatientID.ToString();
                    if ((PatientObj.Name != null) && (parsedValue != 0))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "openModal();", true);
                    }
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
               
                string path = Server.MapPath("~/Content/ProfilePics/").ToString();
                string Name = Request.Form["txtSearch"];
                if (Name !=string.Empty)
                {
                    PatientObj.GetSearchWithName(Name);            
                    DateTime date = DateTime.Now;
                    int year = date.Year;
                    Guid PatientID = PatientObj.PatientID;
                    txtName.Value = PatientObj.Name;
                    string Gender = PatientObj.Gender;
                    if (Gender.Trim() == "Male")
                    {

                        rdoMale.Checked = true;
                    }
                    else if (Gender.Trim() == "Female")
                    {
                        rdoFemale.Checked = true;
                    }
                    else
                    {
                        rdoMale.Checked = false;
                        rdoFemale.Checked = false;
                    }
                    DateTime DT = PatientObj.DOB;
                    int Age = year - DT.Year;
                    txtAge.Value = Age.ToString();
                    txtAddress.Value = PatientObj.Address;
                    txtMobile.Value = PatientObj.Phone;
                    txtEmail.Value = PatientObj.Email;
                    string Status = PatientObj.MaritalStatus;
                    ddlMarital.SelectedValue = Status;
                    txtOccupation.Value = PatientObj.Occupation;
                    string imagetype = PatientObj.ImageType;
                    if(imagetype.Trim()!=string.Empty)
                    {
                        Hdnimagetype.Value = imagetype.Trim();
                        ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
                    }
                    else
                    {
                        ProfilePic.Src = "../images/UploadPic1.png";
                    }
                    HiddenField1.Value = PatientID.ToString();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FUNNNN", "Alert.render('Invalid Suggesion');", true);

                }
               // gridDataBind();
            }
            catch
            {
                Response.Redirect("../Registration/Patients.aspx");
                
            }
        }
        #endregion SearchButtonClick
        
        #region Logout
        //*Event For SideBar Logout Button
        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Session.Clear();
            Response.Redirect("../Default.aspx");
        }

        //*Event For Logout Button placed on the right top of the page
        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Session.Clear();
            Response.Redirect("../Default.aspx");
        }
        
        #endregion Logout

        #endregion Events

        //--NOTE: Below events and functions are not using now

        #region Paging
        /// <summary>
        /// Paging for Grid
        /// Setting header for Paging 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            GridView1.UseAccessibleHeader = false;
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }


        protected void dtgViewTodaysRegistration_PreRender(object sender, EventArgs e)
        {
            dtgViewTodaysRegistration.UseAccessibleHeader = false;
            if (dtgViewTodaysRegistration.Rows.Count > 0)
            {
                dtgViewTodaysRegistration.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        #endregion Paging

        #region FillPatientsDetails In to Registration Sheet
        /// <summary>
        /// Fill the Patient Details For Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        {
            try
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
                if (Patient[9] != string.Empty)
                {
                    Hdnimagetype.Value = Patient[9].Trim();
                    ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
                }
                else
                {
                    ProfilePic.Src = "../images/UploadPic1.png";
                }
                ProfilePic.Visible = true;
                //btnnew.Visible = true;
                HiddenField1.Value = PatientID.ToString();
                gridDataBind();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }


        }
        #endregion FillPatientsDetails In to Registration Sheet

        #region GridDelete
        /// <summary>
        /// Delete Patient Details and File If Exist Token and more Cant delete Msg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                lblErrorCaption.Text = string.Empty;
                lblMsgges.Text = string.Empty;
                Errorbox.Style["display"] = "none";
                lblFileCount.Text = string.Empty;
                lblTokencount.Text = string.Empty;
                divDisplayNumber.Style["display"] = "none";
                Guid PatientID = Guid.Parse(e.CommandArgument.ToString());
                PatientObj.PatientID = PatientID;
                if (!(PatientObj.CheckPatientTokenExist(PatientID)))
                {
                    PatientObj.DeletePatientDetails();
                }
                else
                {
                    string msg = string.Empty;
                    var page = HttpContext.Current.CurrentHandler as Page;
                    msg = Messages.TokenExist;
                    eObj.DeletionNotSuccessMessage(page, msg);
                }

                gridDataBind();
            }
            catch
            {
                Response.Redirect("../Default.aspx");
            }

        }


        #endregion GridDelete

        #region Grid Click
        protected void btngrid_Click(object sender, EventArgs e)
        {
            gridDataBind();
        }
        #endregion  Grid Click

        #region GridBind
        public void gridDataBind()
        {

            #region GridAllRegistration
            //*Grid Bind Showing All Registered Patients In the Modal POPUP

            GridView1.DataSource = PatientObj.GetAllRegistration();
            GridView1.DataBind();
            GridView1.EmptyDataText = "No Records Found";
            lblRegCount.Text = GridView1.Rows.Count.ToString();
            if (Convert.ToInt32(lblRegCount.Text) > 99)
                lblRegCount.Text = "99+";
            #endregion GridAllRegistration

            #region GridTodaysRegistration
            //*Grid Bind Showing Todays Registered Patients In the Modal POPUP

            dtgViewTodaysRegistration.EmptyDataText = "....Till Now No Registration....";
            dtgViewTodaysRegistration.DataSource = PatientObj.GetDateRegistration();

            dtgViewTodaysRegistration.DataBind();

            lblTodayRegCount.Text = dtgViewTodaysRegistration.Rows.Count.ToString();
            #endregion GridTodaysRegistration

            #region GridTodaysAppointments
            //dtgTodaysAppointment.EmptyDataText = "....No Appointments yet....";
            //Appointments appointObj = new Appointments();
            //dtgTodaysAppointment.DataSource = appointObj.GetAllTodaysPatientAppointments();
            //dtgTodaysAppointment.DataBind();
            //lblTodayRegCount.Text = dtgTodaysAppointment.Rows.Count.ToString();
            #endregion GridTodaysAppointments

        }
        #endregion GridBind

        #region Deletion
        [WebMethod]
        public static string DeletePatientByID(string PatientID)
        {
            string result = string.Empty;
            bool DoctorDeleted = false;

            if (PatientID != string.Empty)
            {
                Patient PatientObj = new Patient();
                PatientObj.PatientID = Guid.Parse(PatientID);
                if (!(PatientObj.CheckPatientTokenExist(Guid.Parse(PatientID))))
                {
                    result = PatientObj.DeletePatientByPatientID();

                    if (result != string.Empty)
                    {
                        DoctorDeleted = true;
                    }

                }
            }
            return result;
        }

        #endregion Deletion
    }

       
       
    }
