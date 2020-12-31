<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmSalesReturn.aspx.cs" Inherits="Forms_frmSalesReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="col-sm-6 col-sm-offset-3">
                    <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Sales Return Entry Window </p>
                </div>
                <div class="col-sm-3">
                    <p style="margin-top: 0.5rem">
                        <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                    </p>
                </div>

                <br />
                <div class="col-sm-12" id="InputformItem">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>Bill No :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtBillNo" PlaceHolder="Bill No" CssClass="form-control" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Branch Name :</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlBranch" Class="Select2" Style="width: 260px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Sales Bill No :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSalesBillNo" PlaceHolder="Sales Bill No" CssClass="form-control" AutoComplete="off"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="btnFindSales" runat="server" Text="Find" CssClass="btn-new btn-default" OnClick="btnFindSales_Click" /></td>
                                </tr>
                                <tr>
                                    <td>Item Name :</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlItem" Class="Select2" Style="width: 260px" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>


                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>Customer Name :</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCustomer" CssClass="Select2" Style="width: 260px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td>Return Price :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSalesReturnPrice" PlaceHolder="Sales Price" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Remaining Qnty :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtRemainingQnty" PlaceHolder="Sold Quantity" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Quantity:</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtQuantity" PlaceHolder="Quantity" CssClass="form-control text-align-r" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox></td>
                                </tr>
                            </table>

                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>Amount :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAmount" PlaceHolder="Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Sales Vat :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSalesVatPer" PlaceHolder="Sales Vat" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Total Amount :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTotalAmount" PlaceHolder="Total Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td>Discount Amount :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDiscountAmount" PlaceHolder="Discount Amount" CssClass="form-control text-align-r" AutoComplete="off"></asp:TextBox></td>
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
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Panel ID="Panel1" runat="server" Style="background-color: silver; margin-top: 2rem">
                            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" Style="margin-left: 13rem" OnClick="btnAddNew_Click" />
                            <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="Clear_Click" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-warning" OnClick="btnView_Click" />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                            <span style="margin-left: 35rem;">Searc By Bill No</span>
                            <asp:TextBox ID="txtSearchChallan" runat="server" Style="text-align: center;" PlaceHolder="Bill No" AutoComplete="off"></asp:TextBox>
                            <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-info" OnClick="btnFind_Click" />
                        </asp:Panel>
                    </div>
                </div>

             <div class="row">
                <div class="col-sm-12">
                <asp:Panel ID="pnlSalesGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvItemDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId" EmptyDataText="No Data Found ...."
                        AllowPaging="True" OnRowDeleting="gdvItemDetails_RowDeleting"
                        AutoGenerateColumns="false" PageSize="6">
                        <Columns>
                            <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                <ItemTemplate>
                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Bind("SL_NO")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Id" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemId" runat="server" Text='<%# Bind("ItemId")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDescription" runat="server" CssClass="text-align-l" Text='<%# Bind("ItemDescription")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qnty" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qnty")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesRate" runat="server" Text='<%# Bind("rate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblSalesVat" runat="server" Text='<%# Bind("Vat")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# Bind("Discount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Bind("TotalAmount")%>'></asp:Label>
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
                    <div class="col-sm-12">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Total Item :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTotalItem" PlaceHolder="Total Item" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total Qnty :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTotalQuantity" PlaceHolder="Total Quantity" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Total Vat :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtVat" PlaceHolder="Vat" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="td-space">
                                    <table>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Total Price :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTotalPrice" PlaceHolder="Total Price" CssClass="form-control text-align-r" AutoComplete="off" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Discount Amount :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtDiscount" PlaceHolder="Amount" CssClass="form-control text-align-r" AutoComplete="off"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Net Amount :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtNetAmount" PlaceHolder="Net Amount" CssClass="form-control text-align-r"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnVatComission" runat="server" Text=": :" CssClass=" btn btn-default" Style="margin-top: -81%" OnClick="btnVatComission_Click" /></td>
                                    </table>
                                </td>
                               
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

