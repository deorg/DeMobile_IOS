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
        private string _deviceId;
        public PaymentView(string url, profile_data profile, string deviceId)
        {
            InitializeComponent();
            _profile = profile;
            _deviceId = deviceId;
            bankView.Source = url;
        }

        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            Debug.WriteLine("Web display ===========> " + e.Url);
            if (e.Url == "http://35.197.153.92/Success/Redirect")
            {
                Application.Current.MainPage = new NavigationPage(new Mainpage(_profile, _deviceId));
                Navigation.RemovePage(this);
            }
        }
    }
}
