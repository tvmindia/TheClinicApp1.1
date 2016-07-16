
#region CopyRight

//Author      : SHAMILA T P
//Created Date: Mar-16-2016

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

#endregion Included Namespaces

namespace TheClinicApp1._1.ClinicDAL
{
    public class Category
    {
        #region constructor

        public Category()
        {

            CategoryID = Guid.NewGuid();
        }
        public Category(Guid CategoryID1)
        {
            CategoryID = CategoryID1;
        }

        #endregion constructor

        #region Global Variables

        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        public string Module = "Category";

        #endregion Global Variables

        #region Public Properties

        public Guid CategoryID
        {
            get;
            set;
        }

        public Guid ClinicID
        {
            get;
            set;
        }

        public string CreatedBy
        {
            get;
            set;
        }

        public string CategoryName
        {
            get;
            set;
        }

        public string UpdatedBy
        {
            get;
            set;
        }

     



        #endregion Public Properties

        #region Methods

        #region CATEGORY View Search Paging

        public string ViewAndFilterCategories(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterCategory";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Categories");

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
                eObj.Method = "ViewAndFilterCategories";
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

        #endregion CATEGORY View Search Paging



        #region Add New Category
        public void AddNewCategory()
        {
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertCategories]";

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = CategoryName; 

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                cmd.ExecuteNonQuery();

                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;   //Success
                    eObj.SavedSuccessMessage(page);
                  
                }
                else
                {
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //Already exists!
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
                eObj.Method = "AddNewCategory";

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

        #endregion Add New Category

        #region Update Category
        public void UpdateCategory()
        {
            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateCategories]";

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = CategoryName; 

                cmd.Parameters.Add("@Status", SqlDbType.Int);
                cmd.Parameters["@Status"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int Outputval = (int)cmd.Parameters["@Status"].Value;

                if (Outputval == 1)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //Success
                    eObj.SavedSuccessMessage(page);
                   
                }
                else
                {
                    if (Outputval == 0)
                    {
                        var page = HttpContext.Current.CurrentHandler as Page;  //Already exists!
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
                eObj.Method = "UpdateCategory";

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


        #endregion Update Category

        #region Validate Category Name
        public bool ValidateCategoryName(string CheckCategory)
        {
            bool flag;
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("CheckCategoryNameDuplication", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 255).Value = CheckCategory;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
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
                eObj.Method = "ValidateCategoryName";

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

        #endregion Validate Category Name

        #region View All Category

        public DataTable ViewAllCategory()
        {
            SqlConnection con = null;
            DataTable dtUsers = null;
            try
            {
                dtUsers = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewCategory", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtUsers);
                
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewAllCategory";

                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtUsers;
        }

        #endregion View All Category

        #region Delete Category By CategoryID


        public string DeleteCategoryByIdForWM()
        {
            string result = string.Empty;

            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteCategories";
                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;

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
                eObj.Method = "DeleteCategoryById";

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


        public void DeleteCategoryById()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "DeleteCategories";
                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value =CategoryID;

                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Output);
                cmd.ExecuteNonQuery();
                if (Output.Value.ToString() == "")
                {
                    var page = HttpContext.Current.CurrentHandler as Page;  //not successfull  
                    eObj.DeletionNotSuccessMessage(page);

                }
                else
                {
                    var page = HttpContext.Current.CurrentHandler as Page; //successfull
                    eObj.DeleteSuccessMessage(page);

                }

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "DeleteCategoryById";

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

        #endregion  Delete Category By CategoryID

        #region View Category By CategoryID

        public DataSet ViewCategoryByCategoryIDForWM()
        {
            SqlConnection con = null;
            DataSet dsCatgry = null;
            try
            {
                dsCatgry = new DataSet();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetCategoryDetailsByID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dsCatgry);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewCategoryByCategoryIDForWM";

                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dsCatgry;
        }

        public DataTable ViewCategoryByCategoryID()
        {
            SqlConnection con = null;
            DataTable dtRoles = null;
            try
            {
                dtRoles = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetCategoryDetailsByID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewCategoryByCategoryID";

                eObj.InsertError();
               
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtRoles;
        }

        #endregion View Category By CategoryID


        #region View Medicines By CategoryID

        public DataTable ViewMedicinesByCategoryID()
        {
            SqlConnection con = null;
            DataTable dtRoles = null;
            try
            {
                dtRoles = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewMedicinesByCategoryID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtRoles);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID;
                eObj.Method = "ViewMedicinesByCategoryID";

                eObj.InsertError();
                
            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

            return dtRoles;
        }

        #endregion View Medicines By CategoryID
       

        #endregion Methods

    }
}