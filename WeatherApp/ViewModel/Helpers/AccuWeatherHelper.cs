using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public static class AccuWeatherHelper
    {
        private const string _baseUrl = "http://dataservice.accuweather.com/";
        private const string _apiKey = "gvUG6aGmoQLHPVEtgOnutJUphIZFixh3";
        private const string _getCurrentConditionsEndPoint = "currentconditions/v1/{0}?apikey={1}";
        private const string _getLocationsEndPoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";

        public static async Task<List<City>> GetCitiesAsync(string query)
        {
            string url = _baseUrl + string.Format(_getLocationsEndPoint, _apiKey, query);

            List<City> cities;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }
            return cities;
        }

        public static async Task<CurrentCondition> GetCurrentConditionAsync(string cityKey)
        {
            string url = _baseUrl + string.Format(_getCurrentConditionsEndPoint, cityKey, _apiKey);

            List<CurrentCondition> currentConditions;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                currentConditions = JsonConvert.DeserializeObject<List<CurrentCondition>>(json);
            }
            return currentConditions.FirstOrDefault();
        }
    }
}
