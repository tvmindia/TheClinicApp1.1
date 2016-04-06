using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace TheClinicApp.ClinicDAL
{
    public class Security
    {
        public bool Login(string username, string password) {
            if (username == password) {
                return true;
            }
            return false;
        }
    }

    public class UserAuthendication
    {
        #region Global Variables

        private string userN;
        private string GroupName;
        private Guid Group_ID;
        private string ClinicName;
        private Guid Clinic_ID;
        private Boolean isValidUser;
        

        public string userName
        {

            get
            {
                return userN;
            }
        }
        public Boolean ValidUser
        {
            get
            {

                return isValidUser;
            }

        }

        public string Clinic
        {

            get
            {
                return ClinicName;
            }
        }
        public string Group
        {

            get
            {
                return GroupName;
            }
        }


        public Guid ClinicID{

            get
            {
                return Clinic_ID;
            }
        }

        public Guid GroupID
        {

            get
            {
                return Group_ID;
            }
        }

        #endregion Global Variables

        #region Encrypt Password
        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        #endregion Encrypt Password

        #region User Authentication
        public UserAuthendication(String userName, String password)
        {
            DataTable dt = GetLoginDetails(userName);

            if (dt.Rows.Count > 0)
            {
                string Name = dt.Rows[0]["LoginName"].ToString();
                string Passwd = dt.Rows[0]["Password"].ToString();

                if (userName == Name && (Encrypt(password) == Passwd) )
                {
                    isValidUser = true;
                    userN = userName;
                    ClinicName = " Clinic 1";
                    GroupName = "Thrithvam Ayurveda";
                    //Clinic_ID = new Guid("C0946CD5-EBB4-44CE-9DFC-349BB4D32761");
                    Clinic_ID = new Guid(dt.Rows[0]["ClinicID"].ToString());
                    Group_ID = new Guid("ED6A102A-E904-4471-BF9A-F6BEDB2F36FB");
                }

                else
                {
                    isValidUser = false;
                }
            }
            //------------------* This case is temporaray * ---------------//
            //else
            //{
                
            //    if (userName == password)
            //    {
            //        isValidUser = true;
            //        userN = userName;
            //        ClinicName = " Clinic 1";
            //        GroupName = "Thrithvam Ayurveda";
            //        Clinic_ID = new Guid("C0946CD5-EBB4-44CE-9DFC-349BB4D32761");
            //        Group_ID = new Guid("ED6A102A-E904-4471-BF9A-F6BEDB2F36FB");

            //    }
            //    else
            //    {

            //        isValidUser = false;
            //    }
            //}
        }

        #endregion  User Authentication

        #region Get Login Details
        public DataTable GetLoginDetails(string LoginName)
        {
            SqlConnection con = null;

            try
            {
                DataTable dt = new DataTable();
                dbConnection dcon = new dbConnection();
                con = dcon.GetDBConnection();
                SqlCommand cmd = new SqlCommand("GetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LoginName", SqlDbType.NVarChar, 50).Value = LoginName;
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
       
        #endregion Get Login Details
    }

    public class CryptographyFunctions
    {
        //AES 128bit Cross Platform (Java and C#) Encryption Compatibility
        string key = System.Web.Configuration.WebConfigurationManager.AppSettings["cryptography"];
        /// <summary>
        /// AES 128bit Encryption function
        /// </summary>
        /// <param name="plainText">text to be encrypted</param>
        /// <returns>Encrypted text</returns>
        public string Encrypt(string plainText)
        {
            string encryptedText = "";
            try
            {

                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
                encryptedText = Convert.ToBase64String(new RijndaelManaged
                {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7,
                    KeySize = 128,
                    BlockSize = 128,
                    Key = keyBytes,
                    IV = keyBytes
                }.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length));
            }
            catch (Exception ex)
            {
                //System.IO.File.WriteAllText(@Server.MapPath("~/Text.txt"), ex.Message);
                throw ex;
            }
            return encryptedText;
        }
        /// <summary>
        /// AES 128 Decryption function
        /// </summary>
        /// <param name="encryptedText">Text to be decrypted</param>
        /// <returns>decrypted plain text</returns>
        public string Decrypt(string encryptedText)
        {
            string plainText = "";
            try
            {
                var encryptedBytes = Convert.FromBase64String(encryptedText);
                var keyBytes = new byte[16];
                var secretKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
                plainText = Encoding.UTF8.GetString(
                    new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        Padding = PaddingMode.PKCS7,
                        KeySize = 128,
                        BlockSize = 128,
                        Key = keyBytes,
                        IV = keyBytes
                    }.CreateDecryptor().TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));                
            }
            catch (Exception ex)
            {
                //System.IO.File.WriteAllText(@Server.MapPath("~/Text.txt"), ex.Message);
                throw ex;
            }
            return plainText;
        }
    }
}