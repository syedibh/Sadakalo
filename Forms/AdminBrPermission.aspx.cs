using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AdminBrPermission : System.Web.UI.Page
{

    //DataClassesDataContext db = new DataClassesDataContext();
    //DataClassesDataContext db = new DataClassesDataContext();

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionCBHC"].ToString());

    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsTransaction obJclsTransaction = new clsTransaction();

    private void loadGrid()
    {

        // var givenBrPermission = from t in db.BranchPermissionViews where t.UserId == Int32.Parse(DropDownListUserList.SelectedValue) select t;
        string sqlString = " select * from BranchPermissionView where UserId =" + Int32.Parse(DropDownListUserList.SelectedValue);
        DataSet givenBrPermission = objServiceHandler.ExecuteQuery(sqlString);


        GridViewPermissionGiven.DataSource = givenBrPermission;
        GridViewPermissionGiven.DataBind();

        if (givenBrPermission.Tables["Table1"].Rows.Count == 0)
        {
            //var availableBr = from t in db.BranchNames select t;
            string sqlString1 = " select * from BranchName order by BrName";
            DataSet availableBr = objServiceHandler.ExecuteQuery(sqlString1);
            GridViewAvailable.DataSource = availableBr;
            GridViewAvailable.DataBind();


        }
        else
        {

            string qry = "";
            //foreach (var br in givenBrPermission)
            foreach (DataRow prows in givenBrPermission.Tables["Table1"].Rows)
            {
                if (qry == "")
                {
                    qry = qry + "BrId<>" + prows["BrId"];
                }
                else
                {

                    qry = qry + "and " + "BrId<>" + prows["BrId"];
                }



                string strSql = "SELECT * FROM BranchName where " + qry;
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



            string sqlString = " select * from UserList order by logName";
            DataSet UserList = objServiceHandler.ExecuteQuery(sqlString);


            DropDownListUserList.DataSource = UserList;
            DropDownListUserList.DataBind();



        }

    }


    protected void btnShowStatus_Click(object sender, EventArgs e)
    {

        loadGrid();


    }

    public string AddBranch(string brid, string Userid)
    {
        DataRow oOrderRow;
        string strSql = "";
        DataSet oDS = new DataSet();
        con.Open();
        try
        {

            strSql = "SELECT BrId,UserId FROM BrPermission";
            SqlCommand cmd = new SqlCommand(strSql, con);
            SqlDataAdapter oda = new SqlDataAdapter(cmd);
            SqlCommandBuilder Oledecommandbuilder = new SqlCommandBuilder(oda);
            oda.FillSchema(oDS, SchemaType.Source);

            DataTable pTable = oDS.Tables["Table"];
            pTable.TableName = "BrPermission";
            oOrderRow = oDS.Tables["BrPermission"].NewRow();

            oOrderRow["BrId"] = brid;
            oOrderRow["UserId"] = Userid;


            oDS.Tables["BrPermission"].Rows.Add(oOrderRow);
            oda.Update(oDS, "BrPermission");
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

            string result = AddBranch(Convert.ToInt32(e.CommandArgument).ToString(), Convert.ToInt32(DropDownListUserList.SelectedValue).ToString());

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
}