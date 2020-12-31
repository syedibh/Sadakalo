<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AccBranchChartOfAccounts.aspx.cs" Inherits="Forms_AccBranchChartOfAccounts" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Branch Chart Of Accounts </p>
            </div>
            <div class="col-sm-2">
                <p style="margin-top: 0.5rem">
                    <asp:Label ID="LblMsg" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                </p>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-6 col-lg-offset-3">
                <asp:Panel ID="Panel1" runat="server" Style="background-color: #f99999; border-radius:1rem; height:4rem;">
                    <span style="margin-left: 2rem;">Select Branch Name :</span> 
            <asp:DropDownList ID="DropDownListBranchNameList" runat="server" Class="Select2" DataTextField="BrName" DataValueField="BrId"  style="width:300px;text-align:center !important;" >
            </asp:DropDownList>
                    <asp:Button ID="btnShowStatus" runat="server" Text="Show" CssClass="btn btn-primary" Style="height:4rem" OnClick="btnShowStatus_Click" />

                </asp:Panel>
            </div>
        </div>
        <br />
            <div class="row">
            <div class="col-sm-12 >
                <asp:Panel ID="Panel2" runat="server" Style="background-color: #f99999; border-radius:1rem; height:4rem;">
                    <span style="margin-left: 2rem;">Select con head Name :</span> 
                     <asp:DropDownList ID="DropDownListConHead" runat="server" Class="Select2" DataTextField="Con_head" DataValueField="Con_head"  style="width:300px;text-align:center !important;" >
                     </asp:DropDownList>
                    <asp:Button ID="btnCon_head" runat="server" Text="Show" CssClass="btn btn-primary" Style="height:4rem" OnClick="btnCon_head_Click" />
                     <span style="margin-left: 2rem;">Select Accounts Name :</span> 
                     <asp:DropDownList ID="DropDownListAccountsHead" runat="server" Class="Select2" DataTextField="Accountsname" DataValueField="id"  style="width:300px;text-align:center !important;" >
                     </asp:DropDownList>
                    <asp:Button ID="btnFindByAccountName" runat="server" Text="Show" CssClass="btn btn-primary" Style="height:4rem" OnClick="btnFindByAccountName_Click" />
                </asp:Panel>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="col-sm-6 ">
                <p class="label-warning" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Master Chart Of Accounts</p>
              
                 <asp:GridView ID="GridViewAvailable" runat="server" CssClass="mGrid" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridViewAvailable_RowCommand" OnRowDataBound="GridViewAvailable_RowDataBound" OnSelectedIndexChanged="GridViewAvailable_SelectedIndexChanged" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="AccountsId" />
                    <asp:BoundField DataField="AccountsName" HeaderText="Accounts Head" >
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Con_Head" HeaderText="Con_Head">
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkAdd" runat="server" CommandName="cnAdd">Add</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            </div>
             <div class="col-sm-6 ">
                <p class="label-primary" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Branch Chart Of Accounts </p>
               
                <asp:GridView ID="GridViewPermissionGiven" runat="server" CssClass="mGrid" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"  OnRowCommand="GridViewPermissionGiven_RowCommand" OnRowDataBound="GridViewPermissionGiven_RowDataBound" OnSelectedIndexChanged="GridViewPermissionGiven_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" />
                    <asp:BoundField DataField="BrId" HeaderText="BrId" Visible="False" />
                    <asp:BoundField DataField="AccountsName" HeaderText="AccountsName" >
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="con_head" HeaderText="con_head">
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="cnDelete">Remove</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-6">
           
        </div>
        <div class="col-6">
           
        </div>
    </div>
</div>
</asp:Content>


