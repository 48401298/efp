<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bar-simple.aspx.cs" Inherits="Manage.Web.echarts.bar_simple" %>

<!DOCTYPE html>

<html style="height: 100%">
<head runat="server">
    <title></title>
    <meta charset="utf-8">       
       <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
       <script src="../JS/json2.js" type="text/javascript"></script>
        <script type="text/javascript" src="vendors/echarts/echarts.min.js"></script>
       <script type="text/javascript" src="vendors/echarts-gl/echarts-gl.min.js"></script>
       <script type="text/javascript" src="vendors/echarts-stat/ecStat.min.js"></script>
       <script type="text/javascript" src="vendors/echarts/extension/dataTool.min.js"></script>
       <script type="text/javascript" src="vendors/echarts/map/js/china.js"></script>
       <script type="text/javascript" src="vendors/echarts/map/js/world.js"></script>
       <script type="text/javascript" src="vendors/echarts/extension/bmap.min.js"></script>
       <script type="text/javascript" src="vendors/simplex.js"></script>
</head>
<body style="height: 100%; margin: 0">
    <form id="form1" runat="server" style="height: 100%; margin: 0">
    <asp:Button ID="Button1" runat="server" Text="查询" onclick="Button1_Click" />
    <asp:HiddenField ID="hiChartData" runat="server" />
    <div id="container" style="height: 100%"></div>
    </form> 
       <script type="text/javascript">
           var dom = document.getElementById("container");
           var myChart = echarts.init(dom, "light");
           myChart.on('click', function (param) { clickBar1Function(param); })
           var app = {};
           var chartData = JSON.parse($("#hiChartData").val());
           option = null;
           option = {
               xAxis: {
                   type: 'category',
                   data: chartData.XList
               },
               yAxis: {
                   type: 'value'
               },
               series: [{
                   data: chartData.Datas,
                   type: 'bar'
               }]
           };
           if (option && typeof option === "object") {
               myChart.setOption(option, true);
           }

           function clickBar1Function(clickData) {
               alert(clickData.value);
               alert(clickData.name);
           }
       </script>
</body>
</html>
