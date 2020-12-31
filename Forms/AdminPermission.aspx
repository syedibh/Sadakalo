<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AdminPermission.aspx.cs" Inherits="Forms_AdminPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 100%;
        }
        .auto-style4 {
            height: 25px;
            width: 245px;
        }
                .auto-style1 {
                    height: 25px;
                }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <div class="container">
      <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">User Permision Setup </p>
            </div>
            <div class="col-sm-2">
                <p style="margin-top: 0.5rem">
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                </p>
            </div>
        </div>
        <br />  
       <div class="row">
            <div class="col-sm-8 col-lg-offset-2">
                <asp:Panel ID="Panel1" runat="server" Style="background-color: #f99999; border-radius:1rem; height:4rem;">
                    <span style="margin-left: 2rem;">Select User Name :</span> 
            <asp:DropDownList ID="DropDownListUserName" runat="server" Class="Select2" DataTextField="LogName" DataValueField="userId"  style="width:30rem;text-align:center !important;">
            </asp:DropDownList>
                    <asp:Button ID="showBtn" runat="server" Text="Show" CssClass="btn btn-primary" Style="height:4rem" OnClick="showBtn_Click" />
                    <asp:Button ID="saveBtn" runat="server" Text="Save" CssClass="btn btn-success" Style="height:4rem" OnClick="saveBtn_Click" />

                </asp:Panel>
            </div>
        </div>        
   <br />
           <div class="row">
            <div class="col-lg-12">
                <asp:Panel ID="Panel2" runat="server" Style="background-color: #59e8e8; border-radius:1rem; height:40rem;">
                    <div class="col-lg-4">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" style="font-family: fantasy;font-size:3rem" runat="server" Text="Administration"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CreateNewUserChk" runat="server" Text="Create New User" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="changePasswordChk" runat="server" Text="Change Password" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="BackupDatabaseChk" runat="server" Text="Backup Database" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CanUndoBillChk" runat="server" Text="Can undo bill" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="OptionsChk" runat="server" Text="Options" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="PermissionChk" runat="server" Text="Permission" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="BranchPermissionChk" runat="server" Text="Branch Permission" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-4">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" style="font-family: fantasy;font-size:3rem" Text="Head Office"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="purchaseChk" runat="server" Text="Purchase" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="purchaseReturnChk" runat="server" Text="Purchase Return" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="TransferInHoChk" runat="server" Text="Transfer In HO" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="TransferOutHoChk" runat="server" Text="Transfer Out HO" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ReportHoChk" runat="server" Text="Report HO" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="UpdateClosingBalanceChk" runat="server" Text="Update Closing Balance" />
                                </td>
                            </tr>
                            <tr>
                                <%-- <td class="auto-style3" style="width : 200px"></td>--%>
                                <td>
                                    <asp:CheckBox ID="AdjustmentChk" runat="server" Text="Adjustment" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="OrderChk" runat="server" Text="Order" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="OrderReceivedChk" runat="server" Text="Order Received" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ItemEntryChk" runat="server" Text="Item Entry" OnCheckedChanged="CreateNewUserChk8_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="canChangeRateChk" runat="server" Text="Can Change Rate" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-4">
                        <table>
                        <tr>
                            <td> <asp:Label ID="Label3" runat="server" style="font-family: fantasy;font-size:3rem" Text="Br. Office"></asp:Label> </td>
                        </tr>
                        <tr>
                            <td>  <asp:CheckBox ID="SalesChk" runat="server" Text="Sales" /> </td>
                        </tr>
                        <tr>
                            <td> <asp:CheckBox ID="salesReturnChk" runat="server" Text="Sales Return" /> </td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="TransferInSrChk" runat="server" Text="Transfer In SR" /> </td>
                        </tr>
                        <tr>
                            <td> <asp:CheckBox ID="TransferOutSrChk" runat="server" Text="Transfer Out SR" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="ReportSrChk" runat="server" Text="Report SR" /> </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label4" runat="server" style="font-family: fantasy;font-size:3rem" Text="Accounts"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="AccDrVoucherChk" runat="server" Text="Debit Voucher" /></td>      
                        </tr>
                        <tr>
                            <td ><asp:CheckBox ID="AccCrVoucherChk" runat="server" Text="Credit Voucher" /></td>
                       
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="AccCtVoucherChk" runat="server" Text="Contra Voucher" /></td>    
                        </tr>
                        <tr>
                            <td ><asp:CheckBox ID="AccJvVoucherChk" runat="server" Text="Journal Voucher" /> </td>
                       
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="AccMasterChartOfAccountsChk" runat="server" Text="Master Chart of Accounts" /></td>         
                        </tr>

                        <tr>
                             <td><asp:CheckBox ID="AccBranchChartOfAccountsChk" runat="server" Text="Branch Chart of Accounts" /> </td>
                        <td>
                          <tr>
                            <td><asp:CheckBox ID="AccReportChk" runat="server" Text="Accounts Report" /></td>                            
                        </tr>
                        </table>
                    </div>

                </asp:Panel>
            </div>
        </div>    




   <%-- <table style="width: 600px">
        <tr>
            <td style="width: 100">User Name</td>
            <td style="width: 300">
                          <asp:DropDownList ID="DropDownListUserName" runat="server" Class="Select2" DataTextField="LogName" DataValueField="UserId" Width="300px"></asp:DropDownList> 
                          </td>
            <td style="width: 100"> <asp:Button ID="showBtn" CssClass ="btn btn-primary"   runat="server" Text="Show" OnClick="showBtn_Click" /> </td>
            <td style="width: 100"> <asp:Button ID="saveBtn" runat="server" CssClass ="btn btn-primary" Text="Save" OnClick="saveBtn_Click" /> </td>
        </tr>
    </table>--%>
          
            </div>
</asp:Content>

