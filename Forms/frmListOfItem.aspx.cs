using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmListOfItem : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsItemDependency objItemDependency = new clsItemDependency();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                loadgdvItemMaster();
                loadgdvSupplier();
                loadgdvCategory();
                loadgdvUnit();
                LoadSupplierList();
                LoadCategoryList();
                LoadUnitList();
                LoadStatusList();


                btnSave.Enabled = false;
                btnSaveSupplier.Enabled = false;
                btnSaveUnit.Enabled = false;
                btnSaveCategory.Enabled = false;
            }
        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
        
    }

    #region DropdownList
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

            ddlSupplier.Items.Insert(0, new ListItem("--Select Item Supplier--"));
        }
        catch (Exception)
        {
            
            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadCategoryList()
    {
        try
        {

            string strSql = "SELECT CategoryId,CategoryName FROM Category  ORDER BY CategoryName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlCategory.DataSource = oDs;
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataBind();

            ddlCategory.Items.Insert(0, new ListItem("--Select Item Category--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }
    private void LoadUnitList()
    {
        try
        {

            string strSql = "SELECT UnitId,UnitName FROM Unit  ORDER BY UnitName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlUnit.DataSource = oDs;
            ddlUnit.DataValueField = "UnitId";
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataBind();

            ddlUnit.Items.Insert(0, new ListItem("--Select Item Unit--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }

    private void LoadStatusList()
    {
        try
        {

            string strSql = "SELECT StatusId,StatusName FROM ItemStatus  ORDER BY StatusName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlItemType.DataSource = oDs;
            ddlItemType.DataValueField = "StatusId";
            ddlItemType.DataTextField = "StatusName";
            ddlItemType.DataBind();

            ddlItemType.Items.Insert(0, new ListItem("--Select Item Status--"));
        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong.";
        }
    }


    #endregion

    #region Item
    private void loadgdvItemMaster()
    {
        string filterName = "";
        int branchId = Convert.ToInt32(Session["BranchID"].ToString());
        string seacrCategoryName=ddlSearch.SelectedValue;
        try
        {
            if(!txtSearch.Text.Equals(""))
            {
                filterName = "AND "+seacrCategoryName+ " LIKE '%" + txtSearch.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by I.ITEMID desc) as SL_NO,I.ITEMID,I.ITEMDESCRIPTION,I.SUPPLIERID,S.SUPPLIERNAME,I.CATEGORYID,C.CATEGORYNAME,I.UNITID,U.UNITNAME,I.StatusId,IT.StatusName,I.SALESPRICE,I.PURCHASEPRICE,I.SalesVatPer,I.["+branchId+"] AS Stock FROM ITEM I,SUPPLIER S,CATEGORY C,UNIT U,ItemStatus IT WHERE I.SUPPLIERID=S.SUPPLIERID AND I.UNITID=U.UNITID AND I.CATEGORYID=C.CATEGORYID AND I.STATUSID=IT.STATUSID   " + filterName + " ORDER BY I.ITEMID DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvListOfItem.DataSource = oDs;
            gdvListOfItem.DataBind();
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong.";
        }
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearAllField();
        ShowMaxItemId();
        btnSave.Enabled = true;
        btnAddNew.Enabled = false;

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllField();
    }
    private void ClearAllField()
    {
        txtSalesPrice.Text = "0.00";
        txtPurchasePrice.Text = "0.00";
        txtItemId.Text = "";
        txtItemName.Text = "";
        lblMesseage.Text = "";
        ddlSupplier.SelectedIndex = 0;
        ddlUnit.SelectedIndex = 0;
        ddlItemType.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        btnAddNew.Enabled = true;
        btnSave.Enabled = false;
    }
    private void ShowMaxItemId()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(ItemId),0)+1 FROM Item";
            string itemId = objServiceHandler.ReturnString(strSql);
            txtItemId.Text = itemId.ToString();
            
        }
        catch
        {
            lblMesseage.Text = "Something went wrong!";
            return;
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {
            string filterName = "";
            int branchId = Convert.ToInt32(Session["BranchID"].ToString());
            string seacrCategoryName = ddlSearch.SelectedValue;

            if (!txtSearch.Text.Equals(""))
            {

                filterName = "AND " + seacrCategoryName + " LIKE '%" + txtSearch.Text + "%'";
            }
         
             string strSql = " SELECT ROW_NUMBER() over (order by I.ITEMID desc) as SL_NO,I.ITEMID,I.ITEMDESCRIPTION,S.SUPPLIERNAME,C.CATEGORYNAME,U.UNITNAME,IT.StatusName,I.SALESPRICE,I.PURCHASEPRICE,I.SalesVatPer,I.[" + branchId + "] AS Stock FROM ITEM I,SUPPLIER S,CATEGORY C,UNIT U,ItemStatus IT WHERE I.SUPPLIERID=S.SUPPLIERID AND I.UNITID=U.UNITID AND I.CATEGORYID=C.CATEGORYID AND I.STATUSID=IT.STATUSID   " + filterName + " ORDER BY I.ITEMID DESC";


                string strHTML = "", fileName = "";
                lblMesseage.Text = "";
                fileName = "List_of_Item";
                //------------------------------------------Report File xl processing   -------------------------------------

              DataSet  dtsListOfItem = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Chaina Bangla Health Care Ltd</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>CBCH</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=11 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h5 align=center>List Of Item </h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >Item Id</td>";
                strHTML = strHTML + "<td valign='middle' >Item Description </td>";
                strHTML = strHTML + "<td valign='middle' >Supplier Name </td>";
                strHTML = strHTML + "<td valign='middle' >Unit Name</td>";
                strHTML = strHTML + "<td valign='middle' >Category Name</td>";
                strHTML = strHTML + "<td valign='middle' >Status </td>";
                strHTML = strHTML + "<td valign='middle' >Purchase Price</td>";
                strHTML = strHTML + "<td valign='middle' >Sales Price</td>";
                strHTML = strHTML + "<td valign='middle' >Sales Vat%</td>";
                strHTML = strHTML + "<td valign='middle' >Closing Stock</td>";
                strHTML = strHTML + "</tr>";


                if (dtsListOfItem.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow prow in dtsListOfItem.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + prow["SL_NO"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["ITEMID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["ITEMDESCRIPTION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["SUPPLIERNAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["UNITNAME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > " + prow["CATEGORYNAME"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["StatusName"].ToString() + " </td>";
                        strHTML = strHTML + " <td valign='right' > " + prow["PURCHASEPRICE"].ToString() + " </td>";
                        strHTML = strHTML + " <td  valign='right' > " + prow["SALESPRICE"].ToString() + " </td>";
                        strHTML = strHTML + " <td  valign='right' > " + prow["SalesVatPer"].ToString() + " </td>";
                        strHTML = strHTML + " <td > " + prow["Stock"].ToString() + "</td>"; 
                        
                        strHTML = strHTML + " </tr> ";
                        
                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                
                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

              //  SaveAuditInfo("Preview", "DIS_wise_Cust_Reg_N_VER_Rpt");
                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMesseage.ForeColor = Color.White;
                lblMesseage.Text = "Report Generated Successfully...";

        }
        catch
        {

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            string strItemId= txtItemId.Text;
            string strItemName=txtItemName.Text;
            string strCategoryId= ddlCategory.SelectedValue;
            string strUnitId=ddlUnit.SelectedValue;
            string strSupplierId= ddlSupplier.SelectedValue;
            string strPurchasePrice = txtPurchasePrice.Text;
            string strSalesPrice= txtSalesPrice.Text;
            string strItemStatus=ddlItemType.SelectedValue;
            string strSalesVatPer = txtVatPer.Text;

            if (strItemId.Equals(""))
            {
                lblMesseage.Text = "Please Click Add New Button First";
                return;
            }
            if (strItemName.Equals(""))
            {
                lblMesseage.Text = "Please Input Item Name";
                return;
            }

            if (ddlSupplier.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Supplier ";
                return;
            }

            if (ddlCategory.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Category";
                return;
            }

            if (ddlUnit.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select a Unit";
                return;
            }
            if (ddlItemType.SelectedIndex.Equals(0))
            {
                lblMesseage.Text = "Please Select Item Status";
                return;
            }

            //if (strPurchasePrice.Equals(""))
            //{
            //    lblMesseage.Text = "Please Input Purchase Price";
            //    return;
            //}

            //if (strSalesPrice.Equals(""))
            //{
            //    lblMesseage.Text = "Please Input Sales Price";
            //    return;
            //}

            string strResult = objServiceHandler.AddNewItem(strItemId, strItemName, strPurchasePrice, strSalesPrice, strSupplierId, strUnitId, strCategoryId, strItemStatus, strSalesVatPer);

            if(strResult.Equals("Successful"))
            {
                ClearAllField();
                lblMesseage.Text = "Item Save Successfully";
                loadgdvItemMaster();

            }
            else
            {
                lblMesseage.Text = "Item Save Failed";
                return;
            }

        }
        catch
        {

        }
        
    }
    protected void gdvListOfItem_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gdvListOfItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "";

        try
        {
           string strItemId = gdvListOfItem.DataKeys[e.RowIndex][0].ToString();

            TextBox lblItemName = gdvListOfItem.Rows[e.RowIndex].FindControl("txtItemName") as TextBox;
            string strItemName = lblItemName.Text.Trim() != null ? lblItemName.Text.Trim() : "0";


            DropDownList ddlSupplierId = gdvListOfItem.Rows[e.RowIndex].FindControl("ddlSupplierE") as DropDownList;
            string strSupplierId = ddlSupplierId.SelectedValue.Trim() != null ? ddlSupplierId.SelectedValue.Trim() : "0";

            DropDownList ddlUnitId = gdvListOfItem.Rows[e.RowIndex].FindControl("ddlUnitE") as DropDownList;
            string strUnitId = ddlUnitId.SelectedValue.Trim() != null ? ddlUnitId.SelectedValue.Trim() : "0";

            DropDownList ddlCategoryId = gdvListOfItem.Rows[e.RowIndex].FindControl("ddlCategoryE") as DropDownList;
            string strCategoryId = ddlCategoryId.SelectedValue.Trim() != null ? ddlCategoryId.SelectedValue.Trim() : "0";

            TextBox lblPurchasePrice = gdvListOfItem.Rows[e.RowIndex].FindControl("txtPurchasePrice") as TextBox;
            string strPurchasePrice = lblPurchasePrice.Text.Trim() != null ? lblPurchasePrice.Text.Trim() : "0";

            TextBox lblSalesPrice = gdvListOfItem.Rows[e.RowIndex].FindControl("txtSalesPrice") as TextBox;
            string strSalesPrice = lblSalesPrice.Text.Trim() != null ? lblSalesPrice.Text.Trim() : "0";

            TextBox lblSalesVatPer = gdvListOfItem.Rows[e.RowIndex].FindControl("txtSalesVatPer") as TextBox;
            string strSalesVatPer = lblSalesVatPer.Text.Trim() != null ? lblSalesVatPer.Text.Trim() : "0";

            DropDownList ddlStatusId = gdvListOfItem.Rows[e.RowIndex].FindControl("ddlStatusE") as DropDownList;
            string strItemStatus = ddlStatusId.SelectedValue.Trim() != null ? ddlStatusId.SelectedValue.Trim() : "0";

            if (strItemName.Equals(""))
            {
                lblMesseage.Text = "Please Input Item Name";
                return;
            }

            if (strSupplierId.Equals(""))
            {
                lblMesseage.Text = "Please Select a Supplier ";
                return;
            }

            if (strCategoryId.Equals(""))
            {
                lblMesseage.Text = "Please Select a Category";
                return;
            }

            if (strUnitId.Equals(""))
            {
                lblMesseage.Text = "Please Select a Unit";
                return;
            }

            if (strItemStatus.Equals("0"))
            {
                lblMesseage.Text = "Please Select Item Status";
                return;
            }

            //if (strPurchasePrice.Equals(""))
            //{
            //    lblMesseage.Text = "Please Input Purchase Price";
            //    return;
            //}

            //if (strSalesPrice.Equals(""))
            //{
            //    lblMesseage.Text = "Please Input Sales Price";
            //    return;
            //}

            strSql = objServiceHandler.UpdateItemInfo(strItemId, strItemName, strSupplierId, strCategoryId, strUnitId, strPurchasePrice, strSalesPrice, strItemStatus, strSalesVatPer);

            if (strSql.Equals("Successfull"))
            {
                lblMesseage.Text = "Item Updated Successfully";
                gdvListOfItem.EditIndex = -1;
                loadgdvItemMaster();

            }
            else
            {
                gdvListOfItem.EditIndex = -1;
                lblMesseage.Text = "Item Updated Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong !";
        }

    }
    protected void gdvListOfItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql = "";

        try
        {
            GridViewRow rows = gdvListOfItem.Rows[e.RowIndex];
            string strItemId = gdvListOfItem.DataKeys[e.RowIndex][0].ToString();
           // string strItemId = "";
            DataSet checkExist = objServiceHandler.ExecuteQuery("SELECT * FROM TRANSACTIONDETAILS WHERE ITEMID='" + strItemId + "'");
            if(checkExist.Tables["Table1"].Rows.Count>0)
            {
                lblMesseage.Text = "Item already use another tables";
                return;
            }

            strSql = objServiceHandler.DeleteItemInfo(strItemId);

            if (strSql.Equals("Deleted"))
            {
                loadgdvItemMaster();
                lblMesseage.Text = "Item Deleted Successfully";
               
            }
            else
            {
                lblMesseage.Text = "Item Deleted Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void gdvListOfItem_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gdvListOfItem.EditIndex = e.NewEditIndex;
            loadgdvItemMaster();
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something Went Wrong";
        }
    }
    protected void gdvListOfItem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gdvListOfItem.EditIndex = -1;
            loadgdvItemMaster();
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }
    }
    protected void gdvListOfItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gdvListOfItem.PageIndex = e.NewPageIndex;
            loadgdvItemMaster();

        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong !";
        }

    }
    protected void gdvListOfItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int totRow = gdvListOfItem.Rows.Count;
            int totCol = e.Row.Cells.Count;
            int rowIndex = e.Row.RowIndex;

            if (e.Row.RowType == DataControlRowType.DataRow && gdvListOfItem.EditIndex != e.Row.RowIndex)
            {
                DropDownList ddlSupplier = (e.Row.FindControl("ddlSupplierS") as DropDownList);
                string strSql2 = "SELECT SupplierId,SupplierName From Supplier ";
                DataSet oDs2 = objServiceHandler.ExecuteQuery(strSql2);
                ddlSupplier.DataSource = oDs2;
                ddlSupplier.DataValueField = "SupplierId";
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataBind();
                string strSupplierId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][1].ToString();
                ddlSupplier.Items.FindByValue(strSupplierId).Selected = true;

                DropDownList ddlUnit = (e.Row.FindControl("ddlUnitS") as DropDownList);
                string strSql = "SELECT UnitId,UnitName From Unit ";
                DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
                ddlUnit.DataSource = oDs;
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataBind();
                string strUnitId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][2].ToString();
                ddlUnit.Items.FindByValue(strUnitId).Selected = true;


                DropDownList ddlCategory = (e.Row.FindControl("ddlCategoryS") as DropDownList);
                string strSql1 = "SELECT CategoryId,CategoryName From Category ";
                DataSet oDs1 = objServiceHandler.ExecuteQuery(strSql1);
                ddlCategory.DataSource = oDs1;
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
                string strCategoryId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][3].ToString();
                ddlCategory.Items.FindByValue(strCategoryId).Selected = true;

                DropDownList ddlStatus = (e.Row.FindControl("ddlStatusS") as DropDownList);
                string strSql3 = "SELECT StatusId,StatusName From ItemStatus";
                DataSet oDs3 = objServiceHandler.ExecuteQuery(strSql3);
                ddlStatus.DataSource = oDs3;
                ddlStatus.DataValueField = "StatusId";
                ddlStatus.DataTextField = "StatusName";
                ddlStatus.DataBind();

                string strStatusId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][4].ToString();
                ddlStatus.Items.FindByValue(strStatusId).Selected = true;
            }

            if (e.Row.RowType == DataControlRowType.DataRow && gdvListOfItem.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlSupplier = (e.Row.FindControl("ddlSupplierE") as DropDownList);
                string strSql2 = "SELECT SupplierId,SupplierName From Supplier ";
                DataSet oDs2 = objServiceHandler.ExecuteQuery(strSql2);
                ddlSupplier.DataSource = oDs2;
                ddlSupplier.DataValueField = "SupplierId";
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataBind();
                string strSupplierId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][1].ToString();
                ddlSupplier.Items.FindByValue(strSupplierId).Selected = true;

                DropDownList ddlUnit = (e.Row.FindControl("ddlUnitE") as DropDownList);
                string strSql = "SELECT UnitId,UnitName From Unit ";
                DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
                ddlUnit.DataSource = oDs;
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataBind();
                string strUnitId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][2].ToString();
                ddlUnit.Items.FindByValue(strUnitId).Selected = true;


                DropDownList ddlCategory = (e.Row.FindControl("ddlCategoryE") as DropDownList);
                string strSql1 = "SELECT CategoryId,CategoryName From Category ";
                DataSet oDs1 = objServiceHandler.ExecuteQuery(strSql1);
                ddlCategory.DataSource = oDs1;
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
                string strCategoryId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][3].ToString();
                ddlCategory.Items.FindByValue(strCategoryId).Selected = true;

                DropDownList ddlStatus = (e.Row.FindControl("ddlStatusE") as DropDownList);
                string strSql3 = "SELECT StatusId,StatusName From ItemStatus";
                DataSet oDs3 = objServiceHandler.ExecuteQuery(strSql3);
                ddlStatus.DataSource = oDs3;
                ddlStatus.DataValueField = "StatusId";
                ddlStatus.DataTextField = "StatusName";
                ddlStatus.DataBind();

                string strStatusId = (string)gdvListOfItem.DataKeys[e.Row.RowIndex][4].ToString();
                ddlStatus.Items.FindByValue(strStatusId).Selected = true;
            }
        }
        catch (Exception)
        {
            lblMesseage.Text = "Something went wrong";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(!ddlSearch.SelectedValue.Equals("0"))
        { 
        loadgdvItemMaster();
        }
        else
        {
            lblMesseage.Text = "Please Select Search Category";
        }
    }

    #endregion

    # region Supplier
    private void loadgdvSupplier()
    {
        string filterName = "";
        string seacrCategoryName = ddlSearchSupplier.SelectedValue;
        try
        {
            if (txtSearchSupplier.Text != "")
            {
                filterName = "WHERE " + seacrCategoryName + " LIKE '%" + txtSearchSupplier.Text + "%'";
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
            strSql = "SELECT ISNULL(MAX(SupplierId),0)+1 FROM Supplier";
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
    protected void btnSearchSupplier_Click(object sender, EventArgs e)
    {
        modalSupplier.Show();
        loadgdvSupplier();
    }
    protected void btnSaveSupplier_Click(object sender, EventArgs e)
    {
        try
        {
            modalSupplier.Show();
            string strSupplierId = txtSupplierId.Text;
            string strSupplierName = txtSupplierName.Text;
            string strSupplierEmail=txtEmailAddress.Text;
            string strSupplierAddress=txtSupplierAddress.Text;
            string strSupplierMobile=txtMobileNo.Text;
            string strWebAddress = txtWebAddress.Text;

            if(strSupplierId.Equals(""))
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

            if(result.Equals("Successful"))
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
        catch (Exception )
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
        ddlSupplier.Items.Clear();
        LoadSupplierList();
        string SupplierId = gdvListOfSupplier.DataKeys[gvr.RowIndex].Value.ToString();
        ddlSupplier.SelectedValue = SupplierId;
    }

    #endregion

    # region Category
    private void loadgdvCategory()
    {
        string filterName = "";
        string seacrCategoryName = ddlCategorySearch.SelectedValue;
        try
        {
            if (txtSearchCategory.Text != "")
            {
                filterName = "WHERE " + seacrCategoryName + " LIKE '%" + txtSearchCategory.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by CategoryId desc) as SL_NO,CategoryId,CategoryName FROM  Category  " + filterName + " ORDER BY CategoryId DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvCategory.DataSource = oDs;
            gdvCategory.DataBind();
        }
        // }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblMessage.Text = "Something went wrong.";
        }
    }
    private void ClearCategoryFilled()
    {
        txtCategoryId.Text = "";
        txtCategoryName.Text = "";
        btnAddNewCategory.Enabled = true;
        btnSaveCategory.Enabled = false;
        
    }
    private void ShowMaxCategoryId()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(CategoryId),0)+1 FROM Category";
            string categoryId = objServiceHandler.ReturnString(strSql);
            txtCategoryId.Text = categoryId.ToString();

        }
        catch
        {
            return;
        }

    }
    protected void btnAddNewCategory_Click(object sender, EventArgs e)
    {
        modalCategory.Show();
        ClearCategoryFilled();
        ShowMaxCategoryId();
        lblMesseageCategory.Text = "";
        btnAddNewCategory.Enabled = false;
        btnSaveCategory.Enabled = true;
    }
    protected void btnClearCategory_Click(object sender, EventArgs e)
    {
        modalCategory.Show();
        ClearCategoryFilled();
        lblMesseageCategory.Text = "";
    }
    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        try
        {
            modalCategory.Show();
            string strCategoryId = txtCategoryId.Text;
            string strCategoryName = txtCategoryName.Text;

            if (strCategoryId.Equals(""))
            {
                lblMesseageCategory.Text = "Please Click Add New Button First";
                return;
            }

            if (strCategoryName.Equals(""))
            {
                lblMesseageCategory.Text = "Please Input Category Name";
                return;
            }

            string result = objItemDependency.AddNewCategory(strCategoryId, strCategoryName);

            if (result.Equals("Successful"))
            {
                ClearCategoryFilled();
                loadgdvCategory();
                lblMesseageCategory.Text = "Category Added Successfully";


            }
            else
            {
                lblMesseageCategory.Text = "Category Added Failled";
                return;
            }

        }
        catch
        {
            lblMesseageCategory.Text = "Something went wrong !";
            return;


        }
    }
    protected void btnSearchCategory_Click(object sender, EventArgs e)
    {
        modalCategory.Show();
        loadgdvCategory();

    }
    protected void gdvCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "";

        try
        {
            modalCategory.Show();
            GridViewRow rows = gdvCategory.Rows[e.RowIndex];
            string strCategoryId = gdvCategory.DataKeys[e.RowIndex].Value.ToString();

            TextBox lblCategoryName = gdvCategory.Rows[e.RowIndex].FindControl("txtCategoryNameE") as TextBox;
            string strCategoryName = lblCategoryName.Text.Trim() != null ? lblCategoryName.Text.Trim() : "0";

            if (strCategoryName.Equals(""))
            {
                lblMesseageCategory.Text = "Please Input Category Name ";
                return;
            }

            //  DropDownList ddlEItemMasterCode = gdvBookingInfo.Rows[e.RowIndex].FindControl("ddlEItemMasterCode") as DropDownList;
            // ddlItemCode.Items.Insert(0, new ListItem(strItemCode.ToString(), strItemMasterID.ToString()));

            //  strSql = objServiceHandler.DeleteItemInfo(strItemId);

            strSql = objItemDependency.UpdateCategoryInfo(strCategoryId, strCategoryName);
            if (strSql.Equals("Successfull"))
            {
                ClearCategoryFilled();
                gdvCategory.EditIndex = -1;
                loadgdvCategory();
                lblMesseageCategory.Text = "Category Updated Successfully";

            }
            else
            {
                gdvCategory.EditIndex = -1;
                lblMesseageCategory.Text = "Category Updated Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageCategory.Text = "Something went wrong !";
        }
    }
    protected void gdvCategory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            modalCategory.Show();
            gdvCategory.EditIndex = e.NewEditIndex;
            loadgdvCategory();
        }
        catch (Exception)
        {
            lblMesseageCategory.Text = "Something Went Wrong";
        }
    }
    protected void gdvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql = "";

        try
        {
            modalCategory.Show();
            GridViewRow rows = gdvCategory.Rows[e.RowIndex];
            string strCategoryId = gdvCategory.DataKeys[e.RowIndex].Value.ToString();

            strSql = objItemDependency.DeleteCategoryInfo(strCategoryId);

            if (strSql.Equals("Deleted"))
            {
                ClearCategoryFilled();
                loadgdvCategory();
                lblMesseageCategory.Text = "Category Deleted Successfully";

            }
            else
            {
                lblMesseageCategory.Text = "Category Deleted Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageCategory.Text = "Something went wrong !";
        }
    }
    protected void gdvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            modalCategory.Show();
            gdvCategory.PageIndex = e.NewPageIndex;
            loadgdvCategory();

        }
        catch (Exception)
        {
            lblMesseageCategory.Text = "Something went wrong !";
        }
    }
    protected void gdvCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            modalCategory.Show();
            gdvCategory.EditIndex = -1;
            loadgdvCategory();
        }
        catch (Exception)
        {
            lblMesseageCategory.Text = "Something went wrong !";
        }
    }
    protected void btnSelectCategory_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        ddlCategory.Items.Clear();
        LoadCategoryList();
        string CategoryId = gdvCategory.DataKeys[gvr.RowIndex].Value.ToString();
        ddlCategory.SelectedValue = CategoryId;
    }

    # endregion

    # region Unit
    private void loadgdvUnit()
    {
        string filterName = "";
        string seacrCategoryName = ddlSearchUnit.SelectedValue;
        try
        {
            if (txtSearchUnit.Text != "")
            {
                filterName = "WHERE " + seacrCategoryName + " LIKE '%" + txtSearchUnit.Text + "%'";
            }
            string strSql = " SELECT ROW_NUMBER() over (order by UnitId desc) as SL_NO,UnitId,UnitName FROM  Unit  " + filterName + " ORDER BY UnitId DESC";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvUnit.DataSource = oDs;
            gdvUnit.DataBind();
        }
        // }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //lblMessage.Text = "Something went wrong.";
        }

    }

    private void ClearUnitFilled()
    {
        txtUnitId.Text = "";
        txtUnitName.Text = "";
        btnAddNewUnit.Enabled = true;
        btnSaveUnit.Enabled = false;
       
    }
    private void ShowMaxUnitId()
    {
        try
        {
            string strSql = "";
            strSql = "SELECT ISNULL(MAX(UnitId),0)+1 FROM Unit";
            string unitId = objServiceHandler.ReturnString(strSql);
            txtUnitId.Text = unitId.ToString();

        }
        catch
        {
            return;
        }

    }
    protected void btnAddNewUnit_Click(object sender, EventArgs e)
    {
        modalUnit.Show();
        lblMesseageUnit.Text = "";
        ClearUnitFilled();
        btnAddNewUnit.Enabled = false;
        btnSaveUnit.Enabled = true;
        ShowMaxUnitId();
    }
    protected void btnClearUnit_Click(object sender, EventArgs e)
    {
        modalUnit.Show();
        ClearUnitFilled();
        lblMesseageUnit.Text = "";
    }
    protected void btnSaveUnit_Click(object sender, EventArgs e)
    {
        try
        {
            modalUnit.Show();
            string strUnitId = txtUnitId.Text;
            string strUnitName = txtUnitName.Text;

            if (strUnitId.Equals(""))
            {
                lblMesseageUnit.Text = "Please Click Add New Button First";
                return;
            }

            if (strUnitName.Equals(""))
            {
                lblMesseageUnit.Text = "Please Input Unit Name";
                return;
            }

            string result = objItemDependency.AddNewUnit(strUnitId, strUnitName);

            if (result.Equals("Successful"))
            {
                ClearUnitFilled();
                loadgdvUnit();
                lblMesseageUnit.Text = "Unit Added Successfully";


            }
            else
            {
                lblMesseageUnit.Text = "Unit Added Failled";
                return;
            }

        }
        catch
        {
            lblMesseageUnit.Text = "Something went wrong !";
            return;


        }
    }
    protected void btnSearchUnit_Click(object sender, EventArgs e)
    {
        modalUnit.Show();
        loadgdvUnit();
    }
    protected void gdvUnit_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strSql = "";

        try
        {
            modalUnit.Show();
            GridViewRow rows = gdvUnit.Rows[e.RowIndex];
            string strUnitId = gdvUnit.DataKeys[e.RowIndex].Value.ToString();

            TextBox lblUnitName = gdvUnit.Rows[e.RowIndex].FindControl("txtUnitNameE") as TextBox;
            string strUnitName = lblUnitName.Text.Trim() != null ? lblUnitName.Text.Trim() : "0";
            if(strUnitName.Equals(""))
            {
                lblMesseageUnit.Text = "Please Input Unit Name ";
                return;
            }

            //  DropDownList ddlEItemMasterCode = gdvBookingInfo.Rows[e.RowIndex].FindControl("ddlEItemMasterCode") as DropDownList;
            // ddlItemCode.Items.Insert(0, new ListItem(strItemCode.ToString(), strItemMasterID.ToString()));

            //  strSql = objServiceHandler.DeleteItemInfo(strItemId);

            strSql = objItemDependency.UpdateUnitInfo(strUnitId, strUnitName);
            if (strSql.Equals("Successfull"))
            {
                ClearUnitFilled();
                gdvUnit.EditIndex = -1;
                loadgdvUnit();
                lblMesseageUnit.Text = "Unit Updated Successfully";

            }
            else
            {
                gdvUnit.EditIndex = -1;
                lblMesseageUnit.Text = "Unit Updated Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageUnit.Text = "Something went wrong !";
        }
    }
    protected void gdvUnit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            modalUnit.Show();
            gdvUnit.EditIndex = e.NewEditIndex;
            loadgdvUnit();
        }
        catch (Exception)
        {
            lblMesseageUnit.Text = "Something Went Wrong";
        }
    }
    protected void gdvUnit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            modalUnit.Show();
            gdvUnit.EditIndex = -1;
            loadgdvUnit();
        }
        catch (Exception)
        {
            lblMesseageUnit.Text = "Something went wrong !";
        }
    }
    protected void gdvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            modalUnit.Show();
            gdvUnit.PageIndex = e.NewPageIndex;
            loadgdvUnit();

        }
        catch (Exception)
        {
            lblMesseageUnit.Text = "Something went wrong !";
        }
    }
    protected void gdvUnit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strSql = "";

        try
        {
            modalUnit.Show();
            GridViewRow rows = gdvUnit.Rows[e.RowIndex];
            string strUnitId = gdvUnit.DataKeys[e.RowIndex].Value.ToString();

            strSql = objItemDependency.DeleteUnitInfo(strUnitId);

            if (strSql.Equals("Deleted"))
            {
                ClearUnitFilled();
                loadgdvUnit();
                lblMesseageUnit.Text = "Unit Deleted Successfully";

            }
            else
            {
                lblMesseageUnit.Text = "Unit Deleted Failled";
                return;
            }

        }
        catch (Exception)
        {

            lblMesseageUnit.Text = "Something went wrong !";
        }
    }
    protected void btnSelectUnit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        ddlUnit.Items.Clear();
        LoadUnitList();
        string UnitId = gdvUnit.DataKeys[gvr.RowIndex].Value.ToString();
        ddlUnit.SelectedValue = UnitId;
    }
    # endregion

}