﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackInfoManage.aspx.cs" Inherits="Manage.Web.MES.TrackManage.TrackInfoManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产品追溯资源管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',border:false" style="padding: 5px 10px 5px 10px;">
        <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
            OnClick="btQuery_Click">查询</asp:LinkButton>       
    </div>
    <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
        <div class="easyui-layout" data-options="border:false" fit="true" style="width: 100%;
            height: 600px; overflow: hidden;">
            <div data-options="region:'north',border:false" style="">
                <div class="easyui-panel" title="查询条件" style="height: 68px; padding: 3px;">
                    <table>
                        <tr>
                            <td align="right">
                                所属工厂
                            </td>
                            <td>
                                <asp:DropDownList ID="FACTORYPID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID">
                                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                所属生产线
                            </td>
                            <td>
                                <asp:DropDownList ID="PRID" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID">
                                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                产品
                            </td>
                            <td>
                                <asp:DropDownList ID="PRODUCTIONID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID">
                                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div data-options="region:'center'" title="生产计划列表">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" CssClass="datagrid"
                                OnPreRender="GvList_PreRender" OnRowUpdating="GvList_RowUpdating" DataKeyNames="PID">
                                <Columns>                                   
                                    <asp:BoundField DataField="FNAME" HeaderText="所属工厂">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLNAME" HeaderText="生产线">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PDNAME" HeaderText="产品">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLANDATE" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PLANAMOUNT" HeaderText="计划产量">
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FACTAMOUNT" HeaderText="实际产量">
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BATCHNUMBER" HeaderText="批次号">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>                                   
                                    <asp:CommandField ShowEditButton="True" />
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
        //资源设置
        function edit(id) {
            openAppWindow1('资源设置', "EditPlanManagement.aspx?id=" + id, '500', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>

