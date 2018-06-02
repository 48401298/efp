<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectDataResult.aspx.cs" Inherits="Manage.Web.MobileApp.InspectDataResult" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
        <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="../bootstrap-ace/docs/assets/js/themes/sunburst.css" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>环境监测查询页面</title>

    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <script src="../JS/json2.js" type="text/javascript"></script>
    <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->
    <!-- text fonts -->
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace-fonts.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace.min.css" />

    <!--[if lte IE 9]>
        <link rel="stylesheet" href="../assets/css/ace-part2.min.css" />
    <![endif]-->
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace-rtl.min.css" />

    <!--[if lte IE 9]>
      <link rel="stylesheet" href="../assets/css/ace-ie.min.css" />
    <![endif]-->
    <!-- inline styles related to this page -->
    <!-- ace settings handler -->
    <script src="../bootstrap-ace/assets/js/ace-extra.min.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lte IE 8]>
    <script src="bootstrap-ace/assets/js/html5shiv.js"></script>
    <script src="bootstrap-ace/assets/js/respond.min.js"></script>
    <![endif]-->
    <link href="../bootstrap/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="../JS/angular.min.js" type="text/javascript"></script>
    <style>
        .pagination { display: inline-block; padding-left: 0; margin: 5px 0; border-radius: 4px; }
        .pagination li { display: inline; }
       .pagination li info {
           position: relative; float: left; padding: 6px 12px; margin-left: -1px; line-height: 1.428571429; text-decoration: none;
            }
        .pagination li a { position: relative; float: left; padding: 6px 12px; margin-left: -1px; line-height: 1.428571429; text-decoration: none; border: 1px solid #ddd; }
        .pagination li:first-child a { margin-left: 0; border-bottom-left-radius: 4px; border-top-left-radius: 4px; color: #CCCCCC;}
        .pagination li:last-child a { border-top-right-radius: 4px; border-bottom-right-radius: 4px; color: #CCCCCC;}
        .pagination li a:hover, .pagination li a:focus { background-color: #808080 }
        .pagination .active a, .pagination .active a:hover, .pagination .active a:focus { z-index: 2; color: #CCCCCC; cursor: default; background-color: #808080; border-color: #428bca; }
        .pagination .disabled a, .pagination .disabled a:hover, .pagination .disabled a:focus { color: #CCCCCC; cursor: not-allowed; border-color: #ddd; }
        .pagination-lg li a { padding: 10px 16px; font-size: 18px; }
        .pagination-sm li a, .pagination-sm li span { padding: 5px 10px; font-size: 12px; }
        nav {
                    position: relative;
                    width:100%;
                    height: 50px;
        }
         .pagination {
            right: 0px;
            position: absolute;
         }
          nav li {
            cursor: pointer;
            color: #CCCCCC;
         }
    </style>
</head>
<body class="no-skin">
    <form id="form1" runat="server">
        <!-- #section:basics/navbar.layout -->
        <div id="navbar" class="navbar navbar-default">
            <script type="text/javascript">
                try { ace.settings.check('navbar', 'fixed') } catch (e) { }
            </script>

            <div class="navbar-container" id="navbar-container">
                <!-- #section:basics/sidebar.mobile.toggle -->
                <!-- /section:basics/sidebar.mobile.toggle -->
                <!-- #section:basics/navbar.dropdown -->
                <div class="pull-left" role="navigation">
                    <button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler">
                        <span class="sr-only">Toggle sidebar</span>

                        <span class="icon-bar"></span>

                        <span class="icon-bar"></span>

                        <span class="icon-bar"></span>
                    </button>
                    <!-- #section:basics/navbar.layout.brand -->
                    <a href="#" class="navbar-brand">
                        <small>
                            智慧渔业移动终端
                        </small>
                    </a>
                </div>
                <div class="navbar-buttons navbar-header pull-right" role="navigation">                
                    <ul class="nav ace-nav">
                   
                        <!-- #section:basics/navbar.user_menu -->
                        <li class="light-blue">
                            <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                                <span class="user-info">
                                    <small>欢迎,</small>
                                    管理员
                                </span>

                                <i class="ace-icon fa fa-caret-down"></i>
                            </a>

                            <ul class="user-menu dropdown-menu-right dropdown-menu dropdown-yellow dropdown-caret dropdown-close">
                                <li class="divider"></li>

                                <li>
                                    <a href="#" onclick="window.location.href = 'AppLogin.aspx'">
                                        <i class="ace-icon fa fa-power-off"></i>
                                        注销
                                    </a>
                                </li>
                            </ul>
                        </li>

                        <!-- /section:basics/navbar.user_menu -->
                    </ul>
                </div>

                <!-- /section:basics/navbar.dropdown -->
            </div><!-- /.navbar-container -->
        </div>

        <!-- /section:basics/navbar.layout -->
        <div class="main-container" id="main-container">
            <script type="text/javascript">
                try { ace.settings.check('main-container', 'fixed') } catch (e) { }
            </script>

            <!-- #section:basics/sidebar -->
            <div id="sidebar" class="sidebar                  responsive">
                <script type="text/javascript">
                    try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
                </script>            

                <ul class="nav nav-list">
                    <li class="active open">
                        <a href="AppDefault.aspx">
                            <i class="menu-icon fa fa-tachometer"></i>
                            <span class="menu-text"> 菜单</span>
                        </a>
                        <b class="arrow"></b>
                    </li>   
                    <li class="">
                        <a href="#" onclick="GotoUrl('MobileWeb/InspectDataQuery.aspx');return false;" class="dropdown-toggle">
                            <i class="menu-icon fa fa-desktop"></i>
                            <span class="menu-text">环境监测</span>
                        </a>
                        <b class="arrow"></b>
                    </li>

                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-list"></i>
                            <span class="menu-text">质量追溯</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
            </div>

            <!-- /section:basics/sidebar -->
            <div class="main-content">
                <!-- #section:basics/content.breadcrumbs -->
                <div class="breadcrumbs" id="breadcrumbs">
                    <script type="text/javascript">
                        try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                    </script>

                    <ul class="breadcrumb">
                        <li>
                            <i class="ace-icon fa"></i>
                            <a href="#" onclick="window.location.href='InspectDataQuery.aspx'">返回</a>
                        </li>
                        <!--<li>
                            <a href="#">Other Pages</a>
                        </li>-->

                    </ul><!-- /.breadcrumb -->
                   
                    <!-- /section:basics/content.searchbox -->
                </div>

                <!-- /section:basics/content.breadcrumbs -->
                <div class="page-content" ng-app="myApp" ng-controller="myCtrl" style="text-align:center">      
                <table style="width:100%">
                    <tr>
                        <td>
                            <table class="table table-condensed" style="border: 1px solid #C0C0C0;width:100%">
                                <thead>
                                <tr>
                                    <td align=center>设备编号</td>
                                    <td align=center>监测时间</td>
                                    <td align=center>监测项目</td>
                                    <td align=center>监测值</td>
                                </tr>
                                </thead>
                                <tbody>
                                <tr ng-repeat="x in items">
                                    <td>{{x.DeviceCode}}</td>
                                    <td>{{x.InspectTime}} </td>
                                    <td>{{x.ItemName}}</td>
                                    <td>{{x.InspectData}}</td>
                                </tr>           
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <nav ng-if="data.length != 0">
                                        <ul class="pagination">
                                        <li><info>记录总数：{{data.length}}</info></li>
                                        <li><info>总页数：{{pages}}</info></li>
                                        <li><info>当前页：{{selPage}}</info></li>
                                        <li><a ng-click="First()"><span>|<</span></a></li>
                                        <li>
                                        <a ng-click="Previous()">
                                        <span><</span>
                                        </a>
                                        </li>
                                        <li>
                                        <a ng-click="Next()">
                                        <span>></span>
                                        </a>
                                        </li>
                                        <li><a ng-click="Last()"><span>>|</span></a></li>
                                        </ul>
                                   </nav>
                        </td>
                    </tr>
                </table>                  
                  
                </div><!-- /.page-content -->
            </div><!-- /.main-content -->

            <div class="footer">
                <div class="footer-inner">
                    <!-- #section:basics/footer -->
                    <div class="footer-content">
                        <span class="bigger-120">
                        </span>
                    </div>

                    <!-- /section:basics/footer -->
                </div>
            </div>

            <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
                <i class="ace-icon fa fa-angle-double-up icon-only bigger-110"></i>
            </a>
        </div><!-- /.main-container -->   
            <asp:HiddenField ID="hiInspectData" runat="server" />
    </form>
     <!-- basic scripts -->    
     <script>
         var app = angular.module("myApp", []);
         var mydata = null;
         app.controller("myCtrl", function ($scope, $http) {
             //数据源
             //$scope.data = [{ "DeviceCode": "111", "InspectTime": "2018-1-1 10:10:01", "ItemName": "ph值","InspectData":"7.2"}];
             $scope.data = JSON.parse($("#hiInspectData").val());
             //分页总数
             $scope.pageSize = 10;
             $scope.pages = Math.ceil($scope.data.length / $scope.pageSize);
             $scope.pageList = [];
             $scope.selPage = 1;
             //设置表格数据源(分页)
             $scope.setData = function () {
                 $scope.items = $scope.data.slice(($scope.pageSize * ($scope.selPage - 1)), ($scope.selPage * $scope.pageSize)); //通过当前页数筛选出表格当前显示数据
             }
             $scope.items = $scope.data.slice(0, $scope.pageSize);
             //分页要repeat的数组
             for (var i = 0; i < $scope.pages; i++) {
                 $scope.pageList.push(i + 1);
             }
             //打印当前选中页索引
             $scope.selectPage = function (page) {
                 //不能小于1大于最大
                 if (page < 1 || page > $scope.pages) return;

                 $scope.selPage = page;
                 $scope.setData();
                 $scope.isActivePage(page);
                 console.log("选择的页：" + page);
             };
             //设置当前选中页样式
             $scope.isActivePage = function (page) {
                 return $scope.selPage == page;
             };
             //上一页
             $scope.Previous = function () {
                 $scope.selectPage($scope.selPage - 1);
             }
             //下一页
             $scope.Next = function () {
                 $scope.selectPage($scope.selPage + 1);
             };
             mydata = $scope;
         })
         function fn1() {
             mydata.items = [{ "Name": "111", "City": "111", "Country": "333" }
            , { "Name": "222", "City": "111", "Country": "333"}];
             mydata.$apply();

             alert();
         }
        </script>
</body>
</html>

