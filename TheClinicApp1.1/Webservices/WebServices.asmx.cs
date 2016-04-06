using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace TheClinicApp.Webservices
{
    /// <summary>
    /// Summary description for WebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServices : System.Web.Services.WebService
    {
        #region User Login
        [WebMethod]
        public string UserLogin(string username, string password)
        {
            //return msg data initialization
            DataSet ds = new DataSet();
            DataTable loginMsg = new DataTable();
            loginMsg.Columns.Add("Flag", typeof(Boolean));
            loginMsg.Columns.Add("Message", typeof(String));
            DataRow dr = loginMsg.NewRow();

            ClinicDAL.CryptographyFunctions crypto = new ClinicDAL.CryptographyFunctions();

            username = crypto.Decrypt(username);
            password = crypto.Decrypt(password);

            try
            {
                //user credentials checking
                ClinicDAL.UserAuthendication UA = new ClinicDAL.UserAuthendication(username, password);
                if (UA.ValidUser)
                {
                    dr["Flag"] = true;
                    dr["Message"] = UIClasses.Messages.LoginSuccess;
                    loginMsg.Columns.Add("ClinicID", typeof(String));
                    dr["ClinicID"] = UA.ClinicID;
                }
                else
                {
                    dr["Flag"] = false;
                    dr["Message"] = UIClasses.Messages.LoginFailed;
                }
            }
            catch (Exception ex)
            {
                dr["Flag"] = false;
                dr["Message"] = ex.Message;                 //exception message to be passed as JSON
            }
            finally
            {
                //returning data
                loginMsg.Rows.Add(dr);
                ds.Tables.Add(loginMsg);
            }
            return getDbDataAsJSON(ds);
        }
        #endregion User Login

        #region Get Visit List with Name
        /// <summary>
        /// Get Visit List with Name
        /// </summary>
        /// <returns>JSON of list of visit</returns>
        [WebMethod]
        public string GetVisitList()
        {  //return msg data initialization
            DataSet ds = new DataSet();
            try
            {   //Retrieving details
                ClinicDAL.CaseFile.Visit visit = new ClinicDAL.CaseFile.Visit();
                ds.Tables.Add(visit.GetVisitListforMobile());
            }
            catch (Exception ex)
            {
                //Return error message
                DataTable ErrorMsg = new DataTable();
                ErrorMsg.Columns.Add("Flag", typeof(Boolean));
                ErrorMsg.Columns.Add("Message", typeof(String));
                DataRow dr = ErrorMsg.NewRow();
                dr["Flag"] = false;
                dr["Message"] = ex.Message;
                ErrorMsg.Rows.Add(dr);
                ds.Tables.Add(ErrorMsg);
            }
            finally
            {
            }
            return getDbDataAsJSON(ds);
        }
        #endregion  Get Visit List with Name

        #region Add Vist Attatchment
        /// <summary>
        /// Webservice to get new attachment file from mobile
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string AddVisitAttatchment()
        {
            try
            {
                HttpFileCollection MyFileCollection = HttpContext.Current.Request.Files;
                //Getting file dettails from http request
                if (MyFileCollection.Count > 0)
                {
                                   //string FilePath = Server.MapPath("~/")+DateTime.Now.ToString("ddHHmmssfff")+MyFileCollection[0].FileName;
                                   //MyFileCollection[0].SaveAs(FilePath); //to save coming image to server folder
                    Stream MyStream = MyFileCollection[0].InputStream;
                    ClinicDAL.CaseFile.Visit.VisitAttachment visitAttachment = new ClinicDAL.CaseFile.Visit.VisitAttachment();

                    visitAttachment.Attachment = MyStream;
                    MyStream.Flush();

                    visitAttachment.Type = "." + MyFileCollection[0].FileName.Split('.').Last();
                    visitAttachment.Name = MyFileCollection[0].FileName;

                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["VisitID"]))
                    {
                        visitAttachment.VisitID = new Guid(HttpContext.Current.Request.Form["VisitID"]);
                    }
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["ClinicID"]))
                    {
                        visitAttachment.ClinicID = new Guid(HttpContext.Current.Request.Form["ClinicID"]);
                    }
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Description"]))
                    {
                        visitAttachment.Description = HttpContext.Current.Request.Form["Description"];
                    }

                    float Size = MyFileCollection[0].ContentLength / 1024;
                    float sizeinMB = Size / 1024;
                    string fileSize;
                    if ((int)sizeinMB == 0)
                    {
                        fileSize = Size + "KB";
                    }
                    else
                    {
                        fileSize = sizeinMB.ToString("0.00") + "MB";
                    }
                    visitAttachment.Size = fileSize;
                    
                    string userName = "";
                    if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["userName"]))
                    {
                        userName = HttpContext.Current.Request.Form["userName"];
                    }

                    int k=visitAttachment.InsertFileAttachment(true, userName);
                    return "Message:" + UIClasses.Messages.SuccessfulUpload;
                }

                return "Message:" + UIClasses.Messages.InsertionFailure;
            }
            catch (Exception ex)
            {
                //System.IO.File.WriteAllText(@Server.MapPath("~/Text.txt"), ex.Message);
                return "Message:" + ex.Message;
            }
            finally
            {
            }
        }
        #endregion Add Vist Attatchment

        #region JSON converter and sender
        public String getDbDataAsJSON(DataSet ds)
        {
            ClinicDAL.CryptographyFunctions crypto = new ClinicDAL.CryptographyFunctions();
            try
            {
                DataTable dt = ds.Tables[0];
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                this.Context.Response.ContentType = "";

                return "[" + crypto.Encrypt(serializer.Serialize(rows)) + "]";

            }
            catch (Exception)
            {

                return "";
            }
            finally
            {

            }

        }
        //public String getDbDataAsJSON(SqlCommand cmd, ArrayList imgColName, ArrayList imgFileNameCol)
        //{
        //    try
        //    {
        //        DataSet ds = null;
        //        SqlDataAdapter sda = new SqlDataAdapter();
        //        sda.SelectCommand = cmd;
        //        ds = new DataSet();
        //        sda.Fill(ds);
        //        DataTable dt = ds.Tables[0];
        //        String filePath = Server.MapPath("~/tempImages/");      //temporary folder to store images

        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            //adding data in JSON
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                if (!imgColName.Contains(col.ColumnName))
        //                {
        //                    if (!imgFileNameCol.Contains(col.ColumnName))
        //                        row.Add(col.ColumnName, dr[col]);
        //                }
        //            }
        //            //adding image details in JSON
        //            for (int i = 0; i < imgColName.Count; i++)
        //            {
        //                if (dr[imgColName[i] as string] != DBNull.Value)
        //                {
        //                    String fileURL = filePath + DateTime.Now.ToString("ddHHmmssfff") + dr[imgFileNameCol[i] as string];
        //                    if (!System.IO.File.Exists(fileURL))
        //                    {
        //                        byte[] buffer = (byte[])dr[imgColName[i] as string];
        //                        System.IO.File.WriteAllBytes(fileURL, buffer);
        //                    }
        //                    row.Add(imgColName[i] as string, fileURL);
        //                }
        //            }
        //            rows.Add(row);
        //        }

        //        this.Context.Response.ContentType = "";

        //        return serializer.Serialize(rows);

        //    }
        //    catch (Exception)
        //    {

        //        return "";
        //    }
        //    finally
        //    {

        //    }
        //}
        #endregion JSON converter and sender

        

    }
}
