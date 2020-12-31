using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_SalesMemo : System.Web.UI.Page
{

    decimal qnty = 0;
    decimal totalQnty = 0;
    decimal balance = 0;
    decimal BalanceAmount = 0;
    decimal TotalRowCount=0;
    decimal TotalVat = 0;
    decimal Vat = 0;
    decimal Discount = 0;
    decimal TotalDiscount = 0;


    clsServiceHandler objclsServiceHandler = new clsServiceHandler();
    //clsServiceHandler objServiceHandler = new clsServiceHandler();


    public static string refNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

        refNo = Session["RefNo"].ToString();
        string qryString = "SELECT * FROM TRANSACTIONLIST WHERE refNo='"+refNo+"'";
        DataSet headerinfo = objclsServiceHandler.ExecuteQuery(qryString);
        if (headerinfo.Tables["Table1"].Rows.Count == 1)
        {
            foreach (DataRow prows in headerinfo.Tables["Table1"].Rows)
            {

                LblBillno.Text = prows["vno"].ToString();
                LblDate.Text = prows["Date"].ToString();
                string br = prows["BrId"].ToString();
                string posted_by = prows["PostedBy"].ToString();
                LblUserName.Text = objclsServiceHandler.ReturnString("Select LogName from UserList where  UserId=" + posted_by);
                LblBrName.Text= "Branch name : "+objclsServiceHandler.ReturnString("Select brName from BranchName where  brId=" +br );
                LblBrAddress.Text= objclsServiceHandler.ReturnString("Select Address from BranchName where  brId=" +br );
                LblCompanyName.Text=objclsServiceHandler.ReturnString("Select * from companyInformation where [default]=1");


                if (prows["CustomerId"].Equals("0"))
                {


                    LblCustomerName.Text ="";
                    LblMobileNo.Text = "";

                } 
                else
                {


                    LblCustomerName.Text = objclsServiceHandler.ReturnString("Select CustomerName from customerProfile where  CustomerId=" + prows["CustomerId"].ToString());
                    LblMobileNo.Text = objclsServiceHandler.ReturnString("Select MobileNo from customerProfile where  CustomerId=" + prows["CustomerId"].ToString());
                
                }
            }
        }
        loadItemDetails(refNo);
        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    
    }

    private void loadItemDetails(string ReferenceNo)
    {
        string strSql = "SELECT  TransactionDetails.*, Item.ItemDescription FROM   Item INNER JOIN  TransactionDetails ON Item.ItemId = TransactionDetails.ItemId where TransactionDetails.TransactionList_id=" + ReferenceNo;


        DataSet oDs = objclsServiceHandler.ExecuteQuery(strSql);

     //  gdvSalesDetails.DataSource = oDs;
      //  gdvSalesDetails.DataBind();

        gdvSalesDetails1.DataSource = oDs;
        gdvSalesDetails1.DataBind();




    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            //  lblMesseage.Text = "";
            //txtBillNo.Text = "";
            //txtVat.Text = "0.00";
            //txtDiscount.Text = "0.00";
            //string billNo = txtSearchChallan.Text;
            //if (billNo.Equals(""))
            //{
            //  lblMesseage.Text = "Please Input Bill No For Search";
            // return;
            //}
            //loadgdvSalesDetails(billNo);
            //LoadFooterInfo(billNo);
            //txtBillNo.Text = billNo.ToString();

        }
        catch
        {
            //lblMesseage.Text = "Something went wrong !";
            return;

        }
    }
    protected void gdvSalesDetails1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gdvSalesDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            


            qnty = Convert.ToDecimal(e.Row.Cells[2].Text);
            totalQnty = qnty + totalQnty;

            Vat = Convert.ToDecimal(e.Row.Cells[4].Text);
            TotalVat  = Vat + TotalVat;

            Discount  = Convert.ToDecimal(e.Row.Cells[5].Text);
            TotalDiscount = Discount + TotalDiscount;



            BalanceAmount = Convert.ToDecimal (e.Row.Cells[6].Text);
            balance = balance + BalanceAmount;


            TotalRowCount = TotalRowCount + 1;

        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[0].Text = TotalRowCount.ToString();
            e.Row.Cells[1].Text = "Total : ";
            e.Row.Cells[2].Text = totalQnty.ToString();
            e.Row.Cells[4].Text =TotalVat.ToString();
            e.Row.Cells[5].Text = TotalDiscount.ToString();
            e.Row.Cells[6].Text = BalanceAmount.ToString();



            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
  


        }

    



    }
}