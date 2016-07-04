
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

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
       
        TokensBooking tokenObj = new TokensBooking();
       
        #endregion Global Variables


        protected void Page_Load(object sender, EventArgs e)
        {
          
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            tokenObj.ClinicID = UA.ClinicID.ToString();
            gridviewbind();
        }




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






        protected void ImgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {

        ImageButton ib = sender as ImageButton;
        GridViewRow row = ib.NamingContainer as GridViewRow;
        string  id=GridViewTokenlist.DataKeys[row.RowIndex].Value.ToString();


           
            tokenObj.DeleteToken(id);
          //  gridviewbind();
           
            ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptid", "window.parent.location.href='Tokens.aspx?id=true';", true); 
        



        }


    }
}