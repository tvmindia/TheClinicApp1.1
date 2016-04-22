using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.Stock
{
    public partial class OutOfStock : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Stocks stockObj = new Stocks();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOutOfStockGridview();
            }
        }

        #region Bind Out Of Stock Gridview
        public void BindOutOfStockGridview()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            stockObj.ClinicID = UA.ClinicID.ToString();
            //gridview binding for listing the Out of Stock Medicines 
            DataSet gds = stockObj.ViewOutofStockMedicines();
            gvOutOfStock1.EmptyDataText = "No Records Found";
            gvOutOfStock1.DataSource = gds;
            gvOutOfStock1.DataBind();
        }

        #endregion Bind Out Of Stock Gridview
    }
}