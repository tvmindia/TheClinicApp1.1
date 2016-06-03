
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

#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class HTMLReports
    {

        #region Global Variables

        #endregion Global Variables

        #region Public Properties

        /// <summary>
        /// Holds the dataset to be converted into html table
        /// </summary>
        public DataTable dtForReport
        {
            get;
            set;
        }

        /// <summary>
        /// Holds the names of columns to be added
        /// </summary>
        public string AddColumn
        {
            get;
            set;
        }

        /// <summary>
        /// Holds the names of columns to be removed
        /// </summary>
        public string RemoveColumn
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Methods

        #region Generate Report

        /// <summary>
        /// Converts the datatable to html ,by changing columns of datatable to headers(th) ,and rows into tds . 
        /// Inorder to apply alternative color for the rows ,different classes are added for even and odd rows
        /// </summary>
        /// <returns></returns>
        public  string GenerateReport()
        {
            string HtmlOfTable = string.Empty;

            DataTable dt = dtForReport;                    //Populating a DataTable from database.
           
            StringBuilder html = new StringBuilder();      // Building an HTML string 

             html.Append("<table class='tab'>");            //Table start.


             html.Append("<tr>");                           //Building the Header row.

            //string[] Temp = ColumnsToHide.Split('|');

            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th style='display:none;'>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }


            html.Append("<th>");                            //Creating last header ' view' 
            html.Append(" ");
            html.Append("</th>");
            html.Append("</tr>");

            int rowIndex = 0;

            foreach (DataRow row in dt.Rows)                 //Building the Data rows.
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
                        html.Append("<td'>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                }

                html.Append("</tr>");

            }


            html.Append("</table>");                       //Table end.

            HtmlOfTable = html.ToString();

            return HtmlOfTable;

        }

        #endregion Generate Report

        #endregion Methods
    }
}