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
    
    Main_C oMain = new Main_C();
    SeriesNumber_C oSeriesNumber = new SeriesNumber_C();
    Supplier_C oSupplier = new Supplier_C();
    Item_C oItem = new Item_C();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            displaySupplierList();
   
            displayItemList();



            //Create Temporary Table for Ordered item from Supplier
            createTempItemSupplier();



        }

    }

   

    protected void ddItemList_SelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dtItemList = oItem.GET_ITEM_LIST();

        DataRow[] dr;
        dr = dtItemList.Select("ItemCode = '" + ddItemList.SelectedValue.ToString() + "'");

        if (dr.Length > 0)
        {
            //Will display the selected item info
            foreach (DataRow row in dr)
            {
                lblUOM.Text = row["UomCode"].ToString();
                lblPrice.Text = row["ItemPrice"].ToString();
            }
        }
    }

    protected void U_Save_S_Click(object sender, EventArgs e)
    {

        //Saving Data on Supplier Transaction HDR

        oSupplier.INSERT_SUPPLIER_ORDER_TRANS_HDR(ddSupplierList.SelectedValue.ToString(), Convert.ToDateTime(txtDateTrans.Text), Convert.ToDateTime(txtDeliveryDate.Text), oSeriesNumber.GENERATE_SERIES_NUMBER_TRANS("ST"), txtRemarks.Text, oGlobal.G_USERCODE);


        if (gvSupplierItems.Rows.Count > 0)
        {
        
            //Saving Rows Transaction of Supplier
            foreach (GridViewRow row in gvSupplierItems.Rows)
            {
                string SeriesNum = oSeriesNumber.GENERATE_SERIES_NUMBER_TRANS("ST");
                oGlobal.G_SSNUM = SeriesNum;

                string sItemCode = row.Cells[1].Text;
                double dQty = double.Parse(row.Cells[3].Text);
                string sUOM = row.Cells[4].Text;
                double dPrice = double.Parse(row.Cells[5].Text);

                oSupplier.INSERT_SUPPLIER_ORDER_TRANS_ROWS(ddSupplierList.SelectedValue.ToString(), SeriesNum , sItemCode, dQty, dPrice, sUOM, oGlobal.G_USERCODE);
                
                //Update Stock Inventory
                //oSupplier.UPDATE_STOCK_INVENTORY(sItemCode, dQty);
            }



            //Update Series Number
            oSeriesNumber.UPDATE_SERIES_NUMBER("ST");


            //Direct to the print
            PRINT_NOW("SITransaction_Report.aspx");

            //Prompt a message.
            lblMessageSuccess.Text = "Transaction successfully recorded.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#msgSuccessModal').modal('show');</script>", false);
   
            //Clear Fields
            resetFields();

         
       
        }
    }

     

      protected void lnkAdd_Click(object sender, EventArgs e)
      {
         

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

              double itemPrice = Convert.ToDouble(lblPrice.Text);
              //Instantiate table 
              DataTable dt = (DataTable)Session["tempSupplierOrder"];

              //Add New Row
              DataRow newRow = dt.NewRow();
              
              newRow["CODE"] = ddItemList.SelectedValue.ToString();
              newRow["DESC"] = ddItemList.SelectedItem.Text;
              newRow["QTY"] = double.Parse(txtQuantity.Text);
              newRow["UOM"] = lblUOM.Text;
              newRow["PRICE"] = itemPrice;
              double sSubTotal = double.Parse(txtQuantity.Text) * itemPrice;
              newRow["TOTAL"] = string.Format("{0:N}", sSubTotal);
             
              dt.Rows.Add(newRow);

              Session["tempSupplierOrder"] = dt;

              gvSupplierItems.DataSource = Session["tempSupplierOrder"];
              gvSupplierItems.DataBind();

              ddItemList.SelectedIndex = 0;


              lblRunningTotal.Text =  string.Format("Total Cost: {0:N}", computeRunningTotal());

              //Clear Text
              lblUOM.Text = "";
              lblPrice.Text = "";
              txtQuantity.Text = "";
              
            }
          }
      }

      private void createTempItemSupplier()
      {
          DataTable dt = new DataTable();

          dt.Columns.Add("CODE", System.Type.GetType("System.String"));
          dt.Columns.Add("DESC", System.Type.GetType("System.String"));
          dt.Columns.Add("QTY", System.Type.GetType("System.Double"));
          dt.Columns.Add("UOM", System.Type.GetType("System.String"));
          dt.Columns.Add("PRICE", System.Type.GetType("System.Double"));
          dt.Columns.Add("TOTAL", System.Type.GetType("System.Double"));

          Session["tempSupplierOrder"] = dt;

      }

      private void displaySupplierList()
      {

          oSupplier.GET_SUPPLIER_LIST_DD(ddSupplierList);
          ddSupplierList.Items.Insert(0, new ListItem("-- SELECT SUPPLIER --"));
      }

     
     

      private void displayItemList()
      {
          oItem.GET_ITEM_LIST_DD(ddItemList);
          ddItemList.Items.Insert(0, new ListItem("-- SELECT ITEM --"));
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

      private double computeRunningTotal()
      {
          double dRunningTotal = 0;

          foreach (GridViewRow gvr in gvSupplierItems.Rows)
          {
              if (gvr.RowType == DataControlRowType.DataRow)
              {
                  dRunningTotal = dRunningTotal + Convert.ToDouble(gvr.Cells[6].Text);

              }
          }

          return dRunningTotal;
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
              if (drow["CODE"].ToString() == selCode)
                  drow.Delete();
          }

          dt.AcceptChanges();

          Session["tempSupplierOrder"] = dt;


          gvSupplierItems.DataSource = dt;
          gvSupplierItems.DataBind();

          lblRunningTotal.Text = string.Format("Total Cost: {0:N}", computeRunningTotal());
      }

      protected void ddSupplierList_SelectedIndexChanged(object sender, EventArgs e)
      {
        DataTable dtSupplierList = oSupplier.GET_SUPPLIER_LIST();

        DataRow[] dr;
        dr = dtSupplierList.Select("SupplierCode = '" + ddSupplierList.SelectedValue.ToString() + "'");

        if (dr.Length > 0)
        {
            //Will display the selected supplier info
            foreach (DataRow row in dr)
            {
                lblSupplierContact.Text = row["ContactPerson"].ToString();
                lblSupplierNumbers.Text = row["Telephone"].ToString() + " " + row["MobilePhone"].ToString();
                lblSupplierAddress.Text = row["Address"].ToString();
            }
        }

      }


    //RESET ALL Fields
      private void resetFields()
      {
          txtDateTrans.Text = "";
          txtDeliveryDate.Text = "";
          txtRemarks.Text = "";
          txtQuantity.Text = "";

          ddSupplierList.SelectedIndex = 0;
          ddItemList.SelectedIndex = 0;



          lblPrice.Text = "";
          lblUOM.Text = "";
          lblRunningTotal.Text = "";
          lblSupplierAddress.Text = "";
          lblSupplierContact.Text = "";
          lblSupplierNumbers.Text = "";
          //Clearing Gridview
          gvSupplierItems.DataSource = null;
          gvSupplierItems.DataBind();

          //Will Clear local data table
          DataTable dt = (DataTable)Session["tempSupplierOrder"];
          dt.Clear();
      }


      private void PRINT_NOW(string url)
      {
          string s = "window.open('" + url + "', 'popup_window', 'width=1024, height=768, left=0, top=0, resizable=yes');";
          ScriptManager.RegisterClientScriptBlock(this, this.Page.GetType(), "ReportScript", s, true);
      }

      protected void lnkPrintTransaction_Click(object sender, EventArgs e)
      {
          oGlobal.G_SSNUM = txtPrintTransaction.Text;
          PRINT_NOW("SITransaction_Report.aspx");

      }
}