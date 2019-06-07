using System;
namespace DmobileApp.Model
{
    public class m_smsOffset
    {
        public int cust_no { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string device_id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public double app_version { get; set; }
        public string api_version { get; set; }
    }
}
