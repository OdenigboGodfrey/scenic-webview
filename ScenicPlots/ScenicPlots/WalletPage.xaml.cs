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
	public partial class WalletPage : ContentPage
	{
        static WebViewSource URL = App.BaseURL + App.Wallet;

        static WebView _WebViewer = new WebView();

        private static Label AndroidLoadingLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, Text = "Loading.....", IsVisible = true };

        public WalletPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            _GifHolder.Source = new HtmlWebViewSource
            {
                Html = ContainerPage.html
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android) _GifHolder.IsVisible = false;

            ContainerPage.IsFromThisPage = true;
            if (Device.RuntimePlatform == Device.iOS) _ParentStack.Margin = new Thickness(0, DependencyService.Get<IStatusBar>().GetHeight(), 0, 0);

            _Error.Source = ImageSource.FromResource("ScenicPlots.Assets.error404.png", typeof(WalletPage));

            if (_ParentStack.Children.Count >= 2) ContainerPage.CheckConnection(URL, _Frame, _WebViewer, _ParentStack, _Error, _GifHolder, AndroidLoadingLabel);

            _WebViewer.Source = URL;
        }


        private void _TryAgain_Clicked(object sender, EventArgs e)
        {
            ContainerPage.CheckConnection(URL, _Frame, _WebViewer, _ParentStack, _Error, _GifHolder);
        }

    }
}