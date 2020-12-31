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
public class clsCommon
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());
    private SqlTransaction dbTransaction = null;
	public clsCommon()
	{
	}

    # region Sales
    public string AddTransactionList(string branchId, string voucherType, string voucherNo, string createdBy)
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
            oOrderRow["Vno"] = voucherNo;
            oOrderRow["userId"] = createdBy;

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

    public string AddItemTransactionList(string branchId, string voucherType, string voucherNo, string createdBy,string contraRefNo ,string supplierId,string customerId ,string toBranchId )
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,VType,Vno,userId,contraRef,supplierId,customerId,toBrId FROM TransactionList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "TransactionList";
            oOrderRow = oDS.Tables["TransactionList"].NewRow();

            oOrderRow["BrId"] = branchId;
            oOrderRow["VType"] = voucherType;
            oOrderRow["Vno"] = voucherNo;
            oOrderRow["userId"] = createdBy;
            oOrderRow["SupplierId"] = supplierId;
            oOrderRow["CustomerId"] = customerId;
            oOrderRow["ToBrId"] = toBranchId;
            oOrderRow["ContraRef"] = contraRefNo;

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
    public string UpdateTransactionList(string refNo, string customerId, string supplierId, string totalPrice, string vat, string comission, string paidAmount, string postedBy, string postedDate, string posted, string toBrId,string receivedBy,string receivedDate,string contraRef )
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
            OLEDBCmd.CommandText = "UPDATE TransactionList SET  CustomerId='" + customerId + "',TotalBill='" + totalPrice + "', Vat='" + vat + "', Commission='" + comission + "', AmountPaid="+ paidAmount +", PostedBy='" + postedBy + "',Posteddate='" + postedDate + "', Posted='" + posted + "',ToBrId='" + toBrId + "',ReceivedById='" + receivedBy + "',ReceivedDate='" + receivedDate + "', ContraRef='"+contraRef+"' WHERE  RefNo='" + refNo + "'";
            OLEDBCmd.CommandType = CommandType.Text;
            OLEDBCmd.ExecuteNonQuery();

            dbTransaction.Commit();
            return "Successfull";
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            ex.Message.ToString();
            return ex.Message.ToString(); ;
        }
        finally
        {
            con.Close();
        }
    }
    public string AddTransactionDetails(string itemId, string quantity, string rate, string contraRate, string vat, string discount, string transactionList_Id)
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
            oOrderRow["Qnty"] = quantity;
            oOrderRow["rate"] = rate;
            oOrderRow["ContraRate"] = contraRate;
            oOrderRow["Vat"] = vat;
            oOrderRow["Discount"] = discount;
            oOrderRow["TransactionList_Id"] = transactionList_Id;

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
    public string UpdateTransactionDetails(string transactionList_Id, string itemId, string quantity, string rate,string contraRate, string vat,string discount)
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
            OLEDBCmd.CommandText = "UPDATE TransactionDetails SET  Qnty='" + quantity + "',rate='" + rate + "', ContraRate='" + contraRate + "' Vat='" + vat + "',Discount='" + discount + "' WHERE TransactionList_Id='" + transactionList_Id + "' AND ItemId='" + itemId + "'";
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
    public string AddCustomerPayment(string customerId, string refNo, string paymentAmount, string branchId, string receivedBy)
    {
        DateTime paymentDate = DateTime.Now;
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT  CustomerId, Date,Amount,UserId ,TransactionList_Id, BrId From CustomerPayment";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "CustomerPayment";
            oOrderRow = oDS.Tables["CustomerPayment"].NewRow();

            oOrderRow["CustomerId"] = customerId;
            oOrderRow["TransactionList_Id"] = refNo;
            oOrderRow["Date"] = paymentDate;
            oOrderRow["Amount"] = paymentAmount;
            oOrderRow["UserId"] = receivedBy;
            oOrderRow["BrId"] = branchId;

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
}