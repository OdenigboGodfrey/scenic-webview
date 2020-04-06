using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ScenicPlots
{
    public partial class App : Application
    {
        //Base URL
        public static String BaseURL = "http://scenic.7gmfhpxemg.us-west-2.elasticbeanstalk.com/";
        
        //URLS
        public static String Signin_Web = "signin_web/";
        public static String Signin = "signin/";
        public static String Signup = "signup/";
        public static String Signout = "signout/";
        public static String MyProfile = "myprofile/";
        public static String Recover_Password = "recover/";
        public static String Wallet = "mywallet/";
        public static String Stories = "mystories/";
        public static String MySettings = "mysettings/";

        public App()
        {
            InitializeComponent();
            NavigationPage NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
        }

        public App(string databaseconnection)
        {
            InitializeComponent();
            NavigationPage NavPage = new NavigationPage(new MainPage());
            MainPage = NavPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MainPage = new NavigationPage(new MainPage());
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

    //interfaces for dependency service

    public interface IStatusBar
    {
        int GetHeight();
    }

    public interface ICookieHandler
    {
        int CookieHandler(bool DoClear);
    }

    public interface ICloseAndroid
    {
        void CloseApplication();
    };
}
