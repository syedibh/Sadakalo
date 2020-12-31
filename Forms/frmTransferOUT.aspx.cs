using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmTransferOUT : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                LoadItemId();
                LoadItemList();
                LoadBranch();
                //loadgdvCustomer();
                //loadgdvItemMaster();
                ShowLastBillNo();
                onLoadFilled();
                loadgdvSalesDetails(txtChallanNo.Text);
                LoadFooterInfo(txtChallanNo.Text);

            }

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

    # region DropDownList
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
    private void LoadBranch()
    {
        try
        {

            string strSql = "SELECT BrId,BrName FROM  BranchName  WHERE BRID NOT IN ('" + branchId + "')  ORDER BY BrName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlShowroom.DataSource = oDs;
            ddlShowroom.DataValueField = "BrId";
            ddlShowroom.DataTextField = "BrName";
            ddlShowroom.DataBind();

            ddlShowroom.Items.Insert(0, new ListItem(" -- Select Showroom --"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    #endregion

    private void onLoadFilled()
    {
        txtQuantity.Text = "1";
        txtChallanNo.Enabled = false;
        txtAmount.Enabled = false;
        txtPurchasePrice.Enabled = false;
        txtTotalItem.Enabled = false;
        txtTotalQuantity.Enabled = false;
        txtTotalPrice.Enabled = false;
    }
    private void ClearHeaderFilled()
    {
        ddlItem.SelectedIndex = 0;
        ddlItemId.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtPurchasePrice.Text = "0.00";
        txtAmount.Text = "0.00";
        txtCurrentBalance.Text = "";
    }
    private void ClearAllFilled()
    {
        txtCurrentBalance.Text = "";
        txtChallanNo.Text = "";
        ddlShowroom.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtAmount.Text = "0.00";
        txtPurchasePrice.Text = "0.00";
        txtTotalItem.Text = "0";
        txtTotalQuantity.Text = "0";
        txtTotalPrice.Text = "0.00";
        btnAddNew.Enabled = false;
        btnAdd.Enabled = true;
        btnSave.Enabled = true;
        lblMesseage.Text = "";
        txtSearchChallan.Text = "";
    }
    private void ShowLastBillNo()
    {
        int branchId = Convert.ToInt32(Session["BranchID"].ToString());

        string strSql = "";
        strSql = "SELECT MAX(VNO) AS VNO FROM TransactionList WHERE VType='SO' AND BrId='" + branchId + "'";
        string billNo = objServiceHandler.ReturnString(strSql);
        txtChallanNo.Text = billNo.ToString();
    }
    private void ShowNewBillNo()
    {
        try
        {


            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM TransactionList WHERE VType='SO' AND BrId='" + branchId + "'";
            string billNo = objServiceHandler.ReturnString(strSql);
            if (billNo.Equals(null))
            {
                billNo = "1";
            }
            txtChallanNo.Text = billNo.ToString();

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
            string strSQL = "SELECT SUM(Qnty) TOTALQNTY,SUM(AMOUNT) TOTALAMOUNT FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'";
            DataSet totalInfo = objServiceHandler.ExecuteQuery(strSQL);
            if (totalInfo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in totalInfo.Tables["Table1"].Rows)
                {
                    txtTotalPrice.Text = prows["TOTALAMOUNT"].ToString();
                    txtTotalQuantity.Text = prows["TOTALQNTY"].ToString();

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

            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,TD.ContraRate,(TD.rate * Qnty) Amount ,(TD.Amount) TotalAmount FROM TransactionDetails TD, TransactionList TL,Item I WHERE  TL.RefNo=TD.TransactionList_Id AND TD.ItemId=I.ItemId AND  TL.VType='SO' AND TL.Vno='" + billNo + "' AND TL.BRID='" + branchId + "' ";
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
        string voucherType = "SO";
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
                        DataSet result = objServiceHandler.ExecuteQuery("SELECT RefNo,ToBrId,TotalBill FROM TransactionList where BrId='" + branchId + "' and VType='" + voucherType + "' and Vno='" + billNo + "'");
                        if (result.Tables["Table1"].Rows.Count == 1)
                        {
                            foreach (DataRow prow in result.Tables["Table1"].Rows)
                            {
                                Session["RefNo"] = prow["RefNo"].ToString();
                                txtTotalPrice.Text = prow["TotalBill"].ToString();
                                ddlShowroom.Items.Clear();
                                LoadBranch();
                                string showRoomId = prow["ToBrId"].ToString();
                                if (!showRoomId.Equals("0"))
                                {
                                    ddlShowroom.SelectedValue = showRoomId;
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
            string billNo = txtChallanNo.Text;
            string voucherType = "SO";
            string result = objTransaction.AddNewSales(branchId, voucherType, billNo, userId);
            if (result.Equals("Successful"))
            {
                string referenceNo = objServiceHandler.ReturnString("SELECT IDENT_CURRENT ('TransactionList') REFERENCENO ");
                Session["REFNO"] = referenceNo.ToString();
                loadgdvSalesDetails(txtChallanNo.Text);
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

            string relationId = Session["REFNO"].ToString();
            string itemId = ddlItem.SelectedValue;
            string qntity = txtQuantity.Text;
            string purchasePrice = txtPurchasePrice.Text;

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
            if (purchasePrice.Equals("0.00"))
            {
                lblMesseage.Text = "Please Input Purchase Price ";
                return;
            }



            strSQL = "SELECT TOP 1 *  FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "' AND ITEMID='" + itemId + "' ";
            DataSet checkItemISExist = objServiceHandler.ExecuteQuery(strSQL);
            if (checkItemISExist.Tables["Table1"].Rows.Count == 1)
            {
                lblMesseage.Text = "Item Already Exist !";
                return;
            }

            string costPrice = objServiceHandler.ReturnString("SELECT [dbo].[CostPrice]('"+branchId+"' , '"+itemId+"') as Costprice");




            string balance = objServiceHandler.ReturnString("SELECT [" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + itemId + "'");
            {
                double currentBalance = Convert.ToDouble(balance);
                double currentQuantity = Convert.ToDouble(txtQuantity.Text);
                if (currentQuantity > currentBalance)
                {
                    lblMesseage.Text = "You have not enough Balance";
                    return;
                }
                else
                {
                    string result = objTransaction.AddSalesDetails(itemId, qntity, costPrice, costPrice, relationId, "0", "0");

                    if (result.Equals("Successful"))
                    {
                        txtTotalPrice.Text = "0.00";
                        lblMesseage.Text = "Item Added";
                        loadgdvSalesDetails(txtChallanNo.Text);
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

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string relationId = "";
            string billNo = txtChallanNo.Text;
            string voucherType = "SO";
            string toBrId = ddlShowroom.SelectedValue;
            string totalPrice = txtTotalPrice.Text;
            string vat = "0";
            string comission ="0";
           // string netAmount = txtNetAmount.Text;
            string paidAmount = "0";
            string posted = "True";

            if (ddlShowroom.SelectedIndex == 0)
            {
                lblMesseage.Text = "Please Select Branch";
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
                        lblMesseage.Text = "This Bill Already Posted ! ";
                        return;
                    }

                    else
                    {

                        string result = objTransaction.UpdateTransfer(branchId, voucherType, billNo, relationId, toBrId, totalPrice, vat, comission, paidAmount, userId, posted);
                        if (result.Equals("Successfull"))
                        {

                            lblMesseage.Text = "Sales Save Successfully";
                          //  customerPaymentUpdate(customerId, paidAmount, relationId);
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
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMesseage.Text = "";
            txtChallanNo.Text = "";
            string billNo = txtSearchChallan.Text;
            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Bill No For Search";
                return;
            }
            loadgdvSalesDetails(billNo);
            LoadFooterInfo(billNo);
            txtChallanNo.Text = billNo.ToString();

        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;

        }
    }
    protected void gdvItemDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvItemDetails.EditIndex = e.NewEditIndex;
            loadgdvSalesDetails(txtChallanNo.Text);
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
            loadgdvSalesDetails(txtChallanNo.Text);
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
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

            Label txtRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblRate") as Label;
            string strPurchaseRate = txtRate.Text.Trim() != null ? txtRate.Text.Trim() : "0";

            Label txtAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string strAmount = txtAmount.Text.Trim() != null ? txtAmount.Text.Trim() : "0";

            Label lblQuantityL = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantityH") as Label;
            string Quantity = lblQuantityL.Text.Trim() != null ? lblQuantityL.Text.Trim() : "0";

            Label lblAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string Amount = lblAmount.Text.Trim() != null ? lblAmount.Text.Trim() : "0";

            



            string billNo = txtChallanNo.Text;

            if (billNo.Equals(""))
            {
                lblMesseage.Text = "Please Input ChallanNo No ";
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


            double amount = double.Parse(Amount);
          //  double totalAmount = double.Parse(TotalAmount);
            double quantity = double.Parse(Quantity);
            double tQuantity = double.Parse(txtTotalQuantity.Text);
            double tPrice = double.Parse(txtTotalPrice.Text);
         //   double netAmount = double.Parse(txtNetAmount.Text);

            double amountE = double.Parse(strAmount);
           // double totalAmountE = double.Parse(strTotalAmount);
            double quantityE = double.Parse(strQuantity);
            double rateE = double.Parse(strPurchaseRate);
            amountE = quantityE * rateE;
            strAmount = amountE.ToString();

          //  strTotalAmount = totalAmountE.ToString();

            string balance = objServiceHandler.ReturnString("SELECT [" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
         //   string salesVatPer = objServiceHandler.ReturnString("SELECT SalesVatPer FROM ITEM WHERE ITEMID='" + strItemId + "'");
            double currentBalance = Convert.ToDouble(balance);
            if (quantityE > currentBalance)
            {
                lblMesseage.Text = "You have not enough balance";
                return;
            }


            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='SO' AND VNO='" + billNo + "' ");
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
                        string result = objTransaction.UpdateSalesDetailsInfo(relationId, strItemId, strQuantity, strPurchaseRate, "0");
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
    protected void gdvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow rows = gdvItemDetails.Rows[e.RowIndex];
            string strItemId = gdvItemDetails.DataKeys[e.RowIndex].Value.ToString();
            string billNo = txtChallanNo.Text;
            //Label lblQuantity = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantity") as Label;
            //string strQuantity = lblQuantity.Text.Trim() != null ? lblQuantity.Text.Trim() : "0";

            //Label lblRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblRate") as Label;
            //string strRate = lblRate.Text.Trim() != null ? lblRate.Text.Trim() : "0";

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='SO' AND VNO='" + billNo + "' ");
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
            loadgdvSalesDetails(txtChallanNo.Text);

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
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT [dbo].[CostPrice]('" + branchId + "' , '" + strItemId + "') as Costprice,[" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    txtPurchasePrice.Text = prow["Costprice"].ToString();
                    txtCurrentBalance.Text = prow["Stock"].ToString();
                    double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);
                    double amount = purchasePrice * quantity;
                    txtAmount.Text = amount.ToString();
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
    protected void ddlItemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strItemId = ddlItemId.SelectedValue;
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT PurchasePrice,[" + branchId + "] AS Stock FROM ITEM WHERE ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    ddlItem.Items.Clear();
                    LoadItemList();
                    ddlItem.SelectedValue = strItemId;
                    txtPurchasePrice.Text = prow["PurchasePrice"].ToString();
                    txtCurrentBalance.Text = prow["Stock"].ToString();
                    double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);
                    double amount = purchasePrice * quantity;
                    txtAmount.Text = amount.ToString();

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
        double price = Convert.ToDouble(txtPurchasePrice.Text);
        double quntity = Convert.ToDouble(txtQuantity.Text);
        double amount = (price * quntity);
        txtAmount.Text = amount.ToString();
    }
}