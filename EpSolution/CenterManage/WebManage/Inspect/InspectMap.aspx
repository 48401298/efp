<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectMap.aspx.cs" Inherits="Manage.Web.Inspect.InspectMap" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register src="../Pub/AppWindowControl.ascx" tagname="AppWindowControl" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监测地图</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
    <style type="text/css">
	    body, html,#allmap {width: 100%;height: 100%;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>
</head>
<body class="easyui-layout">
     
    <div id="mapPanel"
		style="overflow: hidden; width: 100%; height: 100%; z-index: 1;">
	</div>
    <div id="appWindow2" class="easyui-window" closed="true" modal="true" title="My Window" iconCls="icon-save" style="width:10px;height:10px;padding:3px;background: #fafafa;">
        <iframe id="appWindow2_Frm" width=100% height="100%" frameborder="0"></iframe>
    </div>

    <script language="javascript" type="text/javascript">
        
            var map;
            var online;
            var offline;
            var label;
            //百度地图API功能
            function loadJScript() {
                var script = document.createElement("script");
                script.type = "text/javascript";
                script.src = "http://api.map.baidu.com/api?v=2.0&ak=iDvRMskAZY4CxcmB7c2nRQOm64aw6NOz&callback=init";
                document.body.appendChild(script);
            }
            function init() {
                myIcon1 = new BMap.Icon("../images/Camera.png", new BMap.Size(32, 32));
                map = new BMap.Map("mapPanel");          // 创建Map实例
                //先定位中国
                var point = new BMap.Point(121.602823, 38.9231); // 创建点坐标

                map.centerAndZoom(point, 12);

                map.enableScrollWheelZoom();                 //启用滚轮放大缩小

                var top_left_control = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }); // 左上角，添加比例尺
                var top_left_navigation = new BMap.NavigationControl();  //左上角，添加默认缩放平移控件
                /*缩放控件type有四种类型:
                BMAP_NAVIGATION_CONTROL_SMALL：仅包含平移和缩放按钮；BMAP_NAVIGATION_CONTROL_PAN:仅包含平移按钮；BMAP_NAVIGATION_CONTROL_ZOOM：仅包含缩放按钮*/
                map.addControl(top_left_control);
                map.addControl(top_left_navigation);

                var overViewOpen = new BMap.OverviewMapControl({ isOpen: true, anchor: BMAP_ANCHOR_BOTTOM_RIGHT });
                map.addControl(overViewOpen);      //右下角，打开    //启用滚轮放大缩小

                online = new BMap.Icon("../images/online.png", new BMap.Size(32, 32));
                offline = new BMap.Icon("../images/offline.png", new BMap.Size(32, 32));

                getAllDevice();

            }

            window.onload = loadJScript;  //异步加载地图

            function getAllDevice() {

                //自动加载所有车辆信息
                $.ajax({
                    url: '../../Pub/GetAllDevicePositionList.ashx?type=CP2',
                    dataType: 'json',
                    method: 'GET',
                    success: function (data) {

                        if (data == null || data == "" || data == "[]") {

                            alert("无设备信息!");
                            return;
                        }

                        var jsoncar = data;

                        if (jsoncar != null && jsoncar != "") {

                            map.clearOverlays();
                            for (var i = 0; i < jsoncar.length; i++) {

                                var myIcon;
                                if (jsoncar[i].onlineStatus == "1") {
                                    myIcon = online;
                                } else {
                                    myIcon = offline;
                                }

                                if (jsoncar[i].Lon == 0 || jsoncar[i].Lat == 0) {

                                    continue;
                                }
                                var tmppt = new BMap.Point(jsoncar[i].Lon, jsoncar[i].Lat);
                                var opts = {
                                    position: tmppt,    // 指定文本标注所在的地理位置
                                    offset: new BMap.Size(10, -30)    //设置文本偏移量
                                }

                                var msg = "设备编号:" + jsoncar[i].DeviceCode + "<br/>" + "设备名称:" + jsoncar[i].DeviceName + "<br/>"

                                //var infoWindow = new BMap.InfoWindow(msg, { enableMessage: false, height: 85 });  // 创建信息窗口对象 
                                //map.openInfoWindow(infoWindow, tmppt); //开启信息窗口

                                var opts = {
                                    position: tmppt,    // 指定文本标注所在的地理位置
                                    offset: new BMap.Size(10, -30)    //设置文本偏移量
                                }
                                var label = new BMap.Label(msg, opts);  // 创建文本标注对象
                                label.setStyle({
                                    fontSize: "12px",
                                    height: "40px",
                                    lineHeight: "20px",
                                    fontFamily: "微软雅黑"
                                });

                                var markertmp = new BMap.Marker(tmppt, { icon: myIcon });  // 创建标注
                                map.addOverlay(label);
                                map.addOverlay(markertmp);

                                addClickHandler(jsoncar[i].DeviceCode, markertmp);
                            }
                        }
                    },
                    error: function (ex) {
                        alert('获取设备信息出错请联系管理员!');
                    }
                });
            }

            function addClickHandler(DeviceCode, marker) {
                marker.addEventListener("click", function (e) {
                    openAppWindow2('查看监测数据', "InspectDataShow.aspx?DeviceCodeLink=" + DeviceCode, '700', '450');
                    return false;
                });
            }

            function openAppWindow2(title, url, width, height) {

                document.getElementById("appWindow2_Frm").src = url;

                var left = (document.body.offsetWidth - width) / 2;
                var top = (document.body.offsetHeight - height) / 2;
                $('#appWindow2').window({
                    title: title,
                    left: left,
                    top: 10,
                    width: width,
                    modal: true,
                    shadow: false,
                    closed: false,
                    closable: true,
                    minimizable: false,
                    maximizable: false,
                    height: height,
                    onClose: function () { document.getElementById("appWindow2_Frm").src = ""; },
                    onMove: function (left, top) {
                        var parentObj = $(this).window('window').parent();
                        if (left > 0 && top > 0) {
                            return;
                        }
                        if (left < 0) {
                            $(this).window('move', {
                                left: 1
                            });
                        }
                        if (top < 0) {
                            $(this).window('move', {
                                top: 1
                            });
                        }
                        var width = $(this).panel('options').width;
                        var height = $(this).panel('options').height;
                        var right = left + width;
                        var buttom = top + height;
                        var parentWidth = parentObj.width();
                        var parentHeight = parentObj.height();
                        if (parentObj.css("overflow") == "hidden") {
                            if (left > parentWidth - width) {
                                $(this).window('move', {
                                    "left": parentWidth - width
                                });
                            }
                            if (top > parentHeight - $(this).parent().height()) {
                                $(this).window('move', {
                                    "top": parentHeight - $(this).parent().height()
                                });
                            }
                        }
                    }
                });
                $('#appWindow2').window('open');
            }

    </script>
</body>
</html>
