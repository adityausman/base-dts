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
    public class Register
    {
        //private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
        //public string con = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString();
        public class cCrudRegister
        {
            public string register_no { get; set; }
            public DateTime date_created { get; set; }
            public string tire_id { get; set; }
            public string location_id { get; set; }
            //public string trailer_id { get; set; }
            public string userid { get; set; }
            public string rfid { get; set; }
        }

        public class cCrudRegisterVehicle
        {
            public string register_no { get; set; }
            public DateTime date_created { get; set; }
            public string vehicle_id { get; set; }
            public string location_id { get; set; }
            public string userid { get; set; }
            public string rfid { get; set; }
        }
        public static DataTable getCheckRfidRegisterStatus(string rfid)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select top 1  reg_rfid, register_tire_no from REGISTER_TIRE where reg_rfid=@rfid " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@rfid", rfid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        public static DataTable getCheckRfidRegisterVehicleStatus(string rfid)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select top 1  reg_rfid, register_vhc_no from REGISTER_VHC where reg_rfid=@rfid " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@rfid", rfid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        public static DataTable getCheckEPCTire(string rfid)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select *  from EPC_TIRE where epc_rfid=@rfid " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@rfid", rfid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }

        public static DataTable getCheckEPCVhc(string rfid)
        {
            DataTable dtTempTable = new DataTable("SO");
            string StrQuery = "select *  from EPC_VHC where epc_rfid=@rfid " + System.Environment.NewLine;
            //StrQuery += "INNER JOIN BARANG B ON R.kd_barang = B.kd_barang where R.rfid =@rfid";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@rfid", rfid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtTempTable);
            con.Close();
            return dtTempTable;
        }
        public class TireLastLocation
        {
            public string location_id { get; set; }
            public string parent { get; set; }
        }
        public static bool checkInboundTyreWorkshop(string tire_id)
        {
            bool data = false;
            TireLastLocation tireLocation = new TireLastLocation();
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = @"select * from INBOUND inb 
						inner join INBOUND_DETAIL inbd on inb.inbound_id = inbd.inbound_id and inb.date_created = inbd.date_created
						where location_id='A00S001' and inbd.tire_id=@tire_id";
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
        public static TireLastLocation getLastLocationTire(string tire_id)
        {
            string data = "";
            TireLastLocation tireLocation = new TireLastLocation();
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = @"select t.location_id, parent from tire t
                                inner join LOCATION l on l.location_id=t.location_id where t.tire_id=@tire_id";
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
                DataRow dr = dtTempTable.Rows[0];
                tireLocation.location_id = dr["location_id"].ToString();
                tireLocation.parent = dr["parent"].ToString();
            }
            return tireLocation;
        }
        public static string getNextRegisterNoTire(string year)
        {
            string limit = "";
             DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select register_no=[dbo].[NextRegisterNoTire](@year) ";
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
                limit = (from DataRow dr in dtTempTable.Rows
                         select (string)dr["register_no"]).FirstOrDefault();
            }
            return limit;
        }


        public static string getNextRegisterNoVehicle(string year)
        {
            string limit = "";
            DataTable dtTempTable = new DataTable("RFID_REGISTER");
            string StrQuery = "select register_no=[dbo].[NextRegisterNoVehicle](@year) ";
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
                limit = (from DataRow dr in dtTempTable.Rows
                         select (string)dr["register_no"]).FirstOrDefault();
            }
            return limit;
        }

        public static cRespon InsertRegisterProcess(cCrudRegister p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            string process_id = "110";
            string description = "Register";
            string axis_id = "0";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created = p.date_created.Add(Tnow);

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            DataTable CheckEPC = getCheckEPCTire(p.rfid);

            DataTable tmpCheckRfid = getCheckRfidRegisterStatus(p.rfid);
            if (CheckEPC.Rows.Count == 0)
            {
                returnRes.flag = false;
                returnRes.message = "#Rfid " + p.rfid + " not found.";
               
            }
            else {
                if (tmpCheckRfid.Rows.Count > 0)
                {
                    foreach (DataRow dr in tmpCheckRfid.Rows)
                    {
                        returnRes.flag = false;
                        returnRes.message = "#Rfid " + p.rfid + " already used. Register No: " + dr[1].ToString().Trim();
                    }
                }
                else
                {
                    if (p.tire_id != "0" && p.tire_id != "")
                    {
                        no_register = getNextRegisterNoTire(yearmonth);
                    }
                    else
                    {
                    }

                    string remark = "";
                    //string axis_id = "0";
                    string tire_mileage = "0"; 

                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            TireLastLocation lastLocationTire = getLastLocationTire(p.tire_id);
                            if (p.location_id == "A00S001")// JIKA REGISTRASINYA DI WORKSHOP
                            {
                                
                                if (lastLocationTire.location_id == "A00S001" || lastLocationTire.parent == "A89")
                                {
                                    if (checkInboundTyreWorkshop(p.tire_id))
                                    {
                                        try
                                        {

                                            string current_epc_tire = Process.getEPCCurrentTire(p.tire_id);
                                            conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=@no_register and tire_id=@tire_id) ) 
                                                BEGIN 
                                                    INSERT INTO REGISTER_TIRE
                                                 (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                                                 values (@no_register,@date_created,@tire_id,@location_id,@userid,@rfid,0)
                                                END 
                                
                                            ", new
                                            {
                                                no_register,
                                                p.date_created,
                                                p.tire_id,
                                                p.location_id,
                                                // p.trailer_id,
                                                p.userid,
                                                p.rfid
                                            }, tran);

                                            conn.Execute(@"
                                                        INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                                            values
                                                            (@tire_id,@current_epc_tire,@process_id, 'XXX','XXX', @location_id,0, @date_created, @userid)
                                    
                                                ", new
                                            {
                                                p.date_created,
                                                p.tire_id,
                                                current_epc_tire,
                                                p.location_id,
                                                process_id,
                                                p.userid,
                                                p.rfid

                                            }, tran);

                                            conn.Execute(@"
                                                    INSERT INTO TIRE_MILEAGE_HISTORY 
                                                 (date_created,tire_id,location_id,km, is_delete,userid)
                                                 values (@date_created,@tire_id,@location_id,0,0,@userid)
                              
                                
                                            ", new
                                            {
                                                p.date_created,
                                                p.tire_id,
                                                p.location_id,
                                                p.userid
                                            }, tran);




                                            conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                                BEGIN 
                                                    UPDATE TIRE set location_id=@location_id, rfid=@rfid,date_modified=@date_created where tire_id=@tire_id
                                                END 
                                
                                            ", new
                                            {
                                                p.date_created,
                                                p.tire_id,
                                                p.location_id,
                                                p.rfid,
                                                process_id
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
                                    else {
                                        returnRes.flag = false;
                                        returnRes.message = "# Failed, Please inbound Serial Number " + p.tire_id;
                                    }
                                    
                                }
                                else
                                {
                                    returnRes.flag = false;
                                    returnRes.message = "# Failed, Serial Number " + p.tire_id + " is not in Workshop.";
                                    goto loncatSini;
                                }
                            }
                            else {
                                if (lastLocationTire.parent == "A89")
                                {
                                    returnRes.flag = false;
                                    returnRes.message = "# Failed, This tyre (" + p.tire_id + ") can only be registered at the Workshop.";
                                    goto loncatSini;
                                }
                                else
                                {
                                    try
                                    {
                                
                                        string current_epc_tire = Process.getEPCCurrentTire(p.tire_id);
                                        conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_tire_no from REGISTER_TIRE where register_tire_no=@no_register and tire_id=@tire_id) ) 
                                        BEGIN 
                                            INSERT INTO REGISTER_TIRE
                                         (register_tire_no,date_created,tire_id,location_id,userid,reg_rfid,is_delete)
                                         values (@no_register,@date_created,@tire_id,@location_id,@userid,@rfid,0)
                                        END 
                                
                                    ", new
                                        {
                                            no_register,
                                            p.date_created,
                                            p.tire_id,
                                            p.location_id,
                                            // p.trailer_id,
                                            p.userid,
                                            p.rfid
                                        }, tran);

                                        conn.Execute(@"
                                                INSERT INTO TIRE_TRACKING (tire_id,epc_rfid, process_id, smo_id, smr_id, location_id,is_delete, date_created, userid)
                                                    values
                                                    (@tire_id,@current_epc_tire,@process_id, 'XXX','XXX', @location_id,0, @date_created, @userid)
                                    
                                        ", new
                                        {
                                            p.date_created,
                                            p.tire_id,
                                            current_epc_tire,
                                            p.location_id,
                                            process_id,
                                            p.userid,
                                            p.rfid

                                        }, tran);

                                        conn.Execute(@"
                                            INSERT INTO TIRE_MILEAGE_HISTORY 
                                         (date_created,tire_id,location_id,km, is_delete,userid)
                                         values (@date_created,@tire_id,@location_id,0,0,@userid)
                              
                                
                                    ", new
                                        {
                                            p.date_created,
                                            p.tire_id,
                                            p.location_id,
                                            p.userid
                                        }, tran);




                                        conn.Execute(@"IF ( EXISTS (select TOP 1 tire_id from TIRE where tire_id=@tire_id) ) 
                                        BEGIN 
                                            UPDATE TIRE set location_id=@location_id, rfid=@rfid,date_modified=@date_created where tire_id=@tire_id
                                        END 
                                
                                    ", new
                                        {
                                            p.date_created,
                                            p.tire_id,
                                            p.location_id,
                                            p.rfid,
                                            process_id
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
                            
                        }
                        loncatSini:
                        conn.Close();
                    }




                   
                }
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Register succesfully. Rfid: " + p.rfid + " Register No:" + no_register + "";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }

        public static cRespon InsertRegisterVehicleProcess(cCrudRegisterVehicle p)
        {

            cRespon returnRes = new cRespon();
            returnRes.flag = true;
            returnRes.status = "";
            returnRes.message = "";
            string no_register = "";
            int RowAffected = 0;
            string process_id = "110";
            string description = "Register";
            string axis_id = "0";
            TimeSpan Tnow = DateTime.Now.TimeOfDay;
            DateTime date_created = p.date_created.Add(Tnow);

            string yearmonth = Convert.ToDateTime(p.date_created).ToString("yyyyMM");

            DataTable CheckEPC = getCheckEPCVhc(p.rfid);
            DataTable tmpCheckRfid = getCheckRfidRegisterVehicleStatus(p.rfid);
            if (CheckEPC.Rows.Count == 0)
            {
                returnRes.flag = false;
                returnRes.message = "#Rfid " + p.rfid + " not found.";
               
            }
            else {
                if (tmpCheckRfid.Rows.Count > 0)
                {
                    foreach (DataRow dr in tmpCheckRfid.Rows)
                    {
                        returnRes.flag = false;
                        returnRes.message = "#Rfid " + p.rfid + " already used. Register No: " + dr[1].ToString().Trim();
                    }
                }
                else
                {
                    if (p.vehicle_id != "0" && p.vehicle_id != "")
                    {
                        no_register = getNextRegisterNoVehicle(yearmonth);
                    }
                    else
                    {
                    }

                    string remark = "";
                    //string axis_id = "0";
                    string tire_mileage = "0";

                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString()))
                    {
                        conn.Open();
                        using (SqlTransaction tran = conn.BeginTransaction())
                        {
                            try
                            {

                                conn.Execute(@"IF (NOT EXISTS (select TOP 1 register_vhc_no from REGISTER_VHC where register_vhc_no=@no_register and vehicle_id=@vehicle_id) ) 
                                BEGIN 
                                    INSERT INTO REGISTER_VHC
                                 (register_vhc_no,date_created,vehicle_id,location_id,userid,reg_rfid,is_delete)
                                 values (@no_register,@date_created,@vehicle_id,@location_id,@userid,@rfid,0)
                                END 
                                
                            ", new
                                {
                                    no_register,
                                    p.date_created,
                                    p.vehicle_id,
                                    p.location_id,
                                    // p.trailer_id,
                                    p.userid,
                                    p.rfid
                                }, tran);




                                conn.Execute(@"IF ( EXISTS (select TOP 1 vehicle_id from VEHICLE where vehicle_id=@vehicle_id) ) 
                                BEGIN 
                                    UPDATE VEHICLE set vehicle_rfid=@rfid,date_modified=@date_created where vehicle_id=@vehicle_id
                                END 
                                
                            ", new
                                {
                                    p.date_created,
                                    p.vehicle_id,
                                    p.location_id,
                                    p.rfid,
                                    process_id
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




                    
                }
            }
            if (returnRes.flag == true)
            {
                returnRes.message = "Register Vehicle succesfully. Rfid: " + p.rfid + " Register No:" + no_register + "";
                returnRes.status = "success";
            }
            else
            {
                returnRes.status = "failed";
            }
            return returnRes;
        }
    }
}