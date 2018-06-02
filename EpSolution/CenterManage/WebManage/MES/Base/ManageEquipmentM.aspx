<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEquipmentM.aspx.cs" Inherits="Manage.Web.MES.Base.ManageEquipmentM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设备管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">  
        <div data-options="region:'west',split:true" title="生产线" style="width:180px;">
            <ul id="eTree" class="easyui-tree" data-options=""></ul>
        </div>
		<div data-options="region:'center',title:'设备'">
            <div class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:100px;overflow:hidden;">
                 <iframe id="frmList" width="100%" height="100%" frameborder=0 src="ManageEquipment.aspx"></iframe>
            </div>
        </div>  
        <asp:HiddenField ID="HiEList" runat="server" />       
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            var treeData = JSON.parse(document.getElementById("HiEList").value);
            $('#eTree').tree({ onClick: function (node) {
                if (node != null) {
                    SelectE(node.id);
                }
                return false;
            }
            });
            $('#eTree').tree('loadData', treeData);
        });
        //选择生产线
        function SelectE(eID) {
            document.getElementById("frmList").src = "ManageEquipment.aspx?eID=" + eID;
        }
    </script>
</body>
</html>