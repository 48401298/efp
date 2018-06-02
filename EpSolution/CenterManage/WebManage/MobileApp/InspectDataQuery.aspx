<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectDataQuery.aspx.cs" Inherits="Manage.Web.MobileApp.InspectDataQuery" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>环境监测查询页面</title>

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
                             
                  <div class="panel panel-primary" style="padding: 5px; margin: 5px; text-align: left">
                      <div class="form-group">
                        <label for="StartDate" class="control-label">起始时间</label>
                        <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd" data-link-field="StartDate" data-link-format="yyyy-mm-dd">
                            <input class="form-control" size="16" type="text" id="StartDateShow" runat=server value="" readonly>
                            <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
					        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
				        <input type="hidden" id="StartDate" runat=server value="" />
                      </div>      
                      <div class="form-group">
                        <label for="dtp_input2" class="control-label">截止时间</label>
                        <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd" data-link-field="EndDate" data-link-format="yyyy-mm-dd">
                            <input class="form-control" size="16" type="text" id="EndDateShow" runat=server value="" readonly>
                            <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
					        <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                        </div>
				        <input type="hidden" runat=server id="EndDate" value="" />
                      </div>          
                      <div class="form-group">
                        <label for="DeviceType">设备类型</label>
                        <asp:DropDownList ID="DeviceType" runat="server" class="form-control"></asp:DropDownList>
                      </div>
                      <div class="form-group">
                        <label for="ItemCode">监测项目</label>
                        <asp:DropDownList ID="ItemCode" class="form-control" runat="server">
                        </asp:DropDownList>
                      </div>
                      <div class="form-group">
                          <asp:Button ID="btQuery" style="width:100%" runat="server" 
                              class="btn btn-lg btn-primary" Text="查询" onclick="btQuery_Click" />
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
        <script type="text/javascript">
            if ('ontouchstart' in document.documentElement) document.write("<script src='../bootstrap-ace/assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
        </script>
        <script src="bootstrap-ace/assets/js/bootstrap.min.js"></script>

        <!-- page specific plugin scripts -->
        <!-- ace scripts -->
        <script src="../bootstrap-ace/assets/js/ace-elements.min.js"></script>
        <script src="../bootstrap-ace/assets/js/ace.min.js"></script>

        <!-- inline scripts related to this page -->
        <link rel="stylesheet" href="../bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="../bootstrap-ace/docs/assets/js/themes/sunburst.css" />

        <script type="text/javascript">            ace.vars['base'] = '..'; </script>
        <script src="../bootstrap-ace/assets/js/ace/ace.onpage-help.js"></script>
        <script src="../bootstrap-ace/docs/assets/js/rainbow.js"></script>
        <script src="../bootstrap-ace/docs/assets/js/language/generic.js"></script>
        <script src="../bootstrap-ace/docs/assets/js/language/html.js"></script>
        <script src="../bootstrap-ace/docs/assets/js/language/css.js"></script>
        <script src="../bootstrap-ace/docs/assets/js/language/javascript.js"></script>
        <script language=javascript>
            function GotoUrl(url) {
                window.location.href = url;
            }
        </script>
        <script src="../JS/jquery.min-1.11.1.js" type="text/javascript"></script>
        <script src="../bootstrap/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
        <script src="../bootstrap/js/locales/bootstrap-datetimepicker.zh-CN.js" type="text/javascript"></script>
        <script type="text/javascript">
            $('.form_date').datetimepicker({
                language: 'zh-CN',
                weekStart: 1,
                todayBtn: 1,
                autoclose: 1,
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                format: "yyyy-mm-dd",
                forceParse: 0
            });
        </script> 
</body>
</html>
