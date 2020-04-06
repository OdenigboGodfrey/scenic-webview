using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScenicPlots
{
    public partial class MainPage : ContentPage
    {
        Image splashimage;

        public MainPage()
        {
            //InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //var sub = new AbsoluteLayout
            //{
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            splashimage = new Image
            {
                Source = "ScenicPlots.jpg",
                Aspect = Aspect.AspectFill
            };
            //AbsoluteLayout.SetLayoutFlags(splashimage, AbsoluteLayoutFlags.PositionProportional);
            //AbsoluteLayout.SetLayoutBounds(splashimage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            ////InitializeComponent ();
            //sub.Children.Add(splashimage);

            this.BackgroundColor = Color.FromHex("#fff");

            var ssub = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            ssub.Children.Add(splashimage);

            this.Content = ssub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashimage.ScaleTo(1, 1500);
            //await splashimage.ScaleTo(0.9, 1500, Easing.Linear);
            //await splashimage.ScaleTo(150, 1200,Easing.Linear); 
            //Application.Current.MainPage = new NavigationPage(new LandingPage());

            NavigationPage NavPage = new NavigationPage(new LandingPage());
            Application.Current.MainPage = NavPage;
        }

        public void _suu(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LandingPage());
        }
    }
}
