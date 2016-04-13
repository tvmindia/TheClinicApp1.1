
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

        #endregion Global Variables

        #region Methods

        #region Bind Category
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
        #endregion Bind Category

        #region Add New Medicine
        public void AddMedicine()
        {
            if (txtmedicineName.Text.Contains("$") || txtmedicineName.Text.Contains("|"))
            {
                //Medicine Name is not valid as it contains $ ,|
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
                StockObj.Unit = txtUnit.Text;

                StockObj.InsertMedicines();
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

        #endregion Methods

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
            }

        }

        #endregion Events

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            AddMedicine();

        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {

        }
    }
}