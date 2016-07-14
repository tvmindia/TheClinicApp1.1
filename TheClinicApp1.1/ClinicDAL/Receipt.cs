using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class Receipt
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;     

        #endregion Global Variables

        #region constructor
        public Receipt()
        {         
            ReceiptID = Guid.NewGuid();
        }
        public Receipt(Guid receipiID)
        {
            ReceiptID = receipiID;            
        }

        #endregion constructor

        #region Property

        public string Module = "Receipt";
       
        public Guid ReceiptID
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string RefNo1
        {
            get;
            set;
        }
        public string RefNo2
        {
            get;
            set;

        }
        public DateTime Date
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

        #endregion Property

        #region Methods
        
        #region InsertReceiptHeader
        /// <summary>
        /// inserting receipt Header
        /// </summary>
        public void InsertReceiptHeader()
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertReceiptHeader]";
                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = ReceiptID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@RefNo1", SqlDbType.NVarChar, 255).Value = RefNo1;
                cmd.Parameters.Add("@RefNo2", SqlDbType.NVarChar, 255).Value = RefNo2;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = Date;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;

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
                eObj.Method = "InsertReceiptHeader";
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

        #endregion InsertReceiptHeader

        #region UpdateReceiptHeader
        /// <summary>
        /// upadting receipt header 
        /// </summary>
        /// <param name="ReceiptID"></param>
        public void UpdateReceiptHeader(string ReceiptID)
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
                cmd.CommandText = "[UpdateReceiptHeader]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@RecepitID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ReceiptID);
                cmd.Parameters.Add("@RefNo1", SqlDbType.NVarChar, 255).Value = RefNo1;
                cmd.Parameters.Add("@RefNo2", SqlDbType.NVarChar, 255).Value = RefNo2;
                cmd.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = Date;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;

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
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavingFailureMessage(page);
                    }
                }

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateReceiptHeader";
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

        #endregion UpdateReceiptHeader

        #region View And Search ReceipteHeader
        /// <summary>
        /// searching in receipt header with the input string
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public DataSet ViewReceiptHeader(string String)
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
                cmd.CommandText = "[ViewReceiptHeader]";

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
                eObj.Method = "ViewReceiptHeader";
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


        #endregion ViewAndSearchReceipteHeader

        #region DeleteReceiptHeader
        /// <summary>
        /// Deleting Receipt Details with ReceiptID and ClinicID
        /// </summary>
        /// <param name="ReceiptID"></param>
        public int DeleteReceiptHeader(string ReceiptID)
        {
            dbConnection dcon = null;
             int Outputval = 0;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteReceiptHeader]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ReceiptID);

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                 Outputval = (int)cmd.Parameters["@Status"].Value;

                //if (Outputval == 1)
                //{
                //    //Success
                //    var page = HttpContext.Current.CurrentHandler as Page;                 
                //    eObj.DeleteSuccessMessage(page);
                //}

                //if (Outputval == 0)
                //{
                //    var page = HttpContext.Current.CurrentHandler as Page;
                //    eObj.DeletionNotSuccessMessage(page);
                //}


            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteReceiptHeader";
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

        #endregion InsertReceiptHeader               

        #region ReloadInsertData
        /// <summary>
        /// Reload the inserted datats into controls
        /// </summary>
        /// <returns></returns>
        public DataSet InsertReloaded()
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
                cmd.CommandText = "[GetReceiptInsertReload]";
                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = ReceiptID;
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
                eObj.Method = "InsertReloaded";
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


        #endregion ReloadInsertData

        #region Get Medicine Details By MedicineName
        /// <summary>
        /// Get Medicine Detials by Pasing Medicine name
        /// </summary>
        /// <param name="MedicineName"></param>
        /// <returns>Medicine category,unit medcode and quantity</returns>
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

        
        #region GetReceiptDetailsByReceiptID
        /// <summary>
        /// Get Recipt Details by Passing Receipt ID
        /// </summary>
        /// <param name="ReceiptID"></param>
        /// <returns></returns>
        public DataSet GetReceiptDetailsByReceiptID(string ReceiptID)
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
                cmd.CommandText = "[GetReceiptDetailsByReceiptID]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ReceiptID);


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
                eObj.Method = "GetReceiptDetailsByReceiptID";
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


        #endregion GetReceiptDetailsByReceiptID

        #region Get Receipt Header By ReceiptID
        /// <summary>
        /// Get Receipt HEader Details  by passing REceipt ID
        /// </summary>
        /// <param name="ReceiptID"></param>
        /// <returns></returns>
        public DataSet GetReceiptHeaderByReceiptID(string ReceiptID)
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
                cmd.CommandText = "[GetReceiptHeaderByReceiptID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ReceiptID);

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
                eObj.Method = "GetReceiptHeaderByReceiptID";
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

        #endregion Get Receipt Header ByReceiptID

        #endregion Methods
  
    }

    public class ReceiptDetails
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;  
        #endregion Global Variables

        #region constructor
        public ReceiptDetails()
        {

            // Guid ex = Guid.NewGuid();

            UniqueID = Guid.NewGuid();


        }
        public ReceiptDetails(Guid UniqueID)
        {

            // Guid ex = Guid.NewGuid();

            this.UniqueID = UniqueID;


        }
        #endregion constructor

        #region Property

        public string Module = "ReceiptDetails";
      
      
        public Guid UniqueID
        {
            get;
            set;
        }

        public Guid ReceiptID
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
        public int QTY
        {
            get;
            set;
        }


        #endregion Property

        #region Methods

        #region autofill
        /// <summary>
        /// used to bind medicine details,by passing medicine name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataSet GetMedCodeUnitCategory(string name)
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
                cmd.CommandText = "[GetMedCodeCategoryUnit]";

                cmd.Parameters.Add("@Medname", SqlDbType.NVarChar, 255).Value = name;
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
                eObj.Method = "GetMedCodeUnitCategory";
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


        #endregion autofill        

        #region Get Receipt Details By UniqueID
        /// <summary>
        /// to get the receipt Details by passing UniqueID from ReceiptDT table 
        /// </summary>
        /// <param name="UniqueID"></param>
        /// <returns></returns>
        public DataSet GetReceiptDetailsByUniqueID(string UniqueID)
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
                cmd.CommandText = "[GetReceiptDetailsByUniqueID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);

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
                eObj.Method = "GetReceiptDetailsByUniqueID";
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

        #endregion Get Receipt Details By UniqueID

        #region InsertReceiptDetails
        /// <summary>
        /// Inserting into Receipt Details
        /// </summary>
        public void InsertReceiptDetails()
        {
            dbConnection dcon = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertReceiptDetails]";

                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = UniqueID;
                cmd.Parameters.Add("@ReceiptID", SqlDbType.UniqueIdentifier).Value = ReceiptID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar, 255).Value = MedicineName;
                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 15).Value = Unit;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                cmd.Parameters.Add("@QTY", SqlDbType.Real).Value = QTY;

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
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavingFailureMessage(page);                        
                    }
                }
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertReceiptDetails";
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

        #endregion InsertReceiptDetails

        #region UpdateReceiptDetails
        /// <summary>
        /// Updating Receipt DEtails with respect to the passed UniqueID
        /// </summary>
        /// <param name="UniqueID"></param>
        public void UpdateReceiptDetails(string UniqueID)
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
                cmd.CommandText = "[UpdateReceiptDetails]";
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);                 
           
                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@QTY", SqlDbType.Real).Value = QTY;

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
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;

                        eObj.SavingFailureMessage(page);
                    }
                }
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "UpdateReceiptDetails";
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


        #endregion UpdateReceiptDetails

        #region ViewReceipteDetails
        /// <summary>
        /// To Get the Receipt Details with Clinic ID
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet ViewReceiptDetails()
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
                cmd.CommandText = "[ViewReceiptDetails]";
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
                eObj.Method = "ViewReceiptDetails";
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


        #endregion ViewReceiptDetails

        #region DeleteReceiptDetails
        /// <summary>
        ///  Deleting Receipt Details with UniqueID and ClinicID
        /// </summary>
        /// <param name="UniqueID"></param>
        public void DeleteReceiptDetails(string UniqueID )
        {

            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteReceiptDetails]";
                cmd.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(UniqueID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

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
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.SavingFailureMessage(page);
                    }
                }
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteReceiptDetails";
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

        #endregion DeleteReceiptDetails        

        #endregion Methods
    }
}