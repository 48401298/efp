<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageVDCamera.aspx.cs" Inherits="Manage.Web.WH.Video.ManageVDCamera" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>摄像头信息管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'west'" title="监控位置" style="padding:5px 10px 5px 10px;width:200px">                       
             <ul id="postionTree" class="easyui-tree" data-options="checkbox:false"></ul>             
        </div>        
        <div data-options="region:'center'" title="摄像头信息" style="padding-right: 10px; padding-left: 10px;">
            <iframe id="frmCamera" src="" frameborder=0 width=98% height=98%></iframe>
        </div>   
        <asp:HiddenField ID="hiPostionList" runat="server" />
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />

    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            var treeData = JSON.parse(document.getElementById("hiPostionList").value);
            $('#postionTree').tree({ onClick: function (node) {
                    if (node != null) {
                        SelectPostion(node.id);
                    }
                    return false;
                }
            });
            $('#postionTree').tree('loadData', treeData);
        });
        //选择位置
        function SelectPostion(postionID) {
            document.getElementById("frmCamera").src = "ListVDCamera.aspx?postionID=" + postionID;
        }
    </script>
</body>
</html>
