using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using static Android.Provider.Settings;
using Android.Telephony;
using Android.Support.V4.App;
using Android;
using Android.Support.V4.Content;
using Android.Support.Design.Widget;
using Java.Lang;

namespace DmobileApp.Droid
{
    [Activity(Label = "DmobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string deviceId = string.Empty;
            string serialSim = string.Empty;
            try
            {
                var context = Application.Context;
                deviceId = Secure.GetString(context.ContentResolver, Secure.AndroidId);

                //string serialSim = "8966051405494794566";
                var tMgr = (TelephonyManager)ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
                serialSim = tMgr.SimSerialNumber;
                LoadApplication(new App(deviceId, serialSim));
            }
            catch
            {
                LoadApplication(new App(deviceId, serialSim));
            }
        }
    }

}