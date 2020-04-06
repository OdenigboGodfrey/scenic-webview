using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScenicPlots.Droid.CookieHandlerClass))]
[assembly: Dependency(typeof(ScenicPlots.Droid.CloseAndroidApplication))]
namespace ScenicPlots.Droid
{
    [Activity(Label = "ScenicPlots", Icon = "@drawable/appicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            LoadApplication(new App());
        }
    }

    public class CookieHandlerClass : ICookieHandler
    {

        int ICookieHandler.CookieHandler(bool DoClear)
        {
            var cookieManager = CookieManager.Instance;
            
            if (DoClear) cookieManager.RemoveAllCookie();

            return (cookieManager.HasCookies) ? 1 : 0;
        }
    }

    public class CloseAndroidApplication : ICloseAndroid
    {
        public void CloseApplication()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}