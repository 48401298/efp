<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectStatisticsEChart.aspx.cs" Inherits="Manage.Web.Inspect.InspectStatisticsEChart" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监测数据统计图</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">    
        <div data-options="region:'north',border:false" style="padding:5px 10px 5px 10px;">
             <asp:LinkButton ID="btQuery" runat="server" CssClass="easyui-linkbutton" data-options="iconCls:'icon-search'"
                        onclick="btQuery_Click" onclientclick="searchData();return false;">查询</asp:LinkButton>                        
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
                                    <asp:DropDownList ID="DeviceCode" runat="server" onchange="deviceCodeChange(this)">
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
                                    <asp:DropDownList ID="ResultType" runat="server" onchange="resultTypeChange(this)"></asp:DropDownList>
                                </td>
                                <td>
                                    统计日期</td>
                                <td>
                                    <asp:TextBox ID="StartTimeH" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="StartTimeD" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="StartTimeM" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="StartTime" runat="server" />
                                </td>
                             
                            </tr>                           
                        </table>
                    </div>
                  </div>        
                  <div id="container" data-options="region:'center'" style="height: 80%;width:95%;border-style: none">
                        
                  </div>  
            </div>
        </div>   
        <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
        <asp:HiddenField ID="hiChartData" runat="server" value="{title:{text:'未来一周气温变化',subtext:'纯属虚构'},tooltip:{trigger:'axis'},legend:{data:['最高气温','最低气温']},toolbox:{show:true,feature:{mark:{show:true},dataView:{show:true,readOnly:false},magicType:{show:true,type:['line','bar']},restore:{show:true},saveAsImage:{show:true}}},calculable:true,xAxis:[{type:'category',boundaryGap:false,data:['周一','周二','周三','周四','周五','周六','周日']}],yAxis:[{type:'value',axisLabel:{formatter:'{value} °C'}}],series:[{name:'最高气温',type:'line',data:[11,11,15,13,12,13,10],markPoint:{data:[{type:'max',name:'最大值'},{type:'min',name:'最小值'}]},markLine:{data:[{type:'average',name:'平均值'}]}},{name:'最低气温',type:'line',data:[1,-2,2,5,3,2,0],markPoint:{data:[{name:'周最低',value:-2,xAxis:1,yAxis:-1.5}]},markLine:{data:[{type:'average',name:'平均值'}]}}]}"/>
    </form>
    <!--图表控件-->
    <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
    <script src="../JS/json2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/echarts.min.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts-gl/echarts-gl.min.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts-stat/ecStat.min.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/extension/dataTool.min.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/map/js/china.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/map/js/world.js"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/extension/bmap.min.js"></script>
    <script type="text/javascript" src="../echarts/vendors/simplex.js"></script>
    <script language="javascript" type="text/javascript">

        function searchData() {
            if (document.getElementById("ResultType").value == "") {
                alert("请选择统计类型!");
                return false;
            }

            var rt = document.getElementById("ResultType").value;
            if (rt == "1") {
                $("#StartTime").val($("#StartTimeH").val());
            } else if (rt == "2") {
                $("#StartTime").val($("#StartTimeD").val());
            } else if (rt == "3") {
                $("#StartTime").val($("#StartTimeM").val());
            }

            if (document.getElementById("StartTime").value == "") {
                alert("请选择日期!");
                return false;
            }

            if (document.getElementById("DeviceCode").value == "") {
                alert("请选择要统计的设备!");
                return false;
            }

            if (document.getElementById("ItemCode").value == "") {
                alert("请选择要统计的项目类型!");
                return false;
            }

            myChart.clear();

            var params = "?ResultType=" + document.getElementById("ResultType").value
            + "&ItemCode=" + document.getElementById("ItemCode").value
            + "&DeviceCode=" + document.getElementById("DeviceCode").value
            + "&DeviceType=" + document.getElementById("DeviceType").value
            + "&OrganID=" + document.getElementById("OrganID").value
            + "&StartTime=" + document.getElementById("StartTime").value;
            $.ajax({
                url: '../../Pub/GetInspectStatisticsEChart.ashx' + params,
                dataType: 'html',
                method: 'GET',
                success: function (data) {
                    if (data == "") {
                        alert("当前查询条件未查询到统计数据!");
                        return;
                    }
                    var obj = eval('(' + data + ')')
                    if (obj && typeof obj === "object") {
                        myChart.setOption(obj, true);
                    }
                },
                error: function (data) {
                    alert("解析数据出错,请联系管理员!");
                }

            });
        }

        //刷新
        function refreshData() {
            document.getElementById('btQuery').click();
        }

        var dom = document.getElementById("container");
        var myChart = echarts.init(dom, "light");


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

        function resultTypeChange(obj) {
            var rt = document.getElementById("ResultType").value;
            if (rt == "1") {
                $("#StartTimeH").show();
                $("#StartTimeD").hide();
                $("#StartTimeM").hide();
                $("#StartTime").val($("#StartTimeH").val());
            } else if (rt == "2") {
                $("#StartTimeH").hide();
                $("#StartTimeD").show();
                $("#StartTimeM").hide();
                $("#StartTime").val($("#StartTimeD").val());
            } else if (rt == "3") {
                $("#StartTimeH").hide();
                $("#StartTimeD").hide();
                $("#StartTimeM").show();
                $("#StartTime").val($("#StartTimeM").val());
            }

        }
        
        $(document).ready(function () {
            orgOrTypeChange();
            $("#ResultType").val("1");
            var myDate = new Date();
            var y = myDate.getFullYear(); //获取完整的年份(4位,1970-????)
            var m = myDate.getMonth() + 1; //获取当前月份(0-11,0代表1月)
            var d = myDate.getDate(); //获取当前日(1-31)
            $("#StartTimeH").val(y + "-" + (m < 10 ? "0" + m : m) + "-" + (d < 10 ? "0" + d : d));
            $("#StartTimeH").show();
            $("#StartTimeD").hide();
            $("#StartTimeM").hide();
        });
        
    </script>
</body>
</html>
