using Plugin.Connectivity;
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
	public partial class SlidePage2 : ContentPage
	{
		public SlidePage2 ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            _Two.Source = ImageSource.FromResource("ScenicPlots.Assets.two.jpg", typeof(SlidePage2));

        }

        private void _Slide3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlidePage3());
        }

        private void _Register_page(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Navigation.PushAsync(new RegisterPage());
            }
            else
            {
                DisplayAlert("Ooops, my internet!!", "Oops,seems your internet connection is out!!", "Ok");
            }
        }
    }
}