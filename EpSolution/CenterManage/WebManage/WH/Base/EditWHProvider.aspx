<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditWHProvider.aspx.cs" Inherits="Manage.Web.WH.Base.EditWHProvider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑供货单位信息</title>
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
                    <div class="easyui-panel" title="供货单位信息" style="padding:3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                       <tr>
                                        <th align=left>供应商</th><td>
                                           <asp:TextBox ID="ProviderName" class="easyui-validatebox" 
                                               data-options="required:true" MaxLength="50" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>所在地区</th><td>
                                           <asp:TextBox ID="AreaCode" CssClass="easyui-validatebox" MaxLength="50" runat="server"></asp:TextBox>
                                           </td>                                       
                                        </tr>  
                                                        
                                       <tr>
                                        <th align=left>地址</th><td colspan="3">
                                           <asp:TextBox ID="Address" class="easyui-validatebox" 
                                               runat="server" MaxLength="100" Width="100%"></asp:TextBox>
                                           </td> 
                                        </tr>  
                                                        
                                       <tr>
                                        <th align=left>邮政编码</th><td>
                                           <asp:TextBox ID="Postalcode" CssClass="easyui-validatebox" MaxLength="15" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>联系人</th><td>
                                           <asp:TextBox ID="Linkman" CssClass="easyui-validatebox" MaxLength="20" runat="server"></asp:TextBox>
                                           </td>                                       
                                        </tr>  
                                                        
                                       <tr>
                                        <th align=left>联系电话</th><td>
                                           <asp:TextBox ID="Telephone" CssClass="easyui-validatebox" MaxLength="30" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>手机</th><td>
                                           <asp:TextBox ID="Mobilephone" CssClass="easyui-validatebox" MaxLength="30" runat="server"></asp:TextBox>
                                           </td>                                       
                                        </tr>  
                                                        
                                       <tr>
                                        <th align=left>传真</th><td>
                                           <asp:TextBox ID="Fax" CssClass="easyui-validatebox" MaxLength="30" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>电子邮箱</th><td>
                                           <asp:TextBox ID="Email" CssClass="easyui-validatebox" MaxLength="50" runat="server"></asp:TextBox>
                                           </td>                                       
                                        </tr>  
                                                        
                                       <tr>
                                        <th align=left>公司主页</th><td>
                                           <asp:TextBox ID="WwwAddress" CssClass="easyui-validatebox" MaxLength="50" runat="server"></asp:TextBox>
                                           </td> 
                                           <th align=left>备注</th><td>
                                           <asp:TextBox ID="Remark" CssClass="easyui-validatebox" MaxLength="100" runat="server"></asp:TextBox>
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
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>    
    </body>
</html>
