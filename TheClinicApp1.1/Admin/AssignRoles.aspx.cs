using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.Admin
{
    public partial class AssignRoles : System.Web.UI.Page
    {
        #region Global Variables

        public string RoleName = null;
        ErrorHandling eObj = new ErrorHandling();
        ClinicDAL.Master mstrObj = new ClinicDAL.Master();
        ClinicDAL.UserAuthendication UA;
        RoleAssign roleObj = new RoleAssign();
        UIClasses.Const Const = new UIClasses.Const();
        private static int PageSize = 8;
        ClinicDAL.User userObj = new ClinicDAL.User();
        DataTable dtUsers = null;
        #endregion Global Variables


        #region Methods

        //---*  General Methods *--//

        #region General Methods

        #region Bind Dummy Row

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Role");
            dummy.Columns.Add("Name");

            dummy.Columns.Add("UniqueID");

            dummy.Rows.Add();
            dtgViewAllUserInRoles.DataSource = dummy;
            dtgViewAllUserInRoles.DataBind();
        }

        #endregion Bind Dummy Row

        #region Bind Users Dropdown

        public void BindUsersDropdown()
        {
            dtUsers = roleObj.GetDetailsOfAllUsers();
            ViewState["dtUsers"] = dtUsers;


            ddlUsers.DataTextField = "FirstName";
            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataSource = dtUsers;
            ddlUsers.DataBind();

            ddlUsers.Items.Insert(0, "--Select--");
        }

        #endregion Bind Users Dropdown

        #region Bind Roles Dropdown

        public void BindRolesDropdown()
        {
            DataTable dtRoles = roleObj.GetDetailsOfAllRoles();


            chklstRoles.DataTextField = "RoleName";
            chklstRoles.DataValueField = "RoleID";
            chklstRoles.DataSource = dtRoles;
            chklstRoles.DataBind();

            //chklstRoles.Items.Insert(0, "--Select--");





            //ddlRoles.DataTextField = "RoleName";
            //ddlRoles.DataValueField = "RoleID";
            //ddlRoles.DataSource = dtRoles;
            //ddlRoles.DataBind();
            //ddlRoles.Items.Insert(0, "--Select--");
        }

        #endregion Bind Roles Dropdown

        #region Bind Gridview

        [WebMethod]
        public static string GetMedicines(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            string query = "ViewAllUserInRoles";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;
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



        #endregion  Bind Gridview

        #endregion General Methods

        //--------------------//


        //---*  DOCTOR *--//

        #region Doctor

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

        public void AddUserToDoctorTable(Guid UserID)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.UserID = UserID;
            userObj.ClinicID = UA.ClinicID;
            DataTable dtuser = userObj.GetUserDetailsByUserID();

            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = dtuser.Rows[0]["FirstName"].ToString();
            mstrObj.DoctorPhone = dtuser.Rows[0]["PhoneNo"].ToString();
            mstrObj.DoctorEmail = dtuser.Rows[0]["Email"].ToString();
            mstrObj.createdBy = UA.userName;
            mstrObj.updatedBy = UA.userName;

            mstrObj.UsrID = UserID;

            mstrObj.InsertDoctors();
        }

        #endregion  Add User To Doctor Table

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

                    ListItem listItem = chklstRoles.Items.FindByValue(GetRoleIDOFDoctor());

                    if (listItem != null) listItem.Selected = true;

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


        #endregion Doctor

        //------------------//


        //---*  USER IN ROLE *--//

        #region USER IN ROLE

        #region Assign Role

        #region Assign Role

        /// <summary>
        /// Assigns  role for the user
        /// </summary>
        public void AddUserRole(Guid UserID, Guid RoleID)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            roleObj.ClinicID = UA.ClinicID;

            string roleid = RoleID.ToString();

            roleObj.RoleID = RoleID;
            roleObj.CreatedBy = UA.userName;
            roleObj.UserID = UserID;

            roleObj.AssignRole();

            ListItem listItem = chklstRoles.Items.FindByValue(roleid);

            if (listItem != null) listItem.Selected = true;

        }

        #endregion Assign Role


        #endregion Assign Role

        #region Delete Assigned role By UserID

        public void DeleteAssignedRoleByUserID(Guid UserID)
        {
            roleObj.UserID = UserID;
            roleObj.DeleteAssignedRoleByUserID();
        }

        #endregion Delete Assigned role By UserID

        #endregion USER IN ROLE

        //--------------------//

        #endregion Methods


        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            var page = HttpContext.Current.CurrentHandler as Page;
            string msg = string.Empty;

            if (!Page.IsPostBack)
            {
                BindDummyRow();

                BindUsersDropdown();
                BindRolesDropdown();

            }

        }

        #endregion Page Load

        #region Save Click

        /// <summary>
        /// saving is done based on checkbox tick 1).IF TiCKED : If role is of doctor , user will be added to doctor table and then assigns role, if not doctor , justassigns role
        ///                                       2).IF NOT TICKED : Assigned roles will be deleted and if role was of doctor , entry will be deleted also from doctor table          
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         
        protected void btSave_ServerClick(object sender, EventArgs e)
        {

            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            Guid UserID = new Guid(ddlUsers.SelectedValue);
            roleObj.UserID = UserID;

            roleObj.ClinicID = UA.ClinicID;
            roleObj.CreatedBy = UA.userName;

            roleObj.UserID = new Guid(ddlUsers.SelectedValue);

            roleObj.UserID = UserID;

            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();


            foreach (ListItem item in chklstRoles.Items)
            {

                if (item.Selected) //Checkbox ticked
                {
                    DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'"); //CHecking whether user has already this role, if not , assigns  role for the user


                    if (RoleAssigned.Length == 0)
                    {
                        //--  (1).If the role is doctor, user is added to doctor doctor table in addition to user in roles (2).If not doctor , added only to user in role

                        Guid RoleID = Guid.Parse(item.Value);

                        if (item.Value == GetRoleIDOFDoctor())
                        {
                            AddUserToDoctorTable(UserID);
                        }

                        AddUserRole(UserID, RoleID);  //Assigns role
                    }


                }

                else
                {
                    DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'"); //If the role unticked was an assigned role , delete from respective tables

                     if (RoleAssigned.Length > 0)
                        {
                        DeleteDoctorByUserID(UserID); //Function deleted both assigned role and doctor entry if exists
                        }
                }
            }

            hdnUserCountChanged.Value = "True";
        }

        #endregion Save Click

        #region Users Dropdown Selected Index Changed

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            roleObj.UserID = new Guid(ddlUsers.SelectedValue);
      DataTable dtAssignedRoles =      roleObj.GetAssignedRoleByUserID();

      if (dtAssignedRoles.Rows.Count > 0)
      {
          foreach (ListItem item in chklstRoles.Items)
          {
              DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'");

              if (RoleAssigned.Length == 0)
              {
                  item.Selected = false; 
              }

              else
              {
                  item.Selected = true;
              }
          }

      }

     
        }

        #endregion Users Dropdown Selected Index Changed

        #region LogOut Click

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


        #endregion LogOut Click

        #endregion Events

        //#region Bind Gridview
        //public void BindGriewWithDetailsOfAssignedRoles()
        //{
        //    DataTable dtAssignedRoles = roleObj.GetDetailsOfAllAssignedRoles();
        //    dtgViewAllUserInRoles.DataSource = dtAssignedRoles;
        //    dtgViewAllUserInRoles.DataBind();
        //}

        //#endregion Bind Gridview

    }
}