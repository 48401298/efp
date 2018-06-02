<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInspectItemInfo.aspx.cs" Inherits="Manage.Web.Inspect.EditInspectItemInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑监测项目信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
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
                    <div class="easyui-panel" title="监测项目信息" style="padding:3px;">
                        <table >
                            <tr>
                                <td>
                                    <table class="editTable" cellpadding=0 cellspacing=0>
                                       <tr>
                                           <th>项目编号</th>
                                           <td>
                                               <asp:TextBox ID="ItemCode" class="easyui-validatebox" 
                                                data-options="required:true" MaxLength="10" runat="server"></asp:TextBox>
                                           </td>
                                           <th>项目名称</th>
                                           <td>
                                                <asp:TextBox ID="ItemName" MaxLength="50" data-options="required:true" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr>   
                                        
                                        <tr>
                                            <th>单位</th>
                                            <td>
                                               <asp:TextBox ID="Unit" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                            <th>小数位数</th><td>
                                                <asp:TextBox ID="PointCount" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>备注</th>
                                            <td colspan=3><asp:TextBox ID="Remark" MaxLength="50" CssClass="easyui-validatebox" runat="server"></asp:TextBox></td>
                                        </tr>              
                                    </table>
                                </td>
                            </tr>       
                         </table>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
    <script language="javascript" language=javascript>

    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>    
    </body>
</html>
