using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DmobileApp.Model
{
    public class m_banks
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<bank_data> data { get; set; }
    }
    public class bank_data
    {
        [JsonProperty("CHANNEL_ID")]
        public string channel_id { get; set; }
        [JsonProperty("CHANNEL_NAME")]
        public string channel_name { get; set; }
        [JsonProperty("CHANNEL_IMG")]
        public string channel_img { get; set; }
    }
}
