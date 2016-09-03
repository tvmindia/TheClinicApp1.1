using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;


namespace TheClinicApp1._1.ClinicDAL
{
    public class TokensBooking
    {

        #region Global Variables

        common cmn = new common();
         ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;  
        string msgText;
        string msgCaption;
        #endregion Global Variables

        //Property
        #region TokenProperty  
      
        public int DateTimeFormatCode = 113;

        public string Module = "TokensBooking";
       
        public int TokenNo
        {
            get;
            set;
        }
        public string DoctorID
        {
            get;
            set;

        }
        public string PatientID
        {
            get;
            set;
        }
        public DateTime DateTime
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreateDate
        {
            get;
            set;
        }  
   

        #endregion Token_Property

        //Methods
        #region Token_Methods

        /*--------- View Search Paging Of Stock ,StockIN ,StockOUT,OutOfStock ------------*/

        #region Medicine View Search Paging

        public string ViewAndFilterPatientBooking(string searchTerm, int pageIndex, int PageSize)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;

            var xml = string.Empty;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ViewAndFilterPatientBooking";

                DateTime now = DateTime.Now;
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode; 

                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "PatientBooking");

                //-----------Paging Section 

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = pageIndex;
                dt.Rows[0]["PageSize"] = PageSize;
                dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                ds.Tables.Add(dt);

                xml = ds.GetXml();


            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterPatientBooking";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

            return xml;
        }

        #endregion Medicine View Search Paging

        /*--------- View Search Paging Of Token OF Doctors ------------*/

        #region ViewAndFilterTokenOfDoctors

        public string ViewAndFilterTokenOfDoctors(string searchTerm, int pageIndex, int PageSize)
        {
            var xml = string.Empty;
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
                DateTime now = DateTime.Now;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ViewAndFilterBookingsForDoctor]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");

                cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;

                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;



                sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds,"DoctorTokens");

                //-----------Paging Section 

                DataTable dt = new DataTable("Pager");
                dt.Columns.Add("PageIndex");
                dt.Columns.Add("PageSize");
                dt.Columns.Add("RecordCount");
                dt.Rows.Add();
                dt.Rows[0]["PageIndex"] = pageIndex;
                dt.Rows[0]["PageSize"] = PageSize;
                dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                ds.Tables.Add(dt);

                xml = ds.GetXml();

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAndFilterTokenOfDoctors";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return xml;

        }

        #endregion ViewAndFilterTokenOfDoctors


        #region GetSearchBoxData
        public DataTable GetSearchBoxData()
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("GetSearchBoxData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion GetSearchBoxData

        #region GetPatientDetails
        /// <summary>
        /// Get Patient Details By Passing File Number
        /// 
        /// </summary>
        /// <param name="str1"></param>
        /// <returns>patient details by joining file,patient and visit tables</returns>
        public DataSet GetpatientDetails(string str1)
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetPatientDetailsForToken]";
                cmd.Parameters.Add("@filenumber", SqlDbType.NVarChar).Value = str1;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                 
                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
               
                ds = new DataSet();
                sda.Fill(ds);              
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetPatientDetails";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return ds;
        }

        #endregion GetPatientDetails

        #region GetPatientTokenDetailsbyID
        /// <summary>
        /// Get Patient Token Detail by passing Patient ID
        /// </summary>
        /// <param name="str1"></param>
        /// <returns>patient details by joining Token,Doctor,file,patient and visit tables</returns>
        public DataSet GetPatientTokenDetailsbyID(string str1)
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetPatientTokenDetailsbyID]";
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(str1);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);   
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetPatientTokenDetailsByID";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;
        }

        #endregion GetPatientDetails

        #region Doctorbind
        /// <summary>
        /// get datas from doctors table
        /// </summary>
        /// <returns> return doctor dataset</returns>
        public DataSet DropBindDoctorsName()
        {
            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetDoctorDetails]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                 sda= new SqlDataAdapter(cmd) ;
                 ds = new DataSet();
                 sda.Fill(ds);    
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DropBindDoctorsName";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;      
    
        }

        #endregion Doctorbind

        #region InsertToken
        /// <summary>
        /// to insert a new token all params should be set.
        /// </summary>
        /// <returns>return generated token number</returns>
        public int InsertToken()
        {
           
            SqlConnection con = null;
            
            try
            {
            
            //DateTime now = DateTime.Now;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertTokens]";
          
            cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
            cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PatientID);        
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;        

            SqlParameter OutparmItemId = cmd.Parameters.Add("@TokenNo", SqlDbType.Int);
            OutparmItemId.Direction = ParameterDirection.Output;            

            cmd.ExecuteNonQuery();
            TokenNo = Convert.ToInt32(OutparmItemId.Value);
            msgText = " Token No:" + TokenNo;
            msgCaption = "Token Booked Sucessfully";

            var page = HttpContext.Current.CurrentHandler as Page;
            eObj.InsertionSuccessMessage(page, msgCaption, msgText);
             }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertToken";
                eObj.InsertError();
            }

            finally
            {        
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return TokenNo;
        }

        #endregion InsertToken

        #region ViewToken
        /// <summary>
        /// TO get PAtient Booking Details by passing Today's date
        /// </summary>
        /// <returns> Patient booking details by joining tokens,doctor and patient tables</returns>
        public DataSet ViewToken()
        {

            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
            
            DateTime now = DateTime.Now;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[ViewPatientsBooking]";
                         
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

            cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode; 

            sda = new SqlDataAdapter();
            cmd.ExecuteNonQuery();
            sda.SelectCommand = cmd;
            ds = new DataSet();
            sda.Fill(ds);
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewToken";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }
            return ds;

        }

        #endregion ViewToken
      
        #region DoctorViewToken
        /// <summary>
        /// TO view token list by doctor's ID
        /// Doctors can view only their Patient's 
        /// </summary>
        /// <returns> Patient list by doctors</returns>
        public DataSet DoctorViewToken()
        {

            SqlConnection con = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {
                DateTime now = DateTime.Now;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DoctorViewPatientsBooking]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =Guid.Parse(ClinicID);
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");

                cmd.Parameters.Add("@FormatCode", SqlDbType.Int).Value = cmn.DateTimeFormatCode;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);     
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DoctorViewToken";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return ds;

        }

        #endregion DoctorViewToken

        #region DeleteToken

        /// <summary>
        /// to delete tokens
        /// </summary>
        /// <param name="id"></param>
        
        public void DeleteToken(string id)
        {
            SqlConnection con = null;

            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteToken]";
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(id);
                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteToken";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {

                    con.Dispose();
                }
            }




        }

        public string DeleteTokenForWM(string id)
        {
            string result = string.Empty;
            SqlConnection con = null;

            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteToken]";
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(id);


                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();

                result = Output.Value.ToString();

               
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteTokenForWM";
                eObj.InsertError();
            }

            finally
            {
                if (con != null)
                {

                    con.Dispose();
                }
            }

            return result;


        }


        #endregion DeleteToken     
        
        #endregion Token_Methods


        
    



    }
}