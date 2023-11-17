using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Security.Principal;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace BASE_RFID_TMS.Models
{
    public static class IdentityExtensions
    {
        public static string GetIdUser(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetJobPosition(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("JobPosition");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetPhone(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Phone");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetCustId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("cust_id");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetCustKeyId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("cust_key_id");
            // Test for null to avoid issues during local testing
            return (claim != null) ? AesOperation.EncryptString(ConfigurationManager.AppSettings["encrypt"], HttpContext.Current.User.Identity.GetCustId()) : string.Empty;
        }

        public static string GetCustKey(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("client_key");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetCustApi(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("api_key");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetpSalt(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PasswordSalt");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }


        //public static string GetDataCustId()
        //{
        //    //string Id = ((ClaimsIdentity)identity).FindFirst("UserId");
        //    string custId = null;
        //    DataTable dtJoint = new DataTable("CUSTOMER_SETTING");
        //    string StrQuery = "select z.cust_id from CUSTOMER_SETTING c inner join ZUSER_CUSTOMER z on c.cust_id = z.cust_id where z.Id = '" + HttpContext.Current.User.Identity.GetUserId() + "'";

        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = StrQuery;
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    con.Open();
        //    da.Fill(dtJoint);
        //    con.Close();

        //    foreach (DataRow dr in dtJoint.Rows)
        //    {
        //        custId = dr[0].ToString().Trim();
        //    }
        //    return custId;
        //}

    }
}
