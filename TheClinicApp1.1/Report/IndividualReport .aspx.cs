
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion Included Namespaces

namespace TheClinicApp1._1.Report
{
    public partial class IndividualReport : System.Web.UI.Page
    {
        #region Global Variables

        Reports clinicReprtObj = new Reports();
        HTMLReports htmlRprtObj = new HTMLReports();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;


        #endregion Global Variables

        public void BindReport()
        {
           
            

                if (Request.QueryString["ReportName"] != null)
                {
                    Page.Title = Request.QueryString["ReportName"].ToString();

                    //  hdfReportName.Value = Request.QueryString["ReportName"].ToString();
                }


                string ID = string.Empty;
                string Date = string.Empty;
                if (Request.QueryString["ID"] != null)
                {
                    ID = Request.QueryString["ID"].ToString();
                }

                //if (Request.QueryString["Date"] != null && Request.QueryString["Date"].ToString() !=string.Empty)
                //{
                //    Date = Request.QueryString["Date"].ToString();
                //}

                UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];


                //if (!IsPostBack)
                //{
                if (ID != string.Empty)
                {
                    clinicReprtObj.ReportID = Guid.Parse(ID);

                    

                    //if (txtFromDate.Value != string.Empty)
                    //{
                    //    clinicReprtObj.FromDate = Convert.ToDateTime(txtFromDate.Value);
                    //}

                    //if (txtToDate.Value != string.Empty)
                    //{
                    //    clinicReprtObj.ToDate = Convert.ToDateTime(txtToDate.Value);
                    //}
                    DataTable dt = clinicReprtObj.GetDataToBeReported();
                    string SpecifiedColumns = clinicReprtObj.SpecifiedColumns;

                    if (!IsPostBack)
                    {
                        BindCoulmnNameDropdown();
                    }

                    string html = clinicReprtObj.GetReport(dt);                               //body
                    if (html != string.Empty)
                    {
                        PlaceHolder1.Controls.Add(new Literal { Text = html });

                        htmlRprtObj.LogoURL = "../images/logo-small.png";                   //header
                        htmlRprtObj.Name = UA.Clinic;

                        htmlRprtObj.ReportName = clinicReprtObj.ReportName;

                        if (htmlRprtObj.LogoURL != null && htmlRprtObj.Name != null && htmlRprtObj.ReportName != null)
                        {

                            string header = htmlRprtObj.GenerateHeader();
                            PlaceHolder3.Controls.Add(new Literal { Text = header });
                        }

                        htmlRprtObj.CreatedBy = UA.userName;                                //footer

                        if (htmlRprtObj.CreatedBy != null)
                        {
                            string footer = htmlRprtObj.GenerateFooter();
                            PlaceHolder2.Controls.Add(new Literal { Text = footer });
                        }



                    }
                   

                }

                // }

           

           

           


        }




        #region Events


        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindReport();
            }


            if (IsPostBack)
            {
               // Page.ClientScript.RegisterStartupScript(this.GetType(), "PostbackKey", "<script type='text/javascript'>var isPostBack = true;</script>");
            }
        }

        #endregion Page Load

        protected void btnFilter_Click(object sender, EventArgs e)
        {

        }

        #endregion Events

        #region Methods

        #region Bind Dropdown With Column Names 

        public void BindCoulmnNameDropdown()
        {
          DataTable dt =  clinicReprtObj.GetReportDetailsByReportID();

          if (dt.Rows.Count > 0)
          {
          string    DefaultCoulmns = dt.Rows[0]["ColumnNames"].ToString();
           string   ColumnCaptions = dt.Rows[0]["ColumnCaptions"].ToString();

       string[] dfultColmns = DefaultCoulmns.Split(',');
       var dfltClmArray = dfultColmns.ToArray();

       string[] Captions = ColumnCaptions.Split(',');
       var CaptionArray = Captions.ToArray();

       Dictionary<string, string> Coulmns = new Dictionary<string, string>();
       
       for (int i = 0; i < dfltClmArray.Length; i++)
       {

           if (clinicReprtObj.SpecifiedColumns != null)
           {

         string[] SpecifiedClmns =  clinicReprtObj.SpecifiedColumns.Split(',');

         foreach (var item in SpecifiedClmns)
         {

             if (CaptionArray[i].ToString() == item.ToString())
             {
                 Coulmns.Add(dfltClmArray[i].ToString(), CaptionArray[i].ToString());  
             }

         }
           }

           else
           {
               Coulmns.Add(dfltClmArray[i].ToString(), CaptionArray[i].ToString());  
           }



      }
       if (Coulmns != null)
       {
           ddlColumns.DataSource = Coulmns;
           ddlColumns.DataTextField = "Value";
           ddlColumns.DataValueField = "Key";
           ddlColumns.DataBind();

           ddlColumns.Items.Insert(0, "--Select--");
       }

       
          }

        }

        #endregion Bind Dropdown With Column Names

        protected void imgAddIcon_Click(object sender, ImageClickEventArgs e)
        {
        
        //     ListItem li = new ListItem();
        //     li.Text = ddlColumns.SelectedItem.Text + "= " + txtvalue.Text;
        //lstSearchConditions.Items.Add(li);
            


          //  string WhereCondition = ddlColumns.SelectedItem.Text + " LIKE '" + txtvalue.Text + "%'";

           


            //lstSearchConditions.Items.Add(WhereCondition);

           
            //DataTable dtCurrentTable = null;

            //if (ViewState["WhereCondition"] != null && dtCurrentTable != null)
            //{
            //    dtCurrentTable = (DataTable)ViewState["dtWhereCondition"];//this viewstate contains a datatable structure on page load

            //}
            //else
            //{
            //    //CreateDatatableForWhereCondition();

               


            //    dtCurrentTable = new DataTable();
            //    dtCurrentTable.Columns.AddRange(new DataColumn[1] { new DataColumn(" ", typeof(string)) });
            //   // dtCurrentTable = (DataTable)ViewState["WhereCondition"];


            //    DataRow drCurrentRow = dtCurrentTable.NewRow();
            //    dtCurrentTable.Rows.Add(drCurrentRow);
            //    // int  rowIndex = dtCurrentTable.Rows.Count - 1;
            //    dtCurrentTable.Rows[0][0] = WhereCondition;

            //    gvSearchConditions.DataSource = dtCurrentTable;
            //    gvSearchConditions.DataBind();


            //    //  BindReport();

            //    //  DataTable dt = (DataTable)ViewState["dt"];
            //    //DataTable dt = clinicReprtObj.GetDataToBeReported();


            //    //string html = clinicReprtObj.GetReport(dt);
            //    //   BindReport();

            //}
        }
        #endregion Methods

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //clinicReprtObj.WhereCondition = "";

            //string WhereCondition = ddlColumns.SelectedItem.Text + " LIKE '" + txtvalue.Text + "%'";

            //if (ViewState["WhereCondition"] != null)
            //{
            //    ViewState["WhereCondition"] = " " + ViewState["WhereCondition"].ToString() + "   OR  " + WhereCondition;
            //}

            //else
            //{
            //    ViewState["WhereCondition"] = " " + WhereCondition;
            //}


            


            if (hdnWhereConditions.Value != string.Empty)
            {
                if (hdnWhereConditions.Value.Contains(","))
                {
                    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace(",", " OR ");
                }

                if (hdnWhereConditions.Value.Contains("="))
                {
                    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace("=", " LIKE ");



                }

            }

            clinicReprtObj.WhereCondition = hdnWhereConditions.Value.ToString();


            BindReport();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (hdnWhereConditions.Value != string.Empty)
            {
                if (hdnWhereConditions.Value.Contains(","))
                {
                    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace(",", " OR ");
                }

                //if (hdnWhereConditions.Value.Contains("="))
                //{
                //    hdnWhereConditions.Value = hdnWhereConditions.Value.Replace("=", " LIKE ");
                //}

              


                //'s%'
            }

            clinicReprtObj.WhereCondition = hdnWhereConditions.Value.ToString();


            BindReport();

            ClientScript.RegisterStartupScript(GetType(), "id", "MakeListUsingArray()", true);

         //   hdnArray.Value = "";
        }
    }
}