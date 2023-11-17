
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

namespace BASE_RFID_TMS.Models
{
    public class Report
    {
        
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());

        public static string qGetListTire(string serial_number, string tire_name, string supplier_id, string brand_id, string vehicle_id)
        {
            string StrQuery = @"SELECT         T.tire_id, T.serial_number, T.tire_name, T.size, T.pattern, 

                                                            i.brand_tire_id, B.brand_tire_name, i.supplier_id, 
                                                         S.supplier_name, G.register_tire_no, CONVERT(varchar, G.date_created, 106) AS register_date,  G.reg_rfid as rfid, V.vehicle_id, v.vehicle_name,
                                                         a.tire_no, a.axis_no, a.axis_type, AE.axis_type_name,  a.description AS tire_position, a.axis_name, T.location_id, l.location, T.date_modified, 
                                                         CONVERT(varchar, T.date_modified, 106) AS str_date_modified, T.km as tire_mileage, T.status_id, ISNULL(CONVERT(varchar, dbo.GetNextInspectionDate(T.tire_id), 106), '-')
                                                         AS next_inspection_date, ISNULL(dbo.GetStatusTireInspectionDate(T.tire_id), 'Never Been Inspected') AS next_inspection_status, ISNULL(dbo.GetStatusTireInspectionDateDay(T.tire_id), '-') AS next_inspection_day,
                                                         ISNULL(dbo.GetLastInspectionDate(T.tire_id), '-') AS last_inspection_date
                                FROM TIRE AS T

                                                INNER JOIN ITEM i on i.tire_code = t.tire_code

                                                INNER JOIN TIRE_AXIS TAX on TAX.tire_id = T.tire_id  and tax.is_delete=0

                                                        LEFT OUTER JOIN AXIS AS a ON TAX.axis_id = a.axis_id LEFT OUTER JOIN
                                                         AXIS_TYPE AS AE ON a.axis_type = AE.axis_type INNER JOIN
                                                         VEHICLE AS v ON tax.vehicle_id = v.vehicle_id INNER JOIN
                                                         SUPPLIER AS S ON i.supplier_id = S.supplier_id LEFT OUTER JOIN
                                                         BRAND_TIRE AS B ON i.brand_tire_id = B.brand_tire_id INNER JOIN
                                                         LOCATION AS l ON T.location_id = l.location_id INNER JOIN
                                                         REGISTER_TIRE AS G ON T.tire_id = G.tire_id 
                                " + System.Environment.NewLine;
            //StrQuery += "t.size,t.pattern,t.brand_id,B.brand_name,t.supplier_id,s.supplier_name,G.register_no,convert(varchar,G.date_created,106) as register_date,G.rfid," + System.Environment.NewLine;
            //StrQuery += "t.vehicle_id,v.vehicle_name,v.vehicle_no,v.is_vehicle,vehicle_setup_no=[dbo].[GetLastVehicleSetupNo](t.vehicle_id),t.axis_id,a.tire_no,a.axis_no,a.axis_position,a.axis_type,ae.axis_type_name,a.tire_axis_position,a.description as tire_position,a.axis_name," + System.Environment.NewLine;
            //StrQuery += "t.location_id,l.location,t.process_id,p.process,t.date_modified,convert(varchar,t.date_modified,106) as str_date_modified,t.tire_mileage,tire_axis_no=[dbo].[GetLastTireAxisNoByTire](t.tire_id)," + System.Environment.NewLine;
            //StrQuery += "is_active=T.is_active,next_inspection_date=isnull(convert(varchar,[dbo].[GetNextInspectionDate](t.tire_id),106),'-'),next_inspection_status=isnull(dbo.[GetStatusTireInspectionDate](t.tire_id),'Never Been Inspected'),next_inspection_day=isnull(dbo.[GetStatusTireInspectionDateDay](t.tire_id),'-'),last_inspection_date=isnull([dbo].[GetLastInspectionDate](t.tire_id),'-') from " + System.Environment.NewLine;
            //StrQuery += "TIRE T" + System.Environment.NewLine;
            //StrQuery += "left join axis a on t.axis_id=a.axis_id" + System.Environment.NewLine;
            //StrQuery += "LEFT join AXIS_TYPE AE ON A.axis_type=AE.axis_type" + System.Environment.NewLine;
            //StrQuery += "inner join VEHICLE v on t.vehicle_id=v.vehicle_id" + System.Environment.NewLine;
            //StrQuery += "--inner join TRAILER tr on t.trailer_id=tr.trailer_id" + System.Environment.NewLine;
            //StrQuery += "inner join SUPPLIER S on T.supplier_id=S.supplier_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN BRAND B on T.brand_id=B.brand_id" + System.Environment.NewLine;
            //StrQuery += "inner join location l on t.location_id=l.location_id" + System.Environment.NewLine;
            //StrQuery += "inner join process p on t.process_id=p.process_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN REGISTER G on T.tire_id=G.tire_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN RECEIVE_DETAIL RD on RD.serial_number=T.tire_id" + System.Environment.NewLine;
            //StrQuery += "LEFT JOIN RECEIVE R on RD.receive_no=R.receive_no" + System.Environment.NewLine;

            string prm = "";
            if (serial_number != "0" && serial_number != "" && serial_number != "-")
            {
                prm = "where t.serial_number='" + serial_number + "'" + System.Environment.NewLine;
            }

            if (tire_name != "0" && tire_name != "" && tire_name != "-")
            {
                if (prm != "")
                {
                    prm += " where T.tire_name LIKE '%" + tire_name + "%'" + System.Environment.NewLine;
                }
                else
                {
                    prm = " and T.tire_name LIKE '%" + tire_name + "%'" + System.Environment.NewLine;
                }

            }

            if (supplier_id != "0" && supplier_id != "" && supplier_id != "-")
            {
                if (prm != "")
                {
                    prm += " where i.supplier_id ='" + supplier_id + "'" + System.Environment.NewLine;
                }
                else
                {
                    prm = " and i.supplier_id ='" + supplier_id + "'" + System.Environment.NewLine;
                }

            }

            if (brand_id != "0" && brand_id != "" && brand_id != "-")
            {
                if (prm != "")
                {
                    prm += " where i.brand_tire_id ='" + brand_id + "'" + System.Environment.NewLine;
                }
                else
                {
                    prm = " and i.brand_tire_id ='" + brand_id + "'" + System.Environment.NewLine;
                }

            }

            if (vehicle_id != "0" && vehicle_id != "" && vehicle_id != "-")
            {
                if (prm != "")
                {
                    prm += " where V.vehicle_id ='" + vehicle_id + "'" + System.Environment.NewLine;
                }
                else
                {
                    prm = " and V.vehicle_id ='" + vehicle_id + "'" + System.Environment.NewLine;
                }

            }
            //StrQuery += "where t.serial_number='SN000010' OR T.tire_name LIKE '%D%'" + System.Environment.NewLine;
            //StrQuery += "OR T.supplier_id='s001'" + System.Environment.NewLine;
            //StrQuery += "or t.brand_id=1" + System.Environment.NewLine;
            //StrQuery += "OR T.vehicle_id='v001'" + System.Environment.NewLine;
            StrQuery += prm + System.Environment.NewLine;
            StrQuery += "order by t.serial_number " + System.Environment.NewLine;

            return StrQuery;
        }

        //public static DataTable getDataReportMutasi(string date_start, string date_end)
        //{
        //    DataTable dtTempTable = new DataTable("ASSET");
        //    string StrQuery = "select tgl_mutasi=convert(varchar,m.tgl_mutasi,106),m.user_id,m.ket_mutasi,m.no_ba,m.tgl_ba," + System.Environment.NewLine;
        //    StrQuery += "md.no_register,md.location_id,L.location,L2.location as parent," + System.Environment.NewLine;
        //    StrQuery += "md.kd_unit_kerja,md.kd_ruang,b.kd_barang,b.nama_barang, k.nama_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "from MUTASI m" + System.Environment.NewLine;
        //    StrQuery += "inner join MUTASI_DETAIL md on m.tgl_mutasi=md.tgl_mutasi " + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN REGISTRASI R on md.no_register=R.no_register" + System.Environment.NewLine;
        //    StrQuery += "INNER Join BARANG b on R.kd_barang=b.kd_barang" + System.Environment.NewLine;
        //    StrQuery += "inner join KELOMPOK_BARANG k on b.kd_kel_barang=k.kd_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "inner join [LOCATION] L on md.location_id=L.location_id" + System.Environment.NewLine;
        //    StrQuery += "inner join [LOCATION] L2 on L.parent=L2.location_id" + System.Environment.NewLine;
        //   // StrQuery += "where cast(m.tgl_mutasi as date) between cast('2021-05-01' as date) and cast('2021-05-28' as date)" + System.Environment.NewLine;


        //    string Strprm = "";
        //    if (date_start !="0" && date_start != "-" && date_start != "")
        //    {
        //        Strprm += "where cast(m.tgl_mutasi as date)  between cast('" + date_start + "' as date) and cast('"+ date_end + "' as date)" + System.Environment.NewLine;
        //    }

        //    //if (receive != "0" && receive != "-" && receive != "")
        //    //{
        //    //    if (Strprm != "")
        //    //    {
        //    //        Strprm += "and p.NO_TERIMA like '%" + receive + "%'" + System.Environment.NewLine;
        //    //    }
        //    //    else
        //    //    {
        //    //        Strprm += "where p.NO_TERIMA like '%" + receive + "%'" + System.Environment.NewLine;
        //    //    }
        //    //}

        //    StrQuery = StrQuery + Strprm;

        //    StrQuery += "order by m.tgl_mutasi desc" + System.Environment.NewLine;


        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = StrQuery;
        //    cmd.Connection = con;
        //    //cmd.Parameters.AddWithValue("@asset_no", asset_no);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.SelectCommand.CommandTimeout = 0;
        //    con.Open();
        //    da.Fill(dtTempTable);
        //    con.Close();
        //    return dtTempTable;
        //}


        //public static DataTable getDataReportInspeksi(string date_start, string date_end)
        //{
        //    DataTable dtTempTable = new DataTable("ASSET");
        //    string StrQuery = "select tgl_inspeksi=convert(varchar,i.tgl_inspeksi,106),i.ket_inspeksi,i.no_register,i.id_kondisi,n.kondisi," + System.Environment.NewLine;
        //    StrQuery += "i.user_id,b.kd_barang,b.nama_barang, k.nama_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "from INSPEKSI i" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN REGISTRASI R on i.no_register=R.no_register" + System.Environment.NewLine;
        //    StrQuery += "INNER Join BARANG b on R.kd_barang=b.kd_barang" + System.Environment.NewLine;
        //    StrQuery += "inner join KELOMPOK_BARANG k on b.kd_kel_barang=k.kd_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "inner join KONDISI n on n.id_kondisi=i.id_kondisi" + System.Environment.NewLine;
        //    //StrQuery += "where cast(i.tgl_inspeksi as date) between cast('2021-05-01' as date) and cast('2021-05-28' as date)" + System.Environment.NewLine;



        //    string Strprm = "";
        //    if (date_start != "0" && date_start != "-" && date_start != "")
        //    {
        //        Strprm += "where cast(i.tgl_inspeksi as date)  between cast('" + date_start + "' as date) and cast('" + date_end + "' as date)" + System.Environment.NewLine;
        //    }

        //    //if (receive != "0" && receive != "-" && receive != "")
        //    //{
        //    //    if (Strprm != "")
        //    //    {
        //    //        Strprm += "and p.NO_TERIMA like '%" + receive + "%'" + System.Environment.NewLine;
        //    //    }
        //    //    else
        //    //    {
        //    //        Strprm += "where p.NO_TERIMA like '%" + receive + "%'" + System.Environment.NewLine;
        //    //    }
        //    //}

        //    StrQuery = StrQuery + Strprm;

        //    StrQuery += "order by i.tgl_inspeksi desc" + System.Environment.NewLine;


        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = StrQuery;
        //    cmd.Connection = con;
        //    //cmd.Parameters.AddWithValue("@asset_no", asset_no);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.SelectCommand.CommandTimeout = 0;
        //    con.Open();
        //    da.Fill(dtTempTable);
        //    con.Close();
        //    return dtTempTable;
        //}

        //public static DataTable getDataAssetPerLocation(string location_id, string category)
        //{
        //    DataTable dtTempTable = new DataTable("ASSET");
        //    string strparam = "";
        //    string StrQuery = "select parent as kategori,full_location,jml=count(kd_barang) from (" + System.Environment.NewLine;
        //    StrQuery += "select R.rfid,R.no_register,B.kd_barang,B.nama_barang,B.kd_kel_barang,K.nama_kel_barang," + System.Environment.NewLine;
        //    StrQuery += "R.location_id,L.location,full_location=[dbo].[getFullParentLocation](R.location_id)+' - '+location," + System.Environment.NewLine;
        //    StrQuery += "B.merk_default as merk,kd_parent=isnull(k.parent,k.kd_kel_barang),parent=isnull(k2.nama_kel_barang,k.nama_kel_barang) " + System.Environment.NewLine;
        //    StrQuery += "from REGISTRASI R" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN BARANG B ON R.kd_barang=B.kd_barang" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN PENERIMAAN_DETAIL P on B.kd_barang=p.kd_barang" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN KELOMPOK_BARANG K on B.kd_kel_barang=k.kd_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "LEFT JOIN KELOMPOK_BARANG K2 on k.parent=k2.kd_kel_barang" + System.Environment.NewLine;
        //    StrQuery += "INNER JOIN LOCATION L ON R.location_id=L.location_id" + System.Environment.NewLine;


        //    if (location_id!="" && location_id!="-" && location_id!="0")
        //    {
        //        if (Master.getIsDetailLocation(location_id) == true)
        //        {
        //            strparam = "where R.location_id='" + location_id + "'" + System.Environment.NewLine;

        //        }
        //        else
        //        {
        //            strparam = "where R.location_id like '%" + location_id + "%'" + System.Environment.NewLine;


        //        }
        //        //strparam = "where (R.location_id='" + location_id + "' or R.location_id in (select distinct location_id from location ll where ll.parent='" + location_id + "'))" + System.Environment.NewLine;

        //    }

        //    if (category != "" && category != "-" && category != "0")
        //    {
        //        if (strparam!="")
        //        {
        //            strparam += " and (k.kd_kel_barang='" + category + "' or k.parent='" + category + "')" + System.Environment.NewLine;

        //        }
        //        else
        //        {
        //            strparam = " where (k.kd_kel_barang='" + category + "' or k.parent='" + category + "')" + System.Environment.NewLine;

        //        }
        //    }
        //    StrQuery += strparam;
        //    StrQuery += ")x" + System.Environment.NewLine;
        //    StrQuery += "group by parent,full_location" + System.Environment.NewLine;
        //    StrQuery += "order by parent" + System.Environment.NewLine;



        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = StrQuery;
        //    cmd.Connection = con;
        //    //cmd.Parameters.AddWithValue("@location_id", location_id);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.SelectCommand.CommandTimeout = 0;
        //    con.Open();
        //    da.Fill(dtTempTable);
        //    con.Close();
        //    return dtTempTable;
        //}
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
        public static DataTable getDataTireTracking(string StartDate= null, string EndDate = null, string UserId = null)
        {
            DataTable dtJoint = new DataTable("SPOOL");
            string roleUser = getRoleUser(UserId);
            string StrQuery = @" select 
                                    UPPER(Ti.tire_name) as tire_name, Ti.tire_id, Ti.rfid, l.location, l.location_id,  
                                       P.process,
                                T.userid, a.Name as username, T.date_created 
                                   
                                from TIRE_TRACKING T
                                inner join TIRE Ti on Ti.tire_id = T.tire_id
                                inner join AspNetUsers a on t.userid=a.UserId
                                inner join LOCATION l on t.location_id=l.location_id
                                inner join PROCESS p on t.process_id=p.process_id
								left join SERV_MAINT_OBSERVATION i on i.tire_id=t.tire_id 
								left join SERV_MAINT_OBSERVATION_DETAIL o on i.smo_id=o.smo_id
								left join OBSERVATION_ITEM b on o.observation_item_id=b.observation_item_id
                               
	                               " + System.Environment.NewLine; //and t.process_id = '400'
            //,o.observation_item_id,isnull(b.observation_item_name, '') as observation,
				        //    o.value, o.notes, b.observation_item_unit
            //string StrQuery = @"
            //                        select 
            //                        UPPER(Ti.tire_name) as tire_name, Ti.tire_id, T.rfid, loc.location, loc.location_id, loc.[group],     
            //                           P.process,
            //                    T.userid, a.Name as username, T.date_created 
            //                        sort=row_number() over (order by t.date_created desc),t.smo_id,t.date_created,convert(varchar,t.date_created,106) as str_date,convert(varchar,t.date_created,108) as str_time,
            //                                t.location_id,l.location,t.process_id,p.process,ti.rfid,t.tire_id,t.userid,a.name,
            //                                is_detail=case when t.process_id in ('400') then 1 else 0 end 
            //                    from TIRE_TRACKING T
            //                    inner join TIRE Ti on Ti.tire_id = T.tire_id
            //                    inner join AspNetUsers a on t.userid=a.UserId
            //                    inner join LOCATION l on t.location_id=l.location_id
            //                    inner join PROCESS p on t.process_id=p.process_id
            //                    where t.tire_id=@tire_id
            //                    order by t.date_created desc

            //                    select UPPER(Ti.tire_name) as tire_name, Ti.tire_id, T.rfid, loc.location, loc.location_id, loc.[group], 
            //                    P.process,
            //                    T.userid, a.Name as username, T.date_created 
            //                    , OB.observation
            //                    from TRACKING T
            //                     INNER JOIN TIRE Ti ON T.tire_id = ti.tire_id
            //                     INNER JOIN PROCESS P ON T.process_id = P.process_id
            //                     LEFT JOIN INSPECTION_DETAIL_OBSERVATION IDO ON IDO.tire_id = Ti.tire_id
            //                     LEFT JOIN OBSERVATION OB ON OB.observation_id = IDO.observation_id
            //                     INNER JOIN LOCATION loc ON T.location_id = loc.location_id
            //                     INNER JOIN AspNetUsers a ON T.userid = a.UserId
            //                    " + System.Environment.NewLine;
            if (roleUser != "0")
            {
                StrQuery += "WHERE T.UserId=@UserId" + System.Environment.NewLine;
                if (StartDate != "0")
                {
                    StrQuery += " AND Cast(convert(varchar,T.DATE_CREATED,110) as date) between @StartDate and @EndDate" + System.Environment.NewLine;

                }
            }
            else
            {
                if (StartDate != "0")
                {
                    StrQuery += " WHERE Cast(convert(varchar,T.DATE_CREATED,110) as date) between @StartDate and @EndDate" + System.Environment.NewLine;

                }
            }
            
            

            StrQuery += @" group by Ti.tire_name, Ti.tire_id, Ti.rfid, l.location, l.location_id,  
                                       P.process_id,P.process,
                                T.userid, a.Name, T.date_created 
order by T.date_created desc
" + System.Environment.NewLine;

            

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtJoint);
            con.Close();
            return dtJoint;
        }
        public static DataTable getDataTireBySerialNumber(string Text = null)
        {
            DataTable dtJoint = new DataTable("SPOOL");
            string StrQuery = @"select tire_id, tire_name from TIRE " + System.Environment.NewLine;
            StrQuery += "WHERE tire_id like @Text or UPPER(tire_name) like UPPER(@Text)" + System.Environment.NewLine;
           

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@Text", Text + "%");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtJoint);
            con.Close();
            return dtJoint;
        }
    }
}