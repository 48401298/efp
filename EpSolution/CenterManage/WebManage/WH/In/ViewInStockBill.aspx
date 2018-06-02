<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ViewInStockBill.aspx.cs" Inherits="Manage.Web.WH.In.ViewInStockBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看入库单</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <style>
        #tblBase td
        {
            height:22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclick="btCancel_Click">返回</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="入库单">
                        <table>
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding=0 cellspacing=0 class="viewTable">
                                       <tr>
                                        <th align=left>入库单号</th><td>
                                           <asp:Label ID="BillNO" runat="server"></asp:Label>
                                           </td> 
                                         <th align=left>入库日期</th>
                                         <td>
                                           <asp:Label ID="BillDate" runat="server"></asp:Label>
                                           </td>                                     
                                        </tr>   
                                        <tr>
                                           <th align=left>供货单位</th><td>
                                           <asp:Label ID="Provider" runat="server"></asp:Label>
                                            </td>
                                            <th align=left>入库方式</th>
                                         <td>
                                           <asp:Label ID="InStockMode" runat="server"></asp:Label>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>仓库</th><td>
                                           <asp:Label ID="Warehouse" runat="server"></asp:Label>
                                            </td>
                                            <th align=left>交货人</th>
                                         <td>
                                           <asp:Label ID="DeliveryPerson" runat="server"></asp:Label>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>验收人</th><td>
                                           <asp:Label ID="Receiver" runat="server"></asp:Label>
                                            </td>
                                            <th align=left>仓库负责人</th>
                                         <td>
                                           <asp:Label ID="WHHeader" runat="server"></asp:Label>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>备注</th><td colspan="3">
                                           <asp:Label ID="Remark" runat="server"></asp:Label>
                                            </td>
                                        </tr>                 
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="800">
                                    <asp:GridView ID="GvList" runat="server" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Seq" HeaderText="序号" />
                                            <asp:BoundField DataField="MatBarCode" HeaderText="识别码" >
                                            <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MatCode" HeaderText="货品编号" >
                                            <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MatName" HeaderText="货品名称" >
                                            <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProduceDate" HeaderText="生产日期">
                                            <HeaderStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MatSpec" HeaderText="规格" />
                                            <asp:BoundField DataField="InAmount" HeaderText="数量" />
                                            <asp:BoundField DataField="UnitName" HeaderText="单位" />
                                            <asp:BoundField DataField="InPrice" HeaderText="单价" />
                                            <asp:BoundField DataField="InSum" HeaderText="金额" />
                                            <asp:BoundField DataField="SaveSite" HeaderText="仓位" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                         </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
