<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialPacking.aspx.cs" Inherits="Manage.Web.MES.Tracking.MaterialPacking" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>装箱移动端</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td nowrap="nowrap" width="100">
                                包装条码</td>
                            <td>
                                <asp:TextBox ID="GoodBarCode" runat="server" class="easyui-validatebox" 
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td nowrap="nowrap" width="100">
                                追溯二维码</td>
                            <td>
                                <asp:TextBox ID="DetailBarCode" oninput="addMat(this.value)" class="easyui-validatebox" runat="server" 
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblmat" style="height:300px;width:100%" iconCls="icon-edit">
                        <thead>
			                <tr>                                   
				                <th field="DetailBarCode" align="center" width="300px">追溯二维码</th>
			                </tr>			                    
		                </thead>
	                </table> 
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="return isValid();" OnClick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClick="btCancel_Click">返回</asp:LinkButton>
                    
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="hiMatList" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        var matList;
        $(function () {
            matList = JSON.parse($("#hiMatList").val());
            $('#tblmat').datagrid({
            });
            $('#tblmat').datagrid("loadData", matList);
            $("#GoodBarCode")[0].focus();
        });

         //增加
        function addMat(value) {
            var code = getQueryString(value,"barcode");
            if (code == "") {
                return;
            }
            matList = $('#tblmat').datagrid("getData");

            if (existsJsonItem(matList.rows, "DetailBarCode", code) == true) {
                $("#DetailBarCode").val("");
                MSI("提示", "已存在！");
                return;
            } 
            var mat = JSON.parse("{}");
            mat["DetailBarCode"] = code;
             
            matList.rows.push(mat);
            $('#tblmat').datagrid("loadData", matList);

            $("#DetailBarCode").val("");
         }

         //校验基本信息合法性
         function isValid() {
             if (isValidate() == false) {
                 return false;
             }

             if ($("#GoodBarCode").val() == "") {
                 MSI("提示", "包装条码不能为空");
                 return false;
             }

             matList = $('#tblmat').datagrid("getData");
             if (matList.rows.length == 0) {
                 MSI("提示", "请添加追溯内容");
                 return false;
             }
             
             $("#hiMatList").val(JSON.stringify(matList.rows));

             return true;
         }

         function getQueryString(url, name) {
             var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
             var paraObj = {}
             for (i = 0; j = paraString[i]; i++) {
                 paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
             }
             var returnValue = paraObj[name.toLowerCase()];
             if (typeof (returnValue) == "undefined") {
                 return "";
             } else {
                 return returnValue;
             } 
         }  
    </script>
</body>
</html>
