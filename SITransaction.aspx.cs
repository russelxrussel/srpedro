using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SRPEDRO;

public partial class Transaction : System.Web.UI.Page
{
    
    xtra oXtra = new xtra();
    seriesNumber oSeriesNumber = new seriesNumber();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            displayTransTypeList();

            displaySupplierList();
            displayBranchList();
            displayItemList();



            //For Branch
            createTempBranch();

        }

    }

    protected void ddBranchList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   

    protected void ddItemList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

      protected void U_Save_S_Click(object sender, EventArgs e)
    {
       
        if (gvSupplierItems.Rows.Count > 0)
        {
            DateTime dtDateTrans = Convert.ToDateTime(txtDateTrans.Text);

            foreach (GridViewRow row in gvSupplierItems.Rows)
            {
                string sCustomer = row.Cells[0].Text;
                string sTransTypeCode = row.Cells[1].Text;
                string sItemCode = row.Cells[2].Text;
                double dPrice = double.Parse(row.Cells[5].Text);
                double dQty = double.Parse(row.Cells[4].Text);
               

                oXtra.INSERT_INVENTORY_TRANS(sCustomer, sTransTypeCode, sItemCode, "", dQty, dPrice, dtDateTrans, "USER-TEST");
                oXtra.UPDATE_INVENTORY_DATA(sItemCode, dQty, dtDateTrans, sTransTypeCode);

                clearFields();
                //Clear Temporary table

                gvSupplierItems.DataSource = null;
                gvSupplierItems.DataBind();

                createTempBranch();

               
                txtDateTrans.Enabled = true;
             

            }
        }

    }

      protected void ddTransType_SelectedIndexChanged(object sender, EventArgs e)
      {
      
      }

      protected void lnkAdd_Click(object sender, EventArgs e)
      {
          double itemPrice = oXtra.getItemPrice(ddItemList.SelectedValue.ToString());

          if (ddItemList.SelectedIndex == 0 || string.IsNullOrEmpty(txtDateTrans.Text)|| ddSupplierList.SelectedIndex == 0 || string.IsNullOrEmpty(txtQuantity.Text))
          {
              //Prompt a message.
              lblErrorPrompt.Text = "Required fields should fill up first.";
              ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#msgErrorModal').modal('show');</script>", false);
     
          }

          else
          {
              //Check if item already on the list
              if(ExistItem(ddItemList.SelectedValue.ToString()))
              {

                  //Prompt a message.
                  lblErrorPrompt.Text = "Item already exist on current request list.";
                  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#msgErrorModal').modal('show');</script>", false);
     
              }
              else
              {
               
             
              //Instantiate table 
              DataTable dt = (DataTable)Session["tempSupplierOrder"];

              //Add New Row
              DataRow newRow = dt.NewRow();
              
              //newRow["SUPPLIERCODE"] = ddSupplierList.SelectedValue.ToString();
              newRow["ITEMCODE"] = ddItemList.SelectedValue.ToString();
              newRow["DESCRIPTION"] = ddItemList.SelectedItem.Text + "***" + oSeriesNumber.generateSeriesNumber("STR");
              newRow["QTY"] = double.Parse(txtQuantity.Text);

              newRow["PRICE"] = itemPrice;
              newRow["TOTAL"] = double.Parse(txtQuantity.Text) * itemPrice;
              //      newRow["DATETRANS"] = Convert.ToDateTime(txtDateTrans.Text);
              dt.Rows.Add(newRow);

              Session["tempSupplierOrder"] = dt;

              gvSupplierItems.DataSource = Session["tempSupplierOrder"];
              gvSupplierItems.DataBind();

              ddItemList.SelectedIndex = 0;
              txtQuantity.Text = "1";

              txtDateTrans.Enabled = false;

              
            }
          }
      }

      private void createTempBranch()
      {
          DataTable dt = new DataTable();
          //dt.Columns.Add("SUPPLIERCODE", System.Type.GetType("System.String"));
          dt.Columns.Add("ITEMCODE", System.Type.GetType("System.String"));
          dt.Columns.Add("DESCRIPTION", System.Type.GetType("System.String"));
          dt.Columns.Add("QTY", System.Type.GetType("System.Double"));
          dt.Columns.Add("PRICE", System.Type.GetType("System.Double"));
          dt.Columns.Add("TOTAL", System.Type.GetType("System.Double"));

          Session["tempSupplierOrder"] = dt;

      }

      private void displaySupplierList()
      {
          oXtra.GetSupplierList(ddSupplierList);
          ddSupplierList.Items.Insert(0, new ListItem("-- SELECT SUPPLIER --"));
      }

      private void displayBranchList()
      {

       
      }

      private void clearFields()
      {
          txtDateTrans.Text = "";
          ddSupplierList.SelectedIndex = 0;
          ddItemList.SelectedIndex = 0;
          txtQuantity.Text = "";
          lblPrice.Text = "";
      }

      private void displayItemList()
      {
          oXtra.GetItemList(ddItemList);
          ddItemList.Items.Insert(0, new ListItem("-- SELECT ITEM --"));
      }

      private void displayTransTypeList()
      {
      }


    //Check if item exist on gridview
    
      private bool ExistItem(string _itemCode)
      {
          bool bExist = false;

          foreach (GridViewRow gvr in gvSupplierItems.Rows)
          {
              if (gvr.RowType == DataControlRowType.DataRow)
              {
                  
                  string sCode = gvr.Cells[1].Text;
                  if (sCode == ddItemList.SelectedValue.ToString())
                  {
                      bExist = true;
                  }

              }
          }


          return bExist;
      }

    //Remove Button Action
      protected void lnkRemoveItem_Click(object sender, EventArgs e)
      {
          //THIS WILL GET THE CELL VALUE OF SELECTED ROW IN GRIDVIEW VIA COMMAND WITHIN TEMPLATE FIELDS.
          var selEdit = (Control)sender;
          GridViewRow r = (GridViewRow)selEdit.NamingContainer;
          // string selAppNum = r.Cells[2].Text;
          string selCode = r.Cells[1].Text;


          DataTable dt = (DataTable)Session["tempSupplierOrder"];

          for (int i = dt.Rows.Count - 1; i >= 0; i--)
          {
              DataRow drow = dt.Rows[i];
              if (drow["ITEMCODE"].ToString() == selCode)
                  drow.Delete();
          }

          dt.AcceptChanges();

          Session["tempSupplierOrder"] = dt;


          gvSupplierItems.DataSource = dt;
          gvSupplierItems.DataBind();
      }
}