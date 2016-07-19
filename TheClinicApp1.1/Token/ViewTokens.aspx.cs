
#region Namespaces
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using TheClinicApp1._1.ClinicDAL;
#endregion Namespaces

namespace TheClinicApp1._1.Token
{
    public partial class ViewTokens : System.Web.UI.Page
    {

        #region Global Variables

        private static int PageSize = 8;
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
       
        TokensBooking tokenObj = new TokensBooking();
       
        #endregion Global Variables


        #region Methods

        #region ToKen booking View Search Paging
        [WebMethod]
        ///This method is called using AJAX For gridview bind , search , paging
        ///It expects page index and search term which is passed from client side
        ///Page size is declared and initialized in global variable section
        public static string ViewAndFilterPatientBooking(string searchTerm, int pageIndex)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            TokensBooking tokenObj = new TokensBooking();
            tokenObj.DateTime = DateTime.Now;
            tokenObj.ClinicID = UA.ClinicID.ToString();

            var xml = tokenObj.ViewAndFilterPatientBooking(searchTerm, pageIndex, PageSize);

            return xml;
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
            dummy.Columns.Add("DOCNAME");
            dummy.Columns.Add("TokenNo");
            dummy.Columns.Add("Name");
            dummy.Columns.Add("Date");
            dummy.Columns.Add("IsProcessed");

            dummy.Columns.Add("UniqueID");

            dummy.Rows.Add();

            GridViewTokenlist.DataSource = dummy;
            GridViewTokenlist.DataBind();
        }

        #endregion Bind Dummy Row


        #endregion ToKen booking View Search Paging

        #region Delete Token By UniqueID

        [WebMethod]
        public static bool DeleteTokenByUniqueID(string UniqueID)
        {
            bool TokenDeleted = false;
            string result = string.Empty;

            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            TokensBooking tokenObj = new TokensBooking();

           result= tokenObj.DeleteTokenForWM(UniqueID);

           if (result != string.Empty)
           {
               TokenDeleted = true;
           }

            return TokenDeleted;

        }

        #endregion Delete Token By UniqueID

        #endregion Methods


        #region Events

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tokenObj.ClinicID = UA.ClinicID.ToString();

            BindDummyRow();

            //gridviewbind();
        }
        #endregion Page Load

        #endregion Events

        //--NOTE: Below events and functions are not using now

        #region gridviewbind
        public void gridviewbind()
        {
            //Gridview Binding to Diplay DoctorName,Token No,Patient Name,TIME
            tokenObj.DateTime = DateTime.Now;

            DataSet gds = tokenObj.ViewToken();
            GridViewTokenlist.EmptyDataText = "No Records Found";
            GridViewTokenlist.DataSource = gds;
            GridViewTokenlist.DataBind();


            foreach (GridViewRow myRow in GridViewTokenlist.Rows)
            {           
                ImageButton DeleteButton = myRow.Cells[0].Controls[1] as ImageButton;
                string isProcessed = myRow.Cells[5].Text;             

                if (DeleteButton != null && isProcessed == "True")
                {                  
                    DeleteButton.Enabled = false;             
                    DeleteButton.ImageUrl = "~/images/Deleteicon2 (3).png";
                    myRow.BackColor = Color.LightGray;
                }
                else
                {           
                    DeleteButton.Enabled = true;              
                    DeleteButton.ImageUrl = "~/images/Deleteicon1.png";
                }
            }
            GridViewTokenlist.Columns[5].Visible=false;
        }

        #endregion gridviewbind

        #region Delete Image Button Click
        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {

        ImageButton ib = sender as ImageButton;
        GridViewRow row = ib.NamingContainer as GridViewRow;
        string  id=GridViewTokenlist.DataKeys[row.RowIndex].Value.ToString();


           
            tokenObj.DeleteToken(id);
          //  gridviewbind();
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptid", "window.parent.location.href='Tokens.aspx?id=true';", true);




        }

        #endregion Delete Image Button Click
    }
}