using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BASE_RFID_TMS.Models;
using Newtonsoft.Json.Linq;

namespace BASE_RFID_TMS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [GetTimeOutSystem]
    [Authorize]
    public class RangePercentageTUR
    {
        public string data { get; set; }
    }
    public class RangePercentageTURDetail
    {
        public string range { get; set; }
        public string valueStart { get; set; }
        public string valueEnd { get; set; }
        public int qtyTire { get; set; }
    }

    public class DashboardController : ApiController
    {
        private string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();


    #region new Dashboard
        //ADJUSMENT PTSI
        [HttpGet]
        [Route("api/Dashboard/DtsTireRunning")]
        public IHttpActionResult DtsTireRunning()
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty  from item i
                            inner join tire t on i.tire_code = t.tire_code
                            inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                            where t.location_id in ('OV00','OV01') and i.tire_code not in ('XXX')
                            group by i.tire_code, bt.brand_tire_name, t.size
";
            
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
        [Route("api/Dashboard/DtsKMTireRunning")]
        public IHttpActionResult DtsKMTireRunning()
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, sum(t.km)/(select count(tire_id) from tire where tire_code=i.tire_code and location_id in ('OV00','OV01') ) as km  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                where t.location_id in ('OV00','OV01') and i.tire_code not in ('XXX')
                                group by i.tire_code, bt.brand_tire_name, t.size";

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
        [Route("api/Dashboard/DtsActualLifetimeTire")]
        public IHttpActionResult DtsActualLifetimeTire()
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, sum(t.km)/(select count(t2.tire_id) from tire t2 inner join SCRAP_TIRE st on st.tire_id=t2.tire_id where tire_code=i.tire_code  ) as km  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join SCRAP_TIRE SD on SD.tire_id = t.tire_id	
                                where i.tire_code not in ('XXX')
                                group by i.tire_code, bt.brand_tire_name, t.size
                                ";

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
        [Route("api/Dashboard/DtsScrapTire/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsScrapTire(string startDate = null, string endDate = null)
        {
            string criteria = "";
            if (startDate != "0") {
                criteria = "where cast(sd.date_created as date) between @startDate and @endDate ";
            }
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(sd.tire_id) as qty  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join SCRAP_TIRE SD on SD.tire_id = t.tire_id
                                "+ criteria;
            StrQuery+=@"group by i.tire_code, bt.brand_tire_name, t.size
";

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Dashboard/DtsTirePerformanceByTireSize")]
        public IHttpActionResult DtsTirePerformanceByTireSize()
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(smr.tire_id) as qty from item i 
                                        inner join tire t on i.tire_code = t.tire_code
                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                        inner join SERV_MAINT_REPAIR SMR on smr.tire_id = t.tire_id
                                        where t.tire_id not in ('XXX') and smr.status_id = '2'
                                        group by i.tire_code, bt.brand_tire_name, t.size
";

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
        [Route("api/Dashboard/DtsTireRunningOprtArea")]
        public IHttpActionResult DtsWsTireRunningOprArea()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty  from item i
                            inner join tire t on i.tire_code = t.tire_code
                            inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                            inner join location loc on loc.location_id= t.location_id
                            inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                            inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                            inner join FLEET f on f.fleet_id = fv.fleet_id
                            where t.location_id in ('OV00') and i.tire_code not in ('XXX') and f.location_type_id=@location_type_id
                            group by i.tire_code, bt.brand_tire_name, t.size
";
                var content = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                if (content.Count < 1)
                {
                    StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, 0 as qty 
                            from item i
                            inner join tire t on i.tire_code = t.tire_code
                            inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                            inner join location loc on loc.location_id= t.location_id
                            inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                            inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                            inner join FLEET f on f.fleet_id = fv.fleet_id
                            where t.location_id in ('OV00') and i.tire_code not in ('XXX')
                            group by i.tire_code, bt.brand_tire_name, t.size
";
                    content = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                }
                data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsTireRunningUnitType")]
        public IHttpActionResult DtsWsTireRunningUnitType()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select vehicle_type_id, vehicle_type from VEHICLE_TYPE
                        where vehicle_type_id not in ('0')";
                var content = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                for (int j = 0; j < content.Count; j++)
                {
                    StrQuery = @"
                            IF (EXISTS (select count(t.tire_id) as qty  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join location loc on loc.location_id= t.location_id
                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
								inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                                    inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                                    inner join FLEET f on f.fleet_id = fv.fleet_id
                                where t.location_id in ('OV00') and i.tire_code not in ('XXX') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id
                                group by vt.vehicle_type_id) )  
                
                BEGIN 
                select  count(t.tire_id) as qty  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join location loc on loc.location_id= t.location_id
                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
								inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                                    inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                                    inner join FLEET f on f.fleet_id = fv.fleet_id
                                where t.location_id in ('OV00') and i.tire_code not in ('XXX') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id
                                group by vt.vehicle_type_id
                END
                ELSE
                BEGIN
                select 0 as qty
                END

";
                    var detail = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id, vehicle_type_id = content[j].vehicle_type_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                    content[j].qty = detail[0].qty;
                   
                }
                    
               
                data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }



        [HttpGet]
        [Route("api/Dashboard/DtsFlatTireOprtArea/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsWsFlatTireOprArea(string startDate = null, string endDate = null)
        {
            string StrQuery = "";
            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(smo2.date_created as date) between @startDate and @endDate ";
            }
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select z.description, z.brand_tire_name, z.tire_code, z.size, count(z.tire) as qty from (
                                select t.tire_id,description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, case when (
                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where smo.tire_id=t.tire_id and 
                                oi.observation_item_id = '2'
                                order by smod.date_created desc) is not null 
                                then
	                                case when (
	                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
	                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
	                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
	                                where smo.tire_id=t.tire_id and 
	                                oi.observation_item_id = '2'
	                                order by smod.date_created desc) < 5 --This is the minimum tread for a tire
	                                then 
	                                1
	                                end
                                end
                                tire
                                from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join location loc on loc.location_id= t.location_id
                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                                inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                                inner join FLEET f on f.fleet_id = fv.fleet_id
								inner join SERV_MAINT_OBSERVATION smo2 on smo2.tire_id = t.tire_id
                                where i.tire_code not in ('XXX') and t.status_id not in ('6')  and f.location_type_id=@location_type_id "+ criteria + @"
                                group by t.tire_id,i.tire_code, bt.brand_tire_name, t.size
                                ) Z
                                group by z.description, z.brand_tire_name, z.tire_code, z.size

";
                var content = conn.Query(StrQuery, new { startDate, endDate, location_type_id = data[i].location_type_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                if (content.Count > 0)
                {
                    data[i].data = content;
                }
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsFlatTireUnitType/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsWsFlatTireUnitType(string startDate = null, string endDate = null)
        {
            string StrQuery = "";
            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(smo2.date_created as date) between @startDate and @endDate ";
            }
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select vehicle_type_id, vehicle_type from VEHICLE_TYPE
                        where vehicle_type_id not in ('0')";
                var content = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                for (int j = 0; j < content.Count; j++)
                {
                    StrQuery = @"
                            IF (EXISTS (select count(z.tire) as qty from (
                                select vt.vehicle_type,  case when (
                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where smo.tire_id=t.tire_id and 
                                oi.observation_item_id = '2'
                                order by smod.date_created desc) is not null 
                                then
	                                case when (
	                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
	                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
	                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
	                                where smo.tire_id=t.tire_id and 
	                                oi.observation_item_id = '2'
	                                order by smod.date_created desc) < 5 --This is the minimum tread for a tire
	                                then 
	                                1
	                                end
                                end
                                tire
                                from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join location loc on loc.location_id= t.location_id
                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
								inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
								inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
								inner join FLEET f on f.fleet_id = fv.fleet_id
								inner join SERV_MAINT_OBSERVATION smo2 on smo2.tire_id = t.tire_id
                                where i.tire_code not in ('XXX') and t.status_id not in ('6') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id " + criteria + @"
                                group by vt.vehicle_type,t.tire_id
                                ) Z) )  
                
                BEGIN 
                select count(z.tire) as qty from (
                                select vt.vehicle_type,  case when (
                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where smo.tire_id=t.tire_id and 
                                oi.observation_item_id = '2'
                                order by smod.date_created desc) is not null 
                                then
	                                case when (
	                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
	                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
	                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
	                                where smo.tire_id=t.tire_id and 
	                                oi.observation_item_id = '2'
	                                order by smod.date_created desc) < 5 --This is the minimum tread for a tire
	                                then 
	                                1
	                                end
                                end
                                tire
                                from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join location loc on loc.location_id= t.location_id
                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
								inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
								inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
								inner join FLEET f on f.fleet_id = fv.fleet_id
                                inner join SERV_MAINT_OBSERVATION smo2 on smo2.tire_id = t.tire_id
                                where i.tire_code not in ('XXX') and t.status_id not in ('6') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id " + criteria + @"
                                group by vt.vehicle_type,t.tire_id
                                ) Z
                
                END
                ELSE
                BEGIN
                select 0 as qty
                END

";
                    var detail = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id, vehicle_type_id = content[j].vehicle_type_id, startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                    content[j].qty = detail[0].qty;

                }


                data[i].data = content;
//                StrQuery = @"select count(z.tire) as qty from (
//                                select vt.vehicle_type,  case when (
//                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
//                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                where smo.tire_id=t.tire_id and 
//                                oi.observation_item_id = '2'
//                                order by smod.date_created asc) is not null 
//                                then
//	                                case when (
//	                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
//	                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//	                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//	                                where smo.tire_id=t.tire_id and 
//	                                oi.observation_item_id = '2'
//	                                order by smod.date_created asc) < 5 --This is the minimum tread for a tire
//	                                then 
//	                                1
//	                                end
//                                end
//                                tire
//                                from item i 
//                                inner join tire t on i.tire_code = t.tire_code
//                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
//                                inner join location loc on loc.location_id= t.location_id
//                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
//                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
//								inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
//								inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id
//								inner join FLEET f on f.fleet_id = fv.fleet_id
//                                where i.tire_code not in ('XXX') and t.status_id not in ('6') and f.location_type_id=@location_type_id
//                                group by vt.vehicle_type,t.tire_id
//                                ) Z
                               
//";
//                var content = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id }, null, true, commandTimeout: 0, commandType: null).ToList();
//                if (content.Count > 0)
//                {
//                    data[i].data = content;
//                }
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsTURScrap/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsTURScrap(string startDate = null, string endDate = null)
        {
            string criteria = "";
            if (startDate != "0")
            {
                criteria = "where cast(sd_.date_created as date) between @startDate and @endDate ";
            }
            string StrQuery = @"select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
                                select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
                                select top 1 cast(smod.value as float) as value from scrap_tire sd
                                inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                order by smod.date_created asc) -
                                (
                                select top 1 cast(smod.value as float) as value  from scrap_tire sd
                                inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                order by smod.date_created desc)/(
                                select top 1 cast(smod.value as float) as value from scrap_tire sd
                                inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                order by smod.date_created asc))/100 as TUR 
                                from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
                                " + criteria;
            StrQuery+= @"group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
                            ) X
                            group by X.description, x.tire_code, x.brand_tire_name, x.size";

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { startDate , endDate}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Dashboard/DtsTireStockAtStore")]
        public IHttpActionResult DtsTireStockAtStore()
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                where t.location_id='A00S002' and i.tire_code not in ('XXX')
                                group by i.tire_code, bt.brand_tire_name, t.size
";

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
        [Route("api/Dashboard/DtsWsTireMonitoring")]
        public IHttpActionResult DtsWsTireMonitoring()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select status_id, status_name from status where status_id in ('1','2','4','9')
                            ";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty  from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                where i.tire_code not in ('XXX') and status_id=@status_id and t.location_id = 'A00S001'
                                group by i.tire_code, bt.brand_tire_name, t.size";
                var content = conn.Query(StrQuery, new { status_id = data[i].status_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                if (content.Count < 1)
                {
                    StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, 0 as qty  
                                from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                where i.tire_code not in ('XXX') and t.location_id = 'A00S001'
                                group by i.tire_code, bt.brand_tire_name, t.size";
                    content = conn.Query(StrQuery, new { status_id = data[i].status_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                }
                data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsWsFlatTire/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsWsFlatTire(string startDate = null, string endDate = null)
        {
            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(smo2.date_created as date) between @startDate and @endDate ";
            }
            string StrQuery = @"
                                select z.description, z.brand_tire_name, z.tire_code, z.size, count(z.tire) as qty from (
                                select t.tire_id,description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, case when (
                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                where smo.tire_id=t.tire_id and 
                                oi.observation_item_id = '2'
                                order by smod.date_created desc) is not null 
                                then
	                                case when (
	                                select top 1 cast(smod.value as float) as value from SERV_MAINT_OBSERVATION smo
	                                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
	                                inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
	                                where smo.tire_id=t.tire_id and 
	                                oi.observation_item_id = '2'
	                                order by smod.date_created desc) < 5 --This is the minimum tread for a tire
	                                then 
	                                1
	                                end
                                end
                                tire
                                from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join SERV_MAINT_OBSERVATION smo2 on smo2.tire_id = t.tire_id
                                where i.tire_code not in ('XXX') and t.status_id not in ('6') "+ criteria + @"
                                group by t.tire_id,i.tire_code, bt.brand_tire_name, t.size
                                ) Z
                                group by z.description, z.brand_tire_name, z.tire_code, z.size
";

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { startDate, endDate}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Dashboard/DtsMonitoringCheckpointTireStock")] // ppt slide 12
        public IHttpActionResult DtsWsMonitoringCheckpointTireStock()
        {
            //string StrQuery = @"select loc.location_id, loc.location, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty  
            //                        from item i
            //                        inner join tire t on i.tire_code = t.tire_code
            //                        inner join LOCATION loc on loc.location_id = t.location_id
            //                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
            //                        where t.location_id not in ('OV00', 'XXX' , 'A00S001', 'A00S002') and i.tire_code not in ('XXX')
            //                         and t.status_id not in ('6')
            //                        group by loc.location_id, loc.location,i.tire_code, bt.brand_tire_name, t.size";

            String StrQuery = @"select  loc.location_id, loc.location,sum(case t.status_id when 1 then 1 else 0 end) as ready,sum(case t.status_id when 2 then 1 else 0 end) as repair,sum(case t.status_id when 3 then 1 else 0 end) as send_to_ws  
							    from item i
                                inner join tire t on i.tire_code = t.tire_code
								inner join location loc on loc.location_id = t.location_id
								where t.location_id not in ('OV00', 'XXX' , 'A00S001', 'A00S002', '999', '888') and i.tire_code not in ('XXX') --and loc.location_type_id = '1'
								 group by loc.location_id, loc.location";

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
        [Route("api/Dashboard/DtsTireScrapReporting")]
        public IHttpActionResult DtsTireScrapReporting()
        {
            string StrQuery = @"select sum(case sd.area_damage_id when 4 then 1 else 0 end) as tread, sum(case sd.area_damage_id when 3 then 1 else 0 end) as sidewall, sum(case sd.area_damage_id when 2 then 1 else 0 end) as shoulder, sum(case sd.area_damage_id when 1 then 1 else 0 end) as bead
                                from scrap_tire sd
                                inner join tire t on t.tire_id = sd.tire_id";

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

        } //ppt slide 15

        [HttpGet]
        [Route("api/Dashboard/DtsTireRunningMonitoringOprtArea")]
        public IHttpActionResult DtsTireRunningMonitoringOprtArea()
        {
            string StrQuery = @"select lt.location_type_id, lt.location_type, count(tu.tire_id) as qty
                                from tire_unit tu
                                inner join vehicle v on v.vehicle_id = tu.vehicle_id
                                inner join fleet_vehicle fv on fv.vehicle_id = v.vehicle_id and FV.is_delete=0
                                inner join fleet f on f.fleet_id = fv.fleet_id
                                inner join location_type lt on lt.location_type_id = f.location_type_id
                                where lt.location_type_id in ('1','2')
                                group by lt.location_type_id, lt.location_type";

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

        } // ppt slide 9

        [HttpGet]
        [Route("api/Dashboard/DtsTireRunningMonitoringOprtArea/{oprt_area}")] //ppt slide 10
        public IHttpActionResult DtsTireRunningOprtArea(string oprt_area)
        {
            string StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(tu.tire_id) as qty from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join tire_unit tu on tu.tire_id = t.tire_id     
								inner join vehicle v on v.vehicle_id = tu.vehicle_id
                                inner join fleet_vehicle fv on fv.vehicle_id = v.vehicle_id and FV.is_delete=0
                                inner join fleet f on f.fleet_id = fv.fleet_id
                                inner join location_type lt on lt.location_type_id = f.location_type_id
								where lt.location_type_id = @oprt_area
                                group by i.tire_code, bt.brand_tire_name, t.size";

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { oprt_area }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Dashboard/DtsCheckpointTireMonitoring/{location_id}")] //ppt slide 13 dan 14
        public IHttpActionResult DtsCheckpointTireMonitoring(string location_id)
        {
            string StrQuery = @"select status_id, status_name from status where is_delete ='0' and status_id in ('1','2','3')";
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                
                for (int i=0; i<data.Count; i++)
                {
                    String status_id = data[i].status_id;
                    StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(t.tire_id) as qty
                                from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id = i.brand_tire_id
                                where i.tire_code not in ('XXX') and t.status_id = @status_id and t.location_id = @location_id
                                group by i.tire_code, bt.brand_tire_name, t.size";
                    var detail= dbConn.Query(StrQuery, new { location_id, status_id}, null, true, commandTimeout: 0, commandType: null).ToList();
                    if (detail.Count < 1)
                    {
                        StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, 0 as qty
                                from item i
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id = i.brand_tire_id
                                where i.tire_code not in ('XXX') and t.location_id = @location_id
                                group by i.tire_code, bt.brand_tire_name, t.size";
                        detail = dbConn.Query(StrQuery, new { location_id, status_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                    }
                    data[i].data = detail;
                }
                
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Dashboard/DtsScrapPartOfTire")] //ppt slide 5 baris 2 pie
        public IHttpActionResult DtsScrapPartOfTire()
        {
            string StrQuery = @"select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                                from scrap_tire sd
                                inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                                inner join tire t on t.tire_id = sd.tire_id ";
            //StrQuery += oprt_area != null ? " where sd.tire_id in (select tire_id from where loca) ":"";
            StrQuery += " group by sd.area_damage_id, ad.area_damage_name";
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
        [Route("api/Dashboard/DtsRepairPerformanceOprtArea/{startDate?}/{endDate?}")] //ppt slide 6
        public IHttpActionResult DtsRepairPerformanceOprtArea(string startDate = null, string endDate = null)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            String StrQuery = @"select location_type_id, location_type from location_type where is_delete = '0' and location_type_id not in ('0')";

            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(r.date_created as date) between @startDate and @endDate ";
            }
            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(r.tire_id) as qty
								from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join serv_maint_repair r on r.tire_id = t.tire_id
								inner join location loc on loc.location_id = r.location_id
								where i.tire_code not in ('XXX') and loc.location_type_id = @location_type_id and r.status_id='2' "+ criteria + @"
                                group by i.tire_code, bt.brand_tire_name, t.size";
                var content = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id, startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                if (content.Count < 1)
                {
                    StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size,0 as qty
								from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join serv_maint_repair r on r.tire_id = t.tire_id
								inner join location loc on loc.location_id = r.location_id
								where i.tire_code not in ('XXX')  and r.status_id='2'  " + criteria + @"
                                group by i.tire_code, bt.brand_tire_name, t.size";
                    content = conn.Query(StrQuery, new { status_id = data[i].status_id, startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                }
                data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }



        [HttpGet]
        [Route("api/Dashboard/DtsScrapTireOprtArea/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsScrapTireOprtArea(string startDate = null, string endDate = null)
        {
            string StrQuery = "";

            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(sd.date_created as date) between @startDate and @endDate ";
            }
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"
                               select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size
                                , count(sd.tire_id) as qty  
                                from item i
                                    inner join tire t on i.tire_code = t.tire_code
                                    inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                    inner join SCRAP_TIRE SD on SD.tire_id = t.tire_id
                                    inner join location loc on loc.location_id= t.location_id
                                    inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                                    inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                                    inner join FLEET f on f.fleet_id = fv.fleet_id
                                 where i.tire_code not in ('XXX') and f.location_type_id=@location_type_id "+ criteria + @"
                                group by i.tire_code, bt.brand_tire_name, t.size
";
                var content = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id, startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                //                if (content.Count < 1)
                //                {
                //                    StrQuery = @"select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, 0 as qty 
                //                            from item i
                //                            inner join tire t on i.tire_code = t.tire_code
                //                            inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                //                            inner join location loc on loc.location_id= t.location_id
                //                            inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=0
                //                            inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id
                //                            inner join FLEET f on f.fleet_id = fv.fleet_id
                //                            where t.location_id in ('OV00') and i.tire_code not in ('XXX')
                //                            group by i.tire_code, bt.brand_tire_name, t.size
                //";
                //                    content = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                //                }

                if (content.Count > 0)
                {
                    data[i].data = content;
                }
                //data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }


        [HttpGet]
        [Route("api/Dashboard/DtsScrapTireUnitType/{startDate?}/{endDate?}")]
        public IHttpActionResult DtsScrapTireUnitType(string startDate = null, string endDate = null)
        {
            string StrQuery = "";
            string criteria = "";
            if (startDate != "0")
            {
                criteria = " and cast(sd.date_created as date) between @startDate and @endDate ";
            }
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"select location_type_id, location_type from location_TYPE
                        where location_type_id in ('1','2')";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @"select vehicle_type_id, vehicle_type from VEHICLE_TYPE
                        where vehicle_type_id not in ('0')";
                var content = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                for (int j = 0; j < content.Count; j++)
                {
                    StrQuery = @"
                            IF (EXISTS (
                    select count(t.tire_id) as qty  
                    from item i
                    inner join tire t on i.tire_code = t.tire_code
                    inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                    inner join SCRAP_TIRE SD on SD.tire_id = t.tire_id
                    inner join location loc on loc.location_id= t.location_id
                    inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                    inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                    inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                    inner join FLEET f on f.fleet_id = fv.fleet_id
                    where i.tire_code not in ('XXX') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id "+ criteria + @"
                    ) )  
                
                BEGIN 
                select count(t.tire_id) as qty  
                    from item i
                    inner join tire t on i.tire_code = t.tire_code
                    inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                    inner join SCRAP_TIRE SD on SD.tire_id = t.tire_id
                    inner join location loc on loc.location_id= t.location_id
                    inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                    inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                    inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                    inner join FLEET f on f.fleet_id = fv.fleet_id
                    where i.tire_code not in ('XXX') and f.location_type_id=@location_type_id and vt.vehicle_type_id=@vehicle_type_id " + criteria + @"
                END
                ELSE
                BEGIN
                select 0 as qty
                END

";
                    var detail = conn.Query(StrQuery, new { location_type_id = data[i].location_type_id, vehicle_type_id = content[j].vehicle_type_id, startDate, endDate }, null, true, commandTimeout: 0, commandType: null).ToList();
                    content[j].qty = detail[0].qty;

                }

               
                data[i].data = content;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnPartOfType")]
        public IHttpActionResult DtsQtyScrapTireBaseOnPartOfType()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"
	                    select  count(aa.area_damage_id) as total_rows from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        where sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            double total_rows = 0;
            for (int i = 0; i < data.Count; i++)
            {
                total_rows = data[i].total_rows;
               
                
            }
            StrQuery = @"select  aa.area_damage_id, aa.area_damage_name, @total_rows as total_rows, sum(aa.qty)  as qty_scrap_per_damage from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        where sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa
	                    group by aa.area_damage_id, aa.area_damage_name";
           var  content = conn.Query(StrQuery, new { total_rows = total_rows }, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < content.Count; i++)
            {
                //total_rows = data[i].total_rows;
                double percentage = total_rows/ content[i].qty_scrap_per_damage;
                content[i].percentage = percentage.ToString();
                
            }
            
            return Json((new { status = true, records = content.Count, message = "", data = content }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnPartOfTypeLowLand")]
        public IHttpActionResult DtsQtyScrapTireBaseOnPartOfTypeLowLand()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"
	                    select  count(aa.area_damage_id) as total_rows from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                        inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                    inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                        inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                        inner join FLEET f on f.fleet_id = fv.fleet_id
	                    where f.location_type_id='1' and sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            double total_rows = 0;
            for (int i = 0; i < data.Count; i++)
            {
                total_rows = data[i].total_rows;


            }
            StrQuery = @"select  aa.area_damage_id, aa.area_damage_name, @total_rows as total_rows, sum(aa.qty)  as qty_scrap_per_damage from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                        inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                    inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                        inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                        inner join FLEET f on f.fleet_id = fv.fleet_id
	                    where f.location_type_id='1' and sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa
	                    group by aa.area_damage_id, aa.area_damage_name";
            var content = conn.Query(StrQuery, new { total_rows = total_rows }, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < content.Count; i++)
            {
                //total_rows = data[i].total_rows;
                double percentage = total_rows / content[i].qty_scrap_per_damage;
                content[i].percentage = percentage.ToString();

            }

            return Json((new { status = true, records = content.Count, message = "", data = content }));
        }
        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnPartOfTypeMineralSoil")]
        public IHttpActionResult DtsQtyScrapTireBaseOnPartOfTypeMineralSoil()
        {
            string StrQuery = "";
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            StrQuery += @"
	                    select  count(aa.area_damage_id) as total_rows from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                        inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                    inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                        inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                        inner join FLEET f on f.fleet_id = fv.fleet_id
	                    where f.location_type_id='2' and sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa";

            conn.Open();
            var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            double total_rows = 0;
            for (int i = 0; i < data.Count; i++)
            {
                total_rows = data[i].total_rows;


            }
            StrQuery = @"select  aa.area_damage_id, aa.area_damage_name, @total_rows as total_rows, sum(aa.qty)  as qty_scrap_per_damage from (select sd.area_damage_id, ad.area_damage_name, count(sd.tire_id) as qty
                        from scrap_tire sd
                        inner join area_damage ad on ad.area_damage_id = sd.area_damage_id
                        inner join tire t on t.tire_id = sd.tire_id
                        inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
                        inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
	                    inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
                        inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
                        inner join FLEET f on f.fleet_id = fv.fleet_id
	                    where f.location_type_id='2' and sd.tire_id not in (select tire_id from disposal_detail)
	                    group by sd.area_damage_id, ad.area_damage_name) as aa
	                    group by aa.area_damage_id, aa.area_damage_name";
            var content = conn.Query(StrQuery, new { total_rows = total_rows }, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < content.Count; i++)
            {
                //total_rows = data[i].total_rows;
                double percentage = total_rows / content[i].qty_scrap_per_damage;
                content[i].percentage = percentage.ToString();

            }

            return Json((new { status = true, records = content.Count, message = "", data = content }));
        }

        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnRangeTUR")]
        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTUR()
        {
            var RangePercentage = new List<RangePercentageTURDetail>
            {
                new RangePercentageTURDetail
                {
                    range = "9-20",
                    valueStart = "0.09",
                    valueEnd = "0.20"
                },
                new RangePercentageTURDetail
                {
                    range = "21-40",
                    valueStart = "0.21",
                    valueEnd = "0.40"
                },
                new RangePercentageTURDetail
                {
                    range= "41-60",
                    valueStart= "0.41",
                    valueEnd= "0.60"
                },
                new RangePercentageTURDetail
                {
                    range= "61-80",
                    valueStart= "0.61",
                    valueEnd= "0.80"
                },
                new RangePercentageTURDetail
                {
                    range= "81-100",
                    valueStart= "0.81",
                    valueEnd= "1"
                },
                // ... Tambahkan item-range lainnya di sini sesuai kebutuhan.
            };

            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();

                foreach (var range in RangePercentage)
                {
                    string StrQuery = @"
                SELECT ISNULL(COUNT(D.tire_code), 0) AS qty_tire
                FROM (
                    SELECT X.description, x.tire_code, x.brand_tire_name, x.size, SUM(x.TUR) AS TUR
                    FROM (
                        SELECT description = i.tire_code + ' ' + t.size, i.tire_code, bt.brand_tire_name, t.size, (
                            (
                                SELECT TOP 1 CAST(smod.value AS FLOAT) AS value
                                FROM scrap_tire sd
                                INNER JOIN SERV_MAINT_OBSERVATION smo ON smo.tire_id = sd.tire_id
                                INNER JOIN SERV_MAINT_OBSERVATION_DETAIL smod ON smod.smo_id = smo.smo_id AND smod.date_created = smo.date_created
                                INNER JOIN OBSERVATION_ITEM oi ON oi.observation_item_id = smod.observation_item_id
                                WHERE sd.tire_id = sd_.tire_id AND oi.observation_item_id = '2'
                                ORDER BY smod.date_created ASC
                            ) -
                            (
                                SELECT TOP 1 CAST(smod.value AS FLOAT) AS value
                                FROM scrap_tire sd
                                INNER JOIN SERV_MAINT_OBSERVATION smo ON smo.tire_id = sd.tire_id
                                INNER JOIN SERV_MAINT_OBSERVATION_DETAIL smod ON smod.smo_id = smo.smo_id AND smod.date_created = smo.date_created
                                INNER JOIN OBSERVATION_ITEM oi ON oi.observation_item_id = smod.observation_item_id
                                WHERE sd.tire_id = sd_.tire_id AND oi.observation_item_id = '2'
                                ORDER BY smod.date_created DESC
                            ) / (
                                SELECT TOP 1 CAST(smod.value AS FLOAT) AS value
                                FROM scrap_tire sd
                                INNER JOIN SERV_MAINT_OBSERVATION smo ON smo.tire_id = sd.tire_id
                                INNER JOIN SERV_MAINT_OBSERVATION_DETAIL smod ON smod.smo_id = smo.smo_id AND smod.date_created = smo.date_created
                                INNER JOIN OBSERVATION_ITEM oi ON oi.observation_item_id = smod.observation_item_id
                                WHERE sd.tire_id = sd_.tire_id AND oi.observation_item_id = '2'
                                ORDER BY smod.date_created ASC
                            )
                        ) / 100 AS TUR
                        FROM item i
                        INNER JOIN tire t ON i.tire_code = t.tire_code
                        INNER JOIN BRAND_TIRE BT ON BT.brand_tire_id = i.brand_tire_id
                        INNER JOIN scrap_tire sd_ ON sd_.tire_id = t.tire_id
                        GROUP BY sd_.tire_id, i.tire_code, bt.brand_tire_name, t.size
                    ) X
                    GROUP BY X.description, x.tire_code, x.brand_tire_name, x.size
                ) AS D
                WHERE D.TUR BETWEEN @valueStart AND @valueEnd";

                    var data = conn.Query(StrQuery, new
                    {
                        valueStart = range.valueStart,
                        valueEnd = range.valueEnd
                    }, commandTimeout: 0, commandType: null).ToList();

                    range.qtyTire = data[0].qty_tire;
                    ListDetail.Add(range);
                }
            }

            return Json(new { status = true, records = 0, message = "", data = ListDetail });
        }
//        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTUR()
//        {
//            string StrQuery = "";
//            string RangePercentage= @"[
//                {
//                    range: '9-20',
//                    valueStart: '0.09',
//                    valueEnd: '0.20'
//                },
//                {
//                    range: '21-40',
//                    valueStart: '0.21',
//                    valueEnd: '0.40'
//                },
//                {
//                    range: '41-60',
//                    valueStart: '0.41',
//                    valueEnd: '0.60'
//                },
//                {
//                    range: '61-80',
//                    valueStart: '0.61',
//                    valueEnd: '0.80'
//                },
//                {
//                    range: '81-100',
//                    valueStart: '0.81',
//                    valueEnd: '1'
//                },
//]";
//            string jsonString = JsonConvert.SerializeObject(RangePercentage);
//            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();
//            ListDetail = JsonConvert.DeserializeObject<List<RangePercentageTURDetail>>(RangePercentage);
            
//            for (int i = 0; i < ListDetail.Count; i++)
//            {
//                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
//                StrQuery = @"
                         
//                                select isnull(count(D.tire_code),0) as qty_tire from (
//                                select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
//                                        select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc) -
//                                        (
//                                        select top 1 cast(smod.value as float) as value  from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created desc)/(
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc))/100 as TUR 
//                                        from item i 
//                                        inner join tire t on i.tire_code = t.tire_code
//                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
//                                        inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
//		                                group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
//                                    ) X
//                                    group by X.description, x.tire_code, x.brand_tire_name, x.size
//                                ) as D
//                                where D.TUR between '" + ListDetail[i].valueStart+ "' and '" + ListDetail[i].valueEnd + "'";

//                conn.Open();
//                var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
//                ListDetail[i].qtyTire = data[0].qty_tire;
               
//            }

//            return Json((new { status = true, records = 0, message = "", data = ListDetail }));
//        }

        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnRangeTURLowLand")]

        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTURLowLand()
        {
            var RangePercentage = new List<RangePercentageTURDetail>
            {
                new RangePercentageTURDetail
                {
                    range = "9-20",
                    valueStart = "0.09",
                    valueEnd = "0.20"
                },
                new RangePercentageTURDetail
                {
                    range = "21-40",
                    valueStart = "0.21",
                    valueEnd = "0.40"
                },
                new RangePercentageTURDetail
                {
                    range= "41-60",
                    valueStart= "0.41",
                    valueEnd= "0.60"
                },
                new RangePercentageTURDetail
                {
                    range= "61-80",
                    valueStart= "0.61",
                    valueEnd= "0.80"
                },
                new RangePercentageTURDetail
                {
                    range= "81-100",
                    valueStart= "0.81",
                    valueEnd= "1"
                },
                // ... Tambahkan item-range lainnya di sini sesuai kebutuhan.
            };

            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();

                foreach (var range in RangePercentage)
                {
                    string StrQuery = @"
                  select isnull(count(D.tire_code),0) as qty_tire from (
                                select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
                                        select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created asc) -
                                        (
                                        select top 1 cast(smod.value as float) as value  from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created desc)/(
                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created asc))/100 as TUR 
                                        from item i 
                                        inner join tire t on i.tire_code = t.tire_code
                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                        inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
		                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
		                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
		                                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
		                                inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
		                                inner join FLEET f on f.fleet_id = fv.fleet_id
		                                where f.location_type_id='1'
		                                group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
                                    ) X
                                    group by X.description, x.tire_code, x.brand_tire_name, x.size
                                ) as D
                                where D.TUR between @valueStart AND @valueEnd";

                    var data = conn.Query(StrQuery, new
                    {
                        valueStart = range.valueStart,
                        valueEnd = range.valueEnd
                    }, commandTimeout: 0, commandType: null).ToList();

                    range.qtyTire = data[0].qty_tire;
                    ListDetail.Add(range);
                }
            }

            return Json(new { status = true, records = 0, message = "", data = ListDetail });
        }
//        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTURLowLand()
//        {
//            string StrQuery = "";
//            string RangePercentage = @"[
//                {
//                    range: '9-20',
//                    valueStart: '0.09',
//                    valueEnd: '0.20'
//                },
//                {
//                    range: '21-40',
//                    valueStart: '0.21',
//                    valueEnd: '0.40'
//                },
//                {
//                    range: '41-60',
//                    valueStart: '0.41',
//                    valueEnd: '0.60'
//                },
//                {
//                    range: '61-80',
//                    valueStart: '0.61',
//                    valueEnd: '0.80'
//                },
//                {
//                    range: '81-100',
//                    valueStart: '0.81',
//                    valueEnd: '1'
//                },
//]";
//            string jsonString = JsonConvert.SerializeObject(RangePercentage);
//            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();
//            ListDetail = JsonConvert.DeserializeObject<List<RangePercentageTURDetail>>(RangePercentage);

//            for (int i = 0; i < ListDetail.Count; i++)
//            {
//                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
//                StrQuery = @"
                         
//                                select isnull(count(D.tire_code),0) as qty_tire from (
//                                select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
//                                        select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc) -
//                                        (
//                                        select top 1 cast(smod.value as float) as value  from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created desc)/(
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc))/100 as TUR 
//                                        from item i 
//                                        inner join tire t on i.tire_code = t.tire_code
//                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
//                                        inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
//		                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
//		                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
//		                                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
//		                                inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
//		                                inner join FLEET f on f.fleet_id = fv.fleet_id
//		                                where f.location_type_id='1'
//		                                group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
//                                    ) X
//                                    group by X.description, x.tire_code, x.brand_tire_name, x.size
//                                ) as D
//                                where D.TUR between '" + ListDetail[i].valueStart + "' and '" + ListDetail[i].valueEnd + "'";

//                conn.Open();
//                var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
//                ListDetail[i].qtyTire = data[0].qty_tire;

//            }

//            return Json((new { status = true, records = 0, message = "", data = ListDetail }));
//        }
        [HttpGet]
        [Route("api/Dashboard/DtsQtyScrapTireBaseOnRangeTURMineralSoil")]

        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTURMineralSoil()
        {
            var RangePercentage = new List<RangePercentageTURDetail>
            {
                new RangePercentageTURDetail
                {
                    range = "9-20",
                    valueStart = "0.09",
                    valueEnd = "0.20"
                },
                new RangePercentageTURDetail
                {
                    range = "21-40",
                    valueStart = "0.21",
                    valueEnd = "0.40"
                },
                new RangePercentageTURDetail
                {
                    range= "41-60",
                    valueStart= "0.41",
                    valueEnd= "0.60"
                },
                new RangePercentageTURDetail
                {
                    range= "61-80",
                    valueStart= "0.61",
                    valueEnd= "0.80"
                },
                new RangePercentageTURDetail
                {
                    range= "81-100",
                    valueStart= "0.81",
                    valueEnd= "1"
                },
                // ... Tambahkan item-range lainnya di sini sesuai kebutuhan.
            };

            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();

                foreach (var range in RangePercentage)
                {
                    string StrQuery = @"
                  select isnull(count(D.tire_code),0) as qty_tire from (
                                select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
                                        select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created asc) -
                                        (
                                        select top 1 cast(smod.value as float) as value  from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created desc)/(
                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
                                        order by smod.date_created asc))/100 as TUR 
                                        from item i 
                                        inner join tire t on i.tire_code = t.tire_code
                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
                                        inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
		                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
		                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
		                                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
		                                inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
		                                inner join FLEET f on f.fleet_id = fv.fleet_id
		                                where f.location_type_id='2'
		                                group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
                                    ) X
                                    group by X.description, x.tire_code, x.brand_tire_name, x.size
                                ) as D
                                where D.TUR between @valueStart AND @valueEnd";

                    var data = conn.Query(StrQuery, new
                    {
                        valueStart = range.valueStart,
                        valueEnd = range.valueEnd
                    }, commandTimeout: 0, commandType: null).ToList();

                    range.qtyTire = data[0].qty_tire;
                    ListDetail.Add(range);
                }
            }

            return Json(new { status = true, records = 0, message = "", data = ListDetail });
        }
//        public IHttpActionResult DtsQtyScrapTireBaseOnRangeTURMineralSoil()
//        {
//            string StrQuery = "";
//            string RangePercentage = @"[
//                {
//                    range: '9-20',
//                    valueStart: '0.09',
//                    valueEnd: '0.20'
//                },
//                {
//                    range: '21-40',
//                    valueStart: '0.21',
//                    valueEnd: '0.40'
//                },
//                {
//                    range: '41-60',
//                    valueStart: '0.41',
//                    valueEnd: '0.60'
//                },
//                {
//                    range: '61-80',
//                    valueStart: '0.61',
//                    valueEnd: '0.80'
//                },
//                {
//                    range: '81-100',
//                    valueStart: '0.81',
//                    valueEnd: '1'
//                },
//]";
//            string jsonString = JsonConvert.SerializeObject(RangePercentage);
//            List<RangePercentageTURDetail> ListDetail = new List<RangePercentageTURDetail>();
//            ListDetail = JsonConvert.DeserializeObject<List<RangePercentageTURDetail>>(RangePercentage);

//            for (int i = 0; i < ListDetail.Count; i++)
//            {
//                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
//                StrQuery = @"
                         
//                                select isnull(count(D.tire_code),0) as qty_tire from (
//                                select X.description, x.tire_code, x.brand_tire_name, x.size, sum(x.TUR) as TUR from (
//                                        select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, ((
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc) -
//                                        (
//                                        select top 1 cast(smod.value as float) as value  from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created desc)/(
//                                        select top 1 cast(smod.value as float) as value from scrap_tire sd
//                                        inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = sd.tire_id
//                                        inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
//                                        inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
//                                        where sd.tire_id=sd_.tire_id and oi.observation_item_id = '2'
//                                        order by smod.date_created asc))/100 as TUR 
//                                        from item i 
//                                        inner join tire t on i.tire_code = t.tire_code
//                                        inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
//                                        inner join scrap_tire sd_ on sd_.tire_id=t.tire_id
//		                                inner join tire_unit tu on tu.tire_id= t.tire_id and tu.is_delete=1
//		                                inner join VEHICLE v on v.vehicle_id = tu.vehicle_id
//		                                inner join VEHICLE_TYPE VT on VT.vehicle_type_id = v.vehicle_type_id
//		                                inner join FLEET_VEHICLE fv on fv.vehicle_id = tu.vehicle_id and FV.is_delete=0
//		                                inner join FLEET f on f.fleet_id = fv.fleet_id
//		                                where f.location_type_id='2'
//		                                group by sd_.tire_id,i.tire_code, bt.brand_tire_name, t.size
//                                    ) X
//                                    group by X.description, x.tire_code, x.brand_tire_name, x.size
//                                ) as D
//                                where D.TUR between '" + ListDetail[i].valueStart + "' and '" + ListDetail[i].valueEnd + "'";

//                conn.Open();
//                var data = conn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
//                ListDetail[i].qtyTire = data[0].qty_tire;

//            }

//            return Json((new { status = true, records = 0, message = "", data = ListDetail }));
//        }

        [HttpGet]
        [Route("api/Dashboard/DtsSummaryRepairPerGroup/{month?}")] //ppt slide 6
        public IHttpActionResult DtsSummaryRepairPerGroup(string month = null)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            //String StrQuery = @"
            //                select  char((T.N % 26) + 65) as [group],T.userid, T.Name
            //                from (
            //                     select row_number() over(order by x.userid) - 1 as N, x.userid, x.Name
            //                     from (
            //                  select smr.userid, usr.Name 
            //                  from serv_maint_repair smr
            //                  inner join AspNetUsers usr on usr.UserId = smr.userid
            //                  group by smr.userid, usr.Name 
            //                  ) as x
            //                     ) as T";

            string criteria = "";
            if (month != "0")
            {
                criteria = "  and stc.month=@month ";
            }
            String StrQuery = @"
                            select gr.group_id, gr.group_name  as [group], em.emp_id, em.emp_name as Name from GROUPS_EMPLOYEES ge
								 inner join groups gr on gr.group_id = ge.group_id
								 inner join EMPLOYEES em on em.emp_id = ge.emp_id
								 inner join SCHEDULE_TIRE_CHANGE stc on stc.group_id = ge.group_id and stc.emp_id=em.emp_id
								 where position_id='1' " + criteria + @"
								 order by ge.group_id, position_id";

            conn.Open();
            var data = conn.Query(StrQuery, new { month }, null, true, commandTimeout: 0, commandType: null).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                StrQuery = @" select isnull(sum(X.qty),0) as qty from( select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(r.tire_id) as qty
								from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join serv_maint_repair r on r.tire_id = t.tire_id
								inner join location loc on loc.location_id = r.location_id
								inner join TIRE_CHANGE tc on tc.old_tire_id=r.tire_id
								inner join SCHEDULE_TIRE_CHANGE stc on stc.tire_change_id = tc.tire_change_id
								where i.tire_code not in ('XXX') and stc.group_id=@group_id
								and r.status_id='2'
                                group by i.tire_code, bt.brand_tire_name, t.size) as X";
                var content_repair = conn.Query(StrQuery, new { group_id = data[i].group_id }, null, true, commandTimeout: 0, commandType: null).ToList();
               
                data[i].data_repair = content_repair;
                StrQuery = @"select isnull(sum(X.qty),0) as qty from( select description=i.tire_code+ ' '+t.size, i.tire_code, bt.brand_tire_name, t.size, count(r.tire_id) as qty
								from item i 
                                inner join tire t on i.tire_code = t.tire_code
                                inner join BRAND_TIRE BT on BT.brand_tire_id=i.brand_tire_id
								inner join serv_maint_repair r on r.tire_id = t.tire_id
								inner join location loc on loc.location_id = r.location_id
								inner join TIRE_CHANGE tc on tc.old_tire_id=r.tire_id
								inner join SCHEDULE_TIRE_CHANGE stc on stc.tire_change_id = tc.tire_change_id
								where i.tire_code not in ('XXX') and stc.group_id=@group_id
								and r.status_id='9'
                                group by i.tire_code, bt.brand_tire_name, t.size) as X";
                var content_patching = conn.Query(StrQuery, new { group_id = data[i].group_id }, null, true, commandTimeout: 0, commandType: null).ToList();

                data[i].data_patching = content_patching;
            }
            return Json((new { status = true, records = data.Count, message = "", data = data }));
        }

        #endregion












        //NCI VERSION
        [HttpGet]
        [Route("api/Dashboard/RptStockLocation/{year}")]
        public IHttpActionResult RptStockLocation(string year)
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
								loc.location_id not in ('XXX', 'A00','999','888', 'A01')
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
        [Route("api/Dashboard/RptStockLocationConsignment/{year}")]
        public IHttpActionResult RptStockLocationConsignment(string year)
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
								inner join TIRE_STOCK_IN tsi on tsi.tire_id = t.tire_id)) + (select count(tire_id) from TIRE_STOCK_IN where location_id=loc.location_id and year(date_created)<=@year) - (select count(tire_id) from TIRE_STOCK_OUT where location_id=loc.location_id and year(date_created)<=@year ) )  as total
                               
                                from LOCATION loc 
                                left join tire t   on t.location_id = loc.location_id
                                where 
								loc.location_id  in ('A01')
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
        // GET api/documentation
        /// <summary>
        /// Fungsi Get Scrap this Month
        /// </summary>
        /// <returns></returns>

        public class cScrapTireDetail
        {
            public string brand_tire_id { get; set; }
            public string brand_tire_name { get; set; }
            public string scrap { get; set; }
        }
        public class cScrapTire
        {
            public string year { get; set; }
            public string month { get; set; }
            public List<cScrapTireDetail> tires { get; set; }
        }
        [HttpGet]
        [Route("api/Dashboard/RptScrapThisYear/{year}")]
        public IHttpActionResult RptScrapThisYear(string year)
        {
            string[] brand_tire = new string[6];
            DataTable dtTempTable = new DataTable("item");
            string StrQuery = @"select year(sd.date_created) as year, month(sd.date_created) as month from SCRAP_TIRE sd
                                inner join tire t on sd.tire_id = t.tire_id
                                inner join item i on i.tire_code = t.tire_code
                                inner join BRAND_TIRE bt on bt.brand_tire_id = i.brand_tire_id
                                where year(sd.date_created)<=@year
                                group by year(sd.date_created), month(sd.date_created)";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@year", year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            string query2="select ";
            
            
            List<cScrapTire> listCST = new List<cScrapTire>();
            if (dtTempTable.Rows.Count > 0)
            {
                int a = 0;
                
                foreach (DataRow item in dtTempTable.Rows)
                {
                    cScrapTire cST = new cScrapTire();
                    cST.year = item[0].ToString();
                    cST.month = item[1].ToString();
                    List<cScrapTireDetail> listScrapTireDetail = new List<cScrapTireDetail>();
                    DataTable dtTempTableDetail = new DataTable("item");
                    string StrQueryDetail = @"select year(sd.date_created) as year, month(sd.date_created) as month,i.brand_tire_id,upper(bt.brand_tire_name) as brand_tire_name, COUNT(i.brand_tire_id) as scrap from SCRAP_TIRE sd
                                    inner join tire t on sd.tire_id = t.tire_id
                                    inner join item i on i.tire_code = t.tire_code
                                    inner join BRAND_TIRE bt on bt.brand_tire_id = i.brand_tire_id
                                    where year(sd.date_created)<=@years and month(sd.date_created)=@months
                                    group by year(sd.date_created), month(sd.date_created),i.brand_tire_id,bt.brand_tire_name
                                             ";
                    SqlConnection conDetail = new SqlConnection();
                    conDetail.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                    SqlCommand cmdDetail = new SqlCommand();
                    cmdDetail.CommandText = StrQueryDetail;
                    cmdDetail.Connection = conDetail;
                    cmdDetail.Parameters.AddWithValue("@years", cST.year);
                    cmdDetail.Parameters.AddWithValue("@months", cST.month);
                    SqlDataAdapter daDetail = new SqlDataAdapter(cmdDetail);
                    daDetail.SelectCommand.CommandTimeout = 0;
                    conDetail.Open();
                    daDetail.Fill(dtTempTableDetail);
                    conDetail.Close();
                    foreach (DataRow itemDetail in dtTempTableDetail.Rows)
                    {
                        cScrapTireDetail cD = new cScrapTireDetail();
                        cD.brand_tire_id = itemDetail[2].ToString();
                        cD.brand_tire_name = itemDetail[3].ToString();
                        cD.scrap = itemDetail[4].ToString();
                        listScrapTireDetail.Add(cD);
                    }
                    cST.tires = listScrapTireDetail;
                    listCST.Add(cST);


                 }
            }
            
            return Json((new { records = dtTempTable.Rows.Count, data = listCST }));
        }
        [HttpGet]
        [Route("api/Dashboard/RptScrapReasonPercentage/{year}")]
        public IHttpActionResult RptScrapReasonPercentage(string year)
        {
            string StrQuery = @"
                                
                            select
                                year(sd.date_created) as date, r.reason_id, r.reason_name,count(sd.tire_id) as value 
                                from SCRAP_TIRE SD
                                right join reason r on r.reason_id = SD.reason_id 
                                where r.reason_id<>'0'
                                and year(sd.date_created)<=@year
                                group by year(sd.date_created), r.reason_id, r.reason_name";
            //StrQuery += "') AND (MONTH(i.date_created) = '" + month + "') GROUP BY i.tire_id, ida.reason_id, ida.repair_category_id, r.reason) AS x GROUP BY reason";
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year= year}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }
        }
        [HttpGet]
        [Route("api/Dashboard/RptScrapThisMonth/{year}/{month}")]
        public IHttpActionResult RptScrapThisMonth(string year, string month)
        {
            string StrQuery = @"SELECT        SUM(jml) AS total, reason
                FROM(SELECT        COUNT(ida.reason_id) AS jml, r.reason
                                          FROM            dbo.INSPECTION AS i INNER JOIN
                                                                    dbo.INSPECTION_DETAIL_ACTION AS ida ON i.inspection_no = ida.inspection_no INNER JOIN
                                                                    dbo.REASON AS r ON ida.reason_id = r.reason_id
                                          WHERE(ida.repair_category_id = 3) AND (YEAR(i.date_created) = @year";
            StrQuery += ") AND (MONTH(i.date_created) = @month) GROUP BY i.tire_id, ida.reason_id, ida.repair_category_id, r.reason) AS x GROUP BY reason";
            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year, month}, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data Scrap Last Month
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Dashboard/RptScrapLastMonth/{year}/{month}")]
        public IHttpActionResult RptScrapLastMonth(string year, string month)
        {
            string StrQuery = @"SELECT        SUM(jml) AS total, reason
                        FROM            (SELECT        COUNT(ida.reason_id) AS jml, r.reason
                          FROM            dbo.INSPECTION AS i INNER JOIN
                                                    dbo.INSPECTION_DETAIL_ACTION AS ida ON i.inspection_no = ida.inspection_no INNER JOIN
                                                    dbo.REASON AS r ON ida.reason_id = r.reason_id
                          WHERE        (ida.repair_category_id = 3) AND (YEAR(i.date_created) = @year" ;
            StrQuery += ") AND (MONTH(i.date_created) = @month) GROUP BY i.tire_id, ida.reason_id, ida.repair_category_id, r.reason) AS x GROUP BY reason";
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year, month }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data Scrap per month
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Dashboard/RptScrapPerMonth/{year}")]
        public IHttpActionResult RptScrapPerMonth(string year)
        {
            string StrQuery = "select * from (" + System.Environment.NewLine;
            StrQuery += "	select m_id=1,m='Jan', scrap=[dbo].[GetCountScrapPerMonth](@year,1)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=2,m='Feb', scrap=[dbo].[GetCountScrapPerMonth](@year,2)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=3,m='Mar', scrap=[dbo].[GetCountScrapPerMonth](@year,3)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=4,m='Apr', scrap=[dbo].[GetCountScrapPerMonth](@year,4)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=5,m='May', scrap=[dbo].[GetCountScrapPerMonth](@year,5)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=6,m='Jun', scrap=[dbo].[GetCountScrapPerMonth](@year,6)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=7,m='Jul', scrap=[dbo].[GetCountScrapPerMonth](@year,7)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=8,m='Aug', scrap=[dbo].[GetCountScrapPerMonth](year(getdate()),8)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=9,m='Sept', scrap=[dbo].[GetCountScrapPerMonth](year(getdate()),9)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=10,m='Oct', scrap=[dbo].[GetCountScrapPerMonth](year(getdate()),10)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=11,m='Nov', scrap=[dbo].[GetCountScrapPerMonth](year(getdate()),11)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=12,m='Dec', scrap=[dbo].[GetCountScrapPerMonth](year(getdate()),12)" + System.Environment.NewLine;
            StrQuery += ")x" + System.Environment.NewLine;
            StrQuery += "order by m_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data Scrap Cost per month
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Dashboard/RptCostPerMonth/{year}")]
        public IHttpActionResult RptCostPerMonth(string year)
        {
            string StrQuery = "select * from (" + System.Environment.NewLine;
            StrQuery += "	select m_id=1,m='Jan', cost=[dbo].[GetCostScrapPerMonth](@year,1)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=2,m='Feb', cost=[dbo].[GetCostScrapPerMonth](@year,2)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=3,m='Mar', cost=[dbo].[GetCostScrapPerMonth](@year,3)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=4,m='Apr', cost=[dbo].[GetCostScrapPerMonth](@year,4)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=5,m='May', cost=[dbo].[GetCostScrapPerMonth](@year,5)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=6,m='Jun', cost=[dbo].[GetCostScrapPerMonth](@year,6)" + System.Environment.NewLine;
            StrQuery += "	union" + System.Environment.NewLine;
            StrQuery += "	select m_id=7,m='Jul', cost=[dbo].[GetCostScrapPerMonth](@year,7)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=8,m='Aug', cost=[dbo].[GetCostScrapPerMonth](year(getdate()),8)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=9,m='Sept', cost=[dbo].[GetCostScrapPerMonth](year(getdate()),9)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=10,m='Oct', cost=[dbo].[GetCostScrapPerMonth](year(getdate()),10)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=11,m='Nov', cost=[dbo].[GetCostScrapPerMonth](year(getdate()),11)" + System.Environment.NewLine;
            //StrQuery += "	union" + System.Environment.NewLine;
            //StrQuery += "	select m_id=12,m='Dec', cost=[dbo].[GetCostScrapPerMonth](year(getdate()),12)" + System.Environment.NewLine;
            StrQuery += ")x" + System.Environment.NewLine;
            StrQuery += "order by m_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        //// GET api/documentation
        ///// <summary>
        ///// Fungsi get data Dashboard General Stock
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        ////[Route("api/Report/getDashboardGeneralStock")]
        //public IHttpActionResult getDashboardGeneralAsset()
        //{

        //    DataTable data = Dashboard.getDataDashboardGeneralStock();
        //    return Json((new { records = data.Rows.Count, data = data }));

        //}


        //// GET api/documentation
        ///// <summary>
        ///// Fungsi get data Dashboard Stock Per Category
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        ////[Route("api/Report/getDashboardGeneralStock")]
        //public IHttpActionResult getDashboardAssetPerCategory()
        //{
        //    DataTable data = Dashboard.getDataDashboardStockPerCategory();
        //    return Json((new { records = data.Rows.Count, data = data }));

        //}

        
    }
}
