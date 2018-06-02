<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InspectDataQuery.aspx.cs" Inherits="Manage.Web.MobileWeb.InspectDataQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <link href="../bootstrap/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">  
       <div class="panel panel-primary" style="margin: 5px;">
            <div class="panel-heading">
              <h3 class="panel-title">监测数据查询</h3>
            </div>
            <div class="panel-body">            
              <div class="form-group">
                <label for="StartDate" class="control-label">起始时间</label>
                <div class="input-group date form_date" data-date="" data-date-format="yyyy年mm月dd日" data-link-field="StartDate" data-link-format="yyyy-mm-dd">
                    <input class="form-control" size="16" type="text" id="StartDateShow" runat=server value="" readonly>
                    <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
					<span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                </div>
				<input type="hidden" id="StartDate" runat=server value="2018-04-27" />
              </div>      
              <div class="form-group">
                <label for="dtp_input2" class="control-label">截止时间</label>
                <div class="input-group date form_date" data-date="" data-date-format="yyyy年mm月dd日" data-link-field="EndDate" data-link-format="yyyy-mm-dd">
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
                <button type="button" style="width:100%" class="btn btn-lg btn-primary">查询</button>
              </div>
            </div>
        </div>        
    </form>  
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
            format:"yyyy-mm-dd",
            forceParse: 0
        });
</script> 
</body>
</html>
