using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SRPEDRO;

public partial class Summary_Report_Item : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
          

        }

        displayReport();
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
        DateTime dtStartDate = Convert.ToDateTime(txtStartDate.Text);
        DateTime dtEndDate = Convert.ToDateTime(txtEndDate.Text);

        ParameterRangeValue myRangeValue = new ParameterRangeValue();
        myRangeValue.StartValue = dtStartDate; 
        myRangeValue.EndValue = dtEndDate;



      
            oReportDocument.Load(Server.MapPath("~/Reports/Items_All_Summary.rpt"));

            oReportDocument.SetParameterValue("DateRange", myRangeValue);
            oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); 

            CrystalReportViewer1.ReportSource = oReportDocument;
      

    }


    protected void lnkHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("home.aspx");
    }
}