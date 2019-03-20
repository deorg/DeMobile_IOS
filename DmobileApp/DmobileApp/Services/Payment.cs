using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;

namespace DmobileApp.Services
{
    public static class Payments
    {
        private static string Host = Constant.WebService.Production.Host;

        public static m_banks getBanks()
        {
            string getBanksUrl = Constant.WebService.Production.Api.Payment.getBanks;
            using (var client = new HttpClient())
            {
                // var RestURL = $"{Host}{Identify}serial_sim={serialSim}&deviceId={deviceId}&app_version=1";      
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(getBanksUrl).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var banks = JsonConvert.DeserializeObject<m_banks>(content);
                if (response.IsSuccessStatusCode)
                {
                    client.Dispose();
                    return banks;
                }
                else
                {
                    client.Dispose();
                    return null;
                }
            }
        }
        public static m_resOrder newOrder(m_newOrder value)
        {
            string registerUrl = Constant.WebService.Production.Api.Payment.newOrder;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string postBody = JsonConvert.SerializeObject(value);
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(registerUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObj = JsonConvert.DeserializeObject<m_resOrder>(response.Content.ReadAsStringAsync().Result);
                        return responseObj;
                    }
                    else
                        return null;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
