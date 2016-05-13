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
    public partial class Categories : System.Web.UI.Page
    {
        #region Global Variables

        ErrorHandling eObj = new ErrorHandling();
        Category CategoryObj = new Category();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;

        #endregion Global Variables

        #region Validate Category Name

        [WebMethod]
        public static bool ValidateCategoryName(string CategoryName)
        
        {
            Category CategoryObj = new Category();

            if (CategoryObj.ValidateCategoryName(CategoryName))
            {
                return true;
            }
            return false;
        }

        #endregion  Validate Category Name


        #region Add New Category
        public void AddNewCategory()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            CategoryObj.CategoryName = txtCategoryName.Value;
            CategoryObj.ClinicID = UA.ClinicID;
            CategoryObj.CreatedBy = UA.userName;

            CategoryObj.AddNewCategory();

        }

        #endregion Add New Category


        #region Bind Category Gridview

        public void BindGridview()
        { 
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            CategoryObj.ClinicID = UA.ClinicID;
           DataTable dt = CategoryObj.ViewAllCategory();

           dtgViewAllCategories.DataSource = dt;
           dtgViewAllCategories.DataBind();

           lblCaseCount.Text = dtgViewAllCategories.Rows.Count.ToString();

        }

        #endregion Bind Category Gridview


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

        protected void Save_ServerClick(object sender, EventArgs e)
        {
            AddNewCategory();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddNewCategory();

            BindGridview();
        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;
            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid Ctgryid = Guid.Parse(dtgViewAllCategories.DataKeys[row.RowIndex].Value.ToString());

            CategoryObj.CategoryID = Ctgryid;
         DataTable dtCtgry =    CategoryObj.ViewMedicinesByCategoryID();

         if (dtCtgry.Rows.Count == 0)
         {
             CategoryObj.CategoryID = Ctgryid;
             CategoryObj.DeleteCategoryById();
         } 

         else
         {
             msg = "Already used . Can't be deleted";
             eObj.DeletionNotSuccessMessage(page, msg);
         }

         BindGridview();


        }

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

        #region Paging
        protected void dtgViewAllCategories_PreRender(object sender, EventArgs e)
        {
            dtgViewAllCategories.UseAccessibleHeader = false;
            dtgViewAllCategories.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion Paging
    }
}