using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mainpage : TabbedPage
    {
        //private string Host = Constant.WebService.Production.Host;
        //private string Identify = Constant.WebService.Production.Api.User.identify;
        public Mainpage(profile_data profile, string deviceId, string serialSim, string version)
        {
            InitializeComponent();

            //var connection = new HubConnection("http://dba2df14.ngrok.io/signalr");
            //var notifyHub = connection.CreateHubProxy("NotificationHub");
            //connection.Start();

            //notifyHub.On("connect", async (string message) =>
            //{
            //    Debug.WriteLine(message);
            //    Debug.WriteLine(message);
            //    Debug.WriteLine(message);
            //    Debug.WriteLine(message);
            //    Debug.WriteLine(message);
            //    Debug.WriteLine(message);
            //    await notifyHub.Invoke("registerContext", deviceId);
            //});
            //notifyHub.On("notify", (string message) =>
            //{
            //    Debug.WriteLine($"pipe => New notification is {message}");
            //});
            //connection.Closed += () =>
            //{
            //    Debug.WriteLine("pipe => Connection closed......");
            //    //await Task.Delay(new Random().Next(0, 5) * 1000);
            //    //await connection.Start();
            //};
            //connection.Reconnected += () =>
            //{
            //    Debug.WriteLine("pipe => Connection reconnected.......");
            //};
            //connection.Reconnecting += () => {
            //    Debug.WriteLine("pipe => Connection reconnecting.......");
            //};

            this.Title = string.Empty;
            var navigationPage = new NavigationPage(new ChatSms(profile, version));
            //this.BarTextColor = Color.Teal;
            navigationPage.Title = "ข้อความ";
            navigationPage.BarBackgroundColor = Color.White;
            navigationPage.BarTextColor = Color.Teal;
            //navigationPage.Icon = "message.png";
            Children.Add(navigationPage);
            navigationPage = new NavigationPage(new LoanPage(profile, deviceId, serialSim, version));
            navigationPage.Title = "สัญญา";
            //navigationPage.Icon = "assignment.png";
            navigationPage.BarBackgroundColor = Color.White;
            navigationPage.BarTextColor = Color.Teal;
            Children.Add(navigationPage);
            if (Device.RuntimePlatform == Device.iOS)
            {
                navigationPage = new NavigationPage(new ExitPage(profile, deviceId, serialSim, version));
                navigationPage.Title = "Logout";
               // navigationPage.Icon = "exit2.png";
                navigationPage.BarBackgroundColor = Color.White;
                navigationPage.BarTextColor = Color.Teal;
                Children.Add(navigationPage);
            }
        }
        //private async Task<bool> identifyAsync()
        //{
        //    string content = "";
        //    HttpClient client = new HttpClient();
        //    var RestURL = $"{Host}{Identify}serial_sim=8966051405494794566&deviceId=fb5f26643085c22b&app_version=1";
        //    client.BaseAddress = new Uri(RestURL);
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await client.GetAsync(RestURL);
        //    content = await response.Content.ReadAsStringAsync();
        //    var profile = JsonConvert.DeserializeObject<m_profile>(content);
        //    if (profile.code == 200)
        //        return true;
        //    else
        //        return false;
        //}

    }
}