
#region Included Namespaces
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
using Messages = TheClinicApp1._1.UIClasses.Messages;
#endregion Included Namespaces

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

        #region AssignRoles View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterAssignedRoles(string searchTerm, int pageIndex, string clinicID)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            RoleAssign roleObj = new RoleAssign();

            if (clinicID == string.Empty)
            {
                roleObj.ClinicID = UA.ClinicID;
            }
            else
            {
                roleObj.ClinicID = Guid.Parse(clinicID);
            }

            var xml = roleObj.ViewAndFilterAssignedRoles(searchTerm, pageIndex, PageSize);
            return xml;

        }

        #region Bind Dummy Row

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Role");
            dummy.Columns.Add("Name");

            //dummy.Columns.Add("UniqueID");
            dummy.Columns.Add("UserID");
            

            dummy.Rows.Add();
            dtgViewAllUserInRoles.DataSource = dummy;
            dtgViewAllUserInRoles.DataBind();
        }

        #endregion Bind Dummy Row


        #endregion AssignRoles View Search Paging


        //---*  General Methods *--//

        #region General Methods


        #region Bind Users Dropdown

        public void BindUsersDropdown()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string loginedUserID = UA.UserID.ToString();

            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }
            
            dtUsers = roleObj.GetDetailsOfAllUsers();
            ViewState["dtUsers"] = dtUsers;

            ddlUsers.DataTextField = "LoginName";
            ddlUsers.DataValueField = "UserID";
            ddlUsers.DataSource = dtUsers;
            ddlUsers.DataBind();

            foreach (ListItem itm in ddlUsers.Items)
            {
                if (itm.Value == loginedUserID)
                {
                    itm.Attributes.Add("disabled", "disabled");
                    itm.Attributes.Add("title",Messages.DisableAssignRole);
                }
            }
            //ddlUsers.Items.FindByValue(loginedUserID).Enabled = false;


            ddlUsers.Items.Insert(0, "--Select--");
        }

        #endregion Bind Users Dropdown

        #region Bind Clinic Dropdown

        public void BindClinicDropdown()
        {
            DataTable dt = new DataTable();
            dt = mstrObj.GetAllClinics();
            ddlClinic.DataSource = dt;
            ddlClinic.DataTextField = "Name";
            ddlClinic.DataValueField = "ClinicID";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("--Select Clinic--", "-1"));
        }

        #endregion Bind Clinic Dropdown

        #region Bind Roles Dropdown

        public void BindRolesDropdown()
        {
            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }

            DataTable dtRoles = roleObj.GetDetailsOfAllRoles();
            DataTable dtgRoles = dtRoles.Copy();
            //foreach(DataRow dr in dtgRoles.Rows)
            for (int i = 0; i < dtRoles.Rows.Count;i++)
            {

                if (dtRoles.Rows[i]["RoleName"].ToString() == Const.RoleSadmin)
                {
                    dtgRoles.Rows.Remove(dtgRoles.Rows[i]);
                    dtgRoles.AcceptChanges();
                   
                }
            }
            chklstRoles.DataTextField = "RoleName";
            chklstRoles.DataValueField = "RoleID";
            chklstRoles.DataSource = dtgRoles;
            chklstRoles.DataBind();
            
        }

        #endregion Bind Roles Dropdown

        #region Get Users

        [WebMethod]
        public static List<ListItem> GetUsers(string clinicID)
        {
            RoleAssign roleObj = new RoleAssign();
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];


            DataTable dt = new DataTable();

            if (clinicID !=string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(clinicID);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }

            dt = roleObj.GetDetailsOfAllUsers();

            List<ListItem> Users = new List<ListItem>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                  Users.Add(new ListItem
                  {
                    Value= dt.Rows[i]["UserID"].ToString(),
                    Text = dt.Rows[i]["LoginName"].ToString()
                              
                 });
            }


            return Users;

            //string query = "SELECT CustomerId, Name FROM Customers";
            //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(constr))
            //{
            //    using (SqlCommand cmd = new SqlCommand(query))
            //    {
            //        List<ListItem> customers = new List<ListItem>();
            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = con;
            //        con.Open();
            //        using (SqlDataReader sdr = cmd.ExecuteReader())
            //        {
            //            while (sdr.Read())
            //            {
            //                customers.Add(new ListItem
            //                {
            //                    Value = sdr["CustomerId"].ToString(),
            //                    Text = sdr["Name"].ToString()
            //                });
            //            }
            //        }
            //        con.Close();
            //        return customers;
            //    }
            //}
        }

        #endregion Get Users

        #region Get Roles
        [WebMethod]
        public static List<ListItem> GetRoles(string clinicID)
        {
            RoleAssign roleObj = new RoleAssign();
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            DataTable dt = new DataTable();

            if (clinicID != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(clinicID);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }
             dt = roleObj.GetDetailsOfAllRoles();
            
            List<ListItem> Roles = new List<ListItem>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Roles.Add(new ListItem
                {
                    Value = dt.Rows[i]["RoleID"].ToString(),
                    Text = dt.Rows[i]["RoleName"].ToString()

                });
            }


            return Roles;

        }

        #endregion Get Roles

        #region Get Assigned Roles

        [WebMethod]
        public static string GetAssignedRoles(RoleAssign roleObj, string clinicID)
        {
            //Master MasterObj = new Master();
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            string jsonResult = null;
            DataTable dt = null;

            if (clinicID != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(clinicID.ToString());
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }
            
            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            dt = dtAssignedRoles;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            jsonResult = jsSerializer.Serialize(parentRow);

            return jsonResult; //Converting to Json

        }

        #endregion Get Assigned Roles

        #endregion General Methods

        //--------------------//

        //---*  DOCTOR *--//

        #region Doctor

        #region GetRoleIDOFDoctor

        public string GetRoleIDOFDoctor()
        {
            string DoctorRoleID = string.Empty;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (hdnClinicID.Value != string.Empty)
            {
                userObj.ClinicID = Guid.Parse(hdnClinicID.Value);
                mstrObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                userObj.ClinicID = UA.ClinicID;

                mstrObj.ClinicID = UA.ClinicID;
            }


          
            //DoctorRoleID = userObj.GetRoleIDOfDoctor();
            DoctorRoleID = mstrObj.GetRoleIDOfDoctor();

            return DoctorRoleID;
        }

        #endregion GetRoleIDOFDoctor

        #region Add User To Doctor Table

        public int AddUserToDoctorTable(Guid UserID)
        {
            int rslt = 0;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            userObj.UserID = UserID;

            if (hdnClinicID.Value != string.Empty)
	{
        userObj.ClinicID = Guid.Parse(hdnClinicID.Value);
        mstrObj.ClinicID = Guid.Parse(hdnClinicID.Value);
	}

            else
            {
                userObj.ClinicID = UA.ClinicID;
                mstrObj.ClinicID = UA.ClinicID;
            }
            
            DataTable dtuser = userObj.GetUserDetailsByUserID();

           
            mstrObj.DoctorName = dtuser.Rows[0]["FirstName"].ToString();
            mstrObj.DoctorPhone = dtuser.Rows[0]["PhoneNo"].ToString();
            mstrObj.DoctorEmail = dtuser.Rows[0]["Email"].ToString();
            mstrObj.createdBy = UA.userName;
            mstrObj.updatedBy = UA.userName;

            mstrObj.UsrID = UserID;

            rslt= mstrObj.InsertDoctors();
            return rslt;
        }

        #endregion  Add User To Doctor Table

        #region Delete Doctor By UserID

        /// <summary>
        /// Doctor will be deleted from doctor and userinroles tables iff DoctorID is not not used yet , otherwise shows 'already used' message
        /// </summary>
        /// <param name="UserID"></param>
        public void DeleteDoctorByUserID(Guid UserID)
        {
            int rslt = 0;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;

            if (hdnClinicID.Value != string.Empty)
            {
                mstrObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                mstrObj.ClinicID = UA.ClinicID;
            }
            
            mstrObj.UsrID = UserID;
            DataTable dtDoctor = mstrObj.GetDoctorDetailsByUserID();

            if (dtDoctor.Rows.Count > 0) //Checking whether user is doctor or not
            {
                //---user is DOCTOR

                mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                string DrID = GetRoleIDOFDoctor();


                bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

                if (IDUsedOrNot) //checking whether doctorid is already used ,if not used doctor is get deleted
                {
                       //------* Deletes all roles except doctor role , as it has been used
                  
                    roleObj.UserID = new Guid(ddlUsers.SelectedValue);

                    if (hdnClinicID.Value != string.Empty)
                    {
                        roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
                    }
                    else{
                         roleObj.ClinicID = UA.ClinicID;
                    }
                   
                    DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

                    if (dtAssignedRoles.Rows.Count > 0)
                    {
                        foreach (ListItem item in chklstRoles.Items)
                        {
                            if (item.Selected == false) //Checkbox ticked
                            {
                            DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'");


                            foreach (DataRow row in RoleAssigned)
                            {
                                if (row["RoleID"].ToString() != DrID)
                                {
                                    DeleteAssignedRoleByUserIDAndRoleID(Guid.Parse(ddlUsers.SelectedValue), Guid.Parse(item.Value));
                                }

                                else
                                {
                                    //msg = "Already used . Can't be deleted";
                                    msg = Messages.AlreadyUsedForDeletion;
                                    eObj.DeletionNotSuccessMessage(page, msg);

                                }
                            }
                          

                        }
                       
                        }

                        roleObj.UserID = new Guid(ddlUsers.SelectedValue);

                        if (hdnClinicID.Value != string.Empty)
                        {
                            roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
                        }
                        else
                        {
                            roleObj.ClinicID = UA.ClinicID;
                        }
                       
                        dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

                        foreach (ListItem item in chklstRoles.Items)
                        {
                            DataRow[] CurrentRoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'");

                            if (CurrentRoleAssigned.Length == 0)
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

                else
                {
                 rslt =    DeleteAssignedRoleByUserID(UserID);

                 if (rslt == 1)
                 {
                     mstrObj.DoctorID = Guid.Parse(dtDoctor.Rows[0]["DoctorID"].ToString());
                     mstrObj.DeleteDoctorByID(true);
                 }
                  
                }

            }

            else
            {
                DeleteAssignedRoleByUserID(UserID);
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

            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }
           

            string roleid = RoleID.ToString();

            roleObj.RoleID = RoleID;
            roleObj.CreatedBy = UA.userName;
            roleObj.UserID = UserID;

            roleObj.AssignRole();

            //ListItem listItem = chklstRoles.Items.FindByValue(roleid);

            //if (listItem != null) listItem.Selected = true;

        }

        #endregion Assign Role


        #endregion Assign Role

        #region Delete Assigned role By UserID

        public int DeleteAssignedRoleByUserID(Guid UserID)
        {
            int rslt = 0;
            roleObj.UserID = UserID;
            rslt = roleObj.DeleteAssignedRoleByUserID();
            return rslt;
        }

        #endregion Delete Assigned role By UserID

        #region Delete Assigned role By UserID And RoleID

        public void DeleteAssignedRoleByUserIDAndRoleID(Guid UserID,Guid RoleID)
        {

            roleObj.UserID = UserID;
            roleObj.RoleID = RoleID;
            roleObj.DeleteAssignedRoleByUserID();
        }

        #endregion Delete Assigned role By UserID And RoleID

        #region Get Role Assigned

        #endregion Get Role Assigned

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

            if (UA.userName != "sadmin")
            {
                ddlClinic.Visible = false;
            }
            else
            {
                ddlClinic.Visible = true;
            }


            if (!Page.IsPostBack)
            {
                BindDummyRow();

                //BindGriewWithDetailsOfAssignedRoles();

                BindUsersDropdown();
                BindRolesDropdown();
                BindClinicDropdown();

            }

            string loginedUserID = UA.UserID.ToString();

            foreach (ListItem itm in ddlUsers.Items)
            {
                if (itm.Value == loginedUserID)
                {
                    itm.Attributes.Add("disabled", "disabled");
                    itm.Attributes.Add("title", Messages.DisableAssignRole);
                }
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
            var SelectedRoles = hdnSelectedRoles.Value;
            string[] Roles = new string[] { };

            if (SelectedRoles.Contains('|'))
            {
               Roles  = SelectedRoles.Split('|');

            }
           
            if ((ddlUsers.SelectedValue != "--Select--" && ddlUsers.SelectedValue != string.Empty) || (hdnSelectedUservalue.Value != string.Empty))
            {
            int rslt = 0;
            string SelectedUserID = string.Empty;
            string msg = string.Empty;

            if (hdnSelectedUservalue.Value != string.Empty)
            {
                SelectedUserID = hdnSelectedUservalue.Value;
            }
            else
            {
               SelectedUserID =  ddlUsers.SelectedValue;
            }
            var page = HttpContext.Current.CurrentHandler as Page;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
                

            Guid UserID = new Guid(SelectedUserID);
            roleObj.UserID = UserID;

            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }
           
            roleObj.CreatedBy = UA.userName;

            roleObj.UserID = new Guid(SelectedUserID);

            roleObj.UserID = UserID;

            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }

            //roleObj.ClinicID = UA.ClinicID;
            DataTable dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            foreach (ListItem item in chklstRoles.Items)
            {
                int pos = Array.IndexOf(Roles, item.Text);


                if (item.Selected || pos> -1) //Checkbox ticked
                {
                    DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'"); //CHecking whether user has already this role, if not , assigns  role for the user


                    if (RoleAssigned.Length == 0)
                    {
                        //--  (1).If the role is doctor, user is added to doctor doctor table in addition to user in roles (2).If not doctor , added only to user in role

                        Guid RoleID = Guid.Parse(item.Value);

                        if (item.Value == GetRoleIDOFDoctor())
                        {
                          rslt =  AddUserToDoctorTable(UserID);

                          if (rslt == 1)
                          {
                                AddUserRole(UserID, RoleID); //Assign dr role
                          }

                        }
                        else
                        {
                            AddUserRole(UserID, RoleID);  //Assigns role
                        }
                       
                    }


                }

                else
                {
                    DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'"); //If the role unticked was an assigned role , delete from respective tables

                     if (RoleAssigned.Length > 0)
                        {
                            roleObj.RoleID = Guid.Parse(item.Value);
                            DeleteDoctorByUserID(UserID); //Function deleted both assigned role and doctor entry if exists
                        }
                }
            }


            //-------------------- * Rebinding checkbox list *----------------------//

            //roleObj.UserID = new Guid(SelectedUserID);

            //if (hdnClinicID.Value != string.Empty)
            //{
            //    roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            //}
            //else
            //{
            //    roleObj.ClinicID = UA.ClinicID;
            //}


            //roleObj.ClinicID = UA.ClinicID;
            // dtAssignedRoles = roleObj.GetAssignedRoleByUserID();

            //if (dtAssignedRoles.Rows.Count > 0)
            //{
            //    foreach (ListItem item in chklstRoles.Items)
            //    {
            //        DataRow[] RoleAssigned = dtAssignedRoles.Select("RoleID = '" + item.Value + "'");

            //        if (RoleAssigned.Length == 0)
            //        {
            //            item.Selected = false;
            //        }

            //        else
            //        {
            //            item.Selected = true;
            //        }
            //    }

            //}
            //else
            //{
            //    foreach (ListItem item in chklstRoles.Items)
            //    {
            //        item.Selected = false;
            //    }
            //}
          


            //BindGriewWithDetailsOfAssignedRoles();

            //hdnUserCountChanged.Value = "True";
            }


            BindUsersDropdown();
            BindRolesDropdown();

            //hdnClinicID.Value = string.Empty;
            hdnSelectedRoles.Value = string.Empty;
            hdnSelectedUservalue.Value = string.Empty;

        }

        #endregion Save Click

        #region Users Dropdown Selected Index Changed

        protected void ddlUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            if (ddlUsers.SelectedValue != "--Select--")
	{
            roleObj.UserID = new Guid(ddlUsers.SelectedValue);

            if (hdnClinicID.Value != string.Empty)
            {
                roleObj.ClinicID = Guid.Parse(hdnClinicID.Value);
            }
            else
            {
                roleObj.ClinicID = UA.ClinicID;
            }

            
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
      else
      {
          foreach (ListItem item in chklstRoles.Items)
          {
              item.Selected = false; 
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
        { string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        }


        #endregion LogOut Click

        

        #endregion Events

        //--NOTE: Below events and functions are not using now

        #region Paging
        protected void dtgViewAllUserInRoles_PreRender(object sender, EventArgs e)
        {
            dtgViewAllUserInRoles.UseAccessibleHeader = false;

            if (dtgViewAllUserInRoles.Rows.Count > 0)
            {
                dtgViewAllUserInRoles.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }

        #endregion Paging

        #region Bind Gridview
        public void BindGriewWithDetailsOfAssignedRoles()
        {
            roleObj.ClinicID = UA.ClinicID;
            DataTable dtAssignedRoles = roleObj.GetDetailsOfAllAssignedRoles();
            dtgViewAllUserInRoles.DataSource = dtAssignedRoles;
            dtgViewAllUserInRoles.DataBind();

            lblCaseCount.Text = dtgViewAllUserInRoles.Rows.Count.ToString();

        }

        #endregion Bind Gridview
    }
}