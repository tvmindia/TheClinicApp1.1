
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

#endregion Included Namespaces

namespace TheClinicApp.ClinicDAL
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

                cmd.Parameters.Add("@CategoryID", SqlDbType.UniqueIdentifier).Value = CategoryID;
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
                    //Success
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

        #endregion Add New Category

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

        #endregion Validate Category Name

        #endregion Methods

    }
}