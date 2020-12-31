using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmPurchase : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsItemDependency objItemDependency = new clsItemDependency();
    clsTransaction objTransaction = new clsTransaction();
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
                LoadSupplierList();
                LoadItemList();
                loadgdvSupplier();
                btnAdd.Enabled = false;
                ShowLastChallanNo();
                LoadGdvItemDetails(txtChallanNo.Text);
                onLoadFilled();
                LoadFooterInfo(txtChallanNo.Text);

            }

           
        }
       
        catch
        {
             Response.Redirect("../Login.aspx");
        }
    }

    #region DropDownList
    private void LoadSupplierList()
    {
        try
        {

            string strSql = "SELECT SupplierId,SupplierName FROM Supplier  ORDER BY SupplierName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlSupplier.DataSource = oDs;
            ddlSupplier.DataValueField = "SupplierId";
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataBind();

            ddlSupplier.Items.Insert(0, new ListItem("-- Select Item Supplier --"));
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

            string strSql = "SELECT ITEMID,ITEMDESCRIPTION FROM ITEM   WHERE  STATUSID NOT IN ('3','2')  ORDER BY ITEMDESCRIPTION ";
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

    #endregion

    #region Item
    #endregion

    #region Supplier
    private void loadgdvSupplier()
    {
        string filterName = "";
        string seacrCategoryName = ddlSearch.SelectedValue;
        try
        {
            if (txtSearch.Text != "")
            {
                filterName = "WHERE " + seacrCategoryName + " LIKE '%" + txtSearch.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by SupplierId desc) as SL_NO,SupplierId,SupplierName,SupplierAddress,MobileNo,Email,WebAddress FROM  Supplier  " + filterName + " ORDER BY SupplierId DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvListOfSupplier.DataSource = oDs;
            gdvListOfSupplier.DataBind();
        }
        // }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblMessage.Text = "Something went wrong.";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        modalSupplier.Show();
    }
    private void ClearSupplierFild()
    {
        txtSupplierId.Text = "";
        txtSupplierName.Text = "";
        txtSupplierAddress.Text = "";
        txtWebAddress.Text = "";
        txtEmailAddress.Text = "";
        txtMobileNo.Text = "";
        btnSaveSupplier.Enabled = false;
        btnAddNewSupplier.Enabled = true;
    }
    private void ShowMaxSupplierId()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT MAX(SupplierId)+1 FROM Supplier";
            string supplierId = objServiceHandler.ReturnString(strSql);
            txtSupplierId.Text = supplierId.ToString();

        }
        catch
        {
            return;
        }

    }

    protected void btnAddNewSupplier_Click(object sender, EventArgs e)
    {

        modalSupplier.Show();
        lblMesseageSupplier.Text = "";
        ClearSupplierFild();
        ShowMaxSupplierId();
        btnAddNewSupplier.Enabled = false;
        btnSaveSupplier.Enabled = true;

    }
    protected void btnClearSupplier_Click(object sender, EventArgs e)
    {
        modalSupplier.Show();
        ClearSupplierFild();
        lblMesseageSupplier.Text = "";
    }
    protected void btnSaveSupplier_Click(object sender, EventArgs e)
    {
        try
        {
            modalSupplier.Show();
            string strSupplierId = txtSupplierId.Text;
            string strSupplierName = txtSupplierName.Text;
            string strSupplierEmail = txtEmailAddress.Text;
            string strSupplierAddress = txtSupplierAddress.Text;
            string strSupplierMobile = txtMobileNo.Text;
            string strWebAddress = txtWebAddress.Text;

            if (strSupplierId.Equals(""))
            {
                lblMesseageSupplier.Text = "Please Click Add New Button First";
                return;
            }

            if (strSupplierName.Equals(""))
            {
                lblMesseageSupplier.Text = "Please Input Supplier Name";
                return;
            }

            if (strSupplierMobile.Equals(""))
            {
                lblMesseageSupplier.Text = "Please Input Supplier Mobile No";
                return;
            }

            string result = objItemDependency.AddNewSupplier(strSupplierId, strSupplierName, strSupplierEmail, strSupplierAddress, strSupplierMobile, strWebAddress);

            if (result.Equals("Successful"))
            {
                ClearSupplierFild();
                loadgdvSupplier();
                lblMesseageSupplier.Text = "Supplier Added Successfully";


            }
            else
            {
                lblMesseageSupplier.Text = "Supplier Added Failled";
                return;
            }

        }
        catch
        {
            lblMesseageSupplier.Text = "Something went wrong !";
            return;


        }

    }
    protected void gdvListOfSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            modalSupplier.Show();
            gdvListOfSupplier.PageIndex = e.NewPageIndex;
            loadgdvSupplier();

        }
        catch (Exception)
        {
            lblMesseageSupplier.Text = "Something went wrong !";
        }
    }

    protected void gdvListOfSupplier_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            modalSupplier.Show();
            gdvListOfSupplier.EditIndex = e.NewEditIndex;
            loadgdvSupplier();
        }
        catch (Exception)
        {
            lblMesseageSupplier.Text = "Something Went Wrong";
        }

    }
    protected void gdvListOfSupplier_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            modalSupplier.Show();
            gdvListOfSupplier.EditIndex = -1;
            loadgdvSupplier();
        }
        catch (Exception)
        {
            lblMesseageSupplier.Text = "Something went wrong !";
        }
    }

    protected void gdvListOfSupplier_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "";

        try
        {
            modalSupplier.Show();
            GridViewRow rows = gdvListOfSupplier.Rows[e.RowIndex];
            string strSupplierId = gdvListOfSupplier.DataKeys[e.RowIndex].Value.ToString();

            TextBox lblSupplierName = gdvListOfSupplier.Rows[e.RowIndex].FindControl("txtSupplierNameE") as TextBox;
            string strSupplierName = lblSupplierName.Text.Trim() != null ? lblSupplierName.Text.Trim() : "0";


            TextBox lblSupplierAddress = gdvListOfSupplier.Rows[e.RowIndex].FindControl("txtAddressE") as TextBox;
            string strSupplierAddress = lblSupplierAddress.Text.Trim() != null ? lblSupplierAddress.Text.Trim() : "0";

            TextBox lblSupplierEmail = gdvListOfSupplier.Rows[e.RowIndex].FindControl("txtEmailAddressE") as TextBox;
            string strSupplierEmail = lblSupplierEmail.Text.Trim() != null ? lblSupplierEmail.Text.Trim() : "0";

            TextBox lblMobile = gdvListOfSupplier.Rows[e.RowIndex].FindControl("txtMobileNoE") as TextBox;
            string strMobile = lblMobile.Text.Trim() != null ? lblMobile.Text.Trim() : "0";

            TextBox lblWebAddress = gdvListOfSupplier.Rows[e.RowIndex].FindControl("txtWebAddressE") as TextBox;
            string strWebAddress = lblWebAddress.Text.Trim() != null ? lblWebAddress.Text.Trim() : "0";



            if (strSupplierName.Equals(""))
            {
                lblMesseageSupplier.Text = "Please Input Supplier Name";
                return;
            }

            if (strMobile.Equals(""))
            {
                lblMesseageSupplier.Text = "Please Input Supplier Mobile No";
                return;
            }


            //  DropDownList ddlEItemMasterCode = gdvBookingInfo.Rows[e.RowIndex].FindControl("ddlEItemMasterCode") as DropDownList;
            // ddlItemCode.Items.Insert(0, new ListItem(strItemCode.ToString(), strItemMasterID.ToString()));

            //  strSql = objServiceHandler.DeleteItemInfo(strItemId);

            strSql = objItemDependency.UpdateSupplierInfo(strSupplierId, strSupplierName, strSupplierAddress, strSupplierEmail, strMobile, strWebAddress);
            if (strSql.Equals("Successfull"))
            {
                ClearSupplierFild();
                gdvListOfSupplier.EditIndex = -1;
                loadgdvSupplier();
                lblMesseageSupplier.Text = "Supplier Updated Successfully";

            }
            else
            {
                gdvListOfSupplier.EditIndex = -1;
                lblMesseageSupplier.Text = "Supplier Updated Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageSupplier.Text = "Something went wrong !";
        }
    }
    protected void gdvListOfSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql = "";

        try
        {
            modalSupplier.Show();
            GridViewRow rows = gdvListOfSupplier.Rows[e.RowIndex];
            string strSupplierId = gdvListOfSupplier.DataKeys[e.RowIndex].Value.ToString();

            DataSet checkExist = objServiceHandler.ExecuteQuery("SELECT * FROM TRANSACTIONLIST WHERE SUPPLIERID='" + strSupplierId + "'");
            if (checkExist.Tables["Table1"].Rows.Count > 0)
            {
                lblMesseageSupplier.Text = "Supplier already use another tables";
                return;
            }

            strSql = objItemDependency.DeleteItemInfo(strSupplierId);

            if (strSql.Equals("Deleted"))
            {
                ClearSupplierFild();
                loadgdvSupplier();
                lblMesseageSupplier.Text = "Supplier Deleted Successfully";

            }
            else
            {
                lblMesseageSupplier.Text = "Supplier Deleted Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageSupplier.Text = "Something went wrong !";
        }
    }
    protected void btnSelcetSupplier_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        string SupplierId = gdvListOfSupplier.DataKeys[gvr.RowIndex].Value.ToString();
        ddlSupplier.SelectedValue = SupplierId;
    }


    #endregion

    #region Purchase

    private void onLoadFilled()
    {
        txtQuantity.Text = "1";
        txtChallanNo.Enabled = false;
        txtAmount.Enabled = false;
        txtSalesPrice.Enabled = false;
        txtTotalItem.Enabled = false;
        txtTotalQuantity.Enabled = false;
        txtTotalPrice.Enabled = false;
        txtNetAmount.Enabled = false;
    }
    private void ClearHeaderFilled()
    {
        txtItemBarcode.Text = "";
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtPurchasePrice.Text = "0.00";
        txtAmount.Text="0.00";
        txtSalesPrice.Text = "0.00";
    }
    private void ClearAllFilled()
    {
        txtChallanNo.Text = "";
        txtSupplierChallanNo.Text = "";
        ddlSupplier.SelectedIndex = 0;
        txtItemBarcode.Text = "";
        ddlItem.SelectedIndex = 0;
        txtQuantity.Text = "1";
        txtPurchasePrice.Text = "0.00";
        txtAmount.Text = "0.00";
        txtSalesPrice.Text = "0.00";
        txtTotalItem.Text = "0";
        txtTotalQuantity.Text = "0";
        txtTotalPrice.Text = "0.00";
        txtVat.Text = "0.00";
        txtComission.Text = "0.00";
        txtNetAmount.Text = "0.00";
        btnAddNew.Enabled = false;
        btnAdd.Enabled = true;
        btnSave.Enabled = true;
        lblMesseage.Text = "";
    }
    private void ShowLastChallanNo()
    {
        int branchId = Convert.ToInt32(Session["BranchID"].ToString());

        string strSql = "";
        strSql = "SELECT MAX(VNO) AS VNO FROM TransactionList WHERE VType='PU' AND BrId='" + branchId + "';";
        string challanNo = objServiceHandler.ReturnString(strSql);
        txtChallanNo.Text = challanNo.ToString();
    }
    private void ShowNewChallanNo()
    {
        try
        {
            

            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM TransactionList WHERE VType='PU' AND BrId='" + branchId + "';";
            string challanNo = objServiceHandler.ReturnString(strSql);
            txtChallanNo.Text = challanNo.ToString();

        }
        catch
        {
            return;
        }
    }
    private void LoadGdvItemDetails(string challanNo)
    {
        try
        {
            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,TD.ContraRate,TD.Amount FROM TransactionDetails TD, TransactionList TL,Item I WHERE  TL.RefNo=TD.TransactionList_Id AND TD.ItemId=I.ItemId AND  TL.VType='PU' AND TL.Vno='" + challanNo + "' AND TL.BRID='"+branchId+"' ";
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

    private void LoadFooterInfo(string challanNo)
    {
        lblMesseage.Text = "";
        try
        {
            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='PU' AND VNO='" + challanNo + "' ");
             if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
             {
                 foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                 {
                     string relationId = prows["REFNO"].ToString();
                     string status = prows["POSTED"].ToString();

                     if (status.Equals("True"))
                     {
                         DataSet result = objServiceHandler.ExecuteQuery("SELECT RefNo,SupplierId,TotalBill,Commission,Vat,AmountPaid FROM TransactionList where BrId='" + branchId + "' and VType='PU' and Vno='" + challanNo + "'");
                         if (result.Tables["Table1"].Rows.Count == 1)
                         {
                             foreach (DataRow prow in result.Tables["Table1"].Rows)
                             {
                                 Session["RefNo"] = prow["RefNo"].ToString();
                                 ddlSupplier.SelectedValue = prow["SupplierId"].ToString();
                                 txtTotalPrice.Text = prow["TotalBill"].ToString();
                                 txtComission.Text = prow["Commission"].ToString();
                                 txtVat.Text = prow["Vat"].ToString();
                                 txtNetAmount.Text = prow["AmountPaid"].ToString();
                             }
                             string TotalQuantity = objServiceHandler.ReturnString("SELECT SUM(Qnty) FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                             txtTotalQuantity.Text = TotalQuantity.ToString();
                         }
                     }
                     else
                     {
                         DataSet result = objServiceHandler.ExecuteQuery("SELECT SUM(QNTY) TOTALQNTY,SUM(AMOUNT) TOTALPRICE  FROM TransactionDetails WHERE TRANSACTIONLIST_ID='"+relationId+"'");
                         if (result.Tables["Table1"].Rows.Count == 1)
                         {
                             foreach (DataRow prow in result.Tables["Table1"].Rows)
                             {
                                 txtTotalQuantity.Text = prow["TOTALQNTY"].ToString();
                                 txtTotalPrice.Text = prow["TOTALPRICE"].ToString();
                                 txtNetAmount.Text = prow["TOTALPRICE"].ToString();
                             }
                         }
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
            lblMesseage.Text="Something went wron!";
            return;
        }
        
    }
    private void totalCalculation()
    {
        try
        {
            double totalQ;
            totalQ = double.Parse(txtTotalQuantity.Text);
            totalQ = totalQ + double.Parse(txtQuantity.Text);
            txtTotalQuantity.Text = totalQ.ToString();

            double findtotal;

            findtotal = (double.Parse(txtTotalPrice.Text));
            findtotal = findtotal + double.Parse(txtAmount.Text);
            txtTotalPrice.Text = findtotal.ToString();

            double netAmount;
            netAmount = double.Parse(txtTotalPrice.Text);
            txtNetAmount.Text = netAmount.ToString();

        }
        catch
        {
            lblMesseage.Text = "Calculation Error !";
            return;
        }
       
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    { 
        try
        {
            Session["REFNO"]="";
            ClearAllFilled();
            ShowNewChallanNo();
            string challanNo = txtChallanNo.Text;
            string voucherType = "PU";
            string result = objTransaction.AddNewPurchase(branchId, voucherType, challanNo, userId);
            if(result.Equals("Successful"))
            {
              //  string referenceNo = objServiceHandler.ReturnString("SELECT IDENT_CURRENT ('TransactionList') REFERENCENO ");
              //  Session["REFNO"]=referenceNo.ToString();
                LoadGdvItemDetails(txtChallanNo.Text);
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
        ClearHeaderFilled();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string challanNo = txtChallanNo.Text;
            string voucherType = "PU";
            string relationId = "";
            string barcodeNo = txtItemBarcode.Text;
            string itemId = ddlItem.SelectedValue;
            string qntity = txtQuantity.Text;
            string purchasePrice = txtPurchasePrice.Text;
            string salesPrice = txtSalesPrice.Text;

            if(ddlItem.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Item ";
                return;
            }
            if (qntity.Equals("0"))
            {
                lblMesseage.Text = "Please Select a Item ";
                return;
            }
            if (purchasePrice.Equals("0.00"))
            {
                lblMesseage.Text = "Please Input Purchase Price ";
                return;
            }
            if (salesPrice.Equals("0.00"))
            {
                lblMesseage.Text = "Please Input Sales Price ";
                return;
            }

            if (Convert.ToDouble(salesPrice) < Convert.ToDouble( purchasePrice))
            {
                lblMesseage.Text = "Sales Price Must be gretter than Purchase Price";
                return;
            }

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + voucherType + "' AND VNO='" + challanNo + "' ");
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

            DataSet checkItemISExist = objServiceHandler.ExecuteQuery("SELECT *  FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='"+relationId+"' AND ITEMID='"+itemId+"' ");
            if (checkItemISExist.Tables["Table1"].Rows.Count == 1)
            {
                lblMesseage.Text = "Item Already Exist !";
                return;
            }

            string result = objTransaction.AddPurchaseDetails(barcodeNo, itemId, purchasePrice, qntity, purchasePrice, relationId);
            
            if(result.Equals("Successful"))
            {
                lblMesseage.Text = "Item Added";
                totalCalculation();
                ClearHeaderFilled();
                LoadGdvItemDetails(txtChallanNo.Text);
                
            }
            else
            {
                lblMesseage.Text = "Item Added Failed";
                return;
            }
          


        }
        catch
        {
            lblMesseage.Text = "Somthing went wrong !";
        }
       
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            lblMesseage.Text = "";
            txtChallanNo.Text = "";
            txtVat.Text = "0.00";
            txtComission.Text = "0.00";
            string challanNo = txtSearchChallan.Text;
            if (challanNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Challan No For Search";
                return;
            }
            LoadGdvItemDetails(challanNo);
            LoadFooterInfo(challanNo);
            txtChallanNo.Text = challanNo.ToString();

        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;

        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
      
        try
        {
            string relationId="";
            string challanNo = txtChallanNo.Text;
            string voucherType = "PU";
            string supplierChallanNo = txtSupplierChallanNo.Text;
            string supplierId = ddlSupplier.SelectedValue;
            string totalPrice=txtTotalPrice.Text ;
            string vat= txtVat.Text ;
            string comission= txtComission.Text ;
            string netAmount=  txtNetAmount.Text ;
            string posted = "True";
         
            if(ddlSupplier.SelectedIndex == 0)
            {
                lblMesseage.Text = "Please Select Supplier";
                return;
            }

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='PU' AND VNO='" + challanNo + "' ");
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

                        string result = objTransaction.UpdatePurchase(branchId, voucherType, challanNo, relationId, supplierId, supplierChallanNo, totalPrice, vat, comission, netAmount, userId, posted);
                        if (result.Equals("Successfull"))
                        {

                            lblMesseage.Text = "Purchase Save Successfully";
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
                                    double currentBalance = prevBalance + currentquantity;
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
                            lblMesseage.Text = "Purchase Save Failed";
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
        {
            try
            {
                Session["RefNo"] = "";
                lblMesseage.Text = "";
                if (txtChallanNo.Text == "")
                {
                    lblMesseage.Text = "First select a bill";
                    return;
                }
                string qryString = "SELECT REFNO  FROM TRANSACTIONLIST WHERE posted=1 and  BRID='" + branchId + "' AND VTYPE='" + "PU" + "' AND VNO='" + txtChallanNo.Text + "'";
                string refNo = objServiceHandler.ReturnString(qryString);


                Session["RefNo"] = refNo.ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Forms/frmPurchaseMemo.aspx','_newtab');", true);


            }
            catch
            {
            }
        }
    }
    protected void btnUnposted_Click(object sender, EventArgs e)
    {

    }
    protected void btnDone_Click(object sender, EventArgs e)
    {

    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strItemId = ddlItem.SelectedValue;
            DataSet itemlist = objServiceHandler.ExecuteQuery("SELECT PurchasePrice,SalesPrice FROM ITEM WHERE ITEMID='" + strItemId + "'");
            if (itemlist.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in itemlist.Tables["Table1"].Rows)
                {
                    txtPurchasePrice.Text = prow["PurchasePrice"].ToString();
                    txtSalesPrice.Text = prow["SalesPrice"].ToString();
                    double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
                    double quantity = Convert.ToDouble(txtQuantity.Text);
                    double amount = purchasePrice * quantity;
                    txtAmount.Text = amount.ToString();
                }

            }
            else
            {
                lblMesseage.Text = "Purchase Price Empty";
                return;
            }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }


    }

    protected void txtPurchasePrice_TextChanged(object sender, EventArgs e)
    {
        double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
        double quntity = Convert.ToDouble(txtQuantity.Text);
        double amount = (purchasePrice * quntity);
        txtAmount.Text = amount.ToString();
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        double purchasePrice = Convert.ToDouble(txtPurchasePrice.Text);
        double quntity = Convert.ToDouble(txtQuantity.Text);
        double amount = (purchasePrice * quntity);
        txtAmount.Text = amount.ToString();
    }
    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        double vat;
        vat = double.Parse(txtVat.Text);
        vat = ((double.Parse(txtTotalPrice.Text) - double.Parse(txtComission.Text)) + vat);
        txtNetAmount.Text = vat.ToString();
    }
    protected void txtComission_TextChanged(object sender, EventArgs e)
    {
        double commision;
        commision = double.Parse(txtComission.Text);
        commision = ((double.Parse(txtTotalPrice.Text) + double.Parse(txtVat.Text)) - commision);
        txtNetAmount.Text = commision.ToString();
    }
    protected void gdvItemDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            GridViewRow rows = gdvItemDetails.Rows[e.RowIndex];
            string strItemId = gdvItemDetails.DataKeys[e.RowIndex].Value.ToString();

            TextBox txtQuantity = gdvItemDetails.Rows[e.RowIndex].FindControl("txtQuantityE") as TextBox;
            string strQuantity = txtQuantity.Text.Trim() != null ? txtQuantity.Text.Trim() : "0";


            TextBox txtRate = gdvItemDetails.Rows[e.RowIndex].FindControl("txtRateE") as TextBox;
            string strRate = txtRate.Text.Trim() != null ? txtRate.Text.Trim() : "0";

            TextBox txtContraRate = gdvItemDetails.Rows[e.RowIndex].FindControl("txtSalesRateE") as TextBox;
            string strSalesRate = txtContraRate.Text.Trim() != null ? txtContraRate.Text.Trim() : "0";

            Label txtAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string strAmount = txtAmount.Text.Trim() != null ? txtAmount.Text.Trim() : "0";

            Label lblQuantityL = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantityH") as Label;
            string Quantity = lblQuantityL.Text.Trim() != null ? lblQuantityL.Text.Trim() : "0";

            Label lblAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string Amount = lblAmount.Text.Trim() != null ? lblAmount.Text.Trim() : "0";
            

            string challanNo=txtChallanNo.Text;

            if(challanNo.Equals(""))
            {
                lblMesseage.Text="Please Input Challan No ";
                return;
            }

            if(strQuantity.Equals(""))
            {
                lblMesseage.Text="Please Input valid Qntity ";
                return;
            }

            if(strQuantity.Equals("0"))
            {
                lblMesseage.Text="Please  Input valid Qntity ";
                return;
            }

            if(strRate.Equals(""))
            {
                lblMesseage.Text="Please Input valid  Rate ";
                return;
            }

            if(strRate.Equals("0"))
            {
                lblMesseage.Text="Please  Input valid  Rate ";
                return;
            }

             if(strSalesRate.Equals(""))
            {
                lblMesseage.Text="Please  Input valid Sales Rate ";
                return;
            }

            if(strSalesRate.Equals("0"))
            {
                lblMesseage.Text="Please Input valid Sales Rate ";
                return;
            }

            double amount = double.Parse(Amount);
            double quantity = double.Parse(Quantity);
            double tQuantity = double.Parse(txtTotalQuantity.Text);
            double tPrice = double.Parse(txtTotalPrice.Text);
            double netAmount = double.Parse(txtNetAmount.Text);

            double amountE = double.Parse(strAmount);
            double quantityE = double.Parse(strQuantity);
            double rateE = double.Parse(strRate);
            double salesrateE = double.Parse(strSalesRate);
            amountE = quantityE * rateE;
            strAmount = amountE.ToString();

            if(rateE>salesrateE)
            {
                lblMesseage.Text="Can not set Sales Rate less than Purchase Price";
                return;
            }

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='PU' AND VNO='" + challanNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prow["REFNO"].ToString();
                    string status = prow["POSTED"].ToString();

                    if(status.Equals("True"))
                    {
                        lblMesseage.Text = "You are not allowed edit posted bill";
                        return;
                    }
                    else
                    {
                        string result = objTransaction.UpdateItemInfo(relationId, strItemId, strQuantity, strRate, strSalesRate);
                        if(result.Equals("Successfull"))
                        {
                            gdvItemDetails.EditIndex = -1;
                            LoadGdvItemDetails(challanNo);
                            lblMesseage.Text = "Item Update Successfully";
                            if(quantityE>quantity)
                            {
                                double quantityChange = quantityE - quantity;
                                tQuantity = tQuantity + quantityChange;
                                txtTotalQuantity.Text = tQuantity.ToString();
                            }
                            if (quantityE < quantity)
                            {
                                double quantityChange = quantity - quantityE;
                                tQuantity = tQuantity - quantityChange;
                                txtTotalQuantity.Text = tQuantity.ToString();
                            }
                            if(amountE>amount)
                            {
                               double amountChange = amountE - amount;
                               tPrice = tPrice + amountChange;
                               netAmount = netAmount + amountChange;
                                txtTotalPrice.Text = tPrice.ToString();
                                txtNetAmount.Text = netAmount.ToString();
                            }
                            if (amountE < amount)
                            {
                                double amountChange = amount - amountE;
                                tPrice = tPrice - amountChange;
                                netAmount = netAmount - amountChange;
                                txtTotalPrice.Text = tPrice.ToString();
                                txtNetAmount.Text = netAmount.ToString();
                            }


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
            LoadGdvItemDetails(txtChallanNo.Text);
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
            LoadGdvItemDetails(txtChallanNo.Text);
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
            string challanNo=txtChallanNo.Text;
            Label lblQuantity = gdvItemDetails.Rows[e.RowIndex].FindControl("lblQuantity") as Label;
            string strQuantity = lblQuantity.Text.Trim() != null ? lblQuantity.Text.Trim() : "0";


            Label lblRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblRate") as Label;
            string strRate = lblRate.Text.Trim() != null ? lblRate.Text.Trim() : "0";

            Label lblContraRate = gdvItemDetails.Rows[e.RowIndex].FindControl("lblSalesRate") as Label;
            string strSalesRate = lblContraRate.Text.Trim() != null ? lblContraRate.Text.Trim() : "0";

            Label lblAmount = gdvItemDetails.Rows[e.RowIndex].FindControl("lblAmount") as Label;
            string strAmount = lblAmount.Text.Trim() != null ? lblAmount.Text.Trim() : "0";

             DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT REFNO,POSTED  FROM TRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='PU' AND VNO='" + challanNo + "' ");
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
                             LoadGdvItemDetails(challanNo);
                             lblMesseage.Text = "Item Deleted Successfully";

                             double amountG = double.Parse(strAmount);
                             double quantityG = double.Parse(strQuantity);
                             double rateG = double.Parse(strRate);
                             amountG = quantityG * rateG;
                             strAmount = amountG.ToString();
                             double tQuantity = double.Parse(txtTotalQuantity.Text);
                             double tPrice = double.Parse(txtTotalPrice.Text);
                             double tNetAmount = double.Parse(txtNetAmount.Text);
                             tQuantity = (tQuantity - quantityG);
                             tPrice = (tPrice - amountG);
                             txtTotalQuantity.Text = tQuantity.ToString();
                             txtTotalPrice.Text = tPrice.ToString();
                             txtNetAmount.Text = tPrice.ToString();

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
            LoadGdvItemDetails(txtChallanNo.Text);

        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }
    #endregion


    protected void btnVatComission_Click(object sender, EventArgs e)
    {

    }
   
}