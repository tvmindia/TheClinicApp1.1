

#region CopyRight

//Author      : SHAMILA T P
//Created Date: April-12-2016

#endregion CopyRight

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

        #region Bind Dummy Row

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


        protected void Page_Load(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            stockObj.ClinicID = UA.ClinicID.ToString();
        

            List<string> RoleName = new List<string>();
            //DataTable dtRols = new DataTable();
                   
            string Login = UA.userName;
            RoleName = UA.GetRoleName1(Login);        
           
            if (!IsPostBack)
            {
                BindDummyRow();
                //BindOutOfStockGridview();
            }
        }

        //#region Bind Out Of Stock Gridview
        //public void BindOutOfStockGridview()
        //{
        //    UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
        //    stockObj.ClinicID = UA.ClinicID.ToString();
        //    //gridview binding for listing the Out of Stock Medicines 
        //    DataSet gds = stockObj.ViewOutofStockMedicines();
        //    gvOutOfStock1.EmptyDataText = "No Records Found";
        //    gvOutOfStock1.DataSource = gds;
        //    gvOutOfStock1.DataBind();
        //}

        //#endregion Bind Out Of Stock Gridview


        #region webmethod
        [WebMethod]
        public static string GetMedicines(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Stocks StockObj = new Stocks();

            StockObj.ClinicID = UA.ClinicID.ToString();
            
            string query = "ViewAndFilterMedicine";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = UA.ClinicID;
           

            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PageSize", PageSize);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

            var xml = GetData(cmd, pageIndex).GetXml();
            return xml;
        }

        #endregion webmethod

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

    }
}