
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
            if (!this.IsPostBack)
            {
                //Populating a DataTable from database.

                ReprtObj.ReportName = "AllRegisteredPatients";
                DataTable dt = ReprtObj.GetDataToBeReported();

                //Building an HTML string.
                StringBuilder html = new StringBuilder();

                //Table start.
                html.Append("<table class='tab'  >");

                //Building the Header row.
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ColumnName != "PatientID" && column.ColumnName != "ClinicID")
                    {
                        html.Append("<th>");
                        html.Append(column.ColumnName);
                        html.Append("</th>");
                    }
                    
                }
                html.Append("</tr>");

                //Building the Data rows.

                int rowIndex = 0;

                foreach (DataRow row in dt.Rows)
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


                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "PatientID" && column.ColumnName != "ClinicID")
                        {
                            html.Append("<td>");
                            html.Append(row[column.ColumnName]);
                            html.Append("</td>");
                        }
                    }
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