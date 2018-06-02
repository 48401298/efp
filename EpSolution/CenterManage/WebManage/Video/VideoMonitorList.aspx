<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoMonitorList.aspx.cs" Inherits="Manage.Web.WH.Video.VideoMonitorList" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>视频实时监控列表</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server"> 
        <asp:Literal ID="PositionList" runat="server"></asp:Literal>
        
        <asp:HiddenField ID="hiPostionList" runat="server" />
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>    
    <script language=javascript>
        //打开实时监控窗口
        function openRTMonitor(id, title) {
            //window.location.href = "RealTimeMonitor.aspx?id=" + id;
            openAppWindow1(title, "RealTimeMonitor.aspx?id=" +id, '620', '450');
            return false;
        }
    </script>
</body>
</html>
