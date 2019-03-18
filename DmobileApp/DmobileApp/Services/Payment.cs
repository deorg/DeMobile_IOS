using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;

namespace DmobileApp.Services
{
    public static class Payment
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
    }
}
