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
                //public const string Host = "http://2ca23fb1.ngrok.io";
                public const string Port = "80";
                public static class Api
                {
                    public static class User
                    {
                        public const string identify = "api/authen/identify?";
                        public const string identify2 = "api/authen/identify2?";
                        public const string register = "api/authen/register";
                        public const string getSms = "api/customer/sms?id=";
                        public const string getSmsOffset = "api/customer/sms/offset?";
                        public const string getSmsOffset2 = "api/customer/sms/offset2";
                        public const string sendSms = "api/customer/sendmessage";
                        public const string getProfile = "api/customer/profile?id=";
                        public const string getContract = "api/customer/contract?id=";
                        public const string logout = "api/authen/logout?cust_no=";
                        public const string checkPhone = "api/authen/checkphone?phone=";
                    }
                    public static class Payment
                    {
                        public const string getBanks = "api/payment/getchannel";
                        public const string newOrder = "api/payment/newpayment2";
                    }
                }
            }
            public static class Development
            {
                public const string Host = "http://localhost:8080";
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
