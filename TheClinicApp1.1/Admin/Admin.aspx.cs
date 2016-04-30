
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

#endregion Included Namespaces

namespace TheClinicApp1._1.Admin
{
    public partial class Admin : System.Web.UI.Page
    {

        #region Global Variables

        ClinicDAL.CryptographyFunctions CryptObj = new CryptographyFunctions();
        ClinicDAL.User userObj = new ClinicDAL.User();
        ClinicDAL.Master mstrObj = new ClinicDAL.Master();
        ClinicDAL.RoleAssign roleObj = new RoleAssign();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

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

        #endregion Methods

        #region Events


        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Page Load

        #region Save Server Click

        protected void Save_ServerClick(object sender, EventArgs e)
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

        #endregion Save Server Click


        #endregion Events



    }
}