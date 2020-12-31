<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmSales.aspx.cs" Inherits="Forms_frmSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <div class="container-fluid">
        <div class="col-sm-6 col-sm-offset-3">
        <p class="label-info" style="text-align:center;font-weight:bold;font-size:20px; height:30px;color:white;margin-top:0.2rem">Sales Entry Window </p>
        </div>
         <div class="col-sm-3">
         <p style="margin-top:0.5rem">  <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
       </p>
       </div>

    <br />
   <div class="col-sm-12" id="InputformItem">
      <table>
          <tr>
              <td>
                  <table >
                      <tr>
                          <td>Bill No :</td>
                          <td><asp:TextBox runat="server" ID="txtBillNo" PlaceHolder="Bill No" CssClass="form-control"></asp:TextBox></td>
                      </tr>
                     
                        <tr>
                          <td>Item Id :</td>
                          <td><asp:DropDownList runat="server" ID="ddlItemId"  Class="Select2" Style="width: 260px" OnSelectedIndexChanged="ddlItemId_SelectedIndexChanged" AutoPostBack="true">
                              </asp:DropDownList>
                          </td>
                      </tr>
                       <tr>
                          <td>Item Name :</td>
                          <td><asp:DropDownList runat="server" ID="ddlItem"  Class="Select2" Style="width: 260px" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                              </asp:DropDownList>
                          </td>
                           <td><asp:Button ID="btnItemMaseter" runat="server" Text="+" CssClass="btn-new btn-default" /></td>
                      </tr>
                      <tr>
                          <td></td>
                          <td>
                          </td>
                      </tr>
                    
                      
                  </table>
              </td>
               <td>
                    <table>
                         <tr>
                          <td>Customer Name :</td>
                          <td><asp:DropDownList runat="server" ID="ddlCustomer" CssClass="Select2" Style="width: 260px">
                              </asp:DropDownList></td>
                          <td><asp:Button ID="btnPopupCustomer" runat="server" Text="+" CssClass="btn-new btn-default" /></td>
                      </tr>
                        <tr>
                          <td>Sales Price :</td>
                          <td><asp:TextBox runat="server" ID="txtSalesPrice" PlaceHolder="Sales Price" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                       <tr>
                          <td>Available Qnty :</td>
                          <td><asp:TextBox runat="server" ID="txtCurrentBalance" PlaceHolder="Available Quantity" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                       <tr>
                          <td>Quantity:</td>
                          <td><asp:TextBox runat="server" ID="txtQuantity" PlaceHolder="Quantity" CssClass="form-control text-align-r" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox></td>
                      </tr>
                   </table>

              </td>
              <td>
                   <table>
                        <tr>
                          <td>Amount :</td>
                          <td><asp:TextBox runat="server" ID="txtAmount" PlaceHolder="Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                        <tr>
                          <td>Sales Vat :</td>
                          <td><asp:TextBox runat="server" ID="txtSalesVatPer" PlaceHolder="Sales Vat" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                        <tr>
                          <td>Total Amount :</td>
                          <td><asp:TextBox runat="server" ID="txtTotalAmount" PlaceHolder="Total Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                       <tr>
                          <td>Discount Amount :</td>
                          <td><asp:TextBox runat="server" ID="txtDiscountAmount" PlaceHolder="Discount Amount" CssClass="form-control text-align-r"></asp:TextBox></td>
                      </tr>
                   </table>
              </td>
              <td>
                  <table>
                       <tr>
                          <td><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass=" btn btn-default" OnClick="btnAdd_Click"/>

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
                <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" Style="margin-left: 10rem" OnClick="btnAddNew_Click" />
                <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="Clear_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-warning" OnClick="btnView_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                <asp:Button ID="btnUnposted" runat="server" Text="Unposted" CssClass="btn btn-success" OnClick="btnUnposted_Click" />
                <asp:Button ID="btnMyCollection" runat="server" Text="My Collection" CssClass="btn btn-info" OnClick="btnMyCollection_Click" />             
                <span style="margin-left: 26rem;">Searc By Bill No</span>
                <asp:TextBox ID="txtSearchChallan" runat="server" Style="text-align: center;" PlaceHolder="Bill No" AutoComplete="off"></asp:TextBox>
                <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-info" OnClick="btnFind_Click" />
            </asp:Panel>
        </div>
    </div>
    <asp:Panel ID="pnlSalesGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvItemDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId" EmptyDataText="No Data Found ...."
                        OnRowUpdating="gdvItemDetails_RowUpdating" OnRowEditing="gdvItemDetails_RowEditing" OnRowCancelingEdit="gdvItemDetails_RowCancelingEdit"
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
                                    <asp:TextBox runat="server" ID="txtTotalItem" PlaceHolder="Total Item" CssClass="form-control text-align-r"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>Total Qnty :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalQuantity" PlaceHolder="Total Quantity" CssClass="form-control text-align-r"></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td>Total Vat :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtVat" PlaceHolder="Vat" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>Received By :</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlReceivedBy"  CssClass="form-control text-align-r" OnSelectedIndexChanged="ddlReceivedBy_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Cash</asp:ListItem>
                                        <asp:ListItem Value="1">bKash</asp:ListItem>
                                        <asp:ListItem Value="2">Card</asp:ListItem>
                                        <asp:ListItem Value="3">Cheque</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td id="lbltnote" runat="server" visible="false"> Transaction Note :</td>
                                <td> <asp:TextBox runat="server" ID="txtTransactionNote" PlaceHolder="Transaction Note" CssClass="form-control" Visible="false"></asp:TextBox></td>
                             </tr>

                        </table>
                    </td>
                    <td>
                        <table>
                           
                        </table>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>Total Price :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalPrice" PlaceHolder="Total Price" CssClass="form-control text-align-r"></asp:TextBox>
                                </td>
                            </tr>
                            
                             <tr>
                                  <td>Discount % :</td>
                                 <td>
                                  <asp:TextBox runat="server" ID="txtDiscountPer" PlaceHolder="Discount%" CssClass="form-control text-align-c" Style="width:9rem !important;" OnTextChanged="txtDiscountPer_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                  </td>
                                 <td>
                                  <asp:TextBox runat="server" ID="txtDiscount" PlaceHolder="Amount" CssClass="form-control text-align-r" Style="width:16.5rem !important;margin-left:-16.5rem"> </asp:TextBox>
                                  </td>
                            </tr>
                             <tr>
                                <td>Net Amount :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNetAmount" PlaceHolder="Net Amount" CssClass="form-control text-align-r"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>Paid Amount :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPaidAmount" PlaceHolder="Paid Amount" CssClass="form-control text-align-r"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                     <td>
                   <table>
                       <tr>
                          <td><asp:Button ID="btnVatComission" runat="server" Text=": :" CssClass=" btn btn-default" Style="margin-top:-81%" OnClick="btnVatComission_Click"/></td>
                   </table>
              </td>
                    <td>
                        <table style="margin-left:9rem!important" >
                            <tr>
                                <td Style="font-weight:bold;font-size:2.9rem">
                                    Recevied Amount 
                                </td>
                             </tr>
                            <tr>
                                <td>
                                 <asp:TextBox runat="server" ID="txtReceivedAmount" PlaceHolder="Received From Customer" CssClass="form-control text-align-c" Style="font-weight:bold;font-size:1.6rem;height:4rem !important;background-color: black;color:yellow" ></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                <asp:Button ID="btnMesseage" runat="server" CssClass="btn btn-info" Text="Show Result" style="  margin-top: 0.5rem;  margin-left: 8rem; color: initial;font-weight: bold;" OnClick="btnMesseage_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        </div>
    </div>

    <div id="PopupSupplier">
            <ajaxToolkit:ModalPopupExtender ID="modalCustomer" runat="server" PopupControlID="pnlCustomer" TargetControlID="btnPopupCustomer"
                         CancelControlID="btnClosePopup" BackgroundCssClass="Background">
                       </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlCustomer" runat="server" Class="Popup" Style="background-color:white;  display:none;  width:125rem; height:60rem;" align="center">
                 <asp:Label ID="lblMesseageCustomer" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                <div class="col-sm-6 col-sm-offset-3">
                    <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Customer List </p>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12" id="InputformPopup">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Customer Id :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCustomerId" PlaceHolder="Customer Id" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Customer Name :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCustomerName" PlaceHolder="Customer Name" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Customer Address :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCustomerAddress" PlaceHolder="Customer Address" CssClass="form-control"></asp:TextBox></td>
                                        </tr>

                                    </table>
                                </td>
                                <td class="td-space"></td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Email Address :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtEmailAddress" PlaceHolder="Email Address" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Mobile No:</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtMobileNo" PlaceHolder="Mobile No" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                <td>Web Address :</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtWebAddress" PlaceHolder="Web Address" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3">
                         <asp:Button ID="btnAddNewCustomer" runat="server" Text="Add New" CssClass="btn btn-primary"  OnClick="btnAddNewCustomer_Click" />
                            <asp:Button ID="btnClearCustomer" runat="server" Text="Clear" CssClass="btn btn-danger"  OnClick="btnClearCustomer_Click" />
                            <asp:Button ID="btnSaveCustomer" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveCustomer_Click"/>
                        
                    </div>
                </div>
                <br />
                <asp:Panel ID="pnlSearch" runat="server" CssClass="panel-Search">
                    <table>
                        <tr>
                            <td>Search By  :</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSearch" CssClass="form-control" Style="width: 260px">
                                     <asp:ListItem Value="MobileNo" Text="Mobile No"></asp:ListItem>
                                    <asp:ListItem Value="CustomerName" Text="Customer Name"></asp:ListItem>
                                    <asp:ListItem Value="CustomerId" Text="Customer Id"></asp:ListItem>
                                    <asp:ListItem Value="MailingAddress" Text="Customer Address"></asp:ListItem>
                                    <asp:ListItem Value="Email" Text="Email Address"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td class="td-space2"></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtSearch" PlaceHolder="Input Search Value" CssClass="form-control"></asp:TextBox></td>
                            <td class="td-space2"></td>
                            <td>
                                <asp:Button runat="server" ID="btnSearch" Text="Search" Style="margin-top: 0.2rem" CssClass="btn btn-warning" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnlGridView" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvCustomerProfile" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="CustomerId" EmptyDataText="No Data Found ...."
                        OnRowUpdating="gdvCustomerProfile_RowUpdating" OnRowEditing="gdvCustomerProfile_RowEditing" OnRowCancelingEdit="gdvCustomerProfile_RowCancelingEdit"
                        OnRowDeleting="gdvCustomerProfile_RowDeleting" OnPageIndexChanging="gdvCustomerProfile_PageIndexChanging" AllowPaging="True"
                        AutoGenerateColumns="false" PageSize="6">
                        <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server"  Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Id" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierId" runat="server" Text='<%# Bind("CustomerId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" CssClass="text-align-l"  Text='<%# Bind("CustomerName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCustomerNameE" runat="server" Text='<%#Bind("CustomerName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Address" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("MailingAddress")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAddressE" runat="server" Text='<%#Bind("MailingAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Bind("MobileNo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMobileNoE" runat="server" Text='<%#Bind("MobileNo") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Address" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmailAddressE" runat="server" Text='<%#Bind("EmailAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Web Address" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWebAddress" runat="server" Text='<%# Bind("WebAddress")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtWebAddressE" runat="server" Text='<%#Bind("WebAddress") %>'></asp:TextBox>
                                    </EditItemTemplate>
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
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelectCustomer" runat="server" Text="Select" OnClick="btnSelectCustomer_Click" />
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

                <asp:Button ID="btnClosePopup" runat="server" CssClass="btn btn-default" Text="Close" />
            </asp:Panel>
            </div>

                <div id="pnlMesseage" class="panel-info">
                    <ajaxToolkit:ModalPopupExtender ID="modalMesseage" runat="server" PopupControlID="MesseagePopup" TargetControlID="btnNoDisplay"
                        CancelControlID="btnHidePopup" BackgroundCssClass="Background">
                    </ajaxToolkit:ModalPopupExtender>
                    <div id="MesseagePopup" style="background-color: #08b9d6; border-radius: 3rem">
                        <p style="font-size: 4rem; color: #070a61; font-weight: bold; text-align: center">
                            <asp:Label ID="lblUserContent" runat="server" Text="" Style="font-size: 3rem; color: blue;"></asp:Label>
                            <br />
                            <asp:Label ID="lblShowResul" runat="server" Text="" Style="font-size: 4rem; color: maroon;"></asp:Label>
                            TK.
                        </p>
                        <br />
                        <asp:Button ID="btnHidePopup" runat="server" CssClass="btn btn-warning" Text="Hide" Style="margin-left: 40%; width: 12rem; font-size: 3rem" />
                        <asp:Button ID="btnNoDisplay" runat="server" CssClass="btn btn-default" Text="" Style="display: none" />
                    </div>
                </div>

               <div id="popupItemMaster">
                <ajaxToolkit:ModalPopupExtender ID="modalItemMaster" runat="server" PopupControlID="pnlItemMaster" TargetControlID="btnItemMaseter"
                    CancelControlID="btnClosePopupItem" BackgroundCssClass="Background">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlItemMaster" runat="server" Class="Popup" Style="background-color: white; display: none; width: 125rem; height: 60rem;" align="center">
                   <asp:Label ID="lblMesseageItemMaster" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                    <div class="col-sm-6 col-sm-offset-3">
                        <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Item Master List </p>
                    </div>
                    <br />
                    <br />
                    <asp:Panel ID="Panel2" runat="server" CssClass="panel-Search">
                        <table>
                            <tr>
                                <td>Search By  :</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSearchItemMaster" CssClass="form-control" Style="width: 260px">
                                        <asp:ListItem Value="ITEMDESCRIPTION" Text="Item Name"></asp:ListItem>
                                        <asp:ListItem Value="ItemId" Text="Item Id"></asp:ListItem>
                                        <asp:ListItem Value="S.SupplierName" Text="Supplier Name"></asp:ListItem>
                                        <asp:ListItem Value="U.UnitName" Text="Unit Id"></asp:ListItem>
                                        <asp:ListItem Value="C.CategoryName" Text="Category Name"></asp:ListItem>
                                        <asp:ListItem Value="IT.SatatusName" Text="Status Name"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSearchItem" PlaceHolder="Input Search Value" CssClass="form-control"></asp:TextBox></td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearchItem" Text="Search" Style="margin-top: 0.2rem" CssClass="btn btn-warning" OnClick="btnSearchItem_Click"  />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                     <br />
                     <br />
                    <asp:Panel ID="Panel3" runat="server" CssClass="View_Panel">
                       <asp:GridView ID="gdvItemMaster" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId,SupplierId,UnitId,CategoryId,StatusId" EmptyDataText="No Data Found ...."
                    AllowPaging="True" AutoGenerateColumns="false" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="Serial No" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSerialNo" runat="server" Style="text-align: left !important" Text='<%# Bind("SL_NO")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Id" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblItemId" runat="server" Style="text-align: left !important" Text='<%# Bind("ItemId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate >
                                <asp:Label ID="lblItemName" runat="server" style="text-align:left" Text='<%# Bind("ItemDescription")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Name">
                           <ItemTemplate >
                                <asp:Label ID="lblSupplierName" runat="server" style="text-align:left" Text='<%# Bind("SUPPLIERNAME")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Name" Visible="true">
                            <ItemTemplate >
                                <asp:Label ID="lblItemUnit" runat="server" style="text-align:left" Text='<%# Bind("UNITNAME")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" Visible="true">
                           <ItemTemplate >
                                <asp:Label ID="lblItemCategory" runat="server" style="text-align:left" Text='<%# Bind("CATEGORYNAME")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Item Staus" Visible="true">
                           <ItemTemplate >
                                <asp:Label ID="lblItemStatus" runat="server" style="text-align:left" Text='<%# Bind("StatusName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Price" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesPrice" runat="server"  Text='<%# Bind("SalesPrice")%>' style="text-align:right"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Main Branch" Visible="true">
                            <ItemTemplate>
                             <asp:Label ID="lblMain" runat="server" Style="text-align: left !important" Text='<%# Bind("MainStore")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mirpur Branch" Visible="true">
                            <ItemTemplate>
                             <asp:Label ID="lblMirpur" runat="server" Style="text-align: left !important" Text='<%# Bind("Mirpur")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mohammadpur Branch" Visible="true">
                            <ItemTemplate>
                             <asp:Label ID="lblMohammadpur" runat="server" Style="text-align: left !important" Text='<%# Bind("Mohammadpur")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>
                    </asp:Panel>

                    <asp:Button ID="btnClosePopupItem" runat="server" CssClass="btn btn-default" Text="Close" />
                </asp:Panel>
            </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

