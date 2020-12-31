<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="frmListOfItem.aspx.cs" Inherits="frmListOfItem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .btn {
            width: 8.5rem !important;
            font-size: 1.5rem !important;
        }

        .btn1 {
            width: 8.5rem !important;
            font-size: 1.5rem !important;
        }

        .table-space {
            margin-left: 18rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div class="container-fluid">
            <div id="item">
             <asp:Panel ID="pnlItem" runat="server" align="center">
                 <div class="row">
                     
                <div class="col-sm-6 col-sm-offset-3">
                    <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white;margin-top:0.2rem">Product List Window </p>
                </div>
                <div class="col-sm-3">
                     <p style="margin-top:0.5rem">  <asp:Label ID="lblMesseage" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red"></asp:Label></p>
                 </div>
                </div>
                <div class="row">
                    <div class="col-sm-12" id="Inputform">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Item Id :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtItemId" PlaceHolder="Item Id" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Item Name :</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtItemName" PlaceHolder="Item Name" CssClass="form-control"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Supplier Name :</td>
                                            <td>
                                                <asp:DropDownList runat="server" ID="ddlSupplier" Class="Select2" Style="width: 260px">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnPopup" runat="server" CssClass="btn-new btn-default" Text="+" /></td>
                                        </tr>
                                       

                                    </table>
                                </td>
                                <td>
                                    <table>
                                         <tr>
                                            <td>Item Category :</td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" Class="Select2" Style="width: 260px">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnPopupCategory" runat="server" CssClass="btn-new btn-default" Text="+" /></td>

                                        </tr>
                                        <tr>
                                            <td>Item Unit :</td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnit" runat="server" Class="Select2" Style="width: 260px">
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnPopupUnit" runat="server" CssClass="btn-new btn-default" Text="+" /></td>
                                        </tr>
                                        <tr>
                                            <td>Status Type :</td>
                                            <td>
                                                <asp:DropDownList ID="ddlItemType" runat="server" Class="Select2" Style="width: 260px">
                                                </asp:DropDownList></td>

                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td>Purchase Price :</td>
                                            <td>
                                                <asp:TextBox ID="txtPurchasePrice" runat="server" PlaceHolder="Purchase Price" CssClass="form-control text-align-r"></asp:TextBox></td>

                                        </tr>
                                        <tr>
                                            <td>Sales Price :</td>
                                            <td>
                                                <asp:TextBox ID="txtSalesPrice" runat="server" PlaceHolder="Sales Price" CssClass="form-control text-align-r"></asp:TextBox></td>

                                        </tr>
                                         <tr>
                                            <td>Sales Vat % :</td>
                                            <td>
                                                <asp:TextBox ID="txtVatPer" runat="server" PlaceHolder="Vat %" CssClass="form-control text-align-r"></asp:TextBox></td>

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
                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-primary" Text="Add New" OnClick="btnAddNew_Click"  />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click"/>
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-info" Text="View" OnClick="btnView_Click" />
                </div>
               </div>
            <br />

            <asp:Panel ID="pnlSearch" runat="server" CssClass="panel-Search">
                <table>
                    <tr>
                        <td>Search By  :</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSearch" Class="Select2" Style="width: 260px">
                                <asp:ListItem Value="ItemDescription" Text="Item Name"></asp:ListItem>
                                <asp:ListItem Value="ItemId" Text="Item Id"></asp:ListItem>
                                <asp:ListItem Value="S.SupplierName" Text="Supplier Name"></asp:ListItem>
                                <asp:ListItem Value="U.UnitName" Text="Unit Id"></asp:ListItem>
                                <asp:ListItem Value="C.CategoryName" Text="Category Name"></asp:ListItem>
                                <asp:ListItem Value="IT.SatatusName" Text="Status Name"></asp:ListItem>
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
                <asp:GridView ID="gdvListOfItem" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="ItemId,SupplierId,UnitId,CategoryId,StatusId" EmptyDataText="No Data Found ...."
                    AllowPaging="True" AutoGenerateColumns="false" PageSize="8" OnPageIndexChanging="gdvListOfItem_PageIndexChanging"
                    OnRowCancelingEdit="gdvListOfItem_RowCancelingEdit" OnRowDataBound="gdvListOfItem_RowDataBound"
                    OnRowDeleting="gdvListOfItem_RowDeleting" OnRowEditing="gdvListOfItem_RowEditing"
                    OnRowUpdating="gdvListOfItem_RowUpdating" OnSelectedIndexChanging="gdvListOfItem_SelectedIndexChanging">
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
                            <EditItemTemplate>
                                <asp:TextBox ID="txtItemName" runat="server" Style="text-align: left !important" Text='<%#Bind("ItemDescription") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier Name">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlSupplierS" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSupplierE" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Name" Visible="true">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlUnitS" runat="server" Enabled="false" >
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlUnitE" runat="server" >
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" Visible="true">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCategoryS" runat="server" Enabled="false" >
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCategoryE" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Item Staus" Visible="true">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlStatusS" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatusE" runat="server">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Purchase Price"  Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPurchasePrice" runat="server" Text='<%# Bind("PurchasePrice")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPurchasePrice" runat="server"  Text='<%#Bind("PurchasePrice") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Price" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesPrice" runat="server"  Text='<%# Bind("SalesPrice")%>' style="text-align:right"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSalesPrice" runat="server"  Text='<%#Bind("SalesPrice") %>' CssClass="text-align-r"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Sales Vat% " Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesVatPer" runat="server"  Text='<%# Bind("SalesVatPer")%>' style="text-align:right"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSalesVatPer" runat="server"  Text='<%#Bind("SalesVatPer") %>' CssClass="text-align-r"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Closing Stock" Visible="true">
                            <ItemTemplate>
                             <asp:Label ID="lblStock" runat="server" Style="text-align: left !important" Text='<%# Bind("Stock")%>'></asp:Label>
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
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this?');" autopostback="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>
            </asp:Panel>
           </asp:Panel>
            </div>
      
            <div id="popupSupplier">
                <ajaxToolkit:ModalPopupExtender ID="modalSupplier" runat="server" PopupControlID="pnlSupplier" TargetControlID="btnPopup"
                    CancelControlID="btnClosePopup" BackgroundCssClass="Background">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlSupplier" runat="server" Class="Popup" Style="background-color: white; display: none; width: 125rem; height: 60rem;" align="center">
                   <asp:Label ID="lblMesseageSupplier" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                    <div class="col-sm-6 col-sm-offset-3">
                        <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Supplier List </p>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12" id="InputformSupplier">
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
                            <asp:Button ID="btnAddNewSupplier" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAddNewSupplier_Click" />
                            <asp:Button ID="btnClearSupplier" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClearSupplier_Click" />
                            <asp:Button ID="btnSaveSupplier" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveSupplier_Click" />

                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="Panel1" runat="server" CssClass="panel-Search">
                        <table>
                            <tr>
                                <td>Search By  :</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSearchSupplier" CssClass="form-control" Style="width: 260px">
                                        <asp:ListItem Value="SupplierName" Text="Supplier Name"></asp:ListItem>
                                        <asp:ListItem Value="SupplierId" Text="Supplier Id"></asp:ListItem>
                                        <asp:ListItem Value="SupplierAddress" Text="Supplier Address"></asp:ListItem>
                                        <asp:ListItem Value="MobileNo" Text="Mobile No"></asp:ListItem>
                                        <asp:ListItem Value="Email" Text="Email Address"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSearchSupplier" PlaceHolder="Input Search Value" CssClass="form-control"></asp:TextBox></td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearchSupplier" Text="Search" Style="margin-top: 0.2rem" CssClass="btn btn-warning" OnClick="btnSearchSupplier_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel2" runat="server" CssClass="View_Panel">
                        <asp:GridView ID="gdvListOfSupplier" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="SupplierId" EmptyDataText="No Data Found ...."
                            OnRowUpdating="gdvListOfSupplier_RowUpdating" OnRowEditing="gdvListOfSupplier_RowEditing" OnRowCancelingEdit="gdvListOfSupplier_RowCancelingEdit"
                            OnRowDeleting="gdvListOfSupplier_RowDeleting" OnPageIndexChanging="gdvListOfSupplier_PageIndexChanging" AllowPaging="True" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Id" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierId" runat="server" Text='<%# Bind("SupplierId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSupplierName" runat="server" Text='<%# Bind("SupplierName")%>'></asp:Label>
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
            </div>

            <div id="popupCategory">
                <ajaxToolkit:ModalPopupExtender ID="modalCategory" runat="server" PopupControlID="pnlCategory" TargetControlID="btnPopupCategory"
                    CancelControlID="btnCloseCategory" BackgroundCssClass="Background">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlCategory" runat="server" Class="Popup" Style="background-color: white; display: none; width: 125rem; height: 60rem;" align="center">
                     <asp:Label ID="lblMesseageCategory" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                    <div class="col-sm-6 col-sm-offset-3">
                        <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Product Category List </p>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12" id="InputformCategory">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>Category Id :</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtCategoryId" PlaceHolder="Category Id" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Category Name :</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtCategoryName" PlaceHolder="Category Name" CssClass="form-control"></asp:TextBox></td>
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
                            <asp:Button ID="btnAddNewCategory" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAddNewCategory_Click" />
                            <asp:Button ID="btnClearCategory" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClearCategory_Click" />
                            <asp:Button ID="btnSaveCategory" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveCategory_Click" />
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="Panel4" runat="server" CssClass="panel-Search">
                        <table>
                            <tr>
                                <td>Search By  :</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCategorySearch" CssClass="form-control" Style="width: 260px">
                                        <asp:ListItem Value="CategoryName" Text="Category Name"></asp:ListItem>
                                        <asp:ListItem Value="CategoryId" Text="Category Id"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSearchCategory" PlaceHolder="Input Search Value" CssClass="form-control"></asp:TextBox></td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearchCategory" Text="Search" Style="margin-top: 0.2rem" CssClass="btn btn-warning" OnClick="btnSearchCategory_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel5" runat="server" CssClass="View_Panel">
                        <asp:GridView ID="gdvCategory" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="CategoryId" EmptyDataText="No Data Found ...."
                            OnRowUpdating="gdvCategory_RowUpdating" OnRowEditing="gdvCategory_RowEditing" OnRowCancelingEdit="gdvCategory_RowCancelingEdit"
                            OnRowDeleting="gdvCategory_RowDeleting" OnPageIndexChanging="gdvCategory_PageIndexChanging"
                            AllowPaging="True" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category Id" Visible="true" HeaderStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryId" runat="server" Text='<%# Bind("CategoryId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCategoryNameE" runat="server" Text='<%#Bind("CategoryName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Select" HeaderStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelectCategory" runat="server" Text="Select" OnClick="btnSelectCategory_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="50">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Button ID="btnCloseCategory" runat="server" CssClass="btn btn-default" Text="Close" />
                </asp:Panel>
            </div>

            <div id="popupUnit">
                <ajaxToolkit:ModalPopupExtender ID="modalUnit" runat="server" PopupControlID="pnlUnit" TargetControlID="btnPopupUnit"
                    CancelControlID="btnCloseUnit" BackgroundCssClass="Background">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlUnit" runat="server" Class="Popup" Style="background-color: white; display: none; width: 125rem; height: 60rem;" align="center">
                     <asp:Label ID="lblMesseageUnit" runat="server" Text="" Style="font-size: 2rem; color: red"></asp:Label>
                    <div class="col-sm-6 col-sm-offset-3">
                        <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white">Unit List </p>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12" id="InputformUnit">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>Unit Id :</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtUnitId" PlaceHolder="Unit Id" CssClass="form-control"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>Unit Name :</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtUnitName" PlaceHolder="Unit Name" CssClass="form-control"></asp:TextBox></td>
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
                            <asp:Button ID="btnAddNewUnit" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAddNewUnit_Click" />
                            <asp:Button ID="btnClearUnit" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClearUnit_Click" />
                            <asp:Button ID="btnSaveUnit" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveUnit_Click" />

                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="Panel7" runat="server" CssClass="panel-Search">
                        <table>
                            <tr>
                                <td>Search By  :</td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlSearchUnit" CssClass="form-control" Style="width: 260px">
                                        <asp:ListItem Value="unitName" Text="Unit Name"></asp:ListItem>
                                        <asp:ListItem Value="unitId" Text="Unit Id"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSearchUnit" PlaceHolder="Input Search Value" CssClass="form-control"></asp:TextBox></td>
                                <td class="td-space2"></td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearchUnit" Text="Search" Style="margin-top: 0.2rem" CssClass="btn btn-warning" OnClick="btnSearchUnit_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                    <asp:Panel ID="Panel8" runat="server" CssClass="View_Panel">
                        <asp:GridView ID="gdvUnit" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="UnitId" EmptyDataText="No Data Found ...."
                            OnRowUpdating="gdvUnit_RowUpdating" OnRowEditing="gdvUnit_RowEditing" OnRowCancelingEdit="gdvUnit_RowCancelingEdit"
                            OnRowDeleting="gdvUnit_RowDeleting" OnPageIndexChanging="gdvUnit_PageIndexChanging"
                            AllowPaging="True" AutoGenerateColumns="false" PageSize="10">
                            <Columns>
                                <asp:TemplateField HeaderText="Serial No" Visible="true" HeaderStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Bind("SL_NO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Id" Visible="true" HeaderStyle-Width="70">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitId" runat="server" Text='<%# Bind("UnitId")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UnitName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUnitNameE" runat="server" Text='<%#Bind("UnitName") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="120">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="update" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="cancel" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Select" HeaderStyle-Width="40">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelectUnit" runat="server" Text="Select" OnClick="btnSelectUnit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="40">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="delete" OnClientClick="return confirm('Are you sure you want to delete this?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pgr" />
                        </asp:GridView>
                    </asp:Panel>

                    <asp:Button ID="btnCloseUnit" runat="server" CssClass="btn btn-default" Text="Close" />
                </asp:Panel>
            </div>
     
       </div>
         
         
          </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger ControlID="btnView" />
            </Triggers>
        </asp:UpdatePanel>
       
    <asp:Button ID="mydefaultbtn" runat="server" Text="Button" Style="visibility: hidden;" />
</asp:Content>

