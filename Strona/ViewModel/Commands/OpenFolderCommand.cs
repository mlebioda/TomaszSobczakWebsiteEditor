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
    public class OpenFolderCommand : ICommand
    {
        public JsonVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
            this.ViewModel.Path = CommandsHelpers.OpenFolder();
            NavItem fotografia = new NavItem();
            NavItem obrazy = new NavItem();
            NavItem teksty = new NavItem();

            if (!Directory.Exists(this.ViewModel.Path))
                return; 

            CommandsHelpers.GetItems(this.ViewModel.Path, ref obrazy, ref fotografia, ref teksty);

            this.ViewModel.Teksty = teksty;
            this.ViewModel.Fotografia = fotografia;
            this.ViewModel.Obrazy = obrazy;

        }

        public OpenFolderCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
