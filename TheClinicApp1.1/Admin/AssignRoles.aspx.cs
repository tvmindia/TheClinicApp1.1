using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        #region Bind Dummy Row

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

           
            dummy.Columns.Add(" ");
            dummy.Columns.Add("Role");
            dummy.Columns.Add("Name");
           
            dummy.Columns.Add("UniqueID");

            dummy.Rows.Add();
            dtgViewAllUserInRoles.DataSource = dummy;
            dtgViewAllUserInRoles.DataBind();
        }

        #endregion Bind Dummy Row

        #region Bind Gridview
        public void BindGriewWithDetailsOfAssignedRoles()
        {
            DataTable dtAssignedRoles = roleObj.GetDetailsOfAllAssignedRoles();
            dtgViewAllUserInRoles.DataSource = dtAssignedRoles;
            dtgViewAllUserInRoles.DataBind();
        }

        #endregion Bind Gridview


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

            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleID";
            ddlRoles.DataSource = dtRoles;
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, "--Select--");
        }

        #endregion Bind Roles Dropdown




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








        protected void Page_Load(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string msg = string.Empty;
            if (!Page.IsPostBack)
            {
                BindDummyRow();

                BindUsersDropdown();
                BindRolesDropdown();

            }

           
                if (Request.QueryString["UniqueID"] != null)
                {
                    Guid UniqueID = Guid.Parse(Request.QueryString["UniqueID"].ToString());
                    roleObj.UniqueID = UniqueID;


                DataTable dt =    roleObj.GetAssignedRoleByUniqueID();

                if (dt.Rows[0]["RoleID"].ToString() == GetRoleIDOFDoctor())
                {
                    mstrObj.DoctorID = Guid.Parse(dt.Rows[0]["RoleID"].ToString());
                    bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();

                    if (IDUsedOrNot)
                    {
                        msg = "Already used . Can't be deleted";
                        eObj.DeletionNotSuccessMessage(page, msg);
                        
                    }

                    else
                    {
                        mstrObj.ClinicID = UA.ClinicID;
                        mstrObj.DoctorName = dt.Rows[0]["FirstName"].ToString();
                        mstrObj.DeleteDoctorByName();
                    }

                } 

                else
                {
                    roleObj.DeleteAssignedRoleByUniqueID();
                }

                   

                    hdnUserCountChanged.Value = "True";
                }
           
        }

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            roleObj.UserID = new Guid(ddlUsers.SelectedValue);

            dtUsers = (DataTable)ViewState["dtUsers"];

          

            roleObj.RoleID = new Guid(ddlRoles.SelectedValue);

            roleObj.ClinicID = UA.ClinicID;
           

            roleObj.CreatedBy = UA.userName;
            roleObj.AssignRole();

            hdnUserCountChanged.Value = "True";
        }
    }
}