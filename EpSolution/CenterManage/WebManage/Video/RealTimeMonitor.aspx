<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealTimeMonitor.aspx.cs" Inherits="Manage.Web.Video.RealTimeMonitor" %>

<!doctype html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta http-equiv="Pragma" content="no-cache" />
	<meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
	<meta http-equiv="Expires" content="0" />
    <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
    <script src="../JS/json2.js" type="text/javascript"></script>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div>
        <div id="divPlugin" class="plugin"></div>
    </div>
    
    <asp:HiddenField ID="hiCamera" runat="server" />    
    </form>    
    <script src="codebase/webVideoCtrl.js"></script>
    <script src="RealTimeMonitor.js" type="text/javascript"></script>
    </body>
</html>
