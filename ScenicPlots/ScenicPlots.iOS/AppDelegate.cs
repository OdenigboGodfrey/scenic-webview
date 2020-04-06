using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ScenicPlots.iOS.StatusBar))]
[assembly: Dependency(typeof(ScenicPlots.iOS.CookieHandlerClass))]
namespace ScenicPlots.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            DependencyService.Register<IStatusBar>();
            DependencyService.Register<ICookieHandler>();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }

    class StatusBar : IStatusBar
    {
        public int GetHeight()
        {
            return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
        }
    }

    class CookieHandlerClass : ICookieHandler
    {
        public int CookieHandler(bool DoClear)
        {
            int Counter = 0;
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;

            if (DoClear) NSUrlCache.SharedCache.RemoveAllCachedResponses();

            foreach (var cookie in CookieStorage.Cookies)
            {
                if (cookie.Domain.Equals(App.BaseURL) || App.BaseURL.Contains(cookie.Domain))
                {
                    if (DoClear) CookieStorage.DeleteCookie(cookie);
                    Counter++;
                }
            }

            return Counter;
        }
    }
}
