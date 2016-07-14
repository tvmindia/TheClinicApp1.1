

#region CopyRight

//Author      : SHAMILA T P
//Created Date: April-12-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;

using TheClinicApp1._1.ClinicDAL;

#endregion Included Namespaces

namespace TheClinicApp1._1.Stock
{
    public partial class Stock : System.Web.UI.Page
    {

        #region Global Variables

        private static int PageSize = 8;
        Stocks stockObj = new Stocks();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string RoleName = null;

        #endregion Global Variables    

        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            stockObj.ClinicID = UA.ClinicID.ToString();


            List<string> RoleName = new List<string>();
            //DataTable dtRols = new DataTable();

            string Login = UA.userName;
            RoleName = UA.GetRoleName1(Login);
            BindOutOfStock();
            if (!IsPostBack)
            {
                BindDummyRow();

            }
        }

        #endregion Page Load

        #region Logout

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

        #endregion Logout
        #endregion Events

        #region Methods

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

            dummy.Rows.Add();
            gvMedicines.DataSource = dummy;
            gvMedicines.DataBind();
        }

        #endregion Bind Dummy Row

        #region Bind Out Of Stock
        public void BindOutOfStock()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            stockObj.ClinicID = UA.ClinicID.ToString();
            //To Get the Count of Out of Stock Medicines 
            DataSet gds = stockObj.ViewOutofStockMedicines();
            lblReOrderCount.Text = gds.Tables[0].Rows.Count.ToString();

        }

        #endregion Bind Out Of Stock

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

        #endregion Medicine View Search Paging

        #endregion  Methods

    }
}