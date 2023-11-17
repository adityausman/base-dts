
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BASE_RFID_TMS.Models
{
    public class Dashboard
    {
        
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
        

        public static DataTable getDataDashboardGeneralStock()
        {
            DataTable dtTempTable = new DataTable("ASSET");
            string StrQuery = "select * from dbo.viDashboardGeneralStock" + System.Environment.NewLine;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@asset_no", asset_no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getDataDashboardStockPerCategory()
        {
            DataTable dtTempTable = new DataTable("ASSET");
            string StrQuery = "select * from dbo.viDashboardStockPerCategory" + System.Environment.NewLine;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@asset_no", asset_no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

    }
}