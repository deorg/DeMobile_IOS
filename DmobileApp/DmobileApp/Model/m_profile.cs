using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DmobileApp.Model
{
    public class m_profile
    {
        public int code { get; set; }
        public string message { get; set; }
        public profile_data data { get; set; }
    }
    public class profile_data
    {

        [JsonProperty("CUST_NO")]
        public int CUST_NO { get; set; }
        [JsonProperty("CUST_NAME")]
        public string CUST_NAME { get; set; }
        [JsonProperty("CITIZEN_NO")]
        public string CITIZEN_NO { get; set; }
        [JsonProperty("TEL")]
        public string TEL { get; set; }
        [JsonProperty("PERMIT")]
        public string PERMIT { get; set; }
        [JsonProperty("CHAT")]
        public string CHAT { get; set; }
        [JsonProperty("APP_VERSION")]
        public double APP_VERSION { get; set; } = 2.8;
        [JsonProperty("BROADCAST")]
        public broadcast_data BROADCAST { get; set; }
    }
    public class broadcast_data
    {
        [JsonProperty("note")]
        public string NOTE { get; set; }
        [JsonProperty("start_time")]
        public DateTime START_TIME { get; set; }
        [JsonProperty("end_time")]
        public DateTime END_TIME { get; set; }
        [JsonProperty("created_time")]
        public DateTime CREATED_TIME { get; set; }
    }
}
