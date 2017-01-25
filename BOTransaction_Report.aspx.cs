using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class SITransaction_Report : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        oReportDocument.Load(Server.MapPath("~/Reports/Branch_Item_Trans.rpt"));


        oReportDocument.SetParameterValue("BSNUM", oGlobal.G_BSNUM); // Set Parameter
            oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials
            CrystalReportViewer1.ReportSource = oReportDocument;
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {
       
        //Cleaning Report Documents
        oReportDocument.Close();

    }
}