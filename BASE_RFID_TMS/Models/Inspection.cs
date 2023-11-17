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
    public class Inspection
    {
        public class cCrudInsepection
        {
            public DateTime date_created { get; set; }
            public string tire_id { get; set; }
            public string location_id { get; set; }
            public string vehicle_id { get; set; }
            public string axis_id { get; set; }
            public string inspection_no { get; set; }
            public string tire_mileage { get; set; }
            public string air_pressure { get; set; }
            public string tread_depth { get; set; }
            public string notes { get; set; }
            public string userid { get; set; }
            public string rfid { get; set; }
            public string observation { get; set; }
            public string action { get; set; }
        }



        public class cCrudInsepectionDetail
        {
            //public DateTime date_created { get; set; }
            //public string tire_id { get; set; }
            //public string location_id { get; set; }
            //public string vehicle_id { get; set; }
            //public string axis_id { get; set; }
            //public string inspection_no { get; set; }
            public string observation_id { get; set; }
            public string notes { get; set; }
        }

        public class cCrudInsepectionDetailAction
        {
            //public DateTime date_created { get; set; }
            //public string tire_id { get; set; }
            //public string location_id { get; set; }
            //public string vehicle_id { get; set; }
            //public string axis_id { get; set; }
            //public string inspection_no { get; set; }
            public string action_id { get; set; }
            public string reason_id { get; set; }
            public string notes { get; set; }
        }

        public static cRespon InsertInspectionProcess(cCrudInsepection p)
        {
            List<cCrudInsepectionDetail> ListObServation = new List<cCrudInsepectionDetail>();
            List<cCrudInsepectionDetailAction> ListAction = new List<cCrudInsepectionDetailAction>();
            if (p.observation!="" && p.observation != "[]" && p.observation != null)
            {
                
                ListObServation = JsonConvert.DeserializeObject<List<cCrudInsepectionDetail>>(p.observation);
            }

            if (p.action != "" && p.action != "[]" && p.action != null)
            {

                ListAction = JsonConvert.DeserializeObject<List<cCrudInsepectionDetailAction>>(p.action);
            }
            
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            string process_id = "400";
            string description = "Inspection";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string yearmonth = p.date_created.ToString("yyyyMM");
            string inspection_no = getNextInspectionNo(yearmonth);
            

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                        
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TRACKING where date_created=@date_created and tire_id=@tire_id and location_id=@location_id and process_id=@process_id and vehicle_id=@vehicle_id and axis_id=@axis_id and date_created=@date_created  ) ) 
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

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from INSPECTION where date_created=@date_created and tire_id=@tire_id and vehicle_id=@vehicle_id and axis_id=@axis_id) ) 
                                        BEGIN 
                                            INSERT INTO INSPECTION 
                                         (date_created,tire_id,location_id,vehicle_id,axis_id,inspection_no,notes,userid,tire_mileage,air_pressure,tread_depth)
                                         values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@inspection_no,@notes,@userid,@tire_mileage,@air_pressure,@tread_depth)
                                        END 
                                
                                    ", new
                                {                     
                                    p.date_created,
                                    p.tire_id,
                                    p.location_id,
                                    p.vehicle_id,
                                    //p.trailer_id,
                                    p.axis_id,
                                    inspection_no,
                                    p.notes,
                                    p.userid,
                                    p.tire_mileage,
                                    p.air_pressure,
                                    p.tread_depth
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

                                conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                        BEGIN 
                                            UPDATE TIRE set location_id=@location_id,process_id=@process_id,date_modified=@date_created where tire_id=@tire_id
                                        END 
                                
                                    ", new
                                {
                                    p.date_created,
                                    p.tire_id,
                                    p.location_id,
                                    process_id
                                }, tran);

                        if (p.observation != "" && p.observation != "[]" && p.observation != null)
                        {

                            //ListObServation = JsonConvert.DeserializeObject<List<cCrudInsepectionDetail>>(p.observation);
                            foreach (var dr in ListObServation)
                            {
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from INSPECTION_DETAIL_OBSERVATION where date_created=@date_created and tire_id=@tire_id and vehicle_id=@vehicle_id and axis_id=@axis_id and inspection_no=@inspection_no and observation_id=@observation_id) ) 
                                        BEGIN 
                                            INSERT INTO INSPECTION_DETAIL_OBSERVATION 
                                         (date_created,tire_id,location_id,vehicle_id,axis_id,inspection_no,notes,observation_id)
                                         values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@inspection_no,@notes,@observation_id)
                                        END 
                                
                                    ", new
                                {
                                    p.date_created,
                                    p.tire_id,
                                    p.location_id,
                                    p.vehicle_id,
                                    p.axis_id,
                                    inspection_no,
                                    dr.notes,
                                    dr.observation_id
                                }, tran);
                            }
                        }

                        if (p.action != "" && p.action != "[]" && p.action != null)
                        {

                            //ListAction = JsonConvert.DeserializeObject<List<cCrudInsepectionDetailAction>>(p.action);
                            foreach (var dra in ListAction)
                            {
                                
                                    conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from INSPECTION_DETAIL_ACTION where date_created=@date_created and tire_id=@tire_id and vehicle_id=@vehicle_id and axis_id=@axis_id and inspection_no=@inspection_no and repair_category_id=@action_id) ) 
                                        BEGIN 
                                            INSERT INTO INSPECTION_DETAIL_ACTION 
                                         (date_created,tire_id,location_id,vehicle_id,axis_id,inspection_no,notes,repair_category_id,reason_id)
                                         values (@date_created,@tire_id,@location_id,@vehicle_id,@axis_id,@inspection_no,@notes,@action_id,@reason_id)
                                        END 
                                
                                    ", new
                                    {
                                        p.date_created,
                                        p.tire_id,
                                        p.location_id,
                                        p.vehicle_id,
                                        p.axis_id,
                                        inspection_no,
                                        p.notes,
                                        dra.action_id,
                                        dra.reason_id
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
                    returnRes.message = "Inspection succesfully. Tire: " + p.tire_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            //}
            return returnRes;
        }

        public static string getNextInspectionNo(string year)
        {
            string insp_no = "";
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select inspection_no=[dbo].[NextInspectionNumber](@year) ";
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
                insp_no = (from DataRow dr in dtTempTable.Rows
                         select (string)dr["inspection_no"]).FirstOrDefault();
            }
            return insp_no;
        }
    }
}