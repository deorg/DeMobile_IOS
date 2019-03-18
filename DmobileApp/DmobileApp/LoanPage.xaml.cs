using System;
using System.Collections.Generic;
using DmobileApp.Model;
using DmobileApp.Services;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class LoanPage : ContentPage
    {
        public LoanPage(profile_data profile)
        {
            InitializeComponent();
            this.Title = profile.CUST_NAME;

            var loan = User.getContract(profile.CUST_NO);
            if(loan.code == 200)
            {
                loan.data.Add(new contract_data
                {
                    con_no = loan.data[0].con_no,
                    cust_no = loan.data[0].cust_no,
                    tot_amt = loan.data[0].tot_amt,
                    pay_amt = loan.data[0].pay_amt,
                    period = loan.data[0].period,
                    bal_amt = loan.data[0].bal_amt,
                    con_date = loan.data[0].con_date,
                    disc_amt = loan.data[0].disc_amt
                });
                loan.data.Add(new contract_data
                {
                    con_no = loan.data[0].con_no,
                    cust_no = loan.data[0].cust_no,
                    tot_amt = loan.data[0].tot_amt,
                    pay_amt = loan.data[0].pay_amt,
                    period = loan.data[0].period,
                    bal_amt = loan.data[0].bal_amt,
                    con_date = loan.data[0].con_date,
                    disc_amt = loan.data[0].disc_amt
                });
                listContract.ItemsSource = loan.data;
            }
        }
        private void OnSelected_contract(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as contract_data;
            Navigation.PushAsync(new Payment(item));
           // DisplayAlert("Selection", $"You selected {item.con_no}", "OK");
        }
    }
}
