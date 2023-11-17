using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static BASE_RFID_TMS.Models.GlobalVar;
using Dapper;

namespace BASE_RFID_TMS.Models
{
    public class Repair
    {
        //private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
        //public string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        public class cCrudRepair
        {
            public DateTime date_created { get; set; }
            public string tire_id { get; set; }
            public string location_id { get; set; }
            public string vehicle_id { get; set; }
            //public string trailer_id { get; set; }
            public string axis_id { get; set; }
            public string repair_category_id { get; set; }
            public string notes { get; set; }
            public string userid { get; set; }
            public string tire_mileage { get; set; }
            public string rfid { get; set; }
        }
        
        public static cRespon InsertRepairProcess(cCrudRepair p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            //string no_register = "";
            int RowAffected = 0;
            string process_id = "220";
            string description = "Repair";
            //string axis_id = "0";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            //DateTime date_created_ = p.date_created.Add(Tnow);

            // string yearmonth = date_created_.ToString("yyyyMM");


            //DataTable tmpCheckRfid = getCheckRfidRegisterStatus(p.rfid);
            //if (tmpCheckRfid.Rows.Count > 0)
            // {
            // foreach (DataRow dr in tmpCheckRfid.Rows)
            // {
            //     returnRes.flag = false;
            //     returnRes.message = "#Rfid " + p.rfid + " already used. Register No: " + dr[1].ToString().Trim();
            // }
            //}
            //else
            //{
            //if (p.tire_id != "0" && p.tire_id != "")
            //{
            //    no_register = getNextRegisterNoTire(yearmonth);
            //}
            //else
            //{
            //}
            process_id = p.repair_category_id;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
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
                                    p.tire_id,
                                    p.location_id,
                                    process_id,
                                    p.vehicle_id,
                                    //p.trailer_id,
                                    p.axis_id,
                                    p.userid,
                                    p.rfid,
                                    description

                                },tran);

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from REPAIR where date_created=@date_created and tire_id=@tire_id and vehicle_id=@vehicle_id and axis_id=@axis_id) ) 
                                        BEGIN 
                                            INSERT INTO REPAIR 
                                         (date_created,tire_id,location_id,vehicle_id,axis_id,repair_category_id,notes,userid,tire_mileage)
                                         values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@repair_category_id,@notes,@userid,@tire_mileage)
                                        END 
                                
                                    ", new
                                {                     
                                    p.date_created,
                                    p.tire_id,
                                    p.location_id,
                                    p.vehicle_id,
                                    //p.trailer_id,
                                    p.axis_id,
                                    p.repair_category_id,
                                    p.notes,
                                    p.userid,
                                    p.tire_mileage
                                }, tran);

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE_MILEAGE_HISTORY where tire_id=@tire_id and location_id=@location_id and vehicle_id=@vehicle_id and axis_id=@axis_id and date_created=@date_created) ) 
                                        BEGIN 
                                            INSERT INTO TIRE_MILEAGE_HISTORY 
                                         (date_created,tire_id,location_id,vehicle_id,axis_id,tire_mileage, remark,userid)
                                         values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@tire_mileage, @description,@userid)
                                        END 
                                
                                    ", new
                                {
                                    p.date_created,
                                    p.tire_id,
                                    p.location_id,
                                    p.vehicle_id,
                                    //p.trailer_id,
                                    p.axis_id,
                                    p.tire_mileage,
                                    description,
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
                    returnRes.message = "Repair succesfully. Tire: " + p.tire_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            //}
            return returnRes;
        }
    }
}