using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using BASE_RFID_TMS.Models;
using static BASE_RFID_TMS.Models.Master;
using System.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace BASE_RFID_TMS.Controllers
{
    public class AliasController : ApiController
    {
        // GET: Alias
        private string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        [HttpGet]
        [Route("api/Alias/getAliasUsername/{alias}")]
        public IHttpActionResult getAliasUsername(string alias)
        {
            string StrQuery = @"select email from AspNetUsers where upper(alias_username)=upper(@alias)";
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { alias }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }
    }
}