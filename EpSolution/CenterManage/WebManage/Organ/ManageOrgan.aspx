<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageOrgan.aspx.cs" Inherits="Manage.Web.Organ.ManageOrgan" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">                       
            <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
                        onclientclick="add();return false;">增加</asp:LinkButton>
            <asp:LinkButton ID="btModify" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-edit'"
                        onclientclick="edit();return false;">修改</asp:LinkButton>
            <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
                        onclientclick="remove();return false;">删除</asp:LinkButton>                    
        </div>        
        <div data-options="region:'center',border:false" title="机构信息" style="padding-right: 10px; padding-left: 10px;">
            <ul id="organTree" class="easyui-tree" data-options="checkbox:false"></ul>
        </div>   
        <asp:HiddenField ID="hiOrganList" runat="server" />
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />

    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            var treeData = JSON.parse(document.getElementById("hiOrganList").value);
            $('#organTree').tree('loadData', treeData);
        });

        //增加
        function add() {
            var node = $('#organTree').tree('getSelected');

            if (node == null) {
                MSI("提示", "请选择上级机构");
                return;
            }
            
            openAppWindow1('增加', "EditOrgan.aspx?parentID="+node.id, '460', '450');
            return false;
        }

        function addNode(id, text) {
            var node = $('#organTree').tree('getSelected');
            if (node) {
                $('#organTree').tree('append',{
                    parent:node.target,
                    data:[{
                        text:text,
                        id: id,
                        parentId: node.id
                    }]
                }); 
            }
        }

        //编辑
        function edit() {
            var node = $('#organTree').tree('getSelected');

            if (node == null) {
                MSI("提示", "请选择要修改机构");
                return;
            }
            if (node.id == "root") {
                MSI("提示", "根节点不能修改");
                return;
            }
            openAppWindow1('编辑', "EditOrgan.aspx?id=" + node.id, '500', '460');
            return false;
        }
          
        function editNode(id, text) {
            var node = $('#organTree').tree('getSelected');
            if (node) {
                node.id = id;
                node.text = text;
                $('#organTree').tree('update', node); 
            }
        }

        function remove() {
            var r = confirm('确定要删除选中的记录？');
            if (r == false)
                return;

            var node = $('#organTree').tree('getSelected');

            if (node == null) {
                MSI("提示", "请选择要删除机构");
                return;
            }
            if (node.id == "root") {
                MSI("提示", "根节点不能删除");
                return;
            }

            alert(node.getChild());

            $.ajax({
                url: 'DeleteOrganAH.ashx?id=' + node.id,
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    $('#organTree').tree('remove', node.target);
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });            
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
