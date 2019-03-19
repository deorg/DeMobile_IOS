using DmobileApp.Model;
using DmobileApp.Concret;
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
    public partial class ListSms : ContentPage
    {
        //private string Host = Constant.WebService.Production.Host;
        //private string getSms = Constant.WebService.Production.Api.User.getSms;
        //private m_profile _profile;
        public ListSms(profile_data profile)
        {
            InitializeComponent();
            this.Title = profile.CUST_NAME;
            //_profile = profile;
            if (profile != null)
            {
                var items = Services.User.getSms(profile.CUST_NO);
                if (items.code == 200)
                {
                    DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
                    if (items.data.Count != 0)
                        listSms.ItemsSource = items.data;
                }
                else
                    DependencyService.Get<IMessage>().longAlert(items.message);
            }
            else
                DependencyService.Get<IMessage>().longAlert("ไม่พบข้อมูลของคุณในระบบ SMS");
        }
        //public async void LoadData()
        //{
        //    var content = "";
        //    HttpClient client = new HttpClient();
        //    var RestURL = $"{Host}{getSms}119954";
        //    client.BaseAddress = new Uri(RestURL);
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    HttpResponseMessage response = await client.GetAsync(RestURL);
        //    content = await response.Content.ReadAsStringAsync();
        //    var Items = JsonConvert.DeserializeObject<m_sms>(content);
        //    listSms.ItemsSource = Items.data;
        //}

        private void message_selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as sms_data;
            DisplayAlert("Selection", $"You selected {item.sms_note}", "OK");
        }
    }
}