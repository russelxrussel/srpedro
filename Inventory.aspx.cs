using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblWarning = (Label)e.Row.Cells[7].FindControl("stockLimitMessage");

            int iRunningStock = Convert.ToInt32(e.Row.Cells[5].Text);
            int iStockLimit = Convert.ToInt32(e.Row.Cells[6].Text);

            if (iStockLimit >= iRunningStock)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                lblWarning.Text = "Warning Stock Limit Reached!";
            }
        }
    }
}