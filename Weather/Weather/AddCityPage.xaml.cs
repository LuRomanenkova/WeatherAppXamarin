using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCityPage : ContentPage
    {
        public static string SelectedCity = "Moscow";

        RestService _restService;
        public AddCityPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

         void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                SelectedCity = _cityEntry.Text;
                //var weatherdata = await RestService.Get(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                //BindingContext = weatherdata;
            }
            else
            {
                SelectedCity = "Moscow";
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?lang=ru&q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric, units=imperial
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }
    }
}