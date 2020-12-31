using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AccBranchChartOfAccounts : System.Web.UI.Page
{


    //DataClassesDataContext db = new DataClassesDataContext();
    //DataClassesDataContext db = new DataClassesDataContext();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());

    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsTransaction obJclsTransaction = new clsTransaction();

    private void loadGrid()
    {

        // var givenBrPermission = from t in db.BranchPermissionViews where t.UserId == Int32.Parse(DropDownListUserList.SelectedValue) select t;
        string sqlString = " select * from AccChartOfAccountsBranchView where BrId =" + Int32.Parse(DropDownListBranchNameList.SelectedValue);
        DataSet givenPermission = objServiceHandler.ExecuteQuery(sqlString);


        GridViewPermissionGiven.DataSource = givenPermission;
        GridViewPermissionGiven.DataBind();

        if (givenPermission.Tables["Table1"].Rows.Count == 0)
        {
            //var availableBr = from t in db.BranchNames select t;
            string sqlString1 = " select * from AccChartOfAccountsMasterView order by AccountsName";
            DataSet availableHead = objServiceHandler.ExecuteQuery(sqlString1);
            GridViewAvailable.DataSource = availableHead;
            GridViewAvailable.DataBind();


        }
        else
        {

            string qry = "";
            //foreach (var br in givenBrPermission)
            foreach (DataRow prows in givenPermission.Tables["Table1"].Rows)
            {
                if (qry == "")
                {
                    qry = qry + "Id<>" + prows["Id"];
                }
                else
                {

                    qry = qry + "and " + "Id<>" + prows["Id"];
                }



                string strSql = "SELECT * FROM AccChartOfAccountsMasterView where " + qry;
                DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

                GridViewAvailable.DataSource = oDs;
                GridViewAvailable.DataBind();


                //var availableBr = from t in db.BranchPermissionViews where + qry +     select t;
                // GridViewAvailable.DataSource = availableBr;
                // GridViewAvailable.DataBind();



            }



        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {



            string sqlString = " select * from BranchName order by BrName";
            DataSet UserList = objServiceHandler.ExecuteQuery(sqlString);


            DropDownListBranchNameList.DataSource = UserList;
            DropDownListBranchNameList.DataBind();


            string sqlString1 = " select * from AccCon_Head order by con_head";
            DataSet conHead = objServiceHandler.ExecuteQuery(sqlString1);


            DropDownListConHead.DataSource = conHead;
            DropDownListConHead.DataBind();


            string sqlString2 = " select * from AccChartOfAccountsMaster order by AccountsName";
            DataSet MasterChartOfAccounts = objServiceHandler.ExecuteQuery(sqlString2);


            DropDownListAccountsHead.DataSource = MasterChartOfAccounts;
            DropDownListAccountsHead.DataBind();
            

        }

    }


    protected void btnShowStatus_Click(object sender, EventArgs e)
    {

        loadGrid();


    }

    public string AddAccountsHead(string AcId, string BrId, string EntryBy)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT top 1 AccChartOfAccounts_id,BranchName_BrId,EntryBy FROM AccChartOfAccountsBranch";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "AccChartOfAccountsBranch";
            oOrderRow = oDS.Tables["AccChartOfAccountsBranch"].NewRow();

            oOrderRow["AccChartOfAccounts_id"] = AcId;
            oOrderRow["BranchName_BrId"] = BrId;
            oOrderRow["entryBy"] = EntryBy;

            oDS.Tables["AccChartOfAccountsBranch"].Rows.Add(oOrderRow);
            oda.Update(oDS, "AccChartOfAccountsBranch");
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

    protected void GridViewAvailable_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "cnAdd")
        {



            //    BrPermission brPer = new BrPermission();
            //  brPer.BrId =Convert.ToInt32(e.CommandArgument);
            // brPer.UserId =Convert.ToInt32(DropDownListUserList.SelectedValue);

            string result = AddAccountsHead(Convert.ToInt32(e.CommandArgument).ToString(), Convert.ToInt32(DropDownListBranchNameList.SelectedValue).ToString(), "1");

            if (result.Equals("Successful"))
            {
                LblMsg.Text = "Successfully added";
            }
            else
            {
                LblMsg.Text = "Problem";

            }







            loadGrid();
        }

    }
    protected void GridViewAvailable_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkAdd = (LinkButton)e.Row.FindControl("lnkAdd");
            lnkAdd.CommandArgument = e.Row.Cells[0].Text;


        }


    }
    protected void GridViewPermissionGiven_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridViewPermissionGiven_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "cnDelete")
        {


            //var del =(from t in db.BrPermissions where t.id==Convert.ToInt32(e.CommandArgument)   select t).Single();

            string result = obJclsTransaction.DeleteBrPermission(Convert.ToInt32(e.CommandArgument).ToString());
            // db.BrPermissions.DeleteOnSubmit(del);
            //db.SubmitChanges();


            loadGrid();
        }

    }
    protected void GridViewPermissionGiven_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkDelete.CommandArgument = e.Row.Cells[0].Text;


        }

    }
    protected void GridViewAvailable_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownListUserList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnCon_head_Click(object sender, EventArgs e)
    {

        if (DropDownListBranchNameList.Text == "")
        {
            LblMsg.Text = "First Select branch ";
            DropDownListBranchNameList.Focus();

            return;

        }
        loadGrid_withConHead();

    }

    private void loadGrid_withConHead()
    {
        string sqlString = " select * from AccChartOfAccountsBranchView where BrId =" + Int32.Parse(DropDownListBranchNameList.SelectedValue) + " and con_head ='" + (DropDownListConHead.SelectedValue)+ "'";
        DataSet givenPermission = objServiceHandler.ExecuteQuery(sqlString);


        GridViewPermissionGiven.DataSource = givenPermission;
        GridViewPermissionGiven.DataBind();

        if (givenPermission.Tables["Table1"].Rows.Count == 0)
        {
            //var availableBr = from t in db.BranchNames select t;
            string sqlString1 = " select * from AccChartOfAccountsMasterView order by AccountsName";
            DataSet availableHead = objServiceHandler.ExecuteQuery(sqlString1);
            GridViewAvailable.DataSource = availableHead;
            GridViewAvailable.DataBind();


        }
        else
        {

            string qry = "";
            //foreach (var br in givenBrPermission)
            foreach (DataRow prows in givenPermission.Tables["Table1"].Rows)
            {
                if (qry == "")
                {
                    qry = qry + "Id<>" + prows["Id"];
                }
                else
                {

                    qry = qry + "and " + "Id<>" + prows["Id"];
                }



                string strSql = "SELECT * FROM AccChartOfAccountsMasterView where " + qry  +" and con_head ='" + (DropDownListConHead.SelectedValue)+"'";
                DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

                GridViewAvailable.DataSource = oDs;
                GridViewAvailable.DataBind();


            }



        }

    }
    protected void btnFindByAccountName_Click(object sender, EventArgs e)
    {
        if (DropDownListBranchNameList.Text == "")
        {
            LblMsg.Text = "First Select branch ";
            DropDownListBranchNameList.Focus();

            return;

        }
        loadGrid_withAccountsName();

    }

    private void loadGrid_withAccountsName()
    {
        string sqlString = " select * from AccChartOfAccountsBranchView where BrId =" + Int32.Parse(DropDownListBranchNameList.SelectedValue) + " and id =" + Int32.Parse(DropDownListAccountsHead .SelectedValue) ;
        DataSet givenPermission = objServiceHandler.ExecuteQuery(sqlString);


        GridViewPermissionGiven.DataSource = givenPermission;
        GridViewPermissionGiven.DataBind();

        if (givenPermission.Tables["Table1"].Rows.Count == 0)
        {
            
            string sqlString1 = " select * from AccChartOfAccountsMasterView where  id =" + Int32.Parse(DropDownListAccountsHead .SelectedValue) ;
            DataSet availableHead = objServiceHandler.ExecuteQuery(sqlString1);
            GridViewAvailable.DataSource = availableHead;
            GridViewAvailable.DataBind();


        }
        else
        {

            
                string strSql = "SELECT top 0 * FROM AccChartOfAccountsMasterView";
                DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

                GridViewAvailable.DataSource = oDs;
                GridViewAvailable.DataBind();


            }



        }

    }
