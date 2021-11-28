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
    public class SaveFileCommand : ICommand
    {
        public JsonVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Polecenie dostępne gdy w każdym z typów obiektów są  itemy
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if(
                ViewModel.Fotografia.isValid()
                && ViewModel.Obrazy.isValid()
                && ViewModel.Teksty.isValid())
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            JsonItem jsonItem = new JsonItem(ViewModel.Obrazy, ViewModel.Fotografia, ViewModel.Teksty);
            CommandsHelpers.SaveJson(jsonItem);
            

        }

        public SaveFileCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
