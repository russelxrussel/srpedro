<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
    
  body
    {
        
 margin-top: 20px;

    }
    
    .pnlTop
    {
    margin-top: 15px;    
    }
    
    </style>

    <title>Sr Pedro Warehouse Management System</title>

</head>
<body>
    
    <form id="form1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  

       <div class="container">
        <div class="panel  panel-primary">
           
         <div class="panel-heading">
                          <h2 class="panel-title">
                                    <strong>Sr Pedro Warehouse Management System</strong></h2>
                              
            </div>


        <!-- This will be the Body -->
        <div class="panel-body">
        <div class="row">
                    
                    <div class="col-md-5">
                  <%--  <img src="images/sp.png" class="img-rounded img-responsive" /> --%>
                    </div>
           
        
                    <div class="col-md-7 pnlTop">
                    
                     <!-- Time and Date -->
                    <div class="text-right text-warning">
                          <asp:UpdatePanel runat="server" ID="upDateTime" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="1000"></asp:Timer>
        <span class="glyphicon glyphicon-time"></span> <asp:Label runat="server" ID="lblDT"></asp:Label>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>
                    </div>


                      <asp:UpdatePanel runat="server" ID="upLogin" UpdateMode="Conditional">
                        <ContentTemplate>
                        <!--Panel for Login -->
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    Login Form</h4>
                            </div>

                            <!--Panel Body -->
                            <div class="panel-body">
                                <div class="input-group input-group-lg">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtUserId" placeholder="username" runat="server" CssClass="form-control focus" AutoCompleteType="Disabled"></asp:TextBox>
                                </div>
                                <br />
                                <div class="input-group input-group-lg">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-credit-card"></span>
                                    </span>
                                    <asp:TextBox ID="txtPassword" runat="server" placeholder="password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <br />
                                
                                <div class="text-right">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Login"
                                        OnClick="btnLogin_Click" />
                                </div>

                            </div>

                            <!--Panel Footer -->
                            
                            <div class="panel-footer">
                       <%-- <asp:LinkButton runat="server" id="lnkShowVide" class="btn btn-primary btn-sm" data-target="#modalVideo" data-toggle="modal"
                            data-backdrop="static">
                             <span class="glyphicon glyphicon-play-circle"></span> Watch Multiple Intelligences Animation
                        </asp:LinkButton> --%>
                      
                    </div>
                        
                        </div><!--End of Login Panel -->
                        
                        <%--Message POPUP--%>
                        <div class="modal fade" id="msgModal">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header bg-danger">
                                        <button class="close" data-dismiss="modal">
                                            &times;</button>
                                        <h4 class="modal-title">
                                            School Integrated Management System</h4>
                                    </div>
                                    <div class="modal-body">
                                        <h4>
                                            <span class="glyphicon glyphicon-alert"></span>&nbsp;
                                            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label></h4>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </div>
                        </div>

                            <%--Video POPUP--%>
                
                        <div class="modal fade" id="modalVideo">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header bg-primary">
                                        <button class="close" data-dismiss="modal">
                                            &times;</button>
                                        <h4 class="modal-title">
                                            Multiple Intelligences Video</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <video id="videoMI" class="img-responsive" controls>
                                        <source src="videos/mi1.mp4" type="video/mp4"></source>
                                        Your browser does not support HTML5 video.
                                        </video>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </div>
                            </div>

                        </ContentTemplate>
            </asp:UpdatePanel>
                    </div> <!--End of Column 2 right -->
            
        </div>
            </div>
           
        <!-- This will be the panel footer -->
       <div class="panel-footer bg-warning">
       <div class="text-right small text-warning">
       Powered by: Russel &COPY;
       </div>
       </div>


       </div> <!-- End of Primary Panel -->





 </div><!-- End of Container -->
 

       
    </form>

    <script src="bootstrap/js/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
   


</body>
</html>
