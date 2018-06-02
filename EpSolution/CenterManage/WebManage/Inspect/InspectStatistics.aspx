<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectStatistics.aspx.cs" Inherits="Manage.Web.Inspect.InspectStatistics" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监测数据统计</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
             <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click">查询</asp:LinkButton>                        
        </div>        
        <div data-options="region:'center',border:false" style="padding-right: 10px; padding-left: 10px;">
            <div  class="easyui-layout" data-options="border:false" fit="true" style="width:100%; height:600px;overflow:hidden;">		
                  <div data-options="region:'north',border:false" style="">                    
                    <div class="easyui-panel" title="查询条件" style="height:90px;padding:3px;">
                        <table class="condiTable">
                            <tr>
                                <td>
                                    所在机构</td>
                                <td>
                                    <asp:DropDownList ID="OrganID" runat="server" onchange="organIdChange(this)">
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    设备类型</td>
                                <td>
                                    <asp:DropDownList ID="DeviceType" runat="server" onchange="deviceTypeChange(this)"></asp:DropDownList>
                                </td>
                                <td>
                                    监测设备</td>
                                <td>
                                    <asp:DropDownList ID="DeviceCode" runat="server" onchange="deviceCodeChange(this);return false;">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdDeviceCode" runat="server" />
                                </td>
                                
                                <td>
                                    监测项目</td>
                                <td>
                                    <asp:DropDownList ID="ItemCode" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    统计类型</td>
                                <td>
                                    <asp:DropDownList ID="ResultType" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    起始时间</td>
                                <td>
                                    <asp:TextBox ID="StartTime" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    结束时间</td>
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
                                            <asp:BoundField DataField="ResultType" HeaderText="监测类型" />
                                            <asp:BoundField DataField="InspectTime" HeaderText="监测时间" />
                                            <asp:BoundField DataField="MaxDataValue" HeaderText="最大值" />
                                            <asp:BoundField DataField="MinDataValue" HeaderText="最小值" />
                                            <asp:BoundField DataField="AvgValue" HeaderText="平均值" />
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
            </div>
        </div>   
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        function organIdChange(obj) {

            orgOrTypeChange();
        }

        function deviceTypeChange(obj) {

            orgOrTypeChange();
        }

        function deviceCodeChange(obj) {
            document.getElementById("hdDeviceCode").value = obj.value;
        }

        function orgOrTypeChange() {
            var params = "?OrganID=" + document.getElementById("OrganID").value + "&DeviceType=" + document.getElementById("DeviceType").value;

            $.ajax({
                url: '../../Pub/GetAllDeviceByOrgAndType.ashx' + params,
                dataType: 'json',
                method: 'GET',
                success: function (data) {

                    if (data && typeof data === "object") {
                        var DeviceCodeList = document.getElementById('DeviceCode');
                        while (DeviceCodeList.options.length > 0) {
                            DeviceCodeList.options.remove(0);
                        }

                        document.getElementById('DeviceCode').add(new Option("    ", ""));
                        for (var i = 0; i < data.length; i++) {
                            var opt = new Option(data[i].DeviceName, data[i].DeviceCode);
                            document.getElementById('DeviceCode').add(opt);
                        }
                        
                        //设备选择项不为空时.设置对应的内容为选中
                        var sel = document.getElementById("hdDeviceCode").value;
                        if (sel != "") {
                            for (var i = 0; i < document.all.DeviceCode.length; i++) {
                                if (sel == document.all.DeviceCode[i].value) {
                                    document.all.DeviceCode.options[i].selected = true;
                                }
                            }
                        }
                    }
                }

            });
        }
        //查询后重置选中的查询项
        orgOrTypeChange();
    </script>
</body>
</html>
