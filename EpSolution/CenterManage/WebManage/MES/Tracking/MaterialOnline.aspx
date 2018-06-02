<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialOnline.aspx.cs"
    Inherits="Manage.Web.MES.Tracking.MaterialOnline" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>原材料上线移动端</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <div class="easyui-panel" title="原材料上线" style="width: 98%">
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
                                                <asp:TextBox ID="PNAME" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                原料条码
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="MatBarCode" oninput="getMatByIDCode(this.value)" runat="server" class="easyui-validatebox" 
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                原料编号
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="MatCode" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                原料名称
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="MatName" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                数量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="MatNum" runat="server" class="easyui-validatebox" 
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                            <th nowrap="nowrap">
                                                规格
                                            </th>
                                            <td>
                                                <asp:TextBox ID="SpecCode" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="return isValid();" OnClick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClick="btCancel_Click">返回</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiMatList" runat="server" />
    <asp:HiddenField ID="hiMatID" runat="server" />
    <asp:HiddenField ID="HiMatBarCode" runat="server" />
    <asp:HiddenField ID="PRODUCTIONID" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">

        var matList;

        $(function () {
//            //绑定条码值变化事件
//            $("#MatBarCode").change(function () {
//                getMatByIDCode($("#MatBarCode").val());
//            });
//            //绑定条码回车事件
//            $('#MatBarCode').keydown(function (e) {
//                if (e.keyCode == 13) {
//                    getMatByIDCode($("#MatBarCode").val());
//                }
//            });

            //绑定编号值变化事件
            $("#BatchNumber").change(function () {
                getPNameByBatchNumber($("#BatchNumber").val());
            });


            matList = JSON.parse($("#hiMatList").val());
                 $('#tblmat').datagrid({
            });
            $('#tblmat').datagrid("loadData", matList);
            $("#MatBarCode")[0].focus();
    });

    var selectMat;
    var pName;
    function getPNameByBatchNumber(batchNumber) {
        if ($("#BatchNumber").val() == "")
            return;
        $.ajax({
            url: '../../Pub/GetProductInfoAjaxHandler.ashx?type=CP&Code=' + batchNumber,
            dataType: 'json',
            method: 'GET',
            success: function (data) {
                if (data.MatCode == "none") {
                    $("#PNAME").val("");

                    MSI("提示", "您输入的生产批次号无效");
                    selectMat = null;
                    return;
                }
                pName = data;
                $("#PRODUCTIONID").val(data.PID);
                $("#PNAME").val(data.PNAME);
                
            },
            error: function (ex) {
                alert('error:' + JSON.stringify(ex));
            }
        });
    }
    //根据货品识别码获取货品信息
    function getMatByIDCode(barCode) {
        if ($("#MatBarCode").val() == "")
            return;
        $.ajax({
            url: '../../Pub/GetMatInfoAjaxHandler.ashx?IDCode=' + barCode,
            dataType: 'json',
            method: 'GET',
            success: function (data) {
                if (data.MatCode == "none") {
                    $("#MatBarCode").val("");
                    $("#hiMatID").val("");
                    $("#MatCode").val("");
                    $("#MatName").val("");
                    $("#MatNum").val("");
                    $("#SpecCode").val("");
                    MSI("提示", "您输入的条码号无效");
                    selectMat = null;
                    return;
                }
                selectMat = data;
                $("#hiMatID").val(data.MatID);
                $("#MatCode").val(data.MatCode);
                $("#MatName").val(data.MatName);
                $("#MatNum").val("1");
                $("#SpecCode").val(data.SpecCode);
                $("#HiMatBarCode").val(barCode);
            },
            error: function (ex) {
                alert('error:' + JSON.stringify(ex));
            }
        });
    }
    //校验基本信息合法性
    function isValid() {
        if ($("#MatBarCode").val("") == "") {
            MSI("提示", "原料条码不能");
            return false;
        }
        return true;
    }
    </script>
</body>
</html>
