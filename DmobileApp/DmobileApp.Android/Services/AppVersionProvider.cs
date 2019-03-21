using System;
using DmobileApp.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionProvider))]
namespace DmobileApp.Droid.Services
{
    public class AppVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var context = Android.App.Application.Context;
                var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);

                return $"{info.VersionName}.{info.VersionCode.ToString()}";
            }
        }
    }
}
