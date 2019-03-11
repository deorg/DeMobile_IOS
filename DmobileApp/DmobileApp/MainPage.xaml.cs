using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mainpage : TabbedPage
    {
        private string Host = Constant.WebService.Production.Host;
        private string Identify = Constant.WebService.Production.Api.User.identify;
        public Mainpage()
        {
            InitializeComponent();
            var navigationPage = new NavigationPage(new Profile());
            navigationPage.Title = "Profile";
            Children.Add(navigationPage);
            navigationPage = new NavigationPage(new ListSms(119954));
            navigationPage.Title = "SMS";
            Children.Add(navigationPage);
        }
        private async Task<bool> identifyAsync()
        {
            string content = "";
            HttpClient client = new HttpClient();
            var RestURL = $"{Host}{Identify}serial_sim=8966051405494794566&deviceId=fb5f26643085c22b&app_version=1";
            client.BaseAddress = new Uri(RestURL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(RestURL);
            content = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<m_profile>(content);
            if (profile.code == 200)
                return true;
            else
                return false;
        }
    }
}