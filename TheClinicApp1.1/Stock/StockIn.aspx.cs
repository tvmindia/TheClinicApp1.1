
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

            string receiptID = string.Empty;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            rpt.ClinicID = UA.ClinicID.ToString();
            lblClinicName.Text = UA.Clinic;
            string Login = UA.userName;
            RoleName = UA.GetRoleName(Login);
            if (RoleName == Const.RoleAdministrator)
            {
                //this.hide.style.Add("display", "none");
                this.admin.Style.Add("Visibility", "Visible");
            }

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
                  string totalIssue = ihd.GetTotalQtyOfAMedicine(MedicineName);

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

        #region Get Medicines

        [WebMethod]
        public static string GetMedicines(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            string query = "ViewAndFilterReceiptHD";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;
            //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = new Guid("2c7a7172-6ea9-4640-b7d2-0c329336f289");

            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

            var xml = GetData(cmd, pageIndex).GetXml();
            return xml;
        }

        private static DataSet GetData(SqlCommand cmd, int pageIndex)
        {

            string strConnString = ConfigurationManager.ConnectionStrings["ClinicAppConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds, "Medicines");
                        DataTable dt = new DataTable("Pager");
                        dt.Columns.Add("PageIndex");
                        dt.Columns.Add("PageSize");
                        dt.Columns.Add("RecordCount");
                        dt.Rows.Add();
                        dt.Rows[0]["PageIndex"] = pageIndex;
                        dt.Rows[0]["PageSize"] = PageSize;
                        dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                        ds.Tables.Add(dt);
                        return ds;
                    }
                }
            }
        }

        #endregion Get Medicines

        #endregion Methods

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