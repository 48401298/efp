<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPwd.aspx.cs" Inherits="Manage.Web.User.ModifyPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-save'" runat="server" 
                        onclientclick="return isValid();" onclick="btSave_Click">确定</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclientclick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="editTable" cellpadding=0 cellspacing=0>
                        <tr>
                            <th>原密码</th>
                            <td>
                                <asp:TextBox ID="OldPassWord" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>新密码</th>
                            <td>
                                <asp:TextBox ID="NewPassWord" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>重复输入</th>
                            <td>
                                <asp:TextBox ID="RepeatPassWord" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
     </table>
    </form>
    <script language=javascript type="text/javascript">
        function isValid() {
            if ($("#OldPassWord").val() == "") {
                MSI("提示","原密码不能为空！");
                return false;
            }
            if ($("#NewPassWord").val() == "") {
                MSI("提示", "新密码不能为空！");
                return false;
            }
            if ($("#NewPassWord").val() != $("#RepeatPassWord").val()) {
                MSI("提示", "两次输入的新密码不一致！");
                return false;
            }
        }
    </script>
</body>
</html>
