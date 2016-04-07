using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp.ClinicDAL;

namespace TheClinicApp1._1.Login
{
    public partial class Reset : System.Web.UI.Page
    {
        #region Global Variables
        TheClinicApp.ClinicDAL.CryptographyFunctions CrypObj = new CryptographyFunctions();
        Guid UserID;
        TheClinicApp.ClinicDAL.User userObj = new TheClinicApp.ClinicDAL.User();
        TheClinicApp.UIClasses.Const Const = new TheClinicApp.UIClasses.Const();


        #endregion Global Variables

        #region Methods

        //#region Encrypt Password
        //private string Encrypt(string clearText)
        //{
        //    string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}

        //#endregion Encrypt Password

        #region Reset Button Click
        protected void btnReset_ServerClick(object sender, EventArgs e)
        {


            userObj.passWord = CrypObj.Encrypt(txtConfirmPassword.Value);


            if (txtNewPassword.Value == txtConfirmPassword.Value)
            {
                userObj.ResetPassword(UserID);
            }

            else
            {
                lblError.Text = " Password does not match the confirm password";
            }
        }

        #endregion Reset Button Click

        #endregion Methods

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["UserID"] != null)
            {
                UserID = Guid.Parse(Request.QueryString["UserID"]);
            }
        }
        #endregion Page Load
    }
}