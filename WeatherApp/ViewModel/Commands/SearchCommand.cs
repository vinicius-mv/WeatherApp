using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public SearchCommand(WeatherVM weatherVM)
        {
            this.WeatherVM = weatherVM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public WeatherVM WeatherVM { get; set; }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if(string.IsNullOrEmpty(query)) { return false; }

            return true;
        }

        public async void Execute(object parameter)
        {
            await WeatherVM.MakeQuery();
        }
    }
}
