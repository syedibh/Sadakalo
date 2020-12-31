<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AccContraVoucher.aspx.cs" Inherits="Forms_AccContraVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Contra Voucher Entry Window </p>
            </div>
            <div class="col-sm-2">
                <p style="margin-top: 0.5rem">
                    <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                </p>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-10 col-lg-offset-2">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>Voucher No :</td>
                                    <td>
                                        <asp:TextBox ID="txtVoucherNo" runat="server" placeholder="Voucher No" CssClass="form-control" Width="260px" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Voucher Date :</td>
                                    <td>
                                        <asp:TextBox ID="txtVoucherDate" runat="server" placeholder="Select Date" CssClass="TransactionDate" Style="width: 260px; height: 30px; margin-top: 0.4rem" autocomplete="off"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Debit (Multiple) :</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlDebitAccount" CssClass="Select2" Style="width: 260px">
                                        </asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>Credit (Multiple) :</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCreditAccount" CssClass="Select2" Style="width: 260px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Amount :</td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Debit Amount" CssClass="form-control" Width="260px" autocomplete="off"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Nots Of Record :</td>
                                    <td>
                                        <asp:TextBox ID="txtRecordNots" runat="server" placeholder="Record Nots" CssClass="form-control" Width="260px" autocomplete="off"></asp:TextBox></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass=" btn btn-default" OnClick="btnAdd_Click" />

                                    </td>
                                </tr>
                            </table>
                        </td>

                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-10 col-lg-offset-1">
                <asp:Panel ID="Panel1" runat="server" Style="background-color: silver; margin-top: 2rem">
                    <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" Style="margin-left: 11rem"  OnClick="btnAddNew_Click"/>
                    <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="btn btn-danger" />
                    <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-warning" />
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default"  OnClick="btnSave_Click"/>
                    <span style="margin-left: 6rem;">Searc By Voucher No</span>
                    <asp:TextBox ID="txtSearchVoucher" runat="server" Style="text-align: center;" PlaceHolder="Voucher No" AutoComplete="off"></asp:TextBox>
                    <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-info" OnClick="btnFind_Click" />
                </asp:Panel>
            </div>
        </div>
        <div class="row">
        <div class="col-lg-10 col-lg-offset-1">
         <asp:Panel ID="pnlSalesGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvVoucherDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" 
                        DataKeyNames="AccTransactionDetails_Id" EmptyDataText="No Data Found ...." OnRowDeleting="gdvVoucherDetails_RowDeleting"
                         AllowPaging="True"
                        AutoGenerateColumns="false" PageSize="6">
                        <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server"  Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Debit Accounts" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDebitAccounts" runat="server" Text='<%# Bind("DEBIT_ACCOUNT_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Credit Accounts" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreditAccount" runat="server" Text='<%# Bind("CREDIT_ACCOUNT_NAME")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nots Of Record" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordNots" runat="server" CssClass="text-align-l"  Text='<%# Bind("TransactionNote")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" CssClass="text-align-l"  Text='<%# Bind("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </asp:Panel>
            </div>
            </div>
        <hr />
        <div class="row">
            <div class="col-lg-12">
                <table style="margin-left: 40%">
                    <tr>
                        <td>Total Credit Amount :</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNetAmount" PlaceHolder="Total Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

