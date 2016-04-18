#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;

#endregion  Included Namespaces

namespace TheClinicApp1._1.Stock
{
    public partial class StockDetails : System.Web.UI.Page
    {
        #region objects

        DataTable dtReceipthdr = null;
        public string listFilter = null;

        ErrorHandling eObj = new ErrorHandling();
        Stocks stok = new Stocks();       
        Receipt rpt = new Receipt();

        Guid receipt;  //passing guid value: ReceiptID  
      
        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;

        #endregion objects

        #region Methods

        //------------------------------ * MEDICINES  * -------------------------------------------//

        #region Get MedicineDetails By Medicine Name

        /// <summary>
        /// To fill textboxes with medicine details when when medicne name is inserted
        /// </summary>
        /// <param name="MedName"></param>
        /// <returns>String of medicine details</returns>

        [WebMethod]

        public static string MedDetails(string MedName)
        {
            Receipt ReceiptdtlsObj = new Receipt();

            UIClasses.Const Const = new UIClasses.Const();
            ClinicDAL.UserAuthendication UA;

            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            ReceiptdtlsObj.ClinicID = UA.ClinicID.ToString();

            DataSet ds = ReceiptdtlsObj.GetMedicineDetailsByMedicineName(MedName);
            string Unit = "";
            string MedCode = "";
            string Category = "";
            string Qty = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                Unit = Convert.ToString(ds.Tables[0].Rows[0]["Unit"]);
                MedCode = Convert.ToString(ds.Tables[0].Rows[0]["MedCode"]);
                Category = Convert.ToString(ds.Tables[0].Rows[0]["CategoryName"]);
                Qty = Convert.ToString(ds.Tables[0].Rows[0]["Qty"]);
            }

            return String.Format("{0}" + "|" + "{1}" + " | " + "{2}" + " | " + "{3}", Unit, MedCode, Category, Qty);


        }


        #endregion Get MedicineDetails By Medicine Name

        #region Bind List Filter

        /// <summary>
        /// Bind list filter with medicine names
        /// </summary>
        public void BindListFilter()
        {
            listFilter = null;
            listFilter = GetMedicineNames();
        }

        #endregion Bind List Filter

        #region Get Medicine Names

        /// <summary>
        /// Get all medicine names to be binded into list filter
        /// </summary>
        /// <returns></returns>
        private string GetMedicineNames()
        {
            // Patient PatientObj = new Patient();
            Stocks stok = new Stocks();

            DataTable dt = stok.SearchBoxMedicine();

            StringBuilder output = new StringBuilder();
            output.Append("[");
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                output.Append("\"" + dt.Rows[i]["Name"].ToString() + "\"");

                if (i != (dt.Rows.Count - 1))
                {
                    output.Append(",");
                }
            }
            output.Append("]");
            return output.ToString();
        }

        #endregion Get Medicine Names

        #region Store Xml To HiddenField

        /// <summary>
        /// Issue details are converted to xml and stored into hidden field ,so that controls can be refilled
        /// </summary>
        public void StoreXmlToHiddenField()
        {
            if (ViewState["ReceiptHdrID"] != null && ViewState["ReceiptHdrID"].ToString() != string.Empty)
            {
                string receiptid = ViewState["ReceiptHdrID"].ToString();

               
                DataSet dsreceipt = GetIssueDetailsByReceiptid(receiptid);
                var xml = dsreceipt.GetXml();

                hdnXmlData.Value = xml;
            }


        }

        #endregion Store Xml To HiddenField#region Store Xml To HiddenField

        #region Get Issue Details By Receiptid

        /// <summary>
        /// To fill xml with saved data
        /// </summary>
        /// <param name="Receiptid"></param>
        /// <returns>Dataset of issue details by id</returns>
        public DataSet GetIssueDetailsByReceiptid(string Receiptid)
        {

            DataSet dsReceipt = null;

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            rpt.ClinicID = UA.ClinicID.ToString();
            dsReceipt = rpt.GetReceiptDetailsByReceiptID(Receiptid);

            return dsReceipt;

        }

        #endregion Get Issue Details By Receiptid

        //-----------------------------  * END  MEDICINES AREA * -------------------//


        #endregion Methods


        protected void Page_Load(object sender, EventArgs e)
        {
            BindListFilter();


          //  txtDate.Attributes.Add("readonly", "readonly");
            string receiptID = string.Empty;

            DataSet dsReceipthdr = null;
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];

            //---------- * Checking whether redirection to the page happened by clicking in gridview or by clicking the new issue button *----------//

            if (Request.QueryString["receiptID"] == null)
            {

                //-----*Redirection due to new issue button click*-----//

                if (!IsPostBack)
                {
                   
                }

            }

            else
            {


                //-----*Redirection due to Grid link click*-----//

                hdnHdrInserted.Value = "True";

                receiptID = Request.QueryString["receiptID"].ToString();

                rpt.ClinicID = UA.ClinicID.ToString();
                DataSet dsHdr = rpt.GetReceiptDetailsByReceiptID(receiptID);



                dtReceipthdr = dsHdr.Tables[0];

                if (dtReceipthdr.Rows.Count > 0)
                {
                    ViewState["ReceiptHdrID"] = Request.QueryString["receiptID"].ToString();

                   

                    if (!IsPostBack)
                    {
                        txtBillNo.Text = dtReceipthdr.Rows[0]["RefNo1"].ToString();
                        txtRefNo2.Text = dtReceipthdr.Rows[0]["RefNo2"].ToString();
                        txtDate1.Value = ((DateTime)dtReceipthdr.Rows[0]["Date"]).ToString("dd-MM-yyyy");
                    }



                    //}
                    dsReceipthdr = rpt.GetReceiptDetailsByReceiptID(receiptID);
                    var xml = dsReceipthdr.GetXml();

                    hdnXmlData.Value = xml;
                }
            }













            //receipt = Guid.Parse(Request.QueryString["ReceiptID"]);
            //UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            //rpt.ClinicID = UA.ClinicID.ToString();

            //GridViewReceiptDetails();
             

        }

        public void GridViewReceiptDetails()
        {
            DataSet gds = rpt.GetReceiptDetailsByReceiptID(receipt);

            txtBillNo.Text = gds.Tables[0].Rows[0][0].ToString();
            txtRefNo2.Text = gds.Tables[0].Rows[0][1].ToString();
            txtDate1.Value = gds.Tables[0].Rows[0][2].ToString();
        }

        

        protected void btSave_ServerClick(object sender, EventArgs e)
        {

            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            

            if (hdnRemovedIDs.Value != string.Empty)
            {

                //----------------- * CASE : DELETE *-----------------------------------//

                string hdRemovedIDValue = hdnRemovedIDs.Value;

                string[] RemovedIDs = hdRemovedIDValue.Split(',');

                for (int i = 0; i < RemovedIDs.Length - 1; i++)
                {

                    if ((RemovedIDs[i] != "") || (RemovedIDs[i] != string.Empty))
                    {

                        ReceiptDetails DetailObj = new ReceiptDetails();
                        string UniqueId = RemovedIDs[i].ToString();

                        //string medId =   DetailObj.GetMedicineIDByUniqueID(Guid.Parse(UniqueId));

                        DetailObj.ClinicID = UA.ClinicID.ToString();
                        DetailObj.DeleteReceiptDetails(UniqueId);
                        hdnRemovedIDs.Value = "";

                    }
                }

            }



            if (hdnRemovedIDs.Value == string.Empty)
            {
                if (hdnTextboxValues.Value != "")
                {

                    if ((txtBillNo.Text != string.Empty) && (txtRefNo2.Text != string.Empty) && (txtDate1.Value != string.Empty))
                    {

                        rpt.ClinicID = UA.ClinicID.ToString();
                        rpt.RefNo1 = txtBillNo.Text;
                        rpt.RefNo2 = txtRefNo2.Text;
                        rpt.CreatedBy = UA.userName;

                        rpt.Date = Convert.ToDateTime(txtDate1.Value);



                        if ((hdnHdrInserted.Value == "") && (hdnTextboxValues.Value != ""))
                        {
                            rpt.InsertReceiptHeader();
                            hdnHdrInserted.Value = "True";
                            ViewState["ReceiptHdrID"] = rpt.ReceiptID;// check view state
                            
                        }

                        if (ViewState["ReceiptHdrID"] != null && ViewState["ReceiptHdrID"].ToString() != string.Empty)
                        {


                            string receiptid = ViewState["ReceiptHdrID"].ToString();

                            rpt.ClinicID = UA.ClinicID.ToString();
                            DataSet dsHdr = rpt.GetReceiptDetailsByReceiptID(receiptid);

                          

                            dtReceipthdr = dsHdr.Tables[0];


                            if (dtReceipthdr.Rows.Count > 0)
                            {

                                string oldDate = ((DateTime)dtReceipthdr.Rows[0]["Date"]).ToString("dd-MM-yyyy");
                                string newDate = txtDate1.Value;

                                if ((txtBillNo.Text != dtReceipthdr.Rows[0]["RefNo1"].ToString()) || (txtRefNo2.Text != dtReceipthdr.Rows[0]["RefNo2"].ToString()) || (oldDate != newDate))
                                {
                                    // ----------------- *  Update header*-----------------------------------//

                                    if (hdnTextboxValues.Value != "")
                                    {

                                        rpt.ClinicID = UA.ClinicID.ToString();
                                       
                                        rpt.RefNo2 = txtRefNo2.Text;
                                        rpt.Date = Convert.ToDateTime(txtDate1.Value);
                                        rpt.UpdatedBy = UA.userName;
                                         
                                        rpt.UpdateReceiptHeader(ViewState["ReceiptHdrID"].ToString());
                                    }

                                }
                            }
                        }

                        string last = string.Empty;




                        string values = hdnTextboxValues.Value;

                        string[] Rows = values.Split('$');



                        for (int i = 0; i < Rows.Length - 1; i++)
                        {
                            ReceiptDetails ReceiptDtObj = new ReceiptDetails(); //Object is created in loop as each entry should have different uniqueID
                            string[] tempRow = Rows;

                            last = tempRow[i].Split('|').Last();

                            string[] columns = tempRow[i].Split('|');

                            if (last == string.Empty || last == "")
                            {

                                //----------------- * CASE : INSERT *-----------------------------------//
                                if ((columns[0] != null) && (columns[4] != null))
                                {


                                    ReceiptDtObj.MedicineName = columns[0];
                                    ReceiptDtObj.Unit = columns[1];
                                    ReceiptDtObj.QTY = Convert.ToInt32(columns[4]);

                                    ReceiptDtObj.CreatedBy = UA.userName; ;
                                    ReceiptDtObj.ClinicID = UA.ClinicID.ToString();

                                    if (ViewState["ReceiptHdrID"] != null && ViewState["ReceiptHdrID"].ToString() != string.Empty)
                                    {
                                        ReceiptDtObj.ReceiptID = Guid.Parse(ViewState["ReceiptHdrID"].ToString());
                                    }

                                    ReceiptDtObj.InsertReceiptDetails();
                                }
                            }

                            if (last != string.Empty)
                            {
                                //----------------- * CASE : UPDATE *---------------------------------//

                                if ((columns[0] != null) && (columns[4] != null))
                                {
                                    string uniqueID = last;
                                    ReceiptDetails UpREceiptDtlObj = new ReceiptDetails(new Guid(uniqueID));

                                    UpREceiptDtlObj.ClinicID = UA.ClinicID.ToString();
                                    UpREceiptDtlObj.QTY = Convert.ToInt32(columns[4]);
                                    UpREceiptDtlObj.UpdatedBy = UA.userName;

                                    //string medicineID = IssuedtlObj.GetMedcineIDByMedicineName(columns[0]);

                                    UpREceiptDtlObj.UpdateReceiptDetails(uniqueID);
                                }
                            }

                        }


                        hdnManageGridBind.Value = "True";

                    }
                }
            }

           // StoreXmlToHiddenField();

        }

       


    }
}