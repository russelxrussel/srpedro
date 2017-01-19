using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SRPEDRO;

public partial class ItemEntry : System.Web.UI.Page
{
    Main_C oXtra = new Main_C();

    Item_C oItem = new Item_C();
    SeriesNumber_C oSeries = new SeriesNumber_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            displayItemList();

            displayUOMList();

        }
    }

    private void displayItemList()
    {
        DataTable dt = new DataTable();
        dt = oItem.GET_ITEM_LIST();

        gvItemList.DataSource = dt;
        gvItemList.DataBind();
    }

    private void displayUOMList()
    {
        oXtra.GetItemUOM(ddUOM);
        ddUOM.SelectedIndex = 0;
    }


    private void clearItemDataFields()
    {
        txtItemCode.Text = "";
        txtItemDescription.Text = "";
        txtBeginBal.Text = "";
        txtStockLimit.Text = "";
        txtItemPrice.Text = "";
        ddUOM.SelectedIndex = 0;
    }

    protected void lnkSaveItem_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtItemCode.Text) && !string.IsNullOrEmpty(txtItemDescription.Text))
        {
            oXtra.INSERT_ITEM_MASTER(txtItemCode.Text, txtItemDescription.Text, ddUOM.SelectedValue.ToString(), double.Parse(txtItemPrice.Text));
            oXtra.INSERT_ITEM_INVENTORY(txtItemCode.Text, double.Parse(txtBeginBal.Text), double.Parse(txtStockLimit.Text));

            clearItemDataFields();
            //Recreate temporary table



        }
        else
        {
            //No effect!
        }
    }

    protected void lnkCreateItem_Click(object sender, EventArgs e)
    {
        ViewState["ACTION"] = "ADD";         
        lblActionTitle.Text = "Create New Item" + oSeries.GENERATE_SERIES_NUMBER_MASTER("ITM");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#ItemContainer').modal('show');</script>", false);
    }

    //EDIT Button Action
    protected void lnkEditItem_Click(object sender, EventArgs e)
    {
        //THIS WILL GET THE CELL VALUE OF SELECTED ROW IN GRIDVIEW VIA COMMAND WITHIN TEMPLATE FIELDS.
        var selEdit = (Control)sender;
        GridViewRow r = (GridViewRow)selEdit.NamingContainer;
        // string selAppNum = r.Cells[2].Text;
        string selCode = r.Cells[1].Text;

        DataTable dt = oItem.GET_ITEM_LIST();

        DataRow[] dr;
        dr = dt.Select("ItemCode = '" + selCode + "'");

        if (dr.Length > 0)
        {
            //Will display the selected item info
            foreach (DataRow row in dr)
            {
                txtItemDescription.Text = row["ItemName"].ToString();
                ddUOM.SelectedValue = row["UomCode"].ToString();
                txtItemPrice.Text = row["ItemPrice"].ToString();
                txtStockLimit.Text = row["MinimumStockLevel"].ToString();
                txtBeginBal.Text = row["BegStock"].ToString();
                txtItemRemarks.Text = row["Remarks"].ToString();


                chkItemStatus.Checked = (bool)row["ItemStatus"];

            }
        }

        ViewState["ACTION"] = "MODIFY";
        lblActionTitle.Text = "Modify Item" + selCode;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#ItemContainer').modal('show');</script>", false);
      
    }

}