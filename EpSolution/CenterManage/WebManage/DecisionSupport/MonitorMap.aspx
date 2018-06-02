<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorMap.aspx.cs" Inherits="Manage.Web.DecisionSupport.MonitorMap" %>
<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--css-->
    <link href="style/demo.css" rel="stylesheet" type="text/css" />
    <!--javascript-->
    <Script src="../JS/jquery.min-1.11.1.js"></Script>
    <Script src="../easyui/jquery.easyui.min.js"></Script>
    <Script src="../easyui/locale/easyui-lang-zh_CN.js"></Script>
    <Script src="../JS/CommonActions.js"></Script>
    <LINK rel="stylesheet" type="text/css" href="../easyui/themes/default/easyui.css"></LINK>
    <LINK rel="stylesheet" type="text/css" href="../easyui/themes/icon.css"></LINK>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
    <div class="demo_main">
        <div style="min-height: 600px; width: 100%;" id="map">
        </div>
        <script type="text/javascript">
            function map_init() {
                var map = new BMap.Map("map"); // 创建Map实例
                var point = new BMap.Point(121.624266, 38.87891); //地图中心点，大连
                map.centerAndZoom(point, 13); // 初始化地图,设置中心点坐标和地图级别。
                map.enableScrollWheelZoom(true); //启用滚轮放大缩小
                //向地图中添加缩放控件
                var ctrlNav = new window.BMap.NavigationControl({
                    anchor: BMAP_ANCHOR_TOP_LEFT,
                    type: BMAP_NAVIGATION_CONTROL_LARGE
                });
                map.addControl(ctrlNav);

                //向地图中添加缩略图控件
                var ctrlOve = new window.BMap.OverviewMapControl({
                    anchor: BMAP_ANCHOR_BOTTOM_RIGHT,
                    isOpen: 1
                });
                map.addControl(ctrlOve);

                //向地图中添加比例尺控件
                var ctrlSca = new window.BMap.ScaleControl({
                    anchor: BMAP_ANCHOR_BOTTOM_LEFT
                });
                map.addControl(ctrlSca);

                var point = new Array(); //存放标注点经纬信息的数组
                var marker = new Array(); //存放标注点对象的数组
                var info = new Array(); //存放提示信息窗口对象的数组
                for (var i = 0; i < markerArr.length; i++) {
                    var p0 = markerArr[i].point.split(",")[0]; //
                    var p1 = markerArr[i].point.split(",")[1]; //按照原数组的point格式将地图点坐标的经纬度分别提出来
                    point[i] = new window.BMap.Point(p0, p1); //循环生成新的地图点
                    marker[i] = new window.BMap.Marker(point[i]); //按照地图点坐标生成标记
                    map.addOverlay(marker[i]);
                    var label = new window.BMap.Label(markerArr[i].title, { offset: new window.BMap.Size(20, -10) });
                    marker[i].setLabel(label);
                    
                    var html = '';
                    html += "<p style=’font-size:12px;lineheight:1.5em;’>监测位置：" + markerArr[i].title + "";
                    html += "</p>";
                    info[i] = new window.BMap.InfoWindow(html); // 创建信息窗口对象
                    $(marker[i]).attr("info", info[i]);
                    var sContent = "<iframe id='ifrpage' src='../Video/VideoMonitorList.aspx?code=" + markerArr[i].id + "' width='800' height='600' frameborder='0' name='ifrpage' ></iframe>";
                    var infoWindow = new window.BMap.InfoWindow(sContent);
                    $(marker[i]).attr("history", infoWindow);
                    $(marker[i]).attr("code", markerArr[i].id);
//                    marker[i].addEventListener("mouseover", function () {
//                        this.openInfoWindow($(this).attr("info"));
//                    });
                    marker[i].addEventListener("click", function () {
                        openAppWindow1("监控列表", "../Video/VideoMonitorList.aspx?code=" + $(this).attr("code"), '800', '580');
                    });
                }
            }
            //异步调用百度js
            function map_load() {
                var load = document.createElement("script");
                load.src = "http://api.map.baidu.com/api?v=1.4&callback=map_init";
                document.body.appendChild(load);
            }
            window.onload = map_load;
        </script>
    </div>
    <uc1:AppWindowControl ID="AppWindowControl1" runat="server" />
    </form>
</body>
</html>
