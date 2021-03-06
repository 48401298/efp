﻿<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ManageUseMat.aspx.cs" Inherits="Manage.Web.WH.In.ManageUseMat" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>领料管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',border:false" style="padding: 5px 10px 5px 10px;">
        <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
            OnClick="btQuery_Click">查询</asp:LinkButton>
        <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
            OnClick="btAdd_Click" OnClientClick="add();return false;" Visible="False">增加</asp:LinkButton>
        <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
            OnClick="btDelete_Click" OnClientClick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton>
    </div>
    <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
        <div class="easyui-layout" data-options="border:false" fit="true" style="width: 100%;
            height: 600px; overflow: hidden;">
            <div data-options="region:'north',border:false" style="">
                <div class="easyui-panel" title="查询条件" style="height: 68px; padding: 3px;">
                    <table>
                        <tr>
                            <td align="right">
                                批次号
                            </td>
                            <td>
                                <asp:TextBox ID="BatchNumber" runat="server" CssClass="easyui-textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div data-options="region:'center'" title="要货信息列表">
                <table style="width: 95%;">
                    <tr>
                        <td>
                            <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" CssClass="datagrid"
                                OnPreRender="GvList_PreRender" OnRowUpdating="GvList_RowUpdating" 
                                DataKeyNames="PID,SUPPLYID" onrowcommand="GvList_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BatchNumber" HeaderText="批次号">
                                        <HeaderStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ProduceName" HeaderText="产品">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FACTORYNAME" HeaderText="工厂">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="WarehouseName" HeaderText="仓库">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="DELIVERYDATE" HeaderText="配送日期" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="领料单">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkView" runat="server">未生成</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkBuild" CommandName="buildBill" 
                                                CommandArgument='<%# Eval("SUPPLYID").ToString() %>' runat="server">生成领料单</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="datagrid-header" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged">
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
        //查看
        function view(id) {
            openAppWindow1('查看', "ViewUseMatBill.aspx?id="+id, '900', '450');
            return false;
        }
        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
