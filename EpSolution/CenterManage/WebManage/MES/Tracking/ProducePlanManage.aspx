<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProducePlanManage.aspx.cs" Inherits="Manage.Web.MES.Tracking.ProducePlanManage" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生产监控</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" title="生产监控" style="width: 98%">
        <table>
            <tr>
                <td>
                     <asp:LinkButton ID="btCancel" runat="server" CssClass="easyui-linkbutton" 
                                    data-options="iconCls:'icon-back'" OnClick="btCancel_Click">返回</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
            <tr>
                <td>
                 <asp:Repeater ID="OnPList" runat="server" onitemcommand="OnPList_ItemCommand">
                    <ItemTemplate>
                        <div style="border: 1px solid #000; margin: 5px">
                    <table class="viewTable">
                        <tr>
                            <th>
                                产品名称
                            </th>
                            <td>
                                <%#Eval("PDNAME")%>
                            </td>
                            <th>
                                生产批次
                            </th>
                            <td>
                                <%#Eval("BATCHNUMBER")%>
                             </td>
                        </tr>
                        <tr>
                            <th>
                                计划产量
                            </th>
                            <td>
                                <%#Eval("PLANAMOUNT")%></td>
                            <th>
                                实际产量
                            </th>
                            <td>
                                <%#Eval("FACTAMOUNT")%></td>
                        </tr>
                        <tr>
                            <th>
                                生产开始时间
                            </th>
                            <td>
                                <%#Eval("StartTime")%>
                            </td>
                            <th>
                            </th>
                            <td>
                                <asp:LinkButton ID="lbtconfirmFinished" CommandName="finished" OnClientClick="return confirmFinished();" CommandArgument=<%#Eval("PID")%>  CssClass ="easyui-linkbutton" runat="server">完成</asp:LinkButton></td>
                        </tr>
                        <tr>
                            
                            <th>
                                物料组成</th>
                            <td colspan=3>
                                <%#Eval("Materials")%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                已完成工序
                            </th>
                            <td colspan=3>
                                <%#Eval("FinishedProcess")%>
                            </td>
                        </tr>
                        <%--<tr>
                            <th>
                                未完成工序
                            </th>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>--%>
                    </table>
                     </div>
                
                    </ItemTemplate>
                </asp:Repeater>
                </td>
            </tr>                        
            </table>
                </td>
            </tr>
        </table>        
        <asp:HiddenField ID="HiPID" runat="server" />
    </div>
    </form>
    <script language=javascript type="text/javascript">
        function confirmFinished() {
            var r = confirm("确认完成吗");
            if(r==false)
                return false;
            return true;
        }
    </script>
</body>
</html>
