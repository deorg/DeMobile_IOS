using System;
using System.Collections.Generic;
using System.Diagnostics;
using DmobileApp.Model;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class PaymentView : ContentPage
    {
        private profile_data _profile;
        private string _deviceId, _serialSim, _version;
        public PaymentView(string url, profile_data profile, string deviceId, string serialSim, string version)
        {
            InitializeComponent();
            Title = "หน้าชำระเงิน";
            _profile = profile;
            _deviceId = deviceId;
            _serialSim = serialSim;
            _version = version;
            bankView.Source = url;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            progress.ProgressTo(0.9, 900, Easing.SpringIn);
        }
        void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            Title = "Loading.....";
        }

        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            Title = string.Empty;
            progress.IsVisible = false;
            Debug.WriteLine("Web display ===========> " + e.Url);
            if (e.Url == "http://35.197.153.92/Success/Redirect")
            {
                Application.Current.MainPage = new NavigationPage(new Mainpage(_profile, _deviceId, _serialSim, _version));
            }
        }
    }
}
