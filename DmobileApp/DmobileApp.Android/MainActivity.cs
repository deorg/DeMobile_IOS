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
using Java.Util;
using System.Threading.Tasks;

namespace DmobileApp.Droid
{
    [Activity(Label = "DmobileApp", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private string phone_number = string.Empty;
        private string deviceId = string.Empty;
        private string versionName = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;


            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //string deviceId = string.Empty;

            try
            {
                var context = Application.Context;
                deviceId = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                versionName = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
                TryToGetPermissions();
                //try
                //{
                //    var tMgr = (TelephonyManager)ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
                //    phone_number = tMgr.Line1Number;
                //}
                //catch
                //{
                //    phone_number = string.Empty;
                //}
                // ActionBar.SetIcon(Resource.Drawable.logo);

                //LoadApplication(new App(deviceId, "2222222222", versionName, ""));
            }
            catch (System.Exception ex)
            {
                // TryToGetPermissions();
                Console.WriteLine(ex.Message);
            }
        }
        #region RuntimePermissions

        /*async Task*/
        void TryToGetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                /*await*/
                GetPermissionsAsync();
                return;
            }
        }
        const int RequestLocationId = 0;

        readonly string[] PermissionsGroupLocation =
            {
                            //TODO add more permissions
                            Manifest.Permission.ReadPhoneNumbers,
                            Manifest.Permission.ReadPhoneState,
             };
        /*async Task*/
        void GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.ReadPhoneState;

            if (CheckSelfPermission(permission) == (int)Android.Content.PM.Permission.Granted)
            {
                //TODO change the message to show the permissions name
                Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                //set alert for executing the task
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("ขออนุญาตเข้าถึงข้อมูลเครื่อง");
                alert.SetMessage("ทางบริษัท โดเมสดิค ลิสซิง ขออนุญาติเข้าถึงข้อมูลหมายเลขโทรศัพท์ของลูกค้าเพื่อใช้ในการยืนยันตัวตน");
                alert.SetPositiveButton("อนูญาตเข้าถึงข้อมูล", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroupLocation, RequestLocationId);
                });

                alert.SetNegativeButton("ไม่อนุญาต", (senderAlert, args) =>
                {
                    Toast.MakeText(this, "ไม่อนุญาต", ToastLength.Short).Show();
                    this.FinishAffinity();
                });

                Dialog dialog = alert.Create();
                dialog.SetCanceledOnTouchOutside(false);
                dialog.Show();


                return;
            }

            RequestPermissions(PermissionsGroupLocation, RequestLocationId);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Android.Content.PM.Permission.Granted)
                        {
                            Toast.MakeText(this, "ข้อมูลหมายเลขโทรศัพท์ได้รับการอนุญาตแล้ว", ToastLength.Long).Show();
                            //var context = Application.Context;
                            //var deviceId = Secure.GetString(context.ContentResolver, Secure.AndroidId);

                            //string serialSim = "8966051405494794566";
                            var tMgr = (TelephonyManager)ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
                            phone_number = tMgr.Line1Number;
                            //LoadApplication(new App(deviceId, serialSim));
                            LoadApplication(new App(deviceId, "2222222222", versionName, phone_number));
                        }
                        else
                        {
                            //Permission Denied :(
                            Toast.MakeText(this, "ข้อมูลหมายเลขโทรศัพท์ไม่ได้รับการอนุญาต!", ToastLength.Long).Show();
                            this.FinishAffinity();
                            LoadApplication(new App(deviceId, "2222222222", versionName, ""));
                        }
                    }
                    break;
            }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #endregion
    }

}