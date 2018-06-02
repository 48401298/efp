<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="ManageCheckStock.aspx.cs" Inherits="Manage.Web.WH.Stock.ManageCheckStock" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>盘点管理</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>                        
            <asp:LinkButton ID="btAdd" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'" OnClientClick="add();return false;"
                        onclick="btAdd_Click" >增加</asp:LinkButton>      
             <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
                        onclientclick="return confirm('确定要删除选中的记录？');" 
                onclick="btDelete_Click">删除</asp:LinkButton>      
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:68px;padding:3px;">
                        <table>
                        <tr>
                            <td align="right">
                                盘点日期</td>
                            <td>
                                
                                <asp:TextBox ID="StartDate" runat="server" Width="100px"></asp:TextBox>
                                
                            </td>                            
                            <td align="center">
                                
                                至</td>                            
                            <td>
                                
                                <asp:TextBox ID="EndDate" runat="server" Width="100px"></asp:TextBox>
                            </td>                            
                            <td>
                                
                                仓库</td>                            
                            <td>
                                
                                <asp:DropDownList ID="Warehouse" runat="server">
                                </asp:DropDownList>
                            </td>                            
                            <td>
                                
                                存储区域</td>                            
                            <td>
                                
                                <asp:DropDownList ID="AreaID" runat="server">
                                </asp:DropDownList>
                            </td>                            
                        </tr>                            
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" title="入库单列表">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="ID" Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BillNO" HeaderText="盘点单号">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BillDate" HeaderText="盘点日期">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WarehouseName" HeaderText="仓库" />
                                            <asp:BoundField DataField="AreaName" HeaderText="存储区域" />
                                            <asp:BoundField DataField="CheckHeader" HeaderText="盘点负责人" />
                                            <asp:BoundField DataField="IsConfirm" HeaderText="状态" />
                                            <asp:BoundField DataField="Remark" HeaderText="备注" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtView" runat="server">查看</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtEdit" runat="server">编辑</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="datagrid-header" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
                                        onpagechanged="AspNetPager1_PageChanged">
                                    </webdiyer:AspNetPager>
                                </td>
                            </tr>
                            
                        </table>
                  </div>  
            </div>
        </div>   
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language=javascript type="text/javascript">
        $(function () {
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
            openAppWindow1('添加', "EditCheckStockBill.aspx", '660', '460');
            return false;
        }      
        //编辑
        function edit(id) {
            openAppWindow1('编辑', "EditCheckStockBill.aspx?id=" + id, '660', '460');
            return false;
        }

        //查看
        function view(id) {
            openAppWindow1('查看', "ViewCheckStockBill.aspx?id=" + id, '660', '460');
            return false;
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
