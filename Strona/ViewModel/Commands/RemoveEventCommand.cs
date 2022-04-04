using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Strona.Model;
using Strona.ViewModel.Commands.Helpers;
using System.IO;

namespace Strona.ViewModel.Commands
{
    public class RemoveEventCommand : ICommand
    {
        public JsonVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.Events.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {

            if (ViewModel.Events.Count > ViewModel.EventSelectedIndex && ViewModel.EventSelectedIndex >= 0)
            {
                ViewModel.Events.RemoveAt(ViewModel.EventSelectedIndex);
                 ViewModel.EventSelectedIndex = -1;
                ViewModel.EventSelectedItem = new EventItem("", "");

            }
        }



        public RemoveEventCommand(JsonVM viewModel)
        {
            this.ViewModel = viewModel;
        }
    }
}
