using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsTransaction
/// </summary>
public class clsTransaction
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());
    private SqlTransaction dbTransaction = null;
	public clsTransaction()
	{
	}

    # region Common
    public string DeleteItemInfo(string relationId, string strItemId)
    {
        string deleteString;

        deleteString = "DELETE FROM TRANSACTIONDETAILS WHERE TRANSACTIONLIST_ID='" + relationId + "' AND ITEMID='" + strItemId + "'";

        string strReturn = "";
        try
        {
            con.Open();
            dbTransaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(deleteString, con, dbTransaction);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            con.Close();
            strReturn = "Deleted";
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
        return strReturn;
    }

    public string UpdateBalanace(string itemId, string quantity, string branchid)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE ITEM SET  [" + branchid + "]='" + quantity + "' WHERE ItemId='" + itemId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }

    # endregion

    # region Purchase
    public string AddNewPurchase(string branchId, string voucherType, string challanNo, string userId)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,VType,Vno,userId FROM TransactionList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionList";
            oOrderRow = oDS.Tables["TransactionList"].NewRow();

            oOrderRow["BrId"] = branchId;
            oOrderRow["VType"] = voucherType;
            oOrderRow["Vno"] = challanNo;
            oOrderRow["userId"] = userId;

            oDS.Tables["TransactionList"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionList");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    public string AddPurchaseDetails(string barcodeNo, string itemId, string purchasePrice, string qntity, string salesPrice, string relationId)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT ItemId,Qnty,rate,ContraRate,TransactionList_Id FROM TransactionDetails";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionDetails";
            oOrderRow = oDS.Tables["TransactionDetails"].NewRow();

            oOrderRow["ItemId"] = itemId;
            oOrderRow["Qnty"] = qntity;
            oOrderRow["rate"] = purchasePrice;
            oOrderRow["ContraRate"] = salesPrice;
            oOrderRow["TransactionList_Id"] = relationId;

            oDS.Tables["TransactionDetails"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionDetails");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }
    public string UpdatePurchase(string branchId, string voucherType, string challanNo, string relationId, string supplierId, string supplierChallanNo, string totalPrice, string vat, string comission, string netAmount, string userId, string posted)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionList SET  SupplierId='" + supplierId + "',TotalBill='" + totalPrice + "', Vat='" + vat + "', Commission='" + comission + "', AmountPaid='" + netAmount + "', PostedBy='" + userId + "',Posted='" + posted + "' WHERE BrId='" + branchId + "' AND VType='" + voucherType + "' AND Vno='" + challanNo + "' AND RefNo='" + relationId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    public string UpdateItemInfo(string relationId, string strItemId, string strQuantity, string strRate, string strSalesRate)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionDetails SET  Qnty='" + strQuantity + "',rate='" + strRate + "', ContraRate='" + strSalesRate + "' WHERE TransactionList_Id='" + relationId + "' AND ItemId='" + strItemId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    # endregion

    # region Sales
    public string AddNewSales(string branchId, string voucherType, string billNo, string userId)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,VType,Vno,userId FROM TransactionList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionList";
            oOrderRow = oDS.Tables["TransactionList"].NewRow();

            oOrderRow["BrId"] = branchId;
            oOrderRow["VType"] = voucherType;
            oOrderRow["Vno"] = billNo;
            oOrderRow["userId"] = userId;

            oDS.Tables["TransactionList"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionList");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    public string AddSalesDetails(string itemId, string qntity, string salesPrice,string contraRate, string relationId,string SalesVatPer,string discountAmount)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT ItemId,Qnty,rate,ContraRate,TransactionList_Id,Vat,Discount FROM TransactionDetails";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionDetails";
            oOrderRow = oDS.Tables["TransactionDetails"].NewRow();

            oOrderRow["ItemId"] = itemId;
            oOrderRow["Qnty"] = qntity;
            oOrderRow["rate"] = salesPrice;
            oOrderRow["ContraRate"] = contraRate;
            oOrderRow["TransactionList_Id"] = relationId;
            oOrderRow["Vat"] = SalesVatPer;
            oOrderRow["Discount"] = discountAmount;

            oDS.Tables["TransactionDetails"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionDetails");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    public string UpdateSales(string branchId, string voucherType, string billNo, string relationId, string customerId, string totalPrice, string vat, string comission, string paidAmount, string userId, string posted)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionList SET  CustomerId='" + customerId + "',TotalBill='" + totalPrice + "', Vat='" + vat + "', Commission='" + comission + "', AmountPaid='" + paidAmount + "', PostedBy='" + userId + "',Posted='" + posted + "' WHERE BrId='" + branchId + "' AND VType='" + voucherType + "' AND Vno='" + billNo + "' AND RefNo='" + relationId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }

    public string UpdateSalesDetailsInfo(string relationId, string strItemId, string strQuantity, string strSalesRate, string strSalesVat)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionDetails SET  Qnty='" + strQuantity + "',rate='" + strSalesRate + "', Vat='" + strSalesVat + "' WHERE TransactionList_Id='" + relationId + "' AND ItemId='" + strItemId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    # endregion



    public string InsertCustomerPayment(string customerId, string relationId, string paymentAmount, string branchId, string userId,string receivedBy,string transactionNote)
    {
        DateTime paymentDate = DateTime.Now;
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT  CustomerId, Date,Amount,UserId ,TransactionList_Id, BrId,CashReceivedType,Remarks From CustomerPayment";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "CustomerPayment";
            oOrderRow = oDS.Tables["CustomerPayment"].NewRow();

            oOrderRow["CustomerId"] = customerId;
            oOrderRow["TransactionList_Id"] = relationId;
            oOrderRow["Date"] = paymentDate;
            oOrderRow["Amount"] = paymentAmount;
            oOrderRow["UserId"] = userId;
            oOrderRow["BrId"] = branchId;
            oOrderRow["CashReceivedType"] = receivedBy;
            oOrderRow["Remarks"] = transactionNote;

            oDS.Tables["CustomerPayment"].Rows.Add(oOrderRow);
            oda.Update(oDS, "CustomerPayment");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
        
         
    }

    public string UpdateDiscount(string itemId, double discountPerItem, string relationId)
    {
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;

        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionDetails SET  Discount='" + discountPerItem + "' WHERE TransactionList_Id='" + relationId + "' AND ItemId='" + itemId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    public string DeleteBrPermission(string brid)
    {
        string deleteString;

        deleteString = "DELETE FROM BRPERMISSION WHERE id='" + brid + "'";

        string strReturn = "";
        try
        {
            con.Open();
            dbTransaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(deleteString, con, dbTransaction);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            dbTransaction.Commit();
            con.Close();
            strReturn = "Deleted";
        }
        catch (Exception ex)
        {
            strReturn = ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
        return strReturn;
    }

    #region TransaferOut
    public string UpdateTransfer(string branchId, string voucherType, string billNo, string relationId, string ToBrId, string totalPrice, string vat, string comission, string paidAmount, string userId, string posted)
    {
        DateTime postedDate = DateTime.Now;
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;


        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionList SET  ToBrId='" + ToBrId + "',TotalBill='" + totalPrice + "', Vat='" + vat + "', Commission='" + comission + "', AmountPaid='" + paidAmount + "', PostedBy='" + userId + "',Posted='" + posted + "', Posteddate='"+postedDate+"' WHERE BrId='" + branchId + "' AND VType='" + voucherType + "' AND Vno='" + billNo + "' AND RefNo='" + relationId + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
    #endregion

    # region TransferIN
    #endregion
    public string InsertNewTransferIN(string ToBrId, string voucherType, string VNo, string strTotalValue, string userId, string posted)
    {
        DateTime postedDate = DateTime.Now;
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,VType,Vno,TotalBill,userId,PostedBy,Posteddate FROM TransactionList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionList";
            oOrderRow = oDS.Tables["TransactionList"].NewRow();

            oOrderRow["BrId"] = ToBrId;
            oOrderRow["VType"] = voucherType;
            oOrderRow["Vno"] = VNo;
            oOrderRow["userId"] = userId;
            oOrderRow["PostedBy"] = userId;
            oOrderRow["Posteddate"] = postedDate;
            oOrderRow["TotalBill"] = strTotalValue;

            oDS.Tables["TransactionList"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionList");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    public string InsertNewTransferDetails(string itemId, string quantity, string rate, string contraRate, string amount, string referenceNo)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT ItemId,Qnty,rate,ContraRate,TransactionList_Id FROM TransactionDetails";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionDetails";
            oOrderRow = oDS.Tables["TransactionDetails"].NewRow();

            oOrderRow["ItemId"] = itemId;
            oOrderRow["Qnty"] = quantity;
            oOrderRow["rate"] = rate;
            oOrderRow["ContraRate"] = contraRate;
            oOrderRow["TransactionList_Id"] = referenceNo;

            oDS.Tables["TransactionDetails"].Rows.Add(oOrderRow);
            oda.Update(oDS, "TransactionDetails");
            return "Successful";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
        finally
        {
            con.Close();
        }
    }

    public string UpdateTransferOut(string refNo, string referenceNo,string userId)
    {
        DateTime receivedDate = DateTime.Now;
        SqlCommand OLEDBCmd;
        SqlTransaction dbTransaction;


        con.Open();
        dbTransaction = con.BeginTransaction();
        try
        {
            OLEDBCmd = new SqlCommand();
            OLEDBCmd.Connection = con;
            OLEDBCmd.Transaction = dbTransaction;
            OLEDBCmd.CommandText = "UPDATE TransactionList SET  ReceivedById='" + userId + "',ReceivedDate='" + receivedDate + "', ContraRef='" + referenceNo + "' WHERE RefNo='" + refNo + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return "";
        }
        finally
        {
            con.Close();
        }
    }
}