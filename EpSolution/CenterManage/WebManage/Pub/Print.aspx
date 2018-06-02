<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="Manage.Web.Pub.Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Lodop/LodopFuncs.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <object id="LODOP1" style="display:none;" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"> 
          <param name="Border" value="0">
          <param name="Color" value="white"> 
          <embed id="LODOP_EM1" TYPE="application/x-print-lodop" border=0 Color="white" PLUGINSPAGE="install_lodop.exe">
        </object> 
        <input id="Button1" onclick="prn_Preview()" type="button" value="button" />
    </form>
    
    <script language="javascript" type="text/javascript">
        var LODOP; //声明为全局变量 
        function prn_Preview() {
            CreatePrintPage();
            LODOP.PREVIEW();
        };
        function CreatePrintPage() {
            LODOP = getLodop(document.getElementById('LODOP1'), document.getElementById('LODOP_EM1'));
            LODOP.ADD_PRINT_BARCODE(28, 34, 307, 47, "128A", "123456789012");

            LODOP.ADD_PRINT_BARCODE(100, 34, 307, 47, "128A", "123456789012");

            LODOP.ADD_PRINT_BARCODE(160, 34, 307, 47, "128A", "123456789012");
        }    </script>
</body>
</html>
