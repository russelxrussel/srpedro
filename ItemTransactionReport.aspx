<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemTransactionReport.aspx.cs" Inherits="ItemTransactionReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%--  <CR:CrystalReportViewer ID="" runat="server" AutoDataBind="true" />--%>

        <CR:CrystalReportViewer ID="crv" runat="server" AutoDataBind="true" 
            HasCrystalLogo="False" ToolPanelView="None" 
            ToolPanelWidth="200px" Width="1024px" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="True" HasToggleGroupTreeButton="False" 
            HasToggleParameterPanelButton="False" ReuseParameterValuesOnRefresh="True" 
             />
    </div>
    </form>
</body>
</html>
