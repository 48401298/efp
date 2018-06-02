<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppLogin.aspx.cs" Inherits="Manage.Web.AppLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <meta name="description" content="">
    <meta name="author" content="">
    <title>智慧渔业移动终端</title>
    <!-- Bootstrap core CSS -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom styles for this template -->
    <link href="css/signin.css" rel="stylesheet">   
    <script src="JS/jquery.min-1.11.1.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server" class="form-signin">
        <h2 class="form-signin-heading" style="text-align: center">智慧渔业移动终端</h2>
            <label for="inputUserName" class="sr-only">用户名</label>
            <asp:TextBox ID="inputUserName" runat="server" class="form-control" placeholder="用户名"></asp:TextBox>
            <label for="inputPassword" class="sr-only">密码</label>
            <asp:TextBox ID="inputPassword" runat="server" class="form-control" 
            placeholder="密码" TextMode="Password"></asp:TextBox>           
            <asp:Button ID="btLogin" runat="server" Text="登录" 
            CssClass="btn btn-lg btn-primary btn-block" OnClientClick="return isLogin();" onclick="btLogin_Click" />
            <button class="btn btn-lg btn-primary btn-block" type="reset">重置</button>        
    </form>
    </div>
    <script language="javascript" type="text/javascript">
        function isLogin() {
            if ($("#inputUserName").val() == "" || $("#inputPassword").val() == "") {
                alert("用户名或密码不能为空");
                return;
            }
        }
    </script>
</body>
</html>
