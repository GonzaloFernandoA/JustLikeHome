<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="ACHE.Mvc.admin.admin_default" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="img/favicon.png" />

    <title>Admin :: Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap-reset.css" rel="stylesheet" />
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/style-responsive.css" rel="stylesheet" />

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 tooltipss and media queries -->
    <!--[if lt IE 9]>
    <script src="js/html5shiv.js"></script>
    <script src="js/respond.min.js"></script>
    <![endif]-->
</head>

<body class="login-body">
    <div class="container">
        <form id="Form1" runat="server" class="form-signin">
            <h2 class="form-signin-heading">acceso al sistema</h2>
            <div class="login-wrap">
                <asp:Panel runat="server" ID="pnlLoginError" Visible="false" class="alert alert-block alert-danger fade in">
                    <button data-dismiss="alert" class="close close-sm" type="button">
                        <i class="fa fa-times"></i>
                    </button>
                    <strong>Error</strong> Usuario y/o contraseña incorrecta
                </asp:Panel>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtUsuario" placeholder="Usuario" autofocus></asp:TextBox>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPwd" placeholder="Contraseña" TextMode="Password" autofocus></asp:TextBox>
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-lg btn-login btn-block" Text="Ingresar" OnClick="Login" />
            </div>
        </form>
    </div>
    <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>

