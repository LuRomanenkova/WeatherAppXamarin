using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Weather.WeatherData;

namespace Weather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCityPage : ContentPage
    {
        public AddCityPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                cityList.ItemsSource = null;
            }

            else
            {
                cityList.ItemsSource = StaticVariables.cities.Where(x => x.name.StartsWith(e.NewTextValue));
            }

        }

        private void cityList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var info = e.Item as Spetial_city;
                StaticVariables.SelectedCity = info.name;

                if (!StaticVariables.historyList.Any(x => x.name == info.name && x.country == info.country))
                {
                    StaticVariables.historyList.Add(info);

                    var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var fileName = Path.Combine(path, StaticVariables.jsonHistoryStore);

                    string data = JsonConvert.SerializeObject(StaticVariables.historyList);
                    File.WriteAllText(fileName, data);
                }
            }
            else
            {
                StaticVariables.SelectedCity = "";
            }
        }
    }
}