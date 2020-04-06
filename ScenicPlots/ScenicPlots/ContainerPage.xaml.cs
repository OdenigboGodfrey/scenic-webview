using FFImageLoading.Forms;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace ScenicPlots
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContainerPage : ContentPage
	{
        private static String Signout = App.Signout, Signin_Web = App.Signin_Web;
        public static bool IsFromThisPage = false, StopLoading = false;
        public static  String Email = "", Password = "";
        public static String src, 
            backgroundColor = "White", 
            html = "";
        private static String URL = App.BaseURL;

        public static WebView _WebViewer = new WebView { Source = URL };

        private static int imageWidth = 50, imageHeight = 50;

        

        private static Label AndroidLoadingLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, Text = "Loading.....", IsVisible = true };

        public ContainerPage ()
		{
			InitializeComponent ();

            NavigationPage.SetHasNavigationBar(this, false);
            //_Gif.Source = ImageSource.FromResource("ScenicPlots.Assets.loader.gif");

            if (Device.RuntimePlatform == Device.Android) _GifHolder.IsVisible = false;

            src = (Device.RuntimePlatform == Device.UWP) ? @"Assets\loader.gif" : "loader.gif";
            html = $"<body\"><img src=\"{src}\" alt=\"Loading...\" width=\"{imageWidth}\" height=\"{imageHeight}\" style=\"width: 50px; height: 50px;margin-top: 50%;margin-left:40%;\"/></body>";
            _GifHolder.Source = new HtmlWebViewSource
            {
                Html = html
            };
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsFromThisPage = true;

            if (Device.RuntimePlatform == Device.Android) _GifHolder.IsVisible = false;

            int IsSignedIn = DependencyService.Get<ICookieHandler>().CookieHandler(false);

            if (IsSignedIn == 0)
            {
                
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password)) URL = App.BaseURL + Signin_Web + "?user=" + Email + "&password=" + Password;
            }


            if (Device.RuntimePlatform == Device.iOS) _ParentStack.Margin = new Thickness(0, DependencyService.Get<IStatusBar>().GetHeight(), 0, 0);

            _Error.Source = ImageSource.FromResource("ScenicPlots.Assets.error404.png", typeof(ContainerPage));

            if (_ParentStack.Children.Count == 2) CheckConnection(URL, _Frame, _WebViewer, _ParentStack,_Error, _GifHolder);
        }



        public static void CheckConnection(WebViewSource Url,Frame ErrorFrame,WebView PageWebView, StackLayout ParentStack, Image _Error, WebView GifHolder, Label AndroidLoadingLabel = null)
        {
            try
            {
                ///init check if app is connected to the internet
                if (CrossConnectivity.IsSupported && !CrossConnectivity.Current.IsConnected)
                {
                    ErrorFrame.IsVisible = true;
                    if (PageWebView != null && ParentStack.Children.Contains(PageWebView))
                    {
                        PageWebView.IsVisible = false;
                        ParentStack.Children.Remove(PageWebView);
                        PageWebView = null;

                        _Error.IsVisible = true;
                    }
                }
                else
                {
                    ErrorFrame.IsVisible = false;

                    if (ParentStack.Children.Count > 2 && Device.RuntimePlatform != Device.Android)
                    {
                        ParentStack.Children.Remove(PageWebView);
                    }
                    else if (ParentStack.Children.Count > 2 && Device.RuntimePlatform == Device.Android)
                    {
                        ParentStack.Children.Remove(PageWebView);
                        if (ParentStack.Children.Count > 2) ParentStack.Children.Add(AndroidLoadingLabel);
                    }

                    PageWebView = new WebView
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Source = Url,
                    };

                    PageWebView.Navigated += (sender, e) =>
                    {
                        GifHolder.IsVisible = false;
                        if (AndroidLoadingLabel != null && ParentStack.Children.Contains(AndroidLoadingLabel)) AndroidLoadingLabel.IsVisible = false;

                        PageWebView.IsVisible = true;
                    };

                    PageWebView.Navigating += (sender, e) =>
                    {
                        PageWebView.IsVisible = false;
                        //if (Device.RuntimePlatform != Device.Android)
                        //{
                        //    GifHolder.IsVisible = true;
                        //}
                        //else
                        //{
                        //    if (AndroidLoadingLabel != null && ParentStack.Children.Contains(AndroidLoadingLabel))
                        //    {
                        //        AndroidLoadingLabel.IsVisible = true;
                        //    }
                        //    else
                        //    {
                        //        if (AndroidLoadingLabel != null) ParentStack.Children.Add(AndroidLoadingLabel);
                        //    }
                        //}
                        //secondary check during navigation
                        if (!CrossConnectivity.Current.IsConnected)
                        {
                            ErrorFrame.IsVisible = true;

                            if (PageWebView != null && ParentStack.Children.Contains(PageWebView))
                            {
                                PageWebView = null;
                                _Error.IsVisible = true;
                            }
                        }



                        //App.Current.MainPage.DisplayAlert("Alert", e.Url + "\n" + App.BaseURL + "/signin\n" + NavPage.PageInstance.CurrentPage + "\n" + NavPage.PageInstance.Children[2], "Ok");

                        if (e.Url.Contains(Signout))
                        {
                            e.Cancel = true;
                            DependencyService.Get<ICookieHandler>().CookieHandler(true);
                            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        }
                        else if (e.Url == App.BaseURL & !IsFromThisPage)
                        {
                            e.Cancel = true;

                            if (NavPage.PageInstance.CurrentPage == NavPage.PageInstance.Children[1]) StoriesPage.IsFreshLoad = false; ///refresh stories page on return to page

                            if (NavPage.PageInstance.CurrentPage != NavPage.PageInstance.Children[0]) NavPage.PageInstance.CurrentPage = NavPage.PageInstance.Children[0]; // switch pages
                        }
                        else if ((e.Url.Contains(App.Signin) && !e.Url.Contains(Signin_Web)) && (NavPage.PageInstance.CurrentPage != NavPage.PageInstance.Children[2]))
                        {
                            e.Cancel = true;
                            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        }
                        else if (e.Url.Contains(App.Signup) && !IsFromThisPage)
                        {
                            e.Cancel = true;
                            App.Current.MainPage.Navigation.PushAsync(new SlidePage1());
                        }
                    };

                    if (ParentStack.Children.Count < 3) ParentStack.Children.Add(PageWebView);
                }
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("Alert", "An error occured.", "Okay");
            }
            
        }

        public static void CheckConnection(WebViewSource Url, Frame ErrorFrame, WebView PageWebView, StackLayout ParentStack, Image _Error, WebView GifHolder)
        {
            try
            {
                ///init check if app is connected to the internet
                if (CrossConnectivity.IsSupported && !CrossConnectivity.Current.IsConnected)
                {
                    ErrorFrame.IsVisible = true;
                    if (PageWebView != null && ParentStack.Children.Contains(PageWebView))
                    {
                        PageWebView.IsVisible = false;
                        ParentStack.Children.Remove(PageWebView);
                        PageWebView = null;

                        _Error.IsVisible = true;
                    }
                }
                else
                {
                    ErrorFrame.IsVisible = false;

                    if (ParentStack.Children.Count >= 2) { ParentStack.Children.Remove(PageWebView); }

                    PageWebView = new WebView
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Source = Url,
                    };

                    PageWebView.Navigated += (sender, e) =>
                    {
                        GifHolder.IsVisible = false;
                        StopLoading = true;


                        PageWebView.IsVisible = true;
                    };

                    PageWebView.Navigating += (sender, e) =>
                    {
                        PageWebView.IsVisible = false;

                        //if (Device.RuntimePlatform != Device.Android)
                        //{
                        //    GifHolder.IsVisible = true;
                        //}
                        //else
                        //{
                        //    StopLoading = false;
                        //    FakeAnimate(AndroidLoadingLabel);
                        //}
                        //secondary check during navigation
                        if (!CrossConnectivity.Current.IsConnected)
                        {
                            ErrorFrame.IsVisible = true;

                            if (PageWebView != null && ParentStack.Children.Contains(PageWebView))
                            {
                                PageWebView = null;
                                _Error.IsVisible = true;
                            }
                        }



                        //App.Current.MainPage.DisplayAlert("Alert", e.Url + "\n" + App.BaseURL + "/signin\n" + NavPage.PageInstance.CurrentPage + "\n" + NavPage.PageInstance.Children[2], "Ok");

                        if (e.Url.Contains(Signout))
                        {
                            e.Cancel = true;
                            DependencyService.Get<ICookieHandler>().CookieHandler(true);
                            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        }
                        else if (e.Url == App.BaseURL & !IsFromThisPage)
                        {
                            e.Cancel = true;

                            if (NavPage.PageInstance.CurrentPage == NavPage.PageInstance.Children[1]) StoriesPage.IsFreshLoad = false; ///refresh stories page on return to page

                            if (NavPage.PageInstance.CurrentPage != NavPage.PageInstance.Children[0]) NavPage.PageInstance.CurrentPage = NavPage.PageInstance.Children[0]; // switch pages
                        }
                        else if ((e.Url.Contains(App.Signin) && !e.Url.Contains(Signin_Web)) && (NavPage.PageInstance.CurrentPage != NavPage.PageInstance.Children[2]))
                        {
                            e.Cancel = true;
                            App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                        }
                        else if (e.Url.Contains(App.Signup) && !IsFromThisPage)
                        {
                            e.Cancel = true;
                            App.Current.MainPage.Navigation.PushAsync(new SlidePage1());
                        }
                    };

                    if (ParentStack.Children.Count < 3) ParentStack.Children.Add(PageWebView);
                }
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("Alert", "An error occured.", "Okay");
            }

        }

        private void _TryAgain_Clicked(object sender, EventArgs e)
        {
            CheckConnection(URL, _Frame, _WebViewer, _ParentStack, _Error, _GifHolder, AndroidLoadingLabel);
        }

        private static void FakeAnimate(Label LabelToChange)
        {
            ///pointer_counter determines the number of pointers to be loaded
            int pointer_counter = 0;
            Device.StartTimer(TimeSpan.FromSeconds(1), delegate
            {
                
                if (pointer_counter > 5)
                {
                    pointer_counter = 0;
                }

                AndroidLoadingLabel.Text = "Loading";

                for (int i = 0; i < pointer_counter; i++)
                {
                    AndroidLoadingLabel.Text += ".";
                }

                pointer_counter++;

                if (!StopLoading) LabelToChange.IsVisible = false;

                return !StopLoading;
            });
        }
    }
}