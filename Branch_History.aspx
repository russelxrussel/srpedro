<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="Branch_History.aspx.cs" Inherits="Branch_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="container container_content">
  
  <div class="panel panel-warning">
    <div class="panel-heading">
    Branch Transaction Summary
    </div>
        <!-- Selection Branch -->
        <asp:UpdatePanel runat="server" ID="upBranchRecords" UpdateMode="Conditional">
            <ContentTemplate>
    <div class="panel-body">

  
        

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-group row">
                    <div class="col-md-3">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-record"></span></span>
                            <asp:DropDownList ID="ddBranchRecords" runat="server"
                                CssClass="dropdown form-control" AutoPostBack="true" 
                                onselectedindexchanged="ddBranchRecords_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                    </div>

                    <div class="col-md-5">
                        <asp:Label runat="server" ID="lblSummaryPriceBranch"></asp:Label>

                    </div>


                </div>

                <asp:GridView runat="server" ID="gvBranchRecords" CssClass="table table-responsive">

                </asp:GridView>
              

       </ContentTemplate>
        </asp:UpdatePanel>
      
 
    </div>
    
    </ContentTemplate>
        </asp:UpdatePanel>
   
   </div>

   </div>
</asp:Content>

