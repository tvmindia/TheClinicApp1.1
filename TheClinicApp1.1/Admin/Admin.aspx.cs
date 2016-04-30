
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

            userObj.firstName = txtFirstName.Text;
            userObj.loginName = txtLoginName.Text;
            userObj.lastName = txtLastName.Text;

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
            userObj.passWord = CryptObj.Encrypt(txtPassword.Text);
            userObj.Email = txtEmail.Text;
            userObj.PhoneNo = txtPhoneNumber.Text;

            
                userObj.AddUser();
              
                    //userObj.UserID = Guid.Parse(hdnUserID.Value);
                    //userObj.UpdateuserByUserID();
                    //BindGriewWithDetailsOfAllUsers();

               

            
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
        UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            mstrObj.loginName = txtLoginName.Text;

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = txtFirstName.Text;
            mstrObj.DoctorPhone = txtPhoneNumber.Text;
            mstrObj.DoctorEmail = txtEmail.Text;
            mstrObj.createdBy = UA.userName;
            mstrObj.updatedBy = UA.userName;

            mstrObj.InsertDoctors();
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
                if (dr["LoginName"].ToString() == txtLoginName.Text)
                {
                    roleObj.UserID = Guid.Parse(dr["UserID"].ToString());
                }
            }

            roleObj.AssignRole();
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
            if (!IsPostBack)
            {
                BindDummyRow();

                //BindGriewWithDetailsOfAllUsers();
            }
            if (Request.QueryString["UsrID"] != null)
            {

                Guid UserID = Guid.Parse(Request.QueryString["UsrID"].ToString());
                userObj.UserID = UserID;
                userObj.DeleteUserByUserID();

                hdnUserCountChanged.Value = "True";
            }
           
        }

        #endregion Page Load



        #region Save Server Click

        protected void Save_ServerClick(object sender, EventArgs e)
        {
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;
            if (txtLoginName.Text != string.Empty || txtPassword.Text != string.Empty || txtFirstName.Text != string.Empty || txtEmail.Text != string.Empty)
            {




                //---------*User is not doctor , operation :add user to user table 

                if (rdoNotDoctor.Checked == true)
                {
                    AddUserToUserTable();
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

        #endregion Save Server Click


        #endregion Events



    }
}