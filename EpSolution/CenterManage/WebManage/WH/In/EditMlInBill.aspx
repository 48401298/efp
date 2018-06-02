<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditMlInBill.aspx.cs" Inherits="Manage.Web.WH.In.EditMlInBill" %>
<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑原材料入库单</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <script language=javascript type="text/javascript">
        //删除按钮
        function formatDeleteButton(value, rec) {
            if (value == "none") {
                return "";
            }
            else {
                return QLinkButtonHtml("删除", value);
            }
        }
    </script>
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
                        onclick="btCancel_Click" onclientclick="parent.closeAppWindow1();return false;" >返回</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="入库信息" style="padding:3px;">
                        <table cellpadding=0 cellspacing=0 >
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding=0 cellspacing=0 class="editTable">
                                       <tr>
                                        <th align=left nowrap="nowrap">入库单号</th><td>
                                           <asp:TextBox ID="BillNO" runat="server" ReadOnly="True"></asp:TextBox>
                                           </td> 
                                         <th align=left nowrap="nowrap">入库日期</th>
                                         <td>
                                             <asp:TextBox ID="BillDate" runat="server"></asp:TextBox>
                                           </td>                                     
                                        </tr>   
                                        <tr>
                                           <th align=left>供货单位</th><td>
                                            <asp:DropDownList ID="ProviderID" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                            <th align=left>入库方式</th>
                                         <td>
                                             <asp:DropDownList ID="InStockMode" runat="server">
                                             </asp:DropDownList>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>仓库</th><td>
                                            <asp:DropDownList ID="Warehouse" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                            <th align=left>交货人</th>
                                         <td>
                                             <asp:TextBox ID="DeliveryPerson" runat="server" MaxLength="15"></asp:TextBox>
                                            </td> 
                                        </tr>                 
                                        <tr>
                                           <th align=left>验收人</th><td>
                                            <asp:DropDownList ID="Receiver" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                            <th align=left nowrap="nowrap">仓库负责人</th>
                                         <td>
                                             <asp:DropDownList ID="WHHeader" runat="server">
                                             </asp:DropDownList>
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
                                    <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                        onclientclick="add();return false;">增加</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblmat" style="height:200px;width:700px" iconCls="icon-edit">
                                                <thead>
			                                        <tr>                                   
				                                        <th field="Seq" align="center">序号</th>
                                                        <th field="MatBarCode" align="center">识别码</th>
                                                        <th field="MatCode" align="center">货品编号</th>
                                                        <th field="MatName" align="center">货品名称</th>
                                                        <th field="ProduceDate" align="center">生产日期</th>
                                                        <th field="InSpecName" align="center">规格</th>
                                                        <th field="InAmount" align="center">数量</th>
                                                        <th field="UnitName" align="center">单位</th>                                                        
                                                        <th field="InPrice" align="center">单价</th>
                                                        <th field="InSum" align="center">金额</th>
                                                        <th field="SaveSite" align="center">仓位</th>
                                                        <th field="Remark" align="center">备注</th>
                                                        <th field="DeleteAction" align="center" formatter="formatDeleteButton" width="60">&nbsp;</th>
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
    <asp:HiddenField ID="HiUPDATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiMatList" runat="server" />
    <asp:HiddenField ID="hiMatID" runat="server" />
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>  
    <script language="javascript" type="text/javascript">
        var matList;
        //初始化
        $(function () {
            matList = JSON.parse($("#hiMatList").val());
            $('#tblmat').datagrid({
            });
            $('#tblmat').datagrid("loadData", matList);
        });

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

            for (var i = 0; i < matList.rows.length; i++) {
                matList.rows[i]["Seq"] = i + 1;
            }

            $('#tblmat').datagrid("loadData", matList);
        }

        //删除物料
        function deleteMat(id) {
            matList = $('#tblmat').datagrid("getData");

            matList.rows = deleteJsonItem(matList.rows, "ID", id);

            for (var i = 0; i < matList.rows.length; i++) {
                matList.rows[i]["Seq"] = i + 1;
            }

            $('#tblmat').datagrid("loadData", matList);
        }

        //保存校验
        function isValid() {
            //校验基本信息合法性
            if (isValidate() == false) {
                return false;
            }

            matList = $('#tblmat').datagrid("getData");
            if (matList.rows.length == 0) {
                MSI("提示", "请添加货品");
                return false;
            }

            $("#hiMatList").val(JSON.stringify(matList.rows));

            return true;
        }
    </script>  
    </body>
</html>
