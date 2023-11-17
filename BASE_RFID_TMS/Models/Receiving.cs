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
    public class Receiving
    {
        public class cCrudReceiving
        {
            public string date_created { get; set; }
            public string supplier_id { get; set; }
            //public string po_no { get; set; }
            //public string po_date { get; set; }
            //public string do_no { get; set; }
            //public string do_date { get; set; }
            public string description { get; set; }
            public string userid { get; set; }
            //public int qty_record { get; set; }
            //public string data { get; set; }
            public string data_detail_serial_number { get; set; }
        }


        public class cCrudReceivingInsertManualSerialNumber
        {
            public string receive_no { get; set; }
            public DateTime date_created { get; set; }
            public int qty_record { get; set; }
            public string data { get; set; }
            public string userid { get; set; }
        }

        public class cCrudReceivingDetailSerialNumber
        {
            
            public string tire_code { get; set; }
            public string serial_number { get; set; }
        }

        public class cCrudReceivingDetailQty
        {
            public string tire_code { get; set; }
            public string qty { get; set; }
            public string price { get; set; }
        }

        public static cRespon InsertReceivingManualInsertSerialNumberProcess(cCrudReceivingInsertManualSerialNumber p)
        {
            List<cCrudReceivingDetailSerialNumber> ListSerialNumber = new List<cCrudReceivingDetailSerialNumber>();
            ListSerialNumber = JsonConvert.DeserializeObject<List<cCrudReceivingDetailSerialNumber>>(p.data);
            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string process_id = "100";
            string description = "Receiving";
            string receive_no = p.receive_no;
            //string date_created = p.date_created.ToString("yyyy-MM-dd");
            string location_id = getDefaultLocationId();
            TimeSpan Tnow = DateTime.Now.TimeOfDay;

            //DateTime date_created = p.date_created.Add(Tnow);
            //string tempdate_created = p.date_created.ToString("yyyy-MM-dd");
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            //string tire_code = p.tire_code;
            int qty = Convert.ToInt16(p.qty_record);
            Boolean endloop = false;
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var dr in ListSerialNumber)
                        {

                            var query = @"IF (NOT EXISTS (select TOP 1 receive_no from RECEIVE_DETAIL where receive_no=@receive_no and serial_number=@serial_number) )  ";
                            query += @" BEGIN 
                                INSERT INTO RECEIVE_DETAIL
                                (receive_no, date_created, serial_number, tire_code)";
                            query += @" values (@receive_no,@date_created,@serial_number,@tire_code) END";

                            conn.Execute(query, new {
                                receive_no,
                                p.date_created,
                                dr.serial_number,
                                dr.tire_code
                            }, tran);
                            //string stock_out_id = getStockOutId(yearmonth);
                        }
                        conn.Execute(@"insert into tire
                                    select tire_id = d.serial_number,@location_id,d.tire_code,i.tire_name,
                                        d.serial_number,i.size,i.pattern, 0, NULL, 1, 0, 
                                        d.date_created, date_modified = d.date_created,''
									from RECEIVE_DETAIL d
                                    inner join RECEIVE r on d.receive_no = r.receive_no
                                    inner join item i on d.tire_code = i.tire_code
                                    where d.receive_no =@receive_no", new
                        {
                            receive_no,
                            location_id
                        }, tran);
                        foreach (var dr in ListSerialNumber)
                        {
                            conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@location_id,@serial_number,0,@userid)", new
                            {
                                yearmonth,
                                location_id,
                                p.date_created,
                                dr.serial_number,
                                p.userid
                            }, tran);
                        }
                        conn.Execute(@"insert into tire_tracking (tire_id, process_id, smo_id, smr_id, location_id, is_delete, date_created, date_modified, userid)
                                        select tire_id = d.serial_number,@process_id,'XXX','XXX', @location_id,0,
                                        d.date_created, date_modified = d.date_created, @userid
									from RECEIVE_DETAIL d
                                    inner join RECEIVE r on d.receive_no = r.receive_no
                                    inner join item i on d.tire_code = i.tire_code
                                    where d.receive_no =@receive_no", new
                        {
                            receive_no,
                            location_id,
                            process_id,
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
                    conn.Close();
                }

                if (returnRes.flag == true)
                {
                    returnRes.message = "Serial number succesfully.";
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
        public static ClsResponReceiveWitManualSN InsertReceivingProcess(cCrudReceiving p)
        {
            List<cCrudReceivingDetailQty> ListTire = new List<cCrudReceivingDetailQty>();
            //ListTire = JsonConvert.DeserializeObject<List<cCrudReceivingDetailQty>>(p.data);

            ClsResponReceiveWitManualSN returnRes = new ClsResponReceiveWitManualSN();
            List<cCrudReceivingDetailSerialNumber> ListSerialNumber = new List<cCrudReceivingDetailSerialNumber>();
            ListSerialNumber = JsonConvert.DeserializeObject<List<cCrudReceivingDetailSerialNumber>>(p.data_detail_serial_number);
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            string process_id = "100";
            string description = "Receiving";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM"); //p.date_created.ToString("yyyyMM");
            string receive_no = getNextReceiveNo(yearmonth);
            string location_id = getDefaultLocationId();
            string location_id_cons = getDefaultLocationIdCons();
            string outbound_id = "";
            string inbound_id = "";
            inbound_id = Process.getNextInboundId(yearmonth);
            outbound_id = Process.getNextOutboundId(yearmonth);
            int cekData = 0;
            int resCekAllData = 0;
            int resCekDataExist = 0;
            foreach (var dr in ListSerialNumber)
            {
                cekData = cekDataTireOutboundConsWH(dr.serial_number);
                resCekAllData += 1;
                if (cekData != 0)
                {
                    resCekDataExist += 1;
                }
            }
            if (resCekAllData != resCekDataExist)
            {
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
                                         values (@outbound_id,@date_created,@location_id_cons,0,@userid)
                                        END 
                                
                                    ", new
                            {
                                outbound_id,
                                p.date_created,
                                location_id_cons,
                                p.description,
                                p.userid
                            }, tran);
                            int qty = 0;
                            int loop = 1;
                            int tempqty = 0;
                            Boolean endloop = false;
                            string serial_number = "";


                            foreach (var dr in ListSerialNumber)
                            {
                                String tire_name = getDataItemTire("tire_name", dr.tire_code);
                                String size = getDataItemTire("size", dr.tire_code);
                                String pattern = getDataItemTire("pattern", dr.tire_code);
                                String tire_id = dr.serial_number;
                                
                                string current_epc_tire = Process.getEPCCurrentTire(dr.serial_number);
                                conn.Execute(@"
                                        
                                        
                                        IF (NOT EXISTS (select tire_id from OUTBOUND ob  
                                                        inner join OUTBOUND_DETAIL obd on obd.outbound_id = ob.outbound_id and ob.date_created = obd.date_created
                                                        where ob.location_id = 'A01' and obd.location_id = 'A00S002' and obd.tire_id=@serial_number) ) 
                                        BEGIN 
                                            update OUTBOUND_DETAIL set is_active= 0, date_modified = @date_created, userid = @userid where tire_id = @serial_number
                                            INSERT INTO OUTBOUND_DETAIL 
                                         (outbound_id,date_created,tire_id,location_id,is_active,is_delete,userid)
                                         values (@outbound_id,@date_created,@serial_number,@location_id,1,0,@userid)

                                            UPDATE TIRE set location_id=@location_id,date_modified=@date_created where tire_id=@serial_number

                                            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id_cons,@serial_number,0,@userid)

                                            insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id,location_id , is_delete, date_created, userid)
                                         values (@serial_number,'0','105','XXX','XXX',@location_id_cons,0,@date_created,@userid)
                                        END 
                                
                                    ", new
                                {
                                    inbound_id,
                                    outbound_id,
                                    p.date_created,
                                    dr.serial_number,
                                    p.description,
                                    location_id,
                                    yearmonth,
                                    location_id_cons,
                                    current_epc_tire,
                                    p.userid
                                }, tran);
                                //conn.Execute(@"
                                //            insert into tire_stock_out (stock_out_id, date_created, location_id, tire_id, is_delete, userid)
                                //           values ([dbo].[NextStockOutId](@yearmonth),@date_created,@location_id_cons,@serial_number,0,@userid)", new
                                //{
                                //    yearmonth,
                                //    location_id_cons,
                                //    p.date_created,
                                //    dr.serial_number,
                                //    p.userid
                                //}, tran);
                                //conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id,location_id , is_delete, date_created, userid)
                                //         values (@serial_number,'0','105','XXX','XXX',@location_id_cons,0,@date_created,@userid)", new
                                //{
                                //    dr.serial_number,
                                //    p.date_created,
                                //    current_epc_tire,
                                //    location_id_cons,
                                //    p.userid
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
            }
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        DateTime inputDate = DateTime.Now;
                        try {
                            inputDate = DateTime.ParseExact(p.date_created, "yyyy-MM-dd HH:mm:ss", null);
                        }
                        catch (Exception e) {
                        }
                        //DateTime inputDate = DateTime.ParseExact(p.date_created, "yyyy-MM-dd HH:mm:ss", null);

                        DateTime newDate = inputDate.AddSeconds(1);

                        string date_created_temp = newDate.ToString("yyyy-MM-dd HH:mm:ss");
                        string date_created = date_created_temp.Replace(".", ":");
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 receive_no from RECEIVE where receive_no=@receive_no) ) 
                                        BEGIN 
                                            INSERT INTO RECEIVE 
                                         (receive_no,date_created,supplier_id,description,userid)
                                         values (@receive_no,@date_created,@supplier_id,@description,@userid)
                                        END 
                                
                                    ", new
                        {
                            receive_no,
                            date_created,
                            p.supplier_id,
                            p.description,
                            p.userid
                        }, tran);
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND where inbound_id=@inbound_id) ) 
                                        BEGIN 
                                            INSERT INTO INBOUND 
                                         (inbound_id,date_created,location_id,is_delete,userid)
                                         values (@inbound_id,@date_created,@location_id,0,@userid)
                                        END 
                                
                                    ", new
                        {
                            inbound_id,
                            date_created,
                            location_id,
                            p.userid
                        }, tran);
                        int qty = 0;
                        int loop = 1;
                        int tempqty = 0;
                        Boolean endloop = false;
                        string serial_number = "";


                        foreach (var dr in ListSerialNumber)
                        {
                            String tire_name = getDataItemTire("tire_name", dr.tire_code);
                            String size = getDataItemTire("size", dr.tire_code);
                            String pattern = getDataItemTire("pattern", dr.tire_code);
                            String tire_id = dr.serial_number;
                            conn.Execute(@"
                                            IF (NOT EXISTS (select TOP 1 receive_no from RECEIVE_DETAIL where receive_no=@receive_no and serial_number=@serial_number) ) 
                                            BEGIN 
                                                INSERT INTO RECEIVE_DETAIL
                                                (receive_no, date_created, serial_number, tire_code)
                                            values (@receive_no,@date_created,@serial_number,@tire_code) 
                                            END", new
                            {
                                yearmonth,
                                receive_no,
                                process_id,
                                date_created,
                                dr.serial_number,
                                dr.tire_code,
                                size,
                                pattern,
                                tire_name,
                                location_id,
                                p.userid
                            }, tran);
                            string current_epc_tire = Process.getEPCCurrentTire(dr.serial_number);
                            conn.Execute(@"
                                        IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND_DETAIL where inbound_id=@inbound_id and date_created=@date_created and tire_id=@serial_number) ) 
                                        BEGIN 
                                            INSERT INTO INBOUND_DETAIL 
                                         (inbound_id,date_created,date_modified,tire_id,is_delete,userid)
                                         values (@inbound_id,@date_created,@date_created,@serial_number,0,@userid)
                                            
                                            UPDATE TIRE set location_id=@location_id,date_modified=@date_created where tire_id=@serial_number
                                        END
                                        
                                
                                    ", new
                            {
                                inbound_id,
                                outbound_id,
                                date_created,
                                dr.serial_number,
                                p.description,
                                location_id,
                                p.userid
                            }, tran);

                            conn.Execute(@"
                                            insert into tire_stock_in (stock_in_id, date_created,date_modified, location_id, tire_id, is_delete, userid)
                                           values ([dbo].[NextStockInId](@yearmonth),@date_created,@date_created,@location_id,@tire_id,0,@userid)", new
                            {
                                yearmonth,
                                location_id,
                                date_created,
                                tire_id,
                                p.userid
                            }, tran);
                            conn.Execute(@"insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created,date_modified, userid)
                                         values (@tire_id,'0','104','XXX','XXX',@location_id,0,@date_created,@date_created,@userid)", new
                            {
                                tire_id,
                                date_created,
                                current_epc_tire,
                                location_id,
                                p.userid
                            }, tran);

                        }
                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 receive_no from RECEIVE_DETAIL_QTY where receive_no=@receive_no) ) 
                                        BEGIN 
                                            INSERT INTO RECEIVE_DETAIL_QTY 
                                         (receive_no,date_created,tire_code,qty)
                                          select @receive_no,@date_created,tire_code,count(serial_number) from RECEIVE_DETAIL
                                            where 
                                            receive_no=@receive_no
                                            group by tire_code
                                        END 
                                
                                    ", new
                        {
                            receive_no,
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
                returnRes.message = "Receiving succesfully. Receive No: " + receive_no;
                returnRes.status = "success";
                returnRes.receive_no = receive_no;
            }
            else
            {
                returnRes.status = "failed";
            }
            //}
            return returnRes;
        }

        public static ClsResponReceiveWitManualSN InsertConsignmentReceivingProcess(cCrudReceiving p)
        {
            //List<cCrudReceivingDetailQty> ListTire = new List<cCrudReceivingDetailQty>();
            //ListTire = JsonConvert.DeserializeObject<List<cCrudReceivingDetailQty>>(p.data);

            ClsResponReceiveWitManualSN returnRes = new ClsResponReceiveWitManualSN();
            List<cCrudReceivingDetailSerialNumber> ListSerialNumber = new List<cCrudReceivingDetailSerialNumber>();
            ListSerialNumber = JsonConvert.DeserializeObject<List<cCrudReceivingDetailSerialNumber>>(p.data_detail_serial_number);
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            int RowAffected = 0;
            string inbound_id = "";
            string process_id = "99";
            string description = "Consignment Inbound";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM"); //p.date_created.ToString("yyyyMM");
            string rcv_date = Convert.ToDateTime(p.date_created).ToString("yyyy-MM-dd");
            string receive_no = getNextConsignmentReceivingNo(yearmonth);
            string location_id = getDefaultLocationIdCons();
            inbound_id = Process.getNextInboundId(yearmonth);
            int cekData = 0;
            int resCekAllData = 0;
            int resCekDataExist = 0;
            foreach (var dr in ListSerialNumber)
            {
                cekData = cekDataTire(dr.serial_number);
                resCekAllData += 1;
                if (cekData != 0)
                {
                    resCekDataExist += 1;
                }
            }
            if (resCekAllData == resCekDataExist)
            {
                returnRes.flag = false;
                returnRes.message = "# Failed, All Data Already Exist";
            }
            else {
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                        

                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 cons_rcv_no from CONSIGNMENT_RECEIVE where cons_rcv_no=@receive_no) ) 
                                            BEGIN 
                                                INSERT INTO CONSIGNMENT_RECEIVE 
                                             (cons_rcv_no,cons_rcv_date,date_created,supplier_id,description,userid)
                                             values (@receive_no,@rcv_date,@date_created,@supplier_id,@description,@userid)
                                            END 
                                
                                        ", new
                            {
                                receive_no,
                                p.date_created,
                                rcv_date,
                                p.supplier_id,
                                p.description,
                                p.userid
                            }, tran);
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
                                location_id,
                                p.userid
                            }, tran);
                            int qty = 0;
                            int loop = 1;
                            int tempqty = 0;
                            Boolean endloop = false;
                            string serial_number = "";
                        


                            foreach (var dr in ListSerialNumber)
                            {

                                String tire_name = getDataItemTire("tire_name",dr.tire_code);
                                String size = getDataItemTire("size", dr.tire_code);
                                String pattern = getDataItemTire("pattern", dr.tire_code);
                                String tire_id = dr.serial_number;
                                string current_epc_tire = Process.getEPCCurrentTire(tire_id);
                                conn.Execute(@"
                                                IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@serial_number ) )
                                                BEGIN
                                                    insert into tire
                                                    (tire_id, location_id,tire_code,tire_name,serial_number,size,pattern,km,rfid, status_id, is_delete, date_created, date_modified,userid)
                                                     values
                                                    (@serial_number,@location_id,@tire_code,@tire_name,@serial_number,@size,@pattern,0,'0', '0', 0, @date_created, @date_created,@userid)

                                               
                                                    IF (NOT EXISTS (select TOP 1 cons_rcv_no from CONSIGNMENT_RECEIVE_DETAIL where cons_rcv_no=@receive_no and tire_id=@serial_number) ) 
                                                   BEGIN 
                                                        INSERT INTO CONSIGNMENT_RECEIVE_DETAIL
                                                        (cons_rcv_no,cons_rcv_date, date_created, tire_id, tire_code, is_delete, date_modified, userid)
                                                    values (@receive_no,@rcv_date,@date_created,@serial_number,@tire_code,0,@date_created,@userid) 
                                                    END
                                
                                                    IF (NOT EXISTS (select TOP 1 inbound_id from INBOUND_DETAIL where inbound_id=@inbound_id and date_created=@date_created and tire_id=@tire_id) ) 
                                                        BEGIN 
                                                            INSERT INTO INBOUND_DETAIL 
                                                         (inbound_id,date_created,date_modified,tire_id,is_delete,userid)
                                                         values (@inbound_id,@date_created,@date_created,@tire_id,0,@userid)  
                                                        END
                                                     UPDATE TIRE set location_id=@location_id,date_modified=@date_created where tire_id=@tire_id
                                                    IF (EXISTS (select tire_id from tire where tire_id=@tire_id and status_id='3') ) 
                                                    BEGIN
                                                        update TIRE set status_id='0', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                                    END    
                                                
                                                        insert into tire_stock_in (stock_in_id, date_created,date_modified, location_id, tire_id, is_delete, userid)
                                                       values ([dbo].[NextStockInId](@yearmonth),@date_created,@date_created,@location_id,@tire_id,0,@userid)


                                                        insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created,date_modified, userid)
                                                         values (@tire_id,'0','104','XXX','XXX',@location_id,0,@date_created,@date_created,@userid)
                                                END", new
                                {
                                    inbound_id,
                                    yearmonth,
                                    receive_no,
                                    process_id,
                                    rcv_date,
                                    dr.serial_number,
                                    dr.tire_code,
                                    size,
                                    pattern,
                                    tire_name,
                                    tire_id,
                                    p.date_created,
                                    location_id,
                                    p.userid,
                                    current_epc_tire
                                }, tran);
                                
                                //conn.Execute(@"
                                       
                                           
                                 
                                //        ", new
                                //{
                                //    inbound_id,
                                //    p.date_created,
                                //    tire_id,
                                //    location_id,
                                //    p.userid
                                //}, tran);
                                //conn.Execute(@"
                                //        IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id ) )
                                //            BEGIN        
                                //                 UPDATE TIRE set location_id=@location_id,date_modified=@date_created where tire_id=@tire_id
                                //                 IF (EXISTS (select tire_id from tire where tire_id=@tire_id and status_id='3') ) 
                                //            BEGIN
                                //                update TIRE set status_id='0', date_modified=@date_created, userid=@userid where tire_id=@tire_id
                                //            END     
                                                                                
                                //        END
                                           
                                 
                                //        ", new
                                //{
                                //    inbound_id,
                                //    p.date_created,
                                //    tire_id,
                                //    location_id,
                                //    p.userid
                                //}, tran);
                                //conn.Execute(@"
                                //               IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id ) )
                                //                BEGIN
                                //                insert into tire_stock_in (stock_in_id, date_created,date_modified, location_id, tire_id, is_delete, userid)
                                //               values ([dbo].[NextStockInId](@yearmonth),@date_created,@date_created,@location_id,@tire_id,0,@userid) END", new
                                //{
                                //    yearmonth,
                                //    location_id,
                                //    p.date_created,
                                //    tire_id,
                                //    p.userid
                                //}, tran);
                                //conn.Execute(@"IF (NOT EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id ) )
                                //                BEGIN
                                //                insert into tire_tracking (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id, is_delete, date_created,date_modified, userid)
                                //             values (@tire_id,'0','104','XXX','XXX',@location_id,0,@date_created,@date_created,@userid)
                                //            END", new
                                //{
                                //    tire_id,
                                //    p.date_created,
                                //    current_epc_tire,
                                //    location_id,
                                //    p.userid
                                //}, tran);

                            }
                            
                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 cons_rcv_no from CONSIGNMENT_RECEIVE_DETAIL_QTY where cons_rcv_no=@receive_no) ) 
                                            BEGIN 
                                                INSERT INTO CONSIGNMENT_RECEIVE_DETAIL_QTY 
                                             (cons_rcv_no,cons_rcv_date,date_created,is_delete,date_modified,tire_code,cons_qty)
                                              select @receive_no,@rcv_date,@date_created,0,@date_created,tire_code,count(tire_id) from CONSIGNMENT_RECEIVE_DETAIL
                                                where 
                                                cons_rcv_no=@receive_no
                                                group by tire_code
                                            END 
                                
                                        ", new
                            {
                                receive_no,
                                p.date_created,
                                rcv_date
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
            

            if (returnRes.flag == true)
            {
                returnRes.message = "Consignment Inbound success.";
                returnRes.status = "success";
                returnRes.receive_no = receive_no;
            }
            else
            {
                returnRes.status = "failed";
            }
            //}
            return returnRes;
        }
        public static string getNextTireIdNo()
        {
            string tire_id = "";
            DataTable dtTempTable = new DataTable("INBOUND");
            string StrQuery = "select tire_id=[dbo].[GetNextTireIdNo]() ";
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
                tire_id = (from DataRow dr in dtTempTable.Rows
                           select (string)dr["tire_id"]).FirstOrDefault();
            }
            return tire_id;
        }

        public static string getDefaultLocationId()
        {
            string location_id = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select sys_value from SYS_SETTINGS where sys_key='default_location_id' ";
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
                location_id = "A00S002";
            }
            return location_id;
        }



        public static string getDefaultLocationIdCons()
        {
            string location_id = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select sys_value from SYS_SETTINGS where sys_key='default_location_id_consignment' ";
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
                location_id = "A01";
            }
            return location_id;
        }

        public static string getDataItemTire(string column, string tire_code)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select "+ column + " from item where tire_code=@tire_code ";
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
            if (dtTempTable.Rows.Count > 0)
            {
                data = (from DataRow dr in dtTempTable.Rows
                               select (string)dr[""+ column+""]).FirstOrDefault();
            }
            else
            {
                data = "0";
            }
            return data;
        }

        public static int cekDataTire(string tire_id)
        {
            string data = "";
            int count = 0;
            DataTable dtTempTable = new DataTable("tire");
            string StrQuery = "select * from tire where tire_id=@tire_id ";
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
                count = dtTempTable.Rows.Count;
            }
            return count;
        }
        public static int cekDataTireOutboundConsWH(string tire_id)
        {
            string data = "";
            int count = 0;
            DataTable dtTempTable = new DataTable("tire");
            string StrQuery = @"select tire_id from OUTBOUND ob  
                                inner join OUTBOUND_DETAIL obd on obd.outbound_id = ob.outbound_id and ob.date_created = obd.date_created
                                where ob.location_id = 'A01' and obd.location_id = 'A00S002' and obd.tire_id=@tire_id ";
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
                count = dtTempTable.Rows.Count;
            }
            return count;
        }
        public static string getStockOutId(string yearmonth)
        {
            string id = "";
            DataTable dtTempTable = new DataTable("SYS_SETTINGS");
            string StrQuery = "select [dbo].[NextStockOutId](@yearmonth) as id";
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
                id = (from DataRow dr in dtTempTable.Rows
                               select (string)dr["id"]).FirstOrDefault();
            }
            return id;
        }
        public static string getNextReceiveNo(string year)
        {
            string receive_no = "";
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select receive_no=[dbo].[GetNextReceivingNo](@year) ";
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
                receive_no = (from DataRow dr in dtTempTable.Rows
                              select (string)dr["receive_no"]).FirstOrDefault();
            }
            return receive_no;
        }
        public static string getNextConsignmentReceivingNo(string year)
        {
            string receive_no = "";
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select receive_no=[dbo].[GetNextConsignmentReceivingNo](@year) ";
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
                receive_no = (from DataRow dr in dtTempTable.Rows
                              select (string)dr["receive_no"]).FirstOrDefault();
            }
            return receive_no;
        }
    }
}