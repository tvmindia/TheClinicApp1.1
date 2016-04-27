
#region CopyRight

//Author      : SHAMILA T P
//Created Date: April-12-2016

#endregion CopyRight


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

using TheClinicApp1._1.ClinicDAL;


namespace TheClinicApp1._1.Stock
{
    public partial class AddNewMedicine : System.Web.UI.Page
    {

        #region Global Variables

        Stocks StockObj = new Stocks();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Methods

        #region Clear Controls

        public void ClearControls()
        {
            txtmedicineName.Text = "";
            txtCode.Text = "";
            //txtUnit.Text = "";
            txtOrderQuantity.Text = "";
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

        #region Add New Medicine
        public void AddMedicine()
        {
            string msg = string.Empty;

              var page = HttpContext.Current.CurrentHandler as Page;

            


              if (ddlCategory.SelectedItem.Text == "--Select--")
              {
                   msg = "Please select a category ! ";

                eObj.InsertionNotSuccessMessage(page, msg);
              }

            //  else  if (txtmedicineName.Text.Contains("$") )
            //{
            //    msg = "Medicine Name is not valid as it contains $";

            //    eObj.InsertionNotSuccessMessage(page, msg);
            //    //Medicine Name is not valid as it contains $ ,|
            //}

            //  else if (txtmedicineName.Text.Contains("|"))
            //{
            //    msg = "Medicine Name is not valid as it contains |";

            //    eObj.InsertionNotSuccessMessage(page, msg);
            //}

              else if ( (txtmedicineName.Text.Contains("$")) || (txtmedicineName.Text.Contains("|")) )
              {
                  msg = "Please enter a valid medicine name";
                  eObj.InsertionNotSuccessMessage(page, msg);
              }


              else if(Convert.ToInt32(txtOrderQuantity.Text) <= 0)
            {
                msg = "Please enter a quantity greater than 0";

                eObj.InsertionNotSuccessMessage(page, msg);
            }

           
            else
            {
                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

                StockObj.Name = txtmedicineName.Text;
                StockObj.MedCode = txtCode.Text;
                StockObj.CategoryID = ddlCategory.SelectedValue;
                StockObj.ReOrderQty = Convert.ToInt32(txtOrderQuantity.Text);
                StockObj.ClinicID = UA.ClinicID.ToString();
                StockObj.CreatedBy = UA.userName;
                //StockObj.Unit = txtUnit.Text;
                StockObj.Unit = ddlUnits.SelectedItem.Text;

                StockObj.InsertMedicines();
                hdnManageGridBind.Value = "True";
            }


        }

        #endregion Add New Medicine

        #region Validate Medicine Name
        [WebMethod]
        public static bool ValidateMedicineName(string MedicineName)
        {
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
            Stocks StockObj = new Stocks();

            if (StockObj.ValidateMedicineCode(MedicineCode))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Medicine Code


        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
                BindUnits();
            }

        }

        #endregion Events

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            AddMedicine();

        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //AddMedicine();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            //Errorbox.Attributes.Add("style", "display:none");
            //ClearControls();
        }

     
       
    }
}