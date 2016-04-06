#region CopyRight

//Author      : Thomson K Varkey
//Created Date: Feb-22-2016

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

namespace TheClinicApp.ClinicDAL
{
    public class Doctor
    {
        ErrorHandling eObj = new ErrorHandling();

        #region GrabFileID
        #region Property
        public Guid PatientIdForFile
        {
            get;
            set;
        }

        #endregion Property
        #region Methods
        public DataTable GetFileIDUSingPatientID()
        {
            SqlConnection con = null;

            try
            {
                DateTime now = DateTime.Now;
                DataTable DtFileID = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetFileID", con);
                cmd.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientIdForFile;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DtFileID = new DataTable();
                adapter.Fill(DtFileID);
                return DtFileID;
            }
            catch (Exception ex)
            {
                //var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
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
        #endregion Methods
        #endregion GrabFileID

        #region Connectionstring
        dbConnection dcon = new dbConnection();
        #endregion Connectionstring

        #region ViewAllRegistration
        /// <summary>
        /// ***********
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRegistration()
        {
            SqlConnection con = null;

            try
            {
                DataTable dt = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewAllRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                //var page = HttpContext.Current.CurrentHandler as Page;
                //eObj.ErrorData(ex, page);
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
        #endregion ViewAllRegistration

        #region ViewDateRegistration
        public DataTable GetDateRegistration()
        {
            SqlConnection con = null;
            try
            {
                DateTime now = DateTime.Now;
                DataTable dt1 = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("ViewDateRegistration", con);
                cmd.Parameters.Add("@CreatedDate", SqlDbType.NVarChar, 50).Value = now.ToString("yyyy-MM-dd");
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt1 = new DataTable();
                adapter.Fill(dt1);
                return dt1;
            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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
        #endregion ViewDateRegistration

       

    }
    public class CaseFile
    {   
        ErrorHandling eObj = new ErrorHandling();

            private DateTime CreatedDateLocal;
            private DateTime UpdatedDateLocal;
            #region Constructors
            public CaseFile()
            {
                FileID = Guid.NewGuid();
            }
            public CaseFile(Guid FileID)
            {
                this.FileID = FileID;
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetFileDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                DataRow row = dt.NewRow();
                row = dt.Rows[0];
                PatientID = new Guid(row["PatientID"].ToString());
                ClinicID = new Guid(row["ClinicID"].ToString());
                FileDate = DateTime.Parse(row["FileDate"].ToString());
                FileNumber = row["FileNumber"].ToString();
                CreatedBy=row["CreatedBy"].ToString();
                CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                UpdatedBy = row["UpdatedBy"].ToString();
                UpdatedDateLocal = DateTime.Parse(row["UpdatedDate"].ToString());
                con.Close();
            }
            #endregion Constructors

            #region Fileproperty
        public Guid FileID
        {
            get;
            set;
        }
        public Guid PatientID
        {
            get;
            set;
        }
        public Guid ClinicID
        {
            get;
            set;
        }
        public DateTime FileDate
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
            get { return CreatedDateLocal; }
        }
        public string UpdatedBy
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get { return UpdatedDateLocal; }
        }
        public string FileNumber
        {
            get;
            set;
        }
        #endregion Fileproperty

            #region File Methods
        #region AddFile
        public void AddFile()
        {
            dbConnection dcon = new dbConnection();

            try
            {


                dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = dcon.SQLCon;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "[InsertNewFile]";
                pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                pud.Parameters.Add("@PatientID", SqlDbType.UniqueIdentifier).Value = PatientID;
                pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@FileNumber", SqlDbType.NVarChar, 50).Value = FileNumber;
                //pud.Parameters.Add("@image", SqlDbType.VarBinary).Value = image; 
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();

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
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);

            }

            finally
            {
                if (dcon.SQLCon != null)
                {
                    dcon.DisconectDB();
                }

            }
        }
        #endregion AddFile

        #region UpdateFile
        public void UpdateFile()
        {
            SqlConnection con = null;
            try
            {

                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand pud = new SqlCommand();
                pud.Connection = con;
                pud.CommandType = System.Data.CommandType.StoredProcedure;
                pud.CommandText = "UpdateFile";
                pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                pud.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                pud.Parameters.Add("@FileNumber", SqlDbType.NVarChar, 50).Value = FileNumber;
                //SqlParameter OutparamId = pud.Parameters.Add("@OutparamId", SqlDbType.SmallInt);
                //OutparamId.Direction = ParameterDirection.Output;
                SqlParameter Output = new SqlParameter();
                Output.DbType = DbType.Int32;
                Output.ParameterName = "@Status";
                Output.Direction = ParameterDirection.Output;
                pud.Parameters.Add(Output);
                pud.ExecuteNonQuery();
                if (int.Parse(Output.Value.ToString()) == -1)
                {
                    // ////not successfull
                    // var page = HttpContext.Current.CurrentHandler as Page;
                    //eObj.U
                }
                else
                {
                    //successfull
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.UpdationSuccessMessage(page);
                }


            }
            catch (Exception ex)
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                eObj.ErrorData(ex, page);
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
        #endregion UpdateFile
        #endregion File Methods

            #region Visit Class
        public class Visit
        {
            ErrorHandling eObj = new ErrorHandling();
            private DateTime CreatedDateLocal;
            private DateTime UpdatedDateLocal;

            #region Constructors
            public Visit(CaseFile caseFile) 
            {
                FileID = caseFile.FileID;
                ClinicID = caseFile.ClinicID;
            }
            public Visit()
            {
               VisitID = Guid.NewGuid();
            }
            public Visit(Guid VisitID)
            {
                this.VisitID = VisitID;
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetVisitDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                DataRow row = dt.NewRow();
                row = dt.Rows[0];
                FileID = new Guid(row["FileID"].ToString());
                DoctorID = new Guid(row["DoctorID"].ToString());
                ClinicID = new Guid(row["ClinicID"].ToString());
                Date = DateTime.Parse(row["Date"].ToString());
                Height = float.Parse(row["Height"].ToString());
                Weight = float.Parse(row["Weight"].ToString());
                Symptoms = row["Symptoms"].ToString();
                Bowel = row["Bowel"].ToString();
                Appettie = row["Appettie"].ToString();
                Micturation = row["Micturation"].ToString();
                Sleep = row["Sleep"].ToString();
                Cardiovascular = row["Cardiovascular"].ToString();
                Nervoussystem = row["Nervoussystem"].ToString();
                Musculoskeletal = row["Musculoskeletal"].ToString();
                Palloe = row["Palloe"].ToString();
                Icterus = row["Icterus"].ToString();
                Clubbing = row["Clubbing"].ToString();
                Cyanasis = row["Cyanasis"].ToString();           
                LymphGen = row["LymphGen"].ToString();
                Edima = row["Edima"].ToString();
                Diagnosys = row["Diagnosys"].ToString();
                Remarks = row["Remarks"].ToString();
                Pulse = row["Pulse"].ToString();
                Bp = row["Bp"].ToString();
                Tounge = row["Tounge"].ToString();
                Heart = row["Heart"].ToString();
                LymphClinic = row["LymphClinic"].ToString();
                RespRate = row["RespRate"].ToString();
                Others = row["Others"].ToString();
                if (row["PrescriptionID"] != DBNull.Value) { PrescriptionID = new Guid(row["PrescriptionID"].ToString()); }                
                CreatedBy=row["CreatedBy"].ToString();
                CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                UpdatedBy = row["UpdatedBy"].ToString();
                UpdatedDateLocal = DateTime.Parse(row["UpdatedDate"].ToString());
                con.Close();
            }
            #endregion Constructors

            #region VisitsProperty
            public Guid VisitID
            {
                get;
                set;
            }
            public Guid FileID
            {
                get;
                set;
            }
            public Guid DoctorID
            {
                get;
                set;
            }
            public Guid ClinicID
            {
                get;
                set;
            }
            public DateTime Date
            {
                get;
                set;
            }
            /// <summary>
            /// Personal details
            /// </summary>
            public float Height
            {
                get;
                set;
            }
            public float Weight
            {
                get;
                set;
            }
            public string Symptoms
            {
                get;
                set;
            }

            public string Bowel
            {
                get;
                set;
            }
            public string Appettie
            {
                get;
                set;
            }
            public string Micturation
            {
                get;
                set;
            }
            public string Sleep
            {
                get;
                set;
            }
            /// <summary>
            /// Systematic Examination
            /// </summary>
            public string Cardiovascular
            {
                get;
                set;
            }
            
            public string Nervoussystem
            {
                get;
                set;
            }
            
            public string Musculoskeletal
            {
                get;
                set;
            }
            /// <summary>
            /// General examination
            /// </summary>
            public string Palloe
            {
                get;
                set;
            }
            public string Icterus
            {
                get;
                set;
            }
            public string Clubbing
            {
                get;
                set;
            }
            public string Cyanasis
            {
                get;
                set;
            }
            public string LymphGen
            {
                get;
                set;
            }
            public string Edima
            {
                get;
                set;
            }
            /// <summary>
            /// Daignosys
            /// </summary>
           
            public string Diagnosys
            {
                get;
                set;
            }
            /// <summary>
            /// Remarks
            /// </summary>
            /// 
            public string Remarks
            {
                get;
                set;
            }
            /// <summary>
            /// Clinical Needs
            /// </summary>
            /// 
            public string Pulse
            {
                get;
                set;
            }
            public string Bp
            {
                get;
                set;
            }
            public string Tounge
            {
                get;
                set;
            }
            public string Heart
            {
                get;
                set;
            }
            public string LymphClinic
            {
                get;
                set;
            }
            public string RespRate
            {
                get;
                set;
            }
            public string Others
            {
                get;
                set;
            }
            public Guid PrescriptionID
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
                get { return CreatedDateLocal; }
            }
            public string UpdatedBy
            {
                get;
                set;
            }
            public DateTime UpdatedDate
            {
                get { return UpdatedDateLocal; }
            }
            #endregion Visitsproperty

            #region Visit Methods

            #region AddVisits
            public void AddVisits()
            {

                SqlConnection con = null;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand pud = new SqlCommand();
                    pud.Connection = con;
                    pud.CommandType = System.Data.CommandType.StoredProcedure;
                    pud.CommandText = "[InsertVisitList]";
                    pud.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    pud.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                    pud.Parameters.Add("@DoctorID", SqlDbType.UniqueIdentifier).Value = DoctorID;                   
                    pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
                    pud.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@Height", SqlDbType.Real).Value =Height;
                    pud.Parameters.Add("@Weight", SqlDbType.Real).Value = Weight;
                    pud.Parameters.Add("@Symptoms", SqlDbType.NVarChar, 255).Value = Symptoms;
                    pud.Parameters.Add("@Bowel", SqlDbType.NVarChar, 255).Value = Bowel;
                    pud.Parameters.Add("@Appettie", SqlDbType.NVarChar, 255).Value = Appettie;
                    pud.Parameters.Add("@Micturation", SqlDbType.NVarChar, 255).Value = Micturation;
                    pud.Parameters.Add("@Sleep", SqlDbType.NVarChar, 255).Value = Sleep;
                    pud.Parameters.Add("@Cardiovascular", SqlDbType.NVarChar, 255).Value = Cardiovascular;
                    pud.Parameters.Add("@Nervoussystem", SqlDbType.NVarChar, 255).Value = Nervoussystem;
                    pud.Parameters.Add("@Musculoskeletal", SqlDbType.NVarChar, 255).Value = Musculoskeletal;
                    pud.Parameters.Add("@Palloe", SqlDbType.NVarChar, 255).Value = Palloe;
                    pud.Parameters.Add("@Icterus", SqlDbType.NVarChar, 255).Value = Icterus;
                    pud.Parameters.Add("@Clubbing", SqlDbType.NVarChar, 255).Value = Clubbing;
                    pud.Parameters.Add("@Cyanasis", SqlDbType.NVarChar, 255).Value = Cyanasis;
                    pud.Parameters.Add("@LymphGen",SqlDbType.NVarChar,255).Value=LymphGen;
                    pud.Parameters.Add("@Edima",SqlDbType.NVarChar,255).Value=Edima;                   
                    pud.Parameters.Add("@Diagnosys", SqlDbType.NVarChar, 255).Value = Diagnosys;
                    pud.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255).Value = Remarks;
                    pud.Parameters.Add("@Pulse", SqlDbType.NVarChar, 255).Value = Pulse;
                    pud.Parameters.Add("@Bp", SqlDbType.NVarChar, 255).Value = Bp;
                    pud.Parameters.Add("@Tounge", SqlDbType.NVarChar, 255).Value = Tounge;
                    pud.Parameters.Add("@Heart", SqlDbType.NVarChar, 255).Value = Heart;
                    pud.Parameters.Add("@LymphClinic", SqlDbType.NVarChar, 255).Value = LymphClinic;
                    pud.Parameters.Add("@RespRate", SqlDbType.NVarChar, 255).Value = RespRate;
                    pud.Parameters.Add("@Others", SqlDbType.NVarChar, 255).Value = Others;
                    pud.Parameters.Add("@PrescriptionID", SqlDbType.UniqueIdentifier).Value = PrescriptionID;
                    pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value =CreatedBy;
                    pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = UpdatedBy;
                    pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                   
                    SqlParameter Output = new SqlParameter();
                    Output.DbType = DbType.Int32;
                    Output.ParameterName = "@Status";
                    Output.Direction = ParameterDirection.Output;
                    pud.Parameters.Add(Output);
                    pud.ExecuteNonQuery();

                    if (Output.Value.ToString() == "")
                    {
                       // not successfull   

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
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.ErrorData(ex, page);

                }

                finally
                {
                    if (con != null)
                    {
                        con.Dispose();
                    }

                }

            }
            #endregion AddVisits

            #region GetVisitsGrid
            public void GetVisits()
            {

                if (VisitID == Guid.Empty)
                {
                    throw new Exception("VisitID Is Empty!!");
                }
                dbConnection dcon = null;
                try
                {
                   
                    DateTime now = DateTime.Now;
                    DataTable GridVisits = null;
                    dcon = new dbConnection();
                    dcon.GetDBConnection();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = dcon.SQLCon;
                    cmd.CommandText = "GetVisitDetails";
                    cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    GridVisits = new DataTable();
                    adapter.Fill(GridVisits);
                    if(GridVisits.Rows.Count>0)
                    {
                        DataRow dr = GridVisits.Rows[0];
                        Height =float.Parse(dr["Height"].ToString());
                        Weight = float.Parse(dr["Weight"].ToString());
                        Symptoms = dr["Symptoms"].ToString();
                        Cardiovascular = dr["Cardiovascular"].ToString();
                        Nervoussystem = dr["Nervoussystem"].ToString();
                        Musculoskeletal = dr["Musculoskeletal"].ToString();
                        Palloe = dr["Palloe"].ToString();
                        Icterus = dr["Icterus"].ToString();
                        Clubbing = dr["Clubbing"].ToString();
                        Cyanasis = dr["Cyanasis"].ToString();
                        Bowel = dr["Bowel"].ToString();
                        Appettie = dr["Appettie"].ToString();
                        Micturation = dr["Micturation"].ToString();
                        Sleep = dr["Sleep"].ToString();
                        Diagnosys = dr["Diagnosys"].ToString();
                        Remarks = dr["Remarks"].ToString();
                        Pulse = dr["Pulse"].ToString();
                        Bp = dr["Bp"].ToString();
                        Tounge = dr["Tounge"].ToString();
                        Heart = dr["Heart"].ToString();
                        LymphGen = dr["LymphGen"].ToString();
                        LymphClinic = dr["LymphClinic"].ToString();
                        RespRate = dr["RespRate"].ToString();
                        Others = dr["Others"].ToString();

                    }
                    
                   }
                catch (Exception ex)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.ErrorData(ex, page);
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
            public DataTable GetGridVisits(Guid FileID)
            {
               
                if (FileID == Guid.Empty)
                {
                    throw new Exception("FileID Is Empty!!");
                }
                SqlConnection con = null;
                try
                {
                   
                    DateTime now = DateTime.Now;
                    DataTable GridBindVisits = new DataTable();
                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand cmd = new SqlCommand("ViewVisitListUsingFileID", con);
                    cmd.Parameters.Add("@FileID", SqlDbType.UniqueIdentifier).Value = FileID;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    GridBindVisits = new DataTable();
                    adapter.Fill(GridBindVisits);
                    con.Close();
                    return GridBindVisits;
                }
                catch (Exception ex)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.ErrorData(ex, page);
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
            #endregion GetVisitsGrid

            //#region AddPrescriptionDT
            //public void AddPrescriptionDT()
            //{
            //    SqlConnection con = null;
            //    try
            //    {

            //        dbConnection dcon = new dbConnection();
            //        con = dcon.GetDBConnection();
            //        SqlCommand pud = new SqlCommand();
            //        pud.Connection = con;
            //        pud.CommandType = System.Data.CommandType.StoredProcedure;
            //        pud.CommandText = "[InsertPrescriptionDT]";
            //        pud.Parameters.Add("@UniqueID", SqlDbType.UniqueIdentifier).Value = UniqueID;
            //        pud.Parameters.Add("@PrescriptionID", SqlDbType.UniqueIdentifier).Value = PrescriptionID;
            //        pud.Parameters.Add("@ClinicID", SqlDbType.UniqueIdentifier).Value = ClinicID;
            //        pud.Parameters.Add("@MedicineID", SqlDbType.UniqueIdentifier).Value = MedicineID;
            //        pud.Parameters.Add("@Qty", SqlDbType.Real).Value = double.Parse(Qty);
            //        pud.Parameters.Add("@Unit", SqlDbType.Real).Value = double.Parse(Unit);
            //        pud.Parameters.Add("@Dosage", SqlDbType.Real).Value = double.Parse(Dosage);
            //        pud.Parameters.Add("@Timing", SqlDbType.NVarChar, 10).Value = Timing;
            //        pud.Parameters.Add("@Days", SqlDbType.Int).Value = Days;
            //        pud.Parameters.Add("@CreatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
            //        pud.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
            //        pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
            //        pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
            //        //pud.Parameters.Add("@image", SqlDbType.VarBinary).Value = image; 
            //        SqlParameter Output = new SqlParameter();
            //        Output.DbType = DbType.Int32;
            //        Output.ParameterName = "@Status";
            //        Output.Direction = ParameterDirection.Output;
            //        pud.Parameters.Add(Output);
            //        pud.ExecuteNonQuery();

            //        if (Output.Value.ToString() == "")
            //        {
            //            //not successfull   

            //            var page = HttpContext.Current.CurrentHandler as Page;
            //            eObj.InsertionNotSuccessMessage(page);

            //        }
            //        else
            //        {
            //            //successfull

            //            var page = HttpContext.Current.CurrentHandler as Page;
            //            eObj.InsertionSuccessMessage(page);


            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        var page = HttpContext.Current.CurrentHandler as Page;
            //        eObj.ErrorData(ex, page);

            //    }

            //    finally
            //    {
            //        if (con != null)
            //        {
            //            con.Dispose();
            //        }

            //    }
            //}
            //#endregion AddPrescriptionDT

            #region UpdateVisits
            /// <summary>
            /// Updates the Visits
            /// </summary>
            public void UpdateVisits()
            {
                SqlConnection con = null;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();
                    SqlCommand pud = new SqlCommand();
                    pud.Connection = con;
                    pud.CommandType = System.Data.CommandType.StoredProcedure;
                    pud.CommandText = "UpdateVisits";
                    pud.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                    pud.Parameters.Add("@Date", SqlDbType.DateTime).Value = DateTime.Now;
                    pud.Parameters.Add("@Height", SqlDbType.Real).Value = Height;
                    pud.Parameters.Add("@Weight", SqlDbType.Real).Value = Weight;
                    pud.Parameters.Add("@Symptoms", SqlDbType.NVarChar, 255).Value = Symptoms;
                    pud.Parameters.Add("@Cardiovascular", SqlDbType.NVarChar, 255).Value = Cardiovascular;
                    pud.Parameters.Add("@Nervoussystem", SqlDbType.NVarChar, 255).Value = Nervoussystem;
                    pud.Parameters.Add("@Musculoskeletal", SqlDbType.NVarChar, 255).Value = Musculoskeletal;
                    pud.Parameters.Add("@Palloe", SqlDbType.NVarChar, 255).Value = Palloe;
                    pud.Parameters.Add("@Icterus", SqlDbType.NVarChar, 255).Value = Icterus;
                    pud.Parameters.Add("@Clubbing", SqlDbType.NVarChar, 255).Value = Clubbing;
                    pud.Parameters.Add("@Cyanasis", SqlDbType.NVarChar, 255).Value = Cyanasis;
                    pud.Parameters.Add("@Bowel", SqlDbType.NVarChar, 255).Value = Bowel;
                    pud.Parameters.Add("@Appettie", SqlDbType.NVarChar, 255).Value = Appettie;
                    pud.Parameters.Add("@Micturation", SqlDbType.NVarChar, 255).Value = Micturation;
                    pud.Parameters.Add("@Sleep", SqlDbType.NVarChar, 255).Value = Sleep;
                    pud.Parameters.Add("@Diagnosys", SqlDbType.NVarChar, 255).Value = Diagnosys;
                    pud.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255).Value = Remarks;
                    pud.Parameters.Add("@UpdatedBY", SqlDbType.NVarChar, 255).Value = "Thomson";
                    pud.Parameters.Add("@UpdatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    //SqlParameter OutparamId = pud.Parameters.Add("@OutparamId", SqlDbType.SmallInt);
                    //OutparamId.Direction = ParameterDirection.Output;
                    SqlParameter Output = new SqlParameter();
                    Output.DbType = DbType.Int32;
                    Output.ParameterName = "@Status";
                    Output.Direction = ParameterDirection.Output;
                    pud.Parameters.Add(Output);
                    pud.ExecuteNonQuery();
                    if (int.Parse(Output.Value.ToString()) == -1)
                    {
                        // ////not successfull
                        // var page = HttpContext.Current.CurrentHandler as Page;
                        //eObj.U
                    }
                    else
                    {
                        //successfull
                        var page = HttpContext.Current.CurrentHandler as Page;
                        eObj.UpdationSuccessMessage(page);
                    }


                }
                catch (Exception ex)
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    eObj.ErrorData(ex, page);
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
            #endregion UpdateVisits

            #region SearchPatientWithName
            public DataTable SearchPatientWithName()
            {
                DataTable dt = null;
                SqlConnection con = null;
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("SearchPatientWithName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@VisitID", SqlDbType.UniqueIdentifier).Value = VisitID;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                dt = new DataTable();
                adapter.Fill(dt);
                con.Close();
                return dt;

            }
            #endregion SearchPatientWithName

            #region Get Visit List with Name for Mobile
            /// <summary>
            /// Get Visit List with Name for Mobile app to upload image
            /// </summary>
            /// <returns>Datatable with details from table</returns>
            public DataTable GetVisitListforMobile()
            {
                DataTable dt = new DataTable();
                SqlConnection con = null;
                SqlDataAdapter daObj;
                try
                {

                    dbConnection dcon = new dbConnection();
                    con = dcon.GetDBConnection();

                    SqlCommand cmdSelect = new SqlCommand("GetVisitListForMobile", con);
                    cmdSelect.CommandType = CommandType.StoredProcedure;
                    //cmdSelect.Parameters.AddWithValue("@DoctorID", DoctorID);

                    daObj = new SqlDataAdapter(cmdSelect);
                    
                    daObj.Fill(dt);

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

                return dt;
            }
            #endregion Get Visit List with Name for Mobile

            #endregion Visit Methods

            #region Visit Attachment Class
            public class VisitAttachment
                {
                    ErrorHandling eObj = new ErrorHandling();
                    private DateTime CreatedDateLocal;

                    #region Constructors
                    public VisitAttachment(Visit visit){
                        VisitID = visit.VisitID;
                        ClinicID = visit.ClinicID;
                    }
                    public VisitAttachment()
                    {
                        AttachID = Guid.NewGuid();
                    }
                    public VisitAttachment(Guid AttachID)
                    {
                        this.AttachID = AttachID;
                        DataTable dt = null;
                        SqlConnection con = null;
                        dbConnection dcon = new dbConnection();
                        con = dcon.GetDBConnection();
                        SqlCommand cmd = new SqlCommand("GetVisitAttachDetails", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@AttachID", SqlDbType.UniqueIdentifier).Value = AttachID;
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = cmd;
                        dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count == 0) { throw new Exception("No such ID"); }
                        DataRow row = dt.NewRow();
                        row = dt.Rows[0];
                        VisitID = new Guid(row["VisitID"].ToString());
                        ClinicID = new Guid(row["ClinicID"].ToString());
                        Description = row["Description"].ToString();
                        Attachment = row["Attachment"];
                        Name = row["Name"].ToString();
                        this.Type = row["Type"].ToString();
                        Size=row["Size"].ToString();
                        CreatedBy=row["CreatedBy"].ToString();
                        CreatedDateLocal = DateTime.Parse(row["CreatedDate"].ToString());
                        con.Close();
                    }
                    #endregion Constructors

                    #region VisitAttachmentsProperty
                    public Guid AttachID
                    {
                        get;
                        set;
                    }
                    public Guid VisitID
                    {
                        get;
                        set;
                    }
                    public Guid ClinicID
                    {
                        get;
                        set;
                    }
                    public string Description
                    {
                        get;
                        set;
                    }
                    public object Attachment
                    {
                        get;
                        set;
                    }
                    public string Name
                    {
                        get;
                        set;
                    }
                    public string Type
                    {
                        get;
                        set;
                    }
                    public string Size
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
                        get { return CreatedDateLocal;}
                    }
                    #endregion VisitAttachmentsProperty
            
                    #region VisitAttachment methods
                    #region FileAttachment
                    public int InsertFileAttachment(Boolean isFromMobile = false, string userName = "")
                    {
                        if (AttachID == null)
                        {
                            throw new Exception("AttachID propery is not set!!");
                        }
                        if(VisitID==null){
                            throw new Exception("VisitID propery is not set!!");
                        }
                        if (ClinicID == null)
                        {
                            throw new Exception("ClinicID propery is not set!!");
                        }
                        int result = 0;
                        SqlConnection con = null;
                        try
                        {
                            dbConnection dcon = new dbConnection();
                            con = dcon.GetDBConnection();
                            UIClasses.Const Const = new UIClasses.Const();

                            SqlCommand cmdInsert = new SqlCommand("FileAttachmentInsert", con);
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            if (isFromMobile)
                            {
                                cmdInsert.Parameters.AddWithValue("@ClinicID", ClinicID);
                                cmdInsert.Parameters.AddWithValue("@CreatedBy", userName);
                            }
                            else
                            {
                                ClinicDAL.UserAuthendication UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];
                                cmdInsert.Parameters.AddWithValue("@ClinicID", UA.ClinicID);
                                cmdInsert.Parameters.AddWithValue("@CreatedBy", UA.userName);
                            }
                            cmdInsert.Parameters.AddWithValue("@AttachID", AttachID);
                            cmdInsert.Parameters.AddWithValue("@VisitID", VisitID);
                            cmdInsert.Parameters.AddWithValue("@Description", Description);
                            cmdInsert.Parameters.AddWithValue("@Attachment", Attachment);
                            cmdInsert.Parameters.AddWithValue("@Name", Name);
                            cmdInsert.Parameters.AddWithValue("@Type", Type);
                            cmdInsert.Parameters.AddWithValue("@Size", Size);
                            cmdInsert.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            result = cmdInsert.ExecuteNonQuery();
                            if (!isFromMobile)
                            {
                                var page = HttpContext.Current.CurrentHandler as Page;
                                eObj.InsertionSuccessMessage(page, "Data Inserted Successfully..!!!!");
                            }
                        }
                        catch (SqlException ex)
                        {
                            if (!isFromMobile)
                            {
                                var page = HttpContext.Current.CurrentHandler as Page;
                                eObj.ErrorData(ex, page);
                            }
                            throw ex;
                        }
                        finally
                        {
                            con.Close();
                        }
                        return result;

                    }
                    #endregion FileAttachment
                    #endregion
                }
            #endregion Visit Attachment Class
        }
        #endregion Visit Class
 
    }
       
}