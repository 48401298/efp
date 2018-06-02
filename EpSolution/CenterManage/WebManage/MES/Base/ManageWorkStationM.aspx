<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageWorkStationM.aspx.cs" Inherits="Manage.Web.MES.Base.ManageWorkStationM" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工位信息管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">  
        <div data-options="region:'west',split:true" title="生产线" style="width:180px;">
            <ul id="wsTree" class="easyui-tree" data-options=""></ul>
        </div>
		<div data-options="region:'center',title:'工位'">
            <div class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:100px;overflow:hidden;">
                 <iframe id="frmList" width="100%" height="100%" frameborder=0 src="ManageWorkStation.aspx"></iframe>
            </div>
        </div>  
        <asp:HiddenField ID="HiWSList" runat="server" />       
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            var treeData = JSON.parse(document.getElementById("HiWSList").value);
            $('#wsTree').tree({ onClick: function (node) {
                if (node != null) {
                    SelectWH(node.id);
                }
                return false;
            }
            });
            $('#wsTree').tree('loadData', treeData);
        });
        //选择仓库
        function SelectWH(wsID) {
            document.getElementById("frmList").src = "ManageWorkStation.aspx?wsID=" + wsID;
        }
    </script>
</body>
</html>