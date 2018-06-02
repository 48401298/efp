<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ManageInStockBill.aspx.cs" Inherits="Manage.Web.WH.In.ManageInStockBill" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入库管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>                        
            <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'" OnClientClick="add();return false;"
                        onclick="btAdd_Click" >入库(按条码)</asp:LinkButton>
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:88px;padding:3px;">
                        <table>
                        <tr>
                            <td align="right">
                                入库日期</td>
                            <td>
                                
                                <asp:TextBox ID="StartDate" runat="server" Width="100px"></asp:TextBox>
                                
                            </td>                            
                            <td>
                                
                                至</td>                            
                            <td>
                                
                                <asp:TextBox ID="EndDate" runat="server" Width="100px"></asp:TextBox>
                            </td>                            
                            <td>
                                
                                入库方式</td>                            
                            <td>
                                
                                <asp:DropDownList ID="InStockMode" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                &nbsp;</td>                            
                            <td>
                                
                                &nbsp;</td>                            
                        </tr>                            
                        <tr>
                            <td align="right">
                                仓库</td>
                            <td>
                                
                                <asp:DropDownList ID="Warehouse" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                货品类别</td>                            
                            <td>
                                
                                <asp:DropDownList ID="ProductType" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                货品</td>                            
                            <td>
                                
                                <asp:DropDownList ID="MatID" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                &nbsp;</td>                            
                            <td>
                                
                                &nbsp;</td>                            
                        </tr>                            
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" title="入库单列表">
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
                                            <asp:BoundField DataField="BillNO" HeaderText="入库单号">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BillDate" HeaderText="入库日期" 
                                                DataFormatString="{0:yyyy-MM-dd}">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProviderName" HeaderText="供货单位" />
                                            <asp:BoundField DataField="InStockMode" HeaderText="入库方式" />
                                            <asp:BoundField DataField="WarehouseName" HeaderText="仓库" />
                                            <asp:BoundField DataField="DeliveryPerson" HeaderText="交货人" />
                                            <asp:BoundField DataField="Receiver" HeaderText="验收人" />
                                            <asp:BoundField DataField="WHHeader" HeaderText="仓库负责人" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注" />
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtView" runat="server">查看</asp:LinkButton>
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
        //添加
        function add() {
            openAppWindow1('添加', "EditMlInBill.aspx", '760', '460');
            return false;
        }      
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditMlInBill.aspx?id=" + id, '760', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
