using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DmobileApp.Model
{
    public class m_custMessage
    {
        public int sms010_pk { get; set; }
        public int cust_no { get; set; }
        public string message { get; set; }
    }
    public class m_sms
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<sms_data> data { get; set; } 
    }
    public class m_custMessageRes
    {
        public int code { get; set; }
        public string message { get; set; }
        public sms_data data { get; set; }
    }
    public class sms_data
    {
        [JsonProperty("SMS010_PK")]
        public int sms010_pk { get; set; }
        [JsonProperty("CUST_NO")]
        public int cust_no { get; set; }
        [JsonProperty("CON_NO")]
        public string con_no { get; set; }
        [JsonProperty("SMS_NOTE")]
        public string sms_note { get; set; }
        [JsonProperty("SMS_TIME")]
        public DateTime sms_time { get; set; }
        [JsonProperty("SENDER")]
        public int? sender { get; set; }
        [JsonProperty("SENDER_TYPE")]
        public string sender_type { get; set; }
        [JsonProperty("SMS010_REF")]
        public int? sms010__ref { get; set; }
        [JsonProperty("READ_STATUS")]
        public string read_status { get; set; }
    }
}
