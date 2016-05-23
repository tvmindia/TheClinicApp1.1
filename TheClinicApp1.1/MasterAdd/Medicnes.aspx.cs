
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
    public partial class Medicnes : System.Web.UI.Page
    {

        #region Global Variables


        Master mstrObj = new Master();
        Stocks StockObj = new Stocks();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        #region  Bind Medicine Gridview

        public void BindGridview()
        {
           DataTable dt = mstrObj.ViewAllMedicines();

           if (dt != null)
           {
               gvMedicines.DataSource = dt;
               gvMedicines.DataBind();

               lblCaseCount.Text = gvMedicines.Rows.Count.ToString();  
           }

        }


        #endregion Bind Medicine Gridview

        #region Add New Medicine
        public void AddMedicine()
        {
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


            if (txtmedicineName.Value.TrimStart() != string.Empty || txtCode.Value.TrimStart() != string.Empty || txtOrderQuantity.Value.TrimStart() != string.Empty)
            {

                if (ddlCategory.SelectedItem.Text == "--Select--")
                {
                    msg = Messages.SelectCatergory;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }

                else if (ddlUnits.SelectedItem.Text == "--Select--")
                {
                    msg = Messages.Selectunit;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }

                else if ((txtmedicineName.Value.Contains("$")) || (txtmedicineName.Value.Contains("|")))
                {
                   
                    msg = Messages.ValidMedicineName;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }

                else if (txtOrderQuantity.Value == string.Empty)
                {
                    
                    msg = Messages.ReorderQtyMandatory;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }

                else if (Convert.ToInt32(txtOrderQuantity.Value) <= 0)
                {
                   
                    msg = Messages.validReorderQty;
                    eObj.InsertionNotSuccessMessage(page, msg);
                }

                else
                {
                    UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

                    StockObj.Name = txtmedicineName.Value.TrimStart();
                    StockObj.MedCode = txtCode.Value;
                    StockObj.CategoryID = ddlCategory.SelectedValue;
                    StockObj.ReOrderQty = Convert.ToInt32(txtOrderQuantity.Value);
                    StockObj.ClinicID = UA.ClinicID.ToString();
                    StockObj.CreatedBy = UA.userName;
                    //StockObj.Unit = txtUnit.Text;
                    StockObj.Unit = ddlUnits.SelectedItem.Text;

                    if (hdnMedID.Value == string.Empty)
                    {
                        StockObj.InsertMedicines();

                        hdnInsertedorNot.Value = "True";
                        BindGridview();
                    }

                    else
                    {
                        StockObj.UpdatedBy = UA.userName;
                        StockObj.UpdateMedicines(hdnMedID.Value);
                        BindGridview();
                    }


                }

            }

            else
            {
               
                msg = Messages.MandatoryFields;
                eObj.InsertionNotSuccessMessage(page, msg);
            }
        }

        #endregion Add New Medicine

        #region Clear Controls

        public void ClearControls()
        {
            txtmedicineName.Value = "";
            txtCode.Value = "";
            //txtUnit.Text = "";
            txtOrderQuantity.Value = "";
            BindCategory();
            BindUnits();
        }


        #endregion Clear Controls

        #region Bind Units DropDown

        public void BindUnits()
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            StockObj.ClinicID = UA.ClinicID.ToString();


            ddlUnits.DataSource = StockObj.ViewUnits();
            ddlUnits.DataTextField = "Description";
            ddlUnits.DataValueField = "Code";
            ddlUnits.DataBind();

            ddlUnits.Items.Insert(0, "--Select--");

        }

        #endregion Bind Units DropDown

        #region Bind Category DropDown
        public void BindCategory()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            StockObj.ClinicID = UA.ClinicID.ToString();

            ddlCategory.DataSource = StockObj.ViewCategories();
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, "--Select--");

        }
        #endregion Bind Category DropDown

        #region Validate Medicine Name
        [WebMethod]
        public static bool ValidateMedicineName(string MedicineName)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrobj = new Master();

           
            Stocks StockObj = new Stocks();

            if (StockObj.ValidateMedicineName(MedicineName))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Medicine Name

        #region Validate Medicine Code
        [WebMethod]
        public static bool ValidateMedicineCode(string MedicineCode)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Master mstrobj = new Master();

           
            Stocks StockObj = new Stocks();

            if (StockObj.ValidateMedicineCode(MedicineCode))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Medicine Code

        #region Paging
        protected void gvMedicines_PreRender(object sender, EventArgs e)
        {
            gvMedicines.UseAccessibleHeader = false;

            if (gvMedicines.Rows.Count >0)
            {
                gvMedicines.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        #endregion Paging

        #region Logout

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        #endregion Logout

        #endregion Methods


        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            if (!IsPostBack)
            {
                BindCategory();
                BindUnits();
                BindGridview();
            }
        }

        #endregion Page Load

        #region Save Button Click
        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddMedicine();
        }

        #endregion Save Button Click

        #region Delete Image Button Click

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            bool isUsed = false;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid MedId = Guid.Parse(gvMedicines.DataKeys[row.RowIndex].Value.ToString());

            StockObj.MedicineID = MedId;
           
            StockObj.ClinicID = UA.ClinicID.ToString();

            isUsed = StockObj.CheckMedicineIDIsUsed();

            if (isUsed == false)
            {
                StockObj.DeleteMedicines(MedId);
            }

            else
            {
                
                msg = Messages.AlreadyUsedForDeletion;
                eObj.DeletionNotSuccessMessage(page, msg);
            }
            BindGridview();
        }

        #endregion  Delete Image Button Click

        #region Update Image Button Click

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];


            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid MedId = Guid.Parse(gvMedicines.DataKeys[row.RowIndex].Value.ToString());
            hdnMedID.Value = MedId.ToString();


            StockObj.ClinicID = UA.ClinicID.ToString();
            DataSet ds = StockObj.GetMedicineDetailsByMedicineID(MedId);

            if (ds.Tables[0].Rows.Count > 0)
            {
              DataTable  dt = ds.Tables[0];

              txtmedicineName.Value = dt.Rows[0]["MedicineName"].ToString();
              txtCode.Value = dt.Rows[0]["MedCode"].ToString();
              txtOrderQuantity.Value = dt.Rows[0]["ReOrderQty"].ToString();

              ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(dt.Rows[0]["CategoryName"].ToString()));
              ddlUnits.SelectedIndex = ddlUnits.Items.IndexOf(ddlUnits.Items.FindByText(dt.Rows[0]["Unit"].ToString()));
            
            }

            BindGridview();
        }

        #endregion Update Image Button Click

        #endregion Events
    }
}