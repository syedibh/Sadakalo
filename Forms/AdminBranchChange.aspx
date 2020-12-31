<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AdminBranchChange.aspx.cs" Inherits="Forms_AdminBranchChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--    <table class="nav-justified" style="width:1000px ">--%>

    <h2>Branch Change</h2>
    <table style="width: 600px">
        <tr>
            <td colspan ="3" style="text-align :center">
                <asp:Label ID="lblMsg" runat="server" Text="" style="font-weight: 700; color: #3366FF"></asp:Label>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
        </tr>



          <tr>
            <td>Available Branch</td>
            <td>:</td>
            <td>
                 <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                 </asp:DropDownList>	
                
            </td>
        </tr>
     
        <tr>
                  <td colspan ="3" style="text-align:center">
                      &nbsp;</td>
        </tr>
     
        <tr>
                  <td colspan ="3" style="text-align:center">
                      &nbsp;</td>
        </tr>
     
        <tr>
                  <td colspan ="3" style="text-align:center">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Autopostback="true"  />
            </td>
        </tr>
       
    </table>
</asp:Content>

