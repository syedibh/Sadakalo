using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AccJurnalVoucher : System.Web.UI.Page
{
    clsAccountHandler objAccounts = new clsAccountHandler();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    public static string branchId;
    public static string userId;
    public static string vType = "4";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchId = Session["BranchID"].ToString();
            userId = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                LoadDebitAccount();
                LoadCreditAccount();
                ShowLastVoucherNo();
                loadgdvVoucherDetails(txtVoucherNo.Text);
                totalCalculation(txtVoucherNo.Text);

            }

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

    #region DropDownList
    private void LoadCreditAccount()
    {
        try
        {

            string strSql = "SELECT CAM.ID,CAM.AccountsName FROM AccChartOfAccountsMaster CAM,AccChartOfAccountsBranch CAB WHERE CAM.ID=CAB.AccChartOfAccounts_id AND CAB.BranchName_BrId='" + branchId + "' AND CAM.CashOrBank NOT IN('B','C')    ORDER BY AccountsName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlCreditAccount.DataSource = oDs;
            ddlCreditAccount.DataValueField = "ID";
            ddlCreditAccount.DataTextField = "AccountsName";
            ddlCreditAccount.DataBind();

            ddlCreditAccount.Items.Insert(0, new ListItem("--Select Credit Account--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadDebitAccount()
    {
        try
        {

            string strSql = "SELECT CAM.ID,CAM.AccountsName FROM AccChartOfAccountsMaster CAM,AccChartOfAccountsBranch CAB WHERE CAM.ID=CAB.AccChartOfAccounts_id AND CAB.BranchName_BrId='" + branchId + "' AND CAM.CashOrBank NOT IN('B','C') ORDER BY AccountsName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            ddlDebitAccount.DataSource = oDs;
            ddlDebitAccount.DataValueField = "ID";
            ddlDebitAccount.DataTextField = "AccountsName";
            ddlDebitAccount.DataBind();

            ddlDebitAccount.Items.Insert(0, new ListItem("--Select Debit Account--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }


    # endregion

    #region Private Method
    private void LoadVoucherNo()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0)+1 AS VNO FROM AccTransactionList WHERE VType='" + vType + "' AND BrId='" + branchId + "'";
            string voucherNo = objServiceHandler.ReturnString(strSql);
            txtVoucherNo.Text = voucherNo;
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    private void ShowLastVoucherNo()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(VNO),0) AS VNO FROM AccTransactionList WHERE VType='" + vType + "' AND BrId='" + branchId + "';";
            string voucherNo = objServiceHandler.ReturnString(strSql);
            txtVoucherNo.Text = voucherNo.ToString();
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }

    }
    private void filledClear()
    {
        ddlDebitAccount.SelectedIndex = 0;
        ddlCreditAccount.SelectedIndex = 0;
        txtRecordNots.Text = "";
        txtAmount.Text = "0.00";
    }
    private void filledClearAll()
    {
        ddlDebitAccount.SelectedIndex = 0;
        ddlCreditAccount.SelectedIndex = 0;
        txtVoucherDate.Text = "";
        txtSearchVoucher.Text = "";
        txtNetAmount.Text = "0.00";
        txtRecordNots.Text = "";
        txtAmount.Text = "0.00";
        loadgdvVoucherDetails(txtVoucherNo.Text);
    }
    private void totalCalculation(string voucherNo)
    {
        try
        {
            string voucherId = objServiceHandler.ReturnString("SELECT ID FROM ACCTRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + vType + "' AND  VNO='" + voucherNo + "'");
            string totalAmount = objServiceHandler.ReturnString("SELECT ISNULL(SUM(Amount),0) AS TOTALAMOUNT FROM ACCTRANSACTIONDETAILS WHERE AccTransactionList_Id='" + voucherId + "'");

            txtNetAmount.Text = totalAmount;

            string headerInfo = "SELECT VDATE,(SELECT DISTINCT(Cr_AccChartOfAccounts_Id) FROM AccTransactionDetails TD WHERE AccTransactionList_Id='" + voucherId + "') Cr_AccChartOfAccounts_Id  FROM AccTransactionList TL WHERE ID='" + voucherId + "'";
            DataSet headerInfoUnsave = objServiceHandler.ExecuteQuery(headerInfo);
            if (headerInfoUnsave.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in headerInfoUnsave.Tables["Table1"].Rows)
                {
                    string voucherDate = prow["VDATE"].ToString();
                    if (!voucherDate.Equals(""))
                    {
                        DateTime VoucherDate = Convert.ToDateTime(prow["VDATE"].ToString());
                        txtVoucherDate.Text = VoucherDate.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        txtVoucherDate.Text = voucherDate;
                    }


                   // ddlCreditAccount.SelectedValue = prow["Cr_AccChartOfAccounts_Id"].ToString();

                }
            }

        }
        catch
        {
            lblMesseage.Text = "No Data Found !";
            return;
        }

    }
    private void loadgdvVoucherDetails(string voucherNo)
    {
        try
        {
            string voucherId = objServiceHandler.ReturnString("SELECT ID FROM ACCTRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + vType + "' AND  VNO='" + voucherNo + "'");
            string strSql = " SELECT ROW_NUMBER() over (order by TD.AccTransactionDetails_Id asc) as SL_NO,TD.AccTransactionDetails_Id, TD.Dr_AccChartOfAccounts_Id,(SELECT AccountsName FROM AccChartOfAccountsMaster WHERE ID=TD.Dr_AccChartOfAccounts_Id) DEBIT_ACCOUNT_NAME, TD.Cr_AccChartOfAccounts_Id,(SELECT AccountsName FROM AccChartOfAccountsMaster WHERE ID=TD.Cr_AccChartOfAccounts_Id) CREDIT_ACCOUNT_NAME,TD.Amount,TD.TransactionNote FROM AccTransactionDetails TD WHERE   TD.AccTransactionList_Id='" + voucherId + "'";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvVoucherDetails.DataSource = oDs;
            gdvVoucherDetails.DataBind();
        }

        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }

    #endregion
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string voucherId = "";
            string voucherNo = txtVoucherNo.Text;
            string debitAccount = ddlDebitAccount.SelectedValue;
            string creditAccount = ddlCreditAccount.SelectedValue;
            string amount = txtAmount.Text;
            string transactionNote = txtRecordNots.Text;
            string posted = "True";

            if (voucherNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Voucher No ";
                return;
            }
            if (amount.Equals(""))
            {
                lblMesseage.Text = "Please Input Amount ";
                return;
            }
            if (ddlDebitAccount.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select Debit Account ";
                return;
            }
            if (ddlCreditAccount.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select Credit Account ";
                return;
            }

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT ID,POSTED  FROM ACCTRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + vType + "' AND VNO='" + voucherNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    voucherId = prows["ID"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You Could not Edit Posted Bill ! ";
                        return;
                    }
                }
            }

            //string strSQL = "SELECT *  FROM AccTransactionDetails WHERE Dr_AccChartOfAccounts_Id='" + debitAccount + "' AND AccTransactionList_Id='" + voucherId + "' ";
            //DataSet checkItemISExist = objServiceHandler.ExecuteQuery(strSQL);
            //if (checkItemISExist.Tables["Table1"].Rows.Count == 1)
            //{
            //    lblMesseage.Text = "Debit Account Already Exist!";
            //    return;
            //}

            string result = objAccounts.AddAccTransactionDetails(voucherId, debitAccount, creditAccount, amount, transactionNote, posted);
            if (result.Equals("Successful"))
            {
                loadgdvVoucherDetails(voucherNo);
                totalCalculation(voucherNo);
                lblMesseage.Text = "Added Successfully";
                filledClear();
            }
            else
            {
                lblMesseage.Text = "Added Failed";
                return;
            }

        }
        catch
        {
            lblMesseage.Text = "Something went wrong!";
            return;
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        try
        {

            LoadVoucherNo();
            filledClearAll();
            string voucherNo = txtVoucherNo.Text;
            string result = objAccounts.AddAccTransactionList(branchId, vType, voucherNo, userId);
            if (!result.Equals("Successful"))
            {
                lblMesseage.Text = "Something went wrong!";
                return;
            }
        }
        catch
        {
            lblMesseage.Text = "Something went wrong!";
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtVoucherDate.Text.Equals(""))
            {
                lblMesseage.Text = "Please Select Voucher Date !";
                return;
            }
            string voucherId = "";
            string voucherNo = txtVoucherNo.Text;
            string debitAccount = ddlDebitAccount.SelectedValue;
            string creditAccount = ddlCreditAccount.SelectedValue;
            string amount = txtAmount.Text;
            string transactionNote = txtRecordNots.Text;
            string postedDate = DateTime.Now.ToString("dd/MM/yyyy");
            string vdate = txtVoucherDate.Text;
            DateTime dateTime = new DateTime();
            dateTime = Convert.ToDateTime(DateTime.ParseExact(vdate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            string voucherDate = dateTime.ToString("yyyy-MM-dd");
            string posted = "True";

            if (voucherNo.Equals(""))
            {
                lblMesseage.Text = "Please Input Voucher No ";
                return;
            }

            //if (ddlCreditAccount.SelectedIndex.Equals(0))
            //{
            //    lblMesseage.Text = "Please Select Credit Account ";
            //    return;
            //}

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT ID,POSTED  FROM ACCTRANSACTIONLIST WHERE BRID='" + branchId + "' AND VTYPE='" + vType + "' AND VNO='" + voucherNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in checkPostedStatus.Tables["Table1"].Rows)
                {
                    voucherId = prows["ID"].ToString();
                    string status = prows["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You Could not Edit Posted Bill ! ";
                        return;
                    }

                    else
                    {



                        string result = objAccounts.UpdateAccTransactionList(voucherId, voucherDate, "", posted, userId, postedDate);
                        if (result.Equals("Successfull"))
                        {
                            lblMesseage.Text = "Voucher Save Successfully";
                            filledClear();
                        }
                        else
                        {
                            lblMesseage.Text = "Voucher Save Failed";
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
        lblMesseage.Text = "";
        string voucherNo = txtSearchVoucher.Text;
        if (voucherNo.Equals(""))
        {
            lblMesseage.Text = "Please Input Voucher No First !";
            return;
        }
        txtVoucherNo.Text = voucherNo;
        loadgdvVoucherDetails(voucherNo);
        totalCalculation(voucherNo);
    }
    protected void gdvVoucherDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow rows = gdvVoucherDetails.Rows[e.RowIndex];
            string strAccTransactionDetailsId = gdvVoucherDetails.DataKeys[e.RowIndex].Value.ToString();
            string voucherNo = txtVoucherNo.Text;

            DataSet checkPostedStatus = objServiceHandler.ExecuteQuery("SELECT ID,POSTED  FROM AccTransactionList WHERE BRID='" + branchId + "' AND VTYPE='" + vType + "' AND VNO='" + voucherNo + "' ");
            if (checkPostedStatus.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prow in checkPostedStatus.Tables["Table1"].Rows)
                {
                    string relationId = prow["ID"].ToString();
                    string status = prow["POSTED"].ToString();

                    if (status.Equals("True"))
                    {
                        lblMesseage.Text = "You are not allowed Delete posted bill";
                        return;
                    }
                    else
                    {
                        string result = objAccounts.DeleteVoucherInfo(strAccTransactionDetailsId);
                        if (result.Equals("Deleted"))
                        {
                            loadgdvVoucherDetails(voucherNo);
                            totalCalculation(voucherNo);
                            lblMesseage.Text = "Item Deleted Successfully";

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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
          try
        {
            Session["RefNo"] = "";
            lblMesseage.Text = "";
            if (txtVoucherNo.Text == "")
            {
                lblMesseage.Text = "First select a bill";
                return;
            }
            string qryString = "SELECT id  FROM accTRANSACTIONLIST WHERE posted=1 and  BRID='" + branchId + "' AND VTYPE=4  AND VNO='" + txtVoucherNo.Text + "'";
            string refNo = objServiceHandler.ReturnString(qryString);


            Session["RefNo"] = refNo.ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/Forms/AccDebitVoucherView.aspx','_newtab');", true);


        }
        catch
        {
        }
    }
    }
