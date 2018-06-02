<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditSchedulingInfo.aspx.cs" Inherits="Manage.Web.MES.Base.EditSchedulingInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑排班信息</title>
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
                    <div class="easyui-panel" title="排班信息" style="padding: 3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                            <th align="left">
                                                所属工厂
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="FAID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%" data-options="required:true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                生产线
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRID" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID" Width="100%" data-options="required:true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                班组
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="WOID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%" data-options="required:true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="WORKDATE" class="easyui-datebox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                工作时段开始
                                            </th>
                                            <td>
                                                <asp:TextBox ID="WORKSTART" class="easyui-validatebox" MaxLength="4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                工作时段结束
                                            </th>
                                            <td>
                                                <asp:TextBox ID="WORKEND" class="easyui-validatebox" MaxLength="4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                班次
                                            </th>
                                            <td>
                                                <asp:TextBox ID="SCHEDULINGORDER" class="easyui-numberbox" precision="0" MaxLength="4" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="REMARK" class="easyui-validatebox" MaxLength="100" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
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
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>
</body>
</html>
