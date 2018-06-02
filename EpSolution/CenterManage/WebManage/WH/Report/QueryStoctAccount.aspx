<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="QueryStoctAccount.aspx.cs" Inherits="Manage.Web.WH.Report.QueryStoctAccount" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库存月台账</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" OnClientClick="return isValid();" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>  
            <asp:Button ID="Button1" runat="server" Visible=false onclick="Button1_Click" Text="计算" />
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:68px;padding:3px;">
                        <table>                                                   
                        <tr>
                            <td align="right">
                                年度月份</td>
                            <td>                               
                                <asp:TextBox ID="YearMonth" runat="server" Width="100px"></asp:TextBox>
                            </td>                            
                            <td>
                                
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
                                            <asp:BoundField HeaderText="货品编号" DataField="MatCode" />
                                            <asp:BoundField HeaderText="货品名称" DataField="MatName" />
                                            <asp:BoundField DataField="ProductType" HeaderText="货品类别" />
                                            <asp:BoundField DataField="UnitName" HeaderText="单位" />
                                            <asp:BoundField HeaderText="期初数量" DataField="PrimeAmount">
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="入库数量" DataField="InAmount" />
                                            <asp:BoundField DataField="OutAmount" HeaderText="出库数量" />
                                            <asp:BoundField DataField="GainAmount" HeaderText="盘盈数量" />
                                            <asp:BoundField DataField="LossAmount" HeaderText="盘亏数量" />
                                            <asp:BoundField DataField="LateAmount" HeaderText="期末数量" />
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
        
        //校验
        function isValid()
        {            
            if($('#YearMonth').val()=="")
            {
                MSI("提示", "年月不能为空");
                return;
            }
            return true;
        }

    </script>
</body>
</html>
