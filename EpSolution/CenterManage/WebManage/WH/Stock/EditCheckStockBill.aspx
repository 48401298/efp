<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditCheckStockBill.aspx.cs" Inherits="Manage.Web.WH.Stock.EditCheckStockBill" %>
<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑库存盘点单</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script language=javascript type="text/javascript">
        
    </script>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btBuild" CssClass="easyui-linkbutton" 
                        runat="server" onclick="btBuild_Click">生成</asp:LinkButton>
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-save'" runat="server" 
                        onclientclick="return isValid();" onclick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btConfirm" CssClass="easyui-linkbutton" 
                        runat="server" onclientclick="return isCheck();" onclick="btConfirm_Click" 
                        Visible="False">确认</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclientclick="parent.closeAppWindow1();return false;" >返回</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="盘点信息" style="padding:3px;">
                        <table cellpadding=0 cellspacing=0 >
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding=0 cellspacing=0 class="editTable">
                                       <tr>
                                        <th align=left nowrap="nowrap">盘点单号</th><td>
                                           <asp:TextBox ID="BillNO" runat="server" ReadOnly="True"></asp:TextBox>
                                           </td> 
                                         <th align=left nowrap="nowrap">盘点日期</th>
                                         <td>
                                             <asp:TextBox ID="BillDate" runat="server"></asp:TextBox>
                                           </td>                                     
                                        </tr>   
                                        <tr>
                                           <th align=left>仓库</th><td>
                                            <asp:DropDownList ID="Warehouse" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                            <th align=left>存储区域</th>
                                         <td>                                
                                            <asp:DropDownList ID="AreaID" runat="server">
                                            </asp:DropDownList>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left nowrap="nowrap">盘点负责人</th><td>
                                             <asp:DropDownList ID="CheckHeader" runat="server" Enabled="False">
                                             </asp:DropDownList>
                                            </td>
                                            <th align=left nowrap="nowrap">是否确认</th>
                                         <td>
                                           <asp:TextBox ID="IsConfirmName" runat="server" ReadOnly="True"></asp:TextBox>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>备注</th><td colspan="3">
                                            <asp:TextBox ID="Remark" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>                 
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblmat" class="easyui-datagrid"  style="height:200px;width:600px" iconCls="icon-edit"
                                    data-options="iconCls: 'icon-edit',singleSelect: true,method:'get',onClickCell: onClickCell">
                                                <thead>
			                                        <tr>                                   
				                                        <th field="Seq" align="center">序号</th>
                                                        <th field="SaveSiteName" align="center">仓位</th>
                                                        <th field="IDCode" align="center">识别码</th>
                                                        <th field="MatCode" align="center">货品编号</th>
                                                        <th field="MatName" align="center">货品名称</th>
                                                        <th field="UnitName" align="center">计量单位</th>
                                                        <th field="StockAmount" align="right">库存数量</th> 
                                                        <th data-options="field:'FactAmount',width:80,align:'right',editor:'numberbox'">实际数量</th> 
                                                        <th data-options="field:'ProfitAmount',width:80,align:'right',editor:'numberbox'">盘盈数量</th> 
                                                        <th data-options="field:'LossAmount',width:80,align:'right',editor:'numberbox'"ign="right">盘亏数量</th> 
                                                        <th data-options="field:'Remark',width:100,editor:'text'">备注</th>
			                                        </tr>			                    
		                                        </thead>
	                                 </table>
                                </td>
                            </tr>
                         </table>
                    </div>
                </td>
            </tr>
        </table>
        
    </div>    
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiCheckList" runat="server" />
    <asp:HiddenField ID="IsConfirm" runat="server" />
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>  
    <script language="javascript" type="text/javascript">
        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var opts = $(this).datagrid('options');
                    var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        });

        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#tblmat').datagrid('validateRow', editIndex)) {
                $('#tblmat').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            if (endEditing()) {
                $('#tblmat').datagrid('selectRow', index)
						.datagrid('editCell', { index: index, field: field });
                editIndex = index;
            }
        }

        var matList;
        //初始化
        $(function () {
            matList = JSON.parse($("#hiCheckList").val());
            $('#tblmat').datagrid({
            });
            $('#tblmat').datagrid("loadData", matList);

            //绑定仓库选择事件
            $("#Warehouse").change(function () {
                BindArea();
            });
        });

        //刷新库位
        function BindArea() {
            var whID = $("#Warehouse").val();
            if (whID == null || whID == "") {
                return;
            }
            $.ajax({
                url: '../../Pub/GetWHAreaHandler.ashx?whID=' + whID,
                dataType: 'json',
                method: 'GET',
                success: function (data) {
                    var jsonObj = data;
                    $("#AreaID").html("");
                    for (var j = 0; j < jsonObj.length; j++) {
                        var varItem = new Option(jsonObj[j].Description, jsonObj[j].ID);
                        document.getElementById("AreaID").options.add(varItem);
                    }

                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }

        //添加
        function add() {
            openAppWindow1('添加货品', "AddMatAmount.aspx?whID=" + $('#Warehouse').val(), '360', '300');
        }


        function addMat(mat) {
            if (existsJsonItem(matList.rows, "ID", mat.ID) == true) {
                MSI("提示", "已存在！");
                return;
            }

            mat["DeleteAction"] = "deleteMat(\'" + mat.ID + "\')";
            matList.rows.push(mat);
            $('#tblmat').datagrid("loadData", matList);
        }

        //保存校验
        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }
            $('#tblmat').datagrid('acceptChanges');

            matList = $('#tblmat').datagrid("getData");

            $("#hiCheckList").val(JSON.stringify(matList.rows));

            return true;
        }

        function isCheck() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }

            var r = confirm('确定盘点完成吗');
            if (r == false)
                return r;

            $('#tblmat').datagrid('acceptChanges');

            matList = $('#tblmat').datagrid("getData");

            $("#hiCheckList").val(JSON.stringify(matList.rows));

            return true;
        }
    </script>  
    </body>
</html>
