
#region CopyRight

//Author      :SHAMILA T P

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
using System.Configuration;
using System.Web.Services;
using TheClinicApp1._1.ClinicDAL;

#endregion Included Namespaces

namespace TheClinicApp1._1.Stock
{
    public partial class StockOut : System.Web.UI.Page
    {
        #region Global variables

        IssueHeaderDetails IssuehdrObj = new IssueHeaderDetails();
        Stocks stok = new Stocks();
        private static int PageSize = 8;
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();
        public string RoleName = null;

        #endregion Global variables

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            //DataTable dtRols = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string issueID = string.Empty;

            string Login = UA.userName;


            RoleName = UA.GetRoleName1(Login);


            if (!IsPostBack)
            {
                BindDummyRow();

            }
         }

        #endregion Page Load

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

        #region Methods

        #region Bind Dummy Row


        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            dummy.Columns.Add(" ");
            dummy.Columns.Add("IssueNO");
            dummy.Columns.Add("IssuedTo");
            //dummy.Columns.Add("IssueID");
            dummy.Columns.Add("Date");
            dummy.Columns.Add("PrescID");
            dummy.Columns.Add("Details");
            dummy.Columns.Add("IssueID");
            dummy.Rows.Add();
            gvIssueHD.DataSource = dummy;
            //gvIssueHD.Columns[2].Visible = false;
            gvIssueHD.DataBind();

        }

        #endregion Bind Dummy Row

        #region StockOUT View Search Paging

        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterIssueHD(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            common cmn = new common();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            Stocks StockObj = new Stocks();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            StockObj.ClinicID = UA.ClinicID.ToString();

            var xml = StockObj.ViewAndFilterIssueHD(searchTerm, pageIndex, PageSize);

            return xml;
        }

        #endregion StockOUT View Search Paging

        #region Delete Issue Header

        /// <summary>
        /// To delete issue header by IssueID
        /// </summary>

        [WebMethod]
        public static bool DeleteIssueheader(string issueID)
        {
            IssueHeaderDetails IssuehdrObj = new IssueHeaderDetails();
            bool IsDeleted = false;

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];    

            IssuehdrObj.ClinicID = UA.ClinicID.ToString();
            int OutputVal = IssuehdrObj.DeleteIssueHeader(issueID);

            if (OutputVal ==1)
            {
                IsDeleted = true; 
            }


            return IsDeleted;
        }

        #endregion Delete Issue Header

        #endregion Methods

    }
}