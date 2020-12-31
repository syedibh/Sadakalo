<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmPurchaseReturn.aspx.cs" Inherits="Forms_frmPurchaseReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
   <div class="container-fluid">
         <div class="col-sm-6 col-sm-offset-3">
        <p class="label-info" style="text-align:center;font-weight:bold;font-size:20px; height:30px;color:white;margin-top:0.2rem">Return To Supplier </p>
         </div>
       <div class="COL-SM-3">
         <p style="margin-top:0.5rem">  <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
       </p>
             </div>

        <br />
   <div class="col-sm-12" id="Inputform">
      <table>
          <tr>
              <td>
                  <table>
                      <tr>
                          <td>Challan No :</td>
                          <td><asp:TextBox runat="server" ID="txtChallanNo" PlaceHolder="Challan No" CssClass="form-control"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td>Purchase Bill No :</td>
                          <td><asp:TextBox runat="server" ID="txtSupplierChallanNo" PlaceHolder="Supplier Challan No" CssClass="form-control" AutoComplete="off"></asp:TextBox></td>
                           <td><asp:Button ID="btnFindPurchase" runat="server" Text="Find" CssClass="btn-new btn-default" OnClick="btnFindPurchase_Click" /></td>
                      </tr>
                      <tr>
                          <td>Supplier Name :</td>
                          <td><asp:DropDownList runat="server" ID="ddlSupplier"  Class="Select2" Style="width: 260px">
                              </asp:DropDownList></td>
                      </tr>
                  </table>
              </td>
               <td>
                    <table>
                          <tr>
                          <td>Item Barcode :</td>
                          <td><asp:TextBox runat="server" ID="txtItemBarcode" PlaceHolder="Item Barcode" CssClass="form-control"></asp:TextBox></td>
                          </tr>
                         <tr>
                          <td>Item Name :</td>
                          <td><asp:DropDownList runat="server" ID="ddlItem"  CssClass="Select2"  Style="width: 260px" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                          </asp:DropDownList>
                          </td>
                          </tr>
                        <tr>
                          <td>Purchase Price :</td>
                          <td><asp:TextBox runat="server" ID="txtPurchasePrice"  PlaceHolder="Purchase Price" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                      
                   </table>
              </td>
              <td >
                   <table >
                        <tr>
                          <td>Remaining Qnty:</td>
                          <td><asp:TextBox runat="server" ID="txtRemainingQnty" PlaceHolder="Remaining Qnty" CssClass="form-control text-align-r" Enabled="false" ></asp:TextBox></td>
                        </tr>
                        <tr>
                          <td>Quantity:</td>
                          <td><asp:TextBox runat="server" ID="txtQuantity" PlaceHolder="Quantity" CssClass="form-control text-align-r" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" AutoComplete="off"></asp:TextBox></td>
                        </tr>
                        <tr>
                          <td>Amount :</td>
                          <td><asp:TextBox runat="server" ID="txtAmount" PlaceHolder="Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox></td>
                      </tr>
                   </table>
              </td>
              <td>
                   <table>
                       <tr>
                          <td><asp:Button ID="btnAdd" runat="server" Text="Add" CssClass=" btn btn-default" OnClick="btnAdd_Click"/></td>
                   </table>
              </td>
          </tr>
      </table>
   </div>
       <div class="row">
       <div class="col-sm-12" >
       <asp:Panel ID="btnfooter" runat="server" Style="background-color:silver;margin-top:2rem">
       <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" Style="margin-left:13rem" OnClick="btnAddNew_Click" />
       <asp:Button ID="Clear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="Clear_Click" />
       <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" />
       <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-warning"  OnClick="btnView_Click"/>
       <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary"  OnClick="btnPrint_Click"/>
       <span style="margin-left: 29rem;">Searc By Challan No</span>
       <asp:TextBox ID="txtSearchChallan" runat="server" Style="text-align: center;" PlaceHolder="Challan No"></asp:TextBox>
       <asp:Button ID="btnFind" runat="server" Text="Find" CssClass="btn btn-info" OnClick="btnFind_Click" />
       </asp:Panel>
        </div>
        </div>
    <%--<hr />--%>
      <asp:Panel ID="pnlItemGrid" runat="server" CssClass="View_Panel">
                    <asp:GridView ID="gdvItemDetails" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr"
                         DataKeyNames="ItemId" EmptyDataText="No Data Found ...." AllowPaging="True" OnRowDeleting="gdvItemDetails_RowDeleting"
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
    <div class="container-fluid">
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
                    <td class="td-space2"></td>
                    <td>
                        <table>
                            <tr>
                                <td>Total Price :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalPrice" PlaceHolder="Total Price" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>Vat :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtVat" PlaceHolder="Vat" CssClass="form-control text-align-r" AutoComplete="off"  AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>Comission :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtComission" PlaceHolder="Comission" CssClass="form-control text-align-r" AutoComplete="off"  AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td>Net Amount :</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNetAmount" PlaceHolder="Net Amount" CssClass="form-control text-align-r" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                   <table>
                       <tr>
                          <td><asp:Button ID="btnVatComission" runat="server" Text=": :" CssClass=" btn btn-default" /></td>
                   </table>
              </td>
                </tr>
            </table>
        </div>
        </div>
    </div>

    <%--  <div id="PopupSupplier">
            <ajaxToolkit:ModalPopupExtender ID="modalSupplier" runat="server" PopupControlID="pnlSupplier" TargetControlID="btnPopupSupplier"
                         CancelControlID="btnClosePopup" BackgroundCssClass="Background">
                       </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlSupplier" runat="server" Class="Popup" Style="background-color:white;  display:none;  width:125rem; height:60rem;" align="center">
                 <asp:Label ID="lblMesseageSupplier" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                <div class="col-sm-6 col-sm-offset-3">
                    <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Supplier List </p>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12" id="InputformPopup">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Supplier Id :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtSupplierId" PlaceHolder="Supplier Id" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Supplier Name :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtSupplierName" PlaceHolder="Supplier Name" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Supplier Address :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtSupplierAddress" PlaceHolder="Supplier Address" CssClass="form-control"></asp:TextBox></td>
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
                         <asp:Button ID="btnAddNewSupplier" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAddNewSupplier_Click"  />
                            <asp:Button ID="btnClearSupplier" runat="server" Text="Clear" CssClass="btn btn-danger"  OnClick="btnClearSupplier_Click" />
                            <asp:Button ID="btnSaveSupplier" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveSupplier_Click" />
                        
                    </div>
                </div>
                <br />
                <asp:Panel ID="pnlSearch" runat="server" CssClass="panel-Search">
                    <table>
                        <tr>
                            <td>Search By  :</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlSearch" CssClass="form-control" Style="width: 260px">
                                    <asp:ListItem Value="SupplierName" Text="Supplier Name"></asp:ListItem>
                                    <asp:ListItem Value="SupplierId" Text="Supplier Id"></asp:ListItem>
                                    <asp:ListItem Value="SupplierAddress" Text="Supplier Address"></asp:ListItem>
                                    <asp:ListItem Value="MobileNo" Text="Mobile No"></asp:ListItem>
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
                    <asp:GridView ID="gdvListOfSupplier" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="SupplierId" EmptyDataText="No Data Found ...."
                        OnRowUpdating="gdvListOfSupplier_RowUpdating" OnRowEditing="gdvListOfSupplier_RowEditing" OnRowCancelingEdit="gdvListOfSupplier_RowCancelingEdit"
                        OnRowDeleting="gdvListOfSupplier_RowDeleting" OnPageIndexChanging="gdvListOfSupplier_PageIndexChanging" AllowPaging="True"
                        AutoGenerateColumns="false" PageSize="10">
                        <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-CssClass="text-align-l">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server"  Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Id" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierId" runat="server" Text='<%# Bind("SupplierId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierName" runat="server" CssClass="text-align-l"  Text='<%# Bind("SupplierName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSupplierNameE" runat="server" Text='<%#Bind("SupplierName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Address" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("SupplierAddress")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAddressE" runat="server" Text='<%#Bind("SupplierAddress") %>'></asp:TextBox>
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
                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("Email")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmailAddressE" runat="server" Text='<%#Bind("Email") %>'></asp:TextBox>
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
                                        <asp:Button ID="btnSelcetSupplier" runat="server" Text="Select" OnClick="btnSelcetSupplier_Click" />
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
            </div>--%>
       </div>
 <asp:Button ID="mydefaultbtn" runat="server" Text="Button" Style="visibility: hidden;" />
         </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

