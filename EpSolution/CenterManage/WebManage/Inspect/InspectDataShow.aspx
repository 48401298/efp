<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectDataShow.aspx.cs" Inherits="Manage.Web.Inspect.InspectDataShow" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑监测项目信息</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="Mainform" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>      
                    <asp:LinkButton ID="btCancel" CssClass="easyui-linkbutton" 
                        data-options="iconCls:'icon-back'" runat="server" 
                        onclientclick="parent.closeAppWindow1();return false;">取消</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:68px;padding:3px;">
                        <table class="condiTable">
                            <tr>
                                <td>
                                    设备编号</td>
                                <td>
                                    <asp:TextBox ID="DeviceName" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    起始时间</td>
                                <td>
                                    <asp:TextBox ID="StartTime" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    结果时间</td>
                                <td>
                                    <asp:TextBox ID="EndTime" runat="server"></asp:TextBox>
                                </td>
                            </tr>                            
                        </table>
                    </div>
                  </div>  
                    <div data-options="region:'center'" style="border-style: none">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" 
                                        CssClass="datagrid" onprerender="GvList_PreRender" 
                                        onrowupdating="GvList_RowUpdating" DataKeyNames="Id" Width="100%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbxSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DeviceName" HeaderText="设备名称"></asp:BoundField>
                                            <asp:BoundField DataField="ItemName" HeaderText="项目名称" />
                                            <asp:BoundField DataField="InspectTime" HeaderText="监测时间" />
                                            <asp:BoundField DataField="InspectData" HeaderText="监测值" />
                                            <asp:BoundField DataField="OrganDESC" HeaderText="机构名称" />
                                            <asp:BoundField DataField="UpdateTime" HeaderText="更新时间">
                                            </asp:BoundField>
                                            <%--<asp:CommandField ShowEditButton="True" />--%>
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
                </td>
            </tr>
        </table>
        
    </div>
    <script language="javascript" language=javascript>

    </script>

    </form>    
    </body>
</html>
