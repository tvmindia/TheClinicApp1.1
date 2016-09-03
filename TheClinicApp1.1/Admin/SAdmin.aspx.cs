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
using System.Web.Services;
using System.Web.Script.Serialization;

namespace TheClinicApp1._1.Admin
{
    public partial class SAdmin : System.Web.UI.Page
    {
        UIClasses.Const Const = new UIClasses.Const();
        private static int PageSize = 8;
        ClinicDAL.Master MasterObj = new ClinicDAL.Master();
        ClinicDAL.UserAuthendication UA;
        protected void Page_Load(object sender, EventArgs e)
        {
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            BindDummyRow();
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
                 if (hdnGroupselect.Value == "Update")
                 {
                     MasterObj.ClinicID = Guid.Parse(hdnClinicID.Value);
                     MasterObj.ClinicName = txtClinicName.Value;
                     MasterObj.ClinicLocation = txtLocation.Value;
                     MasterObj.ClinicPhone = txtPhone.Value;
                     MasterObj.ClinicAddress = txtAddress.Value;
                     MasterObj.updatedBy = UA.userName;
                     if (fluplogo.HasFile)
                     {
                         byte[] ImageByteArray = null;
                         ImageByteArray = ConvertImageToByteArray(fluplogo);
                         MasterObj.Logo = ImageByteArray;

                     }
                     if (fluplogosmall.HasFile)
                     {
                         byte[] ImageByteArray = null;
                         ImageByteArray = ConvertImageToByteArray(fluplogosmall);
                         MasterObj.Logosmall = ImageByteArray;

                     }
                     MasterObj.UpdateClinic();

                 }
                 else
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
                     hdnClinicID.Value = MasterObj.ClinicID.ToString();
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
                 


                 

             }
             catch
             {

             }
           

        }

        #region Bind Dummy Row

        /// <summary>
        /// To implement search in gridview(on keypress) :Gridview is converted to table and
        /// Its first row (of table header) is created using this function
        /// </summary>
        private void BindDummyRow()
        {
            DataTable dummy = new DataTable();

            //dummy.Columns.Add("Edit");
            //dummy.Columns.Add(" ");
            dummy.Columns.Add("GroupName");
            dummy.Columns.Add("ClinicName");
            dummy.Columns.Add("Location");
            dummy.Columns.Add("ClinicID");

            dummy.Rows.Add();
            dtgViewAllClinics.DataSource = dummy;
            dtgViewAllClinics.DataBind();
        }

        #endregion Bind Dummy Row
        #region BindUserDetailsOnEditClick
        /// <summary>
        /// To get specific order details by orderid for the editing purpose
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static string BindClinicDetailsOnEditClick(Clinic ClinicObj)
        {

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            //Clinic ClinicObj = new Clinic();
            //userObj.ClinicID = UA.ClinicID;
            DataSet dtClinic = ClinicObj.GetClinicDetailsByClinicID();


            string jsonResult = null;
            DataSet ds = null;
            ds = dtClinic;

            //Converting to Json
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            jsonResult = jsSerializer.Serialize(parentRow);

            return jsonResult; //Converting to Json
        }
        #endregion BindUserDetailsOnEditClick
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
   
     #region User View Search Paging
      
        [WebMethod]
    public static string ViewAndFilterClinic(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
    
            
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
            Clinic ClinicObj = new Clinic();
                //usrObj.ClinicID = UA.ClinicID;
            var xml = ClinicObj.ViewAndFilterClinic(searchTerm, pageIndex, PageSize);
                return xml;
                        

        }
        #endregion User View Search Paging

        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {

        }
}
}