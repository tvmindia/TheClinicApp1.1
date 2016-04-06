using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace TheClinicApp.ClinicDAL
{
    public class IssueHeaderDetails
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        #endregion Global Variables

        #region constructor

        public IssueHeaderDetails()
        {


            IssueID = Guid.NewGuid();


        }

        #endregion constructor

        #region Property

        public Guid IssueID
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string IssuedTo
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public string PrescID
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get;
            set;
        }


        public string IssueNO
        {
            get;
            set;
        }



        #endregion Property

        #region Methods

        #region Validate IssueNo
        public bool CheckIssueNoDuplication(string IssueNo)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckIssueNoDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IssueNo", SqlDbType.VarChar,20).Value = IssueNo;
                SqlParameter outflag = cmd.Parameters.Add("@flag", SqlDbType.Bit);
                outflag.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                flag = (bool)outflag.Value;
                if (flag == true)
                {
                    return flag;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }

            return false;
        }

        #endregion Validate IssueNo

        #region Get Medicine Details By MedicineName

        public DataSet GetMedicineDetailsByMedicineName(string MedicineName)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetMedicineDetailsByMedicineName]";

                cmd.Parameters.Add("@Medname", SqlDbType.NVarChar, 255).Value = MedicineName;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion Get Medicine Details By MedicineName

        #region Generate_Issue_Number
        public string Generate_Issue_Number()
        {

            string sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            string mn = datevalue.Month.ToString();
            string yy = datevalue.Year.ToString();
            string dd = datevalue.Day.ToString();
            string NUM;

            dbConnection dcon = null;
          
            try
            {
                 DateTime now = DateTime.Now;
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetTopIssueNO]";
              
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                SqlParameter OutparmItemId = cmd.Parameters.Add("@String", SqlDbType.NVarChar,50);
                OutparmItemId.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                NUM = OutparmItemId.Value.ToString();    
             }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }


            if (NUM != "")
            {
                //trim here
                int x = Convert.ToInt32(NUM.Substring(NUM.IndexOf('/') + 1));
                x = x + 1;
                x.ToString();
                string IssueNO = yy + '-' + mn + '-' + dd + '/' + x;

                return IssueNO;

            }

            else
            {
                
                int x = 1;
                string IssueNO = yy + '-' + mn + '-' + dd + '/' + x;
                return IssueNO;

            }

            
        }

        #endregion Generate_Issue_Number
  
        #region InsertIssueHeader
        public void InsertIssueHeader()
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertIssueHeader]";

                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = IssueID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@IssuedTo", SqlDbType.NVarChar, 255).Value = IssuedTo;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = Date;
                cmd.Parameters.Add("@IssueNO", SqlDbType.NVarChar, 50).Value = IssueNO;
                //cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                   //eObj.InsertionSuccessMessage(page);
                }
                else
                {
                    if (Outputval == 0)
                    {
                        //Already exists!
                    }
                }

               
            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion InsertIssueHeader

        #region UpdateIssueHeader
        public void UpdateIssueHeader(string IssueID)
        {

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateIssueHeader]";
                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(IssueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@IssuedTo", SqlDbType.NVarChar, 255).Value = IssuedTo;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = Date;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion UpdateIssueHeader

        #region View_Search_IssueHeader

        public DataSet ViewIssueHeader(string String)
        {

            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ViewIssueHeader]";

                if (String == String.Empty)
                {
                    cmd.Parameters.Add("@String", SqlDbType.NVarChar, 255).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@String", SqlDbType.NVarChar, 255).Value = String;
                }

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion View_Search_IssueHeader

        #region DeleteIssueHeader
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IssueID"></param>
        public void DeleteIssueHeader(string IssueID)
        {
            dbConnection dcon = null;

            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteIssueHeader]";

                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(IssueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion DeleteIssueHeaderDetails

        //Get Issue Details by Passing Issue ID

        #region GetIssueDetailsByIssueID

        public DataSet GetIssueDetailsByIssueID(String IssueID)
        {

            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetIssueDetailsByIssueID]";


                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(IssueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                 sda.Fill(ds, "Medicines");

                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion GetIssueDetailsByIssueID

        #region Get Issue Header Details By IssueID

        public DataSet GetIssueHeaderByIssueID(String IssueID)
        {
            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetIssueHeaderByIssueID]";


                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(IssueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);

                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
        }

        #endregion Get Issue Header Details By IssueID

        #region Get Issue Details By IssueNo

        public DataSet GetIssueDetailsByIssueNO(String IssueNo)
        {

            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetIssueDetailsByIssueNo]";


                cmd.Parameters.Add("@IssueNo", SqlDbType.NVarChar, 255).Value = IssueNo;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds,"Medicines");

                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion Get Issue Details By IssueNo

        #endregion Methods

    }
    public class IssueDetails
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        #endregion Global Variables

        #region constructor

        public IssueDetails()
        {
            UniqueID = Guid.NewGuid();

        }


        public IssueDetails(Guid uniqueID)
        {

            UniqueID = uniqueID;


        }



        #endregion constructor

        #region Property
        public Guid UniqueID
        {
            get;
            set;
        }
        public Guid IssueID
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string MedicineID
        {
            get;
            set;
        }
        public int Qty
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get;
            set;
        }
        public string ReceiptID
        {
            get;
            set;
        }

        public string MedicineName
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        #endregion Property

        #region Methods

        #region Get MedcineID By MedicineName

        public string GetMedcineIDByMedicineName(string MedicineName)
        {
            string MedicineID = string.Empty;
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetMedcineIDByMedicineName]";

                cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar, 255).Value = MedicineName;

               object  ID = cmd.ExecuteScalar();
               if (ID != null)
                {
                     MedicineID = ID.ToString();
                }
              
            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return MedicineID;
        }

        #endregion Get MedcineID By MedicineName

        #region InsertIssueDetails
        public void InsertIssueDetails()
        {
            dbConnection dcon = null;
            //string medID = GetMedcineIDByMedicineName(MedicineName);

            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertIssueDetails]";

                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = UniqueID;
                cmd.Parameters.Add("@IssueID", SqlDbType.UniqueIdentifier).Value = IssueID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar, 255).Value = MedicineName;
                //cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(medID);
                cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    //eObj.InsertionSuccessMessage(page);
                }
                else
                {
                    if (Outputval == 2)
                    {
                        //Out of stock
                    }
                }
              
            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }

        #endregion InsertIssueDetails

        #region UpdateIssueDetails
        public void UpdateIssueDetails(string UniqueID)
        {

            dbConnection dcon = null;

            try
            {

                DateTime now = DateTime.Now;
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateIssueDetails]";



                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                //cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(MedicineID);
                cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;


                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    //eObj.InsertionSuccessMessage(page);
                }


            }

            catch (Exception ex)
            {

                var page = HttpContext.Current.CurrentHandler as Page;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }

        }


        #endregion UpdateIssueDetails

        #region ViewIssueDetails

        public DataSet ViewIssueDetails()
        {

            dbConnection dcon = null;
            DataSet ds = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ViewIssueDetails]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);


                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds);


                return ds;

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }


            }

        }


        #endregion ViewIssueDetails

        #region DeleteIssueDetails

        public void DeleteIssueDetails(string UniqueID)
        {
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteIssueDetails]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                //cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(MedicineID);

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    //eObj.InsertionSuccessMessage(page);
                }

            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
               
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
             }

        }



        #endregion DeleteIssueDetails

        #region Get MedicineID By UniqueID

        public string GetMedicineIDByUniqueID(Guid UniqueID)
        {
            string MedicineID = string.Empty;
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetMedicineIDByUniqueID]";

                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = UniqueID;

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    MedicineID = ID.ToString();
                }

            }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return MedicineID;
        }


        #endregion Get MedicineID By UniqueID

        #endregion Methods

    }
}