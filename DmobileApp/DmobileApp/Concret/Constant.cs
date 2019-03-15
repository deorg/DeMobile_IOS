﻿using System;
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
                        public const string identify = "api/authen/identify?";
                        public const string register = "api/authen/register";
                        public const string getSms = "api/customer/sms?id=";
                        public const string sendSms = "api/customer/sendmessage";
                        public const string getProfile = "api/customer/profile?id=";
                        public const string getContract = "api/customer/contract?id=";
                    }
                }
            }
            public static class Development
            {
                public const string Host = "http://localhost:58465";
                public const string Port = "80";
                public static class User
                {
                    public const string identify = "/api/authen/identify?";
                    public const string register = "api/authen/register";
                    public const string getSms = "api/customer/sms?id=";
                    public const string getProfile = "api/customer/profile?id=";
                }
            }
        }
    }
}
