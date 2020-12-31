<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AdminNewUser.aspx.cs" Inherits="Forms_AdminNewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container">
        <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">New User Window </p>
            </div>
            <div class="col-sm-2">
                <p style="margin-top: 0.5rem">
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                </p>
            </div>
        </div>
        <br />
    <div class="row">
        <div class="col-lg-8 col-lg-offset-3">
            <table>
                <tr>
                    <td>Email Id :</td>
                    <td><asp:TextBox ID="txtEmailId" runat="server" placeholder="Email Address" CssClass="form-control" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Full Name :</td>
                    <td><asp:TextBox ID="txtFullname" runat="server" placeholder="User Full Name" CssClass="form-control" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Mobile No :</td>
                    <td><asp:TextBox ID="txtMobileNo" runat="server" placeholder="Mobile No" CssClass="form-control" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Designation :</td>
                    <td> <asp:TextBox ID="txtDesignation" runat="server" placeholder="Designation"   CssClass="form-control" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Joining Date :</td>
                    <td><asp:TextBox ID="txtJoiningDate" runat="server" placeholder="Select Date" CssClass="TransactionDate" Style="width: 260px; height:30px; margin-top:0.4rem" autocomplete="off"></asp:TextBox></td>
                </tr>
            </table>
        </div>
       </div>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Style="margin-left:40%"  OnClick="btnSave_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Save" CssClass="btn btn-danger"  OnClick="btnClear_Click" />
       <%-- <div class="col-md-2" style="text-align: right">
            <h5>Email Id.</h5>
        </div>
        <div class="col-md-1" style="text-align: center">
            <h5>:</h5>
        </div>
        <div class="col-md-5" style="text-align: left">
            <asp:TextBox ID="txtEmailId" runat="server" Width="200px"></asp:TextBox>
        </div>
        <div class="col-md-4" style="text-align: center">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2" style="text-align: right">
            <h5>Full Name</h5>
        </div>
        <div class="col-md-1" style="text-align: center">
            <h5>:</h5>
        </div>
        <div class="col-md-5" style="text-align: left">
            <asp:TextBox ID="txtFullname" runat="server" Width="200px"></asp:TextBox>
        </div>
        <div class="col-md-4" style="text-align: center">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"style="text-align: right">
            <h5>Mobile No</h5>
        </div>
        <div class="col-md-1" style="text-align: center">
            <h5>:</h5>
        </div>
        <div class="col-md-5" style="text-align: left">
            <asp:TextBox ID="txtMobileNo" runat="server" Width="200px"></asp:TextBox>
        </div>
        <div class="col-md-4" style="text-align: center">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2" style="text-align: right">
            <h5>Designation</h5>
        </div>
        <div class="col-md-1" style="text-align: center">
            <h5>:</h5>
        </div>
        <div class="col-md-5" style="text-align: left">
            <asp:TextBox ID="txtDesignation" runat="server" Width="200px"></asp:TextBox>
        </div>
        <div class="col-md-4" style="text-align: center">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2" style=" text-align: right">
            <h5>Joining Date</h5>
        </div>
        <div class="col-md-1" style=" text-align: center">
            <h5>:</h5>
        </div>
        <div class="col-md-5" style=" text-align: left">
            <asp:TextBox ID="txtJoiningDate" runat="server" Width="200px"></asp:TextBox>
        </div>
        <div class="col-md-4" style="text-align: center">
        </div>
    </div>--%>
    <%--<div class="row">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary"  OnClick="btnSave_Click" />
    </div>--%>
</div>
</asp:Content>

