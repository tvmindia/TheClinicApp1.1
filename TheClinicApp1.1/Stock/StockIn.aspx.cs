#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion  Included Namespaces


namespace TheClinicApp1._1.Stock
{
    public partial class StockIn : System.Web.UI.Page
    {


        #region Global Variables

        public string listFilter = null;

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        ErrorHandling eObj = new ErrorHandling();
        IssueHeaderDetails ihd = new IssueHeaderDetails();
        IssueDetails idt = new IssueDetails();
        Stocks stok = new Stocks();
        Receipt rpt = new Receipt();


        #endregion Global Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            rpt.ClinicID = UA.ClinicID.ToString();

            GridViewStockIN();
        }

        public void GridViewStockIN()
        {

            //gridview binding for listing the Out of Stock Medicines
            string str = "";
            DataSet gds = rpt.ViewReceiptHeader(str);
            GridViewStockin.EmptyDataText = "No Records Found";
            GridViewStockin.DataSource = gds;
            GridViewStockin.DataBind();
            GridViewStockin.Columns[0].Visible = false;


        }

        protected void btSave_ServerClick(object sender, EventArgs e)
        {

        }


        protected void btNew_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Stock/StockInDetails.aspx");
        }
    }
}