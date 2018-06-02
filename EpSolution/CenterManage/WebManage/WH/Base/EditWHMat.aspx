<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="EditWHMat.aspx.cs" Inherits="Manage.Web.WH.Base.EditWHMat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑货品信息</title>
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
                    <div class="easyui-panel" title="货品信息" style="padding:3px;">
                        <table>
                            <tr>
                                <td>
                                    <table class="editTable">
                                       <tr>
                                        <th align=left>货品编号</th>
                                        <td align=left>
                                           <asp:TextBox ID="MatCode" class="easyui-validatebox" 
                                               data-options="required:true" runat="server"></asp:TextBox>
                                           </td>
                                        <th align=left>货品名称</th><td>
                                           <asp:TextBox ID="MatName" class="easyui-validatebox" 
                                               data-options="required:true" runat="server"></asp:TextBox>
                                           </td>                                        
                                        </tr>   
                                        <tr>
                                           <th align=left>编号条码</th>
                                           <td align=left>
                                               <asp:LinkButton ID="lbtPrintCode" runat="server" CssClass="easyui-linkbutton" 
                                                   onclick="lbtPrintCode_Click" onclientclick="return printMatCode();">打印</asp:LinkButton>
                                            </td>
                                           <th align=left>货品类别</th><td>
                                            <asp:DropDownList ID="ProductType" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>                 
                                        <tr>
                                           <th align=left>计量单位</th>
                                           <td align=left>
                                               <asp:DropDownList ID="UnitCode" runat="server">
                                               </asp:DropDownList>
                                            </td>
                                           <th align=left>规格</th><td>
                                            <asp:DropDownList ID="SpecCode" runat="server">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>                 
                                        <tr>
                                           <th align=left>入库价格</th>
                                           <td align=left>
                                           <asp:TextBox ID="InPrice" class="easyui-numberbox"
                                               data-options="" runat="server"></asp:TextBox>
                                            </td>
                                           <th align=left>出库价格</th><td>
                                           <asp:TextBox ID="OutPrice" class="easyui-numberbox" 
                                               data-options="" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>                 
                                        <tr>
                                           <th align=left>保质期(天)</th>
                                           <td align=left>
                                           <asp:TextBox ID="QualityPeriod" class="easyui-numberbox"
                                               data-options="" runat="server"></asp:TextBox>
                                            </td>
                                            <th>过期预警天数</th>
                                            <td>
                                                <asp:TextBox ID="OverdueAlarmDay" class="easyui-numberbox"
                                                        data-options="" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>                 
                                        <tr>
                                           <th align=left>产地</th>
                                           <td align=left colspan="3">
                                           <asp:TextBox ID="ProductPlace" class="easyui-validatebox" 
                                               data-options="" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>                 
                                        <tr>
                                           <th align=left>备注</th>
                                           <td align=left colspan="3">
                                           <asp:TextBox ID="Remark" class="easyui-validatebox" 
                                               data-options="" runat="server" Width="100%"></asp:TextBox>
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

        //打印货品码
        function printMatCode() {
            if ($("#MatCode").val() == "") {
                MSI("提示", "货品编号不能为空");
                return false;
            }
            if ($("#MatName").val() == "") {
                MSI("提示", "货品名称不能为空");
                return false;
            }
        }
    </script>
    <asp:HiddenField ID="hiID" runat="server" />
    <asp:HiddenField ID="HiCREATEUSER" runat="server" />
    <asp:HiddenField ID="HiCREATETIME" runat="server" />
    <iframe id="frmPrint" style="display:none;"></iframe>
    </form>    
    </body>
</html>
