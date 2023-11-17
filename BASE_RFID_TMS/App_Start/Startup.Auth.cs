using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Owin;
using BASE_RFID_TMS.Providers;
using BASE_RFID_TMS.Models;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BASE_RFID_TMS
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
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
        public static TimeSpan AccessTokenExpireTimeSpan_ { get; set; } = TimeSpan.FromMinutes(globalVar.GetTimeoutSys());
        public void ConfigureAuth(IAppBuilder app)
        {
            app.Use<MyOwinMiddleware>();
            // Configure the db context and user manager to use a single instance per request
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            int timeout = globalVar.GetTimeoutSys();
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = AccessTokenExpireTimeSpan_,
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // // Enable the application to use bearer tokens to authenticate users
            //enable disable token
            app.UseOAuthBearerTokens(OAuthOptions);




            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
        //public void ConfigureAuth(IAppBuilder app)
        //{
            
        //    // Konfigurasi context database dan manager
        //    app.CreatePerOwinContext(ApplicationDbContext.Create);
        //    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

        //    // Konfigurasi authentikasi cookie
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
        //        LoginPath = new PathString("/Token"),
        //        Provider = new CookieAuthenticationProvider
        //        {
        //            OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
        //                validateInterval: TimeSpan.FromMinutes(30),
        //                regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
        //        }
        //    });

        //    app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        //    app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

        //    app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

        //    var dataProtectionProvider = app.GetDataProtectionProvider();
        //    var manage = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    if (dataProtectionProvider != null)
        //    {
        //        var dataProtector = dataProtectionProvider.Create("ASP.NET Identity");

        //        manage.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
        //            dataProtector)
        //        {
        //            TokenLifespan = TimeSpan.FromHours(24)
        //        };
        //    }

        //    // Konfigurasi fitur lockout
        //    manage.UserLockoutEnabledByDefault = true;
        //    manage.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    manage.MaxFailedAccessAttemptsBeforeLockout = 3;
        //    //app.UseLockout(new AccountLockoutOptions
        //    //{
        //    //    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15),
        //    //    MaxFailedAccessAttemptsBeforeLockout = 5,
        //    //    // ...
        //    //});
        //}
        public class MyOwinMiddleware : OwinMiddleware
        {
            public MyOwinMiddleware(OwinMiddleware next) :
                base(next)
            { }

            public override async Task Invoke(IOwinContext context)
            {
                var request = context.Request;
                var response = context.Response;

                response.OnSendingHeaders(state =>
                {
                    var resp = (IOwinResponse)state;

                    // without this headers -> client apps will be blocked to consume data from this api
                    if (!resp.Headers.ContainsKey("Access-Control-Allow-Origin"))
                        resp.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                    if (!resp.Headers.ContainsKey("Access-Control-Allow-Headers"))
                        resp.Headers.Add("Access-Control-Allow-Headers", new[] { "*" });
                    if (!resp.Headers.ContainsKey("Access-Control-Allow-Methods"))
                        resp.Headers.Add("Access-Control-Allow-Methods", new[] { "*" });

                    // by default owin is blocking options not from same origin with MethodNotAllowed
                    if (resp.StatusCode == (int)HttpStatusCode.MethodNotAllowed &&
                        HttpMethod.Options == new HttpMethod(request.Method))
                    {
                        resp.StatusCode = (int)HttpStatusCode.OK;
                        resp.ReasonPhrase = HttpStatusCode.OK.ToString();
                    }

                }, response);

                await Next.Invoke(context);
            }
        }

    }

    internal class AccountLockoutOptions
    {
        public TimeSpan DefaultLockoutTimeSpan { get; set; }
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }
    }
}
