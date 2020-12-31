using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_ReportListCrystal : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();

  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           // branchId = Session["BranchID"].ToString();
          //  userId = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                LoadgdvItem();

            }


        }

        catch
        {
            Response.Redirect("../Login.aspx");
        }
      
    }
    protected void gdvListOfItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gdvListOfItem.PageIndex = e.NewPageIndex;
            //gdvListOfItem(txtBillNo.Text);
           
        }
        catch (Exception)
        {
            //lblMesseage.Text = "Something went wrong !";
        }
    }
    private void LoadgdvItem()
    { 
        try
        {
            string strSql = " select id, reportname from report order by reportName ";
            DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

            gdvListOfItem.DataSource = oDs;
            gdvListOfItem.DataBind();
        }

        catch (Exception)
        {
           // lblMesseage.Text = "Something went wrong.";
        }
    
    }

    protected void gdvListOfItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        // GridViewRow rows = gdvListOfItem.Rows[e.RowIndex];
         string strReportId = gdvListOfItem.DataKeys[e.RowIndex][0].ToString();
         string strReportName=gdvListOfItem.DataKeys[e.RowIndex][1].ToString();
    }
}