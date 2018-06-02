﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditOrgan.aspx.cs" Inherits="Manage.Web.Organ.EditOrgan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑机构信息</title>
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
                    <div class="easyui-panel" title="监控机构信息" style="padding:3px;">
                        <table class="editTable">
                                       <tr>
                                        <th align=left>机构名称</th>
                                        <td>
                                           <asp:TextBox ID="OrganDESC" class="easyui-validatebox" 
                                               data-options="required:true" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr>
                                        <tr>
                                        <th align=left>备注</th>
                                        <td>
                                           <asp:TextBox ID="Remark" class="easyui-validatebox" 
                                               runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr>               
                         </table>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
    <script language="javascript" language=javascript>
        $(function () {
        });
             
        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiParentID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>    
    </body>
</html>
