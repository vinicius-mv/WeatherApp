using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SearchCommand SearchCommand { get; }

        public WeatherVM()
        {
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();

            this.LoadDataInDesignerMode();
        }

        private string _cityQuery;

        public string CityQuery
        {
            get { return _cityQuery; }
            set
            {
                _cityQuery = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions _currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return _currentConditions; }
            set
            {
                _currentConditions = value;
                OnPropertyChanged();
            }
        }

        private City _selectedCity;

        public City SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                if(value == null) { return; } 

                _selectedCity = value;
                OnPropertyChanged();
                GetCurrentConditions();
            }
        }

        private async void GetCurrentConditions()
        {
            CityQuery = string.Empty;

            if(SelectedCity == null) { return; }

            CurrentConditions = await TryToGetCurrentConditions(SelectedCity.Key);
            Cities.Clear();
        }

        private async Task<CurrentConditions> TryToGetCurrentConditions(string key)
        {
            try
            {
                return await AccuWeatherHelper.GetCurrentConditionAsync(key);
            }
            catch (Exception ex)
            {
                _selectedCity = null;
                throw new Exception($"{ex.GetType()}: {ex.Message}");
            }
        }

        public async Task MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCitiesAsync(CityQuery);

            Cities.Clear();
            cities.ForEach(c => Cities.Add(c));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadDataInDesignerMode()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly Cloud",
                    Temperature = new Temperature
                    {
                        Metric = new Units() { Value = "21" }
                    }
                };
            }
        }
    }
}
