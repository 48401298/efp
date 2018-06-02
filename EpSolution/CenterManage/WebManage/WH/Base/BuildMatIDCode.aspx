<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeBehind="BuildMatIDCode.aspx.cs" Inherits="Manage.Web.WH.Base.BuildMatIDCode" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生成货品唯一识别码</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
            <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton> 
            <asp:LinkButton ID="btBuild" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-add'"
                        onclick="btAdd_Click">增加</asp:LinkButton>
            <asp:LinkButton ID="btDelete" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-delete'"
                        onclick="btDelete_Click" onclientclick="return confirm('确定要删除选中的记录？');">删除</asp:LinkButton> 
            <asp:LinkButton ID="btPrint" CssClass="easyui-linkbutton" runat="server" 
                onclick="btPrint_Click">打印</asp:LinkButton>
            <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                onclick="btCancel_Click">返回</asp:LinkButton>                   
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="货品信息" style="height:88px;padding:3px;">
                        <table>
                        <tr>
                            <th align="right">
                                货品编号</th>
                            <td>
                                <asp:TextBox ID="MatCode" runat="server" CssClass="easyui-textbox" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <th align="right">
                                货品名称</th>
                            <td>
                                <asp:TextBox ID="MatName" runat="server" CssClass="easyui-textbox" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>   
                            <td>
                                <asp:DropDownList ID="CodeStatus" runat="server">
                                    <asp:ListItem Value="0">未打印</asp:ListItem>
                                    <asp:ListItem Value="1">已打印</asp:ListItem>
                                </asp:DropDownList>
                            </td>                         
                        </tr>                            
                        <tr>
                            <th align="right">
                                生成个数</th>
                            <td>
                                <asp:TextBox ID="Count" runat="server">10</asp:TextBox>
                            </td>
                            <th align="right">
                                规格</th>
                            <td>
                                <asp:DropDownList ID="MatSpec" runat="server">
                                           </asp:DropDownList>
                            </td>   
                            <td>
                                &nbsp;</td>                         
                        </tr>                            
                        </table>
                    </div>
                  </div>        
                  <div data-options="region:'center'" title="货品唯一识别码列表">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="IDCode">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="唯一识别码" DataField="IDCode">
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="条形码">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBarCode" runat="server" style="font-family:'Code 128';font-size:28pt;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="生成日期" DataField="BuildDate" />
                                            <asp:BoundField HeaderText="打印次数" DataField="PrintCount" />
                                            <asp:BoundField DataField="Status" HeaderText="状态" Visible="False" />
                                        </Columns>
                                        <HeaderStyle CssClass="datagrid-header" />
                                    </asp:GridView>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <webdiyer:aspnetpager ID="AspNetPager1" runat="server" 
                                        onpagechanged="AspNetPager1_PageChanged">
                                    </webdiyer:aspnetpager>
                                </td>
                            </tr>
                        </table>
                  </div>  
            </div>
        </div>   
        <asp:HiddenField ID="hiMatID" runat="server" />
        <uc1:appwindowcontrol ID="AppWindowControl1" runat="server" />
        <iframe id="frmPrint"></iframe>
    </form>
    <script language=javascript>
        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }
    </script>
</body>
</html>
