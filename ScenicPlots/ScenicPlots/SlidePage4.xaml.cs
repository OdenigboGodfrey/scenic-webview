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
	public partial class SlidePage4 : ContentPage
	{
		public SlidePage4 ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            _Four.Source = ImageSource.FromResource("ScenicPlots.Assets.four.jpg", typeof(SlidePage4));
        }

        private void Button_Clicked(object sender, EventArgs e)
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