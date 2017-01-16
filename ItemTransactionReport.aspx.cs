using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ItemTransactionReport : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack)
        { 
           
            Page.Title = "Inventory Transactions Summary";

        }

        displayReport();
   }

    private void displayReport()
    {
        oReportDocument.Load(Server.MapPath("~/InventoryTransaction_Branch.rpt"));

        //oReportDocument.SetParameterValue("User", Session["U_USERNAME"].ToString());
        oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials

        
        crv.ReportSource = oReportDocument;
        
    }
}