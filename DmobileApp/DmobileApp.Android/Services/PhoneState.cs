using System;
using Android;
using DmobileApp.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneState))]
namespace DmobileApp.Droid.Services
{
    public class PhoneState
    {
        public void getPhoneNumber()
        {
            //const string permission = Manifest.Permission.ReadPhoneState;

            ////if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            //if (ShouldShowRequestPermissionRationale(permission))
            //{
            //    //TODO change the message to show the permissions name
            //    Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
            //    return;
            //}
        }
    }
}
