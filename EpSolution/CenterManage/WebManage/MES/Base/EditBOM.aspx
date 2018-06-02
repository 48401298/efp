<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EditBOM.aspx.cs" Inherits="Manage.Web.MES.Base.EditBOM" %>
<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑产品BOM信息</title>
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
                    <div class="easyui-panel" title="产品BOM信息" style="padding: 3px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding="0" cellspacing="0" class="editTable">
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                产品信息
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRODUCEID" runat="server" Width="120px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                生产量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="Amount" runat="server" class="easyui-numberbox" data-options="min:0,precision:0"  Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap"  >
                                                主计量单位
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="MainUnit" runat="server" Width="120px">
                        </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
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
                                    <table id="tblbomDetail" style="height: 200px; width: 400px" iconcls="icon-edit">
                                        <thead>
                                            <tr>
                                                <th field="MATRIALNAME" align="center" style="width:150px">
                                                    物料
                                                </th>
                                                <th field="AMOUNT" align="center" style="width:80px">
                                                    数量
                                                </th>
                                                <th field="UNITNAME" align="center" style="width:80px">
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
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiBomDetailList" runat="server" />
    <asp:HiddenField ID="hiBomID" runat="server" />
    <uc1:appwindowcontrol id="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        var bomDetailList;
        //初始化
        $(function () {
            bomDetailList = JSON.parse($("#hiBomDetailList").val());
            $('#tblbomDetail').datagrid({
        });
        $('#tblbomDetail').datagrid("loadData", bomDetailList);
    });

    //添加
    function add() {
        openAppWindow1('添加产品BOM明细', "AddBOMDetail.aspx?bomID=" + $('#hiBomID').val(), '360', '300');
    }


    function addMat(bomDetail) {
        if (existsJsonItem(bomDetailList.rows, "MATRIALID", bomDetail.MATRIALID) == true) {
            MSI("提示", "已存在！");
            return;
        }

        bomDetail["DeleteAction"] = "deleteItem(\'" + bomDetail.MATRIALID + "\')";
        bomDetailList.rows.push(bomDetail);
        $('#tblbomDetail').datagrid("loadData", bomDetailList);
    }

    //保存校验
    function isValid() {
        //校验基本信息合法性
        if (isValidate() == false) {
            return false;
        }

        matList = $('#tblbomDetail').datagrid("getData");
        if (matList.rows.length == 0) {
            MSI("提示", "产品BOM明细不能为空");
            return false;
        }

        $("#hiBomDetailList").val(JSON.stringify(bomDetailList.rows));

        return true;
    }

    //删除物料
    function deleteItem(id) {
        bomDetailList = $('#tblbomDetail').datagrid("getData");
        bomDetailList.rows = deleteJsonItem(bomDetailList.rows, "MATRIALID", id);
        $('#tblbomDetail').datagrid("loadData", bomDetailList);
    }
    </script>
</body>
</html>
