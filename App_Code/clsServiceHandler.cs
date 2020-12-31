using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
public class clsServiceHandler
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());
    private SqlTransaction dbTransaction = null;
        public string ReturnString(string strSql)
        {
            string strReturn = "";
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strReturn = dr[0].ToString();
                        break;
                    }
                }
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

        public string AddPermission(string empid, string buttonname)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {

                strSql = "SELECT TOP 1 empId,ButtonName FROM PERMISSION";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "PERMISSION";
                oOrderRow = oDS.Tables["PERMISSION"].NewRow();

                oOrderRow["empId"] = empid;
                oOrderRow["buttonName"] = buttonname;
                oDS.Tables["PERMISSION"].Rows.Add(oOrderRow);
                oda.Update(oDS, "PERMISSION");
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



        public DataSet ExecuteQuery(string strSQL)
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                oda.Fill(ds, "Table1");
                return ds;

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }

   

        
        public string AddNewItem(string itemId, string itemName, string purchasePrice, string salesPrice, string supplier, string unit, string category, string status, string strSalesVatPer)
        {
            DataRow oOrderRow;
            string strSql = "";
            DataSet oDS = new DataSet();
            con.Open();
            try
            {

                strSql = "SELECT ITEMID,ItemDescription,SupplierId,UnitId,CategoryId,StatusId,PurchasePrice,SalesPrice,SalesVatPer FROM ITEM";
                SqlCommand cmd = new SqlCommand(strSql, con);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
                oda.FillSchema(oDS, SchemaType.Source);

                DataTable pTable = oDS.Tables["Table"];
                pTable.TableName = "ITEM";
                oOrderRow = oDS.Tables["ITEM"].NewRow();

                oOrderRow["ITEMID"] = itemId;
                oOrderRow["ItemDescription"] = itemName;
                oOrderRow["SupplierId"] = supplier;
                oOrderRow["UnitId"] = unit;
                oOrderRow["CategoryId"] = category;
                oOrderRow["StatusId"] = status;
                oOrderRow["PurchasePrice"] = purchasePrice;
                oOrderRow["SalesPrice"] = salesPrice;
                oOrderRow["SalesVatPer"] = strSalesVatPer;

                oDS.Tables["ITEM"].Rows.Add(oOrderRow);
                oda.Update(oDS, "ITEM");
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

        public string UpdateItemInfo(string strItemId, string strItemName, string strSupplierId, string strCategoryId, string strUnitId, string strPurchasePrice, string strSalesPrice, string strItemStatus, string strSalesVatPer)
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
                OLEDBCmd.CommandText = "UPDATE Item SET ItemDescription='" + strItemName + "', SupplierId='" + strSupplierId + "',CategoryId='" + strCategoryId + "', UnitId='" + strUnitId + "', PurchasePrice='" + strPurchasePrice + "', SalesPrice='" + strSalesPrice + "', StatusId='" + strItemStatus + "',SalesVatPer='"+strSalesVatPer+"' WHERE ItemId='" + strItemId + "'";
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
        public string DeleteItemInfo(string strItemId)
        {
            string deleteString;

            deleteString = "DELETE FROM ITEM WHERE ITEMID='" + strItemId + "'";

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

        
       
}
