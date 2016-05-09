using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

namespace TheClinicApp1._1.MasterAdd
{
    public partial class AddDoctor : System.Web.UI.Page
    {

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Master mstrObj = new Master();
        ErrorHandling eObj = new ErrorHandling();


        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;



            if (txtName.Value != string.Empty || txtEmail.Value != string.Empty || txtPhoneNumber.Value != string.Empty)
            {
                mstrObj.ClinicID = UA.ClinicID;
                mstrObj.DoctorName = txtName.Value;
                mstrObj.DoctorEmail = txtEmail.Value;
                mstrObj.DoctorPhone = txtPhoneNumber.Value;
                mstrObj.createdBy = UA.userName;

                mstrObj.InsertDoctors();

            }

            else
            {
                msg = "Please enter a valid medicine name";
                eObj.InsertionNotSuccessMessage(page, msg);
            }



        }
    }
}