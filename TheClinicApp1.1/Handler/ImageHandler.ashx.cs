using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;


namespace TheClinicApp.Handler
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
      

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "image/jpeg";

                if (context.Request.QueryString["PatientID"] != null)
                {
                    Guid PatientID;
                    PatientID = Guid.Parse(context.Request.QueryString["PatientID"]);
                    if (GetImageFromDB(PatientID) != null)
                    {
                        MemoryStream memoryStream = new MemoryStream(GetImageFromDB(PatientID), false);
                        Image imgFromGB = Image.FromStream(memoryStream);
                        imgFromGB.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        context.Response.ContentType = "image/png";
                        context.Response.WriteFile("~/images/UploadPic1.jpg"); 

                    }

                }
                if ((context.Request.QueryString["AttachID"] != null) && (context.Request.QueryString["AttachID"] != ""))
                {
                    TheClinicApp1._1.ClinicDAL.CaseFile.Visit.VisitAttachment AttachObj = new TheClinicApp1._1.ClinicDAL.CaseFile.Visit.VisitAttachment();
                    AttachObj.AttachID = Guid.Parse(context.Request.QueryString["AttachID"]);
                    byte[] productimg = AttachObj.GetAttachmentImages();
                    if (productimg != null)
                    {
                        MemoryStream memoryStream = new MemoryStream(productimg, false);
                        Image proimg = Image.FromStream(memoryStream);
                        proimg.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        //context.Response.ContentType = "image/png";
                        //context.Response.WriteFile("~/img/Default/nologo1.png");
                    }

                }
            }
            catch
            {
                                
            }
            
        }


        private byte[] GetImageFromDB(Guid PatientID)
        {
            SqlConnection con = null;
            byte[] ImageByteArray = null;

            try
            {
                con = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["ClinicAppConnectionString"].ConnectionString);

                con.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.CommandText = "usp_GetProfileImage";
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;

                SqlDataReader rd = cmd.ExecuteReader();
                if ((rd.Read())&&(rd.HasRows)&&(rd["image"]!=DBNull.Value))
                {
                    ImageByteArray = (byte[])rd["image"];
                }
               
                rd.Close();
                con.Close();
                return ImageByteArray;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}