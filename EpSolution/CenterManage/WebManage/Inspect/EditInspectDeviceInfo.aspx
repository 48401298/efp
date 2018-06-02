<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditInspectDeviceInfo.aspx.cs" Inherits="Manage.Web.Inspect.EditInspectDeviceInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑监测设备信息</title>
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
                    <div class="easyui-panel" title="监测设备信息" style="padding:3px;">
                        <table >
                            <tr>
                                <td>
                                    <table class="editTable" cellpadding=0 cellspacing=0>
                                       <tr>
                                           <th>设备编号</th>
                                           <td>
                                               <asp:TextBox ID="DeviceCode" class="easyui-validatebox" 
                                                data-options="required:true" MaxLength="20" runat="server"></asp:TextBox>
                                           </td>
                                           <th>设备名称</th>
                                           <td>
                                                <asp:TextBox ID="DeviceName" MaxLength="50" data-options="required:true" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr>   
                                        <tr>
                                            <th>设备IP</th>
                                            <td>
                                               <asp:TextBox ID="DeviceIP" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                            <th>设备端口</th><td>
                                                <asp:TextBox ID="DevicePort" MaxLength="10" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>网络IP</th>
                                            <td>
                                               <asp:TextBox ID="LanIP" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                            <th>网络端口</th><td>
                                                <asp:TextBox ID="LanPort" MaxLength="10" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>最后登陆时间</th>
                                            <td>
                                               <asp:TextBox ID="LastLoginTime" runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                            <th>最后注册时间</th><td>
                                                <asp:TextBox ID="LastRegisterTime" runat="server" Width="150px"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>经度</th>
                                            <td>
                                               <asp:TextBox ID="Lon" MaxLength="12" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                            <th>纬度</th><td>
                                                <asp:TextBox ID="Lat" MaxLength="12" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>机构ID</th>
                                            <td>
                                               <asp:DropDownList ID="OrganID" runat="server"></asp:DropDownList>
                                            </td>
                                            <th>设备类型</th><td>
                                                <asp:DropDownList ID="DeviceType" runat="server"></asp:DropDownList>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>备注</th>
                                            <td colspan=3><asp:TextBox ID="Remark" MaxLength="100" CssClass="easyui-validatebox" runat="server"></asp:TextBox></td>
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
