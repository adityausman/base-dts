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
    //public class TokenAuthorizationAttribute : AuthorizationFilterAttribute
    //{
    //    private const string ExpectedToken = "7B2xa14uA4kWAEE7Rsi60YM3zLjOzGLtegLvYxPGCJlobDkojzEbhwy7Su9CBR"; // Ganti dengan token statis Anda

    //    public override void OnAuthorization(HttpActionContext actionContext)
    //    {
    //        // Ambil token dari header 'Authorization'
    //        var token = $"Bearer {actionContext.Request.Headers.Authorization?.Parameter}";

    //        if (string.IsNullOrEmpty(token) || token != $"Bearer {ExpectedToken}")
    //        {
    //            // Jika token tidak valid atau tidak ada, kembalikan respon Unauthorized
    //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
    //                    return;
    //        }

    //        base.OnAuthorization(actionContext);
    //    }
    //}

    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    //[TokenAuthorizationAttribute]
    public class SampleApiIntegrationController : ApiController
    {
        // GET: SampleApiIntegration
        [HttpPut]
        [Route("api/Sample/updateFds/{fleet_id}")]
        public IHttpActionResult sampleUpdateFds(MasterController.FleetDataUpdateFDS prm, string fleet_id)
        {
            Boolean status = true;
            string message = "Update Succesfully";
            if (Request.Method != HttpMethod.Put)
            {
                status = false;
                message = "Failed Request";
            }

            return Json((new { status, message }));
        }
        [HttpPost]
        [Route("api/Sample/updateTms")]
        public IHttpActionResult sampleUpdateTms(MasterController.FleetData prm)
        {
            var code = 1;
            string message = "Update Succesfully";
            if (Request.Method != HttpMethod.Post)
            {
                code = 0;
                message = "Update Failed";
            }

            return Json((new { code, message }));
        }
    }
}