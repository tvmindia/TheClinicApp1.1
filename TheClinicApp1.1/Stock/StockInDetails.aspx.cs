﻿
#region CopyRight

//Author      : Gibin
//Modified By : SHAMILA T P

#endregion CopyRight

#region Included Namespaces

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TheClinicApp1._1.ClinicDAL;
using Messages = TheClinicApp1._1.UIClasses.Messages;

#endregion  Included Namespaces

namespace TheClinicApp1._1.Stock
{
    public partial class StockDetails : System.Web.UI.Page
    {
        #region objects

        DataTable dtReceipthdr = null;
        public string listFilter = null;
        public string RoleName = null;
        ErrorHandling eObj = new ErrorHandling();
        Stocks stok = new Stocks();
        Receipt rpt = new Receipt();

        Guid receipt;  //passing guid value: ReceiptID  

        UIClasses.Const Const = new UIClasses.Const();
        ClinicDAL.UserAuthendication UA;
        IssueHeaderDetails IssueHdrObj = new IssueHeaderDetails();

        #endregion objects

        #region Methods

        //------------------------------ * MEDICINES  * -------------------------------------------//

        #region Get Quantity In Stock
        [WebMethod]

        public static string GetQtyInStock(string MedName)
        {
            ClinicDAL.UserAuthendication UA;
            UIClasses.Const Const = new UIClasses.Const();
            UA = (ClinicDAL.UserAuthendication)HttpContext.Current.Session[Const.LoginSession];

            IssueDetails dtlsObj = new IssueDetails();
            dtlsObj.ClinicID = UA.ClinicID.ToString();

            
            string qty = dtlsObj.GetQtyByMedicineName(MedName);
            return qty;
        }

        #endregion Get Quantity In Stock

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

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> RoleName = new List<string>();
            DataTable dtRols = new DataTable();
            UA = (ClinicDAL.UserAuthendication)Session[Const.LoginSession];
            rpt.ClinicID = UA.ClinicID.ToString();
            common cmn = new common();
            string Login = UA.userName;

            stok.ClinicID = UA.ClinicID.ToString();
            stok.CreatedBy = UA.userName;

            RoleName = UA.GetRoleName1(Login);
            //foreach (DataRow dr in dtRols.Rows)
            //{

            //    RoleName.Add(dr["RoleName"].ToString());

            //}
            
   
            txtDate1.Attributes.Add("readonly", "readonly");
            //btSave.Attributes.Add("onclick", "GetTextBoxValues('" + hdnTextboxValues.ClientID + "','" + hdnRemovedIDs.ClientID + "')");

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
                    var today =cmn.ConvertDatenow(DateTime.Now).ToString("dd-MMM-yyyy");
                    txtDate1.Value = today;

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


                if (dtReceipthdr.Rows.Count == 0)
                {
                    dsHdr = rpt.GetReceiptHeaderByReceiptID(receiptID);

                    dtReceipthdr = dsHdr.Tables[0];
                }




                if (dtReceipthdr.Rows.Count > 0)
                {
                    ViewState["ReceiptHdrID"] = Request.QueryString["receiptID"].ToString();



                    if (!IsPostBack)
                    {
                        txtBillNo.Text = dtReceipthdr.Rows[0]["RefNo1"].ToString();
                        txtRefNo2.Text = dtReceipthdr.Rows[0]["RefNo2"].ToString();
                        txtDate1.Value = ((DateTime)dtReceipthdr.Rows[0]["Date"]).ToString("dd-MMM-yyyy");
                    }



                    //}
                    dsReceipthdr = rpt.GetReceiptDetailsByReceiptID(receiptID);
                    var xml = dsReceipthdr.GetXml();

                    hdnXmlData.Value = xml;
                }
            }






        }

        #endregion Page Load

        #region  Save Button Click

        protected void btSave_ServerClick(object sender, EventArgs e)
        {
            string msg = string.Empty;


            if ((txtBillNo.Text.Trim() != string.Empty) && (txtDate1.Value.Trim() != string.Empty) )
            {

                //&& (hdnTextboxValues.Value.Trim() != string.Empty)

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

                        if ((txtBillNo.Text != string.Empty)  && (txtDate1.Value != string.Empty))
                        {


                            rpt.ClinicID = UA.ClinicID.ToString();
                            rpt.RefNo1 = txtBillNo.Text;
                            rpt.RefNo2 = txtRefNo2.Text;
                            rpt.CreatedBy = UA.userName;

                            rpt.Date = Convert.ToDateTime(txtDate1.Value);

                            //rpt.Date = Convert.ToDateTime(txtDate1.Value);



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

                                    string oldDate = ((DateTime)dtReceipthdr.Rows[0]["Date"]).ToString("dd-MMM-yyyy");
                                    string newDate = txtDate1.Value;

                                    if ((txtBillNo.Text != dtReceipthdr.Rows[0]["RefNo1"].ToString()) || (txtRefNo2.Text != dtReceipthdr.Rows[0]["RefNo2"].ToString()) || (oldDate != newDate))
                                    {
                                        // ----------------- *  Update header*-----------------------------------//

                                        if (hdnTextboxValues.Value != "")
                                        {

                                            rpt.ClinicID = UA.ClinicID.ToString();

                                            rpt.RefNo2 = txtRefNo2.Text;


                                            rpt.Date = Convert.ToDateTime(txtDate1.Value);
                                            //rpt.Date = Convert.ToDateTime(txtDate1.Value);

                                            rpt.UpdatedBy = UA.userName;

                                            rpt.RefNo1 = txtBillNo.Text;
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


                                        DataSet dsRptDetails = UpREceiptDtlObj.GetReceiptDetailsByUniqueID(uniqueID);

                                      
                                        int OldQty = Convert.ToInt32(dsRptDetails.Tables[0].Rows[0]["Qty"]);

                                        int CurrentStock = Convert.ToInt32(dsRptDetails.Tables[0].Rows[0]["QtyInStock"]);


                                        if (OldQty != Convert.ToInt32(columns[4]))
	                                        {
		 
                                            UpREceiptDtlObj.QTY = Convert.ToInt32(columns[4]);
                                            UpREceiptDtlObj.UpdatedBy = UA.userName;

                                          int NewQty = Convert.ToInt32(columns[4]);

                                          IssueHdrObj.ClinicID = UA.ClinicID.ToString();

                                          int TotalIssuedQty = Convert.ToInt32(IssueHdrObj.GetTotalIssuedQtyOfAMedicine(columns[0]));

                                          int StockWithoutAnyIssue = TotalIssuedQty + CurrentStock;   //'Stockbefore issue'- is the qty of particular medicine before any issue
                                        int difference = StockWithoutAnyIssue - OldQty;
                                        int qtyNeeded = TotalIssuedQty - difference;


  /*For example : Suppose We have Medicine X ,70 in stock ,
                                            
                                        if We IssueD this Medicine in different times , 30,20,10 .
                                        So total IssueD = 60

                                       Suppose if We are tring to edit 30 StockWithoutAnyIssue new qty as 40 , it can Be only edited upto 20 

                                        STEPS: We have OldQty=30 ,NewQty = 40 , 
                                                     Issue1 = 30, issue2=20 ,issue3 = 10 ,So TotalIssued = 60, 
                                                     StockWithoutAnyIssue = TotalIssuedQty + CurrentStock =60+10=70

                                                     difference = StockWithoutAnyIssue - OldQty = 70-30=40

                                                     qtyNeeded = TotalIssuedQty - difference = 60-40=20
   
   
   
   Some times differnce wil be 0 , for eg: if we have 60 in stock and we issued 60 , that case quantity needed will be issued itself
   
   
   */


                                        if (difference == 0)
                                        {
                                            qtyNeeded = StockWithoutAnyIssue - CurrentStock;
                                        }

                                        else
                                        {
                                            if (difference < TotalIssuedQty)
	                                            {

                                                    qtyNeeded = TotalIssuedQty - difference;
	                                            }
                                        }

                                        if (NewQty < qtyNeeded)
                                            {
                                                var page = HttpContext.Current.CurrentHandler as Page;

                                                msg = "Already issued.Can only be reduced upto " + qtyNeeded;

                                                eObj.InsertionNotSuccessMessage(page, msg);
                                            }

                                            else
                                            {


                                                UpREceiptDtlObj.UpdateReceiptDetails(uniqueID);




                                            }
                                    }
                                    
                                    }
                                }

                            }


                            hdnManageGridBind.Value = "True";

                        }
                    }
                }

                StoreXmlToHiddenField();
            }

            else
            {
                var page = HttpContext.Current.CurrentHandler as Page;

                msg = Messages.ConfirmInput;

                eObj.InsertionNotSuccessMessage(page, msg);
            }



        }

        #endregion Save Button Click

        protected void Logout_ServerClick(object sender, EventArgs e)
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }

        protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
        {string LogoutConfirmation = Request.Form["confirm_value"];

        if (LogoutConfirmation == "true")
        {
            Session.Remove(Const.LoginSession);
            Response.Redirect("../Default.aspx");
        }
        }

    }
}

 
























