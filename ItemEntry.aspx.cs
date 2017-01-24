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
    Util_C oUtil = new Util_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            displayItemList();
            displayLocationList();
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

    private void displayLocationList()
    {
        DataTable dt = new DataTable();
        dt = oUtil.GET_LOCATION_LIST();

        ddLocation.DataSource = dt;
        ddLocation.DataTextField = dt.Columns["LocationDesc"].ToString();
        ddLocation.DataValueField = dt.Columns["LocationCode"].ToString();
        ddLocation.DataBind();
    }


    private void clearItemDataFields()
    {
        //txtItemCode.Text = "";
        txtItemDescription.Text = "";
        txtStockLimit.Text = "";
        txtItemPrice.Text = "";
        ddUOM.SelectedIndex = 0;
    }

   

    protected void lnkCreateItem_Click(object sender, EventArgs e)
    {
        ViewState["ACTION"] = "ADD"; 

        //Text of Save Button
        lnkCreateUpdate.Text = "SAVE";

        //Enable UOM
        ddUOM.Enabled = true;

        chkItemStatus.Checked = true;

        lblActionTitle.Text = "Create New Item";
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
                txtItemRemarks.Text = row["Remarks"].ToString();


                chkItemStatus.Checked = (bool)row["ItemStatus"];

            }
        }

        ViewState["ACTION"] = "MODIFY";

        //Text of Save Button
        lnkCreateUpdate.Text = "UPDATE";

        //Disable UOM
        ddUOM.Enabled = false;

        lblActionTitle.Text = "Modify Item - " + selCode;
        
        //Will use as reference to update item.
        ViewState["SEL_ITEMCODE"] = selCode;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#ItemContainer').modal('show');</script>", false);
      
    }

    protected void lnkCreateUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtItemDescription.Text) || string.IsNullOrEmpty(txtItemPrice.Text) || string.IsNullOrEmpty(txtStockLimit.Text))
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert", "<script>alert('Required Field must fill up');</script>", false);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#ItemContainer').modal('show');</script>", false);

        }
        else
        {
            //If Add link hit
            //Add New Item
            if (ViewState["ACTION"].ToString() == "ADD")
            {
                oItem.INSERT_ITEM_INVENTORY_DATA(oSeries.GENERATE_SERIES_NUMBER_MASTER("ITM"), txtItemDescription.Text.ToUpper(), ddUOM.SelectedValue.ToString(), Convert.ToDouble(txtItemPrice.Text),
                                                chkItemStatus.Checked, txtItemRemarks.Text, Convert.ToDouble(txtStockLimit.Text), "AdminTest", "ITM");

            }
            else if (ViewState["ACTION"].ToString() == "MODIFY")
            {
                oItem.UPDATE_ITEM_INVENTORY_DATA(ViewState["SEL_ITEMCODE"].ToString(), txtItemDescription.Text.ToUpper(), Convert.ToDouble(txtItemPrice.Text),
                                                   chkItemStatus.Checked, txtItemRemarks.Text, Convert.ToDouble(txtStockLimit.Text), "AdminTest");

            }

            //Refresh the current web page  
            Response.Redirect(Request.RawUrl);

        }
    }
}