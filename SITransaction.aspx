﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="SITransaction.aspx.cs" Inherits="Transaction" %>

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
        <div class="panel panel-default">
            <div class="panel-heading">
                <asp:Panel runat="server" ID="xPanelControls">
                    <div class="input-group input-group-sm">
                        <ul class="list-inline">
                            <li>
                                <asp:LinkButton runat="server" ID="U_Save_S" CssClass="btn btn-success" OnClick="U_Save_S_Click"><span class="glyphicon glyphicon-floppy-saved"></span> SAVE</asp:LinkButton></li>
                        </ul>
                    </div>
                </asp:Panel>
            </div>
         
                    <div class="panel-body">
                        <!--Row 1 -->
                        <div class="form-group row">
                            <!-- Supplier-->
                            <div class="col-md-3">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-record"></span>
                                    </span>
                                    <asp:DropDownList ID="ddSupplierList" runat="server" CssClass="dropdown form-control"
                                        OnSelectedIndexChanged="ddSupplierList_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- Document Date-->
                            <div class="col-md-3 col-md-push-6">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtDateTrans" CssClass="calendarInput form-control"
                                        placeholder="Document Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!--End of Row 1 -->
                        <!--Row 2 -->
                        <div class="form-group row">
                            <!--Search Supplier -->
                            <div class="col-md-3">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtSearchSupplier" CssClass="form-control" ClientIDMode="Static">
                                    </asp:TextBox>
                                    <div id="divSearch">
                                    </div>
                                    <strong><asp:Label runat="server" ID="lblSupplierContact" CssClass="form-control"></asp:Label></strong>
                                    <asp:Label runat="server" ID="lblSupplierNumbers" CssClass="form-control"></asp:Label>
                                    <asp:Label runat="server" ID="lblSupplierAddress" CssClass="form-control"></asp:Label>
                                    <!--Hidden Field -->
                                    <asp:HiddenField ID="hfSupplierCode" runat="server" />
                                </div>
                            </div>

                              <!-- Remarks-->
                            <div class="col-md-3 col-md-4">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" placeholder="Remarks"
                                    Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </div>

                            <!-- Delivery Date-->
                            <div class="col-md-3 col-md-push-3">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtDeliveryDate" CssClass="calendarInput form-control"
                                        placeholder="Delivery Date"></asp:TextBox>
                 
                                </div>
                            </div>
                        </div>
                        <!--End of Row 2 -->

                    
                        <hr />
                        <div class="row">
                            <!--Transaction Content-->
                            <!--Items Input -->
                            <div class="col-md-4">
                                <!--Left Column -->
                                <div class="panel panel-warning">
                                    <div class="panel-heading">
                                        Items Selection
                                    </div>
                                    <div class="panel-title">
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon"><span class="glyphicon glyphicon-record"></span>
                                                    </span>
                                                    <asp:DropDownList ID="ddItemList" runat="server" AutoPostBack="True" CssClass="dropdown form-control"
                                                        OnSelectedIndexChanged="ddItemList_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                        <div class="col-md-12">
                                          <div class="input-group input-group-sm">
                                            <span class="input-group-addon">UOM</span><asp:Label runat="server" ID="lblUOM" CssClass="form-control"></asp:Label>
                                                      <span class="input-group-addon">PRICE</span><asp:Label runat="server" ID="lblPrice" CssClass="form-control text-danger"></asp:Label>
                                          </div>
                                        </div>
                                        </div>
                                        
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Quantity<%--<span class="glyphicon glyphicon-asterisk"></span>--%>
                                                    </span>
                                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" placeholder="Quantity"></asp:TextBox>
                                                </div>
                                            </div>
                                       
                                            <div class="col-md-6 text-right">
                                                <asp:LinkButton runat="server" ID="lnkAdd" CssClass="btn btn-warning btn-sm" OnClick="lnkAdd_Click"><span class="glyphicon glyphicon-plus"></span> ADD</asp:LinkButton></li>
                                            </div>
                                        </div>
                                        <!--End of Add button row -->
                                    </div>
                                    <!--End of Left column Body -->
                                </div>
                            </div>
                            <!--End of Left Column -->
                            <div class="col-md-8">
                                <!--Display Items added -->
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="panel-title">
                                                    List of Order Items</div>
                                            </div>

                                            <div class="col-md-6 text-right">
                                            <strong><asp:Label runat="server" ID="lblRunningTotal" CssClass="text-danger"></asp:Label></strong>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <asp:GridView runat="server" ID="gvSupplierItems" CssClass="table table-responsive">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkRemoveItem" runat="server" CssClass="btn btn-danger btn-sm"
                                                                OnClick="lnkRemoveItem_Click">Remove</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <!--End of Display Panel Body -->
                                </div>
                            </div>
                        </div>
                        <!--End of Content Row -->
                      
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
        </div> <!-- End of Main Panel -->

      </div> <!-- End of Container -->
       
           </ContentTemplate>
            </asp:UpdatePanel>   

</asp:Content>

