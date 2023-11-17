
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static BASE_RFID_TMS.Models.GlobalVar;

namespace BASE_RFID_TMS.Models
{
    public class Master
    {
        //public class cLocation
        //{
        //    public int? sort { get; set; }
        //    public string location_id { get; set; }
        //    public string location { get; set; }
        //    public string parent { get; set; }
        //    public string group { get; set; }
        //    public string level { get; set; }
        //    public string area { get; set; }
        //    public string is_detail { get; set; }
        //    public string process_area { get; set; }


        //}
        public class cCrudTyreDirectly
        {
            public string tire_id { get; set; }
            public string location_id { get; set; }
            public string userid { get; set; }
        }
        public class cCrudStatus
        {
            public string status_id { get; set; }
            public string status_name { get; set; }
            public string status_desc { get; set; }
            public string userid { get; set; }
        }
        public class cCrudEmployee
        {
            public string emp_id { get; set; }
            public string department_id { get; set; }
            public string emp_name { get; set; }
            public string emp_address { get; set; }
            public string emp_pos_code { get; set; }
            public string emp_phone { get; set; }
            public string emp_email { get; set; }
            public string emp_is_active { get; set; }
            public string userid { get; set; }
        }
        public class cCrudDepartment
        {
            public string department_id { get; set; }
            public string department_name { get; set; }
            public string userid { get; set; }
        }

        public class cCrudGroups
        {
            public string group_id { get; set; }
            public string group_name { get; set; }
            public string location_id { get; set; }
            public string userid { get; set; }
        }
        public class cCrudPosition
        {
            public string position_id { get; set; }
            public string position_name { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
        }
        public class cCrudTeamSchEmpHeader
        {
            public string date_created { get; set; }
            public string tsch_id { get; set; }
            public string data { get; set; }
            public string userid { get; set; }
        }
        public class cCrudTeamSchEmpDetail
        {
            public string emp_id { get; set; }
            public string position_id { get; set; }
        }
        public class cCrudTeamSchEmp
        {
            public string emp_id { get; set; }
            public string tsch_id { get; set; }
            public string position_id { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
        }
        public class cCrudTeamSchedule
        {
            public string tsch_id { get; set; }
            public string location_id { get; set; }
            public string shift { get; set; }
            public string date_start { get; set; }
            public string date_end { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
        }
        public class cCrudSchedule
        {
            public string year { get; set; }
            public string month { get; set; }
            public string date { get; set; }
            public string group_id { get; set; }
            public string location_id { get; set; }
            public string shift_id { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
        }
        public class cCrudHistoryLogin
        {
            public string histori_login_date { get; set; }
            public string user_login { get; set; }
        }
        public class cCrudGroupEmployee
        {
            public string group_id { get; set; }
            public string data { get; set; }
            public string flag { get; set; }
            public string date_created { get; set; }
            public string userid { get; set; }
        }
        public class cCrudGroupEmployeeDetail
        {
            public string emp_id { get; set; }
            public string position_id { get; set; }
        }
        public class cCrudUsrEmployeeInsert
        {
            public string date_created { get; set; }
            public string data { get; set; }
        }
        public class cCrudUsrEmployeeInsertDetail
        {
            public string emp_id { get; set; }
            public string userid { get; set; }
        }
        public class cCrudUsrEmployeeDelete
        {
            public string emp_id { get; set; }
            public string userid { get; set; }
            public string date_created { get; set; }
        }
        public class cCrudTyreType
        {
            public string tire_type_id { get; set; }
            public string tire_type_name { get; set; }
            public string userid { get; set; }
        }
        public class cCrudFailedAccountLogin
        {
            public string Username { get; set; }
        }
        public class cCrudSetupFleet
        {
            public string fleet_id { get; set; }
            public string fleet_name { get; set; }
            public string location_type_id { get; set; }
            public string description { get; set; }
            public string data { get; set; }
            public string userid { get; set; }
            //public string vehicle_id { get; set; } 
            //public string axis_type { get; set; } 
            //public string vehicle_name { get; set; } 
            //public string date_created { get; set; } 
            //public string sort_number { get; set; }
            //public string  rfid { get; set; } 
            //public string  vehicle_mileage { get; set; } 
            //public string  status_id { get; set; } 
            // public string  is_delete { get; set; }
            //public string  date_modified { get; set; }
        }

        public class cCrudVehicle
        {
            public string vehicle_id { get; set; }
            public string position { get; set; }
            public string vehicle_name { get; set; }
            public string date_created { get; set; }
            public string sort_number { get; set; }
            public string rfid { get; set; }
            public string vehicle_mileage { get; set; }
            public string status_id { get; set; }
            public string is_delete { get; set; }
            public string date_modified { get; set; }
        }
        public class cCrudFleetVehicle
        {
            public string vehicle_id { get; set; }
            public string position { get; set; }
            public string date_created { get; set; }
            public string rfid { get; set; }
            public string vehicle_mileage { get; set; }
            public string status_id { get; set; }
            public string is_delete { get; set; }
            public string date_modified { get; set; }
        }
        public class cCrudFleetVehicleFromFds
        {
            public string vehicle_id { get; set; }
            public string sort { get; set; }
            public string axis_type { get; set; }
            public string vehicle_type_id { get; set; }
            public string date_created { get; set; }
            public string rfid { get; set; }
            public string vehicle_mileage { get; set; }
            public string status_id { get; set; }
            public string is_delete { get; set; }
            public string date_modified { get; set; }
        }

        public class cCrudListAxis
        {
            public string axis_type { get; set; }
            public int number_of_tire { get; set; }
            public string axis_no { get; set; }
            public string userid { get; set; }
        }
        public static string checkStatusTyreDirectly(string tire_id)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=status_id from tire where tire_id=@tire_id ";
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
                         select (string)dr["data"]).FirstOrDefault();
            }
            return data;
        }
        public static cRespon InsertTyreDirectly(cCrudTyreDirectly p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            string yearmonth = Convert.ToDateTime(date_created).ToString("yyyyMM");
            no_register = Register.getNextRegisterNoTire(yearmonth);
            string remark = "";
            //string axis_id = "0";
            string tire_mileage = "0";
            string process_id_reg = "110";
            string checkingTyre = checkStatusTyreDirectly(p.tire_id);
            if (checkingTyre == "")
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                                BEGIN 
                                                    insert into tire (tire_id, location_id, tire_code, tire_name, serial_number,
                                                    size, pattern, km, rfid, status_id, is_delete, date_created, userid)
                                                    values
                                                    (@tire_id, @location_id, 'XXX', @tire_id, @tire_id,
                                                    0, 0, 0, '0', '7', 0, @date_created, @userid)
                                            
                                                    insert into epc_tire (epc_rfid, tire_id, is_delete, date_created, date_modified, userid)
                                                     values
                                                    ('0',@tire_id,0,@date_created,@date_created, @userid)
                                               END
                                                ELSE
                                                BEGIN
                                                    update tire set status_id='7', date_created=@date_created, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                                END
                            ", new
                            {
                                p.tire_id,
                                p.location_id,
                                date_created,
                                p.userid
                            }, tran);
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=@no_register and tire_id=@tire_id) ) 
                                    BEGIN 
                                        INSERT INTO REGISTER_TIRE
                                     (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                                     values (@no_register,@date_created,@tire_id,@location_id,@userid,'0',0)
                                    END 
                                
                                ", new
                            {
                                no_register,
                                date_created,
                                p.tire_id,
                                p.location_id,
                                p.userid
                            
                            }, tran);
                            conn.Execute(@"
                                            INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                                values
                                                (@tire_id,'0',@process_id_reg, 'XXX','XXX', @location_id,0, @date_created, @userid)
                                    
                                    ", new
                            {
                                date_created,
                                p.tire_id,
                                p.location_id,
                                process_id_reg,
                                p.userid

                            }, tran);

                            conn.Execute(@"
                                        INSERT INTO TIRE_MILEAGE_HISTORY 
                                     (date_created,tire_id,location_id,km, is_delete,userid)
                                     values (@date_created,@tire_id,@location_id,0,0,@userid)
                              
                                
                                ", new
                            {
                                date_created,
                                p.tire_id,
                                p.location_id,
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





                    if (returnRes.flag == true)
                    {
                        returnRes.message = "Tyre saved. ";
                        returnRes.status = "success";
                    }
                    else
                    {
                        returnRes.status = "failed";
                    }
                }
            }
            else if (checkingTyre == "7")
            {
                returnRes.flag = true;
                returnRes.message = "Tyre saved.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.flag = false;
                returnRes.message = "# Failed, Tyre already exist.";
            }

            return returnRes;
        }
        public static cRespon InsertTyreStatus(cCrudStatus p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 status_id from [STATUS] where status_id=@status_id ) ) 
                            BEGIN 
                                INSERT INTO [STATUS] 
                                (status_id,status_name,status_desc,is_delete,date_created,date_modified,userid)
                                values (@status_id,@status_name,@status_desc,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE [STATUS] set is_delete=0, status_name=@status_name,status_desc=@status_desc,date_modified=@date_created, userid=@userid
                                where status_id=@status_id
                            END
                                
                        ", new
                        {
                            p.status_id,
                            p.status_name,
                            p.status_desc,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup tyre status succesfully. Tyre status Id : " + p.status_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon deleteTyreStatus(cCrudStatus p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.message = "";
            returnRes.status = "";
            DateTime date_modified = DateTime.Now;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update [STATUS] set is_delete='1', date_modified=@date_modified, userid=@userid  where status_id=@status_id
                                        END 
                                
                                    ", new
                        {
                            p.status_id,
                            p.userid,
                            date_modified
                        }, tran);



                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Delete tyre status succesfully. Tyre Status Id: " + p.status_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static string qGetListLocationFilter(string location_id, string is_wh)
        {

            // DataTable dtTempTable = new DataTable("SPOOL");
            //full_location=case when [level]=0 then [location] else dbo.getFullParentLocation(location_id)+' - '+[location] end
            string StrQuery = "select sort,location_id,[location],parent,[group],[level],area,detail as is_detail,process_area from location where  location_type_id not in('5') and is_delete=0" + System.Environment.NewLine;
            if (is_wh != "0")
            {
                StrQuery += " and location_id not in ('XXX','ZZZ','OV00','888','999','A00')  " + System.Environment.NewLine;
            }
            else {
                StrQuery += " and location_id not in ('XXX','ZZZ','OV00','888','999','A00','A01')  " + System.Environment.NewLine;
            }
            if (location_id != "0" && location_id != null && location_id != "")
            {
                StrQuery += " and location_id=@location_id " + System.Environment.NewLine;
            }
            StrQuery += "order by sort" + System.Environment.NewLine;
            return StrQuery;
        }
        public static string qGetListLocationFilterDispose(string location_id)
        {

            // DataTable dtTempTable = new DataTable("SPOOL");
            //full_location=case when [level]=0 then [location] else dbo.getFullParentLocation(location_id)+' - '+[location] end
            string StrQuery = "select sort,location_id,[location],parent,[group],[level],area,detail as is_detail,process_area from location where location_id not in ('XXX','OV00') and location_type_id in('5') and is_delete=0" + System.Environment.NewLine;
            if (location_id != "0" && location_id != null && location_id != "")
            {
                StrQuery += " and location_id=@location_id " + System.Environment.NewLine;
            }
            StrQuery += "order by sort" + System.Environment.NewLine;
            return StrQuery;
        }

        public static string qGetListRepairCategory()
        {
            string StrQuery = "select repair_category_id, repair_name from REPAIR_CATEGORY order by repair_category_id" + System.Environment.NewLine;
            
            return StrQuery;
        }

        public static string qGetListObservation()
        {
            string StrQuery = "select observation_id, observation from OBSERVATION order by sort asc" + System.Environment.NewLine;

            return StrQuery;
        }

        public static String qDataTireBySerialNumber()
        {
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = @"Select 
			                                 T.tire_id, T.serial_number, 
                                                         T.tire_name, T.size, T.pattern, I.brand_tire_id, BT.brand_tire_name, I.supplier_id, S.supplier_name, T.rfid,
                                                         T.location_id, l.location, T.date_modified, CONVERT(varchar, T.date_modified, 106) AS str_date_modified, T.km as tire_mileage, T.status_id
                                from TIRE T 
                                INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                                --INNER JOIN  REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                                INNER JOIN ITEM I ON I.tire_code = T.tire_code
                                INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                                INNER JOIN BRAND_TIRE AS BT ON I.brand_tire_id = BT.brand_tire_id  " + System.Environment.NewLine;
            StrQuery += "where (T.serial_number=@sn OR" + System.Environment.NewLine;
            StrQuery += "T.rfid =@sn) and  t.tire_id not in(select tire_id from DISPOSAL_DETAIL) and t.is_delete=0" + System.Environment.NewLine;
            
            return StrQuery;
        }

        public static String qDataTireBySerialNumberForInbound(string location_id)
        {
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = @"
                                Select 
			                                T.tire_id, T.serial_number, 
                                                            T.tire_name, T.size, T.pattern, I.brand_tire_id, BT.brand_tire_name, I.supplier_id, S.supplier_name, T.rfid,
                                                            od.location_id, l.location, T.date_modified, CONVERT(varchar, T.date_modified, 106) AS str_date_modified, T.km as tire_mileage, T.status_id
                                from TIRE T 
                                INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                                INNER JOIN  REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                                INNER JOIN ITEM I ON I.tire_code = T.tire_code
                                INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                                INNER JOIN BRAND_TIRE AS BT ON I.brand_tire_id = BT.brand_tire_id  
                                INNER JOIN OUTBOUND_DETAIL od on od.tire_id = T.tire_id " + System.Environment.NewLine;
            StrQuery += "where od.location_id=@location_id and (T.serial_number=@sn OR" + System.Environment.NewLine;
            StrQuery += "T.rfid =@sn) and  t.tire_id not in(select tire_id from DISPOSAL_DETAIL) and t.is_delete=0 and od.is_active=1" + System.Environment.NewLine;

            return StrQuery;
        }
        public static String qDataTireBySerialNumberCustom( string sn)
        {

            DataTable dtTempTable = new DataTable("TIRE");

            //string StrQuery = "select top 100 id=t.tire_id, label=t.tire_id+' - '+t.tire_name, value=t.tire_id, R.po_no,convert(varchar,R.po_date,106) as po_date,R.do_no,convert(varchar,R.do_date,106) as do_date,T.tire_id,t.serial_number,t.tire_name,t.size,t.pattern,t.brand_id,B.brand_name,t.tire_code," + System.Environment.NewLine;
            //StrQuery += "t.supplier_id,s.supplier_name,G.register_no,convert(varchar,G.date_created,106) as register_date,G.rfid," + System.Environment.NewLine;
            //StrQuery += "t.vehicle_id,v.vehicle_name,v.vehicle_no,v.is_vehicle,t.axis_id,a.axis_name," + System.Environment.NewLine;
            //StrQuery += "t.location_id,l.location,t.process_id,p.process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.tire_mileage,t.status_id from TIRE T" + System.Environment.NewLine;
            //StrQuery += "inner join axis a on t.axis_id=a.axis_id" + System.Environment.NewLine;
            //StrQuery += "inner join VEHICLE v on t.vehicle_id=v.vehicle_id" + System.Environment.NewLine;
            ////StrQuery += "inner join TRAILER tr on t.trailer_id=tr.trailer_id" + System.Environment.NewLine;
            //StrQuery += "inner join SUPPLIER S on T.supplier_id=S.supplier_id" + System.Environment.NewLine;
            //StrQuery += "left join BRAND B on T.brand_id=B.brand_id" + System.Environment.NewLine;
            //StrQuery += "inner join location l on t.location_id=l.location_id" + System.Environment.NewLine;
            //StrQuery += "inner join process p on t.process_id=p.process_id" + System.Environment.NewLine;
            //StrQuery += "INNER JOIN REGISTER G on T.tire_id=G.tire_id" + System.Environment.NewLine;
            //StrQuery += "INNER JOIN RECEIVE_DETAIL RD on RD.serial_number=T.tire_id" + System.Environment.NewLine;
            //StrQuery += "INNER JOIN RECEIVE R on RD.receive_no=R.receive_no" + System.Environment.NewLine;
            //StrQuery += "where t.serial_number like '%" + sn + "%' " + System.Environment.NewLine;
            string StrQuery = @"
					Select label=T.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, value=t.tire_id,i.tire_name, i.tire_code,
			                T.tire_id, T.serial_number, 
                                            T.tire_name, T.size, T.pattern, I.brand_tire_id, BT.brand_tire_name, I.supplier_id, S.supplier_name, T.rfid,
                                            T.location_id, l.location, T.date_modified, CONVERT(varchar, T.date_modified, 106) AS str_date_modified, T.km as tire_mileage, T.status_id
                , tu.vehicle_id, v.vehicle_name, ax.tire_no
                from TIRE T 
                INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                INNER JOIN  REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                INNER JOIN ITEM I ON I.tire_code = T.tire_code
                INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                INNER JOIN BRAND_TIRE AS BT ON I.brand_tire_id = BT.brand_tire_id
                left join TIRE_UNIT tu on tu.tire_id = t.tire_id and tu.is_delete=0
				left join TIRE_AXIS ta on ta.tire_id = tu.tire_id  and ta.is_delete=0
				left join axis ax on ax.axis_id = ta.axis_id
				left join VEHICLE v on v.vehicle_id = tu.vehicle_id
" + System.Environment.NewLine;
            StrQuery += "where T.serial_number like '%" + sn + "%' and  t.tire_id not in(select tire_id from DISPOSAL_DETAIL)  and t.is_delete=0   or tu.is_delete=0 " + System.Environment.NewLine;
            //StrQuery += "where (t.serial_number like '%" + sn + "%' or t.tire_name like '%" + sn + "%') " + System.Environment.NewLine;
            return StrQuery;
        }


        public static String qDataTireBySerialNumberForLookupTracking(string sn)
        {

            DataTable dtTempTable = new DataTable("TIRE");
            
            string StrQuery = @"
Select label=T.tire_id+' - '+case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end, case when isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) ='' then t.tire_code else isnull(i.unique_code, i.tire_code+' '+i.size+' '+ case when i.pattern = '-' then '' else i.pattern end) end as unique_code, value=t.tire_id,i.tire_name, i.tire_code,
			                T.tire_id, T.serial_number, 
                                            T.tire_name, T.size, T.pattern, I.brand_tire_id, BT.brand_tire_name, I.supplier_id, S.supplier_name, T.rfid,
                                            T.location_id, l.location, T.date_modified, CONVERT(varchar, T.date_modified, 106) AS str_date_modified, T.km as tire_mileage, T.status_id
                , tu.vehicle_id, v.vehicle_name, ax.tire_no
                from TIRE T 
                INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                INNER JOIN ITEM I ON I.tire_code = T.tire_code
                INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                INNER JOIN BRAND_TIRE AS BT ON I.brand_tire_id = BT.brand_tire_id
                left join TIRE_UNIT tu on tu.tire_id = t.tire_id and tu.is_delete=0
				left join TIRE_AXIS ta on ta.tire_id = tu.tire_id  and ta.is_delete=0
				left join axis ax on ax.axis_id = ta.axis_id
				left join VEHICLE v on v.vehicle_id = tu.vehicle_id
" + System.Environment.NewLine;
            StrQuery += @"where T.serial_number like '%" + sn + @"%' group by T.tire_id,i.unique_code, i.tire_code,i.size,i.pattern,t.tire_code,i.tire_name, 

                           T.serial_number, 
                                            T.tire_name, T.size, T.pattern, I.brand_tire_id, BT.brand_tire_name, I.supplier_id, S.supplier_name, T.rfid,
                                            T.location_id, l.location, T.date_modified, T.date_modified, T.km, T.status_id
                , tu.vehicle_id, v.vehicle_name, ax.tire_no" + System.Environment.NewLine;
            return StrQuery;
        }
        public static string qGetListTireAxisPerVehicle(string fleet_id, string vehicle_id)
        {
            //string  StrQuery = "select TA.tire_id, TA.vehicle_id,TA.axis_id,A.tire_no,a.axis_no,a.axis_position,a.axis_type,AE.axis_type_name,a.tire_axis_position, A.description from TIRE_AXIS TA " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN AXIS A ON TA.axis_id=A.axis_id" + System.Environment.NewLine;
            //StrQuery += "INNER JOIN AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
            //StrQuery += "where ta.tire_axis_no=@axis_setup_no and ta.vehicle_id=@vehicle_id " + System.Environment.NewLine;
            //StrQuery += "order by a.tire_no " + System.Environment.NewLine;
            string StrQuery = "select " + System.Environment.NewLine;
            StrQuery += @"FV.fleet_id, T.tire_id, tu.vehicle_id,A.axis_id,A.tire_no,a.axis_no,a.axis_type,ATY.axis_type_name, A.description,fv.position 
                        from 
                        TIRE T
                        INNER JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and tu.is_delete=0
                        INNER JOIN FLEET_VEHICLE FV ON  FV.vehicle_id = TU.vehicle_id and FV.is_delete=0
                        INNER JOIN TIRE_AXIS TA ON FV.vehicle_id = TU.vehicle_id and ta.tire_id = tu.tire_id  and ta.is_delete=0
                        INNER JOIN VEHICLE V ON V.vehicle_id = FV.vehicle_id
                        INNER JOIN AXIS_TYPE ATY ON ATY.axis_type = V.axis_type
                        INNER JOIN AXIS A ON A.axis_id = TA.axis_id   " + System.Environment.NewLine;
            StrQuery += "where FV.fleet_id='"+ fleet_id + "' and tu.vehicle_id ='" + vehicle_id+"' and t.is_delete=0 " + System.Environment.NewLine;
            StrQuery += "order by FV.position " + System.Environment.NewLine;
            
            return StrQuery;
        }


        public static string qGetListTireAxisPerFleet(string fleet_id)
        {
            //string  StrQuery = "select TA.tire_id, TA.vehicle_id,TA.axis_id,A.tire_no,a.axis_no,a.axis_position,a.axis_type,AE.axis_type_name,a.tire_axis_position, A.description from TIRE_AXIS TA " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN AXIS A ON TA.axis_id=A.axis_id" + System.Environment.NewLine;
            //StrQuery += "INNER JOIN AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
            //StrQuery += "where ta.tire_axis_no=@axis_setup_no and ta.vehicle_id=@vehicle_id " + System.Environment.NewLine;
            //StrQuery += "order by a.tire_no " + System.Environment.NewLine;

            string StrQuery = "select " + System.Environment.NewLine;
            StrQuery += @"FV.fleet_id, T.tire_id, tu.vehicle_id,A.axis_id,A.tire_no,a.axis_no,a.axis_type,ATY.axis_type_name, A.description,fv.position 
                        from 
                        TIRE T
                        INNER JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and tu.is_delete=0
                        INNER JOIN FLEET_VEHICLE FV ON FV.vehicle_id = TU.vehicle_id and FV.is_delete=0
                        INNER JOIN TIRE_AXIS TA ON FV.vehicle_id = TU.vehicle_id and ta.tire_id = tu.tire_id  and ta.is_delete=0
                        INNER JOIN VEHICLE V ON V.vehicle_id = FV.vehicle_id
                        INNER JOIN AXIS_TYPE ATY ON ATY.axis_type = V.axis_type
                        INNER JOIN AXIS A ON A.axis_id = TA.axis_id " + System.Environment.NewLine;
            StrQuery += "where FV.fleet_id=@fleet_id and t.is_delete=0 " + System.Environment.NewLine;
            StrQuery += "order by FV.position " + System.Environment.NewLine;

            return StrQuery;
        }
        public class cTire
        {
            public string po_no { get; set; }
            public string po_date { get; set; }
            public string do_no { get; set; }
            public string do_date { get; set; }
            public string tire_id { get; set; }
            public string serial_number { get; set; }
            public string tire_name { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string brand_id { get; set; }
            public string brand_name { get; set; }
            public string supplier_id { get; set; }
            public string supplier_name { get; set; }
            public string register_no { get; set; }
            public string register_date { get; set; }
            public string rfid { get; set; }
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string vehicle_no { get; set; }
            public Boolean is_vehicle { get; set; }
            public string vehicle_setup_no { get; set; }
            public string axis_id { get; set; }
            public string tire_no { get; set; }
            public string axis_no { get; set; }
            public string axis_position { get; set; }
            public string axis_type { get; set; }
            public string axis_type_name { get; set; }
            public string tire_axis_position { get; set; }
            public string tire_position { get; set; }
            public string location_id { get; set; }
            public string location { get; set; }
            public string process_id { get; set; }
            public string process { get; set; }
            public string date_modified { get; set; }
            public string str_date_modified { get; set; }
            public string tire_mileage { get; set; }
            public string tire_axis_no { get; set; }
            public string status_id { get; set; }
            public string tread_depth_value { get; set; }
            public List<cVehicle> vehicle { get; set; }
        }

        public class cVehicle
        {
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string vehicle_no { get; set; }
            public string rfid { get; set; }
            public string setup_no { get; set; }
            public string sort { get; set; }
            public string is_vehicle { get; set; }
            public string axis_type { get; set; }
            public string axis_type_name { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }

        public class cGetVehicleById
        {
            
            public string fleet_id_a { get; set; }
            public string fleet_id { get; set; }
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string vehicle_rfid { get; set; }
            public string axis_type { get; set; }
            public string axis_type_name { get; set; }
            public string brand_vhc_name { get; set; }
            public string number_of_tire { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }

        public class cCrudLocation
        {
            public string location_id { get; set; }
            public string location { get; set; }
            public string parent { get; set; }
            public string group { get; set; }
            public string level { get; set; }
            public string sort { get; set; }
            public string area { get; set; }
            public string detail { get; set; }
            public string location_type_id { get; set; }
            public string process_area { get; set; }
            public string userid { get; set; }
            //public List<cTireAxis> tire_axis { get; set; }
        }

        public class cTireAxis
        {
            public string vehicle_id { get; set; }
            public string axis_id { get; set; }
            public string tire_no { get; set; }
            public string axis_no { get; set; }
            public string axis_position { get; set; }
            public string axis_type { get; set; }
            public string axis_type_name { get; set; }
            public string tire_axis_position { get; set; }
            public string description { get; set; }
        }

        public class cVehicleDetail
        {
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string vehicle_no { get; set; }
            public string is_vehicle { get; set; }
            public string axis_type { get; set; }
            public string axis_type_name { get; set; }
            public string status_id { get; set; }
            public string vehicle_mileage { get; set; }
            public string setup_no { get; set; }
            public string register_no { get; set; }
            public string register_date { get; set; }
            public string rfid { get; set; }
            public string tire_axis_setup_no { get; set; }
            public string tire_id { get; set; }
            public List<cVehicle> vehicle { get; set; }
        }

        public class cTireForSingleVehicle
        {
            public string vehicle_id { get; set; }
            public string tire_id { get; set; }
            public string axis_id { get; set; }
            public string description { get; set; }
        }
        public static DataTable getDataTireDetail(string sn, string location_id)
    {
        DataTable dtTempTable = new DataTable("TIRE");
            //string StrQuery = "select top 1 R.po_no,convert(varchar,R.po_date,106) as po_date,R.do_no,convert(varchar,R.do_date,106) as do_date,T.tire_id,t.serial_number,t.tire_name," + System.Environment.NewLine;
            //StrQuery += "t.size,t.pattern,t.brand_id,B.brand_name,t.supplier_id,s.supplier_name,G.register_no,convert(varchar,G.date_created,106) as register_date,G.rfid," + System.Environment.NewLine;
            //StrQuery += "t.vehicle_id,v.vehicle_name,v.vehicle_no,v.is_vehicle,vehicle_setup_no=[dbo].[GetLastVehicleSetupNo](t.vehicle_id),t.axis_id,a.tire_no,a.axis_no,a.axis_position,a.axis_type,ae.axis_type_name,a.tire_axis_position,a.description as tire_position," + System.Environment.NewLine;
            //StrQuery += "t.location_id,l.location,t.process_id,p.process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.tire_mileage,tire_axis_no=[dbo].[GetLastTireAxisNoByTire](t.tire_id)," + System.Environment.NewLine;
            //StrQuery += "status_id=T.status_id from " + System.Environment.NewLine;
            //StrQuery += "TIRE T" + System.Environment.NewLine;
            //StrQuery += "left join axis a on t.axis_id=a.axis_id" + System.Environment.NewLine;
            //StrQuery += "LEFT join AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
            //StrQuery += "inner join VEHICLE v on t.vehicle_id=v.vehicle_id" + System.Environment.NewLine;
            //StrQuery += "inner join SUPPLIER S on T.supplier_id=S.supplier_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN BRAND B on T.brand_id=B.brand_id" + System.Environment.NewLine;
            //StrQuery += "inner join location l on t.location_id=l.location_id" + System.Environment.NewLine;
            //StrQuery += "inner join process p on t.process_id=p.process_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN REGISTER G on T.tire_id=G.tire_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN RECEIVE_DETAIL RD on RD.serial_number=T.tire_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN RECEIVE R on RD.receive_no=R.receive_no" + System.Environment.NewLine;
            //StrQuery += "where t.serial_number=@sn or g.rfid=@sn or t.rfid=@sn" + System.Environment.NewLine;
            string criteria_loc = "";
            if (location_id != "OV00") {
                criteria_loc = " and T.location_id='"+location_id+"'";
            }
            string StrQuery = @"
                        IF (EXISTS (select TOP 1 tire_id from tire where (serial_number=@sn OR rfid =@sn) and location_id='OV00' ) )
                        BEGIN
                        select
		                    T.tire_id,t.serial_number,t.tire_name,
                            t.size,t.pattern,I.brand_tire_id,B.brand_tire_name,I.supplier_id,s.supplier_name,G.register_tire_no,convert(varchar,G.date_created,106) as register_date,T.rfid as rfid,
                            tu.vehicle_id,v.vehicle_name,vehicle_setup_no=FV.fleet_id,ta.axis_id,a.tire_no,a.axis_no,a.axis_type,aty.axis_type_name,a.description as tire_position,
                            t.location_id,l.location,0 as process_id,'' as process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.km as tire_mileage,FV.fleet_id as tire_axis_no,
                            status_id=T.status_id, ISNULL((select top 1 smod.value 
				                from SERV_MAINT_OBSERVATION smo
				                inner join SERV_MAINT_OBSERVATION_DETAIL smod on smo.smo_id=smod.smo_id and smo.date_created=smod.date_created
				                where tire_id=T.tire_id and smod.observation_item_id='2' order by smo.date_created desc),0) as tread_depth_value from TIRE T  

							
						LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and tu.is_delete=0
						LEFT JOIN FLEET_VEHICLE FV ON FV.vehicle_id = TU.vehicle_id and fv.is_delete=0
						LEFT JOIN TIRE_AXIS TA ON  FV.vehicle_id = TU.vehicle_id and ta.tire_id = tu.tire_id and ta.is_delete=0
						LEFT JOIN VEHICLE V ON V.vehicle_id = FV.vehicle_id and v.is_delete=0
                        LEFT JOIN AXIS A On A.axis_id = TA.axis_id and a.is_delete=0
                        LEFT JOIN AXIS_TYPE ATY ON ATY.axis_type = A.axis_type and aty.is_delete=0

                        INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                        INNER JOIN  REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                        INNER JOIN ITEM I ON I.tire_code = T.tire_code
                        INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                        INNER JOIN BRAND_TIRE AS B ON I.brand_tire_id = B.brand_tire_id   " + System.Environment.NewLine;
            StrQuery += "where (T.serial_number=@sn OR" + System.Environment.NewLine;
            StrQuery += "T.rfid =@sn) and T.is_delete=0 END"  + System.Environment.NewLine;
            StrQuery += @"ELSE
                            BEGIN" + System.Environment.NewLine;
            StrQuery += @"
                        select
		                    T.tire_id,t.serial_number,t.tire_name,
                            t.size,t.pattern,I.brand_tire_id,B.brand_tire_name,I.supplier_id,s.supplier_name,'' as register_tire_no,'' as register_date,T.rfid as rfid,
                            tu.vehicle_id,v.vehicle_name,vehicle_setup_no=FV.fleet_id,ta.axis_id,a.tire_no,a.axis_no,a.axis_type,aty.axis_type_name,a.description as tire_position,
                            t.location_id,l.location,0 as process_id,'' as process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.km as tire_mileage,FV.fleet_id as tire_axis_no,
                            status_id=T.status_id, ISNULL((select top 1 smod.value 
				            from SERV_MAINT_OBSERVATION smo
				            inner join SERV_MAINT_OBSERVATION_DETAIL smod on smo.smo_id=smod.smo_id and smo.date_created=smod.date_created
				            where tire_id=T.tire_id and smod.observation_item_id='2' order by smo.date_created desc),0) as tread_depth_value from TIRE T  

							
						LEFT JOIN TIRE_UNIT TU ON TU.tire_id = T.tire_id and tu.is_delete=0
						LEFT JOIN FLEET_VEHICLE FV ON FV.vehicle_id = TU.vehicle_id and fv.is_delete=0
						LEFT JOIN TIRE_AXIS TA ON  FV.vehicle_id = TU.vehicle_id and ta.tire_id = tu.tire_id and ta.is_delete=0
						LEFT JOIN VEHICLE V ON V.vehicle_id = FV.vehicle_id and v.is_delete=0
                        LEFT JOIN AXIS A On A.axis_id = TA.axis_id and a.is_delete=0
                        LEFT JOIN AXIS_TYPE ATY ON ATY.axis_type = A.axis_type and aty.is_delete=0

                        INNER JOIN LOCATION AS l ON T.location_id = l.location_id 
                        --INNER JOIN  REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                        INNER JOIN ITEM I ON I.tire_code = T.tire_code
                        INNER JOIN SUPPLIER AS S ON I.supplier_id = S.supplier_id 
                        INNER JOIN BRAND_TIRE AS B ON I.brand_tire_id = B.brand_tire_id   " + System.Environment.NewLine;
            StrQuery += "where (T.serial_number=@sn OR" + System.Environment.NewLine;
            StrQuery += "T.rfid =@sn) and T.is_delete=0 and T.location_id = @location_id END" + System.Environment.NewLine;
            SqlConnection con = new SqlConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = StrQuery;
        cmd.Connection = con;
        cmd.Parameters.AddWithValue("@sn", sn);
        cmd.Parameters.AddWithValue("@location_id", location_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.SelectCommand.CommandTimeout = 0;
        con.Open();
        da.Fill(dtTempTable);
        con.Close();
        return dtTempTable;
    }
        public static cRespon InsertTyreType(cCrudTyreType p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_type_id from TIRE_TYPE where tire_type_id=@tire_type_id ) ) 
                            BEGIN 
                                INSERT INTO TIRE_TYPE 
                                (tire_type_id,tire_type_name,is_delete,date_created,date_modified,userid)
                                values (@tire_type_id,@tire_type_name,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE TIRE_TYPE set is_delete=0, tire_type_name=@tire_type_name,date_modified=@date_created, userid=@userid
                                where tire_type_id=@tire_type_id
                            END
                                
                        ", new
                        {
                            p.tire_type_id,
                            p.tire_type_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup tyre type succesfully. Tyre type Id : " + p.tire_type_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon ResetFailedLogin(cCrudFailedAccountLogin p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                               update aspnetusers set accessfailedcount=0 where email=@Username

                                
                        ", new
                        {
                            p.Username
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "ResetFailedCount";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon SetFailedAccountLogin(cCrudFailedAccountLogin p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                DECLARE @@count int;
                                 select @@count = AccessFailedCount from aspnetusers where email=@Username
                                 if @@count <4 
                                 begin
	                                update aspnetusers set accessfailedcount=+accessfailedcount+1 where email=@Username
                                 end
                                 else
                                 begin 
	                                update aspnetusers set accessfailedcount=+accessfailedcount+1, lockoutenabled=0 where email=@Username
                                 end

                                
                        ", new
                        {
                            p.Username
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "FailedCount";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon deleteTyreType(cCrudTyreType p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.message = "";
            returnRes.status = "";
            DateTime date_modified = DateTime.Now;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update TIRE_TYPE set is_delete='1', date_modified=@date_modified, userid=@userid  where tire_type_id=@tire_type_id
                                        END 
                                
                                    ", new
                        {
                            p.tire_type_id,
                            p.userid,
                            date_modified
                        }, tran);



                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Delete tyre type succesfully. Tyre type Id: " + p.tire_type_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static DataTable getDataListVehicleTrailer(string setup_no)
        {
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = @"select TU.vehicle_id,v.vehicle_name,v.vehicle_rfid as rfid,fv.fleet_id,fv.position as sort,v.axis_type,ae.axis_type_name 
                        from TIRE_UNIT  TU 
                        INNER JOIN VEHICLE V ON TU.vehicle_id=V.vehicle_id
						INNER JOIN FLEET_VEHICLE FV ON  FV.vehicle_id = TU.vehicle_id and FV.is_delete=0
                        INNER JOIN AXIS_TYPE AE ON V.axis_type=AE.axis_type  " + System.Environment.NewLine;
            StrQuery += "where FV.fleet_id=@setup_no and tu.is_delete=0 order by fv.position" + System.Environment.NewLine;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@setup_no", setup_no);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getDataListVehicleTire(string setup_no)
        {
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = @"select TU.vehicle_id,v.vehicle_name,v.vehicle_rfid as rfid,fv.fleet_id,fv.position as sort,v.axis_type,ae.axis_type_name 
                        from TIRE_UNIT  TU 
                        INNER JOIN VEHICLE V ON TU.vehicle_id=V.vehicle_id
						INNER JOIN FLEET_VEHICLE FV ON FV.vehicle_id = TU.vehicle_id and FV.is_delete=0
                        INNER JOIN AXIS_TYPE AE ON V.axis_type=AE.axis_type " + System.Environment.NewLine;
            StrQuery += "where FV.fleet_id=@setup_no and tu.is_delete=0 group by TU.vehicle_id,v.vehicle_name,v.vehicle_rfid,fv.fleet_id,fv.position ,v.axis_type,ae.axis_type_name  order by fv.position" + System.Environment.NewLine;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@setup_no", setup_no);
            //cmd.Parameters.AddWithValue("@tire_id", tire_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getDataListTireAxis(string axis_setup_no, string vehicle_id)
        {
            DataTable dtTempTable = new DataTable("TIRE_AXIS");
            string StrQuery = "select TA.vehicle_id,TA.axis_id,A.tire_no,a.axis_no,a.axis_position,a.axis_type,AE.axis_type_name,a.tire_axis_position, A.description from TIRE_AXIS TA " + System.Environment.NewLine;
            StrQuery += "INNER JOIN AXIS A ON TA.axis_id=A.axis_id" + System.Environment.NewLine;
            StrQuery += "INNER JOIN AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
            StrQuery += "where ta.tire_axis_no=@axis_setup_no and ta.vehicle_id=@vehicle_id " + System.Environment.NewLine;
            StrQuery += "order by a.tire_no " + System.Environment.NewLine;


            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@axis_setup_no", axis_setup_no);
            cmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getDataVehicleDetail(string id)
        {
            DataTable dtTempTable = new DataTable("VEHICLE");
            string StrQuery = @"
select top 1 v.vehicle_id,v.vehicle_name,v.axis_type,ae.axis_type_name,status_id= cast('true' as bit),
                            vehicle_mileage=0,vehicle_setup_no=fv.fleet_id,
                            G.register_vhc_no,convert(varchar,G.date_created,106) as register_date,G.rfid,tire_axis_setup_no=fv.fleet_id, tu.tire_id from 
                           FLEET_VEHICLE FV
							INNER JOIN VEHICLE V on FV.vehicle_id = v.vehicle_id
							inner join fleet f on f.fleet_id=fv.fleet_id
                            LEFT join AXIS_TYPE AE ON v.axis_type=AE.axis_type
                            LEFT JOIN REGISTER_VHC G on v.vehicle_id=G.vehicle_id
                            LEFT JOIN TIRE_UNIT TU on TU.vehicle_id = v.vehicle_id" + System.Environment.NewLine;
            StrQuery += "where v.vehicle_id=@id or g.rfid = @id and fv.is_delete=0   and tu.is_delete=0" + System.Environment.NewLine;



            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        #region Master
        public class cCrudSetupTire
        {
            public string tire_code { get; set; }
            public string tire_name { get; set; }
            public string tire_type_id { get; set; }
            public string unique_code { get; set; }
            public string otd { get; set; }
            public string lr_single { get; set; }
            public string lr_double { get; set; }
            public string sp_single { get; set; }
            public string sp_double { get; set; }
            public string play_rating { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string brand_tire_id { get; set; }
            public string supplier_id { get; set; }
            public string price { get; set; }
            public string userid { get; set; }
        }

        public class cCrudBrandTire
        {
            public string brand_tire_id { get; set; }
            //public DateTime date_created { get; set; }
            public string brand_tire_name { get; set; }
            public string userid { get; set; }
        }
        public class cCrudSupplier
        {
            public string supplier_id { get; set; }
            public string supplier_name { get; set; }
            public string supplier_address { get; set; }
            public string supplier_city { get; set; }
            public string userid { get; set; }
        }
        public class cCrudSysSetting
        {
            public string sys_key { get; set; }
            public string sys_value { get; set; }
            public string ket { get; set; }
        }
        public class cCrudAreaDamage
        {
            public string area_damage_id { get; set; }
            public string area_damage_name { get; set; }
            public string userid { get; set; }
        }
        public class cCrudReason
        {
            public string reason_id { get; set; }
            public string area_damage_id { get; set; }
            public string area_damage_name { get; set; }
            public string reason_name { get; set; }
            public string userid { get; set; }
        }
        public class cCrudAxis
        {
            //public DateTime date_created { get; set; }
            public string axis_type { get; set; }
            public int number_of_tire { get; set; } //digunakan sebagai acuan ketika generate tire_no
            public string axis_no { get; set; }
            public string userid { get; set; }
        }
        public class cCrudAxisType
        {
            public string axis_type { get; set; }
            //public DateTime date_created { get; set; }
            public string axis_type_name { get; set; }
            public string userid { get; set; }
            public string data { get; set; }
            public string is_update { get; set; }
        }
        public class cCrudScriptSql
        {
            public string param { get; set; }
            
        }
        public class cCrudBrandVehicle
        {
            public string brand_vhc_id { get; set; }
            //public DateTime date_created { get; set; }
            public string brand_vhc_name { get; set; }
            public string userid { get; set; }
        }
        public class cCrudTireOnly
        {
            public string tire_id { get; set; }
            public string tire_code { get; set; }
            //public DateTime date_created { get; set; }
            public string tire_name { get; set; }
            public string size { get; set; }
            public string pattern { get; set; }
            public string brand_tire_id { get; set; }
            public string price { get; set; }
        }
        //public static string getNextRegisterNoTire(string year)
        //{
        //    string limit = "";
        //    DataTable dtTempTable = new DataTable("RFID_REGISTER");
        //    string StrQuery = "select register_no=[dbo].[NextRegisterNoTire](@year) ";
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = StrQuery;
        //    cmd.Connection = con;
        //    cmd.Parameters.AddWithValue("@year", year);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.SelectCommand.CommandTimeout = 0;
        //    con.Open();
        //    da.Fill(dtTempTable);
        //    con.Close();
        //    if (dtTempTable.Rows.Count > 0)
        //    {
        //        limit = (from DataRow dr in dtTempTable.Rows
        //                 select (string)dr["register_no"]).FirstOrDefault();
        //    }
        //    return limit;
        //}

        public static DataTable getCheckTireCodeStatus(string tire_code)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select * from ITEM where tire_code=@tire_code " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tire_code", tire_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        
        public static cRespon InsertSetupTire(Boolean is_new,cCrudSetupTire p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;

            DataTable tmpCheckTireCode = getCheckTireCodeStatus(p.tire_code);
            //if (tmpCheckTireCode.Rows.Count > 0 && is_new == true)
            //{
            //    foreach (DataRow dr in tmpCheckTireCode.Rows)
            //    {
            //        returnRes.flag = false;
            //        returnRes.message = "#Tire Code: " + p.tire_code + " already used. Tire Name: " + dr[1].ToString().Trim();
            //    }
            //}
            //else
            //{

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_code from ITEM where tire_code=@tire_code ) ) 
                                BEGIN 
                                    INSERT INTO ITEM 
                                 (tire_code,tire_name,tire_type_id, unique_code, otd,load_rating_single, load_rating_double, play_rating, standard_pressure_single,standard_pressure_double,size,pattern,brand_tire_id,supplier_id,date_created,price, is_delete, userid)
                                 values (@tire_code,@tire_name,@tire_type_id, @unique_code, @otd,@lr_single, @lr_double, @play_rating, @sp_single,@sp_double,@size,@pattern,@brand_tire_id, @supplier_id,@date_created,@price,0, @userid)
                                END
                                ELSE 
                                BEGIN
                                    UPDATE ITEM set is_delete=0, tire_name=@tire_name,tire_type_id=@tire_type_id, unique_code=@unique_code, otd=@otd,load_rating_single=@lr_single, load_rating_double=@lr_double, play_rating=@play_rating, standard_pressure_single=@sp_single,standard_pressure_double=@sp_double,size=@size,pattern=@pattern,brand_tire_id=@brand_tire_id, supplier_id=@supplier_id,price=@price,date_modified=@date_created, userid=@userid
                                    where tire_code=@tire_code
                                END
                                
                            ", new
                            {
                                p.tire_code,
                                p.tire_name,
                                p.size,
                                p.tire_type_id,
                                p.unique_code,
                                p.otd,
                                p.lr_single,
                                p.lr_double,
                                p.play_rating,
                                p.sp_single,
                                p.sp_double,
                                p.pattern,
                                p.brand_tire_id,
                                p.supplier_id,
                                date_created,
                                p.price,
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
                    returnRes.message = "Setup Tire succesfully. Tire Code: " + p.tire_code;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            //}
            return returnRes;
        }
        public static cRespon InsertAxis(cCrudAxis p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int max_tire_no = getMaxAxisId(p.axis_type);
            int number_of_tire = p.number_of_tire;
            int number_generate = max_tire_no + number_of_tire;
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;


           
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                
                if (number_of_tire % 2 == 0)
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            int tire_no = 1;
                            for (int sort = max_tire_no; sort < number_generate; sort++)
                            {

                                string axis_id = 'A' + (sort + 1).ToString();//getAxisId();
                                
                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 AXIS_id from axis where axis_id=@axis_id ) ) 
                            BEGIN 
                                INSERT INTO AXIS
                                (axis_id, axis_type,axis_no, tire_no, description, axis_name, is_delete,date_created, userid)
                                values (@axis_id, @axis_type, @axis_no, @tire_no, '', '',0,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE AXIS set axis_type=@axis_type, axis_no=@axis_no,tire_no=@tire_no ,date_modified=@date_created, userid=@userid
                                where axis_id=@axis_id
                            END
                                
                        ", new
                                {
                                    axis_id,
                                    p.axis_type,
                                    p.axis_no,
                                    date_created,
                                    tire_no,
                                    p.userid
                                }, tran);
                                tire_no++;

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
                else
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, number of tires must be even";
                }
                
                





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Axis succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        //public static cRespon DeleteAxis(cCrudAxis p)
        //{

        //    cRespon returnRes = new cRespon();
        //    returnRes.flag = true;
        //    returnRes.status = "";
        //    returnRes.message = "";
        //    string no_register = "";
        //    int RowAffected = 0;
        //    DateTime date_created = DateTime.Now;



        //    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
        //    {
        //        conn.Open();
        //        using (SqlTransaction tran = conn.BeginTransaction())
        //        {
        //            try
        //            {

        //                conn.Execute(@"IF (EXISTS (select TOP 1 axis_type from AXIS_TYPE where axis_type=@axis_type ) ) 
        //                    BEGIN

        //                        UPDATE AXIS_TYPE set is_delete=1,date_modified=@date_created, userid=@userid
        //                        where axis_type=@axis_type
        //                    END

        //                ", new
        //                {
        //                    p.axis_type,
        //                    p.axis_type_name,
        //                    date_created,
        //                    p.userid
        //                }, tran);

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





        //        if (returnRes.flag == true)
        //        {
        //            returnRes.message = "Setup Tire succesfully. Brand Code: " + p.brand_tire_id;
        //            returnRes.status = "success";
        //        }
        //        else
        //        {
        //            returnRes.status = "failed";
        //        }
        //    }
        //    return returnRes;
        //}
        public static DataTable getCheckAxisType(string axis_type)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select *  from AXIS_TYPE where axis_type=@axis_type " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@axis_type", axis_type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getCheckAxisTypeTireUse(string axis_type)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select *  from TIRE_AXIS where axis_id in (select axis_id from axis where axis_type=@axis_type) and is_delete=0 " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@axis_type", axis_type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        public static cRespon ScriptSql(cCrudScriptSql p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string jsonResult = "";
            string data_select = "";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();

                string queryType = p.param.Split(' ')[0].ToUpper();

                if (queryType == "SELECT")
                {
                    using (SqlCommand command = new SqlCommand(p.param, conn))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(command.ExecuteReader());

                        // Mengonversi DataTable menjadi JSON
                        jsonResult = JsonConvert.SerializeObject(dataTable);

                        // Sekarang, jsonResult berisi data dalam format JSON
                        Console.WriteLine(jsonResult);
                        // Anda dapat mengembalikan jsonResult sebagai respons API atau melakukan apa pun yang sesuai dengan kebutuhan Anda.
                    }
                }
                else if (queryType == "UPDATE" || queryType == "DELETE")
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            SqlCommand command = new SqlCommand(p.param, conn, tran);
                            int rowsAffected = command.ExecuteNonQuery();
                            tran.Commit();

                            jsonResult = rowsAffected + " rows affected.";
                        }
                        catch (Exception e)
                        {
                            tran.Rollback();
                            returnRes.flag = false;
                            returnRes.message = "# Failed, " + e.Message.ToString();
                        }
                    }
                }

                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = jsonResult;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        //public static cRespon ScriptSql(cCrudScriptSql p)
        //{
        //    cRespon returnRes = new cRespon();
        //    returnRes.flag = true;
        //    returnRes.status = "";
        //    returnRes.message = "";
        //    string jsonResult = "";
        //    string data_select = "";
        //    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
        //    {
        //        conn.Open();

        //        string queryType = p.param.Split(' ')[0].ToUpper();
        //        if (queryType == "SELECT")
        //        {
        //            using (SqlCommand command = new SqlCommand(p.param, conn))
        //            {
        //                DataTable dataTable = new DataTable();
        //                dataTable.Load(command.ExecuteReader());

        //                // Mengonversi DataTable menjadi JSON
        //               jsonResult = JsonConvert.SerializeObject(dataTable);

        //                // Sekarang, jsonResult berisi data dalam format JSON
        //                Console.WriteLine(jsonResult);

        //                // Anda dapat mengembalikan jsonResult sebagai respons API atau melakukan apa pun yang sesuai dengan kebutuhan Anda.
        //            }
        //        }
        //        else if (queryType == "UPDATE" || queryType == "DELETE")
        //        {
        //            using (SqlTransaction tran = conn.BeginTransaction())
        //            {
        //                try
        //                {
        //                    conn.Execute(@"" + p.param + "", new
        //                    {
        //                        p.param
        //                    }, tran);
        //                    tran.Commit();
        //                }
        //                catch (Exception e)
        //                {
        //                    tran.Rollback();
        //                    returnRes.flag = false;
        //                    returnRes.message = "# Failed, " + e.Message.ToString();
        //                }
        //            }
        //        }




        //        conn.Close();
        //    }

        //    if (returnRes.flag == true)
        //    {
        //        returnRes.message = jsonResult;
        //        returnRes.status = "success";
        //    }
        //    else
        //    {
        //        returnRes.status = "failed";
        //    }
        //    return returnRes;
        //}
        public static cRespon InsertAxisType(cCrudAxisType p)
        {
            List<cCrudListAxis> ListAxis = new List<cCrudListAxis>();
            ListAxis = JsonConvert.DeserializeObject<List<cCrudListAxis>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date = DateTime.Now;
            string date_created = date.ToString("yyyy-MM-dd");
            if (p.is_update == "false" || p.is_update == "" || p.is_update == "null" || p.is_update == null)
            {
                DataTable CheckAxis = getCheckAxisType(p.axis_type);
                if (CheckAxis.Rows.Count != 0)
                {
                    foreach (DataRow dr in CheckAxis.Rows)
                    {
                        returnRes.flag = false;
                        returnRes.message = "Axis type " + p.axis_type + " already exist.";
                    }
                }
                else
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 axis_type from AXIS_TYPE where axis_type=@axis_type ) ) 
                                BEGIN 
                                    INSERT INTO AXIS_TYPE 
                                    (axis_type,axis_type_name, is_delete,date_created, userid)
                                    values (@axis_type,@axis_type_name,0,@date_created, @userid)
                                END
                                ELSE 
                                BEGIN
                                    UPDATE AXIS_TYPE set is_delete=0, axis_type_name=@axis_type_name,date_modified=@date_created, userid=@userid
                                    where axis_type=@axis_type
                                END
                                
                            ", new
                                {
                                    p.axis_type,
                                    p.axis_type_name,
                                    date_created,
                                    p.userid
                                }, tran);
                                int no_axis_id = 1;
                                string axis_id = GetNextAxisId();
                                string numericString = new string(axis_id.Where(char.IsDigit).ToArray());
                                int number = int.Parse(numericString);
                                foreach (var dr in ListAxis)
                                {
                                    int tire_no = 1;
                                    for (int sort = 0; sort < dr.number_of_tire; sort++)
                                    {
                                        //string axis_id = GetNextAxisId();
                                        //getAxisId();
                                        number++;
                                        conn.Execute(@"

                                         INSERT INTO AXIS
                                            (axis_id, axis_type,axis_no, tire_no, description, axis_name, is_delete,date_created, userid)
                                            values (@axis_id, @axis_type, @axis_no, @tire_no, '', '',0,@date_created, @userid)

                                
                                    ", new
                                        {
                                            axis_id,
                                            p.axis_type,
                                            dr.axis_no,
                                            date_created,
                                            tire_no,
                                            p.userid
                                        }, tran);
                                        tire_no++;

                                        axis_id = 'A' + number.ToString();
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
                }
            }
            else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute(@"
                                    UPDATE AXIS_TYPE set is_delete=0, axis_type_name=@axis_type_name,date_modified=@date_created, userid=@userid
                                    where axis_type=@axis_type
                                
                                
                            ", new
                            {
                                p.axis_type,
                                p.axis_type_name,
                                date_created,
                                p.userid
                            }, tran);
                            tran.Commit();
                        }
                        catch (Exception e) {
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
                returnRes.message = "Setup Axis type succesfully. Axis Type: " + p.axis_type;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon DeleteAxisType(cCrudAxisType p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;


            DataTable CheckAxis = getCheckAxisTypeTireUse(p.axis_type);
            if (CheckAxis.Rows.Count != 0)
            {
                foreach (DataRow dr in CheckAxis.Rows)
                {
                    returnRes.flag = false;
                    returnRes.message = "Axis type " + p.axis_type + " already use.";
                }
            }
            else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (EXISTS (select TOP 1 axis_type from AXIS_TYPE where axis_type=@axis_type ) ) 
                            BEGIN

                                UPDATE AXIS_TYPE set is_delete=1,date_modified=@date_created, userid=@userid
                                where axis_type=@axis_type
                            END
                                
                        ", new
                            {
                                p.axis_type,
                                p.axis_type_name,
                                date_created,
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





                    if (returnRes.flag == true)
                    {
                        returnRes.message = "Setup Axis Type deleted. Brand Code: " + p.axis_type;
                        returnRes.status = "success";
                    }
                    else
                    {
                        returnRes.status = "failed";
                    }
                }
            }
            
            return returnRes;
        }
        public static cRespon InsertLocation(cCrudLocation p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;

            

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 location_id from LOCATION where location_id=@location_id ) ) 
                            BEGIN 
                                INSERT INTO LOCATION 
                                (location_id, location_type_id,location, parent, [group], level, sort, area, detail, process_area, is_delete,date_created, userid)
                                values (@location_id,@location_type_id,@location, @parent, @group, @level, @sort, @area, @detail, @process_area,0,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE LOCATION set location=@location, is_delete=0, location_type_id = @location_type_id, parent=@parent,[group]=@group, level=@level, sort=@sort, area=@area, detail=@detail,process_area=@process_area ,date_modified=@date_created, userid=@userid
                                where location_id=@location_id
                            END
                                
                        ", new
                        {
                            p.location_id,
                            p.location,
                            date_created,
                            p.parent,
                            p.group,
                            p.location_type_id,
                            p.level,
                            p.sort,
                            p.area,
                            p.detail,
                            p.process_area,
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
                




                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Location succesfully. Location ID: " + p.location_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteLocation(cCrudLocation p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 location_id from LOCATION where location_id=@location_id ) ) 
                            BEGIN
                                UPDATE LOCATION set is_delete=1,date_modified=@date_created, userid=@userid
                                where location_id=@location_id
                            END
                                
                        ", new
                        {
                            p.location_id,
                            p.location,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Location succesfully. Location ID: " + p.location_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertAreaDamage(cCrudAreaDamage p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 area_damage_id from area_damage where area_damage_id=@area_damage_id ) ) 
                            BEGIN 
                                INSERT INTO area_damage 
                                (area_damage_id, area_damage_name,is_delete,date_created, date_modified, userid)
                                values (@area_damage_id, @area_damage_name,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE area_damage set area_damage_name=@area_damage_name,date_modified=@date_created, is_delete=0, userid=@userid
                                where area_damage_id=@area_damage_id
                            END
                            
                            IF (NOT EXISTS (select TOP 1 area_damage_id from inspect_area_damage where area_damage_id=@area_damage_id and  inspect_setup_id='8') ) 
                            BEGIN 
                                INSERT INTO inspect_area_damage 
                                (inspect_setup_id, area_damage_id,is_delete,date_created, date_modified, userid)
                                values ('8', @area_damage_id,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE inspect_area_damage set date_modified=@date_created, is_delete=0, userid=@userid
                                where area_damage_id=@area_damage_id and inspect_setup_id='8'
                            END

                            IF (NOT EXISTS (select TOP 1 area_damage_id from inspect_area_damage where area_damage_id=@area_damage_id and  inspect_setup_id='4') ) 
                            BEGIN 
                                INSERT INTO inspect_area_damage 
                                (inspect_setup_id, area_damage_id,is_delete,date_created, date_modified, userid)
                                values ('4', @area_damage_id,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE inspect_area_damage set date_modified=@date_created, is_delete=0, userid=@userid
                                where area_damage_id=@area_damage_id and inspect_setup_id='4'
                            END
                        ", new
                        {
                            p.area_damage_name,
                            p.area_damage_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Area Damage succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteAreaDamage(cCrudAreaDamage p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 area_damage_id from area_damage where area_damage_id=@area_damage_id ) ) 
                            BEGIN
                                UPDATE area_damage set is_delete=1,date_modified=@date_created, userid=@userid
                                where area_damage_id=@area_damage_id
                            END

                            IF (EXISTS (select TOP 1 area_damage_id from inspect_area_damage where area_damage_id=@area_damage_id ) ) 
                            BEGIN
                                UPDATE inspect_area_damage set is_delete=1,date_modified=@date_created, userid=@userid
                                where area_damage_id=@area_damage_id and inspect_setup_id='8'
                            END
                                
                        ", new
                        {
                            p.area_damage_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Area Damage succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertReason(cCrudReason p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 reason_id from reason where reason_id=@reason_id ) ) 
                            BEGIN 
                                INSERT INTO reason 
                                (reason_id, reason_group_id, reason_group,reason_name,is_delete,date_created,date_modified, userid)
                                values (@reason_id, @area_damage_id, @area_damage_name, @reason_name,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE reason set reason_name=@reason_name, reason_group_id=@area_damage_id, reason_group=@area_damage_name,date_modified=@date_created, is_delete=0, userid=@userid
                                where reason_id=@reason_id
                            END
                            
                            IF (NOT EXISTS (select TOP 1 reason_id from inspect_reason where reason_id=@reason_id and inspect_setup_id='8') ) 
                            BEGIN 
                                INSERT INTO inspect_reason 
                                (inspect_setup_id, reason_id,is_delete,date_created,date_modified, userid)
                                values ('8', @reason_id,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE inspect_reason set date_modified=@date_created, is_delete=0, userid=@userid
                                where reason_id=@reason_id and inspect_setup_id='8'
                            END


                            IF (NOT EXISTS (select TOP 1 reason_id from inspect_reason where reason_id=@reason_id and inspect_setup_id='4' ) ) 
                            BEGIN 
                                INSERT INTO inspect_reason 
                                (inspect_setup_id, reason_id,is_delete,date_created,date_modified, userid)
                                values ('4', @reason_id,0,@date_created,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE inspect_reason set date_modified=@date_created, is_delete=0, userid=@userid
                                where reason_id=@reason_id and inspect_setup_id='4'
                            END
                        ", new
                        {
                            p.reason_id,
                            p.reason_name,
                            p.area_damage_id,
                            p.area_damage_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Reason succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteReason(cCrudReason p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 reason_id from reason where reason_id=@reason_id ) ) 
                            BEGIN
                                UPDATE reason set is_delete=1,date_modified=@date_created, userid=@userid
                                where reason_id=@reason_id
                            END

                            IF (EXISTS (select TOP 1 reason_id from inspect_reason where reason_id=@reason_id ) ) 
                            BEGIN
                                UPDATE inspect_reason set is_delete=1,date_modified=@date_created, userid=@userid
                                where reason_id=@reason_id and inspect_setup_id='8'
                            END
                                
                        ", new
                        {
                            p.reason_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Reason succesfully. Reason Id: " + p.reason_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertEmployee(cCrudEmployee p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 EMP_ID from EMPLOYEES where EMP_ID=@emp_id ) ) 
                            BEGIN 
                                INSERT INTO EMPLOYEES 
                                (EMP_ID, DEPARTMENT_ID, EMP_NAME,EMP_ADDRESS,EMP_POS_CODE,EMP_PHONE,EMP_EMAIL,EMP_IS_ACTIVE,DATE_CREATED,DATE_MODIFIED,USERID,IS_DELETE)
                                values (@emp_id, @department_id,@emp_name,@emp_address,@emp_pos_code,@emp_phone,@emp_email,@emp_is_active,@date_created,@date_created,@userid,0)
                            END
                            ELSE 
                            BEGIN
                                UPDATE EMPLOYEES set department_id=@department_id, is_delete=0, emp_name=@emp_name, emp_address = @emp_address, emp_pos_code=@emp_pos_code,emp_phone=@emp_phone, emp_email=@emp_email, emp_is_active=@emp_is_active,date_modified=@date_created, userid=@userid
                                where emp_id=@emp_id
                            END
                                
                        ", new
                        {
                            p.emp_id,
                            p.department_id,
                            p.emp_name,
                            p.emp_address,
                            p.emp_phone,
                            p.emp_pos_code,
                            p.emp_email,
                            p.emp_is_active,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Employee succesfully. Employee ID: " + p.emp_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon InsertDepartment(cCrudDepartment p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 DEPARTMENT_ID from DEPARTMENT where DEPARTMENT_ID=@department_id ) ) 
                            BEGIN 
                                INSERT INTO DEPARTMENT 
                                (DEPARTMENT_ID, DEPARTMENT_NAME,DATE_CREATED,DATE_MODIFIED,USERID,IS_DELETE)
                                values (@department_id,@department_name,@date_created,@date_created,@userid,0)
                            END
                            ELSE 
                            BEGIN
                                UPDATE DEPARTMENT set  department_name=@department_name, date_modified=@date_created, is_delete=0, userid=@userid
                                where department_id=@department_id
                            END
                                
                        ", new
                        {
                            p.department_id,
                            p.department_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Department succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteDepartment(cCrudDepartment p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                UPDATE DEPARTMENT set is_delete=1, date_modified=@date_created, userid=@userid
                                where department_id=@department_id
                                
                        ", new
                        {
                            p.department_id,
                            p.department_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Department succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon InsertGroups(cCrudGroups p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 GROUP_ID from GROUPS where group_id=@group_id ) ) 
                            BEGIN 
                                INSERT INTO GROUPS 
                                (group_id,location_id, GROUP_NAME,DATE_CREATED,DATE_MODIFIED,USERID,IS_DELETE)
                                values (@group_id,@location_id,@group_name,@date_created,@date_created,@userid,0)
                            END
                            ELSE 
                            BEGIN
                                UPDATE GROUPS set is_delete=0, group_name=@group_name,location_id=@location_id, date_modified=@date_created, userid=@userid
                                where group_id=@group_id
                            END
                                
                        ", new
                        {
                            p.group_id,
                            p.group_name,
                            p.location_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Group succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteGroups(cCrudGroups p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                UPDATE GROUPS set is_delete=1, date_modified=@date_created, userid=@userid
                                where group_id=@group_id
                                
                        ", new
                        {
                            p.group_id,
                            p.group_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Group succesfully.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteEmployee(cCrudEmployee p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 emp_id from employees where emp_id=@emp_id ) ) 
                            BEGIN
                                UPDATE employees set is_delete=1,date_modified=@date_created, userid=@userid
                                where emp_id=@emp_id
                            END
                                
                        ", new
                        {
                            p.emp_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Employee succesfully. Employee Id : " + p.emp_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertPosition(cCrudPosition p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 position_id from POSITIONS where position_id=@position_id ) ) 
                            BEGIN 
                                INSERT INTO POSITIONS 
                                (position_id, position_name,is_delete,date_created,date_modified,userid)
                                values (@position_id, @position_name,0,@date_created,@date_created,@userid)
                            END
                            ELSE 
                              
                            BEGIN
                                UPDATE POSITIONS set position_name=@position_name,date_modified=@date_created, is_delete=0, userid=@userid
                                where position_id=@position_id
                            END
                        ", new
                        {
                            p.position_id,
                            p.position_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Set Position.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeletePosition(cCrudPosition p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 position_id from positions where POSITION_id=@position_id ) ) 
                            BEGIN
                                UPDATE POSITIONS set is_delete=1,date_modified=@date_created, userid=@userid
                                where position_id=@position_id
                            END
                                
                        ", new
                        {
                            p.position_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Position success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertTeamSchEmp(cCrudTeamSchEmpHeader p)
        {

            List<cCrudTeamSchEmpDetail> ListDetail = new List<cCrudTeamSchEmpDetail>();
            ListDetail = JsonConvert.DeserializeObject<List<cCrudTeamSchEmpDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;


            
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListDetail)
                        {
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 tsch_id from TSCH_EMPLOYEES where tsch_id=@tsch_id and emp_id=@emp_id ) ) 
                            BEGIN 
                                INSERT INTO TSCH_EMPLOYEES 
                                (emp_id, tsch_id, position_id,is_delete,date_created,date_modified,userid)
                                values (@emp_id, @tsch_id, @position_id,0,@date_created,@date_created,@userid)
                            END
                            ELSE
                            BEGIN
                                UPDATE TSCH_EMPLOYEES set position_id=@position_id, date_modified=@date_created where tsch_id = @tsch_id and emp_id=@emp_id
                            END
                        ", new
                            {
                                dr.position_id,
                                p.tsch_id,
                                dr.emp_id,
                                p.date_created,
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
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Set Team Schedule Employees.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteTeamSchEmp(cCrudTeamSchEmp p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 tsch_id from TSCH_EMPLOYEES where tsch_id=@tsch_id ) ) 
                            BEGIN
                                UPDATE TSCH_EMPLOYEES set is_delete=1,date_modified=@date_created, userid=@userid
                                where tsch_id=@tsch_id
                            END
                                
                        ", new
                        {
                            p.tsch_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Team Schedule Employees success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static string CekLocationSchedule(string location_id, string shift_id, string date_created)
        {
            string reponse = "";
            DataTable dtTempTable = new DataTable("AXIS");
            string StrQuery = @"select * from TEAM_SCHEDULE where location_id=@location_id and shift=@shift_id and cast(date_created as date)=@date_created and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@location_id", location_id);
            cmd.Parameters.AddWithValue("@shift_id", shift_id);
            cmd.Parameters.AddWithValue("@date_created", date_created);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                reponse = "Schedule already taken.";
            }
            return reponse;
        }
        public static cRespon InsertTeamSchedule(cCrudTeamSchedule p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");
            

            if (p.tsch_id == "")
            {
                string cekSchedule = CekLocationSchedule(p.location_id, p.shift, p.date_created);
                string scheduleId = getScheduleId(yearmonth);
                
                if (cekSchedule == "")
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {

                                conn.Execute(@"
                                INSERT INTO TEAM_SCHEDULE 
                                (tsch_id, location_id, shift, date_start, date_end,is_delete,date_created,date_modified,userid)
                                values (@scheduleId, @location_id, @shift, @date_start, @date_end,0,@date_created,@date_created,@userid)
                            
                                
                        ", new
                                {
                                    p.location_id,
                                    p.tsch_id,
                                    p.shift,
                                    scheduleId,
                                    p.date_start,
                                    p.date_end,
                                    p.date_created,
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





                        if (returnRes.flag == true)
                        {
                            returnRes.message = "Setup Set Team Schedule.";
                            returnRes.status = "success";
                        }
                        else
                        {
                            returnRes.status = "failed";
                        }
                    }
                }
                else
                {
                    returnRes.flag = false;
                    returnRes.message = cekSchedule;
                    returnRes.status = "failed";
                }
            }
            else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"
                                UPDATE TEAM_SCHEDULE set location_id = @location_id, shift = @shift, date_start=@date_start, date_end=@date_end, date_modified=@date_created, userid=@userid
                                where tsch_id = @tsch_id
                                
                        ", new
                            {
                                p.location_id,
                                p.tsch_id,
                                p.shift,
                                p.date_start,
                                p.date_end,
                                p.date_created,
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





                    if (returnRes.flag == true)
                    {
                        returnRes.message = "Setup Set Team Schedule.";
                        returnRes.status = "success";
                    }
                    else
                    {
                        returnRes.status = "failed";
                    }
                }
            }
            

           
            return returnRes;
        }
        public static bool checkIsLoginUser(string user_login, int is_logout)
        {
            bool row = false;
            DateTime date = DateTime.Now;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select * from HISTORY_LOGIN where @date BETWEEN history_login_date AND history_login_date_expired and user_login = @user_login and is_logout=@is_logout";
            //QUERY YANG MENYERTAKAN DURASI TOKEN AUTORISASI select * from HISTORY_LOGIN where user_login = 'admin@nci.co.id' and '2023-11-16 14:44:18' BETWEEN history_login_date AND history_login_date_expired and is_logout=0
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@user_login", user_login);
            cmd.Parameters.AddWithValue("@is_logout", is_logout);
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
        public static cRespon UpdateLogin(cCrudHistoryLogin p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                   UPDATE HISTORY_LOGIN set is_logout=1 where cast(history_login_date as date) = @histori_login_date and user_login=@user_login
                                ", new
                        {
                            p.histori_login_date,
                            p.user_login
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

                if (returnRes.flag == true)
                {
                    returnRes.message = "";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static int getTimeoutSys()
        {
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
            return data;

        }
        public static cRespon HistoryLogin(cCrudHistoryLogin p)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            DateTime date_created = DateTime.Now;
            DateTime date_expired = DateTime.Now.AddMinutes(getTimeoutSys());
            string yearmonth = date_created.ToString("yyyyMM");
            
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                if (checkIsLoginUser(p.user_login, 0) == true)
                {//CEK IS LOGOUT FALSE
                    returnRes.flag = false;
                    returnRes.message = "# Failed, Access denied. You are detected still logged in now.";
                }
                else
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            //INSERT INTO HISTORY_LOGIN(history_login_date, user_login, histori_login_date_expired, is_logout)
                            //        values(@date_created, @user_login, @date_expired, 0)
                            conn.Execute(@"
                                   INSERT INTO HISTORY_LOGIN (history_login_date, user_login, history_login_date_expired, is_logout)
                                    values (@date_created, @user_login, @date_expired, 0)
                                ", new
                            {
                                date_created,
                                date_expired,
                                p.user_login
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
                }
                
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertGroupEmployee(cCrudGroupEmployee p)
        {
            List<cCrudGroupEmployeeDetail> ListDetail = new List<cCrudGroupEmployeeDetail>();
            ListDetail = JsonConvert.DeserializeObject<List<cCrudGroupEmployeeDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListDetail)
                        {
                            conn.Execute(@"
                                    IF (NOT EXISTS (select TOP 1 * from GROUPS_EMPLOYEES where  emp_id=@emp_id and group_id=@group_id ) ) 
                                    BEGIN
                                        INSERT INTO GROUPS_EMPLOYEES 
                                        (emp_id, group_id, position_id ,is_delete,date_created,date_modified,userid)
                                        values (@emp_id, @group_id, @position_id ,0,@date_created,@date_created,@userid)
                            
                                    END
                                    ELSE
                                    BEGIN
                                        UPDATE GROUPS_EMPLOYEES SET is_delete=0, position_id=@position_id, date_modified=@date_created, userid=@userid where emp_id=@emp_id and group_id=@group_id
                                    END
                                ", new
                            {
                                dr.position_id,
                                dr.emp_id,
                                p.group_id,
                                p.date_created,
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
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Set Group Employee.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteGroupEmployee(cCrudGroupEmployee p)
        {

            List<cCrudGroupEmployeeDetail> ListDetail = new List<cCrudGroupEmployeeDetail>();
            ListDetail = JsonConvert.DeserializeObject<List<cCrudGroupEmployeeDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        string queryDelEmployee = "";
                        if (p.flag == "1") {
                            queryDelEmployee = " and emp_id=@emp_id";
                        }
                        foreach (var dr in ListDetail)
                        {
                            conn.Execute(@"
                                    UPDATE GROUPS_EMPLOYEES set is_delete=1,date_modified=@date_created, userid=@userid
                                    where group_id=@group_id
                            
                                
                            "+ queryDelEmployee, new
                            {
                                dr.emp_id,
                                p.group_id,
                                date_created,
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
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete group employee success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon InsertSchedule(cCrudSchedule p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");
            int cekDataSch = cekDataScheduleNoIsDelete(p.year, p.month, p.date, p.location_id, p.shift_id, p.group_id);
            if (cekDataSch == 0)
            {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"
                                    INSERT INTO SCHEDULES 
                                    (year, month, date, location_id, group_id, shift_id, status ,is_delete,date_created,date_modified,userid)
                                    values (@year, @month, @date, @location_id, @group_id, @shift_id, 1 ,0,@date_created,@date_created,@userid)
                            
                                
                            ", new
                            {
                                p.location_id,
                                p.year,
                                p.month,
                                p.date,
                                p.shift_id,
                                p.group_id,
                                p.date_created,
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





                    if (returnRes.flag == true)
                    {
                        returnRes.message = "Setup Set Schedule.";
                        returnRes.status = "success";
                    }
                    else
                    {
                        returnRes.status = "failed";
                    }
                }
            }
            else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"
                                    update SCHEDULES set group_id=@group_id, is_delete=0, date_modified=@date_created, userid=@userid where year=@year and month= @month and date=@date and location_id=@location_id and shift_id=@shift_id
                                  
                                
                            ", new
                            {
                                p.location_id,
                                p.year,
                                p.month,
                                p.date,
                                p.shift_id,
                                p.group_id,
                                p.date_created,
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





                    if (returnRes.flag == true)
                    {
                        returnRes.message = "Setup Set Schedule.";
                        returnRes.status = "success";
                    }
                    else
                    {
                        returnRes.status = "failed";
                    }
                }
            }
            



            return returnRes;
        }
        public static cRespon DeleteSchedule(cCrudSchedule p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"
                                UPDATE SCHEDULEs set is_delete=1,date_modified=@date_created, userid=@userid
                                where year=@year and month=@month and date=@date and location_id=@location_id and shift_id=@shift_id
                            
                                
                        ", new
                        {
                            p.year,
                            p.month,
                            p.date,
                            p.location_id,
                            p.shift_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Schedule success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteTeamSchedule(cCrudTeamSchedule p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 tsch_id from TEAM_SCHEDULE where tsch_id=@tsch_id ) ) 
                            BEGIN
                                UPDATE TEAM_SCHEDULE set is_delete=1,date_modified=@date_created, userid=@userid
                                where tsch_id=@tsch_id
                            END
                                
                        ", new
                        {
                            p.tsch_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete Team Schedule success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertUserEmployee(cCrudUsrEmployeeInsert p)
        {

            List<cCrudUsrEmployeeInsertDetail> ListDetail = new List<cCrudUsrEmployeeInsertDetail>();
            ListDetail = JsonConvert.DeserializeObject<List<cCrudUsrEmployeeInsertDetail>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListDetail)
                        {
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 userid from USER_EMPLOYEES where userid=@userid) ) 
                            BEGIN 
                                INSERT INTO USER_EMPLOYEES 
                                (userid, emp_id, is_delete, date_created, date_modified)
                                values (@userid, @emp_id, 0, @date_created, @date_created)
                            END
                            ELSE 
                            BEGIN
                                UPDATE USER_EMPLOYEES set is_delete=0, emp_id=@emp_id, date_modified=@date_created
                                where userid=@userid
                            END
                                
                        ", new
                            {
                                dr.emp_id,
                                p.date_created,
                                dr.userid
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup User Employee success";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteUserEmployee(cCrudUsrEmployeeDelete p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (EXISTS (select TOP 1 emp_id from USER_EMPLOYEES where emp_id=@emp_id and userid=@userid ) ) 
                            BEGIN
                                UPDATE USER_EMPLOYEES set is_delete=1,date_modified=@date_created
                                where emp_id=@emp_id and userid=@userid
                            END
                                
                        ", new
                        {
                            p.emp_id,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Delete User Employee success. Employee Id : " + p.emp_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertSupplier(cCrudSupplier p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 supplier_id from supplier where supplier_id=@supplier_id ) ) 
                            BEGIN 
                                INSERT INTO supplier 
                                (supplier_id,supplier_name,supplier_address,supplier_city, is_delete,date_created, userid)
                                values (@supplier_id,@supplier_name,@supplier_address,@supplier_city,0,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE supplier set supplier_name=@supplier_name,supplier_address=@supplier_address, supplier_city=@supplier_city,date_modified=@date_created, is_delete=0, userid=@userid
                                where supplier_id=@supplier_id
                            END
                                
                        ", new
                        {
                            p.supplier_id,
                            p.supplier_name,
                            p.supplier_city,
                            p.supplier_address,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Supplier succesfully. Supplier Code: " + p.supplier_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteSupplier(cCrudSupplier p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool checkSupplierUsed = checkSupplierTyreUsed(p.supplier_id);
                if (checkSupplierUsed == false)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (EXISTS (select TOP 1 supplier_id from supplier where supplier_id=@supplier_id ) ) 
                                BEGIN
                                    UPDATE supplier set is_delete=1,date_modified=@date_created, userid=@userid
                                    where supplier_id=@supplier_id
                                END
                                
                            ", new
                            {
                                p.supplier_id,
                                p.supplier_name,
                                p.supplier_city,
                                p.supplier_address,
                                date_created,
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
                }
                else
                {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The supplier is already in use.";
                }

                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Supplier succesfully. Supplier Code: " + p.supplier_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon InsertSysSetting(cCrudSysSetting p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 sys_key from sys_settings where sys_key=@sys_key ) ) 
                            BEGIN 
                                INSERT INTO sys_settings 
                                (sys_key,sys_value,ket)
                                values (@sys_key,@sys_value,@ket)
                            END
                            ELSE 
                            BEGIN
                                UPDATE sys_settings set sys_value=@sys_value,ket=@ket
                                where sys_key=@sys_key
                            END
                                
                        ", new
                        {
                            p.sys_key,
                            p.sys_value,
                            p.ket
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup System Setting success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteSysSetting(cCrudSysSetting p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"delete from sys_settings where sys_key=@sys_key
                                
                        ", new
                        {
                            p.sys_key,
                            p.sys_value,
                            p.ket
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup System Setting success.";
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon InsertBrandTire(cCrudBrandTire p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 brand_tire_id from BRAND_TIRE where brand_tire_id=@brand_tire_id ) ) 
                            BEGIN 
                                INSERT INTO BRAND_TIRE 
                                (brand_tire_id,brand_tire_name, is_delete,date_created, userid)
                                values (@brand_tire_id,@brand_tire_name,0,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE BRAND_TIRE set brand_tire_name=@brand_tire_name,date_modified=@date_created, is_delete=0, userid=@userid
                                where brand_tire_id=@brand_tire_id
                            END
                                
                        ", new
                        {
                            p.brand_tire_id,
                            p.brand_tire_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Brand Tire succesfully. Brand Code: " + p.brand_tire_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteBrandTire(cCrudBrandTire p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool checkBrandUsed = checkBrandTyreUsed(p.brand_tire_id);
                if (checkBrandUsed == false)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (EXISTS (select TOP 1 brand_tire_id from BRAND_TIRE where brand_tire_id=@brand_tire_id ) ) 
                                BEGIN
                                    UPDATE BRAND_TIRE set is_delete=1,date_modified=@date_created, userid=@userid
                                    where brand_tire_id=@brand_tire_id
                                END
                                
                            ", new
                            {
                                p.brand_tire_id,
                                p.brand_tire_name,
                                date_created,
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
                }else {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The brand is already in use.";
                }
            
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Tire succesfully. Brand Code: " + p.brand_tire_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static bool checkAxisTypeUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=axis_type from axis where axis_type=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static bool checkAxisTyreUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=axis_id from tire_axis where axis_id=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static bool checkItemTyreUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=tire_code from tire where tire_code=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static bool checkSupplierTyreUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=supplier_id from item where supplier_id=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static bool checkBrandTyreUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=brand_tire_id from item where brand_tire_id=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static bool checkBrandVehicleUsed(string id)
        {
            bool row = false;
            DataTable dtTempTable = new DataTable("TIRE");
            string StrQuery = "select data=brand_vhc_id from vehicle where brand_vhc_id=@id and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@id", id);
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
        public static cRespon InsertBrandVehicle(cCrudBrandVehicle p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {

                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 brand_vhc_id from BRAND_VEHICLE where brand_vhc_id=@brand_vhc_id ) ) 
                            BEGIN 
                                INSERT INTO BRAND_VEHICLE 
                                (brand_vhc_id,brand_vhc_name, is_delete,date_created, userid)
                                values (@brand_vhc_id,@brand_vhc_name,0,@date_created, @userid)
                            END
                            ELSE 
                            BEGIN
                                UPDATE BRAND_VEHICLE set brand_vhc_name=@brand_vhc_name,date_modified=@date_created, is_delete=0, userid=@userid
                                where brand_vhc_id=@brand_vhc_id
                            END
                                
                        ", new
                        {
                            p.brand_vhc_id,
                            p.brand_vhc_name,
                            date_created,
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





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Brand Vehicle succesfully. Brand Code: " + p.brand_vhc_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static cRespon DeleteBrandVehicle(cCrudBrandVehicle p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;



            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                bool checkBrandUsed = checkBrandVehicleUsed(p.brand_vhc_id);
                if (checkBrandUsed == false)
                {
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            conn.Execute(@"IF (EXISTS (select TOP 1 brand_vhc_id from BRAND_VEHICLE where brand_vhc_id=@brand_vhc_id ) ) 
                                BEGIN
                                    UPDATE BRAND_VEHICLE set is_delete=1,date_modified=@date_created, userid=@userid
                                    where brand_vhc_id=@brand_vhc_id
                                END
                                
                            ", new
                            {
                                p.brand_vhc_id,
                                p.brand_vhc_name,
                                date_created,
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
                }
                else {
                    returnRes.flag = false;
                    returnRes.message = "# Failed, The brand is already in use.";
                }
                
                conn.Close();





                if (returnRes.flag == true)
                {
                    returnRes.message = "Setup Tire succesfully. Brand Code: " + p.brand_vhc_id;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static DataTable getCheckTireItemRecord(string tire_code)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select r.register_tire_no, r.tire_id from REGISTER_TIRE R inner join tire t on r.tire_id=t.tire_id where tire_code=@tire_code " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@tire_code", tire_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        public static cRespon DeleteTireItem(cCrudSetupTire p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;

            DataTable tmpCheckTireCode = getCheckTireItemRecord(p.tire_code);
            if (tmpCheckTireCode.Rows.Count > 0)
            {
                foreach (DataRow dr in tmpCheckTireCode.Rows)
                {
                    returnRes.flag = false;
                    returnRes.message = "#Tire Code: " + p.tire_code + " already Registered. Register No: " + dr[0].ToString().Trim() + " , etc...";
                }
            }
            else
            {

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {

                            

                            conn.Execute(@" 
                                BEGIN 
                                    update TIRE set is_delete='1', date_modified=@date_created, userid=@userid where tire_code=@tire_code
                                END 
                            ", new
                            {
                                p.tire_code,
                                p.userid,
                                date_created
                            }, tran);

                            conn.Execute(@" 
                                BEGIN 
                                    update ITEM set is_delete='1', date_modified=@date_created, userid=@userid where tire_code=@tire_code
                                END 
                            ", new
                            {
                                p.tire_code,
                                p.userid,
                                date_created
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
                    returnRes.message = "Delete Tire succesfully. Tire Code: " + p.tire_code;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }

        public static cRespon DeleteTirePerSerial(cCrudTireOnly p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            DateTime date_created = DateTime.Now;

            DataTable tmpCheckTireCode = getCheckTireItemRecord(p.tire_code);
            if (tmpCheckTireCode.Rows.Count > 0)
            {
                foreach (DataRow dr in tmpCheckTireCode.Rows)
                {
                    returnRes.flag = false;
                    returnRes.message = "#Tire Code: " + p.tire_code + " already Registered. Register No: " + dr[0].ToString().Trim() + " , etc...";
                }
            }
            else
            {

                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {



                            conn.Execute(@" 
                                BEGIN 
                                    update TIRE set is_delete='1', date_modified=@date, userid=@userid where tire_code=@tire_code and tire_id=@tire_id
                                END 
                            ", new
                            {
                                p.tire_code,
                                p.tire_id
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
                    returnRes.message = "Delete Tire succesfully. Tire Code: " + p.tire_code;
                    returnRes.status = "success";
                }
                else
                {
                    returnRes.status = "failed";
                }
            }
            return returnRes;
        }
        public static DataTable getDataLogUser(string StartDate, string EndDate)
        {
            DataTable dtJoint = new DataTable("ZLOG_USER");
            string StrQuery = "SELECT CONVERT(VARCHAR,LU.DATE,106) AS DATE, LU.TIME, LU.ID, LU.ACTIVITY, LU.IP_ADDRESS, A.NAME, A.PHONE, A.JOBPOSITION,EMAIL" + System.Environment.NewLine;
            StrQuery += "FROM ZLOG_USER AS LU " + System.Environment.NewLine;
            StrQuery += "INNER JOIN ASPNETUSERS A ON A.ID=LU.Id" + System.Environment.NewLine;
            if (StartDate != "0")
            {
                StrQuery += "WHERE [DATE] BETWEEN '" + StartDate + "' AND '" + EndDate + "'" + System.Environment.NewLine;

            }
           
            //StrQuery += "AND LU.Id not in('dd0beadb-1aba-49e3-938e-56704dc5e74a','5bee9379-4a8f-4be1-9cd6-174f8b8bd1a1','54de9f5f-be7c-456b-9fb2-4d4535ad4eaa','3a9a679a-ddcc-4ae6-bf9d-321d978aa139','277f903b-de07-46e1-97c3-4e010305099a')" + System.Environment.NewLine; //log user yang Role nya NCI tidak ditampilkan
            StrQuery += "ORDER BY LU.DATE DESC,LU.TIME DESC";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtJoint);
            con.Close();
            return dtJoint;
        }
        #endregion


        #region vehicle_trailer
        public class vehicleTrailer
        {
            public DateTime date_created { get; set; }
            public string vehicle_id { get; set; }
            public string setup_no { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
        }
        public class Vehicle
        {
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string axis_type { get; set; }
            public string vehicle_mileage { get; set; }
            public string vehicle_img { get; set; }
            public string vehicle_type_id { get; set; }
            public string is_vehicle { get; set; }
            public string brand_vhc_id { get; set; }
            public string userid { get; set; }


        }
        public class SingleVehicle
        {
            public string vehicle_id { get; set; }
            public string vehicle_name { get; set; }
            public string axis_type { get; set; }
            public string vehicle_mileage { get; set; }
            public string vehicle_img { get; set; }
            public string is_vehicle { get; set; }
            public string brand_vhc_id { get; set; }
            public string userid { get; set; }

            public string data { get; set; }


        }
        

        public static cRespon InsertVehicleTrailer(List<vehicleTrailer> vehicle)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";

            //List<vehicleTrailer> vehicle = new List<vehicleTrailer>();
            //vehicle = JsonConvert.DeserializeObject<List<vehicleTrailer>>(vehicleList);

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {

                    try
                    {
                        string setup_no = vehicle[0].setup_no==null? vehicle[0].vehicle_id+DateTime.Now.ToString("yyyyMMdd"):vehicle[0].setup_no;
                        for(int sort = 0; sort < vehicle.Count(); sort++)
                        {
                            vehicleTrailer val= vehicle[sort];
                            conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE_TRAILER WHERE vehicle_id = @vehicle_id AND setup_no = @setup_no))
                                        BEGIN
                                            INSERT INTO VEHICLE_TRAILER 
                                                (vehicle_id,setup_no,sort,date_created,trailer_id,description,userid)
                                            VALUES
                                                (@vehicle_id,@setup_no,@sort,@date_created,'0',@description,@userid)
                                        END"
                            , new
                            {
                                val.date_created,
                                val.vehicle_id,
                                val.userid,
                                sort,
                                setup_no,
                                val.description
                                
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
                returnRes.message = "Setup vehicle trailer succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertSingleVehicle(SingleVehicle val)
        {
            List<cTireForSingleVehicle> ListTires = new List<cTireForSingleVehicle>();
            ListTires = JsonConvert.DeserializeObject<List<cTireForSingleVehicle>>(val.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string unit_location_id = getDefaultUnitLocationId();
            DateTime date_created = DateTime.Now;
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
                                                (vehicle_id,brand_vhc_id,vehicle_name,date_created,axis_type,vehicle_img,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,@brand_vhc_id,@vehicle_name,@date_created,@axis_type,@vehicle_img,'False',@userid)
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE VEHICLE SET is_delete=0, vehicle_name = @vehicle_name, axis_type = @axis_type, vehicle_img=@vehicle_img, date_modified=@date_created, userid=@userid
                                            WHERE vehicle_id = @vehicle_id
                                        END"
                            , new
                            {
                                date_created,
                                val.vehicle_id,
                                val.vehicle_name,
                                val.axis_type,
                                val.brand_vhc_id,
                                val.vehicle_img,
                                val.vehicle_mileage,
                                val.is_vehicle,
                                val.userid

                            }, tran);
                        foreach (var dr in ListTires)
                        {
                            conn.Execute(@"INSERT INTO TIRE_UNIT
                                                (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                            VALUES
                                                (@vehicle_id,@tire_id,NULL,0,@date_created,@userid)"
                               , new
                               {
                                   val.vehicle_id,
                                   dr.tire_id,
                                   date_created,
                                   val.userid
                               }, tran);
                            conn.Execute(@" INSERT INTO TIRE_AXIS
                                                (vehicle_id,position,tire_id,axis_id,date_created,description, is_delete, userid)
                                            VALUES
                                                (@vehicle_id,@position,@tire_id,@axis_id,@date_created,NULL,0, @userid)
                                        UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                        "
                                , new
                                {
                                    val.userid,
                                    dr.axis_id,
                                    val.vehicle_id,
                                    unit_location_id,
                                    date_created,
                                    dr.tire_id,
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
                returnRes.message = "Setup single vehicle succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon InsertVehicle(Vehicle val)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            DateTime date_created = DateTime.Now;
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
                                                (vehicle_id,brand_vhc_id,vehicle_name, description,date_created,axis_type, vehicle_type_id,vehicle_img,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,@brand_vhc_id,@vehicle_name, '',@date_created,@axis_type, @vehicle_type_id,@vehicle_img,'False',@userid)
                                        END
                                    ELSE
                                        BEGIN
                                            UPDATE VEHICLE SET is_delete=0, vehicle_name = @vehicle_name, brand_vhc_id=@brand_vhc_id, axis_type = @axis_type, vehicle_img=@vehicle_img, date_modified=@date_created, userid=@userid
                                            WHERE vehicle_id = @vehicle_id
                                        END"
                            , new
                            {
                                date_created,
                                val.vehicle_id,
                                val.vehicle_name,
                                val.axis_type,
                                val.brand_vhc_id,
                                val.vehicle_img,
                                val.vehicle_type_id,
                                val.userid

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
                returnRes.message = "Setup vehicle succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon deleteVehicleTrailer(string vehicle_id, string setup_no)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {

                    try
                    {
                        conn.Execute(@"DELETE FROM VEHICLE_TRAILER WHERE vehicle_id = @vehicle_id AND setup_no = @setup_no"
                        , new
                        {
                            vehicle_id,
                            setup_no

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
                returnRes.message = "Trailer deleted succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        public static cRespon deleteVehicle(string vehicle_id)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {

                    try
                    {
                        conn.Execute(@"DELETE FROM VEHICLE_TRAILER WHERE vehicle_id = @vehicle_id
                                        DELETE FROM VEHICLE WHERE vehicle_id = @vehicle_id"
                        , new
                        {
                            vehicle_id

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
                returnRes.message = "Vehicle deleted succesfully.";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        #endregion

        #region tire installation
        public class TireInstallation
        {
            public DateTime date_created { get; set; }
            public string vehicle_id { get; set; }
            public string tire_id { get; set; }
            public string axis_id { get; set; }
            public string userid { get; set; }
        }
        public class TireDismantle
        {
            public DateTime date_created { get; set; }
            public string vehicle_id { get; set; }
            public string tire_id { get; set; }
            public string location_id { get; set; }
            public string axis_id { get; set; }
            public string userid { get; set; }
        }

        public static string getNextTireUnitId()
        {
            string tire_unit_id = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = "select [dbo].[GetNextTireUnitId]() as tire_unit_id ";
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
                tire_unit_id = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["tire_unit_id"]).FirstOrDefault();
            }
            else
            {
                tire_unit_id = "T001";
            }
            return tire_unit_id;
        }
        public static string getDefaultUnitLocationId()
        {
            string location_id = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select sys_value from SYS_SETTINGS where sys_key='unit_location_id' ";
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
                location_id = "OV00";
            }
            return location_id;
        }
        public static int cekDataTireUnit(string vehicle_id, string tire_id)
        {
            string row = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = "select * from TIRE_UNIT where tire_id=@tire_id and vehicle_id = @vehicle_id and is_delete=0 ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
            cmd.Parameters.AddWithValue("@tire_id", tire_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable.Rows.Count;
        }
        public static int cekDataSchedule(string year, string month, string date, string location_id, string shift_id, string group_id)
        {
            string row = "";
            DataTable dtTempTable = new DataTable("SCHEDULES");
            string StrQuery = "select * from SCHEDULES where year=@year and month = @month and date=@date and location_id=@location_id and (shift_id=@shift_id or group_id=@group_id) and is_delete=0";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@location_id", location_id);
            cmd.Parameters.AddWithValue("@shift_id", shift_id);
            cmd.Parameters.AddWithValue("@group_id", group_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable.Rows.Count;
        }

        public static int cekDataScheduleNoIsDelete(string year, string month, string date, string location_id, string shift_id, string group_id)
        {
            string row = "";
            DataTable dtTempTable = new DataTable("SCHEDULES");
            string StrQuery = "select * from SCHEDULES where year=@year and month = @month and date=@date and location_id=@location_id and (shift_id=@shift_id or group_id=@group_id)";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@month", month);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@location_id", location_id);
            cmd.Parameters.AddWithValue("@shift_id", shift_id);
            cmd.Parameters.AddWithValue("@group_id", group_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable.Rows.Count;
        }
        public static string getScheduleId(string yearmonth)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = "select [dbo].[GetNextScheduleEmpId](@yearmonth) as data";
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
        public static string getTireUnitId(string vehicle_id, string tire_id)
        {
            string tire_unit_id = "";
            DataTable dtTempTable = new DataTable("TIRE_UNIT");
            string StrQuery = "select tire_unit_id from TIRE_UNIT where tire_id=@tire_id and vehicle_id = @vehicle_id ";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@vehicle_id", vehicle_id);
            cmd.Parameters.AddWithValue("@tire_id", tire_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                tire_unit_id = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["tire_unit_id"]).FirstOrDefault();
            }
            return tire_unit_id;
        }
        public static string getAxisId()
        {
            string axis_id = "";
            DataTable dtTempTable = new DataTable("AXIS");
            string StrQuery = "select [dbo].[GetNextAxisId]() as axis_id";
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
                axis_id = (from DataRow dr in dtTempTable.Rows
                                select (string)dr["axis_id"]).FirstOrDefault();
            }
            return axis_id;
        }

        public static int getMaxAxisId(string axis_type)
        {
            int axis_id = 0;
            DataTable dtTempTable = new DataTable("AXIS");
            string StrQuery = "select count(axis_type) as MAX_AXIS_ID from AXIS where axis_type =@axis_type";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@axis_type", axis_type);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            if (dtTempTable.Rows.Count > 0)
            {
                axis_id = (from DataRow dr in dtTempTable.Rows
                           select (int)dr["MAX_AXIS_ID"]).FirstOrDefault();
            }
            return axis_id;
        }

        public static string GetNextAxisId()
        {
            string axis_id = "";
            DataTable dtTempTable = new DataTable("AXIS");
            string StrQuery = @"select dbo.[GetNextAxisId]() as axis_id";
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
                axis_id = (from DataRow dr in dtTempTable.Rows
                           select (string)dr["axis_id"]).FirstOrDefault();
            }
            return axis_id;
        }
        public static cRespon InsertTireInstallation(TireInstallation val)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");
            string date_created_ = date_created.ToString("yyyy-MM-dd");
            //string tire_unit_id = getNextTireUnitId();
            string unit_location_id = getDefaultUnitLocationId();
            string current_epc_tire = Process.getEPCCurrentTire(val.tire_id);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    if (cekDataTireUnit(val.vehicle_id, val.tire_id) == 0)
                    {
                        try
                        {
                            conn.Execute(@"INSERT INTO TIRE_UNIT
                                                (vehicle_id,tire_id,description, is_delete, date_created, userid)
                                            VALUES
                                                (@vehicle_id,@tire_id,NULL,0,@date_created,@userid)"
                                , new
                                {
                                    val.vehicle_id,
                                    val.tire_id,
                                    date_created,
                                    val.userid
                                }, tran);
                            conn.Execute(@" INSERT INTO TIRE_AXIS
                                                (vehicle_id,tire_id,axis_id,date_created,description, is_delete, userid)
                                            VALUES
                                                (@vehicle_id,@tire_id,@axis_id,@date_created,NULL,0, @userid)
                                        UPDATE TIRE SET location_id = @unit_location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                        "
                                , new
                                {
                                    val.userid,
                                    val.axis_id,
                                    val.vehicle_id,
                                    unit_location_id,
                                    date_created,
                                    val.tire_id,
                                }, tran);
                            conn.Execute(@"update tire_stock_out set is_current = 0, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                            
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_current, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created_,@unit_location_id,@tire_id,1,0,@userid)", new
                            {
                                yearmonth,
                                unit_location_id,
                                date_created,
                                date_created_,
                                val.tire_id,
                                val.userid
                            }, tran);

                            conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, is_delete, date_created, userid)
                                         values (@tire_id,@current_epc_tire,'102','XXX','XXX',0,@date_created_,@userid)", new
                            {
                                val.tire_id,
                                date_created_,
                                current_epc_tire,
                                val.userid
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
                    else
                    {
                        returnRes.flag = false;
                        returnRes.message = "Ban sudah terpasang sebelumnya!";
                    }
                   
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Tire Installed";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertTireDismantle(TireDismantle val)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            DateTime date_created = DateTime.Now;
            string yearmonth = date_created.ToString("yyyyMM");
            string date_created_ = date_created.ToString("yyyy-MM-dd");
            string current_epc_tire = Process.getEPCCurrentTire(val.tire_id);
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"UPDATE TIRE_UNIT SET is_delete=1, date_modified = @date_created, userid=@userid WHERE tire_id =@tire_id and vehicle_id=@vehicle_id 
                                "
                            , new
                            {
                                val.vehicle_id,
                                date_created,
                                val.tire_id,
                                val.userid
                            }, tran);
                        conn.Execute(@"     
                                    UPDATE TIRE_AXIS SET is_delete=1, date_modified = @date_created, userid=@userid where tire_id =@tire_id and axis_id=@axis_id
                                    
                                    UPDATE TIRE SET location_id = @location_id, date_modified = @date_created WHERE tire_id = @tire_id
                                    "
                            , new
                            {
                                val.axis_id,
                                val.vehicle_id,
                                val.location_id,
                                date_created,
                                val.tire_id,
                                val.userid
                            }, tran);

                        conn.Execute(@"update tire_stock_out set is_current = 0, date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                            
                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_current, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created_,@location_id,@tire_id,1,0,@userid)", new
                        {
                            yearmonth,
                            val.location_id,
                            date_created,
                            date_created_,
                            val.tire_id,
                            val.userid
                        }, tran);

                        conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, is_delete, date_created, userid)
                                         values (@tire_id,@current_epc_tire,'103','XXX','XXX',0,@date_created_,@userid)", new
                        {
                            val.tire_id,
                            date_created_,
                            current_epc_tire,
                            val.userid
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
                returnRes.message = "Tire Dismantled";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        #endregion

        #region Insert Fleet with vehicle
        public static cRespon InsertFleetProcess(cCrudSetupFleet p)
        {
            List<cCrudFleetVehicle> ListVehicle = new List<cCrudFleetVehicle>();
            ListVehicle = JsonConvert.DeserializeObject<List<cCrudFleetVehicle>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";

            //int rowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
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
                                          update FLEET set is_delete=0, fleet_name=@fleet_name, location_type_id = @location_type_id, description=@description, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id
                                        END 
                                
                                    ", new
                        {
                            p.fleet_id,
                            p.fleet_name,
                            p.description,
                            p.location_type_id,
                            date_created,
                            p.userid
                        }, tran);



                        foreach (var dt in ListVehicle)
                        {
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
                                dt.vehicle_id,
                                dt.position,
                                date_created,
                                p.description,
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
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Add Fleet succesfully. Fleet Id: " + p.fleet_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertFleetFromFdsProcess(cCrudSetupFleet p)
        {
            List<cCrudFleetVehicleFromFds> ListVehicle = new List<cCrudFleetVehicleFromFds>();
            ListVehicle = JsonConvert.DeserializeObject<List<cCrudFleetVehicleFromFds>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";

            //int rowAffected = 0;
            DateTime date_created = DateTime.Now;
            DateTime date_modified = DateTime.Now;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE WHERE vehicle_id = @fleet_id))
                                        BEGIN
                                            INSERT INTO VEHICLE
                                                (vehicle_id,brand_vhc_id,vehicle_name, description,date_created,axis_type, vehicle_type_id,vehicle_img, vehicle_rfid, km, hm,is_delete,userid)
                                            VALUES
                                                (@fleet_id,'OWN',@fleet_id, '',@date_created,'1', '1','','0', 0,0,'False',@userid)
                                        END"
                            , new
                            {
                                date_created,
                                p.fleet_id,
                                p.userid

                            }, tran);
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET where fleet_id=@fleet_id) ) 
                                        BEGIN 
                                         INSERT INTO FLEET
                                         (fleet_id, location_type_id, fleet_name, km, hm, description, is_delete, date_created, userid)
                                         values (@fleet_id,@location_type_id,@fleet_name,0,0,@description,0,@date_created, @userid)
                                        END 
                                        ELSE
                                        BEGIN 
                                          update FLEET set is_delete=0, fleet_name=@fleet_name, location_type_id = @location_type_id, description=@description, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id
                                        END 
                                
                                    ", new
                        {
                            p.fleet_id,
                            p.fleet_name,
                            p.description,
                            p.location_type_id,
                            date_created,
                            p.userid
                        }, tran);



                        foreach (var dt in ListVehicle)
                        {

                            conn.Execute(@"IF (NOT EXISTS (SELECT vehicle_id FROM VEHICLE WHERE vehicle_id = @vehicle_id))
                                        BEGIN
                                            INSERT INTO VEHICLE
                                                (vehicle_id,brand_vhc_id,vehicle_name, description,date_created,axis_type, vehicle_type_id,vehicle_img, vehicle_rfid, km, hm,is_delete,userid)
                                            VALUES
                                                (@vehicle_id,'OWN',@vehicle_id, '',@date_created,@axis_type, @vehicle_type_id,'','0', 0,0,'False',@userid)
                                        END"
                                , new
                                {
                                    date_created,
                                    dt.vehicle_id,
                                    dt.vehicle_type_id,
                                    dt.axis_type,
                                    p.userid

                                }, tran);
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 fleet_id from FLEET_VEHICLE where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort) ) 
                                    BEGIN 
                                        INSERT INTO FLEET_VEHICLE
                                     (fleet_id, vehicle_id, position,date_created, description, is_delete, date_modified, userid)
                                     values (@fleet_id, @vehicle_id, @sort,@date_created, @description,'0', @date_modified,@userid)
                                    END 
                                    ELSE
                                        BEGIN 
                                          update FLEET_VEHICLE set is_delete=0, date_modified=@date_created, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id and position=@sort
                                        END 

                                ", new
                            {
                                p.fleet_id,
                                dt.vehicle_id,
                                dt.sort,
                                date_created,
                                p.description,
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
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Add Fleet succesfully. Fleet Id: " + p.fleet_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        #endregion

        #region Delete Fleet
        public static cRespon deleteFleetProcess(string fleet_id, string userid)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.message = "";
            returnRes.status = "";
            DateTime date = DateTime.Now;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update FLEET_VEHICLE set is_delete='1', date_modified=@date, userid=@userid where fleet_id=@fleet_id
                                        END 

                                    ", new
                        {
                            fleet_id,
                            date,
                            userid
                        }, tran);

                        conn.Execute(@"
                                        BEGIN 
                                          update FLEET set is_delete='1', date_modified=@date, userid=@userid where fleet_id=@fleet_id
                                        END 
                                
                                    ", new
                        {
                            fleet_id,
                            date,
                            userid
                        }, tran);



                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Delete Fleet succesfully. Fleet Id: " + fleet_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
        #endregion
        public static cRespon deleteFleetProcessListVehicle(string fleet_id, string vehicle_id, string userid)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.message = "";
            returnRes.status = "";
            DateTime date = DateTime.Now;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update FLEET_VEHICLE set is_delete='1', date_modified=@date, userid=@userid where fleet_id=@fleet_id and vehicle_id=@vehicle_id
                                        END 

                                    ", new
                        {
                            fleet_id,
                            date,
                            userid,
                            vehicle_id
                        }, tran);



                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Delete Vehicle succesfully. Vehicle Id: " + fleet_id;
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        #region Delete Vehicle
        public static cRespon deleteVehicleProcess(string vehicle_id, string userid)
        {
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.message = "";
            returnRes.status = "";
            DateTime date_modified = DateTime.Now;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update VEHICLE set is_delete='1', date_modified=@date_modified, userid=@userid  where vehicle_id=@vehicle_id
                                        END 
                                
                                    ", new
                        {
                            vehicle_id,
                            userid,
                            date_modified
                        }, tran);



                        tran.Commit();

                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        returnRes.flag = false;
                        returnRes.status = "Failed";
                        returnRes.message = "# Failed, " + e.Message.ToString();
                    }
                }
                conn.Close();
            }

            if (returnRes.flag == true)
            {
                returnRes.message = "Delete Vehicle succesfully. Fleet Id: " + vehicle_id;
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