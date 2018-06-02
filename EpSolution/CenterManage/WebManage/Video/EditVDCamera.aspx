<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditVDCamera.aspx.cs" Inherits="Manage.Web.Video.EditVDCamera" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑摄像头信息</title>
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
                    <div class="easyui-panel" title="摄像头信息" style="padding:3px;">
                        <table class="editTable" cellpadding=0 cellspacing=0>
                            <tr>
                                <th align=left>摄像头编号</th>
                                <td>
                                    <asp:TextBox ID="CameraCode" class="easyui-validatebox" 
                                               data-options="required:true" runat="server"></asp:TextBox>
                                </td> 
                                <th nowrap=nowrap align=left>摄像头名称</th>
                                <td>
                                    <asp:TextBox ID="CameraName" class="easyui-validatebox" 
                                               data-options="required:true" runat="server"></asp:TextBox>
                                </td>                                       
                                </tr>                                 
                                       <tr>
                                        <th align=left>设备类型</th>
                                        <td>
                                           <asp:DropDownList ID="EquKind" runat="server">
                                               <asp:ListItem Value="hkws">海康威视</asp:ListItem>
                                           </asp:DropDownList>
                                        </td>  
                                         <th align=left>设备IP</th><td>
                                           <asp:TextBox ID="EquIP" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr>                                         
                                       <tr>
                                            <th align=left>端口</th>
                                            <td>
                                           <asp:TextBox ID="EquPort" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>通道</th>
                                            <td>
                                           <asp:TextBox ID="IChannelID" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr>                                  
                                       <tr>
                                        <th align=left>用户名</th><td>
                                           <asp:TextBox ID="UserName" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td>   
                                           <th align=left>密码</th><td>
                                           <asp:TextBox ID="PassWord" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td>                                      
                                        </tr>                                   
                                       <tr>
                                        <th nowrap=nowrap align=left>码流类型</th>
                                           <td colspan=3>
                                           <asp:DropDownList ID="CodeStream" runat="server">
                                               <asp:ListItem Value="1">主码流</asp:ListItem>
                                               <asp:ListItem Value="2">子码流</asp:ListItem>
                                               <asp:ListItem Value="3">第三码流</asp:ListItem>
                                               <asp:ListItem Value="4">转码码流</asp:ListItem>
                                           </asp:DropDownList>
                                           </td>                                        
                                        </tr>                                         
                                       <tr>
                                        <th nowrap=nowrap align=left>移动监控地址</th>
                                           <td colspan=3>
                                           <asp:TextBox ID="MobileUrl" class="easyui-validatebox" 
                                               data-options="" runat="server" Width="100%"></asp:TextBox>
                                           </td>                                        
                                        </tr>                                         
                                        <tr>
                                           <th align=left>备注</th>
                                           <td colspan=3>
                                           <asp:TextBox ID="Remark" CssClass="easyui-validatebox" runat="server" Width="100%"></asp:TextBox>
                                           </td>
                                        </tr>                 
                                    </table>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>
    <script language="javascript" language="javascript">
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
    <asp:HiddenField ID="PostionID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>    
    </body>
</html>
