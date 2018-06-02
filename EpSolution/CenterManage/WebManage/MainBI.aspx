<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainBI.aspx.cs" Inherits="Manage.Web.MainBI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title></title>
    <script src="JS/jquery.min-1.11.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="echarts/vendors/echarts/echarts.min.js"></script>
    <style type="text/css">
        .chartDiv li
        {
            float: left;
            list-style: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="chartDiv">
        <ul>
            <li><div id="containerChart1" style="height: 500px;width:800px"></div></li>
        </ul>
        <%--<div id="containerChart1" style="height: 100%"></div>--%>
    </div>
    </form>
    <script type="text/javascript">
        bindchart1("containerChart1");

        function bindchart1(chartid) {            
            var dom = document.getElementById(chartid);
            var myChart = echarts.init(dom, "light");
            myChart.on('click', function (param) { clickBar1Function(param); })
            var app = {};

            option = null;
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
                    data: ['1月', '2月', '3月']
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
                        data: [2.0, 4.9, 7.0]
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
