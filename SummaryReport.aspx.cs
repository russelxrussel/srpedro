using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class SummaryReport : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();

            displayReport();
        }
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {

        //Cleaning Report Documents
        oReportDocument.Close();

    }


    protected void U_Print_Click(object sender, EventArgs e)
    {
        displayReport();
    }

    /*
     Function / Routine
     */

    private void displayReport()
    {
        ParameterRangeValue myRangeValue = new ParameterRangeValue();
        myRangeValue.StartValue = txtStartDate.Text; //txtDateStart.Text;
        myRangeValue.EndValue = txtEndDate.Text;


        oReportDocument.Load(Server.MapPath("~/Reports/Supplier_Details_Cost_Specific.rpt"));


        oReportDocument.SetParameterValue("DateRange", myRangeValue);
        oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials
        
        CrystalReportViewer1.ReportSource = oReportDocument;

    }

   
}