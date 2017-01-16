<%@ Page Title="" Language="C#" MasterPageFile="~/mstrPage.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

  

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
       $(document).ready(function () {
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_initializeRequest(InitialRequest);
           prm.add_endRequest(EndRequest);

           //Auto Complete Initial
           //SetAutoComplete();
           //SetAccordion();
           setCalendarInput();

       });

       function InitialRequest(sender, args) {
       }

       function EndRequest(sender, args) {
           //SetAutoComplete();
           setCalendarInput();
       }


       //For Calendar Inputs
       function setCalendarInput() {
           $('.calendarInput').datepicker();
       }
   
</script>

<asp:UpdatePanel runat="server" ID="upMAIN" UpdateMode="Conditional">
<ContentTemplate>

<div class="container container_content">
   
<div class="row">
<div class="col-md-12">
  <!-- TAB Controls -->
  <ul class="nav nav-tabs">
  <li class="active bg-warning"><a href="#tab1" data-toggle="tab">Inventory Data</a></li>
        <li class="bg-warning"><a href="#tabItemEntry" data-toggle="tab">Item Data Entry</a></li>
  <li class="bg-warning"><a href="#tab2" data-toggle="tab">Transaction</a></li>
  <li class="bg-warning"><a href="#tab3" data-toggle="tab">Branch Data</a></li>
  
  </ul>
   
   <div class="tab-content">
 
            <!-- Inventory Section -->
   <div class="tab-pane active" id="tab1">
   <br />
  
    
    </div> <!-- End of TAB Content -->

<!-- Item Data Entry -->
   <div class="tab-pane" id="tabItemEntry">
    <br />

  
   
   </div> <!--End of Item TAB -->


<!-- Transaction Section -->

   <div class="tab-pane" id="tab2">
    <br />


  

    </div>
       <!--End of Transaction TAB -->

   
   

  
    <!-- Branch Data Section -->
   <div class="tab-pane" id="tab3">
            <br />

  

       </div>   
   


   
 </div> 

<!-- End of TAB Controls -->
 </div>

 </div>

</div>

</ContentTemplate>
</asp:UpdatePanel><!-- End of Main Update Panel -->

</asp:Content>

