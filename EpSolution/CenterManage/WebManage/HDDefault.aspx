<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HDDefault.aspx.cs" Inherits="Manage.Web.HDDefault" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>智慧玉洋</title>

    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="bootstrap-ace/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="bootstrap-ace/assets/css/font-awesome.min.css" />

    <!-- page specific plugin styles -->
    <!-- text fonts -->
    <link rel="stylesheet" href="bootstrap-ace/assets/css/ace-fonts.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="bootstrap-ace/assets/css/ace.min.css" />

    <!--[if lte IE 9]>
        <link rel="stylesheet" href="../assets/css/ace-part2.min.css" />
    <![endif]-->
    <link rel="stylesheet" href="bootstrap-ace/assets/css/ace-skins.min.css" />
    <link rel="stylesheet" href="bootstrap-ace/assets/css/ace-rtl.min.css" />

    <!--[if lte IE 9]>
      <link rel="stylesheet" href="../assets/css/ace-ie.min.css" />
    <![endif]-->
    <!-- inline styles related to this page -->
    <!-- ace settings handler -->
    <script src="bootstrap-ace/assets/js/ace-extra.min.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lte IE 8]>
    <script src="bootstrap-ace/assets/js/html5shiv.js"></script>
    <script src="bootstrap-ace/assets/js/respond.min.js"></script>
    <![endif]-->
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
                            智慧玉洋
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
                                    <a href="#">
                                        <i class="ace-icon fa fa-cog"></i>
                                        设置
                                    </a>
                                </li>
                                <li class="divider"></li>

                                <li>
                                    <a href="#" onclick="window.location.href = 'HDLogin.aspx'">
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
            <%--<div id="sidebar" class="sidebar                  responsive">
                <script type="text/javascript">
                    try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
                </script>            

                <ul class="nav nav-list">
                    <li class="active open">
                        <a href="index.html">
                            <i class="menu-icon fa fa-tachometer"></i>
                            <span class="menu-text"> 菜单</span>
                        </a>
                        <b class="arrow"></b>
                    </li>   
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-desktop"></i>
                            <span class="menu-text">原材料入库</span>
                        </a>
                        <b class="arrow"></b>
                    </li>

                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-list"></i>
                            <span class="menu-text">产成品入库</span>
                        </a>
                        <b class="arrow"></b>
                    </li>

                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">原材料出库</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">产成品出库</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">原材料上线</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">追溯跟踪</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">在加工列表</span>
                        </a>
                        <b class="arrow"></b>
                    </li>                    
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">产成品下线</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
                    <li class="">
                        <a href="#" class="dropdown-toggle">
                            <i class="menu-icon fa fa-pencil-square-o"></i>
                            <span class="menu-text">成品装箱</span>
                        </a>
                        <b class="arrow"></b>
                    </li>
            </div>--%>

            <!-- /section:basics/sidebar -->
            <div class="main-content">
                <!-- #section:basics/content.breadcrumbs -->
                <div class="breadcrumbs" id="breadcrumbs">
                    <script type="text/javascript">
                        try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                    </script>

                    <ul class="breadcrumb">
                        <li>
                            <i class="ace-icon fa fa-home home-icon"></i>
                            <a href="#">菜单</a>
                        </li>
                        <!--<li>
                            <a href="#">Other Pages</a>
                        </li>-->

                    </ul><!-- /.breadcrumb -->
                    <!-- #section:basics/content.searchbox -->
                    <div class="nav-search" id="nav-search">
                        <form class="form-search">
                            <span class="input-icon">
                                <input type="text" placeholder="Search ..." class="nav-search-input" id="nav-search-input" autocomplete="off" />
                                <i class="ace-icon fa fa-search nav-search-icon"></i>
                            </span>
                        </form>
                    </div><!-- /.nav-search -->
                    <!-- /section:basics/content.searchbox -->
                </div>

                <!-- /section:basics/content.breadcrumbs -->
                <div class="page-content" style="text-align:center">
                    <div class="row">
                        <div class="row">
                            <asp:Literal ID="MainMenu" runat="server"></asp:Literal>                                                  
                        </div>
                        <div class="row">

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
            window.jQuery || document.write("<script src='bootstrap-ace/assets/js/jquery.min.js'>" + "<" + "/script>");
        </script>

        <!-- <![endif]-->
        <!--[if IE]>
        <script type="text/javascript">
         window.jQuery || document.write("<script src='bootstrap-ace/assets/js/jquery1x.min.js'>"+"<"+"/script>");
        </script>
        <![endif]-->
        <script type="text/javascript">
            if ('ontouchstart' in document.documentElement) document.write("<script src='bootstrap-ace/assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
        </script>
        <script src="bootstrap-ace/assets/js/bootstrap.min.js"></script>

        <!-- page specific plugin scripts -->
        <!-- ace scripts -->
        <script src="bootstrap-ace/assets/js/ace-elements.min.js"></script>
        <script src="bootstrap-ace/assets/js/ace.min.js"></script>

        <!-- inline scripts related to this page -->
        <link rel="stylesheet" href="bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="bootstrap-ace/docs/assets/js/themes/sunburst.css" />

        <script type="text/javascript">            ace.vars['base'] = '..'; </script>
        <script src="bootstrap-ace/assets/js/ace/ace.onpage-help.js"></script>
        <script src="bootstrap-ace/docs/assets/js/rainbow.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/generic.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/html.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/css.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/javascript.js"></script>
        <script language=javascript>
            function GotoUrl(url) {
                window.location.href = url;
            }
        </script>
</body>
</html>
