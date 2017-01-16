using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SRPEDRO
{

    public class cBase
    {

        public class _StudentFields
        {
            public string xStudNum { get; set; }
            public string xStudFullName { get; set; }
            public string xStudLastName { get; set; }
            public string xStudFirstName { get; set; }
            public string xStudMiddleName { get; set; }

        }

        public class _ApplicantFields
        {

            public string xAppNum { get; set; }
            public string xAppFullName { get; set; }
            public string xAppLastName { get; set; }
            public string xAppFirstName { get; set; }
            public string xAppMiddleName { get; set; }
        }


        public static string CS = ConfigurationManager.ConnectionStrings["CSPEDRO"].ToString();


        public static DataSet queryCommandDS(string sqlQuery)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }

        public static DataSet queryCommandDS_StoredProc(string sqlQuery)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }

            return ds;
        }
        public static DataTable queryCommandDT(string sqlQuery)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, cn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;
        }
        public static DataTable queryCommandDT_StoredProc(string sqlQuery)
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

     

       
    }


} //End of NameSpace


