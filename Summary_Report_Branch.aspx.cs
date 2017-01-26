using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SRPEDRO;

public partial class Summary_Report_Branch : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();
    Branch_C oBranch = new Branch_C();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
            displayBranchList();

        }

        displayReport();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {

        //Cleaning Report Documents
        oReportDocument.Close();

    }

    private void displayBranchList()
    {
        oBranch.GET_BRANCH_LIST_DD(ddBranchList);
        ddBranchList.Items.Insert(0, new ListItem("-- ALL --"));
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



        if (ddBranchList.SelectedIndex == 0)
        {
            //This will display all transaction base on date range
            oReportDocument.Load(Server.MapPath("~/Reports/Branch_All_Summary.rpt"));

            oReportDocument.SetParameterValue("DateRange", myRangeValue);
            oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); 

            CrystalReportViewer1.ReportSource = oReportDocument;
        }
        else
        {
            //This will display specific branch transaction only
            oReportDocument.Load(Server.MapPath("~/Reports/Branch_Details_Summary.rpt"));
            try
            {
                oReportDocument.SetParameterValue("BSNum", ddBranchList.SelectedValue.ToString());
                oReportDocument.SetParameterValue("DateRange", myRangeValue);
                oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials

                CrystalReportViewer1.ReportSource = oReportDocument;
            }
            catch
            {

            }

        }

    }


}