using System;
using System.Collections.Generic;
using DmobileApp.Model;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class LoanPage : ContentPage
    {
        public LoanPage(profile_data profile)
        {
            InitializeComponent();
            this.Title = profile.CUST_NAME;
        }
    }
}
