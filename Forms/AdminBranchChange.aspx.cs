using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_AdminBranchChange : System.Web.UI.Page
{

    clsServiceHandler objServiceHandler = new clsServiceHandler();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBranchList();
        }
    }

    private void LoadBranchList()
    {
        try
        {

            string strSql = "SELECT BRID,BRNAME FROM BRANCHNAME  ORDER BY BRNAME ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
            ddlBranch.DataSource = oDs;
            ddlBranch.DataValueField = "BRID";
            ddlBranch.DataTextField = "BRNAME";
            ddlBranch.DataBind();

            ddlBranch.Items.Insert(0, new ListItem("--Select Branch--"));
        }
        catch (Exception)
        {

            lblMsg.Text = "Something went wrong.";
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        try
        {
            string sqlString1 = "SELECT top 1 BrId FROM BrPermission where BrId='" + ddlBranch.SelectedValue + "' and userId=" + Session["UserId"];

            string result = objServiceHandler.ReturnString(sqlString1);
            if (result.Equals(""))
            {

                lblMsg.Text = "You are not allowed for this branch";
                return;
              }


            Session["BranchID"] = "";
                   Session["BranchName"] = "";
            
                   Session["BranchID"] = ddlBranch.SelectedValue;
                    Session["BranchName"] = ddlBranch.SelectedItem.Text;
            


        }
            catch
        {
            
                return;

            }
        }

    }
