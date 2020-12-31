using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsAccountHandler
/// </summary>
public class clsAccountHandler
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());
    private SqlTransaction dbTransaction = null;
	public clsAccountHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    # region Sales
    public string AddAccTransactionList(string branchId, string voucherType, string voucherNo, string createdBy)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,VType,Vno,EntryBy FROM AccTransactionList";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "AccTransactionList";
            oOrderRow = oDS.Tables["AccTransactionList"].NewRow();

            oOrderRow["BrId"] = branchId;
            oOrderRow["VType"] = voucherType;
            oOrderRow["Vno"] = voucherNo;
            oOrderRow["EntryBy"] = createdBy;

            oDS.Tables["AccTransactionList"].Rows.Add(oOrderRow);
            oda.Update(oDS, "AccTransactionList");
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
    public string UpdateAccTransactionList(string voucherId, string voucherDate, string voucherNote, string posted, string postedBy, string postedDate)
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
            OLEDBCmd.CommandText = "UPDATE AccTransactionList SET  VDate='" + voucherDate + "',VoucherNote='" + voucherNote + "', Posted='" + posted + "', PostedBy='" + postedBy + "' WHERE  id='" + voucherId + "'";
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
    public string AddAccTransactionDetails(string voucherId, string debitChartAccountId, string creditChartAccountId, string amount, string transactionNote, string posted)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT AccTransactionList_Id,Dr_AccChartOfAccounts_Id,Cr_AccChartOfAccounts_Id,Amount,TransactionNote,Posted FROM AccTransactionDetails";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "AccTransactionDetails";
            oOrderRow = oDS.Tables["AccTransactionDetails"].NewRow();

            oOrderRow["AccTransactionList_Id"] = voucherId;
            oOrderRow["Dr_AccChartOfAccounts_Id"] = debitChartAccountId;
            oOrderRow["Cr_AccChartOfAccounts_Id"] = creditChartAccountId;
            oOrderRow["Amount"] = amount;
            oOrderRow["TransactionNote"] = transactionNote;
            oOrderRow["Posted"] = posted;

            oDS.Tables["AccTransactionDetails"].Rows.Add(oOrderRow);
            oda.Update(oDS, "AccTransactionDetails");
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
    public string UpdateAccTransactionDetails(string voucherId, string debitChartAccountId, string creditChartAccountId, string amount, string transactionNote)
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
            OLEDBCmd.CommandText = "UPDATE AccTransactionDetails SET  Amount='" + amount + "',Dr_AccChartOfAccounts_Id='" + debitChartAccountId + "', Cr_AccChartOfAccounts_Id='" + creditChartAccountId + "' TransactionNote='" + transactionNote + "' WHERE AccTransactionList_Id='" + voucherId + "'";
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

    public string DeleteVoucherInfo(string strVoucherDetailsId)
    {
        string deleteString;

        deleteString = "DELETE FROM AccTransactionDetails WHERE AccTransactionDetails_Id='" + strVoucherDetailsId + "'";

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

    # endregion


}