using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsItemDependency
/// </summary>
public class clsItemDependency
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());
    private SqlTransaction dbTransaction = null;


    # region Supplier
    public string AddNewSupplier(string strSupplierId, string strSupplierName, string strSupplierEmail, string strSupplierAddress, string strSupplierMobile, string strWebAddress)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {
            strSql = "SELECT SupplierId,SupplierName,SupplierAddress,WebAddress,Email,MobileNo FROM Supplier";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "Supplier";
            oOrderRow = oDS.Tables["Supplier"].NewRow();

            oOrderRow["SupplierId"] = strSupplierId;
            oOrderRow["SupplierName"] = strSupplierName;
            oOrderRow["SupplierAddress"] = strSupplierAddress;
            oOrderRow["WebAddress"] = strWebAddress;
            oOrderRow["Email"] = strSupplierEmail;
            oOrderRow["MobileNo"] = strSupplierMobile;

            oDS.Tables["Supplier"].Rows.Add(oOrderRow);
            oda.Update(oDS, "Supplier");
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

    public string DeletePermission(string useridfordelete)
    {
        string deleteString;

        deleteString = "DELETE FROM permission WHERE empid='" + useridfordelete + "'";

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
    public string DeleteItemInfo(string strSupplierId)
    {
        string deleteString;

        deleteString = "DELETE FROM Supplier WHERE SupplierId='" + strSupplierId + "'";

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


    public string UpdatePassword(string password, string userid)
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
            OLEDBCmd.CommandText = "UPDATE UserList SET password='" + password + "'  WHERE userid='" + userid + "'";
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
    

    public string UpdateSupplierInfo(string strSupplierId, string strSupplierName, string strSupplierAddress, string strSupplierEmail, string strMobile, string strWebAddress)
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
            OLEDBCmd.CommandText = "UPDATE Supplier SET SupplierName='" + strSupplierName + "', SupplierAddress='" + strSupplierAddress + "',WebAddress='" + strWebAddress + "', Email='" + strSupplierEmail + "', MobileNo='" + strMobile + "' WHERE SupplierId='" + strSupplierId + "'";
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

    #region Category
    public string AddNewCategory(string strCategoryId, string strCategoryName)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {
            strSql = "SELECT CategoryId,CategoryName FROM Category";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "Category";
            oOrderRow = oDS.Tables["Category"].NewRow();

            oOrderRow["CategoryId"] = strCategoryId;
            oOrderRow["CategoryName"] = strCategoryName;

            oDS.Tables["Category"].Rows.Add(oOrderRow);
            oda.Update(oDS, "Category");
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
    public string UpdateCategoryInfo(string strCategoryId, string strCategoryName)
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
            OLEDBCmd.CommandText = "UPDATE Category SET CategoryName='" + strCategoryName + "'  WHERE CategoryId='" + strCategoryId + "'";
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

    public string DeleteCategoryInfo(string strCategoryId)
    {
        string deleteString;

        deleteString = "DELETE FROM Category WHERE CategoryId='" + strCategoryId + "'";

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

    #endregion

    #region Unit
    //public DataSet AddNewUnit(string strUnitId, string strUnitName)
    //{
    //    con.Open();
    //    SqlCommand cmd=new SqlCommand("INSERT INTO UNIT(UNITID,UNITNAME) VALUES('"+strUnitId+"','"+strUnitName+"')",con);
    //    cmd.ExecuteNonQuery();

    //    SqlCommand cmd1 = new SqlCommand("SELECT @@IDENTITY AS auto_id", con);
    //    SqlDataAdapter oda = new SqlDataAdapter(cmd1);
    //    DataSet ds = new DataSet();
    //    oda.Fill(ds, "Table1");
    //    return ds;
       
    //    con.Close();
       
    //}
    public string AddNewUnit(string strUnitId, string strUnitName)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {
            strSql = "SELECT UnitId,UnitName FROM Unit";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "Unit";
            oOrderRow = oDS.Tables["Unit"].NewRow();

            oOrderRow["UnitId"] = strUnitId;
            oOrderRow["UnitName"] = strUnitName;

            oDS.Tables["Unit"].Rows.Add(oOrderRow);
            oda.Update(oDS, "Unit");
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
    public string UpdateUnitInfo(string strUnitId, string strUnitName)
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
            OLEDBCmd.CommandText = "UPDATE Unit SET UnitName='" + strUnitName + "'  WHERE UnitId='" + strUnitId + "'";
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
    public string DeleteUnitInfo(string strUnitId)
    {
        string deleteString;

        deleteString = "DELETE FROM Unit WHERE UnitId='" + strUnitId + "'";

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

    #endregion


    #region Customer Profile

    public string AddNewCustomer(string strCustomerId, string strCustomerName, string strCustomerEmail, string strCustomerAddress, string strCustomerMobile, string strWebAddress, string branchId, string userId)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {
            strSql = "SELECT CustomerId,CustomerName,MailingAddress,WebAddress,EmailAddress,MobileNo,BrId,CreatedBy FROM CustomerProfile";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "CustomerProfile";
            oOrderRow = oDS.Tables["CustomerProfile"].NewRow();

            oOrderRow["CustomerId"] = strCustomerId;
            oOrderRow["CustomerName"] = strCustomerName;
            oOrderRow["MailingAddress"] = strCustomerAddress;
            oOrderRow["WebAddress"] = strWebAddress;
            oOrderRow["EmailAddress"] = strCustomerEmail;
            oOrderRow["MobileNo"] = strCustomerMobile;
            oOrderRow["BrId"] = branchId;
            oOrderRow["CreatedBy"] = userId;

            oDS.Tables["CustomerProfile"].Rows.Add(oOrderRow);
            oda.Update(oDS, "CustomerProfile");
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

    public string DeleteCustomerInfo(string strCustomerId, string branchId)
    {
        string deleteString;

        deleteString = "DELETE FROM CustomerProfile WHERE CustomerId='" + strCustomerId + "' AND BrId='" + branchId + "'";

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

    public string UpdateCustomerInfo(string strSupplierId, string strCustomerName, string strCustomerAddress, string strCustomerEmail, string strMobile, string strWebAddress, string branchId)
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
            OLEDBCmd.CommandText = "UPDATE CustomerProfile SET CustomerName='" + strCustomerName + "', MailingAddress='" + strCustomerAddress + "',WebAddress='" + strWebAddress + "', EmailAddress='" + strCustomerEmail + "', MobileNo='" + strMobile + "' WHERE CustomerId='" + strSupplierId + "' and BrId='" + branchId + "'";
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



   
}