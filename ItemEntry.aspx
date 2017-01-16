<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="ItemEntry.aspx.cs" Inherits="ItemEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container container_content">

  <div class="panel panel-primary">
    <div class="panel-heading">
      <asp:Panel runat="server" ID="Panel3">
             <div class="input-group input-group-sm">
                 <ul class="list-inline">
                     <li>
                        
                         <asp:LinkButton runat="server" ID="lnkSaveItem" CssClass="btn btn-success" OnClick="lnkSaveItem_Click"><span class="glyphicon glyphicon-floppy-saved"></span> SAVE</asp:LinkButton></li>
                    
                 </ul>
             </div>
     </asp:Panel>
    </div>
    <div class="panel-body">

 <!-- ItemCode -->
        <div class="form-group row">
        
            <div class="col-md-2">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">CODE: </span>
                    <asp:TextBox runat="server" CssClass="form-control text-uppercase" ID="txtItemCode" MaxLength="6" placeholder="Item Code" required pattern="^[a-zA-Z ]+$"></asp:TextBox>
                </div>

            </div>
      
        </div>

        <!-- Item Description -->
        <div class="form-group row">

            <div class="col-md-4">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">NAME: </span>
                    <asp:TextBox runat="server" CssClass="form-control text-uppercase" ID="txtItemDescription" placeholder="Name / Description"></asp:TextBox>
                </div>

            </div>
        <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Item Description Required!" ControlToValidate="txtItemDescription"></asp:RequiredFieldValidator>--%>

        </div>

        <!-- UOM-->
        <asp:Panel runat="server" ID="Panel4">
   <div class="form-group row">
   
   <div class="col-md-3">
    <div class="input-group input-group-sm">
    <span class="input-group-addon"><span class="glyphicon glyphicon-record"></span></span><asp:DropDownList ID="ddUOM" runat="server" 
         CssClass="dropdown form-control"></asp:DropDownList>
     
    </div>
   </div>

       
   
   </div>  
            </asp:Panel>

         <!-- ITEM PRICE -->
        <div class="form-group row">
   
            <div class="col-md-2">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">PRICING:</span>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtItemPrice" placeholder="Price"></asp:TextBox>
                </div>

            </div>
         <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="* Item Price Required!" ControlToValidate="txtItemPrice"></asp:RequiredFieldValidator>--%>


        </div>

        <!-- BEGINNING BALANCE -->
        <div class="form-group row">
   
   <div class="col-md-2">
    <div class="input-group input-group-sm">
    <span class="input-group-addon">BALANCE:</span>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtBeginBal" placeholder="Beginning Balance"></asp:TextBox>
    </div>
   
   </div>
  <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Quantity Required!" ControlToValidate="txtBeginBal"></asp:RequiredFieldValidator>--%>
   
     
  
    
   
   </div> 

        <!-- STOCK LIMIT -->
        <div class="form-group row">
   
   <div class="col-md-2">
    <div class="input-group input-group-sm">
    <span class="input-group-addon">LIMIT QTY:</span>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtStockLimit" placeholder="Stock Limit"></asp:TextBox>
    </div>
   
   </div>
   <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Quantity Required!" ControlToValidate="txtStockLimit"></asp:RequiredFieldValidator>--%>
           
     
  
    
   
   </div> 
 
    </div>
   </div>

</div>

</asp:Content>

