<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" ValidateRequest="false" CodeBehind="ViewUseMatBill.aspx.cs"
    Inherits="Manage.Web.WH.In.ViewUseMatBill" %>

<%@ Register Src="../../Pub/AppWindowControl.ascx" TagName="AppWindowControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查看领料单</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
   
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" data-options="iconCls:'icon-back'"
                        runat="server" OnClientClick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="easyui-panel" title="要货信息" style="padding: 3px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <table id="tblBase" width="400px" cellpadding="0" cellspacing="0" class="viewTable">
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                生产批次
                                            </th>
                                            <td>
                                                <asp:Label ID="BatchNumber" runat="server" Width="120px" Enabled="false"></asp:Label>
                                            </td>
                                            <th align="left" nowrap="nowrap">
                                                产品
                                            </th>
                                            <td>
                                                <asp:Label ID="PDNAME" runat="server" Width="120px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                工厂
                                            </th>
                                            <td>
                                                <asp:Label ID="FNAME" runat="server" Width="120px" Enabled="false"></asp:Label>
                                            </td>
                                            <th align="left" nowrap="nowrap">
                                                生产线
                                            </th>
                                            <td>
                                                <asp:Label ID="PLNAME" runat="server" Width="120px" Enabled="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                        </tr>
                                        <tr>
                                            <th align=left>仓库</th>
                                            <td>
                                                <asp:Label ID="WarehouseName" runat="server" Width="120px" Enabled="false"></asp:Label>
                                            </td>    
                                             <th align="left" nowrap="nowrap">
                                                配送日期
                                            </th>
                                            <td>
                                                <asp:Label ID="DELIVERYDATE" runat="server" Width="120px"></asp:Label>
                                            </td>                                    
                                        </tr> 
                                        <tr>
                                           
                                        </tr>
                                        <tr>
                                            <th align="left" nowrap="nowrap">
                                                备注
                                            </th>
                                            <td colspan=3>
                                                <asp:Label ID="REMARK" CssClass="easyui-validatebox" MaxLength="100" runat="server" Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    要货数量</td>
                                <td>
                                    领料列表</td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblMaterialList" style="height: 200px;" iconcls="icon-edit">
                                        <thead>
                                            <tr>
                                                <th field="MatName" align="center" style="width: 150px">
                                                    物料
                                                </th>
                                                <th field="AMOUNT" align="center" style="width: 80px">
                                                    数量
                                                </th>
                                                <th field="UnitName" align="center" style="width: 80px">
                                                    单位
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </td>
                                <td>
                                    <table id="tblDetails" iconcls="icon-edit" style="height: 200px; width: 440px">
                                        <thead>
                                            <tr>
                                                <th align="center" field="SaveSite" style="width: 100px">
                                                    货位 
                                                </th>
                                                <th align="center" field="MatBarCode" style="width: 100px">
                                                    条码 
                                                </th>
                                                <th align="center" field="MatName" style="width: 100px">
                                                    物料 
                                                </th>
                                                <th align="center" field="AMOUNT" style="width: 50px">
                                                    数量 
                                                </th>
                                                <th align="center" field="UNITNAME" style="width: 50px">
                                                    单位 
                                                </th>
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
    <asp:HiddenField ID="hiMaterialList" runat="server" />
    <asp:HiddenField ID="hiUseMatList" runat="server" />
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        var materialList;
        var useMatList;
        //初始化
        $(function () {
            materialList = JSON.parse($("#hiMaterialList").val());
            $('#tblMaterialList').datagrid({});
            $('#tblMaterialList').datagrid("loadData", materialList);

            useMatList = JSON.parse($("#hiUseMatList").val());
            $('#tblDetails').datagrid({});
            $('#tblDetails').datagrid("loadData", useMatList);
            
            
        });
    </script>
</body>
</html>
