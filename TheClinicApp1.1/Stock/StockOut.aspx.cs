using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;

namespace TheClinicApp1._1.Stock
{
    public partial class StockOut : System.Web.UI.Page
    {
        private static int PageSize = 5;
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();


        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add("IssueNO");
            dummy.Columns.Add("IssuedTo");
            //dummy.Columns.Add("IssueID");

            dummy.Columns.Add("Date");
            dummy.Columns.Add("Details");
            dummy.Columns.Add("IssueID");

            dummy.Rows.Add();

            gvIssueHD.DataSource = dummy;
            //gvIssueHD.Columns[2].Visible = false;
            gvIssueHD.DataBind();



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDummyRow();

            }
        }

        [WebMethod]
        public static string GetIssueHD(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            string query = "ViewAndFilterIssueHD";
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
                        sda.Fill(ds, "IssueHD");

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



        protected void btSave_ServerClick(object sender, EventArgs e)
        {
             
        }
    }
}