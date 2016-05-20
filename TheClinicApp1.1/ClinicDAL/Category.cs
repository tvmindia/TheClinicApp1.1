
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

        #region Public Properties

        ErrorHandling eObj = new ErrorHandling();
        
        public string Module = "Category";

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

        /// <summary>
        /// User id of logined user
        /// </summary>
        public Guid usrid
        {
            get;
            set;
        }



        #endregion Public Properties

        #region Methods

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
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
               
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
                ErrorHandling eObj = new ErrorHandling();
        
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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
                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = usrid;
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