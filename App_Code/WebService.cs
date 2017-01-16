using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using SRPEDRO;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]

public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public string HelloWorld() {
    //    return "Hello World";
    //}

    [WebMethod]
    public string[] GET_Supplier_List(string _supplierName)
    {

        List<string> listSuppliers = new List<string>();

        string CS = ConfigurationManager.ConnectionStrings["CSPEDRO"].ToString();
        using (SqlConnection cn = new SqlConnection(CS))
        {
            SqlCommand cmd = new SqlCommand("Master.SP_Search_Supplier", cn);
            //SqlCommand cmd = new SqlCommand(storedproc, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIERNAME", _supplierName);

            cn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                //listApplicantNames.Add(rdr["Fullname"].ToString());
                listSuppliers.Add(string.Format("{0}*{1}", rdr["SupplierCode"].ToString(), rdr["SupplierName"].ToString()));
            }

        }

        return listSuppliers.ToArray();
    }


  
    
}
