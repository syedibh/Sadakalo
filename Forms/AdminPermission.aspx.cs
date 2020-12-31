using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Forms_AdminPermission : System.Web.UI.Page
{

    clsServiceHandler clsServiceHandlerObj = new clsServiceHandler();

    clsItemDependency obJclsItemDependency = new clsItemDependency();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == true)
        {
            return;
        }
        DataSet user = clsServiceHandlerObj.ExecuteQuery("Select * from userList order by logname");

        DropDownListUserName.DataSource = user;
        DropDownListUserName.DataBind();

    }
    protected void CreateNewUserChk8_CheckedChanged(object sender, EventArgs e)
    {
    }
    protected void showBtn_Click(object sender, EventArgs e)
    {
        ClearCheckBox();
        showPermission();
  
 
   }
        
   

    private void showPermission()
    {
        string strSql="select * from  Permission where empId ="+ Int32.Parse(DropDownListUserName.SelectedValue);
        DataSet totalInfo = clsServiceHandlerObj.ExecuteQuery(strSql);
   if (totalInfo.Equals(""))
   {
       return;
   }

        if (totalInfo.Tables["Table1"].Rows.Count !=0)
   {
       foreach (DataRow prows in totalInfo.Tables["Table1"].Rows)
       {
           //Administration
           if (prows["buttonName"].ToString() == CreateNewUserChk.Text)
           {
               CreateNewUserChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == PermissionChk.Text)
           {
               PermissionChk.Checked = true;
           }


           else if (prows["buttonName"].ToString() == OptionsChk.Text)
           {
               OptionsChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == CanUndoBillChk.Text)
           {
               CanUndoBillChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == changePasswordChk.Text)
           {
               changePasswordChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == BackupDatabaseChk.Text)
           {
               BackupDatabaseChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == BranchPermissionChk.Text)
           {
               BranchPermissionChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == AdjustmentChk.Text)
           {
               AdjustmentChk.Checked = true;
           }



               //Head office


           else if (prows["buttonName"].ToString() == purchaseChk.Text)
           {
               purchaseChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == purchaseReturnChk.Text)
           {
               purchaseReturnChk.Checked = true;
           }


           else if (prows["buttonName"].ToString() == ItemEntryChk.Text)
           {
               ItemEntryChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == canChangeRateChk.Text)
           {
               canChangeRateChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == TransferOutHoChk.Text)
           {
               TransferOutHoChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == TransferInHoChk.Text)
           {
               TransferInHoChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == ReportHoChk.Text)
           {
               ReportHoChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == OrderChk.Text)
           {
               OrderChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == OrderChk.Text)
           {
               OrderChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == OrderReceivedChk.Text)
           {
               OrderReceivedChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == UpdateClosingBalanceChk.Text)
           {
               UpdateClosingBalanceChk.Checked = true;
           }


        //Branch Office
           else if (prows["buttonName"].ToString() == SalesChk.Text)
           {
               SalesChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == salesReturnChk.Text)
           {
               salesReturnChk.Checked = true;
           }

           //
           else if (prows["buttonName"].ToString() == TransferInSrChk.Text)
           {
               TransferInSrChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == TransferOutSrChk.Text)
           {
               TransferOutSrChk.Checked = true;
           }

           else if (prows["buttonName"].ToString() == ReportSrChk.Text)
           {
               ReportSrChk.Checked = true;
           }


           //Accounts
           


           else if (prows["buttonName"].ToString() == AccDrVoucherChk.Text)
           {
               AccDrVoucherChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == AccCrVoucherChk.Text)
           {
               AccCrVoucherChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == AccCtVoucherChk.Text)
           {
               AccCtVoucherChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == AccJvVoucherChk.Text)
           {
               AccJvVoucherChk.Checked = true;
           }
           else if (prows["buttonName"].ToString() == AccReportChk.Text)
           {
               AccReportChk.Checked = true;
           }



        


           ////Administration
           //iBranchPermision.Visible = false;
           //iNewUser.Visible = false;
           //iPermision.Visible = false;
           //iOption.Visible = false;
           //iPassword.Visible = false;
           //iBackupDB.Visible = false;



       }
           
       }

    }
    protected void saveBtn_Click(object sender, EventArgs e)
    {

        if (Int32.Parse(DropDownListUserName.SelectedValue) == 0)
        {
            lblMsg.Text = "First select employee name";
            DropDownListUserName.Focus();
            return;
        }


  
        string result = obJclsItemDependency.DeletePermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString());
         if (result.Equals("Deleted"))
         {
             lblMsg.Text = "Item Deleted Successfully";

         }
         

//Administration

    if     (CreateNewUserChk.Checked == true)
    {
      string result1= clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), CreateNewUserChk.Text);
    }

    if (PermissionChk.Checked == true)
    {
        string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), PermissionChk.Text);
    }
    if (OptionsChk.Checked == true)
    {
      string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), OptionsChk.Text); 
    }
          if (CanUndoBillChk.Checked == true)
    {

        string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), CanUndoBillChk.Text);

        
    }
          if (changePasswordChk.Checked == true)
    {

        string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), changePasswordChk.Text);

    

    }


          if (BranchPermissionChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), BranchPermissionChk.Text);



          }
          if (BackupDatabaseChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), BackupDatabaseChk.Text);
          }

          if (AdjustmentChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AdjustmentChk.Text);
          }

          if (TransferOutHoChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferOutHoChk.Text);
          }
          if (TransferInHoChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferInHoChk.Text);
          }
     

        //Branch 
        
        if (SalesChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), SalesChk.Text);
          }
          if (salesReturnChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), salesReturnChk.Text);
          }
          if (TransferInSrChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferInSrChk.Text);
          }

          if (TransferOutSrChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferOutSrChk.Text);
          }

          if (ReportSrChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), ReportSrChk.Text);
          }

          if (TransferInSrChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferInSrChk.Text);
          }

          if (TransferOutSrChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), TransferOutSrChk.Text);
          }



        //Head Office

          if (ItemEntryChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), ItemEntryChk.Text);   
          }

          if (canChangeRateChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), canChangeRateChk.Text);

          }

          if (purchaseReturnChk.Checked == true)
          {
              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), purchaseReturnChk.Text);


          }

          if (purchaseChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), purchaseChk.Text);
          }

          if (ReportHoChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), ReportHoChk.Text);
          }



          if (UpdateClosingBalanceChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), UpdateClosingBalanceChk.Text);
          }

          if (AdjustmentChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AdjustmentChk.Text);
          }

          if (OrderChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), OrderChk.Text);
          }

          if (OrderReceivedChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), OrderReceivedChk.Text);
          }


          if (canChangeRateChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), canChangeRateChk.Text);
          }



          if (AdjustmentChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AdjustmentChk.Text);
          }

//Accounts


          if (AccDrVoucherChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AccDrVoucherChk.Text);
          }

          if (AccCrVoucherChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AccCrVoucherChk.Text);
          }

          if (AccCtVoucherChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AccCtVoucherChk.Text);
          }

          if (AccJvVoucherChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AccJvVoucherChk.Text);
          }

          if (AccReportChk.Checked == true)
          {

              string result1 = clsServiceHandlerObj.AddPermission(Int32.Parse(DropDownListUserName.SelectedValue).ToString(), AccReportChk.Text);
          }

  
          
       





          lblMsg.Text = "Data Saved";
          
    
    }


    private void ClearCheckBox()
    {
        
        //Administration
        CreateNewUserChk.Checked = false;
        PermissionChk.Checked = false;
        OptionsChk.Checked = false;
        CanUndoBillChk.Checked = false;
        changePasswordChk.Checked = false;
        BackupDatabaseChk.Checked = false;
        BranchPermissionChk.Checked = false;


        //Head Office
        
        purchaseChk.Checked = false;
        purchaseReturnChk.Checked = false;
    
        ItemEntryChk.Checked = false;
        canChangeRateChk.Checked = false;
        TransferOutHoChk.Checked = false;
        TransferInHoChk.Checked = false;
        OrderChk.Checked = false;
        OrderReceivedChk.Checked = false;
        UpdateClosingBalanceChk.Checked = false;
        ReportHoChk.Checked = false;
        AdjustmentChk.Checked = false;


        //Br office
        SalesChk.Checked = false;
        salesReturnChk.Checked = false;
        TransferOutSrChk.Checked = false;
        TransferInSrChk.Checked = false;
        ReportSrChk.Checked = false;

        //Accounts
        AccDrVoucherChk.Checked=false;
        AccCrVoucherChk.Checked = false;
        AccCtVoucherChk.Checked = false;
        AccJvVoucherChk.Checked = false;
        AccReportChk.Checked = false;







    }


}