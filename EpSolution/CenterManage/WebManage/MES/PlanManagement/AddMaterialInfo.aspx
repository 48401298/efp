<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMaterialInfo.aspx.cs" Inherits="Manage.Web.MES.PlanManagement.AddMaterialInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加原料信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div>
        <div class="easyui-panel" title="原料信息" style="width: 98%">
            <table cellpadding="0" cellspacing="0" class="editTable" width="100%">
                <tr>
                    <th width="100" nowrap="nowrap">
                        物料
                    </th>
                    <td>
                        <asp:DropDownList ID="MATRIALID" runat="server" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>
                        数量
                    </th>
                    <td>
                        <asp:TextBox ID="AMOUNT" runat="server" class="easyui-numberbox" data-options="min:0,precision:0" Width="120px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        单位
                    </th>
                    <td>
                        <asp:DropDownList ID="Unit" runat="server" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                            runat="server" OnClientClick="return add();">保存</asp:LinkButton>
                        <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                            runat="server" OnClientClick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">

        //增加
        function add() {
            if ($("#MATRIALID").val() == "") {
                MSI("提示", "物料不能为空");
                return false;
            }

            var detail = JSON.parse("{}");
            detail["MATRIALID"] = $('#MATRIALID').val();
            detail["MATRIALNAME"] = $("#MATRIALID").find("option:selected").text();
            detail["AMOUNT"] = $("#AMOUNT").val();
            detail["Unit"] = $("#Unit").val();
            detail["UNITNAME"] = $("#Unit").find("option:selected").text();
            parent.addMat(detail);
            parent.closeAppWindow1();
            return false;
        }

        //校验基本信息合法性
        function isValid() {
            if (isValidate() == false) {
                return false;
            }

            return true;
        }
    </script>
</body>
</html>

