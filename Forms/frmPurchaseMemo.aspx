<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPurchaseMemo.aspx.cs" Inherits="Forms_frmPurchaseMemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style1
        {
            height: 21px;
            width: 700px;
        }
        .auto-style2
        {
            width: 700px;
        }
        
        .style3
        {
            height: 22px;
            width: 225px;
        }
        .auto-style3 {
            height: 22px;
        }
        </style>
</head>
<body>
  <form id="form2" runat="server">
    <table align="center">
        <tr>
            <td align="center" class="auto-style1">
                <table width="700" align="center">
                    <tr>
                        <td rowspan="4" width="100" valign="top">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="64px" 
                                ImageAlign="TextTop" ImageUrl="~/Images/sadakalologo.png" 
                                Width="87px" />
                        </td>
                        <td align="center">
                <asp:Label ID="LblCompanyName" runat="server" Font-Bold="True" 
                    Font-Size="X-Large"></asp:Label>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                <asp:Label ID="LblBrName" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                <asp:Label ID="LblBrAddress" runat="server"></asp:Label>
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" >
               
                <hr />
                </td>
        </tr>
        <tr>
            <td align="center" class="auto-style2">
                <asp:Label ID="LblMemoName" runat="server" Text="Received Memo" 
                    style="font-weight: 700; text-decoration: underline" Font-Size="Larger"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <table width="700">
                    <tr>
                        <td align="left" width="100">
                            Bill No</td>
                        <td align="center" width="10">
                            <b>:</b></td>
                        <td class="style3" width="125">
                <asp:Label ID="LblBillno" runat="server" ForeColor="Black"></asp:Label>
                        </td>
                        <td align="center" width="100">
                <asp:Label ID="LblPosted" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                        <td align="right" class="style3" width="125">
                Date</td>
                        <td width="10">
                            :</td>
                        <td class="style3" width="125">
                <asp:Label ID="LblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="100">
                            Customer Name</td>
                        <td align="center" width="10">
                            <b>:</b></td>
                        <td class="style3" width="125" colspan="2">
                <asp:Label ID="LblCustomerName" runat="server" ForeColor="Black"></asp:Label>
                        </td>
                        <td align="right" class="style3" width="125">
                            Mobile No </td>
                        <td width="10">
                            <b>:</b></td>
                        <td class="style3" width="125">
                <asp:Label ID="LblMobileNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
            <asp:GridView ID="gdvSalesDetails1" runat="server" AutoGenerateColumns="False" ShowFooter="True" 
                    Width="700px" OnRowDataBound="gdvSalesDetails1_RowDataBound" OnSelectedIndexChanged="gdvSalesDetails1_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="ItemId" HeaderText="Id" >
                        <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemDescription" HeaderText="ItemDescription" >
                    <ItemStyle Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Qnty" HeaderText="Qnty" >
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Rate" HeaderText="Rate" 
                        DataFormatString="{0:#,##0.00}" >
                        <ControlStyle Width="100px" />
                        <FooterStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                        <ItemStyle HorizontalAlign="Right" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vat" HeaderText="Vat" DataFormatString="{0:#,##0.00}" >
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Discount" HeaderText="Discount" DataFormatString="{0:#,##0.00}" >
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:#,##0.00}" >
                    <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="#CCCCCC" />
            </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="LblTaka" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">
                <table width="700">
                    <tr>
                        <td align="center" width="250" class="auto-style3">
                            &nbsp;</td>
                        <td align="center" width="150" class="auto-style3">
                            </td>
                        <td class="style3" width="125">
                        </td>
                        <td class="style3" width="125">
                        </td>
                        <td class="style3" width="125">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100">
                <asp:Image ID="Image10" runat="server" Height="1px" ImageUrl="~/Picture/th.jpg" 
                    Width="200px" />
                        </td>
                        <td align="center" width="150">
                <asp:Image ID="Image11" runat="server" Height="1px" ImageUrl="~/Picture/th.jpg" 
                    Width="100px" />
                        </td>
                        <td align="center" width="125">
                <asp:Image ID="Image12" runat="server" Height="1px" ImageUrl="~/Picture/th.jpg" 
                    Width="100px" />
                        </td>
                        <td align="center" width="125">
                <asp:Image ID="Image13" runat="server" Height="1px" ImageUrl="~/Picture/th.jpg" 
                    Width="100px" />
                        </td>
                        <td align="center" width="125">
                <asp:Image ID="Image14" runat="server" Height="1px" ImageUrl="~/Picture/th.jpg" 
                    Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="250">
                            Posted By : <asp:Label ID="LblUserName" runat="server"></asp:Label>
                        </td>
                        <td align="center" width="150">
                            &nbsp;</td>
                        <td align="center" width="125">
                            </td>
                        <td align="center" width="125">
                        </td>
                        <td align="center" width="125">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <p>
                &nbsp;</p>
 
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
 
   </form>
  <%--  <form id="form1" runat="server">
    <div>
    
    </div>
    </form>--%>
</body>
</html>
