using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmSalesReturn : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsTransaction objTransaction = new clsTransaction();
    clsCommon objCommon = new clsCommon();
    public static string branchId;
    public static string userId;
    public static string Addnew = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchId = Session["BranchID"].ToString();
            userId = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                LoadBranchList();
                ShowLastBillNo();
                loadgdvSalesDetails(txtBillNo.Text);
                LoadFooterInfo(txtBillNo.Text);
            }

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

    #region DropDownList
    private void LoadBranchList()
    {
        try
        {

            string strSql = "SELECT BRID,BRNAME FROM BRANCHNAME  ORDER BY BRNAME ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlBranch.DataSource = oDs;
            ddlBranch.DataValueField = "BRID";
            ddlBranch.DataTextField = "BRNAME";
            ddlBranch.DataBind();

            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadSalesItemList(string transactionListId)
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
    private void LoadCustomer(string customerId,string mobileNo)
    {
        try
        {
            ddlCustomer.Items.Clear();
            ddlCustomer.Items.Add(new ListItem(mobileNo, customerId));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }

    }
    private void LoadBillNo(string voucherType)
    {
        try 
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM TransactionList WHERE VType='" + voucherType + "' AND BrId='" + branchId + "'";
            string billNo = objServiceHandler.ReturnString(strSql);
            //if (billNo.Equals(null))
            //{
            //    billNo = "1";
            //}
            txtBillNo.Text = billNo;
        }
        catch

        {
            lblMesseage.Text="Something went wrong !";
            return;
        }
    }

    # endregion

    # region Private Method
    private void ShowLastBillNo()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT MAX(VNO) AS VNO FROM TransactionList WHERE VType='SR' AND BrId='" + branchId + "';";
            string billNo = objServiceHandler.ReturnString(strSql);
            txtBillNo.Text = billNo.ToString();
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
                    txtDiscount.Text = prows["TOTALDISCOUNT"].ToString();
                    txtNetAmount.Text = prows["TOTALAMOUNT"].ToString();

                }
            }

            string headerInfo = "SELECT Vno,CustomerId,(SELECT MobileNo FROM CustomerProfile WHERE customerId=TL.CustomerId) AS MobileNo,ContraRef,TobrId FROM TransactionList TL WHERE RefNo='" + relationId + "'";
            DataSet headerInfoUnsave = objServiceHandler.ExecuteQuery(headerInfo);
            if (headerInfoUnsave.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in headerInfoUnsave.Tables["Table1"].Rows)
                {
                    txtBillNo.Text = prow["Vno"].ToString();
                    ddlBranch.SelectedValue = prow["TobrId"].ToString();
                    string contraRef = prow["ContraRef"].ToString();
                    string salesBillNo = objServiceHandler.ReturnString("SELECT VNO FROM TRANSACTIONLIST WHERE REFNO='" + contraRef + "'");
                    txtSalesBillNo.Text = salesBillNo;
                    string customerId = prow["CustomerId"].ToString();
                    string mobileNo = prow["MobileNo"].ToString();
                    if (!customerId.Equals("0"))
                    {
                        LoadCustomer(customerId, mobileNo);
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
    private void loadgdvSalesDetails(string billNo)
    {
        try
        {

            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,(TD.rate * Qnty) Amount ,TD.Vat,TD.Discount,(TD.Amount) TotalAmount FROM TransactionDetails TD, TransactionList TL,Item I WHERE  TL.RefNo=TD.TransactionList_Id AND TD.ItemId=I.ItemId AND  TL.VType='SR' AND TL.Vno='" + billNo + "' AND TL.BRID='" + branchId + "' ";
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
        string strSql = "";
        lblMesseage.Text = "";
        txtVat.Text = "0.00";
        txtDiscount.Text = "0.00";
        string voucherType = "SR";
        try
        {
            strSql = "SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + billNo + "' ";
            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery(strSql);
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prows["REFNO"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        strSql = "SELECT VNO,CustomerId,(SELECT MobileNo FROM CustomerProfile WHERE CustomerId=TL.CustomerId) AS MobileNo, TotalBill,Commission,Vat,AmountPaid,ContraRef,ToBrId FROM TransactionList TL where refNo='" + relationId + "'";
                        DataSet result = objServiceHandler.ExecuteQuery(strSql);
                        if (result.Tables["Table1"].Rows.Count == 1)
                        {
                            foreach (DataRow prow in result.Tables["Table1"].Rows)
                            {
                                string contraRefNo = prow["ContraRef"].ToString();
                                txtBillNo.Text = prow["VNO"].ToString();
                                string salesBillNo = objServiceHandler.ReturnString("SELECT VNO FROM TRANSACTIONLIST WHERE REFNO='" + contraRefNo + "'");
                                txtSalesBillNo.Text = salesBillNo;
                                ddlBranch.SelectedValue = prow["TobrId"].ToString();
                                txtTotalPrice.Text = prow["TotalBill"].ToString();
                                txtDiscount.Text = prow["Commission"].ToString();
                                txtVat.Text = prow["Vat"].ToString();
                                txtNetAmount.Text = prow["TotalBill"].ToString();
                                ddlCustomer.Items.Clear();
                                string customerId = prow["CustomerId"].ToString();
                                string mobileNo=prow["MobileNo"].ToString();
                                if (!customerId.Equals("0"))
                                {
                                    LoadCustomer(customerId, mobileNo);
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
        txtSalesReturnPrice.Text = "0.00";
        txtAmount.Text = "0.00";
        txtSalesVatPer.Text = "0.00";
        txtTotalAmount.Text = "0.00";
        txtDiscountAmount.Text = "0";
    }
    private void ClearAllFilled()
    {
        txtBillNo.Text = "";
        //ddlCustomer.SelectedIndex = 0;
       // ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtAmount.Text = "0.00";
        txtSalesReturnPrice.Text = "0.00";
        txtSalesVatPer.Text = "0.00";
        txtTotalAmount.Text = "0.00";
        txtTotalItem.Text = "0";
        txtTotalQuantity.Text = "0";
        txtTotalPrice.Text = "0.00";
        txtVat.Text = "0.00";
        txtDiscount.Text = "0.00";
        txtNetAmount.Text = "0.00";
        lblMesseage.Text = "";
        txtSearchChallan.Text = "";
        txtDiscountAmount.Text = "0";
    }

    private void customerPaymentUpdate(string customerId, string paymentAmount, string relationId)
    {
        try
        {
            if(customerId.Equals(""))
            {
                customerId = "0";
            }
            double receivedAmount = double.Parse(paymentAmount) * -1 ;
            paymentAmount = receivedAmount.ToString();
            string paymentUpdate = objCommon.AddCustomerPayment(customerId, relationId, paymentAmount, branchId, userId);
            if (!paymentUpdate.Equals("Successful"))
            {
                lblMesseage.Text = "Payment Update Failled !";
            }
        }
        catch
        {
            lblMesseage.Text = "Customer Payment Update Failled !";
            return;
        }
    }

    #endregion
    protected void btnFindSales_Click(object sender, EventArgs e)
    {
        string relationId = "";
        Session["TransactionListId"] = "";
        string customerId = "";
        string mobileNo = "";
        lblMesseage.Text = "";
       
        try
        {
            if (txtSalesBillNo.Text.Equals(""))
            {
                lblMesseage.Text = "Please Input Sales Bill No ";
                return;
            }

            DataSet checkRefNo = objServiceHandler.ExecuteQuery("SELECT tl.refno,TL.CustomerId,(SELECT MobileNo FROM CustomerProfile WHERE CustomerId=TL.CustomerId) AS MobileNo FROM TransactionList TL  WHERE  TL.BrId='" + ddlBranch.SelectedValue + "' AND TL.VType='SA' AND TL.Vno='" + txtSalesBillNo.Text + "' ");
            if (checkRefNo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkRefNo.Tables["Table1"].Rows)
                {
                    relationId = prows["REFNO"].ToString();
                    customerId = prows["CustomerId"].ToString();
                    mobileNo = prows["MobileNo"].ToString();
                    LoadSalesItemList(relationId);
                    LoadCustomer(customerId, mobileNo);
                    Session["TransactionListId"] = relationId.ToString();
                }
            }
            else
            {
                lblMesseage.Text = "Please Input Valid Sales Bill No";
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
            string billNo = txtBillNo.Text;
            string itemId = ddlItem.SelectedValue;
            string qntity = txtQuantity.Text;
            string salesPrice = txtSalesReturnPrice.Text;
            string SalesVatPer = txtSalesVatPer.Text;
            string discountAmount = txtDiscountAmount.Text;
            string voucherType = "SR";


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

            if(billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No";
                return;
            }

            if (Addnew.Equals("True"))
            {
                string strToBranch = ddlBranch.SelectedValue;
                string customerId = ddlCustomer.SelectedValue;
                if (txtSalesBillNo.Equals(""))
                {
                    lblMesseage.Text = "Please input sales bill No ! ";
                    return;
                }

                if (customerId.Equals(""))
                {
                    customerId = "0";
                    return;
                }
                LoadBillNo(voucherType);
                string voucherNo = txtBillNo.Text;
                string contraRefNo = Session["TransactionListId"].ToString();

                string result = objCommon.AddItemTransactionList(branchId, voucherType, voucherNo, userId, contraRefNo, "0", customerId, strToBranch);
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
                if(costPrice.Equals("0"))
                {
                    costPrice = salesPrice;
                }

                    string result = objCommon.AddTransactionDetails(itemId, qntity, salesPrice, costPrice, SalesVatPer, discountAmount,relationId);

                    if (result.Equals("Successful"))
                    {
                        txtVat.Text = "0";
                        txtTotalPrice.Text = "0.00";
                        lblMesseage.Text = "Item Added";
                        loadgdvSalesDetails(txtBillNo.Text);
                        totalCalculation(relationId);
                        ClearHeaderFilled();

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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            Addnew = "True";
            string voucherType = "SR";
            ClearAllFilled();
            LoadBillNo(voucherType);
            string voucherNo = txtBillNo.Text;
            loadgdvSalesDetails("");
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
        
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
       // btnAdd.Enabled = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string relationId = "";
            string billNo = txtBillNo.Text;
            string salesBillNo=txtSalesBillNo.Text;
            string fromBranch = ddlBranch.SelectedValue;
            string voucherType = "SR";
            string customerId = ddlCustomer.SelectedValue;
            string totalPrice = txtTotalPrice.Text;
            string vat = txtVat.Text;
            string comission = txtDiscount.Text;
            string netAmount = txtNetAmount.Text;
            string posted = "True";
            string postedDate=DateTime.Now.ToString();

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

                        string result = objCommon.UpdateTransactionList(relationId, customerId, "0", totalPrice, vat, comission, "0", userId, postedDate, posted, fromBranch, userId, postedDate, salesBillNo);
                        if (result.Equals("Successfull"))
                        {

                            lblMesseage.Text = "Sales Save Successfully";
                            customerPaymentUpdate(customerId, netAmount, relationId);

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
                                    double currentBalance = prevBalance + currentquantity;
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
                            lblMesseage.Text = "Sales Save Failed";
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

            loadgdvSalesDetails(txtSearchChallan.Text);
            LoadFooterInfo(txtSearchChallan.Text);
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
      

    }
    protected void btnVatComission_Click(object sender, EventArgs e)
    {

    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtQuantity.Text = "1";
            string salesRefNo = Session["TransactionListId"].ToString();
            string soldQntity = "";
            string salesReturnItemQnty = "";
            double salesReturnQnty = 0;
            if(salesRefNo.Equals(""))
            {
                lblMesseage.Text = "Please Find Sales Bill First";
                return;
            }
            string strItemId = ddlItem.SelectedValue;
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT Qnty,rate,contraRate,vat,discount FROM TransactionDetails WHERE TransactionList_Id='" + salesRefNo + "' AND  ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    txtSalesReturnPrice.Text = prow["rate"].ToString();
                    soldQntity = prow["Qnty"].ToString();
                    string contraRate = prow["contraRate"].ToString();
                    txtSalesVatPer.Text = prow["vat"].ToString();
                    txtDiscountAmount.Text = prow["discount"].ToString();

                  DataSet previousRefNo = objServiceHandler.ExecuteQuery("SELECT REFNO FROM TransactionLIST WHERE CONTRAREF='"+salesRefNo+"'");
                  if (previousRefNo.Tables["Table1"].Rows.Count >0)
                  {
                      foreach (DataRow prowsr in previousRefNo.Tables["Table1"].Rows)
                      {
                          string salesReturnRefNo = prowsr["REFNO"].ToString();

                         DataSet existReturnQnty = objServiceHandler.ExecuteQuery("SELECT QNTY FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='"+salesReturnRefNo+"' AND ITEMID='"+strItemId+"'");
                         if (existReturnQnty.Tables["Table1"].Rows.Count > 0)
                         {
                             foreach (DataRow prowsrqnty in existReturnQnty.Tables["Table1"].Rows)
                             {
                                 salesReturnItemQnty = prowsrqnty["QNTY"].ToString();
                             }
                             salesReturnQnty = salesReturnQnty + double.Parse(salesReturnItemQnty);
                         }
                      }
                      double remainingQnty = double.Parse(soldQntity) - salesReturnQnty;
                      txtRemainingQnty.Text = remainingQnty.ToString();
                  }
                    else
                  {
                      txtRemainingQnty.Text = soldQntity.ToString();
                  }
                  

                    double rate = Convert.ToDouble(txtSalesReturnPrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);

                    double amount = rate * quantity;
                    txtAmount.Text = amount.ToString();

                    amount = amount + Convert.ToDouble(txtSalesVatPer.Text);
                    txtTotalAmount.Text = amount.ToString();
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
            double salesPrice = Convert.ToDouble(txtSalesReturnPrice.Text);
            double quntity = Convert.ToDouble(txtQuantity.Text);
            double remainingQnty = Convert.ToDouble(txtRemainingQnty.Text);
            if (quntity > remainingQnty)
            {
                lblMesseage.Text = "Not enough Remaining Balance";
                return;
            }
            double amount = (salesPrice * quntity);
            txtAmount.Text = amount.ToString();

            amount = amount + Convert.ToDouble(txtSalesVatPer.Text);
            txtTotalAmount.Text = amount.ToString();
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
            string billNo = txtBillNo.Text;

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='SR' AND VNO='" + billNo + "' ");
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
                            loadgdvSalesDetails(billNo);
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