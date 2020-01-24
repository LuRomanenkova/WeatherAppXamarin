using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;
using static Weather.WeatherData;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Weather
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    public static class ParallelActions
    {
        public static List<Spetial_city> cities = new List<Spetial_city>();

        public static async Task AddCitiesData()
        {
            string jsonFileName = "cityData.json";
            
            if (cities.Count == 0)
            {
                var assembly = typeof(AddCityPage).GetTypeInfo().Assembly;
                foreach (var res in assembly.GetManifestResourceNames())
                {
                    if (res.Contains(jsonFileName))
                    {
                        Stream stream = assembly.GetManifestResourceStream(res);

                        using (var reader = new StreamReader(stream))
                        {
                            string json = reader.ReadToEnd();
                            cities = JsonConvert.DeserializeObject<List<Spetial_city>>(json);
                        }
                    }
                }
            }
        }
    }
    public partial class MainPage : MasterDetailPage
    {

        public MainPage()
        {
            InitializeComponent();

            if (ParallelActions.cities.Count == 0)
            {
                InitData();
            }


            masterPage.listView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }

        private async void InitData()
        {
            await ParallelActions.AddCitiesData();
        }
    }
}
