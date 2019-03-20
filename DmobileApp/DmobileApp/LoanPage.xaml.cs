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
        private string _deviceId;
        public LoanPage(profile_data profile, string deviceId)
        {
            InitializeComponent();
            this.Title = profile.CUST_NAME;
            _profile = profile;
            _deviceId = deviceId;
            var loan = User.getContract(profile.CUST_NO);
            if(loan.code == 200)
            {
                listContract.ItemsSource = loan.data;
            }
        }
        //private void OnSelected_contract(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as contract_data;
        //    Navigation.PushAsync(new Payment(item));
        //    // DisplayAlert("Selection", $"You selected {item.con_no}", "OK");
        //}

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_profile.PERMIT == "BOTH" || _profile.PERMIT == "PAYMENT")
            {
                var item = e.Item as contract_data;
                Navigation.PushAsync(new Payment(_profile, item, _deviceId));
            }
        }
    }
}
