<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialTrace.aspx.cs"
    Inherits="Manage.Web.MES.Tracking.MaterialTrace" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>流程追踪移动端</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="easyui-panel" title="流程追踪移动端" style="width: 98%">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="editTable" width="100%">
                                        <tr>
                                            <th width="100" nowrap="nowrap">
                                                生产批次号
                                            </th>
                                            <td colspan="3">
                                                <asp:DropDownList ID="BatchNumber" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                产品名称
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="PNAME" runat="server" class="easyui-validatebox" Width="100%" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                工位
                                            </th>
                                            <td>
                                                <asp:TextBox ID="WSCODE" oninput="getGXByCode(this.value)" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                工序
                                            </th>
                                            <td>
                                                <asp:TextBox ID="GXNAME" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                设备条码
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="CBBARCODE" oninput="getCBByCode(this.value)" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                设备名称
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="CBNAME" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table cellpadding="0" cellspacing="0" class="editTable" width="90%">
                                        <tr>
                                            <td width="90px">
                                                开始时间
                                            </td>
                                            <td width="90px">
                                                <asp:Label ID="StartTime" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td width="90px">
                                                当前时间
                                            </td>
                                            <td width="90px">
                                                <asp:Label ID="CurrentTime" runat="server" Text=""></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td width="90px">
                                                加工时间
                                            </td>
                                            <td width="110px">
                                                <asp:Label ID="SpendTime" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:LinkButton ID="btStart" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="" OnClick="btStart_Click">开始</asp:LinkButton>
                    <asp:LinkButton ID="btEnd" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="" OnClick="btEnd_Click">结束</asp:LinkButton>
                    <asp:LinkButton ID="btReset" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="" OnClick="btReset_Click">重置</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClick="btCancel_Click">返回</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiGX" runat="server" />
    <asp:HiddenField ID="hiCB" runat="server" />
    <asp:HiddenField ID="hiGW" runat="server" />
    <asp:HiddenField ID="hiPid" runat="server" />
    <asp:HiddenField ID="WORKINGSTARTTIME" runat="server" />
    <asp:HiddenField ID="PRODUCTIONID" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {

            //绑定生产批次号值变化事件
            $("#BatchNumber").change(function () {
                getPNameByBatchNumber($("#BatchNumber").val());
            });

//            //绑定条码值变化事件
//            $("#CBBARCODE").change(function () {
//                getCBByCode($("#CBBARCODE").val());
//            });
//            //绑定条码回车事件
//            $('#CBBARCODE').keydown(function (e) {
//                if (e.keyCode == 13) {
//                    getCBByCode($("#CBBARCODE").val());
//                }
//            });
//            //绑定工位值变化事件
//            $("#WSCODE").change(function () {
//                getGXByCode($("#WSCODE").val());
//            });
//            //绑定工位回车事件
//            $('#WSCODE').keydown(function (e) {
//                if (e.keyCode == 13) {
//                    getGXByCode($("#WSCODE").val());
//                }
//            });
            StartTimer();

            if ($("#hiID").val() == "") {
                $('#btEnd').linkbutton('disable');
            }
            else {
                $('#btStart').linkbutton('disable');
            }
            $("#WSCODE")[0].focus();
        })

        function getPNameByBatchNumber(batchNumber) {
            if ($("#BatchNumber").val() == "")
                return;
            $.ajax({
                url: '../../Pub/GetProductInfoAjaxHandler.ashx?Code=' + batchNumber + '&Type=CP',
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    if (data == "none") {
                        $("#PNAME").val("");
                        MSI("提示", "您输入的生产批次号无效");
                        return;
                    }
                    $("#PNAME").val(data.PNAME);
                    $("#PRODUCTIONID").val(data.PID);
                    
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }
        //根据设备条码获得设备名称
        function getCBByCode(code) {
            if ($("#CBBARCODE").val() == "")
                return;
            $.ajax({
                url: '../../Pub/GetProductInfoAjaxHandler.ashx?Code=' + code + '&Type=CB',
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    if (data.PID == "none") {
                        $("#CBBARCODE").val("");
                        MSI("提示", "您输入的设备条码无效");
                        selectMat = null;
                        return;
                    }
                    $("#CBNAME").val(data.ENAME);
                    $("#hiCB").val(data.PID);                    
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
//            if ($("#BatchNumber").val() != "" && $("#GXNAME").val() != "") {               
//                $.ajax({
//                    url: '../../Pub/GetProductInfoAjaxHandler.ashx?Code=' + code + '&Type=CB2&BatchNumber=' + $("#BatchNumber").val(),
//                    dataType: 'json',
//                    method: 'GET',
//                    success: function (data) {
//                        if (data.PID == "none") {
//                            return;
//                        }
//                        $("#GXNAME").val(data.PNAME);
//                        $("#hiGX").val(data.PID);
//                        $("#hiGW").val(data.GWID);
//                    },
//                    error: function (ex) {
//                        alert('error:' + JSON.stringify(ex));
//                    }
//                });
//            }
        }
        //根据工位获取工序信息
        function getGXByCode(code) {
            if ($("#MatBarCode").val() == "")
                return;
            $.ajax({
                url: '../../Pub/GetProductInfoAjaxHandler.ashx?Code=' + code + '&Type=GX',
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    if (data.PID == "none") {
                        $("#WSCODE").val("");
                        $("#GXNAME").val("");
                        $("#hiGX").val("");
                        $("#hiGW").val("");
                        MSI("提示", "您输入的工位号无效");
                        selectMat = null;
                        return;
                    }
                    $("#GXNAME").val(data.PNAME);
                    $("#hiGX").val(data.PID);
                    $("#hiGW").val(data.GWID);
                    $("#CBBARCODE")[0].focus();
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }

        function StartTimer() {
            
            setInterval(show, 1000);
        }

        function show() {
            if ($("#StartTime").text() != "") {
                $.ajax({
                    url: '../../Pub/GetCurrentTimeAjaxHandler.ashx?date=' + $("#WORKINGSTARTTIME").val(),
                    dataType: 'json',
                    method: 'GET',
                    success: function (data) {
                        $("#CurrentTime").text(data.split('|')[0]);
                        $("#SpendTime").text(data.split('|')[1]);

                    },
                    error: function (ex) {
                        alert('error:' + JSON.stringify(ex));
                    }
                });
            }
        }
    </script>
</body>
</html>
