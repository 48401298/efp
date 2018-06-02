<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectDeviceInfo.aspx.cs" Inherits="Manage.Web.Inspect.InspectDeviceInfo" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监测设备管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
             <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>                        
            <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
                        onclick="btAdd_Click" onclientclick="add();return false;">增加</asp:LinkButton>
            <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
                        onclick="btDelete_Click" onclientclick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton>   
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:68px;padding:3px;">
                        <table class="condiTable">
                            <tr>
                                <td>
                                    所在机构</td>
                                <td>
                                    <asp:DropDownList ID="OrganID" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    设备编号</td>
                                <td>
                                    <asp:TextBox ID="DeviceCode" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    设备名称</td>
                                <td>
                                    <asp:TextBox ID="DeviceName" runat="server"></asp:TextBox>
                                </td>
                            </tr>                           
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" style="border-style: none">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="Id" Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DeviceCode" HeaderText="设备编号"></asp:BoundField>
                                            <asp:BoundField DataField="DeviceName" HeaderText="设备名称" />
                                            <asp:BoundField DataField="DeviceIP" HeaderText="设备IP" />
                                            <asp:BoundField DataField="DevicePort" HeaderText="设备端口" />
                                            <asp:BoundField DataField="LanIP" HeaderText="网络IP" />
                                            <asp:BoundField DataField="LanPort" HeaderText="网络端口" />
                                            <asp:BoundField DataField="LastLoginTime" HeaderText="最后登陆时间" />
                                            <asp:BoundField DataField="LastRegisterTime" HeaderText="最后注册时间" />
                                            <asp:BoundField DataField="Lon" HeaderText="经度" />
                                            <asp:BoundField DataField="Lat" HeaderText="纬度" />
                                            <asp:BoundField DataField="OrganDESC" HeaderText="机构名称" />
                                            <asp:BoundField DataField="DeviceTypeName" HeaderText="设备类型" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                            </asp:BoundField>
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>
                                        <HeaderStyle CssClass="datagrid-header" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
                                        onpagechanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </td>
                            </tr>
                            
                        </table>
                  </div>  
            </div>
        </div>   
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        //增加
        function add() {
            openAppWindow1('增加', "EditInspectDeviceInfo.aspx", '600', '460');
            return false;
        }
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditInspectDeviceInfo.aspx?id=" + id, '600', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
