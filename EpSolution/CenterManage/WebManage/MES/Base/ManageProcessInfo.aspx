<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageProcessInfo.aspx.cs" Inherits="Manage.Web.MES.Base.ManageProcessInfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工序信息管理</title>
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
                <div class="easyui-panel" title="查询条件" style="height: 68px; padding: 3px;">
                    <table>
                        <tr>
                            <td align="right">
                                工序编号
                            </td>
                            <td>
                                <asp:TextBox ID="PCODE" runat="server" CssClass="easyui-textbox"></asp:TextBox>
                            </td>
                            <td align="right">
                                工序名称
                            </td>
                            <td>
                                <asp:TextBox ID="PNAME" runat="server" CssClass="easyui-textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div data-options="region:'center'" title="工序信息列表">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" CssClass="datagrid"
                                OnPreRender="GvList_PreRender" OnRowUpdating="GvList_RowUpdating" DataKeyNames="PID">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PCODE" HeaderText="工序编号">
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PNAME" HeaderText="工序名称">
                                        <HeaderStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PTIME" HeaderText="加工时间(分钟)">
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
    <asp:HiddenField ID="HiFLOWID" runat="server" />
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        //增加
        function add() {
            openAppWindow1('增加', "EditProcessInfo.aspx?flowID=" + $("#HiFLOWID").val(), '520', '450');
            return false;
        }
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditProcessInfo.aspx?id=" + id, '520', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>

