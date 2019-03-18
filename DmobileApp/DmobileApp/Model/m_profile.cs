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
    }
}
