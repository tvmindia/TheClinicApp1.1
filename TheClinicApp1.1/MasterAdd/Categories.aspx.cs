
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
    public partial class Categories : System.Web.UI.Page
    {

        #region Global Variables

        public string RoleName = null;

        ErrorHandling eObj = new ErrorHandling();
        Category CategoryObj = new Category();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        User usrObj = new User();

        #endregion Global Variables

        #region Methods

        #region Validate Category Name

        [WebMethod]
        public static bool ValidateCategoryName(string CategoryName)
        
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Category CategoryObj = new Category();

            CategoryObj.ClinicID = UA.ClinicID;

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
            CategoryObj.CategoryName = txtCategoryName.Value.TrimStart();
           
            CategoryObj.CreatedBy = UA.userName;

            CategoryObj.AddNewCategory();

        }

        #endregion Add New Category

        #region Bind Category Gridview

        public void BindGridview()
        {
           DataTable dt = CategoryObj.ViewAllCategory();

           if (dt != null)
           {
               dtgViewAllCategories.DataSource = dt;
               dtgViewAllCategories.DataBind();

               lblCaseCount.Text = dtgViewAllCategories.Rows.Count.ToString();

           }

        }

        #endregion Bind Category Gridview

        #endregion Methods

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            CategoryObj.ClinicID = UA.ClinicID;

            if (!IsPostBack)
            {
                BindGridview();
            }

        }

        #endregion Page Load

        #region Save Button Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //AddNewCategory();

            var page = HttpContext.Current.CurrentHandler as Page;
         
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

        #endregion Save Button Click

        #region Delete Image Button Click
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

        #endregion Delete Image Button Click

        #region Logout Click

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

        #endregion Logout Click

        #region Paging
        protected void dtgViewAllCategories_PreRender(object sender, EventArgs e)
        {
            dtgViewAllCategories.UseAccessibleHeader = false;

            if (dtgViewAllCategories.Rows.Count > 0)
            {
                dtgViewAllCategories.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }

        #endregion Paging

        #region Update Image Button Click

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

        #endregion Update Image Button Click

        #endregion Events

       


    }
}