using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DmobileApp.Concret;
using DmobileApp.Model;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Profile : ContentPage
	{
        private string Host = Constant.WebService.Production.Host;
        private string getProfile = Constant.WebService.Production.Api.User.getProfile;
		public Profile ()
		{
			InitializeComponent ();
            loadData();
		}
        public async void loadData()
        {
            var content = "";
            HttpClient client = new HttpClient();
            var RestURL = $"{Host}{getProfile}119954";
            client.BaseAddress = new Uri(RestURL);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(RestURL);
            content = await response.Content.ReadAsStringAsync();
            var Items = JsonConvert.DeserializeObject<m_profile>(content);
            BindingContext = Items.data;
        }
	}
}