using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Weather.WeatherData;

namespace Weather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();

            cityHistoryList.ItemsSource = StaticVariables.historyList;
        }

        private void cityHistoryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var info = e.Item as Spetial_city;
                StaticVariables.SelectedCity = info.name;
            }
            else
            {
                StaticVariables.SelectedCity = "";
            }

        }
    }
}