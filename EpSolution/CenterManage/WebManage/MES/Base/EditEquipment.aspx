<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditEquipment.aspx.cs"
    Inherits="Manage.Web.MES.Base.EditEquipment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑设备信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
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
                    <div class="easyui-panel" title="设备信息信息" style="padding: 3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                            <th align="left">
                                                设备编号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="ECODE" class="easyui-validatebox" data-options="required:true" 
                                                    runat="server" MaxLength="10" Width="200px"></asp:TextBox>
                                                <asp:LinkButton ID="lbtPrintCode" runat="server" CssClass="easyui-linkbutton" 
                                                    onclick="lbtPrintCode_Click" onclientclick="return printCode();">打印</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                设备名称
                                            </th>
                                            <td>
                                                <asp:TextBox ID="ENAME" class="easyui-validatebox" data-options="required:true" 
                                                    runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                所属工厂
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="FACTORYPID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                所属生产线
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRODUCTLINEPID" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                设备品牌
                                            </th>
                                            <td>
                                                <asp:TextBox ID="EBRAND" class="easyui-validatebox" runat="server" 
                                                    MaxLength="20" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                设备型号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="ETYPE" class="easyui-validatebox" runat="server" 
                                                    MaxLength="20" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                生产日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="MDATE" class="easyui-datebox" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                供应商及地址
                                            </th>
                                            <td>
                                                <asp:TextBox ID="SUPPLIERADDR" class="easyui-validatebox" runat="server" 
                                                    MaxLength="50" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                供应商联系电话
                                            </th>
                                            <td>
                                                <asp:TextBox ID="SUPPLIERCONTACT" class="easyui-validatebox" 
                                                    runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                识别码
                                            </th>
                                            <td>
                                                <asp:TextBox ID="BARCODE" class="easyui-validatebox" runat="server"  
                                                    MaxLength="32" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="Remark" CssClass="easyui-validatebox" runat="server"  
                                                    MaxLength="100" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
        </td> </tr> </table>
    </div>
    </td> </tr> </table> </div>
    <script language="javascript" language="javascript">
        $(function () {
        });

        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            return true;
        }

        function printCode() {
            if ($("#ECODE").val() == "") {
                MSI("提示","设备编码不能为空")
                return false;
            }
            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <iframe id="frmPrint" style="display:none;"></iframe>
    </form>
</body>
</html>
