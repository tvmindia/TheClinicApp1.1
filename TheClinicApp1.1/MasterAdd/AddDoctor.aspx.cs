
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespcaes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using TheClinicApp1._1.ClinicDAL;
using System.Web.Services;
using Messages = TheClinicApp1._1.UIClasses.Messages;
using System.Web.Script.Serialization;
using System.Threading;

#endregion Included Namespcaes

namespace TheClinicApp1._1.MasterAdd
{
    public partial class AddDoctor : System.Web.UI.Page
    {
        #region Global Variables

        private static int PageSize = 8;
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        User userObj = new User();
        Master mstrObj = new Master();
        ErrorHandling eObj = new ErrorHandling();
        ClinicDAL.CryptographyFunctions CryptObj = new CryptographyFunctions();
        ClinicDAL.RoleAssign roleObj = new RoleAssign();
        MailSending mailObj = new MailSending();

        #endregion Global Variables

        #region Methods

        #region Doctor View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterDoctor(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrObj = new Master();
            mstrObj.ClinicID = UA.ClinicID;
            var xml = mstrObj.ViewAndFilterDoctors(searchTerm, pageIndex, PageSize);
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

            //dummy.Columns.Add("Edit");
            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("Phone");
            dummy.Columns.Add("Email");
            dummy.Columns.Add("DoctorID");
            dummy.Columns.Add("UserID");
            dummy.Rows.Add();
           
            dtgDoctors.DataSource = dummy;
            dtgDoctors.DataBind();
        }

        #endregion Bind Dummy Row

        #endregion Doctor View Search Paging

        #region Delete Doctor By ID

        [WebMethod]
        public static bool DeleteDoctorByID(string DoctorID,string UserID)
        {
            Master mstrObj = new Master();
            RoleAssign roleObj = new RoleAssign();
            User userObj = new User();

            int result = 0;
            bool DoctorDeleted = false;

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorID = Guid.Parse(DoctorID);

            bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

            if (IDUsedOrNot == false)
            {
                //Get roleID of doctor

                mstrObj.ClinicID = UA.ClinicID;
                roleObj.RoleID = Guid.Parse(mstrObj.GetRoleIDOfDoctor());

                //delete assigned role

                roleObj.UserID = Guid.Parse(UserID);
                result=   roleObj.DeleteAssignedRoleByUserIDForWM();

                if (result == 1)
                {
                    //delete doctor
                    mstrObj.DoctorID = Guid.Parse(DoctorID);
                    result=mstrObj.DeleteDoctorByIDForWM();


                    if (result == 1)
                    {
                        //delete user

                    userObj.UserID = Guid.Parse(UserID);
                    userObj.DeleteUserByUserIDForWM();

                    DoctorDeleted = true;

                    }


                }

                
            }


            return DoctorDeleted;

        }

        #endregion Delete Doctor By ID

        #region Bind Doctor Details On Edit Click
        /// <summary>
        /// To get specific order details by orderid for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindDoctorDetailsOnEditClick(Master mstrObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            mstrObj.ClinicID = UA.ClinicID;
            DataSet dtDoctor = mstrObj.GetDoctorDetailsByIDForWM();

            string jsonResult = null;
            DataSet ds = null;
            ds = dtDoctor;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            jsonResult = jsSerializer.Serialize(parentRow);

            return jsonResult; //Converting to Json
        }
        #endregion Bind Doctor Details On Edit Click

        //---------- *  USER *------------//

        #region User

        #region Set Default Password

        public string SetDefaultPassword(string DoctorName) //PATTERN : if dr name has more than 3 characters , first 3 characters of name + 123 , otherwise(name less than 3 character , name + 123)
        {
            string Password = string.Empty;
            
            if (DoctorName != string.Empty)
            {
                if (DoctorName.Length >= 3)
                {
                    DoctorName = DoctorName.Substring(0, 3);

                }

                Password = DoctorName + "123";
               
            }


            return Password;
            
        }

        #endregion Set Default Password

        #region Add User To User Table
        public int AddUserToUserTable()
        {
            int rslt = 0;

            userObj.firstName = txtName.Value.TrimStart();
            userObj.loginName = txtLoginName.Value.TrimStart();
            userObj.lastName = string.Empty;
            userObj.isActive = true;
            userObj.ClinicID = UA.ClinicID;
            userObj.createdBy = UA.userName;
            userObj.updatedBy = UA.userName;

            userObj.Email = txtEmail.Value;
            userObj.PhoneNo = txtPhoneNumber.Value;

            //-------------send this Password and Login Name to Email ID---------------//
            string password = SetDefaultPassword(txtName.Value.TrimStart());
            
            userObj.passWord = CryptObj.Encrypt(password); ;

            if (hdnUserID.Value == string.Empty)
            {
                //INSERT user

                 rslt =   userObj.AddUser();
                hdnUserID.Value = userObj.UserID.ToString();
            }
            else
            {
                //UPDATE user
                userObj.UserID = Guid.Parse(hdnUserID.Value);
                rslt=  userObj.UpdateuserByUserID();
            }


            if (rslt == 1)
            {
                HttpContext ctx = HttpContext.Current;
                new Thread(delegate()
                {
                    HttpContext.Current = ctx;

                    mailObj.MailSubject = "Login Password";
                    mailObj.msg = password;
                    mailObj.Email = txtEmail.Value;
                    mailObj.FormatAndSendEmail(txtName.Value, UA.Clinic, txtLoginName.Value, password);

                }).Start();
            }
            return rslt;
        }

        #endregion Add User To User Table

        #region Delete User By UserID

        public void DeleteUserByUserID(Guid UserID)
        {
            userObj.UserID = UserID;
            userObj.DeleteUserByUserID();
        }

        #endregion Delete User By UserID

        #endregion User

        //---------- *  DOCTOR *------------//

        #region Doctor

        #region Validate Doctor Name

        [WebMethod]
        public static bool ValidateDoctorName(string DoctorName)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrObj = new Master();

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = DoctorName;

            if (mstrObj.CheckDoctorNameDuplication())
            {
                return true;
            }
            return false;
        }

        #endregion Validate Doctor Name

        #region Add Doctor

        public int AddDoctorToDoctorTable()
        {
            int rslt = 0;

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = txtName.Value.TrimStart();
            mstrObj.DoctorEmail = txtEmail.Value;
            mstrObj.DoctorPhone = txtPhoneNumber.Value;

            if ( hdnDrID.Value == string.Empty)
            {
                //INSERT Doctor
                 
                mstrObj.createdBy = UA.userName;

                if (hdnUserID.Value != string.Empty)
                {
                    mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
                }

                rslt= mstrObj.InsertDoctors();
                hdnDrID.Value = mstrObj.DoctorID.ToString();

            }

            else
            {
                //UPDATE Doctor
                mstrObj.DoctorID = Guid.Parse(hdnDrID.Value);

                mstrObj.updatedBy = UA.userName;
                rslt=  mstrObj.UpdateDoctors();
                //}

            }

            return rslt;
        }

        #endregion  Add Doctor

        #endregion Doctror

        //---------- *  USER IN ROLES    *--------------//

        #region UserInRoles

        #region GetRoleIDOFDoctor

        /// <summary>
        /// To get the roleID of doctor  : If the user is doctor , user has to assign the doctor role
        /// </summary>
        /// <returns></returns>
        public string GetRoleIDOFDoctor()
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];


            string DoctorRoleID = string.Empty;

            userObj.ClinicID = UA.ClinicID;

            mstrObj.ClinicID = UA.ClinicID;
            DoctorRoleID = mstrObj.GetRoleIDOfDoctor();

            return DoctorRoleID;
        }

        #endregion GetRoleIDOFDoctor

        #region Assign Role

        /// <summary>
        /// Assigns doctor role for the user
        /// </summary>
        public void AddUserRole()
        {
           
            roleObj.ClinicID = UA.ClinicID;

            string roleid = GetRoleIDOFDoctor();

            roleObj.RoleID = Guid.Parse(roleid);
            roleObj.CreatedBy = UA.userName;
            roleObj.UserID = Guid.Parse(hdnUserID.Value);

            roleObj.ClinicID = UA.ClinicID;

            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            DataRow[] DoctorRoleAssigned = dtAssignedRoles.Select("RoleID = '" + roleid + "'"); //CHecking whether user has already doctor role, if not , assigns doctor role for the user

            if (DoctorRoleAssigned.Length == 0)
            {
                roleObj.AssignRole();
            }

        }

        #endregion Assign Role

        #region Delete Assigned role By UserID

        public int DeleteAssignedRoleByUserID(Guid UserID)
        {
            int rslt = 0;
            roleObj.UserID = UserID;
            rslt =  roleObj.DeleteAssignedRoleByUserID();
            return rslt;
        }

        #endregion Delete Assigned role By UserID

        #endregion UserInRoles

        //-------------------------Webmethods------------------------//
        #region Webmethods
       
        #region ValidateLoginName
        [WebMethod]
        ///Checking login name duplication
        public static bool ValidateLoginName(string LogName)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Master mstrObj = new Master();


            string loginName = LogName;

            ClinicDAL.User userObj = new ClinicDAL.User();
            //userObj.ClinicID = UA.ClinicID;

            if (userObj.ValidateUsername(loginName))
            {
                return true;
            }
            return false;

        }

        #endregion ValidateLoginName

        #region ValidateEmailID
        [WebMethod]
        ///Checking login name duplication
        public static bool ValidateEmailID(string Email)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            User usrObj = new User();

            usrObj.Email = Email;

            usrObj.ClinicID = UA.ClinicID;

            if (usrObj.ValidateEmailID())
            {
                return true;
            }
            return false;

        }

        #endregion ValidateEmailID

        #endregion Webmethods

        #endregion Methods

        //-------------------------Events------------------------//

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            mstrObj.ClinicID = UA.ClinicID;
            hdnLoginedUserID.Value = UA.UserID.ToString();

            BindDummyRow();
            //if (!IsPostBack)
            //{
            //    BindGridview();
            //}
        }

        #endregion  Page Load

        #region Save Button Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int rslt = 0;
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;

            if (txtName.Value.TrimStart() != string.Empty || txtEmail.Value.TrimStart() != string.Empty || txtPhoneNumber.Value.TrimStart() != string.Empty)
            {
             rslt =   AddUserToUserTable();

             if (rslt == 1)
             {
                 rslt=  AddDoctorToDoctorTable();

                 if (rslt == 1)
                 {
                     AddUserRole();  
                 }
               
             }
                
            }

            else
            {
                //msg = "Please fill out all the fields";
                msg = Messages.MandatoryFields;
                eObj.InsertionNotSuccessMessage(page, msg);
               
            }

        }

        #endregion Save Button Click

        #region Delete Image Buttn Click

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            int rslt = 0;
            Errorbox.Attributes.Add("style", "display:none");

            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


             ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            //Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());

            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[0].ToString());
            Guid UserID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[1].ToString());

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorID = DoctorID;

            bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

            //bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();

            if (IDUsedOrNot)
            {
                //msg = "Already used . Can't be deleted";
                msg = Messages.AlreadyUsedForDeletion;
                eObj.DeletionNotSuccessMessage(page, msg); 
            }

         else
            {
                roleObj.RoleID = Guid.Parse(GetRoleIDOFDoctor());
                rslt =DeleteAssignedRoleByUserID(UserID);

                if (rslt == 1)
                {
                mstrObj.DoctorID = DoctorID;
                rslt =  mstrObj.DeleteDoctorByID();

                if (rslt == 1)
                {
                    DeleteUserByUserID(UserID); 
                }

                }
         }
            BindGridview();

        }

        #endregion Delete Image Buttn Click

        #region Update Image Button Click
        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            //Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());



            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[0].ToString());
            Guid UserID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[1].ToString());

            hdnUserID.Value = UserID.ToString();

            mstrObj.DoctorID = DoctorID;
            hdnDrID.Value = DoctorID.ToString();

            mstrObj.ClinicID = UA.ClinicID;
    DataTable dtDrByID =  mstrObj.GetDoctorDetailsByID();

    if (dtDrByID.Rows.Count > 0)
    {
        txtName.Value = dtDrByID.Rows[0]["Name"].ToString();
        txtPhoneNumber.Value = dtDrByID.Rows[0]["Phone"].ToString();
        txtEmail.Value = dtDrByID.Rows[0]["Email"].ToString();
        txtLoginName.Value = dtDrByID.Rows[0]["LoginName"].ToString();
        txtLoginName.Attributes.Add("readonly", "readonly");
    }

        }

        #endregion Update Image Button Click

        #region Logout Click

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        { string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        }

        #endregion Logout Click

        #endregion Events


        //--NOTE: Below events and functions are not using now

        #region Bind Gridview

        public void BindGridview()
        {

            DataTable dtDoctors = mstrObj.ViewDoctors();

            if (dtDoctors != null)
            {
                dtgDoctors.DataSource = dtDoctors;
                dtgDoctors.DataBind();

                lblCaseCount.Text = dtgDoctors.Rows.Count.ToString();
            }

        }

        #endregion Bind Gridview

        #region Paging
        protected void dtgDoctors_PreRender(object sender, EventArgs e)
        {
            dtgDoctors.UseAccessibleHeader = false;

            if (dtgDoctors.Rows.Count > 0)
            {
                dtgDoctors.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }

        #endregion Paging

    }
}