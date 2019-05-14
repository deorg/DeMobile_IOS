using System;
using System.Collections.Generic;
using DmobileApp.ViewModels;
using Xamarin.Forms;

namespace DmobileApp
{
    public partial class ViewImage : ContentPage
    {
        public ViewImage(string url, string cust_name)
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Title = cust_name;
            BackgroundColor = Color.Black;
            Content =
                new PinchToZoomContainer
                {
                    Content = new Image { Source = ImageSource.FromUri(new Uri(url)) }
                };
                }
    }
}
