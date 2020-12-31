<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/HomeMaster.master" AutoEventWireup="true" CodeFile="AdminPasswordChange.aspx.cs" Inherits="Forms_AdminPasswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
      <div class="row">
            <div class="col-sm-8 col-sm-offset-2">
                <p class="label-info" style="text-align: center; font-weight: bold; font-size: 20px; height: 30px; color: white; margin-top: 0.2rem">Password Change Window </p>
            </div>
            <div class="col-sm-2">
                <p style="margin-top: 0.5rem">
                    <asp:Label ID="lblMsg" runat="server" Text="" CssClass="label label-info" Style="font-size: 1.5rem; color: red;"></asp:Label>
                </p>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-6 col-sm-offset-3">
                <table>
                    <tr>
                        <td>LoginName :</td>
                        <td>
                            <asp:TextBox ID="txtLogName" runat="server" CssClass="form-control" placeholder="User Login Name" Width="260px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Old Password :</td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="form-control" Type="password"  placeholder="Old Password" Width="260px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>New Password :</td>
                        <td>
                            <asp:TextBox ID="txtNewPassword1" runat="server" CssClass="form-control" Type="password"  placeholder="New Password" Width="260px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Confirm Password :</td>
                        <td>
                            <asp:TextBox ID="txtNewPassword2" runat="server" CssClass="form-control" Type="password" placeholder="Confirm Password" Width="260px"></asp:TextBox></td>
                    </tr>

                </table>

            </div>
        </div>
        <br />
           <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Save Change" Style="margin-left:38%" OnClick="Button1_Click" />
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />

        </div>



   
</asp:Content>

