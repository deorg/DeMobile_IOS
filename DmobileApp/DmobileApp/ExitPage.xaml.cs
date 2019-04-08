using System;
using System.Collections.Generic;
using DmobileApp.Model;
using DmobileApp.Services;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class ExitPage : ContentPage
    {
        private profile_data _profile;
        private string _device_id, _serialSim, _version;
        public ExitPage(profile_data Profile, string device_id, string serialSim, string version)
        {
            InitializeComponent();
            _profile = Profile;
            _device_id = device_id;
            _serialSim = serialSim;
            _version = version;
        }
        protected async override void OnAppearing()
        {
            var answer = await DisplayAlert("", "ต้องการยืนยันการออกจาก Application หรือไม่?", "ตกลง", "ยกเลิก");
            if (answer)
            {
                User.logout(_profile.CUST_NO);
                Application.Current.MainPage = new NavigationPage(new Register(_device_id, _serialSim, _version));
                //Navigation.RemovePage(this);
            }
            base.OnAppearing();
        }
    }
}
