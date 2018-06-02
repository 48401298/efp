<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="SetIDCodeBuild.aspx.cs" Inherits="Manage.Web.WH.Base.SetIDCodeBuild" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置唯一识别码生成参数</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-save'" runat="server" 
                        onclientclick="return isValid();" onclick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclientclick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="editTable">
                        <tr>
                            <th>生成个数</th>
                            <td>
                                <asp:TextBox ID="CodeCount" runat="server"></asp:TextBox>
                            </td>
                        </tr>                                  
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
