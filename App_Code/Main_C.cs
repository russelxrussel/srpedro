using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

//To Get Web Tools
using System.Web.UI;
//using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SRPEDRO
{
    public class Main_C : cBase
    {

        public bool checkUser(string _usercode, string _password)
        {
            bool x = false;
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Master.User_Data where userCode=@USERCODE and password=@PASSWORD", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@USERCODE", _usercode);
                    cmd.Parameters.AddWithValue("@PASSWORD", _password);

                    cn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        x = true;
                    }
                    else { x = false; }
                }
            }

            return x;
        }

        public DateTime GetServerDate()
        {
            DateTime svrDT;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT GetDate()", cn))
                {


                    cn.Open();
                    svrDT = (DateTime)cmd.ExecuteScalar();
                }
            }

            return svrDT;

        }

        public DataTable queryCommandDT_StoredProc(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }


        /*USER MENU 
         * 01/24/2017
         * RUSSEL VASQUEZ
         */

        public DataSet GET_USER_MENU()
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[xSys].[SP_GET_USER_MENU]", cn))
                //using (SqlCommand cmd = new SqlCommand("Select * from xSystem.Menus", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@UserCode", _UserCode);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }



        //Getting Supplier List
        public DataTable GetSupplierList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select SupplierCode, SupplierName from Master.Supplier_Data order by SupplierName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }



        //Getting Branch List
        public DataTable GetBranchList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select BranchCode, BranchName from Master.Branch_Data order by BranchName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public void GetBranchList(DropDownList dd)
        {
            DataTable dt = GetBranchList();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["BranchCode"].ToString();
            dd.DataTextField = dt.Columns["BranchName"].ToString();
            dd.DataBind();

        }

        //Getting TransactionType
        public DataTable GetTransTypeList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select TransTypeCode, TransTypeName from Util.Transact_Type_RF order by TransTypeName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public void GetTransTypeList(DropDownList dd)
        {
            DataTable dt = GetTransTypeList();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["TransTypeCode"].ToString();
            dd.DataTextField = dt.Columns["TransTypeName"].ToString();
            dd.DataBind();

        }


        //Getting Item List
        public DataTable GetItemList()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select ItemCode, ItemName from Master.Item_Data order by ItemName ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }


        public DataTable GetItemUOM()
        {
            DataTable dt = new DataTable();
            string strSQL = "Select UomCode, UomName from Util.UOM_RF order by UOMID ASC";
            dt = queryCommandDT(strSQL);

            return dt;
        }

        public void GetItemUOM(DropDownList dd)
        {
            DataTable dt = GetItemUOM();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["UOMCode"].ToString();
            dd.DataTextField = dt.Columns["UOMName"].ToString();
            dd.DataBind();
        }


        //Get Item Price
        public double getItemPrice(string _itemCode)
        {
            double xPrice = 0;
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT ItemPrice FROM Master.Item_Data Where ItemCode=@ITEMCODE", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);

                    cn.Open();
                    xPrice = (double)cmd.ExecuteScalar();
                }
            }

            return xPrice;
        }






        public void GENERIC_DROPDOWN(DropDownList dd, DataTable dt, string colValue, string colText)
        {
            DataTable datatable = dt;

            dd.DataSource = datatable;
            dd.DataTextField = dt.Columns[colValue].ToString();
            dd.DataTextField = dt.Columns[colText].ToString();
            dd.DataBind();
        }


        //INSERT INTO Result Slip Table 02-16-2016
        public void INSERT_INVENTORY_TRANS(string _customerCode, string _transTypeCode, string _itemCode, string _uom, double _quantity, double _itemPrice, DateTime _dateTrans, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO TRANS.Inventory(CustomerCode, TransTypeCode, ItemCode, Quantity, ItemPrice, UOM, DateTrans, UserCode) " +
                                                       "VALUES(@CUSTOMERCODE, @TRANSTYPECODE, @ITEMCODE, @QUANTITY, @ITEMPRICE, @UOM, @DATETRANS, @USERCODE)", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@CUSTOMERCODE", _customerCode);
                    cmd.Parameters.AddWithValue("@TRANSTYPECODE", _transTypeCode);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);
                    cmd.Parameters.AddWithValue("@UOM", _uom);
                    cmd.Parameters.AddWithValue("@DATETRANS", _dateTrans);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void UPDATE_INVENTORY_DATA(string _itemCode, double _quantity, DateTime _dateUpdate, string _transTypeCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "";

                if (_transTypeCode == "SI")
                {
                    strSQL = "UPDATE Master.Inventory_Data SET InStock = InStock + @QUANTITY, RunningStock = RunningStock +  @QUANTITY, DateUpdate=@DATEUPDATE WHERE ItemCode=@ITEMCODE";
                }
                else if (_transTypeCode == "BI")
                {
                    strSQL = "UPDATE Master.Inventory_Data SET OutStock = OutStock + @QUANTITY, RunningStock = RunningStock -  @QUANTITY, DateUpdate=@DATEUPDATE WHERE ItemCode=@ITEMCODE";
                }

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                    cmd.Parameters.AddWithValue("@DATEUPDATE", _dateUpdate);



                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_INVENTORY_MD_BRANCH(string _itemCode, double _quantity, DateTime _dateUpdate)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("UPDATE XTRA.Inventory_MD " +
                                                       "SET OutStock = OutStock + @QUANTITY, RunningStock = RunningStock -  @QUANTITY, DateUpdate=@DATEUPDATE " +
                                                       "WHERE ItemCode=@ITEMCODE", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@QUANTITY", _quantity);
                    cmd.Parameters.AddWithValue("@DATEUPDATE", _dateUpdate);



                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


        public void INSERT_ITEM_MASTER(string _itemCode, string _itemName, string _uomCode, double _itemPrice)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Master.Item_Data(ItemCode, ItemName, UomCode, ItemPrice) " +
                                                       "VALUES(Upper(@ITEMCODE), Upper(@ITEMNAME), @UOMCODE, @ITEMPRICE)", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMNAME", _itemName);
                    cmd.Parameters.AddWithValue("@UOMCODE", _uomCode);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void INSERT_ITEM_INVENTORY(string _itemCode, double _begStock, double _stockLimit)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Master.Inventory_Data(ItemCode, BegStock, InStock, RunningStock, MinimumStockLevel) " +
                                                       "VALUES(Upper(@ITEMCODE), @BEGSTOCK, @BEGSTOCK,@BEGSTOCK,@STOCKLIMIT)", cn))
                {
                    // cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@BEGSTOCK", _begStock);
                    cmd.Parameters.AddWithValue("@STOCKLIMIT", _stockLimit);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public DataTable getBranchTransaction(string _branchcode)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SP_BranchInventoryTransaction", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchcode);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }
            }

            return dt;

        }

        public double getBranchTotalPrice(string _branchcode)
        {
            double x = 0;

            try
            {

                using (SqlConnection cn = new SqlConnection(CS))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT SUM(Quantity * ItemPrice) FROM Trans.Inventory WHERE CustomerCode=@BRANCHCODE", cn))
                    {

                        cmd.Parameters.AddWithValue("@BRANCHCODE", _branchcode);

                        cn.Open();

                        x = (double)cmd.ExecuteScalar();
                    }
                }


            }
            catch
            {

            }
            return x;

        }
    }




    public class Branch_C : cBase 
    {
        /* DATA SELECTION
         
            */


        public DataTable GET_BRANCH_LIST()
        {
            string strSQL = "Master.SP_GET_BRANCH_LIST";
            return queryCommandDT_StoredProc(strSQL);
        }

        //Method with Parameter  Supplier
        public void GET_BRANCH_LIST_DD(DropDownList dd)
        {
            DataTable dt = GET_BRANCH_LIST();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["BranchCode"].ToString();
            dd.DataTextField = dt.Columns["BranchName"].ToString();
            dd.DataBind();


        }


        //Branch Info Data Entry
        public void INSERT_BRANCH_INFO(string _branchCode, string _branchName, string _manager, string _areaCode,
                                    string _contactPerson, string _telephone, string _mobilephone,
                                    string _address, string _userCode, string _prefixCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("[Master].[SP_INSERT_BRANCH_INFO]", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@BRANCHNAME", _branchName);
                    cmd.Parameters.AddWithValue("@MANAGER", _manager);
                    cmd.Parameters.AddWithValue("@AREACODE", _areaCode);
                    cmd.Parameters.AddWithValue("@CONTACTPERSON", _contactPerson);
                    cmd.Parameters.AddWithValue("@TELEPHONE", _telephone);
                    cmd.Parameters.AddWithValue("@MOBILEPHONE", _mobilephone);
                    cmd.Parameters.AddWithValue("@ADDRESS", _address);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);
                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void INSERT_BRANCH_TRANS_HDR(string _branchCode, DateTime _documentDate, DateTime _releaseDate, string _bsnum, string _remarks, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Trans.SP_INSERT_BRANCH_TRANS_HDR", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@DOCUMENTDATE", _documentDate);
                    cmd.Parameters.AddWithValue("@RELEASEDATE", _releaseDate);
                    cmd.Parameters.AddWithValue("@BSNUM", _bsnum);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void INSERT_BRANCH_TRANS_ROWS(string _branchCode, string _bsnum, string _itemCode, double _itemQty, double _itemPrice, string _uom, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Trans.SP_INSERT_BRANCH_TRANS_ROWS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BRANCHCODE", _branchCode);
                    cmd.Parameters.AddWithValue("@BSNUM", _bsnum);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMQTY", _itemQty);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);
                    cmd.Parameters.AddWithValue("@UOM", _uom);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }

    public class Supplier_C : cBase
    {

        /* DATA SELECTION
         
         */


        public DataTable GET_SUPPLIER_LIST()
        {
            string strSQL = "Master.SP_GET_SUPPLIER_LIST";
            return queryCommandDT_StoredProc(strSQL);
        }

        //Method with Parameter  Supplier
        public void GET_SUPPLIER_LIST_DD(DropDownList dd)
        {
            DataTable dt = GET_SUPPLIER_LIST();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["SupplierCode"].ToString();
            dd.DataTextField = dt.Columns["SupplierName"].ToString();
            dd.DataBind();


        }


        /* DATA MANIPULATION AREA 
         
         */
        public void INSERT_SUPPLIER_ORDER_TRANS_HDR(string _supplierCode, DateTime _documentDate, DateTime _receiveDate, string _ssnum, string _remarks, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Trans.SP_INSERT_SUPPLIER_TRANS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SUPPLIERCODE", _supplierCode);
                    cmd.Parameters.AddWithValue("@DOCUMENTDATE", _documentDate);
                    cmd.Parameters.AddWithValue("@RECEIVEDATE", _receiveDate);
                    cmd.Parameters.AddWithValue("@SSNUM", _ssnum);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void INSERT_SUPPLIER_ORDER_TRANS_ROWS(string _supplierCode, string _ssnum, string _itemCode, double _itemQty, double _itemPrice, string _uom, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Trans.SP_INSERT_SUPPLIER_TRANS_ROWS", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SUPPLIERCODE", _supplierCode);
                    cmd.Parameters.AddWithValue("@SSNUM", _ssnum);
                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMQTY", _itemQty);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);
                    cmd.Parameters.AddWithValue("@UOM", _uom);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }

    public class Item_C : cBase
    {
        // DATA SELECTION

        //Get View
        public DataTable GET_ITEM_LIST()
        {
            string strSQL = "Master.V_SP_GET_ITEM_LIST";
            return queryCommandDT_StoredProc(strSQL);
        }

        //Method with Parameter  Supplier
        public void GET_ITEM_LIST_DD(DropDownList dd)
        {
            DataTable dt = GET_ITEM_LIST();

            dd.DataSource = dt;
            dd.DataValueField = dt.Columns["ItemCode"].ToString();
            dd.DataTextField = dt.Columns["ItemName"].ToString();
            dd.DataBind();

        }


        public void INSERT_ITEM_INVENTORY_DATA(string _itemCode, string _itemName, string _uomCode, double _itemPrice, bool _itemStatus, string _remarks, double _minimumStock, string _userCode, string _prefixCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Master.SP_INSERT_ITEM_INVENTORY", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMNAME", _itemName);
                    cmd.Parameters.AddWithValue("@UOMCODE", _uomCode);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);
                    cmd.Parameters.AddWithValue("@ITEMSTATUS", _itemStatus);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@MINIMUMSTOCKLEVEL", _minimumStock);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);
                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void UPDATE_ITEM_INVENTORY_DATA(string _itemCode, string _itemName, double _itemPrice, bool _itemStatus, string _remarks, double _minimumStock, string _userCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("Master.SP_UPDATE_ITEM_INVENTORY", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ITEMCODE", _itemCode);
                    cmd.Parameters.AddWithValue("@ITEMNAME", _itemName);
                    cmd.Parameters.AddWithValue("@ITEMPRICE", _itemPrice);
                    cmd.Parameters.AddWithValue("@ITEMSTATUS", _itemStatus);
                    cmd.Parameters.AddWithValue("@REMARKS", _remarks);
                    cmd.Parameters.AddWithValue("@MINIMUMSTOCKLEVEL", _minimumStock);
                    cmd.Parameters.AddWithValue("@USERCODE", _userCode);


                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }


    }

    public class Util_C : cBase
    {
        //Get View
        public DataTable GET_LOCATION_LIST()
        {
            string strSQL = "[Util].[SP_GET_LOCATION_LIST]";
            return queryCommandDT_StoredProc(strSQL);
        }


        public DataTable GET_BRANCH_AREA_LIST()
        {
            string strSQL = "[Util].[SP_GET_BRANCH_AREA_LIST]";
            return queryCommandDT_StoredProc(strSQL);
        
        }

      
    }
}
