<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="BOTransaction.aspx.cs" Inherits="Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
  <style type="text/css">
   
   /*   
   #divSearch
      {
           position: relative;
           display:block;
           top: 30px;
      }
  */
  
    textarea
        {
        resize: none;    
        }
        
  </style>
   

   
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
        $("#<%= txtSearchSupplier.ClientID %>").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "WebService.asmx/GETSupplierList",
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
                       alert('Error' + err);
                       // console.log('Error:', data);
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

    <asp:UpdatePanel runat="server" ID="uplSupplier">
        <ContentTemplate>

            <div class="container container_content">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Panel runat="server" ID="xPanelControls">
                            <asp:LinkButton runat="server" ID="U_Save_S" CssClass="btn btn-success btn-sm" OnClick="U_Save_S_Click"><span class="glyphicon glyphicon-floppy-saved"></span> SAVE</asp:LinkButton>
                        </asp:Panel>
                    </div>
                    
                    <div class="col-md-3 col-md-offset-6">
                        <div class="input-group input-group-sm">
                            <asp:TextBox runat="server" ID="txtPrintTransaction" CssClass="form-control" placeholder="Print Branch Transaction #"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton runat="server" ID="lnkPrintTransaction" CssClass="btn btn-primary btn-sm"
                                    OnClick="lnkPrintTransaction_Click">Print</asp:LinkButton>
                            </span>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="panel panel-info">
                    <div class="panel-heading">
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <!--Main Row -->
                            <!--Left Column -->
                            <div class="col-md-4">
                                <!--Supplier Panel -->
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        Branch Selection
                                    </div>
                                    <div class="panel-body">
                                        <ul class="list-group">
                                            <li class="list-group-item">
                                                <!-- Document Date-->
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-calendar">
                                                    </span></span>
                                                    <asp:TextBox runat="server" ID="txtDateTrans" CssClass="calendarInput form-control"
                                                        placeholder="Document Date"></asp:TextBox>
                                                </div>
                                            </li>
                                            <li class="list-group-item">
                                                <!-- Release Date-->
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-calendar">
                                                    </span></span>
                                                    <asp:TextBox runat="server" ID="txtDateRelease" CssClass="calendarInput form-control"
                                                        placeholder="Release Date"></asp:TextBox>
                                                </div>
                                            </li>
                                            <li class="list-group-item">
                                                <!-- Branch -->
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-record"></span>
                                                    </span>
                                                    <asp:DropDownList ID="ddBranchList" runat="server" CssClass="dropdown form-control" OnSelectedIndexChanged="ddBranchList_SelectedIndexChanged"
                                                         AutoPostBack="True">
                                                    </asp:DropDownList>
                                             <%--   </div>

                                                 <!--Search Supplier -->
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span>
                                            </span>
                                            
                                            <div id="divSearch">
                                            </div>--%>
                                            <strong>
                                            <asp:Label runat="server" ID="lblBranchManager" CssClass="form-control"></asp:Label></strong>
                                             <asp:Label runat="server" ID="lblBranchContact" CssClass="form-control"></asp:Label></strong>
                                            <asp:Label runat="server" ID="lblBranchContactNumbers" CssClass="form-control text-nowrap"></asp:Label>
                                            <asp:Label runat="server" ID="lblBranchAddress" CssClass="form-control text-nowrap"></asp:Label>
                                            <!--Hidden Field -->
                                            <asp:HiddenField ID="hfSupplierCode" runat="server" />

                                            <asp:TextBox runat="server" ID="txtSearchSupplier" Visible="False" CssClass="form-control" ClientIDMode="Static">
                                            </asp:TextBox>
                                        </div>
                                            </li>

                                            <li class="list-group-item">
                                                <!-- Remarks-->
                                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" placeholder="General Remarks"
                                                    Rows="1" TextMode="MultiLine"></asp:TextBox>
                                            </li>
                                        </ul>
                                       
                                    </div>
                                    <!--End of Supplier Panel Body -->
                                </div>
                                <!--End of Supplier Panel -->
                            </div>
                            <!--End of Left Column -->
                            <!--Right Column -->
                            <div class="col-md-8">
                                <!--Item Panel -->
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        Items Selection
                                    </div>
                                    <div class="panel-body">
                                        <ul class="list-group">
                                            <li class="list-group-item">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="input-group input-group-sm">
                                                            <span class="input-group-addon alert-danger"><span class="glyphicon glyphicon-record">
                                                            </span></span>
                                                            <asp:DropDownList ID="ddItemList" runat="server" AutoPostBack="True" CssClass="dropdown form-control"
                                                                OnSelectedIndexChanged="ddItemList_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="input-group input-group-sm">
                                                            <span class="input-group-addon alert-warning">UOM</span><asp:Label runat="server"
                                                                ID="lblUOM" CssClass="form-control"></asp:Label>
                                                            <span class="input-group-addon alert-warning">PRICE</span><asp:Label runat="server"
                                                                ID="lblPrice" CssClass="form-control text-danger"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </li>
                                            <li class="list-group-item">
                                                <div class="row">
                                                    <!--Quantity Input -->
                                                    <div class="col-md-6">
                                                        <div class="input-group input-group-sm">
                                                            <span class="input-group-addon alert-danger">Quantity</span><asp:TextBox runat="server"
                                                                CssClass="form-control" ID="txtQuantity" placeholder="Quantity"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <!--Add Button -->
                                                    <div class="col-md-6">
                                                        <asp:LinkButton runat="server" ID="lnkAdd" CssClass="btn btn-warning btn-sm" OnClick="lnkAdd_Click"><span class="glyphicon glyphicon-plus-sign"></span> ADD</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                            
                                        </ul>
                                        <!--Display added  Items -->
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                List of Item(s) release to Branch
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <asp:GridView runat="server" ID="gvBranchItems" CssClass="table table-responsive table-condensed table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkRemoveItem" runat="server" CssClass="btn btn-danger btn-sm"
                                                                        OnClick="lnkRemoveItem_Click">X</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!--End of Display Panel Body -->
                                            <div class="panel-footer">
                                                <div class="text-right">
                                                    <strong>
                                                        <asp:Label runat="server" ID="lblRunningTotal" CssClass="text-danger"></asp:Label></strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End of Item Panel Body -->
                                </div>
                            </div>
                            <!--End of Right Column -->
                        </div>
                        <!--End of Main Row -->
                    </div>
                    <!--End of Panel Body -->
                    <!--MESSAGE MODAL SECTION-->
                    <!--Message Save SUCCESS-->
                    <div class="modal fade" id="msgSuccessModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header bg-success">
                                    <button class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        Sr Pedro Warehouse System</h4>
                                </div>
                                <div class="modal-body">
                                    <h4>
                                        <span class="glyphicon glyphicon-success"></span>&nbsp;
                                        <asp:Label runat="server" ID="lblMessageSuccess"></asp:Label></h4>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Message Error -->
                    <div class="modal fade" id="msgErrorModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        Sr Pedro Warehouse System</h4>
                                </div>
                                <div class="modal-body">
                                    <h4>
                                        <span class="glyphicon glyphicon-alert"></span>&nbsp;
                                        <asp:Label runat="server" ID="lblErrorPrompt"></asp:Label></h4>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--End of Main Panel Body -->
                <!-- End of uplSupplier -->
            </div>

            <!-- End of Main Panel -->
            <!-- End of Container -->

        </ContentTemplate>
    </asp:UpdatePanel>  
     

</asp:Content>

