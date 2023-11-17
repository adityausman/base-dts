using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BASE_RFID_TMS;

namespace BASE_RFID_TMS.Models
{

    public class Admin
    {
        //private REKIND_JTB_SPOOLEntities db = new REKIND_JTB_SPOOLEntities();
        //private readonly REKIND_JTB_SPOOLEntities _context = new REKIND_JTB_SPOOLEntities();
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());

        public class LoginViewModel
        {
            // [Required]
            // [Display(Name = "Email")]
            //  [EmailAddress]
            public string username { get; set; }
            public string user_name { get; set; }

            //[Required]
            [DataType(DataType.Password)]
           // [Display(Name = "Password")]
            public string password { get; set; }

            //public string status { get; set; }
            
            // [Display(Name = "Remember me?")]
            // public bool RememberMe { get; set; }
        }
        
        public class clsUsr
        {
            public string id { get; set; }
            public string username { get; set; }
            //public string status { get; set; }
            public string user_id { get; set; }
            public string is_admin { get; set; }
            public string name { get; set; }
            public string job_position { get; set; }
            //public string subcont_id { get; set; }
            //public string api_key { get; set; }
            //public string api_wms { get; set; }
            //public string api_icon { get; set; }
            public List<clsmenu> menu { get; set; }
        }

        public class clsmenu
        {
           // public List<clsmenu_detail> fabrication { get; set; }
            //public List<clsmenu_detail> field_installation { get; set; }
            public List<clsmenu_detail> admin { get; set; }
            public List<clsmenu_detail> utility { get; set; }
            //public string menu_id { get; set; }
            //public string menu_name { get; set; }
        }
        

        public class clsmenu_detail
        {
            public string menu_id { get; set; }
            public string menu_name { get; set; }
        }

        
        public static DataTable GetDataUser(LoginViewModel x)
        {
            DataTable dtJoint = new DataTable("usr");
            string StrQuery = "";
            StrQuery += "  SELECT  a.Id,a.Email,a.UserName,a.UserId,a.name,a.JobPosition,is_admin=[dbo].[GetCheckIsRoleAdmin](a.id)" + System.Environment.NewLine;
            //a.api_key,api_wms=([dbo].[GetSys_Settings]('api_wms')),api_icon=([dbo].[GetSys_Settings]('api_icon')),subcont_id"
            StrQuery += "  FROM AspNetUsers a  " + System.Environment.NewLine;
            //StrQuery += "  left join ZUSER_SUBCONT s on a.Id=s.userid" + System.Environment.NewLine;
            StrQuery += "  Where (Email=@user_name OR UserName=@user_name) and  PasswordSalt=@password and IsActive=1 " + System.Environment.NewLine;
            
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@user_name", x.user_name);
            cmd.Parameters.AddWithValue("@password", x.password);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.CommandTimeout = 0;
            con.Open();
            da.Fill(dtJoint);
            con.Close();
            return dtJoint;
        }

        public static DataTable GetDataMenu(string kduser, string parent)
        {
            DataTable dtJoint = new DataTable("usr");
            string StrQuery = "";
            //StrQuery += "  select Menu_ID,Menu_Name from menu " + System.Environment.NewLine;
            //StrQuery += "  where parent in ('300000','400000') --order by Sorting asc" + System.Environment.NewLine;

            StrQuery += "select me.Menu_ID,m.Menu_Name from AspNetUsers u" + System.Environment.NewLine;
            StrQuery += "inner join ZUSER_ROLES s on u.Id = s.Id" + System.Environment.NewLine;
            StrQuery += "inner join ZMENU_ROLE me on s.RoleId = me.RoleId" + System.Environment.NewLine;
            StrQuery += "inner join Zmenu m on me.Menu_ID = m.Menu_ID" + System.Environment.NewLine;
            StrQuery += "where u.UserId = '" + kduser + "' and m.is_handheld=1" + System.Environment.NewLine;
            StrQuery += "and u.IsActive = 1 and IsDelete = 0 and m.Parent = '"+ parent  + "'" + System.Environment.NewLine;

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
        
    }
}