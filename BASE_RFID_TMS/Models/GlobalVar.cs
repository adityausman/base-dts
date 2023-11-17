using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BASE_RFID_TMS.Models
{
    public class GlobalVar
    {
        public class cResponUpload
        {
            public Boolean flag { get; set; }
            public System.Guid id { get; set; }
            public string message { get; set; }
            public string status { get; set; }
        }
        public class cResponTyreDirectly
        {
            public Boolean flag { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public int condition { get; set; }
        }
        public class cRespon
        {
            public Boolean flag { get; set; }
            public string message { get; set; }
            public string status { get; set; }
        }
        public class ClsResponReceive
        {
            public Boolean flag { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public string receive_no { get; set; }
        }

        public class ClsResponReceiveWitManualSN
        {
            public Boolean flag { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public string receive_no { get; set; }
            public string date_receive { get; set; }
        }
        public class ClsResponDistribution
        {
            public Boolean flag { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public string distribution_no { get; set; }
        }



        public static string SetReplaceLineNo_Origin(string param)
        {
            string StrLine_NO = param.Replace("/", "@@");

            return StrLine_NO;
        }

        public static string SetReplaceLineNo(string param)
        {
            string StrLine_NO = param.Replace("@@", "/");

            return StrLine_NO;
        }

        public static string GetParam(string param, int index)
        {
            string[] partsFromString = param.Split(
                new string[] { "@@" }, StringSplitOptions.None);

            return partsFromString[index];
        }
    }
}