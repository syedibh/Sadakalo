using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_HomeMaster : System.Web.UI.MasterPage
{
    clsServiceHandler obJclsServiceHandler = new clsServiceHandler();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            loadWelcomeMsg();
            loadFooterMsg();
            loadLogo();

            if (IsPostBack == true)
            {
                return;
            }

            lblUserName.Text = Session["LoginName"].ToString();
            lblBranch.Text = Session["BranchName"].ToString();
            visibleFalseAll();
            permissionCheck();
        }


        catch
        {
            Response.Redirect("../Login.aspx");
        }


    }

    private void loadWelcomeMsg()
    {
        string titleMsg = obJclsServiceHandler.ReturnString("SELECT title FROM CompanyInformation WHERE [Default]=1");
        lblHeaderTitle.Text = titleMsg;
        title.Text = titleMsg;
    }
    private void loadFooterMsg()
    {
        string footerMsg = obJclsServiceHandler.ReturnString("SELECT FooterMSG FROM CompanyInformation WHERE [Default]=1");
        lblFooterMsg.Text =" " + footerMsg;
    }

    private void loadLogo()
    {
        string logoName = obJclsServiceHandler.ReturnString("SELECT BannerImage FROM CompanyInformation WHERE [Default]=1");
        logo.ImageUrl = "../Images/" + logoName;
    }
    private void visibleFalseAll()
    {
        {
            //Head Office
            iReceivedSupplier.Visible = false;
            iReturnSupplier.Visible = false;
            iListOfItem.Visible = false;
            iReportHO.Visible = false;
            iOrder.Visible = false;
            iOrderReceived.Visible = false;
            iTransferInHO.Visible = false;
            iTransferOutHO.Visible = false;
            iAdjustment.Visible = false;

            //Branch
            iSales.Visible = false;
            iSalesReturn.Visible = false;

            iTransferInSR.Visible = false;
            iTransferOutSR.Visible = false;
            iReportSR.Visible = false;

            iUpdateBalance.Visible = false;

            //Administration
            iBranchPermision.Visible = false;
            iNewUser.Visible = false;
            iPermision.Visible = false;
            iOption.Visible = false;
            iPassword.Visible = false;
            iBackupDB.Visible = false;


            //Accounts
            iDrVoucher.Visible = false;
            iCrVoucher.Visible = false;
            iContraVoucher.Visible = false;
            iJvVoucher.Visible = false;
            iAccountsReport.Visible = false;
            iMasterChartOfAccounts.Visible = false;
            iBranchChartOfAccounts.Visible = false;





        }

    }
  
    private void permissionCheck()
    {
        string sqlString = "select * from permission where empid='" + Session["UserID"].ToString() + "'";
        DataSet permissionTable = obJclsServiceHandler.ExecuteQuery(sqlString);

        if (permissionTable.Equals(""))
        { return; }

        if (permissionTable.Tables["Table1"].Rows.Count != 0)
        {
            foreach (DataRow prows in permissionTable.Tables["Table1"].Rows)
            {
                //Head office

                if (prows["buttonName"].ToString() == "Purchase")
                {
                    iReceivedSupplier.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Purchase Return")
                {
                    iReturnSupplier.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Transfer in HO")
                {
                    iTransferInHO.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Transfer out HO")
                {
                    iTransferOutHO.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Adjustment")
                {
                    iAdjustment.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Update Closing Balance")
                {
                    iUpdateBalance.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Report HO")
                {
                    iReportHO.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Item Entry")
                {
                    iListOfItem.Visible = true;
                }


                else if (prows["buttonName"].ToString() == "Order")
                {
                    iOrder.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Order Received")
                {
                    iOrderReceived.Visible = true;
                }



                             //Branch office

                else if (prows["buttonName"].ToString() == "Sales")
                {
                    iSales.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Sales Return")
                {
                    iSalesReturn.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Transfer In SR")
                                                            
                {
                    iTransferInSR.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Transfer Out SR")
                {
                    iTransferOutSR.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Report SR")
                {
                    iReportSR.Visible = true;
                }



                    //Administration
                else if (prows["buttonName"].ToString() == "Permission")
                {
                    iPermision.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Change Password")
                {
                    iPassword.Visible = true;
                }


                else if (prows["buttonName"].ToString() == "Create New User")
                {
                    iNewUser.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Options")
                {
                    iOption.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Branch Permission")
                {
                    iBranchPermision.Visible = true;
                }


                else if (prows["buttonName"].ToString() == "Backup")
                {
                    iBackupDB.Visible = true;
                }
                //Accounts

                else if (prows["buttonName"].ToString() == "Debit Voucher")
                {
                    iDrVoucher.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Credit Voucher")
                {
                    iCrVoucher.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Contra Voucher")
                {
                    iContraVoucher.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Journal Voucher")
                {
                    iJvVoucher.Visible = true;
                }
                else if (prows["buttonName"].ToString() == "Accounts Report")
                {
                    iAccountsReport.Visible = true;
                }
                   else if (prows["buttonName"].ToString() == "Master Chart of Accounts")
                {
                    iMasterChartOfAccounts.Visible = true;
                }

                else if (prows["buttonName"].ToString() == "Branch Chart of Accounts")
                {
                    iBranchChartOfAccounts.Visible = true;
                }
                
                                        
  
                                        


            }

        }
    }


}