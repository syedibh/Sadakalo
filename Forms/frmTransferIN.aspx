<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmTransferIN.aspx.cs" Inherits="Forms_frmTransferIN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                <div class="col-lg-12 ">
                    <div class="col-lg-8">
                        <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Stock IN </p>
                    </div>
                     <div class="col-lg-4">
                          <p style="margin-top: 0.5rem">
                        <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                    </p>
                    </div>
                    
                </div>
               </div>
            </div>
              <div class="container-fluid">
                  <div class="col-sm-8">
                   <p class="label-primary" style="text-align: center; font-weight: bold; font-size: 12px; height: 20px; color:white; margin-top: 0rem">Bill No </p>
                   <asp:Panel ID="pnlItemGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvItemSummery" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="RefNo,BrId,ToBrId" EmptyDataText="No Data Found ...."
                        OnPageIndexChanging="gdvItemSummery_PageIndexChanging" AllowPaging="True" OnRowUpdating="gdvItemSummery_RowUpdating"
                        AutoGenerateColumns="false" PageSize="5">
                        <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server"  Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RefNo" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrefNo" runat="server" Text='<%# Bind("RefNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From Branch" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblFromBr" runat="server" CssClass="text-align-l"  Text='<%# Bind("BrName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="To Branch" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblToBr" runat="server" Text='<%# Bind("ToBrName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Voucher Type" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVtype" runat="server" Text='<%# Bind("VType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <asp:TemplateField HeaderText="Voucher No" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVno" runat="server" Text='<%# Bind("Vno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contra Ref" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContraRef" runat="server" Text='<%# Bind("ContraRef")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total SalesValue" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalValue" runat="server" Text='<%# Bind("TotalBill")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Posteddate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Import">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Import" CommandName="update" OnClientClick="return confirm('Are you sure you want Import this Voucher?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Details">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDetails" runat="server" Text="Details"  OnClick="btnDetails_Click"  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                               
                            </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </asp:Panel>
                      <hr />
                   <asp:Panel ID="Panel1" runat="server" CssClass="View_Panel">
                   <p class="label-primary" style="text-align: center; font-weight: bold; font-size: 12px; height: 20px; color:white; margin-top: 0rem">Bill Details </p>
                    <asp:GridView ID="gdvItemDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId" EmptyDataText="No Data Found ...."
                        OnPageIndexChanging="gdvItemDetails_PageIndexChanging" AllowPaging="True"
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
                                <asp:TemplateField HeaderText="Qnty" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qnty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%# Bind("rate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        <PagerStyle CssClass="pgr" />
                    </asp:GridView>
                </asp:Panel>
              </div>
                  
                  <div class="col-sm-4">
                      <asp:Panel ID="pnlFindOption" runat="server" CssClass="View_Panel" Style="background-color:cornflowerblue">
                           <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Find Option </p>
                            <table>
                                    <tr>
                                        <td>From Date :</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromDate" PlaceHolder="From Date" Class="TransactionDate"  Style="width: 260px; height:30px;" autocomplete="off"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>To Date :</td>
                                            <td>
                                            <asp:TextBox runat="server" ID="txtToDate" PlaceHolder="To Date" CssClass="TransactionDate" Style="width: 260px; height:30px; margin-top:0.4rem" autocomplete="off"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                            <td>Find Option :</td>
                                            <td><asp:DropDownList runat="server" ID="ddlFindOption"  CssClass="Select2"  Style="width: 260px" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                                 <asp:ListItem Value="0">-- Select Find Option --</asp:ListItem>
                                                 <asp:ListItem Value="1"> Find By Voucher No </asp:ListItem>
                                                 <asp:ListItem Value="2">Date wise None received bill</asp:ListItem>
                                                 <asp:ListItem Value="3">Date wise all bill</asp:ListItem>
                                            </asp:DropDownList></td>
                                     </tr>
                                <tr id="trfindvoucer" runat="server">
                                        <td>Voucher No :</td>
                                            <td>
                                            <asp:TextBox runat="server" ID="txtVoucherNo" PlaceHolder="Voucher No" CssClass="form-control"></asp:TextBox>
                                            </td>
                                    </tr>
                                    <tr>
                                     <td>
                                     </td>
                                     <td>
                                         <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" style="margin-top: 1rem;" OnClick="btnClear_Click"/>
                                         <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-primary" style="margin-top: 1rem;" OnClick="btnFind_Click"/>
                                     </td>
                                 </tr>
                           </table>
                     </asp:Panel>
                  </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

