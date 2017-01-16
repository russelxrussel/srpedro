<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container container_content">

  <div class="panel panel-warning">
    <div class="panel-heading">
    Inventory - Item List
    </div>
    <div class="panel-body">
 
    <div class="row">
       <div class="col-md-12">

           <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" DataSourceID="sq1" OnRowDataBound="GridView1_RowDataBound">
               <Columns>
                   <asp:BoundField DataField="ItemCode" HeaderText="Item" SortExpression="ItemCode" Visible="false" />
                   <asp:BoundField DataField="ItemName" HeaderText="Item" SortExpression="ItemName" />
                   <asp:BoundField DataField="BegStock" HeaderText="Beginning Stock" SortExpression="InStock" />
                   <asp:BoundField DataField="InStock" HeaderText="Stock - In" SortExpression="InStock" />
                   <asp:BoundField DataField="OutStock" HeaderText="Stock - Out" SortExpression="OutStock" />
                   <asp:BoundField DataField="RunningStock" HeaderText="Available Stock" SortExpression="RunningStock" />
                   <asp:BoundField DataField="MinimumStockLevel" HeaderText="Stock Limit" SortExpression="MinimumStockLevel" ItemStyle-ForeColor="Red" />
                  
                   <asp:TemplateField>
                       <ItemTemplate>
                           <%--<asp:LinkButton runat="server" ID="lnkViewTransaction">View Transaction</asp:LinkButton>--%>
                           
                           <asp:Label runat="server" ID="stockLimitMessage" CssClass="text-danger"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
           </asp:GridView>


           <asp:SqlDataSource ID="sq1" runat="server" ConnectionString="<%$ ConnectionStrings:CSPEDRO %>" 
               SelectCommand="SELECT Master.Item_Data.ItemCode, Master.Item_Data.ItemName, Master.Inventory_Data.BegStock, Master.Inventory_Data.InStock, Master.Inventory_Data.OutStock, Master.Inventory_Data.RunningStock, Master.Inventory_Data.MinimumStockLevel FROM Master.Inventory_Data INNER JOIN Master.Item_Data ON Master.Inventory_Data.ItemCode COLLATE Latin1_General_CI_AS = Master.Item_Data.ItemCode ORDER BY Master.Item_Data.ItemName" 
               ProviderName="<%$ ConnectionStrings:CSPEDRO.ProviderName %>"></asp:SqlDataSource>


           </div>

 
    </div>
   </div>
   </div>

   </div> <!-- End of Container -->
</asp:Content>

