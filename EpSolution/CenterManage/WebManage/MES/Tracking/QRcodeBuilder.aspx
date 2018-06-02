<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRcodeBuilder.aspx.cs" Inherits="Manage.Web.MES.Tracking.QRcodeBuilder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>二维码生成</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <table>
            <tr>
                <td>
                    生成个数</td>
                <td>
                    <asp:TextBox ID="BuilderCount" class="easyui-numberbox" runat="server">80</asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="btSave" runat="server" CssClass="easyui-linkbutton" onclick="btSave_Click" 
                       >生成</asp:LinkButton>
                </td>
            </tr>            
        </table>
    
    </div>
    <asp:Image ID="Image1" runat="server" />
    </form>
</body>
</html>
