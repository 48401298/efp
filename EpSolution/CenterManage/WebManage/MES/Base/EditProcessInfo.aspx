<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" validateRequest="false" CodeBehind="EditProcessInfo.aspx.cs"
    Inherits="Manage.Web.MES.Base.EditProcessInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑工序信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script language="javascript" type="text/javascript">
        //删除按钮
        function formatDeleteButton(value, rec) {

            return QLinkButtonHtml("删除", value);
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
                    <div class="easyui-panel" title="工序信息" style="padding: 3px;">
                        <table class="editTable">
                            <tr>
                                <th align="left">
                                    工序编号
                                </th>
                                <td>
                                    <asp:TextBox ID="PCODE" class="easyui-validatebox" data-options="required:true" runat="server"></asp:TextBox>
                                </td>
                                <th align="left">
                                    工序名称
                                </th>
                                <td>
                                    <asp:TextBox ID="PNAME" class="easyui-validatebox" data-options="required:true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th align="left" nowrap=nowrap>
                                    加工时间(分钟)
                                </th>
                                <td>
                                    <asp:TextBox ID="PTIME" class="easyui-validatebox" Text="0" runat="server"></asp:TextBox>
                                </td>
                                <th align="left" nowrap=nowrap>
                                    所属工厂
                                </th>
                                <td>
                                    <asp:DropDownList ID="FACTORYPID" runat="server" DataTextField="PNAME" DataValueField="PID"
                                        Width="100%">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <th align="left">
                                    所属生产线
                                </th>
                                <td>
                                    <asp:DropDownList ID="PRODUCTLINEPID" runat="server" DataTextField="PLNAME" DataValueField="PID"
                                        Width="100%">
                                    </asp:DropDownList>
                                </td>
                                <th align="left">
                                    序号
                                </th>
                                <td>
                                    <asp:TextBox ID="SEQ" runat="server" class="easyui-numberbox" data-options="min:0,max:100,precision:0">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th align="left">
                                    备注
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="REMARK" class="easyui-validatebox" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div id="tabdetail" class="easyui-tabs" style="width: 400px; height: 200px">
                            <div title="相关工位" style="padding: 5px">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="STID" runat="server" DataTextField="WSNAME" DataValueField="PID"
                                                data-options="required:true">
                                            </asp:DropDownList>
                                            <a id="btAddST" class="easyui-linkbutton" onclick="addWS()" data-options="iconCls:'icon-add'">
                                                添加</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tbSt" style="height: 80px" iconcls="icon-edit">
                                                <thead>
                                                    <tr>
                                                        <th field="STNAME" align="center" width="200px">
                                                            工位
                                                        </th>
                                                        <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="40px">
                                                            &nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div title="相关设备" style="padding: 5px">
                                <table width="100%">
                                    <tr>
                                        <td align="left" colspan="4">
                                            <asp:DropDownList ID="EQID" runat="server" DataTextField="ENAME" DataValueField="PID"
                                                data-options="required:true">
                                            </asp:DropDownList>
                                            <a id="btAddEQ" class="easyui-linkbutton" onclick="addEQ()" data-options="iconCls:'icon-add'">
                                                添加</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="tbEq" style="height: 80px" iconcls="icon-edit">
                                                <thead>
                                                    <tr>
                                                        <th field="EQNAME" align="center" width="200px">
                                                            设备
                                                        </th>
                                                        <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="40px">
                                                            &nbsp;
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        var wsList;
        var eqList;

        $(function () {
            wsList = JSON.parse($("#hiWSList").val());
            $('#tbSt').datagrid({});
            $('#tbSt').datagrid("loadData", wsList);
            pList = JSON.parse($("#hiEQList").val());
            $('#tbEq').datagrid({});
            $('#tbEq').datagrid("loadData", pList);

        });


        //增加
        function addWS() {
            if ($("#STID").val() == '') {
                alert('请选择一个工位');
                return;
            }
            var ws = JSON.parse("{}");
            ws["PRID"] = $("#hiID").val();
            ws["STID"] = $("#STID").val();
            ws["STNAME"] = $("#STID").find("option:selected").text();
            ws["DeleteAction"] = "deleteWS(\'" + ws["STID"] + "\')";
            wsList.rows.push(ws);
            $('#tbSt').datagrid("loadData", wsList);
        }

        //增加
        function addEQ() {
            if ($("#EQID").val() == '') {
                alert('请选择一个设备');
                return;
            }
            var eq = JSON.parse("{}");
            eq["PRID"] = $("#hiID").val();
            eq["EQID"] = $("#EQID").val();
            eq["EQNAME"] = $("#EQID").find("option:selected").text();
            eq["DeleteAction"] = "deleteEQ(\'" + eq["EQID"] + "\')";
            
            pList.rows.push(eq);
            $('#tbEq').datagrid("loadData", pList);
        }

        function deleteWS(id) {  //删除操作
            wsList = $('#tbSt').datagrid("getData");

            wsList.rows = deleteJsonItem(wsList.rows, "STID", id);

            $('#tbSt').datagrid("loadData", wsList);
        }

        function deleteEQ(id) {  //删除操作  
            pList = $('#tbEq').datagrid("getData");

            pList.rows = deleteJsonItem(pList.rows, "EQID", id);

            $('#tbEq').datagrid("loadData", pList);
        }

        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            $("#hiWSList").val(JSON.stringify(wsList.rows));
            $("#hiEQList").val(JSON.stringify(pList.rows));
            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="hiWSList" runat="server" />
    <asp:HiddenField ID="hiEQList" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="HiFLOWID" runat="server" />
    </form>
</body>
</html>
