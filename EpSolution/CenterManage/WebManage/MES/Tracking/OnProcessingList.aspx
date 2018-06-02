<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnProcessingList.aspx.cs"
    Inherits="Manage.Web.MES.Tracking.OnProcessingList" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>装箱移动端</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;" />
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" style=" padding: 3px;">
        <table>
            <tr>
                <td align="right">
                    设备条码
                </td>
                <td>
                    <asp:TextBox ID="tbBarCode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        OnClick="btQuery_Click">查询</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClick="btCancel_Click">返回</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Repeater ID="OnPList" runat="server">
            <ItemTemplate>
                <div style="border: 1px solid #000; margin: 5px">
                    <table>
                        <tr>
                            <td colspan="2">
                                产品名称:<%#Eval("ProductName") %>
                            </td>
                            <td>
                            <a href="#" onclick="PClick('<%#Eval("PID") %>')" class="easyui-linkbutton" data-options="iconCls:'icon-redo'">进入</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                工序:<%#Eval("ProcessName")%>
                            </td>
                            <td>
                                设备:<%#Eval("EName")%>
                            </td>
                            <td>
                                原料:<%#Eval("MatName")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                开始时间:<%#Eval("StartTime") %>
                            </td>
                            <td>
                                当前时间:<%#Eval("CurrentTime")%>
                            </td>
                            <td>
                                加工时间:<%#Eval("SpendTime")%>
                            </td>
                        </tr>
                    </table>
                </div>
                
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script language="javascript" type="text/javascript">
        function PClick(id) {
            window.location.href = "MaterialTrace.aspx?id=" + id;
        }
    </script>
    </form>
    
</body>
</html>
