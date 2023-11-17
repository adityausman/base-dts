using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using static BASE_RFID_TMS.Models.Report;
using System.Data;
using BASE_RFID_TMS.Models;

namespace BASE_RFID_TMS.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [GetTimeOutSystem]
    [Authorize]
    public class ReportController : ApiController
    {
        private string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();

# region LIST DATA
        // GET api/documentation
        /// <summary>
        /// Fungsi Get data tire by serial number
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getListInspectionObservationPerTire/{sn}")]
        public IHttpActionResult getListInspectionPerTire(string sn)
        {
           string StrQuery = "select i.date_created,i.tire_id,i.location_id,i.vehicle_id,i.axis_id,i.inspection_no," + System.Environment.NewLine;
            StrQuery += "i.tire_mileage,i.air_pressure,i.tread_depth,i.userid,i.notes,o.observation_id, ob.observation,o.notes as observation_notes " + System.Environment.NewLine;
            StrQuery += "from INSPECTION i " + System.Environment.NewLine;
            StrQuery += "left join INSPECTION_DETAIL_OBSERVATION o on i.inspection_no=o.inspection_no" + System.Environment.NewLine;
            StrQuery += "left join OBSERVATION ob on o.observation_id=ob.observation_id" + System.Environment.NewLine;
            StrQuery += "where i.tire_id=@sn" + System.Environment.NewLine;
            StrQuery += "order by i.date_created desc" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { sn = sn }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        [Route("api/Report/getListInspectionActionPerTire/{sn}")]
        public IHttpActionResult getListInspectionActionPerTire(string sn)
        {
            string StrQuery = "select i.date_created,i.tire_id,i.location_id,i.vehicle_id,i.axis_id,i.inspection_no," + System.Environment.NewLine;
            StrQuery += "i.tire_mileage,i.air_pressure,i.tread_depth,i.userid,i.notes,a.repair_category_id, rc.repair_name,a.notes as action_notes,a.reason_id,rs.reason " + System.Environment.NewLine;
            StrQuery += "from INSPECTION i " + System.Environment.NewLine;
            StrQuery += "left join INSPECTION_DETAIL_ACTION a on i.inspection_no=a.inspection_no" + System.Environment.NewLine;
            StrQuery += "left join REPAIR_CATEGORY rc on a.repair_category_id=rc.repair_category_id" + System.Environment.NewLine;
            StrQuery += "left join REASON rs on a.reason_id=rs.reason_id" + System.Environment.NewLine;
            StrQuery += "where i.tire_id=@sn" + System.Environment.NewLine;
            StrQuery += "order by i.date_created desc" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { sn = sn }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data inspection by vehicle_id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getListInspectionObservationPerVehicle/{vehicle_id}")]
        public IHttpActionResult getListInspectionPerVehicle(string vehicle_id)
        {
            string StrQuery = "select i.date_created,i.tire_id,i.location_id,i.vehicle_id,i.axis_id,i.inspection_no," + System.Environment.NewLine;
            StrQuery += "i.tire_mileage,i.air_pressure,i.tread_depth,i.userid,i.notes,o.observation_id, ob.observation,o.notes as observation_notes " + System.Environment.NewLine;
            StrQuery += "from INSPECTION i " + System.Environment.NewLine;
            StrQuery += "left join INSPECTION_DETAIL_OBSERVATION o on i.inspection_no=o.inspection_no" + System.Environment.NewLine;
            StrQuery += "left join OBSERVATION ob on o.observation_id=ob.observation_id" + System.Environment.NewLine;
            StrQuery += "where i.vehicle_id=@vehicle_id" + System.Environment.NewLine;
            StrQuery += "order by i.date_created desc" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data inspection by vehicle
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getListInspectionActionPerVehicle/{vehicle_id}")]
        public IHttpActionResult getListInspectionActionPerVehicle(string vehicle_id)
        {
            string StrQuery = "select i.date_created,i.tire_id,i.location_id,i.vehicle_id,i.axis_id,i.inspection_no," + System.Environment.NewLine;
            StrQuery += "i.tire_mileage,i.air_pressure,i.tread_depth,i.userid,i.notes,a.repair_category_id, rc.repair_name,a.notes as action_notes,a.reason_id,rs.reason " + System.Environment.NewLine;
            StrQuery += "from INSPECTION i " + System.Environment.NewLine;
            StrQuery += "left join INSPECTION_DETAIL_ACTION a on i.inspection_no=a.inspection_no" + System.Environment.NewLine;
            StrQuery += "left join REPAIR_CATEGORY rc on a.repair_category_id=rc.repair_category_id" + System.Environment.NewLine;
            StrQuery += "left join REASON rs on a.reason_id=rs.reason_id" + System.Environment.NewLine;
            StrQuery += "where i.vehicle_id=@vehicle_id" + System.Environment.NewLine;
            StrQuery += "order by i.date_created desc" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { vehicle_id = vehicle_id }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get data List Registered Tire
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getListTireRegistered/{date_created}/{userid}")]
        public IHttpActionResult getListTireRegistered(string date_created, string userid)
        {

            DateTime dt = DateTime.Now;
            string dtSTring = dt.ToString("yyyy-MM-dd");
            if (date_created != "0" && date_created != "-" && date_created != "")
            {
                dt = Convert.ToDateTime(date_created);
                dtSTring = dt.ToString("yyyy-MM-dd");
            }
            else
            {
                dtSTring = "0";
            }
            string roleUser = getRoleUser(userid);
            string StrQuery = @"select R.tire_id,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code,t.size,t.pattern,i.brand_tire_id,b.brand_tire_name,t.serial_number,
                                R.date_created,str_date_created=convert(varchar,R.date_created,106),time_date_created=convert(varchar,R.date_created,108),
                                R.location_id,R.register_tire_no,tu.vehicle_id,R.userid,R.reg_rfid as rfid,a.Name,a.JobPosition 
                                from REGISTER_TIRE R
                                inner join TIRE t on R.tire_id=t.tire_id
                                inner join TIRE_UNIT tu on tu.tire_id = t.tire_id and tu.is_delete = 0
                                inner join item i on i.tire_code = t.tire_code
                                inner join BRAND_TIRE b on i.brand_tire_id=b.brand_tire_id
                                inner join AspNetUsers a on r.userid=a.UserId" + System.Environment.NewLine;
            StrQuery += "where r.tire_id <>'0'" + System.Environment.NewLine;
            if (date_created != "0" && date_created != "-" && date_created != "")
            {
                StrQuery += "and cast(r.date_created as date)=cast(@date_created as date)" + System.Environment.NewLine;
            }

            if (roleUser != "0" && roleUser != "-" && roleUser != "" )
            {
                StrQuery += "and r.userid=@userid" + System.Environment.NewLine;
            }
            
            StrQuery += "order by R.date_created desc, R.tire_id asc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { userid , date_created }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get List Tire
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getListTireDetail/{serial_number}/{tire_name}/{supplier_id}/{brand_id}/{vehicle_id}")]
        public IHttpActionResult getListTireDetail(string serial_number, string tire_name, string supplier_id, string brand_id, string vehicle_id)
        {
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(qGetListTire(serial_number, tire_name, supplier_id, brand_id, vehicle_id), null, null, true, commandTimeout: 0, commandType: null).ToList();
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
        [Route("api/Report/getListVehicleDetail/{vehicle_id}/{vehicle_name}/{vehicle_no}")]
        public IHttpActionResult getListVehicleDetail(string vehicle_id, string vehicle_name, string vehicle_no)
        {
            string prm = "";
            string StrQuery = @"select x.*,fv.vehicle_id,fv.position as sort from (
	                                select G.register_vhc_no,convert(varchar,G.date_created,106) as register_date,G.reg_rfid,
	                                v.vehicle_id,v.vehicle_name,vehicle_setup_no=v.vehicle_id,v.axis_type,ae.axis_type_name--,
                                   --next_inspection_date=isnull(convert(varchar,[dbo].[GetNextInspectionDateByVehicle](v.vehicle_id),106),'-'),next_inspection_status=isnull(dbo.[GetStatusTireInspectionDateByVehicle](v.vehicle_id),'Never Been Inspected'),next_inspection_day=isnull(dbo.[GetStatusTireInspectionDateDayByVehicle](v.vehicle_id),'-'),last_inspection_date=isnull([dbo].[GetLastInspectionDateByVehicle](v.vehicle_id),'-') 
                                    from
	                                VEHICLE V
	                                LEFT join AXIS_TYPE AE ON v.axis_type=AE.axis_type
	                                LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id
	                                where v.vehicle_id<>'0'
                                " + System.Environment.NewLine;
            //StrQuery += "	where v.vehicle_id<>'0'" + System.Environment.NewLine;
            if (vehicle_id!="0" && vehicle_id != "-" && vehicle_id != "")
            {
                prm = "	and v.vehicle_id=@vehicle_id" + System.Environment.NewLine;
            }

            if (vehicle_name != "0" && vehicle_name != "-" && vehicle_name != "")
            {
                prm += "	and v.vehicle_name like @vehicle_name" + System.Environment.NewLine;
                

            }

            if (vehicle_no != "0" && vehicle_no != "-" && vehicle_no != "")
            {
                prm += "	and v.vehicle_no like @vehicle_no" + System.Environment.NewLine;
               
            }
            StrQuery += prm;
            StrQuery += @")x
                            inner join FLEET_VEHICLE FV on x.vehicle_id=FV.vehicle_id and FV.is_delete=0
                            order by fv.fleet_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new
                {
                    vehicle_id = vehicle_id,
                    vehicle_name = "%" + vehicle_name + "%",
                    vehicle_no = "%" + vehicle_no + "%"
                }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #endregion


        #region Report

        // GET api/documentation
        /// <summary>
        /// Fungsi Get Rpt Tire Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptTireSummary/{supplier_id}/{brand_tire_id}")]
        public IHttpActionResult getRptTireSummary(string supplier_id, string brand_tire_id)
        {
            string StrQuery = @"select t.tire_code,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code,i.size,i.pattern,i.brand_tire_id,b.brand_tire_name,i.supplier_id,s.supplier_name, sum(1) as qty from tire t
                                inner join item i on i.tire_code = t.tire_code
                                inner join SUPPLIER S on i.supplier_id=S.supplier_id
                                INNER JOIN BRAND_TIRE B on i.brand_tire_id=B.brand_tire_id" + System.Environment.NewLine;
            StrQuery += "where t.tire_id !='0' and t.tire_id not in ('XXX')" + System.Environment.NewLine;
            if (supplier_id != "0" && supplier_id != "" && supplier_id != "-")
            {
                StrQuery += "and i.supplier_id=@supplier_id" + System.Environment.NewLine;
            }
            if (brand_tire_id != "0" && brand_tire_id != "" && brand_tire_id != "-")
            {
                StrQuery += "and i.brand_tire_id=@brand_tire_id" + System.Environment.NewLine;
            }
            //StrQuery += "and t.supplier_id='S002'" + System.Environment.NewLine;
            //StrQuery += "and t.brand_id='1'" + System.Environment.NewLine;
            StrQuery += "group by t.tire_code,i.unique_code, i.tire_code,i.size,i.pattern,i.brand_tire_id,b.brand_tire_name,i.supplier_id,s.supplier_name" + System.Environment.NewLine;
            StrQuery += "order by t.tire_code " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { supplier_id = supplier_id, brand_tire_id = brand_tire_id }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get Rpt Tire Detail
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Route("api/Report/getRptTireDetail/{tire_code}")]
        //public IHttpActionResult getRptTireDetail(string tire_code)
        //{
        //    //string StrQuery = "select R.po_no,convert(varchar,R.po_date,106) as po_date,R.do_no,convert(varchar,R.do_date,106) as do_date,T.tire_id,t.serial_number,t.tire_name," + System.Environment.NewLine;
        //    //StrQuery += "t.size,t.pattern,t.brand_id,B.brand_name,t.supplier_id,s.supplier_name,G.register_no,convert(varchar,G.date_created,106) as register_date,G.rfid," + System.Environment.NewLine;
        //    //StrQuery += "t.vehicle_id,v.vehicle_name,v.vehicle_no,v.is_vehicle,vehicle_setup_no=[dbo].[GetLastVehicleSetupNo](t.vehicle_id),t.axis_id,a.tire_no,a.axis_no,a.axis_position,a.axis_type,ae.axis_type_name,a.tire_axis_position,a.description as tire_position,a.axis_name," + System.Environment.NewLine;
        //    //StrQuery += "t.location_id,l.location,t.process_id,p.process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.tire_mileage,tire_axis_no=[dbo].[GetLastTireAxisNoByTire](t.tire_id)," + System.Environment.NewLine;
        //    //StrQuery += "status_id=T.status_id,next_inspection_date=isnull(convert(varchar,[dbo].[GetNextInspectionDate](t.tire_id),106),'-'),next_inspection_status=isnull(dbo.[GetStatusTireInspectionDate](t.tire_id),'Never Been Inspected'),next_inspection_day=isnull(dbo.[GetStatusTireInspectionDateDay](t.tire_id),'-'),last_inspection_date=isnull([dbo].[GetLastInspectionDate](t.tire_id),'-') from " + System.Environment.NewLine;
        //    //StrQuery += "TIRE T" + System.Environment.NewLine;
        //    //StrQuery += "left join axis a on t.axis_id=a.axis_id" + System.Environment.NewLine;
        //    //StrQuery += "LEFT join AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
        //    //StrQuery += "inner join VEHICLE v on t.vehicle_id=v.vehicle_id" + System.Environment.NewLine;
        //    //StrQuery += "--inner join TRAILER tr on t.trailer_id=tr.trailer_id" + System.Environment.NewLine;
        //    //StrQuery += "inner join SUPPLIER S on T.supplier_id=S.supplier_id" + System.Environment.NewLine;
        //    //StrQuery += "LEFT JOIN BRAND B on T.brand_id=B.brand_id" + System.Environment.NewLine;
        //    //StrQuery += "inner join location l on t.location_id=l.location_id" + System.Environment.NewLine;
        //    //StrQuery += "inner join process p on t.process_id=p.process_id" + System.Environment.NewLine;
        //    //StrQuery += "LEFT JOIN REGISTER G on T.tire_id=G.tire_id" + System.Environment.NewLine;
        //    //StrQuery += "LEFT JOIN RECEIVE_DETAIL RD on RD.serial_number=T.tire_id" + System.Environment.NewLine;
        //    //StrQuery += "LEFT JOIN RECEIVE R on RD.receive_no=R.receive_no" + System.Environment.NewLine;
        //    string StrQuery = @"select R.po_no, convert(varchar,R.po_date,106)  as po_date,R.do_no,convert(varchar,R.do_date,106) as do_date,
		      //              T.tire_id,t.serial_number,t.tire_name,
        //                    t.size,t.pattern,I.brand_id,B.brand_name,I.supplier_id,s.supplier_name,G.register_no,convert(varchar,G.date_created,106) as register_date,G.rfid,
        //                    tu.vehicle_id,v.vehicle_name,vehicle_setup_no=V.fleet_id,ta.axis_id,a.tire_no,a.axis_no,a.axis_position,a.axis_type,aty.axis_type_name,a.tire_axis_position,a.description as tire_position,
        //                    t.location_id,l.location,0 as process_id,'' as process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.tire_mileage,tire_axis_no,
        //                    status_id=T.status_id from TIRE T  " + System.Environment.NewLine;
        //    StrQuery += @"LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id
        //                LEFT JOIN TIRE_AXIS TA ON TA.tire_unit_id = TU.tire_unit_id
        //                LEFT JOIN VEHICLE V ON V.vehicle_id = TU.vehicle_id
        //                LEFT JOIN AXIS A On A.axis_id = TA.axis_id
        //                LEFT JOIN AXIS_TYPE ATY ON ATY.axis_type = A.axis_type

        //                INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
        //                INNER JOIN  REGISTER AS G ON T.tire_id = G.tire_id 
        //                INNER JOIN RECEIVE_DETAIL AS RD ON RD.serial_number = T.tire_id 
        //                INNER JOIN RECEIVE AS R ON RD.receive_no = R.receive_no
        //                INNER JOIN ITEM I ON I.tire_code = T.tire_code
        //                INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
        //                INNER JOIN BRAND AS B ON I.brand_id = B.brand_id " + System.Environment.NewLine;
        //    //StrQuery += "where T.serial_number=@sn OR" + System.Environment.NewLine;
        //    //StrQuery += "T.rfid =@sn" + System.Environment.NewLine;
        //    if (tire_code != "0" && tire_code != "" && tire_code != "-")
        //    {
        //        StrQuery += "where t.tire_code=@StrQuery" + System.Environment.NewLine;
        //    }
        //    StrQuery += "order by t.serial_number " + System.Environment.NewLine;

        //    try
        //    {
        //        SqlConnection dbConn = new SqlConnection(con);
        //        dbConn.Open();
        //        var data = dbConn.Query(StrQuery, new { StrQuery = tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
        //        dbConn.Close();
        //        return Json((new { records = data.Count, data = data }));
        //    }

        //    catch (Exception e)
        //    {
        //        return Json((new { msg = e.Message.ToString(), data = "" }));
        //    }

        //}

        // GET api/documentation
        /// <summary>
        /// Fungsi Get report receive header
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptReceive/{date_start}/{date_end}/{supplier_id}")]
        public IHttpActionResult getRptReceive(string date_start, string date_end, string supplier_id)
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

            string prm = "";
            string StrQuery = "select R.receive_no,convert(varchar,R.date_created,106) as date_created,R.po_no,convert(varchar,R.po_date,106) as po_date," + System.Environment.NewLine;
            StrQuery += "R.do_no,convert(varchar,R.do_date,106) as do_date,R.supplier_id,S.supplier_name,R.userid,a.name,R.description" + System.Environment.NewLine;
            StrQuery += "from RECEIVE R" + System.Environment.NewLine;
            StrQuery += "--inner join RECEIVE_DETAIL_QTY RD on R.receive_no=RD.receive_no" + System.Environment.NewLine;
            StrQuery += "inner join AspNetUsers a on R.userid=a.UserId" + System.Environment.NewLine;
            StrQuery += "inner join SUPPLIER S on S.supplier_id=R.supplier_id" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(r.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            if (supplier_id != "0" && supplier_id != "-" && supplier_id != "")
            {
                if (prm != "")
                {
                    prm += "and R.supplier_id=@supplier_id" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where R.supplier_id=@supplier_id" + System.Environment.NewLine;
                }
                
            }
            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += "order by R.date_created desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, supplier_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterLooseRdl/{date_start}/{date_end}")]
        public IHttpActionResult getRptLetterLooseRdl(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"select * from (
                        select LL.letter_loose_no, LL.letter_loose_date, ll.location_id, loc.location,
						lld.axis_id, lld.tire_id,
                        loc.location_type_id, lt.location_type, ll.vehicle_id, v.vehicle_name, ll.shift_group_mech, ll.mechanic, ll.description, ll.photo,
                        aus.Name
                        ,case when (select count(letter_loose_no) from LETTER_LOOSE_DETAIL lld2 where lld2.letter_loose_no=ll.letter_loose_no and lld2.letter_loose_date=ll.letter_loose_date) = (select count(letter_loose_no) from LETTER_FOUND_BY_LOOSE lfl where lfl.letter_loose_no=ll.letter_loose_no and lfl.letter_loose_date=ll.letter_loose_date)
						then 1
						else
						0
						end as is_closed_header
                        ,CASE
								WHEN lfl.tire_id IS NULL THEN 0 
								ELSE 1
								END as is_closed_detail, '' as reff
                        from LETTER_LOOSE LL
                        inner join VEHICLE v on v.vehicle_id = LL.vehicle_id
						inner join LETTER_LOOSE_DETAIL LLD on LLD.letter_loose_no=LL.letter_loose_no
                        inner join location loc on loc.location_id = LL.location_id
                        inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                        inner join AspNetUsers aus on aus.UserId = ll.userid
                        left join LETTER_FOUND_BY_LOOSE lfl on lfl.letter_loose_no = ll.letter_loose_no and lfl.letter_loose_date = ll.letter_loose_date and lfl.tire_id = lld.tire_id
                        where ll.is_delete = 0 
						UNION ALL
						select LL.letter_dismantle_no as letter_loose_no, LL.letter_dismantle_date as letter_loose_date, ll.location_id, loc.location,
						lld.axis_id, lld.tire_id,
                        loc.location_type_id, lt.location_type, ll.vehicle_id, v.vehicle_name, ll.shift_group_mech, ll.mechanic, ll.description, ll.photo, 
                                    aus.Name, 
	                                CASE
                                        WHEN (select count(letter_dismantle_no) from LETTER_DISMANTLE_DETAIL ldd where ldd.letter_dismantle_no=ll.letter_dismantle_no and ldd.letter_dismantle_date = ll.letter_dismantle_date) =  
		                                (select count(letter_dismantle_no) from LETTER_DISMANTLE_DETAIL ldd where ldd.letter_dismantle_no=ll.letter_dismantle_no and ldd.letter_dismantle_date = ll.letter_dismantle_date and ldd.is_closed=1)
		                                THEN 1
                                        ELSE 0
                                    END AS is_closed_header,
									CASE
                                        WHEN lld.is_closed = 1
		                                THEN 1
                                        ELSE 0
                                    END AS is_closed_detail, lld.reff_tire_id as reff
	                                from LETTER_DISMANTLE LL
                                    inner join VEHICLE v on v.vehicle_id = LL.vehicle_id
									inner join LETTER_DISMANTLE_DETAIL LLD on LLD.letter_dismantle_no=LL.letter_dismantle_no and lld.letter_dismantle_date = ll.letter_dismantle_date
                                    inner join location loc on loc.location_id = LL.location_id
                                    inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                                    inner join AspNetUsers aus on aus.UserId = ll.userid) as X" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(X.letter_loose_date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += "order by X.letter_loose_date desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterLoose/{date_start}/{date_end}")]
        public IHttpActionResult getRptLetterLoose(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"select * from(
                                select LL.letter_loose_no, LL.letter_loose_date, ll.location_id, loc.location,
                                    loc.location_type_id, lt.location_type, ll.vehicle_id, v.vehicle_name, ll.shift_group_mech, ll.mechanic, isnull(ll.description,'') as description, ll.photo,
                                    aus.Name,
	                                case when (select count(letter_loose_no) from LETTER_LOOSE_DETAIL lld2 where lld2.letter_loose_no=ll.letter_loose_no and lld2.letter_loose_date=ll.letter_loose_date) = (select count(letter_loose_no) from LETTER_FOUND_BY_LOOSE lfl where lfl.letter_loose_no=ll.letter_loose_no and lfl.letter_loose_date=ll.letter_loose_date)
	                                then 1
	                                else
	                                0
	                                end as is_closed
                                    from LETTER_LOOSE LL
                                    inner join VEHICLE v on v.vehicle_id = LL.vehicle_id
                                    inner join location loc on loc.location_id = LL.location_id
                                    inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                                    inner join AspNetUsers aus on aus.UserId = ll.userid
	                                UNION ALL
	                                select LL.letter_dismantle_no as letter_loose_no, LL.letter_dismantle_date as letter_loose_date, ll.location_id, loc.location,
                                    loc.location_type_id, lt.location_type, ll.vehicle_id, v.vehicle_name, ll.shift_group_mech, ll.mechanic, isnull(ll.description,'') as description, ll.photo,
                                    aus.Name, 
	                                CASE
                                        WHEN (select count(letter_dismantle_no) from LETTER_DISMANTLE_DETAIL ldd where ldd.letter_dismantle_no=ll.letter_dismantle_no and ldd.letter_dismantle_date = ll.letter_dismantle_date) =  
		                                (select count(letter_dismantle_no) from LETTER_DISMANTLE_DETAIL ldd where ldd.letter_dismantle_no=ll.letter_dismantle_no and ldd.letter_dismantle_date = ll.letter_dismantle_date and ldd.is_closed=1)
		                                THEN 1
                                        ELSE 0
                                    END AS is_closed
	                                from LETTER_DISMANTLE LL
                                    inner join VEHICLE v on v.vehicle_id = LL.vehicle_id
                                    inner join location loc on loc.location_id = LL.location_id
                                    inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                                    inner join AspNetUsers aus on aus.UserId = ll.userid) as X
	                                

                         " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(X.letter_loose_date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            
            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += "order by X.letter_loose_date desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterLooseDetail/{letter_loose_no}")]
        public IHttpActionResult getRptLetterLooseDetail(string letter_loose_no)
        {
            

            string prm = "";
            string StrQuery = @"
                            
							select * from (
	                        select LL.letter_loose_no, LL.letter_loose_date,
						    ll.axis_id, ll.tire_id, ll.date_created, ll.is_delete, ll.date_modified, ll.userid,CASE
								    WHEN lfl.tire_id IS NULL THEN 0 
								    ELSE 1
								    END as is_closed, '' as reff from LETTER_LOOSE_DETAIL LL
						    left join LETTER_FOUND_BY_LOOSE lfl on lfl.letter_loose_no = ll.letter_loose_no and lfl.letter_loose_date = ll.letter_loose_date and lfl.tire_id = ll.tire_id

						    UNION ALL
						    select LL.letter_dismantle_no as letter_loose_no, LL.letter_dismantle_date as letter_loose_date,
						    ll.axis_id, ll.tire_id, ll.date_created, ll.is_delete, ll.date_modified, ll.userid,CASE
								    WHEN LL.is_closed = 0 THEN 0 
								    ELSE 1
								    END as is_closed, isnull(ll.reff_tire_id,'') as reff from LETTER_DISMANTLE_DETAIL LL) as X


                            where X.letter_loose_no=@letter_loose_no  " + System.Environment.NewLine;
          
            
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { letter_loose_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterFoundRdl/{date_start}/{date_end}")]
        public IHttpActionResult getRptLetterFoundRdl(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"
                        
select LF.letter_found_no, LF.letter_found_date, lF.location_id, loc.location,
						LFD.tire_id,
                        loc.location_type_id, lt.location_type, LF.shift_group_mech, LF.finder, LF.qty_found, LF.description, LF.photo,
                        aus.Name
                        from LETTER_FOUND LF
                        inner join location loc on loc.location_id = LF.location_id
						inner join LETTER_FOUND_DETAIL LFD on LFD.letter_found_no=LF.letter_found_no
                        inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                        inner join AspNetUsers aus on aus.UserId = lf.userid

                        where lf.is_delete = 0 " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "and cast(LF.letter_found_date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += "order by LF.letter_found_date desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterFound/{date_start}/{date_end}")]
        public IHttpActionResult getRptLetterFound(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"
                        select LF.letter_found_no, LF.letter_found_date, lF.location_id, loc.location,
                        loc.location_type_id, lt.location_type, LF.shift_group_mech, LF.finder, LF.qty_found, LF.description, LF.photo,
                        aus.Name
                        from LETTER_FOUND LF
                        inner join location loc on loc.location_id = LF.location_id
                        inner join LOCATION_TYPE LT on Lt.location_type_id = loc.location_type_id
                        inner join AspNetUsers aus on aus.UserId = lf.userid

                        where lf.is_delete = 0 " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "and cast(LF.letter_found_date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += "order by LF.letter_found_date desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptLetterFoundDetail/{letter_found_no}")]
        public IHttpActionResult getRptLetterFoundDetail(string letter_found_no)
        {


            string prm = "";
            string StrQuery = @"
                            select * from LETTER_FOUND_DETAIL LF
                            where LF.letter_found_no=@letter_found_no and is_delete=0 " + System.Environment.NewLine;


            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { letter_found_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptConsReceive/{date_start}/{date_end}/{supplier_id}")]
        public IHttpActionResult getRptConsReceive(string date_start, string date_end, string supplier_id)
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

            string prm = "";
            string StrQuery = "select R.cons_rcv_no,R.cons_rcv_date," + System.Environment.NewLine;
            StrQuery += "R.supplier_id,S.supplier_name,R.userid,a.name,R.description" + System.Environment.NewLine;
            StrQuery += "from CONSIGNMENT_RECEIVE R" + System.Environment.NewLine;
            StrQuery += "inner join CONSIGNMENT_RECEIVE_DETAIL RD on R.cons_rcv_no=RD.cons_rcv_no" + System.Environment.NewLine;
            StrQuery += "inner join AspNetUsers a on R.userid=a.UserId" + System.Environment.NewLine;
            StrQuery += "inner join SUPPLIER S on S.supplier_id=R.supplier_id" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(r.cons_rcv_date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            if (supplier_id != "0" && supplier_id != "-" && supplier_id != "")
            {
                if (prm != "")
                {
                    prm += "and R.supplier_id=@supplier_id" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where R.supplier_id=@supplier_id" + System.Environment.NewLine;
                }

            }
            StrQuery += prm;
            //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
            //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
            StrQuery += @"
                            group by R.cons_rcv_no,R.cons_rcv_date,
R.supplier_id,S.supplier_name,R.userid,a.name,R.description
                            order by R.cons_rcv_date desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, supplier_id}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptConsReceiveDetail/{receive_no}")]
        public IHttpActionResult getRptConsReceiveDetail(string receive_no)
        {

            string StrQuery = @"SELECT R.cons_rcv_no,R.cons_rcv_date,R.supplier_id, S.supplier_name, 
                         R.userid, a.Name, RD.tire_code, i.tire_name, RD.cons_qty, R.description, i.brand_tire_id, b.brand_tire_name
                        FROM CONSIGNMENT_RECEIVE AS R INNER JOIN
                         CONSIGNMENT_RECEIVE_DETAIL_QTY AS RD ON R.cons_rcv_no = RD.cons_rcv_no and R.cons_rcv_date = RD.cons_rcv_date INNER JOIN
                         ITEM AS i ON RD.tire_code = i.tire_code INNER JOIN
                         BRAND_TIRE AS b ON i.brand_tire_id = b.brand_tire_id INNER JOIN
                         AspNetUsers AS a ON R.userid = a.UserId INNER JOIN
                         SUPPLIER AS S ON S.supplier_id = R.supplier_id" + System.Environment.NewLine;

            StrQuery += "where r.cons_rcv_no=@receive_no" + System.Environment.NewLine;
            StrQuery += "order by RD.tire_code" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { receive_no = receive_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptConsReceiveDetailByDate/{date_start}/{date_end}")]
        public IHttpActionResult getRptConsReceiveDetailByDate(string date_start, string date_end)
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
            string StrQuery = @"SELECT R.cons_rcv_no,R.cons_rcv_date,R.supplier_id, S.supplier_name, 
                         R.userid, a.Name, RDE.tire_code, i.tire_name, RD.cons_qty, R.description, i.brand_tire_id, b.brand_tire_name,
						  t.tire_id, 
			t.tire_name, i.tire_type_id, tt.tire_type_name
                        FROM CONSIGNMENT_RECEIVE AS R INNER JOIN
						CONSIGNMENT_RECEIVE_DETAIL RDE on R.cons_rcv_no=RDE.cons_rcv_no inner join 
                         CONSIGNMENT_RECEIVE_DETAIL_QTY AS RD ON R.cons_rcv_no = RD.cons_rcv_no and R.cons_rcv_date = RD.cons_rcv_date and rd.tire_code = rde.tire_code INNER JOIN
						 
						 tire t on rde.tire_id = t.tire_id inner join 
                         ITEM AS i ON RD.tire_code = i.tire_code INNER JOIN
                         BRAND_TIRE AS b ON i.brand_tire_id = b.brand_tire_id INNER JOIN
                         AspNetUsers AS a ON R.userid = a.UserId INNER JOIN
                         SUPPLIER AS S ON S.supplier_id = R.supplier_id INNER JOIN
                         TIRE_TYPE AS tt ON tt.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "where cast(r.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            StrQuery += "order by r.date_created desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        public class cGetRptConsReceiveDetailBySerialNumber
        {
            public string receive_no { get; set; }
            public string tire_code { get; set; }
        }
        [HttpPost]
        [Route("api/Report/getRptConsReceiveDetailBySerialNumber")]
        public IHttpActionResult getRptConsReceiveDetailBySerialNumber([FromBody]cGetRptConsReceiveDetailBySerialNumber prm)
        {

            string StrQuery = @" select R.cons_rcv_no,convert(varchar,R.date_created,106) as date_created,R.supplier_id,S.supplier_name,R.userid,a.name,RD.tire_code,RD.tire_id, 
			t.tire_name, i.tire_type_id, tt.tire_type_name
            from CONSIGNMENT_RECEIVE R
            inner join CONSIGNMENT_RECEIVE_DETAIL RD on R.cons_rcv_no=RD.cons_rcv_no            
            inner join AspNetUsers a on R.userid=a.UserId
            inner join SUPPLIER S on S.supplier_id=R.supplier_id
			inner join tire t on rd.tire_id = t.tire_id
			inner join item i on i.tire_code=t.tire_code
			inner join TIRE_TYPE tt on tt.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            StrQuery += "where r.cons_rcv_no=@receive_no and rd.tire_code=@tire_code" + System.Environment.NewLine;
            StrQuery += "order by RD.tire_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { receive_no = prm.receive_no, tire_code= prm.tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report receive detail QTY
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptReceiveDetail/{receive_no}")]
        public IHttpActionResult getRptReceiveDetail(string receive_no)
        {
            
            string StrQuery = @"  SELECT R.receive_no, CONVERT(varchar, R.date_created, 106) AS date_created, R.po_no, CONVERT(varchar, R.po_date, 106) AS po_date, R.do_no, CONVERT(varchar, R.do_date, 106) AS do_date, R.supplier_id, S.supplier_name, 
                                       R.userid, a.Name, RD.tire_code, i.tire_name, RD.qty AS qty,
									   tt.tire_type_name, R.description, i.brand_tire_id, b.brand_tire_name, RD.price,i.pattern, i.size
                                FROM   RECEIVE AS R INNER JOIN
                                    RECEIVE_DETAIL_QTY AS RD ON R.receive_no = RD.receive_no and r.date_created = rd.date_created INNER JOIN
                                   
                                    ITEM AS i ON RD.tire_code = i.tire_code INNER JOIN
                                    BRAND_TIRE AS b ON i.brand_tire_id = b.brand_tire_id INNER JOIN
                                    AspNetUsers AS a ON R.userid = a.UserId INNER JOIN
                                    SUPPLIER AS S ON S.supplier_id = R.supplier_id INNER JOIN
                         TIRE_TYPE AS tt ON tt.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            
            StrQuery += @"where r.receive_no=@receive_no " + System.Environment.NewLine;
            StrQuery += "order by RD.tire_code" + System.Environment.NewLine;
            
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { receive_no = receive_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptReceiveDetailByDate/{date_start}/{date_end}")]
        public IHttpActionResult getRptReceiveDetailByDate(string date_start, string date_end)
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

            //string StrQuery = @"SELECT R.receive_no, CONVERT(varchar, R.date_created, 106) AS date_created, R.po_no, CONVERT(varchar, R.po_date, 106) AS po_date, R.do_no, CONVERT(varchar, R.do_date, 106) AS do_date, R.supplier_id, S.supplier_name, 
            //             R.userid, a.Name, RD.tire_code, i.tire_name, RD.qty, R.description, i.brand_tire_id, b.brand_tire_name, RD.price
            //            FROM RECEIVE AS R INNER JOIN
            //             RECEIVE_DETAIL_QTY AS RD ON R.receive_no = RD.receive_no INNER JOIN
            //             ITEM AS i ON RD.tire_code = i.tire_code INNER JOIN
            //             BRAND_TIRE AS b ON i.brand_tire_id = b.brand_tire_id INNER JOIN
            //             AspNetUsers AS a ON R.userid = a.UserId INNER JOIN
            //             SUPPLIER AS S ON S.supplier_id = R.supplier_id" + System.Environment.NewLine;
            string StrQuery = @"
                                 
								SELECT R.receive_no, CONVERT(varchar, R.date_created, 106) AS date_created, R.po_no, CONVERT(varchar, R.po_date, 106) AS po_date, R.do_no, CONVERT(varchar, R.do_date, 106) AS do_date, R.supplier_id, S.supplier_name, 
                                       R.userid, a.Name, RDE.tire_code, i.tire_name, RD.qty AS qty,
									   tt.tire_type_name, R.description, i.brand_tire_id, b.brand_tire_name, i.price,
									   i.pattern, i.size, 
									   t.tire_id, 
			                        t.tire_name, i.tire_type_id, tt.tire_type_name
                                FROM   RECEIVE AS R INNER JOIN
								RECEIVE_DETAIL RDE on R.receive_no=RDE.receive_no and R.date_created = RDE.date_created inner join 
                                    RECEIVE_DETAIL_QTY AS RD ON R.receive_no = RD.receive_no and r.date_created = rd.date_created and rd.tire_code=rde.tire_code INNER JOIN
                                   
								   tire t on rde.serial_number = t.tire_id inner join 
                                    ITEM AS i ON RDE.tire_code = i.tire_code INNER JOIN
                                    BRAND_TIRE AS b ON i.brand_tire_id = b.brand_tire_id INNER JOIN
                                    AspNetUsers AS a ON R.userid = a.UserId INNER JOIN
                                    SUPPLIER AS S ON S.supplier_id = R.supplier_id INNER JOIN
                         TIRE_TYPE AS tt ON tt.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "where cast(r.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            //if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            //{
            //    StrQuery += "and r.userid='" + userid + "'" + System.Environment.NewLine;
            //}

            //StrQuery += "order by R.date_created desc, R.tire_id asc" + System.Environment.NewLine;

            //StrQuery += "where r.receive_no=@receive_no" + System.Environment.NewLine;
            StrQuery += "order by r.date_created desc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptScrap/{date_start}/{date_end}")]
        public IHttpActionResult getRptScrap(string date_start, string date_end)
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

            string StrQuery = @"select sd.scrap_no, sd.date_created, isnull(sd.description,'') as description, sd.tire_id, isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code, r.reason_id, r.reason_name, ad.area_damage_id, ad.area_damage_name, sd.userid, aus.Name from SCRAP_TIRE sd
                                    inner join TIRE t on t.tire_id = sd.tire_id
									inner join reason r on r.reason_id = sd.reason_id
                                    left join area_damage ad on ad.area_damage_id = sd.area_damage_id
                                    inner join ITEM i on i.tire_code=t.tire_code
                                    inner join AspNetUsers aus on aus.UserId= sd.userid
                                    where 
									sd.is_delete=0" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(sd.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            
            StrQuery += "order by sd.date_created" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptDisposal/{date_start}/{date_end}")]
        public IHttpActionResult getRptDisposal(string date_start, string date_end)
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

            string StrQuery = @"select dd.disposal_no, dd.date_created, isnull(dd.description,'') as description, dd.tire_id,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code, i.tire_code, bt.brand_tire_name, dd.userid, aus.Name
                                from DISPOSAL_DETAIL dd
                                                                inner join TIRE t on t.tire_id = dd.tire_id
								                                inner join item i on i.tire_code = t.tire_code
								                                inner join BRAND_TIRE bt on bt.brand_tire_id = i.brand_tire_id
                                                                inner join AspNetUsers aus on aus.UserId= dd.userid
                                                                where 
								                                dd.is_delete=0" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(dd.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            StrQuery += "order by dd.date_created" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report receive detail SN
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Report/getRptReceiveDetailSerialNumber")]
        public IHttpActionResult getRptReceiveDetailSerialNumber([FromBody]cGetRptConsReceiveDetailBySerialNumber prm)
        {

            string StrQuery = @" select R.receive_no,convert(varchar,R.date_created,106) as date_created,R.po_no,convert(varchar,R.po_date,106) as po_date,
            R.do_no,convert(varchar,R.do_date,106) as do_date,R.supplier_id,S.supplier_name,R.userid,a.name,RD.tire_code,RD.serial_number, 
			t.tire_name, i.tire_type_id, tt.tire_type_name
            from RECEIVE R
            inner join RECEIVE_DETAIL RD on R.receive_no=RD.receive_no
            inner join AspNetUsers a on R.userid=a.UserId
            inner join SUPPLIER S on S.supplier_id=R.supplier_id
			inner join tire t on rd.serial_number = t.tire_id
			inner join item i on i.tire_code=t.tire_code
			inner join TIRE_TYPE tt on tt.tire_type_id = i.tire_type_id" + System.Environment.NewLine;
            StrQuery += "where r.receive_no=@receive_no and rd.tire_code=@tire_code" + System.Environment.NewLine;
            StrQuery += "order by RD.serial_number" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { receive_no = prm.receive_no, tire_code = prm.tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report register
        /// </summary>
        /// <returns></returns>

        public static string getRoleUser(string userid)
        {
            string roleid = "";
            DataTable dtTempTable = new DataTable("EPC_TIRE");
            string StrQuery = @"select zr.RoleId from AspNetUsers aus
                                inner join ZUSER_ROLES zr on zr.Id = aus.Id
                                where userid = @userid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@userid", userid);
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
        [HttpGet]
        [Route("api/Report/getRptFleet/{date_start}/{date_end}")]
        public IHttpActionResult getRptFleet(string date_start, string date_end)
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
            string StrQuery = @"
				                select fv.date_modified as date, fv.fleet_id,v.vehicle_id,v.vehicle_name,f.km, f.hm,
	                                    v.axis_type,ae.axis_type_name, fv.position as sort, 
						                count(a.axis_id) as default_number_of_tire
						                from
	                                    FLEET_VEHICLE FV
	                                    INNER JOIN VEHICLE V on FV.vehicle_id = v.vehicle_id
	                                    inner join fleet f on f.fleet_id=fv.fleet_id
	                                    inner join AXIS_TYPE AE ON v.axis_type=AE.axis_type
						                inner join axis a on a.axis_type = ae.axis_type
	                                    LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id and g.vehicle_id<>'0'

						               " + System.Environment.NewLine;
            StrQuery += " where fv.is_delete=0 and f.is_delete=0 " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(fv.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            

            StrQuery += @" group by fv.date_modified,V.vehicle_rfid,
	                                    v.vehicle_id,v.vehicle_name,f.km, f.hm,fv.fleet_id,
	                                    v.axis_type,ae.axis_type_name, fv.position 
                                        order by fv.fleet_id, fv.position" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptTireRegister/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptTireRegister(string date_start, string date_end, string userid)
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
            string roleUser = getRoleUser(userid);
            string StrQuery = @"select R.tire_id, t.status_id,st.status_name, isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code,t.size,t.pattern,i.brand_tire_id,b.brand_tire_name,t.serial_number,
                                R.date_created,str_date_created=convert(varchar,R.date_created,106),time_date_created=convert(varchar,R.date_created,108),
                                R.location_id,R.register_tire_no,isnull(tu.vehicle_id,'') as vehicle_id, isnull(tx.axis_id,'') as axis_id,R.userid,R.reg_rfid as rfid,a.Name,a.JobPosition 
                                ,t.location_id as location_id_update, loc.location as location_update
                                from REGISTER_TIRE R
                                inner join TIRE t on R.tire_id=t.tire_id 
                                inner join STATUS st on st.status_id = t.status_id
			                    left join TIRE_UNIT tu on tu.tire_id=t.tire_id and tu.is_delete=0
			                    left join TIRE_AXIS tx on tx.tire_id=t.tire_id and tx.is_delete=0
                                INNER JOIN ITEM i ON i.tire_code = t.tire_code
								INNER JOIN LOCATION loc on loc.location_id = t.location_id
                                inner join BRAND_TIRE b on i.brand_tire_id=b.brand_tire_id 
                                inner join AspNetUsers a on r.userid=a.UserId" + System.Environment.NewLine;
           
            StrQuery += "where r.tire_id <>'0' and r.tire_id not in (select tire_id from DISPOSAL_DETAIL)   and r.tire_id not in (select tire_id from SCRAP_TIRE)" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(r.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            
            if (roleUser != "0" && roleUser != "-" && roleUser != "" )
            {
                StrQuery += "and r.userid=@userid" + System.Environment.NewLine;
            }

            StrQuery += "order by R.date_created desc, R.tire_id asc" + System.Environment.NewLine;

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
        [Route("api/Report/getRptVehicleRegister/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptVehicleRegister(string date_start, string date_end, string userid)
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

            string roleUser = getRoleUser(userid);
            string StrQuery = @"
                                select R.vehicle_id,v.vehicle_name,b.brand_vhc_id,b.brand_vhc_name,
                                R.date_created,str_date_created=convert(varchar,R.date_created,106),time_date_created=convert(varchar,R.date_created,108),
                                R.location_id,R.register_vhc_no,R.userid,R.reg_rfid as rfid,a.Name,a.JobPosition 
                                from REGISTER_VHC R
                                inner join VEHICLE v on R.vehicle_id=v.vehicle_id
                                inner join BRAND_VEHICLE b on b.brand_vhc_id=v.brand_vhc_id
                                inner join AspNetUsers a on r.userid=a.UserId" + System.Environment.NewLine;

            StrQuery += "where r.vehicle_id <>'0'" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(r.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and r.userid=@userid" + System.Environment.NewLine;
            }

            StrQuery += "order by R.date_created desc, R.vehicle_id asc" + System.Environment.NewLine;

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
        [Route("api/Report/getRptRfidVehicle/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptRfidVehicle(string date_start, string date_end, string userid)
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
            string roleUser = getRoleUser(userid);
            string StrQuery = @"
                                select v.vehicle_id, v.vehicle_name, v.vehicle_img, v.vehicle_rfid, bv.brand_vhc_id, bv.brand_vhc_name from vehicle v
                                inner join BRAND_VEHICLE bv on bv.brand_vhc_id=v.brand_vhc_id
                                where vehicle_rfid is not null and vehicle_rfid<>'0' 
                                and v.is_delete=0 and bv.is_delete=0" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(v.date_modified as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and v.userid=@userid" + System.Environment.NewLine;
            }

            StrQuery += "order by v.date_created desc" + System.Environment.NewLine;

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
        // GET api/documentation
        /// <summary>
        /// Fungsi Get report   Outbound (pickup)
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Route("api/Report/getRptPickupDetail/{pickup_no}/{date_start}/{date_end}/{userid}/{tire_code}")]
        //public IHttpActionResult getRptPickupDetail(string pickup_no, string date_start, string date_end, string userid,string tire_code)
        //{

        //    DateTime dt = DateTime.Now;
        //    string dtSTring = dt.ToString("yyyy-MM-dd");
        //    if (date_start != "0" && date_start != "-" && date_start != "")
        //    {
        //        dt = Convert.ToDateTime(date_start);
        //        dtSTring = dt.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        dtSTring = "0";
        //    }

        //    DateTime dt2 = DateTime.Now;
        //    string dtSTring2 = dt2.ToString("yyyy-MM-dd");
        //    if (date_end != "0" && date_end != "-" && date_end != "")
        //    {
        //        dt2 = Convert.ToDateTime(date_end);
        //        dtSTring2 = dt2.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        dtSTring2 = "0";
        //    }

        //    string prm = "";
        //    string StrQuery = "select P.pickup_no,date_created=convert(varchar,P.date_created,106)," + System.Environment.NewLine;
        //    StrQuery += "P.location_id,L.location,L2.location_id as destination_id, L2.location as destination," + System.Environment.NewLine;
        //    StrQuery += "P.driver,P.vehicle_no,P.notes,P.userid,a.name as [user], PD.tire_id," + System.Environment.NewLine;
        //    StrQuery += "t.tire_name,t.size,t.pattern,t.brand_id,B.brand_name,t.status_id,t.tire_mileage,t.tire_code" + System.Environment.NewLine;
        //    StrQuery += "from PICKUP_DETAIL PD" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN PICKUP P ON PD.pickup_no=P.pickup_no" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN AspNetUsers a on P.userid=a.UserId" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN LOCATION L on P.location_id=L.location_id" + System.Environment.NewLine;
        //    StrQuery += "LEFT JOIN LOCATION L2 on P.destination=L2.location_id" + System.Environment.NewLine;
        //    StrQuery += "inner join TIRE t on pd.tire_id=t.tire_id" + System.Environment.NewLine;
        //    StrQuery += "inner join BRAND B on t.brand_id=B.brand_id" + System.Environment.NewLine;

        //    if (pickup_no != "0" && pickup_no != "-" && pickup_no != "")
        //    {
        //        prm = "where P.pickup_no='"+ pickup_no + "'" + System.Environment.NewLine;
        //    }

        //    if (date_start != "0" && date_start != "-" && date_start != "")
        //    {
        //        if (prm != "")
        //        {
        //            prm += "and cast(P.date_created as date) between cast('" + date_start + "' as date) and cast('" + date_end + "' as date)" + System.Environment.NewLine;
        //        }
        //        else
        //        {
        //            prm = "where cast(P.date_created as date) between cast('" + date_start + "' as date) and cast('" + date_end + "' as date)" + System.Environment.NewLine;
        //        }
                
        //    }

        //    if (userid != "0" && userid != "-" && userid != "" && userid!="001")
        //    {
        //        if (prm != "")
        //        {
        //            prm += "and P.userid='"+ userid + "'" + System.Environment.NewLine;
        //        }
        //        else
        //        {
        //            prm = "where P.userid='" + userid + "'" + System.Environment.NewLine;
        //        }

        //    }

        //    if (tire_code != "0" && tire_code != "-" && tire_code != "")
        //    {
        //        if (prm != "")
        //        {
        //            prm += "and t.tire_code='" + tire_code + "'" + System.Environment.NewLine;
        //        }
        //        else
        //        {
        //            prm = "where t.tire_code='" + tire_code + "'" + System.Environment.NewLine;
        //        }

        //    }



        //    StrQuery += prm;
        //    //StrQuery += "where cast(date_created as date) between cast('2021-06-27' as date) and cast('2021-06-27' as date)" + System.Environment.NewLine;
        //    //StrQuery += "and R.supplier_id" + System.Environment.NewLine;
        //    StrQuery += "order by P.date_created desc, P.pickup_no" + System.Environment.NewLine;

        //    try
        //    {
        //        SqlConnection dbConn = new SqlConnection(con);
        //        dbConn.Open();
        //        var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
        //        dbConn.Close();
        //        return Json((new { records = data.Count, data = data }));
        //    }

        //    catch (Exception e)
        //    {
        //        return Json((new { msg = e.Message.ToString(), data = "" }));
        //    }

        //}

        // GET api/documentation
        /// <summary>
        /// Fungsi Get report Inbound
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptInboundDetail/{inbound_no}/{date_start}/{date_end}/{userid}/{tire_code}")]
        public IHttpActionResult getRptInboundDetail(string inbound_no, string date_start, string date_end, string userid, string tire_code)
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
            string roleUser = getRoleUser(userid);
            string prm = "";
            string StrQuery = "select i.inbound_no,date_created=convert(varchar,i.date_created,106)," + System.Environment.NewLine;
            StrQuery += "i.location_id,L.location,i.reference_no,i.notes,i.userid,a.name as [user], i.tire_id," + System.Environment.NewLine;
            StrQuery += "t.tire_name,t.size,t.pattern,t.brand_id,B.brand_name,t.status_id,[dbo].[GetLastTireMileage](i.tire_id) as tire_mileage, t.tire_code" + System.Environment.NewLine;
            StrQuery += "from INBOUND I" + System.Environment.NewLine;
            StrQuery += "INNER JOIN AspNetUsers a on I.userid=a.UserId" + System.Environment.NewLine;
            StrQuery += "INNER JOIN LOCATION L on I.location_id=L.location_id" + System.Environment.NewLine;
            StrQuery += "inner join TIRE t on I.tire_id=t.tire_id" + System.Environment.NewLine;
            StrQuery += "inner join BRAND B on t.brand_id=B.brand_id" + System.Environment.NewLine;


            if (inbound_no != "0" && inbound_no != "-" && inbound_no != "")
            {
                prm = "where (i.inbound_no=@inbound_no or i.reference_no=@inbound_no)" + System.Environment.NewLine;
            }

            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                if (prm != "")
                {
                    prm += "and cast(i.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where cast(i.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                }

            }

            if (roleUser != "0" && roleUser != "-" && roleUser != "" )
            {
                if (prm != "")
                {
                    prm += "and i.userid=@userid" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where i.userid=@userid" + System.Environment.NewLine;
                }

            }
            string rep_tire_code = tire_code.Replace("-", ".");
            if (tire_code != "0" && tire_code != "-" && tire_code != "" )
            {
                if (prm != "")
                {
                    prm += "and t.tire_code=@rep_tire_code" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where t.tire_code=@rep_tire_code" + System.Environment.NewLine;
                }

            }


            StrQuery += prm;
            StrQuery += "order by i.date_created desc, i.inbound_no" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, inbound_no, userid, rep_tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report relocation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptRelocation/{date_start}/{date_end}/{userid}/{location_id}/{location_before}")]
        public IHttpActionResult getRptRelocation(string date_start, string date_end, string userid, string location_id, string location_before)
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

            string prm = "";
            string StrQuery = "select date_created=convert(varchar,R.date_created,106),r.tire_id,r.location_id,l.location," + System.Environment.NewLine;
            StrQuery += "r.location_before as location_id_before,l2.location as location_before,r.notes,r.userid,a.name from RELOCATION R" + System.Environment.NewLine;
            StrQuery += "inner join TIRE t on r.tire_id=t.tire_id" + System.Environment.NewLine;
            StrQuery += "inner join LOCATION l on r.location_id=l.location_id" + System.Environment.NewLine;
            StrQuery += "inner join LOCATION l2 on r.location_before=l2.location_id" + System.Environment.NewLine;
            StrQuery += "inner join AspNetUsers a on r.userid=a.UserId" + System.Environment.NewLine;
           // StrQuery += "" + System.Environment.NewLine;
            //StrQuery += "where date_created between '' and ''" + System.Environment.NewLine;
            

            if (date_start != "0" && date_start != "-" && date_start != "")
            {
               
             prm = "where cast(R.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
              
            }

            if (userid != "0" && userid != "-" && userid != "" )
            {
                if (prm != "")
                {
                    prm += "and R.userid=@userid" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where R.userid=@userid" + System.Environment.NewLine;
                }

            }

            if (location_id != "0" && location_id != "-" && location_id != "")
            {
                if (prm != "")
                {
                    prm += "and R.location_id=@location_id" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where R.location_id=@location_id" + System.Environment.NewLine;
                }

            }

            if (location_before != "0" && location_before != "-" && location_before != "")
            {
                if (prm != "")
                {
                    prm += "and R.location_before=@location_before" + System.Environment.NewLine;
                }
                else
                {
                    prm = "where R.location_before=@location_before" + System.Environment.NewLine;
                }

            }


            StrQuery += prm;
            StrQuery += "order by R.date_created desc, R.tire_id" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end, userid, location_id, location_before }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptInspection/{date_start}/{date_end}")]
        public IHttpActionResult getRptInspection(string date_start, string date_end)
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

            //string StrQuery = "select i.tire_id, i.date_created, i.inspection_no, i.notes, i.tire_mileage, i.tread_depth, i.air_pressure," + System.Environment.NewLine;
            //StrQuery += "v.vehicle_name, v.vehicle_mileage, v.vehicle_no, VT.description, t.tire_id, t.tire_name, loc.location, loc.[group]," + System.Environment.NewLine;
            //StrQuery += "br.brand_name, tra.trailer_name, su.UserName" + System.Environment.NewLine;
            //StrQuery += "from inspection i" + System.Environment.NewLine;
            //StrQuery += "left join INSPECTION_DETAIL_ACTION IDA ON IDA.axis_id = i.axis_id AND IDA.date_created = i.date_created AND IDA.tire_id = i.tire_id AND IDA.location_id = i.location_id AND IDA.vehicle_id = i.vehicle_id AND IDA.inspection_no = i.inspection_no" + System.Environment.NewLine;
            //StrQuery += "left join INSPECTION_DETAIL_OBSERVATION IDO ON IDO.axis_id = i.axis_id AND IDO.date_created = i.date_created AND IDO.tire_id = i.tire_id AND IDO.location_id = i.location_id AND IDO.vehicle_id = i.vehicle_id AND IDO.inspection_no = i.inspection_no" + System.Environment.NewLine;
            //StrQuery += "inner join tire t on t.tire_id = i.tire_id" + System.Environment.NewLine;
            //StrQuery += "inner join location loc on loc.location_id = i.location_id" + System.Environment.NewLine;
            //StrQuery += "inner join brand br on br.brand_id = t.brand_id" + System.Environment.NewLine;
            //StrQuery += "inner join VEHICLE v on v.vehicle_id = i.vehicle_id" + System.Environment.NewLine;
            //StrQuery += "inner join VEHICLE_TRAILER VT on VT.vehicle_id = i.vehicle_id" + System.Environment.NewLine;
            //StrQuery += "inner join TRAILER TRA on TRA.trailer_id = VT.trailer_id" + System.Environment.NewLine;
            //StrQuery += "inner join AspNetUsers su on su.UserId = i.userid" + System.Environment.NewLine;
            string StrQuery = @"select
                    i.tire_id, i.date_created, i.inspection_no, i.notes, i.tire_mileage, i.tread_depth, i.air_pressure,
                    v.vehicle_name, fv.fleet_id, t.tire_id, t.tire_name, loc.location, loc.[group],
                    br.brand_tire_name, v.vehicle_name, su.UserName
                    from inspection i
                    left join INSPECTION_DETAIL_ACTION IDA ON IDA.axis_id = i.axis_id AND IDA.date_created = i.date_created AND IDA.tire_id = i.tire_id AND IDA.location_id = i.location_id AND IDA.vehicle_id = i.vehicle_id AND IDA.inspection_no = i.inspection_no
                    left join INSPECTION_DETAIL_OBSERVATION IDO ON IDO.axis_id = i.axis_id AND IDO.date_created = i.date_created AND IDO.tire_id = i.tire_id AND IDO.location_id = i.location_id AND IDO.vehicle_id = i.vehicle_id AND IDO.inspection_no = i.inspection_no
                    inner join tire t on t.tire_id = i.tire_id
                    inner join item it on it.tire_code = t.tire_code
                    inner join location loc on loc.location_id = i.location_id
                    inner join brand_tire br on br.brand_tire_id = it.brand_tire_id
                    inner join VEHICLE v on v.vehicle_id = i.vehicle_id
					inner join FLEET_VEHICLE FV on fv.vehicle_id=v.vehicle_id and FV.is_delete=0
                    inner join AspNetUsers su on su.UserId = i.userid
                    " + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(i.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }

            //if (userid != "0" && userid != "-" && userid != "" && userid != "001")
            //{
            //    StrQuery += "and r.userid='" + userid + "'" + System.Environment.NewLine;
            //}

            StrQuery += "order by i.date_created desc, i.tire_id asc" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
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
        [Route("api/Report/getReportOutbound/{date_start}/{date_end}/{userid}/{location_id}")]
        public IHttpActionResult getReportOutbound(string date_start, string date_end, string userid, string location_id)
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
            string roleUser = getRoleUser(userid);
            string StrQuery = @"select OTB.outbound_id,OTB.date_created,OTB_D.tire_id,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code,OTB.location_id as outboundloc_id,LC.location as outbound_location,OTB_D.location_id as destination_id, LOC.location as destination, OTB.userid, AU.Name
                                from OUTBOUND OTB
                                inner join AspNetUsers AU on AU.UserId = OTB.userid 
                                inner join LOCATION LC on LC.location_id = OTB.location_id
                                INNER JOIN OUTBOUND_DETAIL OTB_D on OTB_D.outbound_id=OTB.outbound_id and OTB.date_created=OTB_D.date_created
                                inner join TIRE T on t.tire_id = OTB_D.tire_id
                                INNER JOIN LOCATION LOC on LOC.location_id = OTB_D.location_id
                                inner join ITEM i on i.tire_code=t.tire_code
                                where OTB.is_delete=0 and OTB_d.is_delete=0 " + System.Environment.NewLine;
            if (location_id != "0")
            {
                StrQuery += "and otb.location_id=@location_id" + System.Environment.NewLine;
            }
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(OTB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (roleUser != "0" && roleUser != "-" && roleUser != "" )
            {
                StrQuery += "and OTB.userid=@userid" + System.Environment.NewLine;
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
        [Route("api/Report/getReportInbound/{date_start}/{date_end}/{userid}/{location_id}")]
        public IHttpActionResult getReportInbound(string date_start, string date_end, string userid, string location_id)
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
            string roleUser = getRoleUser(userid);
            string StrQuery = @"select INB.inbound_id,INB_D.tire_id,isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) as unique_code,LC.location, INB.date_created, INB.userid, AU.Name  from INBOUND INB
                                inner join AspNetUsers AU on AU.UserId = INB.userid
                                inner join LOCATION LC on LC.location_id = INB.location_id
                                INNER JOIN INBOUND_DETAIL INB_D on INB_D.inbound_id=INB.inbound_id and INB.date_created=INB_D.date_created
                                inner join TIRE T on t.tire_id = INB_D.tire_id
                                inner join ITEM i on i.tire_code=t.tire_code
                                where inb.is_delete=0 and inb_d.is_delete=0  " + System.Environment.NewLine;
            if (location_id != "0")
            {
                StrQuery += "and inb.location_id=@location_id" + System.Environment.NewLine;
            }
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(INB.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            if (roleUser != "0" && roleUser != "-" && roleUser != "" )
            {
                StrQuery += "and INB.userid=@userid" + System.Environment.NewLine;
            }
            //StrQuery += @"   group by od.tire_id,t.tire_name" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { location_id , date_start, date_end, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptGroupEmployeeRdl/{year}/{month}/{userid}")]
        public IHttpActionResult getRptGroupEmployeeRdl(string year, string month, string userid)
        {
            string roleUser = getRoleUser(userid);
            string StrQuery = @"select year, month, ge.group_id, g.group_name, g.location_id, l.location, ge.emp_id, e.emp_name, ge.position_id, p.position_name
                                from GROUPS_EMPLOYEES ge
                                inner join EMPLOYEES e on e.emp_id = ge.emp_id
                                inner join POSITIONS p on p.position_id = ge.position_id
                                inner join GROUPS g on g.group_id = ge.group_id
                                inner join LOCATION l on l.location_id=g.location_id
                                where year=@year and month=@month
                                " + System.Environment.NewLine;
          
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and ge.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += @"   order by year, month, ge.group_id,p.position_id" + System.Environment.NewLine;

            try
            {
                //Master.qDataTireBySerialNumber(sn)
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year, month, userid }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        #region Tracking
        // GET api/documentation
        /// <summary>
        /// Fungsi Get report Tracking
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Report/getRptSAMRdl/{date_start}/{date_end}")]
        public IHttpActionResult getRptSAMRdl(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"select top 200 sort=row_number() over (order by t.date_created desc),t.smo_id,t.date_created,convert(varchar,t.date_created,106) as str_date,
                                convert(varchar, t.date_created, 108) as str_time,
                                t.location_id,l.location,t.process_id,p.process,t.tire_id,t.userid,a.name,
                                o.value, b.observation_item_name, b.observation_item_unit
                                 from TIRE_TRACKING T
                                inner join TIRE Ti on Ti.tire_id = T.tire_id
                                inner join AspNetUsers a on t.userid = a.UserId
                                inner join LOCATION l on t.location_id = l.location_id
                                inner join PROCESS p on t.process_id = p.process_id
								left join  SERV_MAINT_OBSERVATION smo on smo.smo_id=t.smo_id
								left join SERV_MAINT_OBSERVATION_DETAIL o on smo.smo_id=o.smo_id
								left join OBSERVATION_ITEM b on o.observation_item_id=b.observation_item_id" + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(t.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                prm += " and t.process_id not in (100, 101, 104, 105, 110, 99) ";
            }
            else {
                prm = " where t.process_id not in (100, 101, 104, 105, 110, 99) ";
            }

            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by t.date_created desc " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptVehicleChange/{date_start}/{date_end}")]
        public IHttpActionResult getRptVehicleChange(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"
                    select vc.vehicle_change_no, vc.date, vc.km, vc.hm, au.Name 
                    from VEHICLE_CHANGE vc 
					inner join AspNetUsers au on au.UserId = vc.userid" + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(vc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;

            }


            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by  vc.date" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptVehicleChangeDetail/{vehicle_change_no}")]
        public IHttpActionResult getRptVehicleChangeDetail(string vehicle_change_no)
        {
            

            string prm = "";
            string StrQuery = @"
                   
										 select vc.vehicle_change_no, vc.date, vc.km, vc.hm, 
                    vcd.sort, case when vcd.old_fleet_id = 'XXX' then '' else vcd.old_fleet_id end as old_fleet_id , 
					case when vcd.old_vehicle_id = 'XXX' then '' else vcd.old_vehicle_id end as old_vehicle_id, 
					case when vcd.new_fleet_id = 'XXX' then '' else vcd.new_fleet_id end as new_fleet_id, 
					case when vcd.new_vehicle_id = 'XXX' then '' else vcd.new_vehicle_id end as new_vehicle_id
                    from VEHICLE_CHANGE vc 
                    inner join VEHICLE_CHANGE_DETAIL vcd on vcd.vehicle_change_no = vc.vehicle_change_no and vcd.date = vc.date" + System.Environment.NewLine;


            //if (date_start != "0" && date_start != "-" && date_start != "")
            //{
            //    prm = "where vc.vehicle_change_no=@vehicle_change_no" + System.Environment.NewLine;
                
            //}
            

            StrQuery += "where vc.vehicle_change_no=@vehicle_change_no";
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by  vc.date, vcd.sort" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { vehicle_change_no}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptVehicleChangeRdl/{date_start}/{date_end}")]
        public IHttpActionResult getRptVehicleChangeRdl(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"
                   
					 select vc.vehicle_change_no, vc.date, vc.km, vc.hm, au.Name , 
                    vcd.sort, case when vcd.old_fleet_id = 'XXX' then '' else vcd.old_fleet_id end as old_fleet_id , 
					case when vcd.old_vehicle_id = 'XXX' then '' else vcd.old_vehicle_id end as old_vehicle_id, 
					case when vcd.new_fleet_id = 'XXX' then '' else vcd.new_fleet_id end as new_fleet_id, 
					case when vcd.new_vehicle_id = 'XXX' then '' else vcd.new_vehicle_id end as new_vehicle_id
                    from VEHICLE_CHANGE vc 
                    inner join VEHICLE_CHANGE_DETAIL vcd on vcd.vehicle_change_no = vc.vehicle_change_no and vcd.date = vc.date
					inner join AspNetUsers au on au.UserId = vc.userid" + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(vc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;

            }


            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by  vc.date, vcd.sort" + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptSAM/{date_start}/{date_end}")]
        public IHttpActionResult getRptSAM(string date_start, string date_end)
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

            string prm = "";
            string StrQuery = @"
select top 200 sort=row_number() over (order by t.date_created desc),t.smo_id,t.date_created,convert(varchar,t.date_created,106) as str_date,
                                convert(varchar, t.date_created, 108) as str_time,
                                t.location_id,l.location,t.process_id,p.process,t.tire_id,t.userid,a.name, case when t.process_id = '400' then '1' else '0' end as is_detail
                                 from TIRE_TRACKING T
                                inner join TIRE Ti on Ti.tire_id = T.tire_id
                                inner join AspNetUsers a on t.userid = a.UserId
                                inner join LOCATION l on t.location_id = l.location_id
                                inner join PROCESS p on t.process_id = p.process_id" + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(t.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
                prm += " and t.process_id not in (001,100, 101, 104, 105, 110, 99,004,005,112,111) ";
            }
            else
            {
                prm = " where t.process_id not in (001,100, 101, 104, 105, 110, 99,004,005,112,111)  ";
            }

            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by t.date_created desc " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start , date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptSAMPerTire/{tire_id}")]
        public IHttpActionResult getRptSAMPerTire(string tire_id)
        {
            string prm = "";
            string StrQuery = @"select sort=row_number() over (order by t.date_created desc),t.smo_id,t.date_created,convert(varchar,t.date_created,106) as str_date,
                                convert(varchar, t.date_created, 108) as str_time,
                                t.location_id,l.location,t.process_id,p.process,t.tire_id,t.userid,a.name,
                                o.value, b.observation_item_name, b.observation_item_unit
                                 from TIRE_TRACKING T
                                inner join TIRE Ti on Ti.tire_id = T.tire_id
                                inner join AspNetUsers a on t.userid = a.UserId
                                inner join LOCATION l on t.location_id = l.location_id
                                inner join PROCESS p on t.process_id = p.process_id
								left join  SERV_MAINT_OBSERVATION smo on smo.smo_id=t.smo_id
								left join SERV_MAINT_OBSERVATION_DETAIL o on smo.smo_id=o.smo_id
								left join OBSERVATION_ITEM b on o.observation_item_id=b.observation_item_id" + System.Environment.NewLine;

            
            StrQuery += "where T.tire_id=@tire_id";
            StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by t.date_created desc " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tire_id = tire_id}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getAddDays/{date_}")]
        public IHttpActionResult getAddDays(string date_)
        {
            DateTime date_created = DateTime.Now;
            string date = date_created.AddDays(14).ToString("yyyy-MM-dd");
            DateTime date_now = Convert.ToDateTime(date_);//DateTime.Now.ToString("yyyy-MM-dd")
            string date_now_input = date_now.ToString();
            string date_maintenance = date_now.AddDays(14).ToString("yyyy-MM-dd");
            string vehicle_id = "RTP0186";
            string yearmonth = "202209";
            string tire_id = "2FB2830480";
            string userid = "001";
            SqlConnection dbConn = new SqlConnection(con);
            dbConn.Open();
            //var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
            
            dbConn.Execute(@"
                                           DECLARE @fleet varchar(max);
                                            declare @schmid varchar(max);
                                            declare @result varchar(max);
                                            select @schmid=[dbo].[getnextschedulemaintid](@yearmonth)
                                            select @fleet=fleet_id from fleet_vehicle where vehicle_id=@vehicle_id
                                            if (not exists (select * from schedule_maintenance where fleet_id=@fleet) ) 
                                             begin 
	                                         insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                                values (@schmid,@date_now_input,@fleet,@date_maintenance,1,0,@date_created,@userid)
											
                                            end
											else
											begin
												update schedule_maintenance set schm_flag = 1, date_modified=@date_created where fleet_id=@fleet and schm_date= (select MAX(schm_date) from SCHEDULE_MAINTENANCE where fleet_id= @fleet)
												
												insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                                values (@schmid,@date_now_input,@fleet,@date_maintenance,0,0,@date_created,@userid)
											
											end

                                    ", new
            {
                vehicle_id,
                yearmonth,
                date_now_input,
                date_maintenance,
                tire_id,
                date_created,
                userid
            }, null);
            dbConn.Close();
            return Json((new { msg = "", date_now= date_created.ToString("yyyy-MM-dd"), date_next = date }));
        }
        [HttpGet]
        [Route("api/Report/getRptTopFiveInspectionTreadDepth")]
        public IHttpActionResult getRptTopFiveInspectionTreadDepth()
        {
            string prm = "";
            string StrQuery = @"select  top 5 * from(
                            select f.fleet_id, f.fleet_name, Round(avg(cast(smod.value as real)),2) as avg_value from fleet f
                            inner join FLEET_VEHICLE fv on f.fleet_id = fv.fleet_id and FV.is_delete=0
                            inner join tire_unit tu on tu.vehicle_id = fv.vehicle_id and tu.is_delete=0
                            inner join tire t on t.tire_id = tu.tire_id
                            inner join SERV_MAINT_OBSERVATION smo on smo.tire_id = t.tire_id 
                            inner join SERV_MAINT_OBSERVATION_DETAIL smod on smod.smo_id = smo.smo_id and smod.date_created = smo.date_created
                            inner join OBSERVATION_ITEM oi on oi.observation_item_id = smod.observation_item_id
                            inner join status st on st.status_id = t.status_id
                                    " + System.Environment.NewLine;


            //if (date_start != "0" && date_start != "-" && date_start != "")
            //{
            //    prm = "and cast(smod.date_created as date) between cast('" + date_start + "' as date) and cast('" + date_end + "' as date)" + System.Environment.NewLine;
            //}
            StrQuery += @"where 
                            oi.observation_item_id='2' 
                            group by f.fleet_id, f.fleet_name) as x
                            where x.avg_value<=4" + System.Environment.NewLine;

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
        [Route("api/Report/getRptScheduleMaintenance/{date_start}/{date_end}")]
        public IHttpActionResult getRptScheduleMaintenance(string date_start, string date_end)
        {
            string prm = "";
            string StrQuery = @"
	                            
                                select * from(
		                            select SM.schm_date as date, SM.fleet_id, f.fleet_name, SM.schm_date_maintenance,DATEDIFF(day,  GETDATE(), 
		                            SM.schm_date_maintenance ) as days
		                            ,cast(DATEDIFF(day,  GETDATE(), SM.schm_date_maintenance ) as varchar) + ' day(s) later' as description
		                            from SCHEDULE_MAINTENANCE SM
		                            inner join fleet f on f.fleet_id = sm.fleet_id
		                            where sm.schm_flag=0
		                            group by SM.schm_date,SM.fleet_id, f.fleet_name, SM.schm_date_maintenance) as X
                                where X.days>0
" + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                StrQuery += "and cast(X.date as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
            }
            StrQuery += " order by X.days asc " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { date_start, date_end}, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report Tracking Detail
        /// </summary>
        /// <returns></returns>

      

        [HttpGet]
        [Route("api/Report/getRptTracking/{tire_id}")]
        public IHttpActionResult getRptTracking(string tire_id)
        {
            string tire_id_decode = tire_id.Replace("-", ".");
            string StrQuery = @"select sort=row_number() over (order by t.date_created desc),t.smo_id,t.date_created,convert(varchar,t.date_created,106) as str_date,convert(varchar,t.date_created,108) as str_time,
                                            t.location_id,l.location,t.process_id,p.process,ti.rfid,t.tire_id,t.userid,a.name,
                                            is_detail=case when t.process_id in ('400') then 1 else 0 end 
                                from TIRE_TRACKING T
                                inner join TIRE Ti on Ti.tire_id = T.tire_id
                                inner join AspNetUsers a on t.userid=a.UserId
                                inner join LOCATION l on t.location_id=l.location_id
                                inner join PROCESS p on t.process_id=p.process_id
                                where t.tire_id=@tire_id
                                order by t.date_created desc, p.process_id desc
                                " + System.Environment.NewLine;
            //           string StrQuery = "select sort=row_number() over (order by t.date_created desc),t.date_created,convert(varchar,t.date_created,106) as str_date,convert(varchar,t.date_created,108) as str_time," + System.Environment.NewLine;
            //           StrQuery += "t.location_id,l.location,t.process_id,p.process,t.rfid,t.tire_id,t.vehicle_id,t.axis_id,t.description,t.userid,a.name," + System.Environment.NewLine;
            //           StrQuery += "is_detail=case when t.process_id in ('400') then 1 else 0 end, ins.inspection_no" + System.Environment.NewLine;
            //           StrQuery += "from TRACKING T" + System.Environment.NewLine;
            //           StrQuery += "inner join AspNetUsers a on t.userid=a.UserId" + System.Environment.NewLine;
            //           StrQuery += "inner join LOCATION l on t.location_id=l.location_id" + System.Environment.NewLine;
            //           StrQuery += "inner join PROCESS p on t.process_id=p.process_id" + System.Environment.NewLine;
            //           StrQuery += @"left join INSPECTION ins on ins.date_created=T.date_created and ins.tire_id=T.tire_id and ins.location_id = t.location_id
            //and ins.vehicle_id = t.vehicle_id and ins.axis_id = t.axis_id" + System.Environment.NewLine;
            //           StrQuery += "where t.tire_id=@tire_id" + System.Environment.NewLine;
            //           //StrQuery += ")x" + System.Environment.NewLine;
            //           StrQuery += "order by t.date_created desc" + System.Environment.NewLine;
            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tire_id = tire_id_decode }, null, true, commandTimeout: 0, commandType: null).ToList();
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
        /// Fungsi Get report Tracking Detail
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("api/Report/getRptTrackingDetail/{tire_id}/{process_id}/{inspection_no}")]
        public IHttpActionResult getRptTrackingDetail(string tire_id, string process_id, string inspection_no = null)
        {
            string StrQuery = "";
            string tire_id_decode = tire_id.Replace("-", ".");
            if (process_id == "400")
            {

                StrQuery = @"select sort=ROW_NUMBER() over (order by i.date_created), i.smo_id,i.date_created, 
				            i.tire_id,o.observation_item_id,isnull(b.observation_item_name,'') as observation,
				            o.value, o.notes, b.observation_item_unit
				            from SERV_MAINT_OBSERVATION i
                            left join SERV_MAINT_OBSERVATION_DETAIL o on i.smo_id=o.smo_id
                            left join OBSERVATION_ITEM b on o.observation_item_id=b.observation_item_id
                            where i.tire_id=@tire_id  and i.smo_id=@inspection_no" + System.Environment.NewLine;
                //StrQuery += "left join INSPECTION_DETAIL_OBSERVATION o on i.inspection_no=o.inspection_no" + System.Environment.NewLine;
                //StrQuery += "left join OBSERVATION b on o.observation_id=b.observation_id" + System.Environment.NewLine;
                //StrQuery += "left join INSPECTION_DETAIL_ACTION a on i.inspection_no=a.inspection_no" + System.Environment.NewLine;
                //StrQuery += "left join REPAIR_CATEGORY r on a.repair_category_id=R.repair_category_id" + System.Environment.NewLine;
                //StrQuery += "left join REASON s on a.reason_id=s.reason_id" + System.Environment.NewLine;
                //StrQuery += "where i.tire_id=@tire_id  and i.inspection_no=@inspection_no" + System.Environment.NewLine;
                // StrQuery += "order by i.date_created desc" + System.Environment.NewLine;
            }

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tire_id = tire_id_decode, process_id = process_id, inspection_no = inspection_no }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getTireTracking/{StartDate?}/{EndDate?}/{UserId?}")]
        public IHttpActionResult getTireTracking(string StartDate = null, string EndDate = null, string UserId = null)
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
            
            DataTable data = Report.getDataTireTracking(dtSTring, dtSTring2, UserId);
            return Json((new { records = data.Rows.Count, data = data }));

        }
        [HttpGet]
        [Route("api/Report/getRptChangeTire/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptChangeTire(string date_start, string date_end, string userid)
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
            
            string prm = "";
            string StrQuery = @"
                                select 
                                    tc.date_created as date, tc.location_id, loc.location, tc.vehicle_id, tc.old_tire_id, i1.unique_code as old_tire_unique_code, t1.rfid as old_tire_rfid, tc.new_tire_id, i2.unique_code as new_tire_unique_code, t2.rfid as new_tire_rfid,
                                    tc.km, tc.time_start, tc.time_end, tc.notes , au.Name, em.emp_name
                                    from TIRE_CHANGE tc
                                    inner join LOCATION loc on loc.location_id=tc.location_id
                                    inner join tire t1 on t1.tire_id = tc.old_tire_id
                                    inner join item i1 on i1.tire_code = t1.tire_code
                                    inner join tire t2 on t2.tire_id = tc.new_tire_id
                                    inner join item i2 on i2.tire_code = t2.tire_code
	                                inner join AspNetUsers au on au.UserId=tc.userid
									inner join USER_EMPLOYEES ue on ue.userid= tc.userid
                                    inner join EMPLOYEES em on em.emp_id = ue.emp_id
                                " + System.Environment.NewLine;


            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "where cast(tc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
             
            }
            string roleUser = getRoleUser(userid);
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and tc.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by tc.date_created desc " + System.Environment.NewLine;

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
        [Route("api/Report/getRptScheduleEmployee/{year}/{month}")]
        public IHttpActionResult getRptScheduleEmployee(string year, string month)
        {
            var lastDayOfMonth = 0;
            if (year != "0" && year != "-" && year != "")
            {
                lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(year), Convert.ToInt32(month));
            }


            string prm = "";
            string jml_hari = "";
            string jml_hari_max = "";
            if (lastDayOfMonth == 29)
            {
                jml_hari = @",case when FORMAT(sch.date, 'dd')= '29' then isnull(g.group_name,'') else '' end as 'day29'";
                jml_hari_max = @",MAX(x.day29)as day29";
            }
            else if (lastDayOfMonth == 30)
            {
                jml_hari = @",case when FORMAT(sch.date, 'dd')= '29' then isnull(g.group_name,'') else '' end as 'day29', 
                        case when FORMAT(sch.date, 'dd')= '30' then isnull(g.group_name,'') else '' end as 'day30'";
                jml_hari_max = @",MAX(x.day29)as day29,MAX(x.day30)as day30";
            }
            else
            {
                jml_hari = @",case when FORMAT(sch.date, 'dd')= '29' then isnull(g.group_name,'') else '' end as 'day29', 
                        case when FORMAT(sch.date, 'dd')= '30' then isnull(g.group_name,'') else '' end as 'day30', 
                        case when FORMAT(sch.date, 'dd')= '31' then isnull(g.group_name,'') else '' end as 'day31'";
                jml_hari_max = @",MAX(x.day29)as day29,MAX(x.day30)as day30,MAX(x.day31)as day31";
            }
            string StrQuery = @"
                                 select x.year, x.month, x.location, x.shift, MAX(x.day1) as day1, MAX(x.day2) as day2,  MAX(x.day3)as day3, MAX(x.day4)as day4,
                                  MAX(x.day5) as day5, MAX(x.day6)as day6, MAX(x.day7)as day7, MAX(x.day8)as day8, MAX(x.day9)as day9, MAX(x.day10)as day10,
                                   MAX(x.day11)as day11, MAX(x.day12)as day12, MAX(x.day13)as day13, MAX(x.day14)as day14, MAX(x.day15)as day15, MAX(x.day16)as day16,
                                    MAX(x.day17)as day17, MAX(x.day18)as day18, MAX(x.day19)as day19, MAX(x.day20)as day20, MAX(x.day21)as day21, MAX(x.day22)as day22,
                                 MAX(x.day23)as day23, MAX(x.day24)as day24,MAX(x.day25)as day25, MAX(x.day26)as day26,MAX(x.day27)as day27, MAX(x.day28)as day28 
                                    "+ jml_hari_max + @"
                                from (
		                            select sch.year, sch.month, loc.location, s.shift, sch.is_delete, sch.location_id,
                                    case when FORMAT(sch.date, 'dd')='01' then isnull(g.group_name,'') else '' end as 'day1', 
                                    case when FORMAT(sch.date, 'dd')='02' then isnull(g.group_name,'') else '' end as 'day2', 
                                    case when FORMAT(sch.date, 'dd')='03' then isnull(g.group_name,'') else '' end as 'day3', 
                                    case when FORMAT(sch.date, 'dd')='04' then isnull(g.group_name,'') else '' end as 'day4', 
                                    case when FORMAT(sch.date, 'dd')='05' then isnull(g.group_name,'') else '' end as 'day5', 
                                    case when FORMAT(sch.date, 'dd')='06' then isnull(g.group_name,'') else '' end as 'day6', 
                                    case when FORMAT(sch.date, 'dd')='07' then isnull(g.group_name,'') else '' end as 'day7', 
                                    case when FORMAT(sch.date, 'dd')='08' then isnull(g.group_name,'') else '' end as 'day8', 
                                    case when FORMAT(sch.date, 'dd')='09' then isnull(g.group_name,'') else '' end as 'day9', 
                                    case when FORMAT(sch.date, 'dd')='10' then isnull(g.group_name,'') else '' end as 'day10', 
                                    case when FORMAT(sch.date, 'dd')='11' then isnull(g.group_name,'') else '' end as 'day11', 
                                    case when FORMAT(sch.date, 'dd')='12' then isnull(g.group_name,'') else '' end as 'day12', 
                                    case when FORMAT(sch.date, 'dd')='13' then isnull(g.group_name,'') else '' end as 'day13', 
                                    case when FORMAT(sch.date, 'dd')='14' then isnull(g.group_name,'') else '' end as 'day14', 
                                    case when FORMAT(sch.date, 'dd')='15' then isnull(g.group_name,'') else '' end as 'day15', 
                                    case when FORMAT(sch.date, 'dd')='16' then isnull(g.group_name,'') else '' end as 'day16', 
                                    case when FORMAT(sch.date, 'dd')='17' then isnull(g.group_name,'') else '' end as 'day17', 
                                    case when FORMAT(sch.date, 'dd')='18' then isnull(g.group_name,'') else '' end as 'day18', 
                                    case when FORMAT(sch.date, 'dd')='19' then isnull(g.group_name,'') else '' end as 'day19', 
                                    case when FORMAT(sch.date, 'dd')='20' then isnull(g.group_name,'') else '' end as 'day20', 
                                    case when FORMAT(sch.date, 'dd')='21' then isnull(g.group_name,'') else '' end as 'day21', 
                                    case when FORMAT(sch.date, 'dd')='22' then isnull(g.group_name,'') else '' end as 'day22', 
                                    case when FORMAT(sch.date, 'dd')='23' then isnull(g.group_name,'') else '' end as 'day23', 
                                    case when FORMAT(sch.date, 'dd')='24' then isnull(g.group_name,'') else '' end as 'day24', 
                                    case when FORMAT(sch.date, 'dd')='25' then isnull(g.group_name,'') else '' end as 'day25', 
                                    case when FORMAT(sch.date, 'dd')='26' then isnull(g.group_name,'') else '' end as 'day26', 
                                    case when FORMAT(sch.date, 'dd')='27' then isnull(g.group_name,'') else '' end as 'day27', 
                                    case when FORMAT(sch.date, 'dd')='28' then isnull(g.group_name,'') else '' end as 'day28'
                                    " + jml_hari + @"
                                    from schedules sch
                                    inner join LOCATION loc on loc.location_id = sch.location_id
                                    inner join groups g on sch.group_id=g.group_id
                                    inner join shift s on s.shift_id = sch.shift_id
                                    ) as X 
                        " + System.Environment.NewLine;

            StrQuery += "where x.is_delete=0" + System.Environment.NewLine;
            if (year != "0" && year != "-" && year != "")
            {
                prm = "and x.year=@year and x.month=@month" + System.Environment.NewLine;
            }
            //string roleUser = getRoleUser(userid);
            //if (roleUser != "0" && roleUser != "-" && roleUser != "")
            //{
            //    StrQuery += "and ts.tsch_id = (select tsch_id from TSCH_EMPLOYEES te2 inner join USER_EMPLOYEES ue2 on ue2.emp_id=te2.emp_id where ue2.userid=@userid)" + System.Environment.NewLine;
            //}
            StrQuery += prm;
            StrQuery += @" group by x.year, x.month, x.location, x.shift, x.location_id, x.is_delete order by x.year, x.month,x.location_id";

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year , month}, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptScheduleEmployeeDetail/{tsch_id}")]
        public IHttpActionResult getRptScheduleEmployeeDetail(string tsch_id)
        {
            string prm = "";
            string StrQuery = @"
                              select em.emp_id, em.emp_name, te.position_id, p.position_name
		                        from TEAM_SCHEDULE ts
		                        inner join TSCH_EMPLOYEES te on te.tsch_id = ts.tsch_id
		                        inner join LOCATION loc on loc.location_id = ts.location_id
		                        inner join EMPLOYEES em on em.emp_id = te.emp_id
		                        inner join POSITIONS p on p.position_id = te.position_id
		                        where ts.tsch_id=@tsch_id and te.is_delete=0
                            order by p.position_id
                        " + System.Environment.NewLine;
            
            //StrQuery += prm;
            ////StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            //StrQuery += " order by DATEDIFF(minute, tc.time_start, tc.time_end) asc  " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { tsch_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptScheduleEmployeeRdl/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptScheduleEmployeeRdl(string date_start, string date_end, string userid)
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

            string prm = "";
            string StrQuery = @"select ts.tsch_id, ts.location_id, loc.location, ts.shift, ts.date_start, ts.date_end,em.emp_id, em.emp_name, te.position_id, p.position_name from TEAM_SCHEDULE ts
		                            inner join TSCH_EMPLOYEES te on te.tsch_id = ts.tsch_id
		                            inner join LOCATION loc on loc.location_id = ts.location_id
		                            inner join EMPLOYEES em on em.emp_id = te.emp_id
		                            inner join POSITIONS p on p.position_id = te.position_id
" + System.Environment.NewLine;

            StrQuery += "where te.is_delete=0" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "and cast(ts.date_start as date)>=cast(@date_start as date) or  cast(ts.date_end as date)<=cast(@date_end as date)" + System.Environment.NewLine;

            }
            string roleUser = getRoleUser(userid);
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and ts.tsch_id = (select tsch_id from TSCH_EMPLOYEES te2 inner join USER_EMPLOYEES ue2 on ue2.emp_id=te2.emp_id where ue2.userid=@userid)" + System.Environment.NewLine;
            }
            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += " order by  ts.date_created desc,ts.tsch_id, p.position_id   " + System.Environment.NewLine;

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
        [Route("api/Report/getRptChangeTirePerformance/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptChangeTirePerformance(string date_start, string date_end, string userid)
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

            string prm = "";
            string StrQuery = @"select 
                                    stc.year, stc.month, stc.date, stc.group_id,
                                    tc.date_created as date, tc.location_id, loc.location, tc.vehicle_id, tc.old_tire_id, i1.unique_code as old_tire_unique_code, t1.rfid as old_tire_rfid, tc.new_tire_id, i2.unique_code as new_tire_unique_code, t2.rfid as new_tire_rfid,
                                    tc.km, tc.time_start, tc.time_end, 
                                    cast(DATEDIFF(minute, tc.time_start, tc.time_end) as varchar) + ' minute '  as duration,
                                    tc.notes,au.UserId, au.name
                                    from TIRE_CHANGE tc
			                        inner join schedule_tire_change stc on stc.tire_change_id = tc.tire_change_id
                                    inner join LOCATION loc on loc.location_id=tc.location_id
                                    inner join tire t1 on t1.tire_id = tc.old_tire_id
                                    inner join item i1 on i1.tire_code = t1.tire_code
                                    inner join tire t2 on t2.tire_id = tc.new_tire_id
                                    inner join item i2 on i2.tire_code = t2.tire_code
                                    inner join AspNetUsers au on au.UserId=tc.userid
                                    inner join USER_EMPLOYEES ue on ue.userid = au.UserId" + System.Environment.NewLine;

            StrQuery += "where tc.is_delete=0" + System.Environment.NewLine;
            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "and cast(tc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;
             
            }
            string roleUser = getRoleUser(userid);
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and tc.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += @" group by 
			                stc.year, stc.month, stc.date, stc.group_id,
                            tc.date_created, tc.location_id, loc.location, tc.vehicle_id, tc.old_tire_id, i1.unique_code, t1.rfid, tc.new_tire_id, i2.unique_code , t2.rfid,
                            tc.km, tc.time_start, tc.time_end,tc.notes,au.UserId, au.name
			                order by tc.date_created desc, DATEDIFF(minute, tc.time_start, tc.time_end) asc  " + System.Environment.NewLine;

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
        [Route("api/Report/getRptListTireDetail/{brand_id}/{location_id}/{tire_code}")]
        public IHttpActionResult getRptListTireDetail(string brand_id, string location_id, string tire_code)
        {
            string prm = "";
            string StrQuery = @"select t.tire_id, t.tire_code, case when t.rfid = '0' then '' else t.rfid end as rfid, i.tire_name, i.size, i.otd, i.pattern, i.load_rating_single, i.load_rating_double, i.play_rating, i.standard_pressure_single, i.standard_pressure_double
                    , bt.brand_tire_name, s.supplier_name, loc.location_id, loc.location,
					(select top 1 pr.process from tire_tracking tt2 inner join PROCESS pr on pr.process_id=tt2.process_id where tt2.date_created=max(tt.date_created) and tire_id=tt.tire_id order by  tt2.date_created desc, tt2.process_id desc) as process, max(tt.date_created) as tracking_date--, tt.process_id
                    from tire t
                    inner join item i on i.tire_code=t.tire_code
                    inner join brand_tire bt on bt.brand_tire_id=i.brand_tire_id
                    inner join SUPPLIER s on s.supplier_id= i.supplier_id
                    inner join location loc on loc.location_id=t.location_id
                    left join TIRE_TRACKING tt on tt.tire_id=t.tire_id
                    where t.tire_id not in ('XXX') and t.is_delete=0 and t.tire_id not in (select tire_id from DISPOSAL_DETAIL)
					
                    " + System.Environment.NewLine; 
            if (brand_id != "0" && brand_id != "-" && brand_id != "")
            {
                prm = "and i.brand_tire_id=@brand_tire_id" + System.Environment.NewLine;

            }

            if (location_id != "0" && location_id != "-" && location_id != "")
            {
                prm = "and t.location_id=@location_id" + System.Environment.NewLine;

            }
            string rep_tire_code = tire_code.Replace("-", ".");
            if (tire_code != "0" && tire_code != "-" && tire_code != "")
            {
                prm = "and t.tire_code=@tire_code" + System.Environment.NewLine;

            }
            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += @" group by t.tire_id,tt.tire_id, t.tire_code, t.rfid,  i.tire_name, i.size, i.otd, i.pattern, i.load_rating_single, i.load_rating_double, i.play_rating, i.standard_pressure_single, i.standard_pressure_double
                    , bt.brand_tire_name, s.supplier_name, loc.location_id, loc.location
					order by t.tire_id  " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { brand_id, location_id, tire_code = rep_tire_code }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }
        [HttpGet]
        [Route("api/Report/getRptChangeTirePerformanceDetail/{year}/{month}/{date}/{group_id}")]
        public IHttpActionResult getRptChangeTirePerformanceDetail(string year, string month, string date, string group_id)
        {
            string prm = "";
            string StrQuery = @"


                               
				select au.UserId,em.emp_id, em.emp_name
				, pos.position_id, pos.position_name
                            from TIRE_CHANGE tc
                            inner join LOCATION loc on loc.location_id=tc.location_id
                            inner join AspNetUsers au on au.UserId=tc.userid
							inner join schedule_tire_change stc on stc.tire_change_id = tc.tire_change_id
							inner join schedules sch on sch.year= stc.year and sch.month=stc.month and sch.date=stc.date and sch.location_id = stc.location_id and sch.group_id=stc.group_id and sch.shift_id=stc.shift_id
                            inner join EMPLOYEES em on em.emp_id = stc.emp_id
							inner join groups_employees ge on ge.group_id = stc.group_id and ge.emp_id=stc.emp_id
                            inner join POSITIONS pos on pos.position_id = ge.position_id
                            where sch.year=@year and sch.month=@month and sch.date=@date and sch.group_id=@group_id
							group by au.UserId,em.emp_id, em.emp_name
							,pos.position_id, pos.position_name
                              	
                        " + System.Environment.NewLine;
            

            //StrQuery += prm;
            ////StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            //StrQuery += " order by DATEDIFF(minute, tc.time_start, tc.time_end) asc  " + System.Environment.NewLine;

            try
            {
                SqlConnection dbConn = new SqlConnection(con);
                dbConn.Open();
                var data = dbConn.Query(StrQuery, new { year, month, date, group_id }, null, true, commandTimeout: 0, commandType: null).ToList();
                dbConn.Close();
                return Json((new { records = data.Count, data = data }));
            }

            catch (Exception e)
            {
                return Json((new { msg = e.Message.ToString(), data = "" }));
            }

        }

        [HttpGet]
        [Route("api/Report/getRptChangeTirePerformanceRdl/{date_start}/{date_end}/{userid}")]
        public IHttpActionResult getRptChangeTirePerformanceRdl(string date_start, string date_end, string userid)
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

            string prm = "";
            string StrQuery = @"
                               
                                
							select 
                                         stc.year+stc.month+cast(FORMAT(stc.date, 'yMd') as varchar)+tc.location_id+cast(FORMAT(tc.time_start, 'yMdhms') as varchar)+cast(FORMAT(tc.time_end, 'yMdhms') as varchar) as caption,
                                        tc.date_created as date, tc.location_id, loc.location, tc.vehicle_id, tc.old_tire_id, i1.unique_code as old_tire_unique_code, t1.rfid as old_tire_rfid, tc.new_tire_id, i2.unique_code as new_tire_unique_code, t2.rfid as new_tire_rfid,
                                        tc.km, tc.time_start, tc.time_end, 
                                        cast(DATEDIFF(minute, tc.time_start, tc.time_end) as varchar) + ' minute '   as duration,
                                        tc.notes,au.UserId,em.emp_id, em.emp_name,  pos.position_id, pos.position_name
                                        from TIRE_CHANGE tc
                                        inner join LOCATION loc on loc.location_id=tc.location_id
                                        inner join tire t1 on t1.tire_id = tc.old_tire_id
                                        inner join item i1 on i1.tire_code = t1.tire_code
                                        inner join tire t2 on t2.tire_id = tc.new_tire_id
                                        inner join item i2 on i2.tire_code = t2.tire_code
                                        inner join AspNetUsers au on au.UserId=tc.userid
										inner join schedule_tire_change stc on stc.tire_change_id = tc.tire_change_id
										inner join schedules sch on sch.year= stc.year and sch.month=stc.month and sch.date=stc.date and sch.location_id = stc.location_id and sch.group_id=stc.group_id and sch.shift_id=stc.shift_id
										inner join EMPLOYEES em on em.emp_id = stc.emp_id
										inner join groups_employees ge on ge.group_id = stc.group_id and ge.emp_id=stc.emp_id
										inner join POSITIONS pos on pos.position_id = ge.position_id

" + System.Environment.NewLine;
            StrQuery += "where tc.is_delete=0" + System.Environment.NewLine;

            if (date_start != "0" && date_start != "-" && date_start != "")
            {
                prm = "and cast(tc.date_created as date) between cast(@date_start as date) and cast(@date_end as date)" + System.Environment.NewLine;

            }
            string roleUser = getRoleUser(userid);
            if (roleUser != "0" && roleUser != "-" && roleUser != "")
            {
                StrQuery += "and tc.userid=@userid" + System.Environment.NewLine;
            }
            StrQuery += prm;
            //StrQuery += " and t.process_id not in (100, 101, 104, 105, 110) ";
            StrQuery += @" group by 
			stc.year, stc.month, stc.date, stc.group_id,
            tc.date_created, tc.location_id, loc.location, tc.vehicle_id, tc.old_tire_id, i1.unique_code, t1.rfid, tc.new_tire_id, i2.unique_code , t2.rfid,
            tc.km, tc.time_start, tc.time_end,tc.notes,au.UserId, au.name,em.emp_id, em.emp_name, pos.position_id, pos.position_name
			order by tc.date_created desc, DATEDIFF(minute, tc.time_start, tc.time_end) asc  " + System.Environment.NewLine;

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
        #endregion


        [HttpGet]
        [Route("api/Report/getTireBySerialNumber/{Text?}")]
        public IHttpActionResult getTireBySerialNumber(string Text = null)
        {

            DataTable data = Report.getDataTireBySerialNumber(Text);
            return Json((new { records = data.Rows.Count, data = data }));

        }

      


    }
}
