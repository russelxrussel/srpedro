<%@ Page Language="C#" AutoEventWireup="true" CodeFile="t1.aspx.cs" Inherits="t1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="cssStyle/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="jscripts/jquery-3.1.1.js" type="text/javascript"></script>
    <script src="jscripts/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtSearchSupplier').autocomplete({
                source: [{ label: "Choice1", value: "value1" }, { label: "ChoiceX", value: "value2"}]

            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:TextBox runat="server" ID="txtSearchSupplier" CssClass="form-control"> </asp:TextBox>
    </div>
    </form>
</body>
</html>
