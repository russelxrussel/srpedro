using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SRPEDRO;

public partial class Summary_Report_Supplier : System.Web.UI.Page
{
    ReportDocument oReportDocument = new ReportDocument();
    Supplier_C oSupplier = new Supplier_C();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
            displaySupplierList();

        }

        displayReport();
    }

    protected void Page_UnLoad(object sender, EventArgs e)
    {

        //Cleaning Report Documents
        oReportDocument.Close();

    }

    private void displaySupplierList()
    {
        oSupplier.GET_SUPPLIER_LIST_DD(ddSupplierList);
        ddSupplierList.Items.Insert(0, new ListItem("-- ALL --"));
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
        myRangeValue.StartValue = dtStartDate; //txtDateStart.Text;
        myRangeValue.EndValue = dtEndDate;



        if (ddSupplierList.SelectedIndex == 0)
        {
            //This will display all transaction base on date range
            oReportDocument.Load(Server.MapPath("~/Reports/Supplier_All_Summary.rpt"));

            oReportDocument.SetParameterValue("DateRange", myRangeValue);
            oReportDocument.SetDatabaseLogon("sa", "p@ssw0rd"); // Supply user credentials

            CrystalReportViewer1.ReportSource = oReportDocument;
        }
        else
        {
            //This will display specific supplier transaction only
            oReportDocument.Load(Server.MapPath("~/Reports/Supplier_Details_Summary.rpt"));
            try
            {
                oReportDocument.SetParameterValue("SSNum", ddSupplierList.SelectedValue.ToString());
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