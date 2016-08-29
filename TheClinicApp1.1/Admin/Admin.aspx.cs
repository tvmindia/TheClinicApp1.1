
#region CopyRight

//Author      : SHAMILA T P
//Created Date: April-29-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data.SqlClient;

using TheClinicApp1._1.ClinicDAL;
using System.Web.Services;
using System.Configuration;
using System.Reflection;
using Messages = TheClinicApp1._1.UIClasses.Messages;
using System.Web.Script.Serialization;


#endregion Included Namespaces

namespace TheClinicApp1._1.Admin
{
    public partial class Admin : System.Web.UI.Page
    {

        #region Global Variables

        public string RoleName = null;
        private static int PageSize = 8;
        ClinicDAL.CryptographyFunctions CryptObj = new CryptographyFunctions();
        ClinicDAL.User userObj = new ClinicDAL.User();
        ClinicDAL.Master mstrObj = new ClinicDAL.Master();
        ClinicDAL.RoleAssign roleObj = new RoleAssign();
        ClinicDAL.Clinic ClinicObj = new Clinic();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ClinicDAL.Master MasterObj = new ClinicDAL.Master();
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        #region User View Search Paging
      
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterUser(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                User usrObj = new User();
                usrObj.ClinicID = UA.ClinicID;
                var xml = usrObj.ViewAndFilterUsers(searchTerm, pageIndex, PageSize);
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
            dummy.Columns.Add("LoginName");
            dummy.Columns.Add("FirstName");
            dummy.Columns.Add("LastName");
            dummy.Columns.Add("Active");
            dummy.Columns.Add("UserID");

            dummy.Rows.Add();
            dtgViewAllUsers.DataSource = dummy;
            dtgViewAllUsers.DataBind();
        }

        #endregion Bind Dummy Row



        #endregion User View Search Paging

        #region Set User Count

        public void SetUserCount()
        {
            userObj.ClinicID = UA.ClinicID;
            DataTable dtUsers = userObj.GetDetailsOfAllUsers();
            lblCaseCount.Text = dtUsers.Rows.Count.ToString();
        }

        #endregion Set User Count

//-------------------------------------- * Save *--------------------------------------//
        //---* To USER *--//

        #region User

        #region Add User To User Table
        public void AddUserToUserTable()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.firstName = txtFirstName.Value.TrimStart();
            userObj.loginName = txtLoginName.Value.TrimStart();
            userObj.lastName = txtLastName.Value.TrimStart();

            if (rdoActiveYes.Checked == true)
            {
                userObj.isActive = true;
            }
            else
            {
                if (rdoActiveNo.Checked == true)
                {
                    userObj.isActive = false;
                }
            }
            if(dropdivclinic.Visible==true)
            {
                userObj.ClinicID = Guid.Parse(ddlGroup.SelectedValue);
            }
            else if(dropdivclinic.Visible==false)
            {
                userObj.ClinicID = UA.ClinicID;
            }
            userObj.createdBy = UA.userName;
            userObj.updatedBy = UA.userName;
            userObj.passWord = CryptObj.Encrypt(txtPassword.Value);
            userObj.Email = txtEmail.Value;
            userObj.PhoneNo = txtPhoneNumber.Value;

            if (hdnUserID.Value == string.Empty)
            {
                //INSERT

                userObj.AddUser();
                hdnUserID.Value = userObj.UserID.ToString();

                //if (rdoDoctor.Checked == true)
                //{
                //    hdnUserID.Value = userObj.UserID.ToString();
                //}
             

            }
            else
            {
                //UPDATE

                userObj.UserID = Guid.Parse(hdnUserID.Value);
                userObj.UpdateuserByUserID();
                
            }
             
        }

        #endregion Add User To User Table

        #region Delete User By UserID

        public void DeleteUserByUserID(Guid UserID)
        {
            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;

            roleObj.UserID = UserID;
            roleObj.ClinicID = UA.ClinicID;
            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            //RoleID

            foreach (DataRow dr in dtAssignedRoles.Rows)
            {
                roleObj.RoleID = Guid.Parse(dr["RoleID"].ToString());
                DeleteAssignedRoleByUserID(UserID);
            }



                    
            //if (dtAssignedRoles.Rows.Count == 0)
            //{
            userObj.UserID = UserID;
            userObj.DeleteUserByUserID();
            //}

            //else
            //{
            //    msg = Messages.AlreadyUsedForDeletion;
            //    eObj.DeletionNotSuccessMessage(page, msg);
            //}
        }

        #endregion Delete User By UserID

        #region Refill User Details 

        /// <summary>
        /// Controls will be refilled on edit click
        /// </summary>
        /// <param name="UserID"></param>

        public void RefillUserDetailsOnEditClick(Guid UserID)
        {

            txtLoginName.Attributes.Add("readonly", "readonly"); //LOGIN NAME READ ONLY in case of edit

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.UserID = UserID;
            userObj.ClinicID = UA.ClinicID;
            DataTable dtuser = userObj.GetUserDetailsByUserID();

            txtLoginName.Value = dtuser.Rows[0]["LoginName"].ToString();
            txtFirstName.Value = dtuser.Rows[0]["FirstName"].ToString();
            txtLastName.Value = dtuser.Rows[0]["LastName"].ToString();
           // txtPassword.Value = CryptObj.Decrypt(dtuser.Rows[0]["Password"].ToString());
           // txtConfirmPassword.Value = CryptObj.Decrypt(dtuser.Rows[0]["Password"].ToString());
            txtPassword.Value = string.Empty;

            txtPhoneNumber.Value = dtuser.Rows[0]["PhoneNo"].ToString();

            txtEmail.Value = dtuser.Rows[0]["Email"].ToString();
            bool isActive = Convert.ToBoolean(dtuser.Rows[0]["Active"].ToString());

            if (isActive)
            {
                rdoActiveYes.Checked = true;
                rdoActiveNo.Checked = false;
            }
            else
            {
                rdoActiveNo.Checked = true;
                rdoActiveYes.Checked = false;
            }

            userObj.ClinicID = UA.ClinicID;
            userObj.firstName = dtuser.Rows[0]["FirstName"].ToString();

                     mstrObj.UsrID = UserID;
                    mstrObj.ClinicID = UA.ClinicID;
                    DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

                    if (dtDoctor.Rows.Count > 0) //Checking whether user is doctor , then activate Isdoctor YES radio button
                    {
                        //-----User Is Doctor
                        rdoDoctor.Checked = true;
                        rdoNotDoctor.Checked = false;
                       
                    }

                    else
                    {

                        //--- User Is Not a doctor

                        rdoNotDoctor.Checked = true;
                        rdoDoctor.Checked = false;

                    }

        }

        #endregion Refill User Details

        #endregion User

        //---* To DOCTOR *--//

        #region DOCTOR

        #region GetRoleIDOFDoctor

        /// <summary>
        /// To get the roleID of doctor  : If the user is doctor , user has to assign the doctor role
        /// </summary>
        /// <returns></returns>
        public string GetRoleIDOFDoctor()
        {
            string DoctorRoleID = string.Empty;

              UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            userObj.ClinicID = UA.ClinicID;

            //DoctorRoleID = userObj.GetRoleIDOfDoctor();
            mstrObj.ClinicID = UA.ClinicID;
            DoctorRoleID = mstrObj.GetRoleIDOfDoctor();
            return DoctorRoleID;
        }

        #endregion GetRoleIDOFDoctor

        #region Add User To Doctor Table
        public void AddUserToDoctorTable()
        {
            bool IsDoctor = false;

        UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

        mstrObj.loginName = txtLoginName.Value.TrimStart();
        if (dropdivclinic.Visible == true)
        {
            mstrObj.ClinicID = Guid.Parse(ddlGroup.SelectedValue);
        }
        else if (dropdivclinic.Visible == false)
        {
            mstrObj.ClinicID = UA.ClinicID;
        }
            //mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = txtFirstName.Value.TrimStart();
            mstrObj.DoctorPhone = txtPhoneNumber.Value;
            mstrObj.DoctorEmail = txtEmail.Value;
            mstrObj.createdBy = UA.userName;
            mstrObj.updatedBy = UA.userName;

            if (hdnUserID.Value != string.Empty)
                {

                    mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
                    if (dropdivclinic.Visible == true)
                    {
                        mstrObj.ClinicID = Guid.Parse(ddlGroup.SelectedValue);
                    }
                    else if (dropdivclinic.Visible == false)
                    {
                        mstrObj.ClinicID = UA.ClinicID;
                    }
                    //mstrObj.ClinicID = UA.ClinicID;
                    DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

                    if (dtDoctor.Rows.Count > 0) //Checking whether user is already a doctor 
                    {
                        //--------Already a doctor , So UPDATE

                        IsDoctor = true;
                         mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                         mstrObj.UpdateDoctors();
                        
                    }

                   

                    if (IsDoctor == false)
                    {
                        //----User is not in doctor table , so INSERT

                        if (rdoDoctor.Checked == true)
                        {
                            mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
                        }

                      
                        mstrObj.InsertDoctors();
                    }


                }
            //else
            //{
            //    mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
            //    mstrObj.InsertDoctors();
            //}
          
            
           
        }

        #endregion Add User To Doctor Table

        #region Delete Doctor By UserID

        /// <summary>
        /// Doctor will be deleted from doctor and userinroles tables iff DoctorID is not not used yet , otherwise shows 'already used' message
        /// </summary>
        /// <param name="UserID"></param>
        public void DeleteDoctorByUserID(Guid UserID)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;


            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.UsrID = UserID;
            DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

            if (dtDoctor.Rows.Count > 0) //Checking whether user is doctor or not
            {
                //---user is DOCTOR

                mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());

                bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

                if (IDUsedOrNot) //checking whether doctorid is already used ,if not used doctor is get deleted
                {
                    //msg = "Already used . Can't be deleted";
                    msg = Messages.AlreadyUsedForDeletion;
                    eObj.DeletionNotSuccessMessage(page, msg);

                }

                else
                {
                    roleObj.RoleID = Guid.Parse(GetRoleIDOFDoctor());
                    DeleteAssignedRoleByUserID(UserID);

                    mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                    mstrObj.DeleteDoctorByID(true);

                    if ( hdnDeleteButtonClick.Value == "True")
                    {
                        DeleteUserByUserID(UserID);
                    }

               
                }

            }

            else
            {
                //DeleteAssignedRoleByUserID(UserID);

                if (hdnDeleteButtonClick.Value == "True")
                {
                    DeleteUserByUserID(UserID);
                }
            }
            
        }


        #endregion Delete Doctor By UserID

       
        #endregion DOCTOR

        //---* To USER-In-ROLES *--//

        #region USER-In-ROLES

        #region Assign Role

        /// <summary>
        /// Assigns doctor role for the user
        /// </summary>
        public void AddUserRole()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            if (dropdivclinic.Visible == true)
            {
                roleObj.ClinicID = Guid.Parse(ddlGroup.SelectedValue);
            }
            else if (dropdivclinic.Visible == false)
            {
                roleObj.ClinicID = UA.ClinicID;
            }
            //roleObj.ClinicID = UA.ClinicID;

            string roleid = GetRoleIDOFDoctor();

            roleObj.RoleID = Guid.Parse(roleid);
            roleObj.CreatedBy = UA.userName;
            roleObj.UserID = Guid.Parse(hdnUserID.Value);
            if (dropdivclinic.Visible == true)
            {
                roleObj.ClinicID = Guid.Parse(ddlGroup.SelectedValue);
            }
            else if (dropdivclinic.Visible == false)
            {
                roleObj.ClinicID = UA.ClinicID;
            }
            //roleObj.ClinicID = UA.ClinicID;

            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            DataRow[] DoctorRoleAssigned = dtAssignedRoles.Select("RoleID = '" + roleid + "'"); //CHecking whether user has already doctor role, if not , assigns doctor role for the user

            if (DoctorRoleAssigned.Length == 0)
            {
                roleObj.AssignRole();
            }

        }

        #endregion Assign Role

        #region Delete Assigned role By UserID

        public void DeleteAssignedRoleByUserID(Guid UserID)
        {
            roleObj.UserID = UserID;
            roleObj.DeleteAssignedRoleByUserID();
        }

        #endregion Delete Assigned role By UserID

        #endregion USER-In-ROLES

        //END----------------------//

        #region CheckUserIsDoctor

        [WebMethod]
        public static bool CheckUserIsDoctor(string UsrID)
        {
            bool IsDoctor = false;

            ClinicDAL.Master mstrObj = new ClinicDAL.Master();

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            mstrObj.UsrID = Guid.Parse(UsrID);
                    mstrObj.ClinicID = UA.ClinicID;
                    DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

                    if (dtDoctor.Rows.Count > 0) //Checking whether user is doctor , then activate Isdoctor YES radio button
                    {
                        IsDoctor = true;
                    }

                    return IsDoctor;
        }

        #endregion CheckUserIsDoctor

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


        //LOGIN NAME

        #region Delete User By UserID

         [WebMethod]
        public static bool DeleteUserByID(string UsrID)
        {
             string result= string.Empty;

            bool UserDeleted = false;

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Master mstrObj = new Master();
            ClinicDAL.RoleAssign roleObj = new RoleAssign();
            ClinicDAL.User userObj = new ClinicDAL.User();

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.UsrID = Guid.Parse(UsrID);
            DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

            if (dtDoctor.Rows.Count > 0) //Checking whether user is doctor or not
            {
                //---user is DOCTOR

                mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());

                bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

                 if (IDUsedOrNot == false)
                {
                 
                    string DoctorRoleID = string.Empty;
                    userObj.ClinicID = UA.ClinicID;

                    //DoctorRoleID = userObj.GetRoleIDOfDoctor();
                    mstrObj.ClinicID = UA.ClinicID;
                    DoctorRoleID = mstrObj.GetRoleIDOfDoctor();

                    roleObj.RoleID = Guid.Parse(DoctorRoleID);

                    roleObj.UserID = Guid.Parse(UsrID);
                  result=   roleObj.DeleteAssignedRoleByUserIDForWM();

                  if (result != string.Empty)
                  {

                      mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                        result=     mstrObj.DeleteDoctorByIDForWM(true);

                        if (result != string.Empty)
                        {
                             roleObj.UserID = Guid.Parse(UsrID);
                             roleObj.ClinicID = UA.ClinicID;
                             DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

                    //RoleID

                    foreach (DataRow dr in dtAssignedRoles.Rows)
                    {
                        roleObj.RoleID = Guid.Parse(dr["RoleID"].ToString());
                        //DeleteAssignedRoleByUserID(UsrID);

                        roleObj.UserID = Guid.Parse(UsrID);
                        roleObj.DeleteAssignedRoleByUserIDForWM();

                    }

                    userObj.UserID = Guid.Parse(UsrID);

                    result = userObj.DeleteUserByUserIDForWM();

                    if (result != string.Empty)
                    {
                        UserDeleted = true;
                    }
                        }


                  }


                        //DeleteUserByUserID(UserID);


                   

                }

            }

            else
            {
                //DeleteAssignedRoleByUserID(UserID);
                userObj.UserID = Guid.Parse(UsrID);

                 result = userObj.DeleteUserByUserIDForWM();

                if (result != string.Empty)
                {
                    UserDeleted = true;
                }

            }

            return UserDeleted;

        }

        #endregion Delete User By UserID

        //EDIT

        #region BindUserDetailsOnEditClick
         /// <summary>
         /// To get specific order details by orderid for the editing purpose
         /// </summary>
         /// <param name=""></param>
         /// <returns></returns>
         [System.Web.Services.WebMethod]
         public static string BindUserDetailsOnEditClick(User userObj)
         {

             ClinicDAL.UserAuthendication UA;
             UIClasses.Const Const = new UIClasses.Const();

             UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

             userObj.ClinicID = UA.ClinicID;
             DataSet dtuser = userObj.GetUserDetailsByUserIDForWM();


             string jsonResult = null;
             DataSet ds = null;
             ds = dtuser;

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
         #endregion BindUserDetailsOnEditClick


        #endregion Methods

         #region Events

         #region Page Load

         protected void Page_Load(object sender, EventArgs e)
        {
            BindDummyRow();

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
         
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                BindDropDownGroupforDoc();
            }
            
        }

        #endregion Page Load

        #region Save
        /// <summary>
        /// Based on radio button of is doctor , user will be added to respective tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;
            if (txtLoginName.Value.TrimStart() != string.Empty || txtPassword.Value.TrimStart() != string.Empty || txtFirstName.Value.TrimStart() != string.Empty || txtEmail.Value.TrimStart() != string.Empty)
            {


                if (txtPassword.Value == txtConfirmPassword.Value)
	        {
		 
                if (rdoNotDoctor.Checked == true)
                {
                    AddUserToUserTable();    //INSERT Case  //---------*User is not doctor , operation :add user to user table 

                    if (hdnUserID.Value != string.Empty) 
                    {

                        //----------Case of UPDATE : user has to be deleted from (1).USER table and conditionally from  [ (2).USER In ROLES   (3).Doctor ]

                        Guid UserID = Guid.Parse(hdnUserID.Value);

                        DeleteDoctorByUserID(UserID);
                        
                    }
                }

    //---------* User is a doctor , Operations : 1.add user to user table , 2.add user to the doctor table , 3.add user - role(doctor) to assignroles table

                    else
                    {
                        if (rdoDoctor.Checked == true)
                        {
                            AddUserToUserTable();
                            AddUserToDoctorTable();
                            AddUserRole();
                        }
                    }


                //BindGriewWithDetailsOfAllUsers();


                //hdnUserCountChanged.Value = "True";


            }

                else
                {
                    //msg = "Passwords do not match ! ";
                    msg = Messages.PassowrdMismatch;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }


            }

            else
            {
                //msg = "Please fill out the mandatory fields";
                msg = Messages.MandatoryFields;

                eObj.InsertionNotSuccessMessage(page, msg);
            }

           // hdnUserID.Value = "";
           
        }

        #endregion Save

        #region Logout

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

        #endregion Logout
       
        //#region Paging
        //protected void dtgViewAllUsers_PreRender(object sender, EventArgs e)
        //{
        //    dtgViewAllUsers.UseAccessibleHeader = false;

        //    if (dtgViewAllUsers.Rows.Count > 0)
        //    {
        //        dtgViewAllUsers.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }   

           
        //}

        //#endregion Paging

        #region Update Image Button Click
        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid userid = Guid.Parse(dtgViewAllUsers.DataKeys[row.RowIndex].Value.ToString());


            Guid UserID = userid;
            RefillUserDetailsOnEditClick(UserID);
            hdnUserID.Value = UserID.ToString();

            BindGriewWithDetailsOfAllUsers();
        }

        #endregion Update Image Button Click

        #region Delete Image Button Click
        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            hdnDeleteButtonClick.Value = "True";


            Errorbox.Attributes.Add("style", "display:none");

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid userid = Guid.Parse(dtgViewAllUsers.DataKeys[row.RowIndex].Value.ToString());

            Guid UserID = userid;
            DeleteDoctorByUserID(UserID);
            //DeleteUserByUserID(UserID);

            BindGriewWithDetailsOfAllUsers();


        }

        #endregion Delete Image Button Click

        #endregion Events


        #region Bind Gridview
        public void BindGriewWithDetailsOfAllUsers()
        {
            userObj.ClinicID = UA.ClinicID;
            DataTable dtUsers = userObj.GetDetailsOfAllUsers();

            if (dtUsers != null)
            {
                dtgViewAllUsers.DataSource = dtUsers;
                dtgViewAllUsers.DataBind();

                lblCaseCount.Text = dtgViewAllUsers.Rows.Count.ToString();

            }

            foreach (GridViewRow myRow in dtgViewAllUsers.Rows)
            {
                ImageButton EditButton = myRow.Cells[0].Controls[1] as ImageButton;
                ImageButton DeleteButton = myRow.Cells[1].Controls[1] as ImageButton;

                string name = myRow.Cells[2].Text;

                if (EditButton != null && DeleteButton != null && name == UA.userName)
                {
                    EditButton.Enabled = false;
                    DeleteButton.Enabled = false;

                    //EditButton.ToolTip = Messages.EditImageButtonDisabled;
                    //DeleteButton.ToolTip = Messages.DeleteImageButtonDisabled;

                    EditButton.ImageUrl = "~/images/Editicon2 (2).png";
                    DeleteButton.ImageUrl = "~/images/Deleteicon2 (3).png";


                }
                else
                {
                    EditButton.Enabled = true;
                    DeleteButton.Enabled = true;

                    EditButton.ImageUrl = "~/images/Editicon1.png";
                    DeleteButton.ImageUrl = "~/images/Deleteicon1.png";


                }

            }

        }

        #endregion Bind Gridview

        public void BindDropDownGroupforDoc()
        {

            DataTable dt = new DataTable();
            dt = MasterObj.GetAllClinics();
            ddlGroup.DataSource = dt;
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataValueField = "ClinicID";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("--Select Clinic--", "-1"));
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds;
            string ClinicID = ddlGroup.SelectedValue;
            ds=ClinicObj.ViewClinic(ClinicID);
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string Clinic_Name = dr["Name"].ToString();               
                ClinicDAL.UserAuthendication UA_Changed = new ClinicDAL.UserAuthendication(UA.userName, ClinicID, Clinic_Name);
                if (UA_Changed.ValidUser)
                {
                    Session[Const.LoginSession] = UA_Changed;
                }
            }
        }
    }
}