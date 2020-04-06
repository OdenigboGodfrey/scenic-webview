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
	public partial class LandingPage : ContentPage
	{
        public LandingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }


        protected override void OnAppearing()
        {
            try
            {
                int IsSignedIn = DependencyService.Get<ICookieHandler>().CookieHandler(false);

                if (IsSignedIn > 0)
                {
                    Navigation.PushAsync(new NavPage());
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alert", ex.Message, "Ok");
            }
            

            base.OnAppearing();
        }

        public void _TakeMeToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        public void _logIn_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new LoginPage());
            }
            else
            {
                DisplayAlert("Ooops, my internet!!", "Oops,seems your internet connection is out!!", "Ok");
            }
        }

        public void _signup_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlidePage1());
        }

    }
}