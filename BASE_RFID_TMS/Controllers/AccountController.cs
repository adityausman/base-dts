using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BASE_RFID_TMS.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web.Http.Cors;
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using BASE_RFID_TMS.Results;
using BASE_RFID_TMS.Providers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
//using Microsoft.Owin.Host.SystemWeb;

namespace BASE_RFID_TMS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Account")]

    public class AccountController : ApiController
    {
        public static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
        public Admin adm = new Admin();
       // private RFID_PIPE_SPOOLEntities1 db = new RFID_PIPE_SPOOLEntities1();
        private ApplicationUserManager _userManager;

        [HttpPost]
        [Route("getLockedUser")]
        public IHttpActionResult getLockedUser([FromBody]Master.cCrudFailedAccountLogin prm)
        {
            int data = 0;
            DataTable dtTempTable = new DataTable("aspnetusers");
            string StrQuery = "select AccessFailedCount as data from aspnetusers where email=@username";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@username", prm.Username);
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
            if (data >= 5)
            {
                return Json((new { status = "failed", msg = "User is locked" }));
            }
            else
            {
                return Json((new { status = "success", msg = "" }));
            }
        }

        [HttpPost]
        [Route("HistoryLogin")]
        public IHttpActionResult HistoryLogin([FromBody]Master.cCrudHistoryLogin prm)
        {
            var returnRes = Master.HistoryLogin(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("UpdateLogin")]
        public IHttpActionResult UpdateLogin([FromBody]Master.cCrudHistoryLogin prm)
        {
            var returnRes = Master.UpdateLogin(prm);
            return Json(returnRes);

        }
        [HttpGet]
        [Route("getTimeoutSys")]
        public IHttpActionResult getTimeoutSys()
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
            else {
                data = 5;
            }
            return Json((new { status = "success", msg = "", timeout = data }));

        }
        [HttpGet]
        [Route("getEmailBySAPID/{sap_id}")]
        public IHttpActionResult getEmailBySAPID(string sap_id)
        {
            string data = "";
            DataTable dtTempTable = new DataTable("aspnetusers");
            string StrQuery = "select Email as data from aspnetusers where SAPID=@sap_id";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = StrQuery;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@sap_id", sap_id);
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
            return Json((new { status = "success", msg = "", email= data }));
            
        }
        [HttpPost]
        [Route("setFailedAccountLogin")]
        public IHttpActionResult setFailedAccountLogin([FromBody]Master.cCrudFailedAccountLogin prm)
        {
            var returnRes = Master.SetFailedAccountLogin(prm);
            return Json(returnRes);

        }
        [HttpPost]
        [Route("resetFailedLogin")]
        public IHttpActionResult resetFailedLogin([FromBody]Master.cCrudFailedAccountLogin prm)
        {
            var returnRes = Master.ResetFailedLogin(prm);
            return Json(returnRes);

        }
        public AccountController()
        {
        }
        [HttpPost]
        [Route("checkUserExp")]
        public IHttpActionResult checkUserExp([FromBody]Admin.LoginViewModel prm)
        {

            string StrQuery = @"select * from aspNetUsers where username = @username";
            String message = "";
            Boolean showMessage = false;

            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ToString());
                conn.Open();
                DateTime today = DateTime.Today;
                DateTime date = conn.Query(StrQuery, new { prm.username }, null, true, commandTimeout: 0, commandType: null).ToList()[0].LastPasswordChangeDate;
                TimeSpan diff = today.Subtract(date);
                int maxExp = 30, alert = 23;// nanti diganti dari table database
                if (diff.Days >= maxExp)
                {
                    try
                    {
                        conn.Execute(@"
                                        BEGIN 
                                          update aspNetUsers set lockoutEnabled = 0 where username = @username
                                        END 
                                
                                    ", new
                        {
                            prm.username
                        });
                        showMessage = true;
                        message = "Your password has expired";
                    }
                    catch (Exception e)
                    {
                        showMessage = true;
                        message = e.Message.ToString();
                    }

                }
                else if (diff.Days >= alert)
                {
                    showMessage = true;
                    message = "your password will soon expire in " + (maxExp - diff.Days) + " days, please change your password before your account is locked";

                }
                conn.Close();
            }

            catch (Exception e)
            {
                showMessage = true;
                message = e.Message.ToString();
            }
            return Json((new
            {
                showMessage = showMessage,
                message = message,
            }));
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        //private ApplicationSignInManager _signInManager;
        //private REKIND_JTB_SPOOLEntities db = new REKIND_JTB_SPOOLEntities();
        // private readonly REKIND_JTB_SPOOLEntities _context = new REKIND_JTB_SPOOLEntities();
        // GET api/documentation
        /// <summary>
        /// Fungsi Login, Parameter: username, password
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        //public IHttpActionResult Register([FromBody]RegisterViewModel model)
        //{
        //    var data = new AspNetUsers();
        //    bool lresult = false;
        //    var msg = "";
        //}
        //[HttpPost]
        //[ResponseType(typeof(IEnumerable<Joint.ClsViewJoint>))]
       // [Route("api/account/getJoint/{Spool_No}")]
        public IHttpActionResult Login( [FromBody]Admin.LoginViewModel U)
        {
            bool pstatus = false;
            List<Admin.clsUsr> lstUsr = new List<Admin.clsUsr>();
            List<Admin.clsmenu> lstMenu = new List<Admin.clsmenu>();
            

            foreach (DataRow dr in Admin.GetDataUser(U).Rows)
                {
                    Admin.clsUsr usr = new Admin.clsUsr();
                    usr.id = dr[0].ToString().Trim();
                    usr.username = dr[2].ToString().Trim();
                    usr.user_id = dr[3].ToString().Trim();
                    usr.is_admin = dr[6].ToString().Trim();
                    usr.name = dr[4].ToString().Trim();
                    usr.job_position = dr[5].ToString().Trim();
                   // usr.subcont_id = dr[10].ToString().Trim();
                    //usr.api_key = dr[6].ToString().Trim();
                   // usr.api_wms = dr[7].ToString().Trim();
                    //usr.api_icon = dr[8].ToString().Trim();


                //List<Admin.clsmenu_detail> lstMenuFabrication = new List<Admin.clsmenu_detail>();
                //    foreach (DataRow drx in Admin.GetDataMenu(dr[3].ToString().Trim(), ConfigurationManager.AppSettings["menu_parent_fab"]).Rows)
                //    {
                //        Admin.clsmenu_detail m = new Admin.clsmenu_detail();
                //        m.menu_id = drx[0].ToString().Trim();
                //        m.menu_name = drx[1].ToString().Trim();
                //        lstMenuFabrication.Add(m);
                //    }

                //    List<Admin.clsmenu_detail> lstMenuField = new List<Admin.clsmenu_detail>();
                //    foreach (DataRow drx in Admin.GetDataMenu(dr[3].ToString().Trim(), ConfigurationManager.AppSettings["menu_parent_field"]).Rows)
                //    {
                //        Admin.clsmenu_detail m2 = new Admin.clsmenu_detail();
                //        m2.menu_id = drx[0].ToString().Trim();
                //        m2.menu_name = drx[1].ToString().Trim();
                //        lstMenuField.Add(m2);
                //    }

                    List<Admin.clsmenu_detail> lstMenuAdmin = new List<Admin.clsmenu_detail>();
                    foreach (DataRow drx in Admin.GetDataMenu(dr[3].ToString().Trim(), ConfigurationManager.AppSettings["menu_parent_admin"]).Rows)
                    {
                        Admin.clsmenu_detail m3 = new Admin.clsmenu_detail();
                        m3.menu_id = drx[0].ToString().Trim();
                        m3.menu_name = drx[1].ToString().Trim();
                        lstMenuAdmin.Add(m3);
                    }

                    List<Admin.clsmenu_detail> lstMenuUtility = new List<Admin.clsmenu_detail>();
                    foreach (DataRow drx in Admin.GetDataMenu(dr[3].ToString().Trim(), ConfigurationManager.AppSettings["menu_parent_utility"]).Rows)
                    {
                        Admin.clsmenu_detail m4 = new Admin.clsmenu_detail();
                        m4.menu_id = drx[0].ToString().Trim();
                        m4.menu_name = drx[1].ToString().Trim();
                        lstMenuUtility.Add(m4);
                    }

                Admin.clsmenu mn = new Admin.clsmenu();
                   // mn.fabrication = lstMenuFabrication;
                   // mn.field_installation = lstMenuField;
                     mn.admin = lstMenuAdmin;
                    mn.utility = lstMenuUtility;
                    lstMenu.Add(mn);

                    usr.menu = lstMenu;
                    lstUsr.Add(usr);
                }
           
                return Json((new { status = Convert.ToBoolean(lstUsr.LongCount()), data = lstUsr }));
        }
        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();
            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }
        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Json((new { status = "success"}));
        }
        //// GET api/documentation
        ///// <summary>
        ///// Fungsi Login, Parameter: username, password
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IHttpActionResult> ChangePassword([FromBody]ChangePasswordModel m)
        //{
        //    var data = new AspNetUsers();
        //    bool lresult = false;
        //    string msg = "";
        //    if (!ModelState.IsValid)
        //    {
        //        //return View(model);

        //        foreach (var error in ModelState.Values)
        //        {
        //            if (error.Errors.Count > 0)
        //            {
        //                msg = error.Errors[0].ErrorMessage.ToString();
        //            }
        //        }

        //        return Json(new { result = lresult, msg, data });
        //    }
        //    //var token = UserManager.GeneratePasswordResetToken(m.Code);
        //    //if (token == null)
        //    //{
        //    //    // Don't reveal that the user does not exist
        //    //    //return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    //    return Json(new { result = lresult, msg = "the user does not exist", data });
        //    //}
        //   // var sign = UserManager.FindByEmailAsync(m.Email);
        //    //var sign = UserManager.Find(m.Email, m.CurrentPassword);
        //    //var sign = await SignInManager.PasswordSignInAsync(m.Email, m.CurrentPassword, false, shouldLockout: false);
        //    //if (sign != null)//== SignInStatus.Success)
        //   // {
        //        //var user = await UserManager.FindByNameAsync(m.Email);
        //        // var result = await UserManager.ChangePasswordAsync(m.Code, m.CurrentPassword, m.Password);
        //        //var result = await UserManager.ResetPasswordAsync(m.Code, token, m.Password);
        //        IdentityResult result = await UserManager.ChangePasswordAsync(m.Code, m.CurrentPassword,
        //            m.Password);
        //        if (result.Succeeded)
        //        {
        //            //int RowAffected = 0;
        //            //AspNetUser items = new AspNetUser();
        //            //items.Id = m.Code;
        //            //items.Email = m.Email;
        //            //items.PasswordSalt = m.Password;
        //            //items.IsDelete = false;
        //            //string query = "UPDATE AspNetUsers set PasswordSalt='"+ items.PasswordSalt + "' ";
        //            //RowAffected = con.Execute(query);

        //            AspNetUser items = new AspNetUser();
        //            items = db.AspNetUsers.First(p => p.Id == m.Code);
        //            items.Email = m.Email;
        //            items.PasswordSalt = m.Password;
        //            items.IsDelete = false;
        //            db.SaveChanges();
        //            data = items;
        //            //data.Email = "masukk";
        //            //if (RowAffected > 0)
        //            //{
        //            msg = "Change Password Succesfully";
        //            lresult = true;
        //            //}
        //            //else
        //            //{
        //            //    msg = "Change Password unsuccesfully";
        //            //    lresult = false;
        //            //}

        //            //return RedirectToAction("ResetPasswordConfirmation", "Account");
        //        }
        //        else
        //        {
        //            msg = ((string[])result.Errors)[0];
        //        }
        //        AddErrors(result);

        //        return Json(new { result = lresult, msg, data });
        //        //}
        //        ////else if (sign == SignInStatus.LockedOut)
        //        ////{
        //        ////    return Json(new { result = lresult, msg = "The User is locked", data });
        //        ////}
        //        ////else if (sign == SignInStatus.RequiresVerification)
        //        ////{
        //        ////    return Json(new { result = lresult, msg = "The User is not verified", data });
        //    //}
        //    //else
        //    //{
        //    //    data.Email = "gagal";
        //    //    return Json(new { result = lresult, msg = "Current Password is wrong", data });
        //    //}
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    //IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), m.CurrentPassword,
        //    //    m.Password);

        //    //if (!result.Succeeded)
        //    //{
        //    //    return GetErrorResult(result);
        //    //}

        //    //return Ok();
        //}
        //[HttpPost]
        //public async System.Threading.Tasks.Task<IHttpActionResult> ChangePassword([FromBody]ChangePasswordModel m)
        //{
        //    //db.Configuration.ProxyCreationEnabled = false;
        //    var data = new AspNetUser();
        //    bool lresult = false;
        //    string msg = "";
        //    if (!ModelState.IsValid)
        //    {
        //        //return View(model);

        //        foreach (var error in ModelState.Values)
        //        {
        //            if (error.Errors.Count > 0)
        //            {
        //                msg = error.Errors[0].ErrorMessage.ToString();
        //            }
        //        }

        //        return Json(new { result = lresult, msg, data });
        //    }
        //    var token = UserManager.GeneratePasswordResetToken(m.Code);
        //    if (token == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        //return RedirectToAction("ResetPasswordConfirmation", "Account");
        //        return Json(new { result = lresult, msg = "the user does not exist", data });
        //    }
        //    var sign = UserManager.FindByEmailAsync(m.Email);
        //    //var sign = UserManager.Find(m.Email, m.CurrentPassword);
        //    //var sign = await SignInManager.PasswordSignInAsync(m.Email, m.CurrentPassword, false, shouldLockout: false);
        //    if (sign != null)//== SignInStatus.Success)
        //    {

        //           // var result = await UserManager.ChangePasswordAsync(m.Code, m.CurrentPassword, m.Password);
        //        var result = await UserManager.ResetPasswordAsync(m.Code, token, m.Password);
        //        if (result.Succeeded)
        //        {
        //            //int RowAffected = 0;
        //            //AspNetUser items = new AspNetUser();
        //            //items.Id = m.Code;
        //            //items.Email = m.Email;
        //            //items.PasswordSalt = m.Password;
        //            //items.IsDelete = false;
        //            //string query = "UPDATE AspNetUsers set PasswordSalt='"+ items.PasswordSalt + "' ";
        //            //RowAffected = con.Execute(query);

        //            AspNetUser items = new AspNetUser();
        //            items = db.AspNetUsers.First(p => p.Id == m.Code);
        //            items.Email = m.Email;
        //            items.PasswordSalt = m.Password;
        //            items.IsDelete = false;
        //            db.SaveChanges();
        //            data = items;
        //            //data.Email = "masukk";
        //            //if (RowAffected > 0)
        //            //{
        //                msg = "Change Password Succesfully";
        //                lresult = true;
        //            //}
        //            //else
        //            //{
        //            //    msg = "Change Password unsuccesfully";
        //            //    lresult = false;
        //            //}

        //            //return RedirectToAction("ResetPasswordConfirmation", "Account");
        //        }
        //        else
        //        {
        //            msg = ((string[])result.Errors)[0];
        //        }
        //        AddErrors(result);

        //        return Json(new { result = lresult, msg, data });
        //    //}
        //    ////else if (sign == SignInStatus.LockedOut)
        //    ////{
        //    ////    return Json(new { result = lresult, msg = "The User is locked", data });
        //    ////}
        //    ////else if (sign == SignInStatus.RequiresVerification)
        //    ////{
        //    ////    return Json(new { result = lresult, msg = "The User is not verified", data });
        //    }
        //    else
        //    {
        //        data.Email = "gagal";
        //        return Json(new { result = lresult, msg = "Current Password is wrong", data });
        //    }
        //}

        //public class ChangePasswordModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    [Display(Name = "Email")]
        //    public string Email { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Current Password")]
        //    public string CurrentPassword { get; set; }

        //    [Required]
        //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //    [DataType(DataType.Password)]
        //    [Display(Name = "Password")]
        //    public string Password { get; set; }

        //    [DataType(DataType.Password)]
        //    [Display(Name = "Confirm password")]
        //    [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //    public string ConfirmPassword { get; set; }

        //    public string Code { get; set; }
        //}

        //public ApplicationUserManager UserManager
        //{

        //    get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    //HttpContext.Current
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}
        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}
        //public class ApplicationUserManager : UserManager<ApplicationUser>
        //{
        //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        //        : base(store)
        //    {
        //    }

        //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //    {
        //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        //        // Configure validation logic for usernames
        //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //        {
        //            AllowOnlyAlphanumericUserNames = false,
        //            RequireUniqueEmail = true
        //        };

        //        // Configure validation logic for passwords
        //        manager.PasswordValidator = new PasswordValidator
        //        {
        //            RequiredLength = 6,
        //            RequireNonLetterOrDigit = true,
        //            RequireDigit = true,
        //            RequireLowercase = true,
        //            RequireUppercase = true,
        //        };

        //        // Configure user lockout defaults
        //        manager.UserLockoutEnabledByDefault = true;
        //        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //        manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        //        // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //        // You can write your own provider and plug it in here.
        //        manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
        //        {
        //            MessageFormat = "Your security code is {0}"
        //        });
        //        manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
        //        {
        //            Subject = "Security Code",
        //            BodyFormat = "Your security code is {0}"
        //        });
        //        manager.EmailService = new EmailService();
        //        manager.SmsService = new SmsService();
        //        var dataProtectionProvider = options.DataProtectionProvider;
        //        if (dataProtectionProvider != null)
        //        {
        //            manager.UserTokenProvider =
        //                new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //        }
        //        return manager;
        //    }
        //}
        //public bool SignIn(string Email, string Password)
        //{
        //    var SignInManualReturn = false;
        //    List<USERS> Items = _context.USERS.ToList();
        //    Items = _context.USERS.ToList()
        //        .Where(
        //            uk => (uk.email == Email && uk.password == Password && uk.is_aktif == true
        //                   )).ToList();
        //    int Count = Items.Count();

        //    if (Count >= 1)
        //    {
        //        SignInManualReturn = true;
        //    }
        //    return SignInManualReturn;
        //}
        //public async Task<ActionResult> Login(Admin.LoginViewModel model, string returnUrl)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            //return RedirectToAction("Manage/Home");
        //            //return RedirectToLocal("Manage/Home");
        //            // return RedirectToRoute("Manage");

        //            if (Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Manage", "Home");
        //            }

        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}
        [Authorize]
        [HttpGet]
        public IHttpActionResult Hello()
        {
            string hello = "Hello World";
            return Json(hello);
        }
    }

}
