<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ViewOutMatList.aspx.cs" Inherits="Manage.Web.WH.Query.ViewOutMatList" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出库查询明细</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        onclientclick="parent.closeAppWindow1();return false;"
                        data-options="iconCls:'icon-back'" runat="server" 
                        >关闭</asp:LinkButton>
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div data-options="region:'center'" title="出库查询明细">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="WarehouseID,MatID" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="WarehouseName" HeaderText="仓库" />
                                            <asp:BoundField DataField="SaveSite" HeaderText="仓位" />
                                            <asp:BoundField DataField="OutDate" HeaderText="出库日期" />
                                            <asp:BoundField HeaderText="货品条码" DataField="MatBarCode">
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="货品编号" DataField="MatCode">
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="货品名称" DataField="MatName">
                                            </asp:BoundField>                                            
                                            <asp:BoundField HeaderText="出库数量" DataField="Amount" />
                                            <asp:BoundField HeaderText="单位" DataField="UnitName" />
                                            <asp:BoundField DataField="MatSpec" HeaderText="规格" />
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
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    <asp:HiddenField ID="WarehouseID" runat="server" />
    <asp:HiddenField ID="MatID" runat="server" />
    <asp:HiddenField ID="StartDate" runat="server" />
    <asp:HiddenField ID="EndDate" runat="server" />
    </form>
    <script language=javascript type="text/javascript">
        
    </script>
</body>
</html>
