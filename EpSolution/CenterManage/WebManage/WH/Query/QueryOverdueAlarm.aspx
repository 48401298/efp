<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="QueryOverdueAlarm.aspx.cs" Inherits="Manage.Web.WH.Query.QueryOverdueAlarm" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库存过期预警查询</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>  
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:58px;padding:3px;">
                        <table>                                                   
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
                  <div data-options="region:'center'" title="库存列表">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="ID" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="WarehouseName" HeaderText="仓库" />
                                            <asp:BoundField HeaderText="仓位" DataField="SaveSite" />
                                            <asp:BoundField HeaderText="货品唯一识别码" DataField="MatBarCode" />
                                            <asp:BoundField HeaderText="货品编号" DataField="MatCode" />
                                            <asp:BoundField HeaderText="货品名称" DataField="MatName" />
                                            <asp:BoundField DataField="ProductType" HeaderText="货品类别" />
                                            <asp:BoundField HeaderText="数量" DataField="ProductAmount">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnitName" HeaderText="单位" />
                                            <asp:BoundField DataField="MatSpec" HeaderText="规格" />
                                            <asp:BoundField HeaderText="金额" DataField="ProductSum" />
                                            <asp:BoundField HeaderText="生产日期" DataField="ProduceDate" />
                                            <asp:BoundField DataField="QualityPeriod" HeaderText="保质期(天)" />
                                            <asp:BoundField HeaderText="入库时间" DataField="CREATETIME" />
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
        
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditInMode.aspx?id=" + id, '500', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
