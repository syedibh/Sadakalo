using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_1111Default : System.Web.UI.Page
{
    decimal balance = 0;
    decimal BalanceAmount = 0;
    decimal TotalRowCount = 0;
    

    clsServiceHandler objclsServiceHandler = new clsServiceHandler();
    //clsServiceHandler objServiceHandler = new clsServiceHandler();


    public static string refNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            refNo = Session["RefNo"].ToString();
            string qryString = "SELECT * FROM AccTransactionList WHERE id='" + refNo + "'";
            DataSet headerinfo = objclsServiceHandler.ExecuteQuery(qryString);
            if (headerinfo.Tables["Table1"].Rows.Count == 1)
            {
                foreach (DataRow prows in headerinfo.Tables["Table1"].Rows)
                {

                    LblBillno.Text = prows["vno"].ToString();
                    LblDate.Text = String.Format("{0:ddd, MMM d, yyyy}",  prows["VDate"].ToString());
                    string br = prows["BrId"].ToString();
                    string posted_by = prows["PostedBy"].ToString();
                    LblVoucherNote.Text = prows["VoucherNote"].ToString();
                    LblUserName.Text = objclsServiceHandler.ReturnString("Select LogName from UserList where  UserId=" + posted_by);
                    LblBrName.Text = "Branch name : " + objclsServiceHandler.ReturnString("Select brName from BranchName where  brId=" + br);
                    LblBrAddress.Text = objclsServiceHandler.ReturnString("Select Address from BranchName where  brId=" + br);
                    LblCompanyName.Text = objclsServiceHandler.ReturnString("Select companyName from companyInformation where [default]=1");

                    if (prows["VType"].ToString() == "1")
                    {
                        LblVoucherType.Text = "Debit Voucher";
                    }



                    else if (prows["VType"].ToString() == "2")
                    {
                        LblVoucherType.Text = "Credit Voucher";
             
                    }


                    else if (prows["VType"].ToString() == "3")
                    {
                        LblVoucherType.Text = "Contra Voucher";

                    }

                    else if (prows["VType"].ToString() == "4")
                    {
                        LblVoucherType.Text = "Journal Voucher";

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
        string strSql = "SELECT  *  FROM   AccTransactionDetailsView where id=" + ReferenceNo;


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



            //qnty = Convert.ToDecimal(e.Row.Cells[2].Text);
            //totalQnty = qnty + totalQnty;

            //Vat = Convert.ToDecimal(e.Row.Cells[4].Text);
            //TotalVat = Vat + TotalVat;

            //Discount = Convert.ToDecimal(e.Row.Cells[5].Text);
            //TotalDiscount = Discount + TotalDiscount;



            BalanceAmount = Convert.ToDecimal(e.Row.Cells[3].Text);
            balance = balance + BalanceAmount;


            TotalRowCount = TotalRowCount + 1;

        }


        if (e.Row.RowType == DataControlRowType.Footer)
        {

            e.Row.Cells[0].Text = TotalRowCount.ToString();
            e.Row.Cells[1].Text = "Total : ";
            //e.Row.Cells[2].Text = totalQnty.ToString();
            //e.Row.Cells[4].Text = TotalVat.ToString();
            //e.Row.Cells[5].Text = TotalDiscount.ToString();
            e.Row.Cells[3].Text = balance.ToString();



            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;



        }





    }
}