<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Manage.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/loginstyle.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.min-1.11.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper login" id="container">
      <div class="inner">
        <div class="login-form">
	        <h1 class="login-title">智慧玉洋</h1>
		    <h2 class="login-title">系统登录</h2>
		    <ul class="login-con">
		      <li><label><span class="names">用户名：</span>
              <asp:TextBox ID="txtUser" runat="server" placeholder="用户名" CssClass="text"></asp:TextBox>
              </label></li>
		      <li><label><span class="names">密 码：</span>
              <asp:TextBox ID="txtPassWord" CssClass="text" placeholder="密码" 
                    runat="server" TextMode="Password"></asp:TextBox>
              </label></li>		  
		      <li><label><span class="names">验证码：</span>
              <input name="txtCheckCode" class="code" placeholder="验证码" runat="server" 
                                type="text" id="txtCheckCode" /></label><img id="imgVerify" width="100" height="30" class="yzm" alt="看不清？点击更换" onclick="this.src=this.src+'?'" src="DrawValidatePic.aspx?" /></li>
		      <li><span class="names"></span>
              <asp:Button ID="btLogin" runat="server" CssClass="btns" Text="登录" OnClientClick="return isLogin();"
                    onclick="btLogin_Click" />
              </li>
		    </ul>
	    </div>
      </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        function isLogin() {
            if ($("#txtUser").val() == "" || $("#txtPassWord").val() == "") {
                alert("用户名或密码不能为空");
                return false;
            }

            if ($("#txtCheckCode").val() == "") {
                alert("验证码不能为空");
                return false;
            }
        }
    </script>
</body>
</html>
