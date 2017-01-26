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
    Branch_C oBranch = new Branch_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            displayBranchList();
            displayBranchAreaList();
         

        }
    }

    private void displayBranchList()
    {
        DataTable dt = new DataTable();
        dt = oBranch.GET_BRANCH_LIST();

        gvBranchList.DataSource = dt;
        gvBranchList.DataBind();
    }

  

    private void displayBranchAreaList()
    {
        DataTable dt = new DataTable();
        dt = oUtil.GET_BRANCH_AREA_LIST();

        ddBranchArea.DataSource = dt;
        ddBranchArea.DataTextField = dt.Columns["AreaDescription"].ToString();
        ddBranchArea.DataValueField = dt.Columns["AreaCode"].ToString();
        ddBranchArea.DataBind();
    }


    private void clearItemDataFields()
    {
        //txtItemCode.Text = "";
        txtBranchName.Text = "";
       

        txtContactPerson.Text = "";
       
    }

   

    protected void lnkCreateBranch_Click(object sender, EventArgs e)
    {
        ViewState["ACTION"] = "ADD"; 

        //Text of Save Button
        lnkCreateUpdate.Text = "SAVE";




        lblActionTitle.Text = "Create New Branch";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#BranchContainer').modal('show');</script>", false);
    }

    //EDIT Button Action
    protected void lnkEditItem_Click(object sender, EventArgs e)
    {
        //THIS WILL GET THE CELL VALUE OF SELECTED ROW IN GRIDVIEW VIA COMMAND WITHIN TEMPLATE FIELDS.
        var selEdit = (Control)sender;
        GridViewRow r = (GridViewRow)selEdit.NamingContainer;
        // string selAppNum = r.Cells[2].Text;
        string selCode = r.Cells[1].Text;

        DataTable dt = oBranch.GET_BRANCH_LIST();

        DataRow[] dr;
        dr = dt.Select("BranchCode = '" + selCode + "'");

        if (dr.Length > 0)
        {
            //Will display the selected item info
            foreach (DataRow row in dr)
            {
                txtBranchName.Text = row["BranchName"].ToString();
                ddBranchArea.SelectedValue = row["AreaCode"].ToString();
                txtBranchManager.Text = row["BranchManager"].ToString();
                txtContactPerson.Text = row["BranchContactPerson"].ToString();
                txtTelephone.Text = row["Telephone"].ToString();
                txtMobile.Text = row["MobilePhone"].ToString();
                txtBranchAddress.Text = row["Address"].ToString();
                
            }
        }

        ViewState["ACTION"] = "MODIFY";

        //Text of Save Button
        lnkCreateUpdate.Text = "UPDATE";

        //Disable UOM
        //ddUOM.Enabled = false;

        lblActionTitle.Text = "Modify Branch - " + selCode;
        
        //Will use as reference to update item.
        ViewState["SEL_BRANCHCODE"] = selCode;

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#BranchContainer').modal('show');</script>", false);
      
    }

    protected void lnkCreateUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtBranchName.Text) || string.IsNullOrEmpty(txtBranchAddress.Text))
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
              

                oBranch.INSERT_BRANCH_INFO(oSeries.GENERATE_SERIES_NUMBER_MASTER("BRM"), txtBranchName.Text.ToUpper(), txtBranchManager.Text.ToUpper(), ddBranchArea.SelectedValue.ToString(), txtContactPerson.Text.ToUpper(),
                                           txtTelephone.Text, txtMobile.Text, txtBranchAddress.Text, oGlobal.G_USERCODE, "BRM");
            }
            else if (ViewState["ACTION"].ToString() == "MODIFY")
            {
                oBranch.UPDATE_BRANCH_INFO(ViewState["SEL_BRANCHCODE"].ToString(), txtBranchName.Text.ToUpper(), txtBranchManager.Text.ToUpper(), ddBranchArea.SelectedValue.ToString(), txtContactPerson.Text.ToUpper(),
                                           txtTelephone.Text, txtMobile.Text, txtBranchAddress.Text, oGlobal.G_USERCODE);
            }

            //Refresh the current web page  
            Response.Redirect(Request.RawUrl);

        }
    }
}