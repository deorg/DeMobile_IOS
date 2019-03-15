﻿using DmobileApp.Concret;
using DmobileApp.Model;
using DmobileApp.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DmobileApp
{
    public partial class App : Application
    {
        
        public App(string deviceId="", string simSerial="")
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new ChatSms());
            var resIdentify = User.identify(deviceId, simSerial);
            if (resIdentify != null)
            {
                if (resIdentify.code == 200)
                    MainPage = new NavigationPage(new Mainpage(resIdentify.data));
                else
                    MainPage = new NavigationPage(new Register(deviceId, simSerial));
            }
            else
            {
                DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์");
            }

            //MainPage = new NavigationPage(new ListSms(119954));
            //var profile = User.identify(deviceId, simSerial);
            //if (profile.code == 200)
            //{
            //    if(profile.data != null)
            //        MainPage = new NavigationPage(new ListSms());
            //    else
            //        MainPage = new NavigationPage(new ListSms());
            //}            
            //else
            //    MainPage = new NavigationPage(new ListSms());
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
