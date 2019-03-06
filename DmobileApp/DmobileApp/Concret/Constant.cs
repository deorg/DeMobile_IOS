using System;
using System.Collections.Generic;
using System.Text;

namespace DmobileApp.Concret
{
    public static class Constant
    {
        public static class WebService
        {
            public static class Production
            {
                public const string Host = "http://35.197.153.92";
                public const string Port = "80";
                public static class Api
                {
                    public static class User
                    {
                        public static string getSms = "/api/customer/sms?id=";
                        public static string getProfile = "/api/customer/profile?id=";
                    }
                }
            }
        }
    }
}
