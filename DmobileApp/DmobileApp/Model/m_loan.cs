using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DmobileApp.Model
{
    public class m_contract
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<contract_data> data { get; set; }
    }
    public class contract_data
    {
        [JsonProperty("CON_NO")]
        public string con_no { get; set; }
        [JsonProperty("CUST_NO")]
        public int cust_no { get; set; }
        [JsonProperty("TOT_AMT")]
        public double tot_amt { get; set; }
        [JsonProperty("PAY_AMT")]
        public double pay_amt { get; set; }
        [JsonProperty("PERIOD")]
        public int period { get; set; }
        [JsonProperty("BAL_AMT")]
        public double bal_amt { get; set; }
        [JsonProperty("CON_DATE")]
        public DateTime con_date { get; set; }
        [JsonProperty("DISC_AMT")]
        public double disc_amt { get; set; }
    }
}
