using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

using TheClinicApp1._1.ClinicDAL;
using System.Web.Services;

namespace TheClinicApp1._1.MasterAdd
{
    public partial class AddDoctor : System.Web.UI.Page
    {

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        Master mstrObj = new Master();
        ErrorHandling eObj = new ErrorHandling();


        public void BindGridview()
        {
       DataTable dtDoctors =     mstrObj.ViewDoctors();
       dtgDoctors.DataSource = dtDoctors;
       dtgDoctors.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
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


        #region Validate Doctor Name

        [WebMethod]
        public static bool ValidateDoctorName(string DoctorName)
        {
            Master mstrObj = new Master();

            mstrObj.DoctorName = DoctorName;
            if (mstrObj.CheckDoctorNameDuplication())
            {
                return true;
            }
            return false;
        }

        #endregion Validate Doctor Name




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

                if (hdnDrID.Value == string.Empty)
                {
                    mstrObj.createdBy = UA.userName;
                    mstrObj.InsertDoctors();
                }

                else
                {
                    mstrObj.DoctorID = Guid.Parse(hdnDrID.Value);

                    //bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();


                    //if (IDUsedOrNot)
                    //{
                    //    msg = "Already used . Can't be deleted";
                    //    eObj.DeletionNotSuccessMessage(page, msg);
                    //}

                    //else
                    //{
                        mstrObj.updatedBy = UA.userName;
                        mstrObj.UpdateDoctors();
                    //}

                   
                }
               
                BindGridview();

            }

            else
            {
                msg = "Please fill out all the fields";
                eObj.InsertionNotSuccessMessage(page, msg);
            }



        }

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
             UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;




             ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());


            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorID = DoctorID;
            //bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();

            bool IDUsedOrNot = false;
            if (IDUsedOrNot)
            {
                msg = "Already used . Can't be deleted";
                eObj.DeletionNotSuccessMessage(page, msg); 
            }

         else
            {
                mstrObj.DoctorID = DoctorID;
             mstrObj.DeleteDoctorByID();

             BindGridview();
         }


        }

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());

            mstrObj.DoctorID = DoctorID;
            hdnDrID.Value = DoctorID.ToString();

            mstrObj.ClinicID = UA.ClinicID;
    DataTable dtDrByID =  mstrObj.GetDoctorDetailsByID();

    if (dtDrByID.Rows.Count > 0)
    {
        txtName.Value = dtDrByID.Rows[0]["Name"].ToString();
        txtPhoneNumber.Value = dtDrByID.Rows[0]["Phone"].ToString();
        txtEmail.Value = dtDrByID.Rows[0]["Email"].ToString();

    }

        }

        #region Paging
        protected void dtgDoctors_PreRender(object sender, EventArgs e)
        {

            dtgDoctors.UseAccessibleHeader = false;
            dtgDoctors.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion Paging
    }
}