using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherApp.ViewModel.ValueConverter
{
    class BoolToRainConverter : IValueConverter
    {
        private const string _isRainingText = "Currently raining";
        private const string _isNotRainingText = "Currently not raining";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool)value;

            if(isRaining)  
                return _isRainingText; 

            return _isNotRainingText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string)value;

            if(isRaining == _isRainingText)
                return true;
            
            return false;
        }
    }
}
