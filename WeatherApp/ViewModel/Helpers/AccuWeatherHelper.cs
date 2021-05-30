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
    // Service Class : Contain Business logic.
    // Helper Class : this class is one type of reusable component.
    public static class AccuWeatherHelper
    {
        // Location
        // http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=gvUG6aGmoQLHPVEtgOnutJUphIZFixh3&q=San%20Francisco

        // Current Conditions
        // http://dataservice.accuweather.com/currentconditions/v1/347629?apikey=gvUG6aGmoQLHPVEtgOnutJUphIZFixh3


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
