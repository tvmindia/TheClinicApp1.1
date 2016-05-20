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
    public partial class Categories : System.Web.UI.Page
    {
        #region Global Variables

        Guid usrid = Guid.NewGuid();


        ErrorHandling eObj = new ErrorHandling();
        Category CategoryObj = new Category();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        User usrObj = new User();

        
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

            CategoryObj.CategoryName = txtCategoryName.Value.TrimStart();
            CategoryObj.ClinicID = UA.ClinicID;
            CategoryObj.CreatedBy = UA.userName;

            CategoryObj.usrid = UA.UserID;

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
            //var page = HttpContext.Current.CurrentHandler as Page;
            //UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            //string msg = string.Empty;

            //if (txtCategoryName.Value != string.Empty)
            //{

            //    if (hdnCategoryId.Value != string.Empty)
            //    {
            //        CategoryObj.UpdatedBy = UA.userName;
            //        CategoryObj.CategoryID = Guid.Parse(hdnCategoryId.Value);
            //        CategoryObj.CategoryName = txtCategoryName.Value;
            //        CategoryObj.UpdateCategory();
            //    }
            //    else
            //    {
            //        AddNewCategory();
            //    }
                
            //}

            //else
            //{
            //    //msg = "Please fill out all the fields";
            //    msg = Messages.MandatoryFields;
            //    eObj.InsertionNotSuccessMessage(page, msg);
            //}

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //AddNewCategory();

            var page = HttpContext.Current.CurrentHandler as Page;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            if (txtCategoryName.Value.TrimStart() != string.Empty)
            {

                if (hdnCategoryId.Value != string.Empty)
                {
                    CategoryObj.UpdatedBy = UA.userName;
                    CategoryObj.CategoryID = Guid.Parse(hdnCategoryId.Value);
                    CategoryObj.CategoryName = txtCategoryName.Value.TrimStart();
                    CategoryObj.UpdateCategory();
                }
                else
                {
                    AddNewCategory();
                }

            }

            else
            {
                //msg = "Please fill out all the fields";
                msg = Messages.MandatoryFields;
                eObj.InsertionNotSuccessMessage(page, msg);
            }



            BindGridview();
        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");
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
             //msg = "Already used . Can't be deleted";
             msg = Messages.AlreadyUsedForDeletion;
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

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

            var page = HttpContext.Current.CurrentHandler as Page;

            string msg = string.Empty;
            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid Ctgryid = Guid.Parse(dtgViewAllCategories.DataKeys[row.RowIndex].Value.ToString());

            CategoryObj.CategoryID = Ctgryid;
            hdnCategoryId.Value = Ctgryid.ToString();



            DataTable dt = CategoryObj.ViewCategoryByCategoryID();

            if (dt.Rows.Count > 0)
            {
                txtCategoryName.Value = dt.Rows[0]["Name"].ToString();
            }

        
            //cat
        }
    }
}