﻿
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
              else if ( (txtmedicineName.Text.Contains("$")) || (txtmedicineName.Text.Contains("|")) )
              {
                  msg = "Please enter a valid medicine name";
                  eObj.InsertionNotSuccessMessage(page, msg);
              }

              else if (txtOrderQuantity.Text == string.Empty)
              {
                   msg = "Please enter reorder quantity";
                  eObj.InsertionNotSuccessMessage(page, msg);
              }

              else if(Convert.ToInt32(txtOrderQuantity.Text) <= 0)
              {
                msg = "Please enter a quantity greater than 0";

                eObj.InsertionNotSuccessMessage(page, msg);
              }          
            else
            {
                StockObj.Name = txtmedicineName.Text;
                StockObj.MedCode = txtCode.Text;
                StockObj.CategoryID = ddlCategory.SelectedValue;
                StockObj.ReOrderQty = Convert.ToInt32(txtOrderQuantity.Text);              
                StockObj.CreatedBy = UA.userName;             
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
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];    
            Stocks StockObj = new Stocks();
         
            StockObj.ClinicID = UA.ClinicID.ToString();
          

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
            Stocks StockObj = new Stocks();

            StockObj.ClinicID = UA.ClinicID.ToString();
         
           

            if (StockObj.ValidateMedicineCode(MedicineCode))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Medicine Code


        #endregion Methods

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            StockObj.ClinicID = UA.ClinicID.ToString();
            StockObj.CreatedBy = UA.userName;
   
            if (!IsPostBack)
            {
                BindCategory();
                BindUnits();
            }

        }
        #endregion Page Load

        #region Save Button Click
        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            AddMedicine();
        }
        #endregion Save Button Click

        #region New Button Click
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");
            ClearControls();
        }

        #endregion New Button Click

        #endregion Events

    }
}