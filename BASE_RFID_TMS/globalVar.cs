using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BASE_RFID_TMS
{
    public class globalVar
    {
        public static int getDB()
        {
            int data = 5;
            DataTable dtTempTable = new DataTable("aspnetusers");
            string StrQuery = "select cast(sys_value as int)  as data from SYS_SETTINGS where sys_key='timeout_auth'";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@sap_id", sap_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                        select (int)dr["data"]).FirstOrDefault();
            }
            else
            {
                data = 5;
            }
            return data;
        }
        public static int StrTimeoutSys = getDB();
        public static void SetTimeoutSys(int value)
        {
            StrTimeoutSys = value;

        }
        public static int GetTimeoutSys()
        {
            return StrTimeoutSys;

        }
    }
}