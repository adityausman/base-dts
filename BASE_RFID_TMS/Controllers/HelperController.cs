

using Newtonsoft.Json;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BASE_RFID_TMS.Models;
using System.Configuration;

namespace BASE_RFID_TMS.Controllers
{
    public class HelperController : Controller
    {
        public class Token
        {
            [JsonProperty("flag")]
            public Boolean Flag { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }
            [JsonProperty("error")]
            public string Error { get; set; }
            [JsonProperty("error_description")]
            public string Error_description { get; set; }

        }

        //public ActionResult GetToken(string user, string pass)
        //{
        //    string baseAddress = "http://" + System.Configuration.ConfigurationManager.AppSettings["baseurl"];
        //    //string strpSalt = AesOperation.DecryptString(ConfigurationManager.AppSettings["encrypt"], pass);
        //    Token t = new Token();
        //    List<Token> lToken = new List<Token>();
        //    using (var client = new HttpClient())
        //    {
        //        var form = new Dictionary<string, string>
        //       {
        //           {"grant_type", "password"},
        //           {"username", user},
        //           {"password",pass},
        //       };

        //        try
        //        {
        //            var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(form)).Result;
        //            //var token = t1LBf6zWdVYz9oN1Puctivrt8CLk6kburAP  \
        //            var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
        //            //t = token;
        //            if (string.IsNullOrEmpty(token.Error))
        //            {
        //                t.Flag = true;
        //                t.Message = "Succesfully";
        //                t.AccessToken = token.AccessToken;
        //                t.TokenType = token.TokenType;
        //                t.ExpiresIn = token.ExpiresIn;
        //                t.RefreshToken = token.RefreshToken;
        //                t.Error = "";
        //                t.Error_description = "";
        //            }
        //            else
        //            {
        //                t.Flag = false;
        //                t.Message = token.Error;
        //                t.AccessToken = "";
        //                t.TokenType = "";
        //                t.ExpiresIn = 0;
        //                t.RefreshToken = "";
        //                t.Error = token.Error;
        //                t.Error_description = token.Error_description;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            t.Flag = false;
        //            t.Message = ex.Message.ToString();
        //            t.AccessToken = "";
        //            t.TokenType = "";
        //            t.ExpiresIn = 0;
        //            t.RefreshToken = "";
        //            t.Error = ex.Message.ToString();
        //            t.Error_description = ex.Message.ToString();
        //        }
        //        lToken.Add(t);


        //    }
        //    return Json((new { flag = t.Flag, message = t.Message, data = lToken }), JsonRequestBehavior.AllowGet);
        //}

        //public class pAuth
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }

        //}

        //[System.Web.Http.HttpPost]
        //public ActionResult GetmToken([FromBody]pAuth p)
        //{
        //    string baseAddress = "http://" + System.Configuration.ConfigurationManager.AppSettings["baseurl"];
        //    //string strpSalt = AesOperation.DecryptString(ConfigurationManager.AppSettings["encrypt"], User.Identity.GetpSalt());
        //    Token t = new Token();
        //    List<Token> lToken = new List<Token>();
        //    using (var client = new HttpClient())
        //    {
        //        var form = new Dictionary<string, string>
        //       {
        //           {"grant_type", "password"},
        //           {"username", p.username},
        //           {"password",p.password},
        //       };

        //        try
        //        {
        //            var tokenResponse = client.PostAsync(baseAddress + "/token", new FormUrlEncodedContent(form)).Result;
        //            //var token = t1LBf6zWdVYz9oN1Puctivrt8CLk6kburAP  
        //            var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
        //            //t = token;
        //            if (string.IsNullOrEmpty(token.Error))
        //            {
        //                t.Flag = true;
        //                t.Message = "Succesfully";
        //                t.AccessToken = token.AccessToken;
        //                t.TokenType = token.TokenType;
        //                t.ExpiresIn = token.ExpiresIn;
        //                t.RefreshToken = token.RefreshToken;
        //                t.Error = "";
        //                t.Error_description = "";
        //            }
        //            else
        //            {
        //                t.Flag = false;
        //                t.Message = token.Error;
        //                t.AccessToken = "";
        //                t.TokenType = "";
        //                t.ExpiresIn = 0;
        //                t.RefreshToken = "";
        //                t.Error = token.Error;
        //                t.Error_description = token.Error_description;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            t.Flag = false;
        //            t.Message = ex.Message.ToString();
        //            t.AccessToken = "";
        //            t.TokenType = "";
        //            t.ExpiresIn = 0;
        //            t.RefreshToken = "";
        //            t.Error = ex.Message.ToString();
        //            t.Error_description = ex.Message.ToString();
        //        }
        //        lToken.Add(t);


        //    }
        //    return Json((new { flag = t.Flag, message = t.Message, data = lToken }), JsonRequestBehavior.AllowGet);
        //}
    }
}
