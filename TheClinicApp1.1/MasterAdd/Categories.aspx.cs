using System;
using System.Collections.Generic;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            lblClinicName.Text = UA.Clinic;
        }

        protected void Save_ServerClick(object sender, EventArgs e)
        {
            AddNewCategory();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            AddNewCategory();
        }
    }
}