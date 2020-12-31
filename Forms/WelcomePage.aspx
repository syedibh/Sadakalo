<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="WelcomePage.aspx.cs" Inherits="WelcomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1 {
            font-size: 3rem;
        }

        .style2 {
            text-align: center;
            vertical-align: middle; 
        }
         .navbar-inverse
          {
        }

        td{
            padding:20px!important;
        }
        .btn-primary {
            width: 37rem !important;
            height: 15rem!important;
            font-size: 2.5rem !important;
            font-weight: bold !important;
            margin-top: 2rem !important;
            border-radius: 2rem !important;
            padding-top: 5rem !important;
        }

        .btn-info {
            width: 37rem !important;
            height: 15rem!important;
            font-size: 2.5rem !important;
            font-weight: bold !important;
            margin-top: 2rem !important;
            border-radius: 2rem !important;
            padding-top: 5rem !important;
        }

        .btn-warning {
            width: 37rem !important;
            height: 15rem!important;
            font-size: 2.5rem !important;
            font-weight: bold !important;
            margin-top: 2rem !important;
            border-radius: 2rem !important;
            padding-top: 5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
     <div style="font-size: 1.1rem;text-align: center; vertical-align: middle; margin: auto;" id='bday' runat="server">
         </div>

        <div class="style2">

            <span class="style1">
                    <span class="style1">
                        <asp:Label ID="lblWelComeMsg" runat="server"></asp:Label>
                    </span>
            </span>
            <div id="stockInfo" runat="server">
            <asp:Label ID="lblStok" runat="server" Text="Total Sales Quantity 50" CssClass=" btn btn-primary"></asp:Label>
            <asp:Label ID="lblBooking" runat="server" CssClass="btn btn-info" Text="Total Purchase Qnty 50"></asp:Label>
            <asp:Label ID="lblStokAfterBooking" runat="server" Text="Total Service Quantity 50" CssClass=" btn btn-warning"></asp:Label>
            </div>
            <div  id="divUserStockDetails" runat="server">
            <asp:Label ID="lblBookQtyMonth" runat="server" CssClass="btn btn-primary" Text="Total Service Quantity 50" ></asp:Label>
            <asp:Label ID="lblSaleQty" runat="server"  CssClass="btn btn-info" Text="Total Service Quantity 50" ></asp:Label>
            <asp:Label ID="lblSaleQtyMonth" runat="server"  CssClass=" btn btn-warning" Text="Total Service Quantity 50"></asp:Label>
            </div>
        </div>
      </div>
     </div>
    </div>
       </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

