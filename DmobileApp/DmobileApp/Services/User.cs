using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DmobileApp.Services
{
    public static class User
    {
        public static m_profile identify(string deviceId, string serialSim)
        {
            string Host = Constant.WebService.Production.Host;
            string IdentifyUrl = $"{Constant.WebService.Production.Api.User.identify}serial_sim={serialSim}&deviceId={deviceId}&app_version=1";
            using (var client = new HttpClient())
            {
               // var RestURL = $"{Host}{Identify}serial_sim={serialSim}&deviceId={deviceId}&app_version=1";      
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(IdentifyUrl).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var profile = JsonConvert.DeserializeObject<m_profile>(content);
                if (response.IsSuccessStatusCode)
                {
                    client.Dispose();
                    return profile;
                }
                else
                {
                    client.Dispose();
                    return null;
                }
            }
        }
        public static m_sms getSms(int cust_no)
        {
            string Host = Constant.WebService.Production.Host;
            string SmsUrl = Constant.WebService.Production.Api.User.getSms+cust_no.ToString();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(SmsUrl).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var sms = JsonConvert.DeserializeObject<m_sms>(content);
                if (response.IsSuccessStatusCode)
                {
                    client.Dispose();
                    return sms;
                }
                else
                {
                    client.Dispose();
                    return null;
                }
            }
        }
        public static m_profile register(m_register value)
        {
            string Host = Constant.WebService.Production.Host;
            string registerUrl = Constant.WebService.Production.Api.User.register;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string postBody = JsonConvert.SerializeObject(value);
                var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                var response = client.PostAsync(registerUrl, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseObj = JsonConvert.DeserializeObject<m_profile>(response.Content.ReadAsStringAsync().Result);
                    return responseObj;
                }
                else
                    return null;
            }
        }
    }
}
