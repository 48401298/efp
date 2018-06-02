<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ViewOutStockBill.aspx.cs" Inherits="Manage.Web.WH.Out.ViewOutStockBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看出库单</title>
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
                    <div class="easyui-panel" title="出库单">
                        <table>
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding=0 cellspacing=0 class="viewTable">
                                       <tr>
                                        <th align=left>出库单号</th><td>
                                           <asp:Label ID="BillNO" runat="server"></asp:Label>
                                           </td> 
                                         <th align=left>出库日期</th>
                                         <td>
                                           <asp:Label ID="BillDate" runat="server"></asp:Label>
                                           </td>                                     
                                        </tr>   
                                        <tr>
                                           <th align=left>收货单位</th><td>
                                           <asp:Label ID="Client" runat="server"></asp:Label>
                                            </td>
                                            <th align=left>出库方式</th>
                                         <td>
                                           <asp:Label ID="OutStockMode" runat="server"></asp:Label>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>仓库</th><td>
                                           <asp:Label ID="Warehouse" runat="server"></asp:Label>
                                            </td>
                                            <th align=left>收货人</th>
                                         <td>
                                           <asp:Label ID="Header" runat="server"></asp:Label>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>经手人</th><td>
                                           <asp:Label ID="HandlePerson" runat="server"></asp:Label>
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
                                <td width="600">
                                    <asp:GridView ID="GvList" runat="server" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Seq" HeaderText="序号" />
                                            <asp:BoundField DataField="IDCode" HeaderText="识别码" />
                                            <asp:BoundField DataField="MatCode" HeaderText="货品编号" />
                                            <asp:BoundField DataField="MatName" HeaderText="货品名称" />
                                            <asp:BoundField DataField="SpecCode" HeaderText="规格" />
                                            <asp:BoundField DataField="OutAmount" HeaderText="数量" />
                                            <asp:BoundField DataField="UnitName" HeaderText="单位" />
                                            <asp:BoundField DataField="OutPrice" HeaderText="单价" />
                                            <asp:BoundField DataField="OutSum" HeaderText="金额" />
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
