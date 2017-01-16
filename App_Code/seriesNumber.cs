using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace SRPEDRO
{ 
/// <summary>
/// Summary description for seriesNumber
/// </summary>
    public class seriesNumber: cBase
    {

        public int SERIESNUMBER { get; set; }


        public string generateSeriesNumber(string PREFIX)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                string strSQL = "Select PrefixCode, Series from xSys.SeriesNumber where PrefixCode = '" + PREFIX + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        while (dr.Read())
                        {
                            PrefixCode = dr["PrefixCode"].ToString();

                            if ((int)dr["Series"] > 0)
                            {

                                SERIESNUMBER = (int)dr["Series"] + 1;

                                //format Applicant AutoNumber
                                if (SERIESNUMBER > 999)
                                {
                                    AutoNumber = PrefixCode + "-" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "-" + "0" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    AutoNumber = PrefixCode + "-" + "00" + SERIESNUMBER;

                                }

                                else
                                {
                                    AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;

                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;


                            }
                        }

                    }


                }
            }

            //}

            //catch { 

            //}


            return AutoNumber;

        }

        public void updateSeriesNumber(string PREFIX)
        {
            //Will Add +1 to series to update series number
            //int Series = displayLastSeries(PREFIX) + 1;

            using (SqlConnection cn = new SqlConnection(CS))
            {

                using (SqlCommand cmd = new SqlCommand("Update xSys.SeriesNumberSET series=" + SERIESNUMBER + " WHERE PrefixCode=@prefix", cn))
                {
                    cmd.Parameters.AddWithValue("@prefix", PREFIX);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }

    }

}