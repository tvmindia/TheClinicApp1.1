﻿
#region CopyRight

//Author      : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion  Included Namespaces

namespace TheClinicApp1._1
{
    public partial class ReportsList : System.Web.UI.Page
    {
        #region Global Varibales

        Reports ReprtObj = new Reports();
        UIClasses.Const Const = new UIClasses.Const();

        #endregion Global Varibales

        #region Public Properties

        public List<string> Columns = new List<string>();
          
        #endregion Public Properties

        #region Events

        #region Page Load
      
        protected void Page_Load(object sender, EventArgs e)
         {
             ReprtObj.DisplaySerailNo = true ;
             int rowIndex = 0;
             string ReportID = string.Empty;
             string ReportName = string.Empty;

             Columns.Add("ReportID");
             Columns.Add("Report");                              //Speicfy columns to be displayed
             Columns.Add("Description");

           if (!this.IsPostBack)
             {
                 DataTable dt = ReprtObj.GetReportList();       //Populating a DataTable from database.

                 StringBuilder html = new StringBuilder();      //Building an HTML string.

                 html.Append("<table class='tab'  >");          //Table start.

                 html.Append("<tr>");                          //Building the Header row.  

                 if ( ReprtObj.DisplaySerailNo == true)
                 {
                     html.Append("<th>");
                     html.Append("Sl.No.");
                     html.Append("</th>");
                 }
                     

                foreach (DataColumn column in dt.Columns)
                {

                    if (Columns.Count > 0)
                    {
                        if (Columns.Contains(column.ColumnName))  //Adding the specified columns
                        {
                            if (column.ColumnName != "ReportID") 
                            {
                                html.Append("<th>");
                                html.Append(column.ColumnName);
                                html.Append("</th>");
                            }
                           
                        }
                    }
                    else                                          //USE DEFAULT COLUMNS
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }

                 }


                html.Append("<th>");                              //Creating last header ' view' 
                html.Append(" ");

                html.Append("</th>");
                html.Append("</tr>");


                if (dt.Rows.Count == 0)
                {
                    int ColumnCount = Columns.Count+1;



                    html.Append("<tr style='background-color:#e1e6ef'>");

                    html.Append("<td colspan=" + ColumnCount + " style='text-align:center' >");
                    html.Append("No records found.");
                    html.Append("</td>");


                    html.Append("</tr>");

                    //html.Append("<tr><td colspan=" + ColumnCount + "></td>No Items Found!</tr>");

                }



                foreach (DataRow row in dt.Rows)                  //Building the Data rows.
                {
                    rowIndex = rowIndex + 1;

                    if (rowIndex % 2 == 0)
                    {
                        html.Append("<tr class='even'>");
                    }

                    else
                    {
                        html.Append("<tr class='odd'>");
                    }

                  

                    if (ReprtObj.DisplaySerailNo == true)
                    {
                        html.Append("<td style='padding-left:10px' >");
                        html.Append(rowIndex);
                        html.Append("</td>");

                    }


                    foreach (DataColumn column in dt.Columns)
                    {
                        if (Columns.Count > 0)
                        {
                            if (Columns.Contains(column.ColumnName))  //Adding the specified columns except reportid
                            {

                                if (column.ColumnName != "ReportID")
                                {
                                    html.Append("<td>");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }

                                else
                                {
                                    ReportID = row[column.ColumnName].ToString();  //Get the report id to pass

                                }

                                if (column.ColumnName == "Report")
                                {
                                   ReportName =  row[column.ColumnName].ToString();

                                   ReportName = ReportName.Replace(" ", "_");
                                }


                            }

                        }


                        else
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");

                            if (column.ColumnName == "ReportID")
                            {
                                ReportID = row[column.ColumnName].ToString();
                            }
                        }
                       
                    }


                    html.Append("<td>");                                          //Create clickable image to view report


                   


                    html.Append(" <a   onclick=SetIframeSrc('" + ReportID + "','" + ReportName + "') ><img src='../images/package_icon13.png' title='View report' /></a>");

                    html.Append("</td>");
                  
                    html.Append("</tr>");

                }

                html.Append("</table>");                                          //Table end.

                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });    //Append the HTML string to Placeholder.
               }
            }

        #endregion Page Load

        #region Logout Click
        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        { string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        }

        #endregion  Logout Click

        #endregion Events


    }
    
}