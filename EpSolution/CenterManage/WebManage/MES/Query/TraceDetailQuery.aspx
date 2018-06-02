<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TraceDetailQuery.aspx.cs" Inherits="Manage.Web.MES.Query.TraceDetailQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>追溯明细查询</title>
    <asp:PlaceHolder ID="MyCSS" runat="server"></asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="easyui-panel" title="基本信息" style="padding:3px;width:700px">
        <table class="viewTable" cellpadding=0 cellspacing=0>
            <tr>
                <th nowrap=nowrap>产品名称</th>
                <td nowrap=nowrap>                    
                    <asp:Label ID="ProductName" runat="server" Text="ProductName"></asp:Label>
                    
                </td>                
                <td align=left rowspan="9">                    
                    <video src="/movie.ogg" controls="controls">
                                your browser does not support the video tag
                    </video>
                </td>                
            </tr>
            <tr>
                <th nowrap=nowrap>产品规格</th>
                <td nowrap=nowrap>                   
                    <asp:Label ID="SPECIFICATION" runat="server" Text="SPECIFICATION"></asp:Label>
                   
                </td>
            </tr>
            <tr>
                <th nowrap=nowrap>生产时间</th>
                <td nowrap=nowrap>
                    
                    <asp:Label ID="ProduceDate" runat="server" Text="ProduceDate"></asp:Label>
                </td>                
            </tr>
            <tr>
                <th nowrap=nowrap>生产批次</th>
                <td nowrap=nowrap>
                   
                    <asp:Label ID="BatchNumber" runat="server" Text="BatchNumber"></asp:Label>
                </td>
            </tr>
            <tr>
                <th nowrap=nowrap align="right">工厂</th>
                <td nowrap=nowrap>
                    
                    <asp:Label ID="FactoryName" runat="server" Text="FactoryName"></asp:Label>
                </td>                
            </tr>
            <tr>
                <th nowrap=nowrap align="right">生产线</th>
                <td nowrap=nowrap>
                   
                    <asp:Label ID="LineName" runat="server" Text="LineName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th nowrap=nowrap align="right">工艺</th>
                <td nowrap=nowrap>
                    
                    <asp:Label ID="FlowName" runat="server" Text="FlowName"></asp:Label>
                </td>                
            </tr>
            <tr>
                <th nowrap=nowrap align="right">班组</th>
                <td nowrap=nowrap>
                   
                    <asp:Label ID="WorkGroupName" runat="server" Text="WorkGroupName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>&nbsp;</th>
                <td>
                    
                    &nbsp;</td>
                
            </tr>
        </table>
    </div>
    <div class="easyui-panel" title="原料组成" style="padding:3px;width:700px;height:200px">
        <table id="tblmat" style="height:100px;width:100%" iconCls="icon-edit">
               <thead>
			                                        <tr>                                   
				                                        <th field="MatBarCode" align="center" width="130">原料条码</th>
                                                        <th field="MatCode" align="center" width="100">原料编号</th>
                                                        <th field="MatName" align="center" width="100">原料名称</th> 
                                                        <th field="CREATETIME" align="center" width="150">上线时间</th>                                                      
			                                        </tr>			                    
		        </thead>
	    </table> 
    </div>
    <div class="easyui-panel" title="加工工序" style="padding:3px;width:700px;">
        <table id="tblProcess" style="height:200px;width:100%" iconCls="icon-edit">
                <thead>
			                                        <tr>                                   
				                                        <th field="ProcessCode" align="center" width="100">工序编号</th>
                                                        <th field="ProcessName" align="center" width="120">工序名称</th>
                                                        <th field="WorkingStartTime" align="center" width="150">加工开始时间</th>    
                                                        <th field="WorkingEndTime" align="center" width="150">加工结束时间</th> 
                                                        <th field="EquCode" align="center" width="60">设备编号</th>
                                                        <th field="EquName" align="center" width="120">设备名称</th>                                                    
			                                        </tr>			                    
		       </thead>
	    </table> 
    </div>
    
    </div>
    <script language="javascript" type="text/javascript">
        $(function () {
            var matList = JSON.parse($("#HiMaterial").val());
            $('#tblmat').datagrid({
            });
            $('#tblmat').datagrid("loadData", matList);

            var processList = JSON.parse($("#HiProcess").val());
            $('#tblProcess').datagrid({
            });
            $('#tblProcess').datagrid("loadData", processList);
        });
    </script>  
    <asp:HiddenField ID="HiMaterial" runat="server" />
    <asp:HiddenField ID="HiProcess" runat="server" />
    </form>
    </body>
</html>
