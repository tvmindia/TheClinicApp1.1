﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheClinicApp1._1.Registration
{
    public partial class ViewTodaysRegistration : System.Web.UI.Page
    {
        ClinicDAL.Patient PatientObj = new ClinicDAL.Patient();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region GridDateRegistration
            dtgViewTodaysRegistration.EmptyDataText = "....Till Now No Registration....";
            dtgViewTodaysRegistration.DataSource = PatientObj.GetDateRegistration();
            dtgViewTodaysRegistration.DataBind();
            #endregion GridDateRegistration
        }
        #region EditPatients
        protected void ImgBtnUpdate_Command(object sender, CommandEventArgs e)
        {
            //DateTime date = DateTime.Now;
            //int year = date.Year;
            //string[] Patient = e.CommandArgument.ToString().Split(new char[] { '|' });
            //Guid PatientID = Guid.Parse(Patient[0]);
            //txtName.Value = Patient[1];
            //if (Patient[6].Trim() == "Male")
            //{
            //    rdoFemale.Checked = false;
            //    rdoMale.Checked = true;
            //}
            //else if (Patient[6].Trim() == "Female")
            //{
            //    rdoMale.Checked = false;
            //    rdoFemale.Checked = true;
            //}

            //DateTime dt = Convert.ToDateTime(Patient[5]);
            //int Age = year - dt.Year;
            //txtAge.Value = Age.ToString();
            //txtAddress.Value = Patient[2];
            //txtMobile.Value = Patient[3];
            //txtEmail.Value = Patient[4];
            //ddlMarital.SelectedValue = Patient[7];

            //ProfilePic.Src = "../Handler/ImageHandler.ashx?PatientID=" + PatientID.ToString();
            //ProfilePic.Visible = true;
            ////btnnew.Visible = true;
            //HiddenField1.Value = PatientID.ToString();

        }
        #endregion EditPatients

        #region GridDelete
        protected void ImgBtnDelete_Command(object sender, CommandEventArgs e)
        {

            Guid PatientID = Guid.Parse(e.CommandArgument.ToString());
            PatientObj.PatientID = PatientID;
            PatientObj.DeletePatientDetails();

        }


        #endregion GridDelete
    }
}