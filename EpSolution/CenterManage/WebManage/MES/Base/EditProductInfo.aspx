<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProductInfo.aspx.cs" Inherits="Manage.Web.MES.Base.EditProductInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑产品信息</title>
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
                    <div class="easyui-panel" title="产品信息信息" style="padding: 3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                            <th align="left">
                                                产品编号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PCODE" class="easyui-validatebox" data-options="required:true" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                            <th align="left">
                                                产品名称
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PNAME" class="easyui-validatebox" data-options="required:true" runat="server" MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                工艺流程
                                            </th>
                                            <td>
                                               <asp:DropDownList ID="PRID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                             <th align="left">
                                                规格
                                            </th>
                                            <td>
                                                <asp:TextBox ID="SPECIFICATION" class="easyui-validatebox" runat="server"  MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <th align="left">
                                                制造商</th>
                                            <td>
                                                <asp:TextBox ID="Manufacturer" class="easyui-validatebox" runat="server"  
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                             <th align="left">
                                                 保质期</th>
                                            <td>
                                                <asp:TextBox ID="QualityPeriod" class="easyui-validatebox" runat="server"  
                                                    MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <th align="left">
                                                生产地址</th>
                                            <td colspan="3">
                                                <asp:TextBox ID="ProductionAddress" class="easyui-validatebox" runat="server"  
                                                    MaxLength="100" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <th align="left" nowrap=nowrap>
                                                生产许可证号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="ProductionLicense" class="easyui-validatebox" runat="server"  
                                                    MaxLength="25"></asp:TextBox>
                                            </td>
                                             <th align="left" nowrap=nowrap>
                                                 产品标准号</th>
                                            <td>
                                                <asp:TextBox ID="ProductStandard" class="easyui-validatebox" runat="server"  
                                                    MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td colspan=3>
                                                <asp:TextBox ID="REMARK" class="easyui-validatebox" runat="server"  
                                                    MaxLength="100" Width="100%"></asp:TextBox>
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
