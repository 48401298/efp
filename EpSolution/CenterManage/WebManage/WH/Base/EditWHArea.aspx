<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditWHArea.aspx.cs" Inherits="Manage.Web.WH.Base.EditWHArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑仓位信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-save'" runat="server" 
                        onclientclick="return isValid();" onclick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclientclick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="区域信息" style="padding:3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                        <th align=left>区域编号</th><td>
                                           <asp:TextBox ID="Code" class="easyui-validatebox" 
                                               data-options="required:true" MaxLength="5" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr> 
                                       <tr>
                                        <th align=left>区域名称</th><td>
                                           <asp:TextBox ID="Description" class="easyui-validatebox" 
                                               data-options="required:true" MaxLength="25" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr> 
                                        <tr>
                                            <th align=left>所属仓库</th>
                                            <td>

                                                <asp:DropDownList ID="Warehourse" runat="server" DataTextField="Description" 
                                                    DataValueField="ID" Enabled="False" Width="100%">
                                                </asp:DropDownList>

                                            </td>                                        
                                        </tr>   
                                        <tr>
                                           <th align=left>备注</th><td>
                                           <asp:TextBox ID="Remark" MaxLength="50" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
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
    <script language="javascript" language=javascript>
        $(function () {
        });
             
        function isValid() {
            //校验基本信息合法性
            if ($("#Code").val().trim() == "") {
                MSI("提示", "区域编号不能为空");
                return false;
            }

            if ($("#Description").val().trim() == "") {
                MSI("提示", "区域名称不能为空");
                return false;
            }
            if (isValidate() == false) {
                return false;
            }
            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="WHID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>    
    </body>
</html>
