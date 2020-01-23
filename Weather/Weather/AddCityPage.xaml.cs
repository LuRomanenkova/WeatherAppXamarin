﻿using Newtonsoft.Json;
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
        public static string SelectedCity = "Moscow";
        public List<Spetial_city> cities = new List<Spetial_city>();

        public AddCityPage()
        {
            InitializeComponent();
         

            string jsonFileName = "cityData.json";
            try
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

                int index = 0;

                List<Spetial_city> tmp = new List<Spetial_city>(0);

                foreach (var city in cities)
                {
                    tmp.Add(city);
                    index++;
                    if (index == 10)
                    {
                        break;
                    }
                }

                cityList.ItemsSource = tmp;
            }
            catch
            {
                cityList.ItemsSource = new List<Spetial_city>
                {
                    new Spetial_city
                    {
                        name = "New York",
                        country = "USA"
                    }
                };
            }

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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                cityList.ItemsSource = cities;
            }

            else
            {
                cityList.ItemsSource = cities.Where(x => x.name.StartsWith(e.NewTextValue));
            }

        }

        private void cityList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                SelectedCity = e.SelectedItem.ToString();
            }    
        }
    }
}