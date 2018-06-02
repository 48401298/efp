<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTraceQuery.aspx.cs" Inherits="Manage.Web.MobileApp.GoodsTraceQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="../bootstrap-ace/docs/assets/js/themes/sunburst.css" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title></title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

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
                            <i class="ace-icon fa "></i>
                            <a href="#" onclick="window.location.href='../AppDefault.aspx'">返回</a>
                        </li>
                        <!--<li>
                            <a href="#">Other Pages</a>
                        </li>-->

                    </ul><!-- /.breadcrumb -->
                   
                    <!-- /section:basics/content.searchbox -->
                </div>

                <!-- /section:basics/content.breadcrumbs -->
                <div class="page-content" style="text-align:center">
                             <table style="width:100%;">
                                <tr>
                                    <td width="80px" nowrap="nowrap" style="padding: 5px; font-size: 16px;">
                                        查询码</td>
                                    <td width="100%">
                                        <asp:TextBox ID="barCode" runat="server" Width="100%" Height="48px"></asp:TextBox>
                                    </td>
                                    <td style="padding: 5px">
                                        <asp:Button ID="btQuery" class="btn btn-lg btn-primary" runat="server" Text="查询" onclick="btQuery_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50px" nowrap="nowrap">
                                        &nbsp;</td>
                                    <td width="100%">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <input id="brScanBarCode" class="btn btn-lg btn-primary" 
                                            style="width: 100%; height: 200px; top: 0px; left: 0px;" type="button" 
                                            value="扫码" onclick="scanBarCode();" /></td>
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
    </form>
    <script language=javascript>
        function scanBarCode() {
            window.zhyy.scanCode();
        }
        function getInfoFromAndroid(msg) {
            $("#barCode").val(msg);
        }
    </script>
     <!-- basic scripts -->
        <!--[if !IE]> -->
        <script type="text/javascript">
            window.jQuery || document.write("<script src='../bootstrap-ace/assets/js/jquery.min.js'>" + "<" + "/script>");
        </script>

        <!-- <![endif]-->
        <!--[if IE]>
        <script type="text/javascript">
         window.jQuery || document.write("<script src='bootstrap-ace/assets/js/jquery1x.min.js'>"+"<"+"/script>");
        </script>
        <![endif]-->
</body>
</html>
