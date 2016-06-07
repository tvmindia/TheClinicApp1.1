
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

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        ErrorHandling eObj = new ErrorHandling();

        #endregion Global Variables

        #region Public Properties

    public    List<string> Columns = new List<string>();


        /// <summary>
        /// Holds the dataset to be converted into html table
        /// </summary>
        public DataTable Datasource
        {
            get;
            set;
        }

        public string LogoURL
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public string ReportName
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

            try
            {

                DataTable dt = Datasource;                    //Populating a DataTable from database.

                if (dt != null)
                {
                    StringBuilder html = new StringBuilder();      // Building an HTML string 

                    html.Append("</br>");
                    html.Append("<table>");            //Table start.

                    html.Append("<tr>");                           //Building the Header row.

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (Columns.Count > 0)            
                        {
                            if (Columns.Contains(column.ColumnName))  //Adding the specified columns
                            {
                                html.Append("<th>");
                                html.Append(column.ColumnName);
                                html.Append("</th>");
                            }
                        }

                        else
                        {
                            html.Append("<th>");                    //Use default columns
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                    }


                   
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
                            if (Columns.Count > 0)
                            {
                                if (Columns.Contains(column.ColumnName))  //Adding the specified columns
                                {
                                    html.Append("<td >");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                        }

                        html.Append("</tr>");

                    }


                    html.Append("</table>");                       //Table end.

                    HtmlOfTable = html.ToString();
                }

                else
                {
                    throw new ArgumentException("DataSource is empty");
                }

            }
            catch (Exception ex)
            {
                throw;

            }

            return HtmlOfTable;
         }

        #endregion Generate Report

      
        #region Generate Footer

        public string GenerateFooter()
        {
            StringBuilder html = new StringBuilder();      // Building an HTML string 

            try
            {
                if (CreatedBy != null)
                {
                    html.Append("<div class='footer'>");
                    html.Append("Report generated by: " + CreatedBy + " | " + DateTime.Now.ToString("dd-MMM-yyyy"));
                   
                    html.Append("</div>");
                }
                else
                {
                    throw new ArgumentException("Pass parameters properly");
                }

            }
            catch (Exception)
            {
                
                throw;
            }
           
            return html.ToString();
        }

        #endregion Generate Footer

        #region Generate Header

        public string GenerateHeader()
        {
            StringBuilder html = new StringBuilder();      // Building an HTML string 

            try
            {
                if (LogoURL != null && Name != null && ReportName != null)
                {
                    html.Append("<div class='header'>");
                    html.Append("<div class='logo1'>");
                    html.Append("<img  src='" + LogoURL + "'/>");
                    html.Append("</div>");
                    html.Append("<div class='logo2'>");
                    html.Append("<span class='Clinicname'> " + Name + "</span><p align='center' style='line-height:0px!important;'>" + ReportName + "</p>");

                    html.Append("</div>"); 
                    html.Append("</div>");
                }
                else
                {
                    throw new ArgumentException("Pass parameters properly");
                }

                
            }
            catch (Exception)
            {
                throw;
            }

           
            return html.ToString();
        }

        #endregion Generate Header


        #endregion Methods
    }
}