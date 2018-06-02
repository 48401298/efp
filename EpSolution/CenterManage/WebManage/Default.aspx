<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Manage.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>智慧玉洋</title>
    <%--<title>智慧渔业仓储管理系统V1.0</title>--%>	
    <%--<title>智慧渔业近海环境监测系统V1.0</title>	--%>
    <%--<title>智慧渔业质量追溯系统V1.0</title>--%>
    <link rel="stylesheet" type="text/css" href="easyui/themes/bootstrap/easyui.css">
	<link rel="stylesheet" type="text/css" href="easyui/themes/icon.css">
	<script type="text/javascript" src="JS/jquery.min-1.11.1.js"></script>
	<script type="text/javascript" src="easyui/jquery.easyui.min.js"></script>
    <script src="JS/app/default.js" type="text/javascript"></script>
    <script src="JS/CommonActions.js" type="text/javascript"></script>
    <script language=javascript>
        $(function () {
            //openUrl("", "main.aspx", "tab", "主页", false);
        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 73px;
            height: 41px;
        }
    </style>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div data-options="region:'north',border:false" style="height:54px;padding:0px;background-image: url('images/head.jpg');overflow: hidden;">
	        <table width=100% style="height:54px;">
                                <tr>
                                    <td style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: xx-large; color: #FFFFFF; font-weight: bold;" 
                                                    valign="middle">&nbsp; 智慧玉洋</td>  
    	                            <td align=right valign=bottom style="padding: 5px">
    	                                <table>
	                                        <tr>
                                                <td><asp:Label ID="lblUserName" runat="server" Text="当前用户：" ForeColor="White"></asp:Label></td>
                                                <td align=center width="20px"><span style="color: #FFFFFF">|</span></td>
	                                            <td style="color: #FFFFFF; text-decoration:none"><a style="color: #FFFFFF; text-decoration:none" href="#" onclick="modifyPWD();return false;">
                                                    <span style="color: #FFFFFF; text-decoration:none">密码修改</span></a></td>
	                                            <td align=center width="20px"><span style="color: #FFFFFF">|</span></td>
	                                            <td><a href="Login.aspx" style="color: #FFFFFF; text-decoration:none"><span style="color: #FFFFFF; text-decoration:none">退 出</span></a></td>
	                                        </tr>
	                                    </table>
    	                            </td>
    	                        </tr>
    	                    </table>    
	    </div>
	    <div region="west" id="body" split="true" title="菜单" style="width: 200px;">
	         <iframe id="frmMenu" src="LeftMenu.aspx" width=100% height=98% frameborder=0></iframe>
	    </div>
	    <div region="center" id="MenuTitle" title="" style="background: #fafafa;width:100%">
	        <div id="divTab" class="easyui-tabs" fit="true" style="border:0px">
               
            </div>
	    </div>
        <div id="appWindow1" class="easyui-window" closed="true" modal="true" title="My Window" iconCls="icon-save" style="width:300px;height:500px;padding:5px;background: #fafafa;">
		    <iframe id="appWindow1_Frm" width=100% height="100%" frameborder="0"></iframe>
	    </div>
        <iframe id="ifrmLogin" style="display:none" src="none" ></iframe>
        <input id="HiLogin" type="hidden" />
    </form>
    
    <script language="javascript" type="text/javascript">

        $(function () {
            
        }); 
        //修改密码
        function modifyPWD() {
            openAppWindow1('修改密码', "User/ModifyPwd.aspx", '400', '300');
        }
    </script>
</body>
</html>
