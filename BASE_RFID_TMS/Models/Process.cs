using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using static BASE_RFID_TMS.Models.GlobalVar;

namespace BASE_RFID_TMS.Models
{
    public class Process
    {
        public class cCrudPickup
        {
            public string pickup_no { get; set; }
            public DateTime date_created { get; set; }
            public string location_id { get; set; }
            public string vehicle_no { get; set; }
            public string driver { get; set; }
            public string destination { get; set; }
            public string notes { get; set; }
            public string userid { get; set; }
            public string outbound_no { get; set; }
            public string data { get; set; }
        }
        public class cCrudScrap
        {
            public string scrap_no { get; set; }
            public string date_created { get; set; }
            public string tire_id { get; set; }
            public string reason_id { get; set; }
            public string area_damage_id { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
        }
        public class cCrudUploadImageLetter
        {
            //public [FromForm] IFormFile file { get; set; }
            public string id { get; set; }
            public int type { get; set; }
        }
    public class cCrudServiceMaintenance
        {
            public string inspect_setup_id { get; set; }
            public string area_damage_id { get; set; }
            public string date_created { get; set; }
            public string reason_id { get; set; }
            public string data_observation { get; set; }
            public string vehicle_id { get; set; }
            public string axis_id { get; set; }
            public string location_id { get; set; }
            public string km { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string tire_id { get; set; }
            public string new_tire_id { get; set; }
            public string notes { get; set; }
            public string userid { get; set; }
        }

        public class cCrudDataObservation
        {
            public string observation_item_id { get; set; }
            public string value { get; set; }
            public string notes { get; set; }
        }
        public class cVehicle
        {
            public string vehicle_id { get; set; }
            public string date_created { get; set; }
            public string rfid { get; set; }
            public string userid { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }
        public class cKMTireDirectly
        {
            public string tire_id { get; set; }
            public string date_created { get; set; }
            public string location_id { get; set; }
            public string km { get; set; }
            public string userid { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }
        public class cOdometer
        {
            public string fleet_id { get; set; }
            public string date_created { get; set; }
            public string location_id { get; set; }
            public string km { get; set; }
            public string hm { get; set; }
            public string userid { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }
        public class cCrudPickupDetail
        {
            //public string pickup_no { get; set; }
            //public DateTime date_created { get; set; }
            public string tire_id { get; set; }
            public string rfid { get; set; }
            //public string remark { get; set; }
        }
        public class cCrudScrapTireDetail
        {
            public string tire_id { get; set; }
            public string reason_id { get; set; }
            public string area_damage_id { get; set; }
            public string description { get; set; }
        }

        public class cCrudDisposal
        {
            public string disposal_no { get; set; }
            public string date_created { get; set; }
            public string location_id { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudDisposalTireDetail
        {
            public string tire_id { get; set; }
            //public string brand_id { get; set; }
            public string description { get; set; }
        }


        public class cCrudSLWScrap
        {
            public string slw_no { get; set; }
            public string location_id { get; set; }
            public string date_created { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudUtilityTireStokInUnitPerFleet
        {
            public string fleet_id { get; set; }
            public string fleet_name { get; set; }
            public string location_type_id { get; set; }
            public string km { get; set; }
            public string hm { get; set; }
            public string description { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudUtilityTireStokInUnitPerFleetDataVehicle
        {
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string brand_vhc_id { get; set; }
            public string axis_type { get; set; }
            public string vehicle_type_id { get; set; }
            public string position { get; set; }
            public string rfid { get; set; }
            public string data { get; set; }
        }
        public class cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire
        {
            public string serial_number { get; set; }
            public string tire_code { get; set; }
            public string tire_name { get; set; }
            public string tire_type_id { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string rfid { get; set; }
            public string axis_id { get; set; }
        }
        public class cCrudUtilityTireStokInUnitPerVehicle
        {
            public string date_created { get; set; }
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string brand_vhc_id { get; set; }
            public string axis_type { get; set; }
            public string vehicle_type_id { get; set; }
            public string rfid { get; set; }
            public string data { get; set; }
            public string userid { get; set; }
        }
        public class cCrudUtilityTireStokInUnitPerVehicleDetailTire
        {
            public string serial_number { get; set; }
            public string tire_code { get; set; }
            public string tire_name { get; set; }
            public string tire_type_id { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string rfid { get; set; }
            public string axis_id { get; set; }
        }
        public class cCrudUtilityTireStokInLocation
        {
            public string location_id { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudUtilityTireStokInLocationDetail
        {
            public string serial_number { get; set; }
            public string tire_code { get; set; }
            public string tire_name { get; set; }
            public string tire_type_id { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string rfid { get; set; }
            public string status_id { get; set; }
        }
        public class cCrudInbound
        {
            public string inbound_id { get; set; }
            public string location_id { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudInboundDetail
        {
            public string tire_id { get; set; }
            //public string brand_id { get; set; }
        }

        public class cCrudTyreNonPTSIDetail
        {
            public string tire_id { get; set; }
            public string item_code { get; set; }
            public string date_created { get; set; }
            public string username { get; set; }
            //public string brand_id { get; set; }
        }
        public class cCrudSync
        {
            public string data { get; set; }
        }
        public class cCrudSyncOfflineActivity
        {
            public string trx_no { get; set; }
            public string trx_date { get; set; }
            public string location_id { get; set; }
            public string fleet_id { get; set; }
            public string km { get; set; }
            public string hm { get; set; }
            public string userid { get; set; }
            public List<cCrudSyncOfflineActivityDetail> data { get; set; }
        }

        public class cCrudSyncOfflineActivityDetail
        {
            public string sort { get; set; }
            public string process_id { get; set; }
            public string vehicle_id { get; set; }
            public string number_old { get; set; }
            public string number_new { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string user_id { get; set; }
        }
        public class cCrudSyncDetail
        {
            public string rfid { get; set; }
            public string tag_id { get; set; }
            public string tag_type_id { get; set; }
            public string spec { get; set; }
            public string location_id { get; set; }
            public string process_id { get; set; }
            public string userid { get; set; }
            public string date_created { get; set; }
            //public string brand_id { get; set; }
        }
        public class cCrudReplaceTag
        {
            public string number { get; set; }
            public string type { get; set; }
            public string location_id { get; set; }
            public string old_rfid { get; set; }
            public string new_rfid { get; set; }
            public string notes { get; set; }
            public string username { get; set; }
        }
        public class cCrudChangeEpc
        {
            public string number { get; set; }
            public string type { get; set; }
            public string location_id { get; set; }
            public string new_rfid { get; set; }
            public string username { get; set; }
        }
        public class cCrudWriteData
        {
            public string data { get; set; }
        }
        public class cCrudWriteDataDetail
        {
            public string vhc_type { get; set; }
            public string type_spec { get; set; }
            public string type_number { get; set; }
            public string type_epc { get; set; }
            public string rfid { get; set; }
            public string tag_id { get; set; }
            public string tag_type_id { get; set; }
            public string spec { get; set; }
            public string location_id { get; set; }
            public string process_id { get; set; }
            public string brand { get; set; }
            public string axis_type { get; set; }
            public string username { get; set; }
            public string date_created { get; set; }
            //public string brand_id { get; set; }
        }
        public class cCrudTireChange
        {
            public string tire_change_id { get; set; }
            public string date_created { get; set; }
            public string vehicle_id { get; set; }
            public string location_id { get; set; }
            public string notes { get; set; }
            public string km { get; set; }
            public string time_start { get; set; }
            public string time_end { get; set; }
            public string userid { get; set; }
            public string data_old { get; set; }
            public string data_new { get; set; }
        }

        public class cCrudTireChangeOldData
        {
            public string sort { get; set; }
            public string axis_id { get; set; }
            public string tire_id { get; set; }
        }
        public class cCrudTireChangeNewData
        {
            public string sort { get; set; }
            public string axis_id { get; set; }
            public string tire_id { get; set; }
        }
        public class cCrudTyreChecking
        {

            public string tire_id { get; set; }
            public string tire_code { get; set; }
            public string date_created { get; set; }
            public string location_id { get; set; }
            public string epc_rfid { get; set; }
            public string process_id { get; set; }
            public string userid { get; set; }
        }
        public class cCrudVehicleChange
        {

            public string fleet_id { get; set; }
            public string vehicle_change_no { get; set; }
            public string date_created { get; set; }
            public string notes { get; set; }
            public string km { get; set; }
            public string hm { get; set; }
            public string userid { get; set; }
            public string data_old { get; set; }
            public string data_new { get; set; }
        }
        public class cCrudVehicleCheckDataNew
        {
            public string sort { get; set; }
            public string fleet_id { get; set; }
            public string vehicle_id { get; set; }
        }
        public class cCrudVehicleChangeOldData
        {
            public string sort { get; set; }
            public string vehicle_id { get; set; }
        }
        public class cCrudVehicleChangeNewData
        {
            public string sort { get; set; }
            public string vehicle_id { get; set; }
        }
        public class cCrudVehicleChangeNewDataFromFds
        {
            public string sort { get; set; }
            public string vehicle_id { get; set; }
            public string axis_type { get; set; }
            public string vehicle_type_id { get; set; }
        }
        public class cCrudOutbound
        {
            public string outbound_id { get; set; }
            public string location_id { get; set; }
            public string date_created { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudOutboundDetail
        {
            public string tire_id { get; set; }

            public string location_id { get; set; }
            //public string brand_id { get; set; }
            public string description { get; set; }
        }
        public class cCrudSLWScrapTireDetail
        {
            public string tire_id { get; set; }
            //public string brand_id { get; set; }
            public string description { get; set; }
        }

        public class cCrudLetterLoose
        {
            public string letter_no { get; set; }
            public string letter_date { get; set; }
            public string shift_groud_merch { get; set; }
            public string location_id { get; set; }
            public string vehicle_id { get; set; }
            public string mechanic { get; set; }
            public string description { get; set; }
            public string photo { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudLetterLooseDetail
        {
            public string axis_id { get; set; }
            public string tire_id { get; set; }
        }


        public class cCrudLetterFound
        {
            public string letter_no { get; set; }
            public string letter_date { get; set; }
            public string shift_groud_merch { get; set; }
            public string location_id { get; set; }
            public string finder { get; set; }
            public string qty_found { get; set; }
            public string description { get; set; }
            public string photo { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }
        public class cCrudLetterFoundDetail
        {
            public string tire_id { get; set; }
            public string letter_loose_no { get; set; }
            public string letter_loose_date { get; set; }
        }
        public static cRespon InsertTireChange(cCrudTireChange p)
        {
            List<cCrudTireChangeOldData> ListOld = new List<cCrudTireChangeOldData>();

            ListOld = JsonConvert.DeserializeObject<List<cCrudTireChangeOldData>>(p.data_old);

            List<cCrudTireChangeNewData> ListNew = new List<cCrudTireChangeNewData>();

            ListNew = JsonConvert.DeserializeObject<List<cCrudTireChangeNewData>>(p.data_new);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            string unit_location_id = Master.getDefaultUnitLocationId();
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string tsch_id = getTschIdByUser(p.userid, p.date_created);
            if (tsch_id == "") {

                returnRes.message = "Access denied. Please create your schedule maintenance!";
                returnRes.flag = false;
            } else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            foreach (var dr in ListOld)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_created, userid=@userid WHERE tire_id =@tire_id and vehicle_id=@vehicle_id 
                            "
                                , new
                                {
                                    p.vehicle_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);

                                conn.Execute(@"     
                                    UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_created, userid=@userid where tire_id =@tire_id and axis_id=@axis_id
                                    
                                    UPDATE TIRE SET location_id = @location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                    "
                               , new
                               {
                                   dr.axis_id,
                                   p.vehicle_id,
                                   p.location_id,
                                   p.date_created,
                                   dr.tire_id,
                                   p.userid
                               }, tran);

                                conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);

                                conn.Execute(@"
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    unit_location_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                             values (@tire_id,@current_epc_tire,'103','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                {
                                    dr.tire_id,
                                    p.userid,
                                    current_epc_tire,
                                    p.location_id,
                                    p.date_created,
                                }, tran);

                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='5', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    dr.tire_id,
                                    p.userid,
                                    p.date_created
                                }, tran);

                            }


                            foreach (var dn in ListNew)
                            {
                                string current_epc_tire = getEPCCurrentTire(dn.tire_id);
                                conn.Execute(@"INSERT INTO TIRE_UNIT
                                                (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                            VALUES
                                                (@vehicle_id,@tire_id,NULL,0,@date_created,@userid)"
                                        , new
                                        {
                                            p.vehicle_id,
                                            dn.tire_id,
                                            p.date_created,
                                            p.userid
                                        }, tran);
                                conn.Execute(@" INSERT INTO TIRE_AXIS
                                                (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                            VALUES
                                                (@vehicle_id,@tire_id,@axis_id,@date_created,NULL,0, @userid)
                                        UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                        "
                                    , new
                                    {
                                        p.userid,
                                        dn.axis_id,
                                        p.vehicle_id,
                                        unit_location_id,
                                        p.date_created,
                                        dn.tire_id,
                                    }, tran);
                                conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    unit_location_id,
                                    p.date_created,
                                    dn.tire_id,
                                    p.userid
                                }, tran);



                                conn.Execute(@"
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    dn.tire_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@tire_id,@current_epc_tire,'102','XXX','XXX',@unit_location_id,0,@date_created,@userid)", new
                                {
                                    dn.tire_id,
                                    p.date_created,
                                    current_epc_tire,
                                    unit_location_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    dn.tire_id,
                                    p.userid,
                                    p.date_created
                                }, tran);

                            }

                            string tire_change_id = getNextTireChangeId(yearmonth);

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_change_id from TIRE_CHANGE where tire_change_id=@tire_change_id) ) 
                                        BEGIN 
                                            INSERT INTO TIRE_CHANGE 
                                            (tire_change_id,notes, vehicle_id,tsch_id, location_id, km, time_start, time_end, date_modified,date_created,is_delete,userid)
                                            values (@tire_change_id,@notes, @vehicle_id,@tsch_id, @location_id,@km,@time_start, @time_end,@date_created,@date_created,0,@userid)
                                        END 

                                ", new
                            {
                                tire_change_id,
                                p.vehicle_id,
                                tsch_id,
                                p.location_id,
                                p.notes,
                                p.time_start,
                                p.time_end,
                                p.km,
                                p.date_created,
                                p.userid
                            }, tran);
                            int no = 0;
                            foreach (var dr in ListOld)
                            {
                                var old_tire_id = dr.tire_id;
                                var new_tire_id = ListNew[no].tire_id;
                                conn.Execute(@"
                                        INSERT INTO TIRE_CHANGE_DETAIL 
                                        (tire_change_id,date,sort,old_tire_id,new_tire_id,date_created,date_modified,is_delete,userid)
                                        values (@tire_change_id,@date_created,@sort,@old_tire_id,@new_tire_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    tire_change_id,
                                    dr.sort,
                                    old_tire_id,
                                    new_tire_id,
                                    p.date_created,
                                    p.userid
                                }, tran);
                                no++;
                            }


                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            
            if (returnRes.flag == true)
            {
                returnRes.message = "Change tire success";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertVehicleChangeFromFds(cCrudVehicleChange p)
        {
            List<cCrudVehicleChangeOldData> ListOld = new List<cCrudVehicleChangeOldData>();

            ListOld = JsonConvert.DeserializeObject<List<cCrudVehicleChangeOldData>>(p.data_old);

            List<cCrudVehicleChangeNewDataFromFds> ListNew = new List<cCrudVehicleChangeNewDataFromFds>();

            ListNew = JsonConvert.DeserializeObject<List<cCrudVehicleChangeNewDataFromFds>>(p.data_new);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
            string rubah = "";
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string location_type_id = getLocationTypeFleet(p.fleet_id);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //for (var a = 0; a < ListOld.Count; a++)
                        //{
                        //    for (var b = 0; b < ListNew.Count; b++)
                        //    {
                        //        if (ListOld[a])
                        //        {

                        //        }
                        //        else
                        //        {

                        //        }
                        //    }
                        //}
                        int no = 0;
                        var new_fleet_id = "";
                        string axis_type_new = "";
                        string vehicle_type_id_new = "";
                        foreach (var dr in ListNew)
                        {
                            if (dr.sort == "0")
                            {
                                axis_type_new = "1";
                                vehicle_type_id_new = "1";
                            }
                            else {

                                axis_type_new = dr.axis_type;
                                vehicle_type_id_new = dr.vehicle_type_id;
                            }
                            conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE WHERE vehicle_id = @vehicle_id))
                                        BEGIN
                                            INSERT INTO VEHICLE
                                                (vehicle_id,brand_vhc_id,vehicle_name, description,date_created,axis_type, vehicle_type_id,vehicle_img,vehicle_rfid, km, hm,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,'OWN',@vehicle_id, '',@date_created,@axis_type_new, @vehicle_type_id_new,'','0', 0,0,'False',@userid)
                                        END"
                            , new
                            {
                                date_created,
                                dr.vehicle_id,
                                axis_type_new,
                                vehicle_type_id_new,
                                p.userid

                            }, tran);
                        }
                        if (ListOld.Count == ListNew.Count)
                        { //Jika jumlah baris lama dan baru sama
                            foreach (var dr in ListOld)
                            {
                                var sama = 0;
                                if (dr.sort == ListNew[no].sort && dr.vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else
                                {
                                    //"ada perubahan data";
                                    if (dr.sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[no].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            dr.vehicle_id,
                                            new_vehicle_id,
                                            dr.sort,
                                            p.userid
                                        }, tran);

                                    }


                                }
                                no++;
                            }
                        }
                        else if (ListOld.Count < ListNew.Count)
                        {
                            int one_fleet = 0;
                            foreach (var dr in ListOld)
                            {
                                var sama = 0;
                                if (dr.sort == ListNew[no].sort && dr.vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else
                                {
                                    //"ada perubahan data";
                                    if (dr.sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id  and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                        one_fleet = 1;
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[no].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            dr.vehicle_id,
                                            new_vehicle_id,
                                            dr.sort,
                                            p.userid
                                        }, tran);

                                    }


                                }
                                no++;
                            }
                            if (one_fleet == 0)
                            {
                                int index_list_new = 0;
                                for (int j = 0; j < (ListNew.Count - ListOld.Count); j++)
                                {
                                    index_list_new = ListOld.Count;
                                    conn.Execute(@"
                                    IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                        INSERT INTO FLEET_VEHICLE
                                     (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                     values (@fleet_id, @vehicle_id, @sort,@date_created, (select top 1 description from fleet_vehicle where fleet_id=@fleet_id),'0', @date_modified,@userid)
                                   END
                                     ELSE
                                                BEGIN 
                                                  update fleet_vehicle set is_delete=0, date_modified=@date_created, 
                                                userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                ", new
                                    {
                                        p.fleet_id,
                                        ListNew[index_list_new].vehicle_id,
                                        ListNew[index_list_new].sort,
                                        date_created,
                                        date_modified,
                                        p.userid
                                    }, tran);
                                }
                            }

                        }
                        else if (ListOld.Count > ListNew.Count)
                        {
                            int one_fleet = 0;
                            for (int i = 0; i < ListNew.Count; i++)
                            {
                                var sama = 0;
                                if (ListOld[i].sort == ListNew[no].sort && ListOld[i].vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else
                                {
                                    if (ListOld[i].sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                        one_fleet = 1;
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[i].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            ListOld[i].vehicle_id,
                                            new_vehicle_id,
                                            ListOld[i].sort,
                                            p.userid
                                        }, tran);

                                    }
                                }
                            }

                            if (one_fleet == 0)
                            {
                                int index_list_old = 0;
                                for (int j = 0; j < (ListOld.Count - ListNew.Count); j++)
                                {
                                    index_list_old = ListNew.Count;
                                    conn.Execute(@"
                                           
                                                  update fleet_vehicle set is_delete=1, date_modified=@date_created, 
                                                userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                
                                       
                                ", new
                                    {
                                        p.fleet_id,
                                        ListOld[index_list_old].vehicle_id,
                                        ListOld[index_list_old].sort,
                                        date_created,
                                        date_modified,
                                        p.userid
                                    }, tran);
                                }
                            }
                        }

                        string vhc_change_no = getNextVhcChangeNo(yearmonth);



                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 vehicle_change_no from VEHICLE_CHANGE where vehicle_change_no=@vhc_change_no) ) 
                                        BEGIN 
                                            INSERT INTO VEHICLE_CHANGE 
                                            (vehicle_change_no,date,notes, km, hm, date_modified,date_created,is_delete,userid)
                                            values (@vhc_change_no,@date_created,@notes,@km,@hm,@date_created,@date_created,0,@userid)
                                        END 

                                ", new
                        {
                            vhc_change_no,
                            p.notes,
                            p.km,
                            p.hm,
                            p.date_created,
                            p.userid
                        }, tran);
                        var new_fleet_id2 = "";

                        if (ListOld.Count == ListNew.Count)
                        {
                            var no2 = 0;
                            foreach (var dr in ListOld)
                            {
                                var old_vehicle_id = dr.vehicle_id;
                                var new_vehicle_id = ListNew[no2].vehicle_id;

                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else
                                {
                                    new_fleet_id2 = new_fleet_id;
                                }

                                conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    dr.sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                                no2++;
                            }
                        }
                        else if (ListOld.Count < ListNew.Count)
                        {
                            var no2 = 0;
                            foreach (var dr in ListOld)
                            {
                                var old_vehicle_id = dr.vehicle_id;
                                var new_vehicle_id = ListNew[no2].vehicle_id;
                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else
                                {
                                    new_fleet_id2 = new_fleet_id;
                                }
                                conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    dr.sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                                no2++;
                            }
                            for (int j = 0; j < (ListNew.Count - ListOld.Count); j++)
                            {
                                var index_list_new2 = ListOld.Count;
                                var old_vehicle_id = "XXX";
                                var new_vehicle_id = ListNew[index_list_new2].vehicle_id;
                                conn.Execute(@"
                                   INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListNew[index_list_new2].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid

                                }, tran);
                            }

                        }
                        else if (ListOld.Count > ListNew.Count)
                        {
                            for (int i = 0; i < ListNew.Count; i++)
                            {
                                var old_vehicle_id = ListOld[i].vehicle_id;
                                var new_vehicle_id = ListNew[i].vehicle_id;
                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else
                                {
                                    new_fleet_id2 = new_fleet_id;
                                }
                                conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListOld[i].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                            }
                            for (int j = 0; j < (ListOld.Count - ListNew.Count); j++)
                            {
                                var index_list_old2 = ListNew.Count;
                                var old_vehicle_id = ListOld[index_list_old2].vehicle_id;
                                var new_vehicle_id = "XXX";
                                conn.Execute(@"
                                   INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListOld[index_list_old2].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid

                                }, tran);
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Change vehicle success.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        //public static cRespon setTireChecking(cCrudTyreChecking p)
        //{
        //    cRespon returnRes = new cRespon();
        //    returnRes.flag = true;
        //    returnRes.status = "";
        //    returnRes.message = "";
        //    int RowAffected = 0;
        //    DateTime date_created = DateTime.Now;
        //    DateTime date_modified = DateTime.Now;
        //    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
        //    {
        //    }
        //}
        public static cRespon InsertVehicleChange(cCrudVehicleChange p)
        {
            List<cCrudVehicleChangeOldData> ListOld = new List<cCrudVehicleChangeOldData>();

            ListOld = JsonConvert.DeserializeObject<List<cCrudVehicleChangeOldData>>(p.data_old);

            List<cCrudVehicleChangeNewData> ListNew = new List<cCrudVehicleChangeNewData>();

            ListNew = JsonConvert.DeserializeObject<List<cCrudVehicleChangeNewData>>(p.data_new);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
            string rubah = "";
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string location_type_id = getLocationTypeFleet(p.fleet_id);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        //for (var a = 0; a < ListOld.Count; a++)
                        //{
                        //    for (var b = 0; b < ListNew.Count; b++)
                        //    {
                        //        if (ListOld[a])
                        //        {

                        //        }
                        //        else
                        //        {

                        //        }
                        //    }
                        //}
                        int no = 0;
                        var new_fleet_id = "";
                        if (ListOld.Count == ListNew.Count)
                        { //Jika jumlah baris lama dan baru sama
                            foreach (var dr in ListOld)
                            {
                                var sama = 0;
                                if (dr.sort == ListNew[no].sort && dr.vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else
                                {
                                    //"ada perubahan data";
                                    if (dr.sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[no].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            dr.vehicle_id,
                                            new_vehicle_id,
                                            dr.sort,
                                            p.userid
                                        }, tran);

                                    }


                                }
                                no++;
                            }
                        }
                        else if (ListOld.Count < ListNew.Count)
                        {
                            int one_fleet = 0;
                            foreach (var dr in ListOld)
                            {
                                var sama = 0;
                                if (dr.sort == ListNew[no].sort && dr.vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else
                                {
                                    //"ada perubahan data";
                                    if (dr.sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id  and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                        one_fleet = 1;
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[no].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            dr.vehicle_id,
                                            new_vehicle_id,
                                            dr.sort,
                                            p.userid
                                        }, tran);
                                        
                                    }


                                }
                                no++;
                            }
                            if (one_fleet == 0) {
                                int index_list_new = 0;
                                for (int j = 0; j < (ListNew.Count - ListOld.Count); j++)
                                {
                                    index_list_new = ListOld.Count;
                                    conn.Execute(@"
                                    IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                        INSERT INTO FLEET_VEHICLE
                                     (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                     values (@fleet_id, @vehicle_id, @sort,@date_created, (select top 1 description from fleet_vehicle where fleet_id=@fleet_id),'0', @date_modified,@userid)
                                   END
                                     ELSE
                                                BEGIN 
                                                  update fleet_vehicle set is_delete=0, date_modified=@date_created, 
                                                userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                ", new
                                    {
                                        p.fleet_id,
                                        ListNew[index_list_new].vehicle_id,
                                        ListNew[index_list_new].sort,
                                        date_created,
                                        date_modified,
                                        p.userid
                                    }, tran);
                                }
                            }
                            
                        }
                        else if (ListOld.Count > ListNew.Count) {
                            int one_fleet = 0;
                            for (int i = 0; i < ListNew.Count; i++)
                            {
                                var sama = 0;
                                if (ListOld[i].sort == ListNew[no].sort && ListOld[i].vehicle_id == ListNew[no].vehicle_id)
                                {
                                    //"Tidak ada perubahan data";
                                }
                                else {
                                    if (ListOld[i].sort == "0")
                                    { //Jika yang diganti adalah kepala truk
                                        conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id not in (@fleet_id)

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            p.userid
                                        }, tran);

                                        conn.Execute(@"IF(NOT EXISTS(select TOP 1 fleet_id from FLEET where fleet_id = @vehicle_id and is_delete=0))
                                                    BEGIN
                                                     INSERT INTO FLEET
                                                     (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, date_modified, userid)
                                                     values(@vehicle_id, @location_type_id, 'New Fleet ' + @vehicle_id, (select km from vehicle where vehicle_id=@vehicle_id), (select hm from vehicle where vehicle_id=@vehicle_id), 'From '+@fleet_id, 0, @date_created, @date_created, @userid)
                                                    END
                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            ListNew[no].vehicle_id,
                                            date_modified,
                                            location_type_id,
                                            p.userid
                                        }, tran);
                                        new_fleet_id = ListNew[no].vehicle_id;
                                        foreach (var dt in ListNew)
                                        {

                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                            BEGIN 
                                                INSERT INTO FLEET_VEHICLE
                                             (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                             values (@new_fleet_id, @vehicle_id, @sort,@date_created, 'From '+@fleet_id,'0', @date_modified,@userid)
                                            END 
                                            ELSE
                                                BEGIN 
                                                  update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@new_fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                END 

                                        ", new
                                            {
                                                new_fleet_id,
                                                dt.vehicle_id,
                                                dt.sort,
                                                date_created,
                                                p.fleet_id,
                                                date_modified,
                                                p.userid
                                            }, tran);

                                        }
                                        one_fleet = 1;
                                    }
                                    else
                                    {
                                        //Jika yang diganti adalah trailer/ vehicle
                                        var new_vehicle_id = ListNew[i].vehicle_id;
                                        conn.Execute(@"update fleet_vehicle set is_delete=0, vehicle_id=@new_vehicle_id, description='From '+ @vehicle_id +'-'+@fleet_id, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort

                                    ", new
                                        {
                                            p.date_created,
                                            p.fleet_id,
                                            date_modified,
                                            ListOld[i].vehicle_id,
                                            new_vehicle_id,
                                            ListOld[i].sort,
                                            p.userid
                                        }, tran);

                                    }
                                }
                            }
                             
                            if (one_fleet == 0)
                            {
                                int index_list_old = 0;
                                for (int j = 0; j < (ListOld.Count - ListNew.Count); j++)
                                {
                                    index_list_old = ListNew.Count;
                                    conn.Execute(@"
                                           
                                                  update fleet_vehicle set is_delete=1, date_modified=@date_created, 
                                                userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort
                                                
                                       
                                ", new
                                    {
                                        p.fleet_id,
                                        ListOld[index_list_old].vehicle_id,
                                        ListOld[index_list_old].sort,
                                        date_created,
                                        date_modified,
                                        p.userid
                                    }, tran);
                                }
                            }
                        }

                        string vhc_change_no = getNextVhcChangeNo(yearmonth);



                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 vehicle_change_no from VEHICLE_CHANGE where vehicle_change_no=@vhc_change_no) ) 
                                        BEGIN 
                                            INSERT INTO VEHICLE_CHANGE 
                                            (vehicle_change_no,date,notes, km, hm, date_modified,date_created,is_delete,userid)
                                            values (@vhc_change_no,@date_created,@notes,@km,@hm,@date_created,@date_created,0,@userid)
                                        END 

                                ", new
                        {
                            vhc_change_no,
                            p.notes,
                            p.km,
                            p.hm,
                            p.date_created,
                            p.userid
                        }, tran);
                        var new_fleet_id2 = "";
                        
                        if (ListOld.Count == ListNew.Count)
                        {
                            var no2 = 0;
                            foreach (var dr in ListOld)
                            {
                                var old_vehicle_id = dr.vehicle_id;
                                var new_vehicle_id = ListNew[no2].vehicle_id;

                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else {
                                    new_fleet_id2 = new_fleet_id;
                                }
                              
                                    conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    dr.sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                                no2++;
                            }
                        }
                        else if(ListOld.Count < ListNew.Count)
                        {
                            var no2 = 0;
                            foreach (var dr in ListOld)
                            {
                                var old_vehicle_id = dr.vehicle_id;
                                var new_vehicle_id = ListNew[no2].vehicle_id;
                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else
                                {
                                    new_fleet_id2 = new_fleet_id;
                                }
                                conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    dr.sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                                no2++;
                            }
                            for (int j = 0; j < (ListNew.Count - ListOld.Count); j++)
                            {
                                var index_list_new2 = ListOld.Count;
                                var old_vehicle_id = "XXX";
                                var new_vehicle_id = ListNew[index_list_new2].vehicle_id;
                                conn.Execute(@"
                                   INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListNew[index_list_new2].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                    
                                }, tran);
                            }

                        } else if (ListOld.Count > ListNew.Count)
                        {
                            for (int i = 0; i < ListNew.Count; i++)
                            {
                                var old_vehicle_id = ListOld[i].vehicle_id;
                                var new_vehicle_id = ListNew[i].vehicle_id;
                                if (new_fleet_id == "")
                                {
                                    new_fleet_id2 = p.fleet_id;
                                }
                                else
                                {
                                    new_fleet_id2 = new_fleet_id;
                                }
                                conn.Execute(@"
                                        INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListOld[i].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid
                                }, tran);
                            }
                            for (int j = 0; j < (ListOld.Count - ListNew.Count); j++)
                            {
                                var index_list_old2 = ListNew.Count;
                                var old_vehicle_id = ListOld[index_list_old2].vehicle_id;
                                var new_vehicle_id = "XXX";
                                conn.Execute(@"
                                   INSERT INTO VEHICLE_CHANGE_DETAIL 
                                        (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                        values (@vhc_change_no,@date_created,@sort,@fleet_id,@old_vehicle_id,@new_fleet_id2,@new_vehicle_id,@date_created,@date_created,0,@userid)
                                    
                                ", new
                                {
                                    vhc_change_no,
                                    ListOld[index_list_old2].sort,
                                    p.fleet_id,
                                    old_vehicle_id,
                                    new_fleet_id2,
                                    new_vehicle_id,
                                    p.date_created,
                                    date_modified,
                                    p.userid

                                }, tran);
                            }
                        }
                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Change vehicle success.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertLetterLooseTire(cCrudLetterLoose p)
        {
            List<cCrudLetterLooseDetail> ListDetail = new List<cCrudLetterLooseDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudLetterLooseDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool cekData = checkLetterNoDetachUsed(p.letter_no);
                if (cekData == false)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_loose_no from LETTER_LOOSE where letter_loose_no=@letter_no) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_LOOSE 
                                            (letter_loose_no,letter_loose_date,location_id, vehicle_id, shift_group_mech, mechanic, description, photo,date_created,date_modified,is_delete,userid)
                                            values (@letter_no,@letter_date,@location_id,@vehicle_id,@shift_groud_merch,@mechanic,@description,@photo,@date_created,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                p.letter_no,
                                p.letter_date,
                                p.location_id,
                                p.vehicle_id,
                                p.shift_groud_merch,
                                p.mechanic,
                                p.photo,
                                p.date_created,
                                p.description,
                                p.userid
                            }, tran);
                            foreach (var dr in ListDetail)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_loose_no from LETTER_LOOSE_DETAIL where letter_loose_no=@letter_no and letter_loose_date=@letter_date and axis_id=@axis_id and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_LOOSE_DETAIL 
                                            (letter_loose_no,letter_loose_date,axis_id,tire_id,date_created,date_modified,is_delete,userid)
                                            values (@letter_no,@letter_date,@axis_id,@tire_id,@date_created,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                                {
                                    p.letter_no,
                                    p.letter_date,
                                    p.date_created,
                                    dr.axis_id,
                                    dr.tire_id,
                                    date_modified,
                                    p.userid
                                }, tran);
                                conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_created, userid=@userid WHERE tire_id =@tire_id and vehicle_id=@vehicle_id 
                                    "
                                    , new
                                    {
                                        p.vehicle_id,
                                        p.date_created,
                                        dr.tire_id,
                                        p.userid
                                    }, tran);
                                conn.Execute(@"     
                                        UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_created, userid=@userid where tire_id =@tire_id and axis_id=@axis_id
                                    
                                        UPDATE TIRE SET location_id = 'ZZZ', date_modified = @date_created WHERE tire_id = @tire_id
                                        "
                                    , new
                                    {
                                        dr.axis_id,
                                        p.vehicle_id,
                                        p.location_id,
                                        p.date_created,
                                        dr.tire_id,
                                        p.userid
                                    }, tran);



                                conn.Execute(@"
                                                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);

                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                             values (@tire_id,@current_epc_tire,'111','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                {
                                    dr.tire_id,
                                    p.userid,
                                    current_epc_tire,
                                    p.location_id,
                                    p.date_created,
                                }, tran);

                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='7', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    dr.tire_id,
                                    p.userid,
                                    p.date_created
                                }, tran);
                            }

                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                }
                else
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The letter no is already in use.";
                }
                
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "The statement letter detach created. Letter No: " + p.letter_no;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon DeleteLetterDismantleTire(cCrudLetterLoose p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"DELETE FROM LETTER_DISMANTLE where letter_dismantle_no=@letter_no and letter_dismantle_date=@letter_date
                                
                                    ", new
                        {
                            p.letter_no,
                            p.letter_date
                        }, tran);


                        tran.Commit();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }

                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "The statement letter dismantle deleted. Letter No: " + p.letter_no;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertLetterDismantleTire(cCrudLetterLoose p)
        {
            List<cCrudLetterLooseDetail> ListDetail = new List<cCrudLetterLooseDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudLetterLooseDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool cekData = checkLetterNoDismantleUsed(p.letter_no);
                if (cekData == false)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_dismantle_no from LETTER_DISMANTLE where letter_dismantle_no=@letter_no) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_DISMANTLE 
                                            (letter_dismantle_no,letter_dismantle_date,location_id, vehicle_id, shift_group_mech, mechanic, description, photo,date_created,date_modified,is_delete,userid)
                                            values (@letter_no,@letter_date,@location_id,@vehicle_id,@shift_groud_merch,@mechanic,@description,@photo,@date_created,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                p.letter_no,
                                p.letter_date,
                                p.location_id,
                                p.vehicle_id,
                                p.shift_groud_merch,
                                p.mechanic,
                                p.photo,
                                p.date_created,
                                p.description,
                                p.userid
                            }, tran);
                            foreach (var dr in ListDetail)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_dismantle_no from LETTER_DISMANTLE_DETAIL where letter_dismantle_no=@letter_no and letter_dismantle_date=@letter_date and axis_id=@axis_id and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_DISMANTLE_DETAIL 
                                            (letter_dismantle_no,letter_dismantle_date,axis_id,tire_id, reff_tire_id,date_created,date_modified, is_closed,is_delete,userid)
                                            values (@letter_no,@letter_date,@axis_id,@tire_id, 'XXX',@date_created,@date_created,0,0,@userid)
                                        END 
                                
                                    ", new
                                {
                                    p.letter_no,
                                    p.letter_date,
                                    p.date_created,
                                    dr.axis_id,
                                    dr.tire_id,
                                    date_modified,
                                    p.userid
                                }, tran);
                                
                            }

                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                }
                else
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The letter no is already in use.";
                }

                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "The statement letter dismantle created. Letter No: " + p.letter_no;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static bool checkLetterNoFoundUsed(string no)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=letter_found_no from letter_found where letter_found_no=@no";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }
        public static bool checkTyreCode(string code)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=tire_code from item where tire_code=@code";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@code", code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }
        public static bool checkLetterNoDetachUsed(string no)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=letter_loose_no from letter_loose where letter_loose_no=@no";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }

        public static bool checkLetterNoDismantleUsed(string no)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=letter_dismantle_no from letter_dismantle where letter_dismantle_no=@no";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@no", no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }
        public static cRespon InsertLetterFoundTire(cCrudLetterFound p)
        {
            List<cCrudLetterFoundDetail> ListDetail = new List<cCrudLetterFoundDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudLetterFoundDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool cekData = checkLetterNoFoundUsed(p.letter_no);
                if (cekData == false) {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_found_no from LETTER_FOUND where letter_found_no=@letter_no) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_FOUND 
                                            (letter_found_no,letter_found_date,location_id,finder, shift_group_mech, qty_found, description, photo,date_created,is_delete,userid)
                                            values (@letter_no,@letter_date,@location_id,@finder,@shift_groud_merch,@qty_found,@description,@photo,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                p.letter_no,
                                p.letter_date,
                                p.location_id,
                                p.finder,
                                p.shift_groud_merch,
                                p.qty_found,
                                p.photo,
                                date_created,
                                p.description,
                                p.userid
                            }, tran);
                            foreach (var dr in ListDetail)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_found_no from LETTER_FOUND_DETAIL where letter_found_no=@letter_no and letter_found_date=@letter_date and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_FOUND_DETAIL 
                                            (letter_found_no,letter_found_date,tire_id,date_created,is_delete,userid)
                                            values (@letter_no,@letter_date,@tire_id,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                                {
                                    p.letter_no,
                                    p.letter_date,
                                    date_created,
                                    dr.tire_id,
                                    date_modified,
                                    p.userid
                                }, tran);
                               if (dr.letter_loose_no != ""){
                                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 letter_found_no from LETTER_FOUND_BY_LOOSE where letter_found_no=@letter_no and letter_found_date=@letter_date and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO LETTER_FOUND_BY_LOOSE 
                                            (letter_found_no,letter_found_date,tire_id,letter_loose_no,letter_loose_date)
                                            values (@letter_no,@letter_date,@tire_id,@letter_loose_no,@letter_loose_date)
                                        END 
                                
                                    ", new
                                    {
                                        p.letter_no,
                                        p.letter_date,
                                        dr.letter_loose_no,
                                        dr.tire_id,
                                        dr.letter_loose_date
                                    }, tran);
                                }

                            
                                conn.Execute(@"
                                                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete,date_modified, userid)
                                               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@date_created,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);

                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created,date_modified, userid)
                                             values (@tire_id,@current_epc_tire,'112','XXX','XXX',@location_id,0,@date_created,@date_created,@userid)", new
                                {
                                    dr.tire_id,
                                    p.userid,
                                    current_epc_tire,
                                    p.location_id,
                                    date_created,
                                }, tran);
                                conn.Execute(@"     
                                        UPDATE TIRE SET status_id='8',location_id = @location_id, date_modified = @date_created, userid=@userid WHERE tire_id = @tire_id
                                        "
                                   , new
                                   {
                                       p.location_id,
                                       date_created,
                                       dr.tire_id,
                                       p.userid
                                   }, tran);
                                //conn.Execute(@" 
                                //    BEGIN 
                                //        update TIRE set status_id='8', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //    END 
                                //", new
                                //{
                                //    dr.tire_id,
                                //    p.userid,
                                //    p.date_created
                                //}, tran);
                            }
                            tran.Commit();
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                }
                else
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The letter no is already in use.";
                }
                
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "The statement letter found created. Letter No: " + p.letter_no;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertServiceMaintenance(cCrudServiceMaintenance p)
        {
            List<cCrudDataObservation> ListDataObs = new List<cCrudDataObservation>();
            if (p.data_observation.Trim() != "0")
            {
                ListDataObs = JsonConvert.DeserializeObject<List<cCrudDataObservation>>(p.data_observation);
            }
                
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
            string date_created_ = date_created.ToString("yyyy-MM-dd");
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string unit_location_id = Master.getDefaultUnitLocationId();
            string message = "";

            string current_epc_tire = getEPCCurrentTire(p.tire_id);
            string current_epc_newtire = getEPCCurrentTire(p.new_tire_id);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool cekTireDisposed = checkTireDisposed(p.tire_id);
                if (cekTireDisposed == true)
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, unable to perform this action. The tire was disposed of.";
                }
                else
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        string location_tire_repair = getLocationTire(p.tire_id);
                    
                        //REPAIR
                        if (p.inspect_setup_id == "4")
                        {
                            try
                            {
                                string smr_id = getNextSMR_Id(yearmonth);
                                string new_location_tire_repair = getLocationTire(p.new_tire_id);
                                string action = getActionType(p.inspect_setup_id);
                                string process_repair = "220";
                                //string process_swap = "221";
                                string status_id = "2";
                                conn.Execute(@"
                                                INSERT INTO SERV_MAINT_REPAIR
                                             (smr_id,reason_id,area_damage_id,tire_id,status_id,location_id, notes, is_delete, date_created,userid)
                                             values (@smr_id,@reason_id,@area_damage_id, @tire_id,@status_id,@location_id,@notes, 0,@date_created ,@userid)


                                        ", new
                                {
                                    smr_id,
                                    p.area_damage_id,
                                    p.tire_id,
                                    p.reason_id,
                                    p.location_id,
                                    status_id,
                                    p.notes,
                                    p.date_created,
                                    p.userid
                                }, tran);
                                //if (action == "x")
                                //{
                                //    //UPDATE PERTAMA ADALAH UPDATE LOCATION DATA TIRE YANG DIREPAIR
                                //    //UPDATE KEDUA ADALAH UPDATE LOCATION DATA TIRE YANG DIPASANGKAN DARI STOK YANG TERSEDIA
                                //    //UPDATE KETIGA ADALAH UPDATE PERTUKARAN DATA TIRE DI TABEL TIRE_UNIT
                                //    conn.Execute(@"
                                //                UPDATE TIRE set location_id = @location_id, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //                UPDATE TIRE set location_id = @unit_location_id, date_modified=@date_created, userid=@userid where tire_id=@new_tire_id

                                //                UPDATE TIRE_UNIT set is_delete = 1, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //                UPDATE TIRE_AXIS set is_delete = 1, date_modified=@date_created, userid=@userid where tire_id=@tire_id

                                //                IF (NOT EXISTS (select TOP 1 tire_id from TIRE_UNIT where vehicle_id=@vehicle_id and tire_id=@new_tire_id) ) 
                                //                BEGIN 
                                //                    INSERT INTO TIRE_UNIT (vehicle_id, tire_id, is_delete, date_created, userid)
                                //                    values (@vehicle_id,@new_tire_id,0,@date_created,@userid)
                                //                END

                                //                IF (NOT EXISTS (select TOP 1 tire_id from TIRE_AXIS where vehicle_id=@vehicle_id and tire_id=@new_tire_id) ) 
                                //                BEGIN 
                                //                    INSERT INTO TIRE_AXIS (vehicle_id, tire_id, axis_id, is_delete, date_created, userid)
                                //                    values (@vehicle_id,@new_tire_id, @axis_id,0,@date_created,@userid)
                                //                END
                                //        ", new
                                //    {
                                //        p.vehicle_id,
                                //        unit_location_id,
                                //        p.location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.tire_id,
                                //        p.axis_id,
                                //        p.userid
                                //    }, tran);

                                //    conn.Execute(@"
                                //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        p.location_id,
                                //        date_created,
                                //        p.tire_id,
                                //        p.userid
                                //    }, tran);
                                //    conn.Execute(@"
                                //                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        unit_location_id,
                                //        date_created,
                                //        p.tire_id,
                                //        p.userid
                                //    }, tran);

                                //    conn.Execute(@"
                                //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@new_tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        unit_location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.userid
                                //    }, tran);



                                //    conn.Execute(@"
                                //                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@new_tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        p.location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.userid
                                //    }, tran);



                                //}
                                conn.Execute(@"
                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                                values
                                                (@tire_id,@current_epc_tire,@process_repair, 'XXX',@smr_id,@location_id,0, @date_created, @userid)

                                            
                                        ", new
                                {
                                    smr_id,
                                    p.location_id,
                                    current_epc_tire,
                                    current_epc_newtire,
                                    process_repair,
                                    p.date_created,
                                    p.tire_id,
                                    p.new_tire_id,
                                    p.userid
                                }, tran);
                                //INSERT INTO TIRE_TRACKING(tire_id, epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                //                values
                                //                (@new_tire_id, @current_epc_newtire, '102', 'XXX', @smr_id, @location_id, 0, @date_created, @userid)
                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='2', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    p.tire_id,
                                    p.userid,
                                    p.new_tire_id,
                                    p.date_created
                                }, tran);

                                //BEGIN
                                //    update TIRE set status_id = '1', date_modified = @date_created, userid = @userid where tire_id = @new_tire_id
                                //    END
                                //conn.Execute(@" 
                                //        BEGIN 
                                //            update TIRE set status_id='2', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //        END 
                                //    ", new
                                //{
                                //    p.tire_id,
                                //    p.userid,
                                //    p.date_created
                                //}, tran);
                                //conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                //             values (@tire_id,@current_epc_tire,'220','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                //{
                                //    p.tire_id,
                                //    p.date_created,
                                //    current_epc_tire,
                                //    p.location_id,
                                //    p.userid
                                //}, tran);
                                message = "Repair Successfully";
                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }
                        
                        }
                        //PATCHING
                        if (p.inspect_setup_id == "7")
                        {
                            try
                            {
                                string smr_id = getNextSMR_Id(yearmonth);
                                string new_location_tire_repair = getLocationTire(p.new_tire_id);
                                string action = getActionType(p.inspect_setup_id);
                                string process_repair = "221"; //PATCHING
                                //string process_swap = "221";
                                string status_id = "9";
                                conn.Execute(@"
                                                INSERT INTO SERV_MAINT_REPAIR
                                             (smr_id,reason_id,area_damage_id,tire_id,status_id,location_id, notes, is_delete, date_created,userid)
                                             values (@smr_id,@reason_id,@area_damage_id, @tire_id,@status_id,@location_id,@notes, 0,@date_created ,@userid)
                                       
                                
                                        ", new
                                {
                                    smr_id,
                                    p.area_damage_id,
                                    p.tire_id,
                                    p.reason_id,
                                    p.location_id,
                                    status_id,
                                    p.notes,
                                    p.date_created,
                                    p.userid
                                }, tran);
                                //if (action == "x")
                                //{
                                //    //UPDATE PERTAMA ADALAH UPDATE LOCATION DATA TIRE YANG DIREPAIR
                                //    //UPDATE KEDUA ADALAH UPDATE LOCATION DATA TIRE YANG DIPASANGKAN DARI STOK YANG TERSEDIA
                                //    //UPDATE KETIGA ADALAH UPDATE PERTUKARAN DATA TIRE DI TABEL TIRE_UNIT
                                //    conn.Execute(@"
                                //                UPDATE TIRE set location_id = @location_id, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //                UPDATE TIRE set location_id = @unit_location_id, date_modified=@date_created, userid=@userid where tire_id=@new_tire_id

                                //                UPDATE TIRE_UNIT set is_delete = 1, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //                UPDATE TIRE_AXIS set is_delete = 1, date_modified=@date_created, userid=@userid where tire_id=@tire_id

                                //                IF (NOT EXISTS (select TOP 1 tire_id from TIRE_UNIT where vehicle_id=@vehicle_id and tire_id=@new_tire_id) ) 
                                //                BEGIN 
                                //                    INSERT INTO TIRE_UNIT (vehicle_id, tire_id, is_delete, date_created, userid)
                                //                    values (@vehicle_id,@new_tire_id,0,@date_created,@userid)
                                //                END

                                //                IF (NOT EXISTS (select TOP 1 tire_id from TIRE_AXIS where vehicle_id=@vehicle_id and tire_id=@new_tire_id) ) 
                                //                BEGIN 
                                //                    INSERT INTO TIRE_AXIS (vehicle_id, tire_id, axis_id, is_delete, date_created, userid)
                                //                    values (@vehicle_id,@new_tire_id, @axis_id,0,@date_created,@userid)
                                //                END
                                //        ", new
                                //    {
                                //        p.vehicle_id,
                                //        unit_location_id,
                                //        p.location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.tire_id,
                                //        p.axis_id,
                                //        p.userid
                                //    }, tran);

                                //    conn.Execute(@"
                                //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        p.location_id,
                                //        date_created,
                                //        p.tire_id,
                                //        p.userid
                                //    }, tran);
                                //    conn.Execute(@"
                                //                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        unit_location_id,
                                //        date_created,
                                //        p.tire_id,
                                //        p.userid
                                //    }, tran);

                                //    conn.Execute(@"
                                //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@new_tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        unit_location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.userid
                                //    }, tran);



                                //    conn.Execute(@"
                                //                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@new_tire_id,0,@userid)", new
                                //    {
                                //        yearmonth,
                                //        p.location_id,
                                //        date_created,
                                //        p.new_tire_id,
                                //        p.userid
                                //    }, tran);



                                //}
                                conn.Execute(@"
                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                                values
                                                (@tire_id,@current_epc_tire,'221', 'XXX',@smr_id,@location_id,0, @date_created, @userid)
                                            
                                        ", new
                                {
                                    smr_id,
                                    p.location_id,
                                    current_epc_tire,
                                    p.date_created,
                                    p.tire_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id=@status_id, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    p.tire_id,
                                    p.userid,
                                    status_id,
                                    p.new_tire_id,
                                    p.date_created
                                }, tran);
                                message = "Patching Successfully";
                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }

                        }
                        else //INSPECTION
                        if (p.inspect_setup_id == "5")//(p.data_observation != "0")
                        {
                            try
                            {
                                string smo_id = getNextSMO_Id(yearmonth);
                                string location_tire = getLocationTire(p.tire_id);
                                string process = "400";
                                conn.Execute(@" INSERT INTO SERV_MAINT_OBSERVATION
                                             (smo_id,location_id, tire_id,is_delete, date_created,userid)
                                             values (@smo_id,@location_tire,@tire_id, 0,@date_created ,@userid)
                                       
                                
                                        ", new
                                {
                                    smo_id,
                                    p.date_created,
                                    p.tire_id,
                                    location_tire,
                                    p.userid
                                }, tran);
                                foreach (var dr in ListDataObs)
                                {

                                    //string location_tire = getLocationTire(p.tire_id);

                                    conn.Execute(@" INSERT INTO SERV_MAINT_OBSERVATION_DETAIL
                                             (smo_id, date_created,observation_item_id, value, notes,is_delete,userid)
                                             values (@smo_id,@date_created,@observation_item_id, @value, @notes, 0 ,@userid)
                                       
                                
                                        ", new
                                    {
                                        smo_id,
                                        p.date_created,
                                        dr.observation_item_id,
                                        dr.value,
                                        dr.notes,
                                        p.userid
                                    }, tran);

                                    //tran.Commit();
                                }

                                DateTime date_now = Convert.ToDateTime(Convert.ToDateTime(p.date_created).ToString("yyyy-MM-dd"));
                                string date_now_input = date_now.ToString();
                                DateTime date_maintenance = Convert.ToDateTime(date_now.AddDays(14).ToString("yyyy-MM-dd"));

                                //var data = dbConn.Query(StrQuery, null, null, true, commandTimeout: 0, commandType: null).ToList();
                                string sch_maint_id = getNextScheduleMaint(yearmonth);
                                string fleet = getMyFleetByVhc(p.vehicle_id);
                                conn.Execute(@"
											     if (not exists (select * from schedule_maintenance where fleet_id=@fleet) ) 
                                                 begin 

	                                             insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                                    values ([dbo].[getnextschedulemaintid](@yearmonth),@date_now,@fleet,@date_now,1,0,@date_created,@userid)

	                                             insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                                    values ([dbo].[getnextschedulemaintid](@yearmonth),@date_now,@fleet,@date_maintenance,0,0,@date_created,@userid)
											
                                                end
											    else
											    begin
												     update schedule_maintenance set schm_flag = 1, date_modified=@date_created where fleet_id=@fleet and schm_date= (select MAX(schm_date) from SCHEDULE_MAINTENANCE where fleet_id= @fleet)
	                                             insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                                    values ([dbo].[getnextschedulemaintid](@yearmonth),@date_now,@fleet,@date_maintenance,0,0,@date_created,@userid)
											
											    end

                                        ", new
                                {
                                    fleet,
                                    sch_maint_id,
                                    p.vehicle_id,
                                    yearmonth,
                                    date_now,
                                    date_maintenance,
                                    p.tire_id,
                                    date_created,
                                    p.userid
                                }, tran);
                                //                 else
                                //begin
                                //                                     update schedule_maintenance set schm_flag = 1, date_modified = @date_created where fleet_id = @fleet and schm_date = (select MAX(schm_date) from SCHEDULE_MAINTENANCE where fleet_id = @fleet)

                                //	insert into schedule_maintenance(schm_id, schm_date, fleet_id, schm_date_maintenance, schm_flag, is_delete, date_created, userid)
                                //                                     values(@schmid, @date_now_input, @fleet, @date_maintenance, 0, 0, @date_created, @userid)


                                //                                 end
                                //              conn.Execute(@"
                                //                             DECLARE @fleet varchar(max);
                                //                              declare @schmid varchar(max);
                                //                              declare @result varchar(max);
                                //                              select @schmid=[dbo].[getnextschedulemaintid](@yearmonth)
                                //                              select @fleet=fleet_id from fleet_vehicle where vehicle_id=@vehicle_id
                                //                              if (not exists (select * from schedule_maintenance where fleet_id=@fleet) ) 
                                //                              begin 
                                //                               insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                //                                  values (@schmid,@date_now_input,@fleet,@date_maintenance,1,@tire_id,0,@date_created,@userid)

                                //                              end
                                //else
                                //begin
                                // update schedule_maintenance set schm_flag = 1 where fleet_id=@fleet and schm_date= (select MAX(schm_date) from SCHEDULE_MAINTENANCE where fleet_id= @fleet)

                                // insert into schedule_maintenance (schm_id, schm_date, fleet_id, schm_date_maintenance,schm_flag , is_delete, date_created, userid)
                                //                                  values (@schmid,@date_now_input,@fleet,@date_maintenance,1,@tire_id,0,@date_created,@userid)

                                //end

                                //                      ", new
                                //              {
                                //                  p.vehicle_id,
                                //                  yearmonth,
                                //                  unit_location_id,
                                //                  p.location_id,
                                //                  date_now_input,
                                //                  date_maintenance,
                                //                  date_created,
                                //                  p.new_tire_id,
                                //                  p.tire_id,
                                //                  p.axis_id,
                                //                  p.userid
                                //              }, tran);

                                conn.Execute(@"
                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id,location_id ,is_delete, date_created, userid)
                                                values
                                                (@tire_id,@current_epc_tire,@process, @smo_id,'XXX',@location_tire, 0, @date_created, @userid)
                                        ", new
                                {
                                    smo_id,
                                    p.date_created,
                                    process,
                                    current_epc_tire,
                                    p.tire_id,
                                    location_tire,
                                    p.userid
                                }, tran);
                                message = "Inspection Successfully";
                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }

                        }
                        //DISMANTLE
                        else if (p.inspect_setup_id == "1")
                        {
                            string location_tire = getLocationTire(p.tire_id);
                            try
                            {
                                conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_created, userid=@userid WHERE tire_id =@tire_id and vehicle_id=@vehicle_id 
                                    "
                                    , new
                                    {
                                        p.vehicle_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);
                                conn.Execute(@"     
                                        UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_created, userid=@userid where tire_id =@tire_id and axis_id=@axis_id
                                    
                                        UPDATE TIRE SET location_id = @location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                        "
                                    , new
                                    {
                                        p.axis_id,
                                        p.vehicle_id,
                                        p.location_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);
                                conn.Execute(@"
                                                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    p.tire_id,
                                    p.userid
                                }, tran);



                                conn.Execute(@"
                                                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_tire_repair,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    location_tire_repair,
                                    p.date_created,
                                    p.tire_id,
                                    p.userid
                                }, tran);

                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                             values (@tire_id,@current_epc_tire,'103','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                {
                                    p.tire_id,
                                    p.userid,
                                    current_epc_tire,
                                    p.location_id,
                                    p.date_created,
                                }, tran);

                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='5', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    END 
                                ", new
                                {
                                    p.tire_id,
                                    p.userid,
                                    p.date_created
                                }, tran);
                                message = "Dismantle Successfully";
                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }
                        }
                        //INSTALL
                        else if (p.inspect_setup_id == "2")
                        {
                            if (checkingTyreInstall(p.tire_id) == false)
                            {
                                if (Master.cekDataTireUnit(p.vehicle_id, p.tire_id) == 0)
                                {
                                    try
                                    {
                                        conn.Execute(@"INSERT INTO TIRE_UNIT
                                                        (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                                    VALUES
                                                        (@vehicle_id,@tire_id,NULL,0,@date_created,@userid)"
                                            , new
                                            {
                                                p.vehicle_id,
                                                p.tire_id,
                                                p.date_created,
                                                p.userid
                                            }, tran);
                                        conn.Execute(@" INSERT INTO TIRE_AXIS
                                                        (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                                    VALUES
                                                        (@vehicle_id,@tire_id,@axis_id,@date_created,NULL,0, @userid)
                                                UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                                "
                                            , new
                                            {
                                                p.userid,
                                                p.axis_id,
                                                p.vehicle_id,
                                                unit_location_id,
                                                p.date_created,
                                                p.tire_id,
                                            }, tran);
                                        conn.Execute(@"
                                                    insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                   values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                        {
                                            yearmonth,
                                            unit_location_id,
                                            p.date_created,
                                            p.tire_id,
                                            p.userid
                                        }, tran);



                                        conn.Execute(@"
                                                    insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                                   values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_tire_repair,@tire_id,0,@userid)", new
                                        {
                                            yearmonth,
                                            location_tire_repair,
                                            p.date_created,
                                            p.tire_id,
                                            p.userid
                                        }, tran);
                                        conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                 values (@tire_id,@current_epc_tire,'102','XXX','XXX',@unit_location_id,0,@date_created,@userid)", new
                                        {
                                            p.tire_id,
                                            p.date_created,
                                            current_epc_tire,
                                            unit_location_id,
                                            p.userid
                                        }, tran);
                                        conn.Execute(@" 
                                            BEGIN 
                                                update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                            END 
                                        ", new
                                        {
                                            p.tire_id,
                                            p.userid,
                                            p.date_created
                                        }, tran);
                                        //JIKA TERDETEKSI ADA BA DISMANTLE, MAKA BAN YANG DIDISMANTLE DI BA OTOMATIS TER-APPROVE
                                        conn.Execute(@" 
                                        IF OBJECT_ID('LETTER_DISMANTLE', 'U') IS NOT NULL
										BEGIN
											-- Tabel ada
											 update LETTER_DISMANTLE_DETAIL set reff_tire_id=@tire_id, is_closed=1, date_modified=@date_created, userid=@userid where letter_dismantle_no = (select ld.letter_dismantle_no from LETTER_DISMANTLE ld
            									  inner join LETTER_DISMANTLE_DETAIL ldd on ldd.letter_dismantle_no = ld.letter_dismantle_no  and ldd.letter_dismantle_date = ld.letter_dismantle_date where ld.vehicle_id=@vehicle_id and ldd.axis_id=@axis_id)
										END
                                        ", new
                                        {
                                            p.tire_id,
                                            p.userid,
                                            p.axis_id,
                                            p.vehicle_id,
                                            p.date_created
                                        }, tran);
                                        message = "Running Successfully";
                                    }
                                    catch (Exception e)
                                    {
                                        tran.Rollback();
                                        returnRes.flag = false;
                                        returnRes.message = "# Failed, " + e.Message.ToString();
                                    }
                                }
                                else
                                {
                                    returnRes.flag = false;
                                    returnRes.message = "Tire already installed!";
                                }

                            }
                            else
                            {
                                returnRes.flag = false;
                                returnRes.message = "Tire already installed!";
                            }
                            
                        }
                        //READY
                        else if (p.inspect_setup_id == "3")
                        {
                            try
                            {
                                conn.Execute(@" 
                                        BEGIN 
                                            update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                        END 
                                    ", new
                                {
                                    p.tire_id,
                                    p.userid,
                                    p.date_created
                                }, tran);
                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                             values (@tire_id,@current_epc_tire,'106','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                {
                                    p.tire_id,
                                    p.date_created,
                                    current_epc_tire,
                                    p.location_id,
                                    p.userid
                                }, tran);
                                message = "Ready Successfully";

                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }

                        }
                        //SEND TO WORKSHOP
                        else if (p.inspect_setup_id == "6")
                        {
                            bool checkingTyre = checkingTyreOnUnit(p.tire_id);
                            if (checkingTyre == true)
                            {
                                returnRes.flag = false;
                                returnRes.message = "# Failed, Tyre still on Unit";
                            }
                            else
                            {
                                string outbound_id = getNextOutboundId(yearmonth);
                                try
                                {
                                    conn.Execute(@" 
                                            BEGIN 
                                                update TIRE set status_id='3', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                            END 
                                        ", new
                                    {
                                        p.tire_id,
                                        p.userid,
                                        p.date_created
                                    }, tran);
                                    //conn.Execute(@"IF (NOT EXISTS (select TOP 1 outbound_id from OUTBOUND where outbound_id=@outbound_id) ) 
                                    //            BEGIN 
                                    //                INSERT INTO OUTBOUND 
                                    //             (outbound_id,date_created,location_id,is_delete,userid)
                                    //             values (@outbound_id,@date_created,@location_id,0,@userid)
                                    //            END 

                                    //        ", new
                                    //{
                                    //    outbound_id,
                                    //    p.date_created,
                                    //    p.location_id,
                                    //    p.userid
                                    //}, tran);
                                    //conn.Execute(@"
                                    //            update OUTBOUND_DETAIL set is_active= 0, date_modified = @date_created, userid = @userid where tire_id = @tire_id
                                    //            IF (NOT EXISTS (select TOP 1 outbound_id from OUTBOUND_DETAIL where outbound_id=@outbound_id and date_created=@date_created and tire_id=@tire_id) ) 
                                    //            BEGIN 
                                    //                INSERT INTO OUTBOUND_DETAIL 
                                    //             (outbound_id,date_created,tire_id,location_id,is_active,is_delete,userid)
                                    //             values (@outbound_id,@date_created,@tire_id,@location_id,1,0,@userid)

                                    //                UPDATE TIRE set location_id='XXX',date_modified=@date_modified where tire_id=@tire_id
                                    //            END 

                                    //        ", new
                                    //{
                                    //    outbound_id,
                                    //    p.date_created,
                                    //    p.tire_id,
                                    //    p.location_id,
                                    //    date_modified,
                                    //    p.userid
                                    //}, tran);
                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                 values (@tire_id,@current_epc_tire,'108','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                    {
                                        p.tire_id,
                                        p.date_created,
                                        p.location_id,
                                        current_epc_tire,
                                        p.userid
                                    }, tran);
                                    message = "Send to Workshop Successfully";

                                }
                                catch (Exception e)
                                {
                                    tran.Rollback();
                                    returnRes.flag = false;
                                    returnRes.message = "# Failed, " + e.Message.ToString();
                                }
                            }
                        

                        }
                        //SCRAP PER INDIVIDU
                        else if (p.inspect_setup_id == "8")
                        {
                            string scrap_location_id = getDefaultScrapLocationId();
                            try
                            {
                                string   no_scrap = getNextScrapNoTire(yearmonth);
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 scrap_no from SCRAP_TIRE where scrap_no=@no_scrap and date_created=@date_created and tire_id=@tire_id) ) 
                                            BEGIN 
                                                INSERT INTO SCRAP_TIRE
                                             (scrap_no,date_created,tire_id, reason_id, area_damage_id,description,is_delete,date_modified,userid)
                                             values (@no_scrap,@date_created,@tire_id, @reason_id,@area_damage_id ,@notes,0,@date_created,@userid)
                                            END 
                                
                                        ", new
                                {
                                    no_scrap,
                                    p.date_created,
                                    p.tire_id,
                                    p.notes,
                                    p.reason_id,
                                    p.area_damage_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"
                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id , location_id,is_delete, date_created, userid)
                                                values
                                                (@tire_id,@current_epc_tire,'107', 'XXX','XXX', @scrap_location_id, 0, @date_created, @userid)
                                                update TIRE set status_id='4', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                        ", new
                                {
                                    p.date_created,
                                    p.tire_id,
                                    current_epc_tire,
                                    scrap_location_id,
                                    p.userid
                                }, tran);
                                bool cekDataObservation = cekDataScrapObservation();
                                if (cekDataObservation == true)
                                {
                                    string smo_id = getNextSMO_Id(yearmonth);
                                    conn.Execute(@" INSERT INTO SERV_MAINT_OBSERVATION
                                             (smo_id,location_id, tire_id,is_delete, date_created,userid)
                                             values (@smo_id,@scrap_location_id,@tire_id, 0,@date_created ,@userid)
                                       
                                
                                        ", new
                                    {
                                        smo_id,
                                        p.date_created,
                                        p.tire_id,
                                        scrap_location_id,
                                        p.userid
                                    }, tran);
                                    foreach (var dr in ListDataObs)
                                    {

                                        //string location_tire = getLocationTire(p.tire_id);

                                        conn.Execute(@" INSERT INTO SERV_MAINT_OBSERVATION_DETAIL
                                             (smo_id, date_created,observation_item_id, value, notes,is_delete,userid)
                                             values (@smo_id,@date_created,@observation_item_id, @value, @no_scrap, 0 ,@userid)
                                       
                                
                                        ", new
                                        {
                                            smo_id,
                                            p.date_created,
                                            dr.observation_item_id,
                                            dr.value,
                                            no_scrap,
                                            p.userid
                                        }, tran);

                                        //tran.Commit();
                                    }
                                }
                                message = "Scrap Successfully";
                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }


                        }//CHANGE TYRE
                        else if (p.inspect_setup_id == "9")
                        {
                            string tsch_id = getTschIdByUser(p.userid, p.date_created);
                            if (tsch_id == "")
                            {

                                returnRes.message = "Access denied. Please create your schedule maintenance!";
                                returnRes.flag = false;
                            }
                            else {
                                try
                                {
                                    //DISMANTLE BAN LAMA
                                    conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_created, userid=@userid WHERE tire_id =@tire_id and vehicle_id=@vehicle_id 
                                    "
                                    , new
                                    {
                                        p.vehicle_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);

                                    conn.Execute(@"     
                                            UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_created, userid=@userid where tire_id =@tire_id and axis_id=@axis_id
                                    
                                            UPDATE TIRE SET location_id = @location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                            "
                                    , new
                                    {
                                        p.axis_id,
                                        p.vehicle_id,
                                        p.location_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);

                                    conn.Execute(@"
                                                    insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                    values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                    {
                                        yearmonth,
                                        p.location_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);

                                    conn.Execute(@"
                                                    insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                                    values ([dbo].[NextStockOutId](@yearmonth),@date_created,@unit_location_id,@tire_id,0,@userid)", new
                                    {
                                        yearmonth,
                                        unit_location_id,
                                        p.date_created,
                                        p.tire_id,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                        values (@tire_id,@current_epc_tire,'103','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                    {
                                        p.tire_id,
                                        p.userid,
                                        current_epc_tire,
                                        p.location_id,
                                        p.date_created,
                                    }, tran);

                                    conn.Execute(@" 
                                            BEGIN 
                                                update TIRE set status_id='5', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                            END 
                                        ", new
                                    {
                                        p.tire_id,
                                        p.userid,
                                        p.date_created
                                    }, tran);

                                    //INSTALL BAN BARU

                                    conn.Execute(@"INSERT INTO TIRE_UNIT
                                                        (vehicle_id,tire_id,description, is_delete, date_modified, date_created, userid)
                                                    VALUES
                                                        (@vehicle_id,@new_tire_id,'',0,@date_created,@date_created,@userid)"
                                            , new
                                            {
                                                p.vehicle_id,
                                                p.new_tire_id,
                                                p.date_created,
                                                p.userid
                                            }, tran);
                                    conn.Execute(@" INSERT INTO TIRE_AXIS
                                                        (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                                    VALUES
                                                        (@vehicle_id,@new_tire_id,@axis_id,@date_created,NULL,0, @userid)
                                                UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @new_tire_id
                                                "
                                        , new
                                        {
                                            p.userid,
                                            p.axis_id,
                                            p.vehicle_id,
                                            unit_location_id,
                                            p.date_created,
                                            p.new_tire_id,
                                        }, tran);
                                    conn.Execute(@"
                                                    insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                    values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@new_tire_id,0,@userid)", new
                                    {
                                        yearmonth,
                                        unit_location_id,
                                        p.date_created,
                                        p.new_tire_id,
                                        p.userid
                                    }, tran);



                                    conn.Execute(@"
                                                    insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                                    values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@new_tire_id,0,@userid)", new
                                    {
                                        yearmonth,
                                        p.location_id,
                                        p.date_created,
                                        p.new_tire_id,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                    values (@new_tire_id,@current_epc_newtire,'102','XXX','XXX',@unit_location_id,0,@date_created,@userid)", new
                                    {
                                        p.new_tire_id,
                                        p.date_created,
                                        current_epc_newtire,
                                        unit_location_id,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@" 
                                            BEGIN 
                                                update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@new_tire_id
                                            END 
                                        ", new
                                    {
                                        p.new_tire_id,
                                        p.userid,
                                        p.date_created
                                    }, tran);


                                    //INPUT KE TABEL HISTORY TIRE_CHANGE
                                    string[] data_schedule = Process.getDataScheduleByUser(p.userid);
                                    string year_sch = data_schedule[0];
                                    string month_sch = data_schedule[1];
                                    string date_sch = data_schedule[2];
                                    string location_id_sch = data_schedule[3];
                                    string group_id_sch = data_schedule[4];
                                    string shift_id_sch = data_schedule[5];
                                    string tire_change_id = getNextTireChangeId(yearmonth);

                                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_change_id from TIRE_CHANGE where tire_change_id=@tire_change_id) ) 
                                                BEGIN 
                                                    INSERT INTO TIRE_CHANGE 
                                                    (tire_change_id,notes, vehicle_id, location_id, km, time_start, time_end,old_tire_id, new_tire_id, date_modified,date_created,is_delete,userid)
                                                    values (@tire_change_id,@notes, @vehicle_id, @location_id,@km,@time_start, @time_end,@tire_id,@new_tire_id,@date_created,@date_created,0,@userid)
                                                END 

                                        ", new
                                    {
                                        tire_change_id,
                                        p.vehicle_id,
                                        p.location_id,
                                        p.notes,
                                        p.tire_id,
                                        p.new_tire_id,
                                        p.time_start,
                                        p.time_end,
                                        p.km,
                                        p.date_created,
                                        p.userid
                                    }, tran);


                                    conn.Execute(@"INSERT INTO SCHEDULE_TIRE_CHANGE (year, month, date, location_id, group_id, shift_id,emp_id, tire_change_id, is_delete, date_created, date_modified, userid)
                                                
                                                    select @year_sch, @month_sch, @date_sch, @location_id_sch, @group_id_sch, @shift_id_sch, emp_id, @tire_change_id, 0, @date_created, @date_created, @userid from GROUPS_EMPLOYEES where group_id=@group_id_sch
                                                

                                        ", new//(@year_sch, @month_sch, @date_sch, @location_id_sch, @group_id_sch, @shift_id_sch, @tire_change_id, 0, @date_created, @date_created, @userid)
                                    {
                                        year_sch,
                                        month_sch,
                                        date_sch,
                                        location_id_sch,
                                        group_id_sch,
                                        shift_id_sch,
                                        tire_change_id,
                                        p.vehicle_id,
                                        p.location_id,
                                        tsch_id,
                                        p.notes,
                                        p.tire_id,
                                        p.new_tire_id,
                                        p.time_start,
                                        p.time_end,
                                        p.km,
                                        p.date_created,
                                        p.userid
                                    }, tran);
                                    message = "Change tyre success";

                                }
                                catch (Exception e)
                                {
                                    tran.Rollback();
                                    returnRes.flag = false;
                                    returnRes.message = "# Failed, " + e.Message.ToString();
                                }
                           
                            }
                        

                        }
                        tran.Commit();

                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = message;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertKMTireDirectly(cKMTireDirectly p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            string process_id = "002";
            string description = "Set Kilometer Directly";
            string axis_id = "0";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created_ = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                        INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                            values
                                            (@tire_id,0,@process_id, 'XXX','XXX', @location_id,0, @date_created, @userid)
                                    
                                ", new
                        {
                            p.date_created,
                            p.tire_id,
                            p.location_id,
                            process_id,
                            p.userid

                        }, tran);

                        conn.Execute(@"
                                    INSERT INTO TIRE_MILEAGE_HISTORY 
                                 (date_created,tire_id,location_id,km, is_delete,userid)
                                 values (@date_created,@tire_id,@location_id,@km,0,@userid)
                              
                                
                            ", new
                        {
                            p.date_created,
                            p.tire_id,
                            p.location_id,
                            p.km,
                            p.userid
                        }, tran);


                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }




            if (returnRes.flag == true)
            {
                returnRes.message = "Set KM success.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertRfidVehicle(cVehicle p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            string process_id = "110";
            string description = "RFID Registration for Vehicle";
            string axis_id = "0";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created_ = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                UPDATE VEHICLE SET VEHICLE_RFID=@rfid, DATE_MODIFIED=@date_created, USERID=@userid WHERE VEHICLE_ID=@vehicle_id
                               
                            ", new
                        {
                            p.vehicle_id,
                            p.date_created,
                            p.userid,
                            p.rfid
                        }, tran);


                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }




            if (returnRes.flag == true)
            {
                returnRes.message = "RFID Registration for Vehicle. Rfid: " + p.rfid + " Vehicle ID:" + p.vehicle_id + "";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static string getNextSlwNo(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=[dbo].[NextSendToWorkshopBeforeScrapNo](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }
        public static string getNextInboundId(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("INBOUND");
            string StrQuery = "select no=[dbo].[NextInbound](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }
        public static string getNextOutboundId(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("OUTBOUND");
            string StrQuery = "select no=[dbo].[NextOutbound](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }

        public static bool checkingTyreInstall(string tire_id)
        {
            bool data = false;
            DataTable dtTempTable = new DataTable("OUTBOUND");
            string StrQuery = @" select * from tire t
							 inner join tire_unit tu on tu.tire_id = t.tire_id
							 where t.tire_id=@tire_id and 
							 tu.is_delete=0";
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
                data = true;
            }
            return data;
        }

        public static bool checkingTyreRegister(string tire_id)
        {
            bool data = false;
            DataTable dtTempTable = new DataTable("OUTBOUND");
            string StrQuery = @" select * from register_tire 
							 where tire_id=@tire_id and 
							 is_delete=0";
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
                data = true;
            }
            return data;
        }
        public static bool checkingTyreOnUnit(string tire_id)
        {
            bool data = false;
            DataTable dtTempTable = new DataTable("OUTBOUND");
            string StrQuery = @"select loc.location_type_id from tire t 
                            inner join location loc on loc.location_id=t.location_id
                            where loc.location_type_id='4' and t.tire_id=@tire_id";
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
                data = true;
            }
            return data;
        }
        public static string getNextSMR_Id(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=[dbo].[NextSMRId](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }

        public static string getAxisByTire(string tire_id)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("axis");
            string StrQuery = @"
                                select axis_id from tire_axis where tire_id=@tire_id ";
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
                data = (from DataRow dr in dtTempTable.Rows
                        select (string)dr["axis_id"]).FirstOrDefault();
            }
            return data;
        }
        public static string getAxisByNo(string no, string axis_type)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("axis");
            string StrQuery = @"
                                select * from(
                                SELECT ROW_NUMBER() OVER (Order by axis_id) AS rn, axis_type, axis_id
                                FROM axis where axis_type=@axis_type) as X where X.rn=@no ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@no", no);
            cmd.Parameters.AddWithValue("@axis_type", axis_type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["axis_id"]).FirstOrDefault();
            }
            return data;
        }
        public static string getNextScheduleMaint(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=[dbo].[getnextschedulemaintid](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }

        public static string getMyFleetByVhc(string vehicle_id)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=fleet_id from fleet_vehicle where vehicle_id=@vehicle_id ";
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
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }

        public static string getNextSMO_Id(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=[dbo].[NextSMOId](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }
        public static int getTireValidationSync(string tire_id)
        {
            int data = 0;
            DataTable dtTempTable = new DataTable("TEMP_TAG_DATA");
            string StrQuery = "select tag_id from TEMP_TAG_DATA where tag_id=@tire_id";
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
                data = 1;
            }
            return data;
        }
        public static string getLocationTypeFleet(string fleet_id)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select location_type_id from fleet where fleet_id=@fleet_id";
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
                data = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["location_type_id"]).FirstOrDefault();
            }
            return data;
        }
        public static bool checkTireDisposed(string tire_id)
        {
            bool data = false;
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select tire_id from disposal_detail where tire_id=@tire_id";
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
                data = true;
            }
            return data;
        }
        public static string getLocationTire(string tire_id)
        {
            string location_id = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select location_id from tire where tire_id=@tire_id";
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
                location_id = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["location_id"]).FirstOrDefault();
            }
            return location_id;
        }

        public static string getEPCCurrentTire(string tire_id = "")
        {
            string epc_rfid = "";
            DataTable dtTempTable = new DataTable("EPC_TIRE");
            string StrQuery = "select top 1  epc_rfid from EPC_TIRE where tire_id=@tire_id order by date_created desc";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tire_id", (tire_id != null) ? tire_id : "");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                epc_rfid = (from DataRow dr in dtTempTable.Rows
                            select (string)dr["epc_rfid"]).FirstOrDefault();
            }
            else {
                epc_rfid = "0";
            }
            return epc_rfid;
        }
        public static string getActionType(string inspect_setup_id)
        {
            string service_action_type = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = @"select SA.service_action_type from INSPECT_ACTION_TYPE IAT
                                inner join SERVICE_ACTION SA on SA.service_action_id = IAT.service_action_id
                                where inspect_setup_id=@inspect_setup_id";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@inspect_setup_id", inspect_setup_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                service_action_type = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["service_action_type"]).FirstOrDefault();
            }
            return service_action_type;
        }
        public static cRespon InsertSendToWorkshopBeforeScrapProcess(cCrudSLWScrap p)
        {
            List<cCrudSLWScrapTireDetail> ListScrapTire = new List<cCrudSLWScrapTireDetail>();

            ListScrapTire = JsonConvert.DeserializeObject<List<cCrudSLWScrapTireDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_slw = "";
            int RowAffected = 0;
            string description = "Scrap Tire";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            if (p.slw_no == "0" || p.slw_no == "")
            {
                no_slw = getNextSlwNo(yearmonth);
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 slw_no from SL_WORKSHOP where slw_no=@no_slw) ) 
                                        BEGIN 
                                            INSERT INTO SL_WORKSHOP 
                                         (slw_no,date_created,location_id,is_delete,userid)
                                         values (@no_slw,@date_created,@location_id,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                no_slw,
                                p.date_created,
                                p.description,
                                p.location_id,
                                p.userid
                            }, tran);
                            foreach (var dr in ListScrapTire)
                            {

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 slw_no from SL_WORKSHOP_DETAIL where slw_no=@no_slw and date_created=@date_created and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO SL_WORKSHOP_DETAIL 
                                         (slw_no,date_created,tire_id,description,is_delete,userid)
                                         values (@no_slw,@date_created,@tire_id,@description,0,@userid)

                                            UPDATE TIRE set location_id=@location_id,date_modified=@date_modified where tire_id=@tire_id
                                        END 
                                
                                    ", new
                                {
                                    no_slw,
                                    p.date_created,
                                    dr.tire_id,
                                    dr.description,
                                    p.location_id,
                                    date_modified,
                                    p.userid
                                }, tran);

                            }




                            tran.Commit();
                            //





                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                returnRes.flag = false;
                //no_scrap = p.scrap_no;
            }






            if (returnRes.flag == true)
            {
                returnRes.message = "Send to Workshop succesfully. ID: " + no_slw;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static bool checkLocationOfflineTrx(string location_id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=location_id from location where location_id=@location_id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@location_id", location_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }
        public static bool checkFleetOfflineTrx(string fleet_id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=fleet_id from fleet where fleet_id=@fleet_id and is_delete=0";
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
                row = true;
            }
            return row;
        }
        public static bool checkOfflineTrxNo(string trx_no)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=trx_no from OFFLINE_TRX where trx_no=@trx_no";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@trx_no", trx_no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                row = true;
            }
            return row;
        }
        public static bool checkVehicleOfflineTrx(string vehicle_id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=vehicle_id from vehicle where vehicle_id=@vehicle_id and is_delete=0";
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
                row = true;
            }
            return row;
        }
        public static bool checkTireOfflineTrx(string tire_id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=tire_id from tire where tire_id=@tire_id and is_delete=0";
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
                row = true;
            }
            return row;
        }
        public static cRespon InsertSyncronizeOfflineActivity(cCrudSyncOfflineActivity p) {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string date_modified = DateTime.Now.ToString();
            string yearmonth = DateTime.Now.ToString("yyyyMM");
            bool checkLocation = checkLocationOfflineTrx(p.location_id);
            bool checkFleet = checkFleetOfflineTrx(p.fleet_id);
            string unit_location_id = Master.getDefaultUnitLocationId();
            bool checkVehicle = false;
            bool checkNumberOld = false;
            bool checkNumberNew = false;
            bool is_change_vhc = false;
            if (checkLocation == false)
            {
                returnRes.flag = false;
                returnRes.message = " #Failed, Location is undefined. Make sure the location is correct.";
            }
            else if (checkFleet == false)
            {
                returnRes.flag = false;
                returnRes.message = " #Failed, Fleet is Undefined. Make sure the Fleet is correct.";
            }
            else {
                if (checkOfflineTrxNo(p.trx_no))
                {
                    returnRes.flag = false;
                    returnRes.message = " #Failed, Your transaction already input.";
                }
                else {
                
                    //VALIDASI ERROR
                    foreach (cCrudSyncOfflineActivityDetail detail in p.data)
                    {
                        checkVehicle = checkVehicleOfflineTrx(detail.vehicle_id);
                        if (checkVehicle == false)
                        {
                            returnRes.flag = false;
                            returnRes.message = " #Failed, Vehicle number " + detail.vehicle_id + " is Undefined. Make sure the serial number is correct.";
                            goto loncatError;
                        }
                        if (detail.process_id == "115") {//PROSES CHANGE TYRE
                            checkNumberOld = checkTireOfflineTrx(detail.number_old);
                            checkNumberNew = checkTireOfflineTrx(detail.number_new);
                            if (checkNumberOld == false)
                            {
                                returnRes.flag = false;
                                returnRes.message = " #Failed, Serial number old "+ detail.number_old + " is Undefined. Make sure the serial number is correct.";
                                goto loncatError;
                            }
                            if (checkNumberNew == false)
                            {
                                returnRes.flag = false;
                                returnRes.message = " #Failed, Serial number new " + detail.number_new + " is Undefined. Make sure the serial number is correct.";
                                goto loncatError;
                            }

                        }
                        else if (detail.process_id == "116")//PROSES CHANGE VEHICLE
                        {
                            checkNumberOld = checkVehicleOfflineTrx(detail.number_old);
                            checkNumberNew = checkVehicleOfflineTrx(detail.number_new);
                            if (checkNumberOld == false)
                            {
                                returnRes.flag = false;
                                returnRes.message = " #Failed, Vehicle number old " + detail.number_old + " is Undefined. Make sure the Vehicle number is correct.";
                                goto loncatError;
                            }
                            if (checkNumberNew == false)
                            {
                                returnRes.flag = false;
                                returnRes.message = " #Failed, Vehicle number new " + detail.number_new + " is Undefined. Make sure the Vehicle number is correct.";
                                goto loncatError;
                            }

                        }
                        else
                        {
                            returnRes.flag = false;
                            returnRes.message = " #Failed, Process no " + detail.sort + " Undefined Process.";
                            goto loncatError;
                        }
                    
                    }

                    //JIKA SEMUANYA SUKSES
                    try
                    {
                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                        {
                            conn.Open();
                            using (SqlTransaction tran = conn.BeginTransaction())
                            {
                                try
                                {
                                    conn.Execute(@"INSERT INTO OFFLINE_TRX (trx_no, trx_date, fleet_id, location_id, km, hm, date_created, date_modified, userid)
                                                values(@trx_no, @trx_date, @fleet_id, @location_id, @km, @hm, @date_modified, @date_modified, @userid)
                                    ", new
                                        {
                                            p.trx_no,
                                            p.trx_date,
                                            p.fleet_id,
                                            date_modified,
                                            p.km,
                                            p.hm,
                                            p.location_id,
                                            date,
                                            p.userid
                                        }, tran);
                                    foreach (cCrudSyncOfflineActivityDetail detail in p.data)
                                    {
                                        conn.Execute(@"INSERT INTO OFFLINE_TRX_DETAIL (trx_no, trx_date, sort, process_id, vehicle_id, number_old, number_new, date_created, date_modified)
                                                values(@trx_no, @trx_date, @sort, @process_id, @vehicle_id, @number_old, @number_new, @date_modified, @date_modified)
                                        ", new
                                            {
                                                p.trx_no,
                                                p.trx_date,
                                                p.location_id,
                                                date,
                                                detail.sort,
                                                date_modified,
                                                detail.process_id,
                                                detail.vehicle_id,
                                                detail.number_old,
                                                detail.number_new,
                                                p.userid
                                            }, tran);
                                        if (detail.process_id == "115")//PROSES CHANGE TYRE
                                        {
                                            string tsch_id = getTschIdByUser(p.userid, p.trx_date);
                                            if (tsch_id == "")
                                            {

                                                returnRes.message = "Access denied. Please create your schedule maintenance for user id " + p.userid + " date " + p.trx_date + "!";
                                                returnRes.flag = false;
                                                goto loncatError;
                                            }
                                            else {
                                                string axis_id_old = getAxisByTire(detail.number_old);
                                                string axis_id_new = getAxisByTire(detail.number_new);
                                                string current_epc_tire = getEPCCurrentTire(detail.number_old);
                                                string current_epc_newtire = getEPCCurrentTire(detail.number_new);
                                                try
                                                {
                                                    conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_modified, userid=@userid WHERE tire_id =@number_old and vehicle_id=@vehicle_id 
                                                    "
                                                    , new
                                                    {
                                                        detail.vehicle_id,
                                                        p.trx_date,
                                                        date_modified,
                                                        detail.number_old,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"     
                                                            UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_modified, userid=@userid where tire_id =@number_old and axis_id=@axis_id_old
                                    
                                                            UPDATE TIRE SET location_id = @location_id, date_modified = @date_modified WHERE tire_id = @number_old
                                                            "
                                                    , new
                                                    {
                                                        axis_id_old,
                                                        date_modified,
                                                        detail.vehicle_id,
                                                        p.location_id,
                                                        p.trx_date,
                                                        detail.number_old,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"
                                                        insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                        values ([dbo].[NextStockInId](@yearmonth),@date_modified,@location_id,@number_old,0,@userid)", new
                                                    {
                                                        yearmonth,
                                                        p.location_id,
                                                        p.trx_date,
                                                        date_modified,
                                                        detail.number_old,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"
                                                        insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                                        values ([dbo].[NextStockOutId](@yearmonth),@date_modified,@unit_location_id,@number_old,0,@userid)", new
                                                    {
                                                        yearmonth,
                                                        unit_location_id,
                                                        p.trx_date,
                                                        date_modified,
                                                        detail.number_old,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                            values (@number_old,@current_epc_tire,'103','XXX','XXX',@location_id,0,@date_modified,@userid)", new
                                                    {
                                                        detail.number_old,
                                                        p.userid,
                                                        current_epc_tire,
                                                        p.location_id,
                                                        date_modified,
                                                        p.trx_date,
                                                    }, tran);

                                                    conn.Execute(@" 
                                                        BEGIN 
                                                            update TIRE set status_id='5', date_modified=@date_modified, userid=@userid where tire_id=@number_old
                                                        END 
                                                    ", new
                                                            {
                                                                detail.number_old,
                                                                date_modified,
                                                                p.userid,
                                                                p.trx_date
                                                    }, tran);
                                                    conn.Execute(@"INSERT INTO TIRE_UNIT
                                                                (vehicle_id,tire_id,description, is_delete, date_modified, date_created, userid)
                                                            VALUES
                                                                (@vehicle_id,@number_new,'',0,@date_modified,@date_modified,@userid)"
                                                    , new
                                                    {
                                                        detail.vehicle_id,
                                                        date_modified,
                                                        detail.number_new,
                                                        p.trx_date,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@" INSERT INTO TIRE_AXIS
                                                            (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                                                VALUES
                                                                    (@vehicle_id,@number_new,@axis_id_old,@date_modified,'',0, @userid)
                                                            UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_modified WHERE tire_id = @number_new
                                                            "
                                                    , new
                                                    {
                                                        p.userid,
                                                        axis_id_old,
                                                        detail.vehicle_id,
                                                        date_modified,
                                                        unit_location_id,
                                                        p.trx_date,
                                                        detail.number_new,
                                                    }, tran);
                                                    conn.Execute(@"
                                                        insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                        values ([dbo].[NextStockInId](@yearmonth),@trx_date,@unit_location_id,@number_new,0,@userid)", new
                                                    {
                                                        yearmonth,
                                                        unit_location_id,
                                                        p.trx_date,
                                                        detail.number_new,
                                                        p.userid
                                                    }, tran);



                                                    conn.Execute(@"
                                                        insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                                        values ([dbo].[NextStockOutId](@yearmonth),@trx_date,@location_id,@number_new,0,@userid)", new
                                                    {
                                                        yearmonth,
                                                        p.location_id,
                                                        p.trx_date,
                                                        detail.number_new,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                        values (@number_new,@current_epc_newtire,'102','XXX','XXX',@unit_location_id,0,@date_modified,@userid)", new
                                                    {
                                                        detail.number_new,
                                                        p.trx_date,
                                                        date_modified,
                                                        current_epc_newtire,
                                                        unit_location_id,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@" 
                                                        BEGIN 
                                                            update TIRE set status_id='1', date_modified=@date_modified, userid=@userid where tire_id=@number_new
                                                        END 
                                                    ", new
                                                            {
                                                                detail.number_new,
                                                                p.userid,
                                                                date_modified,
                                                                p.trx_date
                                                    }, tran);
                                                    //INPUT KE TABEL HISTORY TIRE_CHANGE
                                                    string[] data_schedule = Process.getDataScheduleByUser(p.userid);
                                                    string year_sch = data_schedule[0];
                                                    string month_sch = data_schedule[1];
                                                    string date_sch = data_schedule[2];
                                                    string location_id_sch = data_schedule[3];
                                                    string group_id_sch = data_schedule[4];
                                                    string shift_id_sch = data_schedule[5];
                                                    string tire_change_id = getNextTireChangeId(yearmonth);

                                                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_change_id from TIRE_CHANGE where tire_change_id=@tire_change_id) ) 
                                                            BEGIN 
                                                                INSERT INTO TIRE_CHANGE 
                                                                (tire_change_id,notes, vehicle_id, location_id, km, time_start, time_end,old_tire_id, new_tire_id, date_modified,date_created,is_delete,userid)
                                                                values (@tire_change_id,'Change Tyre Sync', @vehicle_id, @location_id,@km,@time_start, @time_end,@number_old,@number_new,@date_modified,@date_modified,0,@userid)
                                                            END 

                                                    ", new
                                                            {
                                                                tire_change_id,
                                                                detail.vehicle_id,
                                                                date_modified,
                                                                p.location_id,
                                                                detail.number_old,
                                                                detail.number_new,
                                                                detail.time_start,
                                                                detail.time_end,
                                                                p.km,
                                                                p.trx_date,
                                                                p.userid
                                                            }, tran);


                                                            conn.Execute(@"INSERT INTO SCHEDULE_TIRE_CHANGE (year, month, date, location_id, group_id, shift_id,emp_id, tire_change_id, is_delete, date_created, date_modified, userid)
                                                
                                                                select @year_sch, @month_sch, @date_sch, @location_id_sch, @group_id_sch, @shift_id_sch, emp_id, @tire_change_id, 0, @date_modified, @date_modified, @userid from GROUPS_EMPLOYEES where group_id=@group_id_sch
                                                

                                                    ", new//(@year_sch, @month_sch, @date_sch, @location_id_sch, @group_id_sch, @shift_id_sch, @tire_change_id, 0, @date_created, @date_created, @userid)
                                                            {
                                                                year_sch,
                                                                month_sch,
                                                                date_sch,
                                                                location_id_sch,
                                                                group_id_sch,
                                                                shift_id_sch,
                                                                date_modified,
                                                                tire_change_id,
                                                                detail.vehicle_id,
                                                                p.location_id,
                                                                tsch_id,
                                                                p.km,
                                                                p.trx_date,
                                                                p.userid
                                                            }, tran);
                                                    returnRes.flag = true;

                                                }
                                                catch (Exception ex)
                                                {
                                                    tran.Rollback();
                                                    returnRes.flag = false;
                                                    returnRes.message = " #Failed, " + ex.Message.ToString();
                                                }
                                            }

                                        }else if (detail.process_id == "116")//PROSES CHANGE FLEET/ VEHICLE
                                        {
                                            string location_type_id = getLocationTypeFleet(p.fleet_id);
                                            is_change_vhc = true;
                                            try
                                            {
                                                if(detail.number_new == "")
                                                {
                                                    conn.Execute(@"update fleet_vehicle set is_delete=1, date_modified=@date_modified, userid=@userid where fleet_id=@fleet_id and vehicle_id not in (@fleet_id)
                                                    ", new
                                                    {
                                                        p.trx_date,
                                                        p.fleet_id,
                                                        date_modified,
                                                        p.userid
                                                    }, tran);
                                                    conn.Execute(@"
                                                              update FLEET_VEHICLE set is_delete=0, date_modified=@date_modified, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id
                                                       

                                                    ", new
                                                            {
                                                                p.fleet_id,
                                                                detail.vehicle_id,
                                                                date_modified,
                                                                p.userid
                                                            }, tran);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                tran.Rollback();
                                                returnRes.flag = false;
                                                returnRes.message = " #Failed, " + ex.Message.ToString();
                                            }
                                        }
                                    
                                    
                                    }
                                    if (is_change_vhc == true)
                                    {
                                        string vhc_change_no = getNextVhcChangeNo(yearmonth);
                                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 vehicle_change_no from VEHICLE_CHANGE where vehicle_change_no=@vhc_change_no) ) 
                                                    BEGIN 
                                                        INSERT INTO VEHICLE_CHANGE 
                                                        (vehicle_change_no,date,notes, km, hm, date_modified,date_created,is_delete,userid)
                                                        values (@vhc_change_no,@date_modified,@notes,@km,@hm,@date_modified,@date_modified,0,@userid)
                                                    END 

                                            ", new
                                        {
                                            vhc_change_no,
                                            notes = "Change Vehicle Sync",
                                            p.km,
                                            p.hm,
                                            date_modified,
                                            p.userid
                                        }, tran);
                                        int no = 1;
                                        foreach (cCrudSyncOfflineActivityDetail detail in p.data)
                                        {
                                            conn.Execute(@"
                                                INSERT INTO VEHICLE_CHANGE_DETAIL 
                                                (vehicle_change_no,date,sort,old_fleet_id,old_vehicle_id,new_fleet_id,new_vehicle_id,date_created,date_modified,is_delete,userid)
                                                values (@vhc_change_no,@date_modified,@no,@fleet_id,@number_old,@fleet_id,@number_new,@date_modified,@date_modified,0,@userid)
                                    
                                        ", new
                                                {
                                                    vhc_change_no,
                                                    p.fleet_id,
                                                    no,
                                                    detail.number_old,
                                                    detail.number_new,
                                                    date_modified,
                                                    p.userid
                                                }, tran);
                                            no++;
                                        }
                                    }
                                    tran.Commit();
                                }
                                catch (Exception ex)
                                {
                                    tran.Rollback();
                                    returnRes.flag = false;
                                    returnRes.message = " #Failed, " + ex.Message.ToString();

                                }
                            }
                            
                            conn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        returnRes.flag = false;
                        returnRes.message = " #Failed, "+ ex.Message.ToString();

                    }
                }
            }

            loncatError:
            if (returnRes.flag == true)
            {
                returnRes.message = "Data Offline Activity Syncronized. ";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertSyncronize(cCrudSync p)
        {
            List<cCrudSyncDetail> ListDetail = new List<cCrudSyncDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudSyncDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Syncronize";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            int DataWasImported = 0;
            int DataNotImportYet = 0;
            string yearmonth =  DateTime.Now.ToString("yyyyMM");
            string year = DateTime.Now.ToString("yyyy");
            string location_inb = "";
            string date_inb = "";
            string userid_inb = "";
            string errTireId = "";
            string msgDuplikatEpc = "";
            string listEpcDuplicates = "";
            string finalEpcDuplicates = "''";
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        
                        HashSet<string> uniqueEpcs = new HashSet<string>();
                        bool isDuplicateEpc = false;
                        string EpcDuplicates = "";
                        HashSet<string> epcList = new HashSet<string>();

                        HashSet<string> epcListNotDouble = new HashSet<string>();
                        foreach (var dr in ListDetail)
                        {
                            // Mengecek apakah EPC sudah ada dalam daftar epcList
                            if (!epcList.Add(dr.rfid))
                            {
                                // Terdapat EPC yang sama
                                epcListNotDouble.Add(dr.rfid);
                                msgDuplikatEpc += dr.rfid + ",";
                                isDuplicateEpc = true;
                            }
                            location_inb = dr.location_id;
                            date_inb = Convert.ToDateTime(dr.date_created).ToString("yyyy-MM-dd");
                            userid_inb = dr.userid;
                        }
                        //foreach (cCrudSyncDetail detail in ListDetail)
                        //{
                        //    if (!uniqueEpcs.Add(detail.rfid))
                        //    {
                        //        // Terdapat EPC yang sama
                        //        EpcDuplicates = detail.rfid;
                        //        msgDuplikatEpc += EpcDuplicates + ",";
                        //        listEpcDuplicates += "'"+ EpcDuplicates + "'" + ",";
                        //        isDuplicateEpc = true;
                        //        //break;
                        //    }

                        //    location_inb = detail.location_id;
                        //    date_inb = Convert.ToDateTime(detail.date_created).ToString("yyyy-MM-dd");
                        //    userid_inb = detail.userid;
                        //}
                        //if (listEpcDuplicates != "") {
                        //    finalEpcDuplicates = listEpcDuplicates.TrimEnd(',');
                        //}
                        if (msgDuplikatEpc != "")
                        {
                            finalEpcDuplicates = msgDuplikatEpc.TrimEnd(',');
                        }

                        string yearmonth2 = Convert.ToDateTime(date_inb).ToString("yyyyMM");
                        foreach (var dr in ListDetail)
                        {
                            if (dr.tag_type_id == "00" && dr.rfid != EpcDuplicates)
                            {
                                DataWasImported += getTireValidationSync(dr.tag_id);
                                DataNotImportYet++;
                            }

                        }
                            if (isDuplicateEpc)
                        {
                            // Ada EPC yang sama dalam daftar
                            msgDuplikatEpc = "Duplicate EPC: " + msgDuplikatEpc.TrimEnd(',');
                        }
                        else
                        {
                            // Tidak ada EPC yang sama dalam daftar
                            Console.WriteLine("Tidak ada EPC yang sama dalam daftar.");
                        }
                        inbound_id = getNextInboundId(yearmonth2);
                        conn.Execute(@"
                                            INSERT INTO INBOUND 
                                         (inbound_id,date_created,location_id,is_delete,userid)
                                         values (@inbound_id,@date_inb,@location_inb,0,@userid_inb)


                                    ", new
                        {
                            inbound_id,
                            date_inb,
                            location_inb,
                            userid_inb
                        }, tran);
                        foreach (var dr in ListDetail)
                        {
                            string tire_id = dr.tag_id.Trim();
                            errTireId = dr.tag_id;
                            conn.Execute(@"
                                        IF (@rfid in @epcListNotDouble)
                                        BEGIN
                                            IF (NOT EXISTS (select TOP 1 rfid from LOG_FAILED_IMPORT_DATA where rfid = @rfid and tag_id=@tire_id)) 
                                            BEGIN 
                                                insert into LOG_FAILED_IMPORT_DATA
                                                        (rfid,tag_id,tag_type_id,spec,location_id,status,process_id,userid,date_created)
                                                        values (@rfid,@tire_id,@tag_type_id,@spec,@location_id,'Duplicate EPC, different Serial Number.',@process_id, @userid,@date_created)
                                                END
                                           END
                                        ELSE
                                        BEGIN
                                            IF (NOT EXISTS (select TOP 1 rfid from TEMP_TAG_DATA where rfid = @rfid)) 
                                            BEGIN 
                                                insert into TEMP_TAG_DATA
                                                (rfid,tag_id,tag_type_id,spec,location_id,process_id,userid,date_created)
                                                values (@rfid,@tire_id,@tag_type_id,@spec,@location_id,@process_id, @userid,@date_created)

                                                IF (@tag_type_id = '00')
                                                BEGIN

                                                    IF (NOT EXISTS (select top 1 tire_id from TIRE where tire_id = @tire_id))
                                                    BEGIN
                                                        insert into TIRE
                                                        (tire_id, location_id, tire_code, tire_name, serial_number, size, pattern, km, rfid, status_id, is_delete, date_created, date_modified, userid)  
                                                        values(@tire_id, @location_id, 
                                                            (select top 1 tire_code from item where tire_code = @spec), 
                                                            (select top 1 tire_name from item where tire_code = @spec),
                                                            @tire_id,
                                                            (select top 1 size from item where tire_code = @spec),
                                                            (select top 1 pattern from item where tire_code = @spec),
                                                            '0',@rfid,'1','0',@date_created,@date_created,@userid
                                                        )
                                                      IF (NOT EXISTS (select TOP 1 epc_rfid from EPC where epc_rfid=@rfid))
                                                            BEGIN
                                                                INSERT INTO EPC (epc_rfid,is_delete,userid,date_created)
                                                                VALUES (@rfid,'0',@userid,@date_created)
                                                            END
                                                            BEGIN
                                                      IF (NOT EXISTS (select TOP 1 epc_rfid from EPC_TIRE where epc_rfid=@rfid and tire_id=@tire_id))
                                                            BEGIN
                                                                INSERT INTO EPC_TIRE (epc_rfid,tire_id,is_delete,userid,date_created)
                                                                VALUES (@rfid,@tire_id,'0',@userid,@date_created)
                                                                END
                                                            END
                                                        /*insert into RECEIVE_DETAIL
                                                        (receive_no, date_created, serial_number, tire_code)
                                                        values(@receive_no, (select top 1 date_created from RECEIVE where receive_no = @receive_no), @tire_id,  (select top 1 tire_code from item where tire_code = @spec))                                        
                                                       */
                                                            insert into REGISTER_TIRE
                                                            (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,date_modified,is_delete)
                                                            values ((select register_no=[dbo].[NextRegisterNoTire](@year)),@date_created,@tire_id,@location_id,@userid,@rfid,@date_created,0)
                                                        insert into TIRE_TRACKING
                                                        (date_created, tire_id, process_id, smo_id, smr_id,epc_rfid, location_id, is_delete, date_modified, userid)
                                                        values 
                                                        (@date_created, @tire_id, @process_id, 'XXX', 'XXX', @rfid,@location_id, '0', @date_created, @userid)


                                                        insert into TIRE_TRACKING
                                                        (date_created, tire_id, process_id, smo_id, smr_id,epc_rfid, location_id, is_delete, date_modified, userid)
                                                        values 
                                                        (@date_created, @tire_id, '110', 'XXX', 'XXX', @rfid,@location_id, '0', @date_created, @userid)
                                                        
                                                         insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete,date_modified, userid)
                                                        values ([dbo].[NextStockInId](@yearmonth),@date_created,'A00S002',@tire_id,0,@date_created,@userid)
                                                        
                                                        IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND_DETAIL where inbound_id=@inbound_id and tire_id=@tire_id) ) 
                                                        BEGIN 
                                                            INSERT INTO INBOUND_DETAIL 
                                                         (inbound_id,date_created,date_modified,tire_id,is_delete,userid)
                                                         values (@inbound_id,@date_inb,@date_created,@tire_id,0,@userid)
                                                        END 
                                                        
                                                        insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                                        values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)

                                                        insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                                        values (@tire_id,@rfid,'104','XXX','XXX',@location_id,0,@date_created,@userid)

                                                    END
                                                    ELSE
                                                    BEGIN
                                                        insert into LOG_FAILED_IMPORT_DATA
                                                        (rfid,tag_id,tag_type_id,spec,location_id,status,process_id,userid,date_created)
                                                        values (@rfid,@tire_id,@tag_type_id,@spec,@location_id,'Duplicate Serial Number.',@process_id, @userid,@date_created)
                                                    END




                                                END
                                            END 
                                            ELSE
                                            BEGIN 
                                            insert into LOG_FAILED_IMPORT_DATA
                                                    (rfid,tag_id,tag_type_id,spec,location_id,status,process_id,userid,date_created)
                                                    values (@rfid,@tire_id,@tag_type_id,@spec,@location_id,'Duplicate EPC, different Serial Number.',@process_id, @userid,@date_created)
                                            END
                                        END
                                       
                                        
                                    ", new
                            {
                                yearmonth,
                                year,
                                dr.rfid,
                                dr.location_id,
                                dr.process_id,
                                dr.spec,
                                dr.tag_id,
                                dr.tag_type_id,
                                dr.userid,
                                inbound_id,
                                date_inb,
                                tire_id,
                                dr.date_created,
                                EpcDuplicates,
                                finalEpcDuplicates,
                                epcListNotDouble
                            }, tran);
                            errTireId = dr.tag_id + "Error insert tire";
                            //conn.Execute(@"
                            //               IF (EXISTS (select TOP 1 tag_id from TEMP_TAG_DATA where tag_id = @tag_id)) 
                            //            BEGIN 
                            //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete,date_modified, userid)
                            //               values ([dbo].[NextStockInId](@yearmonth),@date_created,'A00S002',@tag_id,0,@date_created,@userid)
                            //            END
                            //               ", new
                            //{
                            //    yearmonth,
                            //    dr.location_id,
                            //    dr.date_created,
                            //    dr.tag_id,
                            //    inbound_id,
                            //    date_inb,
                            //    tire_id,
                            //    dr.userid
                            //}, tran);
                            
                            //errTireId = dr.tag_id + "Error insert stock in";
                            //conn.Execute(@"
                            //              IF (EXISTS (select TOP 1 tag_id from TEMP_TAG_DATA where tag_id = @tire_id)) 
                            //            BEGIN 
                            //                IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND_DETAIL where inbound_id=@inbound_id and tire_id=@tire_id) ) 
                            //                BEGIN 
                            //                    INSERT INTO INBOUND_DETAIL 
                            //                 (inbound_id,date_created,date_modified,tire_id,is_delete,userid)
                            //                 values (@inbound_id,@date_inb,@date_created,@tire_id,0,@userid)
                            //                END 
                            //            END

                            //        ", new
                            //{
                            //    inbound_id,
                            //    date_inb,
                            //    dr.date_created,
                            //    tire_id,
                            //    dr.location_id,
                            //    date_modified,
                            //    dr.userid
                            //}, tran);

                            //errTireId = dr.tag_id + "Error insert inbound detail";
                            //conn.Execute(@"
                            //             IF (EXISTS (select TOP 1 tag_id from TEMP_TAG_DATA where tag_id = @tire_id)) 
                            //            BEGIN 
                            //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                            //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)
                            //            END", new
                            //{
                            //    yearmonth,
                            //    dr.location_id,
                            //    dr.date_created,
                            //    tire_id,
                            //    dr.userid
                            //}, tran);

                            //errTireId = dr.tag_id + "Error insert stock in 2";
                            //conn.Execute(@"
                            //            IF (EXISTS (select TOP 1 tag_id from TEMP_TAG_DATA where tag_id = @tire_id)) 
                            //            BEGIN 
                            //            insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                            //             values (@tire_id,@rfid,'104','XXX','XXX',@location_id,0,@date_created,@userid)
                            //            END
                            //              ", new
                            //{
                            //    tire_id,
                            //    dr.date_created,
                            //    dr.rfid,
                            //    dr.location_id,
                            //    dr.userid
                            //}, tran);

                            //errTireId = dr.tag_id + "Error insert tracking terakhir";
                            
                        }


                        if (DataNotImportYet - DataWasImported == 0) {
                            conn.Execute(@"
                                        delete from inbound where inbound_id =@inbound_id
                                          ", new
                            {
                                inbound_id
                            }, tran);
                        }

                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = errTireId+" # Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Data Syncronized. "+ msgDuplikatEpc;//DataNotImportYet - DataWasImported + 
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertTyreNonPTSI(cCrudSync p)
        {
            List<cCrudTyreNonPTSIDetail> ListDetail = new List<cCrudTyreNonPTSIDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudTyreNonPTSIDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Syncronize";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            int DataWasImported = 0;
            int DataNotImportYet = 0;
            string yearmonth = DateTime.Now.ToString("yyyyMM");
            string year = DateTime.Now.ToString("yyyy");
            string location_inb = "";
            string date_inb = "";
            string userid_inb = "";
            string errTireId = "";
            string msgDuplikatEpc = "";
            string listEpcDuplicates = "";
            string finalEpcDuplicates = "''";
            string userid = "";
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                foreach (var dr in ListDetail)
                {
                    if (checkTyreCode(dr.item_code) == false)
                    {
                        returnRes.flag = false;
                        returnRes.message = errTireId + " # Failed, " + dr.item_code + " is not exist. Please input this reference.";
                        goto loncatSini;
                    }
                }

                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListDetail)
                        {
                            string tire_id = dr.tire_id.Trim();
                            userid = getUseridByUsername(dr.username);
                            conn.Execute(@"
                                       IF (NOT EXISTS (select top 1 tire_id from TIRE where tire_id = @tire_id))
                                        BEGIN
                                            insert into TIRE
                                            (tire_id, location_id, tire_code, tire_name, serial_number, size, pattern, km, rfid, status_id, is_delete, date_created, date_modified, userid)  
                                            values(@tire_id, 'A89', 
                                                @item_code, 
                                                (select top 1 tire_name from item where tire_code = @item_code),
                                                @tire_id,
                                                (select top 1 size from item where tire_code = @item_code),
                                                (select top 1 pattern from item where tire_code = @item_code),
                                                '0','0','1','0',@date_created,@date_created,@userid
                                            )
                                        END
                                        
                                       
                                       IF (NOT EXISTS (select top 1 tire_id from TIRE_NON_PTSI where tire_id = @tire_id))
                                        BEGIN
                                            insert into TIRE_NON_PTSI
                                            (tire_id,date_created, date_modified, userid)  
                                            values(@tire_id,@date_created,@date_created,@userid)
                                        END
                                        
                                    ", new
                            {
                                yearmonth,
                                year,
                                dr.item_code,
                                dr.tire_id,
                                userid,
                                dr.date_created,
                            }, tran);
                           

                        }

                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = errTireId + " # Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            loncatSini:
            if (returnRes.flag == true)
            {
                returnRes.message = "Data Saved. ";//DataNotImportYet - DataWasImported + 
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
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
        public static cRespon InsertWriteData(cCrudWriteData p)
        {
            List<cCrudWriteDataDetail> ListDetail = new List<cCrudWriteDataDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudWriteDataDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Syncronize";
            string userid = "";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;
            DateTime date_created = DateTime.Now;

            string yearmonth =  DateTime.Now.ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListDetail)
                        {
                            string tag_id = dr.tag_id.Trim();
                            userid = getUseridByUsername(dr.username);
                            //IF(NOT EXISTS(select top 1 tire_id from TIRE where tire_id = @tag_id))
                            //                    BEGIN
                            //                        insert into TIRE
                            //                        (tire_id, location_id, tire_type_id, tire_code, tire_name, serial_number, size, pattern, km, rfid, status_id, is_delete, date_created, date_modified, userid)
                            //                        values(@tag_id, @location_id, '1',
                            //                            (select top 1 tire_code from item where tire_code = @spec), 
                            //                            (select top 1 tire_name from item where tire_code = @spec),
                            //                            @tag_id,
                            //                            (select top 1 size from item where tire_code = @spec),
                            //                            (select top 1 pattern from item where tire_code = @spec),
                            //                            '0',@rfid,'1','0',@date_created,NULL,@userid)
                            //                    END
                            //insert into tire_stock_in(stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                            //               values([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tag_id,0,@userid)
                            //IF(NOT EXISTS(select TOP 1 vehicle_id from VEHICLE where vehicle_id = @tag_id))
                            //                BEGIN
                            //                    insert into vehicle(vehicle_id, vehicle_name, vehicle_rfid, vehicle_type_id, axis_type, brand_vhc_id, is_delete, userid, date_created)
                            //                    values
                            //                    (@tag_id, @tag_id + ' ' + @brand,
                            //                        @rfid,
                            //                    (select vehicle_type_id from VEHICLE_TYPE where vehicle_type = @vhc_type),
                            //                    @axis_type,
                            //                    (select brand_vhc_id from BRAND_VEHICLE where brand_vhc_name = @brand),
                            //                    '0',@userid,@date_created
                            //                    )
                            //                END

                            conn.Execute(@"
                                       

                                    IF (@tag_type_id = '00')
                                        BEGIN
                                            
                                            IF (NOT EXISTS (select TOP 1 epc_rfid from EPC where epc_rfid=@rfid))
                                                    BEGIN
                                                        INSERT INTO EPC (epc_rfid,is_delete,userid,date_created)
                                                        VALUES (@rfid,'0',@userid,@date_created)
                                                    END
                                            IF (NOT EXISTS (select TOP 1 epc_rfid from EPC_TIRE where epc_rfid=@rfid and tire_id=@tag_id))
                                                    BEGIN
                                                        INSERT INTO EPC_TIRE (epc_rfid,tire_id,is_delete,userid,date_created)
                                                        VALUES (@rfid,@tag_id,'0',@userid,@date_created)
                                                
                                                    END
                                

                                            insert into TIRE_TRACKING
                                            (date_created, tire_id, process_id, smo_id, smr_id,epc_rfid, location_id, is_delete, date_modified, userid)
                                            values 
                                            (@date_created, @tag_id, @process_id, 'XXX', 'XXX', @rfid,@location_id, '0', NULL, @userid)
                                            
                                    END
                                    ELSE
                                    BEGIN
                                       
                                        IF(NOT EXISTS(Select TOP 1 epc_rfid from epc where epc_rfid=@rfid))
                                            BEGIN
                                                insert into epc (epc_rfid,is_delete,userid,date_created)
                                                values(@rfid,'0',@userid,@date_created)
                                            END
                                        IF(NOT EXISTS(Select TOP 1 epc_rfid from epc_vhc where epc_rfid=@rfid and vehicle_id=@tag_id))
                                            BEGIN
                                                insert into epc_vhc (epc_rfid, vehicle_id, is_delete, date_created, userid)
                                                values(@rfid,@tag_id,'0',@date_created,@userid)
                                            END
                                    END
                                    ", new
                            {
                                yearmonth,
                                dr.vhc_type,
                                dr.rfid,
                                dr.location_id,
                                dr.process_id,
                                dr.spec,
                                tag_id,
                                dr.tag_type_id,
                                userid,
                                date_created,
                                dr.brand,
                                dr.axis_type
                            }, tran);
                            //conn.Execute(@"
                            //                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                            //               values ([dbo].[NextStockInId](@yearmonth),@date_created,'A00S002',@tag_id,0,@userid)", new
                            //{
                            //    yearmonth,
                            //    dr.location_id,
                            //    dr.date_created,
                            //    dr.tag_id,
                            //    dr.userid
                            //}, tran);
                        }




                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Write data succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon SetReplaceTag(cCrudReplaceTag p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Replace Tag";
            string userid = "";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;
            DateTime date_created = DateTime.Now;
            string process_id = "113";
            string yearmonth =  DateTime.Now.ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        userid = getUseridByUsername(p.username);
                        conn.Execute(@"
                                       

                                IF (@type = '00')
                                    BEGIN
                                            
                                        IF (NOT EXISTS (select TOP 1 epc_rfid from EPC where epc_rfid=@new_rfid))
                                                BEGIN
                                                    INSERT INTO EPC (epc_rfid,is_delete,userid,date_created,date_modified)
                                                    VALUES (@new_rfid,'0',@userid,@date_created,@date_created)
                                                END
                                        IF (NOT EXISTS (select TOP 1 epc_rfid from EPC_TIRE where epc_rfid=@new_rfid and tire_id=@number))
                                                BEGIN
                                                    INSERT INTO EPC_TIRE (epc_rfid,tire_id,is_delete,userid,date_created,date_modified)
                                                    VALUES (@new_rfid,@number,'0',@userid,@date_created,@date_created)
                                                
                                                END
                                        
                                                INSERT INTO REPLACE_TAG_TIRE (tire_id,old_epc_rfid, new_epc_rfid,notes,is_delete,userid,date_created,date_modified)
                                                VALUES (@number, @old_rfid, @new_rfid,@notes,'0',@userid,@date_created,@date_created)
                                                
                                        update tire set rfid=@new_rfid, date_modified=@date_created, userid=@userid where tire_id=@number
                                        insert into TIRE_TRACKING
                                        (date_created, tire_id, process_id, smo_id, smr_id,epc_rfid, location_id, is_delete, date_modified, userid)
                                        values 
                                        (@date_created, @number, @process_id, 'XXX', 'XXX', @new_rfid,@location_id, '0', @date_created, @userid)
                                            
                                END
                                ELSE
                                BEGIN
                                       
                                    IF(NOT EXISTS(Select TOP 1 epc_rfid from epc where epc_rfid=@new_rfid))
                                        BEGIN
                                            insert into epc (epc_rfid,is_delete,userid,date_created,date_modified)
                                            values(@new_rfid,'0',@userid,@date_created,@date_created)
                                        END
                                    IF(NOT EXISTS(Select TOP 1 epc_rfid from epc_vhc where epc_rfid=@new_rfid and vehicle_id=@number))
                                        BEGIN
                                            insert into epc_vhc (epc_rfid, vehicle_id, is_delete, date_created,date_modified, userid)
                                            values(@new_rfid,@number,'0',@date_created,@date_created,@userid)
                                        END
                                        INSERT INTO REPLACE_TAG_VHC (vehicle_id,old_epc_rfid, new_epc_rfid,notes,is_delete,userid,date_created,date_modified)
                                                VALUES (@number, @old_rfid, @new_rfid,@notes,'0',@userid,@date_created,@date_created)
                                        update vehicle set vehicle_rfid=@new_rfid, date_modified=@date_created, userid=@userid where vehicle_id=@number
                                END
                                ", new
                        {
                            yearmonth,
                            p.old_rfid,
                            p.new_rfid,
                            p.location_id,
                            process_id,
                            p.number,
                            p.type,
                            userid,
                            p.notes,
                            date_created
                        }, tran);
                       



                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Replace Tag succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon SetChangeEpc(cCrudChangeEpc p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Change Epc";
            string userid = "";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;
            DateTime date_created = DateTime.Now;
            string process_id = "114";
            string yearmonth = DateTime.Now.ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                string EPCCurrent = getEPCCurrentTire(p.number);
                if (EPCCurrent != p.new_rfid)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            userid = getUseridByUsername(p.username);
                            conn.Execute(@"
                                       

                                    IF (@type = '00')
                                        BEGIN
                                            
                                            IF (NOT EXISTS (select TOP 1 epc_rfid from EPC where epc_rfid=@new_rfid))
                                                    BEGIN
                                                        INSERT INTO EPC (epc_rfid,is_delete,userid,date_created,date_modified)
                                                        VALUES (@new_rfid,'0',@userid,@date_created,@date_created)
                                                    END
                                            update EPC_TIRE set epc_rfid=@new_rfid, date_modified=@date_created, userid=@userid where tire_id=@number and date_created = (select max(date_created) from epc_tire where tire_id=@number)
                                           
                                            update tire set rfid=@new_rfid, date_modified=@date_created, userid=@userid where tire_id=@number
                                            update TIRE_TRACKING set epc_rfid=@new_rfid, date_modified=@date_created, userid=@userid where tire_id=@number  and date_created = (select max(date_created) from TIRE_TRACKING where tire_id=@number)
                                           
                                            
                                    END
                                    ELSE
                                    BEGIN
                                       
                                        IF(NOT EXISTS(Select TOP 1 epc_rfid from epc where epc_rfid=@new_rfid))
                                            BEGIN
                                                insert into epc (epc_rfid,is_delete,userid,date_created,date_modified)
                                                values(@new_rfid,'0',@userid,@date_created,@date_created)
                                            END
                                        update EPC_VHC set epc_rfid=@new_rfid, date_modified=@date_created, userid=@userid where vehicle_id=@number and date_created = (select max(date_created) from EPC_VHC where vehicle_id=@number)
                                           
                                            update vehicle set vehicle_rfid=@new_rfid, date_modified=@date_created, userid=@userid where vehicle_id=@number 
                                    END
                                    ", new
                            {
                                yearmonth,
                                p.new_rfid,
                                p.location_id,
                                process_id,
                                p.number,
                                p.type,
                                userid,
                                date_created
                            }, tran);




                            tran.Commit();
                            //





                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                }
                else {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, New Epc is not valid (same as old one)";
                }
               
                conn.Close();
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Replace Tag succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertUtilityTireStokInUnitPerFleet(cCrudUtilityTireStokInUnitPerFleet p)
        {
            List<cCrudUtilityTireStokInUnitPerFleetDataVehicle> ListVehicle = new List<cCrudUtilityTireStokInUnitPerFleetDataVehicle>();

            ListVehicle = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInUnitPerFleetDataVehicle>>(p.data);

            //List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire> ListTire = new List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>();

            //ListTire = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>>(ListVehicle.);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "";
            string unit_location_id = Master.getDefaultUnitLocationId();
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;
            string axis = "";
            double total_km = 0;
            DateTime date = DateTime.Now;
            double total_hm = 0;
            double km_before = 0;
            double hm_before = 0;
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET where fleet_id=@fleet_id) ) 
                                        BEGIN 
                                         INSERT INTO FLEET
                                         (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, userid)
                                         values (@fleet_id,@location_type_id,@fleet_name,0,0,@description,0,@date_created, @userid)
                                        END 
                                        ELSE
                                        BEGIN 
                                          update FLEET set fleet_name=@fleet_name, location_type_id = @location_type_id, description=@description, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id
                                        END 
                                
                                    ", new
                        {
                            p.fleet_id,
                            p.fleet_name,
                            p.description,
                            p.location_type_id,
                            p.date_created,
                            p.userid
                        }, tran);
                        foreach (var dr in ListVehicle)
                        {
                            conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE WHERE vehicle_id = @vehicle_id))
                                        BEGIN
                                            INSERT INTO VEHICLE
                                                (vehicle_id,brand_vhc_id,vehicle_name,date_created,axis_type, vehicle_type_id,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,@brand_vhc_id,@vehicle_name,@date_created,@axis_type, @vehicle_type_id,'False',@userid)
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE VEHICLE SET vehicle_name = @vehicle_name, brand_vhc_id=@brand_vhc_id, axis_type = @axis_type, date_modified=@date_created, userid=@userid
                                            WHERE vehicle_id = @vehicle_id
                                        END"
                            , new
                            {
                                p.date_created,
                                dr.vehicle_id,
                                dr.vehicle_name,
                                dr.axis_type,
                                dr.brand_vhc_id,
                                dr.vehicle_type_id,
                                p.userid

                            }, tran);
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@position) ) 
                                    BEGIN 
                                        INSERT INTO FLEET_VEHICLE
                                     (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                     values (@fleet_id, @vehicle_id, @position,@date_created, @description,'0', @date_modified,@userid)
                                    END 
                                    ELSE
                                        BEGIN 
                                          update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@position
                                        END 

                                ", new
                            {
                                p.fleet_id,
                                dr.vehicle_id,
                                dr.position,
                                p.date_created,
                                p.description,
                                date_modified,
                                p.userid
                            }, tran);

                            conn.Execute(@"
 IF (NOT EXISTS (select TOP 1 vehicle_id from VEHICLE where vehicle_id=@vehicle_id) ) 
                                            BEGIN 
IF (NOT EXISTS (select TOP 1 register_vhc_no from REGISTER_VHC where register_vhc_no=[dbo].[NextRegisterNoVehicle](@yearmonth) and vehicle_id=@vehicle_id) ) 
                                BEGIN 
                                    INSERT INTO REGISTER_VHC
                                 (register_vhc_no,date_created,vehicle_id,location_id,userid,reg_rfid,is_delete)
                                 values ([dbo].[NextRegisterNoVehicle](@yearmonth),@date_created,@vehicle_id,'A00S001',@userid,@rfid,0)
                                END 
                                END
                            ", new
                            {
                                yearmonth,
                                p.date_created,
                                dr.vehicle_id,
                                p.userid,
                                dr.rfid
                            }, tran);

                            List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire> ListTire = new List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>();

                            ListTire = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>>(dr.data);
                            foreach (var dt in ListTire)
                            {
                                conn.Execute(@"
                                             IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                                insert into tire (tire_id, location_id, tire_code, tire_name, serial_number,
                                                size, pattern, km, rfid, status_id, is_delete, date_created, userid)
                                                values
                                                (@serial_number, @unit_location_id, @tire_code, @tire_name, @serial_number,
                                                @size, @pattern, 0, @rfid, '1', 0, @date_created, @userid)
                                            
                                                insert into epc_tire (epc_rfid, tire_id, is_delete, date_created, date_modified, userid)
                                                 values
                                                (@rfid,@serial_number,0,@date_created,@date_created, @userid)
                                            END
", new
                                {
                                    dt.serial_number,
                                    dt.tire_type_id,
                                    dt.tire_code,
                                    unit_location_id,
                                    dt.tire_name,
                                    dt.size,
                                    dt.pattern,
                                    dt.rfid,
                                    p.date_created,
                                    p.userid
                                }, tran);
                                conn.Execute(@"
                                            IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,'A00S001',@serial_number,0,@userid) END", new
                                {
                                    yearmonth,
                                    p.date_created,
                                    dt.serial_number,
                                    unit_location_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"
                                              IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                            insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@serial_number,@rfid,'005','XXX','XXX','A00S001',0,@date_created,@userid) END", new
                                {
                                    dt.serial_number,
                                    p.date_created,
                                    dt.rfid,
                                    unit_location_id,
                                    p.userid
                                }, tran);


                                conn.Execute(@"
                                             IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                                    IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=[dbo].[NextRegisterNoTire](@yearmonth) and tire_id=@serial_number) ) 
                                                    BEGIN 
                                                        INSERT INTO REGISTER_TIRE
                                                     (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                                                     values ( [dbo].[NextRegisterNoTire](@yearmonth),@date_created,@serial_number,'A00S001',@userid,@rfid,0)
                                                    END 
                                END
                                                ", new
                                {
                                    yearmonth,
                                    p.date_created,
                                    dt.serial_number,
                                    unit_location_id,
                                    p.userid,
                                    dt.rfid
                                }, tran);

                                conn.Execute(@"
IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                        INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                            values
                                            (@serial_number,@rfid,'110', 'XXX','XXX', 'A00S001',0, @date_created, @userid)
END
                                    
                                ", new
                                {
                                    p.date_created,
                                    dt.serial_number,
                                    unit_location_id,
                                    dt.rfid,
                                    p.userid

                                }, tran);

                            //    conn.Execute(@"
                            //        INSERT INTO TIRE_MILEAGE_HISTORY 
                            //     (date_created,tire_id,location_id,km, is_delete,userid)
                            //     values (@date_created,@tire_id,'A00S001',0,0,@userid)
                              
                                
                            //", new
                            //    {
                            //        p.date_created,
                            //        dt.tire_id,
                            //        unit_location_id,
                            //        p.userid
                            //    }, tran);
                                conn.Execute(@"INSERT INTO TIRE_UNIT
                                                (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                            VALUES
                                                (@vehicle_id,@serial_number,'',0,@date_created,@userid)"
                                   , new
                                   {
                                       dr.vehicle_id,
                                       dt.serial_number,
                                       p.date_created,
                                       p.userid
                                   }, tran);
                                axis = getAxisByNo(dt.axis_id, dr.axis_type);
                                if (axis == "") {
                                    tran.Rollback();
                                    returnRes.flag = false;
                                    returnRes.message = "Axis " + dt.axis_id + " in axis type " + dr.axis_type + " not found ";
                                    goto here;
                                } 
                                conn.Execute(@" INSERT INTO TIRE_AXIS
                                                (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                            VALUES
                                                (@vehicle_id,@serial_number,@axis,@date_created,'',0, @userid)
                                        UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @serial_number
                                        "
                                    , new
                                    {
                                        p.userid,
                                        axis,
                                        dr.vehicle_id,
                                        unit_location_id,
                                        p.date_created,
                                        dt.serial_number,
                                    }, tran);
                                conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@serial_number,0,@userid)", new
                                {
                                    yearmonth,
                                    unit_location_id,
                                    p.date_created,
                                    dt.serial_number,
                                    p.userid
                                }, tran);
                                conn.Execute(@"
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,'A00S001',@serial_number,0,@userid)", new
                                {
                                    yearmonth,
                                    p.date_created,
                                    dt.serial_number,
                                    p.userid
                                }, tran);

                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@serial_number,@rfid,'102','XXX','XXX',@unit_location_id,0,@date_created,@userid)", new
                                {
                                    dt.serial_number,
                                    p.date_created,
                                    dt.rfid,
                                    unit_location_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@serial_number
                                    END 
                                ", new
                                {
                                    dt.serial_number,
                                    p.userid,
                                    p.date_created
                                }, tran);
                                
                            }
                               
                        }
                        

                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
               
                conn.Close();
            }



            here:


            if (returnRes.flag == true)
            {
                returnRes.message = "Utility stock in unit(fleet) success.";
                returnRes.status = "success";
            }
            else
            {
                
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertUtilityTireStokInUnitPerVehicle(cCrudUtilityTireStokInUnitPerVehicle p)
        {
            List<cCrudUtilityTireStokInUnitPerVehicleDetailTire> ListTire = new List<cCrudUtilityTireStokInUnitPerVehicleDetailTire>();

            ListTire = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInUnitPerVehicleDetailTire>>(p.data);

            //List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire> ListTire = new List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>();

            //ListTire = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInUnitPerFleetDataVehicleDetailTire>>(ListVehicle.);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "";
            string axis = "";
            string unit_location_id = Master.getDefaultUnitLocationId();
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try 
                    {
                        conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE WHERE vehicle_id = @vehicle_id))
                                        BEGIN
                                            INSERT INTO VEHICLE
                                                (vehicle_id,brand_vhc_id,vehicle_name,date_created,axis_type, vehicle_type_id,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,@brand_vhc_id,@vehicle_name,@date_created,@axis_type, @vehicle_type_id,'False',@userid)
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE VEHICLE SET vehicle_name = @vehicle_name, brand_vhc_id=@brand_vhc_id, axis_type = @axis_type, date_modified=@date_created, userid=@userid
                                            WHERE vehicle_id = @vehicle_id
                                        END"
                            , new
                            {
                                p.date_created,
                                p.vehicle_id,
                                p.vehicle_name,
                                p.axis_type,
                                p.brand_vhc_id,
                                p.vehicle_type_id,
                                p.userid

                            }, tran);
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_vhc_no from REGISTER_VHC where register_vhc_no=[dbo].[NextRegisterNoVehicle](@yearmonth) and vehicle_id=@vehicle_id) ) 
                                BEGIN 
                                    INSERT INTO REGISTER_VHC
                                 (register_vhc_no,date_created,vehicle_id,location_id,userid,reg_rfid,is_delete)
                                 values ([dbo].[NextRegisterNoVehicle](@yearmonth),@date_created,@vehicle_id,'A00S001',@userid,@rfid,0)
                                END 
                                
                            ", new
                        {
                            yearmonth,
                            p.date_created,
                            p.vehicle_id,
                            p.userid,
                            p.rfid
                        }, tran);
                        foreach (var dr in ListTire)
                        {
                            conn.Execute(@"
                                           IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                            insert into tire (tire_id, location_id, tire_code, tire_name, serial_number,
                                            size, pattern, km, rfid, status_id, is_delete, date_created, userid)
                                            values
                                            (@serial_number, @unit_location_id, @tire_code, @tire_name, @serial_number,
                                            @size, @pattern, 0, @rfid, '1', 0, @date_created, @userid)

                                            insert into epc_tire (epc_rfid, tire_id, is_delete, date_created, date_modified, userid)
                                             values
                                            (@rfid,@serial_number,0,@date_created,@date_created, @userid) END

", new
                            {
                                dr.serial_number,
                                dr.tire_type_id,
                                dr.tire_code,
                                unit_location_id,
                                dr.tire_name,
                                dr.size,
                                dr.pattern,
                                dr.rfid,
                                p.date_created,
                                p.userid
                            }, tran);
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,'A00S001',@serial_number,0,@userid) END", new
                            {
                                yearmonth,
                                p.date_created,
                                dr.serial_number,
                                unit_location_id,
                                p.userid
                            }, tran);
                            conn.Execute(@"
IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@serial_number,@rfid,'005','XXX','XXX','A00S001',0,@date_created,@userid) END", new
                            {
                                dr.serial_number,
                                p.date_created,
                                dr.rfid,
                                unit_location_id,
                                p.userid
                            }, tran);


                            conn.Execute(@" IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=[dbo].[NextRegisterNoTire](@yearmonth) and tire_id=@serial_number) ) 
                                                    BEGIN 
                                                        INSERT INTO REGISTER_TIRE
                                                     (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                                                     values ( [dbo].[NextRegisterNoTire](@yearmonth),@date_created,@serial_number,'A00S001',@userid,@rfid,0)
                                                    END 
                                END
                                                ", new
                            {
                                yearmonth,
                                p.date_created,
                                dr.serial_number,
                                unit_location_id,
                                p.userid,
                                dr.rfid
                            }, tran);

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                        INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                            values
                                            (@serial_number,@rfid,'110', 'XXX','XXX', 'A00S001',0, @date_created, @userid)
                                    END
                                ", new
                            {
                                p.date_created,
                                dr.serial_number,
                                unit_location_id,
                                dr.rfid,
                                p.userid

                            }, tran);

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                    INSERT INTO TIRE_MILEAGE_HISTORY 
                                 (date_created,tire_id,location_id,km, is_delete,userid)
                                 values (@date_created,@serial_number,'A00S001',0,0,@userid)
                              
                                END
                            ", new
                            {
                                p.date_created,
                                dr.serial_number,
                                unit_location_id,
                                p.userid
                            }, tran);
                            conn.Execute(@"INSERT INTO TIRE_UNIT
                                                (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                            VALUES
                                                (@vehicle_id,@serial_number,NULL,0,@date_created,@userid)"
                               , new
                               {
                                   p.vehicle_id,
                                   dr.serial_number,
                                   p.date_created,
                                   p.userid
                               }, tran);
                            axis = getAxisByNo(dr.axis_id, p.axis_type);
                            if (axis == "")
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "Axis " + dr.axis_id + " in axis type " + p.axis_type + " not found ";
                                goto here;
                            }
                            conn.Execute(@" INSERT INTO TIRE_AXIS
                                                (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                            VALUES
                                                (@vehicle_id,@serial_number,@axis,@date_created,NULL,0, @userid)
                                        UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @serial_number
                                        "
                                , new
                                {
                                    p.userid,
                                    axis,
                                    p.vehicle_id,
                                    unit_location_id,
                                    p.date_created,
                                    dr.serial_number,
                                }, tran);
                            conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@unit_location_id,@serial_number,0,@userid)", new
                            {
                                yearmonth,
                                unit_location_id,
                                p.date_created,
                                dr.serial_number,
                                p.userid
                            }, tran);
                            conn.Execute(@"
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,'A00S001',@serial_number,0,@userid)", new
                            {
                                yearmonth,
                                p.date_created,
                                dr.serial_number,
                                p.userid
                            }, tran);

                            conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@serial_number,@rfid,'102','XXX','XXX',@unit_location_id,0,@date_created,@userid)", new
                            {
                                dr.serial_number,
                                p.date_created,
                                p.rfid,
                                unit_location_id,
                                p.userid
                            }, tran);
                            conn.Execute(@" 
                                    BEGIN 
                                        update TIRE set status_id='1', date_modified=@date_created, userid=@userid where tire_id=@serial_number
                                    END 
                                ", new
                            {
                                dr.serial_number,
                                p.userid,
                                p.date_created
                            }, tran);

                        }




                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }




            here:

            if (returnRes.flag == true)
            {
                returnRes.message = "Utility stock in unit(vehicle) success.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertUtilityTireStokInLocation(cCrudUtilityTireStokInLocation p)
        {
            List<cCrudUtilityTireStokInLocationDetail> ListDetail = new List<cCrudUtilityTireStokInLocationDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudUtilityTireStokInLocationDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DateTime date_created_plus_ =   DateTime.Now;//DateTime.ParseExact(p.date_created, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                        // Tambahkan 30 detik
                        date_created_plus_ = date_created_plus_.AddSeconds(5);
                        string date_created_plus = date_created_plus_.ToString("yyyy-MM-dd HH:mm:ss").Replace(".", ":");
                        foreach (var dr in ListDetail)
                        {
                            conn.Execute(@"
                                             IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number) ) 
                                            BEGIN 
                                                insert into tire (tire_id, location_id, tire_code, tire_name, serial_number,
                                                size, pattern, km, rfid, status_id, is_delete, date_created, date_modified, userid)
                                                values
                                                (@serial_number, @location_id, @tire_code, @tire_name, @serial_number,
                                                @size, @pattern, 0, @rfid, @status_id, 0, @date_created, @date_created, @userid)
                                            
                                                insert into epc_tire (epc_rfid, tire_id, is_delete, date_created, date_modified, userid)
                                                 values
                                                ('0',@serial_number,0,@date_created,@date_created, @userid)

                                                insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, date_modified, userid)
                                               values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@serial_number,0,@date_created,@userid)


                                                insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, date_modified, userid)
                                                values (@serial_number,@rfid,'004','XXX','XXX',@location_id,0,@date_created,@date_created,@userid)

                                                IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=[dbo].[NextRegisterNoTire](@yearmonth) and tire_id=@serial_number) ) 
                                                    BEGIN 
                                                        INSERT INTO REGISTER_TIRE
                                                     (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid, date_modified,is_delete)
                                                     values ( [dbo].[NextRegisterNoTire](@yearmonth),@date_created,@serial_number,@location_id,@userid,@rfid,@date_created,0)
                                                    END 

                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, date_modified, userid)
                                                values
                                                (@serial_number,@rfid,'110', 'XXX','XXX', @location_id,0, @date_created,@date_created, @userid)
                                                   
                                                 INSERT INTO TIRE_MILEAGE_HISTORY 
                                                 (date_created,tire_id,location_id,km, is_delete, date_modified,userid)
                                                 values (@date_created,@serial_number,@location_id,0,0,@date_created,@userid)
                              
                                            END

                                            
                            ", new
                            {
                                dr.serial_number,
                                p.location_id,
                                dr.tire_type_id,
                                dr.tire_code,
                                dr.tire_name,
                                dr.size,
                                dr.pattern,
                                dr.rfid,
                                dr.status_id,
                                p.date_created,
                                p.userid,
                                yearmonth
                            }, tran);

                            if (dr.status_id == "4")
                            {
                                //string scrap_location_id = getDefaultScrapLocationId();
                                try
                                {
                                    //string no_scrap = getNextScrapNoTire(yearmonth);
                                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 scrap_no from SCRAP_TIRE where scrap_no=[dbo].[NextScrapNo](@yearmonth) and date_created=@date_created_plus and tire_id=@serial_number) ) 
                                        BEGIN 
                                            INSERT INTO SCRAP_TIRE
                                         (scrap_no,date_created,tire_id, reason_id, area_damage_id,description,is_delete,date_modified,userid)
                                         values ([dbo].[NextScrapNo](@yearmonth),@date_created_plus,@serial_number, '0','0' ,'Direct scrap from utility tyre for location',0,@date_created_plus,@userid)
                                        END 
                                
                                    ", new
                                    {
                                        yearmonth,
                                        date_created_plus,
                                        dr.serial_number,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@"
                                            INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id , location_id,is_delete, date_created, userid)
                                            values
                                            (@serial_number,'0','107', 'XXX','XXX', @location_id, 0, @date_created_plus, @userid)
                                            update TIRE set status_id='4', date_modified=@date_created_plus, userid=@userid where tire_id=@serial_number
                                    ", new
                                    {
                                        p.location_id,
                                        date_created_plus,
                                        dr.serial_number,
                                        p.userid
                                    }, tran);
                                }
                                catch (Exception e)
                                {
                                    tran.Rollback();
                                    returnRes.flag = false;
                                    returnRes.message = "# Failed, " + e.Message.ToString();
                                }
                            }
                    //          conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=[dbo].[NextRegisterNoTire](@yearmonth) and tire_id=@tire_id) ) 
                    //                            BEGIN 
                    //                                INSERT INTO REGISTER_TIRE
                    //                             (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                    //                             values ( [dbo].[NextRegisterNoTire](@yearmonth),@date_created,@tire_id,@location_id,@userid,@rfid,0)
                    //                            END 

                    //                        ", new
                    //                            {
                    //                                yearmonth,
                    //                                p.date_created,
                    //                                dr.tire_id,
                    //                                p.location_id,
                    //                                // p.trailer_id,
                    //                                p.userid,
                    //                                dr.rfid
                    //}, tran);

                    //conn.Execute(@"
                    //        INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                    //            values
                    //            (@tire_id,@rfid,'110', 'XXX','XXX', @location_id,0, @date_created, @userid)

                    //", new
                    //{
                    //    p.date_created,
                    //    dr.tire_id,
                    //    dr.rfid,
                    //    p.location_id,
                    //    p.userid

                    //}, tran);

                    //    conn.Execute(@"
                    //        INSERT INTO TIRE_MILEAGE_HISTORY 
                    //     (date_created,tire_id,location_id,km, is_delete,userid)
                    //     values (@date_created,@tire_id,@location_id,0,0,@userid)


                    //", new
                    //    {
                    //        p.date_created,
                    //        dr.tire_id,
                    //        p.location_id,
                    //        p.userid
                    //    }, tran);
                }




                        tran.Commit();
                        //





                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }






            if (returnRes.flag == true)
            {
                returnRes.message = "Utility stock in location success.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertInbound(cCrudInbound p)
        {
            List<cCrudInboundDetail> ListDetail = new List<cCrudInboundDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudInboundDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string inbound_id = "";
            int RowAffected = 0;
            string description = "Inbound";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            
            if (p.inbound_id == "0" || p.inbound_id == "")
            {
                inbound_id = getNextInboundId(yearmonth);
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND where inbound_id=@inbound_id) ) 
                                        BEGIN 
                                            INSERT INTO INBOUND 
                                         (inbound_id,date_created,location_id,is_delete,userid)
                                         values (@inbound_id,@date_created,@location_id,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                inbound_id,
                                p.date_created,
                                p.location_id,
                                p.userid
                            }, tran);
                            foreach (var dr in ListDetail)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"
                                        update OUTBOUND_DETAIL set is_active= 0, date_modified = @date_created, userid = @userid where tire_id = @tire_id
                                        IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND_DETAIL where inbound_id=@inbound_id and date_created=@date_created and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO INBOUND_DETAIL 
                                         (inbound_id,date_created,date_modified,tire_id,is_delete,userid)
                                         values (@inbound_id,@date_created,@date_created,@tire_id,0,@userid)

                                            UPDATE TIRE set location_id=@location_id,date_modified=@date_modified where tire_id=@tire_id
                                        END 
                                        
                                 
                                    ", new
                                {
                                    inbound_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.location_id,
                                    date_modified,
                                    p.userid
                                }, tran);
                                //IF(EXISTS(select tire_id from tire where tire_id = @tire_id and status_id = '3'))
                                //        BEGIN
                                //            update TIRE set status_id = '1', date_modified = @date_modified, userid = @userid where tire_id = @tire_id
                                //        END
                                conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@tire_id,@current_epc_tire,'104','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                {
                                    dr.tire_id,
                                    p.date_created,
                                    current_epc_tire,
                                    p.location_id,
                                    p.userid
                                }, tran);
                            }




                            tran.Commit();
                            //





                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                returnRes.flag = false;
                //no_scrap = p.scrap_no;
            }






            if (returnRes.flag == true)
            {
                returnRes.message = "Inbound succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertOutbound(cCrudOutbound p)
        {
            List<cCrudOutboundDetail> ListDetail = new List<cCrudOutboundDetail>();

            ListDetail = JsonConvert.DeserializeObject<List<cCrudOutboundDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string outbound_id = "";
            int RowAffected = 0;
            string description = "Outbound";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string tire_registered = "";
            int tire_registered_count = 0;
            if (p.location_id == "A00S002" || p.location_id == "A00S001")
            {//VALIDASI JIKA LOKASI PENGIRIM DARI STORE KE WORKSHOP, DIPERIKSA REGISTRASINYA. NOTE: OUTBOUND DARI STORE OTOMATIS LOKASI PENERIMANYA ADALAH WORKSHOP. KODE LOKASI STORE: A00S002
                foreach (var dr in ListDetail)
                {
                    if(checkingTyreRegister(dr.tire_id) == false)
                    {
                        tire_registered += dr.tire_id + ",";
                        tire_registered_count++;
                    }
                }
            }
            if (tire_registered_count != 0) {
                returnRes.flag = false;
                returnRes.message = "# Failed, " + tire_registered_count + " unregistered tyre(s). Serial Number:" + tire_registered.TrimEnd(',');
            }
            else
            {
                if (p.outbound_id == "0" || p.outbound_id == "")
                {
                    outbound_id = getNextOutboundId(yearmonth);
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 outbound_id from OUTBOUND where outbound_id=@outbound_id) ) 
                                            BEGIN 
                                                INSERT INTO OUTBOUND 
                                             (outbound_id,date_created,location_id,is_delete,userid)
                                             values (@outbound_id,@date_created,@location_id,0,@userid)
                                            END 
                                
                                        ", new
                                {
                                    outbound_id,
                                    p.date_created,
                                    p.location_id,
                                    p.description,
                                    p.userid
                                }, tran);
                                foreach (var dr in ListDetail)
                                {
                                    string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                    conn.Execute(@"
                                            update OUTBOUND_DETAIL set is_active= 0, date_modified = @date_created, userid = @userid where tire_id = @tire_id
                                            IF (NOT EXISTS (select TOP 1 outbound_id from OUTBOUND_DETAIL where outbound_id=@outbound_id and date_created=@date_created and tire_id=@tire_id) ) 
                                            BEGIN 
                                                INSERT INTO OUTBOUND_DETAIL 
                                             (outbound_id,date_created,tire_id,location_id,is_active,is_delete,userid)
                                             values (@outbound_id,@date_created,@tire_id,@location_id,1,0,@userid)

                                                UPDATE TIRE set location_id='XXX',date_modified=@date_modified where tire_id=@tire_id
                                            END 
                                
                                        ", new
                                    {
                                        outbound_id,
                                        p.date_created,
                                        dr.tire_id,
                                        dr.description,
                                        dr.location_id,
                                        date_modified,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@"
                                                insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                               values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                    {
                                        yearmonth,
                                        p.location_id,
                                        p.date_created,
                                        dr.tire_id,
                                        p.userid
                                    }, tran);
                                    conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id,location_id , is_delete, date_created, userid)
                                             values (@tire_id,@current_epc_tire,'105','XXX','XXX',@location_id,0,@date_created,@userid)", new
                                    {
                                        dr.tire_id,
                                        p.date_created,
                                        current_epc_tire,
                                        p.location_id,
                                        p.userid
                                    }, tran);
                                }




                                tran.Commit();
                                //





                            }
                            catch (Exception e)
                            {
                                tran.Rollback();
                                returnRes.flag = false;
                                returnRes.message = "# Failed, " + e.Message.ToString();
                            }
                        }
                        conn.Close();
                    }
                }
                else
                {
                    returnRes.flag = false;
                    //no_scrap = p.scrap_no;
                }
            }
            






            if (returnRes.flag == true)
            {
                returnRes.message = "Outbound succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static string getNextScrapNoTire(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select scrap_no=[dbo].[NextScrapNo](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["scrap_no"]).FirstOrDefault();
            }
            return no;
        }

        public static bool cekDataScrapObservation()
        {
            bool no = false;
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select inspect_setup_id from inspect_observation where inspect_setup_id='8' ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = true;
            }
            return no;
        }
        public static string getNextTireChangeId(string yearmonth)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select [dbo].[GetNextTireChangeId](@yearmonth) as data ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                        select (string)dr["data"]).FirstOrDefault();
            }
            return data;
        }


        public static string getTschIdByUser(string userid, string date)
        {
            string data = "";

            DateTime date_created = DateTime.Parse(date);
            DateTime date_modified = DateTime.Now;
            string date_ = date_created.ToString("yyyy-MM-dd");
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = @"select top 1 te.group_id as data from GROUPS_EMPLOYEES te
                                inner join USER_EMPLOYEES ue on ue.emp_id = te.emp_id
								inner join schedules sch on sch.group_id = te.group_id
                                where ue.userid=@userid and sch.date=@date
								
								order by te.date_created desc ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", date_);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                        select (string)dr["data"]).FirstOrDefault();
            }
            return data;
        }
        public static string[] getDataScheduleByUser(string userid)
        {
            string data = "";
            string[] data_arr = new string[6];
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
            string date = date_created.ToString("yyyy-MM-dd");
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = @"select top 1 sch.year, sch.month, sch.date, sch.location_id, sch.group_id, sch.shift_id from GROUPS_EMPLOYEES te
                                inner join USER_EMPLOYEES ue on ue.emp_id = te.emp_id
								inner join schedules sch on sch.group_id = te.group_id
                                where ue.userid=@userid and sch.date=@date
								
								order by te.date_created desc ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@date", date);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data_arr[0] = dtTempTable.Rows[0].ItemArray[0].ToString();
                data_arr[1] = dtTempTable.Rows[0].ItemArray[1].ToString();
                data_arr[2] = Convert.ToDateTime(dtTempTable.Rows[0].ItemArray[2].ToString()).ToString("yyyy-MM-dd");
                data_arr[3] = dtTempTable.Rows[0].ItemArray[3].ToString();
                data_arr[4] = dtTempTable.Rows[0].ItemArray[4].ToString();
                data_arr[5] = dtTempTable.Rows[0].ItemArray[5].ToString();
                //dtTempTable.Rows[i].ItemArray[0].ToString();
            }
            return data_arr;
        }
        public static string getNextVhcChangeNo(string yearmonth)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select [dbo].[GetNextVhcChangeNo](@yearmonth) as data ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["data"]).FirstOrDefault();
            }
            return data;
        }
        public static string getDefaultScrapLocationId()
        {
            string location_id = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select sys_value from SYS_SETTINGS where sys_key='default_scrap_location_id' ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@year", year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                location_id = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["sys_value"]).FirstOrDefault();
            }
            else
            {
                location_id = "A00S001";
            }
            return location_id;
        }
        public static string[] cekDataTireUtility(string tire_id)
        {
            string[] data = new string[2];
           
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select top 1 tire_id, location_id from TIRE where tire_id=@tire_id ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@year", year);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                data[0] = dtTempTable.Rows[0].ItemArray[0].ToString();
                data[1] = dtTempTable.Rows[0].ItemArray[1].ToString();
            }
            return data;
        }
        public static cRespon InsertScrapProcess(cCrudScrap p)
        {
            List<cCrudScrapTireDetail> ListScrapTire = new List<cCrudScrapTireDetail>();
            
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_scrap = "";
            int RowAffected = 0;
            string location_id = getDefaultScrapLocationId();
            string description = "Scrap Tire";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            if (p.scrap_no == "0" || p.scrap_no == "")
            {
                no_scrap = getNextScrapNoTire(yearmonth);
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            string current_epc_tire = getEPCCurrentTire(p.tire_id);
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 scrap_no from SCRAP_TIRE where scrap_no=@no_scrap and date_created=@date_created and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO SCRAP_TIRE 
                                         (scrap_no,date_created,tire_id, reason_id, area_damage_id,description,is_delete,userid)
                                         values (@no_scrap,@date_created,@tire_id, @reason_id,@area_damage_id ,@description,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                no_scrap,
                                p.date_created,
                                p.tire_id,
                                p.description,
                                p.reason_id,
                                p.area_damage_id,
                                p.userid
                            }, tran);
                            conn.Execute(@"
                                            INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id , location_id,is_delete, date_created, userid)
                                            values
                                            (@tire_id,@current_epc_tire,'107', 'XXX','XXX', @location_id, 0, @date_created, @userid)
                                            update TIRE set status_id='4', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                    ", new
                            {
                                p.date_created,
                                p.tire_id,
                                current_epc_tire,
                                location_id,
                                p.userid
                            }, tran);
                           




                            tran.Commit();
                            //





                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                returnRes.flag = false;
                //no_scrap = p.scrap_no;
            }

            




            if (returnRes.flag == true)
            {
                returnRes.message = "Scrap success. Serial Number: " + p.tire_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }


        public static string getNextDisposalNoTire(string yearmonth)
        {
            string no = "";
            DataTable dtTempTable = new DataTable("SCRAP");
            string StrQuery = "select no=[dbo].[NextDisposalNo](@yearmonth) ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@yearmonth", yearmonth);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                no = (from DataRow dr in dtTempTable.Rows
                      select (string)dr["no"]).FirstOrDefault();
            }
            return no;
        }

        public static cRespon InsertDisposalProcess(cCrudDisposal p)
        {
            List<cCrudDisposalTireDetail> ListDisposalTire = new List<cCrudDisposalTireDetail>();

            ListDisposalTire = JsonConvert.DeserializeObject<List<cCrudDisposalTireDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_disposal = "";
            
            int RowAffected = 0;
            string description = "Scrap Tire";
            //TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_modified = DateTime.Now;

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            if (p.disposal_no == "0" || p.disposal_no == "")
            {
                no_disposal = getNextDisposalNoTire(yearmonth);
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 disposal_no from DISPOSAL where disposal_no=@no_disposal) ) 
                                        BEGIN 
                                            INSERT INTO DISPOSAL 
                                         (disposal_no,date_created,location_id,description,date_modified,is_delete,userid)
                                         values (@no_disposal,@date_created,@location_id,@description,@date_created,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                no_disposal,
                                p.date_created,
                                p.description,
                                p.location_id,
                                p.userid
                            }, tran);
                            foreach (var dr in ListDisposalTire)
                            {
                                string current_epc_tire = getEPCCurrentTire(dr.tire_id);
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 disposal_no from DISPOSAL_DETAIL where disposal_no=@no_disposal and date_created=@date_created and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO DISPOSAL_DETAIL 
                                         (disposal_no,date_created,tire_id,description,date_modified,is_delete,userid)
                                         values (@no_disposal,@date_created,@tire_id,@description,@date_created,0,@userid)
                                        END 
                                         
                                    ", new
                                {
                                    no_disposal,
                                    p.date_created,
                                    dr.tire_id,
                                    dr.description,
                                    p.userid
                                }, tran);
                                conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created, userid)
                                         values (@tire_id,@current_epc_tire,'109','XXX','XXX',@location_id,0,@date_created,@userid)
                                        update TIRE set status_id='6', is_delete=1, date_modified=@date_created, userid=@userid where tire_id=@tire_id    
                                        ", new
                                {
                                    dr.tire_id,
                                    current_epc_tire,
                                    p.date_created,
                                    p.location_id,
                                    p.userid
                                }, tran);
                                conn.Execute(@"
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id,@tire_id,0,@userid)", new
                                {
                                    yearmonth,
                                    p.location_id,
                                    p.date_created,
                                    dr.tire_id,
                                    p.userid
                                }, tran);

                            }




                            tran.Commit();
                            //





                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                    conn.Close();
                }
            }
            else
            {
                returnRes.flag = false;
                //no_scrap = p.scrap_no;
            }






            if (returnRes.flag == true)
            {
                returnRes.message = "Disposal succesfully. No Disposal: " + no_disposal;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertFleetOdometer(cOdometer p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;


            DateTime date = date_created;//date_created.ToString("yyyy-MM-dd");
            double total_km = 0;
            double total_hm = 0;
            double km_before = 0;
            double hm_before = 0;
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            var conn_tire = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            //using (SqlTransaction tran = conn.BeginTransaction())
            //{
            try
            {
                
                DataTable dtTempTable = new DataTable("ODOMETER");
                string StrQuery = @"select top 1 * from ODOMETER where fleet_id=@fleet_id and is_delete=0 order by date desc" + System.Environment.NewLine;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = StrQuery;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@fleet_id", p.fleet_id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.CommandTimeout = 0;
                con.Open();
                da.Fill(dtTempTable);
                con.Close();
                if (dtTempTable.Rows.Count > 0)
                {
                    km_before = (from DataRow dr in dtTempTable.Rows
                                    select (double)dr["km"]).FirstOrDefault();
                    hm_before = (from DataRow dr in dtTempTable.Rows
                                    select (double)dr["hm"]).FirstOrDefault();
                }
                total_km = double.Parse(p.km) - km_before;
                total_hm = double.Parse(p.hm) - hm_before;
                conn.Execute(@"
                                insert into odometer (date, fleet_id, km, hm, is_delete, date_created, date_modified, userid)
                                values
                                (@date, @fleet_id, @km, @hm, 0, @date_created, @date_modified, @userid)
                                
                                update fleet set km=@km, hm=@hm, date_modified=@date_modified, userid=@userid where fleet_id=@fleet_id
                                update vehicle set km=@km, hm=@hm, date_modified=@date_modified, userid=@userid where vehicle_id=@fleet_id
                        ", new
                {
                    date,
                    p.date_created,
                    p.fleet_id,
                    p.km,
                    p.hm,
                    date_modified,
                    p.userid
                }, tran);

                if (dtTempTable.Rows.Count > 0)
                {
                    DataTable dtTempTable2 = new DataTable("ODOMETER");
                    string StrQueryTireUnit = @"select tire_id from tire_unit tu
									inner join FLEET_VEHICLE fv on fv.vehicle_id= tu.vehicle_id
                                                    where fv.fleet_id=@fleet_id and tu.is_delete=0  and fv.is_delete=0 ";
                    cmd.CommandText = StrQueryTireUnit;
                    cmd.Connection = con;
                    //cmd.Parameters.AddWithValue("@fleet_id", p.fleet_id);

                    SqlDataAdapter da2 = new SqlDataAdapter(cmd);
                    da2.SelectCommand.CommandTimeout = 0;
                    con.Open();
                    da2.Fill(dtTempTable2);
                    con.Close();
                    string tire_idx = "";
                    if (dtTempTable2.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempTable2.Rows.Count; i++)
                        {
                            tire_idx = dtTempTable2.Rows[i].ItemArray[0].ToString();
                            DataTable dtTempTable3 = new DataTable("ODOMETER");
                            StrQueryTireUnit = @"select top 1 km from TIRE_MILEAGE_HISTORY where tire_id='" + tire_idx + "' and is_delete=0 order by date_created desc";
                            cmd.CommandText = StrQueryTireUnit;
                            cmd.Connection = con;
                            SqlDataAdapter da3 = new SqlDataAdapter(cmd);
                            da3.SelectCommand.CommandTimeout = 0;
                            con.Open();
                            da3.Fill(dtTempTable3);
                            con.Close();
                            double tire_km_before = 0;
                            if (dtTempTable3.Rows.Count == 0)
                            {
                                tire_km_before = 0;
                            }
                            else
                            {
                                tire_km_before = Convert.ToDouble(dtTempTable3.Rows[0].ItemArray[0].ToString());
                            }
                            double total_tire_km = total_km + tire_km_before;
                            conn.Execute(@"
                                    insert into TIRE_MILEAGE_HISTORY (tire_id, date_created, location_id, km, is_delete, date_modified, userid)
                                    values
                                    (@tire_idx, @date_created, @location_id, @total_tire_km, 0, @date_modified, @userid)
                                
                                    update TIRE set km=@total_tire_km, date_modified=@date_modified, userid=@userid where tire_id=@tire_idx
                            ", new
                            {
                                date,
                                total_tire_km,
                                p.date_created,
                                p.location_id,
                                p.km,
                                tire_idx,
                                date_modified,
                                p.userid
                            }, tran);
                        }
                        //tire_idx = dtTempTable2.Rows[0].ToString();
                        //km_before = (from DataRow dr in dtTempTable.Rows
                        //             select (double)dr["km"]).FirstOrDefault();
                        //hm_before = (from DataRow dr in dtTempTable.Rows
                        //             select (double)dr["hm"]).FirstOrDefault();
                    }

                    DataTable dtTempTable_vhc = new DataTable("ODOMETER");
                    string StrQueryVehicle = @"select vehicle_id from FLEET_VEHICLE fv
                                                    where fv.fleet_id=@fleet_id and fv.is_delete=0  and fv.vehicle_id not in (@fleet_id) ";
                    cmd.CommandText = StrQueryVehicle;
                    cmd.Connection = con;
                    //cmd.Parameters.AddWithValue("@fleet_id", p.fleet_id);

                    SqlDataAdapter da_vhc = new SqlDataAdapter(cmd);
                    da_vhc.SelectCommand.CommandTimeout = 0;
                    con.Open();
                    da_vhc.Fill(dtTempTable_vhc);
                    con.Close();
                    string vhc_idx = "";
                    if (dtTempTable_vhc.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTempTable_vhc.Rows.Count; i++)
                        {
                            vhc_idx = dtTempTable_vhc.Rows[i].ItemArray[0].ToString();
                            DataTable dtTempTable_vhc2 = new DataTable("ODOMETER");
                            StrQueryVehicle = @"select top 1 isnull(km,0) as km, isnull(hm,0) as hm from VEHICLE_KM_HISTORY where vehicle_id='" + vhc_idx + "' and is_delete=0 order by date_created desc";
                            cmd.CommandText = StrQueryVehicle;
                            cmd.Connection = con;
                            SqlDataAdapter da_vhc2 = new SqlDataAdapter(cmd);
                            da_vhc2.SelectCommand.CommandTimeout = 0;
                            con.Open();
                            da_vhc2.Fill(dtTempTable_vhc2);
                            con.Close();
                            double vehicle_km_before = 0;
                            double vehicle_hm_before = 0;
                            if (dtTempTable_vhc2.Rows.Count == 0)
                            {
                                vehicle_km_before = 0;
                                vehicle_hm_before = 0;
                            }
                            else
                            {
                                vehicle_km_before = Convert.ToDouble(dtTempTable_vhc2.Rows[0].ItemArray[0].ToString());
                                vehicle_hm_before = Convert.ToDouble(dtTempTable_vhc2.Rows[0].ItemArray[1].ToString());
                            }
                            double total_vhc_km = total_km + vehicle_km_before;
                            double total_vhc_hm = total_hm + vehicle_hm_before;
                            conn.Execute(@"
                                    insert into VEHICLE_KM_HISTORY (vehicle_id, date, date_created, location_id, km,hm, is_delete, date_modified, userid)
                                    values
                                    (@vhc_idx, @date_created, @date_created, @location_id, @total_vhc_km,@total_vhc_hm, 0, @date_modified, @userid)
                                
                                    update VEHICLE set km=@total_vhc_km, hm=@total_vhc_hm, date_modified=@date_modified, userid=@userid where vehicle_id=@vhc_idx
                            ", new
                            {
                                date,
                                total_vhc_km,
                                total_vhc_hm,
                                p.date_created,
                                p.location_id,
                                p.km,
                                vhc_idx,
                                date_modified,
                                p.userid
                            }, tran);
                        }
                    }
                    tran.Commit();
                    conn.Close();
                }
                else {
                    tran.Commit();
                    conn.Close();
                    returnRes.flag = true;
                }
                
            }
            catch (Exception e)
            {
                tran.Rollback();
                returnRes.flag = false;
                returnRes.message = "# Failed, " + e.Message.ToString();
            }


         

            if (returnRes.flag == true)
            {
                returnRes.message = "Insert odometer successfuly";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertPickupProcess(cCrudPickup p)
        {
            List<cCrudPickupDetail> ListTires = new List<cCrudPickupDetail>();
            ListTires = JsonConvert.DeserializeObject<List<cCrudPickupDetail>>(p.data);
           

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            string process_id = "120";
            string description = "Outbound";
            // TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");
            string pickup_no = getNextPickupNo(yearmonth);
            string vehicle_id = "0";
            string axis_id = "0";


            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 pickup_no from PICKUP where pickup_no=@pickup_no) ) 
                                        BEGIN 
                                            INSERT INTO PICKUP 
                                         (pickup_no,date_created,location_id,vehicle_no,driver,destination,notes,userid,outbound_no)
                                         values (@pickup_no,@date_created,@location_id,@vehicle_no,@driver,@destination,@notes,@userid,@outbound_no)
                                        END 
                                
                                    ", new
                        {
                            pickup_no,
                            p.date_created,
                            p.location_id,
                            p.driver,
                            p.destination,
                            p.vehicle_no,
                            p.notes,
                            p.userid,
                            p.outbound_no
                        }, tran);
                        
                        foreach (var dr in ListTires)
                        {
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from PICKUP_DETAIL where pickup_no=@pickup_no and tire_id=@tire_id) ) 
                                    BEGIN 
                                        INSERT INTO PICKUP_DETAIL 
                                        (pickup_no,date_created,tire_id)
                                        values (@pickup_no,@date_created,@tire_id)
                                    END 
                                
                                ", new
                            {
                                pickup_no,
                                p.date_created,
                                dr.tire_id
                                //dr.remark
                            }, tran);

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TRACKING where date_created=@date_created and tire_id=@tire_id and location_id=@location_id and process_id=@process_id and vehicle_id=@vehicle_id and axis_id=@axis_id  ) ) 
                                        BEGIN 
                                            INSERT INTO TRACKING 
                                         (date_created,tire_id,location_id,process_id,vehicle_id,axis_id,userid,rfid,description,remark)
                                         values (@date_created,@tire_id,@location_id,@process_id,@vehicle_id,@axis_id,@userid,@rfid,@description,@description)
                                        END 
                                    ", new
                            {
                                p.date_created,
                                dr.tire_id,
                                p.location_id,
                                process_id,
                                vehicle_id,
                                axis_id,
                                p.userid,
                                dr.rfid,
                                description

                            }, tran);

                            conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                        BEGIN 
                                            UPDATE TIRE set location_id=@location_id,process_id=@process_id,date_modified=@date_created where tire_id=@tire_id
                                        END 
                                
                                    ", new
                            {
                                p.date_created,
                                dr.tire_id,
                                p.location_id,
                                process_id
                            }, tran);
                        }
                        
                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Pickup succesfully. Pickup No: " + pickup_no;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            //}
            return returnRes;
        }

        public static string getNextPickupNo(string year)
        {
            string pickup_no = "";
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select pickup_no=[dbo].[GetNextPickupNumber](@year) ";
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
            if (dtTempTable.Rows.Count > 0)
            {
                pickup_no = (from DataRow dr in dtTempTable.Rows
                           select (string)dr["pickup_no"]).FirstOrDefault();
            }
            return pickup_no;
        }

        public class cCrudRelocation
        {
            
            public DateTime date_created { get; set; }
            public string location_id { get; set; }
            public string notes { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
        }

        public class cCrudRelocationDetail
        {
            public string tire_id { get; set; }
            public string vehicle_id { get; set; }
            public string axis_id { get; set; }
            public string location_before { get; set; }
            public string rfid { get; set; }
        }

        public static cRespon InsertRelocationProcess(cCrudRelocation p)
        {
            List<cCrudRelocationDetail> ListTires = new List<cCrudRelocationDetail>();
            ListTires = JsonConvert.DeserializeObject<List<cCrudRelocationDetail>>(p.data);


            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            //int RowAffected = 0;
            string process_id = "300";
            string description = "Relocation";
            // TimeSpan Tnow = DateTime.Now.TimeOfDay;
            //string yearmonth = p.date_created.ToString("yyyyMM");
            //string pickup_no = getNextPickupNo(yearmonth);
            //string vehicle_id = "0";
            //string axis_id = "0";


            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListTires)
                        {
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TRACKING where date_created=@date_created and tire_id=@tire_id and location_id=@location_id and process_id=@process_id and vehicle_id=@vehicle_id and axis_id=@axis_id  ) ) 
                                        BEGIN 
                                            INSERT INTO TRACKING 
                                         (date_created,tire_id,location_id,process_id,vehicle_id,axis_id,userid,rfid,description,remark)
                                         values (@date_created,@tire_id,@location_id,@process_id,@vehicle_id,@axis_id,@userid,@rfid,@description,@description)
                                        END 
                                    ", new
                            {
                                p.date_created,
                                dr.tire_id,
                                p.location_id,
                                process_id,
                                dr.vehicle_id,
                                dr.axis_id,
                                p.userid,
                                dr.rfid,
                                description

                            }, tran);

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from RELOCATION where date_created=@date_created and tire_id=@tire_id and location_id=@location_id and vehicle_id=@vehicle_id and axis_id=@axis_id) ) 
                                    BEGIN 
                                        INSERT INTO RELOCATION 
                                        (date_created,tire_id,location_id,vehicle_id,axis_id,location_before,notes,userid)
                                        values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@location_before,@notes,@userid)
                                    END 
                                
                                ", new
                            {
                                p.date_created,
                                dr.tire_id,
                                p.location_id,
                                dr.vehicle_id,
                                dr.axis_id,
                                dr.location_before,
                                p.notes,
                                p.userid
                            }, tran);

                            

                            conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                        BEGIN 
                                            UPDATE TIRE set location_id=@location_id,process_id=@process_id,date_modified=@date_created where tire_id=@tire_id
                                        END 
                                
                                    ", new
                            {
                                p.date_created,
                                dr.tire_id,
                                p.location_id,
                                process_id
                            }, tran);
                        }

                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Relocation succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            //}
            return returnRes;
        }

        //public class cCrudInbound
        //{

        //    public DateTime date_created { get; set; }
        //    public string location_id { get; set; }
        //    public string notes { get; set; }
        //    public string userid { get; set; }
        //    public string inbound_no { get; set; }
        //    public string reference_no { get; set; }
        //    public string data { get; set; }
        //}

        //public class cCrudInboundDetail
        //{
        //    public string tire_id { get; set; }
        //    public string vehicle_id { get; set; }
        //    public string axis_id { get; set; }
        //    public string rfid { get; set; }
        //}

        //public static cRespon InsertInboundProcess(cCrudInbound p)
        //{
        //    List<cCrudInboundDetail> ListTires = new List<cCrudInboundDetail>();
        //    ListTires = JsonConvert.DeserializeObject<List<cCrudInboundDetail>>(p.data);


        //    cRespon returnRes = new cRespon();
        //    returnRes.flag = true;
        //    returnRes.status = "";
        //    returnRes.message = "";
        //    //int RowAffected = 0;
        //    string process_id = "200";
        //    string description = "Inbound";
        //    // TimeSpan Tnow = DateTime.Now.TimeOfDay;
        //    string yearmonth = p.date_created.ToString("yyyyMM");
        //    string inbound_no = getNextInboundNo(yearmonth);
        //    //string vehicle_id = "0";
        //    //string axis_id = "0";


        //    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
        //    {
        //        conn.Open();
        //        using (SqlTransaction tran = conn.BeginTransaction())
        //        {
        //            try
        //            {

        //                foreach (var dr in ListTires)
        //                {
        //                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TRACKING where date_created=@date_created and tire_id=@tire_id and location_id=@location_id and process_id=@process_id and vehicle_id=@vehicle_id and axis_id=@axis_id  ) ) 
        //                                BEGIN 
        //                                    INSERT INTO TRACKING 
        //                                 (date_created,tire_id,location_id,process_id,vehicle_id,axis_id,userid,rfid,description,remark)
        //                                 values (@date_created,@tire_id,@location_id,@process_id,@vehicle_id,@axis_id,@userid,@rfid,@description,@description)
        //                                END 
        //                            ", new
        //                    {
        //                        p.date_created,
        //                        dr.tire_id,
        //                        p.location_id,
        //                        process_id,
        //                        dr.vehicle_id,
        //                        dr.axis_id,
        //                        p.userid,
        //                        dr.rfid,
        //                        description

        //                    }, tran);

        //                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from INBOUND where inbound_no=@inbound_no and date_created=@date_created and tire_id=@tire_id and  location_id=@location_id and vehicle_id=@vehicle_id and axis_id=@axis_id) ) 
        //                            BEGIN 
        //                                INSERT INTO INBOUND 
        //                                (inbound_no,date_created,tire_id,location_id,vehicle_id,axis_id,notes,userid,reference_no)
        //                                values (@inbound_no,@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@notes,@userid,@reference_no)
        //                            END 
                                
        //                        ", new
        //                    {
        //                        inbound_no,
        //                        p.date_created,
        //                        dr.tire_id,
        //                        p.location_id,
        //                        dr.vehicle_id,
        //                        dr.axis_id,
        //                        p.notes,
        //                        p.userid,
        //                        p.reference_no
        //                    }, tran);



        //                    conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
        //                                BEGIN 
        //                                    UPDATE TIRE set location_id=@location_id,process_id=@process_id,date_modified=@date_created where tire_id=@tire_id
        //                                END 
                                
        //                            ", new
        //                    {
        //                        p.date_created,
        //                        dr.tire_id,
        //                        p.location_id,
        //                        process_id
        //                    }, tran);
        //                }

        //                tran.Commit();

        //            }
        //            catch (Exception e)
        //            {
        //                tran.Rollback();
        //                returnRes.flag = false;
        //                returnRes.message = "# Failed, " + e.Message.ToString();
        //            }
        //        }
        //        conn.Close();
        //    }

        //    if (returnRes.flag == true)
        //    {
        //        returnRes.message = "Inbound succesfully.";
        //        returnRes.status = "success";
        //    }
        //    else
        //    {
        //        returnRes.status = "failed";
        //    }
        //    //}
        //    return returnRes;
        //}

        public static string getNextInboundNo(string year)
        {
            string inbound_no = "";
            DataTable dtTempTable = new DataTable("INBOUND");
            string StrQuery = "select inbound_no=[dbo].[GetNextInboundNo](@year) ";
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
            if (dtTempTable.Rows.Count > 0)
            {
                inbound_no = (from DataRow dr in dtTempTable.Rows
                             select (string)dr["inbound_no"]).FirstOrDefault();
            }
            return inbound_no;
        }


        #region tire_mileage
        public class tireMileage
        {
            public DateTime date_created { get; set; }
            public string location_id { get; set; }
            public string vehicle_id { get; set; }
            public string vehicle_mileage { get; set; }
            public string userid { get; set; }
        }


        public static cRespon InsertTireMileage(tireMileage p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string process_id = "500";
            string description = "Update Tire Mileage";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"UPDATE TIRE SET tire_mileage = tire_mileage + (@vehicle_mileage-(SELECT vehicle_mileage FROM VEHICLE WHERE vehicle_id = @vehicle_id)) WHERE tire_id IN (
                                    SELECT t.tire_id
                                    FROM TIRE AS t
                                    INNER JOIN VEHICLE_TRAILER AS vt ON t.vehicle_id = vt.vehicle_id
                                    WHERE vt.setup_no = (SELECT setup_no FROM VEHICLE_TRAILER WHERE vehicle_id = @vehicle_id and sort = '0'))

                                    INSERT INTO TIRE_MILEAGE_HISTORY (date_created, tire_id, location_id, vehicle_id, axis_id, tire_mileage, remark, userid)
                                    SELECT @date_created, t.tire_id, @location_id,t.vehicle_id,t.axis_id,t.tire_mileage + (@vehicle_mileage-(SELECT vehicle_mileage FROM VEHICLE WHERE vehicle_id = @vehicle_id)),'Update tire mileage',@userid
                                    FROM TIRE AS t
                                    INNER JOIN VEHICLE_TRAILER AS vt ON t.vehicle_id = vt.vehicle_id
                                    INNER JOIN VEHICLE AS v ON v.vehicle_id = vt.vehicle_id
                                    WHERE vt.setup_no = (SELECT setup_no FROM VEHICLE_TRAILER WHERE vehicle_id = @vehicle_id and sort = '0')

                                    UPDATE VEHICLE SET vehicle_mileage = vehicle_mileage +(@vehicle_mileage-vehicle_mileage) WHERE vehicle_id = @vehicle_id"
                        , new
                        {
                            p.date_created,
                            p.location_id,
                            process_id,
                            p.vehicle_id,
                            p.userid,
                            description,
                            p.vehicle_mileage

                        }, tran);

                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Tire update mileage succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        #endregion

        
    }
}