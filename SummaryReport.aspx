<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SummaryReport.aspx.cs" Inherits="SummaryReport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Sr Pedro Warehouse Management - Summary Report</title>

    <!--CSS Style Here -->
    <link href="bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/jquery-ui_srpedro.css" rel="stylesheet" type="text/css" />
    
   <!-- Javascript Here -->
    <script src="jscripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="jscripts/bootstrap.js" type="text/javascript"></script>

    <script src="bootstrap/js/jquery-ui_srpedro.js" type="text/javascript"></script>
    <script src="jscripts/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
   
   <script type="text/javascript">
       $(document).ready(function () {
           $('.calendarInput').datepicker();
       });

   

 
</script>

<style  type="text/css">
.container_content
{
padding-top: 10px;
}    
   
</style>  
</head>
<body>
    <form id="form1" runat="server">

    <div class="container container_content">
    
    <div class="row"><!--Main Row -->
        <div class="col-md-3">
            <!-- Left Column -->
        
                <ul class="list-group">
                <li class="list-group-item">
                <!-- Start Date-->
                <div class="input-group input-group-sm">
                    <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-calendar">
                    </span></span>
                    <asp:TextBox runat="server" ID="txtStartDate" CssClass="calendarInput form-control"
                        placeholder="Start Date"></asp:TextBox>
                </div>
                </li>
                
                <li class="list-group-item">
                <!-- End Date-->
                <div class="input-group input-group-sm">
                    <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-calendar">
                    </span></span>
                    <asp:TextBox runat="server" ID="txtEndDate" CssClass="calendarInput form-control"
                        placeholder="End Date"></asp:TextBox>
                </div>
                </li>
                
                <li class="list-group-item">
                <asp:LinkButton runat="server" ID="U_Print" CssClass="btn btn-success btn-sm" 
                        onclick="U_Print_Click"><span class="glyphicon glyphicon-print"></span> PREVIEW</asp:LinkButton>
                </li>
                
                </ul>
                
                
        
        </div><!-- End of Left Column -->
   

    <div class="col-md-9"><!-- Right Column -->
     <div class="row">

      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true"
            EnableParameterPrompt="False" 
            ToolPanelView="ParameterPanel" EnableDatabaseLogonPrompt="False" 
             ReuseParameterValuesOnRefresh="True" />
   
    </div><!-- End of Right Column -->
   

 
  </div><!--End of Main Row -->

    </div><!--End of Container -->

    </form>
</body>
</html>
