<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ManageWHMat.aspx.cs" Inherits="Manage.Web.WH.Base.ManageWHMat" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>规格管理</title>
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
                        <table>
                        <tr>
                            <td align="right">
                                货品名称</td>
                            <td>
                                <asp:TextBox ID="Description" runat="server" CssClass="easyui-textbox"></asp:TextBox>
                            </td>                            
                        </tr>                            
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" title="货品列表">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="货品编号" DataField="MatCode">
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="货品名称" DataField="MatName" />
                                            <asp:BoundField HeaderText="货品类别" DataField="ProductType" />
                                            <asp:BoundField HeaderText="计量单位" DataField="UnitCode" />
                                            <asp:BoundField HeaderText="规格" DataField="SpecCode" />
                                            <asp:BoundField HeaderText="入库价格" DataField="InPrice" />
                                            <asp:BoundField HeaderText="出库价格" DataField="OutPrice" />
                                            <asp:BoundField HeaderText="产地" DataField="ProductPlace" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                            <HeaderStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtBuildCode" runat="server">货品唯一识别码</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtSetSpec" runat="server">货品规格</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
    <script language=javascript type="text/javascript">
        //增加
        function add() {
            openAppWindow1('增加', "EditWHMat.aspx", '600', '450');
            return false;
        }
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditWHMat.aspx?id=" + id, '600', '460');
            return false;
        }

        //货品规格
        function setSpec(id) {
            window.location.href = "ManageMatSpec.aspx?matID="+id;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
