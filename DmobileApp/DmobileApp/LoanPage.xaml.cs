using System;
using System.Collections.Generic;
using DmobileApp.Model;
using DmobileApp.Services;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class LoanPage : ContentPage
    {
        private profile_data _profile;
        private string _deviceId, _serialSim, _version;
        public LoanPage(profile_data profile, string deviceId, string serialSim, string version)
        {
            InitializeComponent();
            this.Title = profile.CUST_NAME;
            _profile = profile;
            _deviceId = deviceId;
            _serialSim = serialSim;
            _version = version;
            var loan = User.getContract(profile.CUST_NO);
            if(loan.code == 200)
            {
                //listContract.ItemsSource = null;
                listContract.ItemsSource = loan.data;
            }
        }
        //private void OnSelected_contract(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as contract_data;
        //    Navigation.PushAsync(new Payment(item));
        //    // DisplayAlert("Selection", $"You selected {item.con_no}", "OK");
        //}
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<App>(this, "refreshContract", app => {
                var contract = User.getContract(_profile.CUST_NO);
                if (contract.code == 200)
                    listContract.ItemsSource = contract.data;
            });
            base.OnAppearing();
            //base.OnAppearing();
            //var loan = User.getContract(_profile.CUST_NO);
            //if (loan.code == 200)
            //{
            //    listContract.ItemsSource = loan.data;
            //}
        }
        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_profile.PERMIT == "BOTH" || _profile.PERMIT == "PAYMENT")
            {
                var item = e.Item as contract_data;
                //var navigationPage = new NavigationPage(new Payment(_profile, item, _deviceId));
                //navigationPage.Title = "ข้อความ";
                //navigationPage.BarBackgroundColor = Color.White;
                //navigationPage.BarTextColor = Color.Teal;
                //Navigation.PushAsync(navigationPage);
               
                Navigation.PushAsync(new Payment(_profile, item, _deviceId, _serialSim, _version));

                //Application.Current.MainPage = new NavigationPage(navigationPage);
            }
            if (sender is ListView lv) lv.SelectedItem = null;
        }
    }
}
