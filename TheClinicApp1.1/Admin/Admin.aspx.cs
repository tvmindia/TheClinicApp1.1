
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

using System.Data;
using System.Data.SqlClient;

using TheClinicApp1._1.ClinicDAL;
using System.Web.Services;
using System.Configuration;
using System.Reflection;

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
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        //---* To USER *--//

        #region User

        #region Add User To User Table
        public void AddUserToUserTable()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.firstName = txtFirstName.Value;
            userObj.loginName = txtLoginName.Value;
            userObj.lastName = txtLastName.Value;

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
            userObj.ClinicID = UA.ClinicID;
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
            userObj.UserID = UserID;
            userObj.DeleteUserByUserID();
        }

        #endregion Delete User By UserID

        #region Refill User Details 

        /// <summary>
        /// Controls will be refilled on edit click
        /// </summary>
        /// <param name="UserID"></param>

        public void RefillUserDetailsOnEditClick(Guid UserID)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.UserID = UserID;
            userObj.ClinicID = UA.ClinicID;
            DataTable dtuser = userObj.GetUserDetailsByUserID();

            txtLoginName.Value = dtuser.Rows[0]["LoginName"].ToString();
            txtFirstName.Value = dtuser.Rows[0]["FirstName"].ToString();
            txtLastName.Value = dtuser.Rows[0]["LastName"].ToString();
            txtPassword.Value = CryptObj.Decrypt(dtuser.Rows[0]["Password"].ToString());

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

            DoctorRoleID = userObj.GetRoleIDOfDoctor();

            return DoctorRoleID;
        }

        #endregion GetRoleIDOFDoctor

        #region Add User To Doctor Table
        public void AddUserToDoctorTable()
        {
            bool IsDoctor = false;

        UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

        mstrObj.loginName = txtLoginName.Value;

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = txtFirstName.Value;
            mstrObj.DoctorPhone = txtPhoneNumber.Value;
            mstrObj.DoctorEmail = txtEmail.Value;
            mstrObj.createdBy = UA.userName;
            mstrObj.updatedBy = UA.userName;

            if (hdnUserID.Value != string.Empty)
                {

                    mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
                    mstrObj.ClinicID = UA.ClinicID;
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

                        mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
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
                    msg = "Already used . Can't be deleted";
                    eObj.DeletionNotSuccessMessage(page, msg);

                }

                else
                {
                    DeleteAssignedRoleByUserID(UserID);

                    mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                    mstrObj.DeleteDoctorByID();

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

            roleObj.ClinicID = UA.ClinicID;

            string roleid = GetRoleIDOFDoctor();

            roleObj.RoleID = Guid.Parse(roleid);
            roleObj.CreatedBy = UA.userName;
            roleObj.UserID = Guid.Parse(hdnUserID.Value);

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

        //----------------------//

        #region ValidateLoginName
        [WebMethod]
        ///Checking login name duplication
        public static bool ValidateLoginName(string LogName)
        {
            string loginName = LogName;

            ClinicDAL.User userObj = new ClinicDAL.User();
            if (userObj.ValidateUsername(loginName))
            {
                return true;
            }
            return false;
        }

        #endregion ValidateLoginName

        #region Bind Dummy Row

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add("Edit");
            dummy.Columns.Add(" ");
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

        #region Delete User By UserID

         [WebMethod]
        public static bool DeleteUserByID(string UsrID)
        {

            bool UserDeleted = false;
            ClinicDAL.User userObj = new ClinicDAL.User();


            Guid UserID = Guid.Parse(UsrID);
            userObj.UserID = UserID;
            userObj.DeleteUserByUserID();
            UserDeleted = true;
            //hdnUserCountChanged.Value = "True";
            return UserDeleted;
        }

        #endregion Delete User By UserID

        #region Bind Gridview

         [WebMethod]
        public static string GetMedicines(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            string query = "ViewAndFilterUsers";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;
            //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = new Guid("2c7a7172-6ea9-4640-b7d2-0c329336f289");

            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

            var xml = GetData(cmd, pageIndex).GetXml();
            return xml;
        }

        private static DataSet GetData(SqlCommand cmd, int pageIndex)
        {

            string strConnString = ConfigurationManager.ConnectionStrings["ClinicAppConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "Medicines");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }

         #endregion Bind Gridview


        #endregion Methods

        #region Events


        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {

            PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            isreadonly.SetValue(this.Request.QueryString, false, null);


            
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            lblUserName.Text = "👤 " + UA.userName + " "; 

              string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (!IsPostBack)
            {
                BindDummyRow();

               
            }
            if (Request.QueryString["UsrID"] != null)  //DELETE Button Click
            {
                Guid UserID = Guid.Parse(Request.QueryString["UsrID"].ToString());
                 DeleteDoctorByUserID(UserID);
                 DeleteUserByUserID(UserID);

                 this.Request.QueryString.Remove("UsrID");
             }


        

            if (Request.QueryString["UsrIDtoEdit"] != null)  //EDIT Button Click
              {

                if (txtLoginName.Value == string.Empty)
	            {
                    Guid UserID = Guid.Parse(Request.QueryString["UsrIDtoEdit"].ToString());
                    RefillUserDetailsOnEditClick(UserID);
                     hdnUserID.Value = UserID.ToString();

                     this.Request.QueryString.Remove("UsrIDtoEdit");
                }

              }

            //Removing query string

           
            hdnUserCountChanged.Value = "True";
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
            if (txtLoginName.Value != string.Empty || txtPassword.Value != string.Empty || txtFirstName.Value != string.Empty || txtEmail.Value != string.Empty)
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


               

               
                hdnUserCountChanged.Value = "True";

            }

            else
            {
                msg = "Please fill out the mandatory fields";

                eObj.InsertionNotSuccessMessage(page, msg);
            }
        }

        #endregion Save

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

        //#region Bind Gridview
        //public void BindGriewWithDetailsOfAllUsers()
        //{
        //    DataTable dtUsers = userObj.GetDetailsOfAllUsers();
        //    dtgViewAllUsers.DataSource = dtUsers;
        //    dtgViewAllUsers.DataBind();
        //}

        //#endregion Bind Gridview
    }
}