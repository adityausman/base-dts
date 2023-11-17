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
using System.Web.Mvc.Filters;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace BASE_RFID_TMS.Controllers
{

    public class GetTimeOutSystem : AuthorizationFilterAttribute
    {
        //private const string ExpectedToken = "7B2xa14uA4kWAEE7Rsi60YM3zLjOzGLtegLvYxPGCJlobDkojzEbhwy7Su9CBR"; // Ganti dengan token statis Anda

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Ambil token dari header 'Authorization'
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
            globalVar.SetTimeoutSys(data);
            var newExpireTimeSpan = TimeSpan.FromMinutes(data);
            Startup.AccessTokenExpireTimeSpan_ = newExpireTimeSpan;
            base.OnAuthorization(actionContext);
        }
    }
   
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [GetTimeOutSystem]
    [Authorize]
    public class MasterController : ApiController
    {
        
        private string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        [HttpGet]
        [Route("api/Master/RptStockLocationUtility/{year}")]
        public IHttpActionResult RptStockLocationUtility(string year)
        {
            string StrQuery = @"select loc.location_id, loc.location, 
                                (select count(tire_id) from TIRE_STOCK_IN where location_id=loc.location_id and year(date_created)<=@year ) as inn , 
                                (select count(tire_id) from TIRE where location_id=loc.location_id and is_delete=0 ) as stock,
								
                                (select count(tire_id) from TIRE_STOCK_OUT where location_id=loc.location_id and year(date_created)<=@year ) as out, 
                                 (
								 (select count(tire_id) from TIRE where location_id=loc.location_id and year(date_created)<=@year and tire_id not in (
								select t.tire_id from 
								LOCATION loc 
                                left join tire t   on t.location_id = loc.location_id
								inner join TIRE_STOCK_IN tsi on tsi.tire_id = t.tire_id)) + (select count(tire_id) from TIRE_STOCK_IN where location_id=loc.location_id and year(date_created)<=@year) - (select count(tire_id) from TIRE_STOCK_OUT where location_id=loc.location_id and year(date_created)<=@year ) )  as total, 
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id = '1'  and is_delete=0 and year(date_created)<=@year ) as ready,
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id = '2'  and is_delete=0 and year(date_created)<=@year ) as repair,
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id in('4')  and is_delete=0 and year(date_created)<=@year ) as scrap,
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id in('9')  and is_delete=0 and year(date_created)<=@year ) as patching,
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id in('3')  and is_delete=0 and year(date_created)<=@year ) as send_to_workshop,
                                (select count(tire_id) from TIRE where location_id=loc.location_id and status_id not in('1','2','4','9','3')  and is_delete=0 and year(date_created)<=@year ) as other
                                from LOCATION loc 
                                left join tire t   on t.location_id = loc.location_id
                                where 
								loc.location_id not in ('XXX', 'A00','999','888', 'A01', 'DA1', 'DA2', 'DA3','OV00', 'ZZZ')
								and loc.is_delete=0
                                group by loc.location_id, loc.location, loc.is_delete
";
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year = year }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet] 
        [Route("api/Master/validationKMFleet/{tire_id}")]
        public IHttpActionResult validationKMFleet(string tire_id)
        {
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string now =  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int Indication_diff = 0;
            int diff = 0;
            try
            {
                DataTable dtTempTable = new DataTable("tire_mileage_history");
                string StrQuery = @"select top 1 *, DATEDIFF(hour, date_created, @now) as diff from tire_mileage_history where tire_id=@tire_id and is_delete=0 
                                     order by date_created desc" + System.Environment.NewLine;
                //YEAR(date_created) = @year and MONTH(date_created)=@month and DAY(date_created)=@day
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = StrQuery;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@tire_id", tire_id);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@day", day);
                cmd.Parameters.AddWithValue("@now", now);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                con.Open();
                da.Fill(dtTempTable);
                con.Close();
                if (dtTempTable.Rows.Count > 0)
                {
                    diff = (from DataRow dr in dtTempTable.Rows
                                    select (int)dr["diff"]).FirstOrDefault();
                    Indication_diff = 1;
                }

                //SqlConnection dbConn = new SqlConnection(con);
                //dbConn.Open();
                //var data = dbConn.Query(StrQuery, new { tire_id = tire_id, year = year, month= month, day= day, now = now }, null, true, commandTimeout: 0, commandType: null).ToList();
                //dbConn.Close();
                if (Indication_diff == 0)
                {
                    return Json((new { diff = diff, message = "Tire not found.", data = dtTempTable }));
                }
                else
                {
                    if (diff <= 1)
                    {
                        return Json((new { diff = diff, message = "You have read the Kilometers approx 1 hour ago.", data = dtTempTable }));
                    }
                    else
                    {
                        return Json((new { diff = diff, message = "Please, read the fleet Kilometers first!", data = dtTempTable }));
                    }
                }
                
                
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireBySerialNumberForInbound/{sn}/{location_id}")]
        public IHttpActionResult getTireBySerialNumberForInbound(string sn, string location_id)
        {

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(Master.qDataTireBySerialNumberForInbound(location_id), new { sn = sn, location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data tire by serial number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireBySerialNumber/{sn}")]
        public IHttpActionResult getTireBySerialNumber(string sn)
        {

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(Master.qDataTireBySerialNumber(), new { sn = sn }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getDataLastValueTire/{sn}")]
        public IHttpActionResult getDataLastValueTire(string sn)
        {

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                string strQuery = @"IF (NOT EXISTS (select t.tire_id, max(smo.date_created) as smo_date, max(smo.smo_id) as smo_id, ISNULL((select top 1 value from SERV_MAINT_OBSERVATION_DETAIL where observation_item_id=1 and smo_id=max(smo.smo_id)),0) as air_pressure, ISNULL((select top 1 value from SERV_MAINT_OBSERVATION_DETAIL where observation_item_id=2 and smo_id=max(smo.smo_id)),0) as tread_depth, t.km from tire t
									inner join SERV_MAINT_OBSERVATION smo on smo.tire_id=t.tire_id
									where t.tire_id not in ('XXX') and t.tire_id=@sn
									group by t.tire_id, t.km) ) 
									BEGIN
										select @sn as tire_id, '' as smo_date, '' as smo_id, '0' as air_pressure, '0' as tread_depth, '0' as km
									END
									ELSE
									BEGIN
										select t.tire_id, max(smo.date_created) as smo_date, max(smo.smo_id) as smo_id, ISNULL((select top 1 value from SERV_MAINT_OBSERVATION_DETAIL where observation_item_id=1 and smo_id=max(smo.smo_id)),0) as air_pressure, ISNULL((select top 1 value from SERV_MAINT_OBSERVATION_DETAIL where observation_item_id=2 and smo_id=max(smo.smo_id)),0) as tread_depth, t.km from tire t
										inner join SERV_MAINT_OBSERVATION smo on smo.tire_id=t.tire_id
										where t.tire_id not in ('XXX') and t.tire_id=@sn
										group by t.tire_id, t.km
									END";
                var data = dbConn.Query(strQuery, new { sn }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data tire by serial number (Combo)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireBySerialNumberCustom/{sn}")]
        public IHttpActionResult getTireBySerialNumberCustom(string sn)
        {

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(Master.qDataTireBySerialNumberCustom(sn), null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireBySerialNumberForTracking/{sn}")]
        public IHttpActionResult getTireBySerialNumberForTracking(string sn)
        {

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(Master.qDataTireBySerialNumberForLookupTracking(sn), null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data master tire custom
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireInbound/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireInbound(string date_start, string date_end, string userid)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
           
            string StrQuery = @"select INB.inbound_id,INB.location_id,LOC.location, INB.date_created from INBOUND INB
                                INNER JOIN LOCATION LOC on LOC.location_id = INB.location_id
                                where inb.is_delete=0 " + System.Environment.NewLine;
           
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(INB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and INB.userid=@userid" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tire_name" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireInboundReport/{date_start}/{date_end}/{userid}/{location_id}")]
        public IHttpActionResult getTireInboundReport(string date_start, string date_end, string userid, string location_id)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
            string roleUser = ReportController.getRoleUser(userid);
            string StrQuery = @"select INB.inbound_id,INB.location_id,LOC.location, INB.date_created, INB.userid, AU.Name from INBOUND INB
                                INNER JOIN LOCATION LOC on LOC.location_id = INB.location_id
                                inner join AspNetUsers AU on AU.UserId = INB.userid
                                where inb.is_delete=0 " + System.Environment.NewLine;
            if (location_id != "0")
            {
                StrQuery += "and inb.location_id=@location_id" + System.Environment.NewLine;
            }
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(INB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and INB.userid=@userid" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tire_name" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { location_id, date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireInboundDetail/{inbound_id}")]
        public IHttpActionResult getTireInboundDetail(string inbound_id)
        {
           
            string StrQuery = @"select INB.inbound_id,INB_D.tire_id,t.tire_name,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code from INBOUND INB
                                INNER JOIN INBOUND_DETAIL INB_D on INB_D.inbound_id=INB.inbound_id and INB.date_created=INB_D.date_created
                                inner join TIRE T on t.tire_id = INB_D.tire_id
								inner join ITEM i on i.tire_code=t.tire_code
                                where inb.inbound_id=@inbound_id and inb.is_delete=0 and inb_d.is_delete=0" + System.Environment.NewLine;
          
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { inbound_id = inbound_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/checkEPC/{epc}")]
        public IHttpActionResult checkEPC(string epc)
        {

            string StrQuery = @"select * from EPC where epc_rfid=@epc" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                Boolean res;
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { epc }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                if (data.Count == 0)
                {
                    res = false;
                }
                else {
                    res = true;
                }
                return Json((new { records = data.Count, res = res }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        //[HttpPost]
        //[Route("api/Master/UploadFileImg")]
        //public HttpResponseMessage UploadFileImg(string id, int type)
        //{
        //    string folder = "";
        //    if (type == 0)
        //    {
        //        folder = "TyreLoose";
        //    }
        //    else
        //    {
        //        folder = "TyreFound";
        //    }
        //    var uploadPath = ConfigurationManager.AppSettings["url_frontend"] + folder;// HttpContext.Current.Server.MapPath("D:/wwwroot/APP/RFID_TMS/TYRE_PTSI/img/" + folder);

        //    HttpResponseMessage result = null;
        //    var httpRequest = HttpContext.Current.Request;
        //    if (httpRequest.Files.Count > 0)
        //    {
        //        var docfiles = new List<string>();
        //        foreach (string file in httpRequest.Files)
        //        {
        //            var postedFile = httpRequest.Files[file];
        //            var filePath = uploadPath + id + Path.GetExtension(postedFile.FileName); //HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
        //            postedFile.SaveAs(filePath);
        //            docfiles.Add(filePath);
        //        }

        //        result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
        //    }
        //    else
        //    {
        //        result = Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }
        //    return result;
        //}
        [HttpGet]
        [Route("api/Master/getTireOutbound/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireOutbound(string date_start, string date_end, string userid)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
            string StrQuery = @"select OTB.outbound_id,OTB.location_id,LOC.location, OTB.date_created from OUTBOUND OTB
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB.location_id
                                where OTB.is_delete=0 " + System.Environment.NewLine;
           
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(OTB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tire_name" + System.Environment.NewLine;
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and OTB.userid=@userid" + System.Environment.NewLine;
            }
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new {  date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireOutboundReport/{date_start}/{date_end}/{userid}/{location_id}")]
        public IHttpActionResult getTireOutboundReport(string date_start, string date_end, string userid, string location_id)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
            string roleUser = ReportController.getRoleUser(userid);
            string StrQuery = @"select OTB.outbound_id,OTB.location_id,LOC.location, OTB.date_created, OTB.userid, AU.Name from OUTBOUND OTB
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB.location_id
                                inner join AspNetUsers AU on AU.UserId = OTB.userid
                                where OTB.is_delete=0 " + System.Environment.NewLine;
            if (location_id != "0")
            {
                StrQuery += "and otb.location_id=@location_id" + System.Environment.NewLine;
            }
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(OTB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tire_name" + System.Environment.NewLine;
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and OTB.userid=@userid" + System.Environment.NewLine;
            }
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { location_id, date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireOutboundStore/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireOutboundStore(string date_start, string date_end, string userid)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
            string StrQuery = @"select OTB.outbound_id,OTB.location_id,LOC.location, OTB.date_created from OUTBOUND OTB
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB.location_id
                                where OTB.is_delete=0 and loc.location_id='A00S002'
                            " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(OTB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tir e_name" + System.Environment.NewLine;
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and OTB.userid=@userid" + System.Environment.NewLine;
            }
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end, userid}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getReason")]
        public IHttpActionResult getReason()
        {

            string StrQuery = @"select reason_id, reason_group_id, reason_group, reason_name from reason where is_delete=0 and reason_id not in('0')" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getAreaDamage")]
        public IHttpActionResult getAreaDamage()
        {

            string StrQuery = @"select area_damage_id, area_damage_name from area_damage where is_delete=0 and area_damage_id not in('0')" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTireOutboundDetail/{outbound_id}")]
        public IHttpActionResult getTireOutboundDetail(string outbound_id)
        {

            string StrQuery = @"select OTB.outbound_id,OTB_D.tire_id,t.tire_name,OTB_D.location_id, LOC.location, isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code from OUTBOUND OTB
                                INNER JOIN OUTBOUND_DETAIL OTB_D on OTB_D.outbound_id=OTB.outbound_id and OTB.date_created=OTB_D.date_created
                                inner join TIRE T on t.tire_id = OTB_D.tire_id
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB_D.location_id
                                inner join ITEM i on i.tire_code=t.tire_code
                                where OTB.outbound_id=@outbound_id and OTB.is_delete=0 and OTB_d.is_delete=0" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { outbound_id = outbound_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/cekDataTireInstalled/{tire_id}")]
        public IHttpActionResult cekDataTireInstalled(string tire_id)
        {
            string row = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = "select * from TIRE_UNIT where tire_id=@tire_id and is_delete=0 ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tire_id", tire_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            //return dtTempTable.Rows.Count;
            if (dtTempTable.Rows.Count > 0)
            {
                return Json((new { records = dtTempTable.Rows.Count, message = "Tire already installed!" }));
            }
            else
            {
                return Json((new { records = dtTempTable.Rows.Count, message = "" }));
            }
        }
        [HttpGet]
        [Route("api/Master/getTireOutboundForInbound/{tire_id}/{location_id}")]
        public IHttpActionResult getTireOutboundForInbound(string tire_id, string location_id)
        {
            string StrQuery = "";


            if (location_id == "A00S001") {
                StrQuery = @"
                                select * from(
                                select od.tire_id as value, label=od.tire_id+' - '+ case when t.tire_name ='' then t.tire_code else t.tire_name end, od.tire_id, case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name
                                from OUTBOUND_DETAIL OD
                                inner join TIRE T on t.tire_id = od.tire_id
                                where od.is_active=1 and od.tire_id like @tire_id and od.location_id=@location_id
                                 group by od.tire_id,t.tire_name, t.tire_code
                                UNION ALL
								select t.tire_id as value, label=t.tire_id+ ' - '+ t.tire_code, t.tire_id, case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name from TIRE t
                                    inner join LOCATION l on l.location_id = t.location_id
							         where t.tire_id not in (select tire_id from REGISTER_TIRE) 
							         and t.tire_id not in (select inbd.tire_id from INBOUND inb 
						        inner join INBOUND_DETAIL inbd on inb.inbound_id = inbd.inbound_id and inb.date_created = inbd.date_created
						        where location_id='A00S001' ) and l.parent not in ('A00') and t.tire_id like @tire_id) as x" + System.Environment.NewLine;
            }
            else
            {
                StrQuery = @"
                                select od.tire_id as value, label=od.tire_id+' - '+ case when t.tire_name ='' then t.tire_code else t.tire_name end, od.tire_id, case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name
                                from OUTBOUND_DETAIL OD
                                inner join TIRE T on t.tire_id = od.tire_id
                                where od.is_active=1 and od.tire_id like @tire_id and od.location_id=@location_id";
                StrQuery += @"   group by od.tire_id,t.tire_name, t.tire_code " + System.Environment.NewLine;
            }
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tire_id = tire_id + "%", location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireCustom/{tire_code}")]
        public IHttpActionResult getTireCustom(string tire_code)
        {
            string rep_tire_code = tire_code.Replace("-", ".");
            string StrQuery = @"select top 100 id=i.tire_code, label=i.tire_code+' - '+isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end), value=i.tire_code,i.unique_code,
                                i.size,i.pattern,i.brand_tire_id,b.brand_tire_name,i.supplier_id,i.price from ITEM i
                                inner join BRAND_TIRE b on b.brand_tire_id = i.brand_tire_id" + System.Environment.NewLine;
            StrQuery += "where (i.tire_code like @rep_tire_code or i.tire_name like @rep_tire_code) and i.is_delete=0" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { rep_tire_code = "%" + rep_tire_code + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get data vehicle by vehicle_id/vehicle no
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getVehicleById/{vehicle_id}")]
        public IHttpActionResult getVehicleById(string vehicle_id)
        {
            string prm = "";
            
            string StrQuery = @"
                        select isnull(fv.fleet_id,'') as fleet_id_a, v.vehicle_id, v.vehicle_name, isnull(v.vehicle_rfid,'') as vehicle_rfid, ae.axis_type, ae.axis_type_name 
                       , bv.brand_vhc_name, case when fv.is_delete=1 then '' else isnull(fv.fleet_id,'') end as fleet_id, COUNT(ax.axis_id) number_of_tire, isnull(fv.is_delete,0) as is_delete      
                       from vehicle v
                        inner JOIN BRAND_VEHICLE BV on BV.brand_vhc_id = V.brand_vhc_id
                        left join FLEET_VEHICLE fv on fv.vehicle_id=v.vehicle_id  
                        left JOIN TIRE_UNIT TU on TU.vehicle_id = V.vehicle_id  and tu.is_delete=0
                        left join AXIS_TYPE AE ON v.axis_type=AE.axis_type
		                left join AXIS ax on ax.axis_type=ae.axis_type
                        where v.vehicle_id <> '0' and v.is_delete=0
                        " + System.Environment.NewLine;//v.vehicle_id not in (select vehicle_id from FLEET_VEHICLE where is_delete=0)
            if (vehicle_id != "0" && vehicle_id != "-" && vehicle_id != "")
            {
                StrQuery += "	and (V.vehicle_id=@vehicle_id or V.vehicle_rfid=@vehicle_id)" + System.Environment.NewLine;
            }
            StrQuery += @" group by v.vehicle_id, v.vehicle_name, v.vehicle_rfid, ae.axis_type, ae.axis_type_name, bv.brand_vhc_name, fv.fleet_id, fv.is_delete" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                List<cGetVehicleById> listVehicle = new List<cGetVehicleById>();
                var data = dbConn.Query(StrQuery, new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                List<cGetVehicleById> Lvehicle = new List<cGetVehicleById>();
                for (int sort = 0; sort < data.Count(); sort++)
                {
                    cGetVehicleById cV = new cGetVehicleById();
                    if (data[sort].fleet_id_a.ToString().Trim() != data[sort].vehicle_id.ToString().Trim() && (data[sort].is_delete.ToString() == "1" || data[sort].is_delete.ToString() == "True" || data[sort].is_delete.ToString() == "true"))
                    { }
                    else {
                        cV.fleet_id_a = data[sort].fleet_id_a.ToString().Trim();
                        cV.fleet_id = data[sort].fleet_id.ToString().Trim();
                        cV.vehicle_id = data[sort].vehicle_id.ToString().Trim();
                        cV.vehicle_name = data[sort].vehicle_name.ToString().Trim();
                        cV.vehicle_rfid = data[sort].vehicle_rfid.ToString().Trim();
                        cV.axis_type = data[sort].axis_type.ToString().Trim();
                        cV.axis_type_name = data[sort].axis_type_name.ToString().Trim();
                        cV.brand_vhc_name = data[sort].brand_vhc_name.ToString().Trim();
                        cV.number_of_tire = data[sort].number_of_tire.ToString().Trim();
                        Lvehicle.Add(cV);
                    }
                    
                }
                dbConn.Close();
                return Json((new { records = data.Count, data = Lvehicle }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        public static string getFleetIdByTire(string tire_id)
        {
            string fleet_id = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = @"select fv.fleet_id 
                                from TIRE_UNIT tu
                                inner
                                join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id where tire_id=@tire_id  and tu.is_delete=0 and fv.is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tire_id", tire_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                fleet_id = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["fleet_id"]).FirstOrDefault();
            }
            return fleet_id;
        }
        [HttpGet]
        [Route("api/Master/getVehicleDetailByTire/{tire_id}")]
        public IHttpActionResult getVehicleDetailByTire(string tire_id)
        {
            string prm = "";
            string fleet_id = getFleetIdByTire(tire_id);
            string StrQuery = @"select G.register_vhc_no,convert(varchar,G.date_created,106) as register_date,V.vehicle_rfid as rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position as sort, 
						count(a.axis_id) as default_number_of_tire
						from
	                    FLEET_VEHICLE FV
	                    INNER JOIN VEHICLE V on FV.vehicle_id = v.vehicle_id
	                    inner join fleet f on f.fleet_id=fv.fleet_id
	                    inner join AXIS_TYPE AE ON v.axis_type=AE.axis_type
						inner join axis a on a.axis_type = ae.axis_type
	                    LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id and g.vehicle_id<>'0'" + System.Environment.NewLine;
            StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            if (tire_id != "0" && tire_id != "-" && tire_id != "")
            {
                StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            }
            StrQuery += @"
                        group by G.register_vhc_no,G.date_created,V.vehicle_rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position 
                        order by fv.fleet_id, fv.position" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/cekNewFleetBeforChangeVehicle/{fleet_id}")]
        public IHttpActionResult cekNewFleetBeforChangeVehicle(string fleet_id)
        {
            string prm = "";
            string StrQuery = @"select G.register_vhc_no,convert(varchar,G.date_created,106) as register_date,V.vehicle_rfid as rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position as sort, 
						count(a.axis_id) as default_number_of_tire
						from
	                    FLEET_VEHICLE FV
	                    INNER JOIN VEHICLE V on FV.vehicle_id = v.vehicle_id
	                    inner join fleet f on f.fleet_id=fv.fleet_id
	                    inner join AXIS_TYPE AE ON v.axis_type=AE.axis_type
						inner join axis a on a.axis_type = ae.axis_type
	                    LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id and g.vehicle_id<>'0'" + System.Environment.NewLine;
            StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;

            if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            {
                StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            }
            StrQuery += @"
                        group by G.register_vhc_no,G.date_created,V.vehicle_rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position 
                        order by fv.fleet_id, fv.position" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getVehicleDetailByFleet/{fleet_id}")]
        public IHttpActionResult getVehicleDetailByFleet(string fleet_id)
        {
            string prm = "";
            string StrQuery = @"select G.register_vhc_no,convert(varchar,G.date_created,106) as register_date,V.vehicle_rfid as rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position as sort, 
						count(a.axis_id) as default_number_of_tire
						from
	                    FLEET_VEHICLE FV
	                    INNER JOIN VEHICLE V on FV.vehicle_id = v.vehicle_id
	                    inner join fleet f on f.fleet_id=fv.fleet_id
	                    inner join AXIS_TYPE AE ON v.axis_type=AE.axis_type
						inner join axis a on a.axis_type = ae.axis_type
	                    LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id and g.vehicle_id<>'0'" + System.Environment.NewLine;
            StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;

            if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            {
                StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            }
            StrQuery += @"
                        group by G.register_vhc_no,G.date_created,V.vehicle_rfid,
	                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                    v.axis_type,ae.axis_type_name, fv.position 
                        order by fv.fleet_id, fv.position" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTireListWorkshopScrap")]
        public IHttpActionResult getTireListWorkshopScrap()
        {
            string prm = "";
            string StrQuery = @"select slw_no, slw.date_created, slw.location_id, loc.location, isnull(slw.description,'') as description from SL_WORKSHOP slw
                                    inner join LOCATION loc on loc.location_id = slw.location_id
                                    where slw.is_delete=0

                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            StrQuery += "order by slw_no,  slw.date_created" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireListWorkshopScrapDetail/{slw_no}")]
        public IHttpActionResult getTireListWorkshopScrapDetail(string slw_no)
        {
            string prm = "";
            string StrQuery = @"
                                select slw_no, slwd.date_created, isnull(slwd.description,'') as description, slwd.tire_id, t.tire_name from SL_WORKSHOP_DETAIL slwd
                                inner join TIRE t on t.tire_id = slwd.tire_id
                                where slw_no = @slw_no and slwd.is_delete=0
                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            StrQuery += "order by slw_no,  slwd.date_created" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { slw_no = slw_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireScrap/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireScrap(string date_start, string date_end, string userid)
        {
            string prm = "";
            string StrQuery = @"select isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as code, t.tire_id, sc.date_created,r.reason_id, r.reason_name,a.area_damage_id, a.area_damage_name, isnull(sc.description,'') as description 
								from SCRAP_TIRE sc
								inner join TIRE t on t.tire_id = sc.tire_id
								inner join item i on i.tire_code = t.tire_code
								inner join REASON r on r.reason_id = sc.reason_id
								inner join AREA_DAMAGE a on a.area_damage_id = sc.area_damage_id
                                where sc.is_delete=0
                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(sc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and sc.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += "order by sc.date_created desc" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, userid}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireScrapDetail/{scrap_no}")]
        public IHttpActionResult getTireScrapDetail(string scrap_no)
        {
            string prm = "";
            string StrQuery = @"select sd.scrap_no, sd.date_created, isnull(sd.description,'') as description, sd.tire_id, t.tire_name, r.reason_id, r.reason_name, ad.area_damage_id, ad.area_damage_name,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code from SCRAP_TIRE sd
                                    inner join TIRE t on t.tire_id = sd.tire_id
									inner join reason r on r.reason_id = sd.reason_id
                                    inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                                    inner join ITEM i on i.tire_code=t.tire_code
                                    where scrap_no = @scrap_no and sd.is_delete=0

                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            StrQuery += "order by sd.scrap_no,  sd.date_created" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { scrap_no= scrap_no}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireDisposal/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireDisposal(string date_start, string date_end, string userid)
        {
            string prm = "";
            string StrQuery = @"select disposal_no, d.date_created, d.location_id, loc.location, isnull(d.description,'') as description from DISPOSAL d
                                inner join LOCATION loc on loc.location_id = d.location_id
                                where d.is_delete=0
                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(d.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and d.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += "order by disposal_no,  d.date_created" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getStatus")]
        public IHttpActionResult getStatus()
        {

            string StrQuery = @"select status_id, status_name, status_desc from [STATUS] where is_delete = 0 " + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpPost]
        [Route("api/Master/setTyreDirectly")]
        public IHttpActionResult setTyreDirectly([FromBody]Master.cCrudTyreDirectly prm)
        {
            var returnRes = Master.InsertTyreDirectly(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setTyreStatus")]
        public IHttpActionResult setTyreStatus([FromBody]Master.cCrudStatus prm)
        {
            var returnRes = Master.InsertTyreStatus(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteTyreStatus")]
        public IHttpActionResult deleteTyreStatus([FromBody]Master.cCrudStatus prm)
        {
            var returnRes = Master.deleteTyreStatus(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getLookupTireWriteDesktop/{is_ptsi}")]
        public IHttpActionResult getLookupTireWriteDesktop(string is_ptsi)
        {
            string prm = "";
            string StrQuery = @" 
				               select t.tire_id+ ' - '+ t.tire_code as tire from TIRE t
                                inner join LOCATION l on l.location_id = t.location_id
							 where t.tire_id not in (select tire_id from REGISTER_TIRE)  and t.tire_id not in (select tire_id from TIRE_TRACKING where epc_rfid not in ('0')) 
                                " + System.Environment.NewLine;
            if (is_ptsi == "1")
            {
                StrQuery += "and l.parent in ('A00') ";
            }
            else if (is_ptsi == "0")
            {
                StrQuery += "and l.parent not in ('A00') ";
            }
                //StrQuery += " and t.tire_id like @sn" + System.Environment.NewLine;

                StrQuery += "order by T.tire_id" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireByLocationForChangeTyre/{sn}/{location_id}")]
        public IHttpActionResult getTireByLocationForChangeTyre(string sn, string location_id)
        {
            string prm = "";
            string StrQuery = @" 
				                select label=T.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, value=t.tire_id from tire t
				                INNER JOIN ITEM I ON I.tire_code = T.tire_code
				                where  status_id='1' and tire_id not in('XXX')  and location_id not in ('OV00') and t.location_id=@location_id
                                " + System.Environment.NewLine;
           
             StrQuery += " and t.tire_id like @sn" + System.Environment.NewLine;
           
            StrQuery += "order by T.tire_id" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { sn = sn + "%", location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireByLocation/{location_id}")]
        public IHttpActionResult getTireByLocation(string location_id)
        {
            string prm = "";
            string StrQuery = @" select * from tire where  
                                status_id='1' and tire_id not in('XXX')
                                " + System.Environment.NewLine;
            if (location_id != "0")
            {
                StrQuery += " and location_id=@location_id" + System.Environment.NewLine;
            }
            StrQuery += "order by tire_id" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireFromConsWH/{brand_id}")]
        public IHttpActionResult getTireFromConsWH(string brand_id)
        {
            string prm = "";
            string StrQuery = @" select i.tire_code, bt.brand_tire_id, bt.brand_tire_name , isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as code, t.tire_id, rd.date_created, rd.cons_rcv_date
								 from CONSIGNMENT_RECEIVE_DETAIL rd
								inner join TIRE t on t.tire_id = rd.tire_id
								inner join item i on i.tire_code = t.tire_code
								inner join BRAND_TIRE bt on bt.brand_tire_id = i.brand_tire_id
								LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and  tu.is_delete=0
                                   where rd.tire_id not in (select tire_id from INBOUND ob  
										inner join INBOUND_DETAIL obd on obd.INBOUND_id = ob.INBOUND_id and ob.date_created = obd.date_created
										where ob.location_id='A00S002'
										)
                                    and t.is_delete=0  and t.status_id='0' and tu.vehicle_id is  null
                                " + System.Environment.NewLine;
            if (brand_id != "0") {
                StrQuery+= " and bt.brand_tire_id=@brand_id" + System.Environment.NewLine;
            }
            StrQuery += "order by rd.cons_rcv_date desc, rd.date_created desc, i.tire_code" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { brand_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireDisposalDetail/{disposal_no}")]
        public IHttpActionResult getTireDisposalDetail(string disposal_no)
        {
            string prm = "";
            string StrQuery = @"select dd.disposal_no, dd.date_created, isnull(dd.description,'') as description, dd.tire_id, t.tire_name,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code from DISPOSAL_DETAIL dd
                                inner join TIRE t on t.tire_id = dd.tire_id
                                inner join ITEM i on i.tire_code=t.tire_code
                                where disposal_no = @disposal_no and dd.is_delete=0
                                " + System.Environment.NewLine;
            //StrQuery += "where FV.is_delete=0" + System.Environment.NewLine;
            //if (fleet_id != "0" && fleet_id != "-" && fleet_id != "")
            //{
            //    StrQuery += "	and (FV.fleet_id=@fleet_id)" + System.Environment.NewLine;
            //}
            StrQuery += "order by dd.disposal_no,  dd.date_created" + System.Environment.NewLine;
            //StrQuery += "	where (V.vehicle_id='TLI0042')" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { disposal_no = disposal_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Location Filter
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/Master/getListLocationFilter/{location_id}/{is_wh}")]
        public IHttpActionResult getListLocationFilter(string location_id, string is_wh)
        {
            // DataTable data = Master.getListLocationFilter(location_id);
            // var data = con.Query<cLocation>(qGetListLocationFilter(location_id)).ToList();

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListLocationFilter(location_id, is_wh), new { location_id = location_id, is_wh }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
            //return Json((new { records = data.Rows.Count, data = data }));

        }
        [HttpGet]
        [Route("api/Master/getListLocationFilterDispose/{location_id}")]
        public IHttpActionResult getListLocationFilterDispose(string location_id)
        {
            // DataTable data = Master.getListLocationFilter(location_id);
            // var data = con.Query<cLocation>(qGetListLocationFilter(location_id)).ToList();

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListLocationFilterDispose(location_id), new { location_id = location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
            //return Json((new { records = data.Rows.Count, data = data }));

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Repair Category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("api/Master/getListRepairCategory/")]
        public IHttpActionResult getListRepairCategory()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListRepairCategory(), null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Observation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListObservation()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListObservation(), null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Supplier
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListSupplier()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select supplier_id,supplier_name from SUPPLIER where is_delete=0", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Brand
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListBrand()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select brand_id,brand_name from BRAND", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        public IHttpActionResult getListBrandVehicle()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select brand_vhc_id,brand_vhc_name from BRAND_VEHICLE where is_delete = 0", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        public IHttpActionResult getListBrandTire()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select brand_tire_id,brand_tire_name from BRAND_TIRE where is_delete = 0 and brand_tire_id not in('0')", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        
        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Vehicle
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListVehicle()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select fv.fleet_id, v.vehicle_id,vehicle_name,fv.date_created,axis_type,rfid = vehicle_rfid,f.km,f.hm,position
                                            from FLEET_VEHICLE FV
                                            inner join FLEET F ON FV.fleet_id = F.fleet_id
                                            INNER JOIN VEHICLE V on V.vehicle_id = fv.vehicle_id
                                            where V.vehicle_id not in('0','XXX') and fv.is_delete=0 order by fv.fleet_id, position", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        [HttpGet]
        [Route("api/Master/getVehicleMasterById/{vehicle_id}")]
        public IHttpActionResult getVehicleMasterById(string vehicle_id)
        {
            try
            {
                string StrQuery;

                StrQuery = @"select v.vehicle_id, v.vehicle_name, v.vehicle_img, v.vehicle_rfid, v.brand_vhc_id, bv.brand_vhc_name, aty.axis_type, aty.axis_type_name, v.vehicle_type_id from vehicle v
                                            inner join BRAND_VEHICLE BV on v.brand_vhc_id = BV.brand_vhc_id
                                            inner join AXIS_TYPE ATY on ATY.axis_type = v.axis_type
                                            where v.vehicle_id  not in('0','XXX') and v.is_delete='false'
                                            ";
                if (vehicle_id != "0" && vehicle_id != "-" && vehicle_id != "")
                {
                    StrQuery += "	and (V.vehicle_id=@vehicle_id or V.vehicle_rfid=@vehicle_id)" + System.Environment.NewLine;
                }
                StrQuery += "order by v.vehicle_id, v.vehicle_name";
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                var data = dbConn.Query(StrQuery, new { vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        public IHttpActionResult getVehicleMaster()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select v.vehicle_id, v.vehicle_name, v.vehicle_img, v.vehicle_rfid, v.brand_vhc_id, bv.brand_vhc_name, aty.axis_type, aty.axis_type_name, v.vehicle_type_id from vehicle v
                                            inner join BRAND_VEHICLE BV on v.brand_vhc_id = BV.brand_vhc_id
                                            inner join AXIS_TYPE ATY on ATY.axis_type = v.axis_type
                                            where v.vehicle_id  not in('0','XXX') and v.is_delete='false'
                                            order by v.vehicle_id, v.vehicle_name", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        public IHttpActionResult getLocationMaster()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select location_id, location, parent, [group], level, sort, area, detail, process_area
                                            from location
                                            where is_delete=0
                                            and location_id not in ('XXX','999','888','ZZZ','OV00')
                                            order by location_id, location", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        public IHttpActionResult getLocationFilterReport()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select location_id, location, parent, [group], level, sort, area, detail, process_area
                                            from location
                                            where is_delete=0
                                            and location_type_id in ('1','2','3','4')
                                            order by location_id, location", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getLocationType")]
        public IHttpActionResult getLocationType()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select location_type_id, location_type from location_type where is_delete = '0' and location_type_id not in ('0','4')"
                , null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        public IHttpActionResult getAxisMaster()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select axis_id, a.axis_type, aty.axis_type_name, tire_no, axis_no
	                                        from axis a
	                                        inner join AXIS_TYPE aty on aty.axis_type = a.axis_type
	                                        where axis_id <> '0'  and aty.is_delete=0 and a.is_delete=0

	                                        order by tire_no", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        [HttpGet]
        [Route("api/Master/getListAxis/{axis_type}")]
        public IHttpActionResult getListAxis(string axis_type)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"

											select axis_no, count(tire_no) number_of_tire from axis where axis_type=@axis_type
                                            and is_delete=0
											group by axis_no", new { axis_type= axis_type}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        public class FleetData
        {
            public string fleet_id { get; set; }
            public string fleet_name { get; set; }
            public string description { get; set; }
            public string chassis_no { get; set; }
            public string trailer_id1 { get; set; }
            public string unit_code1 { get; set; }
            public string trailer_id2 { get; set; }
            public string unit_code2 { get; set; }
            public string trailer_id3 { get; set; }
            public string unit_code3 { get; set; }
            public string trailer_id4 { get; set; }
            public string unit_code4 { get; set; }
            public string dolly_id { get; set; }
        }
        public class FleetDataUpdateFDS
        {
            public string unit_code1 { get; set; }
            public string unit_code2 { get; set; }
            public string unit_code3 { get; set; }
            public string unit_code4 { get; set; }
        }

        public class ResponseUpdatePrimemoverFds
        {
            public Boolean status { get; set; }
            public String message { get; set; }
        }
        public class RootObject
        {
            public Metadata metadata { get; set; }
            public OdometerData response { get; set; }
        }

        public class Metadata
        {
            public string message { get; set; }
            public int code { get; set; }
        }

        public class OdometerData
        {
            public string fleetID { get; set; }
            public DateTime lastUpdate { get; set; }
            public int odometer { get; set; }
            public int hourmeter { get; set; }
        }
        public class FleetVehicle
        {
            public string fleet_id { get; set; }
            public string vehicle_id { get; set; }
            public string position { get; set; }
            public string description { get; set; }
            public string brand_vhc_id { get; set; }
            public string brand_vhc_name { get; set; }
            // Tambahkan properti lain sesuai dengan kolom tabel fleet_vehicle
        }
        public static string getBrandVehicleImport(string brand)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("BRAND_VEHICLE");
            string StrQuery = @" select brand_vhc_id, brand_vhc_name from BRAND_VEHICLE where (lower(brand_vhc_name) like @brand or lower(brand_vhc_id) like @brand) and is_delete=0 ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@brand", "%"+ brand.ToLower() + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                        select (string)dr["brand_vhc_id"]).FirstOrDefault();
            }
            return data;
        }
       public static List<FleetVehicle> getFleetByFleetIDImport(string fleet_id, string brand)
        {
            List<FleetVehicle> fleetList = new List<FleetVehicle>();
            string StrQuery = @"
            SELECT fv.fleet_id, fv.vehicle_id, fv.position, fv.description, v.brand_vhc_id, bv.brand_vhc_name from fleet_vehicle fv
			inner join VEHICLE v on v.vehicle_id = fv.vehicle_id
			inner join BRAND_VEHICLE bv on bv.brand_vhc_id = v.brand_vhc_id
            where fleet_id = @fleet_id and fv.is_delete = 0 and (lower(bv.brand_vhc_name) like @brand or lower(bv.brand_vhc_id) like @brand) and bv.is_delete=0
			and v.is_delete=0 order by position";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(StrQuery, con))
                {
                    cmd.Parameters.AddWithValue("@fleet_id", fleet_id);
                    cmd.Parameters.AddWithValue("@brand", "%"+ brand.ToLower() + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FleetVehicle fleetVehicle = new FleetVehicle
                            {
                                fleet_id = reader["fleet_id"].ToString(),
                                vehicle_id = reader["vehicle_id"].ToString(),
                                position = reader["position"].ToString(),
                                description = reader["description"].ToString(),
                                brand_vhc_id = reader["brand_vhc_id"].ToString(),
                                brand_vhc_name = reader["brand_vhc_name"].ToString(),
                            };
                            fleetList.Add(fleetVehicle);
                        }
                    }
                }
            }

            return fleetList;
        }
        public List<Process.cCrudVehicleChangeOldData> CheckFleetForImportFds(string fleet_id)
        {
            List<Process.cCrudVehicleChangeOldData> result = new List<Process.cCrudVehicleChangeOldData>();
            string no = "";
            DataTable dtTempTable = new DataTable("FLEET_VEHICLE");
            string StrQuery = "select * from FLEET_VEHICLE where fleet_id = @fleet_id and is_delete = 'False' order by position";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@fleet_id", fleet_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTempTable.Rows)
                {
                    // Create an instance of cCrudVehicleChangeOldData for each row and populate its properties
                    Process.cCrudVehicleChangeOldData vehicleData = new Process.cCrudVehicleChangeOldData
                    {
                        sort = dr["position"].ToString(),
                        vehicle_id = dr["vehicle_id"].ToString()
                    };

                    // Add the instance to the result list
                    result.Add(vehicleData);
                }
            }

            return result;
        }

        public List<Process.cCrudVehicleCheckDataNew> CheckDataNewTrailerForImportFds(string vehicle_id)
        {
            List<Process.cCrudVehicleCheckDataNew> result = new List<Process.cCrudVehicleCheckDataNew>();
            string no = "";
            DataTable dtTempTable = new DataTable("FLEET_VEHICLE");
            string StrQuery = "select * from FLEET_VEHICLE where vehicle_id = @vehicle_id and is_delete = 'False' order by position";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTempTable.Rows)
                {
                    // Create an instance of cCrudVehicleChangeOldData for each row and populate its properties
                    Process.cCrudVehicleCheckDataNew vehicleData = new Process.cCrudVehicleCheckDataNew
                    {
                        sort = dr["position"].ToString(),
                        fleet_id = dr["fleet_id"].ToString(),
                        vehicle_id = dr["vehicle_id"].ToString()
                    };

                    // Add the instance to the result list
                    result.Add(vehicleData);
                }
            }

            return result;
        }
        [HttpGet]
        [Route("api/Master/getLastKMTMS/{fleet_id}")]
        public async Task<IHttpActionResult> getLastKMTMS(string fleet_id)
        {
            DateTime date_created = DateTime.Now;
            // URL API yang ingin Anda panggil
            string configUrl = ConfigurationManager.AppSettings["url_getlastkmhm_tms"].ToString();
            string apiUrl = configUrl + fleet_id;
            var responseDataNull = new object[] { };
            string bearerToken = ConfigurationManager.AppSettings["static_token_tms"].ToString();
            string message = "Failed request. Cannot request to API. Check your connection!";
            List<OdometerData> result = null;
            Process.cOdometer odometer = null;
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;

            //SecurityProtocolType tlsVersion = ServicePointManager.SecurityProtocol;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Tls13
                   | SecurityProtocolType.Ssl3;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        RootObject root = JsonConvert.DeserializeObject<RootObject>(responseBody);
                        if (root.metadata.message == "OK")
                        {
                            // Memasukkan data ke dalam List<OdometerData>
                            result = new List<OdometerData>
                            {
                                new OdometerData
                                {
                                    fleetID = root.response.fleetID,
                                    lastUpdate = root.response.lastUpdate,
                                    odometer = root.response.odometer,
                                    hourmeter = root.response.hourmeter
                                }
                            };
                            message = root.metadata.message;
                        }
                        else {
                            message= "Failed request. Error: " + root.metadata.message;
                        }
                    }
                    else
                    {
                        try {
                            throw new Exception("HTTP Request failed with status code: " + response.StatusCode);
                        }
                        catch(Exception ex) {
                            message = "Failed request. Error: " + ex.Message;
                        }
                        
                    }


                }
                catch (Exception ex)
                {
                    message = "Failed request. Error: " + ex.Message.ToString();
                }
            }
            if (result != null)
            {
                //var sortedList = ListNew.OrderBy(j => j.sort);
                //List<JObject> sortedList = ListNew.OrderBy(j => j["sort"]).ToList();
                return Json(new { status = true, records = result.Count, data= result, message });
            }
            else
            {
                return Json(new { status = false, records = 0, data = "", message });
            }

        }

        [HttpPost]
        [Route("api/Master/updatePrimoverTms")]
        public async Task<IHttpActionResult> updatePrimoverTms(FleetData prm)
        {
            var code = 0;
            Boolean status = false;
            DateTime date_created = DateTime.Now;
            // URL API yang ingin Anda panggil
            string configUrl = ConfigurationManager.AppSettings["url_updateprimover_tms"].ToString();
            string apiUrl = configUrl;
            var responseDataNull = new object[] { };
            string bearerToken = ConfigurationManager.AppSettings["static_token_tms"].ToString();
            string message = "Failed request. Cannot request to API. Check your connection!";

            List<FleetData> dataToSend = new List<FleetData>
            {
                new FleetData {
                                fleet_id = prm.fleet_id,
                                fleet_name = prm.fleet_name,
                                description = prm.description,
                                trailer_id1 = prm.trailer_id1,                                
                                unit_code1 = prm.unit_code1,
                                trailer_id2 = prm.trailer_id2,
                                unit_code2 = prm.unit_code2,
                                trailer_id3 = prm.trailer_id3,
                                unit_code3 = prm.unit_code3,
                                trailer_id4 = prm.trailer_id4,
                                unit_code4 = prm.unit_code4,
                                dolly_id = prm.dolly_id
                },
                // Tambahkan objek kelas lain ke dalam daftar ini sesuai kebutuhan Anda
            };
            string jsonData = JsonConvert.SerializeObject(dataToSend);
            List<OdometerData> result = null;
            Process.cOdometer odometer = null;
            Metadata res = null;
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;

            //SecurityProtocolType tlsVersion = ServicePointManager.SecurityProtocol;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Tls13
                   | SecurityProtocolType.Ssl3;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<Metadata>(responseBody);
                        if (res.code == 1) {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            throw new Exception("HTTP Request failed with status code: " + response.StatusCode);
                        }
                        catch (Exception ex)
                        {
                            code = 0;
                            status = false;
                            message = "Failed request. Error: " + ex.Message;
                        }

                    }
                }
            }
            catch (HttpRequestException ex)
            {
                code = 0;
                status = false;
                // Tangani kesalahan "an occurred while sending the request"
                message = "An occurred while sending the request: " + ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                {
                    message += " " + ex.InnerException.InnerException.Message;
                }
            }
            catch (Exception ex)
            {
                code = 0;
                status = false;
                message = "Failed request. Error: " + ex.Message.ToString();
            }

            if (res != null)
            {
                return Json((new { status, res.code, res.message }));
            }
            else
            {
                return Json((new { status, code, message }));
            }


        }
        [HttpPut]
        [Route("api/Master/sampleUpdatePrimoverFds/{fleet_id}")]
        public IHttpActionResult sampleUpdatePrimoverFds(string fleet_id)
        {
            string status = "true";
            string message = "Update Succesfully";
            if (Request.Method != HttpMethod.Put)
            {
                status = "false";
                message = "Failed Request";
            }

            return Json((new { status, message }));
        }
        [HttpPost]
        [Route("api/Master/sampleUpdatePrimoverTms")]
        public IHttpActionResult sampleUpdatePrimoverTms()
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
        [HttpPut]
        [Route("api/master/updatePrimoverFds")]
        public async Task<IHttpActionResult> updatePrimoverFds(FleetData prm)
        {
            Boolean status = false;
            string message = "Failed request. Cannot request to API. Check your connection!";
            
            DateTime date_created = DateTime.Now;
            // URL API yang ingin Anda panggil
            string configUrl = ConfigurationManager.AppSettings["url_updateprimover_fds"].ToString();
            string apiUrl = configUrl + prm.fleet_id;
            var responseDataNull = new object[] { };
            string bearerToken = ConfigurationManager.AppSettings["static_token_fds"].ToString();//"7B2xa14uA4kWAEE7Rsi60YM3zLjOzGLtegLvYxPGCJlobDkojzEbhwy7Su9CBR"; //STATIC TOKEN FROM IT RAPP

            List<FleetDataUpdateFDS> dataToSend = new List<FleetDataUpdateFDS>
            {
                new FleetDataUpdateFDS { 
                                unit_code1 = prm.unit_code1,
                                unit_code2 = prm.unit_code2,
                                unit_code3 = prm.unit_code3,
                                unit_code4 = prm.unit_code4
                },
                // Tambahkan objek kelas lain ke dalam daftar ini sesuai kebutuhan Anda
            };
            string jsonData = JsonConvert.SerializeObject(dataToSend);
            ResponseUpdatePrimemoverFds res = null;
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;
           
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Tls13
                   | SecurityProtocolType.Ssl3;
            try {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<ResponseUpdatePrimemoverFds>(responseBody);

                    }
                    else
                    {
                        try
                        {
                            throw new Exception("HTTP Request failed with status code: " + response.StatusCode);
                        }
                        catch (Exception ex)
                        {
                            message = "Failed request. Error: " + ex.Message;
                        }

                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Tangani kesalahan "an occurred while sending the request"
                message = "An occurred while sending the request: " + ex.InnerException.Message;
                if (ex.InnerException.InnerException != null)
                {
                    message += " " + ex.InnerException.InnerException.Message;
                }
            }
            catch (Exception ex)
            {
                message = "Failed request. Error: " + ex.Message.ToString();
            }
           
            if (res != null)
            {
                return Json((new { res.status, res.message }));
            }
            else
            {
                return Json((new { status, message }));
            }
            
            
            
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Selalu mengembalikan true untuk mempercayai sertifikat,
            // Ini hanya untuk tujuan demonstrasi dan mungkin tidak aman di produksi.
            return true;
        }
        [HttpGet]
        [Route("api/Master/getFleetFds/{fleet_id}")]
        public async Task<IHttpActionResult> getFleetFds(string fleet_id)
        {
            DateTime date_created = DateTime.Now;
            // URL API yang ingin Anda panggil
            string configUrl = ConfigurationManager.AppSettings["url_getprimover_fds"].ToString();
            string apiUrl = configUrl + fleet_id;
            var responseDataNull = new object[] { };
            string bearerToken = ConfigurationManager.AppSettings["static_token_fds"].ToString();//"7B2xa14uA4kWAEE7Rsi60YM3zLjOzGLtegLvYxPGCJlobDkojzEbhwy7Su9CBR"; //STATIC TOKEN FROM IT RAPP
            List<FleetData> fleetDataList = null;
            List<Process.cCrudVehicleChangeOldData> DataOldFleet = null;
            List<Process.cCrudVehicleCheckDataNew> CheckTrailerDataNew = null;
            List<Process.cCrudVehicleChangeNewData> ListNew = null;
            string message = "Failed request. Cannot request to API. Check your connection!";
            string status = "0";
            var indUsedTrailer = 0;
            string usedTrailer = "";
            string myFleetId = "";
            //status 0 success;
            //status 1 warning;
            //status 2 error;
            //status 3 dialog box;
            
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            ServicePointManager.Expect100Continue = true;

            //SecurityProtocolType tlsVersion = ServicePointManager.SecurityProtocol;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Tls13
                   | SecurityProtocolType.Ssl3;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Mengecek kode status respon
                    if (response.IsSuccessStatusCode)
                    {
                        // Membaca konten respon sebagai string
                        string responseBody = await response.Content.ReadAsStringAsync();
                        fleetDataList = JsonConvert.DeserializeObject<List<FleetData>>(responseBody);

                        if (fleetDataList.Count == 0)
                        {
                            message = "Fleet not found at FDS";
                            status = "1";
                        }
                        else
                        {
                            DataOldFleet = CheckFleetForImportFds(fleet_id);
                            if (DataOldFleet.Count() != 0)
                            {
                                ListNew = new List<Process.cCrudVehicleChangeNewData>();
                                foreach (FleetData fleetData in fleetDataList)
                                {
                                    string fleetId = fleetData.fleet_id;
                                    Console.WriteLine("fleet_id: " + fleetId);
                                    FleetData fleetDataClass = new FleetData();
                                    Type fleetDataType = fleetDataClass.GetType();

                                    foreach (PropertyInfo propertyInfo in fleetDataType.GetProperties())
                                    {
                                        Process.cCrudVehicleChangeNewData dataListNew = new Process.cCrudVehicleChangeNewData();
                                       
                                        switch (propertyInfo.Name)
                                        {
                                            case "fleet_id":
                                                dataListNew.sort = "0";
                                                dataListNew.vehicle_id = fleetData.fleet_id.ToString();
                                                ListNew.Add(dataListNew);
                                                break;

                                            case "unit_code1":
                                                if (fleetData.unit_code1 != null) {
                                                    dataListNew.sort = "1";
                                                    dataListNew.vehicle_id = fleetData.unit_code1.ToString();
                                                    ListNew.Add(dataListNew);
                                                }
                                                    
                                                break;

                                            case "unit_code2":
                                                if (fleetData.unit_code2 != null)
                                                {
                                                    if (fleetData.dolly_id == null)
                                                    {
                                                        dataListNew.sort = "2";
                                                        dataListNew.vehicle_id = fleetData.unit_code2.ToString();
                                                        ListNew.Add(dataListNew);

                                                    }
                                                    else
                                                    {
                                                        dataListNew.sort = "3";
                                                        dataListNew.vehicle_id = fleetData.unit_code2.ToString();
                                                        ListNew.Add(dataListNew);
                                                    }
                                                }

                                                break;

                                            case "unit_code3":
                                                if (fleetData.unit_code3 != null)
                                                {
                                                    dataListNew.sort = "3";
                                                    dataListNew.vehicle_id = fleetData.unit_code3.ToString();
                                                    ListNew.Add(dataListNew);
                                                }
                                                break;

                                            case "unit_code4":
                                                if (fleetData.unit_code4 != null)
                                                {
                                                    dataListNew.sort = "4";
                                                    dataListNew.vehicle_id = fleetData.unit_code4.ToString();
                                                    ListNew.Add(dataListNew);
                                                }
                                                    
                                                break;

                                            case "dolly_id":
                                                if (fleetData.dolly_id != null)
                                                {
                                                    dataListNew.sort = "2";
                                                    dataListNew.vehicle_id = fleetData.dolly_id.ToString();
                                                    ListNew.Add(dataListNew);
                                                }

                                                break;
                                            default:
                                                //Console.WriteLine("Pilihan default");
                                                break;

                                            
                                        }
                                        
                                    }
                                }
                                var sama = 1;
                                if (DataOldFleet.Count == ListNew.Count)
                                {
                                    int no = 0;
                                    foreach (var dr in DataOldFleet)
                                    {

                                        if (dr.sort == ListNew[no].sort && dr.vehicle_id == ListNew[no].vehicle_id)
                                        {
                                            //"Tidak ada perubahan data";
                                            sama = 1;
                                        }
                                        else
                                        {
                                            sama = 0;
                                            foreach (var dn in ListNew)
                                            {
                                                if (dn.sort == "0") {
                                                    myFleetId = dn.vehicle_id;
                                                }
                                                else 
                                                {
                                                    CheckTrailerDataNew = CheckDataNewTrailerForImportFds(dn.vehicle_id);
                                                    if (CheckTrailerDataNew.Count != 0)
                                                    {
                                                        if (CheckTrailerDataNew[0].fleet_id != myFleetId)
                                                        {
                                                            indUsedTrailer = 1;
                                                            usedTrailer += "Trailer " + dn.vehicle_id + " has been used in fleet " + CheckTrailerDataNew[0].fleet_id + " <br>";
                                                        }
                                                        else {
                                                            indUsedTrailer += 0;
                                                        }
                                                    }
                                                    else {
                                                        indUsedTrailer += 0;
                                                    }
                                                }
                                                
                                            }
                                            
                                        }
                                        no++;
                                    }
                                }
                                else {

                                    sama = 0;
                                    foreach (var dn in ListNew)
                                    {
                                        if (dn.sort == "0")
                                        {
                                            myFleetId = dn.vehicle_id;
                                        }
                                        else
                                        {
                                            CheckTrailerDataNew = CheckDataNewTrailerForImportFds(dn.vehicle_id);
                                            if (CheckTrailerDataNew.Count != 0)
                                            {
                                                if (CheckTrailerDataNew[0].fleet_id != myFleetId)
                                                {
                                                    indUsedTrailer = 1;
                                                    usedTrailer += "Trailer " + dn.vehicle_id + " has been used in fleet " + CheckTrailerDataNew[0].fleet_id + " <br>";
                                                }
                                                else
                                                {
                                                    indUsedTrailer += 0;
                                                }
                                            }
                                            else
                                            {
                                                indUsedTrailer += 0;
                                            }
                                            
                                        }
                                        
                                    }
                                }
                                if (sama == 1)
                                {
                                    status = "1";
                                    message = "The fleet " + fleet_id + " is already in the system with same trailer.";
                                }
                                else {
                                    if (indUsedTrailer == 0)
                                    {
                                        status = "3";
                                        message = "The fleet " + fleet_id + " is already in the system with different trailer with FDS system. Please check back!";
                                    }
                                    else {
                                        status = "4";
                                        message = usedTrailer + "<br> You cannot save this fleet!";
                                    }
                                    
                                }
                                

                            }
                            else {
                                status = "0";
                                message = "Ready to create the new fleet from FDS system.";
                                ListNew = new List<Process.cCrudVehicleChangeNewData>();
                                foreach (FleetData fleetData in fleetDataList)
                                {
                                    string fleetId = fleetData.fleet_id;
                                    Console.WriteLine("fleet_id: " + fleetId);
                                    FleetData fleetDataClass = new FleetData();
                                    Type fleetDataType = fleetDataClass.GetType();

                                    foreach (PropertyInfo propertyInfo in fleetDataType.GetProperties())
                                    {
                                        Process.cCrudVehicleChangeNewData dataListNew = new Process.cCrudVehicleChangeNewData();

                                        switch (propertyInfo.Name)
                                        {
                                            case "fleet_id":
                                                dataListNew.sort = "0";
                                                dataListNew.vehicle_id = fleetData.fleet_id.ToString();
                                                ListNew.Add(dataListNew);
                                                break;

                                            case "unit_code1":
                                                if (fleetData.unit_code1 != null)
                                                {
                                                    dataListNew.sort = "1";
                                                    dataListNew.vehicle_id = fleetData.unit_code1.ToString();
                                                    ListNew.Add(dataListNew);
                                                }

                                                break;

                                            case "unit_code2":
                                                if (fleetData.unit_code2 != null)
                                                {
                                                    if (fleetData.dolly_id == null)
                                                    {
                                                        dataListNew.sort = "2";
                                                        dataListNew.vehicle_id = fleetData.unit_code2.ToString();
                                                        ListNew.Add(dataListNew);

                                                    }
                                                    else
                                                    {
                                                        
                                                        dataListNew.sort = "3";
                                                        dataListNew.vehicle_id = fleetData.unit_code2.ToString();
                                                        ListNew.Add(dataListNew);
                                                    }
                                                }

                                                break;

                                            case "unit_code3":
                                                if (fleetData.unit_code3 != null)
                                                {
                                                    dataListNew.sort = "3";
                                                    dataListNew.vehicle_id = fleetData.unit_code3.ToString();
                                                    ListNew.Add(dataListNew);
                                                }
                                                break;

                                            case "unit_code4":
                                                if (fleetData.unit_code4 != null)
                                                {
                                                    dataListNew.sort = "4";
                                                    dataListNew.vehicle_id = fleetData.unit_code4.ToString();
                                                    ListNew.Add(dataListNew);
                                                }

                                                break;
                                            case "dolly_id":
                                                if (fleetData.dolly_id != null)
                                                {
                                                    dataListNew.sort = "2";
                                                    dataListNew.vehicle_id = fleetData.dolly_id.ToString();
                                                    ListNew.Add(dataListNew);
                                                }

                                                break;
                                            default:
                                                //Console.WriteLine("Pilihan default");
                                                break;


                                        }

                                    }
                                }

                                foreach (var dn in ListNew)
                                {
                                    if (dn.sort == "0")
                                    {
                                        myFleetId = dn.vehicle_id;
                                    }
                                    else
                                    {
                                        CheckTrailerDataNew = CheckDataNewTrailerForImportFds(dn.vehicle_id);
                                        if (CheckTrailerDataNew.Count != 0)
                                        {
                                            if (CheckTrailerDataNew[0].fleet_id != myFleetId)
                                            {
                                                indUsedTrailer = 1;
                                                usedTrailer += "Trailer " + dn.vehicle_id + " has been used in fleet " + CheckTrailerDataNew[0].fleet_id + " <br>";
                                                status = "4";
                                                message = usedTrailer + "<br> You cannot save this fleet!";
                                            }
                                            else
                                            {
                                                indUsedTrailer += 0;
                                            }
                                        }
                                        else
                                        {
                                            indUsedTrailer += 0;
                                        }

                                    }

                                }
                            }
                        }

                        // Mengembalikan respon dalam format JSON
                        //return Json(fleetDataList);
                    }
                    else
                    {
                        status = "2";
                        message = "Failed request. Status Code: " + response.StatusCode;
                        
                    }
                }
                catch (HttpRequestException ex)
                {
                    // Tangani kesalahan "an occurred while sending the request"
                    status = "2";
                    message = "An occurred while sending the request: " + ex.InnerException.Message ;
                    if (ex.InnerException.InnerException != null)
                    {
                        message += " " + ex.InnerException.InnerException.Message;
                    }
                }
                catch (Exception ex)
                {
                    status = "2";
                    message = "Failed request. Error: "+ ex.Message.ToString();
                }
            }
            kesininih:
            if (fleetDataList != null)
            {
                //var sortedList = ListNew.OrderBy(j => j.sort);
                //List<JObject> sortedList = ListNew.OrderBy(j => j["sort"]).ToList();
                return Json((new { status= status, records = fleetDataList.Count, data_from_fds = fleetDataList, data_old = DataOldFleet, data_new = ListNew.OrderBy(j => j.sort), message = message }));
            }
            else {
                return Json((new { status = status, records = 0, data_from_fds = responseDataNull, data_old = responseDataNull, data_new = responseDataNull, message = message }));
            }

        }
        [HttpGet]
        [Route("api/Master/getSampleTms/{fleet_id}")]
        public IHttpActionResult getSampleTms(string fleet_id)
        {
            string strNull = null;
            string projectDirectory = HttpContext.Current.Server.MapPath("~");

            // Membentuk path lengkap ke file JSON (misalnya, "data.json")
            string jsonFilePath = Path.Combine(projectDirectory, "sample_tms.json");
            try
            {
                // Membaca isi file JSON ke dalam string
                string jsonContent = File.ReadAllText(jsonFilePath);
                List<OdometerData> result = null;
                // Mengurai string JSON menjadi list objek FleetData
                var root = JsonConvert.DeserializeObject(jsonContent);
                //if (root.metadata.message == "OK") {
                //    // Memasukkan data ke dalam List<OdometerData>
                //    result = new List<OdometerData>
                //    {
                //        new OdometerData
                //        {
                //            fleetID = root.response.fleetID,
                //            lastUpdate = root.response.lastUpdate,
                //            odometer = root.response.odometer,
                //            hourmeter = root.response.hourmeter
                //        }
                //    };
                //}
                

                //var filteredObjects = result.Where(obj => obj.fleet_id == fleet_id).ToList();
                //string filteredJson = JsonConvert.SerializeObject(filteredObjects);
                return Json(root);
                // Lakukan operasi lain dengan variabel result jika diperlukan
            }
            catch (FileNotFoundException)
            {
                return Json((new { message = "File JSON tidak ditemukan." }));
            }
            catch (JsonException)
            {
                Console.WriteLine("Gagal mengurai file JSON. Pastikan format JSON benar.");
                return Json((new { message = "Gagal mengurai file JSON. Pastikan format JSON benar." }));
            }
            catch (Exception ex)
            {
                return Json((new { message = $"Terjadi kesalahan: {ex.Message}" }));
            }

        }
        //[TokenAuthorization]
        [HttpGet]
        [Route("api/Master/getSampleFleet/{fleet_id}")]
        public IHttpActionResult getSampleFleet(string fleet_id)
        {
            string strNull = null;
            string projectDirectory = HttpContext.Current.Server.MapPath("~");
            // Membentuk path lengkap ke file JSON (misalnya, "data.json")
            string jsonFilePath = Path.Combine(projectDirectory, "sample_fleet.json");
            //List<dynamic> result = new List<dynamic>
            //{

            //    new {
            //        fleet_id = "BDP0099",
            //        fleet_name = "Volvo",
            //        description = "Volvo FMX 480 6x4",
            //        chassis_no = "YV2XSW0D1MA869906",
            //        trailer_id1 = "TRL_210521040954",
            //        unit_code1 = "TLP0484",
            //        trailer_id2 = "",
            //        unit_code2 = "TLP0446",
            //        trailer_id3 = "",
            //        unit_code3 = "TLP0224",
            //        trailer_id4 = strNull,
            //        unit_code4 = strNull,
            //        dolly_id = strNull
            //    },
            //    new {
            //        fleet_id = "DUPFDS-28",
            //        fleet_name = "Volvo",
            //        description = "DUPFDS-28",
            //        chassis_no = "DUPFDS-28",
            //        trailer_id1 = "TRL_210318022660",
            //        unit_code1 = "TLP0220",
            //        trailer_id2 = strNull,
            //        unit_code2 = strNull,
            //        trailer_id3 = strNull,
            //        unit_code3 = strNull,
            //        trailer_id4 = strNull,
            //        unit_code4 = strNull,
            //        dolly_id = strNull
            //    },
            //    new {
            //        fleet_id = "BDP0080",
            //        fleet_name = "Volvo",
            //        description = "BDP0080",
            //        chassis_no = "BDP0080",
            //        trailer_id1 = "",
            //        unit_code1 = "TLP0668",
            //        trailer_id2 = "",
            //        unit_code2 = "TLP0670",
            //        trailer_id3 = strNull,
            //        unit_code3 = strNull,
            //        trailer_id4 = strNull,
            //        unit_code4 = strNull,
            //        dolly_id = strNull
            //    },
            //    new {
            //        fleet_id = "RTP2801",
            //        fleet_name = "Volvo",
            //        description = "RTP2801",
            //        chassis_no = "RTP2801",
            //        trailer_id1 = "TRL_210318022660",
            //        unit_code1 = "TLP2801",
            //        trailer_id2 = "",
            //        unit_code2 = "TLP2802",
            //        trailer_id3 = strNull,
            //        unit_code3 = strNull,
            //        trailer_id4 = strNull,
            //        unit_code4 = strNull,
            //        dolly_id = "DLI2801"
            //    },
            //    new {
            //        fleet_id = "RTP2802",
            //        fleet_name = "Volvo",
            //        description = "RTP2802",
            //        chassis_no = "RTP2802",
            //        trailer_id1 = "TRL_210318022660",
            //        unit_code1 = "TLP2821",
            //        trailer_id2 = "",
            //        unit_code2 = "TLP2822",
            //        trailer_id3 = strNull,
            //        unit_code3 = strNull,
            //        trailer_id4 = strNull,
            //        unit_code4 = strNull,
            //        dolly_id = strNull
            //    },
            //};
            try
            {
                // Membaca isi file JSON ke dalam string
                string jsonContent = File.ReadAllText(jsonFilePath);

                // Mengurai string JSON menjadi list objek FleetData
                List<FleetData> result = JsonConvert.DeserializeObject<List<FleetData>>(jsonContent);
                var filteredObjects = result.Where(obj => obj.fleet_id == fleet_id).ToList();
                string filteredJson = JsonConvert.SerializeObject(filteredObjects);
                return Json(filteredObjects);
                // Lakukan operasi lain dengan variabel result jika diperlukan
            }
            catch (FileNotFoundException)
            {
                return Json((new { message = "File JSON tidak ditemukan."}));
            }
            catch (JsonException)
            {
                Console.WriteLine("Gagal mengurai file JSON. Pastikan format JSON benar.");
                return Json((new { message = "Gagal mengurai file JSON. Pastikan format JSON benar." }));
            }
            catch (Exception ex)
            {
                return Json((new { message = $"Terjadi kesalahan: {ex.Message}" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getListKmHmFleet/")]
        public IHttpActionResult getListKmHmFleet()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"
                                   select 
                                        f.fleet_id, f.fleet_name, f.km, f.hm 
                                        from fleet f 
                                        where
										f.is_delete=0 and f.fleet_id not in ('XXX')
                                        order by date_modified desc, date_created desc
                                        ", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getListKmHmFleetDetail/{fleet_id}")]
        public IHttpActionResult getListKmHmFleetDetail(string fleet_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"
                                   select 
                                        f.fleet_id, f.fleet_name, f.km, f.hm 
                                        from fleet f 
                                        inner join fleet_vehicle fv on fv.fleet_id = f.fleet_id
                                        inner join vehicle v on v.vehicle_id = fv.vehicle_id
                                        inner join tire_unit tu on tu.vehicle_id = fv.vehicle_id
                                        inner join tire t on t.tire_id = tu.tire_id
                                        where
                                        fv.fleet_id=@fleet_id
                                        and f.is_delete=0 and FV.is_delete=0
                                        group by f.fleet_id, f.fleet_name, f.km, f.hm
                                        ", new { fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getKmHmFleet/{rfid}")]
        public IHttpActionResult getKmHmFleet(string rfid)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"
                                   select 
                                        f.fleet_id, f.fleet_name, f.km, f.hm 
                                        from fleet f 
                                        inner join fleet_vehicle fv on fv.fleet_id = f.fleet_id
                                        inner join vehicle v on v.vehicle_id = fv.vehicle_id
                                        inner join tire_unit tu on tu.vehicle_id = fv.vehicle_id
                                        inner join tire t on t.tire_id = tu.tire_id
                                        where t.rfid=@rfid  or v.vehicle_rfid=@rfid 
                                        or fv.fleet_id=@rfid  or fv.vehicle_id=@rfid 
                                        and f.is_delete=0 and FV.is_delete=0
                                        group by f.fleet_id, f.fleet_name, f.km, f.hm
                                        ", new { rfid = rfid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        public IHttpActionResult getAxisTypeMaster()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(@"select axis_type, axis_type_name from axis_type where  is_delete=0 order by axis_type", null, null, true, commandTimeout: 0, commandType: null).ToList();//axis_type <> '0' and
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Reason
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListReason()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select reason_id,reason from REASON where is_delete=0 and reason_id not in('0')", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Axle Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult getListAxleType()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query("select axis_type,axis_type_name from AXIS_TYPE where axis_type<>'0'  and is_delete=0", null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        // GET api/documentation
        /// <summary>
        /// Fungsi Get List Tire Axis Per Vehicle
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireAxisPerVehicle/{fleet_id}/{vehicle_id}")]
        public IHttpActionResult getTireAxisPerVehicle(string fleet_id, string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListTireAxisPerVehicle(fleet_id, vehicle_id), new { fleet_id = fleet_id, vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireAxisPerFleet/{fleet_id}")]
        public IHttpActionResult getTireAxisPerFleet(string fleet_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListTireAxisPerFleet(fleet_id), new {fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List kondisi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireDetail/{sn}/{location_id}")]
        public IHttpActionResult getTireDetail(string sn, string location_id)
        {
            List<cVehicle> listVehicle = new List<cVehicle>();
            List<cTireAxis> ListMaterial = new List<cTireAxis>();
            cTire cT = new cTire();

            try
            {


                foreach (DataRow dr in Master.getDataTireDetail(sn, location_id).Rows)
                {
                    
                    cT.tire_id = dr[0].ToString().Trim();
                    cT.serial_number = dr[1].ToString().Trim();
                    cT.tire_name = dr[2].ToString().Trim();
                    cT.size = dr[3].ToString().Trim();
                    cT.pattern = dr[4].ToString().Trim();
                    cT.brand_id = dr[5].ToString().Trim();
                    cT.brand_name = dr[6].ToString().Trim();
                    cT.supplier_id = dr[7].ToString().Trim();
                    cT.supplier_name = dr[8].ToString().Trim();
                    cT.register_no = dr[9].ToString().Trim();
                    cT.register_date = dr[10].ToString().Trim();
                    cT.rfid = dr[11].ToString().Trim();
                    cT.vehicle_id = dr[12].ToString().Trim();
                    cT.vehicle_name = dr[13].ToString().Trim();
                    //cT.vehicle_no = dr[18].ToString().Trim();
                    //if (dr[19].ToString() != "")
                    //{
                    //    cT.is_vehicle = Convert.ToBoolean(dr[19].ToString());
                    //}
                    //else
                    //{
                    //    cT.is_vehicle = false;
                    //}

                    cT.vehicle_setup_no = dr[14].ToString().Trim();
                    cT.axis_id = dr[15].ToString().Trim();
                    cT.tire_no = dr[16].ToString().Trim();
                    cT.axis_no = dr[17].ToString().Trim();
                    cT.axis_type = dr[18].ToString().Trim();
                    cT.axis_type_name = dr[19].ToString().Trim();
                    cT.tire_position = dr[20].ToString().Trim();
                    cT.location_id = dr[21].ToString().Trim();
                    cT.location = dr[22].ToString().Trim();
                    cT.process_id = dr[23].ToString().Trim();
                    cT.process = dr[24].ToString().Trim();
                    cT.date_modified = dr[25].ToString().Trim();
                    cT.str_date_modified = dr[26].ToString().Trim();
                    cT.tire_mileage = dr[27].ToString().Trim();
                    cT.tire_axis_no = dr[28].ToString().Trim();
                    cT.status_id = dr[29].ToString().Trim();
                    cT.tread_depth_value = dr[30].ToString().Trim();
                    //cT.is_active = Convert.ToBoolean(dr[37].ToString());
                    //if (dr[33].ToString() != "")
                    //{
                    //    cT.is_active = Convert.ToBoolean(dr[33].ToString());
                    //}
                    //else
                    //{
                    //    cT.is_active = false;
                    //}


                    List<cVehicle> Lvehicle = new List<cVehicle>();
                    foreach (DataRow drx in Master.getDataListVehicleTire(cT.vehicle_setup_no).Rows)
                    {
                        cVehicle cV = new cVehicle();
                        cV.vehicle_id = drx[0].ToString().Trim();
                        cV.vehicle_name = drx[1].ToString().Trim();
                        cV.rfid = drx[2].ToString().Trim();
                        cV.setup_no = drx[3].ToString().Trim();
                        cV.sort = drx[4].ToString().Trim();
                        cV.axis_type = drx[5].ToString().Trim();
                        cV.axis_type_name = drx[6].ToString().Trim();

                        //List<cTireAxis> LTireAxis = new List<cTireAxis>();
                        //foreach (DataRow drTireAxis in Master.getDataListTireAxis(cT.tire_axis_no, cV.vehicle_id).Rows)
                        //{
                        //    cTireAxis cTa = new cTireAxis();
                        //    cTa.vehicle_id = drTireAxis[0].ToString().Trim();
                        //    cTa.axis_id = drTireAxis[1].ToString().Trim();
                        //    cTa.tire_no = drTireAxis[2].ToString().Trim();
                        //    cTa.axis_no = drTireAxis[3].ToString().Trim();
                        //    cTa.axis_position = drTireAxis[4].ToString().Trim();
                        //    cTa.axis_type = drTireAxis[5].ToString().Trim();
                        //    cTa.axis_type_name = drTireAxis[6].ToString().Trim();
                        //    cTa.tire_axis_position = drTireAxis[7].ToString().Trim();
                        //    cTa.description = drTireAxis[8].ToString().Trim();
                        //    LTireAxis.Add(cTa);
                        //}
                        //cV.tire_axis = LTireAxis;
                        Lvehicle.Add(cV);
                    }

                    cT.vehicle = Lvehicle;

                }
                return Json((new { records = 1, data = cT }));
            }
            catch (Exception e)
            {
                return Json((new { records = 0, data = "", msg = e.Message.ToString() }));
            }


        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get List kondisi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getVehicleDetail/{id}")]
        public IHttpActionResult getVehicleDetail(string id)
        {
            List<cVehicle> listVehicle = new List<cVehicle>();
            List<cTireAxis> ListMaterial = new List<cTireAxis>();
            cVehicleDetail cVd = new cVehicleDetail();

            try
            {

                foreach (DataRow dr in Master.getDataVehicleDetail(id.Replace(" ", string.Empty)).Rows)
                {

                    cVd.vehicle_id = dr[0].ToString().Trim();
                    cVd.vehicle_name = dr[1].ToString().Trim();
                    cVd.axis_type = dr[2].ToString().Trim();
                    cVd.axis_type_name = dr[3].ToString().Trim();
                    cVd.status_id = dr[4].ToString().Trim();
                    cVd.vehicle_mileage = dr[5].ToString().Trim();
                    cVd.setup_no = dr[6].ToString().Trim();
                    cVd.register_no = dr[7].ToString().Trim();
                    cVd.register_date = dr[8].ToString().Trim();
                    cVd.rfid = dr[9].ToString().Trim();
                    cVd.tire_axis_setup_no = dr[10].ToString().Trim();
                    cVd.tire_id = dr[11].ToString().Trim();


                    List<cVehicle> Lvehicle = new List<cVehicle>();
                    foreach (DataRow drx in Master.getDataListVehicleTrailer(cVd.setup_no).Rows)
                    {
                        cVehicle cV = new cVehicle();
                        cV.vehicle_id = drx[0].ToString().Trim();
                        cV.vehicle_name = drx[1].ToString().Trim();
                        cV.rfid = drx[2].ToString().Trim();
                        cV.setup_no = drx[3].ToString().Trim();
                        cV.sort = drx[4].ToString().Trim();
                        cV.axis_type = drx[5].ToString().Trim();
                        cV.axis_type_name = drx[6].ToString().Trim();

                        Lvehicle.Add(cV);
                    }

                    cVd.vehicle = Lvehicle;

                }
                return Json((new { records = 1, data = cVd }));
            }
            catch (Exception e)
            {
                return Json((new { records = 0, data = "", msg = e.Message.ToString() }));
            }


        }
        [HttpGet]
        [Route("api/Master/getTyreType")]
        public IHttpActionResult getTyreType()
        {

            string StrQuery = @"select tire_type_id,tire_type_name from TIRE_TYPE where is_delete = 0" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpPost]
        [Route("api/Master/setTyreType")]
        public IHttpActionResult setTyreType([FromBody]Master.cCrudTyreType prm)
        {
            var returnRes = Master.InsertTyreType(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteTyreType")]
        public IHttpActionResult deleteTyreType([FromBody]Master.cCrudTyreType prm)
        {
            var returnRes = Master.deleteTyreType(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getTireOutboundConsignment/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getTireOutboundConsignment(string date_start, string date_end, string userid)
        {
            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                dt = Convert.ToDateTime(date_start);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }

            DateTime dt2 = DateTime.Now;
            string dtSTring2 = dt2.ToString("yyyy-MM-dd");
            if (date_end != "0" && date_end != "-" && date_end != "")
            {
                dt2 = Convert.ToDateTime(date_end);
                dtSTring2 = dt2.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring2 = "0";
            }
            string StrQuery = @"select OTB.outbound_id,OTB.location_id,LOC.location, OTB.date_created from OUTBOUND OTB
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB.location_id
                                where OTB.is_delete=0 and loc.location_id='A01'
                            " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(OTB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tir e_name" + System.Environment.NewLine;
            if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            {
                StrQuery += "and OTB.userid=@userid" + System.Environment.NewLine;
            }
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end, userid}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #region Master
        [HttpGet]
        [Route("api/Master/getUserId/{user_name}")]
        public IHttpActionResult getUserId(string user_name)
        {
            string user_name_decode = user_name.Replace("-", ".");
            string StrQuery = @"select usr.Id, usr.UserId, usr.Name, zm.Menu_ID, zm.Menu_Name, zm.Description, zm.Url, zm.Url_Icon 
			from AspNetUsers usr 
			inner join ZUSER_ROLES zr on zr.Id = usr.Id
			inner join ZMENU_ROLE zmr on zmr.RoleId = zr.RoleId
			inner join ZMENU zm on zm.Menu_ID=zmr.Menu_ID
			where email=@user_name and zm.Status=1
            order by zm.Menu_ID
            " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { user_name = user_name_decode }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data master List Item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getListItem/{brand_tire_id}/{tire_code}")]
        public IHttpActionResult getListItem(string brand_tire_id, string tire_code)
        {
            string param = "";
            string StrQuery = @"select i.tire_code,i.tire_name,
                            i.size,i.pattern,i.brand_tire_id,bt.brand_tire_name as brand_name,i.supplier_id,i.price,i.img_name,total_store=dbo.[GetCountTireInStorePerTireCode](i.tire_code), 
                            i.tire_type_id, ty.tire_type_name,
                            isnull(i.OTD,0) as otd, isnull(i.load_rating_single,0) as lr_single, isnull(i.load_rating_double,0) as lr_double, isnull(i.play_rating,0) as play_rating, isnull(i.standard_pressure_single,0) as sp_single, isnull(i.standard_pressure_double,0) as sp_double, isnull(i.unique_code,i.tire_code +' '+ i.size +' '+ i.pattern) as unique_code,
                            total_installed=dbo.[GetCountTireInstalledPerTireCode](i.tire_code),total_item=dbo.[GetCountTirePerTireCode](i.tire_code),
							s.supplier_name
							from ITEM i
                            inner join BRAND_TIRE bt on bt.brand_tire_id=i.brand_tire_id
							inner join SUPPLIER s on s.supplier_id = i.supplier_id
							inner join TIRE_TYPE ty on ty.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            StrQuery += "where i.is_delete=0 and i.tire_code not in ('XXX')" + System.Environment.NewLine;
            if (brand_tire_id != "" && brand_tire_id != "-" && brand_tire_id != "0")
            {
                param = "and i.brand_tire_id=@brand_tire_id" + System.Environment.NewLine;
            }
            string rep_tire_code = tire_code.Replace("-", ".");
            if (tire_code != "" && tire_code != "-" && tire_code != "0")
            {
                if (param != "")
                {
                    param += "and i.tire_code=@tire_code" + System.Environment.NewLine;
                }
                else
                {
                    param = "where i.tire_code=@tire_code" + System.Environment.NewLine;
                }

            }
            StrQuery += param;
            // StrQuery += "group by i.tire_code,i.tire_name,i.size,i.pattern,i.brand_id,b.brand_name,i.supplier_id,i.price,i.img_name" + System.Environment.NewLine;
            StrQuery += "order by i.date_created desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { brand_tire_id = brand_tire_id, tire_code = rep_tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        // GET api/documentation
        /// <summary>
        /// Fungsi Get data master List detail tire per item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getListItemDetail/{tire_code}")]
        public IHttpActionResult getListItemDetail(string tire_code)
        {
            string StrQuery = @"select sort=row_number() over (order by t.tire_id ), t.tire_id ,i.tire_name,i.size,i.pattern,i.price,
                                                                tire_mileage=t.km,t.rfid,
                                                                t.location_id, l.location, tr.process_id, p.process,
                                                                 i.supplier_id, s.supplier_name, tu.vehicle_id, ta.axis_id, t.status_id, a.axis_name
                                                                from TIRE t
                                                                inner join ITEM i on t.tire_code=i.tire_code
                                                                left join LOCATION l on t.location_id=l.location_id
                                                                left join  tire_unit tu on tu.tire_id = t.tire_id
                                                                left join tire_axis ta on ta.vehicle_id= tu.vehicle_id and ta.tire_id=tu.tire_id 
                                                                left join tracking tr on tr.tire_id= t.tire_id and tr.location_id = t.location_id and tr.vehicle_id = tu.vehicle_id and tr.axis_id = ta.axis_id
                                                                left join PROCESS p on tr.process_id=p.process_id
                                                                left join SUPPLIER s on i.supplier_id=s.supplier_id
                                                                left join axis a on tr.axis_id=a.axis_id" + System.Environment.NewLine;
            StrQuery += "where t.tire_id<>'0' and t.is_delete=0 " + System.Environment.NewLine;
            string rep_tire_code = tire_code.Replace("-", ".");
            if (tire_code != "" && tire_code != "-" && tire_code != "0")
            {
                StrQuery += "and t.tire_code=@tire_code" + System.Environment.NewLine;

            }
            StrQuery += "order by t.tire_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tire_code = rep_tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi set Setup tire/Item
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Master/setSetupTire/{is_new}")]
        public IHttpActionResult setSetupTire(Boolean is_new, [FromBody]Master.cCrudSetupTire prm)
        {
            var returnRes = Master.InsertSetupTire(is_new, prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/setAxis")]
        public IHttpActionResult setAxis([FromBody]Master.cCrudAxis prm)
        {
            var returnRes = Master.InsertAxis(prm);
            return Json(returnRes);

        }
        //[HttpPost]
        //[Route("api/Master/deleteAxis")]
        //public IHttpActionResult deleteAxis([FromBody]Master.cCrudAxis prm)
        //{
        //    var returnRes = Master.DeleteAxis(prm);
        //    return Json(returnRes);

        //}
        //[HttpPost]
        //[Route("api/Master/execScriptSql")]
        //public IHttpActionResult execScriptSql([FromBody]Master.cCrudScriptSql prm)
        //{
        //    var returnRes = Master.ScriptSql(prm);
        //    return Json(returnRes);

        //}
        [HttpPost]
        [Route("api/Master/setAxisType")]
        public IHttpActionResult setAxisType([FromBody]Master.cCrudAxisType prm)
        {
            var returnRes = Master.InsertAxisType(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteAxisType")]
        public IHttpActionResult deleteAxisType([FromBody]Master.cCrudAxisType prm)
        {
            var returnRes = Master.DeleteAxisType(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setLocation")]
        public IHttpActionResult setLocation([FromBody]Master.cCrudLocation prm)
        {
            var returnRes = Master.InsertLocation(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteLocation")]
        public IHttpActionResult deleteLocation([FromBody]Master.cCrudLocation prm)
        {
            var returnRes = Master.DeleteLocation(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/setBrandTire")]
        public IHttpActionResult setBrandTire([FromBody]Master.cCrudBrandTire prm)
        {
            var returnRes = Master.InsertBrandTire(prm);
            return Json(returnRes);

        }



        [HttpPost]
        [Route("api/Master/deleteBrandTire")]
        public IHttpActionResult DeleteBrandTire([FromBody]Master.cCrudBrandTire prm)
        {
            var returnRes = Master.DeleteBrandTire(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setSupplier")]
        public IHttpActionResult setSupplier([FromBody]Master.cCrudSupplier prm)
        {
            var returnRes = Master.InsertSupplier(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getSysSetting")]
        public IHttpActionResult getSysSetting()
        {
            string StrQuery = @"select * from sys_settings
            " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]
        [Route("api/Master/setSysSetting")]
        public IHttpActionResult setSysSetting([FromBody]Master.cCrudSysSetting prm)
        {
            var returnRes = Master.InsertSysSetting(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteSysSetting")]
        public IHttpActionResult deleteSysSetting([FromBody]Master.cCrudSysSetting prm)
        {
            var returnRes = Master.DeleteSysSetting(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/setAreaDamage")]
        public IHttpActionResult setAreaDamage([FromBody]Master.cCrudAreaDamage prm)
        {
            var returnRes = Master.InsertAreaDamage(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteAreaDamage")]
        public IHttpActionResult deleteAreaDamage([FromBody]Master.cCrudAreaDamage prm)
        {
            var returnRes = Master.DeleteAreaDamage(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setReason")]
        public IHttpActionResult setReason([FromBody]Master.cCrudReason prm)
        {
            var returnRes = Master.InsertReason(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteReason")]
        public IHttpActionResult deleteReason([FromBody]Master.cCrudReason prm)
        {
            var returnRes = Master.DeleteReason(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/setEmployee")]
        public IHttpActionResult setEmployee([FromBody]Master.cCrudEmployee prm)
        {
            var returnRes = Master.InsertEmployee(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setDepartment")]
        public IHttpActionResult setDepartment([FromBody]Master.cCrudDepartment prm)
        {
            var returnRes = Master.InsertDepartment(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteDepartment")]
        public IHttpActionResult DeleteDepartment([FromBody]Master.cCrudDepartment prm)
        {
            var returnRes = Master.DeleteDepartment(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getDepartment")]
        public IHttpActionResult getDepartment()
        {
            string StrQuery = @"select * from department where is_delete=0
            " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpPost]
        [Route("api/Master/setGroups")]
        public IHttpActionResult setGroups([FromBody]Master.cCrudGroups prm)
        {
            var returnRes = Master.InsertGroups(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteGroups")]
        public IHttpActionResult DeleteGroups([FromBody]Master.cCrudGroups prm)
        {
            var returnRes = Master.DeleteGroups(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getGroups")]
        public IHttpActionResult getGroups()
        {
            string StrQuery = @"select * from groups where is_delete=0
            " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getUserEmployees")]
        public IHttpActionResult getUserEmployees()
        {

            string StrQuery = @"select emp.emp_id,emp_name,emp_address,emp_pos_code,emp_phone1, emp_phone2, emp_email, emp_is_active from USER_EMPLOYEES ue
                            inner join AspNetUsers au on au.UserId = ue.userid
                            inner join EMPLOYEES emp on emp.emp_id = ue.emp_id
                            where emp.emp_is_active=1 and emp.is_delete=0" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]
        [Route("api/Master/setUserEmployee")]
        public IHttpActionResult setUserEmployee([FromBody]Master.cCrudUsrEmployeeInsert prm)
        {
            var returnRes = Master.InsertUserEmployee(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteUserEmployee")]
        public IHttpActionResult deleteUserEmployee([FromBody]Master.cCrudUsrEmployeeDelete prm)
        {
            var returnRes = Master.DeleteUserEmployee(prm);
            return Json(returnRes);

        }


        [HttpGet]
        [Route("api/Master/getPositions")]
        public IHttpActionResult getPositions()
        {

            string StrQuery = @"select position_id, position_name from POSITIONS where is_delete=0" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getPositionsFilter/{position}")]
        public IHttpActionResult getPositionsFilter(string position)
        {

            string StrQuery = @"select position_id, position_name, value=position_id, label=position_name from POSITIONS where is_delete=0 and (position_id like @position or position_name like @position)" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { position = position + "%"}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]
        [Route("api/Master/setPositions")]
        public IHttpActionResult setPositions([FromBody]Master.cCrudPosition prm)
        {
            var returnRes = Master.InsertPosition(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deletePositions")]
        public IHttpActionResult deletePositions([FromBody]Master.cCrudPosition prm)
        {
            var returnRes = Master.DeletePosition(prm);
            return Json(returnRes);

        }

        [HttpGet]
        [Route("api/Master/getScheduleByUser/{userid}")]
        public IHttpActionResult getScheduleByUser(string userid)
        {
            string[] data_ = Process.getDataScheduleByUser(userid);
            var lastDayOfMonth = DateTime.DaysInMonth(2024, 02);
            string StrQuery = @" select @year, @month, @date, @location_id, @group_id, @shift_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year= data_[0], month = data_[1], date = data_[2], location_id = data_[3], group_id = data_[4], shift_id = data_[5], }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTeamSchedule")]
        public IHttpActionResult getTeamSchedule()
        {

            string StrQuery = @"

							select tsch_id, loc.location_id,loc.location , loc.location + '- Shift ' + cast(shift as varchar) +' - '+ 'From '+ cast(FORMAT(date_start, 'dd-MMMM-yyyy') as varchar)+' to '+ cast(FORMAT(date_end, 'dd-MMMM-yyyy') as varchar) as caption, shift, date_start, date_end from TEAM_SCHEDULE ts
                        inner join LOCATION loc on loc.location_id = ts.location_id where ts.is_delete=0" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTeamScheduleFilter/{tsch_id}")]
        public IHttpActionResult getTeamScheduleFilter(string tsch_id)
        {

            string StrQuery = @"select label=tsch_id, value=tsch_id, loc.location_id, loc.location, shift, date_start, date_end from TEAM_SCHEDULE ts
                        inner join LOCATION loc on loc.location_id = ts.location_id where ts.is_delete=0 and ts.tsch_id like @tsch_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tsch_id = tsch_id + "%"}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]
        [Route("api/Master/setTeamSchedule")]
        public IHttpActionResult setTeamSchedule([FromBody]Master.cCrudTeamSchedule prm)
        {
            var returnRes = Master.InsertTeamSchedule(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getShift")]
        public IHttpActionResult getShift()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select shift_id, shift from shift where Is_Delete = 0
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/checkSchedule/{year}/{month}/{date}/{location_id}/{shift_id}/{group_id}")]
        public IHttpActionResult checkSchedule(string year, string month, string date, string location_id, string shift_id, string group_id)
        {
            try
            {
                int cek = Master.cekDataSchedule(year, month, date, location_id, shift_id, group_id);
                return Json((new { records = cek }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getGroupsFilter/{location_id}")]
        public IHttpActionResult getGroupsFilter(string location_id)
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select * from GROUPS where location_id = @location_id and is_delete = 0
                            ";
                var data = dbConn.Query(query, new { location_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getGroupEmployee/{userid}")]//{year}/{month}/
        public IHttpActionResult getGroupEmployee( string userid)//string year, string month,
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                string roleUser = Report.getRoleUser(userid);
                String query = @"select ge.group_id, g.group_name , g.location_id, l.location
                                from GROUPS_EMPLOYEES ge
                                inner join GROUPS g on g.group_id = ge.group_id
                                inner join LOCATION l on l.location_id=g.location_id
                                where ge.is_delete=0

                            ";
                if (roleUser != "0" && roleUser != "-" && roleUser != "")
                {
                    query += "and ge.userid=@userid" + System.Environment.NewLine;
                }
                query += @" group by  ge.group_id, g.group_name, g.location_id, l.location
                                order by  ge.group_id";
                var data = dbConn.Query(query, new {  userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getGroupEmployeeDetail/{group_id}")]//{year}/{month}/
        public IHttpActionResult getGroupEmployeeDetail( string group_id)//string year, string month,
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"
                                select ge.group_id, g.group_name, g.location_id, l.location, ge.emp_id, e.emp_name, ge.position_id, p.position_name
                                from GROUPS_EMPLOYEES ge
                                inner join EMPLOYEES e on e.emp_id = ge.emp_id
                                inner join POSITIONS p on p.position_id = ge.position_id
                                inner join GROUPS g on g.group_id = ge.group_id
                                inner join LOCATION l on l.location_id=g.location_id
                                where   ge.group_id=@group_id and ge.is_delete=0
                                order by ge.group_id,p.position_id
                            ";
                var data = dbConn.Query(query, new { group_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]
        [Route("api/Master/setGroupEmployee")]
        public IHttpActionResult setGroupEmployee([FromBody]Master.cCrudGroupEmployee prm)
        {
            var returnRes = Master.InsertGroupEmployee(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteGroupEmployee")]
        public IHttpActionResult deleteGroupEmployee([FromBody]Master.cCrudGroupEmployee prm)
        {
            var returnRes = Master.DeleteGroupEmployee(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setSchedule")]
        public IHttpActionResult setSchedule([FromBody]Master.cCrudSchedule prm)
        {
            var returnRes = Master.InsertSchedule(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteSchedule")]
        public IHttpActionResult deleteSchedule([FromBody]Master.cCrudSchedule prm)
        {
            var returnRes = Master.DeleteSchedule(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteTeamSchedule")]
        public IHttpActionResult deleteTeamSchedule([FromBody]Master.cCrudTeamSchedule prm)
        {
            var returnRes = Master.DeleteTeamSchedule(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("api/Master/getListUserApp")]
        public IHttpActionResult getListUserApp()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select au.UserId, au.Email, au.Name from AspNetUsers au where IsDelete = 0 and IsActive = 1
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getListUserEmployee")]
        public IHttpActionResult getListUserEmployee()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select ue.emp_id ,ue.userid,em.emp_name,emp_address,emp_email from USER_EMPLOYEES ue 
                                 inner join EMPLOYEES em on ue.emp_id = em.emp_id
                                 where ue.is_delete = 0
                               ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTeamScheduleEmployees")]
        public IHttpActionResult getTeamScheduleEmployees()
        {

            string StrQuery = @"
                                select te.tsch_id, emp.emp_id, emp.emp_name, p.position_id, p.position_name from TSCH_EMPLOYEES te
                                inner join EMPLOYEES emp on emp.emp_id = te.emp_id
                                inner join positions p on p.position_id=te.position_id
                                where te.is_delete = 0 " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpPost]

        
        [Route("api/Master/setTeamScheduleEmployees")]
        public IHttpActionResult setTeamScheduleEmployees([FromBody]Master.cCrudTeamSchEmpHeader prm)
        {
            var returnRes = Master.InsertTeamSchEmp(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteTeamScheduleEmployees")]
        public IHttpActionResult deleteTeamScheduleEmployees([FromBody]Master.cCrudTeamSchEmp prm)
        {
            var returnRes = Master.DeleteTeamSchEmp(prm);
            return Json(returnRes);

        }

        [HttpGet]
        [Route("api/Master/getEmployees")]
        public IHttpActionResult getEmployees()
        {

            string StrQuery = @"select emp_id, d.department_id, d.department_name,emp_name,emp_address,emp_pos_code,emp_phone, emp_email, emp_is_active from EMPLOYEES em
						inner join DEPARTMENT d on d.department_id = em.department_id
						where em.IS_DELETE=0" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpPost]
        [Route("api/Master/deleteEmployee")]
        public IHttpActionResult deleteReason([FromBody]Master.cCrudEmployee prm)
        {
            var returnRes = Master.DeleteEmployee(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/deleteSupplier")]
        public IHttpActionResult deleteSupplier([FromBody]Master.cCrudSupplier prm)
        {
            var returnRes = Master.DeleteSupplier(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("api/Master/setBrandVehicle")]
        public IHttpActionResult setBrandVehicle([FromBody]Master.cCrudBrandVehicle prm)
        {
            var returnRes = Master.InsertBrandVehicle(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/deleteBrandVehicle")]
        public IHttpActionResult DeleteBrandVehicle([FromBody]Master.cCrudBrandVehicle prm)
        {
            var returnRes = Master.DeleteBrandVehicle(prm);
            return Json(returnRes);

        }
        private string GetClientIp(HttpRequestMessage request = null)
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        [HttpPost]
        [Route("api/Master/setUserLog/{Id}/{activity}")]
        public IHttpActionResult setUserLog(string Id, string activity)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string ipaddress = GetClientIp();
            string res = "";
            Boolean isSuccess = false;
            int RowAffected = 0;

            if (Id != "dd0beadb-1aba-49e3-938e-56704dc5e74a" || Id != "54de9f5f-be7c-456b-9fb2-4d4535ad4eaa" || Id != "5bee9379-4a8f-4be1-9cd6-174f8b8bd1a1" || Id != "3a9a679a-ddcc-4ae6-bf9d-321d978aa139" || Id != "277f903b-de07-46e1-97c3-4e010305099a")
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"INSERT [dbo].[ZLOG_USER] ([date], [time], [Id], [activity], [ip_address])
                            VALUES (@date, @time, @Id, @activity, @ipaddress)";
                            //('" + date + "','" + date + " " + time.Replace('.', ':') + "','" + Id + "','" + activity + "','" + ipaddress + "')

                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@time", date + " " + time.Replace('.', ':'));
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.Parameters.AddWithValue("@activity", activity);
                        cmd.Parameters.AddWithValue("@ipaddress", ipaddress);



                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            isSuccess = true;

                        }
                        catch (SqlException e)
                        {
                            isSuccess = false;
                            res = e.Message.ToString();
                        }

                    }
                }
            }
            //var returnRes = Master.InsertSetupTire(is_new, prm);
            //return Json(returnRes);
            return Json((new { success = isSuccess, result = res }));
        }

        [HttpGet]
        [Route("api/Master/getLogUser/{StartDate}/{EndDate}")]
        public IHttpActionResult getLogUser(string StartDate, string EndDate)
        {
            string dtSTring = "";
            string dtSTring2 = "";
            if (StartDate != "0")
            {
                DateTime dt = Convert.ToDateTime(StartDate);
                dtSTring = dt.ToString("MM-dd-yyyy");
                DateTime dt2 = Convert.ToDateTime(EndDate);
                dtSTring2 = dt2.ToString("MM-dd-yyyy");
            }
            else
            {
                dtSTring = "0";
                dtSTring2 = "0";
            }
            return Json((new { records = Master.getDataLogUser(dtSTring, dtSTring2).Rows.Count, data = Master.getDataLogUser(dtSTring, dtSTring2) }));

        }
        // PUT api/documentation
        /// <summary>
        /// Fungsi set Setup tire/Item
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Route("api/Master/DelTire/{is_new}")]
        public IHttpActionResult delTire([FromBody]Master.cCrudSetupTire prm)
        {
            var returnRes = Master.DeleteTireItem(prm);
            return Json(returnRes);

        }
        [HttpPost]
        //[Route("api/Master/DelTire/{is_new}")]
        public IHttpActionResult deleteTirePerSerial([FromBody]Master.cCrudTireOnly prm)
        {
            var returnRes = Master.DeleteTirePerSerial(prm);
            return Json(returnRes);

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data master List Vehicle
        /// </summary>
        /// <returns></returns>
        [HttpGet]// menampilkan fleet 
        [Route("api/Master/getListFleet")]
        public IHttpActionResult getListFleet()
        {
            string StrQuery = @"select fv.fleet_id, v.vehicle_id,vehicle_name,fv.date_created,axis_type,rfid = vehicle_rfid,km,hm,position
                                from FLEET_VEHICLE FV
                                inner join FLEET F ON FV.fleet_id = F.fleet_id
                                INNER JOIN VEHICLE V on V.vehicle_id = fv.vehicle_id
                                where V.vehicle_id!='0' and  fv.fleet_id= v.vehicle_id and fv.is_delete=0 order by fv.fleet_id, position" + System.Environment.NewLine;
          
          

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }


        }


        // GET api/documentation
        /// <summary>
        /// Fungsi Get data master List detail Vehicle
        /// </summary>
        /// <returns></returns>
        /// //ini belum
        [HttpGet]
        [Route("api/Master/getListVehicleDetail/{setup_no}")]
        public IHttpActionResult getListVehicleDetail(string setup_no)
        {
            string StrQuery = "select v.vehicle_id,v.vehicle_name,v.date_created,v.vehicle_no,v.axis_type,ax.axis_type_name," + System.Environment.NewLine;
            StrQuery += "v.rfid,v.vehicle_mileage,v.is_active,v.is_vehicle,vt.setup_no,vt.date_created,vt.description,vt.sort," + System.Environment.NewLine;
            StrQuery += "vt.userid,tire_qty=[dbo].[GetCountTireInstalledPerVehicle](v.vehicle_id)" + System.Environment.NewLine;
            StrQuery += "from VEHICLE_TRAILER vt" + System.Environment.NewLine;
            StrQuery += "inner join VEHICLE v on vt.vehicle_id=v.vehicle_id" + System.Environment.NewLine;
            StrQuery += "inner join AXIS_TYPE ax on v.axis_type=ax.axis_type" + System.Environment.NewLine;
            StrQuery += "where v.is_vehicle = 'false'" + System.Environment.NewLine;

            if (setup_no != "" && setup_no != "-" && setup_no != "0")
            {
                StrQuery += "and vt.setup_no=@setup_no" + System.Environment.NewLine;

            }
            StrQuery += "order by vt.sort" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { setup_no = setup_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #endregion

        #region vehicle trailer
        // PUT api/documentation
        /// <summary>
        /// Fungsi set tire mileage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setVehicleTrailer([FromBody]List<vehicleTrailer> prm)
        {
            var returnRes = Master.InsertVehicleTrailer(prm);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi set tire mileage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult setVehicle([FromBody]Vehicle prm)
        {
            var returnRes = Master.InsertVehicle(prm);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi delete vehicle trailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Master/delVehicleTrailer/{vehicle_id}/{setup_no}")]
        public IHttpActionResult delVehicleTrailer(string vehicle_id, string setup_no)
        {
            var returnRes = Master.deleteVehicleTrailer(vehicle_id, setup_no);
            return Json(returnRes);

        }

        // PUT api/documentation
        /// <summary>
        /// Fungsi delete vehicle trailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Master/delVehicleTrailer/{vehicle_id}")]
        public IHttpActionResult delVehicle(string vehicle_id)
        {
            var returnRes = Master.deleteVehicle(vehicle_id);
            return Json(returnRes);

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi get list vehicle mileage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getAvailableTrailer")]
        public IHttpActionResult getAvailableTrailer()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                String query = "SELECT v.vehicle_id, v.vehicle_name, v.vehicle_no, v.axis_type, at.axis_type_name, v.rfid FROM VEHICLE AS v ";
                query += " LEFT JOIN VEHICLE_TRAILER AS vt ON vt.vehicle_id = v.vehicle_id ";
                query += " INNER JOIN AXIS_TYPE AS at ON at.axis_type = v.axis_type ";
                query += " WHERE v.is_vehicle = 'false' ";
                query += " AND vt.vehicle_id IS NULL ";

                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        // GET api/documentation
        /// <summary>
        /// Fungsi get list vehicle mileage
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        [Route("api/Master/getFleetAxis/{fleet_id}")]
        public IHttpActionResult getFleetAxis(string fleet_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                String query = "SELECT a.axis_id, a.description, a.axis_name ";
                query += " FROM AXIS AS a ";
                query += " WHERE a.axis_type in (SELECT axis_type FROM FLEET_VEHICLE WHERE fleet_id = @fleet_id)  and is_delete=0";

                var data = dbConn.Query(query, new { fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getVehicleAxis/{vehicle_id}")]
        public IHttpActionResult getVehicleAxis(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                String query = "SELECT a.axis_id, a.description, a.axis_name ";
                query += " FROM AXIS AS a ";
                query += " WHERE a.axis_type = (SELECT axis_type FROM VEHICLE WHERE vehicle_id = @vehicle_id  and is_delete=0)  and is_delete=0";

                var data = dbConn.Query(query, new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        // GET api/documentation
        /// <summary>
        /// Fungsi get vehicle type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getVehicleType")]
        public IHttpActionResult getVehicleType()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                String query = "select vehicle_type_id, vehicle_type from vehicle_type where is_delete = '0' and vehicle_type_id not in ('0')";

                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        #endregion

        [HttpGet]
        [Route("api/Master/getLookupTyreDetachStatus/{sn?}")]
        public IHttpActionResult getLookupTyreDetachStatus(string sn = null)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"
									select label=T.tire_id+' - '+case when t.tire_name ='' then t.tire_code else t.tire_name end, value=t.tire_id,case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name, los.letter_loose_no, los.letter_loose_date  from tire t 
									inner join LETTER_LOOSE_DETAIL los on los.tire_id=t.tire_id
									INNER JOIN ITEM I ON I.tire_code = T.tire_code";
                query += " where t.tire_id not in (select tire_id from LETTER_FOUND_DETAIL)";
                if (sn != null)
                {
                    query += " and upper(T.tire_id) like upper(@sn) ";
                }
                //query += " and vehicle_id not in (select vehicle_id from FLEET_VEHICLE where upper(vehicle_id) like upper('" + vehicle_id + "%') and is_delete = 0 )";
                query += " group by t.tire_code , t.tire_name ,T.tire_id,i.unique_code,i.tire_name, los.letter_loose_no, los.letter_loose_date order by los.letter_loose_date desc ";
                var data = dbConn.Query(query, new { sn = sn + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getLookupVehicle/{vehicle_id?}")]
        public IHttpActionResult getLookupVehicle(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT vehicle_id as id, vehicle_id +' - '+ vehicle_name as label, vehicle_name,vehicle_rfid, vehicle_id as value, axis_type";
                query += " FROM VEHICLE AS a ";
                query += " WHERE is_delete=0";// and vehicle_id not in(select vehicle_id from REGISTER_VHC)
                if (vehicle_id != null)
                {
                    query += " and upper(vehicle_id) like upper(@vehicle_id) ";
                }
                //query += " and vehicle_id not in (select vehicle_id from FLEET_VEHICLE where upper(vehicle_id) like upper('" + vehicle_id + "%') and is_delete = 0 )";

                var data = dbConn.Query(query, new { vehicle_id = vehicle_id + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getLookupVehicleNotInFleet/{vehicle_id?}")]
        public IHttpActionResult getLookupVehicleNotInFleet(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT vehicle_id as id, vehicle_id +' - '+ vehicle_name as label, vehicle_name,vehicle_rfid, vehicle_id as value, axis_type";
                query += " FROM VEHICLE AS a ";
                query += " WHERE is_delete=0  and vehicle_id not in (select vehicle_id from FLEET_VEHICLE where is_delete=0) and vehicle_id not in ('XXX') ";
                if (vehicle_id != null)
                {
                    query += " and upper(vehicle_id) like upper(@vehicle_id) ";
                }
                //query += " and vehicle_id not in (select vehicle_id from FLEET_VEHICLE where upper(vehicle_id) like upper('" + vehicle_id + "%') and is_delete = 0 )";

                var data = dbConn.Query(query, new { vehicle_id = vehicle_id + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireByAxisVhc/{vehicle_id?}/{axis_id?}")]
        public IHttpActionResult getTireByAxisVhc(string vehicle_id, string axis_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT ta.tire_id";
                query += " FROM tire_axis AS ta ";
                query += " inner join tire t on ta.tire_id=t.tire_id";
                query += " WHERE ta.is_delete=0";
                if (vehicle_id != null)
                {
                    query += " and upper(vehicle_id) = upper(@vehicle_id) and  upper(ta.axis_id) = upper(@axis_id)  ";
                }

                var data = dbConn.Query(query, new { vehicle_id = vehicle_id, axis_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getAxisByVhc/{vehicle_id?}/{axis_id?}")]
        public IHttpActionResult getAxisByVhc(string vehicle_id, string axis_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT ta.axis_id as id, ta.axis_id as label, ta.axis_id as value";
                query += " FROM tire_axis AS ta ";
                query += " WHERE ta.is_delete=0";
                if (vehicle_id != null)
                {
                    query += " and upper(vehicle_id) = upper(@vehicle_id) and  upper(ta.axis_id) like upper(@axis_id)  ";
                }

                var data = dbConn.Query(query, new { vehicle_id = vehicle_id, axis_id = axis_id  + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #region vehicle mileage
        // GET api/documentation
        /// <summary>
        /// Fungsi get list vehicle mileage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getLastVehicleMileage/{vehicle_id?}")]
        public IHttpActionResult getLastVehicleMileage(string vehicle_id = null)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                //String query = "SELECT distinct v.vehicle_id, v.vehicle_name, v.vehicle_mileage, ";
                //query += " (SELECT TOP 1 date_created FROM TIRE_MILEAGE_HISTORY WHERE vehicle_id = v.vehicle_id ORDER BY date_created DESC) as date_created, ";
                //query += " (SELECT TOP 1 location FROM LOCATION WHERE location_id = (SELECT TOP 1 location_id FROM TIRE_MILEAGE_HISTORY WHERE vehicle_id = v.vehicle_id ORDER BY date_created DESC)) as location ";
                //query += " FROM VEHICLE AS v   ";
                //query += " WHERE v.is_vehicle = 'true' ";
                //if (vehicle_id != null)
                //{
                //    query += " AND v.vehicle_id = '" + vehicle_id + "' OR v.rfid = '" + vehicle_id + "'";
                //}
                String query = @"
                                SELECT distinct v.fleet_id, v.fleet_name, v.km,  v.hm,
                                isnull((SELECT TOP 1 location FROM LOCATION WHERE location_id = 
	                                (SELECT TOP 1 location_id FROM TIRE_MILEAGE_HISTORY WHERE tire_id = t.tire_id ORDER BY date_created DESC)
                                ),0) as location  
                                FROM FLEET AS v   
                                INNER JOIN FLEET_VEHICLE FV ON FV.fleet_id = v.fleet_id 
                                INNER JOIN VEHICLE VHC on VHC.vehicle_id=fv.vehicle_id
								INNER JOIN TIRE_UNIT TU on TU.vehicle_id = VHC.vehicle_id
								INNER JOIN TIRE T on T.tire_id = TU.tire_id";
                query += " where is_delete='0'";
                if (vehicle_id != null)
                {
                    query += " and (v.fleet_id = @vehicle_id or VHC.vehicle_rfid = @vehicle_id)";
                }
                var data = dbConn.Query(query, new { vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #endregion

        #region tire installation
        // GET api/documentation
        /// <summary>
        /// Fungsi get list axis by vehicle id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getAxisByVehicleNotInstalled/{vehicle_id}")]
        public IHttpActionResult getAxisByVehicleNotInstalled(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(
                    @"SELECT axis_id,axis_name, axis_no, tire_no, description FROM AXIS WHERE axis_type in (SELECT axis_type FROM VEHICLE WHERE vehicle_id = @vehicle_id) and axis_id not in (select axis_id from TIRE_AXIS where vehicle_id=@vehicle_id and is_delete=0)   and is_delete=0 order by axis_no, tire_no"
                    , new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getAxisByVehicle/{vehicle_id}")]
        public IHttpActionResult getAxisByVehicle(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(
                    @"SELECT axis_id,axis_name, axis_no, tire_no, description FROM AXIS WHERE axis_type in (SELECT axis_type FROM VEHICLE WHERE vehicle_id = @vehicle_id)  and is_delete=0 order by axis_no, tire_no"
                    , new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getAxis/{axis_type}/{tire_no}/{axis_no}")]
        public IHttpActionResult getAxis(string axis_type, string tire_no, string axis_no)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(
                    @"select axis_id from axis where axis_type = @axis_type and tire_no = @tire_no and axis_no=@axis_no  and is_delete=0"
                    , new { axis_type = axis_type, tire_no = tire_no, axis_no = axis_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getAxisByFleet/{fleet_id}")]
        public IHttpActionResult getAxisByFleet(string fleet_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(
                    @"SELECT axis_id,axis_name, axis_no, tire_no, description FROM AXIS WHERE axis_type in (SELECT axis_type FROM VEHICLE V INNER JOIN FLEET_VEHICLE FV ON FV.vehicle_id = V.vehicle_id WHERE fleet_id = @fleet_id and fv.is_delete=0) and is_delete=0"
                    , new { fleet_id = fleet_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // GET api/documentation
        /// <summary>
        /// Fungsi get list axis by vehicle id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getTireByVehicle/{vehicle_id}")]
        public IHttpActionResult getTireByVehicle(string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"SELECT t.tire_id as value, t.tire_code, label=t.tire_id+' - '+case when t.tire_name ='' then t.tire_code else t.tire_name end, case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
			                LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
                            WHERE 
                            tu.vehicle_id  like @vehicle_id ";
                query += " and tu.is_delete=0";
                var data = dbConn.Query(query, new { vehicle_id = vehicle_id + "%"}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireDismantled/{sn}")]
        public IHttpActionResult getTireDismantled(string sn)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"SELECT t.tire_id as value, t.tire_code, label=t.tire_id+' - '+case when t.tire_name ='' then t.tire_code else t.tire_name end, case when t.tire_name ='' then t.tire_code else t.tire_name end as tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
			                LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
                            WHERE 
                            t.tire_id  like @sn ";
                query += " and t.tire_id in (select tire_id from tire_unit where is_delete=1) and t.is_delete=0";
                var data = dbConn.Query(query, new { sn = sn +"%"}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireForDisposal/{sn}")]
        public IHttpActionResult getTireForDisposal(string sn)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"
							SELECT t.tire_id as value, t.tire_code, label=t.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, t.tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
							inner join SCRAP_TIRE sd on sd.tire_id = t.tire_id
                            WHERE 
                            t.tire_id  like @sn and t.tire_id not in(select tire_id from DISPOSAL_DETAIL) ";
                query += " and t.is_delete=0";
                var data = dbConn.Query(query, new { sn = sn + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireForOutbound/{sn}/{is_send_to_workshop}")]
        public IHttpActionResult getTireForOutbound(string sn, string is_send_to_workshop)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"SELECT t.tire_id as value, t.tire_code, label=t.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, t.tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
							inner join REGISTER_TIRE reg on reg.tire_id = t.tire_id
			                LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and  tu.is_delete=0
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
                            WHERE 
                            t.tire_id  like @sn and t.location_id not in ('OV00') and t.tire_id not in (select tire_id from SCRAP_TIRE)  and t.status_id not in ('7')
                             
                            ";//and t.tire_id not in (select tire_id from outbound_detail where is_active=0)  and t.tire_id not in (select tire_id from LETTER_DISMANTLE_DETAIL where is_closed=0 or (is_closed=1 and reff_tire_id<>tire_id))    
                if (is_send_to_workshop == "1")
                {
                    query += " and t.status_id='3'";
                }
                else {
                    query += " and t.status_id<>'3'";
                }
                query += @" 
                            and (t.is_delete=0  or tu.is_delete=0)";
                var data = dbConn.Query(query, new { sn = sn + "%"}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTireForOutboundConsWH/{sn}/{is_send_to_workshop}")]
        public IHttpActionResult getTireForOutboundConsWH(string sn, string is_send_to_workshop)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"SELECT t.tire_id as value, t.tire_code, label=t.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, t.tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
			                LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and  tu.is_delete=0
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
                            WHERE 
                            t.tire_id  like @sn and t.tire_id in (select tire_id from CONSIGNMENT_RECEIVE_DETAIL) and t.tire_id not in (select tire_id from OUTBOUND_DETAIL)";
                if (is_send_to_workshop == "1")
                {
                    query += " and t.status_id='3'";
                }
                else
                {
                    query += " and t.status_id<>'3'";
                }
                query += @" 
                            and (t.is_delete=0  or tu.is_delete=0) and t.status_id='0' and tu.vehicle_id is null";
                var data = dbConn.Query(query, new { sn = sn + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireStatus")]
        public IHttpActionResult getTireStatus()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"select * from status where status_id not in ('0','6','7','8') and is_delete=0";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireNotInstalled")]
        public IHttpActionResult getTireNotInstalled()
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"SELECT t.tire_id, t.tire_code, t.tire_name, t.serial_number, t.size, t.pattern, i.brand_tire_id, b.brand_tire_name, i.supplier_id, s.supplier_name, t.rfid
                            FROM tire AS t
			                LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id
			                INNER JOIN ITEM I ON T.tire_code = i.tire_code
                            INNER JOIN BRAND_TIRE AS b ON b.brand_tire_id = i.brand_tire_id
                            INNER JOIN SUPPLIER AS s ON s.supplier_id = i.supplier_id
                            WHERE t.tire_id not in (select tire_id from tire_unit) and (t.is_delete=0   or tu.is_delete=0)";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getTireNotRegistered/{sn}")]
        public IHttpActionResult getTireNotRegistered(string sn)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"
                        select t.tire_id, t.tire_name, value=tire_id, label=tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code  from tire AS t
                        INNER JOIN ITEM I ON T.tire_code = i.tire_code
                        where ";
                query += @"   t.tire_id like @sn and ";
                query += @" t.tire_id not in (select t.tire_id from tire t
                        inner join register_tire r on r.tire_id = t.tire_id)
                        and t.is_delete=0";
                var data = dbConn.Query(query, new { sn = sn + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }


        [HttpGet]
        [Route("api/Master/validationEPC/{sn}/{tag_type_id}")]
        public IHttpActionResult validationEPC(string sn, string tag_type_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = "";
                if (tag_type_id == "00")
                {
                    query = @"
                        select * from epc_tire where tire_id=@sn";
                }
                else {
                    query = @"
                        select * from epc_vhc where vehicle_id=@sn";
                }
                var data = dbConn.Query(query, new { sn }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getTireWorkshopBeforeScrap/{sn}")]
        public IHttpActionResult getTireWorkshopBeforeScrap(string sn)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var query = @"
                        select t.tire_id, i.tire_name, t.tire_id as value, label=t.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code from tire t
                        INNER JOIN ITEM I ON T.tire_code = i.tire_code
                        where t.location_id = 'A00S001' and status_id not in('1','4')";
                query += @"  and  t.tire_id like @sn and  t.tire_id not in(select tire_id from SCRAP_TIRE) and t.tire_id not in(select tire_id from DISPOSAL_DETAIL)";
                //query += @"     group by t.tire_id, i.tire_name
                //        ";
                var data = dbConn.Query(query, new { sn = sn + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        // PUT api/documentation
        /// <summary>
        /// Fungsi set tire mileage
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Master/setTireInstallation")]
        public IHttpActionResult setTireInstallation([FromBody]TireInstallation prm)
        {
            var returnRes = Master.InsertTireInstallation(prm);
            return Json(returnRes);

        }

        [HttpPost]
        [Route("api/Master/setTireDismantle")]
        public IHttpActionResult setTireDismantle([FromBody]TireDismantle prm)
        {
            var returnRes = Master.InsertTireDismantle(prm);
            return Json(returnRes);

        }
        #endregion

        #region Setup Single Vehicle
        [HttpPost]
        [Route("api/Master/setSingleVehicle")]
        public IHttpActionResult setSingleVehicle([FromBody]SingleVehicle prm)
        {

            var returnRes = InsertSingleVehicle(prm);
            return Json(returnRes);
        }
        #endregion
        #region Setup Fleet
        [HttpPost]
        [Route("api/Master/setFleet")]
        public IHttpActionResult setFleet([FromBody]cCrudSetupFleet prm)
        {

            var returnRes = InsertFleetProcess(prm);
            return Json(returnRes);
        }

        [HttpPost]
        [Route("api/Master/setFleetFromFds")]
        public IHttpActionResult setFleetFromFds([FromBody]cCrudSetupFleet prm)
        {

            var returnRes = InsertFleetFromFdsProcess(prm);
            return Json(returnRes);
        }
        [HttpGet]
        [Route("api/Master/getFleet")]
        public IHttpActionResult getFleet()
        {
            string strQuery = @"select f.fleet_id, f.location_type_id, lt.location_type, f.fleet_name, ISNULL(f.description,'') as description, f.km, f.hm, f.is_delete, f.date_created, f.date_modified, isnull(f.userid,'') as userid 
                from FLEET f
                inner join location_type lt on lt.location_type_id = f.location_type_id
                where f.is_delete='False'  and f.fleet_id not in('XXX')";
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(strQuery, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }

        [HttpGet]
        [Route("api/Master/getFleetDetail/{fleet_id}")]
        public IHttpActionResult getFleetDetail(string fleet_id)
        {
            string strQuery = "select * from FLEET_VEHICLE where fleet_id = @fleet_id and is_delete='False' order by position";
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(strQuery, new { fleet_id }, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }

        [HttpPost]
        [Route("api/Master/deleteFleet/{fleet_id}/{user_id}")]
        public IHttpActionResult deleteFleet(string fleet_id, string user_id)
        {
            var returnRes = deleteFleetProcess(fleet_id, user_id);
            return Json(returnRes);
        }

        [HttpPost]
        [Route("api/Master/deleteFleetListVehicle/{fleet_id}/{vehicle_id}/{user_id}")]
        public IHttpActionResult deleteFleetListVehicle(string fleet_id, string vehicle_id, string user_id)
        {
            var returnRes = deleteFleetProcessListVehicle(fleet_id, vehicle_id, user_id);
            return Json(returnRes);
        }

        [HttpPost]
        [Route("api/Master/deleteVehicle/{vehicle_id}/{user_id}")]
        public IHttpActionResult deleteVehicle(string vehicle_id, string user_id)
        {
            var returnRes = deleteVehicleProcess(vehicle_id, user_id);
            return Json(returnRes);
        }

        //[HttpPost]
        //[Route("api/Master/updateFleet")]
        //public IHttpActionResult updateFleet([FromBody]cCrudSetupFleet prm)
        //{
        //    var returnRes = updateFleetProcess(prm);
        //    return Json(returnRes);
        //}
        #endregion
        #region service and maintenance
        // GET api/documentation
        /// <summary>
        /// Fungsi Get service and maintenance action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getServiceAndMaintenance")]
        public IHttpActionResult getServiceAndMaintenance()
        {

            try
            {
                string query = @"SELECT s.inspect_setup_id, s.inspect_setup_name, a.service_action_id, sa.service_action_type, sa.service_action_desc
                                FROM INSPECT_SETUP s
                                INNER JOIN INSPECT_ACTION_TYPE a ON s.inspect_setup_id = a.inspect_setup_id
                                INNER JOIN SERVICE_ACTION sa ON a.service_action_id = sa.service_action_id";
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get service and maintenance area damage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getSAMAreaDamage/{inspect_setup_id?}")]
        public IHttpActionResult getSAMAreaDamage(string inspect_setup_id = null)
        {

            try
            {
                string query = @"SELECT a.area_damage_id, a.area_damage_name
                                FROM AREA_DAMAGE a
                                INNER JOIN INSPECT_AREA_DAMAGE ia ON a.area_damage_id = ia.area_damage_id ";
                if (inspect_setup_id != null)
                {
                    query += " WHERE inspect_setup_id = @inspect_setup_id and a.is_delete=0";
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(query, new { inspect_setup_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }


        // GET api/documentation
        /// <summary>
        /// Fungsi Get service and maintenance reason
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getSAMReason/{inspect_setup_id?}/{area_damage_id?}")]
        public IHttpActionResult getSAMReason(string inspect_setup_id = null, string area_damage_id = null)
        {

            try
            {
                string query = @"SELECT r.reason_id, r.reason_name
                                FROM REASON r
                                INNER JOIN INSPECT_REASON ir ON r.reason_id= ir.reason_id 
								LEFT JOIN AREA_DAMAGE ad on ad.area_damage_id = r.reason_group_id
                                ";
                if (inspect_setup_id != null)
                {
                    query += " WHERE inspect_setup_id = @inspect_setup_id and r.is_delete=0  and ir.is_delete=0";
                    if (area_damage_id != null)
                    {
                        query += " and r.reason_group_id = @area_damage_id";
                    }
                }
                else {
                    if (area_damage_id != null)
                    {
                        query += " where r.reason_group_id = @area_damage_id and r.is_delete=0  and ir.is_delete=0";
                    }
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(query, new { inspect_setup_id, area_damage_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }

        // GET api/documentation
        /// <summary>
        /// Fungsi Get service and maintenance inspection
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Master/getSAMInspection/{inspect_setup_id?}")]
        public IHttpActionResult getSAMInspection(string inspect_setup_id = null)
        {

            try
            {
                string query = @"SELECT o.observation_item_id, o.observation_item_name, o.observation_item_unit
                                FROM OBSERVATION_ITEM o
                                INNER JOIN INSPECT_OBSERVATION ir ON o.observation_item_id= ir.observation_item_id ";
                if (inspect_setup_id != null)
                {
                    query += " WHERE inspect_setup_id = @inspect_setup_id";
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(query, new { inspect_setup_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }
        #endregion

        [HttpGet]
        [Route("api/Master/getLookupEPC/{tire_id?}/{rfid?}")]
        public IHttpActionResult getLookupEPC(string tire_id, string rfid)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT epc_rfid as id, epc_rfid as label";
                query += " FROM EPC_TIRE AS a ";
                query += " WHERE is_delete=0";
                if (rfid != null)
                {
                    query += " and upper(tire_id) like upper(@tire_id) and upper(epc_rfid) like upper(@rfid) ";
                }

                var data = dbConn.Query(query, new { tire_id = tire_id + "%", rfid = rfid + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getLookupEPCVhc/{vhc_id?}/{rfid?}")]
        public IHttpActionResult getLookupEPCVhc(string vhc_id, string rfid)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = "SELECT epc_rfid as id, epc_rfid as label";
                query += " FROM EPC_VHC AS a ";
                query += " WHERE is_delete=0";
                if (rfid != null)
                {
                    query += " and upper(vehicle_id) like upper(@vhc_id) and upper(epc_rfid) like upper(@rfid) ";
                }

                var data = dbConn.Query(query, new { vhc_id = vhc_id + "%", rfid = rfid + "%" }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataAllTire")]
        public IHttpActionResult getDataAllTire()
        {
            try
            {
               
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select * from tire where is_delete=0
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataAllVhc")]
        public IHttpActionResult getDataAllVhc()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select * from vehicle where is_delete=0
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataAllFleet")]
        public IHttpActionResult getDataAllFleet()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select * from FLEET_VEHICLE fv
                                inner join tire_unit tu on tu.vehicle_id = fv.vehicle_id and tu.is_delete=0
                                inner join TIRE_AXIS ta on ta.vehicle_id = fv.vehicle_id and ta.tire_id= tu.tire_id and ta.is_delete=0
                                where fv.is_delete=0
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getStatusRfid/{number}/{type}")]
        public IHttpActionResult getStatusRfid(string number, string type)
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                
                String query = "";
                if (type == "00")
                {
                    query = @"select case when rfid ='0' then '0' else '1' end as is_replace_tag from tire  where tire_id=@number and is_delete=0";
                }
                else {
                    query = @"select case when vehicle_rfid ='0' then '0' else '1' end as is_replace_tag from vehicle  where vehicle_id=@number and is_delete=0";
                }
                SqlCommand command = new SqlCommand(query, dbConn);
                command.Parameters.AddWithValue("@number", number);
                dbConn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                string is_replace_tag = "0";
                foreach (DataRow row in dataTable.Rows)
                {
                    is_replace_tag = row["is_replace_tag"].ToString();
                }
                //dbConn.Close();
                //var data = dbConn.Query(query, new{ number }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = dataTable.Rows.Count, is_replace_tag = is_replace_tag }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Master/getRfid/{number}/{type}")]
        public IHttpActionResult getRfid(string number, string type)
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);

                String query = "";
                if (type == "00")
                {
                    query = @"
                                select t.location_id, loc.location, t.rfid from tire t 
                                inner join location loc on loc.location_id = t.location_id
                                where tire_id=@number and t.is_delete=0";
                }
                else
                {
                    query = @"select top 1 'A00S001' as location_id, (select location from location where location_id='A00S001') as location, isnull(ev.epc_rfid,0) as rfid from vehicle  v
                                inner join epc_vhc ev on ev.vehicle_id= v.vehicle_id
                                where v.vehicle_id=@number and v.is_delete=0 order by ev.date_created desc";
                }
                SqlCommand command = new SqlCommand(query, dbConn);
                command.Parameters.AddWithValue("@number", number);
                dbConn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                string location_id = "";
                string location = "";
                string rfid = "";
                foreach (DataRow row in dataTable.Rows)
                {
                    location_id = row["location_id"].ToString();
                    location = row["location"].ToString();
                    rfid = row["rfid"].ToString();
                }
                //dbConn.Close();
                //var data = dbConn.Query(query, new{ number }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = dataTable.Rows.Count, location_id = location_id, location= location, rfid= rfid }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataAllAxis")]
        public IHttpActionResult getDataAllAxis()
        {
            try
            {

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select * from axis where is_delete=0
                            ";
                var data = dbConn.Query(query, null, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataImportListTire/{date_start}/{date_end}/{is_ptsi}")]
        public IHttpActionResult getDataImportListTire(string date_start, string date_end, string is_ptsi)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string dtSTring = dt.ToString("yyyy-MM-dd");
                if (date_start != "0" && date_start != "-" && date_start != "")
                {
                    dt = Convert.ToDateTime(date_start);
                    dtSTring = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring = "0";
                }

                DateTime dt2 = DateTime.Now;
                string dtSTring2 = dt2.ToString("yyyy-MM-dd");
                if (date_end != "0" && date_end != "-" && date_end != "")
                {
                    dt2 = Convert.ToDateTime(date_end);
                    dtSTring2 = dt2.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring2 = "0";
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                String query = "";
                if (is_ptsi == "0")
                {
                    query += @"

							select t.tire_id, 0 as epc_rfid, t.tire_code, tnp.date_created, t.location_id, loc.location, 
                                    'OTH' as process_id,'Input Tyre Non PTSI' as process, tnp.userid, aus.name
                                    from tire t
									inner join tire_non_ptsi tnp on tnp.tire_id = t.tire_id
									inner join LOCATION loc on loc.location_id = t.location_id
                                    inner join AspNetUsers aus on aus.UserId = tnp.userid" + System.Environment.NewLine;

                    if (date_start != "0" && date_start != "-" && date_start != "")
                    {
                        query += "where cast(tnp.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                    }
                    query += "order by tnp.date_created desc";
                }
                else {
                    query = @"select t.tire_id, et.rfid as epc_rfid, t.tire_code, et.date_created, tr.location_id, loc.location, 
                                    tr.process_id,pr.process, et.userid, aus.name
                                    from tire t
                                    inner join TEMP_TAG_DATA et on et.tag_id=t.tire_id
                                    inner join TIRE_TRACKING tr on tr.date_created = et.date_created and et.tag_id= tr.tire_id
                                    inner join LOCATION loc on loc.location_id = tr.location_id
                                    inner join PROCESS pr on pr.process_id = tr.process_id
                                    inner join AspNetUsers aus on aus.UserId = et.userid
                            ";

                    if (date_start != "0" && date_start != "-" && date_start != "")
                    {
                        query += "where cast(et.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                    }
                    query += "order by et.date_created desc";
                }
                
                var data = dbConn.Query(query, new { date_start, date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        public static string getRoleUserByUsername(string username)
        {
            string roleid = "";
            DataTable dtTempTable = new DataTable("EPC_TIRE");
            string StrQuery = @"select zr.RoleId from AspNetUsers aus
                                inner join ZUSER_ROLES zr on zr.Id = aus.Id
                                where username = @username";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                roleid = (from DataRow dr in dtTempTable.Rows
                          select (string)dr["RoleId"]).FirstOrDefault();
            }
            return roleid;
        }
        
        public static string getUseridByUsername(string username)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("AspNetUsers");
            string StrQuery = @"select userId from AspNetUsers aus
                                where username = @username";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                          select (string)dr["userId"]).FirstOrDefault();
            }
            return data;
        }
        [HttpGet]
        [Route("api/Master/getDataTireForDesktop/{date_start}/{date_end}/{username}/{is_ptsi}")]
        public IHttpActionResult getDataTireForDesktop(string date_start, string date_end, string username, string is_ptsi)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string dtSTring = dt.ToString("yyyy-MM-dd");
                if (date_start != "0" && date_start != "-" && date_start != "")
                {
                    dt = Convert.ToDateTime(date_start);
                    dtSTring = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring = "0";
                }
                string res_username = username.Replace("xxxaaxxx", ".");
                string userid = "";
                DateTime dt2 = DateTime.Now;
                string dtSTring2 = dt2.ToString("yyyy-MM-dd");
                if (date_end != "0" && date_end != "-" && date_end != "")
                {
                    dt2 = Convert.ToDateTime(date_end);
                    dtSTring2 = dt2.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring2 = "0";
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                String query = @"select t.tire_id, et.epc_rfid, t.tire_code, et.date_created, tr.location_id, loc.location, 
							case when (select top 1 tire_id from REPLACE_TAG_TIRE where tire_id=t.tire_id and new_epc_rfid = et.epc_rfid order by date_created desc) is not null then '002' else '001' end as process_id ,
								case when (select top 1 tire_id from REPLACE_TAG_TIRE where tire_id=t.tire_id and new_epc_rfid = et.epc_rfid order by date_created desc) is not null then 'Replace Tag' else 'Write Tag' end as process, et.userid, aus.name
                            from tire t
							inner join epc_tire et on et.tire_id=t.tire_id
							inner join TIRE_TRACKING tr on t.tire_id= tr.tire_id and tr.epc_rfid = et.epc_rfid
                            inner join LOCATION loc on loc.location_id = tr.location_id
                            inner join PROCESS pr on pr.process_id = tr.process_id
                            inner join AspNetUsers aus on aus.UserId = et.userid

                            ";
                if (is_ptsi == "0") { query += "inner join tire_non_ptsi tnp on tnp.tire_id = t.tire_id" + System.Environment.NewLine; }
                query += "WHERE t.is_delete = '0' and tr.process_id in ('001', '113')" + System.Environment.NewLine;
                if (is_ptsi == "0") { query += " and loc.parent not in ('A00')" + System.Environment.NewLine; } else { query += " and loc.parent in ('A00')" + System.Environment.NewLine; }
                if (date_start != "0" && date_start != "-" && date_start != "")
                {
                    query += "and cast(et.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                }
                string roleUser = getRoleUserByUsername(res_username);
                if (roleUser != "0" && roleUser != "-" && roleUser != "")
                {
                    userid = getUseridByUsername(res_username);
                    query += "and tr.userid=@userid" + System.Environment.NewLine;
                }
                query += @" group by t.tire_id, et.epc_rfid, t.tire_code, et.date_created, tr.location_id, loc.location, 
                            tr.process_id,pr.process, et.userid, aus.name order by et.date_created desc";
                var data = dbConn.Query(query, new { date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getLetterNoFormat/{type}")]
        public IHttpActionResult getLetterNoFormat(string type)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string dtSTring = dt.ToString("yyyy-MM-dd");
                
                String query = @"";
                if (type=="0")//LETTER DETACH
                {
                    query = "select letter_no='LSD-'+@dtSTring+'-'+CAST(count(letter_loose_no)+1 as varchar) from LETTER_LOOSE where cast(letter_loose_date as date)=@dtSTring";
                }
                else if(type=="1") {//LETTER DISMANTLE
                    query = "select letter_no='LS-'+@dtSTring+'-'+CAST(count(letter_dismantle_no)+1 as varchar) from LETTER_DISMANTLE where cast(letter_dismantle_date as date)=@dtSTring";
                }else if (type == "2")//LETTER FOUND
                {
                    query = "select letter_no='LSF-'+@dtSTring+'-'+CAST(count(letter_found_no)+1 as varchar) from LETTER_FOUND where cast(letter_found_date as date)=@dtSTring";
                }

                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();

                var data = dbConn.Query(query, new { dtSTring }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Master/getDataVhcForDesktop/{vhc_type_id}/{date_start}/{date_end}/{username}")]
        public IHttpActionResult getDataVhcForDesktop(string vhc_type_id, string date_start, string date_end, string username)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string dtSTring = dt.ToString("yyyy-MM-dd");
                if (date_start != "0" && date_start != "-" && date_start != "")
                {
                    dt = Convert.ToDateTime(date_start);
                    dtSTring = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring = "0";
                }

                string res_username = username.Replace("xxxaaxxx", ".");
                DateTime dt2 = DateTime.Now;
                string userid = "";
                string dtSTring2 = dt2.ToString("yyyy-MM-dd");
                if (date_end != "0" && date_end != "-" && date_end != "")
                {
                    dt2 = Convert.ToDateTime(date_end);
                    dtSTring2 = dt2.ToString("yyyy-MM-dd");
                }
                else
                {
                    dtSTring2 = "0";
                }
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                string c_type_vhc = "";
                if (vhc_type_id == "3")
                {
                    c_type_vhc = "('2', '3')";
                }
                else {
                    c_type_vhc = "('1')";
                }
                String query = @"select vhc.vehicle_id, ev.epc_rfid, bv.brand_vhc_name +';'+cast(vhc.axis_type as varchar) as spec, ev.date_created, 'A00S001' as location_id,
                                'Workshop' as location, case when (select top 1 vehicle_id from REPLACE_TAG_VHC where vehicle_id=vhc.vehicle_id and new_epc_rfid = ev.epc_rfid order by date_created desc) is not null then '002' else '001' end as process_id ,
								case when (select top 1 vehicle_id from REPLACE_TAG_VHC where vehicle_id=vhc.vehicle_id and new_epc_rfid = ev.epc_rfid order by date_created desc) is not null then 'Replace Tag' else 'Write Tag' end as process, ev.userid, aus.name
                                from vehicle vhc
                                inner join EPC_VHC ev on ev.vehicle_id = vhc.vehicle_id
                                inner join VEHICLE_TYPE vt on vt.vehicle_type_id = vhc.vehicle_type_id
                                inner join BRAND_VEHICLE bv on bv.brand_vhc_id = vhc.brand_vhc_id
                                inner join AspNetUsers aus on aus.UserId = ev.userid
                                where vhc.vehicle_type_id in "+ c_type_vhc + @"
                            
                                ";
                if (date_start != "0" && date_start != "-" && date_start != "")
                {
                    query += "and cast(ev.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                }
                string roleUser = getRoleUserByUsername(res_username);
                if (roleUser != "0" && roleUser != "-" && roleUser != "")
                {
                    userid = getUseridByUsername(res_username);
                    query += "and ev.userid=@userid" + System.Environment.NewLine;
                }
                query += "order by ev.date_created desc";
                var data = dbConn.Query(query, new { vhc_type_id, date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
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
