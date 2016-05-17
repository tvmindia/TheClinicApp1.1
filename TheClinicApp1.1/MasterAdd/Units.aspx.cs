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

namespace TheClinicApp1._1.MasterAdd
{
    public partial class Units : System.Web.UI.Page
    {

        Master mstrObj = new Master();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #region Bind Units Gridview

        public void BindGridview()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            mstrObj.ClinicID = UA.ClinicID;
      DataTable   dtUnits =     mstrObj.ViewAllUnits();

      dtgViewAllUnits.DataSource = dtUnits;
      dtgViewAllUnits.DataBind();

      lblCaseCount.Text = dtgViewAllUnits.Rows.Count.ToString();

        }

        #endregion Bind Units Gridview

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            //lblClinicName.Text = UA.Clinic;
            //lblUserName.Text = "👤 " + UA.userName + " "; 

            if (!IsPostBack)
            {
                BindGridview();
            }

        }

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            if (txtDescription.Value != string.Empty)
            {
                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

                //mstrObj.Code = txtCOde.Value;
                mstrObj.Code = txtDescription.Value.TrimStart();
                mstrObj.Description = txtDescription.Value.TrimStart();
                mstrObj.ClinicID = UA.ClinicID;
               

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

                    if (dt.Rows.Count > 0)
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

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            bool isUsed = false;
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;



            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            mstrObj.ClinicID = UA.ClinicID;
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


            //foreach (DataRow dr in dtUnits.Rows)
            //{
            //    if (dr["UnitID"].ToString() == UnitId.ToString())
            //    {
            //        mstrObj.Description = dr["Description"].ToString();

            //        DataTable dt1 = mstrObj.GetPrescDTByUnit();
            //        DataTable dt2 = mstrObj.GetReceiptDTByUnit();
            //        DataTable dt3 = mstrObj.GetMedicineByUnit();


            //        if ( (dt1.Rows.Count > 0) || (dt2.Rows.Count > 0) || (dt3.Rows.Count > 0))
            //        {
            //             msg = "Already used . Can't be deleted";
            //              eObj.DeletionNotSuccessMessage(page, msg);
            //              isUsed = true;
            //              break;
            //        }



            //    }
            //}

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


        #region Validate Unit 

        [WebMethod]
        public static bool ValidateUnit(string Unit)
        {
            Master mstrObj = new Master();

            if (mstrObj.CheckUnitDuplication(Unit))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Unit


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

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];


            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid UnitID = Guid.Parse(dtgViewAllUnits.DataKeys[row.RowIndex].Value.ToString());
            hdnUnitID.Value = UnitID.ToString();

            mstrObj.UnitID = UnitID;
            DataTable dt = mstrObj.GetUnitByID();

            if (dt.Rows.Count > 0)
            {
                txtDescription.Value = dt.Rows[0]["Description"].ToString();
            }





        }

        #region Paging
        protected void dtgViewAllUnits_PreRender(object sender, EventArgs e)
        {
            dtgViewAllUnits.UseAccessibleHeader = false;
            dtgViewAllUnits.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        #endregion Paging
    }
}