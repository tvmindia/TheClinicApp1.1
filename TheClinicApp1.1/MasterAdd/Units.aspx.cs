
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

        Master mstrObj = new Master();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

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

        #endregion Methods

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            mstrObj.ClinicID = UA.ClinicID;
           
            
            if (!IsPostBack)
            {
                BindGridview();
            }

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

                BindGridview();
            }

            else
            {
                //msg = "Please fill out all the fields";
                msg = Messages.MandatoryFields;
                eObj.InsertionNotSuccessMessage(page, msg);
            }

            
        }

        #endregion Save Button Click

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

             isUsed = mstrObj.CheckUnitIsUsed();

            if (isUsed == false)
            {
                mstrObj.UnitID = UnitId;
                mstrObj.DeleteUnitByUnitId();

                BindGridview();
            }

            else
            {
                //msg = "Already used . Can't be deleted";
                msg = Messages.AlreadyUsedForDeletion;
                eObj.DeletionNotSuccessMessage(page, msg);
            }

        }

        #endregion Delete Button Click

        #region Validate Unit

        [WebMethod]
        public static bool ValidateUnit(string Unit)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrObj = new Master();

            if (mstrObj.CheckUnitDuplication(Unit))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Unit

        #region LogOut

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

        #endregion LogOut

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

        #endregion Events
    }
}