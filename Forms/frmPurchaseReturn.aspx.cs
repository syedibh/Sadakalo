using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPurchaseReturn : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsTransaction objTransaction = new clsTransaction();
    clsCommon objCommon = new clsCommon();
    public static string branchId;
    public static string userId;
    public static string Addnew="";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchId = Session["BranchID"].ToString();
            userId = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                ShowLastBillNo();
                loadgdvPurchaseReturnDetails(txtChallanNo.Text);
                LoadFooterInfo(txtChallanNo.Text);

            }

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

    private void LoadBillNo(string voucherType)
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM TransactionList WHERE VType='" + voucherType + "' AND BrId='" + branchId + "'";
            string billNo = objServiceHandler.ReturnString(strSql);
            txtChallanNo.Text = billNo;
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }

    private void LoadCustomer(string supplierId, string supplierName)
    {
        try
        {
            ddlSupplier.Items.Clear();
            ddlSupplier.Items.Add(new ListItem(supplierName, supplierId));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }

    }

    # region Private Method
    private void ShowLastBillNo()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT MAX(VNO) AS VNO FROM TransactionList WHERE VType='PA' AND BrId='" + branchId + "';";
            string billNo = objServiceHandler.ReturnString(strSql);
            txtChallanNo.Text = billNo.ToString();
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }

    }
    private void totalCalculation(string relationId)
    {
        try
        {
            string strSQL = "SELECT SUM(Qnty) TOTALQNTY,SUM(VAT) TOTALVAT,SUM(Discount) TOTALDISCOUNT,SUM(AMOUNT) TOTALAMOUNT FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'";
            DataSet totalInfo = objServiceHandler.ExecuteQuery(strSQL);
            if (totalInfo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in totalInfo.Tables["Table1"].Rows)
                {
                    txtVat.Text = prows["TOTALVAT"].ToString();
                    txtTotalPrice.Text = prows["TOTALAMOUNT"].ToString();
                    txtTotalQuantity.Text = prows["TOTALQNTY"].ToString();
                    txtComission.Text = prows["TOTALDISCOUNT"].ToString();
                    txtNetAmount.Text = prows["TOTALAMOUNT"].ToString();

                }
            }

            string headerInfo = "SELECT Vno,SupplierId,(SELECT SupplierName FROM Supplier WHERE SupplierId=TL.SupplierId) AS SupplierName,ContraRef FROM TransactionList TL WHERE RefNo='" + relationId + "'";
            DataSet headerInfoUnsave = objServiceHandler.ExecuteQuery(headerInfo);
            if (headerInfoUnsave.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in headerInfoUnsave.Tables["Table1"].Rows)
                {
                    txtChallanNo.Text = prow["Vno"].ToString();
                    string contraRef=prow["ContraRef"].ToString();
                    string purchaseBillNo = objServiceHandler.ReturnString("SELECT VNO FROM TRANSACTIONLIST WHERE REFNO='" + contraRef + "'");
                    txtSupplierChallanNo.Text = purchaseBillNo;
                    string supplierId = prow["SupplierId"].ToString();
                    string supplierName = prow["SupplierName"].ToString();
                    if (!supplierId.Equals("0"))
                    {
                        LoadCustomer(supplierId, supplierName);
                    }

                }
            }

        }
        catch
        {
            lblMesseage.Text = "Calculation Error !";
            return;
        }

    }
    private void LoadPurchaseItemList(string transactionListId)
    {
        try
        {

            string strSql = "SELECT TD.ITEMID,I.ItemDescription FROM TransactionDetails TD,ITEM I WHERE TD.ItemId=I.ItemId AND TD.TransactionList_Id='" + transactionListId + "'";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlItem.DataSource = oDs;
            ddlItem.DataValueField = "ITEMID";
            ddlItem.DataTextField = "ItemDescription";
            ddlItem.DataBind();

            ddlItem.Items.Insert(0, new ListItem("--Select Item--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }

    }
    private void loadgdvPurchaseReturnDetails(string billNo)
    {
        try
        {

            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,(TD.rate * Qnty) Amount ,TD.Vat,TD.Discount,(TD.Amount) TotalAmount FROM TransactionDetails TD, TransactionList TL,Item I WHERE  TL.RefNo=TD.TransactionList_Id AND TD.ItemId=I.ItemId AND  TL.VType='PA' AND TL.Vno='" + billNo + "' AND TL.BRID='" + branchId + "' ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvItemDetails.DataSource = oDs;
            gdvItemDetails.DataBind();
            txtTotalItem.Text = gdvItemDetails.Rows.Count.ToString();
        }

        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadFooterInfo(string billNo)
    {
        string strSQL = "";
        lblMesseage.Text = "";
        txtVat.Text = "0.00";
        txtComission.Text = "0.00";
        string voucherType = "PA";
        try
        {
            strSQL="SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + billNo + "' ";
            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery(strSQL);
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prows["REFNO"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        strSQL = "SELECT VNO,SupplierId,(SELECT SupplierName FROM Supplier WHERE SupplierId=TL.SupplierId) AS SupplierName, TotalBill,Commission,Vat,AmountPaid,ContraRef FROM TransactionList TL WHERE REFNO='" + relationId + "'";
                        DataSet result = objServiceHandler.ExecuteQuery(strSQL);
                        if (result.Tables["Table1"].Rows.Count == 1)
                        {
                            foreach (DataRow prow in result.Tables["Table1"].Rows)
                            {
                                string contraRefNo = prow["ContraRef"].ToString();
                                txtChallanNo.Text = prow["VNO"].ToString();
                                string purchaseBillNo = objServiceHandler.ReturnString("SELECT VNO FROM TRANSACTIONLIST WHERE REFNO='" + contraRefNo + "'");
                                txtSupplierChallanNo.Text = purchaseBillNo;
                                txtTotalPrice.Text = prow["TotalBill"].ToString();
                                txtComission.Text = prow["Commission"].ToString();
                                txtVat.Text = prow["Vat"].ToString();
                                txtNetAmount.Text = prow["TotalBill"].ToString();
                                ddlSupplier.Items.Clear();
                                string supplierId = prow["SupplierId"].ToString();
                                string supplierName = prow["SupplierName"].ToString();
                                if (!supplierId.Equals("0"))
                                {
                                    LoadCustomer(supplierId, supplierName);
                                }
                            }
                            string TotalQuantity = objServiceHandler.ReturnString("SELECT SUM(Qnty) FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                            txtTotalQuantity.Text = TotalQuantity.ToString();
                        }
                    }
                    else
                    {
                        totalCalculation(relationId);
                    }

                }
            }
            else
            {
                lblMesseage.Text = "No Data Found !";
                return;
            }


        }
        catch
        {
            lblMesseage.Text = "Something went wron!";
            return;
        }

    }
    private void ClearHeaderFilled()
    {
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtPurchasePrice.Text = "0.00";
        txtAmount.Text = "0.00";
        txtTotalPrice.Text = "0.00";
        txtComission.Text = "0";
    }
    private void ClearAllFilled()
    {
        txtChallanNo.Text = "";
        txtSupplierChallanNo.Text = "";
        txtQuantity.Text = "1";
        txtAmount.Text = "0.00";
        txtPurchasePrice.Text = "0.00";
        txtTotalItem.Text = "0";
        txtTotalQuantity.Text = "0";
        txtTotalPrice.Text = "0.00";
        txtVat.Text = "0.00";
        txtComission.Text = "0.00";
        txtNetAmount.Text = "0.00";
        lblMesseage.Text = "";
        txtSearchChallan.Text = "";
    }

    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtQuantity.Text = "1";
            string purchaseRefNo = Session["TransactionListId"].ToString();
            string soldQntity = "";
            string purchaseReturnItemQnty = "";
            double purchaseReturnQnty = 0;
            if (purchaseRefNo.Equals(""))
            {
                lblMesseage.Text = "Please Find Purchase Bill First";
                return;
            }
            string strItemId = ddlItem.SelectedValue;
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT Qnty,rate,contraRate,vat,discount FROM TransactionDetails WHERE TransactionList_Id='" + purchaseRefNo + "' AND  ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    txtPurchasePrice.Text = prow["rate"].ToString();
                    soldQntity = prow["Qnty"].ToString();
                    string contraRate = prow["contraRate"].ToString();
                  //  txtSalesVatPer.Text = prow["vat"].ToString();
                  //  txtDiscountAmount.Text = prow["discount"].ToString();

                    DataSet previousRefNo = objServiceHandler.ExecuteQuery("SELECT REFNO FROM TransactionLIST WHERE CONTRAREF='" + purchaseRefNo + "'");
                    if (previousRefNo.Tables["Table1"].Rows.Count > 0)
                    {
                        foreach (DataRow prowsr in previousRefNo.Tables["Table1"].Rows)
                        {
                            string purchaseReturnRefNo = prowsr["REFNO"].ToString();

                            DataSet existReturnQnty = objServiceHandler.ExecuteQuery("SELECT QNTY FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + purchaseReturnRefNo + "' AND ITEMID='" + strItemId + "'");
                            if (existReturnQnty.Tables["Table1"].Rows.Count > 0)
                            {
                                foreach (DataRow prowsrqnty in existReturnQnty.Tables["Table1"].Rows)
                                {
                                    purchaseReturnItemQnty = prowsrqnty["QNTY"].ToString();
                                }
                                purchaseReturnQnty = purchaseReturnQnty + double.Parse(purchaseReturnItemQnty);
                            }
                        }
                        double remainingQnty = double.Parse(soldQntity) - purchaseReturnQnty;
                        txtRemainingQnty.Text = remainingQnty.ToString();
                    }
                    else
                    {
                        txtRemainingQnty.Text = soldQntity.ToString();
                    }


                    double rate = Convert.ToDouble(txtPurchasePrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);

                    double amount = rate * quantity;
                    txtAmount.Text = amount.ToString();

                   // amount = amount + Convert.ToDouble(txtSalesVatPer.Text);
                   // txtTotalAmount.Text = amount.ToString();
                }

            }
            else
            {
                lblMesseage.Text = "Sales Price Empty";
                return;
            }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
            double quntity = Convert.ToDouble(txtQuantity.Text);
            double remainingQnty = Convert.ToDouble(txtRemainingQnty.Text);
            if (quntity > remainingQnty)
            {
                lblMesseage.Text = "Not Purchase Quantity";
                return;
            }
            double amount = (purchasePrice * quntity);
            txtAmount.Text = amount.ToString();
        }


        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Addnew = "True";
            string voucherType = "PA";
            ClearAllFilled();
            LoadBillNo(voucherType);
            string voucherNo = txtChallanNo.Text;
            loadgdvPurchaseReturnDetails("");


        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void btnFindPurchase_Click(object sender, EventArgs e)
    {
        string relationId = "";
        Session["TransactionListId"] = "";
        string supplierId = "";
        string supplierName = "";
        lblMesseage.Text = "";

        try
        {
            if (txtSupplierChallanNo.Text.Equals(""))
            {
                lblMesseage.Text = "Please Input Purchase Bill No ";
                return;
            }


            DataSet checkRefNo = objServiceHandler.ExecuteQuery("SELECT TL.REFNO,SupplierId,(SELECT SupplierName FROM Supplier WHERE SupplierId=TL.SupplierId) AS SupplierName FROM TransactionList TL  WHERE   TL.VType='PU' AND TL.Vno='" + txtSupplierChallanNo.Text + "' ");
            if (checkRefNo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkRefNo.Tables["Table1"].Rows)
                {
                    relationId = prows["REFNO"].ToString();
                    supplierId = prows["SupplierId"].ToString();
                    supplierName = prows["SupplierName"].ToString();
                    LoadPurchaseItemList(relationId);
                    LoadCustomer(supplierId, supplierName);
                    Session["TransactionListId"] = relationId.ToString();
                }
            }
            else
            {
                lblMesseage.Text = "Please Input Valid Purchase Bill No";
                return;
            }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string strSQL = "";
            string relationId = "";
            string billNo = txtChallanNo.Text;
            string itemId = ddlItem.SelectedValue;
            string qntity = txtQuantity.Text;
            string purchasePrice = txtPurchasePrice.Text;
            string voucherType = "PA";


            if (qntity.Equals("") || qntity.Equals("0"))
            {
                lblMesseage.Text = "Please Input Return Quantity";
                return;
            }
            double quntity = Convert.ToDouble(txtQuantity.Text);
            double remainingQnty = Convert.ToDouble(txtRemainingQnty.Text);
            if (quntity > remainingQnty)
            {
                lblMesseage.Text = "Not enough Remaining Balance";
                return;
            }

            if (ddlItem.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Item ";
                return;
            }

            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No";
                return;
            }



            if(Addnew.Equals("True"))
            {
                string supplierId = ddlSupplier.SelectedValue;
                if (txtSupplierChallanNo.Equals(""))
                {
                    lblMesseage.Text = "Please input purchase bill ! ";
                    return;
                }

                if (supplierId.Equals(""))
                {
                    supplierId = "0";
                    return;
                }
                LoadBillNo(voucherType);
                string voucherNo = txtChallanNo.Text;
                string contraRefNo = Session["TransactionListId"].ToString();
                
                string result = objCommon.AddItemTransactionList(branchId, voucherType, voucherNo, userId, contraRefNo, supplierId, "0", "0");
                if (!result.Equals("Successful"))
                {
                    lblMesseage.Text = "Something went wrong";
                    return;
                }
            }



            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + billNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    relationId = prows["REFNO"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You Could not Edit Posted Bill ! ";
                        return;
                    }
                }
            }


            strSQL = "SELECT *  FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "' AND ITEMID='" + itemId + "' ";
            DataSet checkItemISExist = objServiceHandler.ExecuteQuery(strSQL);
            if (checkItemISExist.Tables["Table1"].Rows.Count == 1)
            {
                lblMesseage.Text = "Item Already Exist !";
                return;
            }
            else
            {
                string costPrice = objServiceHandler.ReturnString("SELECT [dbo].[CostPrice]('" + branchId + "' , '" + itemId + "') as Costprice");

                string result = objCommon.AddTransactionDetails(itemId, qntity, purchasePrice, costPrice, "0", "0", relationId);

                if (result.Equals("Successful"))
                {
                    txtVat.Text = "0";
                    txtTotalPrice.Text = "0.00";
                    lblMesseage.Text = "Item Added";
                    loadgdvPurchaseReturnDetails(txtChallanNo.Text);
                    totalCalculation(relationId);
                    ClearHeaderFilled();
                    Addnew = "False";

                }
                else
                {
                    lblMesseage.Text = "Item Added Failed";
                    return;
                }
            }
        }

        catch
        {
            lblMesseage.Text = "Somethin went wrong !";
            return;
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string relationId = "";
            string billNo = txtChallanNo.Text;
           // string purchaseBillNo = txtSupplierChallanNo.Text;
            string contraRefNo = Session["TransactionListId"].ToString();
            string voucherType = "PA";
            string supplierId = ddlSupplier.SelectedValue;
            string totalPrice = txtTotalPrice.Text;
            string vat = txtVat.Text;
            string comission = txtComission.Text;
            string netAmount = txtNetAmount.Text;
            string posted = "True";
            string postedDate = DateTime.Now.ToString();

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + billNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    relationId = prows["REFNO"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "This Bill Already Posted ! ";
                        return;
                    }

                    else
                    {

                        string result = objCommon.UpdateTransactionList(relationId, supplierId, "0", totalPrice, vat, comission, "0", userId, postedDate, posted, "0", userId, postedDate, contraRefNo);
                        if (result.Equals("Successfull"))
                        {

                            lblMesseage.Text = "Purchase Return Save Successfully";

                            DataSet itemList = objServiceHandler.ExecuteQuery("SELECT ItemId,Qnty  FROM TransactionDetails  WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                            if (itemList.Tables["Table1"].Rows.Count > 0)
                            {
                                foreach (DataRow prow in itemList.Tables["Table1"].Rows)
                                {
                                    string itemId = prow["ItemId"].ToString();
                                    string quantity = prow["Qnty"].ToString();
                                    string balance = objServiceHandler.ReturnString("SELECT [" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + itemId + "'");
                                    double prevBalance = Convert.ToDouble(balance);
                                    double currentquantity = Convert.ToDouble(quantity);
                                    double currentBalance = prevBalance - currentquantity;
                                    quantity = currentBalance.ToString();

                                    string BalanceUpdate = objCommon.UpdateBalanace(itemId, quantity, branchId);
                                    if (!BalanceUpdate.Equals("Successfull"))
                                    {
                                        lblMesseage.Text = "Item Balance Updated Error !";
                                    }
                                }
                            }

                        }
                        else
                        {
                            lblMesseage.Text = "Purchase Return Save Failed";
                            return;
                        }
                    }
                }
            }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchChallan.Text.Equals(""))
            {
                lblMesseage.Text = "Please input bill no !";
                return;
            }

            loadgdvPurchaseReturnDetails(txtSearchChallan.Text);
            LoadFooterInfo(txtSearchChallan.Text);
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void gdvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow rows = gdvItemDetails.Rows[e.RowIndex];
            string strItemId = gdvItemDetails.DataKeys[e.RowIndex].Value.ToString();
            string challanNo = txtChallanNo.Text;

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='PA' AND VNO='" + challanNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prow["REFNO"].ToString();
                    string status = prow["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You are not allowed Delete posted bill";
                        return;
                    }
                    else
                    {
                        string result = objTransaction.DeleteItemInfo(relationId, strItemId);
                        if (result.Equals("Deleted"))
                        {
                            loadgdvPurchaseReturnDetails(challanNo);
                            lblMesseage.Text = "Item Deleted Successfully";
                            totalCalculation(relationId);


                        }
                        else
                        {
                            lblMesseage.Text = "Item Deleted Failed";
                            return;
                        }
                    }
                }
            }
            else
            {
                lblMesseage.Text = "Something went wrong !";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong !";
        }
    }

}