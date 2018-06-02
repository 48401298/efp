<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditStockLimit.aspx.cs" Inherits="Manage.Web.WH.Base.EditStockLimit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑库存预警设置</title>
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
                    <div class="easyui-panel" title="库存预警设置" style="padding:3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                        <th align=left>货品</th><td>
                                           <asp:DropDownList ID="MatID" runat="server">
                                           </asp:DropDownList>                                        
                                        </tr>
                                         <tr>
                                            <th align=left>计量单位</th>
                                            <td>

                                                <asp:DropDownList ID="UnitID" runat="server" DataTextField="Description" 
                                                    DataValueField="ID" Width="100%">
                                                </asp:DropDownList>

                                            </td>                                        
                                        </tr>    
                                       <tr>
                                        <th align=left>库存上线</th><td>
                                           <asp:TextBox ID="MaxAmount" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr> 
                                        <tr>
                                        <th align=left>库存下线</th><td>
                                           <asp:TextBox ID="MinAmount" class="easyui-validatebox" 
                                               data-options="" runat="server"></asp:TextBox>
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
                                           <asp:TextBox ID="Remark" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
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
