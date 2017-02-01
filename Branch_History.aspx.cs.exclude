using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SRPEDRO;

public partial class Branch_History : System.Web.UI.Page
{
    Main_C oXtra = new Main_C();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            displayBranchList();
        }

        
    }

    private void displayBranchList()
    {
        oXtra.GetBranchList(ddBranchRecords);
        ddBranchRecords.Items.Insert(0, new ListItem("-- SELECT BRANCH --"));


    }

    protected void ddBranchRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = oXtra.getBranchTransaction(ddBranchRecords.SelectedValue.ToString());
        gvBranchRecords.DataSource = dt;
        gvBranchRecords.DataBind();

        lblSummaryPriceBranch.Text = "Total Branch Expense: "  + oXtra.getBranchTotalPrice(ddBranchRecords.SelectedValue.ToString()).ToString();
       
    }
}