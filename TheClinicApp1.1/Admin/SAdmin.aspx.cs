using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Messages = TheClinicApp1._1.UIClasses.Messages;
using TheClinicApp1._1.ClinicDAL;
using System.Data;
using System.IO;

namespace TheClinicApp1._1.Admin
{
    public partial class SAdmin : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.Master MasterObj = new ClinicDAL.Master();
        ClinicDAL.UserAuthendication UA;
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
           if(!IsPostBack)
           {
               BindDropDownGroupforDoc();
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

        protected void btnSave_Click(object sender, EventArgs e)
         {
            //string message = "";
             try
             {
                 if (hdnGroupselect.Value == "New")
                 {
                     MasterObj.GroupName = txtGroupName.Value;
                     if (fluplogo.HasFile)
                     {
                         byte[] ImageByteArray = null;
                         ImageByteArray = ConvertImageToByteArray(fluplogo);
                         MasterObj.Logo = ImageByteArray;

                     }

                     MasterObj.Createdby = UA.userName;
                     MasterObj.InsertGroups();
                 }
                 if (hdnGroupselect.Value == "Exist")
                 {
                     MasterObj.GroupID = Guid.Parse(ddlGroup.SelectedValue);
                 }

                 MasterObj.ClinicName = txtClinicName.Value;
                 MasterObj.ClinicAddress = txtAddress.Value;
                 MasterObj.ClinicLocation = txtLocation.Value;
                 MasterObj.ClinicPhone = txtPhone.Value;
                 MasterObj.Createdby = UA.userName;
                 if (fluplogo.HasFile)
                 {
                     byte[] ImageByteArray = null;
                     ImageByteArray = ConvertImageToByteArray(fluplogo);
                     MasterObj.Logo = ImageByteArray;

                 }
                 MasterObj.InsertClinics();
                 foreach (ListItem item in lstFruits.Items)
                 {
                     if (item.Selected)
                     {
                         //message += item.Text;
                         MasterObj.RoleName = item.Text;
                         MasterObj.createdBy = UA.userName;
                         MasterObj.InsertRole();
                     }
                 }


                 BindDropDownGroupforDoc();

             }
             catch
             {

             }
           

        }

        

        private byte[] ConvertImageToByteArray(FileUpload fuImage)
        {
            byte[] ImageByteArray;
            try
            {
                MemoryStream ms = new MemoryStream(fuImage.FileBytes);
                ImageByteArray = ms.ToArray();
                return ImageByteArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void BindDropDownGroupforDoc()
        {

            DataTable dt = new DataTable();
            dt = MasterObj.BindGroupName();
            ddlGroup.DataSource = dt;
            ddlGroup.DataTextField = "Name";
            ddlGroup.DataValueField= "GroupID";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("--Select Group--", "-1"));
        } 
    }
}