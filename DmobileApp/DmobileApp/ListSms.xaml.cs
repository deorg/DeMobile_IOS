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
        private string Host = Constant.WebService.Production.Host;
        private string getSms = Constant.WebService.Production.Api.User.getSms;
        public ListSms()
        {
            InitializeComponent();
            LoadData();
        }
        public async void LoadData()
        {
            var content = "";
            HttpClient client = new HttpClient();
            var RestURL = $"{Host}{getSms}119954";
            client.BaseAddress = new Uri(RestURL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(RestURL);
            content = await response.Content.ReadAsStringAsync();
            var Items = JsonConvert.DeserializeObject<m_sms>(content);
            ListView1.ItemsSource = Items.data;
        }

        private void message_selected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as sms_data;
            DisplayAlert("Selection", $"You selected {item.sms_note}", "OK");
        }
    }
}