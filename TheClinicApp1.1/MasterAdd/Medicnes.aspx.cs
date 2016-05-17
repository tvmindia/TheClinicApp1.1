using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

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


        #region  Bind Medicine Gridview

        public void BindGridview()
        {
           DataTable dt = mstrObj.ViewAllMedicines();
           gvMedicines.DataSource = dt;
           gvMedicines.DataBind();

           lblCaseCount.Text = gvMedicines.Rows.Count.ToString();

        }


        #endregion Bind Medicine Gridview




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
            else if (ddlUnits.SelectedItem.Text == "--Select--")
            {
                msg = "Please select a unit ! ";

                eObj.InsertionNotSuccessMessage(page, msg);
            }



            else if ((txtmedicineName.Value.Contains("$")) || (txtmedicineName.Value.Contains("|")))
            {
                msg = "Please enter a valid medicine name";
                eObj.InsertionNotSuccessMessage(page, msg);
            }

            else if (txtOrderQuantity.Value == string.Empty)
            {
                msg = "Please enter reorder quantity";
                eObj.InsertionNotSuccessMessage(page, msg);
            }


            else if (Convert.ToInt32(txtOrderQuantity.Value) <= 0)
            {
                msg = "Please enter a quantity greater than 0";

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



                //hdnManageGridBind.Value = "True";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
            lblUserName.Text = "👤 " + UA.userName + " "; 

            if (!IsPostBack)
            {
                BindCategory();
                BindUnits();
                BindGridview();
            }
        }

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddMedicine();
        }

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

    //        if (ds.Tables[0].Rows.Count > 0)
    //{
    //     isUsed = true;
    //}


           
            if (isUsed == false)
            {
                StockObj.DeleteMedicines(MedId);
            }

            else
            {
                msg = "Already used . Can't be deleted";
                eObj.DeletionNotSuccessMessage(page, msg);
            }
            BindGridview();
        }

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

              //ddlCategory.Items.FindByText(dt.Rows[0]["CategoryName"].ToString()).Selected = true;

              //ddlCategory.SelectedItem.Text = dt.Rows[0]["CategoryName"].ToString();
              //ddlUnits.SelectedItem.Text = dt.Rows[0]["Unit"].ToString();

              ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(dt.Rows[0]["CategoryName"].ToString()));
              ddlUnits.SelectedIndex = ddlUnits.Items.IndexOf(ddlUnits.Items.FindByText(dt.Rows[0]["Unit"].ToString()));
            


            }




            //StockObj.ClinicID = UA.ClinicID.ToString();

            //StockObj.Name = txtmedicineName.Value;
            //StockObj.MedCode = txtCode.Value;
            //StockObj.CategoryID = ddlCategory.SelectedValue;
            //StockObj.ReOrderQty = Convert.ToInt32(txtOrderQuantity.Value);
            //StockObj.ClinicID = UA.ClinicID.ToString();
            //StockObj.UpdatedBy = UA.userName;
            ////StockObj.Unit = txtUnit.Text;
            //StockObj.Unit = ddlUnits.SelectedItem.Text;



            //StockObj.UpdateMedicines(MedId);

            BindGridview();
        }

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        #region Paging
        protected void gvMedicines_PreRender(object sender, EventArgs e)
        {
            gvMedicines.UseAccessibleHeader = false;
            gvMedicines.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        #endregion Paging

    }
}