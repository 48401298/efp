<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EditSupplyInfo.aspx.cs"
    Inherits="Manage.Web.MES.PlanManagement.EditSupplyInfo" %>

<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑要货信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script language="javascript" type="text/javascript">
        //删除按钮
        function formatDeleteButton(value, rec) {
            if (value == "none") {
                return "";
            }
            else {
                return QLinkButtonHtml("删除", value);
            }
        }
    </script>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" data-options="iconCls:'icon-save'"
                        runat="server" OnClientClick="return isValid();" OnClick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClientClick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="要货信息" style="padding: 3px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding="0" cellspacing="0" class="editTable">
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                生产计划
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PLANID" runat="server" Width="120px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="BatchNumber" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                产品
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PDNAME" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                工厂
                                            </th>
                                            <td>
                                                <asp:TextBox ID="FNAME" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                生产线
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PLNAME" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align=left>仓库</th>
                                            <td>

                                                <asp:DropDownList ID="Warehouse" runat="server" DataTextField="Description" 
                                                    DataValueField="ID">
                                                </asp:DropDownList>

                                            </td>                                        
                                        </tr> 
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                配送日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="DELIVERYDATE" class="easyui-datebox" runat="server" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="REMARK" CssClass="easyui-validatebox" MaxLength="100" runat="server" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                        OnClientClick="add();return false;">增加</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblMaterialList" style="height: 200px; width: 400px" iconcls="icon-edit">
                                        <thead>
                                            <tr>
                                                <th field="MATRIALNAME" align="center" style="width: 150px">
                                                    物料
                                                </th>
                                                <th field="AMOUNT" align="center" style="width: 80px">
                                                    数量
                                                </th>
                                                <th field="UNITNAME" align="center" style="width: 80px">
                                                    单位
                                                </th>
                                                <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="60px">
                                                    &nbsp;
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="hiPlanID" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiMaterialList" runat="server" />
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        var materialList;
        //初始化
        $(function () {
            materialList = JSON.parse($("#hiMaterialList").val());
            $('#tblMaterialList').datagrid({
        });
        //
        $("#PLANID").change(function () {
            getProducePlanByID();
        });
        $('#tblMaterialList').datagrid("loadData", materialList);
    });

    //添加
    function add() {
        openAppWindow1('添加要货信息', "AddMaterialInfo.aspx", '360', '300');
    }


    function addMat(material) {
        if (existsJsonItem(materialList.rows, "MATRIALID", material.MATRIALID) == true) {
            MSI("提示", "已存在！");
            return;
        }

        material["DeleteAction"] = "deleteItem(\'" + material.MATRIALID + "\')";

        for (var i = 0; i < material.length; i++) {
            materialList.rows.push(material[i]);
        }

        $('#tblMaterialList').datagrid("loadData", materialList);
    }

    //保存校验
    function isValid() {
        //校验基本信息合法性
        if (isValidate() == false) {
            return false;
        }

        matList = $('#tblMaterialList').datagrid("getData");
        if (matList.rows.length == 0) {
            MSI("提示", "要货信息");
            return false;
        }

        $("#hiMaterialList").val(JSON.stringify(materialList.rows));

        return true;
    }

    //删除物料
    function deleteItem(id) {
        materialList = $('#tblMaterialList').datagrid("getData");
        materialList.rows = deleteJsonItem(materialList.rows, "MATRIALID", id);
        $('#tblMaterialList').datagrid("loadData", materialList);
    }

    function getProducePlanByID() {
        if ($("#PLANID").val() == "")
            return;
        $.ajax({
            url: 'GetSupplyDetailsHandler.ashx?type=PP&planID=' + $("#PLANID").val(),
            dataType: 'json',
            method: 'GET',
            success: function (data) {
                if (data.PID == "none") {
                    $("#PDNAME").val("");
                    $("#FNAME").val("");
                    $("#PLNAME").val("");
                    return;
                }
                $("#PDNAME").val(data.ProduceName);
                $("#FNAME").val(data.FactoryName);
                $("#PLNAME").val(data.PLName);
                addMat(data.Details);
            },
            error: function (ex) {
                alert('error:' + JSON.stringify(ex));
            }
        });
    }
    </script>
</body>
</html>
