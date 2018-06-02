<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoMonitor.aspx.cs" Inherits="Manage.Web.DecisionSupport.VideoMonitor" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .vmtd
        {
            width:384px;
            height:256px;
        }
    </style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">    
    <table cellspacing=0 cellpadding=0 style="width:100%;">
        <tr>
            <td valign=top colspan="2" width="768px" height="512px">
                <iframe src="MonitorMap.aspx" frameborder=0 width=100% height=100%></iframe>
            </td>
            <td width="768px" height="512px" colspan="2">
                <iframe src="VideoGroup16.aspx" frameborder=0 width=100% height=100%></iframe></td>
        </tr>
        <tr>
            <td class="vmtd">
                <iframe frameborder=0 width=100% height=100% src="VideoGroup.aspx"></iframe></td>
            <td class="vmtd">
                <iframe frameborder=0 width=100% height=100% src="VideoGroup.aspx"></iframe></td>
            <td class="vmtd">
                <iframe frameborder=0 width=100% height=100% src="VideoGroup.aspx"></iframe></td>
            <td class="vmtd">
                <iframe frameborder=0 width=100% height=100% src="VideoGroup.aspx"></iframe></td>
        </tr>
    </table>
    
    </form>
</body>
</html>
