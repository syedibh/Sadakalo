using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_frmTransferIN : System.Web.UI.Page
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
                LoadgdvItemSummery();
                trfindvoucer.Visible = false;
            }

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }


    private void LoadgdvItemSummery()
    {
        string FromDate = "";
        string ToDate = "";
        string strSubquery = "";
        string strSubquery2 = "";
        string strSubquery3 = "";
        string strSubquery4 = "AND  ReceivedById IS NULL AND ContraRef IS NULL";
        string SearchValue = ddlFindOption.SelectedValue;
        string voucherNo = txtVoucherNo.Text;
        //if(!txtFromDate.Text.Equals("") && !txtToDate.Text.Equals(""))
        //{
        //DateTime strFromDate=Convert.ToDateTime(txtFromDate.Text);
        //FromDate = strFromDate.ToString("mm/dd/yyyy");
        //DateTime strToDate=Convert.ToDateTime(txtToDate.Text);
        //ToDate = strToDate.ToString("mm/dd/yyyy");
        //}
         FromDate = txtFromDate.Text;
         ToDate = txtToDate.Text;

        try
        {
            if(!branchId.Equals("1"))
            {
                strSubquery = "  AND TL.TOBRID='" + branchId + "'";
            }

            if (SearchValue.Equals("1") && !branchId.Equals("1"))
            {
                strSubquery = strSubquery + " AND  Vno = '" + voucherNo + "' " + strSubquery4 + "";
            }
            if (SearchValue.Equals("1") && branchId.Equals("1"))
            {
                strSubquery = " AND  Vno ='" + voucherNo + "' " + strSubquery4 + "";
            }
            if(SearchValue.Equals("2"))
            {
              //  AND CONVERT(DATE,P.PURCHASE_DATE) BETWEEN CONVERT(DATE,'" + dtpAllFrom.DateString + "') AND CONVERT(DATE,'" + toDate + "')
                strSubquery2 = " AND  CONVERT(DATE,POSTEDDATE) BETWEEN CONVERT(DATE,'" + FromDate + "') AND CONVERT(DATE,'" + ToDate + "') " + strSubquery4 + "";
            }

            if (SearchValue.Equals("3"))
            {
                strSubquery3 = " AND CONVERT(DATE,POSTEDDATE) BETWEEN CONVERT(DATE,'" + FromDate + "') AND CONVERT(DATE,'" + ToDate + "')";
            }
            if (SearchValue.Equals("0"))
            {
                strSubquery3 = "" + strSubquery4 +"";
            }

            string strSql = " SELECT ROW_NUMBER() over (order by TL.RefNO asc) as SL_NO, TL.RefNo,TL.BrId,B.BrName,TL.ToBrId,(SELECT BRNAME FROM BranchName WHERE BrId=TL.ToBrId) ToBrName,TL.VType,TL.Vno,TL.ContraRef,TL.TotalBill,FORMAT(TL.Posteddate,'MM/dd/yyyy') AS Posteddate  FROM TransactionList TL,BranchName B WHERE TL.BrId=B.BrId AND  VType='SO'  " + strSubquery + " " + strSubquery2 + " " + strSubquery3 + " ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvItemSummery.DataSource = oDs;
            gdvItemSummery.DataBind();
        }

        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }

    private void loadgdvSalesDetails(string refNo)
    {
        try
        {

            string strSql = " SELECT ROW_NUMBER() over (order by autoid asc) as SL_NO, TD.ItemId,I.ItemDescription,Qnty,TD.rate,(TD.rate * Qnty) Amount  FROM TransactionDetails TD, Item I WHERE TD.ItemId=I.ItemId AND TD.TransactionList_Id='" + refNo + "'  ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvItemDetails.DataSource = oDs;
            gdvItemDetails.DataBind();
        }

        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }

    protected void gdvItemSummery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvItemSummery.PageIndex = e.NewPageIndex;
            LoadgdvItemSummery();

        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void gdvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
        //    gdvItemDetails.PageIndex = e.NewPageIndex;
        //    loadgdvSalesDetails(txtChallanNo.Text);

        //}
        //catch (Exception)
        //{
        //    lblMesseage.Text = "Something went wrong !";
        //}
    }
    protected void btnDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            string refNo = gdvItemSummery.DataKeys[gvr.RowIndex].Value.ToString();
            loadgdvSalesDetails(refNo);
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
    }
    protected void gdvItemSummery_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow rows = gdvItemSummery.Rows[e.RowIndex];
            string refNo = gdvItemSummery.DataKeys[e.RowIndex][0].ToString();
            string FrombrId = gdvItemSummery.DataKeys[e.RowIndex][1].ToString();
            string ToBrId = gdvItemSummery.DataKeys[e.RowIndex][2].ToString();

            Label FromBr = gdvItemSummery.Rows[e.RowIndex].FindControl("lblFromBr") as Label;
            string strFromBr = FromBr.Text.Trim() != null ? FromBr.Text.Trim() : "0";

            Label ToBrName = gdvItemSummery.Rows[e.RowIndex].FindControl("lblToBr") as Label;
            string strToBrName = ToBrName.Text.Trim() != null ? ToBrName.Text.Trim() : "0";

            Label Vtype = gdvItemSummery.Rows[e.RowIndex].FindControl("lblVtype") as Label;
            string strVtype = Vtype.Text.Trim() != null ? Vtype.Text.Trim() : "0";

            Label Vno = gdvItemSummery.Rows[e.RowIndex].FindControl("lblVno") as Label;
            string strVno = Vno.Text.Trim() != null ? Vno.Text.Trim() : "0";

            Label TotalValue = gdvItemSummery.Rows[e.RowIndex].FindControl("lblTotalValue") as Label;
            string strTotalValue = TotalValue.Text.Trim() != null ? TotalValue.Text.Trim() : "0";


            string relationId = refNo;
            string posted = "True";

            string checkPostedStatus = objServiceHandler.ReturnString("SELECT ContraRef  FROM TRANSACTIONLIST WHERE RefNo='" + refNo + "'");
            if (!checkPostedStatus.Equals(""))
            {
                lblMesseage.Text = "This bill already import";
                return;
            }


                    if(!ToBrId.Equals(branchId))
                     {
                      lblMesseage.Text = "You are not eligible import this bill ! ";
                      return;
                     }

                    else
                    {

                         string strSql = "";
                         strSql = "SELECT MAX(VNO)+1 AS VNO FROM TransactionList WHERE VType='SI' AND BrId='" + branchId + "'";
                         string billNo = objServiceHandler.ReturnString(strSql);
                         if (billNo.Equals(null))
                         {
                             billNo = "1";
                         }

                         string result = objTransaction.InsertNewTransferIN(ToBrId, "SI", billNo, strTotalValue, userId, posted);
                         if (result.Equals("Successful"))
                        {
                            string referenceNo = objServiceHandler.ReturnString("SELECT IDENT_CURRENT ('TransactionList') REFERENCENO ");
                            // Session["REFNO"] = referenceNo.ToString();

                            DataSet getTransferOutDetails = objServiceHandler.ExecuteQuery("SELECT ItemId,Qnty,rate,contraRate,Amount  FROM TransactionDetails  WHERE TRANSACTIONLIST_ID='" + relationId + "'");
                            if (getTransferOutDetails.Tables["Table1"].Rows.Count > 0)
                            {
                                foreach (DataRow prow in getTransferOutDetails.Tables["Table1"].Rows)
                                {
                                    string itemId = prow["ItemId"].ToString();
                                    string quantity = prow["Qnty"].ToString();
                                    string rate = prow["rate"].ToString();
                                    string contraRate = prow["contraRate"].ToString();
                                    string amount = prow["Amount"].ToString();

                                    string newTransferDetailsResult = objTransaction.InsertNewTransferDetails(itemId, quantity, rate, contraRate, amount, referenceNo);
                                    if (!newTransferDetailsResult.Equals("Successful"))
                                    {
                                        lblMesseage.Text = "Item Import Failled !";
                                        return;
                                    }
                                }
                            }


                            DataSet itemList = objServiceHandler.ExecuteQuery("SELECT ItemId,Qnty  FROM TransactionDetails  WHERE TRANSACTIONLIST_ID='" + referenceNo + "'");
                            if (itemList.Tables["Table1"].Rows.Count > 0)
                            {
                                foreach (DataRow prow in itemList.Tables["Table1"].Rows)
                                {
                                    string itemId = prow["ItemId"].ToString();
                                    string quantity = prow["Qnty"].ToString();
                                    string balance = objServiceHandler.ReturnString("SELECT [" + ToBrId + "] AS Stock FROM ITEM WHERE ITEMID='" + itemId + "'");
                                    double prevBalance = Convert.ToDouble(balance);
                                    double currentquantity = Convert.ToDouble(quantity);
                                    double currentBalance = prevBalance + currentquantity;
                                    quantity = currentBalance.ToString();

                                    string BalanceUpdate = objTransaction.UpdateBalanace(itemId, quantity, ToBrId);
                                    if (!BalanceUpdate.Equals("Successfull"))
                                    {
                                        lblMesseage.Text = "Item Balance Updated Error !";
                                    }
                                }
                            }


                            string TransferOutresult = objTransaction.UpdateTransferOut(refNo, referenceNo,userId);
                            if (TransferOutresult.Equals("Successfull"))
                            {
                                lblMesseage.Text = "Item Import Successfully";
                                LoadgdvItemSummery();
                            }
                            else
                            {
                                lblMesseage.Text = "Sales Save Failed";
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
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlFindOption.SelectedValue.Equals("1"))
        {
            trfindvoucer.Visible = true;
        }
        else
        {
            trfindvoucer.Visible = false;
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFindOption.SelectedValue.Equals("0"))
            {
                lblMesseage.Text = "Please Select Find Option";
                return;
            }

            if (ddlFindOption.SelectedValue.Equals("2") || ddlFindOption.SelectedValue.Equals("3"))
            {
               if(txtFromDate.Text.Equals("") || txtToDate.Text.Equals(""))
               {
                   lblMesseage.Text = "Please Select From Date And To Date";
                   return;
               }
            }

            LoadgdvItemSummery();
        }
        catch
        {
            lblMesseage.Text = "Something went wrong !";
            return;
        }
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlFindOption.SelectedValue = "0";
        txtVoucherNo.Visible = false;
    }
}