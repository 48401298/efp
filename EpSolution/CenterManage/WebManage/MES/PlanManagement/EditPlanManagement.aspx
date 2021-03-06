﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPlanManagement.aspx.cs" Inherits="Manage.Web.MES.PlanManagement.EditPlanManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑生产计划</title>
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
                    <div class="easyui-panel" title="生产计划" style="padding: 3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
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
                                                <asp:DropDownList ID="PRID" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                产品
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRODUCTIONID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PLANDATE" class="easyui-datebox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                计划产量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PLANAMOUNT" runat="server" class="easyui-numberbox"  data-options="min:0,max:100000,precision:2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                实际产量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="FACTAMOUNT" runat="server" class="easyui-numberbox"  data-options="min:0,max:100000,precision:2" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                批次号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="BATCHNUMBER" class="easyui-validatebox" runat="server" MaxLength="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                状态
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="Status" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID" Width="100%">
                                                    <asp:ListItem Text="未完成" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="完成" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="REMARK" CssClass="easyui-validatebox" MaxLength="100" runat="server"></asp:TextBox>
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
