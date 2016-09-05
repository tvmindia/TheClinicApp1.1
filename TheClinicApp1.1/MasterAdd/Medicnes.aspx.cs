
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
    public partial class Medicnes : System.Web.UI.Page
    {

        #region Global Variables

        private static int PageSize = 8;
        Master mstrObj = new Master();
        Stocks StockObj = new Stocks();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        #region Medicine View Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterMedicine(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Stocks StockObj = new Stocks();

            StockObj.ClinicID = UA.ClinicID.ToString();

            var xml = StockObj.ViewAndFilterMedicine(searchTerm, pageIndex, PageSize);

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
            dummy.Columns.Add("MedicineName");
            dummy.Columns.Add("CategoryName");
            dummy.Columns.Add("MedicineCode");
            dummy.Columns.Add("Unit");
            dummy.Columns.Add("Qty");
            dummy.Columns.Add("ReOrderQty");
            dummy.Columns.Add("MedicineID");

            dummy.Rows.Add();
            gvMedicines.DataSource = dummy;
            gvMedicines.DataBind();
        }

        #endregion Bind Dummy Row

        #endregion Medicine View Search Paging

        #region Delete Medicine By ID

        [WebMethod]
        public static bool DeleteMedicineByID(string MedicineID)
        {
            string result = string.Empty;
            bool MedicineDeleted = false;

            Stocks StockObj = new Stocks();

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            bool isUsed = false;


            StockObj.MedicineID = Guid.Parse(MedicineID);

            StockObj.ClinicID = UA.ClinicID.ToString();

            isUsed = StockObj.CheckMedicineIDIsUsed();

            if (isUsed == false)
            {
            result=    StockObj.DeleteMedicinesForWM(Guid.Parse(MedicineID));

            if (result != string.Empty)
            {
                MedicineDeleted = true;
            }

            }

            return MedicineDeleted;

        }

        #endregion Delete Medicine By ID

        #region Bind Medicine Details On Edit Click
        /// <summary>
        /// To get specific order details by orderid for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindMedicinesDetailsOnEditClick(Stocks StockObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            StockObj.ClinicID = UA.ClinicID.ToString();
            DataSet dsMedicines = StockObj.GetMedicineDetailsByMedicineID(StockObj.MedicineID);


            string jsonResult = null;
            DataSet ds = null;
            ds = dsMedicines;

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
        #endregion Bind Medicine Details On Edit Click

        #region Add New Medicine
        public void AddMedicine()
        {
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


            if (txtmedicineName.Value.Trim() != string.Empty || txtCode.Value.Trim() != string.Empty || txtOrderQuantity.Value.Trim() != string.Empty)
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

                    StockObj.Name = txtmedicineName.Value.Trim();
                    StockObj.MedCode = txtCode.Value.Trim();
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
                        hdnMedID.Value = StockObj.MedicineID.ToString();
                        //BindGridview();
                    }

                    else
                    {
                        StockObj.UpdatedBy = UA.userName;
                        StockObj.UpdateMedicines(hdnMedID.Value);
                        //BindGridview();
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

                 StockObj.ClinicID = UA.ClinicID.ToString();
                 if (StockObj.ValidateMedicineName(MedicineName.Trim()))
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

            StockObj.ClinicID = UA.ClinicID.ToString();

            if (StockObj.ValidateMedicineCode(MedicineCode.Trim()))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Medicine Code

        #region Logout

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        { string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
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
            BindDummyRow();

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            if (!IsPostBack)
            {
                BindCategory();
                BindUnits();
                //BindGridview();
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
            //BindGridview();
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

            //BindGridview();
        }

        #endregion Update Image Button Click

        #endregion Events

//--NOTE: Below events and functions are not using now

        #region Paging
        protected void gvMedicines_PreRender(object sender, EventArgs e)
        {
            gvMedicines.UseAccessibleHeader = false;

            if (gvMedicines.Rows.Count > 0)
            {
                gvMedicines.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

        }
        #endregion Paging

        #region  Bind Medicine Gridview

        public void BindGridview()
        {
            mstrObj.ClinicID = UA.ClinicID;
            DataTable dt = mstrObj.ViewAllMedicines();

            if (dt != null)
            {
                gvMedicines.DataSource = dt;
                gvMedicines.DataBind();

                lblCaseCount.Text = gvMedicines.Rows.Count.ToString();
            }

        }


        #endregion Bind Medicine Gridview

    }
}