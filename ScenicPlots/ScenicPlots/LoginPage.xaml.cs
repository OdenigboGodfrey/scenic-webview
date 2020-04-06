using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScenicPlots
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SignUpPush(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new SlidePage1());
            }
            catch { }
        }

        private void Passwordpush(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new PasswordPage());
            }
            catch
            {

            }
        }

        private async void _logmein(object sender, EventArgs e)
        {

        }

        private async void DoPost(String Username, String Password)
        {
            try
            {
                var FormPostData = new List<KeyValuePair<String, String>>();
                FormPostData.Add(new KeyValuePair<string, string>("user", Username));
                FormPostData.Add(new KeyValuePair<string, string>("password", Password));
                FormUrlEncodedContent Data = new FormUrlEncodedContent(FormPostData);

                String ResponseJson = "";

                var Client = new HttpClient();
                Client.BaseAddress = new Uri(App.BaseURL);

                var res = await Client.PostAsync(Client.BaseAddress + App.Signin, Data);
                var DecodedJson = JObject.Parse(res.ToString());

                if (Convert.ToBoolean(DecodedJson["status"]))
                {
                    await Navigation.PushAsync(new ContainerPage());
                }
                else
                {
                    await DisplayAlert("Alert", DecodedJson["message"].ToString(), "Okay");
                }

            }
            catch
            {
                
            }
            
        }
    }
}