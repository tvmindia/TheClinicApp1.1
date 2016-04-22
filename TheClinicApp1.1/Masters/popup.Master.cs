using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Masters
{

    public partial class popup : System.Web.UI.MasterPage
    {
        ClinicDAL.UserAuthendication UA;
        UIClasses.Const Const = new UIClasses.Const();

        protected void Page_Init(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            if (UA == null)
            {
                Response.Redirect("../Login/Login.aspx");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}