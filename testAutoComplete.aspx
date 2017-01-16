<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="testAutoComplete.aspx.cs" Inherits="testAutoComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitialRequest);
        prm.add_endRequest(EndRequest);

        //Auto Complete Initial
        setAutoComplete();
        setCalendarInput();


    });

    function InitialRequest(sender, args) {
    }

    function EndRequest(sender, args) {
        setAutoComplete();
        setCalendarInput();
    }

    //    "WebService.asmx/GET_Supplier_List",
    function setAutoComplete() {
        $("<%= txtSearchSupplier.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "WebService.asmx/GET_Supplier_List",
                    method: "POST",
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ _supplierName: $("#<%= txtSearchSupplier.ClientID %>").val() }),
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('*')[1],
                                val: item.split('*')[0]
                            }
                        }))

                    },

                    error: function (err) {
                        alert('Error: ' + err);
                        //console.log('Error:', data);
                    }
                })
            },
            select: function (e, i) {
                $("[id*=hfSupplierCode]").val(i.item.val);
            },
            minLength: 2,
            appendTo: '#divSearch'
        });
    }



    // For Calendar Inputs
    function setCalendarInput() {
        $('.calendarInput').datepicker();
    }
</script>
    <div class="container container_content">
        <!--Search Supplier -->
        <div class="col-md-3">
            <div class="input-group input-group-sm">
                <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span>
                </span>
                <asp:TextBox runat="server" ID="txtSearchSupplier" CssClass="form-control" ClientIDMode="Static">
                </asp:TextBox>
                <div id="divSearch">
                </div>
                <!--Hidden Field -->
                <asp:HiddenField ID="hfSupplierCode" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>

