using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherVM()
        {
            LoadDataInDesignerMode();
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
                _selectedCity = value;
                OnPropertyChanged();
            }
        }

        public async Task MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCitiesAsync(CityQuery);
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
                        Metric = new Units
                        {
                            Value = 21,
                        }
                    }
                };
            }
        }
    }
}
