
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
using Messages = TheClinicApp1._1.UIClasses.Messages;

#endregion Included Namespaces

namespace TheClinicApp1._1.MasterAdd
{
    public partial class Units : System.Web.UI.Page
    {
        #region Global Variables

        private static int PageSize = 8;
        Master mstrObj = new Master();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        #region Unit View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterUnits(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrObj = new Master();

            mstrObj.ClinicID = UA.ClinicID;
            var xml = mstrObj.ViewAndFilterUnits(searchTerm, pageIndex, PageSize);
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
            dummy.Columns.Add("Description");
            dummy.Columns.Add("UnitID");


            dummy.Rows.Add();

            dtgViewAllUnits.DataSource = dummy;
            dtgViewAllUnits.DataBind();
        }

        #endregion Bind Dummy Row


        #endregion  Unit View Search Paging

        #region Validate Unit

        [WebMethod]
        public static bool ValidateUnit(string Unit)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrObj = new Master();
            mstrObj.ClinicID = UA.ClinicID;
            if (mstrObj.CheckUnitDuplication(Unit))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Unit

        #region Delete Unit By ID

        [WebMethod]
        public static bool DeleteUnitByID(string UnitID)
        {
            bool UnitDeleted = false;
            bool isUsed = false;

            Master mstrObj = new Master();

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];


            DataTable dtUnits = mstrObj.ViewAllUnits();

            mstrObj.UnitID = Guid.Parse(UnitID);

            DataTable dt = mstrObj.GetUnitByID();

            if (dt.Rows.Count > 0)
            {
                mstrObj.Description = dt.Rows[0]["Description"].ToString();
            }

            mstrObj.ClinicID = UA.ClinicID;
            isUsed = mstrObj.CheckUnitIsUsed();

            if (isUsed == false)
            {
                mstrObj.UnitID = Guid.Parse(UnitID);
                mstrObj.DeleteUnitByUnitIdForWM();
                UnitDeleted = true;
            }


            return UnitDeleted;

        }

        #endregion Delete Unit By ID

        #region Bind Unit Details On Edit Click
        /// <summary>
        /// To get specific Unit details by UnitID for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindUnitDetailsOnEditClick(Master mstrObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            mstrObj.ClinicID = UA.ClinicID;
            DataSet dtUnit = mstrObj.GetUnitByIDForWM();


            string jsonResult = null;
            DataSet ds = null;
            ds = dtUnit;

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
        #endregion Bind Unit Details On Edit Click

        #endregion Methods

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            mstrObj.ClinicID = UA.ClinicID;

            BindDummyRow();
            
            //if (!IsPostBack)
            //{
            //    BindGridview();
            //}

        }

        #endregion Page Load

        #region Save Button Click

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
           
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            if (txtDescription.Value.TrimStart() != string.Empty)
            {
                //mstrObj.Code = txtCOde.Value;
                mstrObj.Code = txtDescription.Value.TrimStart();
                mstrObj.Description = txtDescription.Value.TrimStart();
               
                if (hdnUnitID.Value == string.Empty)
                {
                     mstrObj.createdBy = UA.userName;
                    mstrObj.InsertUnits();
                    hdnUnitID.Value = mstrObj.UnitID.ToString();
                }

                else
                {
                    mstrObj.UnitID = Guid.Parse(hdnUnitID.Value);
                    mstrObj.updatedBy = UA.userName;

                    DataTable dt = mstrObj.GetUnitByID();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                         mstrObj.Description =   dt.Rows[0]["Description"].ToString();
                    }

                    mstrObj.ClinicID = UA.ClinicID;
                  bool isUsed = mstrObj.CheckUnitIsUsed();

                  if (isUsed == false)
                    {
                        mstrObj.Description = txtDescription.Value;
                        mstrObj.UpdateUnits();
                    }
                    else
                  {
                      //msg = "Already used . Can't be changed";

                      msg = Messages.AlreadyUsedForUpdation;
                      eObj.InsertionNotSuccessMessage(page, msg);
                  }
                  

                    
                }

                //BindGridview();
            }

            else
            {
                //msg = "Please fill out all the fields";
                msg = Messages.MandatoryFields;
                eObj.InsertionNotSuccessMessage(page, msg);
            }

            
        }

        #endregion Save Button Click

        #region LogOut

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

        #endregion LogOut

        #endregion Events

        //--NOTE: Below events and functions are not using now

        #region Delete Button Click

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            bool isUsed = false;
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            DataTable dtUnits = mstrObj.ViewAllUnits();

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid UnitId = Guid.Parse(dtgViewAllUnits.DataKeys[row.RowIndex].Value.ToString());

            mstrObj.UnitID = UnitId;

            DataTable dt = mstrObj.GetUnitByID();

            if (dt.Rows.Count > 0)
            {
                mstrObj.Description = dt.Rows[0]["Description"].ToString();
            }

            mstrObj.ClinicID = UA.ClinicID;
            isUsed = mstrObj.CheckUnitIsUsed();

            if (isUsed == false)
            {
                mstrObj.UnitID = UnitId;
                mstrObj.DeleteUnitByUnitId();

                //BindGridview();
            }

            else
            {
                //msg = "Already used . Can't be deleted";
                msg = Messages.AlreadyUsedForDeletion;
                eObj.DeletionNotSuccessMessage(page, msg);
            }

        }

        #endregion Delete Button Click

        #region Update Button Click

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid UnitID = Guid.Parse(dtgViewAllUnits.DataKeys[row.RowIndex].Value.ToString());
            hdnUnitID.Value = UnitID.ToString();

            mstrObj.UnitID = UnitID;
            DataTable dt = mstrObj.GetUnitByID();

            if (dt != null && dt.Rows.Count > 0)
            {
                txtDescription.Value = dt.Rows[0]["Description"].ToString();
            }


        }

        #endregion Update Button Click

        #region Paging
        protected void dtgViewAllUnits_PreRender(object sender, EventArgs e)
        {
            dtgViewAllUnits.UseAccessibleHeader = false;

            if (dtgViewAllUnits.Rows.Count > 0)
            {
                dtgViewAllUnits.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }
        #endregion Paging

        #region Bind Units Gridview

        public void BindGridview()
        {

            DataTable dtUnits = mstrObj.ViewAllUnits();

            if (dtUnits != null)
            {
                dtgViewAllUnits.DataSource = dtUnits;
                dtgViewAllUnits.DataBind();

                lblCaseCount.Text = dtgViewAllUnits.Rows.Count.ToString();
            }

        }

        #endregion Bind Units Gridview

    }
}