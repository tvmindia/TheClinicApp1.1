
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
    public partial class OutOfStock : System.Web.UI.Page
    {
        #region Global Variables

        private static int PageSize = 8;
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Stocks stockObj = new Stocks();

        #endregion Global Variables

        #region Events

        #endregion Events

        #region Methods

        #endregion Methods


        #region Bind Dummy Row

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();
            dummy.Columns.Add("MedicineName");
          dummy.Columns.Add("CategoryName");
            dummy.Columns.Add("Unit");
            dummy.Columns.Add("Qty");
            dummy.Columns.Add("ReOrderQty");


            dummy.Rows.Add();
            gvOutOfStock1.DataSource = dummy;
            gvOutOfStock1.DataBind();
        }

        #endregion Bind Dummy Row

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           
            stockObj.ClinicID = UA.ClinicID.ToString();
          

            if (!IsPostBack)
            {
                BindDummyRow();

                //BindOutOfStockGridview();
            }
        }
        #endregion Page Load

        #region Bind Out Of Stock Gridview
        //public void BindOutOfStockGridview()
        //{
        //    //gridview binding for listing the Out of Stock Medicines 
        //    DataSet gds = stockObj.ViewOutofStockMedicines();
        //    gvOutOfStock1.EmptyDataText = "No Records Found";
        //    gvOutOfStock1.DataSource = gds;
        //    gvOutOfStock1.DataBind();
        //}

        #endregion Bind Out Of Stock Gridview

        #region OutOfStock View Search Paging

        [WebMethod]

        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterOutOfStock(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            Stocks StockObj = new Stocks();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            StockObj.ClinicID = UA.ClinicID.ToString();

            var xml = StockObj.ViewAndFilterOutOfStock(searchTerm, pageIndex, PageSize);
            return xml;

        }

        #endregion OutOfStock View Search Paging

    }
}