
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
            //userObj.ClinicID = new Guid("2c7a7172-6ea9-4640-b7d2-0c329336f289");
            userObj.createdBy = UA.userName;
            userObj.updatedBy = UA.userName;
            userObj.passWord = CryptObj.Encrypt(txtPassword.Value);
            userObj.Email = txtEmail.Value;
            userObj.PhoneNo = txtPhoneNumber.Value;

            if (hdnUserID.Value == string.Empty)
            {
                userObj.AddUser();
            }
            else
            {
                userObj.UserID = Guid.Parse(hdnUserID.Value);
                userObj.UpdateuserByUserID();
                //BindGriewWithDetailsOfAllUsers();
            }
              
                 

               

            
        }

        #endregion Add User To User Table

        #endregion User

        //---* To DOCTOR *--//

        #region DOCTOR

        #region GetRoleIDOFDoctor

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


                    DataTable dts = mstrObj.GetDoctorDetails();
                    userObj.UserID = Guid.Parse(hdnUserID.Value);
                    userObj.ClinicID = UA.ClinicID;
                    DataTable dt = userObj.GetUserDetailsByUserID();

                    foreach (DataRow dr in dts.Rows)
                    {
                        string drname = dr["Name"].ToString();

                        if (drname == dt.Rows[0]["FirstName"].ToString())
                        {
                            IsDoctor = true;

                            if (dt.Rows.Count > 0)
                            {
                                mstrObj.UpdateDoctorByName(dt.Rows[0]["FirstName"].ToString());
                                break;
                            }


                        }

                        //else
                        //{
                           
                        //    break;
                        //}

                    }

                    if (IsDoctor == false)
                    {
                        mstrObj.InsertDoctors();
                    }


                } 
            else
            {
                mstrObj.InsertDoctors();
            }
          
            
           
        }

        #endregion Add User To Doctor Table

        #endregion DOCTOR

        //---* To USER-In-ROLES *--//

        #region USER-In-ROLES

        public void AddUserRole()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            roleObj.ClinicID = UA.ClinicID;
            roleObj.RoleID = Guid.Parse(GetRoleIDOFDoctor());
            roleObj.CreatedBy = UA.userName;


            DataTable dtUsers = roleObj.GetDetailsOfAllUsers();

            foreach (DataRow dr in dtUsers.Rows)
            {
                if (dr["LoginName"].ToString() == txtLoginName.Value)
                {
                    roleObj.UserID = Guid.Parse(dr["UserID"].ToString());

                      DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();
                       roleObj.UserID = Guid.Parse(dr["UserID"].ToString());

                    if (dtAssignedRoles.Rows.Count == 0)
	{
		  roleObj.AssignRole();
	}

                }
            }


          

            //if (hdnUserID.Value == string.Empty)
            //{
               
            

           
           
            //roleObj.UserID = Guid.Parse(foundRow["UserID"].ToString());


        }

        #endregion USER-In-ROLES

        #region ValidateLoginName
        [WebMethod]
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

        #region Bind Gridview
        public void BindGriewWithDetailsOfAllUsers()
        {
            DataTable dtUsers = userObj.GetDetailsOfAllUsers();
            dtgViewAllUsers.DataSource = dtUsers;
            dtgViewAllUsers.DataBind();
        }

        #endregion Bind Gridview

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




        #endregion Methods

        #region Events


        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;

              string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (!IsPostBack)
            {
                BindDummyRow();

                //BindGriewWithDetailsOfAllUsers();
            }
            if (Request.QueryString["UsrID"] != null)
            {

                bool IsDoctor = false;


                Guid UserID = Guid.Parse(Request.QueryString["UsrID"].ToString());
                userObj.UserID = UserID;
                userObj.ClinicID = UA.ClinicID;
                DataTable dt = userObj.GetUserDetailsByUserID();

                if (dt.Rows.Count > 0)
                {


                    mstrObj.ClinicID = UA.ClinicID;
                    DataTable dts = mstrObj.GetDoctorDetails();

                    foreach (DataRow dr in dts.Rows)
                    {
                        string drname = dr["Name"].ToString();


                        if (drname == dt.Rows[0]["FirstName"].ToString())
                        {
                            IsDoctor = true;

                            mstrObj.DoctorID = Guid.Parse(dr["DoctorID"].ToString());
                            bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();

                            if (IDUsedOrNot)
                            {
                                msg="Already used . Can't be deleted";
                                eObj.DeletionNotSuccessMessage(page, msg);
                                break;
                            }

                            else
                            {
                                roleObj.UserID = UserID;
                                roleObj.DeleteAssignedRoleByUserID();

                                mstrObj.ClinicID = UA.ClinicID;
                                mstrObj.DoctorName = dt.Rows[0]["FirstName"].ToString();
                                mstrObj.DeleteDoctorByName();

                                userObj.DeleteUserByUserID();
                                break;
                            }

                           
                        }

                      
                            //userObj.DeleteUserByUserID();
                       
                    }



                    if (IsDoctor == false)
                    {
                        userObj.DeleteUserByUserID();
                    }
                    

                }


                //Guid UserID = Guid.Parse(Request.QueryString["UsrID"].ToString());
                //userObj.UserID = UserID;
                //userObj.DeleteUserByUserID();

                //  userObj.UserID = UserID;
                //  userObj.ClinicID = UA.ClinicID;
                //   DataTable dt =      userObj.GetUserDetailsByUserID();

                //   if (dt.Rows.Count > 0)
                //   {
                //       mstrObj.ClinicID = UA.ClinicID;
                //       mstrObj.DoctorName = dt.Rows[0]["FirstName"].ToString();
                //       mstrObj.DeleteDoctorByName();


                //       roleObj.UserID = UserID;
                //       roleObj.DeleteAssignedRoleByUserID();
                       
                //   }

                 

                hdnUserCountChanged.Value = "True";
            }

            if (Request.QueryString["UsrIDtoEdit"] != null)
              {

                if (txtLoginName.Value == string.Empty)
	{
		 
	


                  Guid UserID = Guid.Parse(Request.QueryString["UsrIDtoEdit"].ToString());
                  userObj.UserID = UserID;
                  userObj.ClinicID = UA.ClinicID;
                   DataTable dt =      userObj.GetUserDetailsByUserID();


             txtLoginName.Value = dt.Rows[0]["LoginName"].ToString();
             txtFirstName.Value = dt.Rows[0]["FirstName"].ToString();
             txtLastName.Value = dt.Rows[0]["LastName"].ToString();
             txtPassword.Value = CryptObj.Decrypt(dt.Rows[0]["Password"].ToString());

             txtPhoneNumber.Value = dt.Rows[0]["PhoneNo"].ToString();

             txtEmail.Value = dt.Rows[0]["Email"].ToString();
             bool isActive = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());

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
             userObj.firstName = dt.Rows[0]["FirstName"].ToString();


             mstrObj.ClinicID = UA.ClinicID;
             DataTable dts = mstrObj.GetDoctorDetails();


             foreach (DataRow dr in dts.Rows)
             {
                 string drname = dr["Name"].ToString();
                 if (drname == dt.Rows[0]["FirstName"].ToString())
                 {
                     rdoDoctor.Checked = true;
                     rdoNotDoctor.Checked = false;
                     break;
                 }

                 else
                 {
                     rdoNotDoctor.Checked = true;
                     rdoDoctor.Checked = false;
                     
                 }
             }

             //int DoctorCount = userObj.CheckUserIsDoctor();

             //if (DoctorCount == 1)
             //{
             //    rdoDoctor.Checked = true;
             //    rdoNotDoctor.Checked = false;
             //}

             //else
             //{
             //    rdoNotDoctor.Checked = true;
             //    rdoDoctor.Checked = false;
             //}

             hdnUserID.Value = UserID.ToString();
            }

              }

        }

        #endregion Page Load



        #region Save Server Click

        protected void Save_ServerClick(object sender, EventArgs e)
        {
          
        }

        #endregion Save Server Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;
            if (txtLoginName.Value != string.Empty || txtPassword.Value != string.Empty || txtFirstName.Value != string.Empty || txtEmail.Value != string.Empty)
            {




                //---------*User is not doctor , operation :add user to user table 

                if (rdoNotDoctor.Checked == true)
                {
                    AddUserToUserTable();

                    if (hdnUserID.Value != string.Empty)
                    {
                        mstrObj.ClinicID = UA.ClinicID;
                        mstrObj.DoctorName = txtFirstName.Value;
                        mstrObj.DeleteDoctorByName();

                        roleObj.UserID = Guid.Parse(hdnUserID.Value);
                        roleObj.DeleteAssignedRoleByUserID();
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


            }

            else
            {
                msg = "Please fill out the mandatory fields";

                eObj.InsertionNotSuccessMessage(page, msg);
            }

        }


        #endregion Events



    }
}