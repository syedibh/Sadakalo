<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="ReportListCrystal.aspx.cs" Inherits="Forms_ReportListCrystal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="Button1" runat="server" Text="Button" />
     <asp:Panel ID="pnlGridView" runat="server" CssClass="View_Panel">
                <asp:GridView ID="gdvListOfItem" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" DataKeyNames="Id,Reportname" EmptyDataText="No Data Found ...."
                    AllowPaging="True" AutoGenerateColumns="False" PageSize="10" OnPageIndexChanging="gdvListOfItem_PageIndexChanging" OnRowUpdating="gdvListOfItem_RowUpdating" >
                    <Columns>
                        <asp:TemplateField HeaderText="Report Id" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblItemId" runat="server" Style="text-align: left !important" Text='<%# Bind("Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Report Name" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Style="text-align: left" Text='<%# Bind("reportName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate >
                                <asp:Button ID="btnEdit" runat="server" CommandName="update" Text="View" />
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="pgr" />
                </asp:GridView>
            </asp:Panel>
</asp:Content>

