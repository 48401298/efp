<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditQualityCheck.aspx.cs"
    Inherits="Manage.Web.WH.Stock.EditQualityCheck" ValidateRequest="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>编辑质检信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script src="../../JS/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../JS/jquery.iframe-transport.js" type="text/javascript"></script>
    <script src="../../JS/jquery.fileupload.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //删除按钮
        function formatDeleteButton(value, rec) {

            return QLinkButtonHtml("删除", value);
        }

        function formatDetailButton(value, rec) {

            return QLinkButtonHtml("查看", value);
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
                                            <th align="left" width="80px">
                                                质检单号
                                            </th>
                                            <td>
                                                <asp:TextBox ID="BillNO" class="easyui-validatebox" data-options="required:true"
                                                    runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                            </td>
                                            <th align="left" width="80px">
                                                检查日期
                                            </th>
                                            <td>
                                                <asp:TextBox ID="CheckDate" CssClass="easyui-datebox" data-options="required:true"
                                                    runat="server" Width="100px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                入库单号
                                            </th>
                                            <td>
                                                <asp:DropDownList ID="BillNoList" runat="server" data-options="required:true" Width="105px">
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="lbtView" runat="server" 
                                                    onclientclick="viewInStockBill();return false;">查看</asp:LinkButton>
                                            </td>
                                            <th align="left">
                                                
                                            </th>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                质检员
                                            </th>
                                            <td>
                                                <asp:TextBox ID="CheckPerson" class="easyui-validatebox" runat="server" MaxLength="50"
                                                    Width="100px" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <th align="left">
                                                质检结果
                                            </th>
                                            <td>
                                                <asp:RadioButton ID="RB1" runat="server" Text="合格" GroupName="cr" Checked="true" />
                                                <asp:RadioButton ID="RB2" runat="server" Text="不合格" GroupName="cr" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th align="left">
                                                质检描述
                                            </th>
                                            <td colspan="3">
                                                <asp:TextBox ID="REMARK" class="easyui-validatebox" runat="server" MaxLength="2000"
                                                    Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                                附件名称 &nbsp<asp:TextBox ID="addName" runat="server" class="easyui-validatebox" Width="100px"></asp:TextBox>
                                                <input type="file" name="file" id="btnFileUpload" /><a id="btAdd" class="easyui-linkbutton"
                                                    data-options="iconCls:'icon-add'">添加</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                                <table id="tblp" style="height: 170px" iconcls="icon-edit">
                                                    <thead>
                                                        <tr>
                                                            <th field="SEQ" align="center" width="40">
                                                                序号
                                                            </th>
                                                            <th field="FileAlias" align="center" width="130">
                                                                附件名称
                                                            </th>
                                                            <th field="DetailAction" align="center" formatter="formatDetailButton" width="60">
                                                                &nbsp;
                                                            </th>
                                                            <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="60">
                                                                &nbsp;
                                                            </th>
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
        var seq = 0;
        var filename = "";
        $(function () {
            pList = JSON.parse($("#hiPList").val());
            $('#tblp').datagrid({});
            $('#tblp').datagrid("loadData", pList);
            

            $('#btnFileUpload').fileupload({
                url: '../../Pub/UploadHandler.ashx?upload=start',
                autoUpload: false,
                replaceFileInput: false,
                done: function (e, data) {
                    $.each(data.result, function (index, file) {
                        $('<p/>').text(file.name).appendTo(document.body);
                    });
                },
                add: function (e, data) {
                    $('#btAdd').off('click').on('click', function () {                        
                        data.submit();
                    });
                },
                success: function (response, status) {
                    filename = response.toString();
                    addFile(filename);
                    $("#addName").val("");
                    $("#btnFileUpload").val("");
                },
                error: function (error) {
                    alert('上传失败！')
                }
            });
        });


        //增加
        function addFile(filename) {
            if (filename == "") {
                alert('请上传一个附件！')
                return false;
            }
            if ($("#addName").val() == "") {
                alert('请输入文件名！')
                return false;
            }
            var mat = JSON.parse("{}");
            mat["CheckID"] = $("#hiID").val();
            mat["FileAlias"] = $("#addName").val();
            seq++;
            mat["SEQ"] = seq;
            mat["FileName"] = filename;
            mat["DetailAction"] = "viewF(\'" + mat["FileName"] + "\')";
            mat["DeleteAction"] = "deleteF(\'" + mat["FileName"] + "\')";
            pList.rows.push(mat);
            $('#tblp').datagrid("loadData", pList);
            $("#tbAdd").val("");
        }

        function deleteF(id) {
            matList = $('#tblp').datagrid("getData");

            matList.rows = deleteJsonItem(matList.rows, "FileName", id);
            $('#tblp').datagrid("loadData", matList);
        }

        function viewF(id) {
            var curWwwPath = window.document.location.href;
            var pathName = window.document.location.pathname;
            var pos = curWwwPath.indexOf(pathName);
            var localhostPaht = curWwwPath.substring(0, pos) + "/UploadedFiles/" + id;
            window.open(localhostPaht);
        }

        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            $("#hiPList").val(JSON.stringify(pList.rows));
            return true;
        }

        //查看入库单
        function viewInStockBill() {
            if ($("#BillNoList").val() == "") {
                MSI("提示", "请选择入库单");
                return;
            }
            window.open("../In/ViewInStockBill.aspx?id=" + $("#BillNoList").val());
            return false;
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="hiPList" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    </form>
</body>
</html>
