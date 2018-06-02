<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QualityTraceQuery.aspx.cs" Inherits="Manage.Web.MES.Query.QualityTraceQuery" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>质量追溯查询</title>
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
                    <div class="easyui-panel" title="查询条件" style="height:90px;padding:3px;">
                        <table>                                                   
                        <tr>
                            <td align="right">
                                生产日期</td>
                            <td>
                                
                                <asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
                            </td>                            
                            <td style="text-align: center">
                                
                                -</td>                            
                            <td>
                                
                                <asp:TextBox ID="EndDate" runat="server"></asp:TextBox>
                            </td>                            
                            <td style="text-align: right">
                                工厂</td>                            
                            <td>
                                
                                <asp:DropDownList ID="Factory" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td style="text-align: right">
                                
                                生产线</td>                            
                            <td>
                                
                                <asp:DropDownList ID="ProductLine" runat="server">
                                </asp:DropDownList>
                            </td>                            
                        </tr>                            
                        <tr>
                            <td align="right">
                                产品</td>
                            <td>
                                
                                <asp:DropDownList ID="Product" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                产品批次</td>                            
                            <td>
                                
                                <asp:TextBox ID="ProductBatchNumber" runat="server"></asp:TextBox>
                            </td>                            
                            <td>
                                原料识别码</td>                            
                            <td>
                                
                                <asp:TextBox ID="MatIDCode" runat="server"></asp:TextBox>
                            </td>                            
                            <td>
                                
                                产品识别码</td>                            
                            <td>
                                
                                <asp:TextBox ID="ProductIDCode" runat="server"></asp:TextBox>
                            </td>                            
                        </tr>                            
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" title="产成品列表">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="PID" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="GoodBarCode" HeaderText="产品识别码" />
                                            <asp:BoundField HeaderText="产品编号" DataField="ProductCode" />
                                            <asp:BoundField HeaderText="产品名称" DataField="ProductName" />
                                            <asp:BoundField HeaderText="产品批次" DataField="BatchNumber" />
                                            <%--<asp:BoundField HeaderText="产品工艺" DataField="PROCESSFLOW" />--%>
                                            <asp:BoundField HeaderText="工厂" DataField="FactoryName" />
                                            <asp:BoundField DataField="LineName" HeaderText="生产线" />
                                            <asp:BoundField HeaderText="生产时间" DataField="PLANDATE">
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtQualityTrace" runat="server">追溯信息</asp:LinkButton>
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

        //查看追溯明细
        function viewTraceDetail(pid) {
            openAppWindow1('查看追溯明细', "TraceDetailQuery.aspx?pid=" + pid, '760', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
