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
    public partial class StockOutDetails : System.Web.UI.Page
    {
        #region Global Variables

        Stocks StockObj = new Stocks();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        #endregion Global Variables

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {

        }
    }
}