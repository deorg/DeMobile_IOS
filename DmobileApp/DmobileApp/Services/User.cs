using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DmobileApp.Services
{
    public static class User
    {
        private static string Host = Constant.WebService.Production.Host;

        public static m_custPhone preIdentify(string phone)
        {
            string checkPhoneUrl = $"{Constant.WebService.Production.Api.User.checkPhone}{phone}";
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(checkPhoneUrl).Result;
                    var content = response.Content.ReadAsStringAsync().Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var customers = JsonConvert.DeserializeObject<m_custPhone>(content);
                        client.Dispose();
                        return customers;
                    }
                    else
                    {
                        client.Dispose();
                        return null;
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine("pipe => " + e.Message);
                return null;
            }
        }
        public static void logout(int cust_no)
        {
            string logoutUrl = $"{Constant.WebService.Production.Api.User.logout}{cust_no}";
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetAsync(logoutUrl).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    //var profile = JsonConvert.DeserializeObject<m_profile>(content);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    client.Dispose();
                    //    return profile;
                    //}
                    //else
                    //{
                    //    client.Dispose();
                    //    return null;
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static m_profile identify(string deviceId, string serialSim, string version)
        {
            //var ver = double.Parse(version);
            string IdentifyUrl = $"{Constant.WebService.Production.Api.User.identify}serial_sim={serialSim}&deviceId={deviceId}&app_version={version}";
            try
            {
                using (var client = new HttpClient())
                { 
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
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public static m_profile identify2(string deviceId, string serialSim, string brand, string model,  string version, string api_version)
        {
            //var ver = double.Parse(version);
            string IdentifyUrl = $"{Constant.WebService.Production.Api.User.identify2}serial_sim={serialSim}&deviceId={deviceId}&brand={brand}&model={model}&app_version={version}&api_version={api_version}";
            try
            {
                using (var client = new HttpClient())
                {
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
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
        public static m_contract getContract(int cust_no)
        {
            string LoanUrl = Constant.WebService.Production.Api.User.getContract + cust_no.ToString();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(LoanUrl).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                var loan = JsonConvert.DeserializeObject<m_contract>(content);
                if (response.IsSuccessStatusCode)
                {
                    client.Dispose();
                    return loan;
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
        public static async Task<m_sms> getSmsOffsetAsync(int cust_no, int skip = 0, int take = 0)
        {
            string SmsUrl = $"{Constant.WebService.Production.Api.User.getSmsOffset}id={cust_no}&skip={skip}&take={take}";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Host);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(SmsUrl).Result;
                var content = await response.Content.ReadAsStringAsync();
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
        public static m_sms getSmsOffset2(m_smsOffset value)
        {
            string registerUrl = Constant.WebService.Production.Api.User.getSmsOffset2;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string postBody = JsonConvert.SerializeObject(value);
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(registerUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObj = JsonConvert.DeserializeObject<m_sms>(response.Content.ReadAsStringAsync().Result);
                        return responseObj;
                    }
                    return null;
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
        public static m_custMessageRes sendSms(m_custMessage message)
        {
            string sendSmsUrl = Constant.WebService.Production.Api.User.sendSms;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Host);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string postBody = JsonConvert.SerializeObject(message);
                    var content = new StringContent(postBody, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(sendSmsUrl, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObj = JsonConvert.DeserializeObject<m_custMessageRes>(response.Content.ReadAsStringAsync().Result);
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
        public static m_profile register(m_register value)
        {
            string registerUrl = Constant.WebService.Production.Api.User.register;
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
                        var responseObj = JsonConvert.DeserializeObject<m_profile>(response.Content.ReadAsStringAsync().Result);
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
