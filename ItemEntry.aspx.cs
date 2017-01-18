using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRPEDRO;

public partial class ItemEntry : System.Web.UI.Page
{
    Main_C oXtra = new Main_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            displayUOMList();

        }
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
  
}