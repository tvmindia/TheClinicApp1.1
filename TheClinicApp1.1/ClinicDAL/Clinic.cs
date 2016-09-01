using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace TheClinicApp1._1.ClinicDAL
{
    public class Clinic
    {
        #region Global Variables
        ErrorHandling eObj = new ErrorHandling();
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        public string Module = "Clinic";
        #endregion Global Variables

        #region constructor

        public Clinic()
        {

            ClinicID = Guid.NewGuid();

        }
        public Clinic(Guid ClinicID1)
        {
            ClinicID = ClinicID1;
            
        }

        #endregion constructor

        #region Property
        public Guid ClinicID
        {
            set;
            get;
        }
        public Guid GroupID
        {
            set;
            get;
        }
        public string Name
        {
            set;
            get;
        }
        public string Location
        {
            set;
            get;
        }
        public string Address
        {
            set;
            get;
        }
        public string Phone
        {
            set;
            get;
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


        #region Method

        #region InsertClinics
        public void InsertClinics()
        {


            dbConnection dcon = null;

            try
            {




                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                cmd.Parameters.Add("@GroupID", SqlDbType.UniqueIdentifier).Value = GroupID;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = Location;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = Phone;

                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;

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
                    eObj.InsertionNotSuccessMessage(page);


                }
                else
                {
                    //successfull

                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.InsertionSuccessMessage(page);


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



        #endregion InsertClinics

        #region UpdateClinics
        public void UpdateClinics()
        {


            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[UpdateClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =ClinicID;
              
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = Name;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar, 255).Value = Location;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Address;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 255).Value = Phone;

              
                cmd.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = UpdatedDate;



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



        #endregion UpdateClinics

        #region DeleteClinics
        public void DeleteClinics()
        {


            dbConnection dcon = null;

            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteClinics]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =  ClinicID;


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



        #endregion DeleteClinics

        #region Get Clinic Details By ClinicID

        public DataSet GetClinicDetailsByClinicID()
        {
            SqlConnection con = null;
            DataSet dtClinic = null;
            try
            {
                dtClinic = new DataSet();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetClinicDetailsByUserID", con);

                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = UserID;
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(dtClinic);

            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

                eObj.Description = ex.Message;
                eObj.Module = Module;

                eObj.UserID = UA.UserID; ;
                eObj.Method = "GetClinicDetailsByUserID";

                eObj.InsertError();

            }
            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }
            return dtClinic;
        }


        #endregion Get Clinic Details By ClinicID

        #region ViewClinic
        public DataSet ViewClinic(string ClinicID)
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
                cmd.CommandText = "[ViewClinic]";

                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value =Guid.Parse(ClinicID);

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

        #endregion ViewClinic

        #region Clinic View Search Paging

        public string ViewAndFilterClinic(string searchTerm, int pageIndex, int PageSize)
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
                cmd.CommandText = "ViewAndFilterClinic";

                //cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;


                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
                cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.Add("@RecordCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                sda = new SqlDataAdapter();
                cmd.ExecuteNonQuery();
                sda.SelectCommand = cmd;
                ds = new DataSet();
                sda.Fill(ds, "Users");

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
                eObj.Method = "ViewAndFilterClinic";
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

        #endregion Clinic View Search Paging

        #region GetLogo
         public byte[] GetClinicLogo()
        {


            dbConnection dcon = null;
            SqlCommand cmd = null;
            SqlDataReader rd = null;
            byte[] imageproduct = null;
            try
            {
                dcon = new dbConnection();
                dcon.GetDBConnection();
                cmd = new SqlCommand();
                cmd.Connection = dcon.SQLCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[SelectLogoByClinicID]";
                cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                rd = cmd.ExecuteReader();
                if ((rd.Read()) && (rd.HasRows) && (rd["Logo"] != DBNull.Value))
                {
                    imageproduct = (byte[])rd["Logo"];
                }
            }
            catch (Exception ex)
            {
                UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                eObj.Description = ex.Message;
                eObj.Module = Module;
                eObj.UserID = UA.UserID;
                eObj.Method = "SelectLogoByClinicID";
                eObj.InsertError();
            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    rd.Close();
                    dcon.DisconectDB();
                }
            }
            return imageproduct;

        }
         public byte[] GetClinicLogosmall()
         {


             dbConnection dcon = null;
             SqlCommand cmd = null;
             SqlDataReader rd = null;
             byte[] imageproduct = null;
             try
             {
                 dcon = new dbConnection();
                 dcon.GetDBConnection();
                 cmd = new SqlCommand();
                 cmd.Connection = dcon.SQLCon;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "[SelectLogoByClinicID]";
                 cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                 rd = cmd.ExecuteReader();
                 if ((rd.Read()) && (rd.HasRows) && (rd["Logosmall"] != DBNull.Value))
                 {
                     imageproduct = (byte[])rd["Logosmall"];
                 }
             }
             catch (Exception ex)
             {
                 UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                 eObj.Description = ex.Message;
                 eObj.Module = Module;
                 eObj.UserID = UA.UserID;
                 eObj.Method = "SelectLogoByClinicID";
                 eObj.InsertError();
             }

             finally
             {
                 if (dcon.SQLCon != null)
                 {
                     rd.Close();
                     dcon.DisconectDB();
                 }
             }
             return imageproduct;

         }
         
        #endregion GetLogo


        #endregion Method



    }
}