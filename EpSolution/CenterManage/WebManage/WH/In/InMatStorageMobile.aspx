<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="InMatStorageMobile.aspx.cs" Inherits="Manage.Web.WH.In.InMatStorageMobile" %>

<!DOCTYPE html PUBLIC "-//WAPFORUM//DTD XHTML Mobile 1.0//EN" "http://www.wapforum.org/DTD/xhtml-mobile10.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入库(数量)</title>
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
                    <div class="easyui-panel" title="入库(数量)" style="width:98%; overflow: hidden;">
                        <table cellpadding=0 cellspacing=0 width=100%>
                            <tr>
                                <td>
                                    <table width=100%>               
                                    <tr>                        
                                        <th nowrap="nowrap">货品</th>
                                            <td width="50%">
                                                <table cellpadding=0 cellspacing=0 style="border-style: none; border-width: 0px;width:100%">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="MatCode" oninput="getMatByMatCode(this.value)" runat="server" 
                                                                    class="easyui-validatebox" Width="100%"></asp:TextBox>
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
                                            <td nowrap="nowrap" style="width:100%">
                                                <table cellpadding=0 cellspacing=0 style="border-style: none; border-width: 0px;width:100%">
                                                    <tr>
                                                        <td style="width:50%"><asp:TextBox ID="MainUnitAmount" runat="server" class="easyui-numberbox" Width="100%"></asp:TextBox></td>
                                                        <td style="width:50%"><asp:TextBox ID="MainUnit" runat="server" ReadOnly="True" Width="100%"></asp:TextBox></td>                                                        
                                                    </tr>
                                                </table>
                                               </td>                                        
                                        </tr>
                                       <tr>
                                        <th nowrap="nowrap">仓库</th>
                                            <td>
                                                <table cellpadding=0 cellspacing=0>
                                                    <tr><td><asp:DropDownList ID="Warehouse" runat="server">
                                                    </asp:DropDownList></td><th nowrap="nowrap">仓位</th><td><asp:DropDownList ID="SaveSite" runat="server">
                                                    </asp:DropDownList></td></tr>
                                                </table>
                                               </td>                                        
                                        </tr>
                                        
                                       <tr>
                                        <th nowrap="nowrap">入库方式</th>
                                            <td>
                                                <asp:DropDownList ID="InStockMode" runat="server">
                                                </asp:DropDownList>
                                               </td>                                        
                                        </tr>
                                        
                                        <tr>
                                        <td align=left colspan="2">&nbsp; <a id="btAdd" class="easyui-linkbutton" onclick="addMat()" data-options="iconCls:'icon-add'">添加</a>
                                        </td>
                                        </tr>                                                                                       
                                        <tr>
                                        <td align=left colspan="2">
                                            <table id="tblmat" style="height:100px;width:100%" iconCls="icon-edit">
                                                <thead>
			                                        <tr>                                   
				                                        <th field="MatCode" align="center" width="150">货品编号</th>
                                                        <th field="MainUnitAmount" align="center" width="60">数量</th>
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
    <asp:HiddenField ID="HiUnit" runat="server" />
    </form> 
     <script language="javascript" type="text/javascript">
         var matList;

         $(function () {
             //绑定仓库选择事件
             $("#Warehouse").change(function () {
                 BindSite();
             });

             //绑定货品号回车事件
             $('#MatCode').keydown(function (e) {
                 if (e.keyCode == 13) {
                     addMat();
                 }
             });

             matList = JSON.parse($("#hiMatList").val());
             $('#tblmat').datagrid({
             });
             $('#tblmat').datagrid("loadData", matList);
             $("#MatCode")[0].focus();
         });

         var selectMat;

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
                         $("#MainUnit").val("");
                         $("#MainUnitAmount").val("");
                         MSI("提示", "您输入的货品编号无效");
                         selectMat = null;
                         return;
                     }
                     selectMat = data;
                     $("#hiMatID").val(data.ID);
                     $("#MatCode").val(data.MatCode);
                     $("#MatName").val(data.MatName);
                     $("#MainUnit").val(data.UnitName);
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

             if ($("#MainUnitAmount").val() == "") {
                 MSI("提示", "数量不能为空");
                 return;
             }

             if (existsJsonItem(matList.rows, "ID", selectMat.BarCode) == true) {
                 MSI("提示", "已存在！");
                 return;
             }

             var mat = JSON.parse("{}");
             mat["MatID"] = selectMat.MatID;
             mat["ID"] = $("#MatCode").val();
             mat["MatCode"] = $("#MatCode").val();
             mat["MainUnitAmount"] = $("#MainUnitAmount").val();
             mat["UnitCode"] = selectMat.UnitCode;
             mat["DeleteAction"] = "deleteMat(\'" + mat["ID"] + "\')";
             matList.rows.push(mat);
             $('#tblmat').datagrid("loadData", matList);

             $("#MatCode").val("");
             $("#MatName").val("");
             $("#InAmount").val("");
             $("#MainUnit").val("");
             $("#MainUnitAmount").val("");
             $("#MatCode")[0].focus();
         }

         //刷新库位
         function BindSite() {
             var whID = $("#Warehouse").val();
             if (whID == null || whID=="") {
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
