﻿
#region Included Namespcaes

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

#endregion Included Namespcaes

namespace TheClinicApp1._1.MasterAdd
{
    public partial class AddDoctor : System.Web.UI.Page
    {
        #region Global Variables

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        User userObj = new User();
        Master mstrObj = new Master();
        ErrorHandling eObj = new ErrorHandling();
        ClinicDAL.CryptographyFunctions CryptObj = new CryptographyFunctions();

        #endregion Global Variables

        #region Methods

        //------------ * General Methods * -----------//

        #region General Methods

        #region Bind Gridview

        public void BindGridview()
        {
            DataTable dtDoctors = mstrObj.ViewDoctors();
            dtgDoctors.DataSource = dtDoctors;
            dtgDoctors.DataBind();

            lblCaseCount.Text = dtgDoctors.Rows.Count.ToString();
        }

        #endregion Bind Gridview

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

        #region Paging
        protected void dtgDoctors_PreRender(object sender, EventArgs e)
        {

            dtgDoctors.UseAccessibleHeader = false;
            dtgDoctors.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        #endregion Paging

        #endregion General Methods

        //---------- *  USER *------------//

        #region User

        #region Set Default Password

        public string SetDefaultPassword(string DoctorName) //PATTERN : if dr name has more than 3 characters , first 3 characters of name + 123 , otherwise(name less than 3 character , name + 123)
        {
            string Password = string.Empty;
            
            if (DoctorName != string.Empty)
            {
                if (DoctorName.Length >= 3)
                {
                    DoctorName = DoctorName.Substring(0, 3);

                }

                Password = DoctorName + "123";
               
            }


            return Password;
            
        }

        #endregion Set Default Password

        #region Add User To User Table
        public void AddUserToUserTable()
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];


            userObj.firstName = txtName.Value;
            userObj.loginName = txtName.Value;
            userObj.lastName = string.Empty;
            userObj.isActive = true;
            userObj.ClinicID = UA.ClinicID;
            userObj.createdBy = UA.userName;
            userObj.updatedBy = UA.userName;

            userObj.Email = txtEmail.Value;
            userObj.PhoneNo = txtPhoneNumber.Value;


            string password = SetDefaultPassword(txtName.Value);

            userObj.passWord = CryptObj.Encrypt(password);

            if (hdnUserID.Value == string.Empty)
            {
                //INSERT user

                userObj.AddUser();
                hdnUserID.Value = userObj.UserID.ToString();
            }
            else
            {
                //UPDATE user
                userObj.UserID = Guid.Parse(hdnUserID.Value);
                userObj.UpdateuserByUserID();
            }


        }

        #endregion Add User To User Table

        #endregion User

        //---------- *  DOCTOR *------------//

        #region Doctor

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

        #region Add Doctor

        public void AddDoctorToDoctorTable()
        {
            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorName = txtName.Value;
            mstrObj.DoctorEmail = txtEmail.Value;
            mstrObj.DoctorPhone = txtPhoneNumber.Value;



            if ( hdnDrID.Value == string.Empty)
            {
                //INSERT Doctor
                 
                mstrObj.createdBy = UA.userName;

                if (hdnUserID.Value != string.Empty)
                {
                    mstrObj.UsrID = Guid.Parse(hdnUserID.Value);
                }

                mstrObj.InsertDoctors();

            }



            else
            {
                //UPDATE Doctor
                mstrObj.DoctorID = Guid.Parse(hdnDrID.Value);

                mstrObj.updatedBy = UA.userName;
                mstrObj.UpdateDoctors();
                //}


            }
        }

        #endregion  Add Doctor

        #endregion Doctror

        #endregion Methods

        #region Events

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
            }
        }

        #endregion  Page Load

        #region Save Button Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;

            if (txtName.Value != string.Empty || txtEmail.Value != string.Empty || txtPhoneNumber.Value != string.Empty)
            {
                AddUserToUserTable();
                AddDoctorToDoctorTable();

            }

            else
            {
                msg = "Please fill out all the fields";
                eObj.InsertionNotSuccessMessage(page, msg);
            }

            BindGridview();

        }

        #endregion Save Button Click

        #region Delete Image Buttn Click

        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");

             UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;

            var page = HttpContext.Current.CurrentHandler as Page;


             ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());


            mstrObj.ClinicID = UA.ClinicID;
            mstrObj.DoctorID = DoctorID;

            bool IDUsedOrNot = mstrObj.CheckDoctorIdUsed();

            //bool IDUsedOrNot = mstrObj.GetDoctorIDInVisits() | mstrObj.GetDoctorIDInTokens() | mstrObj.GetDoctorIDInPrescHD();

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

        #endregion Delete Image Buttn Click

        #region Update Image Button Click
        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            Errorbox.Attributes.Add("style", "display:none");


            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            string msg = string.Empty;
            var page = HttpContext.Current.CurrentHandler as Page;

            ImageButton ib = sender as ImageButton;
            GridViewRow row = ib.NamingContainer as GridViewRow;
            //Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Value.ToString());



            Guid DoctorID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[0].ToString());
            Guid UserID = Guid.Parse(dtgDoctors.DataKeys[row.RowIndex].Values[1].ToString());

            hdnUserID.Value = UserID.ToString();

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

        #endregion Update Image Button Click

        #endregion Events

    }
}