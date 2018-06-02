<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="AddMatAmount.aspx.cs" Inherits="Manage.Web.WH.In.AddMatAmount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加入库信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div>
        <div class="easyui-panel" title="入库信息" style="width:98%">
            <table cellpadding=0 cellspacing=0 class="editTable" width=100%>
               <tr>
                  <th width="100" nowrap="nowrap">货品条码</th><td>
                      <asp:TextBox ID="MatBarCode" runat="server" class="easyui-validatebox" Width="100%"></asp:TextBox>
                                           </td>                                        
                                        </tr> 
               <tr>  
                                        <th>货品编号</th>
                                            <td>
                                                <asp:TextBox ID="MatCode" runat="server" class="easyui-validatebox" Width="100%" 
                                                    ></asp:TextBox>
                                            </td>                                        
                                        </tr> 
               <tr>
                                        <th>货品名称</th>
                                            <td>
                                                <asp:TextBox ID="MatName" runat="server" class="easyui-validatebox" 
                                                    ReadOnly="True" Width="100%" 
                                                    ></asp:TextBox>
                                            </td>                                        
                                        </tr>
               <tr>
                                        <th>规格</th>
                                        <td>
                                            <asp:TextBox ID="SpecCode" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                                         
                                        </td>                                        
                                       </tr> 
               <tr>
                                            <th>数量</th><td>
                                                
                                             <table cellpadding=0 cellspacing=0 width=100%>
                                                <tr><td align=left>
                                               <asp:TextBox ID="InAmount" runat="server" class="easyui-validatebox" Width="100%" 
                                                    ></asp:TextBox>
                                               </td><th width="40">单位</th><td align=left>
                                               <asp:TextBox ID="UnitCode" runat="server" ReadOnly="True" Width="100%"></asp:TextBox>
                                               </td></tr></table>
                                            </td>                                        
                                        </tr>             
               <tr>
                                        <th>仓位</th>
                                            <td>
                                                    <asp:DropDownList ID="SaveSite" runat="server">
                                                    </asp:DropDownList>
                                                    </td>
                                                                  
                                        </tr> 
               <tr>
                            <td colspan=2>
                             <asp:LinkButton ID="btSave" CssClass="easyui-linkbutton" 
                                data-options="iconCls:'icon-save'" runat="server" 
                                onclientclick="return addMat();">保存</asp:LinkButton>
                            <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                                data-options="iconCls:'icon-back'" runat="server" 
                                onclientclick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                            </td>
                         </tr>
             </table>                        
        </div>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        $(function () {
            //绑定条码值变化事件
            $("#MatBarCode").change(function () {
                getMatByIDCode($("#MatBarCode").val());
            });
            //绑定条码回车事件
            $('#MatBarCode').keydown(function (e) {
                if (e.keyCode == 13) {
                    getMatByIDCode($("#MatBarCode").val());
                }
            });

            //绑定编号值变化事件
            $("#MatCode").change(function () {
                getMatByMatCode($("#MatCode").val());
            });
            //绑定编号回车事件
            $('#MatCode').keydown(function (e) {
                if (e.keyCode == 13) {
                    getMatByIDCode($("#MatCode").val());
                }
            });

        });


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
                        MSI("提示", "您输入的条码号无效");
                        selectMat = null;
                        return;
                    }
                    selectMat = data;
                    $("#hiMatID").val(data.ID);
                    $("#MatCode").val(data.MatCode);
                    $("#MatName").val(data.MatName);
                    $("#InAmount").val("1");
                    $("#SpecCode").val(data.OperateSpecName);
                    $("#UnitCode").val(data.OperateUnitName);
                },
                error: function (ex) {
                    alert('error:' + JSON.stringify(ex));
                }
            });
        }


        //增加
        function addMat() {
            if ($("#MatCode").val() == "") {
                MSI("提示", "原料编号不能为空");
                return false;
            }

            var mat = JSON.parse("{}");
            mat["MatID"] = selectMat.MatID;
            mat["MatBarCode"] = $("#MatBarCode").val();
            mat["ID"] = selectMat.BarCode;
            mat["MatCode"] = $("#MatCode").val();
            mat["MatName"] = selectMat.MatName;
            mat["InAmount"] = $("#InAmount").val();
            mat["SpecCode"] = $('#SpecCode').val();
            mat["MainUnitAmount"] = selectMat.MainUnitAmount;
            mat["MatSpec"] = selectMat.OperateSpecName;
            mat["UnitCode"] = selectMat.UnitName;
            mat["SaveSiteName"] = $("#SaveSite").find("option:selected").text();
            mat["SaveSite"] = $("#SaveSite").val();
            mat["DeleteAction"] = "deleteMat(\'" + mat["ID"] + "\')";
            parent.addMat(mat);
            parent.closeAppWindow1();
            return false;
        }

        //校验基本信息合法性
        function isValid() {
            if (isValidate() == false) {
                return false;
            }
            
            return true;
        }
    </script>   
</body>
</html>
