using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace BASE_RFID_TMS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // New code
            config.EnableCors();
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));


            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //   Route  GET method

            // Route for POST method

            config.Routes.MapHttpRoute(
            name: "DefaultApi2",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional }
            );

            //   Route  GET method

            config.Routes.MapHttpRoute(
               name: "DefaultApi1",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { action = "get", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "DefaultApi3",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { action = "put", id = RouteParameter.Optional }
            );

           // config.Routes.MapHttpRoute(
           //   name: "DefaultApi4",
           //   routeTemplate: "api/{controller}/{action}/{id}",
           //   defaults: new { action = "put", id = RouteParameter.Optional }
           //);

            //config.Routes.MapHttpRoute(
            //name: "getListSpoolNdtRequest",
            //routeTemplate: "api/Ndt/getListSpoolNdtRequest/{project_code}/{spool_no}",
            //defaults: new
            //{
            //    action = "get",
            //    project_code = RouteParameter.Optional,
            //    spool_no = RouteParameter.Optional
            //});

           


        }
    }
}
