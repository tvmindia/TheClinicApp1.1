using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class Stocks
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;  

        #endregion Global Variables

        #region constructor

        public Stocks()
        {

             MedicineID  = Guid.NewGuid();
        }
        public Stocks(Guid MedicineID1)
        {

            // Guid ex = Guid.NewGuid();

            MedicineID = MedicineID1;


        }

        #endregion constructor

        #region Property

        public string Module = "Stocks";
     
        public Guid MedicineID
        {
            get;
            set;
        }
        public string ClinicID
        {
            get;
            set;
        }
        public string CategoryID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;

        }
        public string Unit
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
        public int ReOrderQty
        {
            get;
            set;
        }

        public string  MedCode
        {
            get;
            set;
        }



        #endregion Property

        #region Methods    

        #region Get Medicine Details By MedicineID
        /// <summary>
        /// To get the medicine details by  passing Medicine ID
        /// </summary>
        /// <param name="medid"></param>
        /// <returns></returns>
        public DataSet GetMedicineDetailsByMedicineID(Guid medid)
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
                cmd.CommandText = "GetMedicineDetailsByMedicineID";

                cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = medid;
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
                eObj.Method = "GetMedicineDetailsByMedicineID";
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

        #endregion Get Medicine Details By MedicineID

        #region Get Quantity By Medicine Name
        /// <summary>
        /// To get the existing stock quantiy of the particular medicines by medicine name 
        /// </summary>
        /// <param name="MedicineName"></param>
        /// <returns></returns>
        public string GetQtyByMedicineName(string MedicineName)
        {
            string QtyInStock = string.Empty;
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[GetQtyByMedicineName]";

                cmd.Parameters.Add("@MedicineName", SqlDbType.NVarChar, 255).Value = MedicineName;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                object ID = cmd.ExecuteScalar();
                if (ID != null)
                {
                    QtyInStock = ID.ToString();
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

            return QtyInStock;
        }



        #endregion Get Quantity By Medicine Name

        #region Validate Medicine Name
        /// <summary>
        /// To check MEdicine name duplication by passing input medicine name
        /// </summary>
        /// <param name="CheckMedicine"></param>
        /// <returns></returns>
        public bool ValidateMedicineName(string CheckMedicine)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckMedicineNameDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MedicineName", SqlDbType.VarChar,255).Value = CheckMedicine;
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
                eObj.Method = "ValidateMedicineName";
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

        #endregion Validate Medicine Name

        #region Validate Medicine Code
        /// <summary>
        /// To check MEdicine code duplication by passing input medicine code
        /// </summary>
        /// <param name="InputMedicineCode"></param>
        /// <returns></returns>
        public bool ValidateMedicineCode(string InputMedicineCode)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckMedicineCodeDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MedicineCode", SqlDbType.VarChar, 20).Value = InputMedicineCode;
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
                eObj.Method = "ValidateMedicineCode";
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

        #endregion Validate Medicine Code

        #region View Categories
        /// <summary>
        /// Category details from caregories table
        /// </summary>
        /// <returns></returns>
        public DataSet ViewCategories()
        {

            dbConnection dcon = null;
            DataSet dsCategories = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[ViewCategory]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                dsCategories = new DataSet();
                sda.Fill(dsCategories);              

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewCategories";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return dsCategories;

        }

        #endregion View Categories

        #region View Units
        /// <summary>
        /// unit details from unit table
        /// </summary>
        /// <returns></returns>
        public DataSet ViewUnits()
        {

            dbConnection dcon = null;
            DataSet dsunits = null;
            SqlDataAdapter sda = null;
            try
            {

                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ViewUnits";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                dsunits = new DataSet();
                sda.Fill(dsunits);               

            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "ViewUnits";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
            return dsunits;
        }

        #endregion View Units

        #region SearchBoxMedicine
        /// <summary>
        /// To Get the medicine name 
        /// stockinDetails and stockoutDetails for  Pages
        /// </summary>
        /// <returns></returns>
        public DataTable SearchBoxMedicine()
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SearchBoxMedicine", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion SearchBoxMedicine

        #region SearchWithName
        /// <summary>
        /// Searching Medicine details by passing Medicine Name 
        /// </summary>
        /// <param name="SearchName"></param>
        /// <returns></returns>
        public DataTable GetMedcineDetails(string SearchName)
        {
            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("SearchMedicineWithName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = SearchName;
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion SearchWithName
        
        #region InsertMedicines
        /// <summary>
        /// Inserting Medicines , initially quantity of medicine will be ZERO (assining in SQL)
        /// In SQL, Duplication of medicines will be checked and corresponding messages will be given.
        /// </summary>
        public void InsertMedicines()
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertMedicines]";

                cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = MedicineID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(CategoryID);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 15).Value = Unit;     
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;              
                cmd.Parameters.Add("@ReOrderQty", SqlDbType.Real).Value = Convert.ToInt32( ReOrderQty);
                cmd.Parameters.Add("@MedCode", SqlDbType.NVarChar, 20).Value = MedCode;

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
                else if (Outputval == 2)
                {
                    //Already exists! Medicine Code
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.AlreadyExistsMessage(page);
                }
                else
                {
                    if(Outputval == 0) 
                    {
                        //Already exists! Medicine name
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.AlreadyExistsMessage(page);
                    }
                }
            }

            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "InsertMedicines";
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

        #endregion InsertMedicines

        #region UpdateMedicines
        /// <summary>
        /// updating Medicines with respect to the Medicine ID and Clinic ID
        /// In SQL, Duplication is checked while updating and returns coresponding messages 
        /// </summary>
        /// <param name="MedicineID"></param>
        public void UpdateMedicines(string MedicineID )
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateMedicines]";
                cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(MedicineID);
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(CategoryID);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar, 15).Value = Unit;            
                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@ReOrderQty", SqlDbType.Real).Value = ReOrderQty;
                cmd.Parameters.Add("@MedCode", SqlDbType.NVarChar, 20).Value = MedCode;
           
                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;
                cmd.ExecuteNonQuery();
                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.SavedSuccessMessage(page);
                    //Success
                }
                else
                {
                    if (Outputval == 2)
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
                eObj.Method = "UpdateMedicines";
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

        #endregion UpdateMedicines 

        #region DeleteMedicines
        /// <summary>
        /// Deleting Medicines with respect to Medicine ID and Clinic ID 
        /// </summary>
        /// <param name="MedicineID"></param>
        public void DeleteMedicines(Guid MedicineID)
        {
            dbConnection dcon = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteMedicines]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
                cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = MedicineID;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();
                if (Output.Value.ToString() == "")
                {
                    //not successfull   
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.DeletionNotSuccessMessage(page);
                }
                else
                {
                    //successfull
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.DeleteSuccessMessage(page);
                }
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteMedicines";
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

        #endregion InsertMedicines

        #region View_Out_of_Stock_MedicineList
        /// <summary>
        /// TO find the Out of Stock Medicine List with Respect to clinics
        /// </summary>
        /// <returns></returns>
        public DataSet ViewOutofStockMedicines()
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
                cmd.CommandText = "[ViewOutofStockMedicines]";

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
                eObj.Method = "ViewOutofStockMedicines";
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

#endregion Out_of_Stock_MedicineList

        #region Search And View MedicineStock
        /// <summary>
        /// VIEW MEDCINES IF SEARCH ITEM IS NULL ....OTHERWISE SEARCH
        /// 
        /// </summary>
        /// <param name="String"></param>
        /// <returns></returns>
        public DataSet SearchMedicineStock(string String )//In case of view : Empty string has to be passed
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
                cmd.CommandText = "[SearchMedicineStock]";

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
                eObj.Method = "SearchMedicineStock";
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


        #endregion Search And View MedicineStock

        #region CheckMedicineIDIsUsed
        /// <summary>
        /// Before Deleting to ensure that the MedicineID is used anywhere.
        /// Checking Prescription Details,Receipt Details and Issue Detials
        /// </summary>
        /// <returns>Retruns a boolean value</returns>
        public bool CheckMedicineIDIsUsed()
        {
            bool isUsed = false;
     
            SqlConnection con = null;
            try
            {
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckMedicineIDIsUsed", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = MedicineID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);

                SqlParameter outflag = cmd.Parameters.Add("@Cnt", SqlDbType.Bit);
                outflag.Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                object ID = outflag.Value;

              isUsed =  Convert.ToBoolean(ID);
               
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "CheckMedicineIDIsUsed";
                eObj.InsertError();
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return isUsed;
        }
        #endregion CheckMedicineIDIsUsed  

        #endregion Methods
    }
}