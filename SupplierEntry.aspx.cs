using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SRPEDRO;

public partial class SupplierEntry : System.Web.UI.Page
{
    Main_C oXtra = new Main_C();

    Item_C oItem = new Item_C();
    SeriesNumber_C oSeries = new SeriesNumber_C();
    Util_C oUtil = new Util_C();
    Branch_C oBranch = new Branch_C();
    Supplier_C oSupplier = new Supplier_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            displaySupplierList();

        }
    }

    private void displaySupplierList()
    {
        DataTable dt = new DataTable();
        dt = oSupplier.GET_SUPPLIER_LIST();

        gvSupplierList.DataSource = dt;
        gvSupplierList.DataBind();
    }

  

   


    private void clearItemDataFields()
    {
        //txtItemCode.Text = "";
        txtSupplierName.Text = "";
        txtTelephone.Text = "";
        txtMobile.Text = "";
        txtAddress.Text = "";

        txtContactPerson.Text = "";
       
    }

   

    protected void lnkCreate_Click(object sender, EventArgs e)
    {
        ViewState["ACTION"] = "ADD"; 

        //Text of Save Button
        lnkCreateUpdate.Text = "SAVE";

        clearItemDataFields();



        lblActionTitle.Text = "Create New Supplier";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#SupplierContainer').modal('show');</script>", false);
    }

    //EDIT Button Action
    protected void lnkEditItem_Click(object sender, EventArgs e)
    {
        //THIS WILL GET THE CELL VALUE OF SELECTED ROW IN GRIDVIEW VIA COMMAND WITHIN TEMPLATE FIELDS.
        var selEdit = (Control)sender;
        GridViewRow r = (GridViewRow)selEdit.NamingContainer;
        // string selAppNum = r.Cells[2].Text;
        string selCode = r.Cells[1].Text;

        DataTable dt = oSupplier.GET_SUPPLIER_LIST();

        DataRow[] dr;
        dr = dt.Select("SupplierCode = '" + selCode + "'");

        if (dr.Length > 0)
        {
            //Will display the selected item info
            foreach (DataRow row in dr)
            {
                txtSupplierName.Text = row["SupplierName"].ToString();               
                txtContactPerson.Text = row["ContactPerson"].ToString();
                txtTelephone.Text = row["Telephone"].ToString();
                txtMobile.Text = row["MobilePhone"].ToString();
                txtAddress.Text = row["Address"].ToString();   
            }
        }

        ViewState["ACTION"] = "MODIFY";

        //Text of Save Button
        lnkCreateUpdate.Text = "UPDATE";

        //Disable UOM
        //ddUOM.Enabled = false;

        lblActionTitle.Text = "Modify Supplier - " + selCode;
        
        //Will use as reference to update item.
        ViewState["SEL_SUPPLIERCODE"] = selCode;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#SupplierContainer').modal('show');</script>", false);
      
    }

    protected void lnkCreateUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSupplierName.Text) || string.IsNullOrEmpty(txtAddress.Text))
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
              

            // oBranch.INSERT_BRANCH_INFO(oSeries.GENERATE_SERIES_NUMBER_MASTER("BRM"), txtSupplierName.Text.ToUpper(), txtBranchManager.Text.ToUpper(), ddBranchArea.SelectedValue.ToString(), txtContactPerson.Text.ToUpper(),
            //                               txtTelephone.Text, txtMobile.Text, txtAddress.Text, oGlobal.G_USERCODE, "BRM");
                oSupplier.INSERT_SUPPLIER_INFO(oSeries.GENERATE_SERIES_NUMBER_MASTER("SUM"), txtSupplierName.Text.ToUpper(), txtContactPerson.Text.ToUpper(),
                                               txtTelephone.Text, txtMobile.Text, txtAddress.Text, oGlobal.G_USERCODE, "SUM");
            }
            else if (ViewState["ACTION"].ToString() == "MODIFY")
            {
               //oBranch.UPDATE_BRANCH_INFO(ViewState["SEL_SUPPLIERCODE"].ToString(), txtSupplierName.Text.ToUpper(), txtBranchManager.Text.ToUpper(), ddBranchArea.SelectedValue.ToString(), txtContactPerson.Text.ToUpper(),
               //                            txtTelephone.Text, txtMobile.Text, txtAddress.Text, oGlobal.G_USERCODE);
                oSupplier.UPDATE_SUPPLIER_INFO(ViewState["SEL_SUPPLIERCODE"].ToString(), txtSupplierName.Text.ToUpper(), txtContactPerson.Text.ToUpper(),
                                                txtTelephone.Text, txtMobile.Text, txtAddress.Text, oGlobal.G_USERCODE);
            }

            //Refresh the current web page  
            Response.Redirect(Request.RawUrl);

        }
    }
}