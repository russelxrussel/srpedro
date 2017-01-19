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
    public class SeriesNumber_C : cBase
    {

        public int SERIESNUMBER { get; set; }

        //For Transaction Process
        public string GENERATE_SERIES_NUMBER_TRANS(string _prefixCode)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSys.SP_GET_SERIES_NUMBER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

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

                                /*Format Applicant AutoNumber
                                 * UP TO 99999 AutoNumbers
                                 */ 

                                if (SERIESNUMBER > 9999)
                                {
                                    AutoNumber = PrefixCode + "-" + SERIESNUMBER;
                                }

                                else if (SERIESNUMBER > 999)
                                {
                                    AutoNumber = PrefixCode + "-0" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "-" + "00" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 9)
                                {
                                    AutoNumber = PrefixCode + "-" + "000" + SERIESNUMBER;

                                }

                                else
                                {
                                    AutoNumber = PrefixCode + "-" + "0000" + SERIESNUMBER;

                                }

                            }

                            else
                            {
                                SERIESNUMBER = SERIESNUMBER + 1;
                                AutoNumber = PrefixCode + "-" + "0000" + SERIESNUMBER;


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


        //For Creation of Master Item
        public string GENERATE_SERIES_NUMBER_MASTER(string _prefixCode)
        {
            SERIESNUMBER = 0;
            string PrefixCode = "";
            string AutoNumber = "";

            //try
            //{
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSys.SP_GET_SERIES_NUMBER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

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

                                /*Format Applicant AutoNumber
                                 * UP TO 99999 AutoNumbers
                                 */

                              if (SERIESNUMBER > 999)
                                {
                                    AutoNumber = PrefixCode + "-" + SERIESNUMBER;

                                }

                                else if (SERIESNUMBER > 99)
                                {
                                    AutoNumber = PrefixCode + "-0"  + SERIESNUMBER;

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

        public void UPDATE_SERIES_NUMBER(string _prefixCode)
        {
            using (SqlConnection cn = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("xSys.SP_UPDATE_SERIES_NUMBER", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PREFIXCODE", _prefixCode);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                }
            }

        }
    }

}