<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmTransferOUT.aspx.cs" Inherits="Forms_frmTransferOUT" %>

<script runat="server">
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                <div class="col-lg-12 ">
                <div class="col-lg-8  col-lg-offset-2">
                    <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Stock Out </p>
                   
                 </div>
                <div class="COL-lg-2">
                     <p style="margin-top: 0.5rem">
                        <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                    </p>
                </div>
                </div>
               </div>

                <br />
                <div class="col-sm-12" id="Inputform">
                    <table class="table-space">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Challan No :</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtChallanNo" PlaceHolder="Challan No" CssClass="form-control"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Showroom Name :</td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlShowroom" Class="Select2" Style="width: 260px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>Item Name :</td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlItem" CssClass="Select2" Style="width: 260px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>Item Id :</td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlItemId" CssClass="Select2" Style="width: 260px" OnSelectedIndexChanged="ddlItemId_SelectedIndexChanged"  AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>


                                </table>
                            </td>
                            <td class="td-space">
                            </td>
                            <td>
                                <table>
                                      <tr>
                                        <td>Available Qnty :</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtCurrentBalance" PlaceHolder="Balance" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td>Rate :</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtPurchasePrice" PlaceHolder="Purchase Price" CssClass="form-control text-align-r"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Quantity:</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtQuantity" PlaceHolder="Quantity" CssClass="form-control text-align-r"  AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Amount :</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtAmount" PlaceHolder="Amount" CssClass="form-control text-align-r"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass=" btn btn-default" OnClick="btnAdd_Click" /></td>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Panel ID="btnfooter" runat="server" Style="background-color: silver; margin-top: 2rem">
                            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" Style="margin-left: 16rem" OnClick="btnAddNew_Click"  />
                            <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="btn btn-danger" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click"  />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-warning"  />
                            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary"  />
                            <asp:Button ID="btnUnposted" runat="server" Text="Unposted" CssClass="btn btn-success"  />
                            <asp:Button ID="btnDone" runat="server" Text="Done" CssClass="btn btn-info"  />
                            <span style="margin-left: 26rem;">Searc By Challan No</span>
                            <asp:TextBox ID="txtSearchChallan" runat="server" Style="text-align: center;" PlaceHolder="Challan No"></asp:TextBox>
                            <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-info"  OnClick="btnFind_Click" />
                        </asp:Panel>
                    </div>
                </div>

             <asp:Panel ID="pnlItemGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvItemDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId" EmptyDataText="No Data Found ...."
                        OnRowEditing="gdvItemDetails_RowEditing" OnRowCancelingEdit="gdvItemDetails_RowCancelingEdit" OnRowUpdating="gdvItemDetails_RowUpdating"
                        OnRowDeleting="gdvItemDetails_RowDeleting" OnPageIndexChanging="gdvItemDetails_PageIndexChanging" AllowPaging="True"
                        AutoGenerateColumns="false" PageSize="6">
                        <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server"  Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Id" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemId" runat="server" Text='<%# Bind("ItemId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Description" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescription" runat="server" CssClass="text-align-l"  Text='<%# Bind("ItemDescription")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantityH" runat="server" Text='<%# Bind("Qnty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qnty" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qnty")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuantityE" runat="server" Text='<%#Bind("Qnty") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%# Bind("rate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contra Rate" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContraRate" runat="server" Text='<%# Bind("ContraRate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="cancel" />
                                    </EditItemTemplate>
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
    <hr />
    <div class="container-fluid">
        <div class="row">
        <div class="col-sm-12">
            <table>
                <tr>
                    <td class="td-space"></td>
                    <td>
                        <table>
                            <tr>
                                <td>Total Item :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalItem" PlaceHolder="Total Item" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>Total Qnty :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalQuantity" PlaceHolder="Total Quantity" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td ></td>
                    <td>
                        <table>
                            <tr>
                                <td>Total Amount :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalPrice" PlaceHolder="Total Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
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

