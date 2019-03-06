using System;
using System.Collections.Generic;
using System.Text;

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
        public int CUST_NO { get; set; }
        public string CUST_NAME { get; set; }
        public string CITIZEN_NO { get; set; }
        public string TEL { get; set; }
        public string PERMIT { get; set; }
    }
}
