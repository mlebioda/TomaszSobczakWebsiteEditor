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
    public class ReplaceAdjustCommand : ICommand
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

            if (this.ViewModel.Teksty.Items.Count < 1)
                return;

            for(int i = 0; i <  this.ViewModel.Teksty.Items.Count; i++)
            {
                if (this.ViewModel.Teksty.Items[i].Tag.Equals(this.ViewModel.SelectedTextTag))
                    this.ViewModel.Teksty.Items[i].AdjustI = this.ViewModel.SelectedAdjust;
            }

        }

        public ReplaceAdjustCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
