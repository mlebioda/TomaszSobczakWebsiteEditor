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
    public class OpenJsonCommand : ICommand
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
            if (Helpers.CommandsHelpers.AskForOverwriteFile(ViewModel) == false)
                return;

            
            this.ViewModel.Path = CommandsHelpers.OpenFile();

            if (!File.Exists(this.ViewModel.Path))
                return;


            NavItem<Image> fotografia = new NavItem<Image>(ItemType.image);
            NavItem<Image> obrazy = new NavItem<Image>(ItemType.image);
            NavItem<TextItem> teksty = new NavItem<TextItem>(ItemType.text);
            NavItem<Image> artysta = new NavItem<Image>(ItemType.image);
            List<EventItem> eventItem = new List<EventItem>();


            CommandsHelpers.GetItemsFromFile(this.ViewModel.Path, ref obrazy, ref fotografia, ref teksty, ref artysta, ref eventItem);

            this.ViewModel.Teksty = teksty;
            this.ViewModel.Fotografia = fotografia;
            this.ViewModel.Obrazy = obrazy;
            this.ViewModel.Artysta = artysta;
            this.ViewModel.Events = eventItem;

            this.ViewModel.TextTags = CommandsHelpers.getTextTags(teksty);
            if (this.ViewModel.TextTags.Count < 0)
                this.ViewModel.SelectedTextTag = this.ViewModel.TextTags[0];
        }

        public OpenJsonCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
