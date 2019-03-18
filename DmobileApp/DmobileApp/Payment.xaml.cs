using System;
using System.Collections.Generic;
using DmobileApp.Model;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class Payment : ContentPage
    {
        public double balance { get; } = 24234234;
        public Payment(contract_data contract)
        {
            InitializeComponent();
            BindingContext = contract;

            //var banks = Services.Payment.getBanks();
            //listBanks.ItemsSource = banks.data;
        }
    }
}
