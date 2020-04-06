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
	public partial class SlidePage1 : ContentPage
	{
		public SlidePage1 ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void _Slide1_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SlidePage2());
        }

        private void _Register_page(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());

        }
    }
}