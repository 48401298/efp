<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="Manage.Web.LeftMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <title></title>
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
	
		<!-- inline styles related to this page -->

		<!-- ace settings handler -->
        <script src="bootstrap-ace/assets/js/ace-extra.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal ID="Menu" runat="server"></asp:Literal>    
    </form>
    <script language="javascript" type="text/javascript">
        //打开页面
        function openUrl(menuID, url, target, title, closable) {
            parent.openUrl(menuID, url, target, title, closable);
        }
        function openMonitorCenter() {
            window.open("DecisionSupport/Index.aspx");
        }
    </script>
     <!--[if !IE]> -->
		<script type="text/javascript">
		    window.jQuery || document.write("<script src='bootstrap-ace/assets/js/jquery.min.js'>" + "<" + "/script>");
		</script>

		<script type="text/javascript">
		    if ('ontouchstart' in document.documentElement) document.write("<script src='bootstrap-ace/assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
		</script>
        <script src="bootstrap-ace/js/bootstrap.min.js"></script>

		<!-- page specific plugin scripts -->

		<!-- ace scripts -->
        <script src="bootstrap-ace/assets/js/ace-elements.min.js"></script>
        <script src="bootstrap-ace/assets/js/ace.min.js"></script>

		<!-- inline scripts related to this page -->
        <link rel="stylesheet" href="bootstrap-ace/assets/css/ace.onpage-help.css" />
        <link rel="stylesheet" href="bootstrap-ace/docs/assets/js/themes/sunburst.css" />

		<script type="text/javascript">		    ace.vars['base'] = '..'; </script>
        <script src="bootstrap-ace/assets/js/ace/ace.onpage-help.js"></script>
        <script src="bootstrap-ace/docs/assets/js/rainbow.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/generic.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/html.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/css.js"></script>
        <script src="bootstrap-ace/docs/assets/js/language/javascript.js"></script>
</body>
</html>
