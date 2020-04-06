using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScenicPlots
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordPage : ContentPage
	{
        private static String URL = App.BaseURL + App.Recover_Password;
        public static WebView _WebViewer = new WebView { Source = URL };

        private static Label AndroidLoadingLabel = new Label { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, Text = "Loading.....", IsVisible = true };

        public PasswordPage ()
		{
			InitializeComponent ();

            _GifHolder.Source = new HtmlWebViewSource
            {
                Html = ContainerPage.html
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.Android) _GifHolder.IsVisible = false;

            if (Device.RuntimePlatform == Device.iOS) _Frame.Margin = new Thickness(10, DependencyService.Get<IStatusBar>().GetHeight(), 10, 0);

            _Error.Source = ImageSource.FromResource("ScenicPlots.Assets.error404.png", typeof(StoriesPage));

            if (_ParentStack.Children.Count >= 2) ContainerPage.CheckConnection(URL, _Frame, _WebViewer, _ParentStack, _Error, _GifHolder, AndroidLoadingLabel);
        }



        private void _TryAgain_Clicked(object sender, EventArgs e)
        {
            ContainerPage.CheckConnection(URL, _Frame, _WebViewer, _ParentStack, _Error, _GifHolder, AndroidLoadingLabel);
        }
    }
}