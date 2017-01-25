using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class IssueHeaderDetails
    {
        common cmn = new common();

        #region Global Variables

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA; 

        #endregion Global Variables

        #region constructor

        public IssueHeaderDetails()
        {


            IssueID = Guid.NewGuid();


        }

        #endregion constructor

        #region Property


        public string Module = "IssueHeaderDetails";
      
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
        /// <summary>
        /// To check the issue number is duplicated or not by passing input issue number
        /// </summary>
        /// <param name="IssueNo"></param>
        /// <returns>a boolean value</returns>
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
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "CheckIssueNoDuplication";
                eObj.InsertError();

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
        /// <summary>
        /// To medicine details by pasing medicine name
        /// </summary>
        /// <param name="MedicineName"></param>
        /// <returns></returns>
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
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetMedicineDetailsByMedicineName";
                eObj.InsertError();

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return ds;
        }

        #endregion Get Medicine Details By MedicineName

        #region Generate_Issue_Number
        /// <summary>
        /// Issue number is genereated by checking the TOP issue number from Database 
        /// format of issue number is day-month-year/issueno
        /// issueno is incrementing 1 to the top issue number
        ///  TOPISSUENO is trimed for get the last issue number (after /)
        /// x is Issue number
        /// 
        /// </summary>
        /// <returns></returns>
        public string Generate_Issue_Number()
        {

            string sDate =cmn.ConvertDatenow(DateTime.Now).ToString();
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            string mn = datevalue.ToString("MMM");
            string yy = datevalue.Year.ToString();
            string dd = datevalue.Day.ToString();
            string TOPISSUENO;
            string IssueNO=string.Empty;
            dbConnection dcon = null;          
            try
            {
                DateTime now =cmn.ConvertDatenow(DateTime.Now);
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
                TOPISSUENO = OutparmItemId.Value.ToString();

                if (TOPISSUENO != "")
                {
                    //trim here
                    int x = Convert.ToInt32(TOPISSUENO.Substring(TOPISSUENO.IndexOf('/') + 1));
                    x = x + 1;
                    x.ToString();         
                      IssueNO = dd + '-' + mn + '-' + yy + '/' + x;                  
                }

                else
                {
                    int x = 1;                
                     IssueNO = dd + '-' + mn + '-' + yy + '/' + x; 
                }
             }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GenerateIssueNumber";
                eObj.InsertError();
            }
            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }
            return IssueNO;
            
        }

        #endregion Generate_Issue_Number
  
        #region InsertIssueHeader
        /// <summary>
        /// inserting issue header 
        /// </summary>
        /// <returns></returns>
        public int InsertIssueHeader()
        {
            int result = 0;
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
                
                if ((PrescID != null) && (PrescID!=string.Empty))
                {
                    cmd.Parameters.Add("@PrescID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PrescID);
                }

               
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                //cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);
                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    result = 1;
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                }
                else
                {
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);
                        //Already exists!
                    }
                }               
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertIssueHeader";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return result;
        }

        #endregion InsertIssueHeader

        #region UpdateIssueHeader
        /// <summary>
        /// updating issue header with Issue ID
        /// </summary>
        /// <param name="IssueID"></param>
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
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateIssueHeader";
                eObj.InsertError();
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

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "View Issue Header";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return ds;

        }


        #endregion View_Search_IssueHeader

        #region DeleteIssueHeader
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IssueID"></param>
        public int DeleteIssueHeader(string IssueID)
        {
            int Outputval = 0;
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

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                 Outputval = (int)cmd.Parameters["@Status"].Value;

                //if (Outputval == 1)
                //{
                //    //Success
                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    //eObj.SavedSuccessMessage(page);
                //    eObj.DeleteSuccessMessage(page);
                //}

                //if (Outputval == 0)
                //{
                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.DeletionNotSuccessMessage(page);
                //    //eObj.SavingFailureMessage(page);
                //}

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteIssueHeader";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return Outputval;
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

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetIssueDetailsByIssueID";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return ds;
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

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetIssueHeaderByIssueID";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return ds;
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
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetIssueDetailsByIssueNO";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return ds;
        }


        #endregion Get Issue Details By IssueNo

        #region Get Total Qty Of A Medicine

        public string GetTotalIssuedQtyOfAMedicine(string MedicineName)
        {
            string TotlQty = string.Empty;
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetTotalIssuedQtyOfAMedicine]";

                cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar, 255).Value = MedicineName;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    TotlQty = ID.ToString();
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetTotalQtyOfAMedicine";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return TotlQty;
        }

        #endregion Get Total Qty Of A Medicine

        #endregion Methods

    }


    public class IssueDetails
    {
        common cmn = new common();
        string msg = string.Empty;

        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA; 

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

        public string Module = "IssueDetails";
    

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
                cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                //cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);
                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                }
                else
                {
                    if (Outputval == 2)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        msg = "Medicine is out of stock";
                        eObj.InsertionNotSuccessMessage(page, msg);
                        //Out of stock
                    }
                }
              
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertIssueDetails";
                eObj.InsertError();

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
                cmd.Parameters.Add("@Qty", SqlDbType.Real).Value = Qty;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = cmn.ConvertDatenow(DateTime.Now);

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    //Success
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                }
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateIssueDetails";
                eObj.InsertError();

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
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewIssueDetails";
                eObj.InsertError();                
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            } 
            return ds;

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
                    eObj.SavedSuccessMessage(page);
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteIssueDetails";
                eObj.InsertError();

               
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
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetMedicineIDByUniqueID";
                eObj.InsertError();


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

        #region Get Quantity In Stock

        public string GetQtyByMedicineName(string MedName)
        {
            string Qty = string.Empty;
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetQtyByMedicineName]";

                cmd.Parameters.Add("@MedName", SqlDbType.NVarChar, 255).Value = MedName;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                object qtyInIssueDT = cmd.ExecuteScalar();
                if (qtyInIssueDT != null)
                {
                    Qty = qtyInIssueDT.ToString();
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "GetQtyByMedicineName";
                eObj.InsertError();


            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }
            }

            return Qty;
        }
        #endregion Get Quantity In Stock

        #region Get Issue Details By UniqueID

        public DataSet GetIssueDetailsByUniqueID(string UniqueID)
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
                cmd.CommandText = "[GetIssueDetailsByUniqueID]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);


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
                eObj.Method = "GetIssueDetailsByUniqueID";
                eObj.InsertError();

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return ds;
        }

        #endregion Get Issue Details By UniqueID


        #endregion Methods

    }
}