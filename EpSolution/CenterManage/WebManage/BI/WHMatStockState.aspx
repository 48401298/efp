<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WHMatStockState.aspx.cs" Inherits="Manage.Web.BI.WHMatStockState" %>

<!DOCTYPE html>

<html style="height: 100%">
<head runat="server">
    <title>仓库货品库存统计</title>
    <meta charset="utf-8">
    <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../echarts/vendors/echarts/echarts.min.js"></script>
    <script src="../JS/json2.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0">    
    <form id="form1" runat="server">
        <asp:HiddenField ID="hiChartData" runat="server" />
    </form>
    <div id="container" style="height: 100%"></div>
    <script type="text/javascript">
        var dom = document.getElementById("container");
        var myChart = echarts.init(dom, "light");
        var app = {};

        jQuery.support.cors = true;

        if ($("#hiChartData").val() != "") {
            var chartData = JSON.parse($("#hiChartData").val());
            // 成功后回调
            option = {
                title: {
                    text: '库存量(食品一厂原料库)',
                    subtext: '公斤'
                },
                tooltip: {
                    trigger: 'axis'
                },
                calculable: true,
                xAxis: [
                    {
                        type: 'category',
                        data: chartData.XAxisData
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: [
                    {
                        type: 'bar',
                        data: chartData.Series[0].data,
                        label: {
                            normal: {
                                show: true,
                                formatter: "{c}公斤"
                            }
                        }
                    }
                ]
            };
            if (option && typeof option === "object") {
                myChart.setOption(option, true);
            }
        }
        
    </script>
</body>
</html>
