
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


        #endregion Global Varibales


        protected void Page_Load(object sender, EventArgs e)
         {
            bool UsedefaultColumn = true;

           if (!this.IsPostBack)
             {
           
                //Populating a DataTable from database.

             DataTable dt = ReprtObj.GetReportList();

             string ColumnsToHide = "ReportID" + "|" + "IsActive"  + "|"+"ClinicID";

             string[] colmnToHide = ColumnsToHide.Split('|');

             DataTable temp = dt;

             for (int i = 0; i < colmnToHide.Length ; i++)
             {
                 temp.Columns.Remove(colmnToHide[i]);
             }
            

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table class='tab'  >");

                //Building the Header row.
                html.Append("<tr>");

                //string[] Temp = ColumnsToHide.Split('|');

                foreach (DataColumn column in temp.Columns)
                {
                        //if (column.ColumnName != "ReportID" && column.ColumnName != "IsActive" && column.ColumnName != "ClinicID")
                        //{
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        //}
                 }

                //Creating last header ' view' 
                html.Append("<th>");
                html.Append(" ");

                html.Append("</th>");
                html.Append("</tr>");

                //Building the Data rows.

                int rowIndex = 0;

                foreach (DataRow row in temp.Rows)
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

                    foreach (DataColumn column in temp.Columns)
                    {
                        //if (column.ColumnName != "ReportID" && column.ColumnName != "IsActive" && column.ColumnName != "ClinicID")
                        //{
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        //}
                    }


                    //Create clickable image to view report

                    html.Append("<td>");

                    html.Append(" <a href='../Admin/Admin.aspx'><img src='../images/package_icon13.png' title='View report' /></a>");
                       
                   
                    html.Append("</td>");
                    html.Append("</tr>");

                }

                //Table end.
                html.Append("</table>");

                //Append the HTML string to Placeholder.
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });

            }
        }
    }
}