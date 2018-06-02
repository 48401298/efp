<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTrackResource.aspx.cs" Inherits="Manage.Web.MES.TrackManage.SetTrackResource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置追溯资源</title>
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
                    <div class="easyui-panel" title="产品批次信息" style="padding: 3px;">
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
                                                    DataValueField="PID" Width="100%" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <th align="left">
                                                所属生产线
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRID" runat="server" DataTextField="PLNAME" 
                                                    DataValueField="PID" Width="100%" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <th align="left">
                                                产品
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="PRODUCTIONID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%" Enabled="False">
                                                </asp:DropDownList>
                                            </td>
                                            <th align="left">
                                                日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PLANDATE" runat="server" 
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>                                       
                                        <tr>
                                            <th align="left">
                                                计划产量
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PLANAMOUNT" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
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
                                                <asp:TextBox ID="BATCHNUMBER" class="easyui-validatebox" runat="server" 
                                                    MaxLength="30" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="REMARK" CssClass="easyui-validatebox" MaxLength="100" 
                                                    runat="server" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        视频资源</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    视频名称</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    视频文件</td>
                                                                <td>
                                                                    <asp:FileUpload ID="FileUploadVideo" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btVideoAdd" runat="server" CssClass="easyui-linkbutton" 
                                                                        data-options="iconCls:'icon-add'" OnClick="btVideoAdd_Click" OnClientClick="add();return false;">增加</asp:LinkButton>
                                                                    <asp:LinkButton ID="btVideoDelete" runat="server" OnClick="btVideoDelete_Click" CssClass="easyui-linkbutton" 
                                                                        data-options="iconCls:'icon-delete'" 
                                                                        OnClientClick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;<asp:GridView 
                                                            ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxVideo" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="序号">
                                                                <HeaderStyle Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="VideoName" HeaderText="视频名称" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        图片资源</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    图片名称</td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    图片文件</td>
                                                                <td>
                                                                    <asp:FileUpload ID="FileUploadImage" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btImageAdd" runat="server" CssClass="easyui-linkbutton" 
                                                                        data-options="iconCls:'icon-add'" OnClick="btImageAdd_Click" OnClientClick="add();return false;">增加</asp:LinkButton>
                                                                    <asp:LinkButton ID="btImageDelete" runat="server" OnClick="btImageDelete_Click" CssClass="easyui-linkbutton" 
                                                                        data-options="iconCls:'icon-delete'" 
                                                                        OnClientClick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                            </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;<asp:GridView 
                                                            ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbxVideo" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="40px" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="序号">
                                                                <HeaderStyle Width="80px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="VideoName" HeaderText="视频名称" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
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
