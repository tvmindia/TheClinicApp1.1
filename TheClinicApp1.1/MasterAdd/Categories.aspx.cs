
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
    public partial class Categories : System.Web.UI.Page
    {

        #region Global Variables

        private static int PageSize = 8;
        public string RoleName = null;

        ErrorHandling eObj = new ErrorHandling();
        Category CategoryObj = new Category();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        User usrObj = new User();

        #endregion Global Variables

        #region Methods

        #region Categories View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterCategories(string searchTerm, int pageIndex)
        {
            Category CategoryObj = new Category();

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            CategoryObj.ClinicID = UA.ClinicID;
            var xml = CategoryObj.ViewAndFilterCategories(searchTerm, pageIndex, PageSize);
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

            //dummy.Columns.Add("Edit");
            //dummy.Columns.Add(" ");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("CategoryID");


            dummy.Rows.Add();

            dtgViewAllCategories.DataSource = dummy;
            dtgViewAllCategories.DataBind();
        }

        #endregion Bind Dummy Row


        #endregion Categories View Search Paging

        #region Delete Category By ID

        [WebMethod]
        public static bool DeleteCategoryByID(string CategoryID)
        {
            bool CtgryDeleted = false;

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Category CategoryObj = new Category();

            CategoryObj.CategoryID = Guid.Parse(CategoryID);
            DataTable dtCtgry = CategoryObj.ViewMedicinesByCategoryID();

            if (dtCtgry.Rows.Count == 0)
            {
                CategoryObj.CategoryID = Guid.Parse(CategoryID);
          string result =      CategoryObj.DeleteCategoryByIdForWM();

          if (result != string.Empty)
          {
              CtgryDeleted = true;
          }
            }

            return CtgryDeleted;

        }

        #endregion Delete Category By ID

        #region Bind Category Details On Edit Click
        /// <summary>
        /// To get specific order details by orderid for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindCategoryDetailsOnEditClick(Category CategoryObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

           CategoryObj.ClinicID = UA.ClinicID;
           DataSet dtuser = CategoryObj.ViewCategoryByCategoryIDForWM();


            string jsonResult = null;
            DataSet ds = null;
            ds = dtuser;

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
        #endregion Bind Category Details On Edit Click

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

        #endregion Methods

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            CategoryObj.ClinicID = UA.ClinicID;

            if (!IsPostBack)
            {
                BindDummyRow();

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

        }

        #endregion Save Button Click

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

        #endregion Events

//--NOT USING : Below events and functions are not using now

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
            DataTable dtCtgry = CategoryObj.ViewMedicinesByCategoryID();

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
    }
}