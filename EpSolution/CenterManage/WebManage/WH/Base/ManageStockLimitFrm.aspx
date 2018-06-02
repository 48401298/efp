<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ManageStockLimitFrm.aspx.cs" Inherits="Manage.Web.WH.Base.ManageStockLimitFrm" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库存预警设置</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">  
        <div data-options="region:'west',split:true" title="仓库" style="width:180px;">
            <ul id="whTree" class="easyui-tree" data-options=""></ul>
        </div>
		<div data-options="region:'center',title:'库存预警设置'">
            <div class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:100px;overflow:hidden;">
                 <iframe id="frmList" width="100%" height="100%" frameborder=0 src="ListStockLimit.aspx"></iframe>
            </div>
        </div>  
        <asp:HiddenField ID="HiWHList" runat="server" />       
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            var treeData = JSON.parse(document.getElementById("HiWHList").value);
            $('#whTree').tree({ onClick: function (node) {
                if (node != null) {
                    SelectWH(node.id);
                }
                return false;
            }
            });
            $('#whTree').tree('loadData', treeData);
        });
        //选择仓库
        function SelectWH(whID) {
            document.getElementById("frmList").src = "ListStockLimit.aspx?whID=" + whID;
        }
    </script>
</body>
</html>
