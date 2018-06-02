<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageQualityCheck.aspx.cs"
    Inherits="Manage.Web.MES.QualityCheck.ManageQualityCheck" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产品质检管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',border:false" style="padding: 5px 10px 5px 10px;">
        <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
            OnClick="btQuery_Click">查询</asp:LinkButton>
        <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
            OnClick="btAdd_Click" OnClientClick="add();return false;">增加</asp:LinkButton>
        <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
            OnClick="btDelete_Click" OnClientClick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton>
    </div>
    <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
        <div class="easyui-layout" data-options="border:false" fit="true" style="width: 100%;
            height: 600px; overflow: hidden;">
            <div data-options="region:'north',border:false" style="">
                <div class="easyui-panel" title="查询条件" style="height: 86px; padding: 3px;">
                    <table>
                        <tr>
                            <td align="right">
                                质检单号
                            </td>
                            <td>
                                <asp:TextBox ID="BillNO" runat="server" CssClass="easyui-textbox" Width="100px"></asp:TextBox>
                            </td>
                            <td align="right">
                                质检日期
                            </td>
                            <td>
                                <asp:TextBox ID="StartDate" runat="server" CssClass="easyui-datebox" Width="100px"></asp:TextBox>
                            </td>
                            <td align="right">
                                -
                            </td>
                            <td>
                                <asp:TextBox ID="EndDate" runat="server" CssClass="easyui-datebox" Width="100px"></asp:TextBox>
                            </td>
                            <td>
                                质检结果
                            </td>
                            <td>
                                <asp:DropDownList ID="CheckResult" runat="server" Width="80px">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="合格" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="不合格" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                工厂
                            </td>
                            <td>
                                <asp:DropDownList ID="FACTORYPID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                产品
                            </td>
                            <td>
                                <asp:DropDownList ID="PRODUCEID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                            </td>
                            <td align="right" colspan="4">
                             
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div data-options="region:'center'" title="产品质检列表">
                <table style="width: 95%;">
                    <tr>
                        <td>
                            <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" CssClass="datagrid"
                                OnPreRender="GvList_PreRender" OnRowUpdating="GvList_RowUpdating" DataKeyNames="ID">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="BillNO" HeaderText="质检单号">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CheckDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="质检日期">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BATCHNUMBER" HeaderText="产品批次号">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PDNAME" HeaderText="产品名称">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CheckResult" HeaderText="质检结果">
                                        <HeaderStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CheckPerson" HeaderText="质检员">
                                        <HeaderStyle Width="100px" />
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
        //增加
        function add() {
            openAppWindow1('增加', "EditQualityCheck.aspx", '600', '500');
            return false;
        }
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditQualityCheck.aspx?id=" + id, '600', '500');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
