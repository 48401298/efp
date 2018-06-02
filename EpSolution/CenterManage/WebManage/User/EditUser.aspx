<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Manage.Web.User.EditUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑用户信息</title>
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
                    <div class="easyui-panel" title="用户信息" style="padding:3px;">
                        <table >
                            <tr>
                                <td>
                                    <table class="editTable" cellpadding=0 cellspacing=0>
                                       <tr>
                                           <th>用户名</th>
                                           <td>
                                               <asp:TextBox ID="LoginUserID" class="easyui-validatebox" 
                                                data-options="required:true" MaxLength="20" runat="server"></asp:TextBox>
                                           </td>
                                           <th>用户姓名</th>
                                           <td>
                                                <asp:TextBox ID="UserName" MaxLength="50" data-options="required:true" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr>   
                                        <tr>
                                           <th>密码</th>
                                           <td>
                                               <asp:TextBox ID="PassWord" class="easyui-validatebox" 
                                                data-options="required:true" MaxLength="20" runat="server"></asp:TextBox>
                                           </td>
                                            <th>重复输入</th>
                                            <td>
                                                <asp:TextBox ID="RPassWord" MaxLength="20" data-options="required:true" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>   
                                        <tr>
                                            <th>所在机构</th><td>
                                                <input ID="OrganID" runat=server class="easyui-combotree" data-options="url:'GetOrgansHandler.ashx',method:'get',required:true" />
                                           </td>
                                            <th>办公电话</th><td>
                                                <asp:TextBox ID="Tel" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>移动电话</th>
                                            <td>
                                               <asp:TextBox ID="MobileTel" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                            </td>
                                            <th>电子邮件</th><td>
                                                <asp:TextBox ID="Email" MaxLength="20" CssClass="easyui-validatebox" runat="server"></asp:TextBox>
                                           </td>
                                        </tr> 
                                        <tr>
                                            <th>备注</th>
                                            <td colspan=3><asp:TextBox ID="Remark" MaxLength="50" CssClass="easyui-validatebox" runat="server"></asp:TextBox></td>
                                        </tr>              
                                    </table>
                                </td>
                            </tr>       
                            <tr>
                                <td>
                                    <div class="easyui-panel" title="系统角色" style="height:200px;padding:3px;">
                                        <ul id="roleTree" class="easyui-tree" data-options="checkbox:true"></ul>
                                    </div>
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
            var treeData = JSON.parse(document.getElementById("HiRoleList").value);
            $('#roleTree').tree('loadData', treeData);
        });
             
        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }

            //获取选中权限
            var nodes = $('#roleTree').tree('getChecked');
            var s = '';
            for (var i = 0; i < nodes.length; i++) {
                if (s != '') s += ',';
                s += nodes[i].id;
            }
            document.getElementById("HiSelectedRoleList").value = s;


            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiRoleList" runat="server" />
    <asp:HiddenField ID="HiSelectedRoleList" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="IsStop" runat="server" />
    </form>    
    </body>
</html>
