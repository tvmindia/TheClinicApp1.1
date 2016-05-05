using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;


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

        }

        #endregion Bind Units Gridview

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;

            if (!IsPostBack)
            {
                BindGridview();
            }

        }

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            if (txtCOde.Value !=string.Empty && txtDescription.Value != string.Empty)
            {
                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

                mstrObj.Code = txtCOde.Value;
                mstrObj.Description = txtDescription.Value;
                mstrObj.ClinicID = UA.ClinicID;
                mstrObj.createdBy = UA.userName;

                mstrObj.InsertUnits();
                BindGridview();
            }

            else
            {
                msg = "Please fill out all the fields";
                eObj.InsertionNotSuccessMessage(page, msg);
            }

            
        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            bool isUsed = false;
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;



            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            mstrObj.ClinicID = UA.ClinicID;
            DataTable dtUnits = mstrObj.ViewAllUnits();

           


            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid UnitId = Guid.Parse(dtgViewAllUnits.DataKeys[row.RowIndex].Value.ToString());


            foreach (DataRow dr in dtUnits.Rows)
            {
                if (dr["UnitID"].ToString() == UnitId.ToString())
                {
                    mstrObj.Description = dr["Description"].ToString();

                    DataTable dt1 = mstrObj.GetPrescDTByUnit();
                    DataTable dt2 = mstrObj.GetReceiptDTByUnit();
                    DataTable dt3 = mstrObj.GetMedicineByUnit();


                    if ( (dt1.Rows.Count > 0) || (dt2.Rows.Count > 0) || (dt3.Rows.Count > 0))
                    {
                         msg = "Already used . Can't be deleted";
                          eObj.DeletionNotSuccessMessage(page, msg);
                          isUsed = true;
                          break;
                    }



                }
            }

            if (isUsed == false)
            {
                mstrObj.UnitID = UnitId;
                mstrObj.DeleteUnitByUnitId();

                BindGridview();
            }
           
        }
    }
}