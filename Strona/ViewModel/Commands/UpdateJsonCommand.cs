using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Strona.Model;
using Strona.ViewModel.Commands.Helpers;
using System.IO;
using System.Collections.ObjectModel;

namespace Strona.ViewModel.Commands
{
    public class UpdateJsonCommand : ICommand
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

            //this.ViewModel.Path = CommandsHelpers.OpenFolder();
            string dirPath = CommandsHelpers.OpenFolder();
            
            if (!Directory.Exists(dirPath))
            {
                System.Windows.MessageBox.Show("Wybrany folder nie istnieje");
                return;
            }

            string jsonPath = CommandsHelpers.OpenFile();
            if (!File.Exists(jsonPath))
            {
                System.Windows.MessageBox.Show("Wybrany folder nie istnieje");
                return;
            }

            NavItem<Image> dirFotografia = new NavItem<Image>(ItemType.image);
            NavItem<Image> dirObrazy = new NavItem<Image>(ItemType.image);
            NavItem<TextItem> dirTeksty = new NavItem<TextItem>(ItemType.text);
            NavItem<Image> dirArtysta = new NavItem<Image>(ItemType.image);

            NavItem<Image> jsonFotografia = new NavItem<Image>(ItemType.image);
            NavItem<Image> jsonObrazy = new NavItem<Image>(ItemType.image);
            NavItem<TextItem> jsonTeksty = new NavItem<TextItem>(ItemType.text);
            NavItem<Image> jsonArtysta = new NavItem<Image>(ItemType.image);
            ObservableCollection<EventItem> jsonEvents = new ObservableCollection<EventItem>();


            ///1. Sprawdzenie czy w pliku json są ścieżki do plików, których nie ma w folderze



            CommandsHelpers.GetItemsFromDirectory(dirPath, ref dirObrazy, ref dirFotografia, ref dirTeksty, ref dirArtysta);
            CommandsHelpers.GetItemsFromFile(jsonPath, ref jsonObrazy, ref jsonFotografia, ref jsonTeksty, ref jsonArtysta, ref jsonEvents );



            CommandsHelpers.UpdateItemsFromDirectory(
                ref dirFotografia,
                ref dirObrazy,
                ref dirTeksty,
                ref dirArtysta,
                jsonFotografia,
                jsonObrazy,
                jsonTeksty,
                jsonArtysta
                );


            this.ViewModel.Teksty = dirTeksty;
            this.ViewModel.Fotografia = dirFotografia;
            this.ViewModel.Obrazy = dirObrazy;
            this.ViewModel.Artysta = dirArtysta;
            this.ViewModel.Events = jsonEvents;

            this.ViewModel.TextTags = CommandsHelpers.getTextTags(dirTeksty);
            if (this.ViewModel.TextTags.Count < 0)
                this.ViewModel.SelectedTextTag = this.ViewModel.TextTags[0];
        }



        public UpdateJsonCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
