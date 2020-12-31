<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Login</title>
<link href="Content/bootstrap-login.css" rel="stylesheet" />
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="vendor/select2/select2.min.js"></script>
    <link href="vendor/select2/select2.min.css" rel="stylesheet" />


    <script type="text/javascript">
        $(function () {
            $('.Select2').select2({
                placeholder: "Select a Subject",
                allowClear: true
            });
        });
    </script>

<style type="text/css">
	.login-form {
		width: 38.5rem;
		margin: 3rem auto;
	}
    .login-form form {        
    	margin-bottom: 1.5rem;
        background: #f7f7f7;
        box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        padding: 3rem;
    }
    .login-form h2 {
        margin: 0 0 1.5rem;
    }
    .form-control, .login-btn {
        min-height: 3.8rem;
        border-radius: 2px;
    }
    .input-group-addon .fa {
        font-size: 1.8rem;
    }
    .login-btn {
        font-size: 1.5rem;
        font-weight: bold;
    }
	.social-btn .btn {
		border: none;
        margin: 1rem 3rem 0;
        opacity: 1;
	}
    .social-btn .btn:hover {
        opacity: 0.9;
    }
	.social-btn .btn-primary {
        background: #507cc0;
    }
	.social-btn .btn-info {
		background: #64ccf1;
	}
	.social-btn .btn-danger {
		background: #df4930;
	}
    .or-seperator {
        margin-top: 2rem;
        text-align: center;
        border-top: 1px solid #ccc;
    }
    .or-seperator i {
        padding: 0 1rem;
        background: #f7f7f7;
        position: relative;
        top: -1.1rem;
        z-index: 1;
    } 
    .container-fluid
    {
        padding-left:0px !important;
        padding-right:0px !important;
        color:white !important;
        text-align:Center ! important;
        font-size: 2rem;
        font-weight: bold;

    }  
    .footer{
        padding-left:0px !important;
        padding-right:0px !important;
        color:white !important;
        text-align:right ! important;
        font-size: 1.5rem !important;
    }

    .btn{
        width: 10rem !important;
       margin-top: 1rem !important;
    }
    .select2-container--default .select2-selection--single
    {
        height:3.6rem!important;
        border-radius:2px !important;
        margin-top: -0.3rem !important;
    }
</style>
</head>
<body>
    <div id="header">
        <div class="container-fluid">
            <div class="panel-footer" style="background-color:royalblue; height:4rem ! important">
                <p> Welcome To <asp:Label ID="lblwelcomeMsg" runat="server"></asp:Label> </p>
            </div>
        </div>

    </div>
<div class="login-form">
    <h3><asp:Label ID="lblMsg" CssClass="label label-danger"  runat="server"></asp:Label></h3> 
    <form id="form1" runat="server">
        <h2 class="text-center">Login Window</h2>   
        <%--<img src="Images/sadakalologo.png" style="height: 16rem;width: 33rem;" />--%>
        <asp:Image ID="logo" runat="server" style="height: 16rem;width: 33rem;" />
        <br />
        <br />
        <div class="form-group">
        	<div class="input-group">
                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                 <asp:DropDownList ID="ddlBranch" CssClass="Select2" runat="server" style="width:28.6rem!important;">
                 </asp:DropDownList>	
                
            </div>
        </div>
        <div class="form-group">
        	<div class="input-group">
                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                 <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control"  placeholder="User Name"></asp:TextBox>			
            </div>
        </div>
		<div class="form-group">
            <div class="input-group">
                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                 <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"  placeholder="Password" TextMode="Password"></asp:TextBox>			
            </div>
        </div>   
             
        <div class="form-group">
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLogin_Click" />
            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="btnClear_Click" Style="margin-left:0.7rem"  />
            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger" Text="Reset" Style="margin-left:1rem"/>
        </div>
    </form>
</div>
    <div id="footer">
        <div class="container-fluid">
            <div class="footer" style="background-color:royalblue; margin-top:0rem !important; height:3rem ! important">
                <p>Copyright ©  <asp:Label ID="lblFooterMsg" runat="server"></asp:Label></p>
            </div>
        </div>

    </div>
</body>
</html>
