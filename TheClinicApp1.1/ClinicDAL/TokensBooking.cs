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
        ErrorHandling eObj = new ErrorHandling();
        #endregion Global Variables

        //Property
        #region TokenProperty        
        
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


        #region GetSearchBoxData
        public DataTable GetSearchBoxData()
        {

            DataTable dt = null;
            SqlConnection con = null;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand("GetSearchBoxData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            dt = new DataTable();
            adapter.Fill(dt);
            con.Close();
            return dt;

        }
        #endregion GetSearchBoxData



        #region GetPatientDetails

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
                if (con != null)
                {
                    con.Dispose();
                }

            }
        }

        #endregion GetPatientDetails



        #region GetPatientTokenDetailsbyID

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
                if (con != null)
                {
                    con.Dispose();
                }

            }
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

                 return ds;
                

            }

            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }

            }

        
    
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
            
            DateTime now = DateTime.Now;
            dbConnection dcon = new dbConnection();
            con = dcon.GetDBConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[InsertTokens]";

          
            cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
            cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(PatientID);
        
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(ClinicID);
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 255).Value = CreatedBy;
          

            SqlParameter OutparmItemId = cmd.Parameters.Add("@TokenNo", SqlDbType.Int);
            OutparmItemId.Direction = ParameterDirection.Output;

           

            cmd.ExecuteNonQuery();
            TokenNo = Convert.ToInt32(OutparmItemId.Value);
           
                 
             }

            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
            }

            finally
            {


                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.InsertionSuccessMessage(page);


                if (con != null)
                {
                    con.Dispose();
                }

            }
            return TokenNo;
         




        }
        #endregion InsertToken

        #region ViewToken
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

            //cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;
            //cmd.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = DateTime;
            cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
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
                if (con != null)
                {
                    con.Dispose();
                }

            }

        }

        #endregion ViewToken


        #region DoctorViewToken
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
                cmd.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = Guid.Parse(DoctorID);
                cmd.Parameters.Add("@DateTime", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
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
                if (con != null)
                {
                    con.Dispose();
                }

            }

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
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Dispose();
                }
            }



            
        }

        #endregion DeleteToken

     
        
        #endregion Token_Methods


        
    



    }
}