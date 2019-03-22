using DmobileApp.Concret;
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
        
        public App(string deviceId="", string simSerial="", string version = "1")
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new ChatSms());
            var resIdentify = User.identify(deviceId, simSerial, version);
            if (resIdentify != null)
            {
                if (resIdentify.code == 200)
                {
                    MainPage = new NavigationPage(new Mainpage(resIdentify.data, deviceId));
                    //MainPage = new NavigationPage(new Mainpage(resIdentify.data, deviceId) { Icon =  });
                    //if (resIdentify.data.PERMIT == "SMS")
                    //    MainPage = new NavigationPage(new ChatSms(resIdentify.data));
                    //else if (resIdentify.data.PERMIT == "PAYMENT")
                    //    MainPage = new NavigationPage(new LoanPage(resIdentify.data));
                    //else
                    //MainPage = new NavigationPage(new Mainpage(resIdentify.data));
                }
                else
                    MainPage = new NavigationPage(new Register(deviceId, simSerial, version));
            }
            else
            {
                MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้", "พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลองเข้าระบบใหม่ในภายหลัง!", "ตกลง");
                //DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลองเข้าระบบใหม่ในภายหลัง");
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
