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
    public class UnselectCommand : ICommand
    {
        public JsonVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if(ViewModel.EventSelectedItem==null)
            {
                ViewModel.EventSelectedItem = new EventItem("", "");
            }

            if ( //ViewModel.EventSelectedItem != null &&
                ViewModel.EventSelectedItem.Name != "" && ViewModel.EventSelectedItem.Date != "")
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            ViewModel.EventSelectedIndex = -1;
            ViewModel.EventSelectedItem = new EventItem("","");

        }



        public UnselectCommand(JsonVM viewModel)
        {
            this.ViewModel = viewModel;
        }
    }
}
