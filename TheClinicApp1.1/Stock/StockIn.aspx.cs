
#region CopyRight

//Author      : Gibin
//Modified By : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private static int PageSize = 8;
        public string listFilter = null;
        public string RoleName = null;
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        ErrorHandling eObj = new ErrorHandling();
        IssueHeaderDetails ihd = new IssueHeaderDetails();
        IssueDetails idt = new IssueDetails();
        Stocks stok = new Stocks();
        Receipt rpt = new Receipt();
       

        #endregion Global Variables

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            bool CanDelete = true;
            string msg = string.Empty;
            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            string receiptID = string.Empty;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            rpt.ClinicID = UA.ClinicID.ToString();
        
            string Login = UA.userName;

            RoleName = UA.GetRoleName1(Login);
            //foreach (DataRow dr in dtRols.Rows)
            //{
            //    RoleName.Add(dr["RoleName"].ToString());
            //}           
            //GridViewStockIN();

            if (!IsPostBack)
            {
                BindDummyRow();
            }

            if (Request.QueryString["HdrID"] != null)
            {
                receiptID = Request.QueryString["HdrID"].ToString();
                rpt.ClinicID = UA.ClinicID.ToString();              

              DataSet  dsReceipthdr = rpt.GetReceiptDetailsByReceiptID(receiptID);
              ihd.ClinicID = UA.ClinicID.ToString();

              for (int i = 0; i < dsReceipthdr.Tables[0].Rows.Count; i++)
              {

                  int Outqty = Convert.ToInt32(dsReceipthdr.Tables[0].Rows[i]["QTY"]);
                  int qtyInStock = Convert.ToInt32(dsReceipthdr.Tables[0].Rows[i]["QtyInStock"]);

                  string MedicineName = dsReceipthdr.Tables[0].Rows[i]["MedicineName"].ToString();
                  string totalIssue = ihd.GetTotalIssuedQtyOfAMedicine(MedicineName);

                  if (totalIssue != string.Empty)
                  {

                      int TotalIssuedQty = Convert.ToInt32(totalIssue);
                      int TotalStock = TotalIssuedQty + qtyInStock;
                      int difference = TotalStock - Outqty;
                      if ((difference == 0) || (difference < TotalIssuedQty))
                      {
                          CanDelete = false;
                          break;
                      }
                  }
              }

              if (CanDelete == false)
              {
                  var page = HttpContext.Current.CurrentHandler as Page;
                  msg = "Already issued.Can't be deleted";
                  eObj.DeletionNotSuccessMessage(page, msg);
              }

              else
              {
                  rpt.DeleteReceiptHeader(receiptID);
              }
            }
        }

        #endregion page Load

        #region New button Click
        protected void btNew_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Stock/StockInDetails.aspx");
        }

        #endregion New button Click

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

        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add(" ");
            dummy.Columns.Add("RefNo1");
            dummy.Columns.Add("RefNo2");
            dummy.Columns.Add("Date");

            dummy.Columns.Add("Details");
            dummy.Columns.Add("ReceiptID");



            dummy.Rows.Add();
            GridViewStockin.DataSource = dummy;
            GridViewStockin.DataBind();
        }

        #endregion Bind Dummy Row

        #region StockIN View Search Paging

        [WebMethod]
        public static string ViewAndFilterReceiptHD(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            Stocks StockObj = new Stocks();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            StockObj.ClinicID = UA.ClinicID.ToString();

            var xml = StockObj.ViewAndFilterReceiptHD(searchTerm, pageIndex, PageSize);

            return xml;
        }

        #endregion StockIN View Search Paging

        #endregion Methods

    }
}