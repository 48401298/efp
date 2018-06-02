<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWorkGroup.aspx.cs" Inherits="Manage.Web.MES.Base.EditWorkGroup" %>


<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑班组信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script language=javascript type="text/javascript">
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
                    <div class="easyui-panel" title="班组信息" style="padding: 3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                        <tr>
                                            <th align="left">
                                                班组名称
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PNAME" class="easyui-validatebox" data-options="required:true" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                负责人
                                            </th>
                                            <td>
                                                <asp:TextBox ID="PERSONINCHARGE" class="easyui-validatebox" runat="server" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                所属工厂
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="FAID" runat="server" DataTextField="PNAME" 
                                                    DataValueField="PID" Width="100%">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                备注
                                            </th>
                                            <td>
                                                <asp:TextBox ID="REMARK" class="easyui-validatebox" runat="server" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td align=left colspan="2"><asp:TextBox ID="tbAdd" runat="server" class="easyui-validatebox" ></asp:TextBox>
                                               <a id="btAdd" class="easyui-linkbutton" onclick="addMat()" data-options="iconCls:'icon-add'">添加</a>
                                        </td>
                                        </tr>                                                                                       
                                        <tr>
                                        <td align=left colspan="2">
                                            <table id="tblp" style="height:160px" iconCls="icon-edit">
                                                <thead>
			                                        <tr>                                   
				                                        <th field="PEID" align="center" width="130">班组人员</th>
                                                        <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="60">&nbsp;</th>
			                                        </tr>			                    
		                                        </thead>
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
    <script language="javascript" type="text/javascript">
        var pList;

        $(function () {
            pList = JSON.parse($("#hiPList").val());
            $('#tblp').datagrid({});
            $('#tblp').datagrid("loadData", pList);
         });


         //增加
         function addMat() {
             var mat = JSON.parse("{}");
             mat["PID"] = $("#hiID").val();
             mat["PEID"] = $("#tbAdd").val();
             mat["DeleteAction"] = "deleteMat(\'" + mat["MatBarCode"] + "\')";
             pList.rows.push(mat);
             $('#tblp').datagrid("loadData", pList);
             $("#tbAdd").val("");
         }

        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            $("#hiPList").val(JSON.stringify(pList.rows));
            return true;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="hiPList" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>
</body>
</html>
