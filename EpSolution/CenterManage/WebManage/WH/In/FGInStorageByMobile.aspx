<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="FGInStorageByMobile.aspx.cs" Inherits="Manage.Web.WH.In.FGInStorageByMobile" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产成品入库移动端</title>
    <meta name="viewport" content="width=device-width initial-scale=1.0; maximum-scale=1.0; user-scalable=no;">
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
        <table style="width:100%;">            
            <tr>
                <td>
                    <div class="easyui-panel" title="产成品入库" style="width:98%;overflow: hidden;">
                        <table cellpadding=0 cellspacing=0 width=100%>
                            <tr>
                                <td>
                                    <table  width=100%>
                                       <tr>
                                        <th nowrap="nowrap">产品条码</th><td>
                                           <asp:TextBox ID="MatBarCode" oninput="getMatByIDCode(this.value)" runat="server" class="easyui-validatebox" 
                                               Width="100%" 
                                               ></asp:TextBox>
                                           </td>                                        
                                        </tr>   
                                       <tr>
                                        <th nowrap="nowrap">产品</th>
                                            <td width="50%">
                                                <table cellpadding=0 cellspacing=0 style="border-style: none; border-width: 0px;width:100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="MatCode" oninput="getMatByMatCode(this.value)" runat="server" 
                                                                    class="easyui-validatebox" Width="100%" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="MatName" runat="server" class="easyui-validatebox" 
                                                                ReadOnly="True" Width="100%" ></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                           </td>                                        
                                        </tr>                                       
                                       <tr>
                                        <th nowrap="nowrap">数量</th>
                                            <td nowrap="nowrap">
                                                <table cellpadding=0 cellspacing=0 
                                                    style="border-style: none; border-width: 0px;width:100%">
                                                    <tr>
                                                        <td><asp:TextBox ID="InAmount" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="UnitCode" runat="server" ReadOnly="True" Width="100%"></asp:TextBox></td>                                                        
                                                    </tr>
                                                </table>
                                               </td>                                        
                                        </tr>
                                       <tr>
                                        <th nowrap="nowrap">规格</th>
                                            <td>
                                               <asp:TextBox ID="SpecCode" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                                               </td>                                        
                                        </tr>
                                       <tr>
                                        <th nowrap="nowrap">核算数量</th>
                                            <td nowrap="nowrap">
                                                <table cellpadding=0 cellspacing=0 style="border-style: none; border-width: 0px;width:100%">
                                                    <tr>
                                                        <td><asp:TextBox ID="MainUnitAmount" runat="server" class="easyui-validatebox" Width="100%" ReadOnly="True"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="MainUnit" runat="server" ReadOnly="True" Width="100%"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                               </td>                                        
                                        </tr>
                                       <tr>
                                        <th nowrap="nowrap">仓库</th>
                                            <td>
                                                <table>
                                                    <tr><td><asp:DropDownList ID="Warehouse" runat="server">
                                                    </asp:DropDownList></td><th nowrap="nowrap">仓位</th><td><asp:DropDownList ID="SaveSite" runat="server">
                                                    </asp:DropDownList></td></tr>
                                                </table>
                                               </td>                                        
                                        </tr>
                                        
                                        <tr>
                                        <td align=left colspan="2">&nbsp;
                                            <a id="btAdd" class="easyui-linkbutton" onclick="addMat()" data-options="iconCls:'icon-add'">添加</a>
                                        </td>
                                        </tr>                                                                                       
                                        <tr>
                                        <td align=left colspan="2">
                                            <table id="tblmat" style="height:100px;width:100%" iconCls="icon-edit">
                                                <thead>
			                                        <tr>                                   
				                                        <th field="MatBarCode" align="center" width="150">产品条码</th>
                                                        <th field="InAmount" align="center" width="60">数量</th>
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
            <tr>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-save'" runat="server" 
                        onclientclick="return isValid();" onclick="btSave_Click">保存</asp:LinkButton>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" onclick="btCancel_Click" >返回</asp:LinkButton>
                </td>
            </tr>
        </table>       
        
    </div>   
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <asp:HiddenField ID="hiMatList" runat="server" />
    <asp:HiddenField ID="hiMatID" runat="server" />
    </form> 
     <script language="javascript" type="text/javascript">
         var matList;

         $(function () {
             //绑定仓库选择事件
             $("#Warehouse").change(function () {
                 BindSite();
             });

             //绑定条码回车事件
             $('#MatBarCode').keydown(function (e) {
                 if (e.keyCode == 13) {
                     addMat();
                 }
             });

             matList = JSON.parse($("#hiMatList").val());
             $('#tblmat').datagrid({
             });
             $('#tblmat').datagrid("loadData", matList);
             $("#MatBarCode")[0].focus();
         });

         var selectMat;

         //根据货品识别码获取货品信息
         function getMatByIDCode(barCode) {
             if ($("#MatBarCode").val() == "")
                 return;
             $.ajax({
                 url: '../../Pub/GetMatInfoAjaxHandler.ashx?IDCode=' + barCode,
                 dataType: 'json',
                 method: 'GET',
                 success: function (data) {
                     if (data.MatCode == "none") {
                         $("#MatBarCode").val("");
                         $("#hiMatID").val("");
                         $("#MatCode").val("");
                         $("#MatName").val("");
                         $("#InAmount").val("");
                         $("#SpecCode").val("");
                         $("#UnitCode").val("");
                         $("#SpecCode").val("");
                         $("#MainUnit").val("");
                         $("#MainUnitAmount").val("");
                         MSI("提示", "您输入的条码号无效");
                         selectMat = null;
                         return;
                     }

                     if (data.CheckResult != "") {
                         MSI("提示", data.CheckResult);
                         return;
                     }

                     selectMat = data;
                     $("#hiMatID").val(data.ID);
                     $("#MatCode").val(data.MatCode);
                     $("#MatName").val(data.MatName);
                     $("#InAmount").val("1");
                     $("#SpecCode").val(data.OperateSpecName);
                     $("#UnitCode").val(data.OperateUnitName);
                     $("#MainUnit").val(data.UnitName);
                     $("#MainUnitAmount").val(data.MainUnitAmount);
                 },
                 error: function (ex) {
                     alert('error:' + JSON.stringify(ex));
                 }
             });
         }

         function clearInfo() {

         }

         //根据货品编号获取货品信息
         function getMatByMatCode(barCode) {
             if ($("#MatCode").val() == "") {
                 return;
             }
             $.ajax({
                 url: '../../Pub/GetMatInfoAjaxHandler.ashx?MatCode=' + barCode,
                 dataType: 'json',
                 method: 'GET',
                 success: function (data) {
                     if (data.MatCode == "none") {
                         $("#MatCode").val("");
                         $("#hiMatID").val("");
                         $("#MatCode").val("");
                         $("#MatName").val("");
                         $("#InAmount").val("");
                         $("#SpecCode").val("");
                         $("#UnitCode").val("");
                         $("#MainUnit").val("");
                         $("#MainUnitAmount").val("");
                         MSI("提示", "您输入的原料编号无效");
                         selectMat = null;
                         return;
                     }
                     selectMat = data;
                     $("#hiMatID").val(data.ID);
                     $("#MatCode").val(data.MatCode);
                     $("#MatName").val(data.MatName);
                     $("#SpecCode").val(data.SpecCode);
                     $("#UnitCode").val(data.UnitName);
                 },
                 error: function (ex) {
                     alert('error:' + JSON.stringify(ex));
                 }
             });
         }

         //增加
         function addMat() {
             matList = $('#tblmat').datagrid("getData");

             if ($("#MatCode").val() == "") {
                 MSI("提示", "原料编号不能为空");
                 return;
             }

             if (existsJsonItem(matList.rows, "ID", selectMat.BarCode) == true) {
                 MSI("提示", "已存在！");
                 return;
             }

             var mat = JSON.parse("{}");
             mat["MatID"] = selectMat.MatID;
             mat["MatBarCode"] = $("#MatBarCode").val();
             mat["ID"] = $("#MatBarCode").val();
             mat["MatCode"] = $("#MatCode").val();
             mat["InAmount"] = $("#InAmount").val();
             mat["MainUnitAmount"] = selectMat.MainUnitAmount;
             mat["MatSpec"] = selectMat.OperateSpecName;
             mat["UnitCode"] = selectMat.UnitCode;
             mat["DeleteAction"] = "deleteMat(\'" + mat["ID"] + "\')";
             matList.rows.push(mat);
             $('#tblmat').datagrid("loadData", matList);

             $("#MatBarCode").val("");
             $("#MatCode").val("");
             $("#MatName").val("");
             $("#InAmount").val("");
             $("#SpecCode").val("");
             $("#UnitCode").val("");
             $("#SpecCode").val("");
             $("#MainUnit").val("");
             $("#MainUnitAmount").val("");
             $("#MatBarCode")[0].focus();
         }

         //刷新库位
         function BindSite() {
             var whID = $("#Warehouse").val();
             if (whID == null || whID == "") {
                 return;
             }
             $.ajax({
                 url: '../../Pub/GetWHSitesHandler.ashx?whID=' + whID,
                 dataType: 'json',
                 method: 'GET',
                 success: function (data) {
                     var jsonObj = data;
                     $("#SaveSite").html("");
                     for (var j = 0; j < jsonObj.length; j++) {
                         var varItem = new Option(jsonObj[j].Description, jsonObj[j].ID);
                         document.getElementById("SaveSite").options.add(varItem);
                     }

                 },
                 error: function (ex) {
                     alert('error:' + JSON.stringify(ex));
                 }
             });
         }

         //删除物料
         function deleteMat(id) {
             matList = $('#tblmat').datagrid("getData");

             matList.rows = deleteJsonItem(matList.rows, "ID", id);

             $('#tblmat').datagrid("loadData", matList);
         }

         //校验基本信息合法性
         function isValid() {
             if (isValidate() == false) {
                 return false;
             }
             matList = $('#tblmat').datagrid("getData");
             if (matList.rows.length == 0) {
                 MSI("提示", "请添加原料");
                 return false;
             }

             $("#hiMatList").val(JSON.stringify(matList.rows));

             return true;
         }
    </script>  
    </body>
</html>
