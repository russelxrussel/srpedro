<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="SupplierEntry.aspx.cs" Inherits="SupplierEntry" %>

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

<asp:UpdatePanel runat="server" ID="uplMain">
<ContentTemplate>


<div class="container container_content">

<div class="row">
    
    <div class="col-md-3">
    
    

 <asp:LinkButton runat="server" ID="lnkCreate" 
        CssClass="btn btn-primary btn-sm" OnClick="lnkCreate_Click"><span class="glyphicon glyphicon-plus-sign"></span> CREATE</asp:LinkButton>
</div>

<%-- <div class="col-md-3 col-md-offset-6">
                        <div class="input-group input-group-sm">
                            <asp:TextBox runat="server" ID="txtPrintItem" CssClass="form-control" placeholder="Print Item History"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton runat="server" ID="lnk" CssClass="btn btn-primary btn-sm">Print</asp:LinkButton>
                            </span>
                        </div>
                    </div>--%>
    </div>
 <hr />
  <div class="panel panel-warning">
    <div class="panel-heading">
      List of Branch
    </div>
    <div class="panel-body">

    <!-- Display Gridview List of Items -->
    <asp:GridView runat="server" ID="gvSupplierList" 
            CssClass="table table-responsive table-hover table-condensed table-bordered" AutoGenerateColumns="False">
    
        <Columns>
            <asp:TemplateField>
            <ItemTemplate>
            <asp:LinkButton ID="lnkEditItem" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkEditItem_Click">Edit</asp:LinkButton>
            </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="SupplierCode" HeaderText="Code" />
            <asp:BoundField DataField="SupplierName" HeaderText="Name" />
            <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" />
            <asp:BoundField DataField="Address" HeaderText="Address" />
             
                         
           
        </Columns>
    
    </asp:GridView>
 
    </div>
   </div>


        <!--MESSAGE MODAL SECTION-->

        <!--Create Update Container -->
        <div class="modal fade" id="SupplierContainer" data-backdrop="static" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header bg-warning">
                    <button class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><asp:Label runat="server" ID="lblActionTitle"></asp:Label></h4>
                </div>

                <div class="modal-body">

                    <ul class="list-group">
                    
                    <li class="list-group-item">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon">NAME: </span>
                            <asp:TextBox runat="server" CssClass="form-control text-uppercase" ID="txtSupplierName"
                                placeholder="Name / Description"></asp:TextBox>
                        </div>
                    </li>

                
                   <li class="list-group-item">
                       <div class="row">
                           <!-- CONTACT PERSON -->
                           <div class="col-md-4">
                               <div class="input-group input-group-sm">
                                   <span class="input-group-addon"></span>
                                   <asp:TextBox runat="server" CssClass="form-control  text-uppercase" ID="txtContactPerson" placeholder="Contact Person"></asp:TextBox>
                               </div>
                           </div>
                           <!-- TELEPHONE -->
                           <div class="col-md-4">
                               <div class="input-group input-group-sm">
                                   <span class="input-group-addon"></span>
                                   <asp:TextBox runat="server" CssClass="form-control" ID="txtTelephone" placeholder="Land line Number"></asp:TextBox>
                               </div>
                           </div>
                           <!-- MOBILE -->
                           <div class="col-md-4">
                               <div class="input-group input-group-sm">
                                   <span class="input-group-addon"></span>
                                   <asp:TextBox runat="server" CssClass="form-control" ID="txtMobile" placeholder="Mobile Number"></asp:TextBox>
                               </div>
                           </div>
                       </div>
                    </li>

                    <li class="list-group-item">
                                <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" Rows="2" placeholder="Branch Address" CssClass="form-control"></asp:TextBox>

                    </li>

                    
               </ul>
              
              </div>

                <div class="modal-footer">
                <asp:LinkButton runat="server" ID="lnkCreateUpdate" 
                        CssClass="btn btn-success btn-sm" onclick="lnkCreateUpdate_Click"></asp:LinkButton>
               
                <asp:LinkButton runat="server" ID="lnkClose" CssClass="btn btn-danger btn-sm" data-dismiss="modal">Cancel</asp:LinkButton>

                </div>

            </div>
            </div>

          
       </div>


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


</div><!-- END OF DIV CONTAINER-->

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

