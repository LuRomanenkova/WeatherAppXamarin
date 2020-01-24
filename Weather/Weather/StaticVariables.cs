using System;
using System.Collections.Generic;
using System.Text;
using static Weather.WeatherData;

namespace Weather
{
    public static class StaticVariables
    {
        public static List<Spetial_city> cities = new List<Spetial_city>();
        public static List<Spetial_city> historyList = new List<Spetial_city>();
        public static string jsonHistoryStore = "historyList.json";
        public static string SelectedCity = "";
        public static string jsonFileName = "cityData.json";
    }
}
