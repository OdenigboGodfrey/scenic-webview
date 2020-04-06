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
	public partial class NavPage : TabbedPage
	{
        public static TabbedPage PageInstance;

		public NavPage ()
		{
			InitializeComponent ();

            this.Children[0].Title = "Home";
            this.Children[1].Title = "Stories";
            this.Children[2].Title = "Wallet";
            this.Children[3].Title = "Profile";
            this.Children[4].Title = "More";

            PageInstance = this;

            this.CurrentPageChanged += (sender, e) => 
            {
                if (this.CurrentPage != this.Children[0])
                {
                    ContainerPage.IsFromThisPage = false;
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await DisplayAlert("Confirm Action", "Exit Application?", "Yes", "No");

                    if (result)
                    {
                        DependencyService.Get<ICloseAndroid>().CloseApplication();
                    }        

                });
            }

            return true;
        }
    }

}