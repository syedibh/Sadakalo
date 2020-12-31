using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmSales : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsTransaction objTransaction = new clsTransaction();
    clsItemDependency objItemDependency = new clsItemDependency();
    public static string branchId;
    public static string userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchId = Session["BranchID"].ToString();
            userId = Session["UserID"].ToString();
            if(!IsPostBack)
            {
                LoadItemId();
                LoadItemList();
                LoadCustomerList();
                loadgdvCustomer();
                loadgdvItemMaster();
                ShowLastBillNo();
                onLoadFilled();
                loadgdvSalesDetails(txtBillNo.Text);
                LoadFooterInfo(txtBillNo.Text);

            }
           
        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

    # region DropdownList

    private void LoadItemId()
    {
        try
        {

            string strSql = "SELECT ITEMID,ITEMDESCRIPTION FROM ITEM   WHERE Item.[" + branchId + "]>0 and  STATUSID NOT IN ('3','2')   ORDER BY ITEMDESCRIPTION ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlItemId.DataSource = oDs;
            ddlItemId.DataValueField = "ITEMID";
            ddlItemId.DataTextField = "ITEMID";
            ddlItemId.DataBind();

            ddlItemId.Items.Insert(0, new ListItem("-- Select ItemId --"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadItemList()
    {
        try
        {

            string strSql = "SELECT ITEMID,ITEMDESCRIPTION FROM ITEM   WHERE Item.[" + branchId + "]>0 and  STATUSID NOT IN ('3','2')   ORDER BY ITEMDESCRIPTION ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlItem.DataSource = oDs;
            ddlItem.DataValueField = "ITEMID";
            ddlItem.DataTextField = "ITEMDESCRIPTION";
            ddlItem.DataBind();

            ddlItem.Items.Insert(0, new ListItem("-- Select Item --"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadCustomerList()
    {
        try
        {

            string strSql = "SELECT CustomerId,MobileNo FROM  CustomerProfile  ORDER BY CustomerName";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlCustomer.DataSource = oDs;
            ddlCustomer.DataValueField = "CustomerId";
            ddlCustomer.DataTextField = "MobileNo";
            ddlCustomer.DataBind();

            ddlCustomer.Items.Insert(0, new ListItem(" -- Select Customer --"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    # endregion

    #region Sales

    private void onLoadFilled()
    {
        txtQuantity.Text = "1";
        txtBillNo.Enabled = false;
        txtAmount.Enabled = false;
        txtSalesPrice.Enabled = false;
        txtTotalItem.Enabled = false;
        txtTotalQuantity.Enabled = false;
        txtTotalPrice.Enabled = false;
        txtNetAmount.Enabled = false;
    }
    private void ClearHeaderFilled()
    {
       // txtItemBarcode.Text = "";
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtSalesPrice.Text = "0.00";
        txtAmount.Text = "0.00";
        txtSalesPrice.Text = "0.00";
        txtSalesVatPer.Text = "0.00";
        txtTotalAmount.Text = "0.00";
        txtDiscountAmount.Text = "0";
    }
    private void ClearAllFilled()
    {
        txtBillNo.Text = "";
        ddlCustomer.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtAmount.Text = "0.00";
        txtSalesPrice.Text = "0.00";
        txtSalesVatPer.Text = "0.00";
        txtTotalAmount.Text = "0.00";
        txtTotalItem.Text = "0";
        txtTotalQuantity.Text = "0";
        txtTotalPrice.Text = "0.00";
        txtVat.Text = "0.00";
        txtDiscount.Text = "0.00";
        txtNetAmount.Text = "0.00";
        btnAddNew.Enabled = false;
        btnAdd.Enabled = true;
        btnSave.Enabled = true;
        lblMesseage.Text = "";
        txtSearchChallan.Text = "";
        txtDiscountPer.Text = "0";
        txtDiscountAmount.Text = "0";
    }
    private void ShowLastBillNo()
    {
        int branchId = Convert.ToInt32(Session["BranchID"].ToString());

        string strSql = "";
        strSql = "SELECT MAX(VNO) AS VNO FROM TransactionList WHERE VType='SA' AND BrId='" + branchId + "';";
        string billNo = objServiceHandler.ReturnString(strSql);
        txtBillNo.Text = billNo.ToString();
    }

    private void ShowNewBillNo()
    {
        try
        {


            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM TransactionList WHERE VType='SA' AND BrId='" + branchId + "';";
            string billNo = objServiceHandler.ReturnString(strSql);
            if(billNo.Equals(null))
            {
                billNo = "1";
            }
            txtBillNo.Text = billNo.ToString();

        }
        catch
        {
            return;
        }
    }

    private void totalCalculation(string relationId)
    {
        try
        {
            //double totalQ;
            //totalQ = double.Parse(txtTotalQuantity.Text);
            //totalQ = totalQ + double.Parse(txtQuantity.Text);
            //txtTotalQuantity.Text = totalQ.ToString();

            //double findtotal;
            //findtotal = (double.Parse(txtTotalPrice.Text));
            //findtotal = findtotal + double.Parse(txtTotalAmount.Text);
            //txtTotalPrice.Text = findtotal.ToString();
           
            //double totalVat;
            //totalVat = double.Parse(txtVat.Text);
            //totalVat = totalVat+ double.Parse(txtSalesVatPer.Text);
            //txtVat.Text = totalVat.ToString();

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

                }
            }

            if(!txtTotalPrice.Text.Equals(""))
            {
                double netAmount;
                netAmount = double.Parse(txtTotalPrice.Text);
                txtNetAmount.Text = netAmount.ToString();

                double paidAmount;
                paidAmount = double.Parse(txtTotalPrice.Text);
                txtPaidAmount.Text = paidAmount.ToString();
            }
            else
            {
                txtNetAmount.Text = "0.00";
                txtPaidAmount.Text = "0.00";
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

            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,(TD.rate * Qnty) Amount ,TD.Vat,TD.Discount,(TD.Amount) TotalAmount FROM TransactionDetails TD, TransactionList TL,Item I WHERE  TL.RefNo=TD.TransactionList_Id AND TD.ItemId=I.ItemId AND  TL.VType='SA' AND TL.Vno='" + billNo + "' AND TL.BRID='" + branchId + "' ";
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
        lblMesseage.Text = "";
        txtVat.Text = "0.00";
        txtDiscount.Text = "0.00"; 
        string voucherType = "SA";
        try
        {
            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + billNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prows["REFNO"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        DataSet result = objServiceHandler.ExecuteQuery("SELECT RefNo,CustomerId,TotalBill,Commission,Vat,AmountPaid FROM TransactionList where BrId='" + branchId + "' and VType='"+voucherType+"' and Vno='" + billNo + "'");
                        if (result.Tables["Table1"].Rows.Count == 1)
                        {
                            foreach (DataRow prow in result.Tables["Table1"].Rows)
                            {
                                Session["RefNo"] = prow["RefNo"].ToString();
                                txtTotalPrice.Text = prow["TotalBill"].ToString();
                                txtDiscount.Text = prow["Commission"].ToString();
                                txtVat.Text = prow["Vat"].ToString();
                                txtNetAmount.Text = prow["TotalBill"].ToString();
                                txtPaidAmount.Text = prow["AmountPaid"].ToString();
                                ddlCustomer.Items.Clear();
                                LoadCustomerList();
                                string customerId= prow["CustomerId"].ToString();
                                if(!customerId.Equals("0"))
                                {
                                    ddlCustomer.SelectedValue = customerId;
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
       
        try
        {
            Session["REFNO"] = "";
            ClearAllFilled();
            ShowNewBillNo();
            string billNo = txtBillNo.Text;
            string voucherType = "SA";
            string result = objTransaction.AddNewSales(branchId, voucherType, billNo, userId);
            if (result.Equals("Successful"))
            {
                string referenceNo = objServiceHandler.ReturnString("SELECT IDENT_CURRENT ('TransactionList') REFERENCENO ");
                Session["REFNO"] = referenceNo.ToString();
                loadgdvSalesDetails(txtBillNo.Text);
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
            string voucherType = "SA";
            string billNo = txtBillNo.Text;
            string itemId = ddlItem.SelectedValue;
            string qntity = txtQuantity.Text;
            string salesPrice = txtSalesPrice.Text;
            string contraRate = txtSalesPrice.Text;
            string SalesVatPer = txtSalesVatPer.Text;
            string discountAmount = txtDiscountAmount.Text;

            if (ddlItem.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Item ";
                return;
            }
            if (qntity.Equals("0"))
            {
                lblMesseage.Text = "Please input quantity minumum 1  ";
                return;
            }
            if (salesPrice.Equals("0.00"))
            {
                lblMesseage.Text = "Please Input Sales Price ";
                return;
            }

            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No";
                return;
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

            string costPrice = objServiceHandler.ReturnString("SELECT [dbo].[CostPrice]('" + branchId + "' , '" + itemId + "') as Costprice");
           

            string balance = objServiceHandler.ReturnString("SELECT [" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + itemId + "'");
            {
                double currentBalance = Convert.ToDouble(balance);
                double currentQuantity = Convert.ToDouble(txtQuantity.Text);
                if(currentQuantity>currentBalance)
                {
                    lblMesseage.Text = "You have not enough Balance";
                    return;
                }
                else
                {
                    string result = objTransaction.AddSalesDetails(itemId, qntity, salesPrice, costPrice, relationId, SalesVatPer, discountAmount);

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

        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
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
            string billNo = txtBillNo.Text;
            string voucherType = "SA";
            string customerId = ddlCustomer.SelectedValue;
            string totalPrice = txtTotalPrice.Text;
            string vat = txtVat.Text;
            string comission = txtDiscount.Text;
            string netAmount = txtNetAmount.Text;
            string paidAmount = txtPaidAmount.Text;
            string posted = "True";
            string receivedBy=ddlReceivedBy.SelectedValue;
            string transactionNote=txtTransactionNote.Text;

            if (ddlCustomer.SelectedIndex == 0)
            {
                customerId = "0";
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
                        lblMesseage.Text = "This Bill Already Posted ! ";
                        return;
                    }

                    else
                    {

                        string result = objTransaction.UpdateSales(branchId, voucherType, billNo, relationId, customerId, totalPrice, vat, comission, paidAmount, userId, posted);
                        if (result.Equals("Successfull"))
                        {

                            lblMesseage.Text = "Sales Save Successfully";
                            customerPaymentUpdate(customerId, paidAmount, relationId,receivedBy,transactionNote);
                            btnAddNew.Enabled = true;
                            btnAdd.Enabled = false;
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

                                    string BalanceUpdate = objTransaction.UpdateBalanace(itemId, quantity, branchId);
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

    private void customerPaymentUpdate(string customerId,string paymentAmount,string relationId,string receivedBy,string transactionNote)
    {
        try
        {
            string paymentUpdate = objTransaction.InsertCustomerPayment(customerId, relationId, paymentAmount, branchId, userId,receivedBy,transactionNote);
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
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        {
            try
            {
                Session["RefNo"] = "";
                lblMesseage.Text = "";
                if (txtBillNo.Text == "")
                {
                    lblMesseage.Text = "First select a bill";
                    return;
                }

                //string refNo= checkPosting(txtBillNo.Text);
                string qryString = "SELECT REFNO  FROM TRANSACTIONLIST WHERE posted=1 and  BRID='" + branchId + "' AND VTYPE='" + "SA" + "' AND VNO='" + txtBillNo.Text + "'";
                string refNo = objServiceHandler.ReturnString(qryString);


                Session["RefNo"] = refNo.ToString();
             //   Response.Redirect("~/Forms/SalesMemo.aspx?ref_no=" + refNo);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Forms/SalesMemo.aspx','_newtab');", true);


            }
            catch
            {
            }
        }
    }
    protected void btnUnposted_Click(object sender, EventArgs e)
    {

    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMesseage.Text = "";
            txtBillNo.Text = "";
            txtVat.Text = "0.00";
            txtDiscount.Text = "0.00"; 
            string billNo = txtSearchChallan.Text;
            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No For Search";
                return;
            }
            loadgdvSalesDetails(billNo);
            LoadFooterInfo(billNo);
            txtBillNo.Text = billNo.ToString();

        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;

        }
    }
    protected void btnVatComission_Click(object sender, EventArgs e)
    {
        try
        {

        
        string relationId = "";
        string voucherType = "SA";
        string billNo = txtBillNo.Text;
        string discountAmount=txtDiscount.Text;

        

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
                     string totalPrice = objServiceHandler.ReturnString("SELECT SUM(QNTY*RATE) TOTALPRICE FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                     DataSet salesDetailsInfo = objServiceHandler.ExecuteQuery("SELECT ITEMID,(Qnty * rate) AMOUNT FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                     if (salesDetailsInfo.Tables["Table1"].Rows.Count> 0)
                     {
                         foreach (DataRow prow in salesDetailsInfo.Tables["Table1"].Rows)
                         {
                             string itemId = prow["ITEMID"].ToString();
                             string amount =prow["AMOUNT"].ToString();

                             double discountPerItem = (double.Parse(discountAmount) * double.Parse(amount) / double.Parse(totalPrice));

                             string discountUpdate = objTransaction.UpdateDiscount(itemId, discountPerItem, relationId);
                             
                         }
                         loadgdvSalesDetails(billNo);
                         totalCalculation(relationId);

                     }
                     else
                     {
                         lblMesseage.Text = "Discount Updated Failled";
                         return;
                     }
                 }
             }
         }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }


    }
    protected void gdvItemDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow rows = gdvItemDetails.Rows[e.RowIndex];
            string strItemId = gdvItemDetails.DataKeys[e.RowIndex].Value.ToString();

            TextBox txtQuantity = gdvItemDetails.Rows[e.RowIndex].FindControl("txtQuantityE") as TextBox;
            string strQuantity = txtQuantity.Text.Trim() != null ? txtQuantity.Text.Trim() : "0";

            Label txtContraRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblSalesRate") as Label;
            string strSalesRate = txtContraRate.Text.Trim() != null ? txtContraRate.Text.Trim() : "0";

            Label txtAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string strAmount = txtAmount.Text.Trim() != null ? txtAmount.Text.Trim() : "0";

            Label lblQuantityL = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantityH") as Label;
            string Quantity = lblQuantityL.Text.Trim() != null ? lblQuantityL.Text.Trim() : "0";
           
            Label lblAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string Amount = lblAmount.Text.Trim() != null ? lblAmount.Text.Trim() : "0";

            Label lblTotalAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblTotalAmount") as Label;
            string TotalAmount = lblTotalAmount.Text.Trim() != null ? lblTotalAmount.Text.Trim() : "0";

            Label txtTotalAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblTotalAmount") as Label;
            string strTotalAmount = txtTotalAmount.Text.Trim() != null ? txtTotalAmount.Text.Trim() : "0";

            Label lblSalesVat = gdvItemDetails.Rows[e.RowIndex].FindControl("lblSalesVat") as Label;
            string strSalesVat = lblSalesVat.Text.Trim() != null ? lblSalesVat.Text.Trim() : "0";



            string billNo = txtBillNo.Text;

            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No ";
                return;
            }

            if (strQuantity.Equals(""))
            {
                lblMesseage.Text = "Please Input valid Qntity ";
                return;
            }

            if (strQuantity.Equals("0"))
            {
                lblMesseage.Text = "Please  Input valid Qntity ";
                return;
            }

            if (strSalesRate.Equals(""))
            {
                lblMesseage.Text = "Please Input valid  Rate ";
                return;
            }

            if (strSalesRate.Equals("0"))
            {
                lblMesseage.Text = "Please  Input valid  Rate ";
                return;
            }


            double amount = double.Parse(Amount);
            double totalAmount = double.Parse(TotalAmount);
            double salesVat=double.Parse(strSalesVat);
            double quantity = double.Parse(Quantity);
            double tQuantity = double.Parse(txtTotalQuantity.Text);
            double tPrice = double.Parse(txtTotalPrice.Text);
            double netAmount = double.Parse(txtNetAmount.Text);

            double amountE = double.Parse(strAmount);
            double totalAmountE = double.Parse(strTotalAmount);
            double quantityE = double.Parse(strQuantity);
            double rateE = double.Parse(strSalesRate);
            amountE = quantityE * rateE;
            strAmount = amountE.ToString();
           
            strTotalAmount = totalAmountE.ToString();

            string balance = objServiceHandler.ReturnString("SELECT [" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
            string salesVatPer = objServiceHandler.ReturnString("SELECT SalesVatPer FROM ITEM WHERE ITEMID='" + strItemId + "'");
            double currentBalance = Convert.ToDouble(balance);
            if (quantityE > currentBalance)
            {
                lblMesseage.Text = "You have not enough balance";
                return;
            }

            if (salesVat > 0)
            {
                double salesvatpercantage = double.Parse(salesVatPer);
                double vatAmount = ((amountE * salesvatpercantage) / 100);
                strSalesVat = vatAmount.ToString();
                totalAmountE = amountE + vatAmount;
            }

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='SA' AND VNO='" + billNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prow["REFNO"].ToString();
                    string status = prow["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You are not allowed edit posted bill";
                        return;
                    }
                    else
                    {
                        string result = objTransaction.UpdateSalesDetailsInfo(relationId, strItemId, strQuantity, strSalesRate, strSalesVat);
                        if (result.Equals("Successfull"))
                        {
                            gdvItemDetails.EditIndex = -1;
                            loadgdvSalesDetails(billNo);
                            lblMesseage.Text = "Item Update Successfully";
                            totalCalculation(relationId);


                        }
                        else
                        {
                            lblMesseage.Text = "Item Updated Failed";
                            return;
                        }
                    }
                }

            }
            else
            {
                lblMesseage.Text = "Something went wrong";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void gdvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvItemDetails.EditIndex = e.NewEditIndex;
            loadgdvSalesDetails(txtBillNo.Text);
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something Went Wrong";
        }
    }
    protected void gdvItemDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            gdvItemDetails.EditIndex = -1;
            loadgdvSalesDetails(txtBillNo.Text);
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void gdvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        try
        {
            GridViewRow rows = gdvItemDetails.Rows[e.RowIndex];
            string strItemId = gdvItemDetails.DataKeys[e.RowIndex].Value.ToString();
            string billNo = txtBillNo.Text;
            Label lblQuantity = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantity") as Label;
            string strQuantity = lblQuantity.Text.Trim() != null ? lblQuantity.Text.Trim() : "0";

            Label lblRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblSalesRate") as Label;
            string strRate = lblRate.Text.Trim() != null ? lblRate.Text.Trim() : "0";

            Label lblAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblTotalAmount") as Label;
            string strAmount = lblAmount.Text.Trim() != null ? lblAmount.Text.Trim() : "0";

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='SA' AND VNO='" + billNo + "' ");
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
    protected void gdvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvItemDetails.PageIndex = e.NewPageIndex;
            loadgdvSalesDetails(txtBillNo.Text);

        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strItemId = ddlItem.SelectedValue; 
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT PurchasePrice,SalesPrice,SalesVatPer,[" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    txtSalesPrice.Text = prow["SalesPrice"].ToString();
                    txtCurrentBalance.Text = prow["Stock"].ToString();
                    string salesVatPer = prow["SalesVatPer"].ToString();
                    double salesPrice = Convert.ToDouble(txtSalesPrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);
                    double amount = salesPrice * quantity;
                    txtAmount.Text = amount.ToString();

                    txtSalesVatPer.Text = salesVatPer;
                    txtTotalAmount.Text = amount.ToString();

                    double salesvatper = double.Parse(salesVatPer);
                    if (salesvatper > 0)
                    { 
                    double vatAmount = ((amount * salesvatper) / 100);
                    double totalAmount = (amount + vatAmount);
                    txtSalesVatPer.Text = vatAmount.ToString();
                    txtTotalAmount.Text = totalAmount.ToString();
                    }

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
        string strItemId = ddlItem.SelectedValue;
        double salesPrice = Convert.ToDouble(txtSalesPrice.Text);
        double quntity = Convert.ToDouble(txtQuantity.Text);
        double amount = (salesPrice * quntity);
        txtAmount.Text = amount.ToString();

        string salesVatPer = objServiceHandler.ReturnString("SELECT SalesVatPer FROM ITEM WHERE ITEMID='" + strItemId + "'");

        txtSalesVatPer.Text = salesVatPer;
        txtTotalAmount.Text = amount.ToString();

        double salesvatper = double.Parse(salesVatPer);
       

    }

    protected void btnMesseage_Click(object sender, EventArgs e)
    {
        try
        {
            lblMesseage.Text = "";
            lblUserContent.Text = "";
            double netAmount = double.Parse(txtNetAmount.Text);
            double receivedAmount = double.Parse(txtReceivedAmount.Text);
            double changeAmount = receivedAmount - netAmount;
            lblShowResul.Text = "Refundable Amount =" + changeAmount.ToString();

            modalMesseage.Show();
        }

        catch
        {
            lblMesseage.Text = "Please Input Received Amount";
        }
    }
    protected void btnMyCollection_Click(object sender, EventArgs e)
    {
        try
        {
            lblMesseage.Text = "";
            lblUserContent.Text = "";
            string userName = Session["LoginName"].ToString();
            string strSql = "SELECT SUM(AMOUNT) TOTALCOLLECTION FROM CUSTOMERPAYMENT WHERE USERID='1' AND FORMAT(DATE, 'dd/MM/yyyy')=FORMAT(GETDATE(), 'dd/MM/yyyy') ";
            string myCollection = objServiceHandler.ReturnString(strSql);
            lblShowResul.Text = "Total Collection Amount =" + myCollection.ToString();
            lblUserContent.Text = "Username :" + userName.ToString();
            modalMesseage.Show();
        }

        catch
        {
            lblMesseage.Text = "Your Collection Zero";
        }
    }
    protected void txtDiscountPer_TextChanged(object sender, EventArgs e)
    {
        if (txtDiscountPer.Text != "" && txtDiscountPer.Text != null)
        {
            double discountPer = double.Parse(txtDiscountPer.Text);
            double totalAmount = double.Parse(txtTotalPrice.Text);
            double discountAmount = ((totalAmount * discountPer) / 100);
            txtDiscount.Text = discountAmount.ToString();
        }

    }

    private void loadgdvItemMaster()
    {
        string filterName = "";
        // int branchId = Convert.ToInt32(Session["BranchID"].ToString());
        string seacrCategoryName = ddlSearchItemMaster.SelectedValue;
        try
        {
            if (!txtSearchItem.Text.Equals(""))
            {

                filterName = "AND " + seacrCategoryName + " LIKE '%" + txtSearchItem.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by I.ITEMID desc) as SL_NO,I.ITEMID,I.ITEMDESCRIPTION,I.SUPPLIERID,S.SUPPLIERNAME,I.CATEGORYID,C.CATEGORYNAME,I.UNITID,U.UNITNAME,I.StatusId,IT.StatusName,I.SALESPRICE,I.[1] AS MainStore,I.[2] AS Mirpur,I.[3] AS Mohammadpur FROM ITEM I,SUPPLIER S,CATEGORY C,UNIT U,ItemStatus IT WHERE I.SUPPLIERID=S.SUPPLIERID AND I.UNITID=U.UNITID AND I.CATEGORYID=C.CATEGORYID AND I.STATUSID=IT.STATUSID   " + filterName + " ORDER BY I.ITEMID DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvItemMaster.DataSource = oDs;
            gdvItemMaster.DataBind();
        }
        // }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }
    protected void btnSearchItem_Click(object sender, EventArgs e)
    {
        modalItemMaster.Show();
        loadgdvItemMaster();
    }
    protected void ddlItemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strItemId = ddlItemId.SelectedValue;
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT PurchasePrice,SalesPrice,SalesVatPer,[" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    ddlItem.Items.Clear();
                    LoadItemList();
                    ddlItem.SelectedValue = strItemId;
                    txtSalesPrice.Text = prow["SalesPrice"].ToString();
                    txtCurrentBalance.Text = prow["Stock"].ToString();
                    string salesVatPer = prow["SalesVatPer"].ToString();
                    double salesPrice = Convert.ToDouble(txtSalesPrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);
                    double amount = salesPrice * quantity;
                    txtAmount.Text = amount.ToString();

                    txtSalesVatPer.Text = salesVatPer;
                    txtTotalAmount.Text = amount.ToString();

                    double salesvatper = double.Parse(salesVatPer);
                    if (salesvatper > 0)
                    {
                        double vatAmount = ((amount * salesvatper) / 100);
                        double totalAmount = (amount + vatAmount);
                        txtSalesVatPer.Text = vatAmount.ToString();
                        txtTotalAmount.Text = totalAmount.ToString();
                    }

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

    #endregion 
   

    #region CustomerProfile

    private void loadgdvCustomer()
    {
        string filterName = "";
        string seacrCategoryName = ddlSearch.SelectedValue;
        try
        {
            if (txtSearch.Text != "")
            {
                filterName = "AND " + seacrCategoryName + " LIKE '%" + txtSearch.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by CustomerId desc) as SL_NO,CustomerId,CustomerName,MailingAddress,MobileNo,EmailAddress,WebAddress FROM  CustomerProfile WHERE BRID='" + branchId + "' " + filterName + " ORDER BY CustomerId DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvCustomerProfile.DataSource = oDs;
            gdvCustomerProfile.DataBind();
        }
        // }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblMessage.Text = "Something went wrong.";
        }
    }

    private void ClearCustomerFild()
    {
        txtCustomerId.Text = "";
        txtCustomerName.Text = "";
        txtCustomerAddress.Text = "";
        txtWebAddress.Text = "";
        txtEmailAddress.Text = "";
        txtMobileNo.Text = "";
        btnAddNewCustomer.Enabled = true;
        btnSaveCustomer.Enabled = false;
    }
    private void ShowMaxCustomerId()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT MAX(CustomerId)+1 FROM CustomerProfile where BrId='" + branchId + "'";
            string customerId = objServiceHandler.ReturnString(strSql);
            txtCustomerId.Text = customerId.ToString();

        }
        catch
        {
            return;
        }

    }
     protected void btnAddNewCustomer_Click(object sender, EventArgs e)
    {
        modalCustomer.Show();
        ClearCustomerFild();
        ShowMaxCustomerId();
        btnAddNewCustomer.Enabled = false;
        btnSaveCustomer.Enabled = true;
   
    }
    protected void btnSaveCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            modalCustomer.Show();
            string strCustomerId =  txtCustomerId.Text ;
            string strCustomerName = txtCustomerName.Text;
            string strCustomerEmail = txtEmailAddress.Text;
            string strCustomerAddress = txtCustomerAddress.Text;
            string strCustomerMobile = txtMobileNo.Text;
            string strWebAddress =  txtWebAddress.Text ;

            if (strCustomerId.Equals(""))
            {
                lblMesseageCustomer.Text = "Please Click Add New Button First";
                return;
            }

            if (strCustomerName.Equals(""))
            {
                lblMesseageCustomer.Text = "Please Input Customer Name";
                return;
            }

            if (strCustomerMobile.Equals(""))
            {
                lblMesseageCustomer.Text = "Please Input Customer Mobile No";
                return;
            }

            DataSet checkExist = objServiceHandler.ExecuteQuery("SELECT * FROM CustomerProfile WHERE MobileNo='" + strCustomerMobile + "' AND BRID='"+branchId+"'");
            if (checkExist.Tables["Table1"].Rows.Count > 0)
            {
                lblMesseageCustomer.Text = "Customer already exist !";
                return;
            }

            string result = objItemDependency.AddNewCustomer(strCustomerId, strCustomerName, strCustomerEmail, strCustomerAddress, strCustomerMobile, strWebAddress,branchId,userId);

            if (result.Equals("Successful"))
            {
                ClearCustomerFild();
                loadgdvCustomer();
                lblMesseageCustomer.Text = "Customer Added Successfully";


            }
            else
            {
                lblMesseageCustomer.Text = "Customer Added Failled";
                return;
            }

        }
        catch
        {
            lblMesseageCustomer.Text = "Something went wrong !";
            return;
        }
       
    }
    protected void btnClearCustomer_Click(object sender, EventArgs e)
    {
        modalCustomer.Show();
        ClearCustomerFild();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            modalCustomer.Show();
            loadgdvCustomer();
        }
        catch
        {
            lblMesseageCustomer.Text = "Something went wrong !";
            return;
        }
       
    }
    protected void gdvCustomerProfile_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "";

        try
        {
            modalCustomer.Show();
            GridViewRow rows = gdvCustomerProfile.Rows[e.RowIndex];
            string strSupplierId = gdvCustomerProfile.DataKeys[e.RowIndex].Value.ToString();

            TextBox lblCustomerName = gdvCustomerProfile.Rows[e.RowIndex].FindControl("txtCustomerNameE") as TextBox;
            string strCustomerName = lblCustomerName.Text.Trim() != null ? lblCustomerName.Text.Trim() : "0";


            TextBox lblCustomerAddress = gdvCustomerProfile.Rows[e.RowIndex].FindControl("txtAddressE") as TextBox;
            string strCustomerAddress = lblCustomerAddress.Text.Trim() != null ? lblCustomerAddress.Text.Trim() : "0";

            TextBox lblCustomerEmail = gdvCustomerProfile.Rows[e.RowIndex].FindControl("txtEmailAddressE") as TextBox;
            string strCustomerEmail = lblCustomerEmail.Text.Trim() != null ? lblCustomerEmail.Text.Trim() : "0";

            TextBox lblMobile = gdvCustomerProfile.Rows[e.RowIndex].FindControl("txtMobileNoE") as TextBox;
            string strMobile = lblMobile.Text.Trim() != null ? lblMobile.Text.Trim() : "0";

            TextBox lblWebAddress = gdvCustomerProfile.Rows[e.RowIndex].FindControl("txtWebAddressE") as TextBox;
            string strWebAddress = lblWebAddress.Text.Trim() != null ? lblWebAddress.Text.Trim() : "0";



            if (strCustomerName.Equals(""))
            {
                lblMesseageCustomer.Text = "Please Input Customerr Name";
                return;
            }

            if (strMobile.Equals(""))
            {
                lblMesseageCustomer.Text = "Please Input Customer Mobile No";
                return;
            }

            DataSet checkExist = objServiceHandler.ExecuteQuery("SELECT * FROM CustomerProfile WHERE MobileNo='" + strMobile + "' AND BRID='" + branchId + "'");
            if (checkExist.Tables["Table1"].Rows.Count > 0)
            {
                lblMesseageCustomer.Text = "Customer already exist !";
                return;
            }


            //  DropDownList ddlEItemMasterCode = gdvBookingInfo.Rows[e.RowIndex].FindControl("ddlEItemMasterCode") as DropDownList;
            // ddlItemCode.Items.Insert(0, new ListItem(strItemCode.ToString(), strItemMasterID.ToString()));

            //  strSql = objServiceHandler.DeleteItemInfo(strItemId);

            strSql = objItemDependency.UpdateCustomerInfo(strSupplierId, strCustomerName, strCustomerAddress, strCustomerEmail, strMobile, strWebAddress, branchId);
            if (strSql.Equals("Successfull"))
            {
                ClearCustomerFild();
                gdvCustomerProfile.EditIndex = -1;
                loadgdvCustomer();
                lblMesseageCustomer.Text = "Customer Updated Successfully";

            }
            else
            {
                gdvCustomerProfile.EditIndex = -1;
                lblMesseageCustomer.Text = "Customer Updated Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageCustomer.Text = "Something went wrong !";
        }
    }
    protected void gdvCustomerProfile_RowEditing(object sender, GridViewEditEventArgs e)
    {
         try
        {
            modalCustomer.Show();
            gdvCustomerProfile.EditIndex = e.NewEditIndex;
            loadgdvCustomer();
        }
        catch (Exception)
        {
            lblMesseageCustomer.Text = "Something Went Wrong";
        }
    }
    protected void gdvCustomerProfile_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
          try
        {

            modalCustomer.Show();
            gdvCustomerProfile.EditIndex = -1;
            loadgdvCustomer();
        }
        catch (Exception)
        {
            lblMesseageCustomer.Text = "Something went wrong !";
        }
    }
    protected void gdvCustomerProfile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql = "";

        try
        {
            modalCustomer.Show();
            GridViewRow rows = gdvCustomerProfile.Rows[e.RowIndex];
            string strCustomerId = gdvCustomerProfile.DataKeys[e.RowIndex].Value.ToString();

            DataSet checkExist = objServiceHandler.ExecuteQuery("SELECT * FROM TransactionList WHERE CustomerId='" + strCustomerId + "' AND BrId='"+branchId+"'");
            if (checkExist.Tables["Table1"].Rows.Count > 0)
            {
                lblMesseageCustomer.Text = "Customer already use another tables";
                return;
            }

            strSql = objItemDependency.DeleteCustomerInfo(strCustomerId,branchId);

            if (strSql.Equals("Deleted"))
            {
                ClearCustomerFild();
                loadgdvCustomer();
                lblMesseageCustomer.Text = "Customer Deleted Successfully";

            }
            else
            {
                lblMesseageCustomer.Text = "Customer Deleted Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageCustomer.Text = "Something went wrong !";
        }
    }
    protected void gdvCustomerProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         try
        {
            modalCustomer.Show();
            gdvCustomerProfile.PageIndex = e.NewPageIndex;
            loadgdvCustomer();

        }
        catch (Exception)
        {
            lblMesseageCustomer.Text = "Something went wrong !";
        }
    }
    protected void btnSelectCustomer_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        ddlCustomer.Items.Clear();
        LoadCustomerList();
        string CustomerId = gdvCustomerProfile.DataKeys[gvr.RowIndex].Value.ToString();
        ddlCustomer.SelectedValue = CustomerId;
    }
    #endregion



    protected void ddlReceivedBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(!ddlReceivedBy.SelectedValue.Equals("0"))
        {
            txtTransactionNote.Visible = true;
            lbltnote.Visible = true;
        }
        else
        {
            txtTransactionNote.Visible = false;
            lbltnote.Visible = false;
        }
    }
}