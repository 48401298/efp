<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryGoodsTraceDetail.aspx.cs" Inherits="Manage.Web.MobileWeb.QueryGoodsTraceDetail" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
        <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="../bootstrap-ace/docs/assets/js/themes/sunburst.css" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>智慧渔业-产品追溯</title>

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
         .infoView th
         {
             padding: 3px;
         }
         .infoView td
         {
             padding-left: 5px;
         }
          .tableView th
         {
             text-align:center;
             padding: 3px;
         }
         .tableView td
         {
             text-align:center;
             padding-left: 5px;
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
                            智慧渔业-产品追溯
                        </small>
                    </a>
                </div>
                <div class="navbar-buttons navbar-header pull-right" role="navigation">                
                    <ul class="nav ace-nav">
                   
                        <!-- #section:basics/navbar.user_menu -->
                        <li class="light-blue">
                            <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                                进入公众号

                            </a>

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

            
            <!-- /section:basics/sidebar -->
            <div class="main-content">
               
                <!-- /section:basics/content.breadcrumbs -->
                <div class="page-content">   
                  <div class="panel panel-primary">
                    <div class="panel-heading">
                        产品信息                    
                    </div>
                    <div class="panel-body">
                       <table class="infoView">
                            <tr>
                                <th>产品名称:</th>
                                <td>
                                    <asp:Label ID="ProduceName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>产品规格:</th>
                                <td>
                                    <asp:Label ID="SPECIFICATION" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>制造商:</th>
                                <td>
                                    <asp:Label ID="Manufacturer" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>生产地址:</th>
                                <td>
                                    <asp:Label ID="ProductionAddress" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>保质期:</th>
                                <td>
                                    <asp:Label ID="QualityPeriod" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>生产日期:</th>
                                <td>
                                    <asp:Label ID="ProduceDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>生产批次号:</th>
                                <td>
                                    <asp:Label ID="ProduceBatchNumber" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>生产许可证号:</th>
                                <td>
                                    <asp:Label ID="ProductionLicense" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>产品标准号:</th>
                                <td>
                                    <asp:Label ID="ProductStandard" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>查询次数:</th>
                                <td>
                                    <asp:Label ID="QueryCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                       </table>         
                    </div>
                  </div>
                  <div class="panel panel-primary" style="text-align: left">
                    <div class="panel-heading">
                        原料组成                    
                    </div>
                    <div class="panel-body">
                        <table class="table table-striped tableView">
                            <thead>
                                <tr>
                                <th>序号</th>
                                <th>原料</th>
                                <th>产地</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="lblMaterials" runat="server"></asp:Literal>                                  
                            </tbody>
                        </table>                                                    
                    </div>
                  </div>
                  <div class="panel panel-primary" style="text-align: left">
                    <div class="panel-heading">
                        生产工序                    
                    </div>
                    <div class="panel-body">
                        <table class="table table-striped tableView">
                            <thead>
                                <tr>
                                <th>序号</th>
                                <th>加工工序</th>
                                <th>加工设备</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="tblProcess" runat="server"></asp:Literal>         
                            </tbody>
                        </table>
                    </div>
                  </div>
                  <div class="panel panel-primary" style="text-align: left">
                    <div class="panel-heading">
                        监控视频            
                    </div>
                    <div class="panel-body">
                        <video src="movie.ogg" controls="controls" >
                                your browser does not support the video tag
                        </video> 
                    </div>
                  </div>
                  <div class="panel panel-primary" style="text-align: left">
                    <div class="panel-heading">
                        图片精选                    
                    </div>
                    <div class="panel-body">
                        <img src="/wp-content/uploads/2014/06/download.png" class="img-rounded">
                        <img src="/wp-content/uploads/2014/06/download.png" class="img-rounded">
                        <img src="/wp-content/uploads/2014/06/download.png" class="img-rounded">
                        <img src="/wp-content/uploads/2014/06/download.png" class="img-rounded">
                    </div>
                  </div>
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
    </form>
     <!-- basic scripts -->    
     <script type="text/javascript" language=javascript>
         
     </script>
</body>
</html>

