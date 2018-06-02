<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialOffline.aspx.cs"
    Inherits="Manage.Web.MES.Tracking.MaterialOffline" %>

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
                    <div class="easyui-panel" title="产品完工下线" style="width: 98%">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" class="editTable" width="100%">
                                        <tr>
                                            <th width="100" nowrap="nowrap">
                                                生产批次号
                                            </th>
                                            <td colspan="3">
                                                <asp:DropDownList ID="BatchNumber" runat="server" data-options="required:true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                产品条码
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="GoodBarCode" oninput="getMatByIDCode(this.value)" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                产品编号
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="PCode" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                产品名称
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="PName" runat="server" class="easyui-validatebox" ReadOnly="True"
                                                    Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                数量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="OfflineNum" runat="server" class="easyui-numberbox" data-options="min:0,max:100,precision:0,required:true" Width="30px"></asp:TextBox>
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
    <asp:HiddenField ID="FACTORYPID" runat="server" />
    <asp:HiddenField ID="PRID" runat="server" />
    <asp:HiddenField ID="ProductionID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="FactAmount" runat="server" />
    <asp:HiddenField ID="PLANID" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
//            //绑定条码值变化事件
//            $("#BatchNumber").change(function () {
//                getProduceInfoByBarcode($("#BatchNumber").val());
//            });

//            //绑定条码值变化事件
//            $("#GoodBarCode").change(function () {
//                getMatByIDCode($("#GoodBarCode").val());
//            });
//            //绑定条码回车事件
//            $('#GoodBarCode').keydown(function (e) {
//                if (e.keyCode == 13) {
//                    getMatByIDCode($("#GoodBarCode").val());
//                }
//            });
            $("#GoodBarCode")[0].focus();
        });

        function getProduceInfoByBarcode(batchNumber) {
            if ($("#BatchNumber").val() == "")
                return;
            $.ajax({
                url: '../../Pub/GetProductInfoAjaxHandler.ashxK&Code=' + batchNumber,
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    if (data.PID == "none") {
                        $("#FACTORYPID").val("");
                        $("#PRID").val("");
                        $("#ProductionID").val("");
                        $("#FactAmount").val("");
                        $("#PLANID").val("");
                        MSI("提示", "您输入的生产批次号无效");
                        selectMat = null;
                        return;
                    }
                    $("#FACTORYPID").val(data.FACTORYPID);
                    $("#PRID").val(data.PRID);
                    $("#FactAmount").val(data.FactAmount);
                    $("#PLANID").val(data.PLANID);
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }
        //根据货品识别码获取货品信息
        function getMatByIDCode(barCode) {
            if ($("#GoodBarCode").val() == "")
                return;
            $.ajax({
                url: '../../Pub/GetMatInfoAjaxHandler.ashx?IDCode=' + barCode,
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    if (data.MatCode == "none") {
                        $("#GoodBarCode").val("");
                        $("#PCode").val("");
                        $("#PName").val("");
                        $("#SpecCode").val("");
                        MSI("提示", "请选择生产批次号");
                        selectMat = null;
                        return;
                    }
                    selectMat = data;
                    $("#PCode").val(data.MatCode);
                    $("#PName").val(data.MatName);
                    $("#SpecCode").val(data.SpecCode);

                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }
        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            if ($("#BatchNumber").val() == "") {
                MSI("提示", "您输入的条码号无效");
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
